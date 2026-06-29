function PaidAmountOnKeyUp(PM_BFAmount) {
    var PaidAmt = $('#PM_PaidAmt').val();
    var dueAmt = PM_BFAmount - PaidAmt;
    if (dueAmt<0) {
        toastr.error("Paid Amount Gretter Than Benificary Amount");
        $('#PM_DueAmt').val(0);
    }
    else {
        $('#PM_DueAmt').val(dueAmt);
    }
}
function DeleteBeneficery(PM_PartyId) {
    var r = confirm("Are You Sure Want To Delete..!");
    if (r == true) {
        var _data = JSON.stringify({
            PM_PartyId: PM_PartyId
        });
        $.ajax({
            url: '/JQuery/DeleteBeneficery',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Master/BeneficiaryConnectionList")
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
    $('#PM_PartyCode').val("");
    $('#PM_RegDate').val("");
    $('#PM_PartyName').val("");
    $('#PM_FHName').val("");
    $('#AM_AreaID').val("");
    $('#PM_ParaId').val("");
    $('#PM_Address').val("");
    $('#PM_City').val("");
    $('#PM_PhoneNo').val("");
    $('#PM_MobNo').val("");
    $('#PM_PIN').val("");
    $('#CM_ID').val("");
    $('#PM_PaidAmt').val("");
    $('#puser').val("");
    $('#AjaxLoader').hide();
}
function BtnSaveUpdate() {
    if (ValidateOperation() == true) {
        $('#AjaxLoader').show();
        var _data = JSON.stringify({
            PM_PartyId: $('#PM_PartyId').val(),
            PM_PartyCode: $('#PM_PartyCode').val(),
            PM_RegDate: $('#PM_RegDate').val(),
            PM_PartyName: $('#PM_PartyName').val(),
            PM_FHName: $('#PM_FHName').val(),
            AM_AreaID: $('#AM_AreaID').val(),
            PM_ParaId: $('#PM_ParaId').val(),
            PM_Address: $('#PM_Address').val(),
            PM_City: $('#PM_City').val(),
            PM_PIN: $('#PM_PIN').val(),
            PM_PhoneNo: $('#PM_PhoneNo').val(),
            PM_MobNo: $('#PM_MobNo').val(),
            PM_BFAmount: $('#PM_BFAmount').val(),
            PM_PaidAmt: $('#PM_PaidAmt').val(),
            PM_DueAmt: $('#PM_DueAmt').val(),
        });
        $('#AjaxLoader').hide();
        $.ajax({
            url: '/JQuery/InsUpBeneficeryConnection',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    if (data.Result == 10) {
                        toastr.success(data.Message);
                        window.location.href = '/Master/BeneficiaryConnection';
                    }
                    else if (data.Result == -10) {
                        toastr.success(data.Message);
                        window.location.href = '/Master/BeneficiaryConnection';

                    }
                    else if (data.Result == -100) {
                        toastr.success(data.Message);
                        window.location.href = '/Master/BeneficiaryConnection';

                    }
                    else {
                        Refresh();
                        toastr.success(data.Message);
                        var win = window.open('/Report/BeneficaryConnection/' + data.Result + '', '_blank');
                        if (win) {
                            //Browser has allowed it to be opened
                            win.focus();
                            //window.print();
                        } else {
                            //Browser has blocked it
                            alert('Please allow popups for this website');
                        }

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
function ValidateOperation() {
    if ($('#PM_PartyCode').val()=="") {
        toastr.error("Please Enter Consumer Id.......!");
        return false;
    }
    else if ($('#PM_RegDate').val() == "") {
        toastr.error("Please Reg Date.......!");
        return false;
    }
    else if ($('#PM_PartyName').val() == "") {
        toastr.error("Please Enter Party Name.......!");
        return false;
    }
    else {
        return true;
    }
}
function Refresh() {
    window.location.reload();
}
function fetchData() {
    var fromdate = $('#fromdate').val();
    var todate = $('#todate').val();
    var PM_PartyCode = $('#PM_PartyCode').val();
    var url = window.location.origin;
    window.location.href = url + "/Master/BeneficiaryConnectionList?fromdate=" + fromdate + "&todate=" + todate + "&PM_PartyCode=" + PM_PartyCode;
    return false;
}
window.onload = function () {
    $("#PL_PartyCode").focus()
};