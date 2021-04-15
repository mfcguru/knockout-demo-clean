FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS builder
WORKDIR /src

COPY . .
RUN dotnet publish -c release -o /published 

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=builder /published .

ENV DISPLAY :10
COPY xvfb.init /etc/init.d/xvfb
# Removes any windows line endings because linux can`t start the service in this case
RUN sed -i -e 's/\r//g' /etc/init.d/xvfb
RUN chmod +x /etc/init.d/xvfb
RUN update-rc.d xvfb defaults
RUN service xvfb start

EXPOSE ${PORT}
ENTRYPOINT ["dotnet", "KnockoutDemo.dll"]