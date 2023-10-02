create database RH;
\c RH

create table service (
    id SERIAL PRIMARY KEY,
    nom_service VARCHAR(50)
);

insert into service values (default, 'securite');
insert into service values (default, 'sanitaire');
insert into service values (default, 'bureautique');


create table poste(
    id SERIAL PRIMARY KEY,
    nom_poste VARCHAR(50),
    id_ser INT,
    FOREIGN KEY (id_ser) REFERENCES service (id)
);
insert into poste values (default, 'gardien', 1);
insert into poste values (default, 'securite', 1);
insert into poste values (default, 'nettoyeur', 2);
insert into poste values (default, 'eboueur', 2);
insert into poste values (default, 'comptable', 3);
insert into poste values (default, 'secretaire', 3);

create table note_diplome (
    id SERIAL PRIMARY KEY,
    id_poste INT,
    CEPE DOUBLE PRECISION,
    BEPC DOUBLE PRECISION,
    BACC DOUBLE PRECISION,
    LICENSE DOUBLE PRECISION,
    MASTER DOUBLE PRECISION,
    FOREIGN KEY (id_poste) REFERENCES poste (id)
);

insert into note_diplome values (default, 1, 20, 50, 0,0,0);
insert into note_diplome values (default, 2, 50, 10, 0,0,0);
insert into note_diplome values (default, 3, 60, 10, 0,0,0);
insert into note_diplome values (default, 4, 30, 50, 0,0,0);
insert into note_diplome values (default, 5, 0,  0, 0, 50, 60);
insert into note_diplome values (default, 6, 0, 0, 0,60,60);



