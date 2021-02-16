<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frm_InscripcionInteresadoPEC.aspx.vb" Inherits="frm_InscripcionInteresadoPEC" %>


    <!DOCTYPE html>
    <html lang="en">

    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta http-equiv="X-UA-Compatible" content="ie=EmulateIE10">
        <title>Formulario de Inscripción</title>

        <%-- Estilos Externos --%>
        <link rel="stylesheet" href="../assets/css/bootstrap-4.1/bootstrap.min.css">
        <link rel="stylesheet" href="../assets/css/bootstrap-select-1.13.1/bootstrap-select.min.css">
        <link rel="stylesheet" href="../assets/css/bootstrap-datepicker/bootstrap-datepicker.min.css">
        <link href="https://fonts.googleapis.com/css?family=Dosis:300,400,500,700" rel="stylesheet">

        <%-- Estilos Propios --%>
        <link rel="stylesheet" href="css/style.css">
        <link rel="stylesheet" href="css/inscripcionInteresado.css">

        <script src='https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit'></script>
    </head>

    <body>
        <form id="frmInscripcionInteresado" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <input type="hidden" name="tipo" id="hddTipo" runat="server" />
            <input type="hidden" name="tokenCco" id="hddTokenCco" runat="server" />
            <input type="hidden" name="codigoEve" id="hddCodigoEve" runat="server" />
            <input type="hidden" name="codigoCac" id="hddCodigoCac" runat="server" />
            <input type="hidden" name="codigoMin" id="hddCodigoMin" runat="server" />

            <div class="container-fluid">
                <asp:UpdatePanel ID="udpDatosPersonales" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <h1 class="titulo">Formulario de Inscripción</h1>
                        <p>Los campos marcados con <span class="rojo">*</span> son obligatorios</p>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group row">
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control form-control-sm" placeholder="DNI(*)" />
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtApellidoPaterno" runat="server" CssClass="form-control form-control-sm" placeholder="Apellido Paterno(*)" />
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtApellidoMaterno" runat="server" CssClass="form-control form-control-sm" placeholder="Apellido Materno(*)" />
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control form-control-sm" placeholder="Nombres(*)" />
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtNumCelular" runat="server" CssClass="form-control form-control-sm" placeholder="Celular(*)" />
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtNumFijo" runat="server" CssClass="form-control form-control-sm" placeholder="Teléfono" />
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-sm" placeholder="Email(*)"/>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="cmbDepartamento" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" />
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:UpdatePanel ID="udpProvincia" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cmbProvincia" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:UpdatePanel ID="udpDistrito" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cmbDistrito" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-12">
                                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control form-control-sm" placeholder="Direccion(*)" />
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="dtpFecNacimiento" runat="server" CssClass="form-control form-control-sm" placeholder="F. Nacimiento(*)" />
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="cmbSexo" runat="server" CssClass="form-control form-control-sm" >
                                            <asp:ListItem Value="-1">Sexo(*)</asp:ListItem>
                                            <asp:ListItem Value="M">MASCULINO</asp:ListItem>
                                            <asp:ListItem Value="F">FEMENINO</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="cmbEstadoCivil" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                            <asp:ListItem Value="SOLTERO">SOLTERO</asp:ListItem>
                                            <asp:ListItem Value="CASADO">CASADO</asp:ListItem>
                                            <asp:ListItem Value="VIUDO">VIUDO</asp:ListItem>
                                            <asp:ListItem Value="DIVORCIADO">DIVORCIADO</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="E">Estado Civil</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
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
                                <%--<div class="form-group row">
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="cmbDepartamentoInstEduc" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"/>
                                    </div>
                                    <div class="col-sm-8 dropup">
                                        <asp:UpdatePanel ID="udpInstitucionEducativa" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cmbInstitucionEducativa" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>--%>
                                <%--<div class="form-group row">
                                    <div class="col-sm-12">
                                        <asp:UpdatePanel ID="udpCostosTrigger" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cmbCarreraProfesional" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>--%>
                                <%--<asp:UpdatePanel ID="udpCostos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div class="form-group row" id="rowCostos" runat="server" data-oculto="true">
                                            <div class="col-sm-12">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h5><span class="badge badge-light">Costos Estimados</span></h5>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="row">
                                                            <label class="col-sm-2 offset-sm-1 col-form-label form-control-sm">Menualidad:</label>
                                                            <div class="col-sm-2"><span id="spnCostoMes" runat="server"/></div>
                                                            <label class="col-sm-2 offset-sm-2 col-form-label form-control-sm">Total Ciclo:</label>
                                                            <div class="col-sm-2"><span id="spnCostoCiclo" runat="server"/></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                <div class="form-group row">
                                    <div class="col-sm-5">
                                        <div id="validacionCaptcha"></div>
                                    </div>
                                    <div class="col-sm-7">
                                        <div class="contenedor-terminos-condiciones">
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
                                                <button id="btnFakeInformarme" runat="server" class="btn btn-sm btn-secondary">Solicitar Información</button>
                                                <asp:Button ID="btnInformarme" runat="server" UseSubmitBehavior="false" CssClass="btn btn-sm btn-secondary d-none" Text="Solicitar Información" />    
                                                <button id="btnFakeInscribirme" runat="server" class="btn btn-sm btn-primary">Inscribirme</button>
                                                <asp:Button ID="btnInscribirme" runat="server" UseSubmitBehavior="false" CssClass="btn btn-sm btn-primary d-none" Text="Inscribirme" />
                                                <div id="errorMensaje" runat="server" class="alert alert-danger d-none"></div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>                 
                <!-- Modales -->
                <div class="modal fade" id="mdlRespuestaServer" tabindex="-1" role="dialog" aria-labelledby="tituloMensajeServer" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="tituloMensajeServer">Respuesta del Servidor</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="udpMensajeServer" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div id="mensajeServer" runat="server" class="alert"></div>
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
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                            <div class="modal-body">
                                <div id="mensajeGenerico"></div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" id="btnModalAceptar" class="btn btn-sm btn-primary" data-dismiss="modal">Aceptar</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="terminosCondiciones" class="d-none">
                    <div class="terminos-condiciones">
                        <h1>Términos y Condiciones</h1>
                        <p>
                            De conformidad con la Ley N° 29733, Ley de Protección de Datos Personales, autorizo a la USAT a utilizar los datos personales proporcionados o que proporcione a futuro, para la gestión administrativa y comercial que realice. 
                        </p>
                        <p>
                            Asimismo, de conformidad con las Leyes N° 28493 y N° 29571 brindo mi consentimiento para que la USAT me envíe información, 
                            publicidad, encuestas y estadísticas de sus servicios educativos; teniendo pleno conocimiento que puedo acceder, rectificar, 
                            oponerme y cancelar mis datos personales, así como revocar mi consentimiento enviando un correo a informacion@usat.edu.pe. 
                        </p>
                    </div>
                </div>
                <div class="modal fade" id="mdlConfirmacion" tabindex="-1" role="dialog" aria-labelledby="tituloConfirmacion" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="tituloConfirmacion">Mensaje de Confirmación</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div id="mensajeConfirmacion" class="alert alert-warning">Mensaje de alerta!</div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" id="btnModalConfirmCancelar" class="btn btn-sm btn-secondary" data-dismiss="modal">Cancelar</button>
                                <button type="button" id="btnModalConfirmAceptar" class="btn btn-sm btn-primary">Aceptar</button>
                            </div>
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
        <!-- Scripts propios -->
        <script src="js/inscripcionInteresado.js?2"></script>
        <script type="text/javascript">
            Sys.Application.add_load(function () {
                InicializarControles()
            });
        </script>
    </body>

    </html>