FROM microsoft/dotnet:2.1-sdk AS build

WORKDIR /app

COPY . .

RUN dotnet restore

RUN dotnet build

RUN dotnet publish -c Release -o out




FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base

WORKDIR /app

COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "AdminSystem.Api.dll"]
