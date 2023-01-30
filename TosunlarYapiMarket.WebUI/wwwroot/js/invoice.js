$('#data-table').DataTable({
    "language": {
        "url": "https://cdn.datatables.net/plug-ins/1.10.20/i18n/Turkish.json"
    }
});

$(document).on('click', '.deleteInvoice', function (event) {
    event.preventDefault();
    event.stopPropagation();

    const id = $(this).data('id');
    const title = $(this).data('title');

    Swal.fire({
        title: `${title} Silinsin Mi ?`,
        text: `${title} isimli müşterinin fatura silinecektir.`,
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, Silinsin!',
        cancelButtonText: 'Vazgeç'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                url: '/Admin/Invoice/Delete',
                type: 'Get',
                dataType: 'json',
                data: { invoiceId : id },
                success: function (data) {

                    const parsedData = jQuery.parseJSON(data);

                    if (parsedData.ResultStatus === false) {
                        Swal.fire(
                            'İşlem Tamamlanamadı!',
                            `İşlem tamamlanırken bir hata oluştu.`,
                            'error'
                        );
                        return;
                    } else {
                        Swal.fire(
                            'Başaryıla Silindi!',
                            `${title} müşterinin faturası başarıyla silindi.`,
                            'success'
                        );
                        const tableRow = $(`[name='${id}']`);
                        tableRow.fadeOut(2500);
                    }
                },
                error: function (err) {
                    Swal.fire(
                        'İşlem Başarısız!',
                        `İşlem tamamlanırken bir hata oluştu. ${err.responseText}`,
                        'error'
                    );
                }
            });

        }
    });
});