<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConfiguracionEvaluacionesEvento.aspx.vb" Inherits="frmConfiguracionEvaluacionesEvento" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Configuración de Evaluaciones por Evento de Admisión ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
    <link rel="stylesheet" href="../../../assets/toastr-2.1.4-7/toastr.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="../css/style.css">
</head>

<body>
    <form id="form" runat="server">
        <asp:ScriptManager ID="scm" runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <div class="messagealert" id="divMensaje" runat="server">
            </div>
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Configuración de Evaluaciones por Evento de Admisión<img src="img/loading.gif" id="loadingGif"></h1>
                    <hr>
                </div>
            </div>
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="navlistatab" data-toggle="tab" href="#nav-lista" role="tab" aria-controls="nav-lista" 
                        aria-selected="true" runat="server">Lista</a>
                    <a class="nav-item nav-link" id="navmantenimientotab" data-toggle="tab" href="#nav-mantenimiento" role="tab" aria-controls="nav-mantenimiento"
                        aria-selected="false" runat="server">Mantenimiento</a>
                </div>
            </nav>
            <div class="tab-content" id="nav-tabContent">
                <div class="tab-pane fade show active" id="navlista" role="tabpanel" aria-labelledby="nav-lista-tab" runat="server">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="title">Filtros</h5>
                        </div>
                        <div class="card-body">
                            <div class="row form-group">
                                <div class="col-sm-16 d-flex">
                                    <label for="cmbFiltroCicloAcademico" class="form-control-sm">Semestre Academico:</label>
                                    <div class="fill">
                                        <asp:DropDownList ID="cmbFiltroCicloAcademico" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm selectpicker"
                                            data-live-search="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-20 d-flex">
                                    <label for="cmbFiltroCentroCostos" class="form-control-sm">Centro de Costos:</label>
                                    <div class="fill">
                                        <asp:DropDownList ID="cmbFiltroCentroCostos" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                            data-live-search="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-12 text-right">
                                    <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-info" onserverclick="btnListar_Click">
                                        <i class="fa fa-search"></i>
                                        <span class="text">Listar</span>
                                    </button>
                                    <button type="button" id="btnAgregar" runat="server" class="btn btn-accion btn-success" onserverclick="btnAgregar_Click">
                                        <i class="fa fa-plus"></i>
                                        <span class="text">Agregar</span>
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-48">
                                    <hr class="separador">
                                    <asp:GridView ID="grvConfig" runat="server" AutoGenerateColumns="false" 
                                        DataKeyNames="codigo_cee" CssClass="table table-sm" GridLines="None">
                                        <Columns>
                                            <%--<asp:BoundField DataField="nro" HeaderText="NRO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" />--%>
                                            <asp:BoundField DataField="descripcion_Cco" HeaderText="CENTRO DE COSTOS" />
                                            <asp:BoundField DataField="nombre_tev" HeaderText="TIPO DE EVALUACIÓN" />
                                            <asp:BoundField DataField="nombre_Cpf" HeaderText="PROGRAMA DE ESTUDIOS" />
                                            <asp:BoundField DataField="cantidad_cee" HeaderText="CANTIDAD" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="total_peso" HeaderText="PESO(S)" ItemStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:TemplateField HeaderText="OPERACIONES" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-primary" ToolTip = "Editar">
                                                        <span><i class="fa fa-edit"></i></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-danger" ToolTip = "Eliminar"
                                                        OnClientClick="return confirm('¿ Desea eliminar la configuracion ?');">
                                                        <span><i class="fa fa-trash-alt"></i></span>
                                                    </asp:LinkButton>
                                                    <%--<button type="button" id="btnEditar" runat="server" class="btn btn-sm btn-accion btn-primary">
                                                        <i class="fa fa-edit"></i>
                                                    </button>--%>
                                                    <%--<button type="button" id="btnEliminar" runat="server" class="btn btn-sm btn-accion btn-danger">
                                                        <i class="fa fa-trash-alt"></i>
                                                    </button>--%>
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
                <div class="tab-pane fade" id="navmantenimiento" role="tabpanel" aria-labelledby="nav-lista-tab" runat="server">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="title">Registro / Edición</h5>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-48">
                                    <h1 class="title">Configuración de Evaluaciones por Evento de Admisión</h1>
                                    <hr>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label for="cmbCentroCosto" class="form-control-sm col-md-8">Centro de Costos:</label>
                                <div class="col-md-14">
                                    <asp:DropDownList ID="cmbCentroCosto" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                        data-live-search="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label for="cmbCarreraProfesional" class="form-control-sm col-md-8">Programa de Estudios:</label>
                                <div class="col-md-14">
                                    <asp:UpdatePanel ID="udpCarreraProfesional" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:ListBox ID="cmbCarreraProfesional" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm selectpicker"
                                                data-live-search="true">
                                            </asp:ListBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label for="cmbTipoEvaluacion" class="form-control-sm col-md-8">Tipo de Evaluación:</label>
                                <div class="col-md-12">
                                    <asp:DropDownList ID="cmbTipoEvaluacion" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                        data-live-search="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label for="txtCantidad" class="form-control-sm col-md-8">Cantidad:</label>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control form-control-sm" placeholder="Cantidad" AutoPostBack="true" />
                                </div>
                            </div>
                            <div class="row form-group">
                                <label for="grvConfigPesos" class="form-control-sm col-md-8">Pesos por Evaluación:</label>
                                <div class="col-sm-20">
                                    <asp:GridView ID="grvConfigPesos" runat="server" AutoGenerateColumns="false" 
                                        DataKeyNames="codigo_ceep, codigo_cee, peso_ceep, nro_orden_ceep" CssClass="table table-sm" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="descripcion_ceep" HeaderText="EVALUACIÓN" />
                                            <asp:TemplateField HeaderText="PESO" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPeso" runat="server" CssClass="form-control form-control-sm" placeholder="Peso (decimal)" Text='<%# Eval("peso_ceep") %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="OPERACIONES" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <button type="button" id="btnEditar" runat="server" class="btn btn-sm btn-primary" Visible='<%# Container.DataItemIndex = 0 %>'>
                                                        <i class="fa fa-edit"></i>
                                                    </button>
                                                    <button type="button" id="btnGuardar" runat="server" class="btn btn-sm btn-success" Visible='<%# Container.DataItemIndex > 0 %>'>
                                                        <i class="fa fa-save"></i>
                                                    </button>
                                                    <button type="button" id="btnCancelar" runat="server" class="btn btn-sm btn-danger" Visible='<%# Container.DataItemIndex > 0 %>'>
                                                        <i class="fa fa-ban"></i>
                                                    </button>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <HeaderStyle CssClass="thead-dark" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <hr class="separador">
                            <div class="row form-group mb-2">
                                <div class="col-md-20 offset-md-6">
                                    <button type="button" id="btnCancelar" runat="server" class="btn btn-accion btn-light" onserverclick="btnCancelar_Click">
                                        <i class="fa fa-ban"></i>
                                        <span class="text">Cancelar</span>
                                    </button>
                                    <button type="button" id="btnGuardar" runat="server" class="btn btn-accion btn-primary" onserverclick="btnGuardar_Click">
                                        <i class="fa fa-save"></i>
                                        <span class="text">Guardar</span>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
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
</body>

</html>