<#
  Builds every mod in this repository and packs one Thunderstore zip per mod into dist\.

  No Visual Studio or NuGet needed: the .NET SDK's own Roslyn compiler (csc) is run against the game's Managed folder
  and a BepInEx 5 "core" folder, so it also works offline.

  Usage:   powershell -ExecutionPolicy Bypass -File tools\build.ps1 [-Only GraphicsPlus]
  Tweak the two paths below (or pass them as parameters) if your game / BepInEx live elsewhere.
#>
param(
    [string]$GameDir    = 'D:\Programs\steamapps\common\Lethal Company',
    [string]$BepInExCore = 'D:\Programs\ThunderStore\Data\LethalCompany\profiles\123\BepInEx\core',
    [string]$Team       = 'devil1nme',
    [string]$Only       = ''
)
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.IO.Compression, System.IO.Compression.FileSystem, System.Drawing

$root    = Split-Path -Parent $PSScriptRoot
$managed = Join-Path $GameDir 'Lethal Company_Data\Managed'
$build   = Join-Path $root 'build'
$dist    = Join-Path $root 'dist'

# --- locate the Roslyn compiler shipped with the .NET SDK ---------------------------------------------------------
$dotnetRoot = Split-Path -Parent (Get-Command dotnet -ErrorAction Stop).Source
$csc = Get-ChildItem (Join-Path $dotnetRoot 'sdk') -Directory | Sort-Object Name -Descending |
       ForEach-Object { Join-Path $_.FullName 'Roslyn\bincore\csc.dll' } | Where-Object { Test-Path $_ } | Select-Object -First 1
if (-not $csc) { throw 'csc.dll not found: install the .NET SDK (https://dotnet.microsoft.com/download).' }
foreach ($p in @($managed, $BepInExCore)) { if (-not (Test-Path $p)) { throw "Path not found: $p" } }

$unityRefs = Get-ChildItem $managed -Filter *.dll |
    Where-Object { $_.Name -match '^(mscorlib|netstandard|System[\w\.]*|UnityEngine[\w\.]*|Unity\.[\w\.]+|Assembly-CSharp|Facepunch[\w\.\-]*|Dissonance[\w\.]*)\.dll$' } |
    ForEach-Object { $_.FullName }
$baseRefs  = Get-ChildItem $managed -Filter *.dll | Where-Object { $_.Name -match '^(mscorlib|netstandard|System|System\.Core)\.dll$' } | ForEach-Object { $_.FullName }
$pluginRefs  = $unityRefs + @((Join-Path $BepInExCore 'BepInEx.dll'), (Join-Path $BepInExCore '0Harmony.dll'))
$patcherRefs = $baseRefs  + @((Join-Path $BepInExCore 'BepInEx.dll'), (Join-Path $BepInExCore 'Mono.Cecil.dll'))

# --- what to build ------------------------------------------------------------------------------------------------
$mods = @(
    @{ Name = 'LCCompatFixes'; Parts = @(
        @{ Out = 'LCCompatFixes.Patcher'; Src = 'src\Patcher.cs'; Refs = $patcherRefs; Zip = 'patchers/LCCompatFixes.Patcher.dll' },
        @{ Out = 'LCCompatFixes';         Src = 'src\Plugin.cs';  Refs = $pluginRefs;  Zip = 'plugins/LCCompatFixes.dll' }) },
    @{ Name = 'GraphicsPlus'; SharedMenu = $true; Parts = @(
        @{ Out = 'GraphicsPlus'; Src = 'src\*.cs'; Refs = $pluginRefs; Zip = 'plugins/GraphicsPlus.dll' }) },
    @{ Name = 'LanguageSwitch'; SharedMenu = $true; Parts = @(
        @{ Out = 'LanguageSwitch'; Src = 'src\*.cs'; Refs = $pluginRefs; Zip = 'plugins/LanguageSwitch.dll' }) }
)

New-Item -ItemType Directory -Force $build, $dist | Out-Null

