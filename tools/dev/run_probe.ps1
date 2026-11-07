param([string]$Name, [string]$Pattern, [int]$TimeoutSec = 260, [int]$Width = 1280, [int]$Height = 720)
$sp = Split-Path -Parent $MyInvocation.MyCommand.Path
$man  = 'D:\Programs\steamapps\common\Lethal Company\Lethal Company_Data\Managed'
$profile = 'D:\Programs\ThunderStore\Data\LethalCompany\profiles\123'
$core = "$profile\BepInEx\core"
$csc  = 'C:\Program Files\dotnet\sdk\10.0.403\Roslyn\bincore\csc.dll'
$gameDir = 'D:\Programs\steamapps\common\Lethal Company'
$out = "$sp\out"; New-Item -ItemType Directory -Force $out | Out-Null

$refs = (Get-ChildItem $man -Filter *.dll | Where-Object { $_.Name -match '^(mscorlib|netstandard|System[\w\.]*|UnityEngine[\w\.]*|Unity\.[\w\.]+|Assembly-CSharp)\.dll$' } | ForEach-Object { $_.FullName }) + @("$core\BepInEx.dll", "$core\0Harmony.dll")
$lines = @('-nologo', '-noconfig', '-nostdlib+', '-target:library', '-optimize+', '-langversion:9', '-nullable:disable', '-warn:3', "-out:$out\$Name.dll")
foreach ($r in $refs) { $lines += "-r:`"$r`"" }
$lines += "`"$sp\src\$Name\Plugin.cs`""
Set-Content -LiteralPath "$out\$Name.rsp" -Value $lines -Encoding UTF8
$err = (& dotnet $csc "@$out\$Name.rsp" 2>&1 | Out-String) -split "`n" | Where-Object { $_ -match 'error' } | Select-Object -First 6
if ($err) { "COMPILE ERRORS:"; $err; return }
if (Get-Process | Where-Object { $_.ProcessName -eq 'Lethal Company' }) { "GAME RUNNING (the user's) - not launching"; return }

# configs the probe may touch are restored afterwards
$cfgs = @{}
foreach ($n in 'lc.graphicsplus.cfg', 'lc.languageswitch.cfg') { $f = "$profile\BepInEx\config\$n"; $cfgs[$n] = if (Test-Path $f) { [IO.File]::ReadAllText($f) } else { $null } }
Get-ChildItem "$profile\BepInEx" -File | Where-Object { $_.Name -match '^p_' } | Remove-Item -Force -ErrorAction SilentlyContinue
$probe = "$profile\BepInEx\plugins\$Name.dll"
Copy-Item "$out\$Name.dll" $probe -Force
try {
    $p = Start-Process -FilePath "$gameDir\Lethal Company.exe" -WorkingDirectory $gameDir -PassThru -ArgumentList @('--doorstop-enabled', 'true', '--doorstop-target-assembly', "$core\BepInEx.Preloader.dll", '-screen-fullscreen', '0', '-screen-width', "$Width", '-screen-height', "$Height")
    if (-not $p.WaitForExit($TimeoutSec * 1000)) { "timeout - killing"; $p | Stop-Process -Force }
    "exited"
}
finally {
    Remove-Item $probe -Force -ErrorAction SilentlyContinue
    Get-Process | Where-Object { $_.ProcessName -match 'UnityCrashHandler' } | Stop-Process -Force -ErrorAction SilentlyContinue
    foreach ($n in $cfgs.Keys) { $f = "$profile\BepInEx\config\$n"; if ($null -ne $cfgs[$n]) { [IO.File]::WriteAllText($f, $cfgs[$n], (New-Object System.Text.UTF8Encoding($false))) } else { Remove-Item $f -Force -ErrorAction SilentlyContinue } }
}
Select-String -Path "$profile\BepInEx\LogOutput.log" -Pattern $Pattern | ForEach-Object { $_.Line.Substring(0, [Math]::Min(360, $_.Line.Length)) }
Get-ChildItem "$profile\BepInEx" -File | Where-Object { $_.Name -match '^p_' } | Select-Object Name, Length
