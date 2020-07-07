<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmImportarInfoTutoria.aspx.vb" Inherits="academico_PredictorDiserccion_frmImportarInfoTutoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
        <meta http-equiv='X-UA-Compatible' content='IE=8' />
        <meta http-equiv='X-UA-Compatible' content='IE=10' />   
        <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7, IE=EmulateIE9, IE=EDGE" /> 
<title>Carga de Datos eventos Academicos</title>
<link rel='stylesheet' href='../../assets/css/bootstrap.min.css'/>
<link rel='stylesheet' href='../../academico/assets/css/material.css'/>
<link rel='stylesheet' href='../../academico/assets/css/style.css?x=1'/>
<link rel='stylesheet' href='../../academico/assets/css/jquery.dataTables.min.css'/>
<link rel="stylesheet" href='../../assets/css/sweet-alerts/sweetalert.css'/>
<script src="../../academico/assets/js/jquery.js" type="text/javascript"></script>

<script src="../../academico/assets/js/noty/jquery.noty.js" type="text/javascript"></script>
        <script src="../../academico/assets/js/noty/layouts/top.js" type="text/javascript"></script>
        <script src="../../academico/assets/js/noty/layouts/default.js" type="text/javascript"></script>
 <script src="../../academico/assets/js/noty/jquery.desknoty.js" type="text/javascript"></script>
        <script src="../../academico/assets/js/noty/notifications-custom.js" type="text/javascript"></script>
