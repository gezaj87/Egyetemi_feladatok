import MyNode from "./MyNode";

class SimpleLinkedList<T>
{
    private static readonly NULL: null = null;
    public head: MyNode<T>;

    constructor()
    {
        this.Init();
    }

    private Init():void
    {
        this.head = SimpleLinkedList.NULL;
    }

    public InsertAtFront(data: any)
    {
        let p = new MyNode<any>(data);
        p.setNext(this.head);
        this.head = p;
    }

    public InsertAtEnd(data: any)
    {
        if (this.head == SimpleLinkedList.NULL)
        {
            this.InsertAtFront(data);
        }
        else
        {
            let p = this.head;
            while(p.getNext() !== SimpleLinkedList.NULL)
            {
                p = p.getNext();
                // ListaBaElemUtán (P , Adat ) ;
                
            }
            this.InsertAfterElement(p.getValue(), data);
        }
    }

    public InsertAfterElement(beforeThis: any, data: any):boolean
    {
        let BeforeThis = this.Find(beforeThis) as MyNode<T>;
        if (BeforeThis !== SimpleLinkedList.NULL)
        {
            let p = new MyNode<T>(data);
            p.setNext(BeforeThis.getNext());
            BeforeThis.setNext(p);
            return true;
        }
        return false;
    }

    public Find(value: any):any
    {
        let p = this.head;

        while (p !== SimpleLinkedList.NULL && p.getValue() !== value)
        {
            p = p.getNext();
        }

        if (p !== SimpleLinkedList.NULL)
        {
            return p;
        }
        else return false;
    }

    public DeleteFirst():boolean
    {  
        if (this.head !== SimpleLinkedList.NULL)
        {
            let p = this.head;
            this.head = this.head.getNext();
            return true;
        }
        else return false;
    }

    public DeleteLast():boolean
    {
        if (this.head !== SimpleLinkedList.NULL)
        {
            if (this.head.getNext() === SimpleLinkedList.NULL)
            {
                return this.DeleteFirst();
            }
            else
            {
                let pe = this.head;
                let p = this.head.getNext();
                while (p.getNext() !== SimpleLinkedList.NULL)
                {
                    pe = p;
                    p = p.getNext();
                }
                return this.DeleteBeforeElement(pe.getValue());
            }
        }
        else
        {
            return false;
        }
    }

    public DeleteBeforeElement(beforeThis: any):boolean
    {
        let BeforeThis = this.Find(beforeThis) as MyNode<T>;

        if (BeforeThis !== SimpleLinkedList.NULL)
        {
            let p = BeforeThis.getNext();
            BeforeThis.setNext(p.getNext());
            return true;
        }
        else return false;
    }

}

export default SimpleLinkedList;