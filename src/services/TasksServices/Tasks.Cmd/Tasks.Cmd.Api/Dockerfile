FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/services/TasksServices/Tasks.Cmd/Tasks.Cmd.Api/Tasks.Cmd.Api.csproj", "src/services/TasksServices/Tasks.Cmd/Tasks.Cmd.Api/"]
COPY ["src/services/TasksServices/Tasks.Cmd/Tasks.Cmd.Domain/Tasks.Cmd.Domain.csproj", "src/services/TasksServices/Tasks.Cmd/Tasks.Cmd.Domain/"]
COPY ["src/services/TasksServices/Tasks.Common/Tasks.Common.csproj", "src/services/TasksServices/Tasks.Common/"]
COPY ["src/services/TasksServices/Tasks.Cmd/Tasks.Cmd.Application/Tasks.Cmd.Application.csproj", "src/services/TasksServices/Tasks.Cmd/Tasks.Cmd.Application/"]
COPY ["src/services/Tasks.Cmd.Infrastructure/Tasks.Cmd.Infrastructure.csproj", "src/services/Tasks.Cmd.Infrastructure/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj","src/BuildingBlocks/EventBus.Messages/"]
RUN dotnet restore "src/services/TasksServices/Tasks.Cmd/Tasks.Cmd.Api/Tasks.Cmd.Api.csproj"
COPY . .
WORKDIR "/src/src/services/TasksServices/Tasks.Cmd/Tasks.Cmd.Api"
RUN dotnet build "Tasks.Cmd.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tasks.Cmd.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasks.Cmd.Api.dll"]
