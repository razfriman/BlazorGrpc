FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["BlazorGrpc.Api/BlazorGrpc.Api.csproj", "BlazorGrpc.Api/"]
RUN dotnet restore "BlazorGrpc.Api/BlazorGrpc.Api.csproj"
COPY "BlazorGrpc.Api" "BlazorGrpc.Api/"
COPY "BlazorGrpc.Protos" "BlazorGrpc.Protos/"
WORKDIR /src/BlazorGrpc.Api
RUN dotnet build "BlazorGrpc.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorGrpc.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorGrpc.Api.dll"]
