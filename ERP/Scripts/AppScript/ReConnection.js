function DeleteReCon(GS_SIID) {
    var r = confirm("Are You Sure Want To Delete..!");
    if (r == true) {
        var TEntity = JSON.stringify({
            GS_SIID: GS_SIID
        });
        $.ajax({
            url: '/JQuery/DeleteReCon',
            data: TEntity,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Transaction/ReConnectionList")
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

function ChkOnChange(IM_ItemCode) {

    QtyOnkeyUp();
    if ($("#chk_" + IM_ItemCode).is(':checked')) {
        $("#GS_Qty_" + IM_ItemCode).prop('disabled', false);
       
    }
    else {
        $("#GS_Qty_" + IM_ItemCode).prop('disabled', true);
        $("#GS_NetAmt_" + IM_ItemCode).val(0);
        $("#GS_Qty_" + IM_ItemCode).val("");
    }
}
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
            url: '/JQuery/GetConDelForReCon',
            data: { PM_PartyCode: code },
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined) {
                    $('#PL_PartyCode').val(data.ConsumerDetails.PM_PartyCode);
                    $('#PM_PartyCode').val(data.ConsumerDetails.PM_PartyCode);
                    $('#select2-PM_PartyCode-container').html(data.ConsumerDetails.PM_PartyName);
                        $('#PM_FHName').val(data.ConsumerDetails.PM_FHName);
                        $('#AM_AreaName').val(data.ConsumerDetails.AM_AreaName);
                        $('#PM_ParaName').val(data.ConsumerDetails.PM_ParaName);
                        $('#PM_MobNo').val(data.ConsumerDetails.PM_MobNo);
                        $('#GS_BillNo').val(data.BillNo.GS_BillNo);
                }
                else {
                    $('#PL_PartyCode').val("");
                    $('#PM_PartyCode').val("");
                    $('#PM_FHName').val("");
                    $('#AM_AreaName').val("");
                    $('#PM_ParaName').val("");
                    $('#PM_PhoneNo').val("");
                    $('#GS_BillNo').val("");
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
function QtyOnkeyUp(IM_ItemCode) {
    var Qty = $("#GS_Qty_" + IM_ItemCode).val();
    var Rate = $("#IM_Rate_" + IM_ItemCode).val();
    var Amount = parseFloat(isEmpty(Qty)) * parseFloat(isEmpty(Rate));
    $("#GS_NetAmt_" + IM_ItemCode).val(Amount);
    var tableList = $('#tableReConnection >tbody >tr').length;
    var AlocateAmt = 0;
    for (var i = 0; i < tableList; i++) {
        var ItemCode = $("#tableReConnection>tbody:eq(0) tr:eq(" + i + ") td:eq(0)").find('input[type="checkbox"]').val();
        if ($("#chk_" + ItemCode).is(':checked')) {
            var PaidAmt = $("#GS_NetAmt_" + ItemCode).val();
            AlocateAmt = parseFloat(isEmpty(PaidAmt)) + parseFloat(isEmpty(AlocateAmt));
        }
    }
    $('#GS_GrossAmt').val(AlocateAmt.toFixed(2));
    var gross = $("#GS_GrossAmt").val();
    var AdjDAmountPlus = $("#GS_AdjAmt").val();
    var AdjDAmountminus = $("#AdjDAmount").val();
    var AlocateAmt = parseFloat(isEmpty(gross)) + parseFloat(isEmpty(AdjDAmountPlus)) - parseFloat(isEmpty(AdjDAmountminus));
    $("#GS_NetAmt").val(AlocateAmt);
    $("#GS_Paid").val(AlocateAmt);
}
function isEmpty(val) {
  return (val == undefined || val == null || val == "") ? 0 : val;
}
function BtnSaveUpdate() {
    if (ValidateOperation() == true) {
        $('#AjaxLoader').show();
        var RepInsItemList = [];
        var tableList = $('#tableReConnection >tbody >tr').length;
        for (var i = 0; i < tableList; i++) {
            var ItemCode = $("#tableReConnection>tbody:eq(0) tr:eq(" + i + ") td:eq(0)").find('input[type="checkbox"]').val();
            if ($("#chk_" + ItemCode).is(':checked')) {
                RepInsItemList.push(
                    {
                        GS_ItemCode: ItemCode,
                        GS_SIID: $("#GS_SIID_" + ItemCode).val(),
                        ///ins_name: $("#ins_id" + ItemCode).val(),
                        GS_BillNo: $("#GS_BillNo_" + ItemCode).val(),
                        GS_BillNo: $("#GS_BillDate" + ItemCode).val(),
                        GS_Qty: $("#GS_Qty_" + ItemCode).val(),
                        GS_Rate: $("#IM_Rate_" + ItemCode).val(),
                        GS_Amount: $("#GS_NetAmt_" + ItemCode).val(),
                        GS_Vat: $("#GS_Vat_" + ItemCode).val(),
                        GS_VatAmt: $("#GS_VatAmt_" + ItemCode).val(),

                    });
            }

        }
        var TEntity = {
            GS_SIID: $('#GS_SIID').val(),
            GS_BillNo: $('#GS_BillNo').val(),
            GS_BillDate: $('#GS_BillDate').val(),
            GS_PartyCode: $('#PL_PartyCode').val(),
            ///ins_name: $("#ins_id").val(),
            GS_GrossAmt: $('#GS_GrossAmt').val(),
            GS_VatAmt: $('#TaxAmt').val(),
            GS_AdjAmt: $('#GS_AdjAmt').val(),
            GS_NetAmt: $('#GS_NetAmt').val(),
            GS_Paid: $('#GS_Paid').val(),
            GS_Due: $('#GS_Due').val(),
            RepInsItemList: RepInsItemList,
        }
        $('#AjaxLoader').hide();
        $.ajax({
            url: '/JQuery/InsUpReConnection',
            data: JSON.stringify(TEntity),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: 'POST',
            async: "true",
            cache: "false",
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    //alert($('#GS_BillNo').val());
                    toastr.success(data.Message);
                    if (data.Result == 100) {
                        //Refresh();
                        window.location.href = '/Transaction/ReConnection';
                        var win = window.open('/Report/ReConnection?GS_BillNo=' + $('#GS_BillNo').val() + '', '_blank');
                        if (win) {
                            //Browser has allowed it to be opened
                            win.focus();
                           // window.print();
                        } else {
                            //Browser has blocked it
                            alert('Please allow popups for this website');
                        }
                    }
                    if (data.Result == 10) {
                        window.location.href = '/Transaction/ReConnection';
                        var win = window.open('/Report/ReConnection?GS_BillNo=' + $('#GS_BillNo').val() + '', '_blank');
                        if (win) {
                            //Browser has allowed it to be opened
                            win.focus();
                            //window.print();
                        } else {
                            //Browser has blocked it
                            alert('Please allow popups for this website');
                        }
                    }

                    else {
                        Refresh();
                        toastr.success(data.Message);
                        var win = window.open('/Report/ReConnection?GS_BillNo=' + $('#GS_BillNo').val() + '', '_blank');
                        if (win) {
                            //Browser has allowed it to be opened
                            win.focus();
                            window.print();
                        } else {
                            //Browser has blocked it
                            alert('Please allow popups for this website');
                        }
                        //window.location.href = '/Report/BeneficaryConnection/' + data.Result + ''
                    }
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
    var tableList = $('#tableReConnection >tbody >tr').length;
    if ($('#GS_BillNo').val() == "") {
        toastr.error('Please Enter Consumer Id...!');
        return false;
    }
    else {
        let count = 0
        for (var i = 0; i < tableList; i++) {
            var ItemCode = $("#tableReConnection>tbody:eq(0) tr:eq(" + i + ") td:eq(0)").find('input[type="checkbox"]').val();
            if ($("#chk_" + ItemCode).is(':checked')) {
                count = 1;
                return true;
                break;
            }
        }
        if (count == 0) {
            toastr.error('Please Select Atleast One Item...!');
            return false;
        }
    }


}
function OnchangeAdjDAmmttplus() {
    var gross = $("#GS_GrossAmt").val();
    var AdjDAmountPlus = $("#GS_AdjAmt").val();
    var AdjDAmountminus = $("#AdjDAmount").val();
    var AlocateAmt = parseFloat(isEmpty(gross)) + parseFloat(isEmpty(AdjDAmountPlus)) - parseFloat(isEmpty(AdjDAmountminus));
    $("#GS_NetAmt").val(AlocateAmt);
    $("#GS_Paid").val(AlocateAmt);
}
function OnchangeAdjDAmmttminus() {
    var gross = $("#GS_GrossAmt").val();
    var AdjDAmountPlus = $("#GS_AdjAmt").val();
    var AdjDAmountminus = $("#AdjDAmount").val();
    var AlocateAmt = parseFloat(isEmpty(gross)) + parseFloat(isEmpty(AdjDAmountPlus)) - parseFloat(isEmpty(AdjDAmountminus));
    $("#GS_NetAmt").val(AlocateAmt);
    $("#GS_Paid").val(AlocateAmt);
}
function OnchangePaidAmt() {
    var GS_NetAmt = $("#GS_NetAmt").val();
    var GS_Paid = $("#GS_Paid").val();
    if (parseFloat(isEmpty(GS_NetAmt)) < parseFloat(isEmpty(GS_Paid))) {
        $('#GS_Due').val(GS_NetAmt);
        $("#GS_Paid").val(0);
    }
    else {
        $('#GS_Due').val(parseFloat(isEmpty(GS_NetAmt)) - parseFloat(isEmpty(GS_Paid)))
    }
}
function Refresh() {
    window.location.reload();
}
function fetchData() {
    var fromdate = $('#fromdate').val();
    var todate = $('#todate').val();
    var GS_PartyCode = $('#GS_PartyCode').val();
    var url = window.location.origin;
    window.location.href = url + "/Transaction/ReConnectionList?fromdate=" + fromdate + "&todate=" + todate + "&GS_PartyCode=" + GS_PartyCode;
    return false;
}
window.onload = function () {
    $("#PL_PartyCode").focus()
};