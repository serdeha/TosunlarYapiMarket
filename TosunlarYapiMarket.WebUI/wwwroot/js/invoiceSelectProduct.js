$('#stockList1').select2();

$(document).ready(function () {
    var i = 1;
    $('#addRow').on('click', function () {
        i++;
        var html = '';
        html += `<div id="inputFormRow${i}">`;
        html += '<div class="input-group mb-3">';
        html += '<input type="text" name="title[]" class="form-control m-input" placeholder="Enter title" autocomplete="off">';
        html += '<div class="input-group-append">';
        html += '<button id="removeRow" type="button" class="btn btn-danger">Remove</button>';
        html += '</div>';
        html += '</div>';
        $('#newRow').append(html);
    });

    $(document).on('click', '#removeRow', function () {
        $(this).closest('#inputFormRow').remove();
    });
});


$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/Admin/Invoice/GetSelectListForStocks',
        success: function (data) {
            const parsedData = jQuery.parseJSON(data);
            var s = '<option value="-1">Lütfen bir ürün seçiniz.</option>';
            for (var i = 0; i < parsedData.length; i++) {
                console.log(parsedData[i]);
                s += `<option value='${parsedData[i].Value}'> ${parsedData[i].Text} </option>`;
            }
            $('#stockList1').html(s);
        }
    });
})


function OnlyNumeric(selectListId) {
    $(`#stockList${selectListId}`).on('change', function () {

        let listText = $(`#stockList${selectListId} option:selected`).text();

        
    });
}

function AllSelectList(selectListId) {
    $.ajax({
        type: 'GET',
        url: '/Admin/Invoice/GetSelectListForStocks',
        success: function (data) {
            const parsedData = jQuery.parseJSON(data);
            var s = '<option value="-1">Lütfen bir ürün seçiniz.</option>';
            for (var i = 0; i < parsedData.length; i++) {
                console.log(parsedData[i]);
                s += `<option value='${parsedData[i].Value}'> ${parsedData[i].Text} </option>`;
            }
            $(`#stockList${selectListId}`).html(s);
        }
    });
}

function AllSelectTwo(selectListId) {
    $(`#stockList${selectListId}`).select2();
    $(`#stockList${selectListId}`).attr('name', 'StockId');
    $(`#stockListInput${selectListId}`).attr('name', 'StockAnyDetail');
}

$(document).ready(function () {
    //group add limit
    var maxGroup = 100;
    var i = 2;

    //add more fields group
    $(".addMore").click(function () {
        if ($('body').find('.fieldGroup').length < maxGroup) {
            i++;
            var newSelectItem = $('.fieldGroupCopy > div > select');
            var newInputItem = $('.fieldGroupCopy > div > input');
            newSelectItem.attr('id', `stockList${i}`);
            newInputItem.attr('id', `stockListInput${i}`);
            var fieldHTML = '<div class="form-group fieldGroup">' + $(".fieldGroupCopy").html() + '</div>';
            $('body').find('.fieldGroup:last').after(fieldHTML);
            AllSelectTwo(i);
            AllSelectList(i);
            OnlyNumeric(i);

        } else {
            alert('Maximum ' + maxGroup + ' groups are allowed.');
        }
    });

    //remove fields group
    $("body").on("click", ".remove", function () {
        $(this).parents(".fieldGroup").remove();
    });
});