var HideColumns = [];

$(document).ready(function() {

    $("#txtFecha").jqxDateTimeInput({ width: '200px', height: '25px' });

    HideColumns = [];
    fnExportar();
    fnListarFacultad();
    $("#cboFacultad").on("change", function() {
        fnListarEscuela($(this).val());
    });
    $("#cboEscuela").on("change", function() {
        //  fnDataAdpterGrid($(this).val());
        $("#jqxgrid").jqxGrid('clearfilters');
        fnCargarGrid($("#cboFacultad").val(), $(this).val());
    });
    fnColumnasGrid();
    fnCargarGrid(0,0);
    // var data = generatedata(20000);

    $("#jqxConsultar").on('click', function() {
        fnCargarGrid($("#cboFacultad").val(), $("#cboEscuela").val());
    });

});

function fnCargarGrid(codigo_fac,codigo_cpf) {
    var exampleTheme = 'light';
    /* var source =
    {
    localdata: data,
    datafields:
    [
    { name: 'firstname', type: 'string' },
    { name: 'lastname', type: 'string' },
    { name: 'productname', type: 'string' },
    { name: 'date', type: 'date' },
    { name: 'quantity', type: 'number' },
    { name: 'price', type: 'number' }
    ],
    datatype: "array"
    };
    */
    var adapter = fnDataAdpterGrid(codigo_fac,codigo_cpf);
    var buildFilterPanel = function(filterPanel, datafield) {
        var textInput = $("<input style='margin:5px;'/>");
        var applyinput = $("<div class='filter' style='height: 25px; margin-left: 20px; margin-top: 7px;'></div>");
        var filterbutton = $('<span tabindex="0" style="padding: 4px 12px; margin-left: 2px;">Fltrar</span>');
        applyinput.append(filterbutton);
        var filterclearbutton = $('<span tabindex="0" style="padding: 4px 12px; margin-left: 5px;">Limpiar</span>');
        applyinput.append(filterclearbutton);

        filterPanel.append(textInput);
        filterPanel.append(applyinput);
        filterbutton.jqxButton({ theme: exampleTheme, height: 20 });
        filterclearbutton.jqxButton({ theme: exampleTheme, height: 20 });

        var dataSource =
                {
                    localdata: adapter.records,
                    datatype: "array",
                    async: false
                }
        var dataadapter = new $.jqx.dataAdapter(dataSource,
                {
                    autoBind: false,
                    autoSort: true,
                    autoSortField: datafield,
                    async: false,
                    uniqueDataFields: [datafield]
                });
        var column = $("#jqxgrid").jqxGrid('getcolumn', datafield);

        textInput.jqxInput({ theme: exampleTheme, placeHolder: "Enter " + column.text, popupZIndex: 9999999, displayMember: datafield, source: dataadapter, height: 23, width: 175 });
        textInput.keyup(function(event) {
            if (event.keyCode === 13) {
                filterbutton.trigger('click');
            }
        });

        filterbutton.click(function() {
            var filtergroup = new $.jqx.filter();

            var filter_or_operator = 1;
            var filtervalue = textInput.val();
            var filtercondition = 'contains';
            var filter1 = filtergroup.createfilter('stringfilter', filtervalue, filtercondition);
            filtergroup.addfilter(filter_or_operator, filter1);
            // add the filters.
            $("#jqxgrid").jqxGrid('addfilter', datafield, filtergroup);
            // apply the filters.
            $("#jqxgrid").jqxGrid('applyfilters');
            $("#jqxgrid").jqxGrid('closemenu');
        });
        filterbutton.keydown(function(event) {
            if (event.keyCode === 13) {
                filterbutton.trigger('click');
            }
        });
        filterclearbutton.click(function() {
            $("#jqxgrid").jqxGrid('removefilter', datafield);
            // apply the filters.
            $("#jqxgrid").jqxGrid('applyfilters');
            $("#jqxgrid").jqxGrid('closemenu');
        });
        filterclearbutton.keydown(function(event) {
            if (event.keyCode === 13) {
                filterclearbutton.trigger('click');
            }
            textInput.val("");
        });
    }

    var renderExamtra = function(element) {
        $(element).jqxTooltip({ position: 'mouse', content: "<div style='text-align:left'>0:BAJO<br/> 1:MEDIO <br/> 2:ALTO</div>" });
    }

    var renderVocacional = function(element) {
        $(element).jqxTooltip({ position: 'mouse', content: "<div style='text-align:left'>  0:SI TIENE PERFIL VOCACIONAL <br/> 1:NO TIENE PERFIL VOCACIONAL </div>" });
    }
    var renderAnciedad = function(element) {
        $(element).jqxTooltip({ position: 'mouse', content: "<div style='text-align:left'>  0:NO TIENE ANSIEDAD <br/> 1:TIENE ANCIEDAD</div>" });
    }
    var renderDepresion = function(element) {
        $(element).jqxTooltip({ position: 'mouse', content: "<div style='text-align:left'>  0:NO TIENE DEPRESION <br/> 1:TIENE DEPRESION</div>" });
    }
    var renderAutoeficacia = function(element) {
        $(element).jqxTooltip({ position: 'mouse', content: "<div style='text-align:left'>  0:SI TIENE AUTOEFICACIA <br/> 1:NO TIENE AUTOEFICACIA </div>" });
    }
    $("#jqxgrid").jqxGrid(
            {
                width: '100%',
                source: adapter,
                filterable: true,
                sortable: true,
                theme: 'light',
                pageable: true,
                ready: function() {
                },
                autoshowfiltericon: true,
                columnmenuopening: function(menu, datafield, height) {
                    var column = $("#jqxgrid").jqxGrid('getcolumn', datafield);
                    if (column.filtertype === "custom") {
                        menu.height(155);
                        setTimeout(function() {
                            menu.find('input').focus();
                        }, 25);
                    }
                    else menu.height(height);
                },

                columns: [
                  {
                      text: 'CODIGOUNIV', datafield: 'CODIGOUNIV', width: 100,
                      filtertype: "custom",
                      createfilterpanel: function(datafield, filterPanel) {
                          buildFilterPanel(filterPanel, datafield);
                      }
                  },
                  { text: 'DESERTOR', datafield: 'DESERTOR', filtertype: "checkedlist", width: 100 },
                  { text: 'NMATRIC', datafield: 'NMATRIC', filtertype: 'checkedlist', width: 100 },
                  { text: 'CICLOINGRESO', datafield: 'CICLOINGRESO', filtertype: 'checkedlist', width: 110 },
                  { text: 'DISCONTINUO', datafield: 'DISCONTINUO', filtertype: 'checkedlist', width: 110 },
                  
                  { text: '2010-I', datafield: 'M2010_I', filtertype: 'checkedlist', width: 55 },
                  { text: '2010-II', datafield: 'M2010_II', filtertype: 'checkedlist', width: 55 },
                  { text: '2011-I', datafield: 'M2011_I', filtertype: 'checkedlist', width: 55 },
                  { text: '2011-II', datafield: 'M2011_II', filtertype: 'checkedlist', width: 55 },
                  { text: '2012-I', datafield: 'M2012_I', filtertype: 'checkedlist', width: 55 },
                  { text: '2012-II', datafield: 'M2012_II', filtertype: 'checkedlist', width: 55 },
                  { text: '2013-I', datafield: 'M2013_I', filtertype: 'checkedlist', width: 55 },
                  { text: '2013-II', datafield: 'M2013_II', filtertype: 'checkedlist', width: 55 },
                  { text: '2014-I', datafield: 'M2014_I', filtertype: 'checkedlist', width: 55 },
                  { text: '2014-II', datafield: 'M2014_II', filtertype: 'checkedlist', width: 55 },
                  { text: '2015-I', datafield: 'M2015_I', filtertype: 'checkedlist', width: 55 },
                  { text: '2015-II', datafield: 'M2015_II', filtertype: 'checkedlist', width: 55 },
                  { text: '2016-I', datafield: 'M2016_I', filtertype: 'checkedlist', width: 55 },
                  { text: '2016-II', datafield: 'M2016_II', filtertype: 'checkedlist', width: 55 },
                  { text: '2017-I', datafield: 'M2017_I', filtertype: 'checkedlist', width: 55 },
                  { text: '2017-II', datafield: 'M2017_II', filtertype: 'checkedlist', width: 55 },
                  { text: 'NIVELENTRADA', datafield: 'NIVELENTRADA', filtertype: 'checkedlist', width: 150 },
                  { text: 'TASAFALT', datafield: 'TASAFALT', filtertype: 'checkedlist', width: 150 },
                  { text: 'TASAPRES', datafield: 'TASAPRES', filtertype: 'checkedlist', width: 150 },
                  { text: 'TASAAAPRO', datafield: 'TASAAAPRO', filtertype: 'checkedlist', width: 150 },
                  { text: 'TASADESA', datafield: 'TASADESA', filtertype: 'checkedlist', width: 150 },
                  { text: 'DEUDASPAGA', datafield: 'DEUDASPAGA', filtertype: 'checkedlist', width: 150 },
                  { text: 'DEUDASPEND', datafield: 'DEUDASPEND', filtertype: 'checkedlist', width: 150 },
                  { text: 'DEUDASATRASADAS', datafield: 'DEUDASATRASADAS', filtertype: 'checkedlist', width: 150 },
                  { text: 'EXAMENTRA', datafield: 'EXAMENTRA', filtertype: 'checkedlist', width: 150, rendered: renderExamtra },
                  { text: 'VOCACIONAL', datafield: 'VOCACIONAL', filtertype: 'checkedlist', width: 150, rendered: renderVocacional },
                  { text: 'ANSIEDAD', datafield: 'ANSIEDAD', filtertype: 'checkedlist', width: 150, rendered: renderAnciedad },
                  { text: 'DEPRESION', datafield: 'DEPRESION', filtertype: 'checkedlist', width: 150, rendered: renderDepresion },
                  { text: 'AUTOEFICACIA', datafield: 'AUTOEFICACIA', filtertype: 'checkedlist', width: 150, rendered: renderAutoeficacia },
                  { text: 'TRABAJA', datafield: 'TRABAJA', filtertype: 'checkedlist', width: 150 },
                  { text: 'DEPENDIENTES', datafield: 'DEPENDIENTES', filtertype: 'checkedlist', width: 150 },
                  { text: 'PROBABILIDAD', datafield: 'PROBABILIDAD', filtertype: 'checkedlist', width: 150 },
                  { text: 'PROBFAMIL', datafield: 'PROBFAMIL', filtertype: 'checkedlist', width: 150 },
                  { text: 'PECONOM', datafield: 'PECONOM', filtertype: 'checkedlist', width: 150 },
                  { text: 'PSALUD', datafield: 'PSALUD', filtertype: 'checkedlist', width: 150 },
                  { text: 'PACADEM', datafield: 'PACADEM', filtertype: 'checkedlist', width: 150 },
                  { text: 'EDAD', datafield: 'EDAD', filtertype: 'checkedlist', width: 150 },
                  { text: 'FNACIM', datafield: 'FNACIM', filtertype: 'date', width: 150, cellsformat: 'dd/MM/yyyy', cellsalign: 'right' },
                  { text: 'SEXO', datafield: 'SEXO', filtertype: 'checkedlist', width: 150 },
                  { text: 'PROMEPOND', datafield: 'PROMEPOND', filtertype: 'checkedlist', width: 150 },
                  { text: 'PROB_APROF', datafield: 'PROB_APROF', filtertype: 'checkedlist', width: 150 },
                  { text: 'PROB_APCURS', datafield: 'PROB_APCURS', filtertype: 'checkedlist', width: 150 },
                  { text: 'NOTASAPRO', datafield: 'NOTASAPRO', filtertype: 'checkedlist', width: 150 },
                  { text: 'NOTASDESAP', datafield: 'NOTASDESAP', filtertype: 'checkedlist', width: 150 },
                  { text: 'TOTALNOTAS', datafield: 'TOTALNOTAS', filtertype: 'checkedlist', width: 150 },
                  { text: 'CICLODEBEESTAR', datafield: 'CICLODEBEESTAR', filtertype: 'checkedlist', width: 150 },
                  { text: 'RETRASO', datafield: 'RETRASO', filtertype: 'checkedlist', width: 150 }
                ]
            });

    

    $('#events').jqxPanel({ width: 300, height: 80 });

    $("#jqxgrid").on("filter", function(event) {
        $("#events").jqxPanel('clearcontent');
        var filterinfo = $("#jqxgrid").jqxGrid('getfilterinformation');

        var eventData = "Triggered 'filter' event";
        for (i = 0; i < filterinfo.length; i++) {
            var eventData = "Columna Filtrada: " + filterinfo[i].filtercolumntext;
            $('#events').jqxPanel('prepend', '<div style="margin-top: 5px;">' + eventData + '</div>');
        }
    });

    $('#clearfilteringbutton').jqxButton({ height: 25 });
    $('#filterbackground').jqxCheckBox({ checked: true, height: 25 });
    $('#filtericons').jqxCheckBox({ checked: false, height: 25 });
    // clear the filtering.
    $('#clearfilteringbutton').click(function() {
        $("#jqxgrid").jqxGrid('clearfilters');
    });
    // show/hide filter background
    $('#filterbackground').on('change', function(event) {
        $("#jqxgrid").jqxGrid({ showfiltercolumnbackground: event.args.checked });
    });
    // show/hide filter icons
    $('#filtericons').on('change', function(event) {
        $("#jqxgrid").jqxGrid({ autoshowfiltericon: !event.args.checked });
    });
}
function fnDataAdpterGrid(codigo_fac,codigo_cpf) {
 //   alert(codigo_cpf);
    var source =
    {
        datatype: "json",
        type: "POST",
        datafields: [
            { name: 'CODIGOUNIV', type: 'string' },
            { name: 'DESERTOR', type: 'string' },
            { name: 'NMATRIC', type: 'number' },
            { name: 'CICLOINGRESO', type: 'string' },
            { name: 'DISCONTINUO', type: 'string' },            
            { name: 'M2010_I', type: 'number' },
            { name: 'M2010_II', type: 'number' },
            { name: 'M2011_I', type: 'number' },
            { name: 'M2011_II', type: 'number' },
            { name: 'M2012_I', type: 'number' },
            { name: 'M2012_II', type: 'number' },
            { name: 'M2013_I', type: 'number' },
            { name: 'M2013_II', type: 'number' },
            { name: 'M2014_I', type: 'number' },
            { name: 'M2014_II', type: 'number' },
            { name: 'M2015_I', type: 'number' },
            { name: 'M2015_II', type: 'number' },
            { name: 'M2016_I', type: 'number' },
            { name: 'M2016_II', type: 'number' },
            { name: 'M2017_I', type: 'number' },
            { name: 'M2017_II', type: 'number' },
            { name: 'M2018_I', type: 'number' },
            { name: 'M2018_II', type: 'number' },
            { name: 'NIVELENTRADA', type: 'number' },
            { name: 'TASAFALT', type: 'number' },
            { name: 'TASAPRES', type: 'number' },
            { name: 'TASAAAPRO', type: 'number' },
            { name: 'TASADESA', type: 'number' },
            { name: 'DEUDASPAGA', type: 'number' },
            { name: 'DEUDASPEND', type: 'number' },
            { name: 'DEUDASATRASADAS', type: 'number' },            
            { name: 'EXAMENTRA', type: 'number' },
            { name: 'VOCACIONAL', type: 'number' },
            { name: 'ANSIEDAD', type: 'number' },
            { name: 'DEPRESION', type: 'number' },
            { name: 'AUTOEFICACIA', type: 'number' },
            { name: 'TRABAJA', type: 'number' },
            { name: 'DEPENDIENTES', type: 'number' },
            { name: 'PROBABILIDAD', type: 'number' },
            { name: 'PROBFAMIL', type: 'number' },
            { name: 'PECONOM', type: 'number' },
            { name: 'PSALUD', type: 'number' },
            { name: 'PACADEM', type: 'number' },
            { name: 'EDAD', type: 'number' },
            { name: 'FNACIM', type: 'date' },
            { name: 'SEXO', type: 'string' },
            { name: 'PROMEPOND', type: 'number' },
            { name: 'PROB_APROF', type: 'number' },
            { name: 'PROB_APCURS', type: 'number' },
            { name: 'NOTASAPRO', type: 'number' },
            { name: 'NOTASDESAP', type: 'number' },
            { name: 'TOTALNOTAS', type: 'number' },
            { name: 'CICLODEBEESTAR', type: 'number' },
            { name: 'RETRASO', type: 'number' }





        ],
        root: 'rows',
        url: "../../DataJson/PredictorDesercion/GenerarMatrizCSV.aspx",
        data: {
            "Funcion": "CVS",
            "Codigo_cpf": codigo_cpf,
            "Codigo_fac": codigo_fac,
            "Fecha":$("#txtFecha").val()
        }
    };
    var adapter = new $.jqx.dataAdapter(source);
    return adapter;
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
                    JSONToCSVConvertor(JSON.stringify(rows), "Matriz de Conocimiento Alumnos", true, HideColumns);
                }, 8000);
        
        //        console.log(JSON.parse(rows));
        //        setTimeout(function() {
        //            JSONToCSVConvertor(JSON.stringify(rows), "Matriz de Conocimiento Alumnos", true);
        //        }, 4000);

        //console.log(JSON.stringify(rows));
    });

}

