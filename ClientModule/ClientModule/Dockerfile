#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ClientModule/ClientModule/ClientModule.csproj", "ClientModule/ClientModule/"]
COPY ["ClientModule/ClientModule_ApiClasses/ClientModule_ApiClasses.csproj", "ClientModule/ClientModule_ApiClasses/"]
RUN dotnet restore "ClientModule/ClientModule/ClientModule.csproj"
COPY . .
WORKDIR "/src/ClientModule/ClientModule"
RUN dotnet build "ClientModule.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClientModule.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientModule.dll"]