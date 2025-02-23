FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ENV ASPNETCORE_HTTP_PORTS=4114
EXPOSE 8081
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BoardGamesShop.BusinessLogic/BoardGamesShop.BusinessLogic.csproj", "BoardGamesShop.BusinessLogic/"]
COPY ["BoardGamesShop.DataAccess/BoardGamesShop.DataAccess.csproj", "BoardGamesShop.DataAccess/"]
COPY ["BoardGamesShop.Service/BoardGamesShop.Service.csproj", "BoardGamesShop.Service/"]
RUN dotnet restore "BoardGamesShop.Service/BoardGamesShop.Service.csproj"
COPY . .


FROM build AS publish
RUN dotnet publish -c Release -o /app/publish
WORKDIR /src/BoardGamesShop.Service
RUN dotnet build "./BoardGamesShop.Service.csproj" -c Release -o /app/build

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoardGamesShop.Service.dll"]