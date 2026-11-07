param(
    [string]$File = 'D:\Programs\ThunderStore\Data\LethalCompany\profiles\123\BepInEx\p_startup.txt',
    [double]$AfterMenu = 30   # how many seconds after the main menu loaded still count as "starting up"
)
# Numbers from one StartupProbe run (see tools/dev/README.md): when the scenes loaded and how long the game stood still.
# "Frozen" = the sum of all frames longer than 200 ms from the first frame until $AfterMenu seconds after the main menu appeared.
$ci = [Globalization.CultureInfo]::InvariantCulture
function Num([string]$s) { [double]::Parse($s.Replace(',', '.'), $ci) }
$scene = @{}; $frames = @()
foreach ($l in [System.IO.File]::ReadAllLines($File)) {
    if ($l -match '^SCENE ([\d,\.]+) (\S+)') { if (-not $scene.ContainsKey($Matches[2])) { $scene[$Matches[2]] = Num $Matches[1] } }
    elseif ($l -match '^H ([\d,\.]+) dt=([\d,\.]+)') { $frames += [pscustomobject]@{ Age = Num $Matches[1]; Dt = Num $Matches[2] } }
}
$menu = $scene['MainMenu']
$until = if ($menu) { $menu + $AfterMenu } else { 1e9 }
$counted = @($frames | Where-Object { $_.Age -le $until })
$over1 = @($counted | Where-Object { $_.Dt -ge 1.0 })
[pscustomobject]@{
    'first scene (s)'      = $scene['InitSceneLaunchOptions']
    'main menu (s)'        = $menu
    'frames >0.2 s'        = $counted.Count
    'frames >=1 s'         = $over1.Count
    'longest frame (s)'    = ($counted | Measure-Object Dt -Maximum).Maximum
    'frozen total (s)'     = [math]::Round(($counted | Measure-Object Dt -Sum).Sum, 1)
    'frozen in >=1 s (s)'  = [math]::Round(($over1 | Measure-Object Dt -Sum).Sum, 1)
} | Format-List
