param(
    [string]$File = 'D:\Programs\ThunderStore\Data\LethalCompany\profiles\123\BepInEx\p_startup.txt',
    [int]$Top = 25,
    [double]$From = 0,
    [double]$To = 1e9
)
# Summarises the output of tools/dev/StartupProbe: scene loads, frames over 200 ms and where the sampled main-thread stacks spend their time.
# Each "S" line is one sample (every 200 ms): "S <age> <scene> | frame < caller < caller ..." (innermost frame first).
# Frames are managed methods (named by mono_pmip) or "module.dll+0xOffset" for native code.
function Clean([string]$f) {
    $f = $f.Trim() -replace '\s*\[\{[0-9A-Fa-f]+\}.*$', ''
    $f = $f -replace '^\(wrapper [^)]*\)\s*', ''
    $f = $f -replace '\s\(.*$', ''
    if ($f -match '^([\w\.\-]+\.(dll|exe))\+0x') { return $Matches[1] }
    return $f
}
$lines = [System.IO.File]::ReadAllLines($File)
$samples = @(); $hitches = @(); $scenes = @()
foreach ($l in $lines) {
    if ($l.StartsWith('S ')) {
        $bar = $l.IndexOf(' | ')
        if ($bar -lt 0) { continue }
        $head = $l.Substring(2, $bar - 2).Split(' ')
        $age = [double]::Parse($head[0].Replace(',', '.'), [Globalization.CultureInfo]::InvariantCulture)
        if ($age -lt $From -or $age -gt $To) { continue }
        $frames = @($l.Substring($bar + 3).Split(@(' < '), [StringSplitOptions]::RemoveEmptyEntries) | ForEach-Object { Clean $_ } | Where-Object { $_ })
        $samples += [pscustomobject]@{ Age = $age; Scene = $head[1]; Frames = $frames }
    }
    elseif ($l.StartsWith('H ')) { $hitches += $l }
    elseif ($l.StartsWith('SCENE ') -or $l.StartsWith('INIT') -or $l.StartsWith('QUIT')) { $scenes += $l }
}
"== markers"; $scenes
"== samples: $($samples.Count) (about $([math]::Round($samples.Count * 0.2, 1)) s of sampled wall time)"
"== frames over 200 ms: $($hitches.Count)"; $hitches | Select-Object -First 20

# the first frame that does not belong to the runtime / Unity plumbing names the culprit better than the innermost frame
$plumbing = '^(System\.|UnityEngine\.|Mono\.|MonoMod\.|HarmonyLib\.(?!AccessTools)|Microsoft\.|BepInEx\.|mono-2\.0|UnityPlayer|ntdll|KERNEL|Lethal Company|mscorlib|0x|\(wrapper|object:|intptr:|string:|char:|<0|<Module>|win32u|bcrypt|nvapi|[\w\-]+\.dll)'
$leaf = @{}; $incl = @{}
foreach ($s in $samples) {
    $first = $s.Frames | Where-Object { $_ -notmatch $plumbing } | Select-Object -First 1
    if (-not $first) { $first = '(runtime only) ' + ($s.Frames | Select-Object -First 1) }
    $leaf[$first] = 1 + [int]$leaf[$first]
    foreach ($f in ($s.Frames | Select-Object -Unique)) { $incl[$f] = 1 + [int]$incl[$f] }
}
"== top by first non-runtime frame (samples x 0.2 s)"
$leaf.GetEnumerator() | Sort-Object Value -Descending | Select-Object -First $Top | ForEach-Object { '{0,5}  {1}' -f $_.Value, $_.Key }
"== top by inclusive frame"
$incl.GetEnumerator() | Sort-Object Value -Descending | Select-Object -First $Top | ForEach-Object { '{0,5}  {1}' -f $_.Value, $_.Key }

# the most common call paths made of non-plumbing frames only (innermost first): shows who asked for the expensive work
"== most common call paths (non-runtime frames, innermost first)"
$paths = @{}
foreach ($s in $samples) {
    $key = [string](($s.Frames | Where-Object { $_ -notmatch $plumbing } | Select-Object -First 5) -join ' < ')
    $paths[$key] = 1 + [int]$paths[$key]
}
$paths.GetEnumerator() | Sort-Object Value -Descending | Select-Object -First $Top | ForEach-Object { '{0,5}  {1}' -f $_.Value, $_.Key.Substring(0, [Math]::Min(360, $_.Key.Length)) }
