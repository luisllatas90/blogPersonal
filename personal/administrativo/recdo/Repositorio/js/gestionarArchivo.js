var indicador='0';
$(document).ready(function() {

    fnListarIndicador();
    fnListarPeriodoContable();
    $("#cboIndicador").on("change", function() {
   
        fnListarFrecuencia($(this).val(),0);
        
    });

    $("#cboMedioVerficacion").on("change", function() {
        fnListarTipoDocumento($(this).val());
        fnListarPeriodo($(this).val());
        fnListarResponsable($(this).val());
    });
    $("#btnGrabar").on("click", function() {
        fnGuardar();
    });
    $("#btnListar").on("click",function(){
        fnCargarGrid(0,0)    
    });
    $("#btnAgregar").on("click",function(){
        Limpiar();
        
        $("#mdRegistro").modal("show");
        $("#cmbAnio").val($("#cboAnnioContable").val());
    });
   
});
 $.get = function(key) {
        key = key.replace(/[\[]/, '\\[');
        key = key.replace(/[\]]/, '\\]');
        var pattern = "[\\?&]" + key + "=([^&#]*)";
        var regex = new RegExp(pattern);
        var url = unescape(window.location.href);
        var results = regex.exec(url);
        if (results === null) {
            return null;
        } else {
            return results[1];
        }
    }


function fnModal(indicador,medio,periodo,documento){
      //  Limpiar();        
       // alert(indicador);
        $("#cboIndicador").val(indicador);
        fnListarFrecuencia(indicador,medio);
        fnListarTipoDocumento(medio,documento);
        fnListarPeriodo(medio,periodo);
        fnListarResponsable(medio);
        //alert(periodo);
        //$("#cmbPeriodo").val(periodo);
        //$("#cboMedioVerficacion").val(medio);
        $("#mdRegistro").modal("show");
        $("#cmbAnio").val($("#cboAnnioContable").val());
}
function fnListarIndicador() {
    var str = '';
    $.ajax({
        type: "POST",
        dataType: "html",
        url: "../../DataJson/Repositorio/GestionDocumetacion.aspx",
        data: { "Funcion": "Indicador" },
        dataType: "json",
        cache: false,
        success: function(data) {
            $("#cboIndicador").html("");
            $("#cboIndicadorFill").html("");

            jQuery.each(data, function(i, val) {
            str += '<option value="' + val.Value + '">' + val.Label + '</option>';
            });
            $("#cboIndicador").html(str);
            $("#cboIndicadorFill").html(str);
            $("#cboIndicadorFill").val(indicador);


        },
        error: function(result) {
            sOut = '';
            console.log(result);
            // oTable.fnOpen(nTr, sOut, 'details');
            // location.reload();
        }
    });
}


function fnListarFrecuencia(Id,select) {
    var str = '';
    $.ajax({
        type: "POST",
        dataType: "html",
        url: "../../DataJson/Repositorio/GestionDocumetacion.aspx",
        data: { "Funcion": "Medio", "Id": Id },
        dataType: "json",
        cache: false,
        success: function(data) {
        $("#cboMedioVerficacion").html("");

            jQuery.each(data, function(i, val) {
                str += '<option value="' + val.Value + '">' + val.Label + '</option>';
            });
            $("#cboMedioVerficacion").html(str);
            //alert(select);
             $("#cboMedioVerficacion").val(select);
        },
        error: function(result) {
            sOut = '';
            console.log(result);
            // oTable.fnOpen(nTr, sOut, 'details');
            // location.reload();
        }
    });
}

