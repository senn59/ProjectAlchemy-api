FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProjectAlchemy.Web/ProjectAlchemy.Web.csproj", "ProjectAlchemy.Web/"]
RUN dotnet restore "ProjectAlchemy.Web/ProjectAlchemy.Web.csproj"
COPY . .
WORKDIR "/src/ProjectAlchemy.Web"
RUN dotnet build "ProjectAlchemy.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ProjectAlchemy.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectAlchemy.Web.dll"]
