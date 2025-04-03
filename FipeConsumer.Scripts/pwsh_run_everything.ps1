$processes = @()

function Cleanup {
    Write-Host "Stopping all services..."
    foreach ($pid in $processes) {
        try {
            Stop-Process -Id $pid -Force -ErrorAction SilentlyContinue
            Write-Host "Stopped process with PID $pid"
        } catch {
            Write-Host "Failed to stop process with PID $pid"
        }
    }
    exit
}

trap {
    Cleanup
}

Write-Host "Starting the worker..."
Push-Location ../FipeConsumer.Worker
$workerProcess = Start-Process -FilePath "dotnet" -ArgumentList "run" -NoNewWindow -PassThru
$processes += $workerProcess.Id
Pop-Location
Write-Host "Worker started with PID $($workerProcess.Id)"

Write-Host "Starting the API..."
Push-Location ../FipeConsumer.API
$apiProcess = Start-Process -FilePath "dotnet" -ArgumentList "run" -NoNewWindow -PassThru
$processes += $apiProcess.Id
Pop-Location
Write-Host "API started with PID $($apiProcess.Id)"

Write-Host "Setting up the frontend..."
Push-Location ../FipeConsumer.Front
npm install
$frontendProcess = Start-Process -FilePath "npm" -ArgumentList "run build && npm run preview" -NoNewWindow -PassThru
$processes += $frontendProcess.Id
Pop-Location
Write-Host "Frontend started with PID $($frontendProcess.Id)"

Start-Sleep -Seconds 10

Write-Host "Opening the browser to http://localhost:5005/..."
Start-Process "http://localhost:5005/"

Write-Host "All services are running. Press Ctrl+C to stop."

while ($true) {
    Start-Sleep -Seconds 1
}