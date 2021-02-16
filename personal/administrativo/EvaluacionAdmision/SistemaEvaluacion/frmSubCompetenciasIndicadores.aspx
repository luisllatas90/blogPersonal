<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSubCompetenciasIndicadores.aspx.vb" Inherits="frmSubCompetenciasIndicadores" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Registro de Sub Competencias e Indicadores ::.</title>
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
                    <h1 class="main-title">Registro de Sub Competencias e Indicadores<img src="img/loading.gif" id="loadingGif"></h1>
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
                                    <label for="cmbFiltroCompetencia" class="form-control-sm">Competencia:</label>
                                    <div class="fill">
                                        <asp:DropDownList ID="cmbFiltroCompetencia" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                            data-live-search="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--<div class="col-md-12 d-flex" id="divFiltroSubCompetencia" runat="server">
                                    <label for="cmbFiltroSubCompetencia" class="form-control-sm">Sub Competencia:</label>
                                    <div class="fill">
                                        <asp:DropDownList ID="cmbFiltroSubCompetencia" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                            data-live-search="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                                <div class="col-md-28 offset-md-2 text-right">
                                    <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-light" onserverclick="btnListar_Click">
                                        <i class="fa fa-search"></i>
                                        <span class="text">Listar</span>
                                    </button>
                                    <button type="button" id="btnAgregarSubCompetencia" runat="server" class="btn btn-accion btn-success" onserverclick="btnAgregarSubCompetencia_Click">
                                        <i class="fa fa-plus"></i>
                                        <span class="text">Agregar Sub Competencia</span>
                                    </button>
                                    <button type="button" id="btnAgregarIndicador" runat="server" class="btn btn-accion btn-info" onserverclick="btnAgregarIndicador_Click">
                                        <i class="fa fa-plus"></i>
                                        <span class="text">Agregar Indicador</span>
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-48">
                                    <hr class="separador">
                                    <asp:GridView ID="grvSubCompetenciaIndicador" runat="server" AutoGenerateColumns="false" 
                                        DataKeyNames="codigo_ind, codigo_com, nombre_ind, codigo_scom, nombre_scom, descripcion_ind" CssClass="table table-sm" 
                                        GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="nombre_com" HeaderText="COMPETENCIA" />
                                            <asp:BoundField DataField="nombre_scom" HeaderText="SUB COMPETENCIA" />
                                            <asp:TemplateField HeaderText="OPERACIONES" ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Center>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditarSubCompetencia" runat="server" CommandName="EditarSubCompetencia" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-primary" ToolTip = "Editar SubCompetencia">
                                                        <span><i class="fa fa-edit"></i></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminarSubCompetencia" runat="server" CommandName="EliminarSubCompetencia" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-danger" ToolTip = "Eliminar SubCompetencia"
                                                        OnClientClick="return confirm('¿ Desea eliminar la subcompetencia ?');">
                                                        <span><i class="fa fa-trash-alt"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nombre_ind" HeaderText="INDICADOR" ItemStyle-HorizontalAlign=Center />
                                            <asp:BoundField DataField="descripcion_ind" HeaderText="DESCRIPCIÓN DE INDICADOR" ItemStyle-HorizontalAlign=Center />
                                            <asp:TemplateField HeaderText="OPERACIONES" ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Center>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditarIndicador" runat="server" CommandName="EditarIndicador" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-primary" ToolTip = "Editar Indicador">
                                                        <span><i class="fa fa-edit"></i></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminarIndicador" runat="server" CommandName="EliminarIndicador" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-danger" ToolTip = "Eliminar Indicador"
                                                        OnClientClick="return confirm('¿ Desea eliminar el indicador ?');">
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
                                <div class="col-md-24" id="divSubCompetencia" runat="server">
                                    <div class="row">
                                        <div class="col-md-48">
                                            <h1 class="title">Datos de la Sub Competencia</h1>
                                            <hr>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="cmbCompetencia" class="form-control-sm col-md-12">Competencia:</label>
                                        <div class="col-md-24">
                                            <asp:DropDownList ID="cmbCompetencia" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                data-live-search="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="txtSubCompetencia" class="form-control-sm col-md-12">Sub Competencia:</label>
                                        <div class="col-md-30">
                                            <asp:TextBox ID="txtSubCompetencia" runat="server" TextMode="multiline" Columns="70" Rows="5" CssClass="form-control form-control-sm" placeholder="Sub Competencia" />
                                        </div>
                                    </div>
                                    <hr class="separador">
                                    <div class="row form-group">
                                        <div class="col-md-24 offset-md-10">
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
                                <div class="col-md-24" id="divIndicador" runat="server">
                                    <div class="row">
                                        <div class="col-md-48">
                                            <h1 class="title">Datos del Indicador</h1>
                                            <hr>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="cmbAltCompetencia" class="form-control-sm col-md-12">Competencia:</label>
                                        <div class="col-md-24">
                                            <asp:DropDownList ID="cmbAltCompetencia" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm selectpicker"
                                                data-live-search="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="cmbSubCompetencia" class="form-control-sm col-md-12">Sub Competencia:</label>
                                        <div class="col-md-34">
                                            <asp:DropDownList ID="cmbSubCompetencia" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                data-live-search="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="txtIndicador" class="form-control-sm col-md-12">Indicador:</label>
                                        <div class="col-md-24">
                                            <asp:TextBox ID="txtIndicador" runat="server" CssClass="form-control form-control-sm" placeholder="Indicador" />
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="txtDescripcion" class="form-control-sm col-md-12">Descripcion:</label>
                                        <div class="col-md-34">
                                            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="multiline" Columns="70" Rows="5"  CssClass="form-control form-control-sm" placeholder="Indicador" />
                                        </div>
                                    </div>
                                    <hr class="separador">
                                    <div class="row form-group">
                                        <div class="col-md-24 offset-md-10">
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