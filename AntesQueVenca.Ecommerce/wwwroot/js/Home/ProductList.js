$(document).ready(function () {
    $("#productFilter").keypress(function () {
        if (this.value.length > 2) {
            filter();
        }
    });
});

function reloadOrderDetails() {
    var html = getOrderDetailsHtml();
    $("#productsTbody").empty();
    $("#orderTotalPrice").empty();
    $("#productsTbody").html(html);
    $("#orderTotalPrice").html(shoppingCart.totalCart());
    $("#subTotal").html(shoppingCart.subTotalCart());
}

function orderDetails() {
    var html = getOrderDetailsHtml();
    $("#productsTbody").html(html);
    $("#orderTotalPrice").html(shoppingCart.totalCart());
    $('#checkoutModal').show();
    $("#flexRadioDefault1").prop("checked", true);
    loadDeliveryType(1);
}

function getOrderDetailsHtml() {
    var cartArray = shoppingCart.listCart();
    var html = "";
    for (var i in cartArray) {
        var discount = new Decimal(cartArray[i].priceFrom).sub(cartArray[i].unitPrice);
        html += "<tr>";
        html += "<td>";
        html += "<img class='img-fluid' width='100' height='100' id='img-" + cartArray[i].id + "' src='" + cartArray[i].image + "' alt='" + cartArray[i].name + "'>";
        html += "</td>";

        html += "<td  class='col-sm-2'>";
        html += "<span>" + cartArray[i].name + "</span>";
        html += "</td>";

        html += "<td>";
        html += "<span>" + Intl.NumberFormat('pt-br', { style: 'currency', currency: 'BRL' }).format(cartArray[i].priceFrom) + "</span>";
        html += "</td>";

        html += "<td>";
        html += "<span>" + Intl.NumberFormat('pt-br', { style: 'currency', currency: 'BRL' }).format(discount) + "</span>";
        html += "</td>";

        html += "<td>";
        html += "<span>" + Intl.NumberFormat('pt-br', { style: 'currency', currency: 'BRL' }).format(cartArray[i].unitPrice) + "</span>";
        html += "</td>";

        html += "<td>";
        html += "<span>" + cartArray[i].count + "</span>";
        html += "</td>";

        html += "<td>";
        html += "<span>" + Intl.NumberFormat('pt-br', { style: 'currency', currency: 'BRL' }).format(cartArray[i].price) + "</span>";
        html += "</td>";

        html += "<tr>";
    }
    return html;
}

function generateCode() {
    if ($("#userName").val() === "") {
        showCustomAlert("Aviso", "O campo nome é obrigatório!", undefined, false);
        return;
    }

    if ($("#userPhone").val() === "") {
        showCustomAlert("Aviso", "O campo telefone é obrigatório!", undefined, false);
        return;
    }

    if ($("#DeliveryType").val() == 0) {
        showCustomAlert("Aviso", "Selecione uma forma de retirada!", undefined, false);
        return;
    }

    var deliveryType = 1;
    if (parseInt($("#userDataForm input[type='radio']:checked").val()) === 2) {
        if ($("#userCEP").val() === "") {
            showCustomAlert("Aviso", "CEP Inválido", undefined, false);
            return;
        }

        if (parseInt($("#deliveryEligible").val()) === 0) {
            showCustomAlert("Aviso", 'Desculpe, ainda não estamos entregando na sua região! Se possível, faça a reserva no formato retirada.', undefined, false);
            return;
        }


        if ($("#userStreetNumber").val() === "") {
            showCustomAlert("Aviso", "Digite o número ou S/N para sem número", undefined, false);
            return;
        }

        if ($("#paymentType").val() === 0) {
            showCustomAlert("Aviso", "Por favor, selecione a forma de pagamento.", undefined, false);
            return;
        }
        deliveryType = 2;
    }

    var objj = [];
    for (var i in shoppingCart.listCart()) {
        var obj = {
            name: shoppingCart.listCart()[i].name,
            image: shoppingCart.listCart()[i].image,
            price: shoppingCart.listCart()[i].price.toString(),
            priceFrom: shoppingCart.listCart()[i].priceFrom.toString(),
            unitPrice: shoppingCart.listCart()[i].unitPrice.toString(),
            total: shoppingCart.listCart()[i].total.toString(),
            count: shoppingCart.listCart()[i].count,
            id: shoppingCart.listCart()[i].id
        };
        objj.push(obj);
    }

    //const f = $("#deliveryPrice").val().toString();
    //var frete = new Decimal(0);
    var consumerAddress = { Number: $("#userStreetNumber").val(), Street: $("#userStreet").val(), CityName: $("#userCity").val(), Neighborhood: $("#userNeighbor").val(), UF: $("#userUF").val(), PostalCode: $("#userCEP").val(), AddressType: $("#addressType").val(), Complement: $("#userComplement").val() };
    var consumerObj = { CPF: $("#userCPF").val(), Email: $("#userEmail").val(), Name: $("#userName").val(), Phone: $("#userPhone").val(), Address: consumerAddress };
    var postObj = { Consumer: consumerObj, CartItems: objj, DeliveryType: deliveryType, PaymentType: $("#paymentType").val(), Thing: $("#thing").val(), DeliveryPrice: 0 };
    CallMethod('/Home/Add', postObj, onSuccess, onError)
}

