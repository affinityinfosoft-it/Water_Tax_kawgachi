function Refresh() {
    window.location.reload();
}
function BtnSaveUpdate() {
    if (ValidateOperation() == true) {
        $('#AjaxLoader').show();
        var _data = JSON.stringify({
            CM_ID: $('#CM_ID').val(),
            bf_rate: $('#bf_rate').val(),
            fr_rate: $('#fr_rate').val(),
        });
        $.ajax({
            url: '/JQuery/InsUpBeneficaryFixedRate',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Home/Index")
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
    if ($('#bf_rate').val() == "") {
        toastr.error('Please enter Beneficiary Contribution Amount.');
        return false;
    }
    else if ($('#fr_rate').val() == "") {
        toastr.error('Please enter Form Amount.');
        return false;
    }
    return true;
        
}