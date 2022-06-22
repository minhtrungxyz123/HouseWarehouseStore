#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Master.Webapp/Master.Webapp.csproj", "src/Master.Webapp/"]
COPY ["src/HouseWarehouseStore.Data/HouseWarehouseStore.Data.csproj", "src/HouseWarehouseStore.Data/"]
COPY ["src/HouseWare.Base/HouseWare.Base.csproj", "src/HouseWare.Base/"]
COPY ["src/HouseWarehouseStore.Models/HouseWarehouseStore.Models.csproj", "src/HouseWarehouseStore.Models/"]
COPY ["src/HouseWarehouseStore.Common/HouseWarehouseStore.Common.csproj", "src/HouseWarehouseStore.Common/"]
RUN dotnet restore "src/Master.Webapp/Master.Webapp.csproj"
COPY . .
WORKDIR "/src/src/Master.Webapp"
RUN dotnet build "Master.Webapp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Master.Webapp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Master.Webapp.dll"]