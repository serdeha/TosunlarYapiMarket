$(function () {

    const url = "/Admin/Invoice/ShowDiscountPartial";
    const placeHolderDiv = $('#modalPlaceHolder');


    $(document).on('click', '.btnDiscount', function (event) {
        event.preventDefault();
        event.stopPropagation();

        const id = $(this).data('id');


        $.get(url, { invoiceId: id }).done(function (data) {
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

$(document).on('click', '#discountPartial', function (event) {
    event.preventDefault();
    event.stopPropagation();

    const id = $(this).data('id');
    const title = $(this).data('title');
    let invoiceDiscount = $('#inlineFormInputGroup').val();

    Swal.fire({
        title: `İndirim Uygulansın Mı ?`,
        text: `${title} isimli müşterinin tutarından ${invoiceDiscount} ₺ düşürülecektir.`,
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, Düşürülsün!',
        cancelButtonText: 'Vazgeç'
    }).then((result) => {
        if (result.isConfirmed) {
            invoiceDiscount = invoiceDiscount.replace(',', '.');

            $.ajax({
                url: '/Admin/Invoice/AddDiscount',
                type: 'Get',
                dataType: 'json',
                data: { invoiceId: id, discountPrice: invoiceDiscount },
                success: function (data) {

                    const parsedData = jQuery.parseJSON(data);
                    console.log(parsedData);

                    if (parsedData.ResultStatus === false) {
                        Swal.fire(
                            'Geçersiz Tutar!',
                            'Girilen tutar, faturanın tutarından fazla olamaz veya faturanın tümüne indirim uygulanamaz.',
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
                        
                        const newRow = `
                                    <tr name="${parsedData.Id}">
                                        <td>${parsedData.InvoiceCode}</td>
                                        <td>${parsedData.Customer.FirstName} ${parsedData.Customer.LastName}</td>      
                                        <td>
                                            <span class="badge badge-${parsedData.IsPaid ? 'success' : 'danger'}">
                                                ${parsedData.IsPaid ? 'Ödendi':'Ödenmedi'}
                                            </span>
                                        </td>
                                        <td>${moment(parsedData.InvoiceDate).format('DD/MM/YYYY').replaceAll('/', '.')}</td>
                                        <td>${parsedData.InvoiceDescription == null ? "-" : parsedData.InvoiceDescription}</td>
                                        <td>${parsedData.TotalPrice} ₺</td>
                                        <td>
                                            ${parsedData.DiscountedTotalPrice == 0 ? "-" : parsedData.DiscountedTotalPrice} ₺
                                        </td>
                                        <td>
                                            <div class="btn-group btn-group-sm">
                                                <a class="btn btn-success text-white" href="/Admin/Invoice/AddInvoiceStock?invoiceId=${parsedData.Id}">
                                                    <i class="fas fa-plus"></i> Ürün Ekle
                                                </a>
                                                <a class="btn btn-secondary text-white" href="/Admin/Invoice/ShowInvoice?invoiceId=${parsedData.Id}">
                                                    <i class="fas fa-file-invoice"></i> Faturayı Gör
                                                </a>
                                                <button class="btn btn-primary text-white btnDiscount" data-id="${parsedData.Id}" style="${invoice.IsPaid ? 'display:none' : ''}">
                                                    <i class="fa fa-tag"></i>  İndirim
                                                </button>
                                                <button class="btn btn-danger text-white deleteInvoice" data-id="${parsedData.Id}" data-title="${parsedData.Customer.FirstName} ${parsedData.Customer.LastName}">
                                                    <i class="fas fa-trash-alt"></i> Sil
                                                </button>
                                            </div>
                                        </td>
                                    </tr>
                            `;


                        const currentRow = $(`[name=${id}]`);
                        currentRow.replaceWith(newRow);

                        const placeHolderDiv = $('#modalPlaceHolder');
                        placeHolderDiv.find('.modal').modal('hide');
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