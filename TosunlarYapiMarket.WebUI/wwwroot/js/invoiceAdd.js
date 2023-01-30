$(document).ready(function () {

    $('#customerList').select2();

    $('#datepicker-input').datepicker();

    $.ajax({
        url: '/Admin/Invoice/GetInvoiceCode',
        type: 'Get',
        dataType: 'json',
        success: function (data) {
            const parsedData = jQuery.parseJSON(data);
            if (parsedData.invoiceCode === 0) {
                $('#invoiceCodeInput').val(1);
            } else {
                $('#invoiceCodeInput').val(parsedData.invoiceCode);
            }

        },
        error: function (err) {
            console.log(`Hata : ${err.responseText}`);
        }
    })
});