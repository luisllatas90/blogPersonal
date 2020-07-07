var nav1 = window.Event ? true : false;

function solonumerosentero(evt) {
    // Backspace = 8, Enter = 13, '0' = 48, '9' = 57, '.' = 46
    var key = nav1 ? evt.which : evt.keyCode;
    return (key <= 13 || (key >= 48 && key <= 57));
}

// #001 - JR
function rangoanio(ini, fin) {
    var i = 0;
    var t = '';
    t += '<option value="0">Seleccione</option>';
    for (i = fin; i >= ini; i--) {
        t += '<option value="' + i + '">' + i + '</option>';
    }
    return t;
}