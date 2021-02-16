<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmailUsat.aspx.vb" Inherits="administrativo_frmEmailUsat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Actualizar Email y Contraseñas USAT del Estudiante</title>
    <link href="../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../scripts/js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">



        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case '0':
                    cssclass = 'alert-success'
                    break;
                case "1":
                    cssclass = 'alert-danger'
                    break;
                case '3':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            if (cssclass == 'alert-success') {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-success"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else if (cssclass == 'alert-danger') {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else if (cssclass == 'alert-warning') {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-info"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            }
        }
        function ShowMessage2(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case '0':
                    cssclass = 'alert-success'
                    break;
                case "1":
                    cssclass = 'alert-danger'
                    break;
                case '3':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            if (cssclass == 'alert-success') {
                $('#alert_procesado').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-success"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else if (cssclass == 'alert-danger') {
            $('#alert_procesado').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else if (cssclass == 'alert-warning') {
            $('#alert_procesado').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else {
            $('#alert_procesado').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-info"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            }
        }
        
        function ShowMessage3(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case '0':
                    cssclass = 'alert-success'
                    break;
                case "1":
                    cssclass = 'alert-danger'
                    break;
                case '3':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            if (cssclass == 'alert-success') {
                $('#alert_noprocesado').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-success"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else if (cssclass == 'alert-danger') {
                $('#alert_noprocesado').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else if (cssclass == 'alert-warning') {
                $('#alert_noprocesado').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else {
                $('#alert_noprocesado').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-info"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            }
        }
    </script>
    <style>
        #drop {
            border: 2px dashed #bbb;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            padding: 25px;
            text-align: center;
            font: 20pt bold,"Vollkorn";
            color: #bbb;
        }

        #b64data {
            width: 100%;
        }

        a {
            text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="messagealert" id="alert_container"></div>
        <br />
        <div class="messagealert" id="alert_noprocesado"></div>
        <div class="messagealert" id="alert_procesado"></div>
          <br />
        <div class="container-fluid">
            <div class="panel panel-default" id="pnlLista" runat="server">
                <div class="panel-heading">
                    <b>Actualizar Email y Contraseñas USAT</b>
                </div>
                <div class="panel-body">
                    <div class="form-group row">
                        <div class="col-sm-6">
                            <select name="format" onchange="setfmt()" style="display: none;">
                                <option value="json" selected>Importar archivo</option>
                            </select><br />
                            <div id="drop">Coloque un archivo de hoja de cálculo aquí para ver los datos de la hoja</div>
                           <%-- <input type="file" name="xlfile" id="xlf" />--%>
                            <asp:FileUpload ID="xlf" runat="server" CssClass="btn btn-primary" />
                            ... o haga clic aquí para seleccionar un archivo
                        
                        </div>
                        <div class="col-sm-6">
                            <center><a href="formatoEmailUSAT.xlsx" title="Descargar formato" style="cursor:pointer"> <img src="images/formatoExcelEmailUsat.png" width="50%" height="50%" /><br />Descargar Formato</a></center>
                            </div>
                    </div>
                </div>

                <div class="panel-body" style="display: none">
                    <div class="row">
                        <div class="col-12">
                            <div class="table-responsive">
                                <asp:TextBox ID="info" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:TextBox ID="email" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:TextBox ID="clave" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <pre id="out" style="display: block;"></pre>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-12">
                            <div class="table-responsive">

                                <asp:GridView ID="grwResultado" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="nro,nrodocident,emailusat,clave"  CssClass="table table-sm table-bordered table-hover" GridLines="None" RowStyle-Font-Size=X-Small>
                    <Columns>
                        <asp:BoundField DataField="nro" HeaderText="N°" >
                            <HeaderStyle Font-Bold="True"  />
                            <ItemStyle Width="5%" HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nrodocident" HeaderText="NRO DOC" >
                            <HeaderStyle Font-Bold="True"  />
                            <ItemStyle Width="25%" HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="emailusat" HeaderText="EMAIL USAT" >
                            <HeaderStyle Font-Bold="True"  />
                            <ItemStyle Width="45%" HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="clave" HeaderText="CLAVE" >
                            <HeaderStyle Font-Bold="True"  />
                            <ItemStyle Width="25%" HorizontalAlign="left" />
                        </asp:BoundField>
                       <asp:BoundField DataField="procesado" HeaderText="PROCESADO" >
                            <HeaderStyle Font-Bold="True"  />
                            <ItemStyle Width="10%" HorizontalAlign="left" />
                        </asp:BoundField>
                     
                       </Columns>
                        <EmptyDataTemplate>
                        No se ha importado ningun archivo
                        </EmptyDataTemplate>
                        <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                    Font-Size="14px" />                        
                        <AlternatingRowStyle BackColor="#F7F6F4"></AlternatingRowStyle>
                            <EditRowStyle BackColor="#FFFFCC" />
                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                </asp:GridView>
                            </div>

                        </div>

                    </div>
                </div>

            </div>
            <div class="panel-footer">
              
                <asp:Button ID="btnImportar" runat="server" Text="Importar" class="btn btn-info" />
                <asp:Button ID="btnGuardar" runat="server" Text="Actualizar" class="btn btn-primary" />
              
            </div>
        </div>

    </form>
    <script src="../assets/vendor_components/import-excel/dist/cpexcel.js"></script>
    <script src="../assets/vendor_components/import-excel/shim.js"></script>
    <script src="../assets/vendor_components/import-excel/jszip.js"></script>
    <script src="../assets/vendor_components/import-excel/xlsx.js"></script>
    <script>
        /*jshint browser:true */
        /* eslint-env browser */
        /*global Uint8Array */
        /*global XLSX */
        /* exported b64it, setfmt */
        /* eslint no-use-before-define:0 */

        var X = XLSX;
        var XW = {
            /* worker message */
            msg: 'xlsx',
            /* worker scripts */
            worker: './xlsxworker.js'
        };

        var global_wb;
        var per = [];
        var arr = [];
        var process_wb = (function () {
            var OUT = document.getElementById("<%=info.ClientID %>");

        var get_format = (function () {
            var radios = document.getElementsByName("format");
            return function () {
                for (var i = 0; i < radios.length; ++i) if (radios[i].checked || radios.length === 1) return radios[i].value;
            };
        })();

        var to_json = function to_json(workbook) {
            var result = {};
            workbook.SheetNames.forEach(function (sheetName) {
                var roa = X.utils.sheet_to_json(workbook.Sheets[sheetName], { header: 1 });
                if (roa.length) result[sheetName] = roa;
            });
            arr = result;
            console.log(arr);
            return JSON.stringify(result, 2, 2);
        };




        return function process_wb(wb) {
            global_wb = wb;
            var output = "";
            switch (get_format()) {

                case "json": output = to_json(wb); break;
                default: output = "";
            }

            if (OUT.innerText === undefined) OUT.textContent = output;
            else OUT.innerText = output;
            //if(typeof console !== 'undefined') console.log("output", new Date());

            if (get_format() == "json") {
                fnImportar();
            }

        };
    })();

        var setfmt = window.setfmt = function setfmt() { if (global_wb) process_wb(global_wb); };

        var b64it = window.b64it = (function () {
            var tarea = document.getElementById('b64data');
            return function b64it() {
                //if(typeof console !== 'undefined') console.log("onload", new Date());
                var wb = X.read(tarea.value, { type: 'base64', WTF: false });
                process_wb(wb);
            };
        })();

        var do_file = (function () {
            var rABS = typeof FileReader !== "undefined" && (FileReader.prototype || {}).readAsBinaryString;
            var domrabs = false;
            if (!rABS) domrabs.disabled = !(domrabs.checked = false);

            var use_worker = typeof Worker !== 'undefined';
            var domwork = false;
            if (!use_worker) domwork.disabled = !(domwork.checked = false);

            var xw = function xw(data, cb) {
                var worker = new Worker(XW.worker);
                worker.onmessage = function (e) {
                    switch (e.data.t) {
                        case 'ready': break;
                        case 'e': console.error(e.data.d); break;
                        case XW.msg: cb(JSON.parse(e.data.d)); break;
                    }
                };
                worker.postMessage({ d: data, b: rABS ? 'binary' : 'array' });
            };

            return function do_file(files) {
                rABS = domrabs.checked;
                use_worker = domwork.checked;
                var f = files[0];
                var reader = new FileReader();
                reader.onload = function (e) {
                    //if(typeof console !== 'undefined') console.log("onload", new Date(), rABS, use_worker);
                    var data = e.target.result;
                    if (!rABS) data = new Uint8Array(data);
                    if (use_worker) xw(data, process_wb);
                    else process_wb(X.read(data, { type: rABS ? 'binary' : 'array' }));
                };
                if (rABS) reader.readAsBinaryString(f);
                else reader.readAsArrayBuffer(f);
            };
        })();

        (function () {
            var drop = document.getElementById('drop');
            if (!drop.addEventListener) return;

            function handleDrop(e) {
                e.stopPropagation();
                e.preventDefault();
                do_file(e.dataTransfer.files);
            }

            function handleDragover(e) {
                e.stopPropagation();
                e.preventDefault();
                e.dataTransfer.dropEffect = 'copy';
            }

            drop.addEventListener('dragenter', handleDragover, false);
            drop.addEventListener('dragover', handleDragover, false);
            drop.addEventListener('drop', handleDrop, false);
        })();

        (function () {
            var xlf = document.getElementById('xlf');
            if (!xlf.addEventListener) return;
            function handleFile(e) { do_file(e.target.files); }
            xlf.addEventListener('change', handleFile, false);
        })();
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-36810333-1']);
        _gaq.push(['_trackPageview']);
        /*
        (function() {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
        */


        function fnImportar() {

            var obj = {};

            var i = 0;
            var j = 0;
            var key;


            for (var idx in arr) {
                key = arr[idx];
                obj[i] = key;
                i++;
            }
            console.log(obj[0]);

            jQuery.each(obj[0], function (i, val) {
                if (i >= 1) {
                    console.log(val);
                    per[j] = val;
                    j++;
                }
            });


            var email = '';
            var clave = '';
            var l = per.length;
            for (i = 0; i < l; i++) {
                console.log(per[i]);
                email += per[i][0] + ',';
                clave += per[i][1] + ',';
            }

         document.getElementById("<%=email.ClientID%>").value = email;
         document.getElementById("<%=clave.ClientID%>").value = clave;
         document.getElementById("<%=btnImportar.ClientID %>").click();



    }


    </script>
</body>
</html>
