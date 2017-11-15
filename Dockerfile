FROM microsoft/dotnet

ADD . /app
WORKDIR /app/test/Veggerby.Algorithm.Tests
RUN dotnet restore
RUN dotnet build -c Release