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

FROM node:16.14.0-alpine

RUN mkdir -p /app
WORKDIR /app

COPY package*.json /app/
RUN npm install

COPY . /app/

EXPOSE 4200
CMD ["npm", "start"]
