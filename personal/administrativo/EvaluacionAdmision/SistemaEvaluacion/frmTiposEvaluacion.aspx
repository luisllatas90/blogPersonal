<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTiposEvaluacion.aspx.vb" Inherits="frmTiposEvaluacion" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Registro de Tipos de Evaluación ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
    <link rel="stylesheet" href="../../../assets/toastr-2.1.4-7/toastr.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="../css/style.css">
    <!-- <link rel="stylesheet" href="../css/tiposEvaluacion.css"> -->
</head>

<body>
    <form id="form" runat="server">
        <div class="container-fluid">
            <div class="messagealert" id="divMensaje" runat="server">
            </div>
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Registro de Tipos de Evaluación<img src="../img/loading.gif" id="loadingGif"></h1>
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
                                <div class="col-sm-18 d-flex">
                                    <label for="txtFiltroTipoEvaluacion" class="form-control-sm">Tipo de Evaluación:</label>
                                    <div class="fill">
                                        <asp:TextBox ID="txtFiltroTipoEvaluacion" runat="server" CssClass="form-control form-control-sm" placeholder="Nombre" />
                                    </div>
                                </div>
                                <div class="col-sm-30 text-right">
                                    <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-light" onserverclick="btnListar_Click">
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
                                    <asp:GridView ID="grvTipoEvaluacion" runat="server" AutoGenerateColumns="false" 
                                        DataKeyNames="codigo_tev,virtual_tev" CssClass="table table-sm" GridLines="None"
                                        OnRowCreated="grvTipoEvaluacion_OnRowCreated" OnRowDataBound="grvTipoEvaluacion_OnRowDataBound">
                                        <Columns>
                                            <%--<asp:BoundField DataField="nro" HeaderText="NRO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" />--%>
                                            <asp:BoundField DataField="nombre_tev" HeaderText="DESCRIPCIÓN" />
                                            <asp:TemplateField HeaderText="VIRTUAL" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkVirtualTev" runat="server" Checked='<%# Eval("virtual_tev") %>' Enabled ="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="virtual_tev" HeaderText="VIRTUAL" />--%>
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
                                    <h1 class="title">Datos del tipo de evaluación</h1>
                                    <hr>
                                </div>
                            </div>
                            <div class="row form-group d-flex">
                                <label for="txtDescripcion" class="form-control-sm col-md-5">Descripción:</label>
                                <div class="col-md-15">
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control form-control-sm" placeholder="Descripción" />
                                </div>
                            </div>
                            <div class="row form-group d-flex">
                                <label class="form-control-sm col-md-5">Peso Básica:</label>
                                <div class="col-md-5 d-flex">
                                    <asp:TextBox ID="txtBasico" runat="server" CssClass="form-control form-control-sm" placeholder="Básica" />
                                </div>
                                <label class="form-control-sm col-md-5">Peso Intermedia:</label>
                                <div class="col-md-5 d-flex">
                                    <asp:TextBox ID="txtIntermedio" runat="server" CssClass="form-control form-control-sm" placeholder="Intermedia" />
                                </div>
                                <label class="form-control-sm col-md-5">Peso Avanzada:</label>
                                <div class="col-md-5 d-flex">
                                    <asp:TextBox ID="txtAvanzado" runat="server" CssClass="form-control form-control-sm" placeholder="Avanzada" />
                                </div>    
                            </div>
                            <div class="row form-group d-flex">
                                <label for="chkVirtual" class="form-control-sm col-md-5">Virtual:</label>
                                <div class="col-md-15">
                                    <%--<div class="custom-control custom-checkbox custom-control-inline">
                                        <input type="checkbox" id="chkVirtual" name="chkVirtual" class="custom-control-input" runat="server" value="0">--%>
                                        <asp:CheckBox ID="chkVirtual" runat="server" OnCheckedChanged="chkVirtual_ChekedChanged" AutoPostBack="true" />
                                        <%--<label class="custom-control-label form-control-sm" for="chkVirtual"></label>
                                    </div>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control form-control-sm" placeholder="Descripción" />--%>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-48">
                                    <h1 class="title">Configuración de preguntas por indicador</h1>
                                    <hr>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label for="grvConfigPreguntas" class="form-control-sm col-md-5">Configuración:</label>
                                <div class="col-sm-40">
                                    <asp:GridView ID="grvConfigPreguntas" runat="server" AutoGenerateColumns="false" 
                                        DataKeyNames="codigo_tei, codigo_tev, codigo_ind, cantidad_preguntas_tei" CssClass="table table-sm" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="nombre_com" HeaderText="COMPETENCIA" />
                                            <asp:BoundField DataField="nombre_scom" HeaderText="SUBCOMPETENCIA" />
                                            <asp:BoundField DataField="nombre_ind" ItemStyle-HorizontalAlign="Center" HeaderText="INDICADOR" />
                                            <asp:TemplateField HeaderText="N° DE PREGUNTAS" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control form-control-sm" placeholder="Cantidad" Text='<%# Eval("cantidad_preguntas_tei") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="thead-dark" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <hr class="separador">
                            <div class="row form-group mb-2">
                                <div class="col-md-20 offset-md-5">
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