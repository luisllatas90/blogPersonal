<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGenerarConvenio.aspx.vb" Inherits="administrativo_gestion_educativa_frmGenerarConvenio" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Actualizar Datos Inscrito</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/smart-wizard/css/smart_wizard.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.css">
    <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css">
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="css/style.css">
</head>
<body>
    <form id="frmGenerarConvenio" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpDeudas" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div class="card">
                    <div class="card-body">
                        <span class="badge badge-danger"><i class="fa fa-exclamation-triangle"></i>Ya se ha generado el cronograma de pagos</span>
                        <asp:GridView ID="grwDeudas" runat="server" AutoGenerateColumns="false" CssClass="table table-sm"
                            GridLines="None">
                            <Columns>
                                <asp:BoundField DataField="descripcion_Sco" HeaderText="Servicio" />
                                <asp:BoundField DataField="montoTotal_Deu" HeaderText="Deuda" />
                                <asp:BoundField DataField="pagado_Deu" HeaderText="Pago" />
                                <asp:BoundField DataField="saldo_Deu" HeaderText="Saldo" />
                                <asp:BoundField DataField="fechaVencimiento_Deu" HeaderText="Fec. Venc." DataFormatString="{0:dd/MM/yyyy}"  />
                                <asp:BoundField DataField="estado_Deu" HeaderText="Estado" />
                            </Columns>
                            <HeaderStyle CssClass="thead-dark alt" />
                        </asp:GridView>
                    </div>
                    <div class="card-footer text-right">
                        <button type="button" class="btn btn-sm btn-default btn-cancelar">Cerrar</button>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="udpGenerarConvenio" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div class="card">
                    <div class="card-header">
                        Lista de cuotas por generar
                    </div>
                    <div class="card-body">
                        <div id="spnSinCuotas" class="badge badge-warning" runat="server">*No se ha encontrado configuración del convenio.</div>
                        <asp:GridView ID="grwConvenioParticipante" runat="server" AutoGenerateColumns="false" CssClass="table table-sm" GridLines="None">
                            <Columns>
                                <asp:BoundField HeaderText="Nro" />
                                <asp:BoundField DataField="descripcion_sco" HeaderText="Servicio" ReadOnly="True" />
                                <asp:BoundField DataField="monto_cpard" HeaderText="Monto" ReadOnly="True" />
                                <asp:BoundField DataField="fechavenc_cpard" HeaderText="Fecha Vence" ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="card-footer text-right">
                        <button type="button" class="btn btn-sm btn-default btn-cancelar">Cerrar</button>
                        <button type="button" id="btnGenerarConvenio" runat="server" class="btn btn-sm btn-primary">Generar Convenio</button>
                    </div>
                </div>
                <div id="respuestaPostback" class="d-none" runat="server" data-ispostback="false" data-rpta="" data-msg=""></div>
                <div id="mensajeError" runat="server" class="d-none"></div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="mdlMensajes" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span class="modal-title">Respuesta del Servidor</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div id="mensajePostBack" class="alert"></div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- Scripts externos -->
        <script src="../../assets/jquery/jquery-3.3.1.js"></script>
        <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.min.js"></script>
        <script src="../../assets/iframeresizer/iframeResizer.contentWindow.min.js"></script>
        <!-- Scripts propios -->
        <script src="js/funciones.js"></script>
        <script src="js/generarConvenio.js?4"></script>
        <script type="text/javascript">
            var controlId = ''
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
                var elem = args.get_postBackElement();

                controlId = elem.id
                switch (controlId) {
                    case 'btnGenerarConvenio':
                        AtenuarBoton(controlId, false);
                        break;
                }
            });

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function(sender, args) {
                var error = args.get_error();
                if (error) {
                    // Manejar el error
                }
            });

            Sys.Application.add_load(function() { 
                switch (controlId){
                    case 'btnGenerarConvenio':
                        AtenuarBoton(controlId, true);
                        SubmitPostBack();
                        break;
                    default:
                        break;
                }
            });
        </script>
    </form>
</body>
</html>
