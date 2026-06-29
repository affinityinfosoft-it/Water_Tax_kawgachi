$(document).ready(function () {
    $("#fr_rate").prop("disabled", true);
    //$("#AMOUNT").prop("disabled", true);
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
        $.ajax({
            url: '/JQuery/GetConDelForFormSale',
            data: { PM_PartyCode: code },
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined) {
                    if (data.Party.PM_FFlag=="") {
                        alert("From Already Exist...!");
                    }
                    else {
                        $('#PL_PartyCode').val(data.ConsumerDetails.PM_PartyCode);
                        $('#PM_PartyCode').val(data.ConsumerDetails.PM_PartyCode);
                        $('#select2-PM_PartyCode-container').html(data.ConsumerDetails.PM_PartyName);
                        $('#PM_FHName').val(data.ConsumerDetails.PM_FHName);
                        $('#AM_AreaName').val(data.ConsumerDetails.AM_AreaName);
                        $('#PM_ParaName').val(data.ConsumerDetails.PM_ParaName);
                        $('#PM_MobNo').val(data.ConsumerDetails.PM_MobNo);
                        $('#AMOUNT').val($('#fr_rate').val());
                    }
                    
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
            ID: $('#ID').val(),
            BILLDATE: $('#BILLDATE').val(),
            PM_PartyCode: $('#PM_PartyCode').val(),
            fr_rate: $('#fr_rate').val(),
            FORMNO: $('#FORMNO').val(),
            AMOUNT: $('#AMOUNT').val()
        }
        $('#AjaxLoader').hide();
        $.ajax({
            url: '/JQuery/InsUpFORMSALE',
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
                        window.location.href = '/Transaction/FormSales';
                    }
                    else if (data.Result == -10) {
                        toastr.success(data.Message);
                        window.location.href = '/Transaction/FormSales';
                    }
                    else if (data.Result == -100) {
                        toastr.success(data.Message);
                        window.location.href = '/Transaction/FormSales';

                    }
                    else {
                        Refresh();
                        toastr.success(data.Message);
                        var win = window.open('/Report/FormSaleReport/' + data.Result + '', '_blank');
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
    if ($('#PL_PartyCode').val() == "") {
        toastr.error("Please Enter Consumer Id.....!");
        return false;
    }
    else {
        return true
    }
}

function ValidateOperation() {
    if ($('#FORMNO').val() == "") {
        toastr.error("Please Enter From No.....!");
        return false;
    }
    else {
        return true
    }
}
function DeleteFormSale(ID) {
    var r = confirm("Are You Sure Want To Delete..!");
    if (r == true) {
        var TEntity = JSON.stringify({
            ID: ID
        });
        $.ajax({
            url: '/JQuery/DeleteFormSale',
            data: TEntity,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Transaction/FormSalesList")
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
    $('#PL_PartyCode').val("");
    $('#PM_PartyCode').val("");
    $('#PM_FHName').val("");
    $('#AM_AreaName').val("");
    $('#PM_ParaName').val("");
    $('#PM_PhoneNo').val("");
    $('#FORMNO').val("");
    $('#AMOUNT').val("");
    //$('#ID'), val();
    $('#AjaxLoader').hide();
}
function fetchData() {
    var fromdate = $('#fromdate').val();
    var todate = $('#todate').val();
    var PM_PartyCode = $('#PM_PartyCode').val();
    var url = window.location.origin;
    window.location.href = url + "/Transaction/FormSalesList?fromdate=" + fromdate + "&todate=" + todate + "&PM_PartyCode=" + PM_PartyCode;
    return false;
}
window.onload = function () {
    $("#PL_PartyCode").focus()
};