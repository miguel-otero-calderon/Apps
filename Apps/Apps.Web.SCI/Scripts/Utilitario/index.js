$(document).ready(function () {  
    var fecha = "";
    fecha = obtener_fecha_hoy();
    $("#date1").val(fecha);
})

function obtener_fecha_hoy() {
    var f = new Date();
    var day = f.getDate();
    var month = f.getMonth() + 1;
    var year = f.getFullYear();
    var fecha = "";

    if(day < 10)
        day = "0" + day;

    if(month < 10)
        month = "0" + month;

    fecha = year + "-" + month + "-" + day;

    return fecha;

}