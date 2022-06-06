FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AuthorAPI/AuthorAPI.csproj", "AuthorAPI/"]
RUN dotnet restore "AuthorAPI/AuthorAPI.csproj"
COPY . .
WORKDIR "/src/AuthorAPI"
RUN dotnet build "AuthorAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthorAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthorAPI.dll"]
