$(document).ready(function () {
    $('#stockDetailList').select2({
        theme: "classic",
        placeholder: "Bir ürün detayı seçiniz."
    });
});

jQuery.fn.ForceNumericOnly =
    function () {
        return this.each(function () {
            $(this).keydown(function (e) {
                var key = e.charCode || e.keyCode || 0;
                // allow backspace, tab, delete, enter, arrows, numbers and keypad numbers ONLY
                // home, end, period, and numpad decimal
                return (
                    key == 8 ||
                    key == 188 ||
                    key == 9 ||
                    key == 13 ||
                    key == 46 ||
                    key == 110 ||
                    (key >= 35 && key <= 40) ||
                    (key >= 48 && key <= 57) ||
                    (key >= 96 && key <= 105));
            });
        });
    };



$(document).on('click', '.deleteStock', function (event) {
    event.preventDefault();
    event.stopPropagation();

    const id = $(this).data('id');
    const title = $(this).data('title');

    Swal.fire({
        title: `${title} Silinsin Mi ?`,
        text: `${title} isimli ürün silinecektir.`,
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, Silinsin!',
        cancelButtonText: 'Vazgeç'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                url: '/Admin/Stock/Delete',
                type: 'Get',
                dataType: 'json',
                data: { stockId: id },
                success: function (data) {

                    const parsedData = jQuery.parseJSON(data);

                    if (data.ResultStatus === false) {
                        Swal.fire(
                            'İşlem Tamamlanamadı!',
                            `İşlem tamamlanırken bir hata oluştu.`,
                            'error'
                        );
                        return;
                    } else {
                        Swal.fire(
                            'Başaryıla Silindi!',
                            `${parsedData.StockName} başarıyla silindi.`,
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

$(function () {
    $('#kdvInput').ForceNumericOnly();
    $('#priceInput').ForceNumericOnly();
    $('#stockAnyDetailInput').ForceNumericOnly();
})

$(function () {
    $('#stockDetailList').on('change', function () {

        let listText = $('#stockDetailList option:selected').text();

        switch (listText) {
            case 'Adet':
                $('#stockAnyDetailInput').attr('type', 'number');
                $('#stockAnyDetailInput').removeAttr('step');
                break;
            case 'Metre':
                $('#stockAnyDetailInput').removeAttr('type');
                $('#stockAnyDetailInput').attr('step', '0.01');
                break;
            case 'Metrekare':
                $('#stockAnyDetailInput').removeAttr('type');
                $('#stockAnyDetailInput').attr('step', '0.01');
                break;
            case 'Ton':
                $('#stockAnyDetailInput').removeAttr('type');
                $('#stockAnyDetailInput').attr('step', '0.01');
                break;
        }
    });
})


$(function () {
    const url = '/Admin/Stock/GetDetail';
    const placeHolderDiv = $('#modalPlaceHolder');


    $(document).on('click', '.stockDetail', function (event) {
        event.preventDefault();

        const id = $(this).data('id');

        $.get(url, { stockId: id }).done(function (data) {
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

$(document).on('click', '#getStockDetailBtn', function (event) {
    event.preventDefault();
    $('#GetStockDetail').removeAttr("style");
})