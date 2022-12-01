"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const MyNode_1 = require("./MyNode");
class SimpleLinkedList {
    static NULL = null;
    head;
    constructor() {
        this.Init();
    }
    Init() {
        this.head = SimpleLinkedList.NULL;
    }
    InsertAtFront(data) {
        let p = new MyNode_1.default(data);
        p.setNext(this.head);
        this.head = p;
    }
    InsertAtEnd(data) {
        if (this.head == SimpleLinkedList.NULL) {
            this.InsertAtFront(data);
        }
        else {
            let p = this.head;
            while (p.getNext() !== SimpleLinkedList.NULL) {
                p = p.getNext();
                // ListaBaElemUtán (P , Adat ) ;
            }
            this.InsertAfterElement(p.getValue(), data);
        }
    }
    InsertAfterElement(beforeThis, data) {
        let BeforeThis = this.Find(beforeThis);
        if (BeforeThis !== SimpleLinkedList.NULL) {
            let p = new MyNode_1.default(data);
            p.setNext(BeforeThis.getNext());
            BeforeThis.setNext(p);
            return true;
        }
        return false;
    }
    Find(value) {
        let p = this.head;
        while (p !== SimpleLinkedList.NULL && p.getValue() !== value) {
            p = p.getNext();
        }
        if (p !== SimpleLinkedList.NULL) {
            return p;
        }
        else
            return false;
    }
    DeleteFirst() {
        if (this.head !== SimpleLinkedList.NULL) {
            let p = this.head;
            this.head = this.head.getNext();
            return true;
        }
        else
            return false;
    }
    DeleteLast() {
        if (this.head !== SimpleLinkedList.NULL) {
            if (this.head.getNext() === SimpleLinkedList.NULL) {
                return this.DeleteFirst();
            }
            else {
                let pe = this.head;
                let p = this.head.getNext();
                while (p.getNext() !== SimpleLinkedList.NULL) {
                    pe = p;
                    p = p.getNext();
                }
                return this.DeleteBeforeElement(pe.getValue());
            }
        }
        else {
            return false;
        }
    }
    DeleteBeforeElement(beforeThis) {
        let BeforeThis = this.Find(beforeThis);
        if (BeforeThis !== SimpleLinkedList.NULL) {
            let p = BeforeThis.getNext();
            BeforeThis.setNext(p.getNext());
            return true;
        }
        else
            return false;
    }
}
exports.default = SimpleLinkedList;
