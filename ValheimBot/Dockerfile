FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ValheimBot/ValheimBot.csproj", "ValheimBot/"]
COPY ["ValheimBot/Common.csproj", "ValheimBot/"]
RUN dotnet restore "ValheimBot/ValheimBot.csproj"
COPY . .
WORKDIR "/src/ValheimBot"
RUN dotnet build "ValheimBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ValheimBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ValheimBot.dll"]
