FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/services/TasksServices/Tasks.Query/Tasks.Query.Api/Tasks.Query.Api.csproj", "src/services/TasksServices/Tasks.Query/Tasks.Query.Api/"]
COPY ["src/services/TasksServices/Tasks.Common/Tasks.Common.csproj", "src/services/TasksServices/Tasks.Common/"]
COPY ["src/services/TasksServices/Tasks.Query/Tasks.Query.Application/Tasks.Query.Application.csproj", "src/services/TasksServices/Tasks.Query/Tasks.Query.Application/"]
COPY ["src/services/TasksServices/Tasks.Query/Tasks.Query.Domain/Tasks.Query.Domain.csproj", "src/services/TasksServices/Tasks.Query/Tasks.Query.Domain/"]
COPY ["src/services/TasksServices/Tasks.Query/Tasks.Query.Infrastructure/Tasks.Query.Infrastructure.csproj", "src/services/TasksServices/Tasks.Query/Tasks.Query.Infrastructure/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj","src/BuildingBlocks/EventBus.Messages/"]
RUN dotnet restore "src/services/TasksServices/Tasks.Query/Tasks.Query.Api/Tasks.Query.Api.csproj"
COPY . .
WORKDIR "/src/src/services/TasksServices/Tasks.Query/Tasks.Query.Api"
RUN dotnet build "Tasks.Query.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tasks.Query.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasks.Query.Api.dll"]
