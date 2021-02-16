$(document).ready(function() {
    fnLoading(true);
    ope = fnOperacion(2);
    rpta = fnvalidaSession();

    if (rpta == true) {
        /*fnColumnaSesion(1)
        $("#btnAsignar").attr("style", "display:none")
        $("#btnEnviar").attr("style", "display:none")*/
        fnColumnaSesion(2)
        fnColumnaExpedientes($("#CboSesiones").val(), 2)
    } else {
        window.location.href = rpta
    }
    fnLoading(false);
    $("#jqxlistbox").on('checkChange', function(event) {
        fnColumnaExpedientes($(this).val(), 1)

    })
    $("#btnAsignar").click(function() {
        var codigos = ""
        var rows = $("#jqxgrid1").jqxGrid('selectedrowindexes');
        if (rows.length > 0) {
            if ($("#CboSesiones").val() != "") {

                for (var i = 0; i < rows.length; i++) {
                    if (i == rows.length - 1) {
                        codigos += $('#jqxgrid1').jqxGrid('getcellvalue', rows[i], "cod");
                    } else {
                        codigos += $('#jqxgrid1').jqxGrid('getcellvalue', rows[i], "cod") + ',';
                    }
                }

                MoverAlumnoSesion(codigos, $("#CboSesiones").val(), 1);
                $("#jqxgrid1").jqxGrid("clearselection");
                $("#jqxgrid2").jqxGrid("clearselection");
                fnColumnaExpedientes($("#CboSesiones").val(), 2)
                fnColumnaExpedientes($("#CboSesiones").val(), 3)
            } else {
                fnMensaje("error", "Debe Seleccionar una Sesión para Asignar")
            }
        } else {
            fnMensaje("error", "Debe Seleccionar al Menos Un Egresado para Asignar a la Sesión.")
        }

    })
    $("#btnRegistrar").click(function() {
        //alert($(this).text())
        if ($(this).text().trim() == "Asignar") {
            $("#ListaSesion").hide();
            $("#RegistraSesion").show();
            $(this).text("Volver");
            $(this).removeAttr("class");
            $(this).attr("class", "btn btn-danger");
            fnColumnaSesion(2)
            //            fnColumnaExpedientes(0, 2)
            //            $("#btnExportar").hide();
        } else {
            $("#ListaSesion").show();
            $("#RegistraSesion").hide();
            $(this).text("Asignar");
            $(this).removeAttr("class");
            $(this).attr("class", "btn btn-primary");
            fnColumnaSesion(1)
            $("#jqxgrid").jqxGrid("clear");
            //            $("#btnExportar").show();
        }

    })

    //    $("#btnExportar").click(function() {
    //        //    $("#jqxgrid").jqxGrid('exportdata', 'xls', 'Expedientes');
    //        //        console.log($("#jqxgrid").jqxGrid('exportdata', 'json'));
    //        JSONToCSVConvertor("1", true)
    //    })

    $("#CboSesiones").change(function() {
        $("#jqxgrid1").jqxGrid("clear");
        $("#jqxgrid2").jqxGrid("clear");
        if ($("#CboSesiones").val() == "") {
            $("#lblsesion").html("")
            $("#btnEnviar").attr("style", "display:none")
        } else {
            var sesion = $("#CboSesiones option:selected").text()
            $("#lblsesion").html("Lista de Egresados de la SESIÓN " + sesion.substring(6, sesion.length))
            $("#btnAsignar").removeAttr("style")
            fnColumnaExpedientes($(this).val(), 2)
            fnColumnaExpedientes($(this).val(), 3)
        }

    })
    $("#btnAgregarSesion").click(function() {
        Registrar_Sesion();
    })
    $("#btnEnviar").click(function() {
        if ($("#CboSesiones").val() != "") {
            if (confirm("¿Está seguro que desea enviar a consejo universitario? Luego no podrá agregar ni retirar expedientes de la sesión.")) {
                SolicitarResolucion($("#CboSesiones").val());
            }
        } else {
            fnMensaje("error", "Debe Seleccionar una Fecha de Sesión.")

        }
    })
});

