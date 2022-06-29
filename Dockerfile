FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
COPY ./ ./
#COPY ./.env ./Matricula.WebUI/.env
RUN dotnet tool install --global dotnet-ef
#ENV PATH="${PATH}:/root/.dotnet/tools"
#RUN dotnet ef database update --project Matricula.Infrastructure --startup-project Matricula.WebUI
RUN dotnet publish --configuration Release

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
RUN apt update
RUN apt install -y curl
COPY ./.env ./.env
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_PRINT_TELEMETRY_MESSAGE=false
COPY --from=build-env /app/Matricula.WebUI/bin/Release/net6.0 .
EXPOSE 80 
ENTRYPOINT ["dotnet", "Matricula.WebUI.dll"]

#COPY Matricula/Matricula.WebUI/bin/Release/net6.0/ App/
#WORKDIR /App
#
#ENV DOTNET_EnableDiagnostics=0
#
#RUN dotnet tool install --global dotnet-ef
#
#RUN dotnet publish --configuration Release
#
#ENTRYPOINT ["dotnet", "Matricula.WebUI.dll"]