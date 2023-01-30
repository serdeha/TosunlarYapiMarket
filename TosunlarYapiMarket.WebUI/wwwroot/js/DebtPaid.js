$('#data-table').DataTable({
    "language": {
        "url": "https://cdn.datatables.net/plug-ins/1.10.20/i18n/Turkish.json"
    }
});

$(function () {
    const url = '/Admin/Debt/PayOffDebtPartial/';
    const placeHolderDiv = $('#modalPlaceHolder');


    $(document).on('dblclick', '.PayOffDebtDetail', function (event) {
        event.preventDefault();

        const id = $(this).data('debtid');
        const customerName = $(this).data('customer');

        $.get(url, { debtId: id, customer: customerName }).done(function (data) {

            placeHolderDiv.html(data);
            placeHolderDiv.find('.modal').modal('show');
            $('#data-table-payment').DataTable({
                "language": {
                    "url": "https://cdn.datatables.net/plug-ins/1.10.20/i18n/Turkish.json"
                }
            });

        }).fail(function (err) {
            Swal.fire(
                'Lütfen kullanıcı giriş yaptığınızdan emin olunuz.',
                `İşlem gerçekleştirilemedi. Hata : ${err.responseText}`,
                'warning'
            );
        });
    })
})