function fnColumnaSesion(tipo) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        $('#frmLista').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        $('#frmLista').append('<input type="hidden" id="hdcod" name="hdcod" value="%" />');
        var form = $("#frmLista").serializeArray();
        //        console.log(form);
        $.ajax({
            type: "POST",
            url: "../../DataJson/GradosyTitulos/SesionConsejoUniversitario.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
                $("form#frmLista input[id=action]").remove();
                $("form#frmLista input[id=hdcod]").remove();
                //                console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                var listSource = [];
                tb += '<option value="" selected="selected">--Seleccione--</option>'
                for (i = 0; i < filas; i++) {
                    listSource.push({ label: data[i].nom, value: data[i].cod, checked: false });
                    tb += '<option value="' + data[i].cod + '">' + data[i].nom + '</option>'
                }
                if (tipo == 1) {  // Para ListBox
                    //                    listSource.splice(0, 0, 'TODOS');
                    $("#jqxlistbox").jqxListBox({ source: listSource, width: 180, height: 450, checkboxes: true });
                }
                if (tipo == 2) {  // Para Combo
                    $("#CboSesiones").html(tb);
                }
                fnLoading(false);
                //fnLoading(false);
            },
            error: function(result) {
                //console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}

function fnColumnaExpedientes(codigos_sesion, tipo) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        var opcion = ope.pcc
        if (tipo == "2") { opcion = ope.psc; }
        var source =
            {

                datatype: "json",
                type: "POST",
                async: false,
                datafields: [
                //                    { name: 'alumno', map: 'm\\:properties>d\\:ShippedDate', type: 'string' },
                //                    { name: 'nom', map: 'm\\:properties>d\\:Freight', type: 'float' },
                //                    { name: 'ShipName', map: 'm\\:properties>d\\:ShipName', type: 'string' },
                //                    { name: 'ShipAddress', map: 'm\\:properties>d\\:ShipAddress', type: 'string' },
                //                    { name: 'ShipCity', map: 'm\\:properties>d\\:ShipCity', type: 'string' },
                    {name: 'cod', type: 'string' },
                    { name: 'Alumno', type: 'string' },
                    { name: 'NroExp', type: 'string' },
                    { name: 'nom_dgt', type: 'string' },
                    { name: 'enviado', type: 'string' }
                ],
                root: 'rows',
                url: "../../DataJson/GradosyTitulos/SesionConsejoUniversitario.aspx",
                data: { "action": opcion, "hdcod": codigos_sesion }

            };
        //        console.log(source);

        var dataAdapter = new $.jqx.dataAdapter(source);
        var cellsrenderer = function(row, column, value) {
            // alert(value);
            var dataRecord = $("#jqxgrid1").jqxGrid('getrowdata', row);
            return '<button class="btn btn-success" title="Agregar" onclick="Mover_Fila(\'' + dataRecord.Alumno + '\',\'' + dataRecord.NroExp + '\',\'' + dataRecord.cod + '\',1);">+</button>';
        }
        var cellsrenderer2 = function(row, column, value) {
            var dataRecord2 = $("#jqxgrid2").jqxGrid('getrowdata', row);

            return '<button class="btn btn-danger" title="Quitar" onclick="Mover_Fila(\'' + dataRecord2.Alumno + '\',\'' + dataRecord2.NroExp + '\',\'' + dataRecord2.cod + '\',2);">-</button>';

        }
        /*var self = this;
        var pagerrenderer = function() {
        var element = $("<div style='margin-left: 10px; margin-top: 5px; width: 100%; height: 100%;'></div>");
        var datainfo = $("#jqxgrid").jqxGrid('getdatainformation');

            var label = $("<div style='font-size: 11px; margin: 2px 3px; font-weight: bold; float: left;'></div>");
        label.text(datainfo.rowscount);
        label.appendTo(element);
        self.label = label;
        return element;
        }
        */
        if (tipo == "1") {
            // create jqxgrid.
            $("#jqxgrid").jqxGrid(
            {
                width: "100%",
                height: 450,
                source: dataAdapter,
                sortable: true,
                filterable: true,
                //pageable: true,
                //pagerrenderer: pagerrenderer,
                ready: function() {
                    // called when the Grid is loaded. Call methods or set properties here.         
                },
                //selectionmode: 'checkbox',
                altrows: true,
                columns: [
                /*{ text: 'Ship Name', datafield: 'ShipName', width: 250 },
                { text: 'Shipped Date', datafield: 'ShippedDate', width: 100, cellsformat: 'yyyy-MM-dd' },
                { text: 'Freight', datafield: 'Freight', width: 150, cellsformat: 'F2', cellsalign: 'right' },
                { text: 'Ship City', datafield: 'ShipCity', width: 150 },*/
                //                {text: 'cod', datafield: 'cod', visible:'false' },
                {text: 'N° Expediente', datafield: 'NroExp', width: '20%' },
                { text: 'Egresado', datafield: 'Alumno', width: '45%' },
                { text: 'Diploma', datafield: 'nom_dgt', width: '35%' },
                ]
            });
            var datainfo = $("#jqxgrid").jqxGrid('getdatainformation');
            $("#num_filas").html(datainfo.rowscount + " Registro(s)")
        };
        if (tipo == "2") {
            // create jqxgrid.
            $("#jqxgrid1").jqxGrid(
            {
                width: "100%",
                height: 450,
                source: dataAdapter,
                sortable: true,
                filterable: true,
                //pageable: true,
                selectionmode: 'checkbox',
                ready: function() {
                    // called when the Grid is loaded. Call methods or set properties here.         
                },
                //                selectionmode: 'checkbox',
                altrows: true,
                columns: [
                /*{ text: 'Ship Name', datafield: 'ShipName', width: 250 },
                { text: 'Shipped Date', datafield: 'ShippedDate', width: 100, cellsformat: 'yyyy-MM-dd' },
                { text: 'Freight', datafield: 'Freight', width: 150, cellsformat: 'F2', cellsalign: 'right' },
                { text: 'Ship City', datafield: 'ShipCity', width: 150 },*/
                {text: 'Egresado', datafield: 'Alumno', width: '66%' },
                { text: 'N° Expediente', datafield: 'NroExp', width: '20%' },
                { text: ' ', datafield: 'enviado', width: '6%', cellsrenderer: cellsrenderer

                }
                ]

            });
            var datainfo = $("#jqxgrid1").jqxGrid('getdatainformation');
            $("#num_filas1").html(datainfo.rowscount + " Registro(s)")
            //            $("#excelExport").jqxButton();

            //            $("#excelExport").click(function() {

            //                console.log($("#jqxgrid").jqxGrid('exportdata', 'xls', 'jqxGrid'));

            //            });
        };
        if (tipo == "3") {
            // create jqxgrid.
            $("#jqxgrid2").jqxGrid(
            {
                width: "100%",
                height: 410,
                source: dataAdapter,
                sortable: true,
                filterable: true,
                //pageable: true,
                ready: function() {
                    // called when the Grid is loaded. Call methods or set properties here.         
                },
                //                selectionmode: 'checkbox',
                altrows: true,
                columns: [
                /*{ text: 'Ship Name', datafield: 'ShipName', width: 250 },
                { text: 'Shipped Date', datafield: 'ShippedDate', width: 100, cellsformat: 'yyyy-MM-dd' },
                { text: 'Freight', datafield: 'Freight', width: 150, cellsformat: 'F2', cellsalign: 'right' },
                { text: 'Ship City', datafield: 'ShipCity', width: 150 },*/
                //                {text: 'cod', datafield: 'cod', visible: 'false' },
                {text: 'Egresado', datafield: 'Alumno', width: '72%' },
                { text: 'N° Expediente', datafield: 'NroExp', width: '20%' },
                 { text: ' ', datafield: 'enviado', width: '8%', cellsrenderer: cellsrenderer2
                     /*createwidget: function(rowindex, column, value, htmlElement) {
                     var button = $("<button class='btn btn-danger'>-</button>");
                     $(htmlElement).append(button);
                     button.click(function(event) {
                     var Alumno = rowindex.bounddata.Alumno;
                     var Nro_exp = rowindex.bounddata.NroExp;
                     var cod = rowindex.bounddata.cod;
                     Mover_Fila(Alumno, Nro_exp, cod, 2)
                          
                     });
                     },
                     initwidget: function(row, column, value, htmlElement) {
                     }*/
                 }
                ]
            });
            var datainfo = $("#jqxgrid2").jqxGrid('getdatainformation');
            $("#num_filas2").html(datainfo.rowscount + " Registro(s)");
            if (dataAdapter.records.length > 0) {
                if (dataAdapter.records[0].enviado == "1") {
                    $("#btnEnviar").attr("style", "display:none")
                    $("#btnAsignar").attr("style", "display:none")
                    $("#jqxgrid1").jqxGrid('hidecolumn', 'enviado');
                    $("#jqxgrid2").jqxGrid('hidecolumn', 'enviado');
                } else {
                    $("#btnEnviar").removeAttr("style")
                    $("#btnAsignar").removeAttr("style")
                }
            } else {
                $("#btnEnviar").attr("style", "display:none")
            }
        };

        fnLoading(false);

    } else {
        window.location.href = rpta
    }
}