function fnListarTipoDocumento(Id,select) {
    var str = '';
    $.ajax({
        type: "POST",
        dataType: "html",
        url: "../../DataJson/Repositorio/GestionDocumetacion.aspx",
        data: { "Funcion": "TipDoc", "Id": Id },
        dataType: "json",
        cache: false,
        success: function(data) {
            $("#cboDocumento").html("");

            jQuery.each(data, function(i, val) {
                str += '<option value="' + val.Value + '">' + val.Label + '</option>';
             
            });
            $("#cboDocumento").html(str);
            $("#cboDocumento").val(select);
        },
        error: function(result) {
            sOut = '';
            console.log(result);
            // oTable.fnOpen(nTr, sOut, 'details');
            // location.reload();
        }
    });
}
function fnListarPeriodo(Id,select) {
    var str = '';
    $.ajax({
        type: "POST",
        dataType: "html",
        url: "../../DataJson/Repositorio/GestionDocumetacion.aspx",
        data: { "Funcion": "Periodo", "Id": Id },
        dataType: "json",
        cache: false,
        success: function(data) {
            $("#cmbPeriodo").html("");

            jQuery.each(data, function(i, val) {
                str += '<option value="' + val.Value + '">' + val.Label + '</option>';
                $("#txtFrecuencia").val(val.NombreFre);
            });
            $("#cmbPeriodo").html(str);
  $("#cmbPeriodo").val(select);

        },
        error: function(result) {
            sOut = '';
            console.log(result);
            // oTable.fnOpen(nTr, sOut, 'details');
            // location.reload();
        }
    });
}

    function fnListarResponsable(Id) {
    var str = '';
    $.ajax({
        type: "POST",
        dataType: "html",
        url: "../../DataJson/Repositorio/GestionDocumetacion.aspx",
        data: { "Funcion": "Responsable", "Id": Id },
        dataType: "json",
        cache: false,
        success: function(data) {
            $("#cboResponsable").html("");

            jQuery.each(data, function(i, val) {
                str += '<option value="' + val.Value + '">' + val.Label + '</option>';
               
            });
            $("#cboResponsable").html(str);


        },
        error: function(result) {
            sOut = '';
            console.log(result);
            // oTable.fnOpen(nTr, sOut, 'details');
            // location.reload();
        }
    });
}
function fnListarPeriodoContable(Id) {
    var str = '';
    $.ajax({
        type: "POST",
        dataType: "html",
        url: "../../DataJson/Repositorio/GestionDocumetacion.aspx",
        data: { "Funcion": "PeriodoContable"  },
        dataType: "json",
        cache: false,
        success: function(data) {
        $("#cmbAnio").html("");
        $("#cboAnnioContable").html("");
         var anio = (new Date).getFullYear();
         var Id=0;
            jQuery.each(data, function(i, val) {
                str += '<option value="' + val.Value + '">' + val.Label + '</option>';
                if(val.Label==anio){
                    Id= val.Value;
                }
            });
            $("#cmbAnio").html(str);
            $("#cboAnnioContable").html(str);
           
            console.log(anio);
            $("#cboAnnioContable").val(Id);
        },
        error: function(result) {
            sOut = '';
            console.log(result);
            // oTable.fnOpen(nTr, sOut, 'details');
            // location.reload();
        }
    });
}

function fnGuardar() {
    var flag = false;
    var data = new FormData();

    var files = $("#fileData").get(0).files;

    // Add the uploaded image content to the form data collection
    if (files.length > 0) {
        data.append("UploadedImage", files[0]);
    }
    data.append("Funcion", "Registrar");
 
    data.append("CodigoPer", $("#cboResponsable").val());
    data.append("CodigoPct", $("#cmbAnio").val());
    data.append("CodigoPeri", $("#cmbPeriodo").val());
    data.append("CodigoDoc", $("#cboDocumento").val());
    cboDocumento
    // alert();
    $.ajax({
        type: "POST",
        url: "../../DataJson/Repositorio/GestionDocumetacion.aspx",
        data: data,
        dataType: "json",
        cache: false,
        contentType: false,
        processData: false,
        success: function(data) {
            console.log(data[0].Status);
            if (data[0].Status == "OK") {
                flag = true;
                console.log(data);
                fnMensaje('success', 'Subiendo Archivo');
                $('#divMessage').addClass('alert alert-success alert-dismissable');
                $fileupload = $('#fileData');
                $fileupload.replaceWith($fileupload.clone(true));
                $("#mdRegistro").modal("toggle");
                
                   indicador=$("#cboIndicador").val();
                   fnCargarGrid(0,0)  ;
                    Limpiar();
            } else {
            fnMensaje('warning', data[0].Message);
            }
            // $("#divMessage").html("Suviendo archivo");
            //  Limpiar();
        },
        error: function(result) {
            console.log(result);
            $("#divMessage").html(result);
            flag = false;
        }
    });
    return flag;
}

