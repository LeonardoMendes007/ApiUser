FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ApiUser.csproj", "./"]
RUN dotnet restore "ApiUser.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ApiUser.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiUser.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiUser.dll"]
