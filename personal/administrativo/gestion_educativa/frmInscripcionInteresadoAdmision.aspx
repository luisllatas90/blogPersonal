<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmInscripcionInteresadoAdmision.aspx.vb" Inherits="administrativo_pec_test_frmInscripcionInteresadoAdmision" %>

    <!DOCTYPE html>
    <html lang="en">

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
        <link rel="stylesheet" href="css/inscripcionInteresadoAdmision.css?8">
    </head>

    <body>
        <form id="frmInscripcion" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="udpForm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div id="errorMensaje" class="alert alert-danger d-none" runat="server"></div>
                    <div class="row">
                        <div class="col-sm-12">
                            <h6><span id="lblCentroCosto" runat="server" class="badge badge-light"></span></h6>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header">DATOS PERSONALES</div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group row">
                                        <label for="DNI" class="col-sm-4 col-form-label form-control-sm">DNI:</label>
                                        <div class="col-sm-8">
                                            <div class="row no-gutters">
                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="DNI (*)" />
                                                </div>
                                                <div class="col-sm-7">
                                                    <a href="#" id="lnkObtenerDatos" runat="server" class="btn"><i class="fa fa-search"></i>Buscar por DNI</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="txtApellidoPaterno" class="col-sm-4 col-form-label form-control-sm">Apellido Paterno:</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtApellidoPaterno" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="Apellido Paterno (*)" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="txtApellidoMaterno" class="col-sm-4 col-form-label form-control-sm">Apellido Materno:</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtApellidoMaterno" runat="server" CssClass="form-control form-control-sm" placeholder="Apellido Materno (*)" />
                                            <a href="#" id="lnkObtenerDatosPorApellidos" runat="server" visible="false" class="btn"><i class="fa fa-search"></i>Buscar por Apellidos</a>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="txtNombres" class="col-sm-4 col-form-label form-control-sm">Nombres:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="Nombres (*)" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="dtpFecNacimiento" class="col-sm-4 col-form-label form-control-sm">Fec. Nacimiento:</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="dtpFecNacimiento" runat="server" CssClass="form-control form-control-sm" placeholder="Fec. Nacimiento (*)" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="cmbSexo" class="col-sm-4 col-form-label form-control-sm">Sexo:</label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="cmbSexo" runat="server" CssClass="form-control form-control-sm">
                                                <asp:ListItem Value="-1">-- Seleccione --</asp:ListItem>
                                                <asp:ListItem Value="M">MASCULINO</asp:ListItem>
                                                <asp:ListItem Value="F">FEMENINO</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="txtNumCelular" class="col-sm-4 col-form-label form-control-sm">Telefono(s):</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtNumCelular" runat="server" CssClass="form-control form-control-sm" placeholder="Celular" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtNumFijo" runat="server" CssClass="form-control form-control-sm" placeholder="Teléfono" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <h6 class="text-seccion">DIRECCIÓN ACTUAL</h6>
                                    <hr>
                                    <asp:UpdatePanel ID="udpDireccion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div class="form-group row">
                                                <label for="cmbDepartamento" class="col-sm-4 col-form-label form-control-sm">Departamento:</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="cmbDepartamento" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="cmbProvincia" class="col-sm-4 col-form-label form-control-sm">Provincia:</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="cmbProvincia" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="cmbDistrito" class="col-sm-4 col-form-label form-control-sm">Distrito:</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="cmbDistrito" runat="server" CssClass="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="txtDireccion" class="col-sm-4 col-form-label form-control-sm">Dirección:</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control form-control-sm" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-6">
                                    <div class="row">
                                        <label for="txtEmail" class="col-sm-4 col-form-label form-control-sm">Email:</label>
                                        <div class="col-sm-8">
                                            <asp:UpdatePanel ID="udpEmail" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-sm" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="form-check form-check-inline">
                                                <asp:CheckBox ID="chkNoTieneCorreo" runat="server" AutoPostBack="true" CssClass="form-check-input" />
                                                <label for="chkNoTieneCorreo" class="form-check-label">No tiene correo</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row" id="divEmailAlternativo" runat="server">
                                        <label for="txtEmail2" class="col-sm-4 col-form-label form-control-sm">Email alternativo:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtEmail2" runat="server" CssClass="form-control form-control-sm" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-6">
                                    <div class="row" id="divEstadoCivil" runat="server">
                                        <label for="cmbEstadoCivil" class="col-sm-4 col-form-label form-control-sm">Estado Civil:</label>
                                        <div class="col-sm-8">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="cmbEstadoCivil" runat="server" CssClass="form-control form-control-sm">
                                                        <asp:ListItem Value="-1">-- Seleccione --</asp:ListItem>
                                                        <asp:ListItem Value="SOLTERO">SOLTERO</asp:ListItem>
                                                        <asp:ListItem Value="CASADO">CASADO</asp:ListItem>
                                                        <asp:ListItem Value="VIUDO">VIUDO</asp:ListItem>
                                                        <asp:ListItem Value="DIVORCIADO">DIVORCIADO</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div class="row" id="divRUC" runat="server">
                                                <label for="txtRuc" class="col-sm-4 col-form-label form-control-sm">RUC:</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="txtRuc" runat="server" CssClass="form-control form-control-sm" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>  
                            <div class="collapse" id="divDeudas" runat="server">
                                <div class="row">
                                    <div class="offset-sm-1 col-sm-10">
                                        <div class="card card-body">
                                            <asp:UpdatePanel ID="udpDeudas" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <span class="badge badge-light">La persona cuenta con deudas PENDIENTES</span>
                                                    <asp:GridView ID="grwDeudas" runat="server" AutoGenerateColumns="false" CssClass="table table-sm"
                                                        GridLines="None">
                                                        <Columns>
                                                            <asp:BoundField DataField="descripcion_Sco" HeaderText="Servicio" />
                                                            <asp:BoundField DataField="montoTotal_Deu" HeaderText="Deuda" />
                                                            <asp:BoundField DataField="Pago_Deu" HeaderText="Pago" />
                                                            <asp:BoundField DataField="Saldo_Deu" HeaderText="Saldo" />
                                                        </Columns>
                                                        <HeaderStyle CssClass="thead-dark alt" />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card" id="divInfoEducativa" runat="server">
                        <div class="card-header">Información Educativa</div>
                        <div class="card-body">
                            <div class="form-group row">
                                <label for="cmbDepartamentoInstEduc" class="col-sm-2 col-form-label form-control-sm">Departamento</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="cmbDepartamentoInstEduc" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                    />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="cmbInstitucionEducativa" class="col-sm-2 col-form-label form-control-sm">Institución
                                    Educ.
                                </label>
                                <div class="col-sm-5">
                                    <asp:UpdatePanel ID="udpInstitucionEducativa" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbInstitucionEducativa" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"/>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <label for="cmbAnioEstudio" class="col-sm-2 col-form-label form-control-sm">Año de Estudio</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cmbAnioEstudio" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                        <asp:ListItem Value="-1" Selected="True">-- Seleccione --</asp:ListItem>
                                        <asp:ListItem Value="T" Enabled=false>3er Año</asp:ListItem>
                                        <asp:ListItem Value="C" Enabled=false>4to Año</asp:ListItem>
                                        <asp:ListItem Value="Q">5to Año</asp:ListItem>
                                        <asp:ListItem Value="E">Egresado</asp:ListItem>
                                        <asp:ListItem Value="P" Enabled=false>P</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card" id="divDatosLaborales" runat="server">
                        <div class="card-header">DATOS LABORALES</div>
                        <div class="card-body">
                            <div class="form-group row">
                                <label for="txtCentroLabores" class="col-sm-2 col-form-label form-control-sm">Centro de Labores</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtCentroLabores" runat="server" CssClass="form-control form-control-sm" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="txtCargoActual" class="col-sm-2 col-form-label form-control-sm">Cargo Actual
                                </label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtCargoActual" runat="server" CssClass="form-control form-control-sm" />
                                </div>
                            </div>
                        </div>
                    </div>   
                    <div class="card">
                        <div class="card-header">INSCRIPCIÓN</div>
                        <div class="card-body">
                            <div class="form-group row" id="divCicloIngreso" runat="server">
                                <label for="cmbCicloIngreso" class="col-sm-2 col-form-label form-control-sm">Proc. de Admisión:</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cmbCicloIngreso" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" />
                                </div>
                            </div>
                            <%-- <asp:UpdatePanel ID="udpDivEventoCRM" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div id="divEventoCRM" runat="server" class="form-group row">
                                        <label for="cmbEventoCRM" class="col-sm-2 col-form-label form-control-sm">Evento CRM:</label>
                                        <div class="col-sm-5">
                                            <asp:UpdatePanel ID="udpEventoCRM" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="cmbEventoCRM" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel> --%>
                            <div class="form-group row" id="divCarreraProfesional" runat="server">
                                <label for="cmbCarreraProfesional" class="col-sm-2 col-form-label form-control-sm">Carrera:
                                    </label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="cmbCarreraProfesional" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" />
                                </div>
                            </div>
                            <div class="form-group row" id="divModalidadIngreso" runat="server">
                                <label for="cmbModalidadIngreso" class="col-sm-2 col-form-label form-control-sm">Modalidad:</label>
                                <div class="col-sm-4">
                                    <asp:UpdatePanel ID="udpModalidad" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbModalidadIngreso" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="form-group row" id="divTipoParticipante" runat="server">
                                <label for="cmbTipoParticipante" class="col-sm-2 col-form-label form-control-sm">Tipo Inscripción:</label>
                                <div class="col-sm-4">
                                    <asp:UpdatePanel ID="udpTipoParticipante" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbTipoParticipante" runat="server" CssClass="form-control form-control-sm" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="form-group row" id="divModalidadIngresoEC" runat="server">
                                <label for="cmbModalidadIngresoEC" class="col-sm-2 col-form-label form-control-sm">Modalidad:</label>
                                <div class="col-sm-4">
                                    <asp:UpdatePanel ID="udpModalidadIngresoEC" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbModalidadIngresoEC" runat="server" CssClass="form-control form-control-sm" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="udpCostos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="form-group row" id="rowCostos" runat="server" data-oculto="true">
                                        <label for="cmbCarreraProfesional" class="col-sm-2 col-form-label form-control-sm">Costos Estimados: </label>
                                        <div class="col-sm-5">
                                            <div class="row">   
                                                <label class="col-sm-4 col-form-label form-control-sm">Costo Crédito:</label>
                                                <div class="col-sm-4 text-right"><span id="spnCostoCredito" runat="server"/></div>    
                                            </div>
                                            <div class="row">
                                                <label class="col-sm-4 col-form-label form-control-sm">Mensualidad:</label>
                                                <div class="col-sm-4 text-right"><span id="spnCostoMes" runat="server"/></div>
                                            </div>
                                            <div class="row">
                                                <label class="col-sm-4 col-form-label form-control-sm">Total Ciclo:</label>
                                                <div class="col-sm-4 text-right"><span id="spnCostoCiclo" runat="server"/></div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>                    
                    <div id="botonesAccion" runat="server" class="card-footer">
                        <div class="form-group row">
                            <div class="col-sm-10 offset-sm-2">
                                <button type="button" id="btnCancelar" runat="server" class="btn btn-outline-secondary">Cancelar</button>
                                <asp:Button ID="btnRegistrar" runat="server" UseSubmitBehavior="false" CssClass="btn btn-primary" Text="Registrar" OnClientClick="return ValidarForm();" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
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
                            <div id="mensaje" class="alert alert-light"></div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="mdlMensajeServidor" class="modal fade" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
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
                                    <div id="respuestaPostback" runat="server" class="alert" data-rpta="" data-msg=""></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel ID="udpMensajeServidorFooter" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <button type="button" id="btnMensajeAceptarNuevaPersona" runat="server" class="btn btn-primary" data-dismiss="modal">Aceptar</button>
                                    <button type="button" id="btnMensajeCancelarNuevaPersona" runat="server" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                                    <button type="button" id="btnMensajeCerrar" runat="server" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <div id="mdlCoincidencias" class="modal fade" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <span class="modal-title">Seleccione una persona</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="udpCoincidencias" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:GridView ID="grwCoincidencias" runat="server" AutoGenerateColumns="false" DataKeyNames="nroDocIdent" CssClass="table table-sm"
                                        GridLines="None">
                                        <Columns>
                                            <asp:BoundField HeaderText="Nro" />
                                            <asp:BoundField DataField="nroDocIdent" HeaderText="DNI" />
                                            <asp:BoundField DataField="apellidoPaterno" HeaderText="Ape. Paterno" />
                                            <asp:BoundField DataField="apellidoMaterno" HeaderText="Ape. Materno" />
                                            <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion" HeaderText="Sel." />
                                        </Columns>
                                        <HeaderStyle CssClass="thead-dark alt" />
                                    </asp:GridView>
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
        <script src="../../assets/jquery-validation/jquery.validate.min.js"></script>
        <script src="../../assets/jquery-validation/localization/messages_es.min.js"></script>
        <%-- <script src="../../assets/jquery-validation/additional-methods.min.js"></script> --%>
        <script src="../../assets/iframeresizer/iframeResizer.contentWindow.min.js"></script>
        <!-- Scripts propios -->
        <script src="js/funciones.js?1"></script>
        <script src="js/inscripcionInteresadoAdmision.js?40"></script>
        <script type="text/javascript">
            var controlId = ''
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
                var elem = args.get_postBackElement();

                controlId = elem.id
                switch (controlId) {
                    case 'lnkObtenerDatos':
                    case 'lnkObtenerDatosPorApellidos':
                        BuscandoDatos();
                        break;
                }

                if (controlId.indexOf('btnSeleccionarPersona') > -1) {
                    SeleccionandoPersona(controlId);
                }
            });

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function(sender, args) {
                var error = args.get_error();
                if (error) {
                    LlamarParentWithErrors();
                    return false;
                }
            });

            Sys.Application.add_load(function() {        
                InicializarControles();
                
                switch (controlId){
                    case 'lnkObtenerDatos':
                    case 'lnkObtenerDatosPorApellidos':
                        DatosBuscados();
                        break;
                    case 'btnRegistrar':
                        SubmitPostBack();
                        break;
                }

                if (controlId.indexOf('btnSeleccionarPersona') > -1) {
                    PersonaSeleccionada();
                }

                RevisarMensajePostback();
            });
        </script>
    </body>
    </html>