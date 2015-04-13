/// <reference path="../typings/knockout/knockout.d.ts" />

class Owner {
    name = ko.observable();

    save() {
        alert(this.name());
    }
}

var vm = new Owner();
ko.applyBindings(vm);