#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Master.Api/Master.Api.csproj", "src/Master.Api/"]
COPY ["src/Master.Service/Master.Service.csproj", "src/Master.Service/"]
COPY ["src/HouseWarehouseStore.Common/HouseWarehouseStore.Common.csproj", "src/HouseWarehouseStore.Common/"]
COPY ["src/HouseWarehouseStore.Data/HouseWarehouseStore.Data.csproj", "src/HouseWarehouseStore.Data/"]
COPY ["src/HouseWarehouseStore.Models/HouseWarehouseStore.Models.csproj", "src/HouseWarehouseStore.Models/"]
RUN dotnet restore "src/Master.Api/Master.Api.csproj"
COPY . .
WORKDIR "/src/src/Master.Api"
RUN dotnet build "Master.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Master.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Master.Api.dll"]