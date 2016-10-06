/// <reference path="../../typings/bootstrap-notify/bootstrap-notify.d.ts" />

class NotificationHelper {

    public showSuccess(text: string): void {
        $.notify({
	        message: text 
        },{
	        type: 'success'
        });
    }

    public showFailure(text: string): void {
        $.notify({
	        message: text 
        },{
	        type: 'danger'
        });
    }

    public showDefault(text: string): void {
        $.notify({
	        message: text 
        }); 
    }
}
