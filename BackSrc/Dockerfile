FROM microsoft/dotnet:2.0-sdk AS build

WORKDIR /app

COPY . .

RUN cd ./AdminSystem.Api

RUN dotnet restore

RUN dotnet build

RUN dotnet publish -c Release -o out




FROM microsoft/dotnet:2.0-aspnetcore-runtime AS base

WORKDIR /app

COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "AdminSystem.Api.dll"]
