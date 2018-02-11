FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app
EXPOSE 9001

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY src/Dockka.Actors.ActorOne/Dockka.Actors.ActorOne.csproj src/Dockka.Actors.ActorOne/
COPY src/Dockka.Logging/Dockka.Logging.csproj src/Dockka.Logging/
COPY src/Dockka.Config/Dockka.Config.csproj src/Dockka.Config/
COPY src/Dockka.Data/Dockka.Data.csproj src/Dockka.Data/
RUN dotnet restore
COPY . .
WORKDIR /src/src/Dockka.Actors.ActorOne
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Dockka.Actors.ActorOne.dll"]
