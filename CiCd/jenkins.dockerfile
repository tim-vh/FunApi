FROM jenkins/jenkins
USER root

# install dot net core sdk 3.1
RUN wget -q https://packages.microsoft.com/config/ubuntu/19.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb
RUN apt-get update
RUN apt-get install apt-transport-https
RUN apt-get update
RUN apt-get install -y dotnet-sdk-3.1

USER jenkins