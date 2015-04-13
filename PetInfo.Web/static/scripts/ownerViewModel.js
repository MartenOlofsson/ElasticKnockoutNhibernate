/// <reference path="../typings/knockout/knockout.d.ts" />
var Owner = (function () {
    function Owner() {
        this.name = ko.observable();
    }
    Owner.prototype.save = function () {
        alert(this.name());
    };
    return Owner;
})();

var vm = new Owner();
ko.applyBindings(vm);
//# sourceMappingURL=ownerViewModel.js.map
