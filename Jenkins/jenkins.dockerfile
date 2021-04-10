FROM jenkins/jenkins:latest

# Skip setup wizard
ENV JAVA_OPTS -Djenkins.install.runSetupWizard=false

# Install plugins 
COPY jenkins-plugins.txt /usr/share/jenkins/ref/plugins.txt
RUN /usr/local/bin/install-plugins.sh < /usr/share/jenkins/ref/plugins.txt

# Copy seed job
COPY seedjob.groovy /usr/local/seedjob.groovy

# Copy jenkins configuration file
COPY jenkins-configuration.yaml /var/jenkins_home/jenkins-configuration.yaml
ENV CASC_JENKINS_CONFIG /var/jenkins_home/jenkins-configuration.yaml

USER root

# install dot net sdk 5.0
RUN wget -q https://packages.microsoft.com/config/ubuntu/20.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb
RUN apt-get update
RUN apt-get install apt-transport-https
RUN apt-get update
RUN apt-get install -y dotnet-sdk-5.0

# install docker cli
RUN apt-get install -y \
    apt-transport-https \
    ca-certificates \
    curl \
    gnupg-agent \
    software-properties-common

RUN curl -fsSL https://download.docker.com/linux/debian/gpg | apt-key add -

RUN add-apt-repository \
   "deb [arch=amd64] https://download.docker.com/linux/debian \
   $(lsb_release -cs) \
   stable"

RUN apt-get update

RUN apt-get install docker-ce-cli

USER jenkins