FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 9002

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY src/Dockka.ActorTwo/Dockka.ActorTwo.csproj Dockka.ActorTwo/
COPY src/Dockka.Data/Dockka.Data.csproj Dockka.Data/
COPY src/Dockka.Config/Dockka.Config.csproj Dockka.Config/
RUN dotnet restore
COPY ./src .
WORKDIR /src/Dockka.ActorTwo
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Dockka.ActorTwo.dll"]