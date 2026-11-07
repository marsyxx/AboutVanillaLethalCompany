// Shared source: compiled into each mod with __NS__ replaced by the mod's namespace (see tools/build.ps1), so the
// mods stay completely independent of each other.
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using BepInEx.Bootstrap;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace __NS__
{
    internal enum OptionKind { Toggle, Slider, Choice }

    // One row of the settings page. The page only knows this shape; the mod keeps its own settings as the source of truth.
    internal sealed class Option
    {
        public string Group, Label, Tooltip, Unit = "";
        public OptionKind Kind;
        public float Min, Max, Step;
        public string[] Choices;
        public string[] Labels;      // optional display texts for the choices (default: the choice names, prettified)
        public Func<float> Get;      // toggle: 0/1, slider: value, choice: index
        public Action<float> Set;
    }

    // Adds one button to the game's Settings panel (main menu and pause menu) that opens a scrollable page of options.
    // The page is built by cloning the game's own labels, slider, dropdown and button so it looks native, and every
    // clone is detached from the game's settings logic. Any failure is logged once and the vanilla panel stays untouched.
    internal static class SettingsMenu
    {
        private static BaseUnityPlugin host;
        private static string buttonText, pageTitle, objectPrefix;
        private static Func<IList<Option>> source;
        private static Option inlineOption;                 // inline mode: one dropdown placed in the main Settings screen, no page
        private static Func<bool> lockWhen;                 // page mode: when true the game's own Graphics controls are locked
        private static readonly List<Action> shownRefreshers = new List<Action>();
        private static readonly HashSet<int> extended = new HashSet<int>();
        private static bool warned;

        // texts and dropdowns that follow the game language; they are rewritten from the mod's dictionaries each time a page
        // opens, because XUnity only re-translates components that are *active*, and a closed page is not.
        private static Color accent = new Color32(0xBE, 0x49, 0x49, 0xFF);   // the game's heading red (read from its Settings headings when possible)
        private sealed class LocText { public TMP_Text Text; public string English; }
        private sealed class LocDropdown { public TMP_Dropdown Dropdown; public string[] English; }
        private static readonly List<LocText> localizedTexts = new List<LocText>();
        private static readonly List<LocDropdown> localizedDropdowns = new List<LocDropdown>();

        // Page mode: a button inside the Graphics box of the Settings screen opens a scrollable page of options.
        // lockVanillaGraphics: when it returns true, the game's own Graphics controls are greyed out and locked.
        public static void Install(BaseUnityPlugin plugin, string button, string title, Func<IList<Option>> options, Func<bool> lockVanillaGraphics = null)
        {
            host = plugin; buttonText = button; pageTitle = title; source = options; lockWhen = lockVanillaGraphics;
            Start(plugin);
        }

        // Inline mode: a single labelled dropdown placed in the main Settings screen (between the Controls and Graphics boxes).
        public static void InstallInline(BaseUnityPlugin plugin, Option option)
        {
            host = plugin; inlineOption = option;
            Start(plugin);
        }

        private static void Start(BaseUnityPlugin plugin)
        {
            objectPrefix = plugin.Info.Metadata.Name;
            Loc.Init(objectPrefix);
            SceneManager.sceneLoaded += (s, m) => host.StartCoroutine(InjectSoon());
            host.StartCoroutine(InjectSoon());
        }

        private static IEnumerator InjectSoon()
        {
            for (var i = 0; i < 6; i++)
            {
                yield return new WaitForSeconds(i == 0 ? 0.5f : 2f);
                try { InjectAll(); } catch (Exception e) { Warn(e); }
            }
        }

        private static void Warn(object what)
        {
            if (warned) return;
            warned = true;
            Plugin.Log.LogWarning("Settings page not added (the vanilla Settings screen is untouched): " + what);
        }

        private static void InjectAll()
        {
            foreach (var t in Resources.FindObjectsOfTypeAll<Transform>())
            {
                if (t == null || t.name != "SettingsPanel" || !t.gameObject.scene.IsValid()) continue;
                if (!extended.Add(t.GetInstanceID())) continue;
                if (t.Find(objectPrefix + "Button") != null || t.Find(objectPrefix + "Dropdown") != null) continue;
                if (inlineOption != null) ExtendInline(t); else Extend(t);
            }
        }

        // ---- building ------------------------------------------------------------------------------------------

        private static void Extend(Transform panel)
        {
            var captionTemplate = panel.Find("BrightnessSetting/Text (1)");
            var sliderTemplate = panel.Find("BrightnessSetting/Slider");
            var dropdownTemplate = panel.Find("FramerateCap");
            var backTemplate = panel.Find("BackButton");
            // "Set to default" has the animated hover highlight; "Change keybinds" only tints its colour
            var buttonTemplate = panel.Find("SetToDefault") ?? panel.Find("KeybindingsButton");
            var scrollbarTemplate = dropdownTemplate != null ? dropdownTemplate.Find("Template/Scrollbar") : null;
            if (captionTemplate == null || sliderTemplate == null || dropdownTemplate == null || buttonTemplate == null || backTemplate == null)
            {
                Warn("the game's Settings panel has a different layout than expected (a template element is missing)");
                return;
            }

            var created = new List<GameObject>();
            try
            {
                var options = source();
                if (options == null || options.Count == 0) return;

                // The page is a full-screen overlay under the *root canvas* with its own sub-canvas: scrolling then only
                // re-batches the page, not the whole game UI, and it covers the entire screen (not just the panel's area).
                var parentCanvas = panel.GetComponentInParent<Canvas>();
                var rootCanvas = parentCanvas != null ? parentCanvas.rootCanvas : null;
                var page = new GameObject(objectPrefix + "Page", typeof(RectTransform), typeof(Canvas), typeof(GraphicRaycaster), typeof(CanvasRenderer), typeof(Image));
                created.Add(page);
                page.transform.SetParent(rootCanvas != null ? rootCanvas.transform : panel, false);
                var pageRect = page.GetComponent<RectTransform>();
                pageRect.anchorMin = Vector2.zero; pageRect.anchorMax = Vector2.one;
                pageRect.offsetMin = new Vector2(-400f, -400f); pageRect.offsetMax = new Vector2(400f, 400f);   // oversize: no uncovered edges at any aspect ratio
                var subCanvas = page.GetComponent<Canvas>();
                if (rootCanvas != null) subCanvas.additionalShaderChannels = rootCanvas.additionalShaderChannels;   // TextMeshPro needs the same channels
                // No background of its own: whatever the game draws behind its Settings screen (the red-brown tint over the
                // ship in the pause menu, plain black in the main menu) stays, so the page looks like the game's own.
                // The page only has to catch the pointer and hide the vanilla controls (see VanillaHider).
                var bg = page.GetComponent<Image>();
                bg.sprite = null;
                bg.color = new Color(0f, 0f, 0f, 0f);
                bg.raycastTarget = true;
                var header = panel.Find("Headers/Graphics");
                var headerText = header != null ? header.GetComponent<TMP_Text>() : null;
                if (headerText != null) accent = new Color(headerText.color.r, headerText.color.g, headerText.color.b, 1f);   // the game's own heading red

                var refreshers = new List<Action>();
                BuildTitle(page.transform, captionTemplate);
                var scroll = BuildScroll(page.transform, options, captionTemplate, sliderTemplate, dropdownTemplate, scrollbarTemplate, refreshers);
                page.AddComponent<ScrollForwarder>().Target = scroll;     // the wheel scrolls the list wherever the cursor is on the page
                var hider = page.AddComponent<VanillaHider>();
                hider.Panel = panel;

                // "Back": same screen position as the vanilla Back button
                var back = CloneButton(backTemplate, page.transform, objectPrefix + "Back", "> Back", true);
                var backRect = (RectTransform)back.transform;
                backRect.anchorMin = backRect.anchorMax = new Vector2(0.5f, 0.5f);
                backRect.pivot = ((RectTransform)backTemplate).pivot;
                var backLocal = page.transform.InverseTransformPoint(backTemplate.position);
                backRect.localPosition = new Vector3(backLocal.x, backLocal.y, 0f);
                var backSfx = CaptureSfx(backTemplate);
                back.GetComponent<Button>().onClick.AddListener(() => { PlaySfx(backSfx); Deselect(); page.SetActive(false); });

                // opener button on the vanilla panel
                var open = CloneButton(buttonTemplate, panel, objectPrefix + "Button", "> " + buttonText);
                created.Add(open);
                var openRect = (RectTransform)open.transform;
                PlaceOpener(panel, openRect, FindGraphicsLayout(panel));
                var openSfx = CaptureSfx(buttonTemplate);
                open.GetComponent<Button>().onClick.AddListener(() =>
                {
                    PlaySfx(openSfx);
                    Deselect();
                    try { Relocalize(); } catch (Exception e) { Warn(e); }
                    foreach (var r in refreshers) { try { r(); } catch (Exception e) { Warn(e); } }
                    page.SetActive(true);
                    page.transform.SetAsLastSibling();
                    var es = UnityEngine.EventSystems.EventSystem.current;
                    if (es != null)
                        UnityEngine.EventSystems.ExecuteEvents.Execute(open, new UnityEngine.EventSystems.PointerEventData(es), UnityEngine.EventSystems.ExecuteEvents.pointerExitHandler);
                });

                // The game's own dropdowns keep their red "selected" frame until something else is clicked; clear it after a choice.
                foreach (var dd in panel.GetComponentsInChildren<TMP_Dropdown>(true))
                    if (dd.name != objectPrefix + "Dropdown" && dd.transform.parent == panel) dd.onValueChanged.AddListener(_ => Deselect());

                var watcher = panel.gameObject.AddComponent<PanelWatcher>();
                watcher.Page = page;
                watcher.Panel = panel;
                hider.AfterRestore = () => ApplyLock(panel);          // an option may have changed while the page was open

                // Objects made with "new GameObject" are on the Default layer; the in-game UI camera only draws the UI layer,
                // so without this the page is laid out correctly but never drawn (the "empty window" in the pause menu).
                var uiLayer = (rootCanvas != null ? rootCanvas.gameObject : panel.gameObject).layer;
                foreach (var t in page.GetComponentsInChildren<Transform>(true)) t.gameObject.layer = uiLayer;
                page.SetActive(false);
                ApplyLock(panel);
            }
            catch (Exception e)
            {
                foreach (var go in created) if (go != null) UnityEngine.Object.Destroy(go);
                Warn(e);
            }
        }

        // ---- placing things in the game's Settings screen --------------------------------------------------------

        private sealed class GraphicsLayout { public RectTransform Box; public Transform Controls, Terrain, Motion, Pixel, Lighting; public RectTransform ControlsBox; public float LineStep; }

        private static Rect WorldRect(RectTransform rt)
        {
            var c = new Vector3[4];
            rt.GetWorldCorners(c);
            return new Rect(c[0].x, c[0].y, c[2].x - c[0].x, c[2].y - c[0].y);
        }

        private static RectTransform BoxAround(Transform headers, Transform marker)
        {
            if (headers == null || marker == null) return null;
            RectTransform found = null;
            var point = new Vector2(marker.position.x, marker.position.y);
            foreach (Transform t in headers)
            {
                var r = t as RectTransform;
                if (r != null && t.name.StartsWith("BoxFrame") && WorldRect(r).Contains(point)) found = r;
            }
            return found;
        }

        // Where the game's Graphics box and its rows are, found by position rather than by name of the box.
        private static GraphicsLayout FindGraphicsLayout(Transform panel)
        {
            try
            {
                var headers = panel.Find("Headers");
                var title = headers != null ? headers.Find("Graphics") : null;
                var terrain = panel.Find("TerrainDetail"); var motion = panel.Find("MotionBlur"); var pixel = panel.Find("PixelRes"); var light = panel.Find("AdvancedLighting");
                if (title == null || terrain == null || motion == null || pixel == null || light == null) return null;
                var box = BoxAround(headers, title);
                if (box == null) return null;
                var controls = panel.Find("ControlsOptions/Controls");
                return new GraphicsLayout { Box = box, Terrain = terrain, Motion = motion, Pixel = pixel, Lighting = light, Controls = controls, ControlsBox = BoxAround(headers, controls), LineStep = terrain.position.y - motion.position.y };
            }
            catch (Exception) { return null; }
        }

        // The opener sits at the bottom of the Graphics box, under "Indirect lighting"; the box grows downwards to hold it.
        private static void PlaceOpener(Transform panel, RectTransform open, GraphicsLayout layout)
        {
            if (layout == null || layout.LineStep <= 0f)
            {
                open.anchoredPosition = new Vector2(-48f, -162f);     // layout not recognised: the old spot beside the other buttons
                FitHighlight(open, 240f);
                return;
            }
            var column = WorldRect((RectTransform)layout.Terrain);
            open.pivot = new Vector2(0f, 0.5f);
            FitHighlight(open, column.width / panel.lossyScale.x);
            open.position = new Vector3(column.xMin, layout.Lighting.position.y - 0.62f * layout.LineStep, open.position.z);

            var growWorld = WorldRect(layout.Box).yMin - (open.position.y - 0.45f * layout.LineStep);
            if (growWorld > 0f)
            {
                var grow = growWorld / layout.Box.lossyScale.y;
                layout.Box.sizeDelta += new Vector2(0f, grow);
                layout.Box.anchoredPosition -= new Vector2(0f, grow * (1f - layout.Box.pivot.y));
            }
        }

        // Inline mode: one labelled dropdown cloned from the game's own, between the Controls and Graphics boxes.
        private static void ExtendInline(Transform panel)
        {
            var dropdownTemplate = panel.Find("FramerateCap");
            if (dropdownTemplate == null) { Warn("the game's Settings panel has a different layout than expected (no dropdown to copy)"); return; }
            GameObject go = null;
            try
            {
                go = CloneClean(dropdownTemplate, panel, objectPrefix + "Dropdown");
                var dropdown = go.GetComponent<TMP_Dropdown>();
                var names = inlineOption.Choices;
                var english = names.Select(n => Pretty(n)).ToArray();
                dropdown.ClearOptions();
                dropdown.AddOptions(english.ToList());
                localizedDropdowns.Add(new LocDropdown { Dropdown = dropdown, English = english });
                IgnoreForXua(dropdown.captionText);
                var captionTransform = go.transform.Find("Label2");
                var caption = captionTransform != null ? captionTransform.GetComponent<TMP_Text>() : null;
                if (caption != null)
                {
                    caption.text = inlineOption.Label;
                    localizedTexts.Add(new LocText { Text = caption, English = inlineOption.Label });
                    IgnoreForXua(caption);
                }
                Action refresh = () => dropdown.SetValueWithoutNotify(Mathf.Clamp((int)Mathf.Round(inlineOption.Get()), 0, names.Length - 1));
                dropdown.onValueChanged.AddListener(i => { try { inlineOption.Set(i); } catch (Exception e) { Warn(e); } Deselect(); });
                refresh();
                shownRefreshers.Add(refresh);

                var rect = (RectTransform)go.transform;
                var layout = FindGraphicsLayout(panel);
                if (layout != null && layout.ControlsBox != null)
                {
                    var left = WorldRect(layout.ControlsBox).xMax; var right = WorldRect(layout.Box).xMin;
                    var width = Mathf.Min(rect.sizeDelta.x, (right - left) / panel.lossyScale.x - 6f);
                    rect.sizeDelta = new Vector2(width, rect.sizeDelta.y);
                    rect.pivot = new Vector2(0.5f, rect.pivot.y);
                    rect.position = new Vector3((left + right) / 2f, layout.Pixel.position.y, rect.position.z);   // same row as "Pixel Resolution", clear of the microphone box above
                }
                else rect.anchoredPosition = new Vector2(-48f, -126f);      // layout not recognised: a free spot near the bottom buttons

                var watcher = panel.gameObject.AddComponent<PanelWatcher>();
                watcher.Panel = panel;
            }
            catch (Exception e)
            {
                if (go != null) UnityEngine.Object.Destroy(go);
                Warn(e);
            }
        }

        // Runs every time the Settings screen opens.
        internal static void PanelShown(Transform panel)
        {
            try { Relocalize(); } catch (Exception e) { Warn(e); }
            foreach (var r in shownRefreshers) { try { r(); } catch (Exception e) { Warn(e); } }
            ApplyLock(panel);
        }

        // While the Graphics+ page is in use, the game's own Graphics controls are greyed out and cannot be changed.
        private static readonly string[] lockedNames = { "TerrainDetail", "MotionBlur", "PixelRes", "AdvancedLighting" };

        internal static void ApplyLock(Transform panel)
        {
            if (lockWhen == null || panel == null) return;
            bool locked;
            try { locked = lockWhen(); } catch (Exception) { locked = false; }
            foreach (var name in lockedNames)
            {
                var t = panel.Find(name);
                if (t == null) continue;
                var group = t.GetComponent<CanvasGroup>();
                if (group == null) { if (!locked) continue; group = t.gameObject.AddComponent<CanvasGroup>(); }
                group.alpha = locked ? 0.4f : 1f;
                group.interactable = !locked;
            }
        }

        // ---- buttons: animated hover like the game's, and no lingering selection -----------------------------

        // Page texts are localized by this mod (Loc), so XUnity must not touch them: it only re-translates *active* texts,
        // and it would otherwise inspect every one of them each time the clipped list is redrawn while scrolling.
        private static object xuaDefault;
        private static System.Reflection.MethodInfo xuaIgnore;
        private static bool xuaMissing;

        private static void IgnoreForXua(Component text)
        {
            if (xuaMissing || text == null) return;
            try
            {
                if (xuaDefault == null)
                {
                    var core = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "XUnity.AutoTranslator.Plugin.Core");
                    if (core == null) { xuaMissing = true; return; }
                    var translator = core.GetType("XUnity.AutoTranslator.Plugin.Core.AutoTranslator");
                    var property = translator != null ? translator.GetProperty("Default") : null;
                    xuaDefault = property != null ? property.GetValue(null, null) : null;
                    var contract = core.GetType("XUnity.AutoTranslator.Plugin.Core.ITranslator");
                    xuaIgnore = contract != null ? contract.GetMethod("IgnoreTextComponent") : null;
                    if (xuaDefault == null || xuaIgnore == null) return;      // XUnity not ready yet: try again next time
                }
                xuaIgnore.Invoke(xuaDefault, new object[] { text });
            }
            catch (Exception) { /* XUnity changed: the page still works, it is just translated by XUnity as well */ }
        }

        private static GameObject CloneButton(Transform template, Transform parent, string name, string text, bool ownText = false)
        {
            var go = CloneClean(template, parent, name);
            SetButtonText(go, text);
            if (ownText)
                foreach (var tmp in go.GetComponentsInChildren<TMP_Text>(true))
                {
                    localizedTexts.Add(new LocText { Text = tmp, English = text });
                    IgnoreForXua(tmp);
                }
            var button = go.GetComponent<Button>();
            // A clicked Selectable keeps the EventSystem selection (and its "Selected" animation) until something else is
            // selected: that is the "sticking highlight". Our buttons simply never take the selection.
            button.navigation = new Navigation { mode = Navigation.Mode.None };
            go.AddComponent<ButtonReset>();
            return go;
        }

        // The vanilla highlight bar is a fixed 312 units wide; make ours as wide as our (narrower) button.
        private static void FitHighlight(RectTransform button, float width)
        {
            button.sizeDelta = new Vector2(width, button.sizeDelta.y);
            var highlight = button.Find("SelectionHighlight") as RectTransform;
            if (highlight == null) return;
            highlight.sizeDelta = new Vector2(width, highlight.sizeDelta.y);
            highlight.anchoredPosition = new Vector2(width / 2f, highlight.anchoredPosition.y);
        }

        // The vanilla click sounds are persistent listeners on the original button; they are replayed (guarded) by hand.
        private sealed class Sfx { public UnityEngine.Object Target; public System.Reflection.MethodInfo Method; }

        private static List<Sfx> CaptureSfx(Transform template)
        {
            var list = new List<Sfx>();
            try
            {
                var button = template.GetComponent<Button>();
                if (button == null) return list;
                for (var i = 0; i < button.onClick.GetPersistentEventCount(); i++)
                {
                    var name = button.onClick.GetPersistentMethodName(i);
                    var target = button.onClick.GetPersistentTarget(i);
                    if (target == null || string.IsNullOrEmpty(name) || !name.StartsWith("Play")) continue;
                    var method = target.GetType().GetMethod(name, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic, null, Type.EmptyTypes, null);
                    if (method != null) list.Add(new Sfx { Target = target, Method = method });
                }
            }
            catch (Exception) { /* no sound is better than no button */ }
            return list;
        }

        private static void PlaySfx(List<Sfx> sounds)
        {
            foreach (var s in sounds)
            {
                try { if (s.Target != null) s.Method.Invoke(s.Target, null); }
                catch (Exception) { /* menu audio not available in this scene */ }
            }
        }

        private static void Deselect()
        {
            if (UnityEngine.EventSystems.EventSystem.current != null) UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        }

        private static void BuildTitle(Transform page, Transform captionTemplate)
        {
            var title = CloneClean(captionTemplate, page, objectPrefix + "Title");
            var rect = (RectTransform)title.transform;
            SetRect(rect, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 222f), new Vector2(500f, 30f));
            var tmp = title.GetComponent<TMP_Text>();
            tmp.text = pageTitle;
            localizedTexts.Add(new LocText { Text = tmp, English = pageTitle });
            IgnoreForXua(tmp);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontSize = tmp.fontSize * 1.8f;
            tmp.color = accent;
            tmp.enableWordWrapping = false;
        }

        private static PageScrollRect BuildScroll(Transform page, IList<Option> options, Transform captionTemplate, Transform sliderTemplate,
            Transform dropdownTemplate, Transform scrollbarTemplate, List<Action> refreshers)
        {
            var scrollGo = new GameObject("Scroll", typeof(RectTransform), typeof(PageScrollRect));
            scrollGo.transform.SetParent(page, false);
            SetRect((RectTransform)scrollGo.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(-8f, 6f), new Vector2(700f, 372f));

            var viewport = new GameObject("Viewport", typeof(RectTransform), typeof(RectMask2D));
            viewport.transform.SetParent(scrollGo.transform, false);
            Stretch((RectTransform)viewport.transform);

            var content = new GameObject("Content", typeof(RectTransform), typeof(VerticalLayoutGroup), typeof(ContentSizeFitter));
            content.transform.SetParent(viewport.transform, false);
            var contentCanvas = content.AddComponent<Canvas>();           // own sub-canvas: scrolling only moves a pre-built mesh
            content.AddComponent<GraphicRaycaster>();
            var pageCanvas = page.GetComponent<Canvas>();
            if (pageCanvas != null) contentCanvas.additionalShaderChannels = pageCanvas.additionalShaderChannels;
            SetRect((RectTransform)content.transform, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0.5f, 1f), Vector2.zero, new Vector2(0f, 0f));
            var vlg = content.GetComponent<VerticalLayoutGroup>();
            vlg.spacing = 3f; vlg.padding = new RectOffset(4, 4, 4, 4);
            vlg.childAlignment = TextAnchor.UpperCenter;
            vlg.childControlWidth = true; vlg.childControlHeight = true;
            vlg.childForceExpandWidth = true; vlg.childForceExpandHeight = false;
            content.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            var scroll = scrollGo.GetComponent<PageScrollRect>();
            scroll.viewport = (RectTransform)viewport.transform;
            scroll.content = (RectTransform)content.transform;
            scroll.horizontal = false; scroll.vertical = true;
            scroll.movementType = ScrollRect.MovementType.Clamped;
            scroll.inertia = false;                              // no per-frame inertia work: scrolling stops when the wheel / mouse does

            if (scrollbarTemplate != null)
            {
                // vertical scrollbar on the right, cloned from the game's own dropdown list
                var bar = CloneClean(scrollbarTemplate, scrollGo.transform, "Scrollbar");
                SetRect((RectTransform)bar.transform, new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(0f, 0.5f), new Vector2(10f, 0f), new Vector2(18f, 0f));
                var scrollbar = bar.GetComponent<Scrollbar>();
                scrollbar.direction = Scrollbar.Direction.BottomToTop;
                scroll.verticalScrollbar = scrollbar;
                scroll.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;   // hidden when everything fits
                scroll.verticalScrollbarSpacing = 0f;
            }

            string group = null;
            foreach (var option in options)
            {
                if (option.Group != group)
                {
                    group = option.Group;
                    var header = Row(content.transform, 30f);
                    var h = Caption(header.transform, captionTemplate, "- " + group.ToUpperInvariant() + " -");
                    var hr = (RectTransform)h.transform;
                    SetRect(hr, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, -4f), new Vector2(-20f, 24f));
                    h.alignment = TextAlignmentOptions.Center;
                    h.color = accent;
                }
                var row = Row(content.transform, 36f);
                var label = Caption(row.transform, captionTemplate, option.Label);
                SetRect((RectTransform)label.transform, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(14f, 0f), new Vector2(380f, 24f));
                label.alignment = TextAlignmentOptions.MidlineLeft;

                switch (option.Kind)
                {
                    case OptionKind.Slider: BuildSlider(row.transform, option, sliderTemplate, captionTemplate, refreshers); break;
                    case OptionKind.Toggle: BuildDropdown(row.transform, option, dropdownTemplate, new[] { "Off", "On" }, refreshers); break;
                    default: BuildDropdown(row.transform, option, dropdownTemplate, option.Labels ?? option.Choices, refreshers); break;
                }
            }
            return scroll;
        }

        private static GameObject Row(Transform parent, float height)
        {
            var go = new GameObject("Row", typeof(RectTransform), typeof(LayoutElement));
            go.transform.SetParent(parent, false);
            ((RectTransform)go.transform).sizeDelta = new Vector2(0f, height);
            var le = go.GetComponent<LayoutElement>();
            le.minHeight = height; le.preferredHeight = height; le.flexibleHeight = 0f;
            return go;
        }

        private static TMP_Text Caption(Transform parent, Transform template, string text)
        {
            var go = CloneClean(template, parent, "Caption");
            var tmp = go.GetComponent<TMP_Text>();
            tmp.text = text;
            tmp.enableWordWrapping = false;
            tmp.overflowMode = TextOverflowModes.Overflow;
            tmp.raycastTarget = false;
            IgnoreForXua(tmp);
            if (!string.IsNullOrEmpty(text)) localizedTexts.Add(new LocText { Text = tmp, English = text });
            return tmp;
        }

        private static void BuildSlider(Transform row, Option option, Transform sliderTemplate, Transform captionTemplate, List<Action> refreshers)
        {
            var go = CloneClean(sliderTemplate, row, "Slider");
            SetRect((RectTransform)go.transform, new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(-92f, 0f), new Vector2(200f, 20f));
            var slider = go.GetComponent<Slider>();
            slider.wholeNumbers = false;
            slider.minValue = option.Min; slider.maxValue = option.Max;

            var value = Caption(row, captionTemplate, "");
            SetRect((RectTransform)value.transform, new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(-12f, 0f), new Vector2(70f, 24f));
            value.alignment = TextAlignmentOptions.MidlineRight;

            Action<float> show = v => value.text = v.ToString("0.##") + option.Unit;
            Action refresh = () => { var v = Mathf.Clamp(option.Get(), option.Min, option.Max); slider.SetValueWithoutNotify(v); show(v); };
            slider.onValueChanged.AddListener(v => { try { option.Set(v); show(option.Get()); } catch (Exception e) { Warn(e); } });
            refresh();
            refreshers.Add(refresh);
        }

        private static void BuildDropdown(Transform row, Option option, Transform dropdownTemplate, string[] names, List<Action> refreshers)
        {
            var go = CloneClean(dropdownTemplate, row, "Dropdown");
            SetRect((RectTransform)go.transform, new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(-14f, 0f), new Vector2(250f, 28f));
            var caption = go.transform.Find("Label2");            // the vanilla caption above the dropdown: we draw our own label
            if (caption != null) UnityEngine.Object.Destroy(caption.gameObject);
            var dropdown = go.GetComponent<TMP_Dropdown>();
            dropdown.ClearOptions();
            var english = names.Select(n => Pretty(n)).ToArray();
            dropdown.AddOptions(english.ToList());
            localizedDropdowns.Add(new LocDropdown { Dropdown = dropdown, English = english });
            IgnoreForXua(dropdown.captionText);

            Action refresh = () => dropdown.SetValueWithoutNotify(Mathf.Clamp((int)Mathf.Round(option.Get()), 0, names.Length - 1));
            dropdown.onValueChanged.AddListener(i => { try { option.Set(i); } catch (Exception e) { Warn(e); } });
            refresh();
            refreshers.Add(refresh);
        }

        // Rewrites every registered text and dropdown in the current game language (English if the mod has no entry).
        public static void Relocalize()
        {
            localizedTexts.RemoveAll(l => l.Text == null);
            localizedDropdowns.RemoveAll(l => l.Dropdown == null);
            foreach (var l in localizedTexts) l.Text.text = Loc.T(l.English);
            foreach (var d in localizedDropdowns)
            {
                var value = d.Dropdown.value;
                d.Dropdown.ClearOptions();
                d.Dropdown.AddOptions(d.English.Select(e => Loc.T(e)).ToList());
                d.Dropdown.SetValueWithoutNotify(Mathf.Clamp(value, 0, d.English.Length - 1));
            }
        }

        private static string Pretty(string name)
        {
            if (name == "Vanilla") return "Game default";
            var sb = new System.Text.StringBuilder();
            for (var i = 0; i < name.Length; i++)
            {
                if (i > 0 && char.IsUpper(name[i]) && char.IsLower(name[i - 1])) sb.Append(' ');
                sb.Append(name[i]);
            }
            return sb.ToString();
        }

        // ---- cloning helpers -----------------------------------------------------------------------------------

        // Clone a vanilla element and cut its ties to the game's settings logic: the SettingsOption component (which
        // UpdateGameToMatchSettings would otherwise drive) is removed and the serialized listeners are switched off.
        private static GameObject CloneClean(Transform template, Transform parent, string name)
        {
            var go = UnityEngine.Object.Instantiate(template.gameObject, parent, false);
            go.name = name;
            foreach (var so in go.GetComponentsInChildren<SettingsOption>(true)) UnityEngine.Object.DestroyImmediate(so);
            foreach (var b in go.GetComponentsInChildren<Button>(true)) SwitchOff(b.onClick);
            foreach (var s in go.GetComponentsInChildren<Slider>(true)) SwitchOff(s.onValueChanged);
            foreach (var d in go.GetComponentsInChildren<TMP_Dropdown>(true)) SwitchOff(d.onValueChanged);
            go.SetActive(true);
            return go;
        }

        private static void SwitchOff(UnityEventBase ev)
        {
            for (var i = ev.GetPersistentEventCount() - 1; i >= 0; i--) ev.SetPersistentListenerState(i, UnityEventCallState.Off);
        }

        private static void SetButtonText(GameObject button, string text)
        {
            foreach (var tmp in button.GetComponentsInChildren<TMP_Text>(true)) tmp.text = text;
        }

        private static void Stretch(RectTransform rt)
        {
            rt.anchorMin = Vector2.zero; rt.anchorMax = Vector2.one; rt.offsetMin = Vector2.zero; rt.offsetMax = Vector2.zero;
        }

        private static void SetRect(RectTransform rt, Vector2 min, Vector2 max, Vector2 pivot, Vector2 pos, Vector2 size)
        {
            rt.anchorMin = min; rt.anchorMax = max; rt.pivot = pivot; rt.anchoredPosition = pos; rt.sizeDelta = size;
        }
    }

    // Which language the game is in, and the mod's own dictionaries for it. The dictionaries are the same files XUnity reads:
    //   config/Translation/ru/Text/<Mod>.txt  and  config/Translation_de/Mods/<Mod>.txt   (lines "English=Translation")
    internal static class Loc
    {
        private static Dictionary<string, string> ru = new Dictionary<string, string>(), de = new Dictionary<string, string>();

        public static void Init(string modName)
        {
            ru = Read(Path.Combine(Paths.ConfigPath, "Translation", "ru", "Text", modName + ".txt"));
            de = Read(Path.Combine(Paths.ConfigPath, "Translation_de", "Mods", modName + ".txt"));
        }

        private static Dictionary<string, string> Read(string path)
        {
            var map = new Dictionary<string, string>();
            try
            {
                if (!File.Exists(path)) return map;
                foreach (var line in File.ReadAllLines(path, System.Text.Encoding.UTF8))
                {
                    if (line.Length == 0 || line.StartsWith("//")) continue;
                    var i = line.IndexOf('=');
                    if (i <= 0) continue;
                    map[line.Substring(0, i)] = line.Substring(i + 1);
                }
            }
            catch (Exception) { /* a missing or unreadable dictionary just means English */ }
            return map;
        }

        public static string T(string english)
        {
            string value;
            switch (CurrentLanguage())
            {
                case "ru": if (ru.TryGetValue(english, out value)) return value; break;
                case "de": if (de.TryGetValue(english, out value)) return value; break;
            }
            return english;
        }

        // "en", "ru" or "de": LanguageSwitch's choice if it is installed, otherwise whatever XUnity is currently translating to.
        public static string CurrentLanguage()
        {
            try
            {
                PluginInfo info;
                if (Chainloader.PluginInfos.TryGetValue("lc.languageswitch", out info) && info != null && info.Instance != null)
                {
                    var config = info.Instance.Config;
                    foreach (var key in config.Keys)
                    {
                        if (key.Section != "General" || key.Key != "Language") continue;
                        var chosen = Convert.ToString(config[key].BoxedValue);
                        if (!string.IsNullOrEmpty(chosen)) return chosen.StartsWith("R", StringComparison.OrdinalIgnoreCase) ? "ru" : chosen.StartsWith("G", StringComparison.OrdinalIgnoreCase) ? "de" : "en";
                    }
                }
                var core = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "XUnity.AutoTranslator.Plugin.Core");
                if (core == null) return "en";
                var pluginType = core.GetType("XUnity.AutoTranslator.Plugin.Core.AutoTranslationPlugin");
                var current = pluginType.GetField("Current", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic).GetValue(null);
                if (current == null) return "en";
                var disabled = pluginType.GetField("_temporarilyDisabled", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (disabled != null && (bool)disabled.GetValue(current)) return "en";
                var language = core.GetType("XUnity.AutoTranslator.Plugin.Core.Configuration.Settings").GetField("Language").GetValue(null) as string;
                return language == "ru" || language == "de" ? language : "en";
            }
            catch (Exception) { return "en"; }
        }
    }

    // While the page is open the game's own Settings controls are made invisible and unclickable (alpha / raycast on
    // a CanvasGroup per direct child, so no game component is switched off and nothing re-initialises when they come
    // back). The panel's own backdrop is a Graphic of the panel itself, not a child, and stays.
    internal sealed class VanillaHider : MonoBehaviour
    {
        public Transform Panel;
        public Action AfterRestore;
        private sealed class Saved { public CanvasGroup Group; public bool Added; public float Alpha; public bool Blocks, Interactable; }
        private readonly List<Saved> saved = new List<Saved>();

        private void OnEnable()
        {
            if (Panel == null) return;
            foreach (Transform child in Panel)
            {
                var group = child.GetComponent<CanvasGroup>();
                var added = group == null;
                if (added) group = child.gameObject.AddComponent<CanvasGroup>();
                saved.Add(new Saved { Group = group, Added = added, Alpha = group.alpha, Blocks = group.blocksRaycasts, Interactable = group.interactable });
                group.alpha = 0f; group.blocksRaycasts = false; group.interactable = false;
            }
        }

        private void OnDisable()
        {
            foreach (var s in saved)
            {
                if (s.Group == null) continue;
                if (s.Added) Destroy(s.Group);
                else { s.Group.alpha = s.Alpha; s.Group.blocksRaycasts = s.Blocks; s.Group.interactable = s.Interactable; }
            }
            saved.Clear();
            if (AfterRestore != null) AfterRestore();
        }
    }

    // Closes our page whenever the vanilla Settings panel is closed, so it never reappears on its own.
    internal sealed class PanelWatcher : MonoBehaviour
    {
        public GameObject Page;
        public Transform Panel;
        private void OnEnable()
        {
            var es = UnityEngine.EventSystems.EventSystem.current;
            if (es != null) es.SetSelectedGameObject(null);     // the game leaves one of its dropdowns "selected" when the panel opens
            SettingsMenu.PanelShown(Panel);
        }

        private void OnDisable()
        {
            if (Page != null) Page.SetActive(false);
            var es = UnityEngine.EventSystems.EventSystem.current;
            if (es != null) es.SetSelectedGameObject(null);     // never leave a stale selection behind when the menu closes
        }
    }

    // The list. One mouse-wheel notch moves it a fixed, small distance (about one row) whatever scale the game's input
    // module reports the wheel in; the stock ScrollRect multiplies that scale by its sensitivity and jumps several rows.
    internal sealed class PageScrollRect : ScrollRect
    {
        private const float StepPixels = 40f;

        public override void OnScroll(UnityEngine.EventSystems.PointerEventData data)
        {
            if (!IsActive() || content == null || viewport == null) return;
            var range = content.rect.height - viewport.rect.height;
            if (range <= 0f || data.scrollDelta.y == 0f) return;
            var notches = Mathf.Clamp(data.scrollDelta.y, -1f, 1f);
            verticalNormalizedPosition = Mathf.Clamp01(verticalNormalizedPosition + notches * StepPixels / range);
        }
    }

    // A ScrollRect only reacts to the wheel and to dragging while the pointer is over one of its own children. The page
    // background, the title and the gaps between rows are not, so they pass both on to the list here (events bubble up to
    // the nearest parent that handles them; the page is the topmost one). Dragging with the left button therefore
    // scrolls from anywhere on the page, except over a slider (which keeps its own drag).
    internal sealed class ScrollForwarder : MonoBehaviour,
        UnityEngine.EventSystems.IScrollHandler,
        UnityEngine.EventSystems.IInitializePotentialDragHandler,
        UnityEngine.EventSystems.IBeginDragHandler,
        UnityEngine.EventSystems.IDragHandler,
        UnityEngine.EventSystems.IEndDragHandler
    {
        public ScrollRect Target;
        private bool Ready { get { return Target != null && Target.isActiveAndEnabled; } }

        public void OnScroll(UnityEngine.EventSystems.PointerEventData e) { if (Ready) Target.OnScroll(e); }
        public void OnInitializePotentialDrag(UnityEngine.EventSystems.PointerEventData e) { if (Ready) Target.OnInitializePotentialDrag(e); }
        public void OnBeginDrag(UnityEngine.EventSystems.PointerEventData e) { if (Ready) Target.OnBeginDrag(e); }
        public void OnDrag(UnityEngine.EventSystems.PointerEventData e) { if (Ready) Target.OnDrag(e); }
        public void OnEndDrag(UnityEngine.EventSystems.PointerEventData e) { if (Ready) Target.OnEndDrag(e); }
    }

    // When one of our buttons is shown again, start from the normal (not highlighted, not selected) animation state.
    internal sealed class ButtonReset : MonoBehaviour
    {
        private void OnEnable()
        {
            var animator = GetComponent<Animator>();
            if (animator != null && animator.runtimeAnimatorController != null) { animator.Rebind(); animator.Update(0f); }
            var es = UnityEngine.EventSystems.EventSystem.current;
            if (es != null && es.currentSelectedGameObject == gameObject) es.SetSelectedGameObject(null);
        }
    }
}
