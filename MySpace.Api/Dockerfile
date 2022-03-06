FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MySpace.Api.Presentation/MySpace.Api.Presentation.csproj", "MySpace.Api.Presentation/"]
RUN dotnet restore "MySpace.Api.Presentation/MySpace.Api.Presentation.csproj"
COPY . .
WORKDIR "/src/MySpace.Api.Presentation"
RUN dotnet build "MySpace.Api.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MySpace.Api.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MySpace.Api.Presentation.dll"]
