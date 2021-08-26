FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

#
# copy csproj and restore as distinct layers
COPY *.csproj ./
#
RUN dotnet restore 

#
# copy everything else and build app
COPY . ./
#
WORKDIR /app
RUN dotnet publish -c Release -o ./publish
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
#
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "babel-web-app.dll"]
