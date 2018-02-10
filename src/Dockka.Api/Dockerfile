FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY src/Dockka.Api/Dockka.Api.csproj Dockka.Api/
COPY src/Dockka.Data/Dockka.Data.csproj Dockka.Data/
RUN dotnet restore
COPY ./src .
WORKDIR /src/Dockka.Api
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Dockka.Api.dll"]