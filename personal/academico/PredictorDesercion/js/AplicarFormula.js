$(document).ready(function() {
    fnListarFacultad();
    $("#txtFecha").jqxDateTimeInput({ width: '200px', height: '25px' });
    $("#cboFacultad").on("change", function() {
        fnListarEscuela($(this).val());
    });
    $("#cboEscuela").on("change", function() {
        //  fnDataAdpterGrid($(this).val());
        $("#jqxgrid").jqxGrid('clearfilters');
        fnCargarGrid($(this).val());
    });

    fnCargarGrid(0);
    fnFormula();
    $("#jqxConsultar").on('click', function() {
       
        fnCargarGrid(fnCargarGrid($("#cboEscuela").val()));
    });
    // var data = generatedata(20000);
fnExportar();
});

function fnFormula() {
    $.ajax({
        type: "POST",
        dataType: "html",
        url: "../../DataJson/PredictorDesercion/FormulasDesercion.aspx",
        data: { "Funcion": "Listar", 'CodigoCac': 1,'op':"2"},
        dataType: "json",
        cache: false,
        success: function(data) {
            var row = 0;
            //  alert(data.length);
            var str = '';
             console.log(data);
          
           
            jQuery.each(data, function(i, val) {
                var docente = '';
                $("#cboFormula").html("");
                str += '<option value="' + val.CodigoFml + '">' + val.NombreFml + '</option>';                                            
                row++;
            });
            
            $("#cboFormula").html(str);
            ///  oTable.fnOpen(nTr, sOut, 'details');

        },
        error: function(result) {
            sOut = '';
            console.log(result);
            // oTable.fnOpen(nTr, sOut, 'details');
            // location.reload();
        }
    });
}
function fnDetalle(codigo) {
    fnEditarDetalleAlumno(codigo);
    $('#mdRegistro').css("z-index", "400");
    $("#modDetalle").modal('show');
//               $("modDetalle").modal('show');
}
function fnCargarGrid(codigo) {
    var exampleTheme = 'light';
   
    var adapter = fnDataAdpterGrid(codigo);


    var cellsrenderer = function(row, column, value) {
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
    return '<button class="btn btn-success" onclick="fnDetalle(\'' + dataRecord.Id + '\');">Detalle</button>';
    }
    $("#jqxgrid").jqxGrid(
            {
                width: '100%',
                source: adapter,
                filterable: true,
                sortable: true,
                theme: 'light',
                pageable: true,
                height: 600,
                pagesize: 50,
                pagesizeoptions: ['50', '100', '500'],
                autoheight: true,
                ready: function() {
                    $("#jqxgrid").jqxGrid('applyfilters');
                },
                autoshowfiltericon: true,
//                columnmenuopening: function(menu, datafield, height) {
//                    var column = $("#jqxgrid").jqxGrid('getcolumn', datafield);
//                    if (column.filtertype === "custom") {
//                        menu.height(155);
//                        setTimeout(function() {
//                            menu.find('input').focus();
//                        }, 25);
//                    }
//                    else menu.height(height);
//                },

                columns: [

                  { text: 'Id', datafield: 'Id', filtertype: "checkedlist", width: 100 },
                  { text: 'Codigo', datafield: 'Codigo', width: 100 },
                  { text: 'Alumno', datafield: 'Alumno', width: 450 },
                  { text: 'Probabilidad', datafield: 'Probabilidad', filtertype: 'checkedlist', width: 100 } ,
                   { text: 'Estado', datafield: 'Estado', filtertype: 'checkedlist', width: 100 } ,
                  // { text: '', cellsrenderer: cellsrenderer, width: 90 },
                   { text: '', cellsrenderer: cellsrenderer, width: 90 }
                ]
            });



    

   
     
 
   
    
    
}
function fnDataAdpterGrid(codigo) {
    ///  alert(1);
    var source =
    {
        datatype: "json",
        type: "POST",
        datafields: [
            { name: 'Id', type: 'string' },
            { name: 'Codigo', type: 'string' },
            { name: 'Alumno', type: 'number' },
            { name: 'Probabilidad', type: 'string'},
            { name: 'Estado', type: 'string' }],
        root: 'rows',
        url: "../../DataJson/PredictorDesercion/AplicarFormula.aspx",
        data: {
            "Funcion": "Alumnos",
            "Codigo": codigo,
            "CodigoFml":$("#cboFormula").val(),
            "Fecha": $("#txtFecha").val()
        }
    };
    var adapter = new $.jqx.dataAdapter(source);
    return adapter;
}
function fnListarFacultad() {
    var str = '';
    $.ajax({
        type: "POST",
        dataType: "html",
        url: "../../DataJson/PredictorDesercion/GenerarMatrizCSV.aspx",
        data: { "Funcion": "Facultad" },
        dataType: "json",
        cache: false,
        success: function(data) {
            $("#cboFacultad").html("");

            jQuery.each(data, function(i, val) {
                str += '<option value="' + val.Value + '">' + val.Label + '</option>';
            });
            $("#cboFacultad").html(str);


        },
        error: function(result) {
            sOut = '';
            console.log(result);
            // oTable.fnOpen(nTr, sOut, 'details');
            // location.reload();
        }
    });
}

