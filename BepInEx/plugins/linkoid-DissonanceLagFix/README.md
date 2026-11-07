# Dissonance Lag Fix

This plugin significantly reduces the duration of lag spikes simply by changing the log level of DissonanceVoip.
https://github.com/linkoid/Linkoid.Dissonance.LagFix

I profiled the game and found that whenever a lag spike occurs, Dissonance starts spamming warnings-- an I/O operation-- which ended up extending the duration of the lag spike. After lots of experimentation, I found that I was able to cut the duration of the lag spikes in half by changing Dissonance's log level to only log errors.

Here are some examples of the warnings that would spam during a lag spike:
```
[Dissonance:Recording] BasePreprocessingPipeline: Lost X samples in the preprocessor (buffer full), injecting silence to compensate
[Dissonance:Recording] BasePreprocessingPipeline: Preprocessor running slow! Iteration took:Xms for X frames"
[Dissonance:Playback] EncodedAudioBuffer: Encoded audio heap is getting very large (X items)
```

## [Join the Déjà Drift Discord Server](https://discord.gg/yKwt2AWcGF)
