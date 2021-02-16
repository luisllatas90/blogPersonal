<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmVacantesEvento.aspx.vb" Inherits="frmVacantesEvento" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Configuración de Vacantes por Evento de Admisión ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-4.1/css-custom/bootstrap.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-select-1.13.1/css/bootstrap-select.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/fontawesome-5.2/css/all.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-datepicker/css/bootstrap-datepicker.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/toastr-2.1.4-7/toastr.min.css>
    <!-- Estilos propios -->
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_CSS %>/style.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_CSS %>/vacantesEvento.css>
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
                    <h1 class="main-title">Configuración de Vacantes por Evento de Admisión<img src="<%=ClsGlobales.PATH_IMG %>/loading.gif" id="loadingGif"></h1>
                    <hr>
                </div>
            </div>
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav-configuracion-tab" data-toggle="tab" href="#nav-configuracion" role="tab" aria-controls="nav-configuracion"
                        aria-selected="true">Configuración</a>
                </div>
            </nav>
            <div class="tab-content" id="nav-tabContent">
                <div class="tab-pane fade show active" id="nav-configuracion" role="tabpanel" aria-labelledby="nav-configuracion-tab">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="title">Filtros</h5>
                        </div>
                        <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card-body">
                                    <div class="row form-group">
                                        <div class="col-sm-12 d-flex">
                                            <label for="cmbFiltroCicloAcademico" class="form-control-sm">Semestre Acad.:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbFiltroCicloAcademico" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker"
                                                    data-live-search="true">
                                                    <asp:ListItem Text="-- TODOS --" Value="1" />
                                                    <asp:ListItem Text="2020-II" Value="75" />
                                                    <asp:ListItem Text="2020-I" Value="74" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-19 d-flex">
                                            <label for="cmbFiltroCarreraProfesional" class="form-control-sm">Programa de Estudios:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbFiltroCarreraProfesional" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                    data-live-search="true">
                                                    <asp:ListItem Text="-- TODOS --" Value="0" />
                                                    <asp:ListItem Text="ADMINISTRACIÓN" Value="1" />
                                                    <asp:ListItem Text="ING. DE SISTEMAS" Value="2" />
                                                    <asp:ListItem Text="MEDICINA" Value="3" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-17 d-flex">
                                            <label for="cmbFiltroModalidadIngreso" class="form-control-sm">Modalidad de Ingreso:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbFiltroModalidadIngreso" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                    data-live-search="true">
                                                    <asp:ListItem Text="-- TODOS --" Value="1" />
                                                    <asp:ListItem Text="EVALUACIÓN PREFERENTE" Value="1" />
                                                    <asp:ListItem Text="TEST DAHC" Value="2" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-sm-22 d-flex">
                                            <label for="cmbFiltroCentroCostos" class="form-control-sm">Centro de Costos:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbFiltroCentroCostos" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                    data-live-search="true">
                                                    <asp:ListItem Text="-- TODOS --" Value="0" />
                                                    <asp:ListItem Text="ADM - EXAMEN TEST DAHC 2020-II (18-JUL-20)" Value="1" />
                                                    <asp:ListItem Text="ADM - BECA 18 - 2020-II (MIGRACIÓN DEL 2020-I)" Value="2" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-26 text-right">
                                            <asp:UpdatePanel ID="udpAcciones" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-light">
                                                        <i class="fa fa-search"></i>
                                                        <span class="text">Listar</span>
                                                    </button>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-48">
                                            <asp:UpdatePanel ID="udpGrvList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <div id="divGrvList" runat="server">
                                                        <hr class="separador">
                                                        <asp:GridView ID="grvList" runat="server" DataKeyNames="codigo_vae" AutoGenerateColumns="false" CssClass="table table-sm" GridLines="None">
                                                            <Columns>
                                                                <asp:BoundField DataField="descripcion_Cac" HeaderText="SEMESTRE ACADÉMICO" />
                                                                <asp:BoundField DataField="nombre_Cpf" HeaderText="PROGRAMA DE ESTUDIOS" />
                                                                <asp:TemplateField HeaderText="MODALIDAD DE INGRESO" ItemStyle-HorizontalAlign=Center>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="edtName" runat="server" Text='<%# Eval("nombre_Min") & "<br><b>" & Eval("numero_vac") & "</b> Vacantes" %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="descripcion_Cco" HeaderText="CENTRO DE COSTOS" />
                                                                <asp:TemplateField HeaderText="VACANTES POR EVENTO" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <div class="d-flex controles justify-content-between">
                                                                            <asp:HiddenField ID="hddVae" runat="server" Value='<%# Eval("codigo_vae") %>' />
                                                                            <asp:HiddenField ID="hddCco" runat="server" Value='<%# Eval("codigo_cco") %>' />
                                                                            <asp:HiddenField ID="hddVac" runat="server" Value='<%# Eval("codigo_vac") %>' />
                                                                            <asp:TextBox ID="txtVacantes" runat="server" CssClass="form-control form-control-sm text-center only-digits"
                                                                                placeholder="Vacantes" Enabled="False" Text='<%# Eval("cantidad_vae") %>' />
                                                                            <asp:LinkButton type="button" id="btnEditar" runat="server" CommandName="Editar" class="btn btn-sm btn-primary ml-2">
                                                                                <i class="fa fa-edit"></i>
                                                                                <span class="text">Editar</span>
                                                                            </asp:LinkButton>
                                                                            <asp:LinkButton type="button" id="btnGuardar" runat="server" CommandName="Guardar" CommandArgument=<%# Eval("codigo_vae") %>
                                                                                class="btn btn-sm btn-success  ml-2" Visible="False">
                                                                                <i class="fa fa-save"></i>
                                                                                <span class="text">Guardar</span>
                                                                            </asp:LinkButton>
                                                                            <asp:LinkButton type="button" id="btnCancelar" runat="server" CommandName="Cancelar" class="btn btn-sm btn-danger  ml-2"
                                                                                Visible="False">
                                                                                <i class="fa fa-ban"></i>
                                                                                <span class="text">Cancelar</span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="NRO DE ACCESITARIOS" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <div class="d-flex controles justify-content-between">
                                                                            <asp:TextBox ID="txtAccesitarios" runat="server" CssClass="form-control form-control-sm text-center only-digits"
                                                                                placeholder="Accesitarios" Enabled="False" Text='<%# Eval("cantidad_accesitarios_vae") %>' />
                                                                            <asp:LinkButton type="button" id="btnEditarAccesitarios" runat="server" CommandName="EditarAccesitarios"
                                                                                class="btn btn-sm btn-primary ml-2">
                                                                                <i class="fa fa-edit"></i>
                                                                                <span class="text">Editar</span>
                                                                            </asp:LinkButton>
                                                                            <asp:LinkButton type="button" id="btnGuardarAccesitarios" runat="server" CommandName="GuardarAccesitarios"
                                                                                CommandArgument=<%# Eval("codigo_vae") %> class="btn btn-sm btn-success  ml-2" Visible="False">
                                                                                <i class="fa fa-save"></i>
                                                                                <span class="text">Guardar</span>
                                                                            </asp:LinkButton>
                                                                            <asp:LinkButton type="button" id="btnCancelarAccesitarios" runat="server" CommandName="CancelarAccesitarios"
                                                                                class="btn btn-sm btn-danger  ml-2" Visible="False">
                                                                                <i class="fa fa-ban"></i>
                                                                                <span class="text">Cancelar</span>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlMensajeServidor" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span id="spnMenServTitulo" runat="server" class="modal-title">Respuesta del Servidor</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div id="divMenServMensaje" class="alert alert-warning" runat="server"></div>
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
    <script src="<%=ClsGlobales.PATH_JS %>/vacantesEvento.js"></script>

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
                    controlsId.push(controlId);
                    break;
            }

            if (controlId.indexOf('btnEditar') > -1
                || controlId.indexOf('btnGuardar') > -1
                || controlId.indexOf('btnCancelar') > -1) {
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
                        initFiltrosPlugins();
                        break;
                    case 'udpGrvList':
                        initGrvTooltips();
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

            verificarParametros('TAB|MEN_SERV|TOASTR');
        });    
    </script>
</body>

</html>