FROM mcr.microsoft.com/dotnet/framework/sdk:4.7.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY CSharp.Data.Client/*.sln ./CSharp.Data.Client/
COPY CSharp.Data.Client/CSharp.Data.Client/*.csproj ./CSharp.Data.Client/CSharp.Data.Client/
COPY CSharp.Data.Client/CSharp.Data.Client/*.config ./CSharp.Data.Client/CSharp.Data.Client/
RUN nuget restore CSharp.Data.Client/CSharp.Data.Client.sln

COPY *.sln .
COPY CSharp.Saude.FitbitTask/*.csproj ./CSharp.Saude.FitbitTask/
COPY CSharp.Saude.FitbitTask/*.config ./CSharp.Saude.FitbitTask/
RUN nuget restore

# copy everything else and build app
COPY . ./
WORKDIR /app
RUN msbuild /p:Configuration=Release

#ENTRYPOINT ["C:\\app\\CSharp.Saude.FitbitTask\\bin\\Release\\CSharp.Saude.FitbitTask.exe"]
CMD ".\\CSharp.Saude.FitbitTask\\bin\\Release\\CSharp.Saude.FitbitTask.exe"
