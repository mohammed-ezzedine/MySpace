FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MySpace.Authentication.Domain/MySpace.Authentication.Domain.csproj", "MySpace.Authentication.Domain/"]
COPY ["MySpace.Authentication.Application/MySpace.Authentication.Application.csproj", "MySpace.Authentication.Application/"]
COPY ["MySpace.Authentication.Presentation/MySpace.Authentication.Presentation.csproj", "MySpace.Authentication.Presentation/"]
RUN dotnet restore "MySpace.Authentication.Presentation/MySpace.Authentication.Presentation.csproj"
COPY . .
WORKDIR "/src/MySpace.Authentication.Presentation"
RUN dotnet build "MySpace.Authentication.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MySpace.Authentication.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "MySpace.Authentication.Presentation.dll"]
