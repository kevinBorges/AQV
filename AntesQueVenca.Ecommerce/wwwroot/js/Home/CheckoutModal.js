
$('#paymentType').change(function () {
    var tipo = $('#paymentType option:selected').val();

    if (tipo == 1) {
        $('#divTroco').css('display', 'block');
    } else {
        $('#divTroco').css('display', 'none');
        $('.money').val("");
    }

});


$('.money').mask('#.##0,00', { reverse: true });