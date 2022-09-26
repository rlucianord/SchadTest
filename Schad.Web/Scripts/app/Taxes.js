

$(".btnCreate").click(function (evt) {

    $('#taxes').modal({ backdrop: 'static', keyboard: false, show: true });
    $('#taxes').modal('show');
    $("#modal-content").load("./taxes/Create");

});


$(".btnedit").click(function (evt) {

    $("#modal-content").load("./taxes/Edit/" + $(this).data("id"));
    $('#taxes').modal({ backdrop: 'static', keyboard: false, show: true });
    document.getElementById("taxes").style.display = "block";
    $('#taxes').modal('show');
});

$(".btnExit").click(function (evt) {


    window.location = "./Options";
});
$(".close").click(function (evt) {


    window.location = "./taxes";
});
//$(".Export").click(function (e) {
//    e.preventDefault();

//    var ex = document.getElementById("Id_Ano");
//    var id = ex.options[ex.selectedIndex].value;
//    var an = document.getElementById("Id_Mes");
//    var id1 = an.options[an.selectedIndex].value;
// //   var id1 = $('#Id_Mes').find(":selected").val();
//    ExportExcel(id, id1)

//});


$(".btnDetalles").click(function (evt) {


    $("#modal-content").load("./taxes/Details/" + $(this).data("id"));
}

);
$(".btnBorrar").click(function (evt) {


    $("#modal-content").load("/taxes/Delete/" + $(this).data("id"));
}
);
//$('.modal-dialog').css('width', '900px');
//$('.modal-dialog').css('margin', '100px auto 100px auto');

function ExportExcel(Ano, Mes) {

    window.open("../Excel/?Id_Ano=" + Ano + "&" + "Id_Mes=" + Mes);

}