function Mover_Fila(Alumno, Nro_Exp, Cod, tipo) {

    //    var datarow = {};
    //    datarow["Alumno"] = Alumno;
    //    datarow["NroExp"] = Nro_Exp;
    //    datarow["cod"] = Cod;
    //    console.log(datarow)

    if (tipo == 1) {
        //        $("#jqxgrid2").jqxGrid('addrow', null, datarow);
        //        $("#jqxgrid1").jqxGrid('deleterow', $("#jqxgrid1").jqxGrid('getrowid', fila));
        //        $("#jqxgrid1").jqxGrid('sortby', 'Alumno', 'asc');
        //        $("#jqxgrid2").jqxGrid('sortby', 'Alumno', 'asc');
        MoverAlumnoSesion(Cod, $("#CboSesiones").val(), 1)
        $("#jqxgrid1").jqxGrid("clearselection");
        $("#jqxgrid2").jqxGrid("clearselection");
        fnColumnaExpedientes($("#CboSesiones").val(), 2)
        fnColumnaExpedientes($("#CboSesiones").val(), 3)
    } else {
        //        $("#jqxgrid1").jqxGrid('addrow', null, datarow);
        //        $("#jqxgrid2").jqxGrid('deleterow', $("#jqxgrid2").jqxGrid('getrowid', fila));
        //        $("#jqxgrid2").jqxGrid('sortby', 'Alumno', 'asc');
        //        $("#jqxgrid1").jqxGrid('sortby', 'Alumno', 'asc');
        MoverAlumnoSesion(Cod, $("#CboSesiones").val(), 0)
        $("#jqxgrid1").jqxGrid("clearselection");
        $("#jqxgrid2").jqxGrid("clearselection");
        fnColumnaExpedientes($("#CboSesiones").val(), 2)
        fnColumnaExpedientes($("#CboSesiones").val(), 3)
    }
}

