FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ADWebApplication/ADWebApplication.csproj", "ADWebApplication/"]
RUN dotnet restore "ADWebApplication/ADWebApplication.csproj"

COPY . .
WORKDIR "/src/ADWebApplication"
RUN dotnet publish "ADWebApplication.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY start.sh .
RUN chmod +x start.sh

ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["./start.sh"]
