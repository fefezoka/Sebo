# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SEBO.API/SEBO.API.csproj", "SEBO.API/"]
COPY ["SEBO.Ioc/SEBO.Ioc.csproj", "SEBO.Ioc/"]
COPY ["SEBO.Data.Repository/SEBO.Data.Repository.csproj", "SEBO.Data.Repository/"]
COPY ["SEBO.Data.Context/SEBO.Data.Context.csproj", "SEBO.Data.Context/"]
COPY ["SEBO.Domain.Entities/SEBO.Domain.Entities.csproj", "SEBO.Domain.Entities/"]
COPY ["SEBO.Domain.Interface.Repository/SEBO.Domain.Interface.Repository.csproj", "SEBO.Domain.Interface.Repository/"]
COPY ["SEBO.Domain.Utility/SEBO.Domain.Utility.csproj", "SEBO.Domain.Utility/"]
COPY ["SEBO.Domain.Interface.Services/SEBO.Domain.Interface.Services.csproj", "SEBO.Domain.Interface.Services/"]
COPY ["SEBO.Domain.Dto/SEBO.Domain.Dto.csproj", "SEBO.Domain.Dto/"]
COPY ["SEBO.Middleware/SEBO.Middleware.csproj", "SEBO.Middleware/"]
COPY ["SEBO.Services/SEBO.Services.csproj", "SEBO.Services/"]
RUN dotnet restore "./SEBO.API/SEBO.API.csproj"
COPY . .
WORKDIR "/src/SEBO.API"
RUN dotnet build "./SEBO.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SEBO.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SEBO.API.dll"]