FROM microsoft/dotnet:latest
MAINTAINER João Moreno

COPY src /var/vsts-agent/src

RUN /var/vsts-agent/src/dev.sh restore
RUN /var/vsts-agent/src/dev.sh build
RUN /var/vsts-agent/src/dev.sh test