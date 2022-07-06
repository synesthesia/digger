
if (test-path ./nupkg) {
    remove-item ./nupkg -Force -Recurse
}   

dotnet build -c Release

$filename = gci "./nupkg/*.nupkg" | sort LastWriteTime | select -last 1 | select -ExpandProperty "Name"
Write-host $filename

$len = $filename.length
Write-host $len

if ($len -gt 0) {
    Write-Host "pushing to Nuget..."
    nuget push  ".\nupkg\$filename" -source nuget.org
}