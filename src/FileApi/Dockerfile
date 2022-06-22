#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/FileApi/FileApi.csproj", "src/FileApi/"]
COPY ["src/Files.Service/Files.Service.csproj", "src/Files.Service/"]
COPY ["src/HouseWarehouseStore.Common/HouseWarehouseStore.Common.csproj", "src/HouseWarehouseStore.Common/"]
COPY ["src/HouseWarehouseStore.Data/HouseWarehouseStore.Data.csproj", "src/HouseWarehouseStore.Data/"]
COPY ["src/HouseWarehouseStore.Models/HouseWarehouseStore.Models.csproj", "src/HouseWarehouseStore.Models/"]
RUN dotnet restore "src/FileApi/FileApi.csproj"
COPY . .
WORKDIR "/src/src/FileApi"
RUN dotnet build "FileApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FileApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileApi.dll"]