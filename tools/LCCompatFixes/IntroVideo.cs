using System;
using System.IO;
using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

namespace LCCompatFixes;

public sealed class IntroVideo : MonoBehaviour
{
	private const float StartTimeout = 20f;

	private static ManualLogSource log;

	private VideoPlayer player;

	private RenderTexture target;

	private float begun;

	private float wantedVolume = 1f;

	private float firstFrameAt = -1f;

	private float gateOpenedAt = -1f;

	private bool finished;

	private bool firstFrameSeen;

	private bool prepared;

	private bool playing;

	private GUIStyle hintStyle;

	private string pendingPath;

	private bool waitForReady;

	private bool began;

	private bool GateOpen
	{
		get
		{
			if (waitForReady && !LoadMarkers.Ready)
			{
				return Time.realtimeSinceStartup > 1000f;
			}
			return true;
		}
	}

	internal static void Start(BaseUnityPlugin plugin, ManualLogSource logger, string path, bool waitForReady)
	{
		log = logger;
		if (!File.Exists(path))
		{
			log.LogInfo("Intro video not found (" + path + "); skipped.");
			return;
		}
		IntroVideo introVideo = plugin.gameObject.AddComponent<IntroVideo>();
		introVideo.pendingPath = path;
		introVideo.waitForReady = waitForReady;
	}

	private void Begin(string path)
	{
		begun = Time.realtimeSinceStartup;
		wantedVolume = AudioListener.volume;
		target = new RenderTexture(1920, 1080, 0, RenderTextureFormat.ARGB32);
		target.Create();
		player = base.gameObject.AddComponent<VideoPlayer>();
		player.playOnAwake = false;
		player.source = VideoSource.Url;
		player.url = "file://" + path.Replace('\\', '/');
		player.renderMode = VideoRenderMode.RenderTexture;
		player.targetTexture = target;
		player.isLooping = false;
		player.skipOnDrop = true;
		player.audioOutputMode = VideoAudioOutputMode.Direct;
		player.prepareCompleted += delegate
		{
			prepared = true;
		};
		player.loopPointReached += delegate
		{
			Finish("ended");
		};
		player.errorReceived += delegate(VideoPlayer p, string message)
		{
			Fail(message);
		};
		player.Prepare();
	}

	private void Fail(string reason)
	{
		log.LogWarning("Intro video failed: " + reason);
		Finish("failed");
	}

	private void Update()
	{
		if (finished)
		{
			return;
		}
		if (!began)
		{
			began = true;
			try
			{
				Begin(pendingPath);
			}
			catch (Exception ex)
			{
				Fail(ex.Message);
				return;
			}
		}
		if (!GateOpen)
		{
			return;
		}
		if (gateOpenedAt < 0f)
		{
			gateOpenedAt = Time.realtimeSinceStartup;
		}
		if (AudioListener.volume > 0.0001f)
		{
			wantedVolume = AudioListener.volume;
			AudioListener.volume = 0f;
		}
		if (prepared && !playing)
		{
			playing = true;
			try
			{
				player.SetDirectAudioVolume(0, 1f);
				player.Play();
				log.LogInfo("Intro video started (" + player.width + "x" + player.height + ", " + player.length.ToString("0") + " s, audio tracks: " + player.audioTrackCount + ").");
			}
			catch (Exception ex2)
			{
				Fail(ex2.Message);
				return;
			}
		}
		if (player != null && player.isPlaying && !firstFrameSeen && player.frame > 0)
		{
			firstFrameSeen = true;
			firstFrameAt = Time.realtimeSinceStartup;
		}
		Keyboard current = Keyboard.current;
		if (current != null && (current[Key.Space].wasPressedThisFrame || current[Key.Escape].wasPressedThisFrame))
		{
			Finish("skipped");
		}
		else if (!firstFrameSeen && Time.realtimeSinceStartup - gateOpenedAt > 20f)
		{
			Fail("did not start in " + 20f + " s");
		}
	}

	private void OnGUI()
	{
		if (!finished && began && GateOpen && Event.current.type == EventType.Repaint)
		{
			GUI.depth = -10000;
			Rect position = new Rect(0f, 0f, Screen.width, Screen.height);
			GUI.DrawTexture(position, Texture2D.blackTexture);
			if (firstFrameSeen && target != null)
			{
				GUI.DrawTexture(position, target, ScaleMode.ScaleToFit, alphaBlend: false);
			}
			if (hintStyle == null)
			{
				hintStyle = new GUIStyle(GUI.skin.label)
				{
					alignment = TextAnchor.LowerRight,
					fontSize = Mathf.Max(12, Screen.height / 60)
				};
			}
			GUI.Label(new Rect(0f, 0f, Screen.width - 16, Screen.height - 10), "Пробел / Esc: пропустить", hintStyle);
		}
	}

	private void Finish(string how)
	{
		if (finished)
		{
			return;
		}
		finished = true;
		try
		{
			if (player != null)
			{
				player.Stop();
				UnityEngine.Object.Destroy(player);
			}
		}
		catch
		{
		}
		if (target != null)
		{
			target.Release();
			UnityEngine.Object.Destroy(target);
		}
		AudioListener.volume = wantedVolume;
		log.LogInfo("Intro video " + how + " after " + (Time.realtimeSinceStartup - begun).ToString("0.0") + " s; main menu is shown.");
		UnityEngine.Object.Destroy(this);
	}
}
