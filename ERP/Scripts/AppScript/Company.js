function DeleteCompany(CM_ID) {
    var r = confirm("Are You Sure Want To Delete..!");
    if (r==true) {
        var _data = JSON.stringify({
            CM_ID: CM_ID
        });
        $.ajax({
            url: '/JQuery/DeleteCompany',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    $('#AjaxLoader').hide();
                    toastr.success(data.Message);
                    window.location.href = '/Master/CompanyList';
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
    $('#CM_ID').val("");
    $('#code').val("");
    $('#name').val("");
    $('#address').val("");
    $('#city').val("");
    $('#pin').val("");
    $('#phoneno').val("");
    $('#puser').val("");
    $('#dt_of_entry').val("");
    $('#invbill_prefix').val("");
    $('#ho').val("");
    $('#ipd_pref').val("");
    $('#opd_pref').val("");
    $('#opdinv_pref').val("");
    $('#AjaxLoader').hide();

}
function BtnSaveUpdate() {
    if (ValidateOperation() == true) {
        $('#AjaxLoader').show();
        var fileUpload = $("#logo").get(0);
        if (fileUpload.files.length >0) {
            getFiles(fileUpload);
            logo = '/Document/Img/Company/' + fileUpload.files[0].name;
        }
        else {
            logo = $('#hdnImage').val();
        }
        var _data = JSON.stringify({
            CM_ID: $('#CM_ID').val(),
            code: $('#code').val(),
            name: $('#name').val(),
            address: $('#address').val(),
            city: $('#city').val(),
            pin: $('#pin').val(),
            phoneno: $('#phoneno').val(),
            puser: $('#puser').val(),
            dt_of_entry: $('#dt_of_entry').val(),
            invbill_prefix: $('#invbill_prefix').val(),
            ho: $('#ho').val(),
            ipd_pref: $('#ipd_pref').val(),
            opd_pref: $('#opd_pref').val(),
            opdinv_pref: $('#opdinv_pref').val(),
            logo: logo
        });

        $.ajax({
            url: '/JQuery/InsUpCompany',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    $('#AjaxLoader').hide();
                    toastr.success(data.Message);
                    if (data.Result == 100) {
                        Refresh();
                    }
                    if (data.Result == 10) {
                        window.location.href ='/Master/CompanyList';
                    }
                    setTimeout(function () {
                        
                    }, 2000);
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
function getFiles(uploadFileName) {
    var files = uploadFileName.files;
    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
        data.append("fileName", files[i].name);
    }
    $.ajax({
        url: "/api/FileUloadCompany/UploadFiles",
        type: "POST",
        data: data,
        contentType: false,
        processData: false,
        async: false,
        success: function (result) {
        },
        error: function (err) {
        }
    });
}
function ValidateOperation() {
    return true;
}