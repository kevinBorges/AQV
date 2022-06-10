$(document).ready(function () {

    $(".cep").mask("00000-000");
    $(".cpf").mask("000.000.000-00");
    $(".phone").mask("(00) 00000-0000");

    $(".cep").on('input', function () {
        if ($(".cep").val().length === 9)
            searchCep($(".cep").val());
    });
});

function CallMethod(url, parameters, successCallback, errorCallback) {
    $.ajax({
        type: 'POST',
        url: url,
        data: parameters,
        //contentType: 'application/json;',
        contentType: 'application/x-www-form-urlencoded',
        dataType: 'json',
        success: function (data) {
            successCallback(data);
        },
        error: errorCallback
    });
}


function formatMoney(n, c, d, t) {
    c = isNaN(c = Math.abs(c)) ? 2 : c, d = d == undefined ? "," : d, t = t == undefined ? "." : t, s = n < 0 ? "-" : "", i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "", j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
}

function showCustomAlert(title, message, callback, reload) {
    $('#modalCustomAlertTitle').text(title);
    $('#modalCustomAlertMessage').html(message);
    $('#modalCustomAlert').modal('show');
    if (callback !== undefined) {
        $('#btnModalCustomAlertConfirmation').on("click", function () {
            callback();
        });
    }

    if (reload !== undefined) {
        if (reload === true) {
            $('#btnModalConfirmacao').click(function () {
                reload();
            });
        }
    }
}

function reload() {
    location.reload();
}