function DownloadJSON2CSV(objArray) {
    alert(1);
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
    // alert(link);
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
            str += '<option value="' + val.Value + '">' + val.Label   + '</option>';
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

function fnColumnasGrid() {
    var listSource = [
                { label: 'CODIGOUNIV', value: 'CODIGOUNIV', checked: true },
                { label: 'DESERTOR', value: 'DESERTOR', checked: true },
                { label: 'NMATRIC', value: 'NMATRIC', checked: true },
                { label: 'CICLOINGRESO', value: 'CICLOINGRESO', checked: true },
                { label: 'DISCONTINUO', value: 'DISCONTINUO', checked: true },
                
                { label: '2010-I', value: 'M2010_I', checked: true },
                { label: '2010-II', value: 'M2010_II', checked: true },
                { label: '2011-I', value: 'M2011_I', checked: true },
                { label: '2011-II', value: 'M2011_II', checked: true },
                { label: '2012-I', value: 'M2012_I', checked: true },
                { label: '2012-II', value: 'M2012_II', checked: true },
                { label: '2013-I', value: 'M2013_I', checked: true },
                { label: '2013-II', value: 'M2013_II', checked: true },
                { label: '2014-I', value: 'M2014_I', checked: true },
                { label: '2014-II', value: 'M2014_II', checked: true },
                { label: '2015-I', value: 'M2015_I', checked: true },
                { label: '2015-II', value: 'M2015_II', checked: true },
                { label: '2016-I', value: 'M2016_I', checked: true },
                { label: '2016-II', value: 'M2016_II', checked: true },
                { label: '2017-I', value: 'M2017_I', checked: true },
                { label: '2017-II', value: 'M2017_II', checked: true },
                { label: '2018-I', value: 'M2018_I', checked: true },
                { label: '2018-II', value: 'M2018_II', checked: true },
                { label: 'NIVELENTRADA', value: 'NIVELENTRADA', checked: true },
                { label: 'TASAFALT', value: 'TASAFALT', checked: true },
                { label: 'TASAPRES', value: 'TASAPRES', checked: true },
                { label: 'TASAAAPRO', value: 'TASAAAPRO', checked: true },
                { label: 'TASADESA', value: 'TASADESA', checked: true },
                { label: 'DEUDASPAGA', value: 'DEUDASPAGA', checked: true },
                { label: 'DEUDASPEND', value: 'DEUDASPEND', checked: true },
                { label: 'DEUDASATRASADAS', value: 'DEUDASATRASADAS', checked: true },                
                { label: 'EXAMENTRA', value: 'EXAMENTRA', checked: true },
                { label: 'VOCACIONAL', value: 'VOCACIONAL', checked: true },
                { label: 'ANSIEDAD', value: 'ANSIEDAD', checked: true },
                { label: 'DEPRESION', value: 'DEPRESION', checked: true },
                { label: 'AUTOEFICACIA', value: 'AUTOEFICACIA', checked: true },
                { label: 'TRABAJA', value: 'TRABAJA', checked: true },
                { label: 'DEPENDIENTES', value: 'DEPENDIENTES', checked: true },
                { label: 'PROBABILIDAD', value: 'PROBABILIDAD', checked: true },
                { label: 'PROBFAMIL', value: 'PROBFAMIL', checked: true },
                { label: 'PECONOM', value: 'PECONOM', checked: true },
                { label: 'PSALUD', value: 'PSALUD', checked: true },
                { label: 'PACADEM', value: 'PACADEM', checked: true },
                { label: 'EDAD', value: 'EDAD', checked: true },
                { label: 'FNACIM', value: 'FNACIM', checked: true },
                { label: 'SEXO', value: 'SEXO', checked: true },
                { label: 'PROMEPOND', value: 'PROMEPOND', checked: true },
                { label: 'PROB_APROF', value: 'PROB_APROF', checked: true },
                { label: 'PROB_APCURS', value: 'PROB_APCURS', checked: true },
                { label: 'NOTASAPRO', value: 'NOTASAPRO', checked: true },
                { label: 'NOTASDESAP', value: 'NOTASDESAP', checked: true },
                { label: 'TOTALNOTAS', value: 'TOTALNOTAS', checked: true },
                { label: 'CICLODEBEESTAR', value: 'CICLODEBEESTAR', checked: true },
                { label: 'RETRASO', value: 'RETRASO', checked: true },

               ];
    $("#jqxlistbox").jqxListBox({ source: listSource, width: 150, height: 400, checkboxes: true });

    $("#jqxlistbox").on('checkChange', function(event) {
        $("#jqxgrid").jqxGrid('beginupdate');
        if (event.args.checked) {
          
            $("#jqxgrid").jqxGrid('showcolumn', event.args.value);
            deleteHideColumn(HideColumns, event.args.value);
        }
        else {
            HideColumns.push(event.args.value);
            $("#jqxgrid").jqxGrid('hidecolumn', event.args.value);
            console.log(HideColumns);
        }
        $("#jqxgrid").jqxGrid('endupdate');
    });
}

function deleteHideColumn(jsonVar, name) {
        var key;
        var obj = {};
        var i = 0;
        for (var idx in HideColumns) {
            key = HideColumns[idx];
            if (key == name) {
                HideColumns.splice(idx, 1);
         //       console.log(idx);
           //     console.log(key);
            }            
        }
        console.log(HideColumns);
}