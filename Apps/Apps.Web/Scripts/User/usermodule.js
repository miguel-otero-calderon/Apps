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
                companiessplit = companiessplit + checkbox.id + ",";
        });
        $(setting.companiessplit).val(companiessplit);
        if (companiessplit !== "")
            return true;           
        else {
            alert('¿Debe seleccionar una Empresa?');
            return false;
        }

    }

    return {
        ini: function (parameters) {
            setting.codeuser = parameters.codeuser;
            setting.companiessplit = "#" + parameters.companiessplit;
        },
        confirm_delete: function () {
            return _confirm_delete();
        },
        companies_split: function () {
            return _companies_split();
        },        
    }
})();