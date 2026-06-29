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
            url: '/JQuery/GetConDelForWaterTaxCollection',
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
                    $('#PT_RcptNo').val(data.fReceptCode.PT_RcptNo);
                    $('#PT_DtTos').val(data.LastTaxDate.PT_DtTos);
                    $('#PT_DtFrom').val(data.LastTaxDate.PT_DtFroms);
                    $('#TaxPaymentFrom').val(data.LastTaxDate.PT_DtFroms);


                    d1 = new Date($("#PT_DtFrom").val());
                    d2 = new Date($("#PT_PmtDate").val());
                    var Difmonth = MonthDifference(d1, d2)
                    var a = convert(addMonths(d1, Difmonth).toString())
                    var my_date = new Date(a);
                    var last_date = convertLastDayOfMonth(new Date(my_date.getFullYear(), my_date.getMonth(), 0));
                    $("#PT_DtTo").val(last_date);
                    $('#NoOfDueMth').val(Difmonth);
                    OnchangeMonthCalByMonth();

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
//function OnChangeMonthCal() {
//    //No of Due month
//    d1 = new Date($("#TaxPaymentFrom").val());
//    d2 = new Date($("#TaxPaymentTo").val());
//    var month = MonthDifference(d1, d2)
//    //$('#NoOfDuePayMth').val(month);
//    //No of Due month
//    //Tax Due From 
//    pd1 = new Date($("#PT_DtFrom").val());
//    pd2 = new Date($("#PT_DtTo").val());
//    var pmonth = MonthDifference(pd1, pd2)
//    //Tax Due To Month 
//    //Tax Due Date Check
//    var pDate = new Date();
//    var d = new Date(pDate);
//    var dueDate = d.getDate();
//    //Tax Due Date Check

//    if (dueDate > $('#f_date').val()){
//        $('#DueTaxTotalMonth').val(pmonth);//1
//    }
//    else {
//        pmonth = pmonth - 1;
//        $('#DueTaxTotalMonth').val(pmonth);
//    }

//    payd1 = new Date($("#TaxPaymentFrom").val());
//    payd2 = new Date($("#TaxPaymentTo").val());
//    var paymonth = MonthDifference(payd1, payd2)//3

