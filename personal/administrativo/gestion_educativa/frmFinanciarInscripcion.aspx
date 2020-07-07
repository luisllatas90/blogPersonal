<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmFinanciarInscripcion.aspx.vb" Inherits="administrativo_pec_test_frmFinanciarInscripcion" %>

    <!DOCTYPE html>
    <html lang="en">

    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta http-equiv="X-UA-Compatible" content="ie=edge">
        <title>Financiar Inscripción</title>
        <!-- Estilos externos -->
        <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css">
        <link rel="stylesheet" href="../../assets/smart-wizard/css/smart_wizard.css">
        <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.css">
        <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
        <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
        <!-- Estilos propios -->
        <link rel="stylesheet" href="css/style.css?10">
        <link rel="stylesheet" href="css/financiarInscripcion.css?10">
    </head>

    <body>
        <form id="frmFinanciamiento" runat="server" class="frm-datos-postulante">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="udpForm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false" >
                <ContentTemplate>
                    <div id="errorMensaje" class="alert alert-danger d-none" runat="server"></div>
                    <div class="row">
                        <div class="col-sm-12">
                            <h6><span id="lblCentroCosto" runat="server" class="badge badge-light"></span></h6>
                        </div>
                    </div>
                    <div class="data-server" id="isPostBack" data-value="<%=Convert.toInt32(Page.isPostBack)%>"></div>
                     <div id="smartwizard">
                        <ul>
                            <li>
                                <a href="#step1">
                                    <small>Datos Generales</small>
                                </a>
                            </li>
                            <li>
                                <a href="#step2">
                                    <small>Forma de Pago</small>
                                </a>
                            </li>
                        </ul>
                        <div>
                            <div id="step1" class="tab-pane stepcontent">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <label for="cmbParticipante" class="col-sm-2 col-form-label form-control-sm">Cargar a Partic.</label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="cmbParticipante" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" />
                                                    </div>
                                                    <asp:Label ID="lblAlumno" runat="server" Visible="False"></asp:Label>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="cmbServicio" class="col-sm-2 col-form-label form-control-sm">Servicio
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="cmbServicio" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" />
                                                    </div>
                                                </div>
                                                <asp:UpdatePanel ID="udpStep1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false" >
                                                    <ContentTemplate>
                                                        <div class="form-group row">
                                                            <label for="txtPrecio" class="col-sm-2 col-form-label form-control-sm">Precio
                                                            </label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control form-control-sm" Style="text-align: right" />
                                                            </div>
                                                        </div>
                                                        <div id="cambioprecio" runat="server">
                                                            <div class="form-group row">
                                                                <label for="txtRecargo" class="col-sm-2 col-form-label form-control-sm">Recargo</label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox ID="txtRecargo" runat="server" CssClass="form-control form-control-sm decimal" Style="text-align: right" data-decimales="2" />
                                                                </div>
                                                                <label for="txtDescuento" class="col-sm-1 col-form-label form-control-sm">Dscto.</label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox ID="txtDescuento" runat="server" CssClass="form-control form-control-sm decimal" Style="text-align: right" data-decimales="2" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtTotal" class="col-sm-2 col-form-label form-control-sm">Total a Pagar</label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control form-control-sm" Style="text-align: right" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtObservacion" class="col-sm-2 col-form-label form-control-sm">Observación</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control form-control-sm" TextMode="multiline" Columns="50" Rows="3" />
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <asp:UpdatePanel ID="udpDeudas" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div class="card" id="divDatosDeudas" runat="server" Visible="False">
                                                    <div class="card-body">
                                                        <h6>Se han encontrado las siguientes deudas:</h6>
                                                        <asp:GridView ID="DgvDeudas" runat="server" AutoGenerateColumns="False" 
                                                            CssClass="table table-sm">
                                                            <HeaderStyle CssClass="thead-dark" />
                                                            <Columns>
                                                                <asp:BoundField DataField="FechaCargo" HeaderText="Fec. Cargo" />
                                                                <asp:BoundField DataField="Servicio" HeaderText="Servicio" />
                                                                <asp:BoundField DataField="FechaVencimiento" DataFormatString="{0:dd/MM/yyyy}" 
                                                                    HeaderText="Fec. Vence" />
                                                                <asp:BoundField DataField="Cargo" HeaderText="Cargo">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Abonos" HeaderText="Abonos">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Saldo" HeaderText="Saldo">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Observacion" HeaderText="Observacion" />
                                                                <asp:BoundField DataField="codigo_Deu" HeaderText="Codigo Deu" 
                                                                    Visible="false" />
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:GridView ID="DgvDeudas1" runat="server" AutoGenerateColumns="False" 
                                                            CssClass="table table-sm">
                                                            <HeaderStyle CssClass="thead-dark" />
                                                            <Columns>
                                                                <asp:BoundField DataField="descripcion_Sco" HeaderText="Servicio" />
                                                                <asp:BoundField DataField="montoTotal_Deu" HeaderText="Deuda" />
                                                                <asp:BoundField DataField="Pago_Deu" HeaderText="Pago" />
                                                                <asp:BoundField DataField="Saldo_Deu" HeaderText="Saldo" />
                                                                <asp:BoundField DataField="fechaVencimiento_Deu" HeaderText="Fec. Vence" DataFormatString="{0:dd/MM/yyyy}" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div id="step2" class="tab-pane stepcontent">
                                <asp:UpdatePanel ID="udpStep2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false" >
                                    <ContentTemplate>
                                        <div class="card">
                                            <div class="card-body">
                                                <div class="form-group row">
                                                    <label for="chkUnaCuota" class="col-sm-2 col-form-label form-control-sm">Una sola Cuota</label>
                                                    <div class="col-sm-4">
                                                        <asp:CheckBox ID="chkUnaCuota" runat="server" Checked />
                                                    </div>
                                                </div>
                                                <div class="form-group row" id="divUnaCuota">
                                                    <label for="txtCuota" class="col-sm-3 col-form-label form-control-sm">Cuota Única</label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtCuota" runat="server" CssClass="form-control form-control-sm" Style="text-align: right" Enabled ="false" />
                                                    </div>
                                                    <label for="dtpFecVenc" class="col-sm-2 col-form-label form-control-sm">Fec.
                                                        Vencimiento</label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="dtpFecVenc" runat="server" CssClass="form-control form-control-sm" />
                                                    </div>
                                                </div>
                                                <div id="divMasCuotas">
                                                    <div class="form-group row">
                                                        <label for="txtCuotaInicial" class="col-sm-3 col-form-label form-control-sm">Cuota Inicial</label>
                                                        <div class="col-sm-2">
                                                            <asp:TextBox ID="txtCuotaInicial" runat="server" CssClass="form-control form-control-sm" Style="text-align: right" />
                                                        </div>
                                                    
                                                        <label for="dtpFecVencInicial" class="col-sm-3 col-form-label form-control-sm">Fec.
                                                            Vencimiento Cuota Inicial</label>
                                                        <div class="col-sm-2">
                                                            <asp:TextBox ID="dtpFecVencInicial" runat="server" CssClass="form-control form-control-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group row" >
                                                        <label for="txtSaldo" class="col-sm-3 col-form-label form-control-sm">Saldo a Financiar</label>
                                                        <div class="col-sm-2">
                                                            <asp:TextBox ID="txtSaldo" runat="server" CssClass="form-control form-control-sm" Style="text-align: right"  Enabled=false />
                                                        </div>
                                                        <label for="cmbCuotas" class="col-sm-3 col-form-label form-control-sm">Cuotas</label>
                                                        <div class="col-sm-2">
                                                            <asp:DropDownList ID="cmbCuotas" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="cmbTipoCuota" class="col-sm-3 col-form-label form-control-sm">Tipo de Cuota</label>
                                                        <div class="col-sm-3">
                                                            <asp:DropDownList ID="cmbTipoCuota" runat="server" CssClass="form-control form-control-sm">
                                                                <asp:ListItem Value="-1">-- Seleccione --</asp:ListItem>
                                                                <asp:ListItem Value="F">FIJA</asp:ListItem>
                                                                <asp:ListItem Value="V">VARIABLE</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <label for="cmbTipoPeriodo" class="col-sm-2 col-form-label form-control-sm">Tipo de Periodo</label>
                                                        <div class="col-sm-3">
                                                            <asp:DropDownList ID="cmbTipoPeriodo" runat="server" CssClass="form-control form-control-sm">
                                                                <asp:ListItem Value="-1">-- Seleccione --</asp:ListItem>
                                                                <asp:ListItem Value="F">FIJO</asp:ListItem>
                                                                <asp:ListItem Value="V">VARIABLE</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="cmbDiaPago" class="col-sm-3 col-form-label form-control-sm">Día de Pago</label>
                                                        <div class="col-sm-2">
                                                            <asp:DropDownList ID="cmbDiaPago" runat="server" CssClass="form-control form-control-sm" >
                                                                <asp:ListItem>01</asp:ListItem>
                                                                <asp:ListItem>02</asp:ListItem>
                                                                <asp:ListItem>03</asp:ListItem>
                                                                <asp:ListItem>04</asp:ListItem>
                                                                <asp:ListItem>05</asp:ListItem>
                                                                <asp:ListItem>06</asp:ListItem>
                                                                <asp:ListItem>07</asp:ListItem>
                                                                <asp:ListItem>08</asp:ListItem>
                                                                <asp:ListItem>09</asp:ListItem>
                                                                <asp:ListItem>10</asp:ListItem>
                                                                <asp:ListItem>11</asp:ListItem>
                                                                <asp:ListItem>12</asp:ListItem>
                                                                <asp:ListItem>13</asp:ListItem>
                                                                <asp:ListItem>14</asp:ListItem>
                                                                <asp:ListItem>15</asp:ListItem>
                                                                <asp:ListItem>16</asp:ListItem>
                                                                <asp:ListItem>17</asp:ListItem>
                                                                <asp:ListItem>18</asp:ListItem>
                                                                <asp:ListItem>19</asp:ListItem>
                                                                <asp:ListItem>20</asp:ListItem>
                                                                <asp:ListItem>21</asp:ListItem>
                                                                <asp:ListItem>22</asp:ListItem>
                                                                <asp:ListItem>23</asp:ListItem>
                                                                <asp:ListItem>24</asp:ListItem>
                                                                <asp:ListItem>25</asp:ListItem>
                                                                <asp:ListItem>26</asp:ListItem>
                                                                <asp:ListItem>27</asp:ListItem>
                                                                <asp:ListItem>28</asp:ListItem>
                                                                <asp:ListItem>29</asp:ListItem>
                                                                <asp:ListItem>30</asp:ListItem>
                                                            
                                                                </asp:DropDownList>   
                                                        </div>
                                                        <label for="cmbMesCuota" class="col-sm-3 col-form-label form-control-sm">Mes 1ra Cuota</label>
                                                        <div class="col-sm-3">
                                                            <asp:DropDownList ID="cmbMesCuota" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" />
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <div class="col-sm-5">
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <asp:Button ID="btnCalcular" runat="server" Text="Generar Cuotas"  CssClass="btn btn-primary submit" ValidationGroup="Generar" Width="126px" >
                                                            </asp:Button>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="col-sm-3">
                                                        </div>
                                                        <div class="col-sm-6 d-none" id="divCuotas" runat="server" >
                                                            <asp:Table ID="TblCuotas" runat="server" >
                                                            </asp:Table>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="col-sm-3">
                                                            <asp:Label ID="lblMensaje" runat="server" class="badge badge-light"></asp:Label>
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
                    <div id="botonesAccion" runat="server" class="card-footer">
                        <div class="form-group row">
                            <div class="col-sm-10 offset-sm-2">
                                <button id="btnCancelar" runat="server" class="btn btn-outline-secondary">Cancelar</button>
                                <asp:Button ID="btnRegistrar" runat="server" UseSubmitBehavior="false" CssClass="btn btn-primary" Text="Registrar" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="mdlMensajesCliente" class="modal fade" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-md" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <span class="modal-title">Mensaje</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div id="mensaje" class="alert alert-light"></div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="mdlMensajeServidor" class="modal fade" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <span class="modal-title">Respuesta del Servidor</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="udpMensajeServidor" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div id="respuestaPostback" runat="server" class="alert" data-ispostback="false" data-rpta="" data-msg=""></div>
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
        <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
        <script src="../../assets/smart-wizard/js/jquery.smartWizard.js"></script>
        <script src="../../assets/jquery-validation/jquery.validate.min.js"></script>
        <script src="../../assets/jquery-validation/localization/messages_es.min.js"></script>
        <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>
        <script src="../../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
        <script src="../../assets/iframeresizer/iframeResizer.contentWindow.min.js"></script>
        <!-- Scripts propios -->
        <script src="js/funciones.js?1"></script>
        <script src="js/financiarInscripcion.js?10"></script>
        <script type="text/javascript">
            var controlId = ''
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
                var elem = args.get_postBackElement();
                controlId = elem.id
                switch (controlId) {
                }
            });

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function(sender, args) {
                var error = args.get_error();
                if (error) {
                    // Manejar el error
                }
            });

            Sys.Application.add_load(function() {
                InicializarControles();
                CambioCuotas();
                switch (controlId) {
                    case 'lnkObtenerDatos':
                        DatosBuscados();
                        break;
                    case 'cmbServicio': 
                        CalcularTotal();
                    case 'btnRegistrar':
                        SubmitPostBack();
                        break;
//                    case 'chkUnaCuota':
//                        CambioCuotas();
//                        break;
                }
            });
        </script>
    </body>
    </html>