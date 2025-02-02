# Build variables
ARG SDK_V=8.0
ARG ASPNET_V=$SDK_V
ARG PROJECT_PATH=API/API.csproj
ARG USER=dot-net
ARG MODE=Debug

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:$SDK_V AS build
ARG MODE
WORKDIR /src
COPY . .
RUN dotnet restore $PROJECT_PATH
RUN dotnet build -c $MODE -o /app/build

# Publish stage
FROM build AS publish
ARG MODE
RUN dotnet publish -c $MODE -o /app/publish --no-restore /p:UseAppHost=false

# Final minimized image
FROM mcr.microsoft.com/dotnet/aspnet:$ASPNET_V
ARG USER
RUN useradd --create-home --shell /bin/bash $USER \
    && mkdir /app \
    && chown -R $USER:$USER /app
USER $USER
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRM.AIService.API.dll"]