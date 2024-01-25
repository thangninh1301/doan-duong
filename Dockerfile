FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BackEnd/BackEnd.csproj", "./"]

RUN dotnet restore
COPY . .
WORKDIR "/src/BackEnd"
RUN dotnet build "BackEnd.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyApp.dll"]
#docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=PasSWord321@" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest ubuntu@13.212.32.188
#ssh -i id_rsa -R 1433:127.0.0.1:1433 bolt@10.10.10.159
