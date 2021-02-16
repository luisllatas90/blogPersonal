<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frm_InscripcionInteresadoReducido.aspx.vb" Inherits="frm_InscripcionInteresadoReducido" validaterequest="true" %>


<!DOCTYPE html>
<html lang="en">

<head>
    <script>(function (w, d, s, l, i) {
            w[l] = w[l] || []; w[l].push({
                'gtm.start':
                    new Date().getTime(), event: 'gtm.js'
            }); var f = d.getElementsByTagName(s)[0],
                j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
                    'https://www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
        })(window, document, 'script', 'dataLayer', 'GTM-PXZB68Q');</script>

    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=EmulateIE10">
    <title>Formulario de Inscripción</title>

    <%-- Estilos Externos --%>
    <link rel="stylesheet" href="../assets/css/bootstrap-4.1/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/css/bootstrap-select-1.13.1/bootstrap-select.min.css">
    <!-- <link rel="stylesheet" href="../assets/css/bootstrap-datepicker/bootstrap-datepicker.min.css"> -->
    <link href="https://fonts.googleapis.com/css?family=Dosis:300,400,500,700" rel="stylesheet">

    <%-- Estilos Propios --%>
    <link rel="stylesheet" href="css/style.css?30">
    <link rel="stylesheet" href="css/inscripcionInteresadoReducido.css?20">

    <script src='https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit'></script>
</head>

