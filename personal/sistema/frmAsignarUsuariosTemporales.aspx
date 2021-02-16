<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAsignarUsuariosTemporales.aspx.vb" Inherits="sistema_frmAsignarUsuariosTemporales" %>

    <!DOCTYPE html>
    <html lang="en">

    <head id="Head1" runat="server">
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>.:: Asignar Usuarios de Acceso Temporal ::.</title>
        <!-- Estilos externos -->
        <link rel="stylesheet" href="../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
        <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
        <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">
        <link rel="stylesheet" href="../assets/tempusdominus-bootstrap/css/tempusdominus-bootstrap-4.css">
        <link rel="stylesheet" href="../assets/toastr-2.1.4-7/toastr.min.css">
        <!-- Estilos propios -->
        <link rel="stylesheet" href="private/css/style.css">
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
                    <div class="col-md-48">
                        <h1 class="main-title">Asignar Usuarios de Acceso Temporal<img src="private/img/loading.gif" id="loadingGif"></h1>
                        <hr>
                    </div>
                </div>
                <nav>
                    <div class="nav nav-tabs" id="nav-tab" role="tablist">
                        <a class="nav-item nav-link active" id="nav-lista-tab" data-toggle="tab" href="#nav-lista" role="tab" aria-controls="nav-lista" aria-selected="true">Historial</a>
                        <a class="nav-item nav-link disabled" id="nav-mantenimiento-tab" data-toggle="tab" href="#nav-mantenimiento" role="tab" aria-controls="nav-mantenimiento"
                            aria-selected="false">Registro</a>
                    </div>
                </nav>
                <div class="tab-content" id="nav-tabContent">
                    <div class="tab-pane fade show active" id="nav-lista" role="tabpanel" aria-labelledby="nav-lista-tab">
                        <div class="card">
                            <div class="card-header">
                                <h5 class="title">Filtros</h5>
                            </div>
                            <div class="card-body">
                                <div class="row form-group">
                                    <div class="col-md-22 d-flex">
                                        <label for="cmbFiltroRegistradoPor" class="form-control-sm">Registrado Por:</label>
                                        <div class="fill">
                                            <asp:DropDownList ID="cmbFiltroRegistradoPor" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                data-live-search="true">
                                                <asp:ListItem Text="TODOS" Value="-1" Selected="True" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-13 d-flex">
                                        <label for="txtFiltroUsuarioOrig" class="form-control-sm">Usuario Original:</label>
                                        <div class="fill">
                                            <asp:TextBox ID="txtFiltroUsuarioOrig" runat="server" CssClass="form-control form-control-sm" placeholder="Usuario" />
                                        </div>
                                    </div>
                                    <div class="col-md-13 d-flex">
                                        <label for="txtFiltroUsuarioTemp" class="form-control-sm">Usuario Temporal:</label>
                                        <div class="fill">
                                            <asp:TextBox ID="txtFiltroUsuarioTemp" runat="server" CssClass="form-control form-control-sm" placeholder="Usuario" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <div class="col-md-10">
                                        <div class="custom-control custom-checkbox custom-control-inline">
                                            <input type="checkbox" id="chkFiltroVigente" name="chkFiltroVigente" class="custom-control-input" runat="server" value="1">
                                            <label class="custom-control-label form-control-sm" for="chkFiltroVigente">Solo Vigentes</label>
                                        </div>
                                    </div>
                                    <div class="col-md-20 offset-md-18 text-right">
                                        <asp:UpdatePanel ID="udpAcciones" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-light">
                                                    <i class="fa fa-search"></i>
                                                    <span class="text">Listar</span>
                                                </button>
                                                <button type="button" id="btnAgregar" runat="server" class="btn btn-accion btn-success ml-1">
                                                    <i class="fa fa-plus"></i>
                                                    <span class="text">Registrar</span>
                                                </button>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-48">
                                        <asp:UpdatePanel ID="udpGrvList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div id="divGrvList" runat="server">
                                                    <hr class="separador">
                                                    <asp:GridView ID="grvList" runat="server" DataKeyNames="codigo_cua" AutoGenerateColumns="false" CssClass="table table-sm" GridLines="None">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="NRO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="usuario_orig" HeaderText="USUARIO REAL" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="usuario_temp" HeaderText="USUARIO TEMPORAL" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:TemplateField HeaderText="VIGENTE" ItemStyle-HorizontalAlign=Center>
                                                                <ItemTemplate>
                                                                    <%# IIf(Eval("vigente_cua"), "SI" , "NO" )%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="fecha_hasta_cua" HeaderText="HASTA" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="nombreCompleto" HeaderText="REALIZÓ EL CAMBIO" />
                                                            <asp:BoundField DataField="fecha_reg" HeaderText="FECHA REGISTRO" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="equipo_reg" HeaderText="EQUIPO REGISTRO" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:TemplateField HeaderText="OPERACIONES" ItemStyle-HorizontalAlign=Center>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton type="button" id="btnEditar" runat="server" CommandName="Editar" CommandArgument=<%# Eval("codigo_cua") %>
                                                                        Visible="False" class="btn btn-sm btn-primary">
                                                                        <i class="fa fa-edit"></i>
                                                                        <span class="text">Editar</span>
                                                                    </asp:LinkButton>

                                                                    <!-- <button type="button" id="btnFakeDesactivar" runat="server" class="btn btn-sm btn-warning ml-1" Visible='False' title="Desactivar">
                                                                        <i class="fa fa-ban"></i>
                                                                        <span class="text">Desactivar</span>
                                                                    </button>
                                                                    <asp:LinkButton id="btnDesactivar" runat="server" CommandName="Desactivar" Visible="False" CommandArgument=<%# Eval("codigo_cua") %>
                                                                        class="d-none">
                                                                    </asp:LinkButton>

                                                                    <button type="button" id="btnFakeActivar" runat="server" class="btn btn-sm btn-info ml-1" Visible='False' title="Activar">
                                                                        <i class="fa fa-check-double"></i>
                                                                        <span class="text">Activar</span>
                                                                    </button>
                                                                    <asp:LinkButton id="btnActivar" runat="server" CommandName="Activar" Visible="False" CommandArgument=<%# Eval("codigo_cua") %>
                                                                        class="d-none">
                                                                    </asp:LinkButton> -->

                                                                    <button type="button" id="btnFakeEliminar" runat="server" class="btn btn-sm btn-danger ml-1" title="Eliminar">
                                                                        <i class="fa fa-trash-alt"></i>
                                                                        <span class="text">Eliminar</span>
                                                                    </button>
                                                                    <asp:LinkButton id="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument=<%# Eval("codigo_cua") %> CssClass="d-none">
                                                                    </asp:LinkButton>
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
                                <h5 class="title">Registro</h5>
                            </div>
                            <div class="card-body">
                                <asp:UpdatePanel ID="udpMantenimiento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-48">
                                                <h1 class="title">Ingrese los datos para asignar el acceso temporal</h1>
                                                <hr>
                                            </div>
                                        </div>
                                        <div class="row form-group mt-2">
                                            <label for="cmbUsuarioReal" class="form-control-sm col-md-8">Usuario Real:</label>
                                            <div class="col-md-16">
                                                <asp:DropDownList ID="cmbUsuarioReal" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker" data-live-search="true">
                                                    <asp:ListItem Text="-- SELECCIONE --" Value="0" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <label for="cmbUsuarioTemporal" class="form-control-sm col-md-8">Usuario Temporal:</label>
                                            <div class="col-md-16">
                                                <asp:DropDownList ID="cmbUsuarioTemporal" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                    data-live-search="true">
                                                    <asp:ListItem Text="-- SELECCIONE --" Value="0" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-md-48">
                                                <asp:UpdatePanel ID="udpFechaHasta" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                    <ContentTemplate>
                                                        <div class="row form-group">
                                                            <label for="dtpFechaHasta" class="form-control-sm col-md-8">Vigente Hasta:</label>
                                                            <div class="col-md-7">
                                                                <div class="input-group date" id="mrkFechaHasta" data-target-input="nearest">
                                                                    <input type="text" id="dtpFechaHasta" runat="server" class="form-control datetimepicker-input" data-target="#mrkFechaHasta" />
                                                                    <div class="input-group-append" data-target="#mrkFechaHasta" data-toggle="datetimepicker">
                                                                        <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="input-group date" id="mrkHoraHasta" data-target-input="nearest">
                                                                    <input type="text" id="dtpHoraHasta" runat="server" class="form-control datetimepicker-input" data-target="#mrkHoraHasta" />
                                                                    <div class="input-group-append" data-target="#mrkHoraHasta" data-toggle="datetimepicker">
                                                                        <div class="input-group-text"><i class="fa fa-clock"></i></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="custom-control custom-checkbox custom-control-inline">
                                                                    <input type="checkbox" id="chkHabilitarHasta" name="chkHabilitarHasta" class="custom-control-input" runat="server" value="1">
                                                                    <label class="custom-control-label form-control-sm" for="chkHabilitarHasta">Habilitar</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <hr class="separador">
                                        <div class="row form-group mb-2">
                                            <div class="col-md-40 offset-md-8">
                                                <button type="button" id="btnCancelar" runat="server" class="btn btn-accion btn-light">
                                                    <i class="fa fa-ban"></i>
                                                    <span class="text">Cancelar</span>
                                                </button>
                                                <button type="button" id="btnGuardar" runat="server" class="btn btn-accion btn-primary ml-2">
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
        <script src="../assets/jquery/jquery-3.3.1.js"></script>
        <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.min.js"></script>
        <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>
        <script src="../assets/bootstrap-select-1.13.1/js/i18n/defaults-es_ES.min.js"></script>
        <script src="../assets/moment/moment-with-locales.js"></script>
        <script src="../assets/tempusdominus-bootstrap/js/tempusdominus-bootstrap-4.js"></script>
        <script src="../assets/toastr-2.1.4-7/toastr.min.js"></script>
        <!-- Scripts propios -->
        <script src="private/js/funciones.js"></script>
        <script src="private/js/asignarUsuariosTemporales.js"></script>

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
                    case 'btnAgregar':
                        controlsId.push(controlId);
                        break;
                    case 'btnCancelar':
                        controlsId.push(controlId);
                        controlsId.push('btnGuardar');
                        break;
                    case 'btnGuardar':
                    case 'btnGuardarContinuar':
                        controlsId.push('btnCancelar');
                        controlsId.push('btnGuardar');
                        controlsId.push('btnGuardarContinuar');
                        break;
                }

                if (controlId.indexOf('btnEditar') > -1) {
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
                        case 'udpFechaHasta':
                            initFormDatetimepickers();
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

                if (controlId.indexOf('btnDesactivar') > -1
                    || controlId.indexOf('btnActivar') > -1
                    || controlId.indexOf('btnEliminar') > -1) {
                    accionConfirmadaFinalizada();
                }

                verificarParametros('TAB|MEN_SERV|TOASTR');
            });    
        </script>
    </body>

    </html>