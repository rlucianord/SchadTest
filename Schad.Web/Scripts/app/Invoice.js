var InvoiceLines = (function () {

    var _DominioUrl = "";

    var _RoundNumber = function (num) {
        return num.toFixed(2);
    };

    var _ajax = function (url, data, ajaxFunction) {
        $.ajax({
            type: "POST",
            url: _DominioUrl + url,
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: ajaxFunction,
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
           
        });
        return false;
    };

    var _ajaxLines = function (response) {
        $('#item-invoice').empty();
        var xtemp = $('#template').val();
        var template = jQuery.validator.format($.trim($("#template").val()));
        var subtotal = 0;
        var tax = 0;
        if (response === "[]") {
            $('#holdNameBtn').hide();
            $('#holdListBtn').removeClass('d-none');
            $('#holdListBtn').addClass('d-block');
        } else {
            $('#holdNameBtn').show();
            $('#holdListBtn').removeClass('d-block');
            $('#holdListBtn').addClass('d-none');
        }
        jQuery.each(JSON.parse(response), function (i, val) {
            subtotal = subtotal + val.Subtotal;
            tax = tax + val.TaxAmount;
            $('#item-invoice').append($(template(val.ItemNum, _RoundNumber(val.ItemPrice), val.ItemDescripcion, val.Quantity, _RoundNumber(val.PricePer * val.Quantity))));
        });
        $('#subtotal-money').html('$' + _RoundNumber(subtotal));
        $('#tax-money').html('$' + _RoundNumber(tax));
        $('#pay-total').html('$' + _RoundNumber(subtotal + tax));
    };

    var _ajaxHold = function (response) {
        if (response !== "") {
            alert('Nothing to hold!');
        } else {
            $('#holdNameBtn').hide();
            $('#holdListBtn').removeClass('d-none');
            $('#holdListBtn').addClass('d-block');
            _ajaxLines("[]");
            Invoice.hold();
            clearLines();

        }

    };

    var _ajaxInvoice = function (response) {
        if (response === 'OK' || response === "" || response.length === 2) {
            alert('Payment complete!');
            location.reload();
        } else {
            alert('Something is wrong! ' + response);
        }
    };

    var _changeQuantity = function (idProduct, quantity) {
        if (quantity <= 0) {
            alert("Quantity cannot be less than or equal to 0");
        } else {
            var url = "/Invoice/ChangeQnty";
            var data = '{IdProducto: "' + idProduct + '", quantity: ' + quantity + ' }';
            _ajax(url, data, _ajaxLines);
        }
    };

    var _changeprice = function (idProduct, price) {
        if (price <= 0) {
            alert("Price cannot be less than or equal to 0");
        } else {
            var url = "/Invoice/ChangePrice";
            var data = '{IdProducto: "' + idProduct + '", price: ' + price + ' }';
            _ajax(url, data, _ajaxLines);
        }
    };

    var _changeDiscount = function (idProduct, discount) {
        if (discount < 0) {
            alert("Discount cannot be less than 0");
        } else {
            var url = "/Invoice/ChangeDiscount";
            var data = '{IdProducto: "' + idProduct + '", discount: ' + discount + ' }';
            _ajax(url, data, _ajaxLines);
        }
    };

    var add = function (idProduct, stock) {
        if (stock <= 0) {
            alert("There is no stock of this product, please update.");
        } else {
            var url = "/Invoice/Addlines";
            var data = '{IdProducto: "' + idProduct + '" }';
            _ajax(url, data, _ajaxLines);
        }
    };

    var del = function (idProduct) {
        var url = "/Invoice/DeleLine";
        var data = '{IdProducto: "' + idProduct + '" }';
        _ajax(url, data, _ajaxLines);
    };

    var clearLines = function () {
        var url = "/Invoice/ClearLines";
        var data = null;
        _ajax(url, data, _ajaxLines);
        Invoice.hold();
    };
    var canceldelete = function () {
        location.reload();
    };
    


    var addInvoice = function (print) {
       
        var total = $('#pay-total').html().slice(1);
        if (total > 0) {
            var url = "/Invoice/AddInvoice";
            var customer = $('#CustomerId').val();
            var data = '{AmtChange: ' + $('#atmChangeValue').val() + ', PaymentMethod: ' + $('#PaymentId').val() + ', CustomerId: ' + customer+ ', print: ' + print + ' }';
            _ajax(url, data, _ajaxInvoice);
        } else {
            alert('Nothing to pay');
        }
        return false;
    };

    var addHold = function () {
        var name = $('#holdName').val();
        var url = "../Invoice/AddHold";
        var data = '{name: "' + name + '" }';

        _ajax(url, data, _ajaxHold);

    };

    var showHold = function (holdId) {
        var url = "/Invoice/ShowHold";
        var data = '{holdId: "' + holdId + '" }';
        _ajax(url, data, _ajaxLines);

        $('#holdListmodal').modal('hide');

    };

    var optApply = function (e) {
        var id = $(e).data('id');
        var action = $(e).data('action');
        var data = $('#opt-input').val();
        switch (action) {
            case 'quantity':
                _changeQuantity(id, data);
                break;
            case 'price':
                _changeprice(id, data);
                break;
            case 'discount':
                _changeDiscount(id, data);
                break;
            default:
                alert('Not Found!');
        }
    };

    return {
        add: add,
        del: del,
        addInvoice: addInvoice,
        optApply: optApply,
        clearLines: clearLines,
        //addHold: addHold,
        //showHold: showHold,
        canceldelete: canceldelete
    };
})();