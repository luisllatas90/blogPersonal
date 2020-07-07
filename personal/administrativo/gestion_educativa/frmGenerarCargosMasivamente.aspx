<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGenerarCargosMasivamente.aspx.vb" Inherits="frmGenerarCargosMasivamente" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Generación de cargos masivamente ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
    <link rel="stylesheet" href="../../assets/tempusdominus-bootstrap/css/tempusdominus-bootstrap-4.min.css">
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../../assets/toastr-2.1.4-7/toastr.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/generarCargosMasivamente.css?x=1">
</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scr" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpParams" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:HiddenField ID="hddCod" Value="0" runat="server" />
                <asp:HiddenField ID="hddCodD" Value="0" runat="server" />
                <asp:HiddenField ID="hddTipoVista" Value="L" runat="server" />
                <asp:HiddenField ID="hddTipoSeleccionAlumnos" Value="M" runat="server" />
                <asp:HiddenField ID="hddFiltrosAlumno" Value="" runat="server" />
                <asp:HiddenField ID="hddParamsToastr" Value="" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Generación de Cargos<img src="img/loading.gif" id="loadingGif"></h1>
                    <hr>
                </div>
            </div>
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav-lista-tab" data-toggle="tab" href="#nav-lista" role="tab" aria-controls="nav-lista" aria-selected="true">Lista</a>
                    <a class="nav-item nav-link disabled" id="nav-mantenimiento-tab" data-toggle="tab" href="#nav-mantenimiento" role="tab" aria-controls="nav-mantenimiento"
                        aria-selected="false">Mantenimiento</a>
                </div>
            </nav>
            <div class="tab-content" id="nav-tabContent">
                <div class="tab-pane fade show active" id="nav-lista" role="tabpanel" aria-labelledby="nav-lista-tab">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-xl-16 col-lg-20 d-flex">
                                    <label for="txtFiltroDescripcion" class="form-control-sm">Descripción:</label>
                                    <div class="fill">
                                        <asp:TextBox ID="txtFiltroDescripcion" runat="server" CssClass="form-control form-control-sm" placeholder="Descripcion" />
                                    </div>
                                </div>
                                <div class="col-xl-32 col-lg-28 text-right">
                                    <asp:UpdatePanel ID="udpAccionesLista" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-azul">
                                                <i class="fa fa-search"></i>
                                                <span class="text">Listar</span>
                                            </button>
                                            <button type="button" id="btnNuevo" runat="server" class="btn btn-accion btn-verde">
                                                <i class="fa fa-plus-square"></i>
                                                <span class="text">Nuevo</span>
                                            </button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-48">
                                    <asp:UpdatePanel ID="udpGrvProgramacion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div id="divGrvProgramacion" runat="server">
                                                <hr>
                                                <asp:GridView ID="grvProgramacion" runat="server" DataKeyNames="codigo_pcm" AutoGenerateColumns="false" CssClass="table table-sm"
                                                    GridLines="None">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="NRO" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="tipoConfiguracion_pcm" HeaderText="TIPO" />
                                                        <asp:BoundField DataField="descripcion_pcm" HeaderText="DESCRIPCIÓN" />
                                                        <asp:BoundField DataField="tipoEjecucion_pcm" HeaderText="TIPO EJECUCIÓN" />
                                                        <asp:BoundField HeaderText="PROGRAMACIÓN" />
                                                        <asp:BoundField HeaderText="ACCIONES" ItemStyle-HorizontalAlign="Center" />
                                                    </Columns>
                                                    <HeaderStyle CssClass="thead-dark" />
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="nav-mantenimiento" role="tabpanel" aria-labelledby="nav-lista-tab">
                    <div class="card">
                        <div class="card-body">
                            <asp:UpdatePanel ID="udpMantenimiento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-48">
                                            <h1 class="main-title">Registrar/Actualizar Programación<img src="img/loading.gif" id="loadingGif"></h1>
                                            <hr>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xl-20 col-lg-20 d-flex">
                                            <div class="custom-control custom-radio custom-control-inline">
                                                <input type="radio" id="rbtConfiguracionPredefinida" name="rbtTipoConfiguracion" class="custom-control-input" value="P"
                                                    runat="server" checked>
                                                <label class="custom-control-label form-control-sm" for="rbtConfiguracionPredefinida">Configuración Predefinida</label>
                                            </div>
                                            <div class="fill">
                                                <asp:UpdatePanel ID="udpTipoConfiguracion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="cmbTipoConfiguracion" runat="server" AutoPostBack="false"
                                                            CssClass="form-control form-control-sm selectpicker" data-live-search="true">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-xl-20 col-lg-20 d-flex">
                                            <div class="custom-control custom-radio custom-control-inline">
                                                <input type="radio" id="rbtConfiguracionManual" name="rbtTipoConfiguracion" class="custom-control-input" runat="server" value="M">
                                                <label class="custom-control-label form-control-sm" for="rbtConfiguracionManual">Configuración Manual</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xl-12 col-lg-15 d-flex">
                                            <label for="cmbCicloAcademico" class="form-control-sm">Ciclo Académico:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbCicloAcademico" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                    data-live-search="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-xl-16 col-lg-20 d-flex">
                                            <label for="txtDescripcion" class="form-control-sm">Descripcion:</label>
                                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control form-control-sm" placeholder="Descripción" />
                                        </div>
                                    </div>
                                    <hr class="thin">
                                    <asp:UpdatePanel ID="udpProgramacion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div class="row form-group no-gutters no-margin">
                                                <div class="col-xl-16 col-lg-19 d-flex">
                                                    <label for="rbtProgEjecucion" class="form-control-sm">Ejecución:</label>
                                                    <div class="custom-control custom-radio custom-control-inline">
                                                        <input type="radio" id="rbtEjecManual" name="rbtProgEjecucion" class="custom-control-input" runat="server" value="M"
                                                            checked>
                                                        <label class="custom-control-label form-control-sm" for="rbtEjecManual">Manual</label>
                                                    </div>
                                                    <div class="custom-control custom-radio custom-control-inline">
                                                        <input type="radio" id="rbtProgUnaVez" name="rbtProgEjecucion" class="custom-control-input" runat="server" value="U">
                                                        <label class="custom-control-label form-control-sm" for="rbtProgUnaVez">Prog. Única</label>
                                                    </div>
                                                    <div class="custom-control custom-radio custom-control-inline">
                                                        <input type="radio" id="rbtProgPeriodico" name="rbtProgEjecucion" class="custom-control-input" runat="server" value="P">
                                                        <label class="custom-control-label form-control-sm" for="rbtProgPeriodico">Prog. Periódica</label>
                                                    </div>
                                                </div>
                                                <div class="col-xl-8 col-lg-9 d-flex">
                                                    <label for="txtFechaHoraInicioProg" class="form-control-sm">Inicio:</label>
                                                    <div class="input-group date" id="mrkFechaHoraInicioProg" data-target-input="nearest">
                                                        <input type="text" id="txtFechaHoraInicioProg" runat="server" class="form-control datetimepicker-input"
                                                            data-target="#mrkFechaHoraInicioProg" />
                                                        <div class="input-group-append" data-target="#mrkFechaHoraInicioProg" data-toggle="datetimepicker">
                                                            <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xl-8 col-lg-9 d-flex">
                                                    <label for="txtFechaHoraFinProg" class="form-control-sm">Término:</label>
                                                    <div class="input-group date" id="mrkFechaHoraFinProg" data-target-input="nearest">
                                                        <input type="text" id="txtFechaHoraFinProg" runat="server" class="form-control datetimepicker-input"
                                                            data-target="#mrkFechaHoraFinProg" />
                                                        <div class="input-group-append" data-target="#mrkFechaHoraFinProg" data-toggle="datetimepicker">
                                                            <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xl-9 col-lg-11 d-flex">
                                                    <label for="txtMinutosProg" class="form-control-sm">Ejecutar cada:</label>
                                                    <asp:ListBox ID="cmbEjecutarCada" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker">
                                                        <asp:ListItem text="-- SELECCIONE --" value="-1" disabled></asp:ListItem>
                                                        <asp:ListItem text="12 horas" value="720"></asp:ListItem>
                                                        <asp:ListItem text="8 horas" value="480"></asp:ListItem>
                                                        <asp:ListItem text="4 horas" value="240"></asp:ListItem>
                                                        <asp:ListItem text="2 horas" value="120"></asp:ListItem>
                                                        <asp:ListItem text="1 hora" value="60"></asp:ListItem>
                                                        <asp:ListItem text="30 minutos" value="30"></asp:ListItem>
                                                        <asp:ListItem text="20 minutos" value="20"></asp:ListItem>
                                                    </asp:ListBox>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <hr class="thin">
                                    <asp:UpdatePanel ID="udpConfigManual" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div id="divConfigManual" runat="server">
                                                <div class="row">
                                                    <div class="col-xl-48">
                                                        <div class="card">
                                                            <div class="card-header">
                                                                <h5 class="title">Datos del Cargo</h5>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="row form-group no-gutters">
                                                                    <div class="col-xl-18 col-lg-18 d-none">
                                                                        <label for="rbtTipoCargo" class="form-control-sm">Tipo:</label>
                                                                        <div class="fill">
                                                                            <asp:UpdatePanel ID="udpTipoDeuda" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                                                <ContentTemplate>
                                                                                    <div class="custom-control custom-radio custom-control-inline">
                                                                                        <input type="radio" id="rbtTipoConfigurado" name="rbtTipoCargo" value="C"
                                                                                            class="custom-control-input" runat="server" checked>
                                                                                        <label class="custom-control-label form-control-sm"
                                                                                            for="rbtTipoConfigurado">Configurado</label>
                                                                                    </div>
                                                                                    <asp:DropDownList ID="cmbTipoDeuda" runat="server" AutoPostBack="true"
                                                                                        CssClass="form-control form-control-sm selectpicker">
                                                                                        <asp:ListItem text="-- SELECCIONE --" value="-1"></asp:ListItem>
                                                                                        <asp:ListItem text="MATRÍCULA" value="M"></asp:ListItem>
                                                                                        <asp:ListItem text="PENSIONES" value="P"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <div class="custom-control custom-radio custom-control-inline">
                                                                                        <input type="radio" id="rbtTipoManual" name="rbtTipoCargo" value="M"
                                                                                            class="custom-control-input" runat="server">
                                                                                        <label class="custom-control-label form-control-sm" for="rbtTipoManual">Manual</label>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xl-40 col-lg-40">
                                                                        <asp:UpdatePanel ID="udpServicioCentroCosto" runat="server" UpdateMode="Conditional"
                                                                            ChildrenAsTriggers="false">
                                                                            <ContentTemplate>
                                                                                <div id="divServicioCentroCosto" runat="server">
                                                                                    <div class="row">
                                                                                        <div class="col-lg-24 d-flex">
                                                                                            <label for="cmbServicioConcepto" class="form-control-sm">Concepto:</label>
                                                                                            <div class="fill">
                                                                                                <asp:UpdatePanel ID="udpServicioConcepto" runat="server" UpdateMode="Conditional"
                                                                                                    ChildrenAsTriggers="false">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:DropDownList ID="cmbServicioConcepto" runat="server"
                                                                                                            AutoPostBack="true" CssClass="form-control form-control-sm selectpicker"
                                                                                                            data-live-search="true">
                                                                                                        </asp:DropDownList>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-lg-24 d-flex">
                                                                                            <label for="cmbCentroCosto" class="form-control-sm">Centro de Costo:</label>
                                                                                            <div class="fill">
                                                                                                <asp:UpdatePanel ID="udpCentroCosto" runat="server" UpdateMode="Conditional"
                                                                                                    ChildrenAsTriggers="false">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:DropDownList ID="cmbCentroCosto" runat="server" AutoPostBack="false"
                                                                                                            CssClass="form-control form-control-sm selectpicker"
                                                                                                            data-live-search="true">
                                                                                                        </asp:DropDownList>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-xl-7 col-lg-9 d-flex">
                                                                        <label for="txtImporte" class="form-control-sm">Importe:</label>
                                                                        <asp:TextBox ID="txtImporte" runat="server" CssClass="form-control form-control-sm" placeholder="S/" />
                                                                    </div>
                                                                    <div class="col-xl-10 col-lg-13 d-flex">
                                                                        <label for="txtFechaVencimiento" class="form-control-sm">Fecha de Vencimiento:</label>
                                                                        <div class="input-group date" id="mrkFechaVencimiento" data-target-input="nearest">
                                                                            <input type="text" id="txtFechaVencimiento" runat="server" class="form-control datetimepicker-input"
                                                                                data-target="#mrkFechaVencimiento" />
                                                                            <div class="input-group-append" data-target="#mrkFechaVencimiento" data-toggle="datetimepicker">
                                                                                <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xl-31 col-lg-26 d-flex">
                                                                        <label for="txtObservacion" class="form-control-sm">Observación:</label>
                                                                        <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control form-control-sm"
                                                                            placeholder="Observación" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr class="thin">
                                                <div class="row">
                                                    <div class="col-sm-48">
                                                        <div class="card" id="cardAlumnos">
                                                            <div class="card-header">
                                                                <h5 class="title">Alumnos</h5>
                                                            </div>
                                                            <div class="card-body">
                                                                <nav>
                                                                    <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                                                        <a class="nav-item nav-link" id="nav-manual-tab" data-toggle="tab" href="#nav-manual" role="tab"
                                                                            aria-controls="nav-manual" aria-selected="true">Filtrar Alumnos</a>
                                                                        <a class="nav-item nav-link" id="nav-importar-tab" data-toggle="tab" href="#nav-importar" role="tab"
                                                                            aria-controls="nav-importar" aria-selected="false">Importar Alumnos</a>
                                                                    </div>
                                                                </nav>
                                                                <div class="tab-content" id="nav-tabContent">
                                                                    <div class="tab-pane fade" id="nav-manual" role="tabpanel" aria-labelledby="nav-manual-tab">
                                                                        <div class="card">
                                                                            <div class="card-body">
                                                                                <iframe id="ifrmFiltrosAlumno" runat="server" src="" width="100%" scrolling="no"
                                                                                    frameborder="0"></iframe>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="tab-pane fade" id="nav-importar" role="tabpanel" aria-labelledby="nav-importar-tab">
                                                                        <div class="row">
                                                                            <div class="col-sm-48">
                                                                                <div class="card">
                                                                                    <div class="card-body">
                                                                                        <div class="row">
                                                                                            <label for="flCargarAlumnos" class="col-form-label form-control-sm col-lg-6">Seleccionar
                                                                                                Archivo:</label>
                                                                                            <div class="col-lg-20">
                                                                                                <div class="custom-file">
                                                                                                    <asp:FileUpload ID="fluArchivoAlumnos" runat="server"
                                                                                                        class="custom-file-input" />
                                                                                                    <label class="custom-file-label" for="cargar-alumnos">Importar Alumnos</label>
                                                                                                    <asp:UpdatePanel ID="udpCodUnivAlumnos" runat="server" UpdateMode="Conditional"
                                                                                                        ChildrenAsTriggers="false">
                                                                                                        <ContentTemplate>
                                                                                                            <input type="hidden" name="hddCodUnivAlumnos" id="hddCodUnivAlumnos"
                                                                                                                runat="server">
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-lg-20 offset-lg-6">
                                                                                                <a href="#" id="lnkDescargarPlantilla">Descargar Plantilla</a>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
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
                                    <div class="row form-group">
                                        <div class="col-lg-10 d-flext">
                                            <asp:UpdatePanel ID="udpMostrarAlumnos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <button type="button" id="btnFakeMostrarAlumnos" class="btn btn-accion btn-celeste">
                                                        <i class="fa fa-search"></i>
                                                        <span class="text">Alumnos Encontrados</span>
                                                    </button>
                                                    <button type="button" id="btnMostrarAlumnos" runat="server" class="btn btn-accion btn-naranja d-none">
                                                        <i class="fa fa-search"></i>
                                                        <span class="text">Alumnos Encontrados</span>
                                                    </button>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-xl-38 text-right">
                                            <asp:UpdatePanel ID="udpAccionesMantenimiento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <button type="button" id="btnCancelar" runat="server" class="btn btn-accion btn-light">
                                                        <i class="fa fa-ban"></i>
                                                        <span class="text">Cancelar</span>
                                                    </button>
                                                    <button type="button" id="btnFakeGuardar" class="btn btn-accion btn-azul">
                                                        <i class="fa fa-save"></i>
                                                        <span class="text">Guardar</span>
                                                    </button>
                                                    <button type="button" id="btnGuardar" runat="server" class="btn btn-accion btn-naranja d-none">
                                                        <i class="fa fa-save"></i>
                                                        <span class="text">Guardar</span>
                                                    </button>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlAlumnos" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpAlumnos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div id="divAlumnoParametros" runat="server" data-mostrar="false"></div>
                            <div class="modal-header">
                                <span id="spnAlumnoTitulo" runat="server" class="modal-title">Respuesta del Servidor</span>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-48 d-flex flex-action">
                                        <h5 class="body-title" id="hCantidadAlumnos" runat="server"></h5>
                                        <button type="button" id="btnExportar" class="btn btn-accion btn-verde">
                                            <i class="fa fa-file-excel"></i>
                                            <span class="text">Exportar</span>
                                        </button>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-48">
                                        <span id="spnAdvertencia">Tener en cuenta que la lista de alumnos puede variar en el tiempo</span>
                                    </div>
                                </div>
                                <hr>
                                <asp:GridView ID="grvAlumnos" runat="server" AutoGenerateColumns="false" DataKeyNames="codigo_alu" CssClass="table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField HeaderText="Nro" />
                                        <asp:BoundField DataField="apellidoPat_Alu" HeaderText="Ape. Paterno" />
                                        <asp:BoundField DataField="apellidoMat_Alu" HeaderText="Ape. Materno" />
                                        <asp:BoundField DataField="nombres_Alu" HeaderText="Descripcions" />
                                        <asp:BoundField DataField="nroDocIdent_Alu" HeaderText="Doc. Ident." />
                                        <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Código" />
                                    </Columns>
                                    <HeaderStyle CssClass="thead-dark" />
                                </asp:GridView>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
        <div id="mdlMensajeClienteConfirmar" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span class="modal-title">Confirmar Operación</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div id="divMensajeConfirmar"></div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" id="btnConfirmarCancelar" data-dismiss="modal">Cancelar</button>
                        <button type="button" class="btn btn-primary" id="btnConfirmarContinuar">Continuar</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- Scripts externos -->
    <script src="../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.min.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/i18n/defaults-es_ES.min.js"></script>
    <script src="../../assets/moment/moment-with-locales.js"></script>
    <script src="../../assets/tempusdominus-bootstrap/js/tempusdominus-bootstrap-4.min.js"></script>
    <script src="../../assets/iframeresizer/iframeResizer.min.js"></script>
    <script src="../../assets/toastr-2.1.4-7/toastr.min.js"></script>
    <!-- <script src="../../assets/papaparse/papaparse.min.js"></script> -->
    <script src="../../assets/sheetjs/xlsx.full.min.js"></script>
    <script src="../../assets/filesaver/FileSaver.min.js"></script>
    <!-- Scripts propios -->
    <script src="js/funciones.js"></script>
    <script src="js/generarCargosMasivamente.js?x=1"></script>

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