<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmInscritos.aspx.vb" Inherits="administrativo_pec_test_frmInscritos" %>

    <!DOCTYPE html>
    <html lang="es">

    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>Actualizar Datos Inscrito</title>
        <!-- Estilos externos -->
        <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css">
        <link rel="stylesheet" href="../../assets/smart-wizard/css/smart_wizard.css">
        <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.css">
        <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css">
        <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
        <!-- Estilos propios -->
        <link rel="stylesheet" href="css/style.css">
        <link rel="stylesheet" href="css/inscritos.css?1">
    </head>

    <body>
        <form id="frmPostulantes" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
             <input type="hidden" id="urlMod" name="urlMod" runat="server">
            <input type="hidden" id="urlId" name="urlId" runat="server">
            <input type="hidden" id="urlCtf" name="urlCtf" runat="server">
            <div class="container-fluid">
                <asp:UpdatePanel ID="udpCabecera" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="card">
                            <div class="card-body">
                                <img src="img/loading.gif" id="loading-gif">
                                <h6>Lista de interesados inscritos con cargo generado</h6>
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
                            aria-selected="true">Interesados</a>
                    </li>
                    <li class="nav-item">
                        <a href="#ingresantes" id="ingresantes-tab" class="nav-link" data-toggle="tab" role="tab" aria-controls="ingresantes" aria-selected="false">Inscritos</a>
                    </li>
                </ul>
                <div class="tab-content" id="contentTabs">
                    <div class="tab-pane show active" id="postulantes" role="tabpanel" aria-labelledby="postulantes-tab">
                        <img src="img/loading.gif" class="loading-gif">
                        <div class="card">
                            <div class="card-header">Lista de interesados con cargo generado</div>
                            <div class="card-body">
                                <asp:UpdatePanel ID="udpFiltrosInteresados" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <asp:UpdatePanel ID="udpBotonesInteresados" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                    <ContentTemplate>
                                                        <button type="button" id="btnListarPostulante" runat="server" class="btn btn-accion btn-celeste">
                                                            <i class="fa fa-sync-alt"></i>
                                                            <span class="text">Listar</span>
                                                        </button>
                                                        <button type="button" id="btnRegistrarPostulante" runat="server" class="btn btn-accion btn-azul">
                                                            <i class="fa fa-plus-square"></i>
                                                            <span class="text">Inscribir</span>
                                                        </button>
                                                        <button type="button" id="btnExportarInteresados" runat="server" class="btn btn-accion btn-verde">
                                                            <i class="fa fa-file-export"></i>
                                                            <span class="text">Exportar</span>
                                                        </button>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="row justify-content-end">
                                                    <label for="txtFiltroDNI" class="col-sm-1 col-form-label form-control-sm">DNI:</label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtFiltroDNI" runat="server" CssClass="form-control form-control-sm" placeholder="DNI" />
                                                    </div>
                                                    <label for="cmbEstadoAlumno" class="col-sm-2 col-form-label form-control-sm">Estado:</label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="cmbEstadoAlumno" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm">
                                                            <asp:ListItem Value="T">TODOS</asp:ListItem>
                                                            <asp:ListItem Value="A">ACTIVOS</asp:ListItem>
                                                            <asp:ListItem Value="I">INACTIVOS</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <hr>
                                        <div class="row">
                                            <div class="col-sm-2">
                                                <input type="text" id="txtFiltroPostulantes" runat="server" class="form-control form-control-sm" placeholder="Filtrar en tabla" />
                                            </div>
                                            <label for="cmbFilasPorPaginaPostu" class="offset-sm-8 col-sm-1 col-form-label form-control-sm">Filas:</label>
                                            <div class="col-sm-1">
                                                <asp:DropDownList ID="cmbFilasPorPaginaPostu" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm">
                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="50" Selected="True">50</asp:ListItem>
                                                    <asp:ListItem Value="0">- ∞ -</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="udpPostulantes" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:GridView ID="grwPostulantes" runat="server" AutoGenerateColumns="false" DataKeyNames="codigo_pso, cli" CssClass="table table-sm"
                                    GridLines="None">
                                    <Columns>
                                        <asp:BoundField HeaderText="Nro" />
                                        <asp:BoundField DataField="TipoDoc" HeaderText="Tipo Doc." />
                                        <asp:BoundField DataField="Nrodoc" HeaderText="Nro Doc." />
                                        <asp:BoundField DataField="Participante" HeaderText="Participante" />
                                        <asp:BoundField DataField="CodigoUniver_Alu" HeaderText="Cód. Univ." />
                                        <asp:BoundField DataField="cicloIng_Alu" HeaderText="Ciclo Ingreso" />
                                        <asp:BoundField DataField="CargoTotal" HeaderText="Cargo Total" />
                                        <asp:BoundField DataField="AbonoTotal" HeaderText="Abono Total" />
                                        <asp:BoundField DataField="SaldoTotal" HeaderText="Saldo Total" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion" HeaderText="Mov." />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion" HeaderText="Edit." />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion" HeaderText="Gen. Conv." /> 
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion" HeaderText="Finan." /> 
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion" HeaderText="Impr." />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion" HeaderText="Conv." />
                                    </Columns>
                                    <HeaderStyle CssClass="thead-dark" />
                                </asp:GridView>
                                <asp:UpdatePanel ID="udpPgrPostulantes" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div id="rowPagination" runat="server" class="row">
                                            <div class="col-sm-3">
                                                <h6><span id="lblPaginacion" runat="server" class="badge badge-light"></span></h6>
                                            </div>
                                            <div class="col-sm-9">
                                                <nav>
                                                    <ul id="pgrPostulantes" runat="server" class="pagination pagination-sm justify-content-end"></ul>
                                                </nav>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div id="errorMensaje" runat="server"></div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div id="mdlMovimientosPostulante" class="modal fade" tabindex="-1" role="dialog">
                            <div class="modal-dialog modal-lg" role="document">
                                <asp:UpdatePanel ID="udpMovimientosPostulante" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
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
                                                        <%-- <asp:BoundField DataField="Trans" HeaderText="Transf." ReadOnly="True" /> --%>
                                                        <%-- <asp:BoundField DataField="observacion_Deu" HeaderText="Observacion" ReadOnly="True" /> --%>
                                                        <%-- <asp:TemplateField HeaderText="Genera Mora" /> --%>
                                                        <asp:BoundField DataField="FechaVenc" HeaderText="Fecha Venc." ReadOnly="True" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div id="mdlInscripcionPostulante" class="modal fade" tabindex="-1" role="dialog" data-postback-listar="true">
                            <div class="modal-dialog modal-lg" role="document">
                                <asp:UpdatePanel ID="udpInscripcionPostulante" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
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
                                                <button type="button" class="btn btn-primary submit" id="btnRegistrarPostulanteModal">Registrar</button>
                                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div id="mdlMantenimientoPostulante" class="modal fade" tabindex="-1" role="dialog">
                            <div class="modal-dialog modal-lg" role="document">
                                <asp:UpdatePanel ID="udpMantenimientoPostulante" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <span class="modal-title">Actualizar Datos</span>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <iframe src="" id="ifrmMantenimientoPostulante" runat="server" frameborder="0" width="100%" scrolling="no"></iframe>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-primary submit" id="btnModificarPostulanteModal">Registrar</button>
                                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div id="mdlFinanciamiento" class="modal fade" tabindex="-1" role="dialog">
                            <div class="modal-dialog modal-lg" role="document">
                                <asp:UpdatePanel ID="udpFinanciamiento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <span class="modal-title">Financiar Inscripción</span>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <iframe src="" id="ifrmFinanciamiento" runat="server" frameborder="0" width="100%" scrolling="no"></iframe>
                                            </div>
                                            <%-- <div class="modal-footer">
                                                <button type="button" class="btn btn-primary submit" id="btnGuardarRequisitosAdmision">Guardar</button>
                                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                            </div> --%>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="udpFichaInscripcion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <iframe src="" id="ifrmFichaInscripcion" runat="server" frameborder="0" width="100%" scrolling="no"></iframe>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="mdlGenerarConvenio" class="modal fade" tabindex="-1" role="dialog">
                        <div class="modal-dialog modal-lg" role="document">
                            <asp:UpdatePanel ID="udpGenerarConvenio" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <span class="modal-title">Generar Convenio</span>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <iframe src="" id="ifrmGenerarConvenio" runat="server" frameborder="0" width="100%" scrolling="no"></iframe>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="tab-pane" id="ingresantes" role="tabpanel" aria-labelledby="ingresantes-tab">
                        <img src="img/loading.gif" class="loading-gif">
                        <div class="card">
                            <div class="card-header">Lista de interesados con inscripción pagada</div>
                            <div class="card-body">
                                <asp:UpdatePanel ID="udpBotonesIngresantes" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <button type="button" id="btnListarIngresante" runat="server" class="btn btn-accion btn-celeste">
                                                    <i class="fa fa-sync-alt"></i>
                                                    <span class="text">Listar</span>
                                                </button>
                                                <button type="button" id="btnExportarIngresantes" runat="server" class="btn btn-accion btn-verde">
                                                    <i class="fa fa-file-export"></i>
                                                    <span class="text">Exportar</span>
                                                </button>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="row">
                                                    <label for="cmbEstadoAlumno" class="col-sm-2 col-form-label form-control-sm">Estado:</label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm">
                                                            <asp:ListItem Value="T">TODOS</asp:ListItem>
                                                            <asp:ListItem Value="A">ACTIVOS</asp:ListItem>
                                                            <asp:ListItem Value="I">INACTIVOS</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control form-control-sm" placeholder="Filtrar" />
                                                    </div>
                                                     <label for="cmbFilasPorPaginaPostu" class="col-sm-1 col-form-label form-control-sm">Filas:</label>
                                                    <div class="col-sm-2">
                                                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm">
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                            <asp:ListItem Value="0">TODOS</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <%--<div class="card-body">
                                <asp:UpdatePanel ID="udpFiltrosIngresantes" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div class="form-group row">
                                            <label for="cmbCicloAcademico" class="col-form-label form-control-sm col-sm-1">Semestre:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbCicloAcademico" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" />
                                            </div>
                                            <label for="cmbModalidadIngreso" class="col-form-label form-control-sm col-sm-1">Modalidad:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbModalidadIngreso" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" />
                                            </div>
                                            <label for="cmbCarreraProfesional" class="col-form-label form-control-sm col-sm-1">Carrera:</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="cmbCarreraProfesional" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" />
                                            </div>
                                        </div>
                                        <hr>
                                        <div class="form-group row">
                                            <div class="col-sm-6">
                                                <button type="button" id="btnListarIngresante" runat="server" class="btn btn-sm btn-outline-secondary">
                                                    <i class="fa fa-sync-alt"></i>
                                                    <span class="text">Listar</span>
                                                </button>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtFiltroIngresantes" runat="server" CssClass="form-control form-control-sm" placeholder="Filtrar" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>--%>
                        </div>
                        <asp:UpdatePanel ID="udpIngresantes" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:GridView ID="grwIngresantes" runat="server" AutoGenerateColumns="false" DataKeyNames="codigo_pso, codigo_Alu" CssClass="table table-sm"
                                    GridLines="None">
                                    <Columns>
                                        <asp:BoundField HeaderText="Nro" />
                                        <asp:BoundField DataField="TipoDoc" HeaderText="Tipo Doc." />
                                        <asp:BoundField DataField="Nrodoc" HeaderText="Nro. Doc." />
                                        <asp:BoundField HeaderText="Participante" DataField="participante" />            
                                        <asp:BoundField DataField="codUniversitario" HeaderText="Cód. Univ." />
                                        <asp:BoundField DataField="carrera" HeaderText="Escuela" />
                                        <asp:BoundField DataField="nombre_Min" HeaderText="Modalidad Ingreso" />
                                        <asp:BoundField DataField="CentroCosto" HeaderText="Centro Costo" />
                                        <asp:BoundField DataField="CicloIngreso" HeaderText="Ciclo Ingreso" />
                                        <asp:BoundField DataField="fechaRegistro_Dal" DataFormatString={0:g}  HeaderText="Fecha Registro" />
                                        <asp:BoundField DataField="usureg_Dal" HeaderText="Usuario Registro" />
                                        <asp:BoundField DataField="EstadoPostulacion" HeaderText="Estado" />  
                                    </Columns>
                                    <HeaderStyle CssClass="thead-dark" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
            <div id="mdlMensajeServidor" class="modal fade" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-md" role="document">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="udpMensajeServidorParametros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div id="divMdlMenServParametros" runat="server" data-mostrar="false"></div>
                            </ContentTemplate>  
                        </asp:UpdatePanel> 
                        <div class="modal-header">
                            <asp:UpdatePanel ID="udpMensajeServidorHeader" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <span id="spnMensajeServidorTitulo" runat="server" class="modal-title">Respuesta del Servidor</span>
                                </ContentTemplate>  
                            </asp:UpdatePanel>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="udpMensajeServidorBody" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div id="mensajeServer" class="alert alert-warning" runat="server"></div>
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
        <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>
        <script src="../../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
        <script src="../../assets/iframeresizer/iframeResizer.min.js"></script>
        <script src="../../assets/fileDownload/jquery.fileDownload.js"></script>
        <!-- Scripts propios -->
        <script src="js/funciones.js"></script>
        <script src="js/inscritos.js"></script>
        <script type="text/javascript">
            var controlId = ''
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
                var elem = args.get_postBackElement();

                controlId = elem.id
                switch (controlId) {
                    case 'cmbEstadoAlumno':
                        CargandoPanelPostulantes();
                    case 'btnListarPostulante':
                        AtenuarBoton(controlId, false);
                        CargandoPanelPostulantes();
                        break;
                    case 'btnRegistrarPostulante':
                        AtenuarBoton(controlId, false);
                        AlternarLoadingGif('interno', false);
                        break;
                    case 'btnListarIngresante':
                        AtenuarBoton(controlId, false);
                        CargandoPanelIngresantes();
                        break;
                }

                if (controlId.indexOf('btnInfoPostulante') > -1 
                    || controlId.indexOf('btnEditarPostulante') > -1 
                    || controlId.indexOf('btnGenerarConvenio') > -1
                    || controlId.indexOf('btnFinanciar') > -1
                    || controlId.indexOf('btnImprimirFichaPecIngresante') > -1) {
                    AtenuarBoton(controlId, false);
                    AlternarLoadingGif('interno', false);
                }
            });

            Sys.Application.add_load(function() {
                var elem = document.getElementById(controlId);

                switch (controlId) {
                    case 'cmbEstadoAlumno':
                        PanelPostulantesCargado();
                        break;
                    case 'btnListarPostulante':
                        AtenuarBoton(controlId, true);
                        PanelPostulantesCargado(controlId);
                        break;
                    case 'btnRegistrarPostulante':
                        FormularioRegistroInscripcionCargado();
                        break;
                    case 'btnListarIngresante':
                        AtenuarBoton(controlId, true);
                        PanelIngresantesCargado();
                        break;
                }

                if (controlId.indexOf('btnInfoPostulante') > -1) {
                    CargarModalMovimientos(controlId);
                }

                if (controlId.indexOf('btnEditarPostulante') > -1) {
                    CargarModalMantenimientoPostulante(controlId, 'btnRegistrar');
                    FiltrarGrillaPostulantes();
                }

                if (controlId.indexOf('btnGenerarConvenio') > -1) {
                    CargarModalGenerarConvenio(controlId, 'btnGuardar');
                }

                if (controlId.indexOf('btnFinanciar') > -1) {
                    OperacionFinanciarCargada(controlId, 'btnGuardar');
                }

                if (controlId.indexOf('btnImprimirFichaPecIngresante') > -1) {
                    VerificarRespuestaFichaInscripcion();
                    FichaInscripcionCargada(controlId);
                }
            });
        </script>
    </body>

    </html>