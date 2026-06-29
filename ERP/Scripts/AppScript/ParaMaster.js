function DeletePara(PM_ParaId) {
    var r = confirm("Are You Sure Want To Delete..!");
    if (r == true) {
        var _data = JSON.stringify({
            PM_ParaId: PM_ParaId
        });
        $.ajax({
            url: '/JQuery/DeletePara',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Master/ParaList")
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
    $('#AM_AreaCode').val("");

    $('#PM_ParaCode').val("");
    $('#PM_ParaName').val("");
    $('#AjaxLoader').hide();

}
function BtnSaveUpdate() {
    if (ValidateOperation() == true) {

        $('#AjaxLoader').show();
        var _data = JSON.stringify({
            PM_ParaId: $('#PM_ParaId').val(),

            PM_ParaCode: $('#PM_ParaCode').val(),
            AM_AreaCode: $('#AM_AreaID').val(),
            PM_ParaName: $('#PM_ParaName').val(),
        });
        $('#AjaxLoader').hide();

        $.ajax({
            url: '/JQuery/InsUpPara',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Master/ParaList")
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