#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DeliveryModule/DeliveryModule/DeliveryModule.csproj", "DeliveryModule/DeliveryModule/"]
COPY ["DeliveryModule/DeliveryModule_ApiClasses/DeliveryModule_ApiClasses.csproj", "DeliveryModule/DeliveryModule_ApiClasses/"]
RUN dotnet restore "DeliveryModule/DeliveryModule/DeliveryModule.csproj"
COPY . .
WORKDIR "/src/DeliveryModule/DeliveryModule"
RUN dotnet build "DeliveryModule.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliveryModule.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliveryModule.dll"]