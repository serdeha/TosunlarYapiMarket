$('#data-table').DataTable({
    "language": {
        "url": "https://cdn.datatables.net/plug-ins/1.10.20/i18n/Turkish.json"
    }
});


moment.locale('tr');

$(document).on('click', '.deleteDebt', function (event) {
    event.preventDefault();
    event.stopPropagation();

    const id = $(this).data('id');
    const title = $(this).data('title');

    Swal.fire({
        title: `${title} Silinsin Mi ?`,
        text: `${title} isimli borç silinecektir.`,
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, Silinsin!',
        cancelButtonText: 'Vazgeç'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                url: '/Admin/Debt/Delete',
                type: 'Get',
                dataType: 'json',
                data: { debtId: id },
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


$(function () {

    const url = "/Admin/Debt/PayDebtPartial";
    const placeHolderDiv = $('#modalPlaceHolder');


    $(document).on('click',
        '.payDebt',
        function (event) {
            event.preventDefault();
            event.stopPropagation();

            const id = $(this).data('id');


            $.get(url, { debtId: id }).done(function (data) {
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
});

$(document).on('click', '#payDebtPartial', function (event) {
    event.preventDefault();
    event.stopPropagation();

    const id = $(this).data('id');
    const title = $(this).data('title');
    let paidDebt = $('#inlineFormInputGroup').val();

    Swal.fire({
        title: `Borç Ödensin Mi ?`,
        text: `${title} isimli müşterinin borcu ödenecektir.`,
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, Ödensin!',
        cancelButtonText: 'Vazgeç'
    }).then((result) => {
        if (result.isConfirmed) {
            paidDebt = paidDebt.replace(',', '.');

            $.ajax({
                url: '/Admin/Debt/PayDebt',
                type: 'Get',
                dataType: 'json',
                data: { debtId: id, amountPaid: paidDebt },
                success: function (data) {

                    const parsedData = jQuery.parseJSON(data);
                    console.log(parsedData);

                    if (parsedData.ResultStatus === false) {
                        Swal.fire(
                            'İşlem Tamamlanamadı!',
                            `İşlem tamamlanırken bir hata oluştu.`,
                            'error'
                        );
                        return;
                    } else if (parsedData.ResultStatus === "Error") {
                        Swal.fire(
                            'Geçersiz Tutar!',
                            'Girilen tutar, müşteri borcundan fazla olduğundan dolayı herhangi bir işlem yapılamadı.',
                            'error'
                        );
                        return;
                    }
                    else {
                        Swal.fire(
                            'Başarıyla Ödendi!',
                            `${title} başarıyla ödendi.`,
                            'success'
                        );
                        const newRow = `<tr class="PayOffDebtDetail" name="${parsedData.Id}" data-debt="${parsedData.Id}" data-customer="${parsedData.Customer.FirstName + " " + parsedData.Customer.LastName}" role="row">
                                <td> ${title}</td>
                                <td> ${moment(parsedData.CreatedDate).format('DD/MM/YYYY').replaceAll('/', '.')} </td>
                                <td> ${moment(parsedData.PaymentDate).format('DD/MM/YYYY').replaceAll('/', '.')} </td>
                                <td> ${parseFloat(parsedData.TotalAmount)} ₺ </td>
                                <td> ${parseFloat(parsedData.PaymentAmount)} ₺ </td>
                                <td>
                                    <div class="btn-group">
                                                <a class="btn btn-success text-white font-weight-bold payDebt" data-id="${parsedData.Id}">
                                                    <i class="fas fa-credit-card"></i> Öde
                                                </a>
                                                <a class="btn btn-primary text-white font-weight-bold" href="/Admin/Debt/Update?debtId=${parsedData.Id}">
                                                    <i class="fas fa-edit"></i> Düzenle
                                                </a>
                                                <a class="btn btn-danger text-white font-weight-bold deleteDebt" data-id="3" data-title="${title}}">
                                                    <i class="fas fa-trash-alt"></i> Sil
                                                </a>
                                    </div>
                                </td>
                        </tr>`;

                        if (parsedData.PaymentAmount <= 0) {
                            const currentRow = $(`[name=${id}]`);
                            currentRow.fadeOut(3500);

                            const placeHolderDiv = $('#modalPlaceHolder');
                            placeHolderDiv.find('.modal').modal('hide');
                        } else {
                            const currentRow = $(`[name=${id}]`);
                            currentRow.replaceWith(newRow);

                            const placeHolderDiv = $('#modalPlaceHolder');
                            placeHolderDiv.find('.modal').modal('hide');
                        }
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
})

$(function () {
    const url = '/Admin/Debt/PayOffDebtPartial/';
    const placeHolderDiv = $('#modalPlaceHolder');


    $(document).on('dblclick', '.PayOffDebtDetail', function (event) {
        event.preventDefault();

        const id = $(this).data('debt');
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