<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEventoVirtual.aspx.vb" Inherits="Alumni_frmEventoVirtual" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Mantenimiento de Eventos Virtuales ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
    <link rel="stylesheet" href="../assets/tempusdominus-bootstrap/css/tempusdominus-bootstrap-4.min.css">
    <link rel="stylesheet" href="../assets/toastr-2.1.4-7/toastr.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="otros/css/style.css">
    <link rel="stylesheet" href="otros/css/eventoVirtual.css?x=4">
</head>

<body>
    <form id="frmEventoVirtual" runat="server">
        <asp:ScriptManager ID="scmEventoVirtual" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpParams" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:HiddenField ID="hddCodEvi" Value="" runat="server" />
                <asp:HiddenField ID="hddTipoVista" Value="" runat="server" />
                <asp:HiddenField ID="hddParamsToastr" Value="" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Mantenimiento de Eventos Virtuales<img src="img/loading.gif" id="loadingGif"></h1>
                    <hr>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-48">
                    <nav>
                        <div class="nav nav-tabs" id="nav-tab" role="tablist">
                            <a class="nav-item nav-link active" id="nav-lista-tab" data-toggle="tab" href="#nav-lista" role="tab" aria-controls="nav-lista"
                                aria-selected="true">Lista de
                                Eventos</a>
                            <a class="nav-item nav-link disabled" id="nav-mantenimiento-tab" data-toggle="tab" href="#nav-mantenimiento" role="tab"
                                aria-controls="nav-mantenimiento" aria-selected="false">Mantenimiento de Evento</a>
                        </div>
                    </nav>
                    <div class="tab-content" id="nav-tabContent">
                        <div class="tab-pane fade show active" id="nav-lista" role="tabpanel" aria-labelledby="nav-lista-tab">
                            <div class="row">
                                <div class="col-sm-48">
                                    <div class="card">
                                        <div class="card-header">
                                            <h5 class="title">Filtros</h5>
                                        </div>
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-sm-16 d-flex">
                                                    <label for="txtFiltroNombre" class="form-control-sm">Nombre:</label>
                                                    <div class="fill">
                                                        <asp:TextBox ID="txtFiltroNombre" runat="server" CssClass="form-control form-control-sm" placeholder="Nombre del Evento" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-16 d-flex">
                                                    <label for="txtFiltroPonente" class="form-control-sm">Ponente:</label>
                                                    <div class="fill">
                                                        <asp:TextBox ID="txtFiltroPonente" runat="server" CssClass="form-control form-control-sm" placeholder="Ponente del Evento" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-16 text-right">
                                                    <asp:UpdatePanel ID="udpAcciones" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                        <ContentTemplate>
                                                            <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-azul">
                                                                <i class="fa fa-search"></i>
                                                                <span class="text">Listar</span>
                                                            </button>
                                                            <button type="button" id="btnRegistrar" runat="server" class="btn btn-accion btn-verde">
                                                                <i class="fa fa-plus-square"></i>
                                                                <span class="text">Nuevo</span>
                                                            </button>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-48">
                                                    <asp:UpdatePanel ID="udpEventos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                        <ContentTemplate>
                                                            <div id="divEventos" runat="server">
                                                                <hr>
                                                                <asp:GridView ID="grvEvento" runat="server" DataKeyNames="codigo_evi" AutoGenerateColumns="false"
                                                                    CssClass="table table-sm" GridLines="None">
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="NRO" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="nombre_evi" HeaderText="NOMBRE" />
                                                                        <asp:BoundField DataField="nombrePonente_evi" HeaderText="PONENTE" />
                                                                        <asp:BoundField HeaderText="FECHA Y HORA" />
                                                                        <asp:BoundField DataField="url_evi" HeaderText="ENLACE" />
                                                                        <asp:BoundField HeaderText="TIPO" />
                                                                        <asp:BoundField DataField="codigo_evi" HeaderText="CÓDIGO" />
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
                            </div>
                        </div>
                        <div class="tab-pane fade" id="nav-mantenimiento" role="tabpanel" aria-labelledby="nav-mantenimiento-tab">
                            <div class="card">
                                <div class="card-header">
                                    <h5 class="title">Mantenimiento</h5>
                                </div>
                                <div class="card-body">
                                    <asp:UpdatePanel ID="udpMantenimiento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div class="row form-group">
                                                <label for="txtNombre" class="form-control-sm col-sm-5">Nombre:</label>
                                                <div class="col-sm-16">
                                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-sm" placeholder="Nombre del Evento" />
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <label for="txtNombrePonente" class="form-control-sm col-sm-5">Ponente:</label>
                                                <div class="col-sm-16">
                                                    <asp:TextBox ID="txtNombrePonente" runat="server" CssClass="form-control form-control-sm" placeholder="Ponente del Evento" />
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <label for="dtpFecha" class="form-control-sm col-sm-5">Fecha:</label>
                                                <div class="col-sm-8">
                                                    <div class="input-group date" id="mrkFecha" data-target-input="nearest">
                                                        <input type="text" id="dtpFecha" runat="server" class="form-control datetimepicker-input" data-target="#mrkFecha" />
                                                        <div class="input-group-append" data-target="#mrkFecha" data-toggle="datetimepicker">
                                                            <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <label for="dtpHoraInicio" class="form-control-sm col-sm-4">Hora Inicio:</label>
                                                <div class="col-sm-6">
                                                    <div class="input-group date" id="mrkHoraInicio" data-target-input="nearest">
                                                        <input type="text" id="dtpHoraInicio" runat="server" class="form-control datetimepicker-input"
                                                            data-target="#mrkHoraInicio" />
                                                        <div class="input-group-append" data-target="#mrkHoraInicio" data-toggle="datetimepicker">
                                                            <div class="input-group-text"><i class="fa fa-clock"></i></div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <label for="dtpHoraFin" class="form-control-sm col-sm-4">Hora Fin:</label>
                                                <div class="col-sm-6">
                                                    <div class="input-group date" id="mrkHoraFin" data-target-input="nearest">
                                                        <input type="text" id="dtpHoraFin" runat="server" class="form-control datetimepicker-input" data-target="#mrkHoraFin" />
                                                        <div class="input-group-append" data-target="#mrkHoraFin" data-toggle="datetimepicker">
                                                            <div class="input-group-text"><i class="fa fa-clock"></i></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <label for="txtUrl" class="form-control-sm col-sm-5">Enlace:</label>
                                                <div class="col-sm-28">
                                                    <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control form-control-sm" placeholder="http://www.enlaceevento.com" />
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <label for="cmbTipo" class="form-control-sm col-sm-5">Tipo:</label>
                                                <div class="col-sm-14">
                                                    <asp:ListBox ID="cmbTipo" runat="server" AutoPostBack="false" SelectionMode="Multiple" data-live-search="true"
                                                        CssClass="form-control form-control-sm selectpicker">
                                                        <asp:ListItem Value="ES">ESTUDIANTES</asp:ListItem>
                                                        <asp:ListItem Value="EG">EGRESADOS</asp:ListItem>
                                                    </asp:ListBox>
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <div class="col-sm-20 offset-sm-6">
                                                    <asp:UpdatePanel ID="udpAccionesMantenimiento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                        <ContentTemplate>
                                                            <button type="button" id="btnFakeManGuardar" runat="server" class="btn btn-accion btn-azul">
                                                                <i class="fa fa-save"></i>
                                                                <span class="text">Guardar</span>
                                                            </button>
                                                            <button type="button" id="btnManGuardar" runat="server" class="btn d-none"></button>
                                                            <button type="button" id="btnManCancelar" runat="server" class="btn btn-accion btn-light">
                                                                <i class="fa fa-ban"></i>
                                                                <span class="text">Cancelar</span>
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
    <!-- <script src="../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script> -->
    <!-- <script src="../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script> -->
    <script src="../assets/moment/moment-with-locales.js"></script>
    <script src="../assets/tempusdominus-bootstrap/js/tempusdominus-bootstrap-4.min.js"></script>
    <script src="../assets/toastr-2.1.4-7/toastr.min.js"></script>
    <script src="../assets/sheetjs/xlsx.full.min.js"></script>
    <!-- Scripts propios -->
    <script src="js/funciones.js"></script>
    <script src="otros/js/eventoVirtual.js?x=4"></script>

    <script type="text/javascript">
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id

            alternarLoadingGif('global', false);

            switch (controlId) {
                case 'btnListar':
                    atenuarBoton(controlId, false);
                    break;
                case 'btnRegistrar':
                case 'btnManCancelar':
                    atenuarBoton(controlId, false);
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
                }
            }
        });

        Sys.Application.add_load(function () {
            var elem = document.getElementById(controlId);

            alternarLoadingGif('global', true);

            switch (controlId) {
                case 'btnListar':
                    atenuarBoton(controlId, true);
                    break;
                case 'btnRegistrar':
                    verificarParametros('TAB');
                    atenuarBoton(controlId, true);
                    break;
                case 'btnManCancelar':
                    verificarParametros('TAB');
                    atenuarBoton(controlId, true);
                    break;
                case 'btnManGuardar':
                    verificarParametros('TAB');
                    atenuarBoton(controlId, true);
                    break;
            }

            if (controlId.indexOf('btnEditar') > -1) {
                atenuarBoton(controlId, true);
                verificarParametros('TAB');
            }

            if (controlId.indexOf('btnEliminar') > -1) {
                eliminarProcesado();
            }

            verificarMensajeServer();
            verificarToastrServer();
        });    
    </script>
</body>

</html>