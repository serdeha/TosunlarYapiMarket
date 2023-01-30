$(document).ready(function() {

    $.ajax({
        url: '/Admin/Customer/GetCustomerNo',
        type: 'Get',
        dataType: 'json',
        success: function(data) {
            const parsedData = jQuery.parseJSON(data);
            if (parsedData.customerNo === false) {
                console.log('Bir hata oluştu');
                window.href = window.href;
            } else {
                $('#customerNoInput').val(parsedData.customerNo);
            }
        },
        error: function(err) {
            console.log(`Hata : ${err.responseText}`);
        }
    });
});

$(document).on('click', '.deleteCustomer', function (event) {
    event.preventDefault();
    event.stopPropagation();

    const id = $(this).data('id');
    const title = $(this).data('title');

    Swal.fire({
        title: `${title} Silinsin Mi ?`,
        text: `${title} isimli müşteri silinecektir.`,
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, Silinsin!',
        cancelButtonText: 'Vazgeç'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                url: '/Admin/Customer/Delete',
                type: 'Get',
                dataType: 'json',
                data: { customerId : id },
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
                            `${title} başarıyla silindi.`,
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

$(function() {
    const url = "/Admin/Customer/GetDetail";
    const placeHolderDiv = $('#modalPlaceHolder');

    $(document).on('click', '.customerDetail', function (event) {
        event.preventDefault();

        const id = $(this).data('id');


        $.get(url, { customerId: id }).done(function (data) {
            if (data.ResultStatus === false) {
                Swal.fire(
                    'Lütfen yönetici ile iletişime geçiniz.',
                    `İşlem gerçekleştirilemedi. Hata : ${err.responseText}`,
                    'warning'
                );
            }
            placeHolderDiv.html(data);
            placeHolderDiv.find('.modal').modal('show');
        }).fail(function (err) {
            Swal.fire(
                'Lütfen yönetici ile iletişime geçiniz.',
                `İşlem gerçekleştirilemedi. Hata : ${err.responseText}`,
                'warning'
            );
        });
    });
})