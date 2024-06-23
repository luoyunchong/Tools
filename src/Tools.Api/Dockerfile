#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Tools.Api/Tools.Api.csproj", "src/Tools.Api/"]
COPY ["src/SM4/SM4.csproj", "src/SM4/"]
COPY ["src/Tools/Tools.csproj", "src/Tools/"]
RUN dotnet restore "./src/Tools.Api/Tools.Api.csproj"
COPY . .
WORKDIR "/src/src/Tools.Api"
RUN dotnet build "./Tools.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Tools.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tools.Api.dll"]