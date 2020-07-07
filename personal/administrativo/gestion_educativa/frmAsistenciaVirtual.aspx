<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAsistenciaVirtual.aspx.vb" Inherits="administrativo_gestion_educativa_frmAsistenciaVirtual" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Asistencia Virtual ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
    <link rel="stylesheet" href="../../assets/toastr-2.1.4-7/toastr.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/asistenciaVirtual.css?x=3">
</head>

<body>
    <form id="frmAsistenciaVirtual" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Reporte de asistencia virtual<img src="img/loading.gif" id="loadingGif"></h1>
                    <hr>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-48">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="title">Filtros</h5>
                        </div>
                        <div class="card-body">
                            <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="row form-group">
                                        <div class="col-sm-12 d-flex">
                                            <label for="cmbCicloAcademico" class="form-control-sm">Semestre:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbCicloAcademico" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-18 d-flex">
                                            <label for="cmbCarreraProfesional" class="form-control-sm">Carrera:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbCarreraProfesional" runat="server" AutoPostBack="false"
                                                    CssClass="form-control form-control-sm selectpicker">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-18 d-flex">
                                            <label for="cmbDepartamentoAcademico" class="form-control-sm">Dep. Académico:</label>
                                            <div class="fill">
                                                <asp:DropDownList ID="cmbDepartamentoAcademico" runat="server" AutoPostBack="false"
                                                    CssClass="form-control form-control-sm selectpicker">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-sm-12 d-flex">
                                            <label for="dtpFechaDesde" class="form-control-sm">Fecha Desde:</label>
                                            <div class="fill">
                                                <asp:TextBox ID="dtpFechaDesde" runat="server" CssClass="form-control form-control-sm" placeholder="Desde" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12 d-flex">
                                            <label for="dtpFechaHasta" class="form-control-sm">Fecha Hasta:</label>
                                            <div class="fill">
                                                <asp:TextBox ID="dtpFechaHasta" runat="server" CssClass="form-control form-control-sm" placeholder="Hasta" />
                                            </div>
                                        </div>
                                        <div class="col-sm-24 text-right">
                                            <button type="button" id="btnListar" class="d-none" runat="server"></button>
                                            <button type="button" id="btnFakeListar" runat="server" class="btn btn-accion btn-primary">
                                                <i class="fa fa-search"></i>
                                                <span class="text">Mostrar Asistencia</span>
                                            </button>
                                            <button type="button" id="btnExportar" class="btn btn-accion btn-success">
                                                <i class="fa fa-file-excel"></i>
                                                <span class="text">Exportar</span>
                                            </button>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="row">
                                <div class="col-sm-48">
                                    <asp:UpdatePanel ID="udpAsistencia" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div id="divAsistencia" runat="server">
                                                <hr>
                                                <asp:GridView ID="grvAsistencia" runat="server" AutoGenerateColumns="false" CssClass="table table-sm" GridLines="None">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="NRO" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="nombre_Fac" HeaderText="FACULTAD" />
                                                        <asp:BoundField DataField="nombre_Dac" HeaderText="DEPARTAMENTO ACADÉMICO" />
                                                        <asp:BoundField DataField="ESCUELA" HeaderText="CARRERA" />
                                                        <asp:BoundField DataField="ASIGNATURA" HeaderText="ASIGNATURA" />
                                                        <asp:BoundField DataField="GRUPO" HeaderText="GRUPO" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="DOCENTE" HeaderText="DOCENTE" />
                                                        <asp:BoundField DataField="MODALIDAD VIRTUAL" HeaderText="MODALIDAD VIRTUAL" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="FECHA" HeaderText="FECHA" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="HORARIO" HeaderText="HORARIO" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="TOMO ASISTENCIA" HeaderText="TOMÓ ASISTENCIA" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="MATRICULADOS" HeaderText="MATRICULADOS" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="bold negro" />
                                                        <asp:BoundField DataField="Presente" HeaderText="PRESENTE" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="bold verde" />
                                                        <asp:BoundField DataField="Falto" HeaderText="FALTÓ" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="bold rojo" />
                                                        <asp:BoundField DataField="Tardanza" HeaderText="TARDANZA" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="bold naranja" />
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
    <script src="../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.min.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/i18n/defaults-es_ES.min.js"></script>
    <script src="../../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="../../assets/toastr-2.1.4-7/toastr.min.js"></script>
    <script src="../../assets/sheetjs/xlsx.full.min.js"></script>
    <!-- Scripts propios -->
    <script src="js/funciones.js"></script>
    <script src="js/asistenciaVirtual.js?x=6"></script>

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

            verificarMensajeServer();
        });    
    </script>
</body>

</html>