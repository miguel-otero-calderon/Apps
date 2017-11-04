var usermodule = (function () {
    var setting = {
        codeuser: "",
        companiesupdate: "",
    };

    var _confirm_delete = function () {
        return confirm("¿Desea eliminar el usuario '" + setting.codeuser + "'?");
    }

    var _companies_update = function () {
        var codigos = "";
        var checkboxes = $("input[name='company']");
        $.each(checkboxes, function (index, checkbox) {
            if (checkbox.checked) {
                codigos = codigos + checkbox.id + ",";
            }
        });
        $(setting.companiesupdate).val(codigos);
        if (codigos !== "")
            return true;           
        else
            return confirm('¿Desea grabar los cambios ,sin elegir una Empresa para el Usuario?');
    }

    return {
        ini: function (parameters) {
            setting.codeuser = parameters.codeuser;
            setting.companiesupdate = "#" + parameters.companiesupdate;
        },
        confirm_delete: function () {
            _confirm_delete();
        },
        companies_update: function () {
            _companies_update();
        },        
    }
})();