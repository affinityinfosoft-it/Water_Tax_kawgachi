$(function () {

    $('#daterange').daterangepicker({

        autoUpdateInput: false,

        locale: {
            cancelLabel: 'Clear',
            format: 'DD-MM-YYYY'
        }

    }, function (start, end) {

        $('#daterange span').html(
            start.format('DD-MM-YYYY') +
            ' - ' +
            end.format('DD-MM-YYYY')
        );

        console.log("From Date:", start.format('YYYY-MM-DD'));
        console.log("To Date:", end.format('YYYY-MM-DD'));

        LoadDashboard(
            start.format('YYYY-MM-DD'),
            end.format('YYYY-MM-DD')
        );

    });


    $('#daterange').on('cancel.daterangepicker', function () {

        $('#daterange span').html('Select Date Range');

    });

});
function LoadDashboard(fromDate, toDate) {

    $.ajax({

        url: '/Home/GetDashboardByDate',

        type: 'POST',

        data: {
            FromDate: fromDate,
            ToDate: toDate
        },

        success: function (data) {

            $('.card-water h3')
                .html('₹ ' + data.WaterTaxCollection);


            $('.card-beneficiary h3')
                .html('₹ ' + data.BeneficiaryCollection);


            $('.card-total h3')
                .html('₹ ' + data.TotalCollection);


        }

    });

}