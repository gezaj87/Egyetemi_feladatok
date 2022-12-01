import SimpleLinkedList from "./SimpleLinkedList";
import SentinelList from "./SentinelList";
// import MyNode from "./MyNode";

class Program
{
    public static Main(): void
    {
        const randomNumbers: number[] = this.ListOfRandomNumbers(10, 1, 100);

        let myLinkedList = new SimpleLinkedList();
        // for (let index = 0; index <= 10; index++) {
        //     myLinkedList.InsertAtEnd(index);
            
        // }
        // this.PrintLinkedList(myLinkedList.head);

        const sentinelList = new SentinelList();
        sentinelList.InsertBeforeElement('A');
        sentinelList.InsertBeforeElement('B');
        sentinelList.InsertBeforeElement('C');

        
        sentinelList.InsertBeforeElement('X', 'B');
        sentinelList.Delete('C');
        this.PrintSentinelList(sentinelList);

        // console.log(sentinelList.Find('B'));
        
    }

    static PrintLinkedList(head):void
    {
        let current = head;

        while(current != null)
        {
            console.log(current.value);
            current = current.next;
        }
    }

    static PrintSentinelList(list: SentinelList<any>):void
    {
        let p = list.head;

        while(p != list.tail)
        {
            console.log(p.getValue());
            // console.log(p);
            p = p.getNext();
        }

    }

    static ListOfRandomNumbers(items: number, min:number, max:number):number[]
    {
        const arr: number[] = new Array(items);

        for (let i:number = 0; i < items; i++)
        {
            arr[i] = (Math.floor(Math.random() * (max - min) + min));
        }

        return arr;
    }


}

console.clear();
Program.Main();

//npx ts-node bead.ts