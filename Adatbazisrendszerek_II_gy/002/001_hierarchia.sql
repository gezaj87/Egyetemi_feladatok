select e.ename név, e.job munkakör, f.ename "főnök neve"
from scott.emp e
left join scott.emp f on f.empno = e.mgr
order by "főnök neve" ASC NULLS FIRST, név ASC;

/*
 Készítsen lekérdezést, amely a főfőnöktől 4 hierarchikus távolságban lévő beosztottakat listázza.
*/

select f0.ename, f1.ename, f2.ename, f3.ename, f4.ename "negyedik szint"
from scott.emp f0
    inner join scott.emp f1 on f1.mgr = f0.empno
    inner join scott.emp f2 on f2.mgr = f1.empno
    inner join scott.emp f3 on f3.mgr = f2.empno
    inner join scott.emp f4 on f4.mgr = f3.empno
where f0.mgr is null
order by 5 asc;

/*
# HIEARHIKUS ADATSZERKEZETEK KEZELÉSE
  A fenti megoldás Oracle specifikus megoldása
*/


select t.*
from (
        select
            e.ename "alkalmazott",
            PRIOR e.ename "közvetlen főnök",
            CONNECT_BY_ROOT e.ename "legfőbb főnök",
            LEVEL szint,
            SYS_CONNECT_BY_PATH(e.ename, '->') útvonal -- hiearhija útvonal megj.
        from scott.emp e
        START WITH e.mgr IS NULL
        CONNECT BY e.mgr = PRIOR e.empno
        ORDER SIBLINGS BY e.ename
    ) t
WHERE t.szint = 4;     