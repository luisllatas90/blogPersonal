<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmInscripcionContinua.aspx.vb" Inherits="administrativo_pec_test_frmInscripcionContinua" %>

    <!DOCTYPE html>
    <html lang="es">

    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>Inscripción a Programas de Educación Continua</title>
        <!-- Estilos externos -->
        <link rel="stylesheet" href="assets/bootstrap-4.1/css/bootstrap.min.css">
        <link rel="stylesheet" href="assets/smart-wizard/css/smart_wizard.css">
        <link rel="stylesheet" href="assets/bootstrap-select-1.13.1/css/bootstrap-select.css">
        <link rel="stylesheet" href="assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
        <link rel="stylesheet" href="assets/fontawesome-5.2/css/all.min.css">
        <!-- Estilos propios -->
        <link rel="stylesheet" href="css/style.css">
        <link rel="stylesheet" href="css/postulantes.css">
    </head>

    <body>
        <form id="frmInscripcionContinua" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="container-fluid">
                <asp:UpdatePanel ID="udpCabecera" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="card">
                            <div class="card-body">
                                <h6>Inscripción a Programas de Educación Continua</h6>
                                <hr>
                                <div class="row">
                                    <label for="centroCosto" class="col-sm-2 col-form-label form-control-sm">
                                        Centro de Costo:</label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="cmbCentroCosto" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <ul class="nav nav-tabs" role="tablist">
                    <li class="nav-item">
                        <a href="#postulantes" id="postulantes-tab" class="nav-link active" data-toggle="tab" role="tab" aria-controls="postulantes"
                            aria-selected="true">Inscritos</a>
                    </li>
                    <li class="nav-item">
                        <a href="#ingresantes" id="ingresantes-tab" class="nav-link" data-toggle="tab" role="tab" aria-controls="ingresantes" aria-selected="false">Ingresantes</a>
                    </li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane show active" id="postulantes" role="tabpanel" aria-labelledby="postulantes-tab">
                        <img src="img/loading.gif" id="loading-gif">
                        <div class="card">
                            <div class="card-header">Lista de Inscritos</div>
                            <div class="card-body">
                                <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <button type="button" id="btnRegistrarPostulante" runat="server" class="btn btn-sm btn-outline-secondary">
                                                    <i class="fa fa-plus-square"></i>
                                                    <span class="text">Registrar Inscrito</span>
                                                </button>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="row">
                                                    <label for="cmbEstadoAlumno" class="col-sm-2 col-form-label form-control-sm">Estado:</label>
                                                    <div class="col-sm-2">
                                                        <asp:DropDownList ID="cmbEstadoAlumno" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm">
                                                            <asp:ListItem Value="T">TODOS</asp:ListItem>
                                                            <asp:ListItem Value="A">ACTIVOS</asp:ListItem>
                                                            <asp:ListItem Value="I">INACTIVOS</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label for="txtFiltroPostulantes" class="col-sm-3 col-form-label form-control-sm">DNI/Apellidos y Nombres:</label>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="txtFiltroPostulantes" runat="server" CssClass="form-control form-control-sm" placeholder="Filtrar" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="udpPostulantes" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <button type="button" id="btnRefrescarGrillaPostulantes" runat="server" class="btn btn-outline-primary d-none">Refrescar</button>
                                <asp:GridView ID="grwPostulantes" runat="server" AutoGenerateColumns="false" DataKeyNames="codigo_pso, cli" CssClass="table table-sm"
                                    GridLines="None">
                                    <Columns>
                                        <asp:BoundField HeaderText="Nro" />
                                        <asp:BoundField DataField="TipoDoc" HeaderText="Tipo Doc." />
                                        <asp:BoundField DataField="Nrodoc" HeaderText="Nro Doc." />
                                        <asp:BoundField DataField="Participante" HeaderText="Participante" />
                                        <asp:BoundField DataField="CodUniversitario" HeaderText="Cód. Univ." />
                                        <asp:BoundField DataField="cicloIng_Alu" HeaderText="Ciclo Ingreso" />
                                        <asp:BoundField DataField="CargoTotal" HeaderText="Cargo Total" />
                                        <asp:BoundField DataField="AbonoTotal" HeaderText="Abono Total" />
                                        <asp:BoundField DataField="SaldoTotal" HeaderText="Saldo Total" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion" HeaderText="Mov." />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion" HeaderText="Edit." />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion" HeaderText="Impr." />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion" HeaderText="Requ." />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion" HeaderText="Conv." />
                                    </Columns>
                                    <HeaderStyle CssClass="thead-dark" />
                                </asp:GridView>
                                <div id="errorMensaje" runat="server"></div>
                                <div id="mdlMovimientos" class="modal fade" tabindex="-1" role="dialog">
                                    <div class="modal-dialog modal-lg" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <span class="modal-title">Movimientos</span>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <asp:GridView ID="grwMovimientosPostulante" runat="server" AutoGenerateColumns="false" CssClass="table table-sm" GridLines="None">
                                                    <Columns>
                                                        <asp:BoundField DataField="Servicio" HeaderText="Servicio" ReadOnly="True" />
                                                        <asp:BoundField DataField="codigo_Deu" HeaderText="Codigo Deuda" ReadOnly="True" />
                                                        <asp:BoundField DataField="Documento" HeaderText="Documento" ReadOnly="True" />
                                                        <asp:BoundField DataField="Fecha_Operacion" HeaderText="Fecha Operacion" ReadOnly="True" />
                                                        <asp:BoundField DataField="Cargos" HeaderText="Cargos" ReadOnly="True" />
                                                        <asp:BoundField DataField="Abonos" HeaderText="Abonos" ReadOnly="True" />
                                                        <asp:BoundField DataField="Saldo" HeaderText="Saldo" ReadOnly="True" />
                                                        <asp:BoundField DataField="Trans" HeaderText="Transf." ReadOnly="True" />
                                                        <asp:BoundField DataField="observacion_Deu" HeaderText="Observacion" ReadOnly="True" />
                                                        <asp:TemplateField HeaderText="Genera Mora" />
                                                        <asp:BoundField DataField="FechaVenc" HeaderText="Fecha Venc." ReadOnly="True" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-primary" data-dismiss="modal">Cerrar</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div id="mdlInscripcionPostulante" class="modal fade" tabindex="-1" role="dialog">
                            <div class="modal-dialog modal-lg" role="document">
                                <asp:UpdatePanel ID="udpInscripcionPostulante" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <span class="modal-title">Formulario de registro</span>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <iframe src="" id="ifrmInscripcionPostulante" runat="server" frameborder="0" width="100%" scrolling="no"></iframe>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-primary" id="btnRegistrarPostulanteModal">Registrar</button>
                                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div id="mdlMantenimientoPostulante" class="modal fade" tabindex="-1" role="dialog">
                            <div class="modal-dialog modal-lg" role="document">
                                <asp:UpdatePanel ID="udpMantenimientoPostulante" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <span class="modal-title">Formulario de registro</span>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <iframe src="" id="ifrmMantenimientoPostulante" runat="server" frameborder="0" width="100%" scrolling="no"></iframe>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="ingresantes" role="tabpanel" aria-labelledby="ingresantes-tab">
                        <table class="table table-sm">
                            <tr>
                                <th>N°</th>
                                <th>N° Doc</th>
                                <th>Participante</th>
                                <th>Cód. Univ.</th>
                                <th>Ciclo Ingr.</th>
                                <th>Cargo Total</th>
                                <th>Abono Total</th>
                                <th>Saldo Total</th>
                                <th>Movs.</th>
                                <th>Editar</th>
                                <th>Imprimir</th>
                                <th>Requisitos</th>
                                <th>Convenio</th>
                            </tr>
                            <tr>
                                <td>1</td>
                                <td>45597882</td>
                                <td>Ramirez Mendoza Miguel</td>
                                <td>CH45597882</td>
                                <td>2018-II</td>
                                <td>150.00</td>
                                <td>150.00</td>
                                <td>0.00</td>
                                <td class="text-center">
                                    <button type="button" class="btn btn-info btn-sm">
                                        <i class="fa fa-search-plus"></i>
                                    </button>
                                </td>
                                <td class="text-center">
                                    <button type="button" class="btn btn-primary btn-sm">
                                        <i class="fa fa-edit"></i>
                                    </button>
                                </td>
                                <td class="text-center">
                                    <button type="button" class="btn btn-success btn-sm">
                                        <i class="fa fa-print"></i>
                                    </button>
                                </td>
                                <td class="text-center">
                                    <button type="button" class="btn btn-secondary btn-sm">
                                        <i class="fa fa-check-square"></i>
                                    </button>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>3</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>4</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>5</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div id="mdlMensajes" class="modal fade" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-sm" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <span class="modal-title">Mensaje</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div id="mensajePostBack" class="alert alert-light"></div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>
        </form>
        <!-- Scripts externos -->
        <script src="assets/jquery/jquery-3.3.1.js"></script>
        <script src="assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
        <script src="assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>
        <script src="assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
        <script src="assets/iframeresizer/iframeResizer.min.js"></script>
        <!-- Scripts propios -->
        <script src="js/funciones.js"></script>
        <script src="js/postulantes.js"></script>
        <script type="text/javascript">
            var controlId = ''
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
                var elem = args.get_postBackElement();

                controlId = elem.id
                switch (controlId) {
                    case 'cmbCentroCosto':
                    case 'cmbEstadoAlumno':
                    case 'btnRefrescarGrillaPostulantes':
                        CargandoPanelPostulantes()
                        break;
                }
            });

            Sys.Application.add_load(function () {
                switch (controlId) {
                    case 'cmbCentroCosto':
                    case 'cmbEstadoAlumno':
                    case 'btnRefrescarGrillaPostulantes':
                        PanelPostulantesCargado();
                        break;
                    case 'btnRegistrarPostulante':
                        InitModalInscripcionPostulante();
                        CargarModalInscripcionPostulante();
                }

                if (controlId.indexOf('btnInfoPostulante') > -1) {
                    InitModalMovimientos();
                    CargarModalMovimientos();
                    FiltrarGrillaPostulantes();
                }

                if (controlId.indexOf('btnEditarPostulante') > -1) {
                    InitModalMantenimientoPostulante();
                    CargarModalMantenimientoPostulante();
                    FiltrarGrillaPostulantes();
                }
            });

            // function DoPostPack(controlId) {
            //     console.log(controlId);
            //     __doPostBack(controlId, '');
            // }
        </script>
    </body>

    </html>