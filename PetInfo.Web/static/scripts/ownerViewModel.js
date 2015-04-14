/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/jquery/jquery.d.ts" />
var Owner = (function () {
    function Owner() {
        this.Name = ko.observable();
        this.Age = ko.observable();
    }
    return Owner;
})();

var OwnersViewModel = (function () {
    function OwnersViewModel() {
        //this should be an object of type owner?
        this.Name = ko.observable();
        this.Age = ko.observable();
        this.searchterm = ko.observable();
        this.searchjson = ko.observable();
        this.owners = ko.observableArray([]);
        this.addingNew = ko.observable(false);
    }
    OwnersViewModel.prototype.save = function () {
        this.saveUser();
    };

    OwnersViewModel.prototype.search = function () {
        var _this = this;
        $.ajax({
            url: "/search/" + this.searchterm()
        }).done(function (data) {
            _this.searchjson(JSON.stringify(data, null, 2));
        }).fail(function (ex) {
            console.log(ex);
        });
        ;
    };

    OwnersViewModel.prototype.toggleAddNew = function () {
        this.addingNew(!this.addingNew());
    };

    OwnersViewModel.prototype.saveUser = function () {
        var json = ko.toJSON(this);
        this.owners.push(this);
        $.ajax({
            url: "/owners/save",
            method: 'POST',
            contentType: "application/json",
            data: json
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
            debugger;
            var ownersFromServer = ko.toJS(data);
            for (var i = 0; i < ownersFromServer.length; i++) {
                _this.owners.push(ownersFromServer[i]);
            }
        }).fail(function (ex) {
            debugger;
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
