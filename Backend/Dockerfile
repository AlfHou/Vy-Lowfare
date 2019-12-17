FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app


# Copy csproj into container and restore
COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image (see multi stage builds if this is not clear)
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "Backend.dll"]