//    if ($('#DueTaxTotalMonth').val() > 0) {
//        if (paymonth>=12) {
//            var paymonth = paymonth - $('#DueTaxTotalMonth').val();//2
//            var TotalAdvPay = paymonth * $('#f_amt').val();//120
//            var TotalDuePay = $('#DueTaxTotalMonth').val() * $('#s_amt').val();//65
//            $('#PT_Amount').val((TotalAdvPay + TotalDuePay) - $('#f_amt').val());
//        }
//        else {
//            var paymonth = paymonth - $('#DueTaxTotalMonth').val();//2
//            var TotalAdvPay = paymonth * $('#f_amt').val();//120
//            var TotalDuePay = $('#DueTaxTotalMonth').val() * $('#s_amt').val();//65
//            $('#PT_Amount').val(TotalAdvPay + TotalDuePay);
//        }
//    }
//    else {
//        if (paymonth>=12) {
//            $('#PT_Amount').val((paymonth * $('#f_amt').val()) - $('#f_amt').val());
//        }
//        else {
//            $('#PT_Amount').val(paymonth * $('#f_amt').val());
//        }
//    }
//}
function RecptDateOnchange() {
    var CheckInDate = $('#PT_PmtDate').val();
    var PT_DtFrom = $('#PT_DtFrom').val();
    var Difmonth = MonthDifference(new Date(PT_DtFrom), new Date(CheckInDate))
    var a = convert(addMonths(new Date(PT_DtFrom), Difmonth).toString())
    var my_date = new Date(a);
    var last_date = convertLastDayOfMonth(new Date(my_date.getFullYear(), my_date.getMonth(), 0));
    $("#PT_DtTo").val(last_date);
    $('#NoOfDueMth').val(Difmonth);
    OnchangeMonthCalByMonth();
}
function OnChangeMonthCal() {

    debugger
    //CheckinDate//
    var CheckInDate = $('#PT_PmtDate').val();
    //CheckinDate//
    //NoOfDueMth//
    var NoOfDueMth = $('#NoOfDueMth').val();
    //NoOfDueMth//
    //NoOfDuePayMth//
    var NoOfDuePayMth = $('#NoOfDuePayMth').val();
    //NoOfDuePayMth//
    //f_date//
    var f_date = $('#f_date').val();
    //f_date//
    //s_date//
    var s_date = $('#s_date').val();
    //s_date//
    //t_date//
    var t_date = $('#t_date').val();
    //t_date//
    //
    var PT_DtTo = $('#PT_DtTo').val();
    //
    //LessEqualFirstSlab/
    var LessEqualFirstSlab = $('#f_amt').val();
    //LessEqualFirstSlab/
    //LessEqualSecondSlab/
    var LessEqualSecondSlab = $('#s_amt').val();
    //LessEqualSecondSlab/
    //LessEqualThiredSlab/
    var LessEqualThiredSlab = $('#t_amt').val();
    //LessEqualThiredSlab/
    //TaxPaymentFrom
    var TaxPaymentFrom = $('#TaxPaymentFrom').val();
    //TaxPaymentFrom
    //TaxPaymentTo
    var TaxPaymentTo = $('#TaxPaymentTo').val();
    //TaxPaymentTo
    var TotalAmt = 0;

    // for (var i = 1; i <= NoOfDuePayMth; i++) {
        if (new Date(CheckInDate).getDate() <= f_date) {
            if (NoOfDueMth == 1 && new Date(CheckInDate).getFullYear() == new Date(PT_DtTo).getFullYear() && new Date(CheckInDate).getMonth() == new Date(PT_DtTo).getMonth() || 
                NoOfDueMth == 1 && new Date(CheckInDate).getFullYear() == new Date(PT_DtTo).getFullYear() && new Date(CheckInDate).getMonth() == new Date(PT_DtTo).getMonth()+1) {
                TotalAmt += (LessEqualFirstSlab * NoOfDueMth)
                TotalAmt += (LessEqualFirstSlab * (NoOfDuePayMth - NoOfDueMth))
            }
            else {
                if (NoOfDuePayMth - NoOfDueMth > 0) {
                    if (new Date(CheckInDate).getFullYear() == new Date(PT_DtTo).getFullYear() && new Date(CheckInDate).getMonth() == new Date(PT_DtTo).getMonth() + 1) {
                        TotalAmt += (LessEqualSecondSlab * (NoOfDueMth))
                        TotalAmt += (LessEqualFirstSlab * (NoOfDuePayMth - (NoOfDueMth)))
                        TotalAmt = (TotalAmt - (parseInt(LessEqualSecondSlab) - parseInt(LessEqualFirstSlab)))
                    }
                    else {
                        TotalAmt += (LessEqualSecondSlab * NoOfDueMth)
                        TotalAmt += (LessEqualFirstSlab * (NoOfDuePayMth - NoOfDueMth))
                    }   
                }
            else {
                    if (new Date(TaxPaymentTo).getFullYear() == new Date(PT_DtTo).getFullYear() && new Date(TaxPaymentTo).getMonth() == new Date(PT_DtTo).getMonth()) {
                        TotalAmt += LessEqualFirstSlab*1;
                        TotalAmt += (LessEqualSecondSlab * (NoOfDuePayMth - 1))
                    }
                    else {
                        TotalAmt += (LessEqualSecondSlab * NoOfDuePayMth)
                    }
                }
            }         
        }
        else if (new Date(CheckInDate).getDate() > f_date && new Date(CheckInDate).getDate() <= s_date) {
            if (NoOfDueMth == 1 && new Date(CheckInDate).getFullYear() == new Date(PT_DtTo).getFullYear() && new Date(CheckInDate).getMonth() == new Date(PT_DtTo).getMonth() ) {
                TotalAmt += (LessEqualFirstSlab * NoOfDueMth)
                TotalAmt += (LessEqualFirstSlab * (NoOfDuePayMth - NoOfDueMth))
        }
            else {
                if (NoOfDuePayMth - NoOfDueMth > 0) {
                    TotalAmt += (LessEqualSecondSlab * NoOfDueMth)
                    TotalAmt += (LessEqualFirstSlab * (NoOfDuePayMth - NoOfDueMth))
                }
                else {
                    TotalAmt += (LessEqualSecondSlab * NoOfDuePayMth)
                }
            }
        }
        else if (new Date(CheckInDate).getDate() > s_date && new Date(CheckInDate).getDate() <= t_date) {
            if (NoOfDueMth == 1 && new Date(CheckInDate).getFullYear() == new Date(PT_DtTo).getFullYear() && new Date(CheckInDate).getMonth() == new Date(PT_DtTo).getMonth()) {
                TotalAmt += (LessEqualFirstSlab * NoOfDueMth)
                TotalAmt += (LessEqualFirstSlab * (NoOfDuePayMth - NoOfDueMth))
        }
            else {
                if (NoOfDuePayMth - NoOfDueMth > 0) {
                    TotalAmt += (LessEqualThiredSlab * NoOfDueMth)
                    TotalAmt += (LessEqualFirstSlab * (NoOfDuePayMth - NoOfDueMth))
                }
                else {
                    TotalAmt += (LessEqualThiredSlab * NoOfDuePayMth)
                }
            }
        }

        if ((NoOfDuePayMth - NoOfDueMth)>1) {
        TotalAmt = TotalAmt - (LessEqualFirstSlab * parseInt(NoOfDuePayMth/12))
        }
        $('#PT_Amount').val(parseFloat(TotalAmt));
        }


