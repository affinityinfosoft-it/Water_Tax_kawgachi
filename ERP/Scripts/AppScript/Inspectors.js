function DeleteInspectors(ins_code) {
    var r = confirm("Are You Sure Want To Delete..!");
    if (r == true) {
        var _data = JSON.stringify({
            ins_code: ins_code
        });
        $.ajax({
            url: '/JQuery/DeleteInspectors',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    $('#AjaxLoader').hide();
                    toastr.success(data.Message);
                    window.location.href = '/Master/InspectorsList';
                    //setTimeout(function () {

                    //}, 2000);
                }
                else {
                    toastr.error(data.Message);
                }
            },
            error: function () {
                toastr.error('Something wrong happened.');
            }

        });
    }

}
function Refresh() {
    $('#AjaxLoader').show();

    $('#ins_code').val("");
    $('#ins_name').val("");
    $('#AjaxLoader').hide();

}
function BtnSaveUpdate() {
    if (ValidateOperation() == true) {

        $('#AjaxLoader').show();
        var _data = JSON.stringify({
            ins_code: $('#ins_code').val(),
            ins_name: $('#ins_name').val(),
        });
        $('#AjaxLoader').hide();

        $.ajax({
            url: '/JQuery/InsUpInspectors',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    toastr.success(data.Message);
                    if (data.Result == 100) {
                        //Refresh();
                        toastr.success(data.Message);
                        window.location.href = '/Master/InspectorsList';
                    }
                    if (data.Result == 10) {
                        window.location.href = '/Master/InspectorsList';

                    }
                    //setTimeout(function () {

                    //}, 2000);
                }
                else {
                    toastr.error(data.Message);
                }
            },
            error: function () {
                toastr.error('Something wrong happened.');
            }

        });
    }
}
function ValidateOperation() {
    return true;
}