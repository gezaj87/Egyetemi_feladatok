"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class MyNode {
    value;
    next;
    constructor(value = null, next = null) {
        this.value = value;
        this.next = next;
    }
    getValue() {
        return this.value;
    }
    setValue(value) {
        this.value = value;
    }
    getNext() {
        return this.next;
    }
    setNext(next) {
        this.next = next;
    }
}
exports.default = MyNode;
