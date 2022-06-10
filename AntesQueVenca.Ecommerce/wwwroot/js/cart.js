// ************************************************
// Shopping Cart API
// ************************************************

var shoppingCart = (function () {
    // =============================
    // Private methods and propeties
    // =============================
    var cart = [];

    // Constructor
    function Item(id, name, price, count, image, priceFrom, unitPrice, totalQuantity) {
        this.id = id;
        this.name = name;
        this.price = price;
        this.priceFrom = priceFrom;
        this.count = count;
        this.image = image;
        this.unitPrice = unitPrice;
        this.totalQuantity = totalQuantity;
    }

    // Save cart
    function saveCart() {
        sessionStorage.setItem('shoppingCart', JSON.stringify(cart));
    }

    // Load cart
    function loadCart() {
        cart = JSON.parse(sessionStorage.getItem('shoppingCart'));
    }
    if (sessionStorage.getItem("shoppingCart") !== null) {
        loadCart();
    }


    // =============================
    // Public methods and propeties
    // =============================
    var obj = {};

    // Add to cart
    obj.addItemToCart = function (id, name, price, count, image, totalQuantity, priceFrom, unitPrice) {
        for (var item in cart) {
            if (cart[item].id === id) {
                if (cart[item].count >= totalQuantity) {
                    showCustomAlert('Aviso', 'Estoque insuficiente para esse produto;', undefined, false);
                    return false;
                }
                cart[item].count++;
                //var p = cart[item].price;
                cart[item].price = new Decimal(cart[item].price).add(price);
                cart[item].priceFrom = priceFrom;
                cart[item].unitPrice = unitPrice;
                cart[item].totalQuantity = totalQuantity; 
                saveCart();
                return true;
            }
        }
        var productItem = new Item(id, name, price, count, image, priceFrom, unitPrice, totalQuantity);
        cart.push(productItem);
        saveCart();
        return true;
    }
    // Set count from item
    obj.setCountForItem = function (id, count) {
        for (var i in cart) {
            if (cart[i].id === id) {
                cart[i].count = count;
                break;
            }
        }
    };
    // Remove item from cart
    obj.removeItemFromCart = function (id, price) {
        for (var item in cart) {
            if (cart[item].id === id) {
                cart[item].count--;
                cart[item].price = new Decimal(cart[item].price).sub(price);
                if (cart[item].count === 0) {
                    cart.splice(item, 1);
                }
                break;
            }
        }
        saveCart();
    }

    // Remove all items from cart
    obj.removeItemFromCartAll = function (id) {
        for (var item in cart) {
            if (cart[item].id === id) {
                cart.splice(item, 1);
                break;
            }
        }
        saveCart();
    }

    // Clear cart
    obj.clearCart = function () {
        cart = [];
        saveCart();
        $("#cartUl").empty();
    }

    // Count cart 
    obj.totalCount = function () {
        var totalCount = 0;
        for (var item in cart) {
            totalCount += cart[item].count;
        }
        return totalCount;
    }

    // SubTotal cart
    obj.subTotalCart = function () {
        var subTotalCart = new Decimal(0);
        for (var item in cart) {
            subTotalCart = subTotalCart.add(cart[item].price);
        }

        return "R$ " + new Decimal(subTotalCart);
    }

    // Total cart
    obj.totalCart = function () {
        var totalCart = new Decimal(0);
        for (var item in cart) {
            totalCart = totalCart.add(cart[item].price);
        }
        //return "R$ " + Number(totalCart.toFixed(2));

        //if (parseInt($("#userDataForm input[type='radio']:checked").val()) === 2) 
        //{
        //    var frete = new Decimal(9.9/*$("#valorFrete").text().replace(",", ".").replace("R$", "").replace(" ","")*/);
        //    totalCart = totalCart.add(frete);
        //};
        var total = Decimal(totalCart);
        return Intl.NumberFormat('pt-br', { style: 'currency', currency: 'BRL' }).format(total);
    }


    // List cart
    obj.listCart = function () {
        var cartCopy = [];
        for (i in cart) {
            item = cart[i];
            itemCopy = {};
            for (p in item) {
                itemCopy[p] = item[p];

            }
            itemCopy.total = new Decimal(item.unitPrice).mul(item.count);
            cartCopy.push(itemCopy)
        }
        return cartCopy;
    }

    // cart : Array
    // Item : Object/Class
    // addItemToCart : Function
    // removeItemFromCart : Function
    // removeItemFromCartAll : Function
    // clearCart : Function
    // countCart : Function
    // totalCart : Function
    // listCart : Function
    // saveCart : Function
    // loadCart : Function
    return obj;
})();


// *****************************************
// Triggers / Events
// ***************************************** 
// Add item
$(document).on("click", '.add-to-cart', function (event) {
    event.preventDefault();
    var id = $(this).data('id');
    var name = $(this).data('name');
    var price = new Decimal($(this).data('price').toString());
    var image = $(this).data('image-icon');
    var totalQuantity = $(this).data('quantity');
    var priceFrom = new Decimal($(this).data('price-from').toString());
    var unitPrice = new Decimal($(this).data('unit-price').toString());
    var ret = shoppingCart.addItemToCart(id, name, price, 1, image, totalQuantity, priceFrom, unitPrice);
    if(ret===true)
        displayCart();
});

