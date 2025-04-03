# Etapa base con el entorno de ejecución de .NET
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa de compilación con el SDK de .NET
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar archivos de proyecto y restaurar dependencias
COPY ["PeliculasAPI.csproj", "./"]
RUN dotnet restore "PeliculasAPI.csproj"

# Copiar el resto del código fuente y compilar
COPY . .
RUN dotnet build "PeliculasAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "PeliculasAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final, imagen mínima para producción
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Establecer la entrada del contenedor
ENTRYPOINT ["dotnet", "PeliculasAPI.dll"]