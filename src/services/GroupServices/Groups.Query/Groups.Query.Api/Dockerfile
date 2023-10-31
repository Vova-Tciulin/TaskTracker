FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/services/GroupServices/Groups.Query/Groups.Query.Api/Groups.Query.Api.csproj", "src/services/GroupServices/Groups.Query/Groups.Query.Api/"]
COPY ["src/services/GroupServices/Groups.Query/Groups.Query.Domain/Groups.Query.Domain.csproj", "src/services/GroupServices/Groups.Query/Groups.Query.Domain/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/services/GroupServices/Groups.Query/Groups.Query.Infrastructure/Groups.Query.Infrastructure.csproj", "src/services/GroupServices/Groups.Query/Groups.Query.Infrastructure/"]
COPY ["src/services/GroupServices/Group.Common/Group.Common.csproj", "src/services/GroupServices/Group.Common/"]
COPY ["src/services/GroupServices/Groups.Query/Groups.Query.Application/Groups.Query.Application.csproj", "src/services/GroupServices/Groups.Query/Groups.Query.Application/"]
RUN dotnet restore "src/services/GroupServices/Groups.Query/Groups.Query.Api/Groups.Query.Api.csproj"
COPY . .
WORKDIR "/src/src/services/GroupServices/Groups.Query/Groups.Query.Api"
RUN dotnet build "Groups.Query.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Groups.Query.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Groups.Query.Api.dll"]
