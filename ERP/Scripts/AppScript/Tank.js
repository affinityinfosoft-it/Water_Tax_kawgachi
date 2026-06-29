function DeleteTank(TM_ID) {
    var r = confirm("Are You Sure Want To Delete..!");
    if (r == true) {
        var _data = JSON.stringify({
            TM_ID: TM_ID
        });
        $.ajax({
            url: '/JQuery/DeleteTank',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Master/TankList")
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
    $('#TM_TankCode').val("");
    $('#TM_TankName').val("");
    $('#TM_Lt').val("");
    $('#TM_Rate').val("");
    $('#AjaxLoader').hide();

}
function BtnSaveUpdate() {
    if (ValidateOperation() == true) {
        $('#AjaxLoader').show();
        var _data = JSON.stringify({
            TM_ID: $('#TM_ID').val(),
            TM_TankCode: $('#TM_TankCode').val(),
            TM_TankName: $('#TM_TankName').val(),
            TM_Lt: $('#TM_Lt').val(),
            TM_Rate: $('#TM_Rate').val()
        });
        $.ajax({
            url: '/JQuery/InsUpTank',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Master/TankList")
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
    if ($('#TM_TankCode').val() == "") {
        toastr.error('Please enter tank code.');
        return false;
    }
    else if ($('#TM_TankName').val() == "") {
        toastr.error('Please enter tank Name.');
        return false;
    }
    else if ($('#TM_Lt').val() == "Select") {
        toastr.error('Please enter Tank Ltr.');
        return false;
    }
    else if ($('#TM_Rate').val() == "") {
        toastr.error('Please enter Tank Rate.');
        return false;
    }


    return true;
}
