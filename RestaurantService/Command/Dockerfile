FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["RestaurantService/Command/OMF.RestaurantService.Command.csproj", "RestaurantService/Command/"]
COPY ["Infrastructure/ServiceBus/ServiceBus.csproj", "Infrastructure/ServiceBus/"]
COPY ["Infrastructure/DataAccess/DataAccess.csproj", "Infrastructure/DataAccess/"]
COPY ["OMF.Common/OMF.Common.csproj", "OMF.Common/"]
COPY ["Infrastructure/BaseService/BaseService.csproj", "Infrastructure/BaseService/"]
RUN dotnet restore "RestaurantService/Command/OMF.RestaurantService.Command.csproj"
COPY . .
WORKDIR "/src/RestaurantService/Command"
RUN dotnet build "OMF.RestaurantService.Command.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OMF.RestaurantService.Command.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OMF.RestaurantService.Command.dll"]