function MoverAlumnoSesion(Cod_egr, cod_ses, tipo) { //1 agregar ,0 quitar
    if ($("#CboSesiones").val() != "") {
        $("form#frmRegistroSesion input[id=action]").remove();
        $('#frmRegistroSesion').append('<input type="hidden" id="action" name="action" value="' + ope.mov + '" />');
        $('#frmRegistroSesion').append('<input type="hidden" id="hdcod" name="hdcod" value="' + Cod_egr + '" />');
        $('#frmRegistroSesion').append('<input type="hidden" id="hdcods" name="hdcods" value="' + cod_ses + '" />');
        $('#frmRegistroSesion').append('<input type="hidden" id="tipo" name="tipo" value="' + tipo + '" />');
        var form = $("#frmRegistroSesion").serializeArray();
        $("form#frmRegistroSesion input[id=action]").remove();
        $("form#frmRegistroSesion input[id=hdcod]").remove();
        $("form#frmRegistroSesion input[id=hdcods]").remove();
        $("form#frmRegistroSesion input[id=tipo]").remove();
        //$('#hdcod').remove();
        console.log(form);
        $.ajax({
            type: "POST",
            url: "../../DataJson/GradosyTitulos/SesionConsejoUniversitario.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
                //                console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                } else {
                    fnMensaje("error", data[0].msje)
                }
            },
            error: function(result) {
                console.log(result)
                fnMensaje("error", result)
            }
        });
    } else {
        fnMensaje("error", "Debe Seleccionar una Fecha de Sesión.")

    }

}



function Registrar_Sesion() {

    if ($("#txtFecha").val() != "") {
        $("form#frmRegistroSesion input[id=action]").remove();
        $('#frmRegistroSesion').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
        var form = $("#frmRegistroSesion").serializeArray();
        $("form#frmRegistroSesion input[id=action]").remove();
        //$('#hdcod').remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../../DataJson/GradosyTitulos/SesionConsejoUniversitario.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
                //                console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    $("#txtFecha").val("")
                    fnColumnaSesion(2)
                } else {
                    fnMensaje("error", data[0].msje)
                }
            },
            error: function(result) {
                console.log(result)
                fnMensaje("error", result)
            }
        });
    } else {
        fnMensaje("error", "Debe Seleccionar una Fecha de Sesión.")

    }
}

