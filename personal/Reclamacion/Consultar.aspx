<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Consultar.aspx.vb" Inherits="Consultar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <meta http-equiv="Pragma" content="no-cache" />
    <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <%--Compatibilidad con IE--%>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <title>Estado de Reclamaciones</title>
    <link href="libs/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="libs/bootstrap-select-1.12.4/css/bootstrap-select.min.css" rel="stylesheet"
        type="text/css" />
    <link href="libs/fontawesome-5.2/css/all.min.css" rel="stylesheet" />

    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <script src="js/jquery-ui.js" type="text/javascript"></script>

    <script src="libs/bootstrap-3.3.7-dist/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="libs/bootstrap-select-1.12.4/js/bootstrap-select.min.js" type="text/javascript"></script>

    <script src="js/bootbox.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#txtSol").keydown(function(e) {
                if (e.keyCode == 13 && e.shiftKey) {
                    e.preventDefault();
                }
            });
        });
        
        function abrirModalAtender(cod, per, ped, sol, nom, id) {
            $('#txtCod').val(cod);
            $('#txtPer').val(per);
            $('#txtPed').val(ped);
            $('#txtSol').val(sol);
            $('#txtNom').val(nom);

            if ($('#txtSol').val() !== "" || $('#txtSol').length > 1) {
                $("#txtSol").attr('disabled', true);
            } else {
                $("#txtSol").attr('disabled', false);
            }

            $('#lblTitAtender').html("Cambio de estado del Reclamo: " + cod);
            $('#modalAtender').data("ID", id);
            $('#modalAtender').modal('show');
        }

        function cerrarModalAtender() {
            $('#modalAtender').modal('hide');

            $("#btnBuscar").click();
        }

        function cambiarEstado(e) {
            var id = $('#modalAtender').data("ID");

            if ($("#txtSol").val() == "" || $("#txtSol").val() == null || $("#txtSol").val().length < 5) {
                bootbox.alert({
                    message: "Ingrese una Respuesta válida",
                    size: 'small',
                    callback: function() {
                        $("#txtSol").focus();
                    }
                });

                return;
            }

            $("#btnAceptAtend").click();
            $("#btnCambiarAtenc").text("");
            $("#btnCambiarAtenc").html("<i class='fa fa-spin fa-spinner'></i> Procesando...");
            $("#btnCambiarAtenc").attr("disabled", true);
            $("#btnCambiarAtenc").css("cursor", "progress");

            setTimeout(function() {
                var $check = $("[id$= '" + id + "']");

                $check.prop("disabled", true);
                $('#modalAtender').modal('hide');

                __doPostBack('btnBuscar', '');

                //$("#btnBuscar").click();
            }, 6000);
        }

        function abrirModalAsignar(cod) {
            $('#txtCod').val(cod);
            $("#btnCambiarAtenc").removeAttr("disabled");
            $("#btnCambiarAtenc").css("cursor", "default");

            $('#modalAsignar').modal('show');
        }

        function abrirModalDetalle(cod, nom, dni, tel, mai, dir, apo, tbc, mto, des, tip, ped, sol) {
            $('#txtCod').val(cod);
            $('#txtNombre').val(nom);
            $('#txtDNI').val(dni);
            $('#txtTelefono').val(tel);
            $('#txtEmail').val(mai);
            $('#txtDireccion').val(dir);
            $('#txtApoderado').val(apo);

            if (tbc == "S") {
                $("#rbtServicio").prop("checked", true);
            } else {
                $("#rbtProducto").attr("checked", "checked");
            }

            $('#txtMonto').val(mto);
            $('#txtDescripcion').val(des);

            if (tip == "R") {
                $("#rbtReclamo").prop("checked", true);
            } else {
                $("#rbtQueja").prop("checked", true);
            }

            $('#txtPedido').val(ped);
            $('#txtSolucion').val(sol);

            $('#lblTitDetalle').html("Detalle del Reclamo: " + cod);

            $('#modalDetalle').modal('show');
        }

        function imprimir(DivID) {
            var disp_setting = "toolbar=yes,location=no,";
            disp_setting += "directories=yes,menubar=yes,";
            disp_setting += "scrollbars=yes,width=650, height=600, left=100, top=25";
            var content_vlue = document.getElementById(DivID).innerHTML;
            var docprint = window.open("", "", disp_setting);
            docprint.document.open();
            docprint.document.write('<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"');
            docprint.document.write('"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">');
            docprint.document.write('<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">');
            docprint.document.write('<head>');
            docprint.document.write('<style type="text/css">body{ margin:0px;');
            docprint.document.write('font-family:verdana,Arial;color:#000;');
            docprint.document.write('font-family:Verdana, Geneva, sans-serif; font-size:12px;}');
            docprint.document.write('a{color:#000;text-decoration:none;} </style>');
            docprint.document.write('</head><body onLoad="self.print()"><center>');
            docprint.document.write(content_vlue);
            docprint.document.write('</center></body></html>');
            docprint.document.close();
            docprint.focus();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" autocomplete="Off">
    <div class="container-fluid">
        <div class="panel panel-default" style="margin-top: 10px; background-color: #FBFBFB;">
            <div class="panel-body">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">
                                <div class="form-group">
                                    <label for="dtpDesde">
                                        Desde</label>
                                    <input type="text" id="dtpDesde" class="form-control" runat="server" />
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-2">
                                <div class="form-group">
                                    <label for="dtpHasta">
                                        Hasta</label>
                                    <input type="text" id="dtpHasta" class="form-control" runat="server" />
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-8">
                                <div class="form-group">
                                    <label for="cboEstado">
                                        Estado</label>
                                    <select name="cboEstado" id="cboEstado" class="form-control" runat="server">
                                        <option value="%">TODOS</option>
                                        <option value="P">PENDIENTES</option>
                                        <option value="A">ATENDIDOS</option>
                                        <option value="R">EN REVISIÓN</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-12">
                                <div class="form-group">
                                    <label for="txtFiltro">
                                        Búsqueda (DNI o Nombre)</label>
                                    <input name="txtFiltro" type="text" id="txtFiltro" class="form-control upper-case"
                                        runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="text-align: right;">
                    <asp:Button ID="btnBuscar" OnClick="btnBuscar_click" runat="server" Text="Buscar"
                        class="btn btn-usat-color" />
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div class="panel panel-default" style="margin-top: 10px; background-color: #FBFBFB;">
            
                <div class="table-responsive">
                    <asp:GridView ID="gvReclamos" runat="server" DataKeyNames="codigo, personal, pedido, solucion, atendido, domicilio, apoderado, monto, descripcion"
                        CssClass="table table-sm table-hover" AutoGenerateColumns="False" GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="CÓDIGO"></asp:BoundField>
                            <asp:BoundField DataField="fecha" HeaderText="FECHA"></asp:BoundField>
                            <asp:BoundField DataField="nroDoc" HeaderText="DNI/CU"></asp:BoundField>
                            <asp:BoundField DataField="nombres" HeaderText="RECLAMANTE" HtmlEncode="False"></asp:BoundField>
                            <asp:BoundField DataField="email" HeaderText="E-MAIL/TELF."></asp:BoundField>
                            <asp:BoundField DataField="tipo" HeaderText="TIPO"></asp:BoundField>
                            <asp:BoundField DataField="estado" HeaderText="ESTADO"></asp:BoundField>
                            <asp:BoundField DataField="dias" HeaderText="DÍAS"></asp:BoundField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="personal" runat="server" Value='<%# Eval("personal") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="pedido" runat="server" Value='<%# Eval("pedido") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="solucion" runat="server" Value='<%# Eval("solucion") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="atendido" runat="server" Value='<%# Eval("atendido") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="domicilio" runat="server" Value='<%# Eval("domicilio") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="LEER" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="IMPR" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="ASIG" Visible="false" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="ATEN" />
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="apoderado" runat="server" Value='<%# Eval("apoderado") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="monto" runat="server" Value='<%# Eval("monto") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="descripcion" runat="server" Value='<%# Eval("descripcion") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No se ha registrado ningún reclamo
                        </EmptyDataTemplate>
                        <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                            Font-Size="12px" />
                        <RowStyle Font-Size="11px" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                    </asp:GridView>
                </div>
            
        </div>
    </div>
    <!-- Modal -->
    <div id="modalAtender" runat="server" tabindex="-1" role="dialog" class="modal fade"
        data-backdrop="static" data-keyboard="false" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <!-- Modal content -->
            <div class="modal-content">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title" id="lblTitAtender">
                        Cambio de estado</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12">
                            <div class="form-group">
                                <label for="txtPed">
                                    Detalle del reclamo</label>
                                <asp:TextBox ID="txtPed" name="txtPed" runat="server" CssClass="form-control" MaxLength="8000"
                                    TextMode="multiline" Columns="50" Rows="6" placeholder="Detalle del pedido" ReadOnly="true"
                                    Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-12">
                            <div class="form-group">
                                <label for="txtSol">
                                    Respuesta</label>
                                <asp:TextBox ID="txtSol" name="txtSol" runat="server" CssClass="form-control" MaxLength="8000"
                                    TextMode="multiline" Columns="50" Rows="6" placeholder="Ingrese descripción de la solución al reclamo"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-12">
                            <div class="form-group">
                                <center>
                                    <label class="control-label">
                                        ¿Desea cambiar el estado a 'atendido'?</label>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnAceptAtend" OnClick="btnGuardar_click" runat="server" Style="display: none"
                            class="btn btn-info" />
                        <a id="btnCambiarAtenc" class="btn btn-info" onclick="cambiarEstado(event);">Guardar</a>
                        <asp:Button ID="btnCancelAtend" runat="server" Text="Cancelar" class="btn btn-danger" />
                    </center>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal -->
    <div id="modalAsignar" runat="server" tabindex="-1" role="dialog" class="modal fade"
        data-backdrop="static" data-keyboard="false" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <!-- Modal content -->
            <div class="modal-content">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Asignar personal</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12">
                            <div class="form-group">
                                <label for="ddlPersonal">
                                    Digite el apellido del personal que atenderá éste caso</label>
                                <asp:DropDownList ID="ddlPersonal" runat="server" CssClass="form-control selectpicker"
                                    data-live-search="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnAceptAsig" OnClick="btnAsignar_click" runat="server" Text="Asignar"
                            class="btn btn-info" />
                        <asp:Button ID="btnCancelAsig" runat="server" Text="Cancelar" class="btn btn-danger" />
                    </center>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal -->
    <div id="modalDetalle" runat="server" tabindex="-1" role="dialog" class="modal fade"
        aria-hidden="true">
        <div id="modalDet" class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title" id="lblTitDetalle">
                        Detalle del Reclamo</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12 col-sm-9">
                            <div class="form-group">
                                <label for="txtNombre">
                                    Nombre del reclamante</label>
                                <input name="txtNombre" type="text" id="txtNombre" class="form-control upper-case"
                                    readonly="readonly" disabled="disabled" />
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-3">
                            <div class="form-group">
                                <label for="txtDNI">
                                    DNI / CE</label>
                                <input name="txtDNI" type="text" id="txtDNI" class="form-control" readonly="readonly"
                                    disabled="disabled" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-3">
                            <div class="form-group">
                                <label for="txtTelefono">
                                    Teléfono</label>
                                <input name="txtTelefono" type="text" id="txtTelefono" class="form-control" readonly="readonly"
                                    disabled="disabled" />
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-4">
                            <div class="form-group">
                                <label for="txtEmail">
                                    E-mail</label>
                                <input name="txtEmail" type="text" id="txtEmail" class="form-control upper-case"
                                    readonly="readonly" disabled="disabled" />
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-5">
                            <div class="form-group">
                                <label for="txtDireccion">
                                    Dirección</label>
                                <input name="txtDireccion" type="text" id="txtDireccion" class="form-control upper-case"
                                    readonly="readonly" disabled="disabled" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-12">
                            <div class="form-group">
                                <label for="txtApoderado">
                                    Apoderado</label>
                                <input name="txtApoderado" type="text" id="txtApoderado" class="form-control upper-case"
                                    placeholder="Nombre del apoderado" readonly="readonly" disabled="disabled" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-6 col-sm-3">
                            <div class="radio">
                                <label>
                                    <input type="radio" name="grupoBien" id="rbtServicio" value="S" disabled="disabled" />
                                    <span style="font-weight: bold; cursor: default;">Servicio</span>
                                </label>
                            </div>
                        </div>
                        <div class="col-xs-6 col-sm-9">
                            <div class="radio">
                                <label>
                                    <input type="radio" name="grupoBien" id="rbtProducto" value="P" disabled="disabled" />
                                    <span style="font-weight: bold; cursor: default;">Producto</span>
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-3">
                            <div class="form-group">
                                <label for="txtMonto">
                                    Monto reclamado</label>
                                <input name="txtMonto" type="text" id="txtMonto" class="form-control" readonly="readonly"
                                    disabled="disabled" />
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-9">
                            <div class="form-group">
                                <label for="txtDescripcion">
                                    Descripción del bien contratado</label>
                                <input name="txtDescripcion" type="text" id="txtDescripcion" class="form-control upper-case"
                                    readonly="readonly" disabled="disabled" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-6 col-sm-3">
                            <div class="radio">
                                <label>
                                    <input type="radio" name="grupoTipo" id="rbtReclamo" value="R" disabled="disabled" />
                                    <span style="font-weight: bold; cursor: default;">Reclamo</span>
                                </label>
                            </div>
                        </div>
                        <div class="col-xs-6 col-sm-9">
                            <div class="radio">
                                <label>
                                    <input type="radio" name="grupoTipo" id="rbtQueja" value="Q" disabled="disabled" />
                                    <span style="font-weight: bold; cursor: default;">Queja</span>
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-12">
                            <div class="form-group">
                                <label for="txtPedido">
                                    Descripción del pedido</label>
                                <textarea cols="0" name="txtPedido" id="txtPedido" class="form-control" rows="6"
                                    readonly="readonly" disabled="disabled"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-12">
                            <div class="form-group">
                                <label for="txtSolucion">
                                    Respuesta o solución</label>
                                <textarea cols="0" name="txtSolucion" id="txtSolucion" class="form-control" rows="6"
                                    readonly="readonly" disabled="disabled"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <center>
                        <!-- 
                        <button type="button" id="btnImprimir" class="btn btn-success" onclick="imprimir('modalDetalle');"
                            data-dismiss="modal" visible="false">
                            Imprimir</button> -->
                        <button type="button" id="btnCerrar" class="btn btn-danger" data-dismiss="modal">
                            &nbsp; Cerrar &nbsp;</button>
                    </center>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="txtCod" runat="server" />
    <asp:HiddenField ID="txtPer" runat="server" />
    <asp:HiddenField ID="txtNom" runat="server" />
    <asp:HiddenField ID="txtDir" runat="server" />
    <asp:HiddenField ID="txtMail" runat="server" />
    <asp:HiddenField ID="txtFecha" runat="server" />
    </form>

    <script type="text/javascript">
        function getLastDay(m, y) {
            return m === 2 ? y & 3 || !(y % 25) && y & 15 ? 28 : 29 : 30 + (m + (m >> 3) & 1);
        }

        $(function() {
            var mydate = new Date();
            var year = mydate.getYear();
            var month = '0' + (mydate.getMonth() + 1);
            month = month.substr(month.length - 2);

            if (year < 1000) {
                year += 1900
            }

            $("#dtpDesde").datepicker({
                changeMonth: true,
                changeYear: true,
                regional: "es-ES",
                showAnim: "drop",
                dateFormat: "dd/mm/yy",
                altFormat: "dd/mm/yy" //,
                //altField: "#dtpHasta"
            });
            $("#dtpHasta").datepicker({
                changeMonth: true,
                changeYear: true,
                regional: "es-ES",
                showAnim: "drop",
                dateFormat: "dd/mm/yy",
                altFormat: "dd/mm/yy"
            });
            $("#dtpDesde").val('01/' + month + '/' + year);
            $("#dtpHasta").val((getLastDay(mydate.getMonth(), year) - 1) + '/' + month + '/' + year);
        });
    </script>

</body>
</html>
