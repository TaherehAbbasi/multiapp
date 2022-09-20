
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /opt/app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /opt/src
COPY ["MultiApp.csproj", "./"]
RUN dotnet restore "./MultiApp.csproj"
COPY . .
RUN dotnet build "MultiApp.csproj" -c Release -o /opt/app

FROM build AS publish
VOLUME /opt/app
RUN dotnet publish "MultiApp.csproj" -c Release -o /opt/app

FROM base AS final
WORKDIR /opt/src
COPY --from=publish /opt/app .
ENTRYPOINT ["dotnet", "MultiApp.dll"]



