
drop domain if exists phone;
create domain phone as text check(
   octet_length(value) between 1/*+*/ + 8 and 1/*+*/ + 15 + 3 and value ~ '^\+\d+$'
);
comment on domain phone is 'Номер телефона в международном формате E.164';

drop domain if exists  email;
create domain email as text check(
    octet_length(value) between 6 and 320 and value like '_%@_%.__%'
);

create extension if not exists "uuid-ossp";

drop table if exists public.users;
create table public.users
(
  userId uuid not null default(uuid_generate_v1()) primary key,
  userName character varying(64) COLLATE pg_catalog."C" NOT NULL,
  firstName character varying(64) COLLATE pg_catalog."C" NOT NULL,
  lastName character varying(64) COLLATE pg_catalog."C" NOT NULL,
  phone phone,
  email email,
  unique (userName),
  unique (email),
  unique (phone)
);
alter table public.users owner to AvitoUsr;


drop table if exists public.users;
create table public.users
(
  userId uuid not null default(uuid_generate_v1()) primary key,
  userName character varying(64) COLLATE pg_catalog."C" NOT NULL,
  firstName character varying(64) COLLATE pg_catalog."C" NOT NULL,
  lastName character varying(64) COLLATE pg_catalog."C" NOT NULL,
  phone phone,
  email email,
  unique (userName),
  unique (email),
  unique (phone)
);
alter table public.users owner to AvitoUsr;

create table public.products
(
  productId uuid not null default uuid_generate_v1() primary key,
  productName character varying(1024) COLLATE pg_catalog."C" NOT NULL,
  productCaption character varying(1024) COLLATE pg_catalog."C" NOT NULL,
  productDecription text,
  userId uuid references public.users(userId)  
);
alter table public.products owner to AvitoUsr;

create table public.productImages
(
  productImageId uuid not null default uuid_generate_v1() primary key,
  num integer,
  productId uuid references public.products(productId),
  decription text,
  imageRef character varying(1024) COLLATE pg_catalog."C" NOT NULL
);
alter table public.productImages owner to AvitoUsr;
