<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRecalculoResultados.aspx.vb" Inherits="ProcesamientoResultados_frmRecalculoResultados" %>

<!DOCTYPE html>
<html lang="en">

<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Calcular Resultados ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-4.1/css-custom/bootstrap.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-select-1.13.1/css/bootstrap-select.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/fontawesome-5.2/css/all.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-datepicker/css/bootstrap-datepicker.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/toastr-2.1.4-7/toastr.min.css>
    <!-- Estilos propios -->
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_CSS %>/style.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_CSS %>/recalculoResultados.css>
</head>

<body>
    <form id="form" runat="server">
        <asp:ScriptManager ID="scm" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpParams" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:HiddenField ID="hddCod" Value="" runat="server" />
                <asp:HiddenField ID="hddCac" Value="" runat="server" />
                <asp:HiddenField ID="hddCco" Value="" runat="server" />
                <asp:HiddenField ID="hddMin" Value="" runat="server" />
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
                    <h1 class="main-title">Calcular Resultados<img src=<%=ClsGlobales.PATH_IMG %>/loading.gif id="loadingGif"></h1>
                    <hr>
                </div>
            </div>
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav-lista-tab" data-toggle="tab" href="#nav-lista" role="tab" aria-controls="nav-lista" aria-selected="true">Resultados</a>
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
                                        <div class="col-sm-12 d-flex">
                                            <label for="cmbFiltroCicloAcademico" class="form-control-sm">Semestre Acad.:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbFiltroCicloAcademico" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm selectpicker"
                                                    data-live-search="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-20 d-flex">
                                            <label for="cmbFiltroCentroCosto" class="form-control-sm">Centro Costo:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbFiltroCentroCosto" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                    data-live-search="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-16 d-flex">
                                            <label for="cmbFiltroModalidadIngreso" class="form-control-sm">Modalidad de Ingreso:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbFiltroModalidadIngreso" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                    data-live-search="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-sm-20 d-flex">
                                            <label for="cmbFiltroCarreraProfesional" class="form-control-sm">Programa de Estudios:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbFiltroCarreraProfesional" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                    data-live-search="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-28 text-right">
                                            <asp:LinkButton id="btnListar" runat="server" class="btn btn-sm btn-light">
                                                <i class="fa fa-search"></i>
                                                <span class="text">Listar</span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="udpOperaciones" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div id="divOperaciones" runat="server" class="row">
                                        <div class="col-sm-48">
                                            <hr>
                                            <div class="card">
                                                <div class="card-header">Procesar resultados del evento</div>
                                                <div class="card-body">
                                                    <div class="row form-group">
                                                        <div class="col-md-21 d-flex">
                                                            <div class="alert alert-info" id="divMensaje" runat="server"></div>
                                                        </div>
                                                        <div class="col-md-27 d-flex justify-content-end">
                                                            <div id="divProcesarResultados" class="d-flex" runat="server">
                                                                <div class="d-flex flex-column mr-2">
                                                                    <label for="spnNroVacantes" class="form-control-sm">Vacantes:</label>
                                                                    <span class="badge badge-info" id="spnNroVacantes" runat="server"></span>
                                                                </div>
                                                                <div class="d-flex flex-column mr-2">
                                                                    <label for="txtNroAccesitarios" class="form-control-sm">Accesitarios:</label>
                                                                    <asp:TextBox ID="txtNroAccesitarios" runat="server" CssClass="form-control form-control-sm" placeholder="--" />
                                                                </div>
                                                                <div class="d-flex flex-column">
                                                                    <label for="txtNotaMinima" class="form-control-sm">Nota mínima:</label>
                                                                    <asp:TextBox ID="txtNotaMinima" runat="server" CssClass="form-control form-control-sm" placeholder="--" />
                                                                </div>
                                                            </div>
                                                            <div id="divAcciones" class="d-flex" runat="server">
                                                                <asp:LinkButton id="btnProcesar" runat="server" class="btn btn-sm btn-primary ml-3">
                                                                    <i class="fa fa-cogs"></i>
                                                                    <span class="text">Procesar</span>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton id="btnFakeConfirmar" runat="server" class="btn btn-sm btn-danger ml-2">
                                                                    <i class="fas fa-check-double"></i>
                                                                    <span class="text">Confirmar</span>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton id="btnConfirmar" runat="server"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
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
                                                <asp:LinkButton id="btnExportar" runat="server" OnClientClick="return exportarResultados();" class="btn btn-sm btn-success mb-2 ml-2">
                                                    <i class="fas fa-file-download"></i>
                                                    <span class="text">Exportar</span>
                                                </asp:LinkButton>
                                                <asp:GridView ID="grvList" runat="server" AutoGenerateColumns="true" CssClass="table table-sm" GridLines="None">
                                                    <HeaderStyle CssClass="thead-dark" />
                                                </asp:GridView>
                                            </div>
                                            <div id="divCeroElementos" runat="server">
                                                <hr>
                                                <div class="row">
                                                    <div class="col-sm-48">
                                                        <div class="alert alert-warning">
                                                            No se ha encontrado ningún registro
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
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
    <script src="<%=ClsGlobales.PATH_ASSETS %>/sheetjs/xlsx.full.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/filesaver/FileSaver.min.js"></script>
    <!-- Scripts propios -->
    <script src="<%=ClsGlobales.PATH_JS %>/funciones.js"></script>
    <script src="<%=ClsGlobales.PATH_JS %>/recalculoResultados.js?x=1"></script>

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
                case 'btnProcesar':
                    controlsId.push(controlId);
                    break;
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
                case 'btnConfirmar':
                    accionConfirmadaFinalizada();
                    break;
            }

            verificarParametros('TAB|MEN_SERV|TOASTR|ACC_ADIC');
        });    
    </script>
</body>

</html>