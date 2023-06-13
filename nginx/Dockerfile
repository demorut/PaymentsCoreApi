FROM nginx
COPY labrador.cert.pem /etc/nginx/labrador.cert.pem
COPY labrador.cert.key /etc/nginx/labrador.cert.key

RUN rm /etc/nginx/conf.d/default.conf
COPY nginx.conf /etc/nginx/conf.d