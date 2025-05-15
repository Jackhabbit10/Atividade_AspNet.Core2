create database projeto_cidade;

use projeto_cidade;

create table tb_usuario(
	cod_usu int primary key auto_increment,
    nome_usu varchar(80),
    email_usu varchar(80),
    senha_usu varchar(80)
);

create table tb_produto(
	cod_prod int primary key auto_increment,
    nome_prod varchar(80),
    descricao_prod varchar(80),
    preco_prod decimal(10,2)
);

select * from tb_usuario;
select * from tb_produto;
