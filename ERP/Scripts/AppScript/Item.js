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

        var IM_Type = $('#IM_Type').val(); 
        var _data = JSON.stringify({
            IM_ID: $('#IM_ID').val(),
            IM_ItemCode: $('#IM_ItemCode').val(),
            IM_ItemName: $('#IM_ItemName').val(),
            ItemGroupID: $('#ItemGroupID').val(),
            ItemSubGroupID: $('#ItemSubGroupID').val(),
            IM_ItemDescription: $('#IM_ItemDescription').val(),
            IM_GST: $('#IM_GST').val(),
            IM_StockInHand: $('#IM_StockInHand').val(),
            IM_Unit: $('#IM_Unit').val(),
            IM_Rate: $('#IM_Rate').val(),
            //CreatedDate: $('#CreatedDate').val(),
            IM_Type: IM_Type
        });

        $.ajax({
            url: '/JQuery/InsUpItem',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8', 
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Master/ItemList");
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
    else if ( $('#IM_Type').val() == "") {
        toastr.error('Please select item type.');
        return false;
    }
    
    return true;
}

$(document).ready(function () {
    $('#ItemGroupID').change(function () {
        var itemGroupId = $(this).val();
        if (itemGroupId) {
            $.ajax({
                url: '/Master/GetItemSubGroups',
                type: 'GET',
                data: { itemGroupId: itemGroupId },
                success: function (data) {
                    var subGroupDropdown = $('#ItemSubGroupID');
                    subGroupDropdown.empty();
                    subGroupDropdown.append($('<option>', {
                        value: '',
                        text: 'Select SubGroup'
                    }));
                    $.each(data, function (i, subgroup) {
                        subGroupDropdown.append($('<option>', {
                            value: subgroup.Value,
                            text: subgroup.Text
                        }));
                    });
                }
            });
        }
    });
});



