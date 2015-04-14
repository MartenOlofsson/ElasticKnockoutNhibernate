/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/jquery/jquery.d.ts" />

class Owner {
    Name = ko.observable<String>();
    Age = ko.observable<Number>();
}

class OwnersViewModel {
    //this should be an object of type owner?
    Name = ko.observable<String>();
    Age = ko.observable<Number>();
    searchterm = ko.observable<String>();
    searchjson = ko.observable();

    owners = ko.observableArray<Owner>([]);
    addingNew = ko.observable<Boolean>(false);
    save() {
        this.saveUser();
    }

    search() {
        $.ajax({
            url: "/search/" + this.searchterm(),
        }).done((data) => {
            this.searchjson(JSON.stringify(data, null, 2));
        }).fail((ex) => {
            console.log(ex);
        });;
    }

    toggleAddNew() {
        this.addingNew(!this.addingNew());
    }

    saveUser() {
        var json = ko.toJSON(this);
        this.owners.push(this);
        $.ajax({
            url: "/owners/save",
            method: 'POST',
            contentType: "application/json",
            data: json
        }).done((data) => {
            console.log(data);
        }).fail((ex) => {
            
        });
    }

    load() {
        $.ajax({
            url: "/owners"
        }).done((data) => {
                debugger;
            var ownersFromServer = ko.toJS(data);
            for (var i = 0; i < ownersFromServer.length; i++) {
                this.owners.push(ownersFromServer[i]);
            }
        })
            .fail((ex) => {
                debugger;
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