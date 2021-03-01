FROM node:14
ARG SERVER_PORT

WORKDIR /usr/src/app
COPY ./package*.json ./
RUN npm install

COPY ./ ./

EXPOSE ${SERVER_PORT}
CMD [ "node", "index.js" ]