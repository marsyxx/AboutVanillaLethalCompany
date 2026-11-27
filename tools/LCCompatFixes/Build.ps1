param(
    [Parameter(Mandatory)][string]$GameDir,
    [Parameter(Mandatory)][string]$BepInExCore,
    [Parameter(Mandatory)][string]$OutputDir
)

$ErrorActionPreference = 'Stop'
$managed = Join-Path (Resolve-Path -LiteralPath $GameDir) 'Lethal Company_Data/Managed'
$core = Resolve-Path -LiteralPath $BepInExCore
$output = (New-Item -ItemType Directory -Force -Path $OutputDir).FullName
$dotnet = (Get-Command dotnet).Source
$sdkVersion = (& $dotnet --version).Trim()
$csc = Join-Path (Split-Path $dotnet) "sdk/$sdkVersion/Roslyn/bincore/csc.dll"
if (!(Test-Path -LiteralPath $csc)) {
    throw "Roslyn compiler not found at $csc"
}

$refs = @(Get-ChildItem -LiteralPath $managed -Filter '*.dll' | Where-Object {
    $_.Name -match '^(mscorlib|netstandard|System[\w.]*|UnityEngine[\w.]*|Unity\.[\w.]+|Assembly-CSharp|Facepunch[\w.\-]*|Dissonance[\w.]*)\.dll$'
} | ForEach-Object FullName)
$refs += Join-Path $core 'BepInEx.dll'
$refs += Join-Path $core '0Harmony.dll'
$sources = Get-ChildItem -LiteralPath $PSScriptRoot -Filter '*.cs'
$dll = Join-Path $output 'LCCompatFixes.dll'
$response = Join-Path $output 'LCCompatFixes.rsp'
$arguments = @('-nologo', '-nostdlib+', '-deterministic+', '-target:library',
    '-optimize+', '-langversion:12', '-nullable:disable', '-warnaserror+', ('-out:"' + $dll + '"'))
$arguments += $refs | ForEach-Object { '-r:"' + $_ + '"' }
$arguments += $sources | ForEach-Object { '"' + $_.FullName + '"' }
Set-Content -LiteralPath $response -Value $arguments -Encoding UTF8
& $dotnet $csc "@$response"
if ($LASTEXITCODE -ne 0) {
    throw "LCCompatFixes compilation failed with exit code $LASTEXITCODE."
}