function DueMonthDifference(d1, d2) {
    var months;
    months = (d2.getFullYear() - d1.getFullYear()) * 12;
    months -= d1.getMonth();
    months += d2.getMonth();
    return months < 0 ? 0 : months + 1;
}
function MonthDifference(d1, d2) {
    var months;
    months = (d2.getFullYear() - d1.getFullYear()) * 12;
    months -= d1.getMonth();
    months += d2.getMonth();
    return months == 0 ? 1 : months<0 ?0:months;
}
function DayDifference(d1, d2) {
    var days;
    var Difference_In_Time = d2.getTime() - d1.getTime();
    days = Difference_In_Time / (1000 * 3600 * 24);
    //alert(Difference_In_Days);
    return days <= 0 ? 0 : days + 1;
}
function BtnSaveUpdate() {
    if (ValidateOperation() == true) {
        $('#AjaxLoader').show();
        var TEntity = {
            PT_ID: $('#PT_ID').val(),
            PT_RcptNo: $('#PT_RcptNo').val(),
            PT_PtyCode: $('#PM_PartyCode').val(),
            PT_PmtDate: $('#PT_PmtDate').val(),
            PT_DtFrom: $('#TaxPaymentFrom').val(),
            PT_DtTo: $('#TaxPaymentTo').val(),
            PT_Mth: $('#NoOfDuePayMth').val(),
            PT_Rate: $('#PT_Amount').val(),
            PT_Amount: $('#PT_Amount').val()
        }
        $('#AjaxLoader').hide();
        $.ajax({
            url: '/JQuery/InsUpWaterTaxCollection',
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
                        window.location.href = '/Transaction/WaterTaxCollection';
                    }
                    else if (data.Result == -10) {
                        toastr.success(data.Message);
                        window.location.href = '/Transaction/WaterTaxCollection';
                    }
                    else if (data.Result == -100) {
                        toastr.success(data.Message);
                        window.location.href = '/Transaction/WaterTaxCollection';

                    }
                    else {
                        window.location.href = '/Transaction/WaterTaxCollection';
                        toastr.success(data.Message);
                        var win = window.open('/Report/WaterTaxCollectionReport/' + data.Result + '', '_blank');
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
                if (data != null && data != undefined && data.IsSuccess == true) {
                    toastr.success(data.Message);
                    if (data.Result == 100) {
                        //Refresh();
                        //window.location.href = '/Transaction/FormSalesList';
                    }
                    if (data.Result == 10) {
                        //window.location.href = '/Transaction/FormSalesList';
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
    return true;
}
function DeleteWaterTaxCollection(PT_ID) {
    var r = confirm("Are You Sure Want To Delete..!");
    if (r == true) {
        var TEntity = JSON.stringify({
            PT_ID: PT_ID
        });
        $.ajax({
            url: '/JQuery/DeleteWaterTaxCollection',
            data: TEntity,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined && data.IsSuccess == true) {
                    Calltoastrsuccessredirectanotherpage(data.Message, "/Master/AreaList/Transaction/WaterTaxCollectionList")
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
function OnchangeMonthCalByMonth() {
    if ($('#PL_PartyCode').val() != "") {
        if ($("#NoOfDuePayMth").val() == "") {
            $('#TaxPaymentTo').val("");
            $('#PT_Amount').val(0);
        }
        else {
            var TaxPaymentFrom = $("#TaxPaymentFrom").val();
            var NoOfDuePayMth = $("#NoOfDuePayMth").val();
            var a = convert(addMonths(new Date(TaxPaymentFrom), NoOfDuePayMth).toString())
            var my_date = new Date(a);
            var last_date = converts(new Date(my_date.getFullYear(), my_date.getMonth(), 0));
            $('#TaxPaymentTo').val(last_date);
            OnChangeMonthCal();
        }
    }
    else {
        toastr.error("Please Enter Consumer Id.......!");
        $('#TaxPaymentTo').val("");
        $('#PT_Amount').val(0);
        $('#NoOfDuePayMth').val("");
    }


}
function addMonths(date, months) {
    var d = date.getDate();
    date.setMonth(date.getMonth() + +months);
    if (date.getDate() != d) {
        date.setDate(0);
    }
    return date;
}
function converts(str) {
    var date = new Date(str),
        mnth = ("0" + (date.getMonth() + 1)).slice(-2),
        day = ("0" + date.getDate()).slice(-2);
    return [date.getFullYear(), mnth, day].join("-");
}
function convert(str) {
    var mnths = {
        Jan: "01",
        Feb: "02",
        Mar: "03",
        Apr: "04",
        May: "05",
        Jun: "06",
        Jul: "07",
        Aug: "08",
        Sep: "09",
        Oct: "10",
        Nov: "11",
        Dec: "12"
    },
        date = str.split(" ");

    return [date[3], mnths[date[1]], date[2]].join("-");
}
function convertLastDayOfMonth(str) {
    var date = new Date(str),
        mnth = ("0" + (date.getMonth() + 1)).slice(-2),
        day = ("0" + date.getDate()).slice(-2);
    return [date.getFullYear(), mnth, day].join("-");
}
//alert(pmonth);
//alert(Day);
//if (pmonth == 1) {
//    if (true) {
//        if (f_date >= Day) {
//            $('#PT_Amount').val(parseFloat(LessEqualFirstSlab));
//        }
//        else {
//            $('#PT_Amount').val(parseFloat(LessEqualSecondSlab));
//        }
//    }
//}
//else {
//    if (pmonth > 1) {

//    }
//}
function Refresh() {
    window.location.reload();
}
function fetchData() {
    var fromdate = $('#fromdate').val();
    var todate = $('#todate').val();
    var PT_PtyCode = $('#PT_PtyCode').val();
    var url = window.location.origin;
    window.location.href = url + "/Transaction/WaterTaxCollectionList?fromdate=" + fromdate + "&todate=" + todate + "&PT_PtyCode=" + PT_PtyCode;
    return false;
}
window.onload = function () {
    if ($('#PL_PartyCode').val() != null && $('#PL_PartyCode').val()!="") {
        $('#PM_PartyCode').val($('#PL_PartyCode').val())
        $('#select2-PM_PartyCode-container').html($('#PM_PartyName').val());
    }
    $("#PL_PartyCode").focus()
};