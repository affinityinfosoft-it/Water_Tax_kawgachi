$(document).ready(function () {
    //$("#PL_PaidAmount").prop("disabled", true);
    //$("#btnInsUp").prop("disabled", true);
});
function SearchByPartyName() {
    var PM_PartyCode = $('#PM_PartyCode').val();
    FillConsumerDerails(PM_PartyCode);
}
function SearchByPartyCode() {
    var PM_PartyCode = $('#PL_PartyCode').val();
    FillConsumerDerails(PM_PartyCode);
}
function FillConsumerDerails(code) {
    if (Validate() == true) {
        $('#AjaxLoader').show();
        //var TEntity = JSON.stringify({
        //    PM_PartyCode: code
        //});
        $.ajax({
            url: '/JQuery/GetConDtlForFerrul',
            data: { PM_PartyCode: code },
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null || data != undefined) {
                    $('#PL_PartyCode').val(data.ConsumerDetails.PM_PartyCode);
                    $('#PM_PartyCode').val(data.ConsumerDetails.PM_PartyCode);
                    $('#select2-PM_PartyCode-container').html(data.ConsumerDetails.PM_PartyName);
                    $('#PM_FHName').val(data.ConsumerDetails.PM_FHName);
                    $('#AM_AreaName').val(data.ConsumerDetails.AM_AreaName);
                    $('#PM_ParaName').val(data.ConsumerDetails.PM_ParaName);
                    $('#PM_MobNo').val(data.ConsumerDetails.PM_MobNo);

                    $('#Auto_No').val(data.FormNo.Auto_No);
                    $('#fReceptCode').val(data.fReceptCode.fReceptCode);
                    $('#cReceptCode').val(data.cReceptCode.cReceptCode);
                   
                }
                else {
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
function Validate() {
    return true;
}
function BtnSaveUpdate() {
    if (ValidateOperation() == true) {
        $('#AjaxLoader').show();
        var TEntity = {
            FerulId: $('#FerulId').val(),
            bill_date: $('#bill_date').val(),
            //bill_No: $('#bill_No').val(),
            PM_PartyCode: $('#PM_PartyCode').val(),
            fAmt: $('#fAmt').val(),
            cAmt: $('#cAmt').val(),
            fReceptCode: $('#fReceptCode').val(),
            cReceptCode: $('#cReceptCode').val(),
        }
        $('#AjaxLoader').hide();
        $.ajax({
            url: '/JQuery/InsUpFerrul',
            data: JSON.stringify(TEntity),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: 'POST',
            async: "true",
            cache: "false",
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    if (data.Result == 10) {
                        toastr.success(data.Message);
                        window.location.href = '/Transaction/FerrulCharge';
                    }
                    else if (data.Result == -10) {
                        toastr.success(data.Message);
                        window.location.href = '/Transaction/FerrulCharge';
                    }
                    else if (data.Result == -100) {
                        toastr.success(data.Message);
                        window.location.href = '/Transaction/FerrulCharge';

                    }
                    else {
                        window.location.href = '/Transaction/FerrulCharge';
                        toastr.success(data.Message);
                        var win = window.open('/Report/FerrulChargesReport/' + data.Result + '', '_blank');
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
    if ($('#PL_PartyCode').val()=="") {
        toastr.error("Please Enter Consumer Id.....!");
        return false;
    }
    else {
        return  true
    }
}
function DeleteFerrul(ID) {
    var r = confirm("Are You Sure Want To Delete..!");
    if (r == true) {
        var TEntity = JSON.stringify({
            FerulId: ID
        });
        $.ajax({
            url: '/JQuery/DeleteFerrul',
            data: TEntity,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Transaction/FerrulChargeList")
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
function OnKeyfAmt() {
    hdn_fAmt = $('#hdn_fAmt').val();
    fAmt = $('#fAmt').val();
    if (parseFloat(isEmpty(hdn_fAmt)) < parseFloat(isEmpty(fAmt))) {
        toastr.error("Please Enter Valid Amount.....!");
        $('#fAmt').val(0);
    }
}
function OnKeycAmt() {
    hdn_cAmt = $('#hdn_cAmt').val();
    cAmt = $('#cAmt').val();
    if (parseFloat(isEmpty(hdn_cAmt)) < parseFloat(isEmpty(cAmt))) {
        toastr.error("Please Enter Valid Amount.....!");
        $('#cAmt').val(0);
    }
}
function isEmpty(val) {
    return (val == undefined || val == null || val == "") ? 0 : val;
}
function Refresh() {
    $('#AjaxLoader').show();
    $("#table tbody").empty();

    $('#PM_PartyCode').val("");
    $('#PM_FHName').val("");
    $('#AM_AreaName').val("");
    $('#PM_ParaName').val("");
    $('#PM_PhoneNo').val("");
    $('#Auto_No').val("");
    $('#fReceptCode').val("");
    $('#cReceptCode').val("");
    $('#AjaxLoader').hide();
}

function fetchData() {
    var fromdate = $('#fromdate').val();
    var todate = $('#todate').val();
    var custId = $('#custId').val();
    var url = window.location.origin;
    window.location.href = url + "/Transaction/FerrulChargeList?fromdate=" + fromdate + "&todate=" + todate + "&custId=" + custId;
    return false;
}
window.onload = function () {
    $("#PL_PartyCode").focus()
};