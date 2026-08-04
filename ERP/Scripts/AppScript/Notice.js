$(document).ready(function () {
    ToggleNoticeFor();

    $("input[name='NoticeFor']").change(function () {

        ToggleNoticeFor();

    });
    $('#NM_PartyCode').keypress(function (e) {

        if (e.which == 13) {

            e.preventDefault();

            SearchByPartyCode();
        }

    });
    $('#NM_AreaId').change(function () {

        var areaId = $(this).val();

        LoadPara(areaId, "");

    });
    $("#btnSave").click(function () {

        BtnSaveUpdate();

    });

});

function SearchByPartyCode() {

    var partyCode = $('#NM_PartyCode').val();

    if (partyCode == "")
        return;

    FillConsumerDetails(partyCode);

}

function FillConsumerDetails(code) {

    $('#AjaxLoader').show();

    $.ajax({

        url: '/JQuery/GetConsumerForNotice',

        type: 'GET',

        data: { PartyCode: code },

        dataType: 'json',

        success: function (data) {

            $('#AjaxLoader').hide();

            if (data.IsSuccess) {

                $('#NM_PartyCode').val(data.ConsumerDetails.NM_PartyCode);
                $('#PartyName').val(data.ConsumerDetails.PartyName);
                $('#FatherName').val(data.ConsumerDetails.FatherName);
                $('#Mobile').val(data.ConsumerDetails.Mobile);

                $('#NM_AreaId').val(data.ConsumerDetails.NM_AreaId);

                LoadPara(
                    data.ConsumerDetails.NM_AreaId,
                    data.ConsumerDetails.NM_ParaId
                );

            }
            else {

                toastr.error(data.Message);

            }

        },

        error: function () {

            $('#AjaxLoader').hide();

            toastr.error("Something went wrong.");

        }

    });

}

function LoadPara(areaId, selectedPara) {

    $("#NM_ParaId").empty();

    $("#NM_ParaId").append('<option value="">Select Para</option>');

    if (areaId == "")
        return;

    $.ajax({

        url: '/JQuery/GetParaByArea',

        type: 'GET',

        data: { AreaId: areaId },

        dataType: 'json',

        success: function (data) {

            if (data.IsSuccess) {

                $.each(data.Data, function (i, item) {

                    $('#NM_ParaId').append(

                        $('<option></option>')
                            .val(item.PM_ParaId)
                            .text(item.PM_ParaName)

                    );

                });

                if (selectedPara != "")
                    $('#NM_ParaId').val(selectedPara);

            }

        }

    });

}

function ToggleNoticeFor() {

    var noticeFor = $("input[name='NoticeFor']:checked").val();

    if (noticeFor == "Party") {

        $("#divParty").show();
        $("#divArea").hide();
        $("#divPara").hide();

    }
    else if (noticeFor == "Area") {

        $("#divParty").hide();
        $("#divArea").show();
        $("#divPara").hide();

    }
    else if (noticeFor == "Para") {

        $("#divParty").hide();
        $("#divArea").show();
        $("#divPara").show();

    }
    else {

        $("#divParty").hide();
        $("#divArea").hide();
        $("#divPara").hide();

    }

}
function BtnSaveUpdate() {

    if (!ValidateOperation())
        return;

    var formData = new FormData();

    formData.append("NM_Id", $("#NM_Id").val());
    formData.append("NM_NT_Id", $("#NM_NT_Id").val());
    formData.append("NM_Title", $("#NM_Title").val());
    formData.append("NM_Notice", $("#NM_Notice").val());
    formData.append("NM_FromDate", $("#NM_FromDate").val());
    formData.append("NM_ToDate", $("#NM_ToDate").val());
    formData.append("NM_PartyCode", $("#NM_PartyCode").val());
    formData.append("NM_AreaId", $("#NM_AreaId").val());
    formData.append("NM_ParaId", $("#NM_ParaId").val());
    formData.append("NM_IsPublish", $("#NM_IsPublish").is(":checked"));

    var file = $("#NoticeFile")[0].files[0];

    if (file != null) {
        formData.append("NoticeFile", file);
    }

    $.ajax({

        url: "/JQuery/InsUpNotice",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,

        success: function (data) {

            if (data.IsSuccess) {

                toastr.success(data.Message);

                window.location.href = "/Transaction/NoticeList";

            }
            else {

                toastr.error(data.Message);

            }

        }

    });

}

function ValidateOperation() {

    if ($('#NM_NT_Id').val() == "") {

        toastr.error("Select Notice Type");

        return false;

    }

    if ($('#NM_Title').val() == "") {

        toastr.error("Enter Notice Title");

        return false;

    }

    if ($('#NM_Notice').val() == "") {

        toastr.error("Enter Notice");

        return false;

    }

    if ($('#NM_FromDate').val() == "") {

        toastr.error("Select From Date");

        return false;

    }

    if ($('#NM_ToDate').val() == "") {

        toastr.error("Select To Date");

        return false;

    }

    var noticeFor = $("input[name='NoticeFor']:checked").val();

    if (noticeFor == "Party") {

        if ($('#NM_PartyCode').val() == "") {

            toastr.error("Enter Party Code");

            return false;

        }

    }

    if (noticeFor == "Area") {

        if ($('#NM_AreaId').val() == "") {

            toastr.error("Select Area");

            return false;

        }

    }

    if (noticeFor == "Para") {

        if ($('#NM_AreaId').val() == "") {

            toastr.error("Select Area");

            return false;

        }

        //if ($('#NM_ParaId').val() == "") {

        //    toastr.error("Select Para");

        //    return false;

        //}

    }

    return true;

}

function DeleteNotice(id) {

    if (!confirm("Are you sure want to delete?"))
        return;

    $.ajax({

        url: '/JQuery/DeleteNotice',

        type: 'POST',

        data: JSON.stringify({ NM_Id: id }),

        contentType: 'application/json; charset=utf-8',

        success: function (data) {

            if (data.IsSuccess) {

                toastr.success(data.Message);

                location.reload();

            }
            else {

                toastr.error(data.Message);

            }

        }

    });

}