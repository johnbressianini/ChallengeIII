ChallengeApi é WebApi utilizando JWT, identity, testes unitários, testes integrados, Arquitetura baseado em DDD e o banco de dados em Postgres.
Publicado em um cluster Kubernets local.

Segue o arquivo Docker File.

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:8000,http://+:80
ENV ASPNETCORE_ENVIROMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApplication1/ChallengeApi.csproj", "WebApplication1/"]
RUN dotnet restore "WebApplication1/ChallengeApi.csproj"
COPY . .
WORKDIR "/src/WebApplication1"
RUN dotnet build "ChallengeApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChallengeApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChallengeApi.dll"]


Está configurado para deploys emo CI/ CD, com uma maquina virtual para ser o agent que recebe os artefatos publicados e também adicionado o Application Insight ao monitoramento.

ChallengeApiPI ela está Simples do ponto de vista que por ora temos apenas as classes de noticias e usuario, porém organizada e fácil de compreensão.
A ideia de usar o banco NoSQL foi por carater de estudos.

Ao realizar alterações na branch Master, já é disparado o pipeline e replica para a em produção.

Segue informaçõe sobre o projeto.

URL Azure Repos
https://johnbressianini.visualstudio.com/bressianini-factory/_git/challengeII

************************Nos files do projeto  tem um video explicando melhor o tech challenge

Url Publicado:
challengenews.azurewebsites.net

curl -X 'POST' \
  'challengenews.azurewebsites.net/api/v1/noticia' \
  -H 'accept: */*' \
  -H 'Content-Type: multipart/form-data' \
  -F 'Title=Title new' \
  -F 'Description=description new' \
  -F 'Author=author new'
  
  O arquivo Yml utilizado para a publicação e testes
  
  
  pool:
  name: Azure Pipelines
  demands:
  - msbuild
  - visualstudio
  - vstest

#Your build pipeline references an undefined variable named ‘Parameters.solution’. Create or edit the build pipeline for this YAML file, define the variable on the Variables tab. See https://go.microsoft.com/fwlink/?linkid=865972
#Your build pipeline references an undefined variable named ‘Parameters.solution’. Create or edit the build pipeline for this YAML file, define the variable on the Variables tab. See https://go.microsoft.com/fwlink/?linkid=865972
#Your build pipeline references the ‘BuildPlatform’ variable, which you’ve selected to be settable at queue time. Create or edit the build pipeline for this YAML file, define the variable on the Variables tab, and then select the option to make it settable at queue time. See https://go.microsoft.com/fwlink/?linkid=865971
#Your build pipeline references the ‘BuildConfiguration’ variable, which you’ve selected to be settable at queue time. Create or edit the build pipeline for this YAML file, define the variable on the Variables tab, and then select the option to make it settable at queue time. See https://go.microsoft.com/fwlink/?linkid=865971
#Your build pipeline references the ‘BuildConfiguration’ variable, which you’ve selected to be settable at queue time. Create or edit the build pipeline for this YAML file, define the variable on the Variables tab, and then select the option to make it settable at queue time. See https://go.microsoft.com/fwlink/?linkid=865971
#Your build pipeline references the ‘BuildPlatform’ variable, which you’ve selected to be settable at queue time. Create or edit the build pipeline for this YAML file, define the variable on the Variables tab, and then select the option to make it settable at queue time. See https://go.microsoft.com/fwlink/?linkid=865971
#Your build pipeline references the ‘BuildConfiguration’ variable, which you’ve selected to be settable at queue time. Create or edit the build pipeline for this YAML file, define the variable on the Variables tab, and then select the option to make it settable at queue time. See https://go.microsoft.com/fwlink/?linkid=865971
#Your build pipeline references an undefined variable named ‘Parameters.connectedServiceName’. Create or edit the build pipeline for this YAML file, define the variable on the Variables tab. See https://go.microsoft.com/fwlink/?linkid=865972
#Your build pipeline references an undefined variable named ‘Parameters.WebAppName’. Create or edit the build pipeline for this YAML file, define the variable on the Variables tab. See https://go.microsoft.com/fwlink/?linkid=865972

steps:
- task: NuGetToolInstaller@0
  displayName: 'Use NuGet 6.5.0'
  inputs:
    versionSpec: 6.5.0

- task: NuGetCommand@2
  displayName: 'NuGet restore'
  inputs:
    restoreSolution: '$(Parameters.solution)'

- task: NuGetToolInstaller@1
  displayName: 'Use NuGet '
  enabled: false

- task: DotNetCoreCLI@2
  displayName: restore
  inputs:
    command: restore
    feedsToUse: config
    nugetConfigPath: '.\NuGet.config'
  enabled: false

- task: VSBuild@1
  displayName: 'Build solution **\*.sln'
  inputs:
    solution: '$(Parameters.solution)'
    msbuildArgs: '/p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:publishUrl="$(Agent.TempDirectory)\WebAppContent\\"'
    platform: '$(BuildPlatform)'
    configuration: '$(BuildConfiguration)'

- task: ArchiveFiles@2
  displayName: 'Archive Files'
  inputs:
    rootFolderOrFile: '$(Agent.TempDirectory)\WebAppContent'
    includeRootFolder: false

- task: VSTest@2
  displayName: 'VsTest - testAssemblies'
  inputs:
    testAssemblyVer2: |
     **\$(BuildConfiguration)\*test*.dll
     !**\obj\**
    platform: '$(BuildPlatform)'
    configuration: '$(BuildConfiguration)'

- task: AzureWebApp@1
  displayName: 'Azure Web App Deploy: challengenews'
  inputs:
    azureSubscription: '$(Parameters.connectedServiceName)'
    appType: webApp
    appName: '$(Parameters.WebAppName)'
    package: '$(build.artifactstagingdirectory)/**/*.zip'

- task: PublishSymbols@2
  displayName: 'Publish symbols path'
  inputs:
    SearchPattern: '**\bin\**\*.pdb'
    PublishSymbols: false
  continueOnError: true

- task: PublishBuildArtifacts@1
  displayName: 'Publish Artifact: drop'
  inputs:
    PathtoPublish: '$(build.artifactstagingdirectory)'
  condition: succeededOrFailed()
