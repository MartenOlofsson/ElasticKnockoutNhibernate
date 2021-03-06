﻿/// <reference path="../typings/knockout/knockout.d.ts" />
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
    haspets = ko.computed(function() {
        
    });
    Pets = ko.observableArray([]);
    owners = ko.observableArray<Owner>([]);
    addingNew = ko.observable<Boolean>(false);
    save() {
        this.saveUser();
    }


    search(searchquery : string) {
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
        var json = JSON.stringify({ Name: this.Name(), Pets: this.Pets(), Age: this.Age(), Id: 123 });
        json = ko.toJSON(this);
        this.owners.push(this);
        $.ajax({
            url: "/owners/save",
            method: 'POST',
            contentType: "application/json",
            dataType: 'json',
            data: json,
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