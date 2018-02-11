FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 9001

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY src/Dockka.ActorOne/Dockka.ActorOne.csproj Dockka.ActorOne/
COPY src/Dockka.Data/Dockka.Data.csproj Dockka.Data/
COPY src/Dockka.Config/Dockka.Config.csproj Dockka.Config/
RUN dotnet restore
COPY ./src .
WORKDIR /src/Dockka.ActorOne
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Dockka.ActorOne.dll"]