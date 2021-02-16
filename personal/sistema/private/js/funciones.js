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

function onlyDigits(e) {
    return (e.charCode >= 48 && e.charCode <= 57)
}

function onlyLetters(e) {
    var charCode = e.which || e.keyCode;
    var charStr = String.fromCharCode(charCode);
    return /[A-zÀ-ú]/i.test(charStr) || e.charCode == 32;
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

function processCSV(data, skipHeader, onlyFirstSheet) {
    var workbook = XLSX.read(data, {
        type: 'binary'
    });

    var result = {};
    workbook.SheetNames.forEach(function (sheetName) {
        var roa = XLSX.utils.sheet_to_json(workbook.Sheets[sheetName], {
            range: skipHeader ? 0 : -1,
        });
        if (roa.length) result[sheetName] = roa;
    });
    if (onlyFirstSheet && Object.keys(result).length > 0) {
        return result[Object.keys(result)[0]];
    } else {
        return result;
    }
};

function generateCSV(json) {
    var worksheet = XLSX.utils.json_to_sheet(json);
    return XLSX.utils.sheet_to_csv(worksheet);
}

function s2ab(s) {
    var buf = new ArrayBuffer(s.length);
    var view = new Uint8Array(buf);
    for (var i = 0; i != s.length; ++i) view[i] = s.charCodeAt(i) & 0xFF;
    return buf;
}