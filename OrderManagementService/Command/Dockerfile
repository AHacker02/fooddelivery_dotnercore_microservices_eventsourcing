#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["OrderManagementService/Command/OMF.OrderManagementService.Command.csproj", "OrderManagementService/Command/"]
COPY ["Infrastructure/ServiceBus/ServiceBus.csproj", "Infrastructure/ServiceBus/"]
COPY ["Infrastructure/DataAccess/DataAccess.csproj", "Infrastructure/DataAccess/"]
COPY ["OMF.Common/OMF.Common.csproj", "OMF.Common/"]
COPY ["Infrastructure/BaseService/BaseService.csproj", "Infrastructure/BaseService/"]
RUN dotnet restore "OrderManagementService/Command/OMF.OrderManagementService.Command.csproj"
COPY . .
WORKDIR "/src/OrderManagementService/Command"
RUN dotnet build "OMF.OrderManagementService.Command.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OMF.OrderManagementService.Command.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OMF.OrderManagementService.Command.dll"]