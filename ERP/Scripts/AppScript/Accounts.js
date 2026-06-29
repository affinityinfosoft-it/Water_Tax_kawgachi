function DeleteItem(IM_ID) {
    var r = confirm("Are You Sure Want To Delete..!");
    if (r == true) {
        var _data = JSON.stringify({
            IM_ID: IM_ID
        });
        $.ajax({
            url: '/JQuery/DeleteItem',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Master/ItemList")
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
    $('#IM_ItemCode').val("");
    $('#IM_ItemName').val("");
    $('#IM_Unit').val("");
    $('#IM_Rate').val("");
    $('#IM_Type').val("");
    $('#AjaxLoader').hide();

}
function BtnSaveUpdate() {
    if (ValidateOperation() == true) {
        $('#AjaxLoader').show();
        var _data = JSON.stringify({
            AM_AccountId: $('#AM_AccountId').val(),
            AM_AccountCode: $('#AM_AccountCode').val(),
            AM_AccDescription: $('#AM_AccDescription').val(),
            AM_LongName: $('#AM_LongName').val(),
            AM_GroupCode: $('#GM_GroupCode').val(),
            ISSubAc: $('#ISSubAc').val(),
            AM_IsFund: $('#AM_IsFund').val(),
            IsFeesHead: $('#IsFeesHead').val(),
         
        });

        $.ajax({
            url: '/JQuery/InsUpAccount',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Accounts/AccountMasterList");
                } else {
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
    if ($('#IM_ItemCode').val() == "") {
        toastr.error('Please enter item code.');
        return false;
    }
    else if ($('#IM_ItemName').val() == "") {
        toastr.error('Please enter item Name.');
        return false;
    }
    else if ($('#IM_Unit').val() == "Select") {
        toastr.error('Please enter item Unit.');
        return false;
    }
    else if ($('#IM_Rate').val() == "") {
        toastr.error('Please enter item Rate.');
        return false;
    }
    else if ($('#IM_Type').val() == "") {
        toastr.error('Please select item type.');
        return false;
    }

    return true;
}