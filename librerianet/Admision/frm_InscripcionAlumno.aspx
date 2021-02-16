<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frm_InscripcionAlumno.aspx.vb" Inherits="frm_InscripcionAlumno" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>.:: Inscripción completa ::.</title>

    <%-- Estilos Externos --%>
    <link rel="stylesheet" href="../assets/css/bootstrap-4.1/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/css/bootstrap-select-1.13.1/bootstrap-select.min.css">
    <link rel="stylesheet" href="../assets/css/bootstrap-datepicker/bootstrap-datepicker.min.css">
    <link rel="stylesheet" href="../assets/css/toastr/toastr.min.css">
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,500,700" rel="stylesheet">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" 
        integrity="sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf" crossorigin="anonymous">

    <%-- Estilos Propios --%>
    <link rel="stylesheet" href="css/inscripcionAlumno.css?x=110">
</head>
<body>
    <form id="frmInscripcionAlumno" runat="server">
        <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>
        <input type="hidden" name="hddPathFicha" id="hddPathFicha" runat="server">
        <div class="container-fluid">
            <nav class="nav nav-pills nav-justified" id="pasos-tabs" role="tablist">
                <a class="nav-item nav-link" role="tab" data-toggle="tab" href="#modalidad-carrera">
                    <div class="d-flex flex-sm-row flex-column flex-wrap flex-grow-1 justify-content-around align-items-center">
                        <span class="numero">1</span>
                        <div class="scope-texto">
                            <span class="texto">Modalidad y <br> Carrera Profesional</span>
                        </div>
                    </div>
                </a>
                <a class="nav-item nav-link disabled" role="tab" data-toggle="tab" href="#datos-personales">
                    <div class="d-flex flex-sm-row flex-column flex-wrap flex-grow-1 justify-content-around align-items-center">
                        <span class="numero">2</span>
                        <div class="scope-texto">
                            <span class="texto">Datos Personales</span>
                        </div>
                    </div>
                </a>
                <a class="nav-item nav-link disabled" role="tab" data-toggle="tab" href="#datos-academicos">
                    <div class="d-flex flex-sm-row flex-column flex-wrap flex-grow-1 justify-content-around align-items-center">
                        <span class="numero">3</span>
                        <div class="scope-texto">
                            <span class="texto">Datos Académicos</span>
                        </div>
                    </div>
                </a>
                <a class="nav-item nav-link disabled" role="tab" data-toggle="tab" href="#datos-padres">
                    <div class="d-flex flex-sm-row flex-column flex-wrap flex-grow-1 justify-content-around align-items-center">
                        <span class="numero">4</span>
                        <div class="scope-texto">
                            <span class="texto">Datos de Padres</span>
                        </div>
                    </div>
                </a>
            </nav>
            <div class="tab-content" id="contenido-tabs">
                <div class="tab-pane fade show active" id="modalidad-carrera" role="tabpanel" aria-labelledby="modalidad-carrera-tab">
                    <div class="card parent-card">
                        <div class="card-header title-header">Cuéntanos, ¿Qué te gustaría estudiar?</div>
                        <div class="card-body">
                            <div class="form-group row">
                                <div class="col-lg-4 offset-lg-4 col-md-4 offset-md-4 col-sm-6 offset-sm-3 col-xs-12">
                                    <asp:UpdatePanel ID="udpCarreraProfesional" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbCarreraProfesional" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-live-search="true" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="udpModalidades" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="container-modalidades" id="divContainerModalidades" runat="server">
                                    <div class="card-header title-header">Elije tu modalidad de ingreso</div>
                                    <div class="card-body">
                                        <asp:LinkButton id="btnModExamenAdmision" runat="server" CssClass="modalidad" OnClientClick="return ValidarPaso1('btnModExamenAdmision');">
                                            <asp:PlaceHolder runat="server">
                                                <img src="img/modalidad.png" alt=""/>
                                                <span class="texto" id="spnModExamenAdmision" runat="server">EXAMEN DE ADMISIÓN</span>
                                            </asp:PlaceHolder>
                                        </asp:LinkButton>
                                        <asp:LinkButton id="btnModTestDahc" runat="server" CssClass="modalidad" OnClientClick="return ValidarPaso1('btnModTestDahc');">
                                            <asp:PlaceHolder runat="server">
                                                <img src="img/modalidad.png" alt=""/>
                                                <span class="texto" id="spnModTestDahc" runat="server">TEST DAHC</span>
                                            </asp:PlaceHolder>
                                        </asp:LinkButton>
                                        <asp:LinkButton id="btnModEscuelaPreUsat" runat="server" CssClass="modalidad" OnClientClick="return ValidarPaso1('btnModEscuelaPreUsat');">
                                            <asp:PlaceHolder runat="server">
                                                <img src="img/modalidad.png" alt=""/>
                                                <span class="texto" id="spnModEscuelaPreUsat" runat="server">ESCUELA PRE USAT</span>
                                            </asp:PlaceHolder>
                                        </asp:LinkButton>
                                        <asp:LinkButton id="btnModPrimerosPuestos" runat="server" CssClass="modalidad" OnClientClick="return ValidarPaso1('btnModPrimerosPuestos');">
                                            <asp:PlaceHolder runat="server">
                                                <img src="img/modalidad.png" alt=""/>
                                                <div class="texto" id="spnModPrimerosPuestos" runat="server">PRIMEROS PUESTOS</div>
                                            </asp:PlaceHolder>
                                        </asp:LinkButton>
                                        <asp:LinkButton id="btnModTalentoMedicina" runat="server" CssClass="modalidad" OnClientClick="return ValidarPaso1('btnModTalentoMedicina');">                                        
                                            <asp:PlaceHolder runat="server">
                                                <img src="img/modalidad.png" alt=""/>
                                                <div class="texto" id="spnModTalentoMedicina" runat="server">TALENTO MEDICINA</div>
                                            </asp:PlaceHolder>
                                        </asp:LinkButton>
                                        <div class="alert alert-danger" id="alertProcesoConcluido" runat="server" Visible="False">
                                            El proceso de inscripción ha concluído
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="tab-pane fade" id="datos-personales" role="tabpanel" aria-labelledby="datos-personales-tab">
                    <span id="tituloDatosPersonales">Cuéntanos sobre ti</span>
                    <div class="card parent-card">
                        <div class="card-header title-header">Queremos conocerte</div>
                        <div class="card-body d-flex flex-wrap justify-content-between">
                            <div class="card groupbox tipo-documento">
                                <div class="card-header header-groupbox">TIPO DE DOCUMENTO</div>
                                <div class="card-body">
                                    <asp:DropDownList ID="cmbTipoDocIdentidad" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    <asp:UpdatePanel ID="udpNroDocIdentidad" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtNroDocIdentidad" runat="server" CssClass="form-control form-control-sm" placeholder="N° DOCUMENTO" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="card groupbox nacionalidad">
                                <div class="card-header header-groupbox">NACIONALIDAD</div>
                                <div class="card-body">
                                    <div class="custom-control custom-radio">
                                        <input type="radio" id="rbtNacionalidadPeruana" runat="server" name="rbtNacionalidad" class="custom-control-input">
                                        <label class="custom-control-label" for="rbtNacionalidadPeruana">PERUANA</label>
                                        <asp:UpdatePanel ID="udpBtnFakeNacionalidad" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:Button ID="btnFakeNacionalidadPeruana" runat="server" AutoPostBack="true" CssClass="cancel" />
                                                <asp:Button ID="btnFakeNacionalidadExtranjera" runat="server" AutoPostBack="true" CssClass="cancel" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <asp:UpdatePanel ID="udpPaisNacimiento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbPaisNacimiento" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-live-search="true">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="card groupbox sexo">
                                <div class="card-header header-groupbox">SEXO</div>
                                <div class="card-body">
                                    <div class="custom-control custom-radio">
                                        <input type="radio" id="rbtSexoMasculino" runat="server" name="rbtSexo" class="custom-control-input" checked>
                                        <label class="custom-control-label" for="rbtSexoMasculino">M</label>
                                    </div>
                                    <div class="custom-control custom-radio">
                                        <input type="radio" id="rbtSexoFemenino" runat="server" name="rbtSexo" class="custom-control-input">
                                        <label class="custom-control-label" for="rbtSexoFemenino">F</label>
                                    </div>
                                </div>
                            </div>
                            <div class="card groupbox fecha-nacimiento">
                                <div class="card-header header-groupbox">FECHA NACIMIENTO</div>
                                <div class="card-body">
                                    <asp:TextBox ID="dtpFecNacimiento" runat="server" CssClass="form-control form-control-sm" placeholder="Fec. Nacimiento" />
                                </div>
                            </div>
                            <br>
                            <div class="card groupbox nombre-completo">
                                <div class="card-body">
                                    <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="NOMBRES" />
                                    <asp:TextBox ID="txtApellidoPaterno" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="APELLIDO PATERNO" />
                                    <asp:TextBox ID="txtApellidoMaterno" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="APELLIDO MATERNO" />
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="udpContainerNacimiento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="container-nacimiento" id="divContainerNacimiento" runat="server">
                                    <div class="card-header title-header">¿Dónde naciste?</div>
                                    <div class="card-body d-flex flex-wrap justify-content-between">
                                        <div class="card groupbox lugar-nacimiento">
                                            <div class="card-body">
                                                <asp:UpdatePanel ID="udpDepNacimiento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="cmbDepNacimiento" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-size="6"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="udpProNacimiento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="cmbProNacimiento" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-size="6"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="udpDisNacimiento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="cmbDisNacimiento" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-size="6"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="card-header title-header">Actualmente ¿Dónde vives?</div>
                        <div class="card-body d-flex flex-wrap justify-content-between">
                            <div class="card groupbox direccion-actual">
                                <div class="card-body">
                                    <asp:TextBox ID="txtDireccionActual" runat="server" CssClass="form-control form-control-sm" placeholder="DIRECCIÓN" />
                                    <asp:UpdatePanel ID="udpDepActual" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbDepActual" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-size="6"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="udpProActual" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbProActual" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-size="6"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="udpDisActual" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbDisActual" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-size="6"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="card-header title-header">Queremos estar en contacto contigo</div>
                        <div class="card-body d-flex flex-wrap justify-content-between">
                            <div class="card groupbox contacto">
                                <div class="card-body">
                                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="TELÉFONO CASA" />
                                    <asp:UpdatePanel ID="udpCelular" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="CELULAR" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="udpEmail" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtEmail" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" placeholder="CORREO ELECTRÓNICO" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="udpVerificacionEmail" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:HiddenField ID="hddEmailCoincidente" runat="server" Value="0" />
                                            <asp:HiddenField ID="hddEmailVerificado" runat="server" Value="0" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:TextBox ID="txtFacebook" runat="server" CssClass="form-control form-control-sm" placeholder="PERFIL DE FACEBOOK (www.facebook.com/tuperfil)" />
                                </div>
                            </div>
                        </div>
                        <div class="card-header title-header">¿Tienes alguna discapacidad?</div>
                        <div class="card-body d-flex flex-wrap justify-content-center">
                            <button type="button" class="btn pregunta-discapacidad" data-discapacidad="true">
                                <span class="texto">SI</span>
                            </button>
                            <button type="button" class="btn pregunta-discapacidad active" data-discapacidad="false">
                                <span class="texto">NO</span>
                            </button>
                        </div>
                        <div class="container-discapacidad d-none" id="divContainerDiscapacidad">
                            <div class="card groupbox discapacidad">
                                <div class="card-body">
                                    <label class="pregunta">¿QUÉ TIPO DE DISCAPACIDAD?</label>
                                    <asp:ListBox ID="cmbDiscapacidad" runat="server" SelectionMode="Multiple" 
                                        CssClass="form-control form-control-sm selectpicker">
                                        <asp:ListItem Value="F">FÍSICA</asp:ListItem>
                                        <asp:ListItem Value="S">SENSORIAL</asp:ListItem>
                                        <asp:ListItem Value="A">AUDITIVA</asp:ListItem>
                                        <asp:ListItem Value="V">VISUAL</asp:ListItem>
                                        <asp:ListItem Value="I">INTELECTUAL</asp:ListItem>
                                    </asp:ListBox>
                                    <asp:TextBox ID="txtOtraDiscapacidad" runat="server" CssClass="form-control form-control-sm" placeholder="OTRA DISCAPACIDAD" />
                                </div>
                            </div>
                        </div>
                        <div class="container-nav">
                            <span id="spLoadingValidacionEmail"></span>
                            <ul id="ulValidacionEmail" class="error-validation-email">
                            </ul>
                            <ul class="error-validation error-datos-personales"></ul>
                            <div class="botones">
                                <button type="button" class="btn" id="btnDatosPersonalesPrev">
                                    <span class="texto"><< ATRAS</span>
                                </button>
                                <button type="button" class="btn" id="btnDatosPersonalesNext">
                                    <span class="texto">SIGUIENTE >></span>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="datos-academicos" role="tabpanel" aria-labelledby="datos-academicos-tab">
                    <div class="card parent-card">
                        <div class="card-header title-header">Queremos saber sobre tus estudios en el cole</div>
                        <div class="card-body">
                            <div class="card groupbox ubicacion-colegio">
                                <div class="card-body">
                                    <asp:UpdatePanel ID="udpDepInstitucionEducativa" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbDepInstitucionEducativa" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-size="6">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="udpInstitucionEducativa" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbInstitucionEducativa" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-size="6" data-live-search="true"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="container-estudiante">
                                <div class="container-condicion">
                                    <div class="card groupbox condicion-estudiante">
                                        <div class="card-body">
                                            <asp:UpdatePanel ID="udpCondicionEstudiante" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="cmbCondicionEstudiante" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="udpDatosCondicion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div class="card groupbox" id="divDatosCondicion" runat="server">
                                            <div class="card-header header-groupbox">AÑO DE EGRESO / MÉRITO</div>
                                            <div class="card-body">
                                                <asp:TextBox ID="txtAnioEgreso" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="AÑO DE EGRESO" />
                                                <asp:DropDownList ID="cmbOrdenMerito" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker">
                                                    <asp:ListItem Value="1">PRIMER PUESTO</asp:ListItem>
                                                    <asp:ListItem Value="2">SEGUNDO PUESTO</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="-1">OTRO</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtOtroMerito" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="OTRO PUESTO" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="container-nav">
                            <ul class="error-validation error-datos-academicos"></ul>
                            <div class="botones">
                                <button type="button" class="btn" id="btnDatosAcademicosPrev">
                                    <span class="texto"><< ATRAS</span>
                                </button>
                                <button type="button" class="btn" id="btnDatosAcademicosNext">
                                    <span class="texto">SIGUIENTE >></span>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="datos-padres" role="tabpanel" aria-labelledby="datos-padres-tab">
                    <div class="card parent-card">
                        <div class="card-header title-header">Queremos saber sobre tu papá</div>
                        <div class="card-body">
                            <div class="card groupbox nombres">
                                <div class="card-body">
                                    <asp:TextBox ID="txtDniPadre" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="DNI" />
                                    <asp:TextBox ID="txtNombresPadre" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="NOMBRES" />
                                    <asp:TextBox ID="txtApellidoPaternoPadre" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="APELLIDO PATERNO" />
                                    <asp:TextBox ID="txtApellidoMaternoPadre" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="APELLIDO MATERNO" />
                                </div>
                            </div>
                            <div class="card groupbox fecha-nacimiento">
                                <div class="card-header header-groupbox">FECHA NACIMIENTO</div>
                                <div class="card-body">
                                    <asp:TextBox ID="dtpFecNacPadre" runat="server" CssClass="form-control form-control-sm" placeholder="FEC. NACIMIENTO" />
                                </div>
                            </div>
                            <div class="card groupbox direccion-actual">
                                <div class="card-body">
                                    <asp:TextBox ID="txtDireccionPadre" runat="server" CssClass="form-control form-control-sm" placeholder="DIRECCIÓN ACTUAL" />
                                    <asp:UpdatePanel ID="udpDepPadre" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbDepPadre" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-size="6"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="udpProPadre" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbProPadre" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-size="6"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="udpDisPadre" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbDisPadre" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-size="6"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="card groupbox datos-contacto">
                                <div class="card-body">
                                    <asp:TextBox ID="txtTelefonoPadre" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="TELÉFONO DE CASA" />
                                    <asp:TextBox ID="txtCelularPadre" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="CELULAR" />
                                    <asp:TextBox ID="txtEmailPadre" runat="server" CssClass="form-control form-control-sm" placeholder="CORREO ELECTRÓNICO" />
                                    <div class="custom-control custom-checkbox">
                                        <input type="checkbox" class="custom-control-input" id="chkRespPagoPadre" runat="server" checked>
                                        <label class="custom-control-label" for="chkRespPagoPadre">RESPONSABLE DE PAGO</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-header title-header">Queremos saber sobre tu mamá</div>
                        <div class="card-body">
                            <div class="card groupbox nombres">
                                <div class="card-body">
                                    <asp:TextBox ID="txtDniMadre" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="DNI" />
                                    <asp:TextBox ID="txtNombresMadre" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="NOMBRES" />
                                    <asp:TextBox ID="txtApellidoPaternoMadre" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="APELLIDO PATERNO" />
                                    <asp:TextBox ID="txtApellidoMaternoMadre" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="APELLIDO MATERNO" />
                                </div>
                            </div>
                            <div class="card groupbox fecha-nacimiento">
                                <div class="card-header header-groupbox">FECHA NACIMIENTO</div>
                                <div class="card-body">
                                    <asp:TextBox ID="dtpFecNacMadre" runat="server" CssClass="form-control form-control-sm" placeholder="FEC. NACIMIENTO" />
                                </div>
                            </div>
                            <div class="card groupbox direccion-actual">
                                <div class="card-body">
                                    <asp:TextBox ID="txtDireccionMadre" runat="server" CssClass="form-control form-control-sm" placeholder="DIRECCIÓN ACTUAL" />
                                    <asp:UpdatePanel ID="udpDepMadre" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbDepMadre" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-size="6"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="udpProMadre" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbProMadre" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-size="6"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="udpDisMadre" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbDisMadre" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-size="6"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="card groupbox datos-contacto">
                                <div class="card-body">
                                    <asp:TextBox ID="txtTelefonoMadre" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="TELÉFONO DE CASA" />
                                    <asp:TextBox ID="txtCelularMadre" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="CELULAR" />
                                    <asp:TextBox ID="txtEmailMadre" runat="server" CssClass="form-control form-control-sm" placeholder="CORREO ELECTRÓNICO" />
                                    <div class="custom-control custom-checkbox">
                                        <input type="checkbox" class="custom-control-input" id="chkRespPagoMadre" runat="server">
                                        <label class="custom-control-label" for="chkRespPagoMadre">RESPONSABLE DE PAGO</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="collapse">
                            <div class="card-header title-header">Queremos saber sobre tu apoderado</div>
                            <div class="card-body">
                                <div class="card groupbox nombres">
                                    <div class="card-body">
                                        <asp:TextBox ID="txtDniApoderado" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="DNI" />
                                        <asp:TextBox ID="txtNombresApoderado" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="NOMBRES" />
                                        <asp:TextBox ID="txtApellidoPaternoApoderado" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="APELLIDO PATERNO" />
                                        <asp:TextBox ID="txtApellidoMaternoApoderado" runat="server" CssClass="form-control form-control-sm only-letters" placeholder="APELLIDO MATERNO" />
                                    </div>
                                </div>
                                <div class="card groupbox fecha-nacimiento">
                                    <div class="card-header header-groupbox">FECHA NACIMIENTO</div>
                                    <div class="card-body">
                                        <asp:TextBox ID="dtpFecNacApoderado" runat="server" CssClass="form-control form-control-sm" placeholder="FEC. NACIMIENTO" />
                                    </div>
                                </div>
                                <div class="card groupbox direccion-actual">
                                    <div class="card-body">
                                        <asp:TextBox ID="txtDireccionApoderado" runat="server" CssClass="form-control form-control-sm" placeholder="DIRECCIÓN ACTUAL" />
                                        <asp:UpdatePanel ID="udpDepApoderado" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cmbDepApoderado" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-size="6"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="udpProApoderado" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cmbProApoderado" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-size="6"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="udpDisApoderado" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cmbDisApoderado" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker" data-size="6"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="card groupbox datos-contacto">
                                    <div class="card-body">
                                        <asp:TextBox ID="txtTelefonoApoderado" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="TELÉFONO DE CASA" />
                                        <asp:TextBox ID="txtCelularApoderado" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="CELULAR" />
                                        <asp:TextBox ID="txtEmailApoderado" runat="server" CssClass="form-control form-control-sm" placeholder="CORREO ELECTRÓNICO" />
                                        <div class="custom-control custom-checkbox">
                                            <input type="checkbox" class="custom-control-input" id="chkRespPagoApoderado" runat="server">
                                            <label class="custom-control-label" for="chkRespPagoApoderado">RESPONSABLE DE PAGO</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="container-nav">
                            <ul class="error-validation error-datos-padres"></ul>
                            <div class="botones">
                                <button type="button" class="btn" id="btnDatosPadresPrev">
                                    <span class="texto"><< ATRAS</span>
                                </button>
                                <button type="button" class="btn" id="btnFinalizarInscripcion">
                                    <span class="texto">FINALIZAR INSCRIPCIÓN >></span>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlConfirmarInscripcion" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1>CONFIRMACIÓN DE INSCRIPCIÓN</h1>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">X</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="instrucciones">
                            <asp:UpdatePanel ID="udpInstrucciones" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="paso" id="divPasoUno" runat="server">
                                        <div id="divNroUno" runat="server" class="numero">1</div>
                                        <div class="contenido">
                                            <p id="pTextoRegular" runat="server">
                                                Para poder rendir la evaluación de admisión, primero debes revisar los requisitos según la modalidad por la que postulas en 
                                                <a href="http://www.tuproyectodevida.pe/modalidades/" target="_blank">http://www.tuproyectodevida.pe/modalidades/</a>, 
                                                luego debes acercarte a cualquier agente o banco del BCP o BBVA, para pagar tu derecho de examen, 
                                                también puedes pagar por aplicativo móvil. El código es el Nº de DNI del postulante.
                                            </p>
                                            <p id="pTextoEscuelaPre" runat="server">
                                                Para postular por la modalidad de Escuela Pre USAT, primero debes revisar los requisitos en
                                                <a href="http://www.tuproyectodevida.pe/modalidades/" target="_blank">http://www.tuproyectodevida.pe/modalidades/</a>, 
                                                luego debes acercarte a cualquier agente o banco del BCP o BBVA, para pagar tu derecho de examen, 
                                                también puedes pagar por aplicativo móvil. El código es el Nº de DNI del postulante.</p>
                                            <span id="spnPrecioInscripcion" runat="server"></span> 
                                            <div class="container-imagenes">
                                                <img id="imgBcp" src="img/bcp2.jpg" alt="">
                                                <img id="imgBbva" src="img/bbva2.png" alt="">
                                            </div>
                                            <span runat="server" id="spnMensajeTiempoPagoRegular">
                                                El plazo para realizar el pago por derecho de postulación <u>vence dentro de las 48 horas</u>, 
                                                pero <u>si la inscripción lo haces en el último día y hora de inscripción, el plazo para pagar es de una hora</u>.
                                                Cabe resaltar, que si no realizas el pago respectivo no podrás participar de las capacitaciones y tampoco podrás rendir el examen.
                                            </span>
                                            <span runat="server" id="spnMensajeTiempoPagoEscuelaPre">El plazo para realizar el pago vence en 48 horas, tener en cuenta que asegura el cupo para estudiar la PRE USAT cuando realiza el pago respectivo.</span>
                                            <p id="pTextoAdvertenciaHoraExamen" runat="server"> NOTA: Los postulantes que no se presenten o no lleguen a la hora programada para rendir el Examen de Admisión en el lugar, fecha y hora señalados, 
                                                pierden su derecho como postulante, sin lugar a devolución del importe cancelado.</p>
                                            <br>
                                        </div>
                                    </div>
                                    <div class="paso">
                                        <div id="divNroDos" runat="server" class="numero">2</div>
                                        <div class="contenido">
                                            <p id="prfPasoDos" runat="server"></p>
                                            <br>
                                            <asp:GridView ID="grwRequisitosAdmision" runat="server" AutoGenerateColumns="false" DataKeyNames="Valor" CssClass="table table-sm table-bordered"
                                                GridLines="None" Visible="false">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Nro" />
                                                    <asp:BoundField DataField="Nombre" HeaderText="Requisito" />
                                                </Columns>
                                                <HeaderStyle CssClass="thead-dark alt" />
                                            </asp:GridView>
                                            <!-- <p>PRESENTAR LOS REQUISITOS EN OFICINA DE INFORMES – CAMPUS USAT, HASTA EL DÍA JUEVES PREVIO AL EXAMEN DE ADMISIÓN, HORA LIMITE 4:00 P.M.</p> -->
                                        </div>
                                    </div>
                                    <div class="paso">
                                        <div id="divNroTres" runat="server" class="numero">3</div>
                                        <div class="contenido">
                                            <p id="prfPasoTres" runat="server">Imprime la ficha que se descarga de manera automática al finalizar la inscripción y hazlo firmar por tu apoderado, también lo debes de adjuntas junto con los requisitos.</p>
                                        </div>
                                    </div>
                                    <div class="terminos-condiciones">
                                        <h1>CONFIRMACIÓN DE INSCRIPCIÓN</h1>
                                        <div class="custom-control custom-checkbox">
                                            <input type="checkbox" class="custom-control-input" id="chkCondicion1">
                                            <label class="custom-control-label" for="chkCondicion1" id="lblCondicion1EscuelaPre" runat="server">
                                                ACEPTO REALIZAR EL PAGO DE INSCRIPCIÓN A LA PRE USAT Y ADJUNTAR TODOS LOS REQUISITOS SOLICITADOS, 
                                                HASTA 72 HORAS ANTES DE INICIAR ESTUDIOS EN LA PRE USAT.
                                            </label>
                                            <label class="custom-control-label" for="chkCondicion1" id="lblCondicion1Especial" runat="server">
                                                ACEPTO ADJUNTAR TODOS LOS REQUISITOS SOLICITADOS, HASTA 72 HORAS ANTES DE RENDIR LA EVALUACIÓN DE ADMISIÓN.
                                            </label>
                                            <label class="custom-control-label" for="chkCondicion1" id="lblCondicion1Regular" runat="server">
                                                ACEPTO <span id="spnCondicionPago" runat="server">REALIZAR EL PAGO POR DERECHO DE EVALUACIÓN DE ADMISION Y,</span> 
                                                ADJUNTAR TODOS LOS REQUISITOS SOLICITADOS, HASTA 72 HORAS ANTES DE RENDIR LA EVALUACIÓN. 
                                            </label>
                                        </div>
                                        <div class="custom-control custom-checkbox">
                                            <input type="checkbox" class="custom-control-input" id="chkCondicion2">
                                            <label class="custom-control-label" for="chkCondicion2">
                                                ACEPTO BAJO JURAMENTO QUE LOS DATOS PERSONALES Y ACADÉMICOS CONSIGNADOS EN LA FICHA DE INSCRIPCIÓN VIRTUAL SON FIDEDIGNOS, 
                                                CASO CONTRARIO APLICAR LO ESTIPULADO EN EL REGLAMENTO DE ADMISIÓN
                                                <span class="rojo"><a href="https://commondatastorage.googleapis.com/usat/webusat/trasparencia/reglamento_de_admision.pdf?v=1" target="_blank">[VER REGLAMENTO]</a></span>
                                            </label>
                                        </div>
                                        <div class="custom-control custom-checkbox d-none">
                                            <input type="checkbox" class="custom-control-input" id="chkCondicion4">
                                            <label class="custom-control-label" for="chkCondicion4">
                                                ACEPTO HABER LEÍDO LA DIRECTIVA DEL PROCESO DE ADMISIÓN VIRTUAL (<span class="rojo"><a href="https://storage.googleapis.com/usat/tuproyectodevida/admision/DIRECTIVAADMISIONVIRTUAL.pdf" target="_blank">DESCARGAR</a></span>)
                                            </label>
                                        </div>
                                        <div class="custom-control custom-checkbox">
                                            <input type="checkbox" class="custom-control-input" id="chkCondicion3">
                                            <label class="custom-control-label" for="chkCondicion3">
                                                ACEPTO LOS TÉRMINOS Y CONDICIONES SOBRE LA PROTECCIÓN DE DATOS PERSONALES 
                                                <span class="rojo">
                                                    <a data-toggle="collapse" href="#collapseTerminosCondiciones">[VER TÉRMINOS].</a>
                                                </span>
                                            </label>
                                            <div class="collapse" id="collapseTerminosCondiciones">
                                                <h1>Términos y Condiciones</h1>
                                                <p>
                                                    De conformidad con la Ley N° 29733, Ley de Protección de Datos Personales, autorizo a la USAT a utilizar los datos personales proporcionados o que proporcione a futuro, para la gestión administrativa y comercial que realice. 
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
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br>
                            <div class="alert alert-info guia-token">Ingresa el código de confirmación que te llegará a tu celular para confirmar la inscripción.</div>
                            <asp:UpdatePanel ID="udpToken" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="container-token">
                                        <asp:LinkButton id="btnGenerarToken" runat="server" OnClientClick="return ValidarGenerarToken();" text="GENERAR CÓDIGO" />
                                        <asp:TextBox ID="txtToken" runat="server" CssClass="form-control form-control-sm" placeholder="INGRESAR CÓDIGO" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="container-celular-token">
                                <div class="custom-control custom-checkbox">
                                    <input type="checkbox" class="custom-control-input" id="chkCambiarCelular" runat="server">
                                    <label class="custom-control-label" for="chkCambiarCelular">¿NO ES TU NÚMERO? PUEDES MODIFICARLO DESDE AQUÍ:</label>
                                </div>
                                <asp:TextBox ID="txtCelularToken" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="CELULAR" />
                            </div>
                        </div>
                        <div class="container-nav">
                            <ul class="error-validation error-confirmar-inscripcion"></ul>
                            <div class="alert alert-danger" id="msg-paciencia">Al hacer clic en "CONFIRMAR INSCRIPCIÓN" se procesará el formulario. Esta tarea puede tardar varios segundos, por favor espere la culminación del proceso.</div>
                            <asp:UpdatePanel ID="udpConfirmarInscripcion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <button type="button" id="btnVerificarDatos">&lt;&lt;&nbsp;¿DESEAS VERIFICAR TUS DATOS?</button>
                                    <asp:LinkButton id="btnConfirmarInscripcion" runat="server" OnClientClick="return ValidarConfirmarInscripcion();" text="CONFIRMAR INSCRIPCIÓN >>"/>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlMensajesServidor" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <asp:UpdatePanel ID="udpMensajeServidorParametros" runat="server" UpdateMode="Conditional"
                        ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div id="divMdlMenServParametros" runat="server" data-mostrar="false"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="modal-header">
                        <asp:UpdatePanel ID="udpMensajeServidorHeader" runat="server" UpdateMode="Conditional"
                            ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <span id="spnMensajeServidorTitulo" runat="server" class="modal-title">Mensaje de Alerta</span>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">X</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="udpMensajeServidorBody" runat="server" UpdateMode="Conditional"
                            ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div id="divRespuestaPostback" runat="server" class="alert" data-ispostback="false"
                                    data-rpta="" data-msg="" data-control=""></div>
                                <asp:Label ID="lblErrorMessage" runat="server" CssClass="d-none" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <asp:UpdatePanel ID="udpMensajeServidorFooter" runat="server" UpdateMode="Conditional"
                            ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <button type="button" id="btnMensajeCerrar" runat="server" class="btn btn-sm btn-default"
                                    data-dismiss="modal">Cerrar</button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlMensajeConfirmacion" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <asp:UpdatePanel ID="udpMensajeConfirmacionParametros" runat="server" UpdateMode="Conditional"
                        ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div id="divMdlMenConfParametros" runat="server" data-mostrar="false"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="modal-header">
                        <span id="spnMensajeConfirmacionTitulo" runat="server" class="modal-title">Respuesta</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">X</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="udpMensajeConfirmacionBody" runat="server" UpdateMode="Conditional"
                            ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div id="divRespuestaConfirmacion" runat="server" data-ispostback="false"
                                    data-msg=""></div>
                                <asp:Label ID="lblErrorMessageConfirmacion" runat="server" CssClass="d-none" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="container-descargar-ficha">
                            <button type="button" class="btn" id="btnDescargarFicha">
                                <span class="texto">DESCARGAR FICHA</span>
                            </button>
                            <button type="button" class="btn" id="btnEnviarFichaCorreo">
                                <span class="texto">ENVIAR REQUISITOS AQUÍ</span>
                            </button>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-sm btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="udpMensajesToastr" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div id="divMensajesToastr" runat="server" data-mostrar="false"></div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="udpDataFichaInscripcion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div id="divDataFichaInscripcion" runat="server" data-mostrar="false"></div>
            </ContentTemplate>
        </asp:UpdatePanel>
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
    <script src="js/funciones.js?10"></script>
    <script src="js/inscripcionAlumno.js?x=200"></script>

    <script type="text/javascript">
        var elem;
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            elem = args.get_postBackElement();
            controlId = elem.id

            if (elem != undefined && elem.classList.contains('modalidad')) {
                HabilitarDeshabilitarBoton(controlId, true);
            }

            switch (controlId) {
                case 'btnGenerarToken':
                case 'btnValidarToken':
                    HabilitarDeshabilitarBoton(controlId, true);
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
                var udpPanelId = updatedPanels[i].id;
                
                switch(udpPanelId) {
                    case 'udpNroDocIdentidad':
                        RevisarValidacionDocIdentidad()
                        break;

                    case 'udpContainerNacimiento':
                        CargarCombo('cmbPaisNacimiento');
                        CargarCombo('cmbDepNacimiento');
                        CargarCombo('cmbProNacimiento');
                        CargarCombo('cmbDisNacimiento');
                        break;

                    case 'udpDepNacimiento':
                        CargarCombo('cmbDepNacimiento');
                        break;

                    case 'udpProNacimiento':
                        CargarCombo('cmbProNacimiento');
                        break;

                    case 'udpDisNacimiento':
                        CargarCombo('cmbDisNacimiento');
                        break;

                    case 'udpDepActual':
                        CargarCombo('cmbDepActual');
                        break;

                    case 'udpProActual':
                        CargarCombo('cmbProActual');
                        break;

                    case 'udpDisActual':
                        CargarCombo('cmbDisActual');
                        break;

                    case 'udpVerificacionEmail':
                        ValidarEmail();
                        break;

                    case 'udpInstitucionEducativa':
                        CargarCombo('cmbInstitucionEducativa', { 
                            liveSearch: true,
                            size: 6,
                        });
                        break;

                    case 'udpCondicionEstudiante':
                        CargarCombo('cmbCondicionEstudiante');
                        break;
 
                    case 'udpDatosCondicion': 
                        PanelOrdenMeritoCargado();
                        break;

                    case 'udpProPadre':
                        CargarCombo('cmbProPadre');
                        break;

                    case 'udpDisPadre':
                        CargarCombo('cmbDisPadre');
                        break;

                    case 'udpProMadre':
                        CargarCombo('cmbProMadre');
                        break;

                    case 'udpDisMadre':
                        CargarCombo('cmbDisMadre');
                        break;

                    case 'udpProApoderado':
                        CargarCombo('cmbProApoderado');
                        break;

                    case 'udpDisApoderado':
                        CargarCombo('cmbDisApoderado');
                        break;
                }
            }
        });

        Sys.Application.add_load(function () {
            if (elem != undefined && elem.classList.contains('modalidad')) {
                HabilitarDeshabilitarBoton(controlId, false);
                SeleccionarTab('datos-personales');
            }

            switch (controlId) {
                case 'btnFakeNacionalidadPeruana':
                case 'btnFakeNacionalidadExtranjera':
                    ResetearValidacion('datos-personales');
                    break;
                
                case 'cmbDepInstitucionEducativa':
                    ResetearValidacion('datos-academicos');
                    break;

                case 'btnGenerarToken':
                    HabilitarDeshabilitarBoton(controlId, false);
                    RevisarMensajePostbackToastr();
                    DeshabilitarCelularToken();
                    break;
                
                case 'btnValidarToken':
                    RevisarMensajePostbackToastr();
                    break;

                case 'btnConfirmarInscripcion': 
                    RevisarMensajePostbackToastr();
                    RevisarMensajeConfirmacion();
                    RevisarDescargaFichaInscripcion();
                    break;
            }
            
            RevisarMensajePostback();
        });
    </script>
</body>
</html>