FROM debian:bookworm-slim

EXPOSE 20005
EXPOSE 22005/udp
EXPOSE 22006

RUN apt update && apt install -y apt-transport-https wget

RUN wget https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb

RUN dpkg -i packages-microsoft-prod.deb

RUN echo 'deb http://httpredir.debian.org/debian testing main contrib non-free' > /etc/apt/sources.list

RUN apt update && apt install -y liblocal-lib-perl libjson-perl libatomic1 procps dotnet-sdk-8.0 libstdc++6

RUN wget http://ftp.debian.org/debian/pool/main/o/openssl/libssl1.1_1.1.1w-0+deb11u1_amd64.deb

RUN dpkg -i libssl1.1_1.1.1w-0+deb11u1_amd64.deb

WORKDIR /opt/app

RUN wget https://cdn.rage.mp/updater/prerelease/server-files/linux_x64.tar.gz && \
	tar -xzf linux_x64.tar.gz

WORKDIR /opt/app/ragemp-srv

COPY ./client_packages /opt/app/ragemp-srv/client_packages
COPY ./dotnet/resources /opt/app/ragemp-srv/dotnet/resources
COPY ./dotnet/settings.xml /opt/app/ragemp-srv/dotnet/
COPY ./maps /opt/app/ragemp-srv/maps
COPY ./packages /opt/app/ragemp-srv/packages
COPY ./serverdata /opt/app/ragemp-srv/serverdata
COPY ./conf.json /opt/app/ragemp-srv/

RUN chmod +x ragemp-server

CMD ["./ragemp-server"]