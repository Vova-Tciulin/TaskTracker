FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/services/GroupServices/Groups.Cmd/Groups.Cmd.Api/Groups.Cmd.Api.csproj", "src/services/GroupServices/Groups.Cmd/Groups.Cmd.Api/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/services/GroupServices/Groups.Cmd/Groups.Cmd.Infrastructure/Groups.Cmd.Infrastructure.csproj", "src/services/GroupServices/Groups.Cmd/Groups.Cmd.Infrastructure/"]
COPY ["src/services/GroupServices/Groups.Cmd/Groups.Cmd.Domain/Groups.Cmd.Domain.csproj", "src/services/GroupServices/Groups.Cmd/Groups.Cmd.Domain/"]
COPY ["src/services/GroupServices/Group.Common/Group.Common.csproj", "src/services/GroupServices/Group.Common/"]
COPY ["src/services/GroupServices/Groups.Cmd/Groups.Cmd.Application/Groups.Cmd.Application.csproj", "src/services/GroupServices/Groups.Cmd/Groups.Cmd.Application/"]
RUN dotnet restore "src/services/GroupServices/Groups.Cmd/Groups.Cmd.Api/Groups.Cmd.Api.csproj"
COPY . .
WORKDIR "/src/src/services/GroupServices/Groups.Cmd/Groups.Cmd.Api"
RUN dotnet build "Groups.Cmd.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Groups.Cmd.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Groups.Cmd.Api.dll"]
