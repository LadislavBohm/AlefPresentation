/// <reference path="../../typings/bootstrap-notify/bootstrap-notify.d.ts" />
var NotificationHelper = (function () {
    function NotificationHelper() {
    }
    NotificationHelper.prototype.showSuccess = function (text) {
        $.notify({
            message: text
        }, {
            type: 'success'
        });
    };
    NotificationHelper.prototype.showFailure = function (text) {
        $.notify({
            message: text
        }, {
            type: 'danger'
        });
    };
    NotificationHelper.prototype.showDefault = function (text) {
        $.notify({
            message: text
        });
    };
    return NotificationHelper;
}());
//# sourceMappingURL=General.js.map