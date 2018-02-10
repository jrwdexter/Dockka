# Requirements

In order to run this application, you need:

* Docker

In order to build and develop, add:

* .NET Core
* Yarn
* Webpack (install `webpack` and `webpack-dev-server` globally)

## Technologies

Technologies in play include:

* Docker
* Akka
* ASP.NET Core
* SignalR
* React and Redux
* ELK Stack
* Entity Framework
* Microsoft SQL Server

## Running the Application

In order to build the application, run:

`docker-compose up`

That should be it.

## Building the application

In order to build the application:

### Front-end

```
cd Dockka.Web
yarn
webpack
```

Alternatively, `webpack-dev-server` can be used for development.