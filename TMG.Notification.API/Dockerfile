#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["TMG.Notification.API/TMG.Notification.API.csproj", "TMG.Notification.API/"]
COPY ["TMG.Notification.QueryHandler/TMG.Notification.QueryHandler.csproj", "TMG.Notification.QueryHandler/"]
COPY ["TMG.Notification.Data/TMG.Notification.Data.csproj", "TMG.Notification.Data/"]
COPY ["TMG.Notification.Query/TMG.Notification.Query.csproj", "TMG.Notification.Query/"]
COPY ["TMG.Notification.CommandHandler/TMG.Notification.CommandHandler.csproj", "TMG.Notification.CommandHandler/"]
COPY ["TMG.Notification.Command/TMG.Notification.Command.csproj", "TMG.Notification.Command/"]
RUN dotnet restore "TMG.Notification.API/TMG.Notification.API.csproj"
COPY . .
WORKDIR "/src/TMG.Notification.API"
RUN dotnet build "TMG.Notification.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TMG.Notification.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TMG.Notification.API.dll"]