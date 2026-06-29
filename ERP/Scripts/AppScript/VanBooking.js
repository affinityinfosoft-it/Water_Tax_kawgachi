$('#AP_Code').on('keypress', function (e) {
    if (e.which === 13) {
        e.preventDefault();
        SearchByAPCode();
    }
});

function SearchByAPCode() {
    var apCode = $('#AP_Code').val();
    if (apCode) {
        FillApplicantDetails(apCode);
    }
}

function FillApplicantDetails(code) {
    $('#AjaxLoader').show();
    $.ajax({
        url: '/JQuery/GetConDelForVanBooking',
        data: { AP_Code: code },
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('#AjaxLoader').hide();
            if (data != null && data.ApplicantDetails != null) {
                $('#AP_Code').val(data.ApplicantDetails.AP_Code);
                $('#AP_Name').val(data.ApplicantDetails.AP_Name);
                $('#AP_FathersName').val(data.ApplicantDetails.AP_FathersName);
                $('#AP_Address').val(data.ApplicantDetails.AP_Address);
                $('#AP_Mobile').val(data.ApplicantDetails.AP_Mobile);

                $('#AM_AreaID').val(data.ApplicantDetails.AM_AreaID).trigger('change');
                $('#PM_ParaId').val(data.ApplicantDetails.PM_ParaId).trigger('change');
            } else {
                toastr.error(data.Message || "No applicant found!");
            }
        },
        error: function () {
            $('#AjaxLoader').hide();
            toastr.error('Something went wrong while fetching applicant details.');
        }
    });
}










function DeleteBooking(AP_ID) {
    var r = confirm("Are You Sure Want To Delete..!");
    if (r == true) {
        var _data = JSON.stringify({
            AP_ID: AP_ID
        });
        $.ajax({
            url: '/JQuery/DeleteVanBooking',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    $('#AjaxLoader').hide();
                    toastr.success(data.Message);
                    window.location.href = '/Transaction/VanbookingList';
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
function Refresh() {
    $('#AjaxLoader').show();
    $('#AP_Code').val(),
        $('#AP_Name').val(),
        $('#AP_FathersName').val(),
        $('#AP_Mobile').val(),
        $('#AM_AreaID').val(),
        $('#PM_ParaId').val(),
        $('#AP_Address').val(),
        $('#DeliveryLandmark').val(),
        $('#Bk_Purpose').val(),
        $('#VM_ID').val(),
        $('#FromDate').val(),
        $('#FromTime').val(),
        $('#ToDate').val(),
        $('#ToTime').val(),
        $('#Amount').val(),
        $('#FormNo').val(),  
        $('#FormAmount').val(),
        $('#Qty').val(),
        $('#RegDate').val()

}
function BtnSaveUpdate() {
    if (ValidateOperation() == true) {
        debugger
        $('#AjaxLoader').show();
        var _data = JSON.stringify({
            AP_ID: $('#AP_ID').val(),
            AP_Code: $('#AP_Code').val(),
            AP_Name: $('#AP_Name').val(),
            AP_FathersName: $('#AP_FathersName').val(),
            /*VB_RcptNo: $('#VB_RcptNo').val(),*/
            AP_Mobile: $('#AP_Mobile').val(),
            AM_AreaID: $('#AM_AreaID').val(),
            PM_ParaId: $('#PM_ParaId').val(),
            AP_Address: $('#AP_Address').val(),
            DeliveryLandmark: $('#DeliveryLandmark').val(),
            Bk_Purpose: $('#Bk_Purpose').val(),
            VM_ID: $('#VM_ID').val(),
            FromDate: $('#FromDate').val(),
            FromTime: $('#FromTime').val(),
            ToDate: $('#ToDate').val(),
            ToTime: $('#ToTime').val(),
            Qty: $('#Qty').val(),
            FormNo: $('#FormNo').val(),
            FormAmount: $('#FormAmount').val(),
            Amount: $('#Amount').val(),
            CM_ID: $('#CM_ID').val(),
            CreatedDate: $('#CreatedDate').val(),
            RegDate: $('#RegDate').val(),

        });
        $('#AjaxLoader').hide();
        $.ajax({
            url: '/JQuery/InsUpVanbooking',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    if (data.Result == -1) {
                        toastr.success(data.Message);
                        window.location.href = '/Transaction/VanbookingList';
                    }
                    else if (data.Result == -10) {
                        toastr.success(data.Message);
                        window.location.href = '/Transaction/VanbookingList';

                    }
                    else if (data.Result == -100) {
                        toastr.success(data.Message);
                        window.location.href = '/Transaction/VanbookingList';

                    }
                    else {
                        Refresh();
                        toastr.success(data.Message);
                        var win = window.open('/Report/VanBooking/' + data.Result + '', '_blank');
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
    if ($('#AP_Code').val() == "") {
        toastr.error('Please enter Applicant Name.');
        return false;
    }
    else if ($('#AP_Name').val() == "") {
        toastr.error('Please enter Applicant Name.');
        return false;
    }
    return true;
}
function Refresh() {
    window.location.reload();
}
function fetchData() {
    var fromdates = $('#fromdates').val();
    var todates = $('#todates').val();
    var AP_Code = $('#AP_Code').val();
    var url = window.location.origin;
    window.location.href = url + "/Transaction/VanbookingList?fromdates=" + fromdates + "&todates=" + todates + "&AP_Code=" + AP_Code;
    return false;
}