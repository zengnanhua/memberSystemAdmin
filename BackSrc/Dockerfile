FROM microsoft/dotnet:2.1-sdk AS build

WORKDIR /app

COPY . .

RUN cd ./AdminSystem.Api

RUN dotnet restore

RUN dotnet build

RUN dotnet publish -c Release -o out




FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base

WORKDIR /app

COPY --from=build /app/AdminSystem.Api/out ./

ENTRYPOINT ["dotnet", "AdminSystem.Api.dll"]
