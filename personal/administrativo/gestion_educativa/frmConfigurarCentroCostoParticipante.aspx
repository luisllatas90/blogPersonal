<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConfigurarCentroCostoParticipante.aspx.vb" Inherits="administrativo_gestion_educativa_frmConfigurarCentroCostoParticipante" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Configurar Centros de Costo por Tipo de Participante</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css">
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/configurarCentroCostoParticipante.css">
</head>
<body>
    <form id="frmConfigurarCentroCostoParticipante" runat="server">
        <asp:ScriptManager ID="scmConfigurarCentroCostoParticipante" runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <div class="card main">
                <div class="card-body">
                    <h5 class="title">Configurar Convenio</h5>
                    <hr>
                    <div class="form-group row">
                        <label for="cmbTipoEstudio" class="col-sm-2 col-form-label form-control-sm">Tipo de Estudio:</label>
                        <div class="col-sm-3">
                            <asp:UpdatePanel id="udpTipoEstudio" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cmbTipoEstudio" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-none-selected-text="No hay elementos">
                                        <asp:ListItem Selected="True" Value="-1">-- Seleccione --</asp:ListItem>
                                        <asp:ListItem Value="1">ESCUELA PRE</asp:ListItem>
                                        <asp:ListItem Value="5">POSTGRADO</asp:ListItem>
                                        <asp:ListItem Value="8">SEGUNDA ESPECIALIDAD</asp:ListItem>
                                        <asp:ListItem Value="10">GO USAT</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="from-group row">
                        <label for="cmbCentroCosto" class="col-sm-2 col-form-label form-control-sm">Centro de Costo:</label>
                        <div class="col-sm-8">
                            <asp:UpdatePanel id="udpCentroCosto" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cmbCentroCosto" runat="server" AutoPostBack="true" Enabled="false" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card servicios">
                <div class="card-header">
                    <span>Servicios asociados</span>
                    <img src="img/loading.gif" class="loading-gif">
                </div>
                <div class="card-body">
                    <asp:UpdatePanel id="udpBtnListarServicios" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <button type="button" id="btnListarServicios" runat="server" class="btn btn-accion d-none">
                                <i class="fa fa-sync-alt"></i>
                                <span class="text">Listar</span>
                            </button>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel id="udpServicioConcepto" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:GridView ID="grwServicioConcepto" runat="server" AutoGenerateColumns="false" DataKeyNames="codigo_Sco,codigo_Scc" CssClass="table table-sm" GridLines="None">
                                <Columns>
                                    <asp:BoundField HeaderText="Nro" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="descripcion_Sco" HeaderText="Servicio" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="precio_Sco" HeaderText="Precio" />
                                    <asp:BoundField DataField="fechaVencimiento_Sco" HeaderText="Fec. Vencimiento" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:TemplateField HeaderText="Genera Mora?">
                                        <ItemTemplate><%#IIf(Boolean.Parse(Eval("generaMora_Sco").ToString()), "Si", "No")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="nroPartes_Sco" HeaderText="Cuotas" />
                                    <asp:TemplateField HeaderText="Inscripción?">
                                        <ItemTemplate>
                                            <div class="custom-control custom-checkbox without-label">
                                                <asp:CheckBox ID="chkServicioConcepto" runat="server" OnCheckedChanged="chkServicioConcepto_CheckedChanged" AutoPostBack="true" />
                                                <asp:Label runat="server" AssociatedControlID="chkServicioConcepto" CssClass="custom-control-label" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Operaciones">
                                        <ItemTemplate>
                                            <button type="button" id="btnDetalleConvenio" runat="server" OnServerClick="btnDetalleConvenio_Click" class="btn btn-primary btn-sm" data-cparc='<%#Eval("codigo_cparc")%>' data-scc='<%#Eval("codigo_Scc")%>'>
                                                <i class='fa fa-cog'></i>
                                            </button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField ItemStyle-CssClass="operacion" HeaderText="Convenio" /> --%>
                                </Columns>
                                <HeaderStyle CssClass="thead-dark alt" />
                                <RowStyle HorizontalAlign="Center"></RowStyle>
                        </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="mdlDetalleConvenio" class="modal fade" tabindex="-1" role="dialog" data-postback-listar="true">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpDetalleConvenio" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <span class="modal-title">Convenio</span>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <iframe id="ifrmDetalleConvenio" src="frmDetalleConvenio.aspx" runat="server" name="ifrmDetalleConvenio" frameborder="0" width="100%" scrolling="no"></iframe>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-primary submit" id="btnEnviar">Guardar</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="mdlMensajesCliente" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span class="modal-title">Mensaje</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div id="mensajePostBack" class="alert alert-light"></div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <form action="frmDetalleConvenio.aspx" id="frmDetalleConvenio" target="ifrmDetalleConvenio" method="POST">
        <input type="hidden" id="hddCodigoScc" runat="server" name="codigoScc">
    </form>
    <!-- Scripts externos -->
    <script src="../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>
    <script src="../../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="../../assets/iframeresizer/iframeResizer.min.js"></script>
    <!-- Scripts propios -->
    <script src="js/funciones.js"></script>
    <script src="js/configurarCentroCostoParticipante.js"></script>
    <script type="text/javascript">
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id

            switch (controlId) {
                case 'cmbCentroCosto':
                case 'btnListarServicios':
                    AlternarLoadingGif('interno', false);
                    break;
            }

            if (controlId.indexOf('chkServicioConcepto') > 1) {
                AlternarLoadingGif('interno', false);
            }

            if (controlId.indexOf('btnDetalleConvenio') > 1) {
                AlternarLoadingGif('interno', false);
                AtenuarBoton(controlId, false);
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function(sender, args) {
            var error = args.get_error();
            if (error) {
                // Manejar el error
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (sender, args) {
            var updatedPanels = args.get_panelsUpdated();  

            var udpFormUpdated = false
            for (var i = 0; i < updatedPanels.length; i++) {
                var udpPanelId = updatedPanels[i].id;
                switch (udpPanelId) {
                    case 'udpCentroCosto':
                        InitComboCentroCosto();
                        break;
                }
            }
        });

        Sys.Application.add_load(function() {
            var elem = document.getElementById(controlId);

            switch (controlId) {
                case 'cmbCentroCosto':
                case 'btnListarServicios':
                    AlternarLoadingGif('interno', true);
                    break;
            }

            if (controlId.indexOf('chkServicioConcepto') > 1) {
                AlternarLoadingGif('interno', true);
            } 

            if (controlId.indexOf('btnDetalleConvenio') > 1) {
                MostrarModalDetalleConvenio(controlId, 'btnSubmit');
            }
        });
    </script>
</body>
</html>