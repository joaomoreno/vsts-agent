FROM microsoft/dotnet:latest
MAINTAINER João Moreno

ARG SOURCE_COMMIT

RUN apt-get update
RUN apt-get install unzip

COPY src /var/vsts-agent/src

RUN /var/vsts-agent/src/dev.sh layout
RUN /var/vsts-agent/src/dev.sh test