foreach ($mod in $mods) {
    if ($Only -and $Only -ne $mod.Name) { continue }
    $modDir = Join-Path $root $mod.Name
    Write-Host "== $($mod.Name)" -ForegroundColor Cyan

    # compile
    $outDir = Join-Path $build $mod.Name
    New-Item -ItemType Directory -Force $outDir | Out-Null
    $entries = @()
    foreach ($part in $mod.Parts) {
        $dll = Join-Path $outDir ($part.Out + '.dll')
        $rsp = Join-Path $outDir ($part.Out + '.rsp')
        $sources = @(Get-ChildItem (Join-Path $modDir $part.Src) | ForEach-Object { $_.FullName })
        if ($mod.SharedMenu) {
            # Shared/SettingsMenu.cs is compiled into each mod under that mod's own namespace
            $generated = Join-Path $outDir 'SettingsMenu.generated.cs'
            $text = (Get-Content -Raw -Encoding UTF8 (Join-Path $root 'Shared\SettingsMenu.cs')).Replace('__NS__', $mod.Name)
            [System.IO.File]::WriteAllText($generated, $text, (New-Object System.Text.UTF8Encoding($false)))
            $sources += $generated
        }
        $lines = @('-nologo', '-nostdlib+', '-deterministic+', '-target:library', '-optimize+', '-langversion:9', '-nullable:disable', '-warn:3', "-out:$dll")
        foreach ($r in $part.Refs) { $lines += "-r:`"$r`"" }
        foreach ($s in $sources)   { $lines += "`"$s`"" }
        Set-Content -LiteralPath $rsp -Value $lines -Encoding UTF8
        & dotnet $csc "@$rsp"
        if ($LASTEXITCODE -ne 0) { throw "Compilation failed: $($part.Out)" }
        Write-Host "   built $($part.Out).dll"
        $entries += @{ File = $dll; Entry = $part.Zip }
    }

    # validate the package metadata the way Thunderstore does
    $pkg = Join-Path $modDir 'package'
    $manifest = Get-Content -Raw -Encoding UTF8 (Join-Path $pkg 'manifest.json') | ConvertFrom-Json
    if ($manifest.name -notmatch '^[A-Za-z0-9_]{1,128}$') { throw "manifest name invalid: $($manifest.name)" }
    if ($manifest.version_number -notmatch '^\d+\.\d+\.\d+$') { throw "version_number invalid: $($manifest.version_number)" }
    if ($manifest.description.Length -gt 250) { throw "description too long ($($manifest.description.Length) > 250)" }
    if ($null -eq $manifest.PSObject.Properties['website_url'] -or $null -eq $manifest.PSObject.Properties['dependencies']) { throw 'manifest needs website_url and dependencies' }
    $icon = [System.Drawing.Image]::FromFile((Join-Path $pkg 'icon.png'))
    if ($icon.Width -ne 256 -or $icon.Height -ne 256) { throw "icon.png must be 256x256 (is $($icon.Width)x$($icon.Height))" }
    $icon.Dispose()
    foreach ($f in 'README.md', 'manifest.json', 'CHANGELOG.md') {
        $bytes = [System.IO.File]::ReadAllBytes((Join-Path $pkg $f))
        if ($bytes.Length -ge 3 -and $bytes[0] -eq 0xEF -and $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF) { throw "$f must be UTF-8 without BOM" }
    }

    # zip: files at the root of the archive, forward slashes, no wrapping folder
    $zipPath = Join-Path $dist ("{0}-{1}-{2}.zip" -f $Team, $manifest.name, $manifest.version_number)
    if (Test-Path $zipPath) { Remove-Item $zipPath }
    $zip = [System.IO.Compression.ZipFile]::Open($zipPath, 'Create')
    try {
        foreach ($f in 'manifest.json', 'README.md', 'icon.png', 'CHANGELOG.md', 'LICENSE') {
            [void][System.IO.Compression.ZipFileExtensions]::CreateEntryFromFile($zip, (Join-Path $pkg $f), $f, 'Optimal')
        }
        foreach ($e in $entries) {
            [void][System.IO.Compression.ZipFileExtensions]::CreateEntryFromFile($zip, $e.File, $e.Entry, 'Optimal')
        }
        # extra files shipped with the mod (e.g. LanguageSwitch's German dictionary under config/)
        $cfg = Join-Path $pkg 'config'
        if (Test-Path $cfg) {
            Get-ChildItem $cfg -Recurse -File | ForEach-Object {
                $rel = $_.FullName.Substring($pkg.Length + 1).Replace('\', '/')
                [void][System.IO.Compression.ZipFileExtensions]::CreateEntryFromFile($zip, $_.FullName, $rel, 'Optimal')
            }
        }
    } finally { $zip.Dispose() }
    Write-Host ("   packed {0} ({1:N1} KB)" -f (Split-Path -Leaf $zipPath), ((Get-Item $zipPath).Length / 1KB)) -ForegroundColor Green
}
