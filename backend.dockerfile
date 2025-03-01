FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /build
COPY ./src .
RUN dotnet publish --output /app
WORKDIR /app
RUN rm -rf ../src