<script type="text/javascript" src="../../assets/js/sweet-alert/sweetalert.min.js?x=3"></script>
<script type="text/javascript">
    var DataJson = '';
    $(document).ready(function() {
            LoadMatriz();
         
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
              function LoadMatriz() {
                  /// alert($("#NroRendicion").val());
                  $.ajax({
                      type: "POST",
                      //contentType: "application/json; charset=utf-8",
                      url: "../../DataJson/PredictorDesercion/ImportarDataTutoria.aspx",
                      data: { "Funcion": "Matriz", id: $.get("id"), mod: $.get("mod"), ctf: $.get("ctf") },
                      dataType: "json",
                      cache: false,
                      success: function(data) {
                          var value = "";
                          var sOut = '';
                          $("#cboMatriz").html("");
                          jQuery.each(data, function(i, val) {
                              sOut += ' <option value=' + val.Value + ' >' + val.Label + ' </option>'
                              value = val.Value;
                            
                          });

                          $("#cboMatriz").html(sOut);
                          LoadRubro(value);

                      },
                      error: function(result) {

                      }
                  });
              }
              function LoadMatriz(Codigo) {
                  /// alert($("#NroRendicion").val());
                  $.ajax({
                      type: "POST",
                      //contentType: "application/json; charset=utf-8",
                      url: "../../DataJson/PredictorDesercion/ImportarDataTutoria.aspx",
                      data: { "Funcion": "Matriz", id: Codigo },
                      dataType: "json",
                      cache: false,
                      success: function(data) {

                          var sOut = '';
                          $("#cboMatriz").html("");
                          jQuery.each(data, function(i, val) {
                          sOut += ' <option value=' + val.Value + ' >' + val.Label + ' </option>'
                          });

                          $("#cboMatriz").html(sOut);
                      },
                      error: function(result) {

                      }
                  });
              }
              function Grabar() {
                  if (validar() == true) {
                      $("#lblMessage").html("Espere un momento.. se esta procesando la información")
                      $("#lblMessage").addClass('alert alert-success alert-dismissable');
                      $("#btnAgregar").prop('disabled', false);
                      var data = jQuery.parseJSON(DataJson);
                      
                      var row = 0;
                      var RowError=0
                      var result = 0;
                      var tam = data.Tutoria.length;
                      var regs = 0;
                      jQuery.each(data.Tutoria, function(i, val) {
                          regs = regs + 1;
                          $.ajax({
                              type: "POST",
                              //contentType: "application/json; charset=utf-8",
                              url: "../../DataJson/PredictorDesercion/ImportarDataTutoria.aspx",
                              data: { "Funcion": "Post",
                              CODIGO: val.CODIGO,
                              CICLOINGRESO: val.CICLOINGRESO.trim(),
                              COGNITIVO: val.COGNITIVO,
                              SOCIOAFECTIVO: val.SOCIOAFECTIVO,
                              VOCACIONAL: val.VOCACIONAL,
                              AUTOEFICACIA: val.AUTOEFICACIA,
                              ANSIEDAD: val.ANSIEDAD,
                              DEPRESION: val.DEPRESION,
                              Codigo_mtz: $("#cboMatriz").val()

                              },
                              dataType: "json",
                              cache: false,
                              success: function(result) {
                                    row = row + 1;
                                  $("#" + result[0].Code).css("background-color", "yellow");
                                  $("#lblMessage").html("Registro a procesar : " + tam + "  Procesados :" + row)
                                  $("#lblMessage").addClass('alert alert-success alert-dismissable');
                                  ///console.log(result[0].Code);
                                  $("#btnAgregar").prop('disabled', true);
                              },
                              error: function(result) {
                                  //console.log(result);
                                  RowError = RowError + 1;
                              }
                          });
                           
                              swal({
                                  title: "Información",
                                  text: "Se Procesara : " + tam + " registros, al finalizar Verifique los registros sin color amarillo ",
                                  type: "info",
                                  showCancelButton: false,
                                  confirmButtonColor: '#6fd64b',
                                  confirmButtonClass: 'btn-success',
                                  confirmButtonText: 'Ok',
                                  closeOnConfirm: true,
                                  animation: "slide-from-top"
                                  //closeOnCancel: false
                              },
                          function() {


                          });

                           

                      });
                     
                      
                  }

              }
              function validar() 
              {
                  var res = true;
                  if ($("#cboMatriz").val() == String.empty) 
                  {
                      fnMensaje('warning', "Seleccione el centro de costos");
                      res = false;
                  }
                   
              return res;
              }              
</script>
 
</head>
<body>
 <div class="wrapper">
     <div class="content">
        <div class="overlay">
        </div>
         <div class="main-content">
          <div class="row">
                    <div class="manage_buttons" id="divOpc">
                        <div class="row">
                            <div id="PanelBusqueda">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="page_header">
                                        <div class="pull-left">
                                            <i class="icon ti-volume page_header_icon"></i><span class="main-text">Cargar Evaluaciones TUTORIA</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <%--<a class="btn btn-primary" id="btnListar" href="#"><i class="ion-android-search"></i>
                                                    &nbsp;Listar</a>--%>
                                                     <a onclick="Grabar();" class="btn btn-green" id="btnAgregar" href="#" data-toggle="modal"
                                                        data-target="#mdRegistro"><i class="ion-android-add"></i>&nbsp;Grabar</a>
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmBuscarConvoc" onsubmit="return false;"
                                        action="#" method="post">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="col-sm-12 col-md-4 control-label">
                                                        Fecha Matriz de conocimiento</label>
                                                    <div class="col-sm-12 col-md-8">
                                                        <select name="cboMatriz" class="form-control" id="cboMatriz">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                                
                                                <div class="form-group">
                                                    <label class="col-sm-12 col-md-4 control-label">
                                                        Seleccione Excel </label>
                                                    <div class="col-sm-12 col-md-8">
                                                          <input type="file" class="form-control" name="xlfile" id="xlf" />
                                                    </div>
                                                </div>
                                                  
                                               <div class="form-group">
                                                <label class="col-sm-12 col-md-4 control-label">
                                                       </label>
                                                        <div class="col-sm-12 col-md-8">
                                                    <label id="lblMessage" class="col-sm-12 col-md-12"  name="lblMessage"/> 
                                                    </div>
                                                </div> 
                                            </div>
                                        </div>
                                        </form>
                                    </div>
                                </div>                            
                            </div>
                        </div>
                    </div>
                </div>
         <div class="row">
           <div class="panel panel-piluku" id="PanelLista">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Listado de Alumnos Tutoria <span class="panel-options"><a class="panel-refresh" href="#">
                                    <i class="icon ti-reload" onclick="fnBuscarConvocatoria(false)"></i></a><a class="panel-minimize"
                                        href="#"><i class="icon ti-angle-up"></i></a></span>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <div id="tConvocatoria_wrapper" class="dataTables_wrapper" role="grid">
                                    <table id="tConvocatoria" name="tConvocatoria" class="display dataTable" width="100%">
                                        <thead>
                                            <tr role="row">
                                                <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                    tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                    N°
                                                </td>
                                                <td width="38%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    CODIGO
                                                </td>
                                                <td width="20%" style="font-weight: bold; width: 169px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                   ALUMNO
                                                </td>
                                                <td width="10%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                    CICLOINGRESO
                                                </td>
                                                <td width="10%" style="font-weight: bold; width: 226px; text-align: center;" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Fin: activate to sort column ascending">
                                                    ESCUELA
                                                </td>
                                                <td width="7%" style="font-weight: bold; width: 203px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Estado: activate to sort column ascending">
                                                    COGNITIVO
                                                </td>
                                                <td width="5%" style="font-weight: bold; width: 203px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Estado: activate to sort column ascending">
                                                    SOCIOAFECTIVO
                                                </td>
                                                <td width="10%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                    VOCACIONAL
                                                </td>
                                                 <td width="10%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                    AUTOEFICACIA
                                                </td>
                                                 <td width="10%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                   ANSIEDAD
                                                </td>
                                                <td width="10%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                   DEPRESION
                                                </td>
                                                
                                            </tr>
                                        </thead>
                                        <tfoot>
                                            <tr>
                                                <th colspan="7" rowspan="1">
                                                </th>
                                            </tr>
                                        </tfoot>
                                        <tbody id="TbodyPrint">
                                            <%--  <tr class="odd">
                                                <td valign="top" colspan="7" class="dataTables_empty">
                                                    No se ha encontrado informacion
                                                </td>
                                            </tr>--%>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
         </div>
         </div>
     </div>
 
</div>
  
<pre>
<b>



 
<%--<div id="drop">Drop a spreadsheet file here to see sheet data</div>--%>

<%--<textarea id="b64data">... or paste a base64-encoding here</textarea>
--%>

 <input type="checkbox" name="useworker" checked visible>
 <input type="checkbox" name="xferable" checked visible>
 <input type="checkbox" name="userabs" checked visible>
</pre>

<div id="htmlout"></div>
<br />
<!-- uncomment the next line here and in xlsxworker.js for encoding support -->
<script src="../../assets/js/xls/dist/cpexcel.js"></script>
<script src="../../assets/js/xls/shim.js"></script>
<script src="../../assets/js/xls/jszip.js"></script>
<script src="../../assets/js/xls/xlsx.js"></script>
<script>
    /*jshint browser:true */
    /* eslint-env browser */
    /* eslint no-use-before-define:0 */
    /*global Uint8Array, Uint16Array, ArrayBuffer */
    /*global XLSX */
    var X = XLSX;
    var XW = {
        /* worker message */
        msg: 'xlsx',
        /* worker scripts */
        rABS: '../../assets/js/xls/xlsxworker2.js',
        norABS: '../../assets/js/xls/xlsxworker1.js',
        noxfer: '../../assets/js/xls/xlsxworker.js'
    };

    var rABS = typeof FileReader !== "undefined" && typeof FileReader.prototype !== "undefined" && typeof FileReader.prototype.readAsBinaryString !== "undefined";
    if (!rABS) {
        document.getElementsByName("userabs")[0].disabled = true;
        document.getElementsByName("userabs")[0].checked = false;
    }

    var use_worker = typeof Worker !== 'undefined';
    if (!use_worker) {
        document.getElementsByName("useworker")[0].disabled = true;
        document.getElementsByName("useworker")[0].checked = false;
    }

    var transferable = use_worker;
    if (!transferable) {
        document.getElementsByName("xferable")[0].disabled = true;
        document.getElementsByName("xferable")[0].checked = false;
    }

    var wtf_mode = false;

    function fixdata(data) {
        var o = "", l = 0, w = 10240;
        for (; l < data.byteLength / w; ++l) o += String.fromCharCode.apply(null, new Uint8Array(data.slice(l * w, l * w + w)));
        o += String.fromCharCode.apply(null, new Uint8Array(data.slice(l * w)));
        return o;
    }

    function ab2str(data) {
        var o = "", l = 0, w = 10240;
        for (; l < data.byteLength / w; ++l) o += String.fromCharCode.apply(null, new Uint16Array(data.slice(l * w, l * w + w)));
        o += String.fromCharCode.apply(null, new Uint16Array(data.slice(l * w)));
        return o;
    }

    function s2ab(s) {
        var b = new ArrayBuffer(s.length * 2), v = new Uint16Array(b);
        for (var i = 0; i != s.length; ++i) v[i] = s.charCodeAt(i);
        return [v, b];
    }

    function xw_noxfer(data, cb) {
        var worker = new Worker(XW.noxfer);
        worker.onmessage = function(e) {
            switch (e.data.t) {
                case 'ready': break;
                case 'e': console.error(e.data.d); break;
                case XW.msg: cb(JSON.parse(e.data.d)); break;
            }
        };
        var arr = rABS ? data : btoa(fixdata(data));
        worker.postMessage({ d: arr, b: rABS });
    }

    function xw_xfer(data, cb) {
        var worker = new Worker(rABS ? XW.rABS : XW.norABS);
        worker.onmessage = function(e) {
            switch (e.data.t) {
                case 'ready': break;
                case 'e': console.error(e.data.d); break;
                default: var xx = ab2str(e.data).replace(/\n/g, "\\n").replace(/\r/g, "\\r"); console.log("done"); cb(JSON.parse(xx)); break;
            }
        };
        if (rABS) {
            var val = s2ab(data);
            worker.postMessage(val[1], [val[1]]);
        } else {
            worker.postMessage(data, [data]);
        }
    }

    function xw(data, cb) {
        transferable = document.getElementsByName("xferable")[0].checked;
        if (transferable) xw_xfer(data, cb);
        else xw_noxfer(data, cb);
    }

   

    function to_json(workbook) {
        var result = {};
        workbook.SheetNames.forEach(function(sheetName) {
            var roa = X.utils.sheet_to_json(workbook.Sheets[sheetName]);
            if (roa.length > 0) {
                result[sheetName] = roa;
            }
        });
      
        return result;
    }

    function to_csv(workbook) {
        var result = [];
        workbook.SheetNames.forEach(function(sheetName) {
            var csv = X.utils.sheet_to_csv(workbook.Sheets[sheetName]);
            if (csv.length > 0) {
                result.push("SHEET: " + sheetName);
                result.push("");
                result.push(csv);
            }
        });
        return result.join("\n");
    }

    function to_formulae(workbook) {
        var result = [];
        workbook.SheetNames.forEach(function(sheetName) {
            var formulae = X.utils.get_formulae(workbook.Sheets[sheetName]);
            if (formulae.length > 0) {
                result.push("SHEET: " + sheetName);
                result.push("");
                result.push(formulae.join("\n"));
            }
        });
        return result.join("\n");
    }

    var HTMLOUT = document.getElementById('htmlout');
    function to_html(workbook) {
        HTMLOUT.innerHTML = "";
        workbook.SheetNames.forEach(function(sheetName) {
            var htmlstr = X.write(workbook, { sheet: sheetName, type: 'binary', bookType: 'html' });
            HTMLOUT.innerHTML += htmlstr;
        });
    }

    var tarea = document.getElementById('b64data');
    function b64it() {
        if (typeof console !== 'undefined') console.log("onload", new Date());
        var wb = X.read(tarea.value, { type: 'base64', WTF: wtf_mode });
        process_wb(wb);
    }
    window.b64it = b64it;

    var OUT = document.getElementById('out');
    var global_wb;
    function ListTable() {
        global_wb = wb;
        var output = "";
        output = JSON.stringify(to_json(wb), 2, 2);
       // console.log(output);
    }
    function process_wb(wb) {
        global_wb = wb;
        var output = "";
       
                output = JSON.stringify(to_json(wb), 2, 2);
                DataJson = output;    
                 
        
      //  if (OUT.innerText === undefined) OUT.textContent = output;
      //  else OUT.innerText = output;
       // if (typeof console !== 'undefined') console.log("output", new Date());
        var data = jQuery.parseJSON(output); //.Alumnos;
       // console.log(data.Alumnos);
        //console.log(data[0]);
        var sOut = '';
        $("#TbodyPrint").html("");
        var index = 1;
        jQuery.each(data.Tutoria, function(i, val) {

            sOut += '<tr id="' + val.CODIGO + '">';
            sOut += '<td>' + index + ' ' + '</td>';
          //  sOut += '<td>' + val.CODIGO + ' ' + '</td>';
            var codigo = val.CODIGO;
            //console.log(codigo);
            if (codigo == 'undefined' || codigo==null || codigo=='') {
                sOut += '<td>' + ' - ' + '</td>';
            } else {
                sOut += '<td>' + val.CODIGO + ' ' + '</td>';
            }
            sOut += '<td>' + val.ALUMNO + ' ' + '</td>';
            sOut += '<td>' + val.CICLOINGRESO + ' ' + '</td>';
            sOut += '<td>' + val.ESCUELA + ' ' + '</td>';
            sOut += '<td>' + val.COGNITIVO + ' ' + '</td>';
            sOut += '<td>' + val.SOCIOAFECTIVO + ' ' + '</td>';
            sOut += '<td>' + val.VOCACIONAL + ' ' + '</td>';
            sOut += '<td>' + val.AUTOEFICACIA + ' ' + '</td>';
            sOut += '<td>' + val.ANSIEDAD + ' ' + '</td>';
            sOut += '<td>' + val.DEPRESION + ' ' + '</td>';
            sOut += '</tr>';
            index = index + 1;
        });
    $("#TbodyPrint" ).html(sOut);
    }
    function setfmt() { if (global_wb) process_wb(global_wb); }
    window.setfmt = setfmt;

    var drop = document.getElementById('drop');
    function handleDrop(e) {
        e.stopPropagation();
        e.preventDefault();
        rABS = document.getElementsByName("userabs")[0].checked;
        use_worker = document.getElementsByName("useworker")[0].checked;
        var files = e.dataTransfer.files;
        var f = files[0];
        {
            var reader = new FileReader();
            //var name = f.name;
            reader.onload = function(e) {
                if (typeof console !== 'undefined') console.log("onload", new Date(), rABS, use_worker);
                var data = e.target.result;
                if (use_worker) {
                    xw(data, process_wb);
                } else {
                    var wb;
                    if (rABS) {
                        wb = X.read(data, { type: 'binary' });
                    } else {
                        var arr = fixdata(data);
                        wb = X.read(btoa(arr), { type: 'base64' });
                    }
                    process_wb(wb);
                }
            };
            if (rABS) reader.readAsBinaryString(f);
            else reader.readAsArrayBuffer(f);
        }
    }

    function handleDragover(e) {
        e.stopPropagation();
        e.preventDefault();
        e.dataTransfer.dropEffect = 'copy';
    }
/*
    if (drop.addEventListener) {
        drop.addEventListener('dragenter', handleDragover, false);
        drop.addEventListener('dragover', handleDragover, false);
        drop.addEventListener('drop', handleDrop, false);
    }
*/

    var xlf = document.getElementById('xlf');
    function handleFile(e) {
        rABS = document.getElementsByName("userabs")[0].checked;
        use_worker = document.getElementsByName("useworker")[0].checked;
        var files = e.target.files;
        var f = files[0];
        {
            var reader = new FileReader();
            //var name = f.name;
            reader.onload = function(e) {
                if (typeof console !== 'undefined') console.log("onload", new Date(), rABS, use_worker);
                var data = e.target.result;
                if (use_worker) {

                    var xwProces = xw(data, process_wb);
                   // console.log(xwProces);
                } else {
                    var wb;
                    if (rABS) {
                        wb = X.read(data, { type: 'binary' });
                    } else {
                        var arr = fixdata(data);
                        wb = X.read(btoa(arr), { type: 'base64' });
                    }
                   
                    process_wb(wb);

                }
            };
            if (rABS) reader.readAsBinaryString(f);
            else reader.readAsArrayBuffer(f);
        }
    }
    $("#xlf").on('click', function() {
         $("#TbodyPrint").html("");
    });
    $("#xlf").on('change', function(e) {
     
        handleFile(e);
    });

   // if (xlf.addEventListener) xlf.addEventListener('change', handleFile, false);
</script>

</body>
</html>
