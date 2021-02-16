<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAccesitarios.aspx.vb" Inherits="frmAccesitarios" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Lista de Accesitarios ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
    <link rel="stylesheet" href="../../../assets/toastr-2.1.4-7/toastr.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="../css/style.css">
    
    <!-- Scripts externos -->
    <script src="../../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../../assets/bootstrap-4.1/js/bootstrap.bundle.min.js"></script>
    <script src="../../../assets/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>
    <script src="../../../assets/bootstrap-select-1.13.1/js/i18n/defaults-es_ES.min.js"></script>
    <script src="../../../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../../../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="../../../assets/toastr-2.1.4-7/toastr.min.js"></script>
    <!-- Scripts propios -->
    <script src="../js/funciones.js"></script>
    <script src="../js/accesitarios.js?1"></script>
    
</head>

<body>
    <form id="form" runat="server">
        <div class="container-fluid">
            <div class="messagealert" id="divMensaje" runat="server">
            </div>
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Lista de Accesitarios<img src="i../mg/loading.gif" id="loadingGif"></h1>
                    <hr>
                </div>
            </div>
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav-lista-tab" data-toggle="tab" href="#nav-lista" role="tab" aria-controls="nav-lista" aria-selected="true">Lista</a>
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
                                <div class="col-sm-13 d-flex">
                                    <label for="cmbFiltroCicloAcademico" class="form-control-sm">Semestre Academico:</label>
                                    <div class="fill">
                                        <asp:DropDownList ID="cmbFiltroCicloAcademico" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm selectpicker"
                                            data-live-search="true" data-size="10">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-22 d-flex">
                                    <label for="cmbFiltroCentroCosto" class="form-control-sm">Centro de Costo:</label>
                                    <div class="fill">
                                        <asp:DropDownList ID="cmbFiltroCentroCostos" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                            data-live-search="true">
                                            <%--<asp:ListItem Text="-- TODOS --" Value="1" />
                                            <asp:ListItem Text="ADM - EXAMEN TEST DAHC 2020-II (18-JUL-20)" Value="1" />
                                            <asp:ListItem Text="ADM - BECA 18 - 2020-II (MIGRACIÓN DEL 2020-I)" Value="2" />--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-13 d-flex">
                                    <label for="cmbFiltroModalidadIngreso" class="form-control-sm">Modalidad:</label>
                                    <div class="fill">
                                        <asp:DropDownList ID="cmbFiltroModalidadIngreso" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                            data-live-search="true" data-dropdown-align-right="true" data-size="10">
                                            <asp:ListItem Text="-- TODOS --" Value="1" />
                                            <asp:ListItem Text="TEST DAHC" Value="1" />
                                            <asp:ListItem Text="EXAMEN DE ADMISIÓN" Value="1" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-sm-22 d-flex">
                                    <label for="cmbFiltroCarreraProfesional" class="form-control-sm">Programa de Estudios:</label>
                                    <div class="fill">
                                        <asp:DropDownList ID="cmbFiltroCarreraProfesional" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                            data-live-search="true" data-size="10">
                                            <asp:ListItem Text="-- TODOS --" Value="1" />
                                            <asp:ListItem Text="ADMINISTRACIÓN" Value="1" />
                                            <asp:ListItem Text="ING. DE SISTEMAS" Value="1" />
                                            <asp:ListItem Text="MEDICINA" Value="1" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-26 text-right">
                                    <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-info" onserverclick="btnListar_Click">
                                        <i class="fa fa-search"></i>
                                        <span class="text">Listar</span>
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-48">
                                    <hr class="separador">
                                    <asp:GridView ID="grvAccesitarios" runat="server" AutoGenerateColumns="false" 
                                        DataKeyNames="codigo_alu" CssClass="table table-sm" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="puesto" HeaderText="NRO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" />
                                            <%--<asp:BoundField DataField="centroCostos" HeaderText="CENTRO DE COSTOS" />--%>
                                            <asp:BoundField DataField="nombre_Cpf" HeaderText="PROGRAMA DE ESTUDIOS" />
                                            <asp:BoundField DataField="nombre_Min" HeaderText="MODALIDAD DE INGRESO" />
                                            <asp:BoundField DataField="nroDocIdent_Alu" HeaderText="DOC. IDENTIDAD" />
                                            <asp:BoundField DataField="nombreCompleto" HeaderText="POSTULANTE" />
                                            <asp:BoundField DataField="nota_elu" ItemStyle-HorizontalAlign="Center" HeaderText="NOTA" />
                                            <asp:BoundField DataField="cant_noti" ItemStyle-HorizontalAlign="Center" HeaderText="NOTIFICACIONES" />
                                            <asp:BoundField DataField="ind_cargo" ItemStyle-HorizontalAlign="Center" HeaderText="CARGO GENERADO" />
                                            <asp:TemplateField HeaderText="OPERACIONES" ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Center>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnNotificar" runat="server" CommandName="Notificar" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-primary" ToolTip = "Notificar">
                                                        <span><i class="fa fa-bell"></i></span>
                                                    </asp:LinkButton>
                                                    <%--<button type="button" id="btnNotificar" runat="server" class="btn btn-sm btn-primary">
                                                        <i class="fas fa-bell"></i>
                                                    </button>--%>
                                                    <button type="button" id="btnGenerarCargoMatricula" runat="server" class="btn btn-sm btn-success">
                                                        <i class="fas fa-coins"></i>
                                                    </button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="thead-dark" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlNotificar" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span runat="server" class="modal-title">Notificar</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="alert alert-warning">
                            ¿Realmente desea enviar una notificación al accesitario?
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        <button type="button" class="btn btn-primary" data-dismiss="modal"
                            id="btnEnviarNoti" runat="server" onserverclick="btnEnviarNoti_Click">Enviar</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlGenerarCargoMatricula" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span runat="server" class="modal-title">Generar Cargo de Matrícula</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="alert alert-warning">
                            ¿Realmente desea generar el cargo de matrícula? Se generará el cargo por un monto de: <br> 
                            <h5 class="text-right"><span class="badge badge-danger">S/400.00</span></h5>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Generar</button>
                    </div>
                </div>
            </div>
        </div>
    </form>

</body>

</html>