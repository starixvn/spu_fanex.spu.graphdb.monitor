FROM registry.nexdev.net:5050/docker/dotnet/core/sdk:5.0.102-ca-patch-buster-slim AS builder
WORKDIR /Fanex.SPU.GraphDb.Monitor

COPY ./Fanex.SPU.GraphDb.Monitor/Fanex.SPU.GraphDb.Monitor ./Fanex.SPU.GraphDb.Monitor
COPY ./Fanex.SPU.GraphDb.Monitor/Fanex.SPU.GraphDb.Monitor.JobService/Fanex.SPU.GraphDb.Monitor.JobService.csproj ./Fanex.SPU.GraphDb.Monitor.JobService/Fanex.SPU.GraphDb.Monitor.JobService.csproj
COPY ./Fanex.SPU.GraphDb.Monitor/Libs ./Libs

WORKDIR /Fanex.SPU.GraphDb.Monitor/Fanex.SPU.GraphDb.Monitor.JobService
RUN dotnet nuget add source http://nuget.nexdev.net/api/v2
RUN dotnet restore -v n

COPY ./Fanex.SPU.GraphDb.Monitor/Fanex.SPU.GraphDb.Monitor.JobService .

RUN dotnet publish -r linux-x64 --self-contained false -o /app 

FROM registry.nexdev.net:5050/docker/dotnet/core/aspnet:5.0-buster-slim AS base

RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /usr/lib/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /usr/lib/ssl/openssl.cnf

WORKDIR /app
COPY --from=builder /app .
ENV TZ="America/La_Paz"

ENTRYPOINT ["dotnet", "Fanex.SPU.GraphDb.Monitor.JobService.dll"]
