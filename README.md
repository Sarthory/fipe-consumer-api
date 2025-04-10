# Fipe Consumer

## Description

This project was implemented to participate in a technical test proposed by Hahn Software.
The requirements were followed strictly, with an effort to adhere to all the rules and regulations.
This project consists of 3 layers:

- Worker, located at ./FipeConsumer.Worker
- API, located at ./FipeConsumer.API
- Front, located at ./FipeConsumer.Front

The worker, with an hourly job, requests and consumes the [Fipe API](https://deividfortuna.github.io/fipe/) content and stores the data on an MSSQL instance.

The API serves the "cloned" data in 4 basic endpoints; Brands, Models, Years, and Price.

The Front is responsible for conducting the user through the flow and showing a specific selected vehicle's current price and details, based on Fipe API.

## Table of Contents

- [Description](#description)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Running the Project](#running-the-project)
- [Usage](#usage)
- [Contact](#contact)

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- .NET 9+
- Node.js 18+
- Docker (optional, for data)
- Database MSSQL 2022

### Installation

Clone this repository in your local machine.

Make sure that your MSSQL instance is running.

Adjust _appettings.json_ connection strings for these projects:

- FipeConsumer.API _(your_project_path/fipe-consumer-api/FipeConsumer.API)_
- FipeConsumer.Worker _(your_project_path/fipe-consumer-api/FipeConsumer.Worker)_

Run the following command on the solution root folder _(your_project_path/fipe-consumer-api)_:

`dotnet ef database update --project FipeConsumer.Infrastructure --startup-project FipeConsumer.API`

This will create the database, tables, and associations.

## Running the Project

Navigate to the Scripts folder _(your_project_path/fipe-consumer-api/FipeConsumer.Scripts)_

- If you are using bash, execute the following script:

  `./bash_run_everything.sh`

- If you are using Powershell, execute the following script:

  `./pwsh_run_everything.ps1`

The Worker, API, and Front will start, and a browser window should pop up, loading the frontend at http://localhost:5005/

###### **Note:** Notice that, for the front to show content, the Worker must run first to populate the database. You can wait for an automatic execution, hourly, or you can trigger the Job manually by visiting http://localhost:5000/hangfire/recurring and triggering "fipe-upsert-job" manually.

## Usage

With the projects running, you should be able to:

- Access the Hangfire dashboard to consult and trigger Jobs:
  - Visit http://localhost:5000/hangfire
- Access the Swagger page to consult available endpoints on API:
  - Visit http://localhost:5001/swagger/index.html
- Access the frontend to navigate and consult available car prices:
  - Visit http://localhost:5005/

To see the presentation video of this software, [Click here](https://mega.nz/file/IkFTGKLa#ck9KWbyMScehO7HZrHDsJm-QslsvbVZnqSCUo-A29Ek)

## Contact

**Felipe Sartori** - felipe@sartori.app

[Sartori Apps](https://www.sartori.app) - [Linkedin](#https://www.linkedin.com/in/ff-sartori/)
