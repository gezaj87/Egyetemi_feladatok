# 1) Ki lakik odalent? (6 pont)
# - Írj egy mocking_spongebob nevű függvényt, amely egy adatot olvas be a felhasználótól, billentyűzetről.
# - A függvény alakítsa át a felhasználótól kapott szöveget úgy, hogy a szövegben VÉletLEnszErŰeN VÁLtAkoZnAK a kis- és nagybetűk!
    # ○ Pythonban véletlenszámot a random modul (import random) egyik
    # függvényével tudsz létrehozni. Ennek neve: random.randint().
    # ○ Paramétere egy alsó és egy felső határ; random.randint(0, 1) véletlenszerűen 0-t
    # vagy 1-et ad vissza.

# - A visszatérési érték az átalakított szöveg legyen!
import random

def mocking_spongebob():
    text = input("Írjon be egy szöveget: ")
    text = text.lower()
    text = list(text)
    
    for i in range(len(text)):
        if random.randint(0, 1) == 0:
            text[i] = text[i].upper()
    text = "".join(text)
    return text

#result = mocking_spongebob()
#print(result)


# 2) Leghosszabb szó (6 pont)
# - Írj egy leghosszabb_szo nevű függvényt, amely egy szöveget kap paraméterül (a szöveg szóközzel elválasztott szavakat tartalmaz)!
# - A függvény térjen vissza a szövegben található leghosszabb szóval!
# - Amennyiben több szó is ugyanolyan hosszú, akkor az algoritmus a szövegben legkorábban előforduló szót adja vissza!
# - Ha egy írásjel szerepel a szövegben, akkor az számítson bele a mellette lévő szó hosszába!

def leghosszabb_szo(text):
    text = text.split()
    the_longest = ""
    for i in range(len(text)):
        if len(text[i]) > len(the_longest):
            the_longest = text[i]
    return the_longest

#result = leghosszabb_szo('Szia uram! Jeles zárthelyi dolgozat érdekel?')
#print(result)


# 3) Borospince (30 pont)

class Bor:
    def __init__(self, fajta, evjarat, alkoholtartalom = 12.5):
        self.__fajta = fajta
        self.__evjarat = evjarat
        if alkoholtartalom >= 0 and alkoholtartalom <= 100:
            self.__alkoholtartalom = alkoholtartalom
        else:
            raise Exception("Nem megfelelo alkoholtartalom!")

    @property
    def fajta(self):
        return self.__fajta

    @fajta.setter
    def fajta(self, fajta):
        self.__fajta = fajta

    @property
    def evjarat(self):
        return self.__evjarat

    @evjarat.setter
    def evjarat(self, evjarat):
        self.__evjarat = evjarat

    @property
    def alkoholtartalom(self):
        return self.__alkoholtartalom

    @alkoholtartalom.setter
    def alkoholtartalom(self, alkoholtartalom):
        if alkoholtartalom >= 0 and alkoholtartalom <= 100:
            self.__alkoholtartalom = alkoholtartalom
        else:
            print("Nem megfelelo alkoholtartalom!")

    def __str__(self):
        return f"{self.__fajta} (evjarat: {self.__evjarat}), melynek alkoholtartalma: {self.__alkoholtartalom}%"
    

class Szekreny:
    def __init__(self):
        self.borok = []

    def get_bor(self, n):
        if n < 0 or n >= len(self.borok):
            print("Nem letezo index!")
        else:
            return self.borok[n]

    def atlag_alkoholtartalom(self):
        if len(self.borok) == 0:
            print("Ures a szekreny!")
        else:
            osszeg = 0
            for bor in self.borok:
                osszeg += bor.alkoholtartalom
            return osszeg / len(self.borok)

    def statisztika(self):
        stat = {}
        for bor in self.borok:
            if bor.fajta in stat:
                stat[bor.fajta] += 1
            else:
                stat[bor.fajta] = 1
        return stat

    def megisszak(self, bor):
        if not isinstance(bor, Bor):
            print("Nem bor!")
        elif bor not in self.borok:
            print("A Bor nem talalhato!")
        else:
            self.borok.remove(bor)

    def __str__(self):
        if len(self.borok) == 0:
            return "Ez egy ures szekreny."
        else:
            stat = Szekreny.statisztika()
            items = 0
            result = ""
            for fajta, db in stat.items():
                if items != len(stat) -1:
                    result += f"{db} {fajta}, "
                    items += 1
                else:
                    result += f"{db} {fajta}"
                    
            return result
        