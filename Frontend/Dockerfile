# Build stage
FROM node:lts-alpine as build-stage

WORKDIR /app

# Copy package and packege lock to image
COPY package*.json ./

# Install dependencies
RUN npm install

COPY . .

# Build app for production
RUN npm run build


# Production stage
FROM nginx:stable-alpine as production-stage
COPY --from=build-stage /app/dist /usr/share/nginx/html

# Change nginx config
RUN rm /etc/nginx/conf.d/default.conf
COPY nginx/nginx.conf /etc/nginx/conf.d

CMD ["nginx", "-g", "daemon off;"]

