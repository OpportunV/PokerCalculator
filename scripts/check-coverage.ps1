$lf = 0
$lh = 0

Select-String -Path './coverage/lcov.info' -Pattern '^LF:(\d+)' | ForEach-Object {
    $lf += [int]$_.Matches.Groups[1].Value
}

Select-String -Path './coverage/lcov.info' -Pattern '^LH:(\d+)' | ForEach-Object {
    $lh += [int]$_.Matches.Groups[1].Value
}

if ($lf -eq 0) {
    Write-Host "No lines found in the coverage data. Coverage cannot be calculated."
    exit 1
}

$coveragePercentage = ($lh / $lf) * 100

Write-Host "Coverage Percentage: $coveragePercentage%"

$coverageThreshold = $env:COVERAGE_THRESHOLD

if ($coveragePercentage -lt [double]$coverageThreshold) {
    Write-Host "Coverage percentage is below threshold ($coverageThreshold%)."
    exit 1
} else {
    Write-Host "Coverage meets the threshold."
}