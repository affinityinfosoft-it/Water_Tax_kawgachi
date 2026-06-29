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
            url: '/JQuery/GetConsumerDetails',
            data: { PM_PartyCode: code},
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                var PL_RcptNo = data.ReceptNo.PL_RcptNo;
                $('#PL_RcptNo').val(PL_RcptNo + 1);
                $('#AjaxLoader').hide();
                if (data.ConsumerDetails != null || data.ConsumerDetails != undefined) {
                    $('#PL_PartyCode').val(data.ConsumerDetails.PM_PartyCode);
                    $('#PM_PartyCode').val(data.ConsumerDetails.PM_PartyCode);
                    $('#select2-PM_PartyCode-container').html(data.ConsumerDetails.PM_PartyName);
                    $('#PM_FHName').val(data.ConsumerDetails.PM_FHName);
                    $('#AM_AreaName').val(data.ConsumerDetails.AM_AreaName);
                    $('#PM_ParaName').val(data.ConsumerDetails.PM_ParaName);
                    $('#PM_MobNo').val(data.ConsumerDetails.PM_MobNo);
                   

                    $("#table tbody").empty();
                    var AlocateAmt = 0;
                    $.each(data.DueList, function (key, value) {
                        var html = "";
                        var sl = 1;
                        if (value.PL_PaidAmount>0) {
                            html += '<tr><td><input class="form-check-input" type="checkbox" onClick="ChkOnClick(\'' + value.PL_BillNo + '\')" id="chk' + value.PL_BillNo + '" value=' + value.PL_BillNo + ' /></td>';
                            html += "<td style='display:none'><input type='text' id='BType" + value.PL_BillNo + "' value=" + value.PL_BType + "  /></td>";
                            html += "<td><input type='text' id='bill" + value.PL_BillNo + "' value=" + value.PL_BillNo + " readonly  /></td>";
                            html += "<td><input type='date' id='Bdate" + value.PL_BillNo + "' value=" + value.PL_BillDates + "  /></td>";
                            html += "<td><input type='text' id='BillAmt" + value.PL_BillNo + "' value=" + value.PL_PaidAmount + " readonly /></td>";
                            html += "<td><input type='text' id='PaidAmt" + value.PL_BillNo + "' onkeyup='txtPL_PaidAmountOnKey();' value=" + value.PL_PaidAmount + "  /></td><tr>";
                            //html += "<td><input type='text' id='Dist" + value.PL_BillNo + "' onkeyup='txtDiscountOnKey();'  /></td></tr>";
                            $("#table tbody").append(html);
                            sl++;
                            //AlocateAmt += value.PL_PaidAmount;
                        }
                        
                    });
                    //$('#PL_PaidAmount').val(AlocateAmt);
                }
                else {
                    toastr.error("Please Enter Correct Consumer Id");
                    Refresh();
                }
            },
            error: function () {
                $('#AjaxLoader').hide();
                toastr.error('Something wrong happened.');
            }

        });
    }
}
function ChkOnClick(PL_BillNo) {
    var PaidAmt = $("#PaidAmt" + PL_BillNo).val();
    var AlocAmt = $("#PL_PaidAmount").val();
    if ($("#chk" + PL_BillNo).is(':checked')) {
        $("#PL_PaidAmount").val(parseFloat(PaidAmt) + parseFloat(AlocAmt));
    }
    else {
        $("#PL_PaidAmount").val(AlocAmt - PaidAmt);

    }
   
}
function Validate() {
    return true;
}
function txtDiscountOnKey() {
    var txtDiscount = $('#txtDiscount').val();
    var PL_PaidAmount = $('#txtPL_PaidAmount').val();
    var total = PL_PaidAmount - txtDiscount;
    $('#PL_PaidAmount').val(total);
}
function txtPL_PaidAmountOnKey() {
    var tableList = $('#table >tbody >tr').length;
    var AlocateAmt = 0;
    for (var i = 0; i < tableList; i++) {
        var BillNo = $("#table>tbody:eq(0) tr:eq(" + i + ") td:eq(0)").find('input[type="checkbox"]').val();
        if ($("#chk" + BillNo).is(':checked')) {
            var PaidAmt = $("#PaidAmt" + BillNo).val();
            var OsAmt = $("#BillAmt" + BillNo).val();
            if (PaidAmt == "" || PaidAmt == undefined || PaidAmt==null) {
                PaidAmt = 0;
            }
            if (OsAmt == "" || OsAmt == undefined || OsAmt == null) {
                OsAmt = 0;
            }
            if (parseFloat(OsAmt) >= parseFloat(PaidAmt)) {

                AlocateAmt = parseFloat(PaidAmt) + parseFloat(AlocateAmt);

            }
            else {
                $("#PaidAmt" + BillNo).val(0)
            }
        }
    }
    $('#PL_PaidAmount').val(AlocateAmt)
    //var BillAmt = $('#txtBillAmount').val();
    //var txtDiscount = $('#txtDiscount').val();
    //var PL_PaidAmount = $('#txtPL_PaidAmount').val();
    //var total = PL_PaidAmount - txtDiscount;
    //$('#PL_PaidAmount').val(total);
    //if (BillAmt < total) {
    //    toastr.error('Paid Amount Greater Than OS Amount....! ');
    //    $('#txtPL_PaidAmount').val(0);
    //    $('#PL_PaidAmount').val(0);
    //}
}
function BtnSaveUpdate() {
    if (ValidateOperation() == true) {
        $('#AjaxLoader').show();
        var PartyLadher = [];
        var tableList = $('#table >tbody >tr').length;
        for (var i = 0; i < tableList; i++) {
            var BillNo = $("#table>tbody:eq(0) tr:eq(" + i + ") td:eq(0)").find('input[type="checkbox"]').val();
            if ($("#chk" + BillNo).is(':checked')) {
              PartyLadher.push(
               {
                      PL_BillNo: $("#bill" + BillNo).val(),
                      PL_BType: $("#BType" + BillNo).val(),
                      PL_BillDate: $("#Bdate" + BillNo).val(),
                      PL_BillAmount: $("#BillAmt" + BillNo).val(),
                      PL_PaidAmount: $("#PaidAmt" + BillNo).val(),
               });
            }
            
        }
        var TEntity = {
            PL_Id: $('#PL_Id').val(),
            PL_RcptNo: $('#PL_RcptNo').val(),
            PL_RcptDate: $('#PL_RcptDate').val(),
            PL_PartyCode: $('#PL_PartyCode').val(),
            PL_RcptType: $('#PL_RcptType:checked').val(),
            PL_ChqNo: $('#PL_ChqNo').val(),
            PL_ChqDate: $('#PL_ChqDate').val(),
            PL_Bank: $('#PL_Bank').val(),
            PartyLadgerList: PartyLadher,
            }
        $('#AjaxLoader').hide();
        $.ajax({
            url: '/JQuery/InsUpConPayment',
            data: JSON.stringify(TEntity),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: 'POST',
            async: "true",
            cache: "false",
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Transaction/ConsumerPaymentList")
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
    var BillAmt = $('#txtBillAmount').val();
    var PaidAmt = $('#txtPL_PaidAmount').val();
    if (BillAmt < PaidAmt) {
        toastr.error('Paid Amount Greater Than OS Amount....! ');
        return false;
    }
    else {
        return true;
    }
}
function DeleteConsumerPayment(PL_Id) {
    var r = confirm("Are You Sure Want To Delete..!");
    if (r == true) {
        var TEntity = JSON.stringify({
            PL_Id: PL_Id
        });
        $.ajax({
            url: '/JQuery/DeleteConsumerPayment',
            data: TEntity,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Transaction/ConsumerPaymentList")
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
    $("#table tbody").empty();

    $('#PM_PartyCode').val("");
    $('#PM_RegDate').val("");
    $('#PM_PartyName').val("");
    $('#PM_FHName').val("");
    $('#AM_AreaName').val("");
    $('#PM_ParaId').val("");
    $('#PM_Address').val("");
    $('#PM_City').val("");
    $('#PM_PhoneNo').val("");
    $('#PM_MobNo').val("");
    $('#PM_ParaName').val("");
    $('#CM_ID').val("");
    $('#PM_PaidAmt').val("");
    $('#puser').val("");
    $('#AjaxLoader').hide();
}
function fetchData() {
    var fromdate = $('#fromdate').val();
    var todate = $('#todate').val();
    var PL_PartyCode = $('#PL_PartyCode').val();
    var url = window.location.origin;
    window.location.href = url + "/Transaction/ConsumerPaymentList?fromdate=" + fromdate + "&todate=" + todate + "&PL_PartyCode=" + PL_PartyCode;
    return false;
}
window.onload = function () {
    $("#PL_PartyCode").focus()
};