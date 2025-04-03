#!/bin/bash
# This script sets up and runs the FipeConsumer application, including the worker, API, and frontend.
echo "Starting the worker..."
cd ../FipeConsumer.Worker || exit
dotnet run &
WORKER_PID=$!
echo "Worker started with PID $WORKER_PID"

# Navigate to the API directory and start the API
echo "Starting the API..."
cd ../FipeConsumer.API || exit
dotnet run &
API_PID=$!
echo "API started with PID $API_PID"

# Navigate to the frontend directory, install dependencies, and start the frontend
echo "Setting up the frontend..."
cd ../FipeConsumer.Front || exit
npm i &
npm run build &
npm run preview &
FRONTEND_PID=$!
echo "Frontend started with PID $FRONTEND_PID"

# Wait a few seconds to ensure the frontend server is up
sleep 5

# Open the browser to the frontend URL
echo "Opening the browser to http://localhost:5005/home..."
start http://localhost:5005/

# Wait for user to terminate the script
echo "All services are running. Press Ctrl+C to stop."
trap "echo 'Stopping services...'; kill $WORKER_PID $API_PID $FRONTEND_PID; exit" SIGINT
wait