FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AuthorBlazor/AuthorBlazor.csproj", "AuthorBlazor/"]
RUN dotnet restore "AuthorBlazor/AuthorBlazor.csproj"
COPY . .
WORKDIR "/src/AuthorBlazor"
RUN dotnet build "AuthorBlazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthorBlazor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthorBlazor.dll"]
