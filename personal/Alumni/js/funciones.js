var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};

function soloNumeros(e) {
    return (e.charCode >= 48 && e.charCode <= 57)
}

function soloNumerosDecimal(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode != 8 && unicode != 46) {
        if (unicode < 48 || unicode > 57) //if not a number
        { return false } //disable key press    
    }
}

function soloNumerosHora(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode != 8 && unicode != 58) {
        if (unicode < 48 || unicode > 57) //if not a number
        { return false } //disable key press    
    }
}

function soloNumerosFecha(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode != 8 && unicode != 47) {
        if (unicode < 48 || unicode > 57) //if not a number
        { return false } //disable key press    
    }
}

function soloLetras(e) {
    var charCode = e.which || e.keyCode;
    var charStr = String.fromCharCode(charCode);
    return /[A-zÀ-ú]/i.test(charStr) || e.charCode == 32;
}

function deleteAllCookies(doc) {
    var cookies = doc.cookie.split(";");

    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i];
        var eqPos = cookie.indexOf("=");
        var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
        doc.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
    }
}

function readCookie(doc, name) {
    var nameEQ = name + "=";
    var ca = doc.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function floatNumber(event, element, decimals) {
    var selection = window.getSelection();
    var valElement = element.value;
    var valSelection = selection.toString();

    if (valSelection != '') {
        valElement = valElement.replace(valSelection, '');
    }
    var result = true;
    if ((event.which != 46 || valElement.indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        if (event.which != 46) {
            console.log('No se pueden ingresar letras');
        }
        if ((valElement.indexOf('.') != -1)) {
            console.log('Ya se ha ingresado el punto decimal');
        }
        result = false;
    }

    var positionCursor = event.target.selectionStart;
    var positionDecimal = valElement.indexOf('.');
    if (valElement.indexOf(".") > -1) {
        if ((positionCursor > positionDecimal) && (valElement.split('.')[1].length > (decimals - 1))) {
            return false;
        }
    }

    return result;
}