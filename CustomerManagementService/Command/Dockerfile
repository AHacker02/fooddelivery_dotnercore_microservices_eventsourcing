FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CustomerManagementService/Command/OMF.CustomerManagementService.Command.csproj", "CustomerManagementService/Command/"]
COPY ["Infrastructure/ServiceBus/ServiceBus.csproj", "Infrastructure/ServiceBus/"]
COPY ["Infrastructure/DataAccess/DataAccess.csproj", "Infrastructure/DataAccess/"]
COPY ["OMF.Common/OMF.Common.csproj", "OMF.Common/"]
COPY ["Infrastructure/BaseService/BaseService.csproj", "Infrastructure/BaseService/"]
RUN dotnet restore "CustomerManagementService/Command/OMF.CustomerManagementService.Command.csproj"
COPY . .
WORKDIR "/src/CustomerManagementService/Command"
RUN dotnet build "OMF.CustomerManagementService.Command.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OMF.CustomerManagementService.Command.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OMF.CustomerManagementService.Command.dll"]