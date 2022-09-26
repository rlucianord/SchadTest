//import { Modal } from "../bootstrap.bundle";


var Inventory = (function () {

    var _getFormData = function (unindexed_array) {
        var indexed_array = {};

        $.map(unindexed_array, function (n, i) {
            indexed_array[n['name']] = n['value'];
        });

        return indexed_array;
    };

    var _main = function (response) {
        $('#main').html(response);
    };

    var _modal = function (response) {
        $('#inventory-modal-body').html(response);
        $('#inventory-modal').modal({ backdrop: 'static', keyboard: false, show: true });
        document.getElementById("inventory-modal").style.display = "block";
       
        $('#inventory-modal').modal('toggle');
        $('#inventory-modal').modal('show');

    };

    var _postData = function (response) {
            
        if (response === "") {
            alert("Successful operation!");
            return true
        //    var r = confirm("Do yow want to reload page?!");
        //    if (r === true) {
        //        this.window.location.reload();
        //    }
          
        }
        else {
            alert("Operation Fails it is the product already exist!");
            console.Write(response);
            return false;
            //$('#inventory-modal').modal('hide');
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
                alert("Successful operation!");
            },
            error: function (response) {
                alert('Operation fail!');

            }
        });
    };

            var _ajaxGet = function (url, data, target) {
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

    var index = function (title, newItem = false) {
        var url = "../Inventory/Index";
        var data = '{name: "' + title + '" }';
        $('#main').html('Loading...');
        _ajax(url, data, _main);
        if (newItem) {
            create();
        }
    };
    var loaddepartment = function (title) {
        var url = "../Inventory/Loaddepartment";

        var data = '{name: "' + title + '" }';
        $('#main').html('Loading...');
        _ajax(url, data, _main);
    };

    var create = function () {
        var url = "../Inventory/Create";
        var data = null;
        _ajaxGet(url, data, _modal);
    };



    var edit = function (id) {
        var url = "../Inventory/Edit/" + id;
        var data = null;
        _ajaxGet(url, data, _modal);
    };

    var createPost = function (event) {
        event.preventDefault();
        var formData = $('#CreateForm').serializeArray();
        var url = "../Inventory/Create";
        var data = JSON.stringify(_getFormData(formData));
        _ajax(url, data, _postData);
        return false;
    };
    var exit = function () {
        //event.preventDefault();
       
        var url = "../options";
		 window.location=url;
        //var data = JSON.stringify(_getFormData(formData));
       // _ajax(url, data, _postData);
        return false;

    };
    var editPost = function (event) {
        event.preventDefault();
        var formData = $('#EditForm').serializeArray(); 
        var url = "../Inventory/Edit";
        var data = JSON.stringify(_getFormData(formData));

        _ajax(url, data, _postData);
        return false;
    };

    var restock = function (title) {
        var url = "../Inventory/InventoryStocks";
        var data = '{name: "' + title + '" }';
        $('#main').html('Loading...');
        _ajaxGet(url, data, _main);
    }

    var addRestock = function (title) {
        event.preventDefault();
        var formData = $('#RestockForm').serializeArray();
        var url = "../Inventory/InventoryAllStocks";
        var data = JSON.stringify(_getFormData(formData));

        _ajax(url, data, _postData);
        return false;
    }

    var addSpeRestock = function () {
        var inputs = $('.restock-value');
        var filterInputs = inputs.filter(function () { return this.value > 0 });
        if (filterInputs.length != 0) {
            var data = new Array();
            filterInputs.map(function () {
                data.push({ id: this.id, value: Number(this.value) });
            });

            var url = "../Inventory/InventoryStocks";
            var data = JSON.stringify(data);
            _ajax(url, data, _postData);
        } else {
            alert("Items must have a quantity to restock");
        }
    }

    return {
        index: index,
        create: create,
        edit: edit,
        createPost: createPost,
        editPost: editPost,
        exit: exit,
        loaddepartment: loaddepartment,
        restock: restock,
        addRestock: addRestock,
        addSpeRestock: addSpeRestock
    };
})();