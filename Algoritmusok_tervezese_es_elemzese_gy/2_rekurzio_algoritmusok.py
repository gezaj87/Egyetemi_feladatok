from array import array

# 1) Binary search
def BinarySearch(V: array, T: int, low: int, high: int) -> int:
    if low > high:
        return -1;
    else:
        mid = (low + high) // 2; 
        if T == V[mid]:
            return mid;
       
        elif T > V[mid]:
            return BinarySearch(V, T, mid + 1, high)
        
        else:                        
            return BinarySearch(V, T, low, mid - 1)    

vektor = [1,2,3,4,5,6,7,8];    
eredmeny = BinarySearch(vektor, 6, 0, len(vektor)-1)
#print(eredmeny)


# 2) Pascal's Triangle
def pascal_triangle(n):
    if n == 1:
        return [[1]]
    else:
        new_row = [1]
        result = pascal_triangle(n - 1)
        last_row = result[-1]
        for i in range(len(last_row) - 1):
            new_row.append(last_row[i] + last_row[i + 1])
        new_row += [1]
        result.append(new_row)
    return result

print(pascal_triangle(5))
# pascal_triangle(5)


# 3) Tower of Hanoi
def TowerOfHanoi(n, from_rod, to_rod, helper_rod):
    if n == 0:
        return
    TowerOfHanoi(n-1, from_rod, helper_rod, to_rod)
    print("Move disk", n, "from rod", from_rod, "to rod", to_rod)
    TowerOfHanoi(n-1, helper_rod, to_rod, from_rod)

TowerOfHanoi(3, "A", "C", "B")

    
