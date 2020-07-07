<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmFiltrarAlumnos.aspx.vb" Inherits="administrativo_gestion_educativa_frmFiltrarAlumnos" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Generación de cargos masivamente ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../../assets/toastr-2.1.4-7/toastr.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/filtrarAlumnos.css?x=1">
</head>

<body>
    <form id="frm" runat="server">
        <asp:ScriptManager ID="scr" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpControlesOcultos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <input type="hidden" name="hddTipoForm" id="hddTipoForm" runat="server" value="N">
                <input type="hidden" name="hddCadenaFiltros" id="hddCadenaFiltros" runat="server">
                <button type="button" id="btnGenerarCadenaFiltros" runat="server" class="btn btn-accion btn-celeste d-none">
                    <i class="fa fa-search"></i>
                    <span class="text">Generar Filtros</span>
                </button>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="udpParametrosMensaje" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <input type="hidden" name="hddRpta" id="hddRpta" runat="server">
                <input type="hidden" name="hddMsg" id="hddMsg" runat="server">
                <input type="hidden" name="hddControl" id="hddControl" runat="server">
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Filtrar Alumnos<img src="img/loading.gif" id="loadingGif"></h1>
                    <hr>
                </div>
            </div>
            <asp:UpdatePanel ID="udpFiltrosAlumno" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="row form-group row-filtros">
                        <div class="col-sm-16 d-flex col-filtro" id="divTipoEstudio" runat="server">
                            <label for="cmbTipoEstudio" class="form-control-sm">Tipo de Estudio:</label>
                            <div class="fill">
                                <asp:DropDownList ID="cmbTipoEstudio" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm selectpicker"
                                    data-live-search="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-14 d-flex col-filtro" id="divSemestreInscripcion" runat="server">
                            <label for="cmbCicloIngreso" class="form-control-sm">Semestre de Inscripción:</label>
                            <div class="fill">
                                <asp:DropDownList ID="cmbCicloIngreso" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                    data-live-search="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group row-filtros">
                        <div class="col-sm-18 d-flex col-filtro" id="divCarreraProfesional" runat="server">
                            <label for="cmbCarreraProfesional" class="form-control-sm">Carrera:</label>
                            <div class="fill">
                                <asp:UpdatePanel ID="udpCarreraProfesional" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:ListBox ID="cmbCarreraProfesional" runat="server" AutoPostBack="false" SelectionMode="Multiple" data-live-search="true"
                                            CssClass="form-control form-control-sm selectpicker">
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-sm-18 d-flex col-filtro" id="divExcluirCarreraProfesional" runat="server">
                            <label for="cmbExcluirCarreraProfesional" class="form-control-sm">Excluir Carrera:</label>
                            <div class="fill">
                                <asp:UpdatePanel ID="udpExcluirCarreraProfesional" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:ListBox ID="cmbExcluirCarreraProfesional" runat="server" AutoPostBack="false" SelectionMode="Multiple" data-live-search="true"
                                            CssClass="form-control form-control-sm selectpicker">
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group row-filtros">
                        <div class="col-lg-12 col-xl-9 d-flex col-filtro" id="divCondicion" runat="server">
                            <label for="cmbCondicion" class="form-control-sm">Condición:</label>
                            <div class="fill">
                                <asp:ListBox ID="cmbCondicion" runat="server" AutoPostBack="false" SelectionMode="Multiple" CssClass="form-control form-control-sm selectpicker">
                                    <asp:ListItem text="POSTULANTE" value="P"></asp:ListItem>
                                    <asp:ListItem text="INGRESANTE" value="I"></asp:ListItem>
                                </asp:ListBox>
                            </div>
                        </div>
                        <div class="col-lg-17 col-xl-14 d-flex col-filtro" id="divAlcanzoVacante" runat="server">
                            <label for="rbtAlcanzoVacante" class="form-control-sm">¿Alcanzó Vacante?:</label>
                            <div class="fill">
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtSiAlcanzoVacante" name="rbtAlcanzoVacante" class="custom-control-input" value="1" runat="server">
                                    <label class="custom-control-label form-control-sm" for="rbtSiAlcanzoVacante">Si</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtNoAlcanzoVacante" name="rbtAlcanzoVacante" class="custom-control-input" value="0" runat="server">
                                    <label class="custom-control-label form-control-sm" for="rbtNoAlcanzoVacante">No</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtAmbosAlcanzoVacante" name="rbtAlcanzoVacante" class="custom-control-input" value="" runat="server" checked>
                                    <label class="custom-control-label form-control-sm" for="rbtAmbosAlcanzoVacante">Todos</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-17 col-xl-14 d-flex col-filtro" id="divTieneDeuda" runat="server">
                            <label for="rbtTieneDeuda" class="form-control-sm">¿Tiene Deuda?:</label>
                            <div class="fill">
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtSiTieneDeuda" name="rbtTieneDeuda" class="custom-control-input" value="1" runat="server">
                                    <label class="custom-control-label form-control-sm" for="rbtSiTieneDeuda">Si</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtNoTieneDeuda" name="rbtTieneDeuda" class="custom-control-input" value="0" runat="server">
                                    <label class="custom-control-label form-control-sm" for="rbtNoTieneDeuda">No</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtAmbosTieneDeuda" name="rbtTieneDeuda" class="custom-control-input" value="" runat="server" checked>
                                    <label class="custom-control-label form-control-sm" for="rbtAmbosTieneDeuda">Todos</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group row-filtros">
                        <div class="col-xl-48">
                            <hr>
                            <h6 class="subtitulo">Otros Filtros</h6>
                        </div>
                        <div class="col-lg-12 col-xl-10 d-flex col-filtro" id="divCicloAcademico" runat="server">
                            <label for="cmbCicloAcademico" class="form-control-sm">Semestre Académico:</label>
                            <div class="fill">
                                <asp:DropDownList ID="cmbCicloAcademico" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-16 col-xl-12 d-flex col-filtro" id="divContinuador" runat="server">
                            <label for="rbtContinuador" class="form-control-sm">¿Continuador?:</label>
                            <div class="fill">
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtSiContinuador" name="rbtContinuador" class="custom-control-input" value="1" runat="server">
                                    <label class="custom-control-label form-control-sm" for="rbtSiContinuador">Si</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtNoContinuador" name="rbtContinuador" class="custom-control-input" value="0" runat="server">
                                    <label class="custom-control-label form-control-sm" for="rbtNoContinuador">No</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtAmbosContinuador" name="rbtContinuador" class="custom-control-input" value="" runat="server" checked>
                                    <label class="custom-control-label form-control-sm" for="rbtAmbosContinuador">Todos</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-18 col-xl-14 d-flex col-filtro" id="divInscritoCicloActual" runat="server">
                            <label for="rbtInscritoCicloActual" class="form-control-sm">¿Inscrito en Ciclo Actual?:</label>
                            <div class="fill">
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtSiInscritoCicloActual" name="rbtInscritoCicloActual" class="custom-control-input" value="1" runat="server">
                                    <label class="custom-control-label form-control-sm" for="rbtSiInscritoCicloActual">Si</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtNoInscritoCicloActual" name="rbtInscritoCicloActual" class="custom-control-input" value="0" runat="server">
                                    <label class="custom-control-label form-control-sm" for="rbtNoInscritoCicloActual">No</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtAmbosInscritoCicloActual" name="rbtInscritoCicloActual" class="custom-control-input" value="" runat="server" checked>
                                    <label class="custom-control-label form-control-sm" for="rbtAmbosInscritoCicloActual">Todos</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row row-filtros">
                        <div class="col-lg-16 col-xl-12 d-flex col-filtro" id="divEgresado" runat="server">
                            <label for="rbtEgresado" class="form-control-sm">¿Egresado?:</label>
                            <div class="fill">
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtSiEgresado" name="rbtEgresado" class="custom-control-input" value="1" runat="server">
                                    <label class="custom-control-label form-control-sm" for="rbtSiEgresado">Si</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtNoEgresado" name="rbtEgresado" class="custom-control-input" value="0" runat="server">
                                    <label class="custom-control-label form-control-sm" for="rbtNoEgresado">No</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtAmbosEgresado" name="rbtEgresado" class="custom-control-input" value="" runat="server" checked>
                                    <label class="custom-control-label form-control-sm" for="rbtAmbosEgresado">Todos</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-19 col-xl-14 d-flex col-filtro" id="divMatriculaExcepcion" runat="server">
                            <label for="rbtMatriculaExcepcion" class="form-control-sm">¿Con Matrícula Excepción?:</label>
                            <div class="fill">
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtSiMatriculaExcepcion" name="rbtMatriculaExcepcion" class="custom-control-input" value="1" runat="server">
                                    <label class="custom-control-label form-control-sm" for="rbtSiMatriculaExcepcion">Si</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtNoMatriculaExcepcion" name="rbtMatriculaExcepcion" class="custom-control-input" value="0" runat="server">
                                    <label class="custom-control-label form-control-sm" for="rbtNoMatriculaExcepcion">No</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbtAmbosMatriculaExcepcion" name="rbtMatriculaExcepcion" class="custom-control-input" value="" runat="server" checked>
                                    <label class="custom-control-label form-control-sm" for="rbtAmbosMatriculaExcepcion">Todos</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
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
                                <span id="spnMenServTitulo" runat="server" class="modal-title">Respuesta del Servidor</span>
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
    </form>
    <!-- Scripts externos -->
    <script src="../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.min.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/i18n/defaults-es_ES.min.js"></script>
    <script src="../../assets/iframeresizer/iframeResizer.contentWindow.min.js"></script>
    <script src="../../assets/toastr-2.1.4-7/toastr.min.js"></script>
    <script src="../../assets/sheetjs/xlsx.full.min.js"></script>
    <!-- Scripts propios -->
    <script src="js/funciones.js"></script>
    <script src="js/filtrarAlumnos.js?x=1"></script>

    <script type="text/javascript">
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id

            alternarLoadingGif('global', false);

            switch (controlId) {
                case 'btnGenerarCadenaFiltros':
                    atenuarBoton(controlId, false);
                    break;
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
                    case 'udpCarreraProfesional':
                        initSelectPicker('cmbCarreraProfesional', {
                            size: 5
                        });
                        break;
                    case 'udpExcluirCarreraProfesional':
                        initSelectPicker('cmbExcluirCarreraProfesional', {
                            size: 5
                        });
                        break;
                }
            }
        });

        Sys.Application.add_load(function () {
            var elem = document.getElementById(controlId);

            alternarLoadingGif('global', true);

            switch (controlId) {
                case 'btnGenerarCadenaFiltros':
                    atenuarBoton(controlId, true);
                    devolverCadenaFiltros();
                    break;
            }

            verificarMensajeServer();
            verificarParametrosMensaje(true);
        });    
    </script>

</body>

</html>