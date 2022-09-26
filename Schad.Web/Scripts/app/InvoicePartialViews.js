 
  jQuery(document).ready(function () {
        $('.dropdown-toggle').dropdown()
  });

var Invoice = (function () {


    var _main = function (response) {
        $('#main').html(response);
    };

    var _hold = function (response) {
        $('#holdList').html(response);
        if (response !== "") {
            $('#holdNameBtn').hide();
            $('#holdListBtn').removeClass('d-none');
            $('#holdListBtn').addClass('d-block');
        }
    };

    var _ajax = function (url, data, target) {
        $.ajax({
            type: "POST",
            url: url,
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: target,
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    };




    var index = function (title, search = true) {
        $('#main').html('');
        var url = "../invoice/Index";
        var data = '{name: "' + title + '", search: ' + search +' }';
       
        $('#main').html('Loading...');
        _ajax(url, data, _main);

    };
    var CreateInvoice = function CreateInvoice() {
        var title = "";
        var url = "../invoice/Index";
        var data = '{name: "' + title + '" }';
        $('#main').html('Loading...');
        _ajax(url, data, _main);
    };


    var payment = function () {
        var total = $('#pay-total').html().slice(1);
        if (Number(total) > 0) {
            var url = "../invoice/Payment";
            var data = null;
            $('#main').html('Loading...');
            _ajax(url, data, _main);
        } else {
            alert('Nothing to pay');
        }
    };

   


    var _ajaxget = function (url, data, target) {
        $.ajax({
            type: "GET",
            url: url,
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: target,
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    };


    var _departments = function (url) {

        
      
            $("#departments-modal-content").html("");
            $(".modal").on("hidden.bs.modal", function () {
                $(this).removeData();
            });
        
        $("#departments-modal-content").load(url);

        document.getElementById("departments").style.display = "block";
        $('#departments').modal({ backdrop: 'static', keyboard: false, show: true });
        $('#departments').modal('toggle');
        $('#departments').modal('show');

    };

    var departmentEdit = function (title) {
        var url = "../Departments/Edit/" + title;
        var data = title;

        _ajaxget(url, data, _departments(url));
    };
  
    
    return {
        index: index,
        payment: payment,
        CreateInvoice: CreateInvoice,
        

    };
})();