function Limpiar(){
   
    $("#cboMedioVerficacion").html('<option value="" selected="">-- Seleccione -- </option>');
    $("#cboDocumento").html('<option value="" selected="">-- Seleccione -- </option>');
    $("#cboResponsable").html('<option value="" selected="">-- Seleccione -- </option>');
    $("#cboResponsable").html('<option value="" selected="">-- Seleccione -- </option>');
    $("#txtFrecuencia").val('');
    $("#cmbPeriodo").html('<option value="" selected="">-- Seleccione -- </option>');
    $("#fileData").val("");
    
    fnListarIndicador();
    
}


function fnCargarGrid(codigo_fac, codigo_cpf) {
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
    var adapter = fnDataAdpterGrid(codigo_fac, codigo_cpf);
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
var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
         var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
         
};
  
    $("#jqxgrid").jqxGrid(
            {
                width: '100%',
                source: adapter,
                filterable: true,
                sortable: true,
                theme: 'light',
                pageable: true,
                autorowheight: true,
                height: 400,
                pagesize: 60,
                 enabletooltips: true,
                 selectionmode: 'multiplecellsadvanced',
                pagesizeoptions: ['50', '100', '500'],
                ready: function() {
                },
                autoshowfiltericon: true,               
                columns: [
                  {text: 'Indicador', datafield: 'Indicador', width: 250},
                  { text: 'Medio Verificacion', datafield: 'MedioVerificacion', filtertype: "checkedlist", width: 250 },
                  { text: 'Documento', datafield: 'documento', filtertype: 'checkedlist', width: 150 },
                   { text: 'Frecuencia', datafield: 'Frecuencia', filtertype: 'checkedlist', width: 150 },                  
                  { text: 'AÑO', datafield: 'ANIO', filtertype: 'checkedlist', width: 100 },
                  { text: 'SEMESTRE I', datafield: 'SEMESTRE_I', filtertype: 'checkedlist', width: 100 },
                  { text: 'SEMESTRE II', datafield: 'SEMESTRE_II', filtertype: 'checkedlist', width: 100 },
                  { text: 'ENE', datafield: 'ENERO', filtertype: 'checkedlist', width: 100 },
                  { text: 'FEB', datafield: 'FEBRERO', filtertype: 'checkedlist', width: 100 },
                  { text: 'MAR', datafield: 'MARZO', filtertype: 'checkedlist', width: 100 },
                  { text: 'ABR', datafield: 'ABRIL', filtertype: 'checkedlist', width: 100 },
                  { text: 'MAY', datafield: 'MAYO', filtertype: 'checkedlist', width: 100 },
                  { text: 'JUN', datafield: 'JUNIO', filtertype: 'checkedlist', width: 100 },
                  { text: 'JUL', datafield: 'JULIO', filtertype: 'checkedlist', width: 100 },
                  { text: 'AGO', datafield: 'AGOSTO', filtertype: 'checkedlist', width: 100 },
                  { text: 'SET', datafield: 'SETIEMBRE', filtertype: 'checkedlist', width: 100 },
                  { text: 'OCT', datafield: 'OCTUBRE', filtertype: 'checkedlist', width: 100 },
                  { text: 'NOV', datafield: 'NOVIEMBRE', filtertype: 'checkedlist', width: 100 },
                  { text: 'DIC', datafield: 'DICIEMBRE', filtertype: 'checkedlist', width: 100 }
                  
                ]
            });



  

    

}
function Versiones(data){

$.ajax({
                    type: "POST",
                    
                    url: "../../DataJson/Repositorio/GestionDocumetacion.aspx",
                    data: { "codigos": data, "Funcion": "VersionDoc" },
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        var row = 1;
                        //  alert(data.length);
                        var sOut = '';
                        $("#TDocs").html("");
                        jQuery.each(data, function(i, val) {
                            var docente = '';
                            // alert(NroRend);
                            var Eliminar = '';
                             var Adjunto ='<a id='+val.IdArchivosCompartidos+' name='+val.IdArchivosCompartidos+' onclick="DescargarArchivo(this)" style="color: #fb5d5d;font-size:20px" href="#"  class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-download"></i><span class="badge info-number message">Descargar</span></a>';
                                                  
                          
                                sOut += '<tr>';
                                sOut += '<td style="border-bottom: none;border-top: none;margin-left: 7px;font-size: 14px;">' + val.Codigo + ' ' + '</td>';
                                sOut += '<td style="border-bottom: none;border-top: none;margin-left: 7px;font-size: 14px;">' + val.Documento + ' ' + '</td>';
                                sOut += '<td style="border-bottom: none;border-top: none;margin-left: 7px;font-size: 14px;padding-right:6px;" class="text-right">' + val.NombreDoc + ' ' + '</td>';
                                sOut += '<td style="border-bottom: none;border-top: none;margin-left: 7px;font-size: 14px;">' + val.Fecha + ' ' + '</td>';
                                sOut += '<td style="border-bottom: none;border-top: none;margin-left: 7px;font-size: 14px;">' + Adjunto + ' ' + '</td>';
                                sOut += '</tr>';                           
                            row++;
                        });

                        $("#TDocs").html(sOut);
                        ///  oTable.fnOpen(nTr, sOut, 'details');

                    },
                    error: function(result) {
                        sOut = '';
                        // oTable.fnOpen(nTr, sOut, 'details');
                        // location.reload();
                    }
                });
                $("#divVersion").modal("show");

}

