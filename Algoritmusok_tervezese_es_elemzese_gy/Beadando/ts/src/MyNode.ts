class MyNode<T>
{
    private value: T;
    private next: MyNode<T>;

    constructor(value:T = null, next:MyNode<T> = null)
    {
        this.value = value;
        this.next = next;
    }

    public getValue():T
    {
        return this.value;
    }
    public setValue(value: T):void
    {
        this.value = value;
    }

    public getNext():MyNode<T>
    {
        return this.next;
    }
    public setNext(next:MyNode<T>):void
    {
        this.next = next;
    }
}

export default MyNode;