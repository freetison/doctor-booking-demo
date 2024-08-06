FROM node:alpine

WORKDIR /usr/src/app

COPY . /usr/src/app

RUN npm install -g @angular/cli

RUN npm install --legacy-peer-deps

CMD ["ng", "serve", "--port", "4201", "--host", "0.0.0.0", "--disable-host-check"]
