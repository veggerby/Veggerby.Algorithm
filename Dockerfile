from microsoft/dotnet

ADD . /app
WORKDIR /app
RUN dotnet restore
RUN dotnet build ./test/Veggerby.Algorithm.Tests -c Release