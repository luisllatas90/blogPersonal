<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistroAsistencia.aspx.vb" Inherits="TomarEvaluacion_frmRegistroAsistencia" %>

<!DOCTYPE html>
<html lang="en">

<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Registro de Asistencia ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-4.1/css-custom/bootstrap.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-select-1.13.1/css/bootstrap-select.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/fontawesome-5.2/css/all.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-datepicker/css/bootstrap-datepicker.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/toastr-2.1.4-7/toastr.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/datatables/datatables.min.css>
    <!-- Estilos propios -->
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_CSS %>/style.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_CSS %>/registroAsistencia.css>
</head>

<body>
    <form id="form" runat="server">
        <asp:ScriptManager ID="scm" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpParams" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:HiddenField ID="hddCod" Value="" runat="server" />
                <asp:HiddenField ID="hddTipoVista" Value="" runat="server" />
                <asp:HiddenField ID="hddParamsToastr" Value="" runat="server" />
                <!-- Parámetros para mensajes desde el servidor -->
                <asp:HiddenField ID="hddMenServMostrar" Value="false" runat="server" />
                <asp:HiddenField ID="hddMenServRpta" Value="1" runat="server" />
                <asp:HiddenField ID="hddMenServTitulo" Value="" runat="server" />
                <asp:HiddenField ID="hddMenServMensaje" Value="" runat="server" />
                <!-- Acciones adicionales -->
                <asp:HiddenField ID="hddTipoModalAccion" Value="" runat="server" />
                <asp:HiddenField ID="hddMostrarModalAccion" Value="false" runat="server" />
                <asp:HiddenField ID="hddOcultarModalAccion" Value="false" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Registro de Asistencia<img src=<%=ClsGlobales.PATH_IMG %>/loading.gif id="loadingGif"></h1>
                    <hr>
                </div>
            </div>
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav-lista-tab" data-toggle="tab" href="#nav-lista" role="tab" aria-controls="nav-lista" aria-selected="true">Lista</a>
                    <a class="nav-item nav-link" id="nav-mantenimiento-tab" data-toggle="tab" href="#nav-mantenimiento" role="tab" aria-controls="nav-mantenimiento"
                        aria-selected="false">Mantenimiento</a>
                </div>
            </nav>
            <div class="tab-content" id="nav-tabContent">
                <div class="tab-pane fade show active" id="nav-lista" role="tabpanel" aria-labelledby="nav-lista-tab">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="title">Filtros</h5>
                        </div>
                        <div class="card-body">
                            <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="row form-group">
                                        <div class="col-sm-14 d-flex">
                                            <label for="cmbFiltroCicloAcademico" class="form-control-sm">Semestre Académico:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbFiltroCicloAcademico" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm selectpicker"
                                                    data-live-search="true">
                                                    <asp:ListItem Text="-- SELECCIONE --" Value="1" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-22 d-flex">
                                            <label for="cmbFiltroCentroCosto" class="form-control-sm">Centro Costo:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbFiltroCentroCosto" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm selectpicker"
                                                    data-live-search="true">
                                                    <asp:ListItem Text="-- SELECCIONE --" Value="1" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 d-flex justify-content-between">
                                            <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-light">
                                                <i class="fa fa-search"></i>
                                                <span class="text">Listar</span>
                                            </button>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="row">
                                <div class="col-sm-48">
                                    <asp:UpdatePanel ID="udpGrvList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div id="divGrvList" runat="server">
                                                <hr>
                                                <div class="row">
                                                    <div class="col-sm-48 d-flex mb-2">
                                                        <div class="custom-control custom-checkbox custom-control-inline ml-2">
                                                            <input type="checkbox" id="chkActivarCierre" name="chkActivarCierre" class="custom-control-input">
                                                            <label class="custom-control-label form-control-sm" for="chkActivarCierre">Activar Cierre</label>
                                                        </div>
                                                        <button type="button" id="btnCerrarAsistencia" runat="server" class="btn btn-accion btn-success invisible">
                                                            <i class="fa fa-lock"></i>
                                                            <span class="text">Cerrar Asistencias</span>
                                                        </button>
                                                    </div>
                                                </div>
                                                <asp:GridView ID="grvList" runat="server" AutoGenerateColumns="false" CssClass="table table-sm" GridLines="None">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <div class="custom-control custom-checkbox no-label d-none" id="divChkHeader">
                                                                    <input type="checkbox" runat="server" id="chkHeader" class="custom-control-input">
                                                                    <asp:Label runat="server" ID="lblHeader" AssociatedControlId="chkHeader" CssClass="custom-control-label" />
                                                                </div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hddCodigoGru" Value='<%# Eval("codigo_gru") %>' runat="server" />
                                                                <asp:HiddenField ID="hddAsignado" Value='<%# Eval("asignado") %>' runat="server" />
                                                                <asp:HiddenField ID="hddAsistencia" Value='<%# Eval("total_asistencia") %>' runat="server" />
                                                                <asp:HiddenField ID="hddCerrado" Value='<%# Eval("cerrado") %>' runat="server" />
                                                                <div class="custom-control custom-checkbox no-label custom-control-inline d-none" id="divActivarCierre" runat="server">
                                                                    <input type="checkbox" id="chkSeleccionarGrupo" name="chkSeleccionarGrupo" class="custom-control-input" runat="server">
                                                                    <asp:Label runat="server" ID="lblHeader" AssociatedControlId="chkSeleccionarGrupo"
                                                                        CssClass="custom-control-label form-control-sm" />
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="NRO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" />
                                                        <asp:BoundField DataField="codigo" HeaderText="CÓDIGO GRUPO" ItemStyle-HorizontalAlign=Center />
                                                        <asp:BoundField DataField="nombre" HeaderText="NOMBRE GRUPO" />
                                                        <asp:BoundField DataField="nombre_amb" HeaderText="AMBIENTE" ItemStyle-HorizontalAlign=Center />
                                                        <asp:BoundField DataField="cap_dis" HeaderText="ASIGNADOS / CAPACIDAD" ItemStyle-HorizontalAlign=Center />
                                                        <asp:BoundField DataField="total_asistencia" HeaderText="ASISTENCIAS MARCADAS" ItemStyle-HorizontalAlign=Center />
                                                        <asp:BoundField DataField="estado_cierre" HeaderText="ESTADO" ItemStyle-HorizontalAlign=Center />
                                                        <asp:TemplateField HeaderText="ASISTENCIA" ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Center>
                                                            <ItemTemplate>
                                                                <asp:LinkButton id="btnAsistencia" runat="server" CommandName="Asistencia" CommandArgument=<%# Eval("codigo_gru") %>
                                                                    class="btn btn-sm btn-accion btn-primary">
                                                                    <i class="fa fa-tasks"></i>
                                                                    <span class="text">Tomar Asistencia</span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="INCIDENTES" ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Center>
                                                            <ItemTemplate>
                                                                <div class="d-flex controles justify-content-center">
                                                                    <asp:LinkButton id="btnIncidencia" runat="server" CommandName="Incidencia" CommandArgument=<%# Eval("codigo_gru") %>
                                                                        class="btn btn-sm btn-accion btn-danger">
                                                                        <i class="fa fa-exclamation-circle"></i>
                                                                        <span class="text">Registrar</span>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton id="btnVisualizarIncidencias" runat="server" CommandName="VisualizarIncidencias"
                                                                        CommandArgument=<%# Eval("codigo_gru") %> class="btn btn-sm btn-accion btn-secondary ml-1">
                                                                        <i class="fa fa-search"></i>
                                                                        <span class="text">Mostrar</span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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
                        <div class="card-header">
                            <h5 class="title">Registro / Edición</h5>
                        </div>
                        <div class="card-body">
                            <asp:UpdatePanel ID="udpMantenimiento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-48">
                                            <h1 class="title">Lista de Postulantes<span id="spnGrupoCerrado" runat="server" class="badge badge-danger float-right grupo-cerrado">Este grupo ya se
                                                    encuentra cerrado</span></h1>
                                            <hr>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-48">
                                            <asp:GridView ID="grvAsistencia" runat="server" AutoGenerateColumns="false" CssClass="table table-sm" GridLines="None">
                                                <Columns>
                                                    <asp:BoundField HeaderText="NRO" ItemStyle-HorizontalAlign=Center />
                                                    <asp:BoundField DataField="codigoUniver_Alu" HeaderText="CÓDIGO" ItemStyle-HorizontalAlign=Center />
                                                    <asp:BoundField DataField="nroDocIdent_Alu" HeaderText="DNI" ItemStyle-HorizontalAlign=Center />
                                                    <asp:TemplateField HeaderText="APELLIDOS">
                                                        <ItemTemplate>
                                                            <span><%# Eval("apellidoPat_Alu") & " " & Eval("apellidoMat_Alu") %></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="nombres_Alu" HeaderText="NOMBRES" />
                                                    <asp:BoundField DataField="nombre_cpf" HeaderText="PROGRAMA DE ESTUDIOS" ItemStyle-HorizontalAlign=Center />
                                                    <asp:BoundField DataField="CargoTotal" HeaderText="CARGO" ItemStyle-HorizontalAlign=Center />
                                                    <asp:BoundField DataField="AbonoTotal" HeaderText="ABONO" ItemStyle-HorizontalAlign=Center />
                                                    <asp:BoundField DataField="SaldoTotal" HeaderText="SALDO" ItemStyle-HorizontalAlign=Center />
                                                    <asp:TemplateField HeaderText="ASISTENCIA" ItemStyle-HorizontalAlign=Center>
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hddCodAlu" runat="server" Value='<%# Eval("codigo_alu") %>' />
                                                            <asp:HiddenField ID="hddFechaCierre" runat="server" Value='<%# Eval("fechaCierre_ase") %>' />
                                                            <asp:HiddenField ID="hddSaldo" runat="server" Value='<%# Eval("SaldoTotal") %>' />
                                                            <div class="custom-control custom-checkbox no-label custom-control-inline" id="divActivarEdicion" runat="server">
                                                                <input type="checkbox" id="chkActivarEdicion" name="chkActivarEdicion" class="custom-control-input" runat="server" value="1">
                                                                <label class="custom-control-label form-control-sm" for="chkActivarEdicion">&nbsp;</label>
                                                            </div>
                                                            <div class="o-switch btn-group" data-toggle="buttons" role="group">
                                                                <label class="btn btn-danger" id="btnNoAsistio" runat="server">
                                                                    <input type="radio" name="rbtAsistencia" id="rbtNoAsistio" runat="server" autocomplete="off">NA
                                                                </label>
                                                                <label class="btn btn-primary" id="btnAsistio" runat="server">
                                                                    <input type="radio" name="rbtAsistencia" id="rbtAsistio" runat="server" autocomplete="off">A
                                                                </label>
                                                                <label class="btn btn-success" id="btnTardanza" runat="server">
                                                                    <input type="radio" name="rbtAsistencia" id="rbtTardanza" runat="server" autocomplete="off">T
                                                                </label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="thead-dark" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <hr class="separador">
                                    <div class="row form-group mb-2">
                                        <div class="col-md-20 offset-md-27 text-right">
                                            <button type="button" id="btnCancelar" runat="server" class="btn btn-accion btn-light">
                                                <i class="fa fa-ban"></i>
                                                <span class="text">Cancelar</span>
                                            </button>
                                            <button type="button" id="btnFakeGuardar" runat="server" class="btn btn-accion btn-primary">
                                                <i class="fa fa-save"></i>
                                                <span class="text">Guardar</span>
                                            </button>
                                            <button type="button" id="btnGuardar" runat="server" class="d-none">
                                            </button>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlMensaje" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span id="spnMenServTitulo" runat="server" class="modal-title">Respuesta del Servidor</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div id="divMdlMensaje" class="alert alert-warning"></div>
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
                        <button type="button" class="btn btn-default" id="btnConfCancelar" data-dismiss="modal">Cancelar</button>
                        <button type="button" class="btn btn-primary" id="btnConfContinuar">Continuar</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlCerrarAsistencias" class="modal fade" tabindex="-1" role="dialog">
            <asp:UpdatePanel ID="udpCerrarAsistencias" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="modal-dialog modal-md" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <span class="modal-title">Confirmar Operación</span>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="card">
                                    <div class="card-header alt-header">Cerrar Asistencias</div>
                                    <div class="card-body">
                                        <div class="row form-group">
                                            <label for="txtFechaCierreAsistencias" class="form-control-sm col-sm-20">Fecha de Cierre:</label>
                                            <div class="col-sm-19">
                                                <asp:TextBox ID="txtFechaCierreAsistencias" runat="server" CssClass="form-control form-control-sm" placeholder="01/01/2000" data-provide="datepicker" />
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-sm-48">
                                                <div class="alert alert-info">
                                                    Al confirmar se cerraran las asistencias de los grupos seleccionados: <br>
                                                    <b><span id="spnGrupos" runat="server"></span></b>
                                                    ¿Desea continuar?
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                                <button type="button" class="btn btn-primary" id="btnConfCerrarAsistencias" runat="server">Confirmar</button>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="mdlRegistrarIncidencia" class="modal fade" tabindex="-1" role="dialog">
            <asp:UpdatePanel ID="udpRegistrarIncidencia" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="modal-dialog modal-md" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <span class="modal-title">Registrar una Incidencia</span>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="card">
                                    <div class="card-header alt-header">Ingrese el motivo de la Incidencia</div>
                                    <div class="card-body">
                                        <div class="row form-group">
                                            <label for="txtGrupoAdmision" class="form-control-sm col-md-10">Grupo:</label>
                                            <div class="col-md-36">
                                                <asp:TextBox ID="txtGrupoAdmision" runat="server" CssClass="form-control form-control-sm" Enabled=false />
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <label for="txtIncidencia" class="form-control-sm col-md-10">Incidencia:</label>
                                            <div class="col-md-36">
                                                <asp:TextBox ID="txtIncidencia" runat="server" TextMode="multiline" Columns="70" Rows="4" CssClass="form-control form-control-sm"
                                                    placeholder="Ingrese una descripción de la incidencia" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                                <button type="button" class="btn btn-primary" id="btnConfRegistrarIncidencia" runat="server">Confirmar</button>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="mdlVisualizarIncidencias" class="modal fade" tabindex="-1" role="dialog">
            <asp:UpdatePanel ID="udpVisualizarIncidencias" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="modal-dialog modal-md" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <span class="modal-title">Visualizar Incidencias</span>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="card">
                                    <ul class="list-group list-sm incidencias">
                                        <asp:Repeater id="rptIncidencia" runat="server">
                                            <ItemTemplate>
                                                <li class="list-group-item"><%# Eval("descripcion_ine") %></li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
    <!-- Scripts externos -->
    <script src="<%=ClsGlobales.PATH_ASSETS %>/jquery/jquery-3.3.1.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-4.1/js/bootstrap.bundle.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-select-1.13.1/js/i18n/defaults-es_ES.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/toastr-2.1.4-7/toastr.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/datatables/datatables.min.js"></script>
    <!-- Scripts propios -->
    <script src="<%=ClsGlobales.PATH_JS %>/funciones.js"></script>
    <script src="<%=ClsGlobales.PATH_JS %>/registroAsistencia.js"></script>

    <script type="text/javascript">
        var controlId = ''
        var controlsId = [];

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id
            controlsId = [];

            alternarLoadingGif('global', false);

            switch (controlId) {
                case 'btnListar':
                case 'btnCerrarAsistencia':
                case 'btnConfCerrarAsistencias':
                case 'btnConfRegistrarIncidencia':
                case 'btnCancelar':
                case 'btnGuardar':
                    controlsId.push(controlId);
                    break;
            }

            if (controlId.indexOf('btnAsistencia') > -1
                || controlId.indexOf('btnIncidencia') > -1
                || controlId.indexOf('btnVisualizarIncidencias') > -1) {
                controlsId.push(controlId);
            }

            // Desactivo los botones
            controlsId.forEach(function (el) {
                atenuarBoton(el, false);
            });
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
            var error = args.get_error();
            if (error) {
                mostrarMensajeModal(-1, error);

                controlsId.forEach(function (el) {
                    atenuarBoton(el, true);
                });
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (sender, args) {
            var updatedPanels = args.get_panelsUpdated();

            var udpFormUpdated = false
            for (var i = 0; i < updatedPanels.length; i++) {
                var udpPanelId = updatedPanels[i].id;

                switch (udpPanelId) {
                    case 'udpFiltros':
                        initFilterPlugins();
                        break;
                    case 'udpCerrarAsistencias':
                        initCerrarPlugins();
                        break;
                    case 'udpMantenimiento':
                        initFormPlugins();
                        break;
                }
            }
        });

        Sys.Application.add_load(function () {
            var elem = document.getElementById(controlId);

            alternarLoadingGif('global', true);

            // Vuelvo a activar los botones
            controlsId.forEach(function (el) {
                atenuarBoton(el, true);
            });

            switch (controlId) {
                case 'btnGuardar':
                    accionConfirmadaFinalizada();
                    break;
            }

            verificarParametros('TAB|MEN_SERV|TOASTR|ACC_ADIC');
        });    
    </script>
</body>

</html>