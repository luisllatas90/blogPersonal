<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frm_InscripcionEventoVirtual.aspx.vb" Inherits="Admision_frm_InscripcionEventoVirtual" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Inscripciones a Eventos Virtuales ::.</title>

    <!-- Estilos Externos -->
    <link rel="stylesheet" href="../assets/css/bootstrap-4.1/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/css/bootstrap-select-1.13.1/bootstrap-select.min.css">
    <link rel="stylesheet" href="../assets/css/toastr/toastr.min.css">
    <!-- <link rel="stylesheet" href="../assets/css/bootstrap-datepicker/bootstrap-datepicker.min.css"> -->
    <link href="https://fonts.googleapis.com/css?family=Dosis:300,400,500,700" rel="stylesheet">

    <!-- Estilos Propios -->
    <link rel="stylesheet" href="css/inscripcionEventoVirtual.css?x=1">
</head>

<body>
    <form id="frmEventoVirtual" runat="server">
        <asp:HiddenField ID="hddEvi" Value="" runat="server" />
        <asp:HiddenField ID="hddNombre" Value="" runat="server" />
        <asp:HiddenField ID="hddUrl" Value="" runat="server" />

        <asp:ScriptManager ID="scmEventoVirtual" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpParams" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:HiddenField ID="hddParamsToastr" Value="" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="udpForm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div class="container">
                    <br>
                    <h4 id="hTitulo" runat="server"></h4>
                    <h6>Regístrate para participar de esta charla online</h6>
                    <hr>
                    <div class="form-row">
                        <div class="form-group col-sm-8">
                            <label for="txtNombreCompleto">Nombres y Apellidos</label>
                            <input type="text" class="form-control form-control-sm" name="txtNombreCompleto" id="txtNombreCompleto" runat="server"
                                placeholder="Nombres y Apellidos">
                        </div>
                        <div class="form-group col-sm-4">
                            <label for="txtDocIdentidad">DNI</label>
                            <input type="text" class="form-control form-control-sm" name="txtDocIdentidad" id="txtDocIdentidad" runat="server" placeholder="DNI">
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-sm-6">
                            <label for="txtEmail">Correo Electrónico</label>
                            <input type="text" class="form-control form-control-sm" name="txtEmail" id="txtEmail" runat="server" placeholder="Correo Electrónico">
                        </div>
                        <div class="form-group col-sm-6">
                            <label for="txtCelular">Celular</label>
                            <input type="text" class="form-control form-control-sm" name="txtCelular" id="txtCelular" runat="server" placeholder="Celular">
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-sm-6">
                            <label for="cmbTipoParticipante">Tipo de Participante</label>
                            <select class="form-control form-control-sm selectpicker" id="cmbTipoParticipante" runat="server">
                                <option selected>-- Seleccione --</option>
                            </select>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-sm-6">
                            <label for="">¿Deseas obtener constancia de participación?</label>
                            <br>
                            <div class="custom-control custom-radio custom-control-inline">
                                <input class="custom-control-input" type="radio" name="rbtConstancia" id="rbtConstanciaSi" value="1" runat="server">
                                <label class="custom-control-label" for="rbtConstanciaSi">Si</label>
                            </div>
                            <div class="custom-control custom-radio custom-control-inline">
                                <input class="custom-control-input" type="radio" name="rbtConstancia" id="rbtConstanciaNo" value="0" runat="server">
                                <label class="custom-control-label" for="rbtConstanciaNo">No</label>
                            </div>
                        </div>
                        <div class="form-group col-sm-6">
                            <label for="">¿Actualmente estas trabajando?</label>
                            <br>
                            <div class="custom-control custom-radio custom-control-inline">
                                <input class="custom-control-input" type="radio" name="rbtTrabajando" id="rbtTrabajandoSi" value="1" runat="server">
                                <label class="custom-control-label" for="rbtTrabajandoSi">Si</label>
                            </div>
                            <div class="custom-control custom-radio custom-control-inline">
                                <input class="custom-control-input" type="radio" name="rbtTrabajando" id="rbtTrabajandoNo" value="0" runat="server">
                                <label class="custom-control-label" for="rbtTrabajandoNo">No</label>
                            </div>
                        </div>
                    </div>
                    <div class="collapse" id="datosLaborales">
                        <div class="form-row">
                            <div class="form-group col-sm-8">
                                <label for="txtEmpresa">Institución/Empresa en donde laboras</label>
                                <input type="text" class="form-control form-control-sm" name="txtEmpresa" id="txtEmpresa" runat="server" placeholder="Empresa">
                            </div>
                            <div class="form-group col-sm-4">
                                <label for="txtCargo">Cargo</label>
                                <input type="text" class="form-control form-control-sm" name="txtCargo" id="txtCargo" runat="server" placeholder="Cargo">
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-sm-6">
                                <label for="">Si eres estudiante o egresado USAT, ¿Por qué medio ingresaste a laborar?</label>
                                <br>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input class="custom-control-input" type="radio" name="rbtOfertaLaboral" id="rbtOfertaLaboralAlumni" value="A" runat="server">
                                    <label class="custom-control-label" for="rbtOfertaLaboralAlumni">ALUMNI</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input class="custom-control-input" type="radio" name="rbtOfertaLaboral" id="rbtOfertaLaboralExterna" value="E" runat="server">
                                    <label class="custom-control-label" for="rbtOfertaLaboralExterna">EXTERNA</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr>
                    <div class="form-row">
                        <div class="form-group col-sm-7">
                            <label for="">Lee los términos y condiciones:
                                <a href="http://www.usat.edu.pe/terminos-y-condiciones/" target="_blank">http://www.usat.edu.pe/terminos-y-condiciones/</a>
                            </label>
                            <br>
                            <div class="custom-control custom-checkbox custom-control-inline">
                                <input class="custom-control-input" type="checkbox" name="chkTerminosCondiciones" id="chkTerminosCondiciones" runat="server">
                                <label class="custom-control-label" for="chkTerminosCondiciones">Acepto términos y condiciones</label>
                            </div>
                        </div>
                        <div class="form-group col-sm-5">
                            <div id="validacionCaptcha"></div>
                        </div>
                    </div>
                    <hr>
                    <div class="form-row">
                        <div class="form-group col-sm-6">
                            <asp:UpdatePanel ID="udpControls" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <button type="button" class="btn btn-primary" id="btnFakeEnviar" runat="server" disabled>Enviar Datos</button>
                                    <button type="button" class="btn btn-warning d-none" id="btnEnviar" runat="server">Enviar Datos</button>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="form-group col-sm-6 text-right">
                            <img src="img/loading.gif" id="loadingGif">
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
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
                                <span id="spnMenServTitulo" runat="server" class="modal-title">Respuesta del Servidor</span>
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
    <script src="../assets/js/jquery-3.3.1/jquery-3.3.1.js"></script>
    <script src="../assets/js/bootstrap-4.1/bootstrap.bundle.min.js"></script>
    <script src="../assets/js/bootstrap-select-1.13.1/bootstrap-select.min.js"></script>
    <script src="../assets/js/bootstrap-select-1.13.1/i18n/defaults-es_ES.min.js"></script>
    <script src="../assets/js/bootstrap-datepicker/bootstrap-datepicker.min.js"></script>
    <script src="../assets/js/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="../assets/js/jquery-validation/jquery.validate.js"></script>
    <script src="../assets/js/jquery-validation/localization/messages_es.min.js"></script>
    <script src="../assets/js/iframeresizer/iframeResizer.contentWindow.min.js"></script>
    <script src="../assets/js/fileDownload/jquery.fileDownload.js"></script>
    <script src="../assets/js/toastr/toastr.min.js"></script>
    <!-- Scripts propios -->
    <script src="js/funciones.js?x=1"></script>
    <script src="js/inscripcionEventoVirtual.js?x=1"></script>

    <script src='https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit'></script>

    <script type="text/javascript">
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id

            switch (controlId) {
                case 'btnEnviar':
                    enableDisableControl('btnFakeEnviar', true);
                    break;
            }

            alternarLoadingGif('global', false);
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
                    case 'udpForm':
                        initCmbTipoParticipante();
                        cargarCaptcha();
                        break;
                }
            }
        });

        Sys.Application.add_load(function () {
            var elem = document.getElementById(controlId);

            switch (controlId) {
            }

            verificarCambiosAjax();
            alternarLoadingGif('global', true);
        });    
    </script>
</body>

</html>