$(document).on("click", '.remove-to-cart', function (event) {
    var id = $(this).data('id');
    var price = new Decimal($(this).data('price').toString());
    shoppingCart.removeItemFromCart(id, price);
    displayCart();
});

// Clear items
$('.clear-cart').click(function () {
    shoppingCart.clearCart();
    displayCart();
});


function displayCart() {
    var cartArray = shoppingCart.listCart();

    //var output = "";
    //for (var i in cartArray) {
    //    output += "<tr>"
    //        + "<td>" + cartArray[i].name + "</td>"
    //        + "<td>(" + cartArray[i].price + ")</td>"
    //        + "<td><div class='input-group'><button class='minus-item input-group-addon btn btn-primary' data-name=" + cartArray[i].name + ">-</button>"
    //        + "<input type='number' class='item-count form-control' data-name='" + cartArray[i].name + "' value='" + cartArray[i].count + "'>"
    //        + "<button class='plus-item btn btn-primary input-group-addon' data-name=" + cartArray[i].name + ">+</button></div></td>"
    //        + "<td><button class='delete-item btn btn-danger' data-name=" + cartArray[i].name + ">X</button></td>"
    //        + " = "
    //        + "<td>" + cartArray[i].total + "</td>"
    //        + "</tr>";
    //}
    //$('.show-cart').html(output);
    //$('.total-cart').html(shoppingCart.totalCart());
    //$('.total-count').html(shoppingCart.totalCount());
    var html = "";
    for (var i in cartArray) {
        html += "<li>";
        html += "<div class='media'>";
        html += "<img class='img-radius' id='img-" + cartArray[i].id + "' src='" + cartArray[i].image + "' alt='Generic placeholder image'>";
        html += "<div class='media-body'>";
        html += "<p><strong>" + cartArray[i].name + "</strong></p>";
        html += "<p>Qtd: " + cartArray[i].count + "</p>";
        html += "<p>Valor: " + Intl.NumberFormat('pt-br', { style: 'currency', currency: 'BRL' }).format(cartArray[i].unitPrice) + "</p>";
        html += "<p>Total: " + Intl.NumberFormat('pt-br', { style: 'currency', currency: 'BRL' }).format(cartArray[i].price);
        html += "<div class='task-board m-0 float-right'>";
        html += "<div class='i-main'>";
        html += "<img src='../../img/remove.png' style='height: 32px; width: 32px;' class='i-block remove-to-cart' data-id='" + cartArray[i].id + "' data-price='" + cartArray[i].unitPrice.toString().replace(",", ".") + "' data-price-from='" + cartArray[i].price.toString().replace(",", ".") + "' data-unit-price='" + cartArray[i].unitPrice.toString().replace(",", ".") + "' data-clipboard-text='feather icon-plus' data-filter='icon-plus' data-toggle='tooltip' title='' style='width: 50px;height: 50px;' data-original-title='Remover' />";
        html += "<img src='../../img/add.png' style='height: 32px; width: 32px;' class='i-block add-to-cart' data-quantity='" + cartArray[i].totalQuantity + "' data-id='" + cartArray[i].id + "' data-price='" + cartArray[i].unitPrice.toString().replace(",", ".") + "' data-name='" + cartArray[i].name + "' data-image-icon='" + cartArray[i].image + "' data-price-from='" + cartArray[i].price.toString().replace(",", ".") + "' data-unit-price='" + cartArray[i].unitPrice.toString().replace(",", ".") + "' data-clipboard-text='feather icon-plus' data-filter='icon-plus' data-toggle='tooltip' title='' style='width: 50px;height: 50px;' data-original-title='Adicionar' />";
        html += "</div>";
        html += "</div>";
        html += "</p>";
        html += "</div>";
        html += "</div>";
        html += "</li>";
        $("#img-" + cartArray[i].id).attr("src", cartArray[i].image);
    }

    //$('#cartDropDown').modal();
    $('#cartDropDownMenu').show();
    //$('#cartDropDown').addClass('show');
    //$('#cartDropDownMenu').addClass('show');
    //$("#dropLink").attr("aria-expanded", "true");
    $("#cartUl").html(html);
    $(".total-cart").html(shoppingCart.totalCart());
}

//function hideCart() {
//    $('#cartDropDown').removeClass('show');
//    $('#cartDropDownMenu').removeClass('show');
//    $("#dropLink").attr("aria-expanded", "false");
//}

// Delete item button

$('.i-main').on("click", ".delete-item", function (event) {
    var id = $(this).data('id')
    shoppingCart.removeItemFromCartAll(id);
    displayCart();
})


// -1
$('.i-main').on("click", ".minus-item", function (event) {
    var id = $(this).data('name')
    shoppingCart.removeItemFromCart(id);
    displayCart();
})
// +1
$('.show-cart').on("click", ".plus-item", function (event) {
    var id = $(this).data('id')
    shoppingCart.addItemToCart(id);
    displayCart();
})

// Item count input
$('.show-cart').on("change", ".item-count", function (event) {
    var id = $(this).data('id');
    var count = Number($(this).val());
    shoppingCart.setCountForItem(id, count);
    displayCart();
});


//displayCart();
