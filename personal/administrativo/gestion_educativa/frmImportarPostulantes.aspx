<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmImportarPostulantes.aspx.vb" Inherits="administrativo_gestion_educativa_frmImportarPostulantes" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>.:: Importar Postulantes ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/smart-wizard/css/smart_wizard.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.css">
    <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/importarPostulantes.css">
</head>
<body>
    <form id="frmPostulantes" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <br>
            <div class="card main">
                <img src="img/loading.gif" id="loading-gif">
                <div class="card-body">
                    <h6>Importar Postulantes</h6>
                    <hr>
                </div>
                <asp:UpdatePanel ID="udpCombos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="form-group row">
                            <label for="cmbTipoEstudio" class="col-sm-2 col-form-label form-control-sm">Tipo de Estudio:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="cmbTipoEstudio" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm selectpicker" data-none-selected-text="No hay elementos">
                                    <asp:ListItem Selected="True" Value="-1">-- Seleccione --</asp:ListItem>
                                    <asp:ListItem Value="1">ESCUELA PRE</asp:ListItem>
                                    <asp:ListItem Value="5">POSTGRADO</asp:ListItem>
                                    <asp:ListItem Value="8">SEGUNDA ESPECIALIDAD</asp:ListItem>
                                    <asp:ListItem Value="10">GO USAT</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="cmbCentroCosto" class="col-sm-2 col-form-label form-control-sm">Centro de Costo:</label>
                            <div class="col-sm-7">
                                <asp:UpdatePanel ID="udpCentroCosto" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>   
                                        <asp:DropDownList ID="cmbCentroCosto" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm selectpicker" data-none-selected-text="No hay elementos">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="cmbCarreraProfesional" class="col-sm-2 col-form-label form-control-sm">Carrera Profesional:</label>
                            <div class="col-sm-7">
                                <asp:UpdatePanel ID="udpCarreraProfesional" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>   
                                        <asp:DropDownList ID="cmbCarreraProfesional" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm selectpicker" data-none-selected-text="No hay elementos">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="cmbCicloAcademico" class="col-sm-2 col-form-label form-control-sm">Proceso de Admisión:</label>
                            <div class="col-sm-2">
                                <asp:UpdatePanel ID="udpCicloAcademico" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cmbCicloAcademico" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm selectpicker" data-none-selected-text="No hay elementos">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <label for="cmbModalidadIngreso" class="col-sm-2 col-form-label form-control-sm">Modalidad de Ingreso:</label>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="udpModalidadIngreso" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cmbModalidadIngreso" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm selectpicker" data-none-selected-text="No hay elementos">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="form-group row">
                    <label for="" class="col-sm-2 col-form-label form-control-sm">Archivo:</label>
                    <div class="col-sm-7">
                        <div class="custom-file">
                            <asp:FileUpload ID="fluNotas" runat="server" accept=".csv" />
                            <label class="custom-file-label" for="fluNotas">Seleccione un archivo</label>
                        </div>
                        <small class="text-muted"><em>Formatos permitidos: <strong>.csv</strong></em></small>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-sm-7 offset-sm-2">
                        <asp:Button ID="btnEnviar" runat="server" CssClass="d-none" Text="Enviar" />
                        <asp:UpdatePanel ID="udpValidar" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <button type="button" id="btnValidar" runat="server" class="btn btn-sm btn-primary">
                                    <i class="fa fa-upload"></i>
                                    <span class="text">Enviar</span>
                                </button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlMensajeServidor" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span class="modal-title">Respuesta del Servidor</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="udpMensajeServidor" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div id="respuestaPostback" runat="server" class="alert" data-ispostback="false" data-rpta="" data-msg="" data-control=""></div>
                                <div id="divErrorMessage" runat="server" class="d-none"></div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
    <script src="../../assets/iframeresizer/iframeResizer.min.js"></script>
    <!-- Scripts propios -->
    <script src="js/funciones.js"></script>
    <script src="js/importarPostulantes.js?1"></script>
    <script type="text/javascript">
        var controlId = '';
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();

            controlId = elem.id
            switch (controlId) {
                case 'btnValidar':
                    Validando();
                    break;
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function(sender, args) {
            var error = args.get_error();
            if (error) {
                console.log(error);
                return false;
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (sender, args) {
            var updatedPanels = args.get_panelsUpdated();  

            var udpFormUpdated = false
            for (var i = 0; i < updatedPanels.length; i++) {
                var udpPanel = updatedPanels[i];
                switch (udpPanel.id) {
                    case 'udpCentroCosto':
                        InitComboCentroCosto();
                        break;

                    case 'udpCarreraProfesional':
                        InitComboCarreraProfesional();
                        break;

                    case 'udpModalidadIngreso':
                        InitComboModalidadIngreso();
                        break;
                }
                if (udpPanel.id == 'udpForm') {
                    cargarCaptcha();
                }
            }
        });

        Sys.Application.add_load(function() {
            var elem = document.getElementById(controlId);
            console.log(controlId);

            switch (controlId) {
                case 'btnValidar':
                    CallbackValidacion();
                    break;
            }
        });
    </script>
</body>
</html>