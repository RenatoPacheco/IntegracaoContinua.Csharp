rm -rf test/TestResults
dotnet restore
dotnet build --configuration=Release --no-restore
dotnet test --no-build --verbosity=normal --collect:"XPlat Code Coverage"
reportgenerator "-reports:**/coverage.cobertura.xml" "-targetdir:test/TestResults/CoverageReport" -reporttypes:Html