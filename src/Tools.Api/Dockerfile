#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update -y && apt-get install -y libfontconfig1

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Tools.Api/Tools.Api.csproj", "src/Tools.Api/"]
COPY ["src/SM4/SM4.csproj", "src/SM4/"]
COPY ["src/Tools/Tools.csproj", "src/Tools/"]
RUN dotnet restore "src/Tools.Api/Tools.Api.csproj"
COPY . .
WORKDIR "/src/src/Tools.Api"
RUN dotnet build "Tools.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tools.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tools.Api.dll"]