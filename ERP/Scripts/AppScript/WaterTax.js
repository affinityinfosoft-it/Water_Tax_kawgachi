function Refresh() {
    window.location.reload();
}
function BtnSaveUpdate() {
    if (ValidateOperation() == true) {
        $('#AjaxLoader').show();
        var _data = JSON.stringify({
            CM_ID: $('#CM_ID').val(),
            f_date: $('#f_date').val(),
            f_amt: $('#f_amt').val(),
            s_date: $('#s_date').val(),
            s_amt: $('#s_amt').val(),
            t_date: $('#t_date').val(),
            t_amt: $('#t_amt').val()
        });
        $.ajax({
            url: '/JQuery/InsUpWaterTaxSlab',
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
                    $('#AjaxLoader').hide();
                    toastr.error(data.Message);
                }
            },
            error: function () {
                $('#AjaxLoader').hide();
                toastr.error('Something wrong happened.');
            }

        });
    }
}
function ValidateOperation() {
    if ($('#f_date').val() == "") {
        toastr.error('Please enter First Slab Day.');
        return false;
    }
    else if ($('#f_amt').val() == "") {
        toastr.error('Please enter Tax Amount Rate.');
        return false;
    }
     else if ($('#s_date').val() == "") {
        toastr.error('Please enter Second Slab Day.');
        return false;
    }
     else if  ($('#s_amt').val() == "") {
        toastr.error('Please enter Tax Amount Rate.');
        return false;
    }
     else if ($('#t_date').val() == "") {
        toastr.error('Please enter Thired Slab Day.');
        return false;
    }
     else if ($('#t_amt').val() == "") {
        toastr.error('Please enter Tax Amount Rate.');
        return false;
    }
    return true;
}