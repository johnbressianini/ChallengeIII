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