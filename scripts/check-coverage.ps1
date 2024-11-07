$coveragePercentage = Select-String -Path './coverage/lcov.info' -Pattern '"lineCoverage":\K[0-9.]*' | ForEach-Object { $_.Matches.Groups[0].Value }

Write-Host "Coverage Percentage: $coveragePercentage"

$coverageThreshold = $env:COVERAGE_THRESHOLD

if ([double]$coveragePercentage -lt [double]$coverageThreshold) {
    Write-Host "Coverage percentage is below threshold ($coverageThreshold%)."
    exit 1
} else {
    Write-Host "Coverage meets the threshold."
}