function onSuccess(data) {
    if (data.Success === true) {
        shoppingCart.clearCart();
        $('#checkoutModal').modal('toggle');
        $("#orderResume").empty();

        if (parseInt($("#userDataForm input[type='radio']:checked").val()) === 1) {
            $('#confirmationModal').modal();
            $("#confirmationCode").text(data.Retorno.OrderId);
            $("#confirmationDate").text(data.Retorno.WithdrawDate);
        }
        else {
            $("#confirmationCodeDelivery").text(data.Retorno.OrderId);
            $('#confirmationModalDelivery').modal();
        }
    }
    else {
        showCustomAlert('Aviso', data.Message, undefined, false);
    }
}

function onError(response) {
    showCustomAlert('Aviso', response, undefined, false);
}

function loadDeliveryType(type) {
    if (type === 1) {
        $("#orderDelivery").hide();
        $("#orderWithDrawal").show();
        $(".frete").css("visibility", "hidden");
        $("#deliveryEligible").val(1);
    }
    if (type === 2) {
        $("#orderWithDrawal").hide();
        $("#orderDelivery").show();
        $(".frete").css("visibility", "visible");
    }

    $("#subTotal").html(shoppingCart.subTotalCart());
    shoppingCart.totalCart();
    reloadOrderDetails()
}

function searchCep(cepNumber) {
    var obj = { cep: cepNumber };
    CallMethod('/Home/SearchCep', obj, onSuccessCep, onError)
}

function onSuccessCep(data) {
    if (data.Success === false) {
        $("#address").hide();
        $("#deliveryEligible").val(0);
        showCustomAlert('Aviso', 'Desculpe, ainda não estamos entregando na sua região :(', undefined, false);
    }
    else {
        reloadOrderDetails();
        $("#frete").empty();
        $("#deliveryPrice").empty();
        var frete = "<th>Frete:</th>";
        frete += "<td id='valorFrete'>R$ " + Intl.NumberFormat('pt-br', {currency: 'BRL' }).format(data.Retorno) + "</td>";
        $("#frete").append(frete);
        $("#deliveryPrice").val(data.Retorno);
        var total = new Decimal(parseFloat($("#orderTotalPrice").text().toString().replace(",", ".").replace("R$", "").replace(" ", "")));
        var FreteTotal = new Decimal(parseFloat(data.Retorno));
        $("#orderTotalPrice").empty();
        $("#orderTotalPrice").html(Intl.NumberFormat('pt-br', { style: 'currency', currency: 'BRL' }).format(total.add(FreteTotal)));
        var cep = $("#userCEP").val();
        $.ajax({
            url: 'https://viacep.com.br/ws/' + cep + '/json',
            dataType: 'json',
            contentType: "application/json",
            statusCode: {
                200: function (data) { loadAdress(data); } // Ok
                , 400: function (msg) { alert(msg); } // Bad Request
                , 404: function (msg) { console.log("CEP não encontrado!!"); } // Not Found
            }
        });
    }
    
}

function loadAdress(data) {
    $("#deliveryEligible").val(1);
    $("#userStreet").val(data.logradouro);
    $("#userNeighbor").val(data.bairro);
    $("#userCity").val(data.localidade);
    $("#userUF").val(data.uf);
    $("#userStreetNumber").focus();
    $("#address").show();
}

function filter() {
    $("#productName").val($("#productFilter").val());
    $("#filterForm").submit();
}