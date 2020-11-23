FROM jenkins/jenkins

# Skip setup wizard
ENV JAVA_OPTS -Djenkins.install.runSetupWizard=false

# Install plugins 
COPY jenkins-plugins.txt /usr/share/jenkins/ref/plugins.txt
RUN /usr/local/bin/install-plugins.sh < /usr/share/jenkins/ref/plugins.txt

# Change configuration
COPY jenkins-configuration.yaml /var/jenkins_home/jenkins-configuration.yaml
ENV CASC_JENKINS_CONFIG /var/jenkins_home/jenkins-configuration.yaml

USER root

# install dot net core sdk 3.1
RUN wget -q https://packages.microsoft.com/config/ubuntu/19.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb
RUN apt-get update
RUN apt-get install apt-transport-https
RUN apt-get update
RUN apt-get install -y dotnet-sdk-3.1

USER jenkins