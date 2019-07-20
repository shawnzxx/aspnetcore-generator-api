# Build Stage
FROM microsoft/aspnetcore-build:2 AS build-env
WORKDIR /integration

COPY integration.csproj .
RUN dotnet restore

COPY . .
ENTRYPOINT ["dotnet", "test"]