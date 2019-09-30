FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["BlazorGrpc.Web.Server/BlazorGrpc.Web.Server.csproj", "BlazorGrpc.Web.Server/"]
COPY ["BlazorGrpc.Web.Client/BlazorGrpc.Web.Client.csproj", "BlazorGrpc.Web.Client/"]
RUN dotnet restore "BlazorGrpc.Web.Server/BlazorGrpc.Web.Server.csproj"
COPY "BlazorGrpc.Web.Server" "BlazorGrpc.Web.Server/"
COPY "BlazorGrpc.Web.Client" "BlazorGrpc.Web.Client/"
COPY "BlazorGrpc.Protos" "BlazorGrpc.Protos/"
WORKDIR /src/BlazorGrpc.Web.Server
RUN dotnet build "BlazorGrpc.Web.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorGrpc.Web.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorGrpc.Web.Server.dll"]
