function DeleteItemSubGroup(ItemSubGroupID) {
    var r = confirm("Are You Sure Want To Delete..!");
    if (r == true) {
        var _data = JSON.stringify({
            ItemSubGroupID: ItemSubGroupID
        });
        $.ajax({
            url: '/JQuery/DeleteItemSubGroup',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Master/ItemSubGroupList")
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
    $('#ItemCode').val("");

    $('#ItemSubCode').val("");
    $('#ItemSubGroupName').val("");
    $('#AjaxLoader').hide();

}
function BtnSaveUpdate() {
    if (ValidateOperation() == true) {

        $('#AjaxLoader').show();
        var _data = JSON.stringify({
            ItemSubGroupID: $('#ItemSubGroupID').val(),
            ItemSubCode: $('#ItemSubCode').val(),
            ItemCode: $('#ItemGroupID').val(),
            ItemSubGroupName: $('#ItemSubGroupName').val(),
            CM_ID: $('#CM_ID').val(),
        });
        $('#AjaxLoader').hide();

        $.ajax({
            url: '/JQuery/InsUpItemSubGroup',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Master/ItemSubGroupList")
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