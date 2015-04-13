/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/jquery/jquery.d.ts" />

class Owner {
    name = ko.observable<String>();
    age = ko.observable<Number>();
}

class OwnersViewModel {
    //this should be an object of type owner?
    name = ko.observable<String>();
    age = ko.observable<Number>();

    owners = ko.observableArray<Owner>([]);
    addingNew = ko.observable(false);
    save() {
        this.saveUser();
    }

    toggleAddNew() {
        this.addingNew(!this.addingNew());
    }

    saveUser() {
        var owner = new Owner();
        owner.name = this.name;
        this.owners.push(owner);
        $.ajax({
            url: "/owners/save",
            method: 'POST',
            type: "application/json"
        }).done((data) => {
            console.log(data);
        }).fail((ex) => {
            
        });
    }

    load() {
        $.ajax({
            url: "/owners"
        }).done((data) => {
            var ownersFromServer = ko.toJS(data);
            for (var i = 0; i < ownersFromServer.length; i++) {
                this.owners.push(ownersFromServer[i]);
            }
        })
        .fail((ex) => {
            console.log(ex);
        });
    }

    init() {
        this.load();
    }
}

var vm = new OwnersViewModel();
vm.init();
ko.applyBindings(vm);