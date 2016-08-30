FROM microsoft/dotnet:latest
MAINTAINER João Moreno

COPY src /var/vsts-agent/src

RUN apt-get update
RUN apt-get install unzip
RUN /var/vsts-agent/src/dev.sh layout
RUN /var/vsts-agent/src/dev.sh test