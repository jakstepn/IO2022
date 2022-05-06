#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ShopModule/ShopModule/ShopModule.csproj", "ShopModule/ShopModule/"]
COPY ["ShopModule/ShopModule_ApiClasses/ShopModule_ApiClasses.csproj", "ShopModule/ShopModule_ApiClasses/"]
RUN dotnet restore "ShopModule/ShopModule/ShopModule.csproj"
COPY . .
WORKDIR "/src/ShopModule/ShopModule"
RUN dotnet build "ShopModule.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopModule.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopModule.dll"]