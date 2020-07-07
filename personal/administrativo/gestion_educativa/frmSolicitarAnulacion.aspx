<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSolicitarAnulacion.aspx.vb" Inherits="administrativo_gestion_educativa_frmSolicitarAnulacion" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>.:: Solicitar anulación ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css">
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/solicitarAnulacion.css">
</head>

<body>
    <form id="frmSolicitarAnulacion" runat="server">
        <input type="hidden" name="txtSaldo" id="txtSaldo" runat="server">
        <asp:ScriptManager ID="scmSolicitarAnulacion" runat="server"></asp:ScriptManager>
        <div class="container-fluid" id="mainContainer" runat="server">
            <div class="card">
                <div class="card-header">Solicitar anulación de deudas</div>
                <div class="card-body">
                    <div class="row form-group" id="divControlesDatos" runat="server">
                        <label for="cmbMotivoAnulacion" class="col-sm-2 col-form-label form-control-sm">Motivo:</label>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="cmbMotivoAnulacion" runat="server" AutoPostBack="False" CssClass="form-control form-control-sm selectpicker"
                                data-live-search="true">
                            </asp:DropDownList>
                        </div>
                        <label for="txtObservacion" class="col-sm-1 col-form-label form-control-sm">Obs.:</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control form-control-sm"
                                placeholder="Observación" />
                        </div>
                    </div>
                    <asp:UpdatePanel id="udpDeudas" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:GridView ID="grwDeudas" runat="server" AutoGenerateColumns="false" DataKeyNames="codigo_Deu"
                                ShowFooter="True" CssClass="table table-sm" GridLines="None">
                                <Columns>
                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Servicio" HeaderText="Servicio" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="center" DataField="Fecha_Operacion"
                                        HeaderText="Fecha" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="right" DataField="Cargos" HeaderText="Cargos" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="right" DataField="Abonos" HeaderText="Abonos" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="right" DataField="Saldo" HeaderText="Saldo" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="observacion_Deu"
                                        HeaderText="Observación" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="center" DataField="FechaVenc" HeaderText="Fec. Venc." />
                                    <asp:TemplateField HeaderText="Anular">
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control form-control-sm" />
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control form-control-sm decimal"
                                                data-decimales="2" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="thead-dark alt" />
                                <RowStyle HorizontalAlign="Center"></RowStyle>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <label id="lblMEnsajeDeuda" runat="server" class="col-sm-11 col-form-label form-control-sm"></label>
                    </div>
                </div>
                <asp:UpdatePanel id="udpBotonesAccion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div id="divBotonesAccion" runat="server" class="card-footer">
                            <div class="row">
                                <div class="col-sm-12 text-right">
                                    <button type="button" id="btnCancelar" runat="server" class="btn btn-outline-secondary">Cancelar</button>
                                    <button type="button" id="btnAnular" runat="server" class="btn btn-accion btn-azul"
                                        disabled="disabled">
                                        <i class="fa fa-save"></i>
                                        <span class="text">Solicitar Anulación</span>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="mdlMensajesServidor" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <asp:UpdatePanel ID="udpMensajeServidorParametros" runat="server" UpdateMode="Conditional"
                        ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div id="divMdlMenServParametros" runat="server" data-mostrar="false"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="modal-header">
                        <asp:UpdatePanel ID="udpMensajeServidorHeader" runat="server" UpdateMode="Conditional"
                            ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <span id="spnMensajeServidorTitulo" runat="server" class="modal-title">Respuesta del
                                    Servidor</span>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="udpMensajeServidorBody" runat="server" UpdateMode="Conditional"
                            ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div id="respuestaPostback" runat="server" class="alert" data-ispostback="false"
                                    data-rpta="" data-msg="" data-control=""></div>
                                <asp:Label ID="lblErrorMessage" runat="server" CssClass="d-none" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <asp:UpdatePanel ID="udpMensajeServidorFooter" runat="server" UpdateMode="Conditional"
                            ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <button type="button" id="btnMensajeCerrar" runat="server" class="btn btn-default"
                                    data-dismiss="modal">Cerrar</button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
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
                        <div id="textoMensaje" class="alert alert-light"></div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- Scripts externos -->
    <script src="../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>
    <script src="../../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="../../assets/iframeresizer/iframeResizer.contentWindow.min.js"></script>
    <!-- Scripts propios -->
    <script src="js/funciones.js"></script>
    <script src="js/solicitarAnulacion.js"></script>
    <script type="text/javascript">
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id

            switch (controlId) {
                case 'btnAnular':
                    AtenuarBoton(controlId, false);
                    AtenuarBoton('btnCancelar', false);
                    break;
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
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
                }
            }
        });

        Sys.Application.add_load(function () {
            var elem = document.getElementById(controlId);

            switch (controlId) {
                case 'btnAnular':
                    AtenuarBoton(controlId, true);
                    AtenuarBoton('btnCancelar', true);
                    SubmitPostBack();
                    break;
            }

            RevisarRespuestaServidor();
        });
    </script>
</body>

</html>