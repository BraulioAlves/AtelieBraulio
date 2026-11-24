FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AtelieBraulio/AtelieBraulio.csproj", "AtelieBraulio/"]
RUN dotnet restore "AtelieBraulio/AtelieBraulio.csproj"
COPY . .
WORKDIR "/src/AtelieBraulio"
RUN dotnet build "AtelieBraulio.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AtelieBraulio.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AtelieBraulio.dll"]