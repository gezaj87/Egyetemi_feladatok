"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const MyNode_1 = require("./MyNode");
class SentinelList {
    head;
    tail;
    constructor() {
        this.head = new MyNode_1.default();
        this.tail = new MyNode_1.default();
        this.Init();
    }
    Init() {
        this.tail = this.head;
    }
    Find(data) {
        let p;
        this.tail.setValue(data);
        p = this.head;
        while (p.getValue() !== data) {
            p = p.getNext();
        }
        if (p == this.tail)
            return p;
        else
            return p;
    }
    InsertBeforeElement(data, beforeThis = this.tail) {
        let BeforeThis = null;
        if (beforeThis !== this.tail) {
            BeforeThis = this.Find(beforeThis);
        }
        else
            BeforeThis = this.tail;
        // console.log(BeforeThis);return;
        let p = new MyNode_1.default(BeforeThis.getValue(), BeforeThis.getNext());
        BeforeThis.setValue(data);
        BeforeThis.setNext(p);
        if (this.tail == BeforeThis) {
            this.tail = p;
        }
    }
    Delete(deleteThis) {
        let findElement = this.Find(deleteThis);
        if (findElement && findElement !== this.tail) {
            let DeleteThis = findElement;
            let p = DeleteThis.getNext();
            DeleteThis.setValue(p.getValue());
            DeleteThis.setNext(p.getNext());
            if (this.tail == p) {
                this.tail = DeleteThis;
            }
        }
    }
}
exports.default = SentinelList;
