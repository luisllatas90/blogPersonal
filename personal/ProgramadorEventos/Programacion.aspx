<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Programacion.aspx.vb" Inherits="Programacion" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Programación de comunicaciones</title>
    <link href="libs/bootstrap-4.1/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/programacion.css?4" rel="stylesheet" type="text/css" />
    <link href="libs/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />

    <script src="libs/jquery/jquery-3.3.1.js" type="text/javascript"></script>

    <script src="libs/popper-1.14.4/js/popper.js" type="text/javascript"></script>

    <script src="libs/bootstrap-4.1/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="libs/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>

    <script src="libs/iframeresizer/iframeResizer.min.js" type="text/javascript"></script>

    <script src="js/programacion.js" type="text/javascript"></script>

    <script src="js/bootbox.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        function mostrarMensaje(mensaje, tipo) {
            var backcolor = "#64C45C";
            var forecolor = "#FFFFFF";

            if (tipo == "danger") {
                backcolor = "#FC7E7E";
                forecolor = "#FFFFFF";
            } else if (tipo == "warning") {
                backcolor = "#FFDC96";
                forecolor = "#000000";
            }

            var box = bootbox.alert({ message: mensaje, backdrop: true });
            box.find('.modal-body').css({ 'background-color': backcolor });
            box.find('.bootbox-body').css({ 'color': forecolor });
            box.find(".btn-primary").removeClass("btn-primary").addClass("btn-" + tipo);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" autocomplete="off" method="post">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <div class="container-fluid">
        <div class="panel panel-default" style="margin-top: 15px; background-color: #FBFBFB;">
            <asp:UpdatePanel ID="udpCabecera" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="panel panel-body">
                        <h2 class="page-header" style="margin-top: 0px; margin-bottom: 5px; border-bottom: 1px solid #FFC4C4;">
                            <span style="color: rgb(205,0,0);">Programación de comunicaciones</span></h2>
                        <div class="row" style="padding-left: 1em">
                            <label runat="server" id="lblEvento" style="font-weight: bold;">
                                EXPOUSAT</label>
                        </div>
                        <div class="row" style="margin-top: 15px; margin-bottom: 15px; padding-left: 1em">
                            <div class="col-xs-12 col-sm-4">
                                <button type="button" id="btnAtras" class="btn btn-sm btn-danger" runat="server"
                                    onclick="JavaScript:window.history.back(1); return false;">
                                    <i class='fa fa-home'></i>
                                </button>
                                &nbsp; &nbsp;
                                <button type="button" id="btnRegistrar" class="btn btn-sm btn-success" runat="server">
                                    Nueva Programación
                                </button>
                                &nbsp;
                                <button type="button" id="btnRefrescar" class="btn btn-sm btn-info" runat="server">
                                    Buscar
                                </button>
                            </div>
                            <div class="col-xs-12 col-sm-8 text-right">
                                <label for="ddlCarrera" id="lblCarrera" runat="server">
                                Carrera:
                                </label>
                                <asp:DropDownList ID="ddlCarrera" runat="server" CssClass="form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="panel panel-body">
                <div class="table-responsive">
                    <asp:UpdatePanel ID="udpProgramacion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:GridView ID="grwProgramacion" runat="server" AutoGenerateColumns="false" DataKeyNames="codigo_pro, evento, scheduleId_pro"
                                CssClass="table table-sm table-bordered table-hover" GridLines="None">
                                <Columns>
                                    <asp:BoundField DataField="codigo_pro" HeaderText="CÓDIGO" />
                                    <asp:BoundField DataField="descripcion" HeaderText="DESCRIPCIÓN" />
                                    <asp:BoundField DataField="mensaje" HeaderText="ID / MENSAJE" />
                                    <asp:BoundField DataField="registrado" HeaderText="REGISTRADO" />
                                    <asp:BoundField DataField="detalle" HeaderText="DETALLE" />
                                    <asp:BoundField DataField="estado" HeaderText="ESTADO" />
                                    <asp:TemplateField HeaderText="AVANCE" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <div class="progress">
                                                <div id="pg-success" 
                                                    class='progress-bar progress-bar-striped progress-bar-animated bg-success' 
                                                    role="progressbar"
                                                    style='width: <%# EnviadosPorcentaje(Eval("enviados"), Eval("total"), True) %>'>
                                                    <%# Eval("enviados") %>
                                                </div>
                                                <div id="pg-danger" 
                                                    class="progress-bar progress-bar-striped progress-bar-animated bg-danger" 
                                                    role="progressbar"
                                                    style="width: <%# RestantesPorcentaje(Eval("enviados"), Eval("total"), True) %>">
                                                    <%# Eval("total") - Eval("enviados") %>
                                                </div>
                                            </div>
                                            <div class="pg-total"><%# Eval("total") %></div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="EDITAR" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="ENVIAR" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="DESACT" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="LOG" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No se registró ninguna programación
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                    Font-Size="10px" />
                                <EditRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Programación Mantenimiento -->
    <div id="modalProgramar" class="modal fade" tabindex="-1" role="dialog" data-postback-listar="true"
        data-backdrop="static" data-keyboard="false" aria-hidden="true">
        <div class="modal-dialog modal-xl" role="document">
            <asp:UpdatePanel ID="panProgramacion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <!-- Modal content -->
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                            <span class="modal-title">Programar Comunicación</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <iframe src="" id="ifrmProgramacionMantenimiento" runat="server" frameborder="0"
                                width="100%" scrolling="no"></iframe>
                        </div>
                        <%-- <div class="modal-footer">
                            <center>
                                <button type="button" id="btnAceptar" class="btn btn-info submit">
                                    Aceptar</button>
                                <button type="button" id="btnCancelar" class="btn btn-danger" data-dismiss="modal">
                                    Cancelar</button>
                            </center>
                        </div> --%>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <!-- Modal Programación Mantenimiento -->
    <div id="mdlProgramacionLog" class="modal fade" tabindex="-1" role="dialog" data-postback-listar="true"
        data-backdrop="static" data-keyboard="false" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="panProgramacionLog" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <!-- Modal content -->
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                            <span class="modal-title">Log de comunicaciones</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <iframe src="" id="ifrmProgramacionLog" runat="server" frameborder="0"
                                width="100%" scrolling="no"></iframe>
                        </div>
                        <div class="modal-footer">
                            <center>
                                <button type="button" id="btnCerrar" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                            </center>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="mdlMensajes" class="modal fade" tabindex="-1" role="dialog" data-postback-listar="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="modal-title">Mensaje</span>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div id="mensajePostBack" class="alert alert-light">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var controlId = '';

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function(sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id;

            if (controlId.indexOf('btnRegistrar') > -1 
                || controlId.indexOf('btnEditar') 
                || controlId.indexOf('btnEnviar') 
                || controlId.indexOf('btnEstado') 
                || controlId.indexOf('btnLog')> -1) {
                AtenuarBoton(controlId, false);
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (sender, args) {
            var updatedPanels = args.get_panelsUpdated();

            var udpFormUpdated = false
            for (var i = 0; i < updatedPanels.length; i++) {
                var udpPanelId = updatedPanels[i].id;
                console.log(udpPanelId);
                switch (udpPanelId) {
                }
            }
        });

        Sys.Application.add_load(function() {
            if (controlId.indexOf('btnRegistrar') > -1) {
                FormularioRegistroProgramacion();
            }

            if (controlId.indexOf('btnRefrescar') > -1) {
                AtenuarBoton(controlId, true);
            }

            if (controlId.indexOf('btnEditar') > -1) {
                CargarModalProgramacion(controlId, 'btnRegistrar');
            }

            if (controlId.indexOf('btnLog') > -1) {
                CargarModalProgramacionLog(controlId);
            }
        });
    </script>

</body>
</html>
