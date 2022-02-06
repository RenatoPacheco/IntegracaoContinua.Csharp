rm -rf ./coverage
dotnet restore
dotnet build --configuration=Release --no-restore
dotnet test --no-build --verbosity=normal --collect:"XPlat Code Coverage" --results-directory ./coverage
reportgenerator "-reports:coverage/**/coverage.cobertura.xml" "-targetdir:coverage/report" -reporttypes:Html