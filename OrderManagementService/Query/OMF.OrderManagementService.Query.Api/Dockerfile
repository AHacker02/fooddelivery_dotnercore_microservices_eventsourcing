FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["OrderManagementService/Query/OMF.OrderManagementService.Query.csproj", "OrderManagementService/Query/"]
COPY ["Infrastructure/ServiceBus/ServiceBus.csproj", "Infrastructure/ServiceBus/"]
COPY ["Infrastructure/DataAccess/DataAccess.csproj", "Infrastructure/DataAccess/"]
COPY ["OMF.Common/OMF.Common.csproj", "OMF.Common/"]
COPY ["Infrastructure/BaseService/BaseService.csproj", "Infrastructure/BaseService/"]
RUN dotnet restore "OrderManagementService/Query/OMF.OrderManagementService.Query.csproj"
COPY . .
WORKDIR "/src/OrderManagementService/Query"
RUN dotnet build "OMF.OrderManagementService.Query.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OMF.OrderManagementService.Query.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OMF.OrderManagementService.Query.dll"]