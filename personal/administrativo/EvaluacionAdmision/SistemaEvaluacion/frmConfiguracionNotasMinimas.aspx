<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConfiguracionNotasMinimas.aspx.vb" Inherits="SistemaEvaluacion_frmConfiguracionNotasMinimas" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Configuración de Notas Mínimas de Ingreso ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-4.1/css-custom/bootstrap.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-select-1.13.1/css/bootstrap-select.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/fontawesome-5.2/css/all.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-datepicker/css/bootstrap-datepicker.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/toastr-2.1.4-7/toastr.min.css>
    <!-- Estilos propios -->
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_CSS %>/style.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_CSS %>/configuracionNotasMinimas.css>
</head>

<body>
    <form id="form" runat="server">
        <asp:ScriptManager ID="scm" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpParams" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:HiddenField ID="hddCod" Value="" runat="server" />
                <asp:HiddenField ID="hddCac" Value="" runat="server" />
                <asp:HiddenField ID="hddCco" Value="" runat="server" />
                <asp:HiddenField ID="hddCpf" Value="" runat="server" />
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
                    <h1 class="main-title">Configuración de Notas Mínimas de Ingreso<img src="<%=ClsGlobales.PATH_IMG %>/loading.gif" id="loadingGif"></h1>
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
                        <div class="card-body">
                            <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="row form-group">
                                        <div class="col-sm-12 d-flex">
                                            <label for="cmbFiltroCicloAcademico" class="form-control-sm">Semestre Acad.:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbFiltroCicloAcademico" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm selectpicker"
                                                    data-live-search="true">
                                                    <asp:ListItem Text="-- SELECCIONE --" Value="-1" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-18 d-flex">
                                            <label for="cmbFiltroCentroCostos" class="form-control-sm">Centro de Costos:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbFiltroCentroCosto" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                    data-live-search="true">
                                                    <asp:ListItem Text="-- TODOS --" Value="-1" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-18 d-flex">
                                            <label for="cmbFiltroCarreraProfesional" class="form-control-sm">Programa de Estudios:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbFiltroCarreraProfesional" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                    data-live-search="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-sm-48 text-right">
                                            <asp:LinkButton id="btnValoresPorDefecto" runat="server" class="btn btn-accion btn-info mr-2">
                                                <i class="fa fa-sync"></i>
                                                <span class="text">Resetear Valores</span>
                                            </asp:LinkButton>
                                            <asp:LinkButton id="btnListar" runat="server" class="btn btn-accion btn-light">
                                                <i class="fa fa-search"></i>
                                                <span class="text">Listar</span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="udpGrvList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div id="divGrvList" runat="server">
                                        <hr>
                                        <asp:GridView ID="grvList" runat="server" DataKeyNames="codigo_cnm" AutoGenerateColumns="false" CssClass="table table-sm" GridLines="None">
                                            <Columns>
                                                <asp:BoundField DataField="nombre_Fac" HeaderText="FACULTAD" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="nombre_Cpf" HeaderText="PROGRAMA DE ESTUDIOS" />
                                                <asp:TemplateField HeaderText="NOTA MÍNIMA" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <div class="d-flex controles">
                                                            <asp:HiddenField ID="hddCnm" runat="server" Value='<%# Eval("codigo_cnm") %>' />
                                                            <asp:HiddenField ID="hddCpf" runat="server" Value='<%# Eval("codigo_cpf") %>' />
                                                            <asp:HiddenField ID="hddCco" runat="server" Value='<%# Eval("codigo_cco") %>' />
                                                            <asp:TextBox ID="txtNotaMinima" runat="server" CssClass="form-control form-control-sm text-center only-digits" placeholder="--"
                                                                Enabled="False" Text='<%# Eval("nota_min_cnm") %>' />
                                                            <asp:LinkButton type="button" id="btnEditar" runat="server" CommandName="Editar" title="Editar Nota" class="btn btn-sm btn-primary ml-2">
                                                                <i class="fa fa-edit"></i>
                                                                <span class="text">Editar</span>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton type="button" id="btnGuardar" runat="server" CommandName="Guardar" title="Guardar" CommandArgument=<%# Eval("codigo_cnm") %>
                                                                class="btn btn-sm btn-success ml-2 mr-1" Visible="False">
                                                                <i class="fa fa-save"></i>
                                                                <span class="text">Guardar</span>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton type="button" id="btnCancelar" runat="server" CommandName="Cancelar" title="Cancelar" class="btn btn-sm btn-danger"
                                                                Visible="False">
                                                                <i class="fa fa-ban"></i>
                                                                <span class="text">Cancelar</span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NOTAS / NIVELACIÓN" ItemStyle-Width="25%">
                                                    <ItemTemplate>
                                                        <div class="d-flex d-flex justify-content-between controles">
                                                            <ul class="competencias">
                                                                <asp:Repeater id="rptNotasPorCompetencia" runat="server">
                                                                    <ItemTemplate>
                                                                        <li><%# Eval("competencia") %>: <b><%# Eval("nota") %></b></li>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </ul>
                                                            <asp:LinkButton type="button" id="btnEditarCompetencias" runat="server" CommandName="Competencias" title="Notas por Competencia"
                                                                CommandArgument=<%# Eval("codigo_cnm") & "," & Eval("codigo_cpf") %> class="btn btn-sm btn-success">
                                                                <i class="fas fa-tasks"></i>
                                                                <span class="text">Asignar</span>
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
        <div id="mdlNotasPorDefecto" class="modal fade" tabindex="-1" role="dialog">
            <asp:UpdatePanel ID="udpNotasPorDefecto" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <span class="modal-title">Reiniciar valores por defecto</span>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="card">
                                    <div class="card-header alt-header">Ingrese los valores de admisión</div>
                                    <div class="card-body">
                                        <div class="row form-group">
                                            <label for="txtCarreraProfesional" class="form-control-sm col-md-11">Programa de Estudios:</label>
                                            <div class="col-md-34">
                                                <asp:TextBox ID="txtCarreraProfesional" runat="server" CssClass="form-control form-control-sm" Enabled="false" />
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <label for="txtNota" class="form-control-sm col-md-11">Notas Ingreso:</label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtNota" runat="server" CssClass="form-control form-control-sm text-center" placeholder="--" />
                                            </div>
                                            <label for="txtNotaNivelacion" class="form-control-sm col-md-8">Nota / Nivelación:</label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtNotaNivelacion" runat="server" CssClass="form-control form-control-sm text-center" placeholder="--" />
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-md-48">
                                                <div class="alert alert-danger mt-2">
                                                    <div class="custom-control custom-checkbox">
                                                        <input type="checkbox" class="custom-control-input" id="chkConfNotasPorDefecto" runat="server">
                                                        <label class="custom-control-label" for="chkConfNotasPorDefecto">
                                                            <b>Al confirmar la operación se modificarán las notas mínimas para el evento seleccionado. ¿Está seguro de continuar?</b>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                                <asp:LinkButton type="button" class="btn btn-primary" id="btnConfNotasPorDefecto" runat="server">Confirmar</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="mdlNotaMinimaCompetencias" class="modal fade" tabindex="-1" role="dialog">
            <asp:UpdatePanel ID="udpNotaMinimaCompetencias" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <span class="modal-title">Configuración de notas mínimas por competencia</span>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="card">
                                    <div class="card-header alt-header">Notas mínimas por competencia</div>
                                    <div class="card-body">
                                        <div class="row form-group">
                                            <div class="col-sm-48">
                                                <asp:UpdatePanel ID="udpGrvNotaMinimaCompetencia" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="grvNotaMinimaCompetencias" runat="server" DataKeyNames="codigo_cnm" AutoGenerateColumns="false" CssClass="table table-sm"
                                                            GridLines="None">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="NRO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" />
                                                                <asp:BoundField DataField="nombre_com" HeaderText="COMPETENCIA" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:TemplateField HeaderText="NOTA MÍNIMA" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtNotaMinima" runat="server" CssClass="form-control form-control-sm only-digits" placeholder="--"
                                                                            Enabled="False" Text='<%# Eval("nota_min_cnc") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="OPERACIONES" ItemStyle-Width="30%" ItemStyle-HorizontalAlign=Center>
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hddCnc" runat="server" Value='<%# Eval("codigo_cnc") %>' />
                                                                        <asp:HiddenField ID="hddCnm" runat="server" Value='<%# Eval("codigo_cnm") %>' />
                                                                        <asp:HiddenField ID="hddCom" runat="server" Value='<%# Eval("codigo_com") %>' />
                                                                        <asp:LinkButton type="button" id="btnEditar" runat="server" CommandName="Editar" class="btn btn-sm btn-primary">
                                                                            <i class="fa fa-edit"></i>
                                                                            <span class="text">Editar</span>
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton type="button" id="btnGuardar" runat="server" CommandName="Guardar" CommandArgument=<%# Eval("codigo_cnc") %>
                                                                            class="btn btn-sm btn-success" Visible="False">
                                                                            <i class="fa fa-save"></i>
                                                                            <span class="text">Guardar</span>
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton type="button" id="btnCancelar" runat="server" CommandName="Cancelar" class="btn btn-sm btn-danger"
                                                                            Visible="False">
                                                                            <i class="fa fa-ban"></i>
                                                                            <span class="text">Cancelar</span>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="thead-dark" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div id="divCeroElementos" runat="server">
                                                    <div class="alert alert-warning">
                                                        No se ha encontrado ninguna competencia configurada en los perfiles de ingreso para esta carrera.
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
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
    <!-- Scripts propios -->
    <script src="<%=ClsGlobales.PATH_JS %>/funciones.js"></script>
    <script src="<%=ClsGlobales.PATH_JS %>/configuracionNotasMinimas.js"></script>

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
                case 'btnValoresPorDefecto':
                case 'btnConfNotasPorDefecto':
                    controlsId.push(controlId);
                    break;
            }

            if (controlId.indexOf('btnEditar') > -1
                || controlId.indexOf('btnEditarCompetencias') > -1
                || controlId.indexOf('btnCancelar') > -1
                || controlId.indexOf('btnGuardar') > -1) {
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