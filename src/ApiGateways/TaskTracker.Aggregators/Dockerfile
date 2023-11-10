FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ApiGateways/TaskTracker.Aggregators/TaskTracker.Aggregators.csproj", "src/ApiGateways/TaskTracker.Aggregators/"]
RUN dotnet restore "src/ApiGateways/TaskTracker.Aggregators/TaskTracker.Aggregators.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/TaskTracker.Aggregators"
RUN dotnet build "TaskTracker.Aggregators.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskTracker.Aggregators.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskTracker.Aggregators.dll"]
