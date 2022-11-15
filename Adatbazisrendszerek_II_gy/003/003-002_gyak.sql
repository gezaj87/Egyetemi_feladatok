/*
    2. FELADAT: Készítsünk PL/SQL szkriptet, amely futtatásával új beosztást tudunk készíteni! 
    A folyamat egyes lépéseiről tájékoztassuk a felhasználót képernyőre írás formájában!

    -   A feladat megoldása elején a beosztás neve konstans értékként kerül definiálásra, 
        a hozzá kapcsolódó azonosító pedig változóként (mint javasolt azonosító), egyik érték sem lehet null. 
        Továbbá a min és max értékek is megadhatóak, ha ezek nincsenek megadva akkor értéküket úgy kell meghatározni, 
        hogy a min az aktív munkakörök legkisebb, a maximum a munkakörök legnagyobb érétke legyen.
    -   Ellenőrizzük, hogy ilyen névvel létezik-e már beosztás. 
        A név összehasonlításánál a sor eleji és végi  whitespace karaktereket nem kell figyelembe venni,
        valamint a kis- és nagybetű különbséget sem.  Ha létezik a beosztás, 
        akkor annak azonosítója kerüljön be a korábban definiált változóba, majd a szkript leáll.
    -   Ha még nem létezik az beosztás, akkor ellenőrizzük, hogy a javasolt azonosító helyes-e (nincs még használatban), 
        ha már használatban van, akkor generáljunk egy újat, ehhez vegyük a táblában előforduló legnagyobb értéket és adjunk hozzá egyet. 
        Majd a szkript végén rögzítsük az új rekordot.
*/


SET SERVEROUTPUT ON;
DECLARE
    v_nev CONSTANT VARCHAR2(250) NOT NULL := 'rendszergazda'; 
    v_id beosztasok.id%TYPE NOT NULL := 1;
    v_min beosztasok.fizetes_min%TYPE;
    v_max beosztasok.fizetes_max%TYPE;
    v_cnt INT := 0; --ideiglenes 
BEGIN
    IF v_min IS NULL THEN
        SELECT MIN(b.fizetes_min) min
        INTO
            v_min
        FROM beosztasok b
        WHERE b.aktiv = 1;
    END IF;
    
    IF v_max IS NULL THEN
        SELECT MAX(b.fizetes_min) max
        INTO v_max
        FROM beosztasok b
        WHERE b.aktiv = 1;
    END IF;
    
    -- annak ellenőrzése, hogy létezik-e már a rekord:
    SELECT COUNT(*)
    INTO v_cnt
    FROM beosztasok b
    WHERE UPPER(TRIM(b.nev)) = UPPER(TRIM(v_nev));
        
    IF v_cnt <> 0 THEN
        SELECT b.id INTO v_id
        FROM beosztasok b
        WHERE UPPER(TRIM(b.nev)) = UPPER(TRIM(v_nev));
        DBMS_OUTPUT.PUT_LINE('Létező rekord volt!');
    ELSE -- nem létezik
        SELECT COUNT(*) INTO v_cnt
        FROM beosztasok b
        WHERE b.id = v_id;
        
        IF v_cnt = 1 THEN
            -- az elsődleges kód már használatban van => újat kell készíteni
            -- a feladat azt mondja, h ekkor válaszd ki a legnagyobbat és adj hozzá 1-et!
            SELECT MAX(b.id) + 1 INTO v_id
            FROM beosztasok b;
        END IF;
        
        DBMS_OUTPUT.PUT_LINE('Kísérlet a rekord beszúrására!');
        INSERT INTO beosztasok(id, nev, fizetes_min, fizetes_max)
        VALUES (v_id, v_nev, v_min, v_max);
        COMMIT;
    END IF;
    
    
END;




