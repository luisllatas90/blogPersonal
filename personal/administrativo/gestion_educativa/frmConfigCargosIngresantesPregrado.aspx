<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConfigCargosIngresantesPregrado.aspx.vb" Inherits="administrativo_gestion_educativa_frmConfigCargosIngresantesPregrado" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Cargos Ingresantes Pregrado ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../../assets/toastr-2.1.4-7/toastr.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/configCargosIngresantesPregrado.css?x=1">
</head>

<body>
    <form id="frm" runat="server">
        <asp:ScriptManager ID="scr" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpParams" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:HiddenField ID="hddCod" Value="0" runat="server" />
                <asp:HiddenField ID="hddJsonConfig" Value="0" runat="server" />
                <asp:HiddenField ID="hddTipoVista" Value="L" runat="server" />
                <asp:HiddenField ID="hddParamsToastr" Value="" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="container-fluid">
            <div class="row">
                <div class="col-xl-48">
                    <h1 class="title">Configuración General</h1>
                    <hr>
                </div>
            </div>
            <div class="row form-group">
                <div class="col-xl-12 d-flex">
                    <label for="rbtInscritoCicloActual" class="form-control-sm">¿Mismos Importes?:</label>
                    <div class="fill">
                        <div class="custom-control custom-checkbox custom-control-inline">
                            <input type="checkbox" id="chkMismoImporte" name="chkMismoImporte" class="custom-control-input" value="1">
                            <label class="custom-control-label form-control-sm" for="chkMismoImporte">Si</label>
                        </div>
                        <input type="text" name="txtImporteGeneral" id="txtImporteGeneral" class="form-control form-control-sm">
                    </div>
                </div>
            </div>
            <hr>
            <div class="row form-group row-agregar">
                <div class="col-xl-40 d-flex">
                    <h3 class="title">Agregar cargos</h3>
                </div>
                <div class="col-xl-8 text-right">
                    <button type="button" class="btn btn-sm btn-success" id="btnAgregarFila">
                        <i class="fas fa-plus-square"></i>
                    </button>
                </div>
            </div>
            <div class="row form-group d-none" id="row-template">
                <div class="col-xl-11 d-flex">
                    <label for="dtpFechaPeriodo" class="form-control-sm">Fecha Periodo:</label>
                    <div class="fill">
                        <input type="text" name="dtpFechaPeriodo" id="dtpFechaPeriodo" class="form-control form-control-sm">
                    </div>
                </div>
                <div class="col-xl-11 d-flex">
                    <label for="txtImporte" class="form-control-sm">Importe:</label>
                    <div class="fill">
                        <input type="text" name="txtImporte" id="txtImporte" class="form-control form-control-sm">
                    </div>
                </div>
                <div class="col-xl-12 d-flex">
                    <label for="dtpFechaVencimiento" class="form-control-sm">Fecha de Vencimiento:</label>
                    <div class="fill">
                        <input type="text" name="dtpFechaVencimiento" id="dtpFechaVencimiento" class="form-control form-control-sm">
                    </div>
                </div>
                <div class="col-xl-14 d-flex">
                    <label for="cmbCategoriaEscuela" class="form-control-sm">Grupo de Escuelas:</label>
                    <div class="fill">
                        <select name="cmbCategoriaEscuela" id="cmbCategoriaEscuela" class="form-control form-control-sm" multiple>
                            <option value="E">EDUCACIÓN</option>
                            <option value="O">ODONTOLOGÍA</option>
                            <option value="M">MEDICINA</option>
                            <option value="T">OTRAS</option>
                        </select>
                        <button type="button" class="btn btn-sm btn-danger btn-quitar">
                            <i class="fas fa-minus-square"></i>
                        </button>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xl-48" id="form-dinamico">

                </div>
            </div>
        </div>
        <div id="mdlMensajeServidor" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <asp:UpdatePanel ID="udpMenServParametros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div id="divMenServParametros" runat="server" data-mostrar="false"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="modal-header">
                        <asp:UpdatePanel ID="udpMenServHeader" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <span id="spnMenServTitulo" runat="server" class="modal-title">Respuesta del
                                    Servidor</span>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="udpMenServBody" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div id="divMenServMensaje" class="alert alert-warning" runat="server"></div>
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
    <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.min.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>
    <script src="../../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/i18n/defaults-es_ES.min.js"></script>
    <script src="../../assets/iframeresizer/iframeResizer.contentWindow.min.js"></script>
    <script src="../../assets/toastr-2.1.4-7/toastr.min.js"></script>
    <!-- Scripts propios -->
    <script src="js/funciones.js"></script>
    <script src="js/configCargosIngresantesPregrado.js?x=1"></script>

    <script type="text/javascript">
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id

            alternarLoadingGif('global', false);

            switch (controlId) {
                case 'btnListar':
                case 'btnNuevo':
                case 'btnMostrarAlumnos':
                    atenuarBoton(controlId, false);
                    break;
                case 'btnCancelar':
                case 'btnGuardar':
                    atenuarBoton('btnCancelar', false);
                    atenuarBoton('btnGuardar', false);
                    break;
            }

            if (controlId.indexOf('btnEditar') > -1) {
                atenuarBoton(controlId, false);
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
                    case 'udpMantenimiento':
                        initFormMantenimiento();
                        break;
                    case 'udpTipoConfiguracion':
                        initSelectPicker('cmbTipoConfiguracion');
                        break;
                    case 'udpConfigManual':
                        initIframe();
                        initSelectPicker('cmbTipoDeuda');
                        initSelectPicker('cmbServicioConcepto');
                        initSelectPicker('cmbCentroCosto');
                        initFechaVencimiento();
                        break;
                    case 'udpTipoDeuda':
                        initSelectPicker('cmbTipoDeuda');
                        break;
                    case 'udpProgramacion':
                        initFechaHoraInicioProg();
                        initFechaHoraFinProg();
                        initSelectPicker('cmbEjecutarCada');
                        break;
                    case 'udpServicioCentroCosto':
                    case 'udpServicioConcepto':
                        initSelectPicker('cmbServicioConcepto', {
                            size: 8,
                        });
                    case 'udpServicioCentroCosto':
                    case 'udpCentroCosto':
                        initSelectPicker('cmbCentroCosto', {
                            size: 8,
                            dropdownAlignRight: true,
                        });
                        break;
                }
            }
        });

        Sys.Application.add_load(function () {
            var elem = document.getElementById(controlId);

            alternarLoadingGif('global', true);

            switch (controlId) {
                case 'btnMostrarAlumnos':
                    atenuarBoton(controlId, true);
                    verificarMostrarAlumnos();
                    break;
                case 'btnListar':
                case 'btnNuevo':
                    atenuarBoton(controlId, true);
                    break;
                case 'btnCancelar':
                case 'btnGuardar':
                    atenuarBoton('btnCancelar', true);
                    atenuarBoton('btnGuardar', true);
                    break;
            }

            if (controlId.indexOf('btnEditar') > -1) {
                atenuarBoton(controlId, true);
            }

            if (controlId.indexOf('btnEjecutar') > -1) {
                confirmarEjecutado();
            }

            verificarCambiosAjax();
        });    
    </script>
</body>

</html>