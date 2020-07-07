<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmIncripsionConeii.aspx.vb" Inherits="administrativo_pec_frmIncripsionConeii" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
        <meta http-equiv='X-UA-Compatible' content='IE=8' />
        <meta http-equiv='X-UA-Compatible' content='IE=10' />   
        <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7, IE=EmulateIE9, IE=EDGE" /> 
<title>JS-XLSX Live Demo</title>
<link rel='stylesheet' href='../../assets/css/bootstrap.min.css'/>
<link rel='stylesheet' href='../../academico/assets/css/material.css'/>
<link rel='stylesheet' href='../../academico/assets/css/style.css?x=1'/>
<link rel='stylesheet' href='../../academico/assets/css/jquery.dataTables.min.css'/>
<script src="../../academico/assets/js/jquery.js" type="text/javascript"></script>

<script type="text/javascript">
$(document).ready(function() {
LoadCecos();
                   
              });
              function LoadCecos() {
                  /// alert($("#NroRendicion").val());
                  $.ajax({
                      type: "GET",
                      //contentType: "application/json; charset=utf-8",
                      url: "../../DataJson/EventosAcademicos/EventosAcademicos.aspx",
                      data: { "Funcion": "cecos" },
                      dataType: "json",
                      cache: false,
                      success: function(data) {

                          var sOut = '';
                          $("#cboCecos").html("");
                          jQuery.each(data, function(i, val) {
                              sOut += ' <option value=' + val.Label + ' >' + val.Label + ' </option>'
                          });

                          $("#cboCecos").html(sOut);
                      },
                      error: function(result) {

                      }
                  });
              }
</script>
</head>
<body>
  <div class="col-md-12 nopad-right">
       <div class="panel panel-piluku">
         <div class="panel-heading">
                <h3 class="panel-title">
                    Registro de participantes 
                    <span class="panel-options">
                        <a href="#" class="panel-refresh">
                            <i class="icon ti-reload"></i> 
                        </a>
                        <a href="#" class="panel-minimize">
                            <i class="icon ti-angle-up"></i> 
                        </a>
                        <a href="#" class="panel-close">
                            <i class="icon ti-close"></i> 
                        </a>
                    </span>
                </h3>
            </div>
       </div>
         <div class="panel-body">
             <div class="row">
                <div class="col-lg-12">
                    <form  enctype="multipart/form-data"  class="form-horizontal" role="form" id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" method="post"   >                
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3  col-md-2 col-sm-3 col-xs-3 control-label">Centro de Costos  :</label>
                            <div class="col-lg-2 col-md-2 col-sm-3 col-xs-3">                                
                                <select name="cboCecos" id="cboCecos"></select>
                            </div>
                        </div> 
                         <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3  col-md-2 col-sm-3 col-xs-3 control-label">Rubro :</label>
                            <div class="col-lg-2 col-md-2 col-sm-3 col-xs-3">                                
                                <select name="cboRubro" id="cboRubro"></select>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3  col-md-2 col-sm-3 col-xs-3 control-label">Seleccione  :</label>
                            <div class="col-lg-2 col-md-2 col-sm-3 col-xs-3">                                
                                <input type="file" class="form-control" name="xlfile" id="xlf" />
                            </div>
                        </div> 
                    </form>
                </div>
             </div>
             <div class="row">
                 <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12">
                      <table border=1 class="display dataTable cell-border" id="tblPrint" style="width:100%;font-size:12px;">
                            <thead>
                                <tr role="row">		
                                    <th style="width:2%">#</th>
                                    <th style="width:10%">Tipo Documento</th>
                                    <th style="width:15%;padding-right:6px;">Nro.Documento</th>
                                    <th style="width:15%">Ape. Paterno</th>
                                    <th style="width:15%">Ape. Materno</th>											
                                    <th style="width:15%;padding-right:6px;">Nombres</th>
                                    <th style="width:15%">Fecha Nac.</th>
                                    <th style="width:15%">Importe</th>
                                    <th style="width:15%">Fec/Vencimiento</th>
                                </tr>
                            </thead>
                            <tbody id="TbodyPrint" runat="server">
                            </tbody>	
                        </table>
                    </div>							
             </div>
         </div>
          <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-xs-6 col-sm-6">
                                     <a href="#" onclick="ListTable();"  class="btn btn-primary btn-icon-primary btn-icon-block btn-icon-blockleft" data-toggle="modal" data-target="#ModalPrint" data-whatever="@getbootstrap"><i  class="ion  ion-ios-printer-outline"></i><span>Imprimir</span></a>
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
        console.log(output);
    }
    function process_wb(wb) {
        global_wb = wb;
        var output = "";
       
                output = JSON.stringify(to_json(wb), 2, 2);
                
                 
        
      //  if (OUT.innerText === undefined) OUT.textContent = output;
      //  else OUT.innerText = output;
       // if (typeof console !== 'undefined') console.log("output", new Date());
        var data = jQuery.parseJSON(output); //.Alumnos;
       // console.log(data.Alumnos);
        //console.log(data[0]);
        var sOut = '';
        $("#TbodyPrint").html("");
        var index = 0;
        jQuery.each(data.Alumnos, function(i, val) {

            sOut += '<tr>';
            sOut += '<td>' + index + ' ' + '</td>';
            sOut += '<td>' + val.TipoDoc + ' ' + '</td>';
            sOut += '<td>' + val.NumDoc + ' ' + '</td>';
            sOut += '<td>' + val.ApellPaterno + ' ' + '</td>';
            sOut += '<td>' + val.ApellMaterno + ' ' + '</td>';
            sOut += '<td>' + val.Nombres + ' ' + '</td>';
            sOut += '<td>' + val.FecNac + ' ' + '</td>';
            sOut += '<td>' + val.Importe + ' ' + '</td>';
            sOut += '<td>' + val.fecVence + ' ' + '</td>';
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
    $("#xlf").on('change', function(e) {
     
        handleFile(e);
    });

   // if (xlf.addEventListener) xlf.addEventListener('change', handleFile, false);
</script>

</body>
</html>
