/*
    3. FELADAT:
    Hozzunk létre új táblát szervezeti_egysegek néven. A szervezeti egységek hierarchiája maximum 3 szintű, 
    az 1. szintű egységek azonosítója 3 karakter, a 2. szintűeké 6 karakter (az első 3 a szülő), 
    a 3. szintűeké 9 karakter (az első 6 a szülő). Ezt az értéket tároljuk, ez lesz az elsődleges kulcs. 
    Az azonosító csak az angol ábécé nagybetűit  tartalmazhatja. 
    A szervezeti egységnek van neve, szülő szervezeti egysége (1. szinten null), van létrehozás, módosítás és törlése ideje mezőjük. 
    Kezdeteben a létrehozás és a módosítás ugyanaz az időbélyeg legyen. 
    A tényleges rekordtörlést ennél a táblánál nem támogatjuk: ha a törölvemező ki van töltve az azt jelenti, hogy a rekordot törültük.
*/

CREATE TABLE szervezeti_egysegek(
    kod VARCHAR2(9) NOT NULL,
    nev VARCHAR2(250) NOT NULL,
    szulo_kod VARCHAR2(9) DEFAULT NULL,
    letrehozas_ideje TIMESTAMP DEFAULT SYSTIMESTAMP NOT NULL,
    modositas_ideje TIMESTAMP DEFAULT SYSTIMESTAMP NOT NULL,
    torles_ideje TIMESTAMP DEFAULT NULL,
    
    CONSTRAINT PK_sze_egys PRIMARY KEY(kod),
    CONSTRAINT PK_sze_egys_kod CHECK(
        REGEXP_LIKE(kod,'^[A-Z](3)$', 'c') OR
        REGEXP_LIKE(kod,'^[A-Z](6)$', 'c') OR
        REGEXP_LIKE(kod,'^[A-Z](9)$', 'c')
    ),
    CONSTRAINT FK_sze_egys_szulo_kod FOREIGN KEY(szulo_kod) REFERENCES szervezeti_egysegek(kod),
    -- önhivatkozás: a szulo_kod csak a kod mező értékét veheti fel!
    CONSTRAINT CK_sze_egys_szulo_kod CHECK(
        (szulo_kod IS NULL AND LENGTH(kod) = 3) OR (szulo_kod IS NOT NULL and LENGTH(kod) > 3)
    ),
    CONSTRAINT CK_sze_egys_modositas_ideje CHECK(
        letrehozas_ideje <= modositas_ideje
    ),
    CONSTRAINT CK_sze_egys_torles_ideje CHECK(
        torles_ideje IS NULL OR
        letrehozas_ideje < torles_ideje OR
        modositas_ideje < torles_ideje
    )
);
