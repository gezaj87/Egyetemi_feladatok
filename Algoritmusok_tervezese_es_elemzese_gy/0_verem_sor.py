class Stack:
    def __init__(self):
        self.items = []

    def is_empty(self):
        return len(self.items) == 0

    def push(self, item):
        self.items.append(item)

    def peek(self):
        if self.is_empty():
            return None

        return self.items[-1]

    def pop(self):
        if self.is_empty():
            return None

        return self.items.pop()
    
    def length(self):
        return len(self.items);
    
    
class Queue:
    def __init__(self):
        self.items = []

    def is_empty(self):
        return len(self.items) == 0

    def enqueue(self, item):
        self.items.append(item)

    def peek(self):
        if self.is_empty():
            return None

        return self.items[0]

    def dequeue(self):
        if self.is_empty():
            return None

        return self.items.pop(0)
    
    def length(self):
        return len(self.items);
    
# 1) Adott a 3, 6, 10, 8, 1, 9, 7 számsorozat, melyeket ebben a sorrendben tárolunk el. Adjuk meg milyen sorrendben vehetjük ki az elemeket verem és sor adatszerkezet esetén!

verem = Stack();
verem.push(3); verem.push(6); verem.push(10); verem.push(8); verem.push(1); verem.push(9); verem.push(7);

# for i in range(verem.length()):
#     print(verem.pop(), end=', ');

# print();    
sor = Queue();
sor.enqueue(3); sor.enqueue(6); sor.enqueue(10); sor.enqueue(8); sor.enqueue(1); sor.enqueue(9); sor.enqueue(7);

for j in range(sor.length()):
    print(sor.dequeue(), end=', ');

print('')
    
    
    
    
print('from collections import deque....');
from collections import deque;
ketvegu_sor = deque([3,6,10,8,1,9,7]);

for k in range(len(ketvegu_sor)):
    print(ketvegu_sor.popleft(), end=', ');
    

print();
print('f2) feladat')  
# f2) Az alábbi sorozatban lévő betűk esetén alkalmazzon push(letter) műveletet, míg a csillagok esetén alkalmazzon pop() műveletet. Ezután adja meg a pop() műveletek által visszaadott értékek sorozatát, amikor a push(letter) és pop() műveleteket egy kezdetben üres LIFO vermen hajtuk végre: E A S * Y * Q U E * * * S T * * * I O * N * * *

# Az alábbi sorozatban lévő betűk esetén alkalmazzon enqueue(letter) műveletet, míg a csillagok esetén alkalmazzon dequeue() műveletet. Ezután adja meg a dequeue()műveletek által visszaadott értékek sorozatát, amikor az enqueue(letter) és dequeue() műveleteket egy kezdetben üres FIFO soron hajtuk végre:


def f2(string):
    verem = Stack();
    sss = string.split(' ');
    
        
    for i in range(len(sss)):
        if (sss[i] == '*'):
            print(verem.pop(), end='');
        else:
            verem.push(sss[i]);
        


f2('E A S * Y * Q U E * * * S T * * * I O * N * * *');
print();
def f2_2(string):
    sor = Queue();
    sss = string.split(' ');
    
    for i in range(len(sss)):
        if sss[i] == '*':
            print(sor.dequeue(), end='');
        else:
            sor.enqueue(sss[i]);
            
f2_2('E A S * Y * Q U E * * * S T * * * I O * N * * *')