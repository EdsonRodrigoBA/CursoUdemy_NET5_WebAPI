FROM mysql:5.7.22
EXPOSE 3306
COPY ./WebApiAspNetCore5/db/Migrations/ /home/database/
COPY ./WebApiAspNetCore5/db/Migrations/ /home/Datasets/
COPY ./WebApiAspNetCore5/ci/init_database.sh//docker-entrypoint-initdb.d/init_database.sh
