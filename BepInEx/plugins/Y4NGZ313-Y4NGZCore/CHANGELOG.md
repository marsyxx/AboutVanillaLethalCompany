# Y4NGZCore changelog

## 1.0.13

- The shared round-end card re-asserts its stock claims every frame while vanilla could draw a page it covers and on each Show, Advance and Hide cue, re-claims a stock group that was destroyed, and logs one `stock re-assert` line per episode naming the first node it found raised; `HudRoundEndStock` gains `ChildGroupsOf` (every direct child of a named parent is claimed by node, same-named siblings included, at bind and on each refresh; `Y4NGZ_` objects are never stock); `HudSuppressionRegistry.ForceRestoreAll` keeps live prefab-theme claims instead of handing their stock back at every landing, through the new `RefCountedSuppressionRegistry.ForceRestoreAllExcept` (the old `ForceRestoreAll` is unchanged) (#1789).
- The shared UI palette is now three hand-authored color tables (Vanilla, Green, Blue) instead of colors derived from one hue; color preset Vanilla (`HudColorPreset.Default`) uses the new Vanilla table, taken from the stock HUD's own colors, instead of Orange, and `UiThemeState` gains `Backplate` and `Border`. The Orange preset is removed: `HudColorPreset` 1 and `UiColorTheme` 0 are retired and never reused, a published 1 reads as Vanilla, and `UiThemeState.OrangeMode` is gone (#1523).
- Station hide-HUD contract: StationHudHideSignal publishes the shared Hide HUD at stations value and StationHudHold lets optional owners claim the stock HUD (#1523).
- Added the optional facility-security capability and the planet-card `SetSecurity` view; Hairline requires its bundle's `Security` text node and hides that row while the status is unknown (#1741).
- Added `Y4NGZCompany.Core.SpecialMoonRule` (`Configure`, `IsSpecial`, `Classify`, `SpecialMoonReason`): one shared answer to "is this moon special" (non-spawning, no resolvable interior, drop-off desk seen while landed, or named in ShipSystems' `Special Moons` list with `-Name` exclusion) for the MOONS page and Contracted's assignment gate (#1760).
- `OutlineEffect` gains opt-in `DepthTestAgainstDisplay` (a manual composite draws its mask against the display's scene depth), `LineTexels` and `LastCompositeStatus`; CCTV and Contracted outlines are unchanged (#1760).
- Removed `HudTheme.Vintage` (numeric value 3, retired and never reused; `(HudTheme)3` reads as `Vanilla`, `HudThemes.SlotCount` is 3) with the Y4NGZUI Vintage theme. Only Y4NGZUI 1.0.0, unpublished, ever bound it (#1523).
- Added `HudSurface.PerkStatus` (26) (`HudSurfaces.Count` 27) for Y4NGZUpgrades' perk status row (y4ngz313/Y4NGZUpgrades#701).
- `DawnLibCompat` can list a ship unlockable in the vanilla store with a live price (`DefineShopUnlockable`), points a shop unlockable's purchase nodes at its real unlockables-list index (`SyncShopUnlockableIds`), and registers items under a chosen namespace through `RegisterNamespacedItem` (#1741).
- The chat prefab's `Input` label binds with rich text on, like `Log` and `Hint`, so a theme can colour the compose line (#1523).
- Added `TurretFocusSignal` (client-local turret-piloting flag) and a `HudSuppressionRegistry.Suppress` overload with a floor alpha, so a holder can dim a HUD group instead of hiding it (#1560).
- The shared round-end card is created hidden and pulled back down whenever its Animator rests in Hidden while drawn, so a card bound after the report closed (theme switched on after the round) or one whose report page stayed up no longer stays on screen (#1523).
- The scan view contract gains `HudScanNode.Distance`, `IHudScanView.SetSweep` (a code-built sweep ring) and `SetTotalBar` (a quota bar under the total), and `HudScanPrefabRenderer` takes an ink colour (#1523).
- The scan view contract gains `SetNodeThreat`, `SetThreatPulse` and `SetPin` (`HudScanPin`), and `PinnedScanNodes` gains owner pins that never evict another owner's pin (`Pin(node, seconds, owner)`, `IsPinnedBy`) and `IsPinOnly` (#1523).
- The chat view contract gains SetChrome, which shows or hides the Chat prefab's plate and rule (#1523).
- Core now owns the outline effect and its shader bundle (`y4ngz-outlineeffect.bundle`, moved from LethalCCTV), which LethalCCTV's outlines and the ShipSystems turret feed draw with (#1560).
- The loading view contract gains SetContract, which writes the Loading prefab's TopBand/Contract text and hides the node when empty; a theme bundle without the node leaves Hairline unavailable (#1523).
- FixtureReservationRegistry.TryReserveObstacle: an obstacle claim blocks overlap for every placer but does not count toward fixture separation or crowding, so another mod's props can be avoided without pushing contract fixtures away (#1702).
- The Field Support seam (`IFieldSupportDesk`, `WireDiagnosticRequest`, `FieldSupport`, `ModuleCapabilities.FieldSupport`) and `FacilityMeltdownCompat.TryStartMeltdown` are now `[Obsolete]`: nothing provides them since Contracted's native Defuse was removed, and code that still uses them builds with a warning (#1702).
- Added the neutral contract reward-effect types (`ContractEffect`, `ContractEffectPlan`, `ContractEffectReceipt`, `ContractEffectText`), the `ship_upgrade` and `next_landing_item` reward kinds with `QuotaRewardGrant.TargetId`, reward and penalty summaries on `MoonAssignmentPresentation`, and a host-only Core provider that pays the `credits` reward kind into the terminal purse (#1702).
- Added `ScanNodePolicy` and `ScanNodeRole`, the shared owner of scan node range, line of sight, node type and layer (#1702).
- Added `EnemyLocationPulseService.PulseEnemiesToward`, which steers the calling peer's owned interior enemies toward a live position (#1741).
- Added the internal vanilla HUD text recipe (`VanillaHudText` sizes, `VanillaHudFace` and `VanillaHudText.StockFace`: the true distance-field 3270 with its own plain material, else the weight counter's font and material), which Contracted's card and Y4NGZUI's Minimal control tips draw with; Contracted and Y4NGZUI built with it need this Core build (#1523).
- The contract card view reports `HeightIsProvisional` for a height read while the card was hidden, and showing or hiding the belt dock lays the card out again (#1523).
- Quota rewards can now grant items: a `next_landing_item` reward spawns the named items in the ship on the next landing, and an unknown item name stays owed (#1741).
- Added the optional moon security directory (`IMoonSecurityDirectory`, `MoonSecurity`, `ModuleCapabilities.MoonSecurityDirectory`): per moon, its risk tier, whether facility security can activate there and the activation chance (#1760).
- Interior entrance reachability: when the inside entrance anchors replace the provisional ones, the round's fail-open now ends only after a tile is proven reachable from them; if they reach none, the provisional verdicts are kept (`REACHABILITY_RECOMPUTE_EMPTY`) instead of vetoing every tile, and unreachable-tile lines name the NavMesh failure (`pathStatus`) (#1741).
- `OutlineEffect` gains opt-in `DrawsDisabledTargets`: registry targets whose renderer is disabled still draw; off by default, so existing outlines are unchanged (#1741).
- Feed camera profiles gain a Volumetrics opt-in (four-argument `CameraRenderProfile.ApplyFeedProfile`); the three-argument call keeps volumetrics off, so LethalCCTV feeds are unchanged (#1760).

## 1.0.12

- Round-end XpRank: the Hairline tier tint now overrides colour tags that Y4NGZUpgrades writes into the rank text (#1523).
- The round-end prefab view gains per-tile reveal beats and optional `Revealing`/`RevealN`/`ConfirmN` Animator parameters; bundles without them show the settled report as before (#1523).
- Added the scan-node label override contract (`ScanNodeLabelOverrides`, `ScanNodeLabel`) so a plugin can supply scan label text and colour to the Y4NGZUI scan renderer (#1523).
- Added `UiThemeState.IsPublished` and `UiPalette.ForHue`, so a reader can tell Core's unpublished defaults from a theme Y4NGZUI published; Core's own refresh fallback no longer counts as a publish (#1560).
- Added neutral HUD theme contracts for the optional Y4NGZUI: `HudTheme`, `HudSurface`, `UiSkin`, the `UiThemeState` skin signal (`CurrentHudTheme`, `Skin`, `SkinChanged`), the per-surface `HudSurfaceOwnership` resolver and the `IHudSettingsHost` settings seam (#1523).
- Added the HUD theme-package contract (`HudThemePackage`, `HudSurfaceRenderer`, `HudSurfaceAttachment`, `HudMotionCue`, the `HudThemePackages` registry and the `HudSurfaceSlot` helper), `UiThemeState.RequestedHudTheme` and 12 more `HudSurface` values (24 in all, for the Contracted card and belt, the CCTV arcs and markers and the Upgrades surfaces). `CurrentHudTheme` is now the effective theme: a theme other than Vanilla draws only while its package is registered and available, and the saved choice is kept meanwhile (#1523).
- Retired unused Terminal Upgrades lifecycle identities and friend access, and clarified Ship Systems and Core sales ownership (#1525).
- Added the prefab HUD contract for the optional Y4NGZUI: the bind-point table and check (`HudPrefabContract`), the bundle seam (`IHudPrefabSource`), the `IHudVitalsView`/`IHudHotbarView`/`IHudClockView` setters, their prefab renderers and a hide-only renderer for surfaces a prefab already covers (#1523).
- G2 fixes: the HUD prefab contract drops Utility/Key, Container/Leaves, Container/Profit and Slot[n]/Locked, allows ContentSizeFitter, and the hotbar view takes the unlocked slot count only (#1523).
- Added `VanillaFonts`, which finds the game's true distance-field 3270 font by render mode and glyph coverage, for plugins that draw HUD text (#1614).
- Added the group 2 prefab HUD contract for the optional Y4NGZUI: Scan, Notifications, ControlTips and ContractCard bind points, views and prefab renderers, the Contracted card seam (`IHudContractCardSource`, `HudContractCardState`), per-theme required prefab sets, the card's content-driven height (`PreferredHeight`, the prefab's laid-out `Header` plus `Rows`) and the notification lane alpha (`SetLaneAlpha`) (#1523).
- Added `PinnedScanNodes`, which keeps up to 4 scan nodes on the local scan HUD for a set time through vanilla's own scan assignment, so stock and Y4NGZUI scan renderers both draw them; pins clear on ship leave and scene unload (#1614).
- Added the Chat prefab key and view, and optional Vitals `Mic` and `HitFlash` bind points (#1523).
- Added `HudSettingsControl.VisibleForThemes`, so a settings row can show only under chosen HUD themes, and an optional timed fade on HUD suppression (`Suppress`, `Release` and `ReleaseAll` overloads with `fadeSeconds`, driven by `HudSuppressionFader`; the old signatures stay instant) (#1631).
- Added the `HudCriticalPrompt` seam (`Begin`/`End`/`Active`), so a plugin can keep a critical prompt visible when the player hides control tips (#1631).
- Added `HudSurface.PlanetCard` (24) and `HudSurface.Loading` (25) (`HudSurfaces.Count` 26) and typed prefab renderers for the quota banners, desk sale, planet card and loading screen (`HudQuotaBannersPrefabRenderer`, `HudDeskSalePrefabRenderer`, `HudPlanetCardPrefabRenderer`, `HudLoadingPrefabRenderer`) (#1523).
- Themed contract cards fade the check in when an objective completes, and the shared palette follows the HUD theme: the colour preset applies under Vanilla, other themes read the Default palette (#1523).
- The Hairline contract card's plate fits its text: Core sizes it to the widest line in 40 px steps from 320 px to 560 px on the card's layout pass, keeps its right edge, only grows while one card shows and starts afresh on a new title or source bind. The card's header bind points move under a new `HeaderDock` (`Header`, `Header/Icon` and `Header/Title` become `HeaderDock/Header`, `HeaderDock/Header/Icon` and `HeaderDock/Header/Title`) and `Frame` becomes a bind point, so a Y4NGZUI bundle from before this change leaves the whole Hairline theme unavailable (#1523).
- Added the `RoundEnd` prefab key with its bind points (the `Report`, `Fines`, `XpRank` and `Fired` panels, four report rows, six report footer cells that can carry Contracted's bottom row with the grade pinned right, each a label over a value over a muted qualifier line, the all-dead cover) and the `IHudRoundEndView` setter view (`SetFooterCells` takes the footer; `HudRoundEndFooterCell.Qualifier` empty hides the line) (#1523).
- Added the round-end prefab seam for the optional Y4NGZUI: `HudRoundEndState`, `IHudRoundEndSource`, `HudRoundEndVanillaSource` (reads vanilla's report, fines and fired screens and takes a Contracted override), `HudRoundEndPrefabRenderer` and `HudRoundEndStock`, which mount one shared `RoundEnd` card and show the panel that matches vanilla's visible page (#1523).
- Added the `QuotaBanners`, `DeskSale`, `PlanetCard` and `Loading` prefab keys with their bind points, Animator parameters and setter views (`IHudQuotaBannersView`, `IHudDeskSaleView`, `IHudPlanetCardView`, `IHudLoadingView`); Y4NGZUI's Hairline binds and requires all four since the G3d renderers below (#1523).
- Prefab HUD labels enable rich text at bind so vanilla and mod colour tags render instead of printing literally, including the Hairline round-end rank and XP (#1523).
- The Hairline chat prefab's width fits its text: Core narrows it to the widest log line plus the layout's side padding, never under the hint line, in 16 px steps up to the prefab's authored width, keeping its left edge (#1523).
- The `RoundEnd` report's footer cells become meter tiles at `Report/Meters/Cell0..5`, the all-dead group moves under the rows (`Report/Rows/AllDead`), and a new `Report/Outcome` stamp bind point with `IHudRoundEndView.SetOutcome` shows the contract outcome that `HudRoundEndVanillaSource.SetReportOverride` now carries; a Y4NGZUI bundle from before this change leaves the Hairline theme unavailable (#1523).
- The `Loading` prefab key swaps `Backdrop` for the letterbox bands `TopBand` and `BottomBand` and adds `TopBand/Moon` and `TopBand/Weather` with `IHudLoadingView.SetMoon`/`SetWeather`; while bound, the loading renderer hides vanilla's LoadingText children and hands them back on release (#1523).
- The round-end prefab view gains optional `XpRank/Gain` and `XpRank/Next` text bind points (hidden while their value is unknown) and optional `XpShow`/`Promote` Animator parameters, fired when the card reaches the XP panel (not again when a theme switch re-attaches it mid-XP) and once per XP entry when the rank rises there; a reveal begun over a running one hides its tiles again (#1523).
- The round-end XP state carries the shift's gain, the next rank, the XP still needed, the rank tier and whether the rank rose, captured from vanilla's levelling or published by another mod through `HudRoundEndRank` (#1523).
- The shared round-end card now stays hidden once no page shows it (it re-asserts its hide when a hide was dropped or undone), ends its report and XP panels at ship leave, ship reset and disconnect, and logs each Show, Advance, Hide and hide re-assert (#1523).
- Removed the terminal-session repaint (`PresentationBroker.RefreshActiveTerminalSession`, `ITerminalSessionCoordinator.RefreshActiveSession`); the liveness reads stay. Teardown slot 300 is now `ModuleTeardownOrder.ThemeObservers` (#1523).

## 1.0.11

- Removed the retired fuel-source members from the ship-fuel routing contract: `ShipFuelRouteQuote.HasFuelSource` and `IShipFuelRoutingService`/`ShipFuelRoutingServices` `SourceFillCount` and `FilledPumpFuelAmount` (#914).
- Interior placement: new free-standing floor-prop mode (room-centred, clearance ring, doorway path) for creature props such as the Cook cauldron (Monsters#304, #1443).
- The debug mob-ignore switch is now answered per player, so a Y4NGZDebugTools dummy that shares the host's id is no longer ignored along with the host (Y4NGZPerformance#48).
- Added `CoreDiagnostics.PerformanceTimingLoggingProvider` and `CoreDiagnostics.PerformanceTimingLogging`, so diagnostics held to Core's rules can honor a plugin's Performance Timing Logging switch (#1405).

## 1.0.10

- Interior placement treats the tile a facility entrance stands in as reachable, and a tall tile's reachability probe now reaches its floor (#1271).
- Added shared camera profiles and counted effect leases so CCTV and objective hints restore camera settings without overriding one another (#1219).
- Added an optional spatial objective contract with stable identity, a live object handle, and an area/object distinction. Contracted and LethalCCTV candidates require Core 1.0.10 (#1061).
- Interior placement makes a fixture whose floor sits outside the elevation band of the facility entrances prove a complete walking route from an entrance to its interaction point before it is placed (#1156).
- The debug mob-ignore switch is now read from one shared place, so a plugin that asks whether a player has been switched off gets the same answer as every other one (#1159).
- Added a performance-report restyle seam, so a report that lands after the first restyle pass is still themed (#1160).
- Interior placement never puts anything in the Rubber Rooms padded cells, and a plugin that picks its own tiles can ask the same question (#874).
- Shared fuel quantity formatting for Ship Systems and Contracted (#885).
- An isolated battery-warning render slot for Ship Systems (#890).
- An empty fixture registry reports infinite distance; zero now unambiguously identifies a fixture at the same horizontal position (#686).
- Placement requests can keep fixture separation mandatory while relaxing other geometric preferences, so Contracted exhausts separated bomb placements before accepting a crowded fallback (#686).
- Added `QuotaUnlockCategories.Moon`, the neutral unlock category for quota-locked moons; with no quota progression installed every moon stays unlocked (#862).

## 1.0.6

### Added

- Neutral UI signals other Y4NGZ plugins read without depending on Y4NGZUI: the shared theme's write calls, the gameplay HUD visibility gate, the HUD motion offset, and the presentation callbacks a contract plugin hands to the UI layer. This is what lets Contracted run without Y4NGZUI installed.
- GeneralImprovements Better Monitors screens can now be painted individually by a single presentation, so a plugin can show different content on different monitors of the same wall instead of one shared picture. Screens a presentation does not address keep showing its main content.

## 1.0.5

### Fixed

- Screens no longer stay stuck on old takeover or reward content after a presentation ends: every monitor not carrying the reward screens returns to normal.
- GeneralImprovements Better Monitors no longer wash Y4NGZ monitor content out to a flat glow.

## 1.0.4

### Added

- New cross-plugin seams for the #666 Ship Systems split: the ship upgrade-state and route-fuel capabilities, the power/status signal hub, and the Core-held monitor presentation scopes, so the split fuel/power and terminal/upgrades plugins coordinate whichever subset is installed.
- Terminal + Upgrades lifecycle identities (Harmony owner, module id, tick and teardown slots).
- GeneralImprovements Better Monitors small screens can now be addressed by name, so plugins can host readouts on a chosen screen.

### Fixed

- Announcements on GeneralImprovements Better Monitors now cover all three large lower monitors instead of two.

## 1.0.3

### Fixed

- Bumped the package version so the shipped payload unambiguously contains `SplitAssemblyDiagnostics`: the 1.0.2 payload changed meaning mid-version (a stale pre-diagnostics 1.0.2 build TypeLoadExceptioned every split module). Core now bumps with every change until the split set ships to Thunderstore.
- GeneralImprovements Better Monitors screens no longer flood white under Y4NGZ monitor content: the shared GI property-block binding forced a white HDR emissive color onto materials whose emissive-map keyword a property block cannot enable, so the content texture was never sampled while the white emission was. The emissive push is now gated on the material actually sampling an emissive map (#659).
- Updated dependent package staging, readmes, and docs to require Y4NGZCore 1.0.3.

## 1.0.2

### Added

- Module manifests and compatibility handshakes used by independently shipped Y4NGZ modules.
- Deterministic module tick and teardown ordering, capability registration, and diagnostics.
- Shared monitor-surface arbitration and ship-layout notifications.
- Neutral contracts for objectives, quota progression, presentation, sales, environment, and Ship Systems providers.
- Named-message registration guards and reconnect-safe network lifecycle helpers.

### Fixed

- Assigned the post-split Core API surface a fresh package version so mod managers cannot substitute an older 1.0.1 DLL that lacks `ModuleHandshakeService`.
- Updated dependent package staging to require Y4NGZCore 1.0.2.
