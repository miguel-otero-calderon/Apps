var usermodule = (function () {
    var setting = {
        codeuser: "",
        companiessplit: "",
    };

    var _confirm_delete = function () {
        return confirm("¿Desea eliminar el usuario '" + setting.codeuser + "'?");
    }

    var _companies_split = function () {
        var companiessplit = "";
        var checkboxes = $("input[name='company']");
        $.each(checkboxes, function (index, checkbox) {
            if (checkbox.checked)
                companiessplit = companiessplit + checkbox.id.replace("CodeCompany-", "") + ",";
        });
        $(setting.companiessplit).val(companiessplit);
        if (companiessplit !== "")
            return true;           
        else
            return confirm('¿Desea grabar los cambios ,sin elegir una Empresa para el Usuario?');
    }

    return {
        ini: function (parameters) {
            setting.codeuser = parameters.codeuser;
            setting.companiessplit = "#" + parameters.companiessplit;
        },
        confirm_delete: function () {
            _confirm_delete();
        },
        companies_split: function () {
            _companies_split();
        },        
    }
})();