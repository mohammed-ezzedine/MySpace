FROM node:16.14.0-alpine AS build

RUN mkdir -p /app
WORKDIR /app

COPY package*.json /app/
RUN npm install

COPY . /app/
RUN npm run build

FROM nginx:alpine

RUN rm -rf /usr/share/nginx/html/* && rm -rf /etc/nginx/conf.d/default.conf

COPY ./default.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist/my-space /usr/share/nginx/html

EXPOSE 80
