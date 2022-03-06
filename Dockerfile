FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-dotnet
WORKDIR /app
EXPOSE 80

# Copy and build API.
COPY . .
RUN dotnet restore MyGames/src/MyGames.API/MyGames.API.csproj
RUN dotnet build --no-restore --configuration Release MyGames/src/MyGames.API/MyGames.API.csproj
RUN dotnet test --no-build --verbosity normal MyGames/tests/MyGames.API.Tests.Unit/MyGames.API.Tests.Unit.csproj
RUN dotnet test --no-build --verbosity normal MyGames/tests/MyGames.Core.Tests.Unit/MyGames.Core.Tests.Unit.csproj

RUN dotnet publish -r linux-x64 --no-restore -c Release -o ./dist/release --self-contained MyGames/src/MyGames.API/MyGames.API.csproj

FROM node:17-alpine as build-webapp
WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH

COPY MyGames/src/MyGames.Frontend/ .

RUN npm install --silent

# COPY . .
RUN npm run build


# runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-dotnet /app/dist/release .
COPY --from=build-webapp /app/.next ./wwwroot/

ENTRYPOINT [ "./MyGames.API" ]