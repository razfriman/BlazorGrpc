# Blazor WebAssembly + ASP.NET Core + gRPC

[![GitHub Actions status](https://github.com/razfriman/BlazorGrpc/workflows/C%23%20.NET%20Core%20CI/badge.svg)](https://github.com/razfriman/BlazorGrpc/actions)

This sample projects demonstrates how you can use the power of Blazor, ASP.NET Core, and gRPC to create a web application which can communicate with a backend that uses gRPC with maximum code reusability.

By simply defining our API contracts once via `.proto` files, the request/response objects are created for both the backend and frontend (Blazor). Additionally, code-generation creates the gRPC clients and gRPC servers we need to implement the services.

This means your client and server code can easily stay in sync.

## Projects

## BlazorGrpc.Protos

Contains `.proto` definitions used in the project

## BlazorGrpc.WeatherService

Simple gRPC service that returns dummy weather data

## BlazorGrpc.Api

Acts as a gRPC gateway (supporting unary gRPC calls).

Accepts incoming JSON POST requests by taking incoming requests and routing them to the correct gRPC service, and transforming the result back into JSON before returning the response.

Additionally, Swagger is generated from the API Gateway to create a human-readable view of the available request and response methods.

## BlazorGrpc.Web.Client

Blazor WebAssembly (Client-side Blazor) project.

Reuses the `.proto`-generated objects to perform gRPC requests through the `BlazorGrpc.Api` API gateway.

While we cannot make gRPC calls directly from the browser/WASM, this lets use reuse all of the existing request/reseponse models directly in the browser.


## BlazorGrpc.Web.Server

ASP.NET Core project hosting the Blazor WebAssembly project.

Exposes a `/Configuration` endpoint to easily pass data to the client project. In this case, it is passing the `apiUrl` for the client so we can dynamically change path to the api server.

---

## Getting Started (Docker)

A `docker-compose` configuration is provided so you can easily get started by running:

```
docker-compose up
```

### Services / Ports
The projects are exposed via the following ports:

Web Client: `http: 5101 / https: 5201`

API Gateway: `http: 5102 / https: 5202`

Weather gRPC Service: `http: 5103 / https: 5203` (Requires HTTPS/HTTP2)

Helpful links

`https://localhost:5201` - the Blazor client side application

`https://localhost:5202/swagger` - the gRPC API Gateway used to allow the Blazor app to communicate with the backend. Consider this like a BFF (backend-for-frontend).

`https://localhost:5202/health` - Health check page for the gRPC API Gateway

`https://localhost:5203/health` - Health check page for the gRPC weathe service

---

## Logging

ASP.NET Core 3 provides you with correlation IDs out of the box, so you can process these logs and get tracing between multiple services. In this example the API Gateway call that initiates the request to the `weather` gRPC service is tracked via the same ID. This is really helpful when you have multiple services and can see the path of an entire request.

```
api_1      | info: BlazorGrpc.Api.Controllers.WeatherServiceController[0]                    api_1      |       [gRPC API Gateway    ] [GetWeather] [12c15313-4723c328bfbb8586]           

weather_1  | info: BlazorGrpc.WeatherService.Services.WeatherService[0]                      weather_1  |       [gRPC Weather Service] [GetWeather] [12c15313-4723c328bfbb8586]     

api_1      | info: BlazorGrpc.Api.Controllers.WeatherServiceController[0]                    api_1      |       [gRPC API Gateway    ] [GetWeather] [12c15315-4723c328bfbb8586]           

weather_1  | info: BlazorGrpc.WeatherService.Services.WeatherService[0]                      weather_1  |       [gRPC Weather Service] [GetWeather] [12c15315-4723c328bfbb8586]     
```

## Screenshot

![screenshot](https://user-images.githubusercontent.com/1769935/65869246-6a1b2a00-e3bd-11e9-9f6c-600cb57dd20d.PNG)


---

Any questions? You can reach me via r@razfriman.com or https://razfriman.com