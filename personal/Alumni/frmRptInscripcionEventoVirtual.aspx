<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRptInscripcionEventoVirtual.aspx.vb" Inherits="administrativo_gestion_educativa_frmRptInscripcionEventoVirtual" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Reporte de inscripciones a eventos virtuales ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
    <link rel="stylesheet" href="../assets/toastr-2.1.4-7/toastr.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="otros/css/style.css">
    <link rel="stylesheet" href="otros/css/rptInscripcionEventoVirtual.css?x=1">
</head>

<body>
    <form id="form" runat="server">
        <asp:ScriptManager ID="scm" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpParams" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:HiddenField ID="hddParamsToastr" Value="" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-48">
                    <h1 class="main-title">Reporte de Inscritos en Eventos Virtuales<img src="img/loading.gif" id="loadingGif"></h1>
                    <hr>
                </div>
            </div>
            <div class="row">
                <div class="col-md-48">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="title">Filtros</h5>
                        </div>
                        <div class="card-body">
                            <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="row form-group">
                                        <div class="col-md-16 d-flex">
                                            <label for="cmbEventoVirtual" class="form-control-sm">Evento:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbEventoVirtual" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-10 d-flex">
                                            <label for="dtpFechaDesde" class="form-control-sm">Fecha Desde:</label>
                                            <div class="fill">
                                                <asp:TextBox ID="dtpFechaDesde" runat="server" CssClass="form-control form-control-sm" placeholder="Desde" />
                                            </div>
                                        </div>
                                        <div class="col-md-10 d-flex">
                                            <label for="dtpFechaHasta" class="form-control-sm">Fecha Hasta:</label>
                                            <div class="fill">
                                                <asp:TextBox ID="dtpFechaHasta" runat="server" CssClass="form-control form-control-sm" placeholder="Hasta" />
                                            </div>
                                        </div>
                                        <div class="col-md-12 text-right">
                                            <button type="button" id="btnListar" class="d-none" runat="server"></button>
                                            <button type="button" id="btnFakeListar" runat="server" class="btn btn-accion btn-primary">
                                                <i class="fa fa-search"></i>
                                                <span class="text">Listar</span>
                                            </button>
                                            <button type="button" id="btnExportar" class="btn btn-accion btn-success">
                                                <i class="fa fa-file-excel"></i>
                                                <span class="text">Exportar</span>
                                            </button>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="udpGrvInscripcionEvento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div id="divGrvInscripcionEvento" runat="server">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-sm-48">
                                                <asp:GridView ID="grvInscripcionEvento" runat="server" DataKeyNames="codigo_evi" AutoGenerateColumns="false"
                                                    CssClass="table table-sm" GridLines="None">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="NRO" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="nombre_evi" HeaderText="EVENTO" />
                                                        <asp:BoundField DataField="numDocIdentidad_iev" HeaderText="DNI" />
                                                        <asp:BoundField DataField="nombreCompleto_iev" HeaderText="NOMBRE COMPLETO" />
                                                        <asp:BoundField DataField="email_iev" HeaderText="EMAIL" />
                                                        <asp:BoundField DataField="celular_iev" HeaderText="CELULAR" />
                                                        <asp:BoundField DataField="estaTrabajando_iev" HeaderText="¿TRABAJANDO?" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="empresa_iev" HeaderText="EMPRESA" />
                                                        <asp:BoundField DataField="medioIngresoLaboral_iev" HeaderText="MEDIO INGRESO LAB." />
                                                        <asp:BoundField DataField="descripcion_tpar" HeaderText="TIPO DE PARTICIPANTE" />
                                                        <asp:BoundField DataField="obtenerConstancia_iev" HeaderText="¿CONSTANCIA?" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="medioInscripcion_iev" HeaderText="MEDIO DE INSCR." ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="fechaHoraReg_iev" HeaderText="FECHA DE INSCR." />
                                                    </Columns>
                                                    <HeaderStyle CssClass="thead-dark" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
    </form>
    <!-- Scripts externos -->
    <script src="../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.min.js"></script>
    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>
    <script src="../assets/bootstrap-select-1.13.1/js/i18n/defaults-es_ES.min.js"></script>
    <script src="../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="../assets/toastr-2.1.4-7/toastr.min.js"></script>
    <script src="../assets/sheetjs/xlsx.full.min.js"></script>
    <!-- Scripts propios -->
    <script src="js/funciones.js"></script>
    <script src="otros/js/rptInscripcionEventoVirtual.js?x=1"></script>

    <script type="text/javascript">
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id

            alternarLoadingGif('global', false);

            switch (controlId) {
                case 'btnListar':
                    atenuarBoton('btnFakeListar', false);
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
                }
            }
        });

        Sys.Application.add_load(function () {
            var elem = document.getElementById(controlId);

            alternarLoadingGif('global', true);

            switch (controlId) {
                case 'btnListar':
                    atenuarBoton('btnFakeListar', true);
                    break;
            }

            verificarCambiosAjax();
        });    
    </script>
</body>

</html>