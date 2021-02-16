<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaPreguntaEvaluacion.aspx.vb" Inherits="GenerarEvaluacion_frmListaPreguntaEvaluacion" %>

<!DOCTYPE html>
<html lang="en">

<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Revisión de Preguntas de Evaluación ::.</title>
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
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="messagealert" id="divMensaje" runat="server">
            </div>
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Revisión de Preguntas de Evaluación<img src="img/loading.gif" id="loadingGif"></h1>
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
                                    <label for="cmbCco" class="form-control-sm">Centro Costo:</label>
                                    <div class="fill">
                                        <asp:DropDownList ID="cmbCco" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                            data-live-search="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-12 text-right">
                                    <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-light" onserverclick="btnListar_Click">
                                        <i class="fa fa-search"></i>
                                        <span class="text">Listar</span>
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-48">
                                    <hr class="separador">
                                    <asp:GridView ID="grvEvaluacion" runat="server" AutoGenerateColumns="false" 
                                        DataKeyNames="codigo_evl, nombre_evl, codigo_cco, codigo_tev" CssClass="table table-sm" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="nombre_tev" HeaderText="TIPO EVALUACIÓN" />
                                            <asp:BoundField DataField="nombre_evl" HeaderText="NOMBRE" />
                                            <asp:TemplateField HeaderText="VIRTUAL" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkVirtualEvl" runat="server" Checked='<%# Eval("virtual_evl") %>' Enabled ="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="tipo_prv_descripcion" HeaderText="TIPO PREGUNTA" />
                                            <asp:BoundField DataField="cant_total" HeaderText="TOTAL" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="cant_pendiente" HeaderText="PENDIENTES" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="cant_conforme" HeaderText="CONFORMES" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="cant_observada" HeaderText="OBSERVADAS" ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="OPERACIONES" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-primary" 
                                                        visible='<%# iif(Eval("virtual_evl"),"false","true") %>'
                                                        ToolTip = "Editar">
                                                        <span><i class="fa fa-eye"></i></span>
                                                    </asp:LinkButton>
                                                    <%--<button type="button" id="btnEditar" runat="server" class="btn btn-sm btn-accion btn-primary">
                                                        <i class="fa fa-eye"></i>
                                                        <span class="text">Ver</span>
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
                                    <h1 class="title" id="txtItem" runat="server">Pregunta N° 01</h1>
                                    <hr>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-48" id="divSelectPregunta" runat="server">
                                </div>
                            </div>
                            <%--<div class="row form-group">
                                <label for="txtPregunta" class="form-control-sm col-md-4">Pregunta:</label>
                                <div class="col-md-15">
                                    <asp:TextBox ID="txtPregunta" runat="server" TextMode="multiline" Columns="70" Rows="4" CssClass="form-control form-control-sm"
                                        placeholder="Enunciado de la Pregunta" />
                                </div>
                            </div>
                            <div class="row form-group">
                                <label for="rbtControl4" class="form-control-sm col-md-4">Respuesta:</label>
                                <div class="col-md-12">
                                    <div class="custom-control custom-radio custom-control-inline">
                                        <input type="radio" id="rbtControl41" name="rbtControl4" class="custom-control-input" runat="server" value="1">
                                        <label class="custom-control-label form-control-sm" for="rbtControl41">Valor 1</label>
                                    </div>
                                    <div class="custom-control custom-radio custom-control-inline">
                                        <input type="radio" id="rbtControl42" name="rbtControl4" class="custom-control-input" runat="server" value="2">
                                        <label class="custom-control-label form-control-sm" for="rbtControl42">Valor 2</label>
                                    </div>
                                    <div class="custom-control custom-radio custom-control-inline">
                                        <input type="radio" id="rbtControl43" name="rbtControl4" class="custom-control-input" runat="server" value="3">
                                        <label class="custom-control-label form-control-sm" for="rbtControl43">Valor 3</label>
                                    </div>
                                    <div class="custom-control custom-radio custom-control-inline">
                                        <input type="radio" id="rbtControl44" name="rbtControl4" class="custom-control-input" runat="server" value="4">
                                        <label class="custom-control-label form-control-sm" for="rbtControl44">Valor 4</label>
                                    </div>
                                    <div class="custom-control custom-radio custom-control-inline">
                                        <input type="radio" id="rbtControl45" name="rbtControl4" class="custom-control-input" runat="server" value="5">
                                        <label class="custom-control-label form-control-sm" for="rbtControl45">Valor 5</label>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-48">
                                    <h1 class="title">Verificar:</h1>
                                    <hr>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label for="rbtControl04" class="form-control-sm col-md-6">Respuesta:</label>
                                <div class="col-md-18">
                                    <div class="custom-control custom-radio custom-control-inline">
                                        <input type="radio" id="rbConforme" name="rbtControl04" class="custom-control-input" runat="server" value="1">
                                        <label class="custom-control-label form-control-sm" for="rbConforme">Conforme</label>
                                    </div>
                                    <div class="custom-control custom-radio custom-control-inline">
                                        <input type="radio" id="rbNoConforme" name="rbtControl04" class="custom-control-input" runat="server" value="2">
                                        <label class="custom-control-label form-control-sm" for="rbNoConforme">No Conforme</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label for="txtObservacion" class="form-control-sm col-md-6">Observación:</label>
                                <div class="col-md-18">
                                    <asp:TextBox ID="txtObservacion" runat="server" TextMode="multiline" Columns="70" Rows="4" CssClass="form-control form-control-sm"
                                        placeholder="Ingrese una observación" />
                                </div>
                            </div>
                            <hr class="separador">
                            <div class="row form-group mb-2">
                                <div class="col-md-20 offset-md-4">
                                    <button type="button" id="btnAnterior" runat="server" class="btn btn-accion btn-primary" onserverclick="btnAnterior_Click">
                                        <i class="fa fa-chevron-circle-left"></i>
                                        <span class="text">Anterior</span>
                                    </button>
                                    <button type="button" id="btnSiguiente" runat="server" class="btn btn-accion btn-primary" onserverclick="btnSiguiente_Click">
                                        <span class="text" id="txtSiguiente" runat = "server" >Siguiente</span>
                                        <i class="fa fa-chevron-circle-right"></i>
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
