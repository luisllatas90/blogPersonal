<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRequisitosAdmision.aspx.vb"
    Inherits="administrativo_pec_test_frmRequisitosAdmision" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Requisitos de Admisión</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/requisitosAdmision.css?3">
</head>
<body>
    <form id="frmRequisitosAdmision" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpRequisitos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div class="card">
                    <div class="card-header">Seleccionar los requisitos que han sido entregados:</div>
                    <div class="card-body">
                        <div class="list-group-flush" id="listaRequisitos" runat="server"></div>
                        <hr>
                        <span class="badge badge-light">* Lista de documentos requeridos para continuar con el proceso de matrícula</span>
                    </div>
                    <div class="card-footer" id="botonesFormulario" runat="server">
                        <button type="button" id="btnGuardar" runat="server" class="btn btn-sm btn-primary">Guardar</button>
                        <button type="button" class="btn btn-sm btn-light">Cancelar</button>
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
        <script src="js/requisitosAdmision.js"></script>
        <script type="text/javascript">
            var controlId = ''
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
                var elem = args.get_postBackElement();

                controlId = elem.id
                switch (controlId) {
                }
            });

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function(sender, args) {
                var error = args.get_error();
                if (error) {
                    // Manejar el error
                }

                switch (controlId){
                    case 'btnGuardar':
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
