/*
    1. FELADAT: Hozzunk létre új táblát beosztasok néven. 
    Minden beosztáshoz rendelünk egy numerikus egész azonosítót, mint elsődleges kulcs, minden beosztásnak van neve, 
    tároljuk, hogy aktív vagy inaktív, és végül van lehetőségünk egy fizetési minimum és maximum meghatározására, 
    amelyek megadása kötelező. Emellett tároljuk, hogy mikor került létrehozásra az adott beosztás.
*/

CREATE TABLE beosztasok(
    id INT NOT NULL, 
    nev VARCHAR2(255) NOT NULL,
    aktiv INT DEFAULT 1 NOT NULL,
    fizetes_min NUMBER(12, 2) NOT NULL, -- 12 jegyű szám max: 10 egész jegy és 2 tizedes jegy
    fizetes_max NUMBER(12, 2) NOT NULL,
    letrehozas_ideje TIMESTAMP DEFAULT SYSTIMESTAMP NOT NULL,  
    
    CONSTRAINT PK_beosztasok PRIMARY KEY (id),
    CONSTRAINT CK_beosztasok_aktiv CHECK(aktiv in (0,1)),
    CONSTRAINT CK_beosztasok_fizetes CHECK(fizetes_min <= fizetes_max)
);


INSERT INTO beosztasok(id, nev, fizetes_min, fizetes_max) values (1, 'asszisztens', 250000, 500000);
COMMIT;


SELECT b.id azon, b.nev "beosztás neve", TO_CHAR(b.letrehozas_ideje, 'YYYY-MM-DD HH24:MI:SS:FF2') "létrehozás ideje"
FROM beosztasok b;
