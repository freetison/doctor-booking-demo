## doctor-booking-demo

## Features

[x] Vertical Slice Architecture: https://www.jimmybogard.com/vertical-slice-architecture/

[x] Fast-endpoints: https://fast-endpoints.com/

[x] RestSharp: https://restsharp.dev/

[x] Docker

[ ] Database: it just a demo, no persistance here.

[x] Cors

[x] Swagger

[x] User Secretsonly for demo

[x] Test : Nunit and shouldly

## Execute using docker.

    Need to have docker running
    Execeute ``` docker-compose up --build ```  in the project root
    open http://localhost:4201/ in a browser
    ** this is just a demo, select a Monday and then pick a time for the Appointment

## Generating Code Coverage HTML Report

    Execute all from bash
    dotnet tool install -g dotnet-reportgenerator-globaltool
    dotnet tool install -g coverlet.console

    # Run tests from /test/Api.Test
    ./generate-coverage-report.sh
