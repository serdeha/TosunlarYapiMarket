$('#data-table').DataTable({
    "language": {
        "url": "https://cdn.datatables.net/plug-ins/1.10.20/i18n/Turkish.json"
    }
});



$(function () {
    const url = "/Admin/Note/GetUndoDetail";
    const placeHolderDiv = $('#modalPlaceHolder');

    $(document).on('click', '.approvedNoteDetail', function (event) {
        event.preventDefault();

        const id = $(this).data('id');


        $.get(url, { noteId: id }).done(function (data) {
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

$(function() {
    const url = "/Admin/Note/UndoNote";
    const placeHolderDiv = $('#modalPlaceHolder');

    $(document).on('click', '#undoNote', function(event) {
        event.preventDefault();
        event.stopPropagation();

        const id = $(this).data('id');
        const title = $(this).data('title');

        Swal.fire({
            title: `${title} Geri Getirilsin Mi ?`,
            text: `${title} başlıklı not geri getirilecektir.`,
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet, Getirilsin!',
            cancelButtonText: 'Vazgeç'
        }).then((result) => {
            if (result.isConfirmed) {

                $.get(url, { noteId: id }).done(function (data) {
                    if (data.ResultStatus === false) {
                        Swal.fire(
                            'Lütfen yönetici ile iletişime geçiniz.',
                            `İşlem gerçekleştirilemedi. Hata : ${err.responseText}`,
                            'warning'
                        );
                    }

                    const parsedData = jQuery.parseJSON(data);
                    placeHolderDiv.find('.modal').modal('hide');
                    Swal.fire(
                        `${parsedData.NoteTitle} Not'u Getirildi.`,
                        `${parsedData.NoteTitle} başlıklı not geri getirildi.`,
                        'success'
                    );

                    const tableRow = $(`[name="${id}"]`);
                    tableRow.fadeOut(2500);
                }).fail(function (err) {
                    Swal.fire(
                        'Lütfen yönetici ile iletişime geçiniz.',
                        `İşlem gerçekleştirilemedi. Hata : ${err.responseText}`,
                        'warning'
                    );
                });
            }
        });
    });
});

$(function () {
    $(document).on('click', '.noteDelete', function (event) {
        event.preventDefault();
        event.stopPropagation();

        const id = $(this).data('id');
        const title = $(this).data('title');

        Swal.fire({
            title: `${title} Silinsin Mi ?`,
            text: `${title} note'u silinecektir.`,
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet, Silinsin!',
            cancelButtonText: 'Vazgeç'
        }).then((result) => {
            if (result.isConfirmed) {

                $.ajax({
                    url: '/Admin/Note/Delete',
                    type: 'Get',
                    dataType: 'json',
                    data: { noteId: id },
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
});