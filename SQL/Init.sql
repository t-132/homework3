drop role if exists AvitoUsr;
create role AvitoUsr login
encrypted password 'md55388677cdb0fb802da8c9d1155cd59c4'
nosuperuser inherit nocreatedb nocreaterole noreplication connection limit 1;

drop tablespace if exists Avito_tblspace;

create tablespace Avito_tblspace location E'D:\\PostgreSQL\\17';

drop database if exists AvitoDb;

create database AvitoDb 
with owner = AvitoUsr
    encoding = 'UTF8'
    tablespace = Avito_tblspace
    lc_collate = 'Russian_Russia.1251'
    lc_ctype = 'Russian_Russia.1251'
    connection limit = -1;



