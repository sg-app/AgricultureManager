param(
    [version]$startVersion = [version]::new("0.0.0.0")
)
# PowerShell-Skript zur Berechnung einer semantischen Version basierend auf Git-Commit-Nachrichten und aktualisierung der .csproj-Datei

function Get-SemanticVersion {
    param (
        [string[]] $commitMessages,
        [version]$startVersion
    )

    $major = $startVersion.Major
    $minor = $startVersion.Minor
    $patch = $startVersion.Build

    [array]::Reverse($commitMessages)
    foreach ($message in $commitMessages) {
        if ($message -match "^(build|chore|ci|docs|feat|fix|perf|refactor|revert|style|test)(\([\w\s-]*\))?(!:|:.*\n\n((.+\n)+\n)?BREAKING CHANGE:\s.+)") {
            $major++
            $minor = 0
            $patch = 0
        }
        elseif ($message -match "^(feat)(\([\w\s-]*\))?:") {
            $minor++
            $patch = 0
        }
        elseif ($message -match "^(build|chore|ci|docs|fix|perf|refactor|revert|style|test)(\([\w\s-]*\))?:") {
            $patch++
        }
    }

    return "$major.$minor.$patch"
}

function Update-CsprojVersion {
    param (
        [string] $csprojFilePath,
        [string] $newVersion
    )

    [xml]$xml = Get-Content -Path $csprojFilePath

    $versionElement = $xml.SelectSingleNode("//Project/PropertyGroup/Version")
    if ($versionElement) {
        $versionElement.'#text' = $newVersion
    } 
    else {
        $propertyGroup = $xml.CreateElement("PropertyGroup")
        $xml.Project.AppendChild($propertyGroup)
        $versionElement = $xml.CreateElement("Version")
        $versionElement.InnerText = $newVersion
        $propertyGroup.AppendChild($versionElement)
    }

    $xml.Save($csprojFilePath)
}

$slnPath = ""
$projPath = "AgricultureManager.CoreApp"
$startVersion = "0.0.0.0"
$projName = "agriculturemanager"
$registry = "dockerhub.gschreiner.ddnss.de"

Set-Location "$PSScriptRoot\$slnPath"

$commitMessages = git log --pretty=format:"%s" | Select-String -Pattern "^(.*)"

if ($commitMessages.Count -gt 0) {
    $semanticVersion = Get-SemanticVersion -commitMessages $commitMessages -startVersion $startVersion
    Write-Output "Die berechnete semantische Version ist: $semanticVersion"

    Set-Location "$PSScriptRoot\$slnPath\$projPath"
    $csprojFilePath = Get-Item *.csproj
    Update-CsprojVersion -csprojFilePath $csprojFilePath -newVersion $semanticVersion
    Write-Output "Die Version in der .csproj-Datei wurde erfolgreich aktualisiert."
    

    Set-Location "$PSScriptRoot\$slnPath"
    docker build -f ./${projPath}/Dockerfile -t ${registry}/${projName}:latest -t ${registry}/${projName}:${semanticVersion} .
    if ($LASTEXITCODE -ne 0) { Write-Output "Docker Build Failed. Aborting further execution." exit $LASTEXITCODE }

    docker push ${registry}/${projName}:latest
    if ($LASTEXITCODE -ne 0) { Write-Output "Docker Push (latest) Failed. Aborting further execution." exit $LASTEXITCODE }

    docker push ${registry}/${projName}:${semanticVersion}
    if ($LASTEXITCODE -ne 0) { Write-Output "Docker Push (${semanticVersion}) Failed. Aborting further execution." exit $LASTEXITCODE }

    git add *.csproj
    git commit -m "New Version ${semanticVersion} created" 
    git tag v${semanticVersion}
} 
else {
    Write-Output "Es wurden keine Git-Commits im aktuellen Verzeichnis gefunden."
}
