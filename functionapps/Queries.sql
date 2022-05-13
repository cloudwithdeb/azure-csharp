--Create Table--
CREATE TABLE users
(
    id int,
    username text,
    amount float,
    email text,
    loc text
);

-- Inter data into database--
insert into dbo.users values(1, 'Owusu', 89.10, 'owusu@gmail.com', 'Kasoa Ghana Flag'),
(2, 'Bright', 71.10, 'bright@gmail.com', 'Kumasi Papaasi'), (3, 'Debrah', 67.77,'debrah@gmail.com','Tema Moto Way');