
insert into public.users(username, firstname, lastname, phone, email)
    values ('usr1', 'user1', 'user_1', '+79991234567', 'usr1@mail.com'),
           ('usr2', 'user2', 'user_2', '+79991234568', 'usr2@mail.com'),
           ('usr3', 'user3', 'user_3', '+79991234569', 'usr3@mail.com'),
           ('usr4', 'user4', 'user_4', '+79991234510', 'usr4@mail.com'),
           ('usr5', 'user5', 'user_5', '+79991234511', 'usr5@mail.com');

insert into public.products(productname, productcaption, productdecription, userid)    
select 'граж', 'огромный гараж', 'продам огромный гараж 4 кв.м',userId
from public.users where username = 'usr1'

insert into public.products(productname, productcaption, productdecription, userid)    
select 'собака', 'отличная собака', 'продам отлчиную собаку',userId from public.users where username = 'usr2'
union all
select 'тарелка', 'большая тарелка', 'новая, муха не сидела',userId from public.users where username = 'usr2'

insert into public.products(productname, productcaption, productdecription, userid)    
select 'носок', 'старый носок', 'продам носок на удачу',userId from public.users where username = 'usr3'
union all
select 'тарелка', 'тарелка', 'вид на фото',userId from public.users where username = 'usr3'
union all
select 'силос', 'силос', 'силос 10 тонн',userId from public.users where username = 'usr3'

insert into public.productimages(num, productid, decription, imageref)
select 1,productid,'фото1', '../' || productid::text || '/1.jpg' from public.products
union all
select 2,productid,'фото1', '../' || productid::text || '/2.jpg' from public.products    