function SolicitarResolucion(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);

        $("form#frmRegistroSesion input[id=action]").remove();
        $('#frmRegistroSesion').append('<input type="hidden" id="action" name="action" value="' + ope.srcu + '" />');
        $('#frmRegistroSesion').append('<input type="hidden" id="hdcod" name="hdcod" value="' + cod + '" />');
        var form = $("#frmRegistroSesion").serializeArray();
        $("form#frmRegistroSesion input[id=action]").remove();
        $("form#frmRegistroSesion input[id=hdcod]").remove();
        //$('#hdcod').remove();
        console.log(form);
        $.ajax({
            type: "POST",
            url: "../../DataJson/GradosyTitulos/SesionConsejoUniversitario.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
                //                console.log(data);
                if (data[0].rpta == 1) {
                    $("#jqxgrid1").jqxGrid("clearselection");
                    $("#jqxgrid2").jqxGrid("clearselection");
                    fnColumnaExpedientes($("#CboSesiones").val(), 2)
                    fnColumnaExpedientes($("#CboSesiones").val(), 3)
                    fnMensaje("success", data[0].msje)

                } else {
                    fnMensaje("error", data[0].msje)
                }
            },
            error: function(result) {
                console.log(result)
                fnMensaje("error", result)
            }
        });

        fnLoading(false);

    } else {
        window.location.href = rpta
    }
}

/*
function JSONToCSVConvertor(ReportTitle, ShowLabel) {
JSONData = $("#jqxgrid").jqxGrid('exportdata', 'json')
//    console.log(JSONData);
var array = typeof objArray != 'object' ? JSON.parse(JSONData) : objArray;
// if ($.browser.msie) {

ieExcel(array)

//} else {

// mozCSV(array)

// }
}
//If JSONData is not an object then JSON.parse will parse the JSON string in an Object
/* var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;
var CSV = '';
//Set Report title in first row or line

//  CSV += ReportTitle + '\r\n\n';

//This condition will generate the Label/Header
if (ShowLabel) {
var row = "";

//This loop will extract the label from 1st index of on array
for (var index in arrData[0]) {

//Now convert each value to string and comma-seprated
row += index + ',';
}

row = row.slice(0, -1);

//append Label row with line break
CSV += row + '\r\n';
}

//1st loop is to extract each row
for (var i = 0; i < arrData.length; i++) {
var row = "";

//2nd loop will extract each column and convert it in string comma-seprated
for (var index in arrData[i]) {
row += '"' + arrData[i][index] + '",';
}

row.slice(0, row.length - 1);

//add a line break after each row
CSV += row + '\r\n';
}

if (CSV == '') {
alert("Invalid data");
return;
}

//Generate a file name
var fileName = "Alumnos_";
//this will remove the blank-spaces from the title and replace it with an underscore
fileName += ReportTitle.replace(/ /g, "_");

//    if (navigator.appName == "Microsoft Internet Explorer") {
var oWin = window.open();
oWin.document.write('sep=,\r\n' + CSV);
oWin.document.close();
oWin.document.execCommand('SaveAs', true, fileName + ".csv");
oWin.close();
//    }

//Initialize file format you want csv or xls
//    var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);
//    downloadWithName(uri, "Data.csv");

}

function downloadWithName(uri, name) {
var link = document.createElement("a");
link.download = name;
link.href = uri;
link.execCommand('SaveAs', true, 'nombre' + ".xls");
//    link.click();

// alert(link);
}
function JSONtoCSV(){

var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;

if ($.browser.msie) {

ieExcel(array)

} else {

mozCSV(array)

}
}

function mozCSV(array) {
var str = '';

for (var i = 0; i < array.length; i++) {
var line = '';

for (var index in array[i]) {
line += array[i][index] + ',';
}

line.slice(0, line.Length - 1);
str += line + '\r\n';

}

window.open("data:text/csv;charset=utf-8," + escape(str))
}

function ieExcel(array) {

var oExcel = new ActiveXObject("Excel.Application");
//'Add a new workbook.
wbook = oExcel.workbooks.add;
//wbook.Worksheets(1).Activate;
ws = wbook.Worksheets(1);
cell = ws.Cells;

//Your code can be modified as follows
var line = '';

for (var i = 0; i < array.length; i++) {
var line = '';
var j = 1;
for (var index in array[i]) {
cell(i + 1 ,j) = array[i][index];
j++;
}
line.slice(0,line.Length-1);

}

	
//here is a way to do what you've coded in jQuery
$.each(objArray, function(i) {
var col = 1;
var row = i + 1;
$.each(this, function() {
cell(row, col) = this.toString();
col++;
});
});
//' Release control to user.
with (oExcel) {
Range("A1").select
ActiveSheet.UsedRange.EntireColumn.AutoFit
Visible = true;
UserControl = true;
}
}
*/