import MyNode from "./MyNode";

class SentinelList<T>
{
    public head: MyNode<T>;
    public tail: MyNode<T>;

    public constructor()
    {
        this.head = new MyNode<T>();
        this.tail = new MyNode<T>();
        this.Init();
    }

    private Init():void
    {
        this.tail = this.head;
    }

    public Find(data: any):any
    {
        let p: MyNode<T>;

        this.tail.setValue(data);
        p = this.head;

        while (p.getValue() !== data)
        {
            p = p.getNext();
        }

        if (p == this.tail) return p;
        else return p;

    }

    public InsertBeforeElement(data: any, beforeThis: any = this.tail):void
    {
        let BeforeThis: MyNode<T> = null;
        if (beforeThis !== this.tail)
        {   
            BeforeThis = this.Find(beforeThis);

        }
        else BeforeThis = this.tail;
        
        // console.log(BeforeThis);return;

        let p = new MyNode<T>(BeforeThis.getValue(), BeforeThis.getNext());
        BeforeThis.setValue(data);
        BeforeThis.setNext(p);

        if (this.tail == BeforeThis)
        {
            this.tail = p;
            
        }

    }

    public Delete(deleteThis: any):void
    {
        let findElement = this.Find(deleteThis);
        if (findElement && findElement !== this.tail)
        {
            let DeleteThis = findElement as MyNode<T>;
            let p = DeleteThis.getNext();
            DeleteThis.setValue(p.getValue());
            DeleteThis.setNext(p.getNext());

            if (this.tail == p)
            {
                this.tail = DeleteThis;
            }
        }
    }
}

export default SentinelList;