FROM node:16.14.0-alpine AS build

RUN mkdir -p /app
WORKDIR /app

COPY package*.json /app/
RUN npm install

COPY . /app/
RUN npm run build:ssr && npm run prerender & npm run build

FROM nginx:alpine

RUN rm -rf /usr/share/nginx/html/* && rm -rf /etc/nginx/conf.d/default.conf

COPY ./default.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist/MySpace/browser /usr/share/nginx/html

EXPOSE 80
