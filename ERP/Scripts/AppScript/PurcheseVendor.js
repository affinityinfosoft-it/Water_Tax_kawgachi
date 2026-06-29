function DeleteVendor(PVM_VendorID) {
    var r = confirm("Are You Sure Want To Delete..!");
    if (r == true) {
        var _data = JSON.stringify({
            PVM_VendorID: PVM_VendorID
        });
        $.ajax({
            url: '/JQuery/DeleteVendor',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    $('#AjaxLoader').hide();
                    toastr.success(data.Message);
                    window.location.href = '/Master/PurchaseVendorList';
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
    $('#PVM_VendorCode').val("");
    $('#PVM_VendorName').val("");
    $('#PVM_Address1').val("");
    $('#PVM_Address2').val("");
    $('#PVM_City').val("");
    $('#PVM_PIN').val("");
    $('#PVM_Phone').val("");
    $('#PVM_FaxNo').val("");
    $('#PVM_Email').val("");
    $('#PVM_SubAccID').val("");
    $('#AjaxLoader').hide();

}
function BtnSaveUpdate() {
    if (ValidateOperation() == true) {

        $('#AjaxLoader').show();
        var _data = JSON.stringify({
            PVM_VendorID: $('#PVM_VendorID').val(),
            PVM_VendorCode:$('#PVM_VendorCode').val(),
            PVM_VendorName:$('#PVM_VendorName').val(),
            PVM_Address1:$('#PVM_Address1').val(),
            PVM_Address2:$('#PVM_Address2').val(),
            PVM_City:$('#PVM_City').val(),
            PVM_PIN:$('#PVM_PIN').val(),
            PVM_Phone:$('#PVM_Phone').val(),
            PVM_FaxNo:$('#PVM_FaxNo').val(),
            PVM_Email:$('#PVM_Email').val(),
            PVM_SubAccID:$('#PVM_SubAccID').val(),
        });
        $('#AjaxLoader').hide();

        $.ajax({
            url: '/JQuery/InsUpPurcheseVendor',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    if (data.Result == 100) {
                        Refresh();
                        toastr.success(data.Message);
                        window.location.href = '/Master/PurchaseVendorList';
                    }
                    if (data.Result == 10) {
                        window.location.href = '/Master/PurchaseVendorList';

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
function ValidateOperation()
{
    if ($('#PVM_VendorCode').val() == "") {
        toastr.error('Please enter Company Name.');
        return false;
    }
    else if ($('#PVM_VendorName').val() == "") {
        toastr.error('Please enterCompany Name.');
        return false;
    }
    return true;
}