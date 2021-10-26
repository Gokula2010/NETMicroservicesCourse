FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app
EXPOSE 80

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "CommandService.dll" ]




# FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
# WORKDIR /app
# EXPOSE 80

# FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# WORKDIR /src
# COPY ["PlatformService.csproj", "./"]
# RUN dotnet restore "PlatformService.csproj"
# COPY . .
# RUN dotnet publish "PlatformService.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=build /app/publish .
# ENTRYPOINT ["dotnet", "PlatformService.dll"]
