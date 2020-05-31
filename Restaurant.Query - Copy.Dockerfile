#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5051
ENV ASPNETCORE_ENVIRONMENT Development

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["RestaurantService/Query/OMF.RestaurantService.Query.Api/OMF.RestaurantService.Query.Api.csproj", "RestaurantService/Query/OMF.RestaurantService.Query.Api/"]
COPY ["RestaurantService/Query/OMF.RestaurantService.Query.Service/OMF.RestaurantService.Query.Service.csproj", "RestaurantService/Query/OMF.RestaurantService.Query.Service/"]
COPY ["OMF.Common/OMF.Common.csproj", "OMF.Common/"]
COPY ["RestaurantService/Query/OMF.RestaurantService.Query.Repository/OMF.RestaurantService.Query.Repository.csproj", "RestaurantService/Query/OMF.RestaurantService.Query.Repository/"]
COPY ["Infrastructure/BaseService/BaseService.csproj", "Infrastructure/BaseService/"]
RUN dotnet restore "RestaurantService/Query/OMF.RestaurantService.Query.Api/OMF.RestaurantService.Query.Api.csproj"
COPY . .
WORKDIR "/src/RestaurantService/Query/OMF.RestaurantService.Query.Api"
RUN dotnet build "OMF.RestaurantService.Query.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OMF.RestaurantService.Query.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OMF.RestaurantService.Query.Api.dll"]