# IntegracaoContinua.Csharp

### Github badge

[![Licensed under the MIT License](https://img.shields.io/badge/License-MIT-blue.svg)](./LICENSE)
[![Build](https://github.com/RenatoPacheco/IntegracaoContinua.Csharp/workflows/Build/badge.svg?branch=main)](https://github.com/RenatoPacheco/IntegracaoContinua.Csharp/actions/workflows/build.yml)
[![Integration Tests](https://github.com/RenatoPacheco/IntegracaoContinua.Csharp/workflows/Integration%20Tests/badge.svg?branch=main)](https://github.com/RenatoPacheco/IntegracaoContinua.Csharp/actions/workflows/integration-tests.yml)
[![codecov](https://codecov.io/gh/RenatoPacheco/IntegracaoContinua.Csharp/branch/main/graph/badge.svg?token=6YLN9GKD8X)](https://codecov.io/gh/RenatoPacheco/IntegracaoContinua.Csharp)

### Azure badge

[![Build Status](https://img.shields.io/azure-devops/build/renatopacheco/IntegracaoContinua.Csharp/7/main)](https://renatopacheco.visualstudio.com/IntegracaoContinua.Csharp/_build/latest?definitionId=7&branchName=main)
[![Pipeline Status](https://renatopacheco.visualstudio.com/IntegracaoContinua.Csharp/_apis/build/status/Integration%20Tests?branchName=main)](https://renatopacheco.visualstudio.com/IntegracaoContinua.Csharp/_build/latest?definitionId=7&branchName=main)
[![Coverage Status](https://img.shields.io/azure-devops/coverage/renatopacheco/IntegracaoContinua.Csharp/7/main)](https://renatopacheco.visualstudio.com/IntegracaoContinua.Csharp/_build/latest?definitionId=7&branchName=main)
[![Test Status](https://img.shields.io/azure-devops/tests/renatopacheco/IntegracaoContinua.Csharp/7/main?compact_message&failed_label=failed&passed_label=passed&skipped_label=skipped)](https://renatopacheco.visualstudio.com/IntegracaoContinua.Csharp/_build/latest?definitionId=7&branchName=main)


Um projeto para fazer testes de configuração para integração contínua usando [GitHub Actions] e o [Azure Pipelines] além de que todo o projeto será feito com [Vscode], para não depender de recursos do [Visual Studio]. Já o código será feito com [.NET Core 3.1] e os testes em [.NET 5].

## Requisitos

Além do [Vscode], o projeto necessita de:

* [.Net 5] - Neste projeto a versão usada foi a [.Net 5.0.4](https://dotnet.microsoft.com/en-us/download/dotnet/5.0).
* [Coverlet] - Para gerar o arquivo XML de cobertura de testes.
* [Report Generator] - Vamos precisar dele para gerar os relatórios de testes usando o arquivo gerado pelo [Coverlet].

### Report Generator

O [Coverlet] é usado como um pacote no projeto de testes, para gerar o arquivo de cobertura de testes. Mas para gerar um relatório, é necessário que o [Report Generator] esteja instalado no computador, neste caso em escopo global. Nesse projeto estou usando a versão 4.8.6. 

```bash
dotnet tool install --global dotnet-reportgenerator-globaltool --version 4.8.6
```

# Testes

Para gerar o relatório de testes, execute o comando abaixo:

```bash
dotnet restore
dotnet build --configuration=Release --no-restore
dotnet test --no-build --verbosity=normal --collect:"XPlat Code Coverage" --results-directory ./coverage
reportgenerator "-reports:coverage/**/coverage.cobertura.xml" "-targetdir:coverage/report" -reporttypes:Html
```

A partir da execução, o comando irá gerar um relatório de testes no diretório **./coverage/report**.

Porém, a partir da segunda execução, pode haver problemas no relatório, pois ele lê todos os XMLs que encontrar no diretório, e não só o gerado pela primeira execução. Para resolver isso, é necessário apagar o diretório **./coverage** e executar o comando novamente. 

```bash
rm -rf ./coverage
dotnet restore
dotnet build --configuration=Release --no-restore
dotnet test --no-build --verbosity=normal --collect:"XPlat Code Coverage" --results-directory ./coverage
reportgenerator "-reports:coverage/**/coverage.cobertura.xml" "-targetdir:coverage/report" -reporttypes:Html
```

E para não ter de ficar executando cada comando a cada teste que queirar gerar o relatório, criei um [buildReport.sh](./buildReport.sh) para pra facilitar, assim é só executar o shell script abaixo:

```bash
./buildReport.sh
```

[GitHub Actions]:<https://help.github.com/en/actions/automating-your-workflow-with-github-actions>
[Azure Pipelines]:<https://docs.microsoft.com/en-us/azure/devops/pipelines/languages/csharp/>
[Vscode]:<https://code.visualstudio.com/>
[Visual Studio]:<https://visualstudio.microsoft.com/>
[.Net Core 3.1]:<https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-1>
[.NET 5]:<https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-5>
[Report Generator]:<https://github.com/danielpalme/ReportGenerator>
[Coverlet]:<https://github.com/coverlet-coverage/coverlet>
[shields.io]:<https://shields.io/category/coverage>