function fnEditarDetalleAlumno(codigo) {
    var str = '';
    var row = $('#jqxgrid').jqxGrid('selectedrowindex');
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
    $.ajax({
        type: "POST",
        dataType: "html",
        url: "../../DataJson/PredictorDesercion/AplicarFormula.aspx",
        data: { "Funcion": "Detalle", "Codigo": codigo },
        dataType: "json",
        cache: false,
        success: function(data) {
        jQuery.each(data, function(i, val) {
        $("#tituloFicha").html("FICHA DEL ESTUDIANTE " + " " + dataRecord.Alumno + " " + dataRecord.Codigo);
                $("#DESERTOR").val(val.DESERTOR);
                $("#NMATRIC").val(val.NMATRIC);
                $("#CICLOINGRESO").val(val.CICLOINGRESO);
                $("#DISCONTINUO").val(val.DISCONTINUO);
                $("#NIVELENTRADA").val(val.NIVELENTRADA);
                $("#TASAFALT").val(val.TASAFALT);
                $("#TASAPRES").val(val.TASAPRES);
                $("#TASAAAPRO").val(val.TASAAAPRO);
                $("#TASADESA").val(val.TASADESA);
                $("#DEUDASPAGA").val(val.DEUDASPAGA);
                $("#DEUDASATRASADAS").val(val.DEUDASATRASADAS);
                $("#DEUDASPEND").val(val.DEUDASPEND);
                $("#EXAMENTRA").val(val.EXAMENTRA);
                $("#VOCACIONAL").val(val.VOCACIONAL);
                $("#ANCIEDAD").val(val.ANCIEDAD);
                $("#DEPRESION").val(val.DEPRESION);
                $("#AUTOEFICACIA").val(val.AUTOEFICACIA);
                $("#TRABAJA").val(val.TRABAJA);
                $("#DEPENDIENTES").val(val.DEPENDIENTES);
                $("#PECONOM").val(val.PECONOM);
                $("#PSALUD").val(val.PSALUD);
                $("#PACADEM").val(val.PACADEM);
                $("#EDAD").val(val.EDAD);
                $("#FNACIM").val(val.FNACIM);
                $("#PROMEPOND").val(val.PROMEPOND);
                $("#NOTASAPRO").val(val.NOTASAPRO);
                $("#NOTASDESAP").val(val.NOTASDESAP);
                $("#TOTALNOTAS").val(val.TOTALNOTAS);
            });


        },
        error: function(result) {
            sOut = '';
            console.log(result);
            // oTable.fnOpen(nTr, sOut, 'details');
            // location.reload();
        }
    });
}

function fnListarEscuela(codigo) {
    var str = '';
    $.ajax({
        type: "POST",
        dataType: "html",
        url: "../../DataJson/PredictorDesercion/GenerarMatrizCSV.aspx",
        data: { "Funcion": "Escuela", "Codigo": codigo },
        dataType: "json",
        cache: false,
        success: function(data) {
            $("#cboEscuela").html("");

            jQuery.each(data, function(i, val) {
                str += '<option value="' + val.Value + '">' + val.Label + '</option>';
            });
            $("#cboEscuela").html(str);


        },
        error: function(result) {
            sOut = '';
            console.log(result);
            // oTable.fnOpen(nTr, sOut, 'details');
            // location.reload();
        }
    });
}

