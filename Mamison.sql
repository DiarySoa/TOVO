create database ressource;
\c ressource;



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
    FOREIGN KEY (id_serv_pos) REFERENCES service_poste (id)

);

INSERT INTO candidat VALUES( default ,'Rasolomanana','Diarisoa','26-06-2004','123@gmail.com','sexe','tel','situation','adresse','region','province');

SELECT nom_poste FROM ser_pos where nom_service = 'securite';

create view candidat_et_diplome as (SELECT c.*, cd.id as id_cd, cd.niveau_etude,cd.diplome, cd.cv, cd.lettre_de_motivaiton, cd.experience from candidat_diplome cd JOIN candidat c ON cd.id_candidat = c.id);

SELECT * FROM candidat_et_diplome WHERE diplome = (SELECT diplome FROM service_poste WHERE id = 13) AND sexe = (SELECT sexe FROM service_poste WHERE id = 13) AND date_de_naissance >= (CURRENT_DATE - INTERVAL '1 year' * (SELECT age_f FROM service_poste WHERE id = 13)) AND date_de_naissance <= (CURRENT_DATE - INTERVAL '1 year' * (SELECT age_d FROM service_poste WHERE id = 13))AND lieu = (SELECT lieu FROM service_poste WHERE id = 13)



SELECT c.nom_candidat,c.prenom_candidat,c.dtn,c.email,c.sexe,c.telephone,c.situation,c.adresse,c.region,c.province 
    FROM candidat c 
        JOIN candidat_diplome cd 
        ON cd.id_candidat = c.id 
        JOIN candidat_poste cp 
        ON cp.id_cand_dip = cd.id 
    WHERE cd.niveau_etude = (SELECT diplome FROM service_poste WHERE id ="+id_serP+") 
    AND c.sexe = (SELECT sexe FROM service_poste WHERE id ="+id_serP+") 
    AND(DATE_PART('year', CURRENT_DATE) - DATE_PART('year', c.dtn)) >= CAST((SELECT age_d FROM service_poste WHERE id ="+id_serP+") AS INTEGER) 
    AND(DATE_PART('year', CURRENT_DATE) - DATE_PART('year', c.dtn)) <= CAST((SELECT age_f FROM service_poste WHERE id ="+id_serP +") AS INTEGER)
    AND c.province = (SELECT lieu FROM service_poste WHERE id ="+id_serP+ " limit (SELECT CAST((valeur_horaire/7)*3  as INTEGER) FROM service_poste WHERE id = " + id_serP + "))

-- 

create view competance as
SELECT c.nom_candidat,c.prenom_candidat,c.dtn,c.email,c.sexe,c.telephone,c.situation,c.adresse,c.region,c.province 
    FROM candidat c 
        JOIN candidat_diplome cd 
        ON cd.id_candidat = c.id 
        JOIN candidat_poste cp 
        ON cp.id_cand_dip = cd.id 
    WHERE cd.niveau_etude = (SELECT diplome FROM service_poste WHERE id ="+ id_serP + ") 
    AND c.sexe = (SELECT sexe FROM service_poste WHERE id ="+ id_serP + ") 
    AND(DATE_PART('year', CURRENT_DATE) - DATE_PART('year', c.dtn)) >= CAST((SELECT age_d FROM service_poste WHERE id ="+ id_serP + ") AS INTEGER) 
    AND(DATE_PART('year', CURRENT_DATE) - DATE_PART('year', c.dtn)) <= CAST((SELECT age_f FROM service_poste WHERE id ="+ id_serP + ") AS INTEGER)
    AND c.province = (SELECT lieu FROM service_poste WHERE id = "+ id_serP + " limit (SELECT CAST((valeur_horaire/7)*3/30  as INTEGER) FROM service_poste WHERE id = "+ id_serP + "));


