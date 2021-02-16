<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRevisionResultados.aspx.vb" Inherits="ProcesamientoResultados_frmRevisionResultados" %>

<!DOCTYPE html>
<html lang="en">

<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Revisión de Respuestas por Evaluación ::.</title>
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
                    <h1 class="main-title">Revisión de Respuestas por Evaluación<img src="../img/loading.gif" id="loadingGif"></h1>
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
                                    <label for="cmbFiltro1" class="form-control-sm">Centro Costo:</label>
                                    <div class="fill">
                                        <asp:DropDownList ID="cmbFiltroCentroCostos" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
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
                                    <asp:GridView ID="grvEvaluaciones" runat="server" AutoGenerateColumns="false" 
                                        DataKeyNames="codigo_evl, codigo_cco, codigo_tev" CssClass="table table-sm" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="descripcion_Cco" HeaderText="CENTRO COSTO" />
                                            <asp:BoundField DataField="nombre_tev" HeaderText="TIPO EVALUACIÓN" />
                                            <asp:BoundField DataField="nombre_evl" HeaderText="NOMBRE"  />
                                            <asp:BoundField DataField="cant_total" HeaderText="TOTAL" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="cant_pendiente" HeaderText="PENDIENTES" ItemStyle-HorizontalAlign="Center"/>
                                            <asp:BoundField DataField="cant_conforme" HeaderText="CONFORMES" ItemStyle-HorizontalAlign="Center"/>
                                            <asp:BoundField DataField="cant_observada" HeaderText="OBSERVADO" ItemStyle-HorizontalAlign="Center"/>
                                            <asp:TemplateField HeaderText="OPERACIONES" ItemStyle-Width="15%" ItemStyle-HorizontalAlign=Center>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-primary" ToolTip = "Editar">
                                                        <span><i class="fa fa-eye"></i></span>
                                                    </asp:LinkButton>
                                                    <%--<button type="button" id="btnEditar" runat="server" class="btn btn-sm btn-accion btn-primary">
                                                        <i class="fa fa-eye"></i>
                                                        <span class="text">Vista</span>
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
                                    <h1 class="title">Listado Evaluaciones por Postulantes</h1>
                                    <hr>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-48">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grvPostulante" runat="server" AutoGenerateColumns="false" 
                                            DataKeyNames="codigo_elu, codigo_alu" CssClass="table table-sm" GridLines="None"
                                            OnRowDataBound="grvPostulante_OnRowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIVER." ItemStyle-HorizontalAlign=Center/>
                                                <asp:BoundField DataField="nroDocIdent_Alu" HeaderText="NRO. DOC. IDENT." ItemStyle-HorizontalAlign=Center />
                                                <asp:BoundField DataField="nombreCompleto" HeaderText="Apellidos y Nombres"  />
                                                <%--<asp:BoundField DataField="nro" HeaderText="Item" ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Center/>
                                                <asp:BoundField DataField="codigo" HeaderText="CODIGO" />
                                                <asp:BoundField DataField="dni" HeaderText="DNI" />
                                                <asp:BoundField DataField="nombrecompleto" HeaderText="APELLIDOS Y NOMBRES" />
                                                <asp:BoundField DataField="r1" HeaderText="01" />
                                                <asp:BoundField DataField="r2" HeaderText="02" />
                                                <asp:BoundField DataField="r3" HeaderText="03" />
                                                <asp:BoundField DataField="r4" HeaderText="04" />
                                                <asp:BoundField DataField="r5" HeaderText="05" />
                                                <asp:BoundField DataField="r6" HeaderText="06" />
                                                <asp:BoundField DataField="r7" HeaderText="07" />
                                                <asp:BoundField DataField="r8" HeaderText="08" />
                                                <asp:BoundField DataField="r9" HeaderText="09" />
                                                <asp:BoundField DataField="r10" HeaderText="10" />
                                                <asp:BoundField DataField="r11" HeaderText="11" />
                                                <asp:BoundField DataField="r12" HeaderText="12" />
                                                <asp:BoundField DataField="r13" HeaderText="13" />
                                                <asp:BoundField DataField="r14" HeaderText="14" />
                                                <asp:BoundField DataField="r15" HeaderText="15" />
                                                <asp:BoundField DataField="r16" HeaderText="16" />
                                                <asp:BoundField DataField="r17" HeaderText="17" />
                                                <asp:BoundField DataField="r18" HeaderText="18" />
                                                <asp:BoundField DataField="r19" HeaderText="19" />
                                                <asp:BoundField DataField="r20" HeaderText="20" />
                                                <asp:TemplateField HeaderText="Operaciones" ItemStyle-Width="20%" ItemStyle-HorizontalAlign=Center>
                                                    <ItemTemplate>
                                                        <div class="col-md-12">
                                                            <div class="custom-control custom-checkbox custom-control-inline">
                                                                <input type="checkbox" id="chkControl51" name="chkControl51" class="custom-control-input" runat="server" value="1">
                                                                <label class="custom-control-label form-control-sm" for="chkControl51">Conforme</label>
                                                            </div>
                                                            <div class="custom-control custom-checkbox custom-control-inline">
                                                                <input type="checkbox" id="chkControl52" name="chkControl52" class="custom-control-input" runat="server" value="2">
                                                                <label class="custom-control-label form-control-sm" for="chkControl52">Observado</label>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <HeaderStyle CssClass="thead-dark" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <hr class="separador">
                            <div class="row form-group mb-2">
                                <div class="col-md-20 offset-md-4">
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
