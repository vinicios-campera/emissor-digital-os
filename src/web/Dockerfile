FROM node:20-alpine as build
WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH
COPY package.json /app/
COPY package-lock.json /app/
COPY .env /app/

ARG GITHUB_TOKEN
RUN echo "@vinicios-campera:registry=https://npm.pkg.github.com" > /app/.npmrc \
    && echo "//npm.pkg.github.com/:_authToken=${GITHUB_TOKEN}" >> /app/.npmrc

RUN npm install --ignore-engines
COPY . /app
RUN npm run build

FROM nginx:1.17.8-alpine
COPY --from=build /app/build /usr/share/nginx/html
RUN rm /etc/nginx/conf.d/default.conf
COPY nginx/nginx.conf /etc/nginx/conf.d
EXPOSE 80
LABEL org.opencontainers.image.source https://github.com/vinicios-campera/emissor-digital-os
CMD ["nginx", "-g", "daemon off;"]