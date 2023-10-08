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

create table service_poste(
    id SERIAL PRIMARY KEY,
    id_service INT,
    valeur_horaire DOUBLE PRECISION,
    id_poste INT,
    diplome VARCHAR(50),
    FOREIGN KEY (id_poste) REFERENCES poste (id),
    FOREIGN KEY (id_service) REFERENCES service (id)
);


create table candidat(
    id SERIAL PRIMARY KEY,
    nom_candidat VARCHAR(50),
    prenom_candidat VARCHAR(50),
    dtn DATE,
    email VARCHAR(50),
    sexe VARCHAR(50),
    telephone VARCHAR(50)
);

create table candidat_diplome(
    id SERIAL PRIMARY KEY,
    id_candidat int,
    niveau_etude VARCHAR(50),
    diplome VARCHAR(50),
    cv VARCHAR(50),
    lettre_de_motivaiton VARCHAR(50),
    FOREIGN KEY (id_candidat) REFERENCES candidat (id)
);

///////////////////////////////////////////////////////////////////////////////////////////////////////


create database ressource;
\c ressource

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

create view ser_pos as(select s.id as id_service, s.nom_service, p.* from poste p JOIN service s ON p.id_ser = s.id);

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

create table service_poste(
    id SERIAL PRIMARY KEY,
    id_service INT,
    valeur_horaire DOUBLE PRECISION,
    id_poste INT,
    diplome VARCHAR(50),
    sexe VARCHAR(50),
    age_d VARCHAR(50),
    age_f VARCHAR(50),
    lieu VARCHAR(50),
    FOREIGN KEY (id_poste) REFERENCES poste (id),
    FOREIGN KEY (id_service) REFERENCES service (id)
);


create table candidat(
    id SERIAL PRIMARY KEY,
    nom_candidat VARCHAR(50),
    prenom_candidat VARCHAR(50),
    dtn DATE,
    email VARCHAR(50),
    sexe VARCHAR(50),
    telephone VARCHAR(50),
    situation VARCHAR(50),
    adresse VARCHAR(50),
    region VARCHAR(50),
    province VARCHAR(50)
);

create table candidat_diplome(
    id SERIAL PRIMARY KEY,
    id_candidat int,
    niveau_etude VARCHAR(50),
    diplome VARCHAR(50),
    cv VARCHAR(50),
    lettre_de_motivaiton VARCHAR(50),
    experience int,
    FOREIGN KEY (id_candidat) REFERENCES candidat (id)
);

create table candidat_poste(
    id_cand_dip int,
    id_serv_pos int,
    FOREIGN KEY (id_cand_dip) REFERENCES candidat_diplome (id),
    FOREIGN KEY (id_serv_pos) REFERENCES candidat (id)

);

INSERT INTO candidat VALUES( default ,'Rasolomanana','Diarisoa','26-06-2004','123@gmail.com','sexe','tel','situation','adresse','region','province');

SELECT nom_poste FROM ser_pos where nom_service = 'securite';

create view candidat_et_diplome as (SELECT c.*, cd.id as id_cd, cd.niveau_etude,cd.diplome, cd.cv, cd.lettre_de_motivaiton, cd.experience from candidat_diplome cd JOIN candidat c ON cd.id_candidat = c.id);







