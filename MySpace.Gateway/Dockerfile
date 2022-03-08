FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MySpace.Gateway.csproj", "MySpace.Gateway/"]
RUN dotnet restore "MySpace.Gateway/MySpace.Gateway.csproj"
COPY [".", "MySpace.Gateway/"]
WORKDIR "/src/MySpace.Gateway"
RUN dotnet build "MySpace.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MySpace.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MySpace.Gateway.dll"]