SELECT c.nom_candidat,c.prenom_candidat,c.dtn,c.email,c.sexe,c.telephone,c.situation,c.adresse,c.region,c.province 
FROM candidat c 
JOIN candidat_diplome cd 
ON cd.id_candidat = c.id 
JOIN candidat_poste cp 
ON cp.id_cand_dip = cd.id 
WHERE cd.niveau_etude = (SELECT diplome FROM service_poste WHERE id =11) 
AND c.sexe = (SELECT sexe FROM service_poste WHERE id =11) 
AND (DATE_PART('year', CURRENT_DATE) - DATE_PART('year', c.dtn)) >= CAST((SELECT age_d FROM service_poste WHERE id =11) AS INTEGER) 
AND (DATE_PART('year', CURRENT_DATE) - DATE_PART('year', c.dtn)) <= CAST((SELECT age_f FROM service_poste WHERE id =11) AS INTEGER) 
AND c.province = (SELECT lieu FROM service_poste WHERE id = 11 limit (SELECT CAST((valeur_horaire/7*3/30) as INTEGER) FROM service_poste WHERE id = 11))



-- 
SELECT * 
FROM candidat_et_diplome 
WHERE diplome = (SELECT diplome FROM service_poste WHERE id = "+ id_serP + ") 
AND sexe = (SELECT sexe FROM service_poste WHERE id = " + id_serP + ") 
AND dtn >= (CURRENT_DATE - INTERVAL '1 year' * (SELECT age_f FROM service_poste WHERE id = " + id_serP + ")) 
AND dtn <= (CURRENT_DATE - INTERVAL '1 year' * (SELECT age_d FROM service_poste WHERE id = "+ id_serP + "))
AND lieu = (SELECT lieu FROM service_poste WHERE id = " + id_serP + ")
;

SELECT  candidat.nom_candidat ,
        candidat.prenom_candidat ,
        candidat.dtn,
        candidat.email ,
        candidat.sexe ,
        candidat.telephone ,
        candidat.situation ,
        candidat.adresse ,
        candidat.region ,
        candidat.province 
FROM candidat
JOIN candidat_diplome
ON candidat_diplome.id_candidat = candidat.id
JOIN candidat_poste 
ON candidat_poste.id_cand_dip = candidat_diplome.id 
WHERE diplome = (SELECT diplome FROM service_poste WHERE id = 1) 
AND sexe = (SELECT sexe FROM service_poste WHERE id = 1) 
AND dtn >= (CURRENT_DATE - INTERVAL '1 year' * (SELECT age_f FROM service_poste WHERE id = 1)) 
AND dtn <= (CURRENT_DATE - INTERVAL '1 year' * (SELECT age_d FROM service_poste WHERE id = 1))
AND lieu = (SELECT lieu FROM service_poste WHERE id = 1)
;


SELECT
    c.nom_candidat,
    c.prenom_candidat,
    c.dtn,
    c.email,
    c.sexe,
    c.telephone,
    c.situation,
    c.adresse,
    c.region,
    c.province
FROM
    candidat c
JOIN
    candidat_diplome cd
ON
    cd.id_candidat = c.id
JOIN
    candidat_poste cp
ON
    cp.id_cand_dip = cd.id
WHERE
    cd.niveau_etude = (SELECT diplome FROM service_poste WHERE id = 5)
    AND c.sexe = (SELECT sexe FROM service_poste WHERE id = 5 )
    AND (DATE_PART('year', CURRENT_DATE) -  DATE_PART('year', c.dtn)) >= CAST((SELECT age_d FROM service_poste WHERE id = 5) AS INTEGER)
    AND (DATE_PART('year', CURRENT_DATE) -  DATE_PART('year', c.dtn)) <= CAST((SELECT age_f FROM service_poste WHERE id = 5) AS INTEGER)
    AND c.province = (SELECT lieu FROM service_poste WHERE id = 5)
    
    limit (SELECT CAST((valeur_horaire/7)*3  as INTEGER) FROM service_poste WHERE id = 5);
    

create table employe(
    id SERIAL PRIMARY KEY,
    nom VARCHAR(50),
    prenom VARCHAR(50),
    genre VARCHAR(50),
    date_de_naissance DATE
);

create table embauche(
    id SERIAL PRIMARY KEY,
    id_employe int,
    date_embauche DATE,
    numero_cnaps VARCHAR(50),
    FOREIGN KEY(id_employe) REFERENCES employe(id)
);

SELECT eb.*, emp.nom, emp.prenom, emp.genre, emp.date_de_naissance FROM embauche eb JOIN employe emp ON eb.id_employe = emp.id;

create view employe_embaucher as (SELECT eb.*, emp.nom, emp.prenom, emp.genre, emp.date_de_naissance FROM embauche eb JOIN employe emp ON eb.id_employe = emp.id);
