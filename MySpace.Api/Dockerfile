FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MySpace.Api/MySpace.Api.csproj", "MySpace.Api/"]
COPY ["MySpace.Api.Application/MySpace.Api.Application.csproj", "MySpace.Api.Application/"]
COPY ["MySpace.Api.Domain/MySpace.Api.Domain.csproj", "MySpace.Api.Domain/"]
COPY ["MySpace.Api.Persistence/MySpace.Api.Persistence.csproj", "MySpace.Api.Persistence/"]
COPY ["MySpace.Api.Presentation/MySpace.Api.Presentation.csproj", "MySpace.Api.Presentation/"]
RUN dotnet restore "MySpace.Api/MySpace.Api.csproj"
COPY . .
WORKDIR "/src/MySpace.Api"
RUN dotnet build "MySpace.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MySpace.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MySpace.Api.dll"]