<body>
    <noscript>
        <iframe src="https://www.googletagmanager.com/ns.html?id=GTM-PXZB68Q" height="0" width="0" style="display:none;visibility:hidden"></iframe>
    </noscript>

    <form id="frmInscripcionInteresadoReducido" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <input type="hidden" name="tokenCco" id="hddTokenCco" runat="server" />
        <input type="hidden" name="nombreEve" id="hddNombreEve" runat="server" />
        <input type="hidden" name="descripcionCac" id="hddDescripcionCac" runat="server" />
        <input type="hidden" name="codigoMin" id="hddCodigoMin" runat="server" />
        <input type="hidden" name="nivelesEstudio" id="hddNivelesEstudio" runat="server" />
        <input type="hidden" name="preferente" id="hddPreferente" runat="server" />
        <input type="hidden" name="codigosIed" id="hddCodigosIed" runat="server" />
        <input type="hidden" name="codigosCpf" id="hddCodigosCpf" runat="server" />
        <input type="hidden" name="datosApod" id="hddDatosApod" runat="server" />
        <input type="hidden" name="datosProf" id="hddDatosProf" runat="server" />
        <input type="hidden" name="consultas" id="hddConsultas" runat="server" />
        <input type="hidden" name="titulo" id="hddTitulo" runat="server" />
        <input type="hidden" name="textoBtnInformacion" id="hddTextoBtnInformacion" runat="server" />
        <input type="hidden" name="campoAdicional1" id="hddCampoAdicional1" runat="server" />
        <input type="hidden" name="campoAdicional2" id="hddCampoAdicional2" runat="server" />
        <input type="hidden" name="campoAdicional3" id="hddCampoAdicional3" runat="server" />
        <input type="hidden" name="campoAdicional4" id="hddCampoAdicional4" runat="server" />
        <input type="hidden" name="campoAdicional5" id="hddCampoAdicional5" runat="server" />
        <input type="hidden" name="campoAdicional6" id="hddCampoAdicional6" runat="server" />

        <asp:UpdatePanel ID="udpForm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div class="container-fluid">
                    <asp:UpdatePanel ID="udpDatosPersonales" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <h1 id="hTitulo" runat="server" class="titulo">Formulario de Inscripción</h1>
                            <p>Los campos marcados con <span class="rojo">*</span> son obligatorios</p>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div id="divDatosApoderado" runat="server">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <span class="separador">Datos de Padres o Apoderado</span>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtDNIApoderado" runat="server" CssClass="form-control form-control-sm" placeholder="DNI Apoder. (*)" />
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtNombresApoderado" runat="server" CssClass="form-control form-control-sm" placeholder="Nombres (*)" />
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtApePatApoderado" runat="server" CssClass="form-control form-control-sm" placeholder="Apellido Paterno (*)" />
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtApeMatApoderado" runat="server" CssClass="form-control form-control-sm" placeholder="Apellido Materno (*)" />
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtNumCelApoderado" runat="server" CssClass="form-control form-control-sm" placeholder="Celular (*)" />
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtEmailApoderado" runat="server" CssClass="form-control form-control-sm" placeholder="Email" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <span class="separador">Datos de Hijo(a)</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control form-control-sm" placeholder="DNI (*)" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control form-control-sm" placeholder="Nombres (*)" />
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtApellidoPaterno" runat="server" CssClass="form-control form-control-sm" placeholder="Apellido Paterno (*)" />
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtApellidoMaterno" runat="server" CssClass="form-control form-control-sm" placeholder="Apellido Materno (*)" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtNumCelular" runat="server" CssClass="form-control form-control-sm" placeholder="Celular (*)" />
                                        </div>
                                        <div class="col-sm-4 d-none">
                                            <asp:TextBox ID="txtNumFijo" runat="server" CssClass="form-control form-control-sm" placeholder="Teléfono Fijo" Visible="false" />
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtEmail" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" placeholder="Email (*)" />
                                            <asp:UpdatePanel ID="udpEmail" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:HiddenField ID="hddEmailCoincidente" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hddEmailVerificado" runat="server" Value="0" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="form-group row d-none">
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control form-control-sm" placeholder="Dirección de domicilio (*)"
                                                Visible="false" />
                                        </div>
                                    </div>
                                    <div class="form-group row d-none">
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cmbDepartamento" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" Visible="false" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:UpdatePanel ID="udpProvincia" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="cmbProvincia" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                                        Visible="false" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:UpdatePanel ID="udpDistrito" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="cmbDistrito" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" Visible="false" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-4 d-none">
                                            <asp:TextBox ID="dtpFecNacimiento" runat="server" CssClass="form-control form-control-sm" placeholder="Fecha Nac. (día/mes/año*)"
                                                Visible="false" />
                                        </div>
                                        <div class="col-sm-4 d-none">
                                            <asp:DropDownList ID="cmbSexo" runat="server" CssClass="form-control form-control-sm" Visible="false">
                                                <asp:ListItem Value="-1">Sexo(*)</asp:ListItem>
                                                <asp:ListItem Value="M">MASCULINO</asp:ListItem>
                                                <asp:ListItem Value="F">FEMENINO</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cmbAnioEstudio" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                                <asp:ListItem Selected="True" Value="-1">-- Grado de Estudios --</asp:ListItem>
                                                <asp:ListItem Value="T">3er Año</asp:ListItem>
                                                <asp:ListItem Value="C">4to Año</asp:ListItem>
                                                <asp:ListItem Value="Q">5to Año</asp:ListItem>
                                                <asp:ListItem Value="E">Ya terminé el colegio</asp:ListItem>
                                                <asp:ListItem Value="U">Soy estudiante universitario</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cmbDepartamentoInstEduc" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" />
                                        </div>
                                        <div class="col-sm-6 dropup">
                                            <asp:UpdatePanel ID="udpInstitucionEducativa" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="cmbInstitucionEducativa" runat="server" AutoPostBack="True"
                                                        CssClass="form-control form-control-sm selectpicker" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="form-group row d-none" id="divDatosProfesion" runat="server">
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtCentroLabores" runat="server" CssClass="form-control form-control-sm" placeholder="Centro de Labores" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtCargo" runat="server" CssClass="form-control form-control-sm" placeholder="Cargo Actual" />
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtRuc" runat="server" CssClass="form-control form-control-sm" placeholder="RUC" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="cmbCarreraProfesional" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" />
                                        </div>
                                    </div>
                                    <div class="form-group row" id="divConsultas" runat="server">
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtConsultas" runat="server" TextMode="multiline" Rows="3" CssClass="form-control form-control-sm"
                                                placeholder="¿Tienes alguna consulta?" />
                                        </div>
                                    </div>
                                    <div class="row captcha-y-terminos">
                                        <div class="col-sm-12">
                                            <span class="separador">Valida el captcha y acepta las condiciones</span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-5 captcha-y-terminos">
                                            <div id="validacionCaptcha"></div>
                                        </div>
                                        <div class="col-sm-7">
                                            <div class="contenedor-terminos-condiciones row">
                                                <div class="custom-control custom-checkbox">
                                                    <input type="checkbox" id="chkTerminosCondiciones" runat="server" class="custom-control-input ignore">
                                                    <label for="chkTerminosCondiciones" class="custom-control-label">
                                                        He leído y acepto los términos y condiciones -> <a href="#" id="lnkLeerTerminosCondiciones">Leer aquí*</a>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="udpBotonesSubmit" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div class="form-group row">
                                                <div class="col-sm-12 text-center">
                                                    <button id="btnFakeInformarme" runat="server" class="btn btn-sm btn-secondary"></button>
                                                    <asp:Button ID="btnInformarme" runat="server" UseSubmitBehavior="false" CssClass="btn btn-sm btn-secondary d-none"
                                                        Text="Solicitar Información" />
                                                    <div id="errorMensaje" runat="server" class="alert alert-danger d-none"></div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- Modales -->
        <div class="modal fade" id="mdlConfirmacion" tabindex="-1" role="dialog" aria-labelledby="tituloConfirmacion" aria-hidden="true">
            <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-body">
                        <div id="mensajeConfirmacion" runat="server">¿Está a punto de inscribirse, desea continuar?</div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" id="btnModalConfirmCancelar" class="btn btn-sm btn-secondary" data-dismiss="modal">Cancelar</button>
                        <button type="button" id="btnModalConfirmAceptar" class="btn btn-sm btn-primary">Aceptar</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="mdlRespuestaServer" tabindex="-1" role="dialog" aria-labelledby="tituloMensajeServer" aria-hidden="true">
            <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-body">
                        <asp:UpdatePanel ID="udpMensajeServer" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div id="mensajeServer" runat="server"></div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" id="btnModalAceptar" class="btn btn-sm btn-primary" data-dismiss="modal">Aceptar</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="mdlGenerico" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-body">
                        <div id="mensajeGenerico"></div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" id="btnModalAceptar" class="btn btn-sm btn-danger" data-dismiss="modal">Aceptar</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="terminosCondiciones" class="d-none">
            <div class="terminos-condiciones">
                <h1>Términos y Condiciones</h1>
                <p>
                    De conformidad con la Ley N° 29733, Ley de Protección de Datos Personales, autorizo a la USAT a utilizar los datos personales proporcionados o que proporcione a
                    futuro, para la gestión administrativa y comercial que realice.
                </p>
                <p>
                    Asimismo, de conformidad con las Leyes N° 28493 y N° 29571 brindo mi consentimiento para que la USAT me envíe información,
                    publicidad, encuestas y estadísticas de sus servicios educativos; teniendo pleno conocimiento que puedo acceder, rectificar,
                    oponerme y cancelar mis datos personales, así como revocar mi consentimiento enviando un correo a informacion@usat.edu.pe.
                </p>
                <p>
                    En el caso de postular por la modalidad de Traslados, graduados o titulados, bachillerato, deportista destacado o persona con discapacidad,
                    Beca Socioeconómica, escribir a informes.admision@usat.edu.pe o llamar al 606217 para consultar por los requisitos y precios.
                </p>
            </div>
        </div>
        <div id="consideracionesAdmision" class="d-none">
            <div class="consideraciones-admision">
                <h1>Consideraciones</h1>
                <ul>
                    <li>Recuerda que la carrera se abre con un número mínimo de 15 personas matriculadas.</li>
                    <li>No hay devolución de dinero por derecho de examen, en el caso que no asistas a rendir el examen en la fecha, hora y día programado.</li>
                    <li>El costo de crédito es de acuerdo al costo actual de la pensión del colegio de procedencia. En el caso de las carreras de Medicina
                        y Odontología no aplica categorización.</li>
                    <li>La devolución de documentos como certificado de estudios para los que no cierren matrícula en la fecha programada, deben solicitar
                        su devolución en un plazo máximo de 30 días, caso contrario la USAT no se responsabiliza por la entrega del mismo.</li>
                </ul>
            </div>
        </div>
    </form>
    <!-- Scripts externos -->
    <script src="../assets/js/jquery-3.3.1/jquery-3.3.1.js"></script>
    <script src="../assets/js/bootstrap-4.1/bootstrap.bundle.min.js"></script>
    <script src="../assets/js/bootstrap-select-1.13.1/bootstrap-select.min.js"></script>
    <script src="../assets/js/bootstrap-select-1.13.1/i18n/defaults-es_ES.min.js"></script>
    <!-- <script src="../assets/js/bootstrap-datepicker/bootstrap-datepicker.min.js"></script>
        <script src="../assets/js/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script> -->
    <script src="../assets/js/jquery-mask/jquery.mask.min.js"></script>
    <script src="../assets/js/jquery-validation/jquery.validate.min.js"></script>
    <script src="../assets/js/jquery-validation/localization/messages_es.min.js"></script>
    <script src="../assets/js/iframeresizer/iframeResizer.contentWindow.min.js"></script>
    <!-- Scripts propios -->
    <script src="js/inscripcionInteresadoReducido.js?10"></script>
    <script type="text/javascript">
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
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
                if (udpPanel.id == 'udpForm') {
                    cargarCaptcha();
                }
            }
        });

        Sys.Application.add_load(function () {
            InicializarControles();
        });
    </script>
</body>

</html>