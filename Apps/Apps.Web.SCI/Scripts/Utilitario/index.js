$(document).ready(function () {      
    $("#date1").val(obtener_fecha_hoy());
    $("#error").val("");

})

var obtener_fecha_hoy = function() {
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

var validar_fecha = function (fecha) {
    try
    {
        if (validate_fecha_formato_ingles(fecha))
            return true;
        else
            return false;
    }
    catch (err)
    {
        return false;
    }

}

var validar_load_files = function () {
    var texto;
    var value;
    var file;

    $("#error").html("");
    texto = $("#date1").val();
    value = validar_fecha(texto);
    if (value == false)
    {
        texto = "Digite una fecha con el formato 'yyyy-mm-dd'.";
        $("#error").html(texto);
        $("#date1").focus();
        return false;
    }

    if (validate_file("file1") == false)
        return false;

    if (validate_file("file2") == false)
        return false;

    return true;
}

function isValidDate(day, month, year) {
    var dteDate;
    month = month - 1;
    dteDate = new Date(year, month, day);
    return ((day == dteDate.getDate()) && (month == dteDate.getMonth()) && (year == dteDate.getFullYear()));
}

function validate_fecha_formato_ingles(fecha) {
    var patron = new RegExp("^(19|20)+([0-9]{2})([-])([0-9]{1,2})([-])([0-9]{1,2})$");

    if (fecha.search(patron) == 0) {
        var values = fecha.split("-");
        if (isValidDate(values[2], values[1], values[0])) {
            return true;
        }
    }
    return false;
}

function validate_file(file_id) {
    var texto;
    var file = $("#" + file_id);
    var date = $("#date1").val();
    if (file.val() == "")
    {
        texto = "Falta seleccionar el " + file_id;
        $("#error").html(texto);
        $(file).focus();
        return false;
    }
    else {
        texto = file.val();
        if (texto.endsWith(date + ".csv"))
            return true;
        else {
            texto = "Nombre de archivo incorrecto...Porqye no termina en '" + date + "'";
            $("#error").html(texto);
            $(file).focus();
            return false;
        }
    }
}