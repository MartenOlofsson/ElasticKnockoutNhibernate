/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/jquery/jquery.d.ts" />
var Owner = (function () {
    function Owner() {
        this.name = ko.observable();
        this.age = ko.observable();
    }
    return Owner;
})();

var OwnersViewModel = (function () {
    function OwnersViewModel() {
        //this should be an object of type owner?
        this.name = ko.observable();
        this.age = ko.observable();
        this.owners = ko.observableArray([]);
        this.addingNew = ko.observable(false);
    }
    OwnersViewModel.prototype.save = function () {
        this.saveUser();
    };

    OwnersViewModel.prototype.toggleAddNew = function () {
        this.addingNew(!this.addingNew());
    };

    OwnersViewModel.prototype.saveUser = function () {
        var owner = new Owner();
        owner.name = this.name;
        this.owners.push(owner);
        $.ajax({
            url: "/owners/save",
            method: 'POST',
            type: "application/json"
        }).done(function (data) {
            console.log(data);
        }).fail(function (ex) {
        });
    };

    OwnersViewModel.prototype.load = function () {
        var _this = this;
        $.ajax({
            url: "/owners"
        }).done(function (data) {
            var ownersFromServer = ko.toJS(data);
            for (var i = 0; i < ownersFromServer.length; i++) {
                _this.owners.push(ownersFromServer[i]);
            }
        }).fail(function (ex) {
            console.log(ex);
        });
    };

    OwnersViewModel.prototype.init = function () {
        this.load();
    };
    return OwnersViewModel;
})();

var vm = new OwnersViewModel();
vm.init();
ko.applyBindings(vm);
//# sourceMappingURL=ownerViewModel.js.map
