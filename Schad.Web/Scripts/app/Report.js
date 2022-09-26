
$(document).ready(function () {
    $('#screen').addClass('overhiden');
  

});

$("#btninventoryreport").click(function (evt) {
    
    $("#dialog").ejDialog("open");
});
$(".btnExit").click(function (evt) {


    window.location = "./report";
});
function exit() {

    window.location = "../report";

}


//var Report = (function () {


//    var exit = function (event) {
//        event.preventDefault();
//        var url = "~/Home";
//        var data = JSON.stringify(_getFormData(formData));
//        _ajax(url, data, _postData);
//        return false;
//    };

  
//    var _ajaxGet = function (url, data, target) {
//        $.ajax({
//            type: "GET",
//            url: url,
//            data: data,
//            contentType: "application/json; charset=utf-8",
//            dataType: "html",
//            success: target,
//            failure: function (response) {
//                alert(response.responseText);
//            },
//            error: function (response) {
//                alert(response.responseText);
//            }
//        });
//    };
//      var _modal = function (respose) {
       
//           $('#Reports').modal({ backdrop: 'static', keyboard: false, show: true });
//        document.getElementById("Reports").style.display = "block";
//        $('#Reports-modal-body').html(respose);
//        $('#Reports').modal('show');


//    };

//    var _main = function (response) {

//        $('#main').html(response);

//    };
//    var GetReport = function (report, title) {
//        var url = "./report/InvetoryReport";
//        var data = '{name: "' + title + '" }';
//        //_modal(url);
//      //  $('#Reports-modal-body').html('Loading...');
//        _ajaxGet(url, data, _modal);
//           };
    



//    return {
//        GetReport: GetReport,
//        exit: exit
//    };
//})();