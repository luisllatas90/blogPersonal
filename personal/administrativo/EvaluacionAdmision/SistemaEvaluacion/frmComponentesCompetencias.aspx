<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmComponentesCompetencias.aspx.vb" Inherits="frmComponentesCompetencias" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Registro de Componentes y Competencias ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
    <link rel="stylesheet" href="../../../assets/toastr-2.1.4-7/toastr.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="../css/style.css">
    <link rel="stylesheet" href="../css/plantilla.css">
</head>

<body>
    <form id="form" runat="server">
        <div class="container-fluid">
            <div class="messagealert" id="divMensaje" runat="server">
            </div>
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Registro de Componentes y Competencias<img src="img/loading.gif" id="loadingGif"></h1>
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
                                <div class="col-md-18 d-flex">
                                    <label for="cmbFiltroComponente" class="form-control-sm">Componente:</label>
                                    <div class="fill">
                                        <asp:DropDownList ID="cmbFiltroComponente" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                            data-live-search="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--<div class="col-md-16 d-flex" id="divFiltroCompetencia" runat="server">
                                    <label for="cmbFiltroCompetencia" class="form-control-sm">Competencia:</label>
                                    <div class="fill">
                                        <asp:DropDownList ID="cmbFiltroCompetencia" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                            data-live-search="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                                <div class="col-md-28 offset-md-2 text-right">
                                    <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-light" onserverclick="btnListar_Click">
                                        <i class="fa fa-search"></i>
                                        <span class="text">Listar</span>
                                    </button>
                                    <button type="button" id="btnAgregarComponente" runat="server" class="btn btn-accion btn-success" onserverclick="btnAgregarComponente_Click">
                                        <i class="fa fa-plus"></i>
                                        <span class="text">Agregar Componente</span>
                                    </button>
                                    <button type="button" id="btnAgregarCompetencia" runat="server" class="btn btn-accion btn-info" onserverclick="btnAgregarCompetencia_Click">
                                        <i class="fa fa-plus"></i>
                                        <span class="text">Agregar Competencia</span>
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-48">
                                    <hr class="separador">
                                    <asp:GridView ID="grvComponenteCompetencia" runat="server" AutoGenerateColumns="false" 
                                        DataKeyNames="codigo_cca, codigo_cmp, nombre_cmp, codigo_com, nombre_com" CssClass="table table-sm" GridLines="None"
                                        OnRowCreated="grvComponenteCompetencia_OnRowCreated">
                                        <Columns>
                                            <asp:BoundField DataField="nombre_cmp" HeaderText="NOMBRE DEL COMPONENTE" />
                                            <asp:TemplateField HeaderText="OPERACIONES" ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Center>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditarComponente" runat="server" CommandName="EditarComponente" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-primary" ToolTip = "Editar Componente">
                                                        <span><i class="fa fa-edit"></i></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminarComponente" runat="server" CommandName="EliminarComponente" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-danger" ToolTip = "Eliminar Componente"
                                                        OnClientClick="return confirm('¿ Desea eliminar el componente ?');">
                                                        <span><i class="fa fa-trash-alt"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nombre_com" HeaderText="NOMBRE DE LA COMPETENCIA" />
                                            <asp:BoundField DataField="nombre_corto_com" HeaderText="NOMBRE CORTO" />
                                            <asp:TemplateField HeaderText="OPERACIONES" ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Center>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditarCompetencia" runat="server" CommandName="EditarCompetencia" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-primary" ToolTip = "Editar Competencia" Visible="False">
                                                        <span><i class="fa fa-edit"></i></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminarCompetencia" runat="server" CommandName="EliminarCompetencia" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-danger" ToolTip = "Eliminar Competencia">
                                                        <span><i class="fa fa-trash-alt"></i></span>
                                                    </asp:LinkButton>
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
                                <div class="col-md-24" id="divComponente" runat="server">
                                    <div class="row">
                                        <div class="col-md-48">
                                            <h1 class="title">Datos del componente</h1>
                                            <hr>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="txtComponente" class="form-control-sm col-md-10">Componente:</label>
                                        <div class="col-md-18">
                                            <asp:TextBox ID="txtComponente" runat="server" CssClass="form-control form-control-sm" placeholder="Nombre del componente" />
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="cmbCompetencia" class="form-control-sm col-md-10">Competencia:</label>
                                        <div class="col-md-30">
                                            <asp:ListBox ID="cmbCompetencia" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm selectpicker"
                                                data-live-search="true" selectionmode="multiple">
                                            </asp:ListBox>
                                        </div>
                                    </div>
                                    <hr class="separador">
                                    <div class="row form-group">
                                        <div class="col-md-38 offset-md-10">
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
                                <div class="col-md-24" id="divCompetencia" runat="server">
                                    <div class="row">
                                        <div class="col-md-48">
                                            <h1 class="title">Editar competencia</h1>
                                            <hr>
                                        </div>
                                    </div>
                                    <div class="row form-group" id="divEditCompetencia" runat="server">
                                        <label for="cmbComponente" class="form-control-sm col-md-10">Competencia:</label>
                                        <div class="col-md-30">
                                            <asp:DropDownList ID="cmbCompetenciaEditar" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                data-live-search="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <hr class="separador">
                                    <div class="row form-group">
                                        <label for="txtCompetencia" class="form-control-sm col-md-10">Competencia:</label>
                                        <div class="col-md-30">
                                            <asp:TextBox ID="txtCompetencia" runat="server" TextMode="multiline" Columns="70" Rows="5" CssClass="form-control form-control-sm" value="GENERALES: Descripción larga de competencia"/>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="txtAbrevCompetencia" class="form-control-sm col-md-10">Abreviatura:</label>
                                        <div class="col-md-18">
                                            <asp:TextBox ID="txtAbrevCompetencia" runat="server" CssClass="form-control form-control-sm" placeholder="Abrev. Competencia" />
                                        </div>
                                    </div>
                                    <hr class="separador">
                                    <div class="row form-group">
                                        <div class="col-md-38 offset-md-10">
                                            <button type="button" id="btnAltCancelar" runat="server" class="btn btn-accion btn-light" onserverclick="btnAltCancelar_Click">
                                                <i class="fa fa-ban"></i>
                                                <span class="text">Cancelar</span>
                                            </button>
                                            <button type="button" id="btnAltGuardar" runat="server" class="btn btn-accion btn-primary" onserverclick="btnAltGuardar_Click">
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