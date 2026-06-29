function SearchByPartyName() {
    var PM_PartyCode = $('#PM_PartyCode').val();
    FillConsumerDerails(PM_PartyCode);
}
function SearchByPartyCode() {
    var PM_PartyCode = $('#PartyCode').val();
    FillConsumerDerails(PM_PartyCode);
}

function Refresh() {
    window.location.reload();
}
function FillConsumerDerails(code) {
    if (Validate() == true) {
        $('#AjaxLoader').show();
        $.ajax({
            url: '/JQuery/GetConDelForConTaxPayment',
            data: { PM_PartyCode: code },
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                $('#AjaxLoader').hide();
                if (data != null && data != undefined) {
                        $('#PartyCode').val(data.ConsumerDetails.PM_PartyCode);
                        $('#PM_PartyCode').val(data.ConsumerDetails.PM_PartyCode);
                        $('#select2-PM_PartyCode-container').html(data.ConsumerDetails.PM_PartyName);
                        $('#PM_FHName').val(data.ConsumerDetails.PM_FHName);
                        $('#AM_AreaName').val(data.ConsumerDetails.AM_AreaName);
                        $('#PM_ParaName').val(data.ConsumerDetails.PM_ParaName);
                        $('#PM_PhoneNo').val(data.ConsumerDetails.PM_PhoneNo);
                     $('#PM_PartyId').val(data.ConsumerDetails.PM_PartyId);
                    
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
        var PM_SFlag = "";
        if ($("#PM_SFlag").is(':checked')) {
            PM_SFlag = 'A';
        }
        var TEntity = {
            PM_PartyId: $('#PM_PartyId').val(),
            PM_PartyCode: $('#PM_PartyCode').val(),
            PM_SFlag: PM_SFlag,
            PM_TaxDate: $('#PM_TaxDate').val()
        }
        $('#AjaxLoader').hide();
        $.ajax({
            url: '/JQuery/InsUpConTaxPayment',
            data: JSON.stringify(TEntity),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: 'POST',
            async: "true",
            cache: "false",
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    toastr.success(data.Message);
                    if (data.Result == 100) {
                        //Refresh();
                        //window.location.href = '/Transaction/FormSalesList';
                        window.location.reload();
                    }
                    if (data.Result == -10) {
                       // window.location.href = '/Transaction/FormSalesList';
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
    if ($('#PM_PartyCode').val() == "") {
        toastr.error("Please Enter Consumer Id.....!");
        return false;
    }
    else {
        return true
    }
}


