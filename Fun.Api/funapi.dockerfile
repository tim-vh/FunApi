FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /funapi
COPY bin/Release/netcoreapp3.1/. ./
EXPOSE 5000
ENTRYPOINT ["dotnet", "Fun.Api.dll"]