function DeleteFile(name) {
    var str = '';
    $.ajax({
        type: "POST",
        dataType: "html",
        url: "../../DataJson/Repositorio/GestionDocumetacion.aspx",
        data: { "Funcion": "Delete","name":name },
        dataType: "json",
        cache: false,
        success: function(data) {
         
            jQuery.each(data, function(i, val) {
                 
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

function DescargarArchivo(codigoDren) {
                var flag = false;
                var data = new FormData();
              
                
              
                    // console.log($(codigoDren).attr('id'));
                    // alert(codigoDren);

                    data.append("Funcion", "DescargarArchivo");
                    data.append("Codigo", $(codigoDren).attr('id'));
                    // alert();
                    $.ajax({
                        type: "POST",
                        url: "../../DataJson/Repositorio/GestionDocumetacion.aspx",
                        data: { "Funcion": "DescargarArchivo", "Codigo": $(codigoDren).attr('id') },
                        dataType: "json",
                        cache: false,
                        //  contentType: false,
                        //processData: false,
                        success: function(data) {
                            console.log(data);
                            jQuery.each(data, function(i, val) {

                                // alert(val.Status);
                                if (val.Status == "OK") {
                                    //var file = 'data:application/octet-stream;base64,' + val.File;
                                    // window.open(escape(file), "Title", "");
                                    //  demo(val.File);
                                    //downloadWithName(file, val.Nombre);
                                    downloadWithName(val.Host, val.Nombre);
                                    DeleteFile(val.Nombre);
                                    //                                    if (navigator.userAgent.indexOf("NET") > -1) {


                                    //                                        var param = { 'Id': $(codigoDren).attr('id') };
                                    //                                        // OpenWindowWithPost("DataJson/DescargarArchivo.aspx", "", "NewFile", param);
                                    //                                        window.open("DataJson/DescargarArchivo.aspx?Id=" + $(codigoDren).attr('id'), 'ta', "");

                                    //                                    } else {

                                    //                                        if (val.Extencion == '.png' || val.Extencion == '.jpg') {
                                    //                                            $("#imgDocumento").attr('src', 'data:image/png;base64,' + val.File);
                                    //                                        }
                                    //                                    }


                                    // $("#continuemodal").modal("show");
                                    //  $("#DivSessionUser").html(val.Message);
                                } else {
                                    // alert("Oks");
                                    $("#continuemodal").modal("show");
                                    $("#DivSessionUser").html(val.Message);
                                    // fnMensaje('warning', val.Message);
                                }

                            });

                            // $("#divMessage").html("Suviendo archivo");
                            //  Limpiar();
                        },
                        error: function(result) {
                            $("#divMessage").html(result);
                        }
                    });

                }
                function redireccionar() {
                //    alert(window.location.host + "/campusvirtual");
                    window.top.location.href = "http://" + window.location.host + "/campusvirtual/personal";
                }
                function getAbsolutePath() {
                    var loc = window.location;
                    var pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf('/') + 1);
                    return loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pathName.length));
                }
            function downloadWithName(uri, name) {
                var link = document.createElement("a");
                link.download = name;
                link.href = uri;
                link.click();
                
                
               // alert(link);
            }
            function demo(resultByte) {


                var objbuilder = '';
                objbuilder += ('<object width="100%" height="100%"      data="data:application/pdf;base64,');
                objbuilder += (resultByte);
                objbuilder += ('" type="application/pdf" class="internal">');
                objbuilder += ('<embed src="data:application/pdf;base64,');
                objbuilder += (resultByte);
                objbuilder += ('" type="application/pdf" />');
                objbuilder += ('</object>');
                
//                
//                var win = window.open("","_blank","titlebar=yes");
//        document.title = "My Title";
//        document.write('<html><body>');
//        document.write(objbuilder);
//        document.write('</body></html>');
//        layer = jQuery(win.document);
//        
            
                var blob = new Blob([resultByte], { type: "data:application/octet-stream;base64" });
                var file = 'data:application/octet-stream;base64,' + resultByte;
                var link = document.createElement("a");
                link.href = objbuilder;
                link.download = "myFileName.pdf";
                link.click();


               


              //  alert('toma');
            }
            

function VistaPrevia(CodigoDren, event) {
                $.ajax({
                    type: "POST",
                    url: "DataJson/ListarRendicion.aspx",
                    data: { "Funcion": "ArchivoCompartido", "Id": CodigoDren },
                    dataType: "json",
                    cache: false,
                    success: function(data) {

                    jQuery.each(data, function(i, val) {
                    //DivArchivos
                    $("#DivArchivos").html("<div><input type='hidden' id='" + val.IdArchivosCompartidos + "' name='" + val.IdArchivosCompartidos + "' /><a href='#' onclick='DescargarArchivo(this)' name='"+val.IdArchivosCompartidos+"'  id='" + val.IdArchivosCompartidos + "' href=''>" + val.Nombre + '</a></div>')
                    
                    });
                      //  event.preventDefault();
                    },
                    error: function(result) {
                         
                    }
                });


               // event.preventDefault();
            }
           
function fnDataAdpterGrid(codigo_fac, codigo_cpf) {
    //   alert(codigo_cpf);
    var source =
    {
        datatype: "json",
        type: "POST",
        datafields: [
            { name: 'Indicador', type: 'string' },
            { name: 'MedioVerificacion', type: 'string' },
            { name: 'documento', type: 'string' },
            { name: 'Frecuencia', type: 'string' },
            { name: 'Fecha', type: 'string' },
            { name: 'ANIO', type: 'string' },

            { name: 'SEMESTRE_I', type: 'number' },
            { name: 'SEMESTRE_II', type: 'number' },
            { name: 'ENERO', type: 'number' },
            { name: 'FEBRERO', type: 'number' },
            { name: 'MARZO', type: 'number' },
            { name: 'ABRIL', type: 'number' },
            { name: 'MAYO', type: 'number' },
            { name: 'JUNIO', type: 'number' },
            { name: 'JULIO', type: 'number' },
            { name: 'AGOSTO', type: 'number' },
            { name: 'SETIEMBRE', type: 'number' },
            { name: 'OCTUBRE', type: 'number' },
            { name: 'NOVIEMBRE', type: 'number' },
            { name: 'DICIEMBRE', type: 'number' } 
        ],
        root: 'rows',
        url: "../../DataJson/Repositorio/GestionDocumetacion.aspx",
        data: {
            "Funcion": "Version",
            "CodigoInd": $("#cboIndicadorFill").val(),
            "CodigoPct": $("#cboAnnioContable").val(),
            "ctf": $.get("ctf")          
        }
    };
    var adapter = new $.jqx.dataAdapter(source);
    return adapter;
}