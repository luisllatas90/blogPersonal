<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMatriculaPreGradoNivelacion.aspx.vb" Inherits="administrativo_gestion_educativa_frmMatriculaPreGradoNivelacion" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <title>.:: Matrícula por Curso Programado ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css">
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/matriculaPreGradoNivelacion.css?20">
</head>

<body>
    <form id="frmMatriculaPreGradoNivelacion" runat="server">
        <asp:ScriptManager ID="scmMatriculaPreGradoNivelacion" runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <div class="card" id="filters-card">
                <div class="card-body">
                    <h5 class="title">Matrículas por Curso Programado</h5>
                    <hr>
                    <div class="row form-group">
                        <label for="cmbCicloAcademico" class="col-sm-5 col-form-label form-control-sm">Semestre:</label>
                        <div class="col-sm-7">
                            <asp:UpdatePanel ID="udpCicloAcademico" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cmbCicloAcademico" runat="server" AutoPostBack="true"
                                        CssClass="form-control form-control-sm selectpicker" data-live-search="true"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label for="cmbPlanEstudios" class="col-sm-6 col-form-label form-control-sm">Plan de Estudios:</label>
                        <div class="col-sm-28">
                            <asp:UpdatePanel ID="udpPlanEstudios" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cmbPlanEstudios" runat="server" AutoPostBack="true"
                                        CssClass="form-control form-control-sm selectpicker" data-live-search="true">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label for="cmbCarreraProfesional" class="col-sm-5 col-form-label form-control-sm">Carrera Prof.:</label>
                        <div class="col-sm-18">
                            <asp:UpdatePanel ID="udpCarreraProfesional" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cmbCarreraProfesional" runat="server" AutoPostBack="true"
                                        CssClass="form-control form-control-sm selectpicker" data-live-search="true"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label for="cmbGrupoHorario" class="col-sm-5 col-form-label form-control-sm">Grupo Horario:</label>
                        <div class="col-sm-18">
                            <asp:UpdatePanel ID="udpGrupoHorario" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cmbGrupoHorario" runat="server" AutoPostBack="true"
                                        CssClass="form-control form-control-sm selectpicker"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label for="cmbCursosProgramados" class="col-sm-5 col-form-label form-control-sm">Cursos:</label>
                        <div class="col-sm-16">
                            <asp:UpdatePanel ID="udpCursosProgramados" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:ListBox ID="cmbCursosProgramados" runat="server" AutoPostBack="true" SelectionMode="Multiple" 
                                        CssClass="form-control form-control-sm selectpicker">
                                    </asp:ListBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label for="cmbEstadoPagoMatricula" class="col-sm-5 col-form-label form-control-sm">Estado Pago:</label>
                        <div class="col-sm-10">
                            <asp:UpdatePanel ID="udpEstadoPagoMatricula" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cmbEstadoPagoMatricula" runat="server" AutoPostBack="true"
                                        CssClass="form-control form-control-sm selectpicker">
                                        <asp:ListItem Value="-1">TODOS</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">PAGÓ MATRÍCULA</asp:ListItem>
                                        <asp:ListItem Value="0">NO PAGÓ MATRÍCULA</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-sm-36">Lista de alumnos por filtros seleccionados</div>
                        <div class="col-sm-12 text-right rel-absolute">
                            <img src="img/loading.gif" class="loading-gif" id="loading-gif">
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row" id="divConsideraciones">
                        <div class="col-sm-18">
                            <span class="badge badge-info">No se permiten matrículas si el estudiante está INACTIVO</span>
                        </div>
                        <div class="col-sm-18">
                            <asp:UpdatePanel ID="udpSpnVacantes" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <span class="badge badge-light" runat="server" id="spnVacantes"></span>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-12 text-right">
                            <div class="button-group">
                                <asp:UpdatePanel ID="udpBotonera" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-verde">
                                            <i class='fa fa-search'></i>
                                            <span class="text">Listar</span>
                                        </button>
                                        <button type="button" id="btnMatricular" runat="server" class="btn btn-accion btn-azul">
                                            <i class='fa fa-save'></i>
                                            <span class="text">Matricular</span>
                                        </button>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <hr>
                    <asp:UpdatePanel ID="udpAlumnos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:GridView ID="grwAlumnos" runat="server" AutoGenerateColumns="false"
                                DataKeyNames="codigo_alu, matriculado, estadoActual_alu, pagoMatricula" CssClass="table table-sm" GridLines="None">
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <div class="custom-control custom-checkbox">
                                                <input type="checkbox" runat="server" id="chkHeader" class="custom-control-input">
                                                <asp:Label runat="server" ID="lblHeader" AssociatedControlId="chkHeader" CssClass="custom-control-label" />
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="custom-control custom-checkbox">
                                                <input type="checkbox" runat="server" id="chkElegir" class="custom-control-input">
                                                <asp:Label runat="server" ID="lblHeader" AssociatedControlId="chkElegir" CssClass="custom-control-label" />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Nro" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Código" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="nombreCompleto" HeaderText="Apellidos y Nombres" />
                                    <asp:BoundField DataField="nombre_Cpf" HeaderText="Carrera Profesional" />
                                    <asp:BoundField DataField="estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="pagoMatricula" HeaderText="Pagó matrícula?" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="matriculado" HeaderText="Matriculado" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="estadoDeuda_alu" HeaderText="Deuda" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="numerodeudas" HeaderText="No. Deudas" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion"
                                        HeaderText="Retirar" />
                                </Columns>
                                <HeaderStyle CssClass="thead-dark" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
                                <span id="spnMensajeServidorTitulo" runat="server" class="modal-title">Respuesta del
                                    Servidor</span>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
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
                                <button type="button" id="btnMensajeCerrar" runat="server" class="btn btn-default"
                                    data-dismiss="modal">Cerrar</button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlRetirarMatricula" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpRetirarMatricula" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <span class="modal-title">Cursos Matriculados</span>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                            <asp:UpdatePanel ID="udpSinCursos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="alert alert-info" id="divSinCursos" runat="server">No se han encontrado cursos</div>
                                    <asp:GridView ID="grwCursosMatriculados" runat="server" AutoGenerateColumns="false"
                                    CssClass="table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="nombre_Cur" HeaderText="Curso" ReadOnly="True" />
                                        <asp:BoundField DataField="grupoHor_Cup" HeaderText="Grupo Horario" ReadOnly="True" />
                                        <asp:BoundField DataField="estado_Dma" HeaderText="Estado" ReadOnly="True" />
                                        <asp:BoundField DataField="condicion_Dma" HeaderText="Con Nota Final" ReadOnly="True" />
                                    </Columns>
                                </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-primary submit" id="btnRetirarMatricula" runat="server">Aceptar</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
    <!-- Scripts externos -->
    <script src="../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.min.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/i18n/defaults-es_ES.min.js"></script>
    <script src="../../assets/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script src="../../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="../../assets/jquery-validation/jquery.validate.min.js"></script>
    <script src="../../assets/jquery-validation/localization/messages_es.min.js"></script>
    <script src="../../assets/iframeresizer/iframeResizer.contentWindow.min.js"></script>
    <!-- Scripts propios -->
    <script src="js/funciones.js"></script>
    <script src="js/matriculaPreGradoNivelacion.js?32"></script>
    <script type="text/javascript">
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id

            switch (controlId) {
                case 'cmbCarreraProfesional':
                    VerificarLoadingGif();
                    break;
                case 'cmbCursosProgramados':
                    VerificarLoadingGif();
                    break;
                case 'btnListar':
                    AtenuarBoton(controlId, false);
                    AtenuarLoadingGif(false);
                    AtenuarElemento('grwAlumnos', false);
                    break;
                case 'btnMatricular':
                    AtenuarBoton(controlId, false);
                    AtenuarLoadingGif(false);
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
                    case 'udpPlanEstudios':
                        InitComboPlanEstudios();
                        break;
                    case 'udpGrupoHorario':
                        InitComboGrupoHorario();
                        break;
                    case 'udpCursosProgramados':
                        InitComboCursosProgramados();
                        break;
                    case 'udpAlumnos': 
                        AtenuarLoadingGif(true);
                        break;
                }
            }
        });

        Sys.Application.add_load(function() {
            var elem = document.getElementById(controlId);

            switch (controlId) {
                case 'btnListar':
                    AtenuarBoton(controlId, true);
                    AtenuarLoadingGif(true);
                    AtenuarElemento('grwAlumnos', true);
                    break;
                case 'btnMatricular':
                    AtenuarBoton(controlId, true);
                    AtenuarLoadingGif(true);
                    break;
            }

            if (controlId.indexOf('btnRetirar') > -1) {
                CargarModalRetiro(controlId);
            }

            RevisarMensajePostback();

        });    
    </script>
</body>

</html>