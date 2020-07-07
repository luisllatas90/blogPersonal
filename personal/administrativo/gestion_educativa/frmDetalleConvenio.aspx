<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDetalleConvenio.aspx.vb" Inherits="administrativo_gestion_educativa_frmDetalleConvenio" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Inscripción de Interesado</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/smart-wizard/css/smart_wizard.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.css">
    <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/detalleConvenio.css?8">
</head>
<body>
    <form id="frmDetalleConvenio" runat="server">
        <asp:ScriptManager ID="scmDetalleConvenio" runat="server"></asp:ScriptManager>
        <div class="card">
            <div class="card-header">Información del Convenio</div>
            <div class="card-body">
                <div class="row form-group">
                    <label for="spnCentroCosto" class="col-sm-2 col-form-label form-control-sm">Centro de Costo:</label>
                    <div class="col-sm-10">
                        <span id="spnCentroCosto" runat="server" class="badge badge-light">CENTRO DE COSTO</span>
                    </div>
                </div>
                <div class="row form-group">
                    <label for="spnServicioConcepto" class="col-sm-2 col-form-label form-control-sm">Concepto:</label>
                    <div class="col-sm-4">
                        <span id="spnServicioConcepto" runat="server" class="badge badge-light">CUOTAS</span>
                    </div>
                    <label for="cmbTipoParticipante" class="col-sm-2 col-form-label form-control-sm">Tipo de Participante:</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="cmbTipoParticipante" runat="server" CssClass="form-control form-control-sm" />
                    </div>
                </div>
                <div class="row form-group">
                    <label for="txtPrecio" class="col-sm-2 col-form-label form-control-sm">Precio:</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="Precio (*)" Enabled=false />
                    </div>
                    <label for="txtOperacion" class="col-sm-2 offset-sm-2 col-form-label form-control-sm">Operación:</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtOperacion" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="Operación (*)" />
                    </div>
                </div>
                <div class="row form-group">
                    <label for="txtCuotas" class="col-sm-2 col-form-label form-control-sm">Cuotas:</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtCuotas" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="Cuotas (*)" Enabled=false />
                    </div>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-header">Cuotas</div>
            <div class="card-body">
                <div class="row form-group">
                    <label for="txtFechaPrimeraCuota" class="col-sm-2 offset-sm-2 col-form-label form-control-sm">Fec. 1° Cuota:</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtFechaPrimeraCuota" runat="server" CssClass="form-control form-control-sm" placeholder="1° Cuota" />
                    </div>
                    <div class="col-sm-3 text-left">
                        <asp:UpdatePanel id="udpBtnGenerarCuotas" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <button type="button" id="btnGenerarCuotas" runat="server" class="btn btn-accion btn-azul">
                                    <i class='fa fa-sync-alt'></i>
                                    <span class="text">Generar</span>
                                </button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <hr>
                <div class="row">
                    <div class="col-sm-10 offset-sm-1">
                        <asp:UpdatePanel id="udpCuotas" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:GridView ID="grwCuotas" runat="server" AutoGenerateColumns="false" CssClass="table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Nro" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <label for="txtNroCuota"><%# Eval("nro") %></label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtNroCuota" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="Cuota Nro" Enabled=false Text=<%# Bind("nro") %> />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Vence" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <label for="txtFechaVencimiento"><%# DateTime.Parse(Eval("fechaVencimiento").toString()).toString("d") %></label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtFechaVencimiento" runat="server" CssClass="form-control form-control-sm" placeholder="Fecha Vencimiento" Text=<%# Bind("fechaVencimiento") %> />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Monto" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <label for="txtMonto"><%# Eval("monto") %></label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control form-control-sm decimal" placeholder="Monto" data-decimales="2" Text=<%# Bind("monto") %> />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Operaciones" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btnEditar" ToolTip="Editar" CssClass="btn btn-sm btn-success" CommandName="Edit" Text='<i class="fa fa-edit"></i>' />
                                                <asp:LinkButton runat="server" ID="btnActualizar" ToolTip="Actualizar" CssClass="btn btn-sm btn-primary" CommandName="Update" Text='<i class="fa fa-save"></i>' />
                                                <asp:LinkButton runat="server" ID="btnCancelar" ToolTip="Cancelar" CssClass="btn btn-sm btn-danger" CommandName="Cancel" Text='<i class="fa fa-ban"></i>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="thead-dark alt" />
                                    <RowStyle HorizontalAlign="Center"></RowStyle>
                            </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-1">
                        <img src="img/loading.gif" class="loading-gif" id="loading-gif-cuotas">
                    </div>
                </div>
            </div>
        </div>
        <div id="divBotonesAccion" runat="server" class="card-footer">
            <div class="row">
                <div class="col-sm-12 text-right">
                    <button type="button" id="btnCancelar" runat="server" class="btn btn-outline-secondary">Cancelar</button>
                    <asp:UpdatePanel ID="udpBtnSubmit" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:Button ID="btnSubmit" runat="server" UseSubmitBehavior="false" CssClass="btn btn-primary" Text="Guardar" OnClientClick="if(!ValidarForm()) return false;" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="mdlMensajesServidor" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <asp:UpdatePanel ID="udpMensajeServidorParametros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div id="divMdlMenServParametros" runat="server" data-mostrar="false"></div>
                        </ContentTemplate>  
                    </asp:UpdatePanel> 
                    <div class="modal-header">
                        <asp:UpdatePanel ID="udpMensajeServidorHeader" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <span id="spnMensajeServidorTitulo" runat="server" class="modal-title">Respuesta del Servidor</span>
                            </ContentTemplate>  
                        </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="udpMensajeServidorBody" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div id="divRespuestaPostback" runat="server" class="alert" data-ispostback="false" data-rpta="" data-msg="" data-control=""></div>
                                <asp:Label ID="lblErrorMessage" runat="server" CssClass="d-none" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <asp:UpdatePanel ID="udpMensajeServidorFooter" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <button type="button" id="btnMensajeCerrar" runat="server" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
    <script src="../../assets/jquery-validation/jquery.validate.min.js"></script>
    <script src="../../assets/jquery-validation/localization/messages_es.min.js"></script>
    <script src="../../assets/iframeresizer/iframeResizer.contentWindow.min.js"></script>
    <!-- Scripts propios -->
    <script src="js/funciones.js"></script>
    <script src="js/detalleConvenio.js"></script>
    <script type="text/javascript">
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id

            switch (controlId) {
                case 'btnGenerarCuotas':
                    AtenuarBoton(controlId, false);
                    AlternarLoadingGif('cuotas', false);
                    break;
            }

            if (controlId.indexOf('btnEditar') > -1 || controlId.indexOf('btnActualizar') > -1 || controlId.indexOf('btnCancelar') > -1) {
                AtenuarBoton(controlId, false);
                AlternarLoadingGif('cuotas', false);
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function(sender, args) {
            var error = args.get_error();
            if (error) {
                // Manejar el error
                switch(controlId) {
                    case 'btnSubmit':
                        ErrorPostback();
                        break;
                    default:
                         AlternarLoadingGif('cuotas', true);
                }
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (sender, args) {
            var updatedPanels = args.get_panelsUpdated();  

            var udpFormUpdated = false
            for (var i = 0; i < updatedPanels.length; i++) {
                var udpPanelId = updatedPanels[i].id;
                switch (udpPanelId) {
                    case 'udpCuotas':
                        InitControlesCuotas();
                        break;
                }
            }
        });

        Sys.Application.add_load(function() {
            var elem = document.getElementById(controlId);

            switch (controlId) {
                case 'btnGenerarCuotas':
                    AtenuarBoton(controlId, true);
                    AlternarLoadingGif('cuotas', true);
                    break;
                case 'btnSubmit':
                    SubmitPostBack();
                    break;
            }

            if (controlId.indexOf('btnEditar') > -1 || controlId.indexOf('btnActualizar') > -1 || controlId.indexOf('btnCancelar') > -1) {
                AtenuarBoton(controlId, true);
                AlternarLoadingGif('cuotas', true);
            }

            if(controlId.indexOf('btnActualizar') > -1) {
                RevisarRespuestaServidor();
            }
        });
    </script>
</body>
</html>