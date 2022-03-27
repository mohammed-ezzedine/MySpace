#FROM node:16.14.0-alpine AS appbuild
#
#RUN mkdir -p /app
#WORKDIR /app
#
#COPY package*.json /app/
#RUN npm install
#
#COPY . /app/
#RUN npm run build --prod
#
#FROM node:16.14.0-alpine
#RUN mkdir -p /app
#WORKDIR /app
#
#COPY package*.json /app/
#
#RUN npm install
#COPY --from=appbuild /app/dist /app/
#
#ENTRYPOINT npm start

FROM node:16.14.0-alpine AS build

RUN mkdir -p /app
WORKDIR /app

COPY package*.json /app/
RUN npm install
RUN npm i @angular/cli

COPY . /app/

RUN node_modules/.bin/ng build

EXPOSE 4200
CMD ["npm", "run", "build"]

FROM nginx
COPY --from=build /app/dist/my-space /usr/share/nginx/html
