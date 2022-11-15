# 1) Adott egy x: egész típusú szám, add össze a számokat x-ig rekurzió segítségével!
def Osszead(x:int) ->int :
    global steps;
    if (x == 0):
        return 0;
    return x + Osszead(x - 1);
               
eredmeny = Osszead(5);
# print(eredmeny);


# 2) Add össze egy lista elemeinek összegét rekurzió segítségével!
def ListaOsszege(v: list, n: int) ->int:
    if n == 0:
        return v[0];
    return v[n] + ListaOsszege(v, n - 1);

vektor = [1,2,3,4];
vektor_n = len(vektor) - 1;
eredmeny = ListaOsszege(vektor, vektor_n);
#print(eredmeny);


# 3) számold ki X n-edik hatványát rekurzió segítségével!
def Hatvany(x: int, n: int)->int:
    if n == 0:
        return 1;
    return x * Hatvany(x, n-1);

eredmeny = Hatvany(10, 3);
# print(eredmeny);


# 4) Állapítsd meg a paraméterben kapott számról, hogy a természetes számok halmazába tartozik vagy nem abba a halmazba tartozik rekurzió segítségével!
def TermeszetesSzamE(x: int) -> bool:
    if x == 0:
        return True;
    if x < 0:
        return False;
    return TermeszetesSzamE(x - 1);

eredmeny = TermeszetesSzamE(10);
#print(eredmeny);


# 5) Adja össze egy nem negatív egész szám számjegyeinek értékeit rekurzió használatával!
def SzamjegyekOsszege(x: int) -> int:
    if x == 0:
        return 0;
    return x % 10 + SzamjegyekOsszege(x // 10);

eredmeny = SzamjegyekOsszege(1111);
#print(eredmeny);


# A régi, nyomógombos telefonok billentyűzete a képen látható módon néz ki. A számjegyeket tartalmazó billentyűkkel (a 0 és 1 billentyűk kivételével) különböző betűket lehet leírni (pl. a 3-as billentyű segítségével a d , e és f betűket írhatjuk le).
# Írj egy letter_combinations nevű függvényt, amely egy olyan szöveget kap paraméterül, amely a telefonon megnyomott számjegyeket tartalmazza a megnyomás sorrendjében. A függvény adja vissza egy listában az adott gombok adott sorrendben történő lenyomásával kapható összes betűkombinációt. Feltételezhetjük, hogy az input mindig megfelelő lesz (tehát nem lesznek benne oda nem illő karakterek (0, 1, *, #)).

# Példák:
# Input: "532"
# Return:
# [
# "jda", "jdb", "jdc", "jea", "jeb", "jec",
# "jfa", "jfb", "jfc", "kda", "kdb", "kdc",
# "kea", "keb", "kec", "kfa","kfb", "kfc",
# "lda", "ldb", "ldc", "lea", "leb", "lec",
# "lfa", "lfb", "lfc"
# ]

def letter_combinations(digits: str) -> list:
    if len(digits) == 0:
        return [];
    if len(digits) == 1:
        return get_letters(digits[0]);
    return [x + y for x in get_letters(digits[0]) for y in letter_combinations(digits[1:])];

def get_letters(digit: str) -> list:
    if digit == '2':
        return ['a', 'b', 'c'];
    elif digit == '3':
        return ['d', 'e', 'f'];
    elif digit == '4':
        return ['g', 'h', 'i'];
    elif digit == '5':
        return ['j', 'k', 'l'];
    elif digit == '6':
        return ['m', 'n', 'o'];
    elif digit == '7':
        return ['p', 'q', 'r', 's'];
    elif digit == '8':
        return ['t', 'u', 'v'];
    elif digit == '9':
        return ['w', 'x', 'y', 'z'];
    
    else: return [];

eredmeny = letter_combinations("23");
#print(eredmeny) 
   

# 6) Írjon Python programot rekurzív algoritmus használatával, ahol a nulla kivételével egy természetes számot vár paraméterben az algoritmus és annak segítségével visszaadja a harmonikus sor n-edik tagját.

def harmonikus_sor_n(n: int)->float:
    if n == 1:
        return 1;
    return 1 / n + harmonikus_sor_n(n-1)

eredmeny = harmonikus_sor_n(5);
#print(eredmeny);


# 7) Írjon Python programot rekurzív algoritmus használatával, ahol egy természetes számot vár paraméterben az algoritmus és annak segítségével kiszámolja az összegét az alábbi egész számokat tartalmazó sorozatnak: n + (n−2) + (n−4) + (n-6) + ..., amíg az n − x < 1.

def osszeg(n: int)->float:
    if (n < 1):
        return n;
    return n + osszeg(n-2);

eredmeny = osszeg(10);
#print(eredmeny);


# 8) Ackermann függvény eredménye
def Ackermann(m: int, n: int)-> int:
    if (m == 0):
        return n + 1;
    if (m  > 0 and n == 0):
        return Ackermann(m-1, 1)
    return Ackermann(m-1, Ackermann(m, n-1));

eredmeny = Ackermann(2, 2);
#print(eredmeny);


# 9) FAKTORIÁLIS REKURZÍV KISZÁMÍTÁSA n nemnegatív egész szám faktoriálisának kiszámítása
def Faktorialis(k: int)->int:
    if k == 0:
        return 1;
    return k * Faktorialis(k-1);

eredmeny = Faktorialis(5);
#print(eredmeny);


# 10) FIBONACCI-SZÁMOK ELŐÁLLÍTÁSA n-edik fibonacci szám
def Fib(n:int) -> int:
    if n == 0:
        return 0;
    elif n == 1:
        return 1;
    else: 
        return Fib(n-1) + Fib(n-2);
    
eredmeny = Fib(10);
#print(eredmeny);


# 11) Írjon Python programot, amely egy rekurzív algoritmus használatával kiszámolja két szám legnagyobb közös osztóját az Euklideszi algoritmus segítségével!
def legnagyobb_kozos_oszto(a: int, b: int)->int:
    if b == 0:
        return a;
    return legnagyobb_kozos_oszto(b, a % b);

#print(legnagyobb_kozos_oszto(161, 119))


# 12) Egy természetes számot vár paraméterben az algoritmus és annak segítségével kiszámolja az összegét az alábbi egész számokat tartalmazó sorozatnak: n + (n−2) + (n−4) + (n-6) + ..., amíg az n − x < 1.
def Megold(n: int)->int:
    if n < 1:
        return n;
    return n + Megold(n-2);

eredmeny = Megold(10);
#print(eredmeny);


# 13) Írjon rekurzív algoritmust, amely megállapítja egy paraméterben kapott sztringről, hogy az palindrom.
def Palindrom(s):
    if len(s) <= 1:
        return True
    else:
        if s[0] == s[-1]:
            return Palindrom(s[1:-1])
        else:
            return False

eredmeny = Palindrom("indulagörögaludni")
#print(eredmeny)