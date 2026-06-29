function DeleteVan(VM_ID) {
    var r = confirm("Are You Sure Want To Delete..!");
    if (r == true) {
        var _data = JSON.stringify({
            VM_ID: VM_ID
        });
        $.ajax({
            url: '/JQuery/DeleteVan',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Master/VanList")
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
    $('#VM_VanCode').val("");
    $('#VM_VanName').val("");
    $('#VM_Type').val("");
    $('#VM_Rate').val("");
    $('#AjaxLoader').hide();

}
function BtnSaveUpdate() {
    if (ValidateOperation() == true) {
        $('#AjaxLoader').show();
        var _data = JSON.stringify({
            VM_ID: $('#VM_ID').val(),
            VM_VanCode: $('#VM_VanCode').val(),
            VM_VanName: $('#VM_VanName').val(),
            VM_Type: $('#VM_Type').val(),
            VM_Rate: $('#VM_Rate').val()
        });
        $.ajax({
            url: '/JQuery/InsUpVan',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Master/VanList")
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
    if ($('#VM_VanCode').val() == "") {
        toastr.error('Please enter tank code.');
        return false;
    }
    else if ($('#VM_VanName').val() == "") {
        toastr.error('Please enter tank Name.');
        return false;
    }
    else if ($('#VM_Type').val() == "Select") {
        toastr.error('Please enter Tank Ltr.');
        return false;
    }
    else if ($('#VM_Rate').val() == "") {
        toastr.error('Please enter Tank Rate.');
        return false;
    }


    return true;
}
