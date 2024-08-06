#!/bin/bash

# Run the tests and collect coverage
dotnet test Api.Test.csproj -c Release --collect:"XPlat Code Coverage" --settings ./coverlet.runsettings

# Find the coverage file
COVERAGE_FILE=$(find TestResults -name 'coverage.cobertura.xml')

# Generate the report
reportgenerator -reports:$COVERAGE_FILE -targetdir:./TestResults/coverage-report -reporttypes:Html

