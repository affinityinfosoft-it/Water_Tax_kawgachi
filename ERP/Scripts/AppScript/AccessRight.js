function MenuOnclickAccess(MenuId) {
    var obj = {
        RoleId: $('#RoleId').val(),
        MenuId: MenuId
    }
    $.ajax({
        type: "POST",
        //url: "../handlers/DisplaySpecificOrderDetail.ashx?OrderId=" + Orderid, //+ "tk=" + Date.toLocaleTimeString()
        url: "/Admin/AccessRightMenuList",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: "true",
        cache: "false",
        success: function (data) {
            var html = '';
            $('#AccessRightTable tbody').empty();
            $.each(data, function (key, item) {
                html += '<tr>';
                html += '<td hidden>' + item.MenuId + '</td>';
                html += '<td style="color:blue;">' + item.MenuName + '</td>';
                if (item.CanView == true) {
                    html += '<td><input type="checkbox" class="form-check-input" name="CanView" id="CanView' + item.MenuId + '" value="' + item.CanView + '" checked></td>';
                }
                else {
                    html += '<td><input type="checkbox" class="form-check-input" name="CanView" id="CanView' + item.MenuId + '" value="' + item.CanView + '"></td>';
                }
                if (item.CanAdd == true) {
                    html += '<td><input type="checkbox" class="form-check-input" name="CanAdd" id="CanAdd' + item.MenuId + '" value="' + item.CanAdd + '" checked></td>';
                }
                else {
                    html += '<td><input type="checkbox" class="form-check-input" name="CanAdd" id="CanAdd' + item.MenuId + '" value="' + item.CanAdd + '"></td>';
                }
                if (item.CanEdit == true) {
                    html += '<td><input type="checkbox" class="form-check-input" name="CanEdit" id="CanEdit' + item.MenuId + '" value="' + item.CanEdit + '" checked></td>';
                }
                else {
                    html += '<td><input type="checkbox" class="form-check-input" name="CanEdit" id="CanEdit' + item.MenuId + '" value="' + item.CanEdit + '"></td>';
                }
                if (item.CanDelete == true) {
                    html += '<td><input type="checkbox" class="form-check-input" name="CanDelete" id="CanDelete' + item.MenuId + '" value="' + item.CanDelete + '" checked></td>';
                }
                else {
                    html += '<td><input type="checkbox" class="form-check-input" name="CanDelete" id="CanDelete' + item.MenuId + '" value="' + item.CanDelete + '"></td>';
                }
                if (item.CanSubmit == true) {
                    html += '<td><input type="checkbox" class="form-check-input" name="CanSubmit" id="CanSubmit' + item.MenuId + '" value="' + item.CanSubmit + '" checked></td>';
                }
                else {
                    html += '<td><input type="checkbox" class="form-check-input" name="CanSubmit" id="CanSubmit' + item.MenuId + '" value="' + item.CanSubmit + '"></td>';
                }
                if (item.CanUpdate == true) {
                    html += '<td><input type="checkbox" class="form-check-input" name="CanUpdate" id="CanUpdate' + item.MenuId + '" value="' + item.CanUpdate + '" checked></td>';
                }
                else {
                    html += '<td><input type="checkbox" class="form-check-input" name="CanUpdate" id="CanUpdate' + item.MenuId + '" value="' + item.CanUpdate + '"></td>';
                }
                html += '</tr>';
            });
            $('#AccessRightTable tbody').html(html);
        }
    });
}
function InsertUpdateAssignRights() {
    var AccessRightsItem = [];
    //var tblAccess_RightList = document.getElementById('AccessRightTable');
    var rowCount = $('#AccessRightTable >tbody >tr').length;
    for (var i = 0; i < rowCount; i++) {
        var MenuId = $("#AccessRightTable>tbody:eq(0) tr:eq(" + i + ") td:eq(0)").text().trim()
        AccessRightsItem.push(
            {
                CanView: $("#CanView" + MenuId + "").is(":checked"),
                CanAdd: $("#CanAdd" + MenuId + "").is(":checked"),
                CanEdit: $("#CanEdit" + MenuId + "").is(":checked"),
                CanDelete: $("#CanDelete" + MenuId + "").is(":checked"),
                CanSubmit: $("#CanSubmit" + MenuId + "").is(":checked"),
                CanUpdate: $("#CanUpdate" + MenuId + "").is(":checked"),
                MenuId: MenuId
            });
    }
    var _data = JSON.stringify({

        arm: {
            RoleId: $("#RoleId").val(),
            lstAccessRights: AccessRightsItem
        }
    });
    $.ajax({
        url: "/Admin/InsUpAccessRightMenu",
        contentType: "application/json",
        dataType: "json",
        type: "POST",
        data: _data,
        success: function (response) {
            if (response != null && response != undefined && response.IsSuccess == true) {
                //success / error / warning / info
                toastr.success(response.Message)
                window.location.reload();
                //windows.location.href = "/Admin/Message";
            } else {
                toastr.error(response.Message)
            }
        },
        error: function (jqxhr, settings, thrownError) {
            console.log(jqxhr.status + '\n' + thrownError);
        }
    });


}
function CanViewSelectAllOnClick() {
    var checkoutHistory = document.getElementById('CanViewSelect');
    if (checkoutHistory.checked) {
        $('input[name=CanView]').prop('checked', true);

    } else {
        $('input[name=CanView]').attr('checked', false);
    }
}
function CanEditSelectAllOnClick() {
    var checkoutHistory = document.getElementById('CanEditSelect');
    if (checkoutHistory.checked) {
        $('input[name=CanEdit]').prop('checked', true);

    } else {
        $('input[name=CanEdit]').attr('checked', false);
    }
}
function CanDeleteSelectAllOnClick() {
    var checkoutHistory = document.getElementById('CanDeleteSelect');
    if (checkoutHistory.checked) {
        $('input[name=CanDelete]').prop('checked', true);

    } else {
        $('input[name=CanDelete]').attr('checked', false);
    }
}
function CanSubmitSelectAllOnClick() {
    var checkoutHistory = document.getElementById('CanSubmitSelect');
    if (checkoutHistory.checked) {
        $('input[name=CanSubmit]').prop('checked', true);

    } else {
        $('input[name=CanSubmit]').attr('checked', false);
    }
}
function CanUpdateSelectAllOnClick() {
    var checkoutHistory = document.getElementById('CanUpdateSelect');
    if (checkoutHistory.checked) {
        $('input[name=CanUpdate]').prop('checked', true);

    } else {
        $('input[name=CanUpdate]').attr('checked', false);
    }
}
function CanAddSelectAllOnClick() {
    var checkoutHistory = document.getElementById('CanAddSelect');
    if (checkoutHistory.checked) {
        $('input[name=CanAdd]').prop('checked', true);

    } else {
        $('input[name=CanAdd]').attr('checked', false);
    }
}