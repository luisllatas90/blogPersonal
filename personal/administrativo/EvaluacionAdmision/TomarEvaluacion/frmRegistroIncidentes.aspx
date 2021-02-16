<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistroIncidentes.aspx.vb" Inherits="TomarEvaluacion_frmRegistroIncidentes" %>

<!DOCTYPE html>
<html lang="en">

<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Registro de Incidencias ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-4.1/css-custom/bootstrap.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-select-1.13.1/css/bootstrap-select.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/fontawesome-5.2/css/all.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-datepicker/css/bootstrap-datepicker.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/toastr-2.1.4-7/toastr.min.css>
    <!-- Estilos propios -->
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_CSS %>/style.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_CSS %>/registroIncidentes.css>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Registro de Incidencias<img src=<%=ClsGlobales.PATH_IMG %>/loading.gif id="loadingGif"></h1>
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
                                        <div class="col-sm-13 d-flex">
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
                                                <asp:DropDownList ID="cmbFiltroCentroCosto" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                    data-live-search="true">
                                                    <asp:ListItem Text="-- SELECCIONE --" Value="1" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-13 text-right">
                                            <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-light">
                                                <i class="fa fa-search"></i>
                                                <span class="text">Listar</span>
                                            </button>
                                            <button type="button" id="btnAgregar" runat="server" class="btn btn-accion btn-success">
                                                <i class="fa fa-plus"></i>
                                                <span class="text">Agregar</span>
                                            </button>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-48">
                                            <asp:UpdatePanel ID="udpGrvList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <div id="divGrvList" runat="server">
                                                        <hr class="separador">
                                                        <asp:GridView ID="grvList" runat="server" AutoGenerateColumns="false" CssClass="table table-sm" GridLines="None">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="NRO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" />
                                                                <asp:BoundField DataField="descripcion_cco" HeaderText="CENTRO COSTO" />
                                                                <asp:BoundField DataField="nombre_gru" HeaderText="GRUPO" />
                                                                <asp:BoundField DataField="descripcion_ine" HeaderText="INCIDENTE" />
                                                                <asp:TemplateField HeaderText="OPERACIONES" ItemStyle-Width="15%" ItemStyle-HorizontalAlign=Center>
                                                                    <ItemTemplate>
                                                                        <div class="d-flex controles justify-content-center">
                                                                            <asp:LinkButton type="button" id="btnEditar" runat="server" CommandName="Editar" CommandArgument=<%# Eval("codigo_ine") %>
                                                                                class="btn btn-sm btn-accion btn-primary">
                                                                                <i class="fa fa-edit"></i>
                                                                                <span class="text">Editar</span>
                                                                            </asp:LinkButton>
                                                                            <button type="button" id="btnFakeEliminar" runat="server" class="btn btn-sm btn-accion btn-danger ml-2" data-toggle="tooltip"
                                                                                data-placement="top" title="Eliminar">
                                                                                <i class="far fa-trash-alt"></i>
                                                                                <span class="text">Eliminar</span>
                                                                            </button>
                                                                            <asp:LinkButton type="button" id="btnEliminar" runat="server" CommandName="Eliminar"
                                                                                CommandArgument=<%# Eval("codigo_ine") %> class="d-none">
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                                            <h1 class="title">Datos Principales:</h1>
                                            <hr>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="cmbCicloAcademico" class="form-control-sm col-md-7">Semestre Académico:</label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="cmbCicloAcademico" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm selectpicker" data-live-search="true">
                                                <asp:ListItem Text="-- SELECCIONE --" Value="1" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="cmbCentroCosto" class="form-control-sm col-md-7">Centro Costo:</label>
                                        <div class="col-md-16">
                                            <asp:DropDownList ID="cmbCentroCosto" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm selectpicker" data-live-search="true">
                                                <asp:ListItem Text="-- SELECCIONE --" Value="1" />
                                                <asp:ListItem Text="OPCIÓN 1" Value="1" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="cmbGrupoAdmision" class="form-control-sm col-md-7">Grupo:</label>
                                        <div class="col-md-10">
                                            <asp:DropDownList ID="cmbGrupoAdmision" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker" data-live-search="true">
                                                <asp:ListItem Text="-- SELECCIONE --" Value="1" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="txtIncidencia" class="form-control-sm col-md-7">Incidencia:</label>
                                        <div class="col-md-16">
                                            <asp:TextBox ID="txtIncidencia" runat="server" TextMode="multiline" Columns="70" Rows="4" CssClass="form-control form-control-sm"
                                                placeholder="Ingrese incidencia" />
                                        </div>
                                    </div>
                                    <hr class="separador">
                                    <div class="row form-group mb-2">
                                        <div class="col-md-20 offset-md-7">
                                            <button type="button" id="btnCancelar" runat="server" class="btn btn-accion btn-light">
                                                <i class="fa fa-ban"></i>
                                                <span class="text">Cancelar</span>
                                            </button>
                                            <button type="button" id="btnGuardar" runat="server" class="btn btn-accion btn-primary">
                                                <i class="fa fa-save"></i>
                                                <span class="text">Guardar</span>
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
    </form>
    <!-- Scripts externos -->
    <script src="<%=ClsGlobales.PATH_ASSETS %>/jquery/jquery-3.3.1.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-4.1/js/bootstrap.bundle.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-select-1.13.1/js/i18n/defaults-es_ES.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/toastr-2.1.4-7/toastr.min.js"></script>
    <!-- Scripts propios -->
    <script src="<%=ClsGlobales.PATH_JS %>/funciones.js"></script>
    <script src="<%=ClsGlobales.PATH_JS %>/registroIncidentes.js"></script>

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
                case 'btnAgregar':
                case 'btnCancelar':
                case 'btnGuardar':
                    controlsId.push(controlId);
                    break;
            }

            if (controlId.indexOf('btnEditar') > -1 || controlId.indexOf('btnEliminar') > -1) {
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

            if (controlId.indexOf('btnEliminar') > -1) {
                accionConfirmadaFinalizada();
            }

            verificarParametros('TAB|MEN_SERV|TOASTR');
        });    
    </script>
</body>

</html>