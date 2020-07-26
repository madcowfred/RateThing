FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build

RUN apt-get update && apt-get install -y build-essential nodejs npm

WORKDIR /app

# Restore npm packages now so that this step is cached
COPY src/RateThing.Web/package*.json /app/src/RateThing/
WORKDIR /app/src/RateThing.Web
RUN npm install

WORKDIR /app
COPY . .
#RUN dotnet test

WORKDIR /app/src/RateThing.Web
RUN npm run build-prod
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /app
COPY --from=build /app/src/RateThing.Web/out ./

ENV ASPNETCORE_URLS "http://0:5000"
EXPOSE 5000
ENTRYPOINT ["dotnet", "RateThing.Web.dll"]