function fnExportar() {
    $("#btnListar").on("click", function() {
       
        //console.log(HideColumns);
        // $("#jqxgrid").jqxGrid('exportdata', 'csv', 'jqxGrid');
        var griddata = $('#jqxgrid').jqxGrid('getdatainformation');
        //  var gridColumn = $('#jqxgrid').jqxGrid('iscolumnvisible', 'columndatafield');
        //console.log(gridColumn);
        //alert(griddata.rowscount);
        var rows = [];
        var rowCount = 0;
        for (var i = 0; i < griddata.rowscount; i++) {
            var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', i);
            rows.push(dataRecord);
            rowCount = rowCount + 1;
            // console.log(rowCount);
            if (rowCount == griddata.rowscount) {


                //  console.log(jsData);
                var dat = setTimeout(function() {

                    //DownloadJSON2CSV(JSON.stringify(rows));
                    //          JSONToCSVConvertor(JSON.stringify(rows), "Matriz de Conocimiento Alumnos", true, HideColumns);
                }, 4000);
                //                JSONToCSVConvertor(JSON.stringify(rows), "Matriz de Conocimiento Alumnos", true);
            }
        }

        var dat = setTimeout(function() {
            JSONToCSVConvertor(JSON.stringify(rows), "Matriz de Conocimiento Alumnos", true);
        }, 8000);

        //        console.log(JSON.parse(rows));
        //        setTimeout(function() {
        //            JSONToCSVConvertor(JSON.stringify(rows), "Matriz de Conocimiento Alumnos", true);
        //        }, 4000);

        //console.log(JSON.stringify(rows));
    });

}
function JSONToCSVConvertor(JSONData, ReportTitle, ShowLabel) {
//    var key;
//    var obj = {};
//    var i = 0;
//    for (var idx in JSONData) {
//        key = JSONData[idx];
//        delete key+'.2017-I';
//        obj[i] = key;

//        i++;
//    }
//    console.log(obj);
   // delete JSONData.M2010_I;
    //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
    var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;
   //delete arrData.M2010_I;
  // arrData= eliminarPorName(arrData, 'M2010_I');

               
  
  
  
   // console.log(arrData);
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

    //Initialize file format you want csv or xls
    var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);
    
    var dat = setTimeout(function() {
    downloadWithName(uri, "Data.csv");
        
    }, 3000);
    // Now the little tricky part.
    // you can use either>> window.open(uri);
    // but this will not work in some browsers
    // or you will not get the correct file extension    

    //this trick will generate a temp <a /> tag
//    var link = document.createElement("a");
//    link.href = uri;

//    //set the visibility hidden so it will not effect on your web-layout
//    link.style = "visibility:hidden";
//    link.download = fileName + ".csv";

//    //this part will append the anchor tag and remove it after automatic click
//    document.body.appendChild(link);
//    link.click();
//    document.body.removeChild(link);
}
function DownloadJSON2CSV(objArray) {
   
    var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;
    var str = '';

    for (var i = 0; i < array.length; i++) {
        var line = '';
        for (var index in array[i]) {
            if (line != '') line += ','

            line += array[i][index];
        }

        str += line + '\r\n';
    }

   // var file = 'data:application/octet-stream;base64,' + str;
    var uri = 'data:text/csv;charset=utf-8,' + escape(str);
    downloadWithName(uri, "datos.csv");

    if (navigator.appName != 'Microsoft Internet Explorer') {
        //alert(navigator.appName);
        console.log(str);
        window.open('data:text/csv;charset=utf-8,' + escape(str));
    }
    else {
        var popup = window.open('', 'csv', '');
        popup.document.body.innerHTML = '<pre>' + str + '</pre>';
    }
}
function downloadWithName(uri, name) {
    var link = document.createElement("a");
    link.download = name;
    link.href = uri;
    link.click();
}
    // alert(link);
