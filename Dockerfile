FROM node:14-alpine
ARG SERVER_PORT

ENV DOCKER true

WORKDIR /usr/src/app

# Server
COPY /Server/package*.json ./
RUN npm install
COPY /Server ./

# Branding
COPY /Branding/Artwork/Backdrops/Bricks.png ./servers/eclipse/public/img/art/bricks.png
COPY /Branding/Artwork/Backdrops/Main.png ./servers/eclipse/public/img/art/default.png

COPY /Branding/Logos/Primary/Big.png ./servers/eclipse/public/img/brand/large.png
COPY /Branding/Logos/Primary/Small.png ./servers/eclipse/public/img/brand/small.png

# Packaging
RUN mkdir ./packaging
COPY /Packaging/Version ./packaging/version
COPY /.git/refs/heads/main ./packaging/commit

EXPOSE ${SERVER_PORT}
CMD [ "node", "index.js" ]