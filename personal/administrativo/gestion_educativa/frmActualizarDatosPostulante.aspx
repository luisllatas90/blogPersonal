<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmActualizarDatosPostulante.aspx.vb" Inherits="administrativo_pec_test_frmActualizarDatosPostulante" EnableViewStateMac="False" enableEventValidation="False" %>

    <!DOCTYPE html>
    <html lang="es">

    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>Actualizar Datos Postulante</title>
        <!-- Estilos externos -->
        <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css">
        <link rel="stylesheet" href="../../assets/smart-wizard/css/smart_wizard.css">
        <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.css">
        <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
        <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
        <!-- Estilos propios -->
        <link rel="stylesheet" href="css/style.css?30">
        <link rel="stylesheet" href="css/actualizarDatosPostulante.css?40">
    </head>

    <body id="actualizar-datos-postulante">
        <img src="img/loading.gif" id="loading-gif">
        <form id="frmDatosPostulante" runat="server" class="frm-datos-postulante" data-edit="">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="udpForm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="data-server" id="isPostBack" data-value="<%=Convert.toInt32(Page.isPostBack)%>"></div>
                    <div id="smartwizard">
                        <ul>
                            <li id="liDatosPostulacion" runat="server">
                                <a href="#divDatosPostulacion" data-step="1">
                                    <small>DATOS DE POSTULACIÓN</small>
                                </a>
                            </li>
                            <li>
                                <a href="#step-2" data-step="2">
                                    <small>INFORMACIÓN PERSONAL</small>
                                </a>
                            </li>
                            <li id="liInformacionEducativa" runat="server">
                                <a href="#divInformacionEducativa" data-step="3">
                                    <small>INFORMACIÓN EDUCATIVA</small>
                                </a>
                            </li>
                            <li>
                                <a href="#step-4" data-step="4">
                                    <small>FAMILIARES</small>
                                </a>
                            </li>
                        </ul>
                        <div>
                            <div id="divDatosPostulacion" class="tab-pane step-content" runat="server">
                                <div class="card">
                                    <div class="card-body">
                                        <h6 class="text-seccion">DATOS GENERALES</h6>
                                        <hr>
                                        <div class="form-group row">
                                            <label for="cicloIngreso" class="col-sm-2 col-form-label form-control-sm">Ciclo Académico</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbCicloIngreso" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label for="carreraProfesional" class="col-sm-2 col-form-label form-control-sm">Carrera
                                                Profesional </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="cmbCarreraProfesional" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label for="cmbModalidadIngreso" class="col-sm-2 col-form-label form-control-sm">Modalidad</label>
                                            <div class="col-sm-4">
                                                <asp:UpdatePanel ID="udpModalidadIngreso" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="cmbModalidadIngreso" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div id="divNivelacion" runat="server">
                                            <div class="form-group row">
                                                <label for="" class="col-sm-2 col-form-label form-control-sm">Nivelación</label>
                                                <div class="col-sm-2">
                                                    <div class="form-check form-check-inline">
                                                        <asp:CheckBox ID="chkNivCompetencia1" runat="server" CssClass="form-check-input" />
                                                        <label for="chkNivCompetencia1" class="form-check-label">Redacción</label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="row">
                                                        <div class="col-sm-10">
                                                            <asp:TextBox ID="txtCalifNivCompetencia1" runat="server" CssClass="form-control form-control-sm" placeholder="Calific." />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-check form-check-inline">
                                                        <asp:CheckBox ID="chkNivCompetencia2" runat="server" CssClass="form-check-input" />
                                                        <label for="chkNivCompetencia2" class="form-check-label">Comprensión</label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="row">
                                                        <div class="col-sm-10">
                                                            <asp:TextBox ID="txtCalifNivCompetencia2" runat="server" CssClass="form-control form-control-sm" placeholder="Calific." />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-sm-2 offset-sm-2">
                                                    <div class="form-check form-check-inline">
                                                        <asp:CheckBox ID="chkNivCompetencia3" runat="server" CssClass="form-check-input" />
                                                        <label for="chkNivCompetencia3" class="form-check-label">Matemáticas</label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="row">
                                                        <div class="col-sm-10">
                                                            <asp:TextBox ID="txtCalifNivCompetencia3" runat="server" CssClass="form-control form-control-sm" placeholder="Calific." />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-check form-check-inline">
                                                        <asp:CheckBox ID="chkNivCompetencia4" runat="server" CssClass="form-check-input" />
                                                        <label for="chkNivCompetencia4" class="form-check-label">Biociencias</label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="row">
                                                        <div class="col-sm-10">
                                                            <asp:TextBox ID="txtCalifNivCompetencia4" runat="server" CssClass="form-control form-control-sm" placeholder="Calific." />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="step-2" class="tab-pane step-content">
                                <div class="card">
                                    <div class="card-body">
                                        <asp:UpdatePanel ID="udpDatosPersonales" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <h6 class="text-seccion">DATOS PERSONALES</h6>
                                                        <hr>
                                                        <div class="form-group row">
                                                            <label for="tipoDocIdentidad" class="col-sm-4 col-form-label form-control-sm">Tipo Doc. Identidad:</label>
                                                            <div class="col-sm-8">
                                                                <asp:DropDownList ID="cmbTipoDocIdentidad" runat="server" CssClass="form-control form-control-sm">
                                                                    <asp:ListItem Selected="True" Value="DNI">DNI</asp:ListItem>
                                                                    <asp:ListItem Value="CARNE DE EXTRANJERIA">CARNÉ DE EXTRANJERÍA</asp:ListItem>
                                                                    <asp:ListItem Value="PASAPORTE">PASAPORTE</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="nroDocIdentidad" class="col-sm-4 col-form-label form-control-sm">N° Doc. Identidad:</label>
                                                            <div class="col-sm-8">
                                                                <div class="row no-gutters">
                                                                    <div class="col-sm-5">
                                                                        <asp:TextBox ID="txtNroDocIdentidad" runat="server" CssClass="form-control form-control-sm" placeholder="DNI (*)" />
                                                                    </div>
                                                                    <div class="col-sm-7">
                                                                        <a href="#" id="lnkObtenerDatos" runat="server" class="btn disabled"><i class="fa fa-search"></i>Buscar Datos</a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="apellidoPaterno" class="col-sm-4 col-form-label form-control-sm">Apellidos:</label>
                                                            <div class="col-sm-8">
                                                                <div class="row no-gutters">
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtApellidoPaterno" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="Ape. Paterno (*)" />
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtApellidoMaterno" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="Ape. Materno (*)" />
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <a href="#" id="lnkObtenerDatosPorApellidos" runat="server" visible="false" class="btn disabled"><i class="fa fa-search"></i>Buscar por Apellidos</a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtNombres" class="col-sm-4 col-form-label form-control-sm">Nombres:</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="Nombres (*)" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="fecNacimiento" class="col-sm-4 col-form-label form-control-sm">Fec. Nacimiento:</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="dtpFecNacimiento" runat="server" CssClass="form-control form-control-sm" placeholder="Fec. Nacimiento (*)" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="genero" class="col-sm-4 col-form-label form-control-sm">Sexo:</label>
                                                            <div class="col-sm-5">
                                                                <asp:DropDownList ID="cmbGenero" runat="server" CssClass="form-control form-control-sm">
                                                                    <asp:ListItem Value="-1" Selected="True">-- Seleccione --</asp:ListItem>
                                                                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                                    <asp:ListItem Value="F">Femenino</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="numTelefono" class="col-sm-4 col-form-label form-control-sm">Telefono Fijo:</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtNumTelefono" runat="server" CssClass="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="numCelular" class="col-sm-4 col-form-label form-control-sm">N° Celular:</label>
                                                            <div class="col-sm-8">
                                                                <div class="row no-gutters">
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtNumCelular" runat="server" CssClass="form-control form-control-sm" />
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:DropDownList ID="cmbOperadorCelular" runat="server" CssClass="form-control form-control-sm">
                                                                            <asp:ListItem Selected="True" Value="">-- Seleccione --</asp:ListItem>
                                                                            <asp:ListItem Value="MOVISTAR">Movistar</asp:ListItem>
                                                                            <asp:ListItem Value="CLARO">Claro</asp:ListItem>
                                                                            <asp:ListItem Value="ENTEL">Entel</asp:ListItem>
                                                                            <asp:ListItem Value="BITEL">Bitel</asp:ListItem>
                                                                            <asp:ListItem Value="INKACEL">Inkacel</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>    
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtEmail" class="col-sm-4 col-form-label form-control-sm">Email</label>
                                                            <div class="col-sm-8">
                                                                <asp:UpdatePanel ID="udpEmail" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtEmail" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" placeholder="Email" />
                                                                        <asp:TextBox ID="txtEmailAlternativo" runat="server" CssClass="form-control form-control-sm" placeholder="Email Alternativo" />
                                                                        <asp:HiddenField ID="hddEmailCoincidente" runat="server" Value="0" />
                                                                        <asp:HiddenField ID="hddEmailVerificado" runat="server" Value="0" />
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
                                                        <h6 class="text-seccion">LUGAR DE NACIMIENTO</h6>
                                                        <hr>
                                                        <div class="form-group row">
                                                            <label for="paisNacimiento" class="col-sm-4 col-form-label form-control-sm">Pais</label>
                                                            <div class="col-sm-8">
                                                                <asp:DropDownList ID="cmbPaisNacimiento" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <asp:UpdatePanel ID="udpLugarNacimiento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                            <ContentTemplate>
                                                                <div class="form-group row">
                                                                    <label for="dptoNacimiento" class="col-sm-4 col-form-label form-control-sm">Departamento</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="cmbDptoNacimiento" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label for="prvNacimiento" class="col-sm-4 col-form-label form-control-sm">Provincia</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="cmbPrvNacimiento" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label for="dstNacimiento" class="col-sm-4 col-form-label form-control-sm">Distrito</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="cmbDstNacimiento" runat="server" CssClass="form-control form-control-sm">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <br>
                                                        <h6 class="text-seccion">DIRECCIÓN ACTUAL</h6>
                                                        <hr>
                                                        <div class="form-group row">
                                                            <label for="dptoActual" class="col-sm-4 col-form-label form-control-sm">Departamento</label>
                                                            <div class="col-sm-8">
                                                                <asp:DropDownList ID="cmbDptoActual" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <asp:UpdatePanel ID="udpDireccionActual" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                            <ContentTemplate>
                                                                <div class="form-group row">
                                                                    <label for="prvActual" class="col-sm-4 col-form-label form-control-sm">Provincia</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="cmbPrvActual" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label for="dstActual" class="col-sm-4 col-form-label form-control-sm">Distrito</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="cmbDstActual" runat="server" CssClass="form-control form-control-sm">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <div class="form-group row">
                                                            <label for="txtDireccion" class="col-sm-4 col-form-label form-control-sm">Dirección</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtDireccion" runat="server" TextMode="multiline" Rows="3" CssClass="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:UpdatePanel ID="udpDeudas" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                    <ContentTemplate>
                                                        <div class="collapse" id="divDeudas" runat="server">
                                                            <div class="row">
                                                                <div class="offset-sm-1 col-sm-10">
                                                                    <div class="card card-body">
                                                                        <span class="badge badge-light">La persona cuenta con deudas PENDIENTES</span>
                                                                        <asp:GridView ID="grwDeudas" runat="server" AutoGenerateColumns="false" CssClass="table table-sm"
                                                                            GridLines="None">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="descripcion_Sco" HeaderText="Servicio" />
                                                                                <asp:BoundField DataField="montoTotal_Deu" HeaderText="Deuda" />
                                                                                <asp:BoundField DataField="Pago_Deu" HeaderText="Pago" />
                                                                                <asp:BoundField DataField="Saldo_Deu" HeaderText="Saldo" />
                                                                                <asp:BoundField DataField="fechaVencimiento_Deu" HeaderText="Fec. Vence" DataFormatString="{0:dd/MM/yyyy}" />
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="thead-dark alt" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <br>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h6 class="text-seccion">DATOS ADICIONALES</h6>
                                                        <hr>
                                                        <div class="form-group row">
                                                            <label class="col-sm-2 col-form-label form-control-sm">Discapacidad(es)</label>
                                                            <div class="col-sm-5">
                                                                <div class="form-check form-check-inline">
                                                                    <asp:CheckBox ID="chkDiscFisica" runat="server" CssClass="form-check-input" />
                                                                    <label for="chkDiscFisica" class="form-check-label">Física</label>
                                                                </div>
                                                                <div class="form-check form-check-inline">
                                                                    <asp:CheckBox ID="chkDiscSensorial" runat="server" CssClass="form-check-input" />
                                                                    <label for="chkDiscSensorial" class="form-check-label">Sensorial</label>
                                                                </div>
                                                                <div class="form-check form-check-inline">
                                                                    <asp:CheckBox ID="chkDiscAuditiva" runat="server" CssClass="form-check-input" />
                                                                    <label for="chkDiscAuditiva" class="form-check-label">Auditiva</label>
                                                                </div>
                                                                <div class="form-check form-check-inline">
                                                                    <asp:CheckBox ID="chkDiscVisual" runat="server" CssClass="form-check-input" />
                                                                    <label for="chkDiscVisual" class="form-check-label">Visual</label>
                                                                </div>
                                                                <div class="form-check form-check-inline">
                                                                    <asp:CheckBox ID="chkDiscIntelectual" runat="server" CssClass="form-check-input " />
                                                                    <label for="chkDiscIntelectual" class="form-check-label">Intelectual</label>
                                                                </div>
                                                            </div>
                                                            <label class="col-sm-1 col-form-label form-control-sm">Otra</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtDiscOtra" runat="server" CssClass="form-control form-control-sm" placeholder="Otra Discapacidad" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="cmbReligion" class="col-sm-2 col-form-label form-control-sm">Religión</label>
                                                            <div class="col-sm-4">
                                                                <asp:DropDownList ID="cmbReligion" runat="server" CssClass="form-control form-control-sm">
                                                                    <asp:ListItem Selected="True" Value="">-- Seleccione --</asp:ListItem>
                                                                    <asp:ListItem Value="CATOLICO">Católico</asp:ListItem>
                                                                    <asp:ListItem Value="CATOLICO">Protestante</asp:ListItem>
                                                                    <asp:ListItem Value="CATOLICO">Ateo</asp:ListItem>
                                                                    <asp:ListItem Value="CATOLICO">Otro</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label class="col-sm-2 col-form-label form-control-sm">Sacramento(s)</label>
                                                            <div class="col-sm-5">
                                                                <div class="form-check form-check-inline">
                                                                    <asp:CheckBox ID="chkSacBautismo" runat="server" CssClass="form-check-input" />
                                                                    <label for="chkSacBautismo" class="form-check-label">Bautismo</label>
                                                                </div>
                                                                <div class="form-check form-check-inline">
                                                                    <asp:CheckBox ID="chkSacEucaristia" runat="server" CssClass="form-check-input" />
                                                                    <label for="chkSacEucaristia" class="form-check-label">Eucaristía</label>
                                                                </div>
                                                                <div class="form-check form-check-inline">
                                                                    <asp:CheckBox ID="chkSacConfirmacion" runat="server" CssClass="form-check-input" />
                                                                    <label for="chkSacConfirmacion" class="form-check-label">Confirmación</label>
                                                                </div>
                                                                <div class="form-check form-check-inline">
                                                                    <asp:CheckBox ID="chkSacMatrimonio" runat="server" CssClass="form-check-input" />
                                                                    <label for="chkSacMatrimonio" class="form-check-label">Matrimonio</label>
                                                                </div>
                                                                <div class="form-check form-check-inline">
                                                                    <asp:CheckBox ID="chkSacOrdenSacerdotal" runat="server" CssClass="form-check-input" />
                                                                    <label for="chkSacOrdenSacerdotal" class="form-check-label">Orden Sacerdotal</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div id="divInformacionEducativa" class="tab-pane step-content" runat="server">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h6 class="text-seccion">DATOS GENERALES</h6>
                                                <hr>
                                                <div class="form-group row">
                                                    <label for="cmbPaisInstitucion" class="col-sm-2 col-form-label form-control-sm">Procedencia</label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="cmbPaisInstitucion" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <asp:UpdatePanel ID="udpInstitucionEducativa" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                    <ContentTemplate>
                                                        <div class="form-group row">
                                                            <label for="cmbDptoInstitucion" class="col-sm-2 col-form-label form-control-sm">Departamento</label>
                                                            <div class="col-sm-4">
                                                                <asp:DropDownList ID="cmbDptoInstitucion" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="cmbPrvInstitucion" class="col-sm-2 col-form-label form-control-sm">Provincia</label>
                                                            <div class="col-sm-4">
                                                                <asp:DropDownList ID="cmbPrvInstitucion" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="cmbDstInstitucion" class="col-sm-2 col-form-label form-control-sm">Distrito</label>
                                                            <div class="col-sm-4">
                                                                <asp:DropDownList ID="cmbDstInstitucion" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="cmbInstitucionEducativa" class="col-sm-2 col-form-label form-control-sm">Inst. Educativa</label>
                                                            <div class="col-sm-9">
                                                                <asp:DropDownList ID="cmbInstitucionEducativa" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <asp:UpdatePanel ID="udpCostos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                            <ContentTemplate>
                                                                <div class="form-group row" id="rowCostos" runat="server" data-oculto="true">
                                                                    <div class="col-sm-9 offset-sm-2">
                                                                        <div class="row">   
                                                                            <label class="col-sm-2 col-form-label form-control-sm">Cst. Crédito:</label>
                                                                            <span class="col-sm-2" id="spnCostoCredito" runat="server"/>
                                                                            <label class="col-sm-2 col-form-label form-control-sm">Mensualidad:</label>
                                                                            <span class="col-sm-2" id="spnCostoMes" runat="server"/>
                                                                            <label class="col-sm-2 col-form-label form-control-sm">Total Ciclo:</label>
                                                                            <span class="col-sm-2" id="spnCostoCiclo" runat="server"/>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="udpInstEducDatosAdicionales" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                    <ContentTemplate>
                                                        <div class="form-group row">
                                                            <label for="rbtInstPublica" class="col-sm-2 col-form-label form-control-sm">Tipo</label>
                                                            <div class="col-sm-3">
                                                                <div class="form-check form-check-inline">
                                                                    <asp:RadioButton ID="rbtInstPublica" GroupName="tipoInstitucion" runat="server" CssClass="form-check-label" Checked="true"/>
                                                                    <label for="rbtInstPublica" class="form-check-label">Pública</label>
                                                                </div>
                                                                <div class="form-check form-check-inline">
                                                                    <asp:RadioButton ID="rbtInstPrivada" GroupName="tipoInstitucion" runat="server" CssClass="form-check-label" />
                                                                    <label for="rbtInstPrivada" class="form-check-label">Privada</label>
                                                                </div>
                                                            </div>
                                                            <label for="rbtColegioAplicacionNo" class="col-sm-2 col-form-label form-control-sm">Col. Aplicación</label>
                                                            <div class="col-sm-3">
                                                                <div class="form-check form-check-inline">
                                                                    <asp:RadioButton ID="rbtColegioAplicacionNo" GroupName="colegioAplicacion" runat="server" CssClass="form-check-label" Checked="true"/>
                                                                    <label for="rbtColegioAplicacionNo" class="form-check-label">No</label>
                                                                </div>
                                                                <div class="form-check form-check-inline">
                                                                    <asp:RadioButton ID="rbtColegioAplicacionSi" GroupName="colegioAplicacion" runat="server" CssClass="form-check-label" />
                                                                    <label for="rbtColegioAplicacionSi" class="form-check-label">Si</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="form-group row">
                                                    <label for="txtAnioPromocion" class="col-sm-2 col-form-label form-control-sm">Promoción</label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtAnioPromocion" runat="server" CssClass="form-control form-control-sm" placeholder="Año" />
                                                    </div>
                                                    <label for="txtSeccionPromocion" class="col-sm-1 col-form-label form-control-sm">Sección</label>
                                                    <div class="col-sm-1">
                                                        <asp:TextBox ID="txtSeccionPromocion" runat="server" CssClass="form-control form-control-sm" placeholder="Sec." maxlength="1"/>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-check form-check-inline">
                                                            <asp:CheckBox ID="chkEgresado" runat="server" CssClass="form-check-input" Checked="true" />
                                                            <label for="chkEgresado" class="form-check-label">Egresado</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-2 col-form-label form-control-sm">Mérito</label>
                                                    <div class="col-sm-4">
                                                        <div class="form-check form-check-inline">
                                                            <asp:RadioButton ID="rbtMerito1ro" GroupName="merito" runat="server" CssClass="form-check-input" />
                                                            <label for="rbtMerito1ro" class="form-check-label">1°</label>
                                                        </div>
                                                        <div class="form-check form-check-inline">
                                                            <asp:RadioButton ID="rbtMerito2do" GroupName="merito" runat="server" CssClass="form-check-input" />
                                                            <%-- <input type="radio" class="form-check-input" name="merito" id="rbtMerito2do"> --%>
                                                            <label for="rbtMerito2do" class="form-check-label">2°</label>
                                                        </div>
                                                        <div class="form-check form-check-inline">
                                                            <asp:RadioButton ID="rbtMeritoNinguno" GroupName="merito" runat="server" CssClass="form-check-input" Checked="true" />
                                                            <%-- <input type="radio" class="form-check-input" name="merito" id="rbtMeritoNinguno"> --%>
                                                            <label for="rbtMeritoNinguno" class="form-check-label">Ninguno</label>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <asp:UpdatePanel ID="udpFechaPrimeraMatricula" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                            <ContentTemplate>
                                                                <div class="row" id="divFechaPrimeraMatricula" runat="server">
                                                                    <label class="col-sm-7 col-form-label form-control-sm">Fecha 1° Matrícula</label>
                                                                    <div class="col-sm-5">
                                                                        <asp:TextBox ID="dtpFechaPrimeraMatricula" runat="server" CssClass="form-control form-control-sm" placeholder="Fecha 1° Matr."/>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="cmbNivelIngles" class="col-sm-2 col-form-label form-control-sm">Nivel de Inglés</label>
                                                    <div class="col-sm-2" style="padding-right: 0;">
                                                        <asp:DropDownList ID="cmbNivelIngles" runat="server" CssClass="form-control form-control-sm">
                                                            <asp:ListItem Selected="True" Value="">NINGUNO</asp:ListItem>
                                                            <asp:ListItem Value="B">BÁSICO</asp:ListItem>
                                                            <asp:ListItem Value="I">INTERMEDIO</asp:ListItem>
                                                            <asp:ListItem Value="A">AVANZADO</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label for="txtOtrosDatosIngles" class="col-sm-2 col-form-label form-control-sm">Otros Datos Inglés</label>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="txtOtrosDatosIngles" runat="server" TextMode="multiline" Rows="3" 
                                                            CssClass="form-control form-control-sm" placeholder="Otros Datos Inglés" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="step-4" class="tab-pane step-content">
                                <asp:UpdatePanel ID="udpDatosFamiliares" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div class="card">
                                            <h6 class="card-header">INFORMACIÓN DEL PADRE</h6>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <h6 class="text-seccion">DATOS GENERALES</h6>
                                                        <hr>
                                                        <div class="form-group row">
                                                            <label for="txtApellidoPaternoPadre" class="col-sm-4 col-form-label form-control-sm">Apellidos</label>
                                                            <div class="col-sm-8">
                                                                <div class="row no-gutters">
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtApellidoPaternoPadre" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="Ape. Paterno" />
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtApellidoMaternoPadre" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="Ape. Materno" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtNombresPadre" class="col-sm-4 col-form-label form-control-sm">Nombres</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtNombresPadre" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="Nombres" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtDocIdentPadre" class="col-sm-4 col-form-label form-control-sm">DNI</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtDocIdentPadre" runat="server" CssClass="form-control form-control-sm only-digits" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="dtpFecNacPadre" class="col-sm-4 col-form-label form-control-sm">Fec.
                                                                Nacimiento</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="dtpFecNacPadre" runat="server" CssClass="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtEmailPadre" class="col-sm-4 col-form-label form-control-sm">Email</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtEmailPadre" runat="server" CssClass="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label class="col-sm-4 col-form-label form-control-sm">Resp. de Pago</label>
                                                            <div class="form-check form-check-inline">
                                                                <asp:RadioButton ID="rbtResPagPadreNo" GroupName="resPagPadre" runat="server" CssClass="form-check-input" Checked="true"
                                                                />
                                                                <label for="rbtResPagPadreNo" class="form-check-label">No</label>
                                                            </div>
                                                            <div class="form-check form-check-inline">
                                                                <asp:RadioButton ID="rbtResPagPadreSi" GroupName="resPagPadre" runat="server" CssClass="form-check-input" />
                                                                <label for="rbtResPagPadreSi" class="form-check-label">Si</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h6 class="text-seccion">DATOS DE CONTACTO</h6>
                                                        <hr>
                                                        <asp:UpdatePanel ID="udpDireccionPadre" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                            <ContentTemplate>
                                                                <div class="form-group row">
                                                                    <label for="depActPadre" class="col-sm-4 col-form-label form-control-sm">Departamento</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="cmbDptoPadre" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label for="prvActPadre" class="col-sm-4 col-form-label form-control-sm">Provincia</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="cmbPrvPadre" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label for="dstActPadre" class="col-sm-4 col-form-label form-control-sm">Distrito</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="cmbDstPadre" runat="server" CssClass="form-control form-control-sm">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <div class="form-group row">
                                                            <label for="txtDirPadre" class="col-sm-4 col-form-label form-control-sm">Dirección</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtDirPadre" runat="server" CssClass="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtNumTelPadre" class="col-sm-4 col-form-label form-control-sm">Telefono
                                                                Fijo</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtNumTelPadre" runat="server" CssClass="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtNumCelPadre" class="col-sm-4 col-form-label form-control-sm">N°
                                                                Celular</label>
                                                            <div class="col-sm-8">
                                                                <div class="row no-gutters">
                                                                    <div class="col-sm-5">
                                                                        <asp:TextBox ID="txtNumCelPadre" runat="server" CssClass="form-control form-control-sm" />
                                                                    </div>
                                                                    <div class="col-sm-7">
                                                                        <asp:DropDownList ID="cmbOpeCelPadre" runat="server" CssClass="form-control form-control-sm">
                                                                            <asp:ListItem Selected="True" Value="">-- Seleccione --</asp:ListItem>
                                                                            <asp:ListItem Value="MOVISTAR">Movistar</asp:ListItem>
                                                                            <asp:ListItem Value="CLARO">Claro</asp:ListItem>
                                                                            <asp:ListItem Value="ENTEL">Entel</asp:ListItem>
                                                                            <asp:ListItem Value="BITEL">Bitel</asp:ListItem>
                                                                            <asp:ListItem Value="INKACEL">Inkacel</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div> 
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <br>
                                        <div class="card">
                                            <h6 class="card-header">INFORMACIÓN DE LA MADRE</h6>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <h6 class="text-seccion">DATOS GENERALES</h6>
                                                        <hr>
                                                        <div class="form-group row">
                                                            <label for="txtApellidoPaternoMadre" class="col-sm-4 col-form-label form-control-sm">Apellidos</label>
                                                            <div class="col-sm-8">
                                                                <div class="row no-gutters">
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtApellidoPaternoMadre" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="Ape. Paterno" />
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtApellidoMaternoMadre" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="Ape. Materno" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtNombresMadre" class="col-sm-4 col-form-label form-control-sm">Nombres</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtNombresMadre" runat="server" CssClass="form-control form-control-sm only-letters" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtDocIdentMadre" class="col-sm-4 col-form-label form-control-sm">DNI</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtDocIdentMadre" runat="server" CssClass="form-control form-control-sm only-digits" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="dtpFecNacMadre" class="col-sm-4 col-form-label form-control-sm">Fec.
                                                                Nacimiento</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="dtpFecNacMadre" runat="server" CssClass="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtEmailMadre" class="col-sm-4 col-form-label form-control-sm">Email</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtEmailMadre" runat="server" CssClass="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label class="col-sm-4 col-form-label form-control-sm">Resp. de Pago</label>
                                                            <div class="form-check form-check-inline">
                                                                <asp:RadioButton ID="rbtResPagMadreNo" GroupName="resPagMadre" runat="server" CssClass="form-check-input" Checked="true"
                                                                />
                                                                <label for="rbtResPagMadreNo" class="form-check-label">No</label>
                                                            </div>
                                                            <div class="form-check form-check-inline">
                                                                <asp:RadioButton ID="rbtResPagMadreSi" GroupName="resPagMadre" runat="server" CssClass="form-check-input" />
                                                                <label for="rbtResPagMadreSi" class="form-check-label">Si</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h6 class="text-seccion">DATOS DE CONTACTO</h6>
                                                        <hr>
                                                        <asp:UpdatePanel ID="udpDireccionMadre" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                            <ContentTemplate>
                                                                <div class="form-group row">
                                                                    <label for="depActMadre" class="col-sm-4 col-form-label form-control-sm">Departamento</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="cmbDptoMadre" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label for="prvActMadre" class="col-sm-4 col-form-label form-control-sm">Provincia</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="cmbPrvMadre" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label for="dstActMadre" class="col-sm-4 col-form-label form-control-sm">Distrito</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="cmbDstMadre" runat="server" CssClass="form-control form-control-sm">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <div class="form-group row">
                                                            <label for="txtDirMadre" class="col-sm-4 col-form-label form-control-sm">Dirección</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtDirMadre" runat="server" CssClass="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtNumTelMadre" class="col-sm-4 col-form-label form-control-sm">Telefono
                                                                Fijo</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtNumTelMadre" runat="server" CssClass="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtNumCelMadre" class="col-sm-4 col-form-label form-control-sm">N°
                                                                Celular</label>
                                                            <div class="col-sm-8">
                                                                <div class="row no-gutters">
                                                                    <div class="col-sm-5">
                                                                        <asp:TextBox ID="txtNumCelMadre" runat="server" CssClass="form-control form-control-sm" />
                                                                    </div>
                                                                    <div class="col-sm-7">
                                                                        <asp:DropDownList ID="cmbOpeCelMadre" runat="server" CssClass="form-control form-control-sm">
                                                                            <asp:ListItem Selected="True" Value="">-- Seleccione --</asp:ListItem>
                                                                            <asp:ListItem Value="MOVISTAR">Movistar</asp:ListItem>
                                                                            <asp:ListItem Value="CLARO">Claro</asp:ListItem>
                                                                            <asp:ListItem Value="ENTEL">Entel</asp:ListItem>
                                                                            <asp:ListItem Value="BITEL">Bitel</asp:ListItem>
                                                                            <asp:ListItem Value="INKACEL">Inkacel</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div> 
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <br>
                                        <div class="card">
                                            <h6 class="card-header">
                                                <div class="form-check form-check-inline">
                                                    <asp:CheckBox ID="chkInfApoderado" runat="server" CssClass="form-check-input" />
                                                    <label for="chkInfApoderado" class="form-check-label">INFORMACIÓN DEL APODERADO</label>
                                                </div>
                                            </h6>
                                            <div ID="datos-apoderado" class="card-body <% If Not chkInfApoderado.Checked Then  %>d-none<% End If %>">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <h6 class="text-seccion">DATOS GENERALES</h6>
                                                        <hr>
                                                        <div class="form-group row">
                                                            <label for="txtApellidoPaternoApoderado" class="col-sm-4 col-form-label form-control-sm">Apellidos</label>
                                                            <div class="col-sm-8">
                                                                <div class="row no-gutters">
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtApellidoPaternoApoderado" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="Ape. Paterno" />
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtApellidoMaternoApoderado" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="Ape. Materno" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtNombresApoderado" class="col-sm-4 col-form-label form-control-sm">Nombres</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtNombresApoderado" runat="server" CssClass="form-control form-control-sm only-letters" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtDocIdentApoderado" class="col-sm-4 col-form-label form-control-sm">DNI</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtDocIdentApoderado" runat="server" CssClass="form-control form-control-sm only-digits" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="dtpFecNacApoderado" class="col-sm-4 col-form-label form-control-sm">Fec.
                                                                Nacimiento</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="dtpFecNacApoderado" runat="server" CssClass="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtEmailApoderado" class="col-sm-4 col-form-label form-control-sm">Email</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtEmailApoderado" runat="server" CssClass="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label class="col-sm-4 col-form-label form-control-sm">Resp. de Pago</label>
                                                            <div class="form-check form-check-inline">
                                                                <asp:RadioButton ID="rbtResPagApoderadoNo" GroupName="resPagApoderado" runat="server" autopostback="true" CssClass="form-check-input" Checked="true"
                                                                />
                                                                <label for="rbtResPagApoderadoNo" class="form-check-label">No</label>
                                                            </div>
                                                            <div class="form-check form-check-inline">
                                                                <asp:RadioButton ID="rbtResPagApoderadoSi" GroupName="resPagApoderado" runat="server" autopostback="true" CssClass="form-check-input" />
                                                                <label for="rbtResPagApoderadoSi" class="form-check-label">Si</label>
                                                            </div> 
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h6 class="text-seccion">DATOS DE CONTACTO</h6>
                                                        <hr>
                                                        <asp:UpdatePanel ID="udpDireccionApoderado" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                            <ContentTemplate>
                                                                <div class="form-group row">
                                                                    <label for="depActApoderado" class="col-sm-4 col-form-label form-control-sm">Departamento</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="cmbDptoApoderado" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label for="prvActApoderado" class="col-sm-4 col-form-label form-control-sm">Provincia</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="cmbPrvApoderado" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label for="dstActApoderado" class="col-sm-4 col-form-label form-control-sm">Distrito</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="cmbDstApoderado" runat="server" CssClass="form-control form-control-sm">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <div class="form-group row">
                                                            <label for="txtDirApoderado" class="col-sm-4 col-form-label form-control-sm">Dirección</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtDirApoderado" runat="server" CssClass="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtNumTelApoderado" class="col-sm-4 col-form-label form-control-sm">Telefono
                                                                Fijo</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtNumTelApoderado" runat="server" CssClass="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtNumCelApoderado" class="col-sm-4 col-form-label form-control-sm">N°
                                                                Celular</label>
                                                            <div class="col-sm-8">
                                                                <div class="row no-gutters">
                                                                    <div class="col-sm-5">
                                                                        <asp:TextBox ID="txtNumCelApoderado" runat="server" CssClass="form-control form-control-sm" />
                                                                    </div>
                                                                    <div class="col-sm-7">
                                                                        <asp:DropDownList ID="cmbOpeCelApoderado" runat="server" CssClass="form-control form-control-sm">
                                                                            <asp:ListItem Selected="True" Value="">-- Seleccione --</asp:ListItem>
                                                                            <asp:ListItem Value="MOVISTAR">Movistar</asp:ListItem>
                                                                            <asp:ListItem Value="CLARO">Claro</asp:ListItem>
                                                                            <asp:ListItem Value="ENTEL">Entel</asp:ListItem>
                                                                            <asp:ListItem Value="BITEL">Bitel</asp:ListItem>
                                                                            <asp:ListItem Value="INKACEL">Inkacel</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div> 
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <asp:button ID="btnSubmit" Text="Guardar" runat="server" UseSubmitBehavior="false" CssClass="d-none" />
                    <asp:button ID="btnSubmitAndContinue" Text="Guardar y Continuar" runat="server" UseSubmitBehavior="false" CssClass="d-none" />
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
                                    <div id="respuestaPostback" runat="server" class="alert" data-ispostback="false" data-rpta="" data-msg="" data-control=""></div>
                                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="d-none" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel ID="udpMensajeServidorFooter" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <button type="button" id="btnMensajeAceptarNuevaPersona" runat="server" class="btn btn-primary" data-dismiss="modal">Registrar</button>
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
        <script src="../../assets/smart-wizard/js/jquery.smartWizard.js"></script>
        <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>
        <script src="../../assets/bootstrap-select-1.13.1/js/i18n/defaults-es_ES.js"></script>
        <script src="../../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
        <script src="../../assets/jquery-validation/jquery.validate.min.js"></script>
        <script src="../../assets/jquery-validation/localization/messages_es.min.js"></script>
        <script src="../../assets/iframeresizer/iframeResizer.contentWindow.min.js"></script>
        <!-- Scripts propios -->
        <script src="js/funciones.js"></script>
        <script src="js/actualizarDatosPostulante.js?340"></script>
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
                    // Manejar el error
                }
            });

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (sender, args) {
                var updatedPanels = args.get_panelsUpdated();

                var udpFormUpdated = false
                for (var i = 0; i < updatedPanels.length; i++) {
                    var udpPanelId = updatedPanels[i].id;
                    switch (udpPanelId) {
                        case 'udpEmail':
                            VerificarEmail();
                            break;
                    }
                }
            });

            Sys.Application.add_load(function() {
                var elem = document.getElementById(controlId);

                InicializarControles();

                switch (controlId){
                    case 'lnkObtenerDatos':
                    case 'lnkObtenerDatosPorApellidos':
                        DatosBuscados();
                        VerificarDeudas();
                        break;
                    case 'cmbModalidadIngreso':
                        InitDtpFechaPrimeraMatricula();
                        break;
                    case 'btnSubmit':
                        SubmitPostBack();
                        break;
                    case 'btnSubmitAndContinue':
                        ContinuarStep();
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