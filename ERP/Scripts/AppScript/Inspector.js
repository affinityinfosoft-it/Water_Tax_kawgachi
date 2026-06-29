function DeleteInspector(Ins_Id) {
    var r = confirm("Are You Sure Want To Delete..!");
    if (r == true) {
        var _data = JSON.stringify({
            Ins_Id: Ins_Id
        });
        $.ajax({
            url: '/JQuery/DeleteInspector',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Master/InspectorList")
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
            Ins_Id: $('#Ins_Id').val(),
            ins_code: $('#ins_code').val(),
            ins_name: $('#ins_name').val(),
        });
        $('#AjaxLoader').hide();

        $.ajax({
            url: '/JQuery/InsUpInspector',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Master/InspectorList")
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