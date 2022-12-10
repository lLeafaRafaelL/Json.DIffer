#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JsonDiffer.Api/JsonDiffer.Api.csproj", "JsonDiffer.Api/"]
RUN dotnet restore "JsonDiffer.Api/JsonDiffer.Api.csproj"
COPY . .
WORKDIR "/src/JsonDiffer.Api"
RUN dotnet build "JsonDiffer.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JsonDiffer.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JsonDiffer.Api.dll"]