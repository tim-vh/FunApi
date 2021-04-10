FROM mcr.microsoft.com/dotnet/runtime:5.0
WORKDIR /funapi
COPY bin/Release/net5.0/. ./
EXPOSE 5000
ENTRYPOINT ["dotnet", "Fun.Api.dll"]