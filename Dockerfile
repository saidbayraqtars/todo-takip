# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# csproj'leri kopyala, restore (layer cache için ayrı)
COPY src/GorevTakip.Entities/GorevTakip.Entities.csproj GorevTakip.Entities/
COPY src/GorevTakip.DataAccess/GorevTakip.DataAccess.csproj GorevTakip.DataAccess/
COPY src/GorevTakip.Business/GorevTakip.Business.csproj GorevTakip.Business/
COPY src/GorevTakip.Web/GorevTakip.Web.csproj GorevTakip.Web/
RUN dotnet restore GorevTakip.Web/GorevTakip.Web.csproj

# Kalan kaynagi kopyala, publish
COPY src/ .
RUN dotnet publish GorevTakip.Web/GorevTakip.Web.csproj -c Release -o /app

# Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app .

# Host'un verdigi porta bagla (Railway/Render PORT enjekte eder)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "GorevTakip.Web.dll"]
