"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const SimpleLinkedList_1 = require("./SimpleLinkedList");
const SentinelList_1 = require("./SentinelList");
// import MyNode from "./MyNode";
class Program {
    static Main() {
        const randomNumbers = this.ListOfRandomNumbers(10, 1, 100);
        let myLinkedList = new SimpleLinkedList_1.default();
        // for (let index = 0; index <= 10; index++) {
        //     myLinkedList.InsertAtEnd(index);
        // }
        // this.PrintLinkedList(myLinkedList.head);
        const sentinelList = new SentinelList_1.default();
        sentinelList.InsertBeforeElement('A');
        sentinelList.InsertBeforeElement('B');
        sentinelList.InsertBeforeElement('C');
        sentinelList.InsertBeforeElement('X', 'B');
        sentinelList.Delete('C');
        this.PrintSentinelList(sentinelList);
        // console.log(sentinelList)
        // console.log(sentinelList.Find('B'));
    }
    static PrintLinkedList(head) {
        let current = head;
        while (current != null) {
            console.log(current.value);
            current = current.next;
        }
    }
    static PrintSentinelList(list) {
        let p = list.head;
        while (p != list.tail) {
            console.log(p.getValue());
            // console.log(p);
            p = p.getNext();
        }
    }
    static ListOfRandomNumbers(items, min, max) {
        const arr = new Array(items);
        for (let i = 0; i < items; i++) {
            arr[i] = (Math.floor(Math.random() * (max - min) + min));
        }
        return arr;
    }
}
console.clear();
Program.Main();
//npx ts-node bead.ts
