var companymodule = (function () {
    var setting = {
        codecompany: ""
    };

    var _get_selected = function () {
        var radiobuttons = $("input[name='company']");
        $(setting.codecompany).val("");
        $.each(radiobuttons, function (index, radiobutton)
        {
            if (radiobutton.checked) {
                $(setting.codecompany).val(radiobutton.id);
            }                
        });
    }

    var _validate_company_selected = function () {
        _get_selected();
        
        if ($(setting.codecompany).val() === "") {
            alert('¿Debe seleccionar una Empresa?');
            return false;
        }
        else {
            return true;
        }
    }

    return {
        ini: function (parameters) {
            setting.codecompany = "#" + parameters.codecompany;
        },
        validate_company_selected: function () {
            return _validate_company_selected();
        },
    }
})();