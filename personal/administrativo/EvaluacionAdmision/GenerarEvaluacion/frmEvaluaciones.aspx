<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEvaluaciones.aspx.vb" Inherits="frmEvaluaciones" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Registro de Evaluación ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
    <link rel="stylesheet" href="../../../assets/toastr-2.1.4-7/toastr.min.css">
    <link rel="stylesheet" href="../../../assets/smart-wizard/css/smart_wizard.css">
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
    <script src="../../../assets/smart-wizard/js/jquery.smartWizard.js"></script>
    <!-- Scripts propios -->
    <script src="../js/funciones.js"></script>
    <script src="../js/evaluaciones.js?x4"></script>
    
    <style>
        .custom-file-label::after{
        	content: "";
        }
        #lnkPlantilla
        {
            font-size: 0.9rem;
            font-weight: 500;
        }
    </style>
    
    <script type="text/javascript">

        function FileSelected(fu) {
            var fn = fu.value;
            $("#spnFile").empty();

            if (fn !== "") {
                var idx = fn.lastIndexOf("\\") + 1;
                fn = fn.substr(idx, fn.lenght);
                $("#hf").val("1");
                $("#spnFile").text(fn);
            } else {
                $("#hf").val("0");
                $("#spnFile").text("No se eligió ningun archivo");
            }
        }
    
    </script>
    
</head>

<body>
    <form id="form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:HiddenField ID="hfcodigoprv" runat="server" />
        <asp:HiddenField ID="hfidentificadorprv" runat="server" />
        <asp:HiddenField ID="hfnivel" runat="server" />
        <div class="container-fluid">
            <div class="messagealert" id="divMensaje" runat="server">
            </div>
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Registro de Evaluación<img src="../img/loading.gif" id="loadingGif"></h1>
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
                                    <label for="cmbFiltroCentroCosto" class="form-control-sm">Centro de Costo:</label>
                                    <div class="fill">
                                        <asp:DropDownList ID="cmbFiltroCentroCosto" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                            data-live-search="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-12 text-right">
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
                                    <a href="../Docs/PLANTILLA_PREGUNTAS.csv" id="lnkPlantilla" class="float-right mb-2 mr-2" download>Descargar Plantilla</a>
                                    <asp:GridView ID="grvEvaluaciones" runat="server" AutoGenerateColumns="false" 
                                        DataKeyNames="codigo_evl, codigo_cco, codigo_tev, nombre_evl, virtual_evl" CssClass="table table-sm" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="descripcion_Cco" HeaderText="CENTRO DE COSTOS" />
                                            <asp:BoundField DataField="nombre_tev" HeaderText="TIPO DE EVALUACIÓN" />
                                            <asp:BoundField DataField="nombre_evl" HeaderText="NOMBRE" />
                                            <asp:TemplateField HeaderText="VIRTUAL" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkVirtualEvl" runat="server" Checked='<%# Eval("virtual_evl") %>' Enabled ="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OPERACIONES" ItemStyle-Width="15%" ItemStyle-HorizontalAlign=Center>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-primary" ToolTip = "Editar">
                                                        <span><i class="fa fa-edit"></i></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-danger" ToolTip = "Eliminar Componente"
                                                        OnClientClick="return confirm('¿ Desea eliminar la evaluación ?');">
                                                        <span><i class="fa fa-trash-alt"></i></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnImportar" runat="server" CommandName="Importar" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-light" ToolTip = "Importar Configuración"
                                                        visible='<%# Eval("virtual_evl") %>'
                                                        OnClientClick="return confirm('¿ Desea importar la configuración ?');">
                                                        <span><i class="fa fa-upload"></i></span>
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
                                <div class="col-sm-48">
                                    <nav>
                                        <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                            <a class="nav-item nav-link active" id="navdatosgeneralestab" data-toggle="tab" href="#navdatosgenerales" role="tab"
                                                aria-controls="nav-datos-generales" aria-selected="true" runat="server">Datos Generales</a>
                                            <a class="nav-item nav-link" id="navpreguntastab" data-toggle="tab" href="#navpreguntas" role="tab" aria-controls="nav-preguntas"
                                                aria-selected="false" runat="server">Preguntas</a>
                                            <a class="nav-item nav-link" id="navimportartab" data-toggle="tab" href="#navimportar" role="tab" aria-controls="nav-importar"
                                                aria-selected="false" runat="server">Importar</a>
                                            <%--<a class="nav-item nav-link" id="navordentab" data-toggle="tab" href="#navorden" role="tab" aria-controls="nav-orden"
                                                aria-selected="false" runat="server">Orden de Evaluación</a>--%>
                                        </div>
                                    </nav>
                                    <div class="tab-content" id="nav-tabContent">
                                        <div class="tab-pane fade show active" id="navdatosgenerales" role="tabpanel" aria-labelledby="nav-datos-generales-tab" runat="server">
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="row form-group">
                                                        <label for="cmbCentroCosto" class="form-control-sm col-md-8">Centro de Costos:</label>
                                                        <div class="col-md-15">
                                                            <asp:DropDownList ID="cmbCentroCosto" runat="server" AutoPostBack="false"
                                                                CssClass="form-control form-control-sm selectpicker" data-live-search="true">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row form-group">
                                                        <label for="checkbox" class="form-control-sm col-md-8">Virtual:</label>
                                                        <div class="col-md-15">
                                                            <div class="custom-control custom-checkbox custom-control-inline">
                                                                <input type="checkbox" id="chkVirtual" name="chkVirtual" class="custom-control-input" runat="server" value="0">
                                                                <label class="custom-control-label form-control-sm" for="chkVirtual"></label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row form-group">
                                                        <label for="cmbTipoEvaluacion" class="form-control-sm col-md-8">Tipo:</label>
                                                        <div class="col-md-10">
                                                            <asp:DropDownList ID="cmbTipoEvaluacion" runat="server" AutoPostBack="false"
                                                                CssClass="form-control form-control-sm selectpicker" data-live-search="true">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row form-group">
                                                        <label for="txtNombre" class="form-control-sm col-md-8">Nombre:</label>
                                                        <div class="col-md-15">
                                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-sm" placeholder="Nombre" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade active" id="navpreguntas" role="tabpanel" aria-labelledby="nav-preguntas-tab" runat="server">
                                            <div class="card">
                                                <div class="card-body">
                                                    <%--<div class="card-header">
                                                        <h5 class="title">Selección de Preguntas</h5>
                                                    </div>--%>
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <div class="col-md-48">
                                                                <h1 class="title">Selección de Preguntas</h1>
                                                                <hr>
                                                            </div>
                                                        </div>
                                                        <div class="row form-group">
                                                            <label for="cmbCompetencia" class="form-control-sm col-md-6">Competencia:</label>
                                                            <div class="col-md-14">
                                                                <asp:DropDownList ID="cmbCompetencia" runat="server" AutoPostBack="true" 
                                                                    CssClass="form-control form-control-sm selectpicker" data-live-search="true">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="row form-group">
                                                            <label for="grvConfigPesos" class="form-control-sm col-md-6">Lista de Indicadores:</label>
                                                            <div class="col-sm-30">
                                                                <asp:GridView ID="grvConfig" runat="server" AutoGenerateColumns="false" 
                                                                    DataKeyNames="codigo_scom, codigo_ind, nro_item, codigo_prv, identificador_prv, nombre_ncp, virtual_prv" 
                                                                    CssClass="table table-sm" GridLines="None">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="nombre_scom" HeaderText="SUBCOMPETENCIA" />
                                                                        <asp:BoundField DataField="nombre_ind" HeaderText="INDICADOR" ItemStyle-HorizontalAlign=Center />
                                                                        <asp:BoundField DataField="nro_item" HeaderText="N° ITEM" ItemStyle-HorizontalAlign=Center />
                                                                        <asp:BoundField DataField="identificador_prv" HeaderText="COD. PREGUNTA" ItemStyle-HorizontalAlign=Center />
                                                                        <asp:BoundField DataField="nombre_ncp" HeaderText="NIVEL COMPLEJIDAD" ItemStyle-HorizontalAlign=Center />
                                                                        <%--<asp:BoundField DataField="codPregunta" HeaderText="CÓD. PREGUNTA" ItemStyle-HorizontalAlign=Center />
                                                                        <asp:BoundField DataField="dificultad" HeaderText="NIVEL DE COMPLEJIDAD" ItemStyle-HorizontalAlign=Center />--%>
                                                                        <asp:TemplateField HeaderText="OPERACIONES" ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Center>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnSeleccionar" runat="server" CommandName="Seleccionar" 
                                                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                                                    CssClass="btn btn-sm btn-accion btn-primary" 
                                                                                    visible = <%# IIF(Eval("virtual_prv"), "false", "true") %>
                                                                                    ToolTip = "Seleccionar">
                                                                                    <span><i class="fa fa-edit"></i></span>
                                                                                </asp:LinkButton>
                                                                                <asp:LinkButton ID="btnMostrar" runat="server" CommandName="Mostrar" 
                                                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                                                    CssClass="btn btn-sm btn-accion btn-info" ToolTip = "Mostrar"
                                                                                    visible = <%# IIF(Eval("codigo_prv")=-1, "false", "true") %>>
                                                                                    <span><i class="fa fa-search"></i></span>
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
                                        </div>
                                        <div class="tab-pane fade active" id="navimportar" role="tabpanel" aria-labelledby="nav-importar-tab" runat="server">
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-md-48">
                                                            <h1 class="title">Seleccionar Archivo</h1>
                                                            <hr>
                                                        </div>
                                                    </div>
                                                    <div class="row form-group">
                                                        <label for="fluEvaluacion" class="form-control-sm col-md-6">Archivo:</label>
                                                        <div class="col-md-18">
                                                            <div class="custom-file">
                                                                <label id="lblfuEvaluacion" runat="server" class="custom-file-label" for="fuEvaluacion" 
                                                                    style="font-style: normal; font-size: small; font-weight: normal">
                                                                    <input id="btnfuEvaluacion" type="button" value="Seleccionar Archivo" runat="server" />
                                                                    <span id="spnFile" runat="server">No se eligió ningun archivo</span>
                                                                </label>
                                                                <asp:FileUpload ID="fuEvaluacion" runat="server" CssClass="form-control input-sm" accept=".csv" 
                                                                    Style="display: none;" onChange="FileSelected(this);"/>
                                                            </div>
                                                            <small class="text-muted"><em>Formatos permitidos: <strong>.csv</strong></em></small>
                                                        </div>
                                                    </div>
                                                    <%--<hr class="separador">
                                                    <div class="row form-group mb-2">
                                                        <div class="col-md-20 offset-md-4">
                                                            <button type="button" id="btnCancelarAlt" runat="server" class="btn btn-accion btn-light" onserverclick="btnCancelar_Click">
                                                                <i class="fa fa-ban"></i>
                                                                <span class="text">Cancelar</span>
                                                            </button>
                                                            <button type="button" id="btnGuardarAlt" runat="server" class="btn btn-accion btn-primary" onserverclick="btnGuardar_Click">
                                                                <i class="fa fa-save"></i>
                                                                <span class="text">Guardar</span>
                                                            </button>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<div class="tab-pane fade active" id="navorden" role="tabpanel" aria-labelledby="nav-orden-tab" runat="server">
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="row form-group">
                                                        <div class="col-md-48">
                                                            <div class="row form-group">
                                                                <label for="txtDescripcion" class="form-control-sm col-md-5">Orden de preguntas:</label>
                                                                <div class="col-md-18">
                                                                    <ol class="list-group list-sm">
                                                                        <li class="list-group-item">Redacción</li>
                                                                        <li class="list-group-item">Comprensión</li>
                                                                        <li class="list-group-item">Matemáticas</li>
                                                                        <li class="list-group-item">Física</li>
                                                                        <li class="list-group-item">Química</li>
                                                                        <li class="list-group-item">Anatomía</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <hr class="separador">
                                                    <div class="row form-group mb-2">
                                                        <div class="col-md-20 offset-md-5">
                                                            <button type="button" id="btnOrdenCancelar" runat="server" class="btn btn-accion btn-light">
                                                                <i class="fa fa-ban"></i>
                                                                <span class="text">Cancelar</span>
                                                            </button>
                                                            <button type="button" id="btnOrdenGuardar" runat="server" class="btn btn-accion btn-primary">
                                                                <i class="fa fa-save"></i>
                                                                <span class="text">Guardar</span>
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>--%>
                                    </div>
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
        <!-- MODALES -->
        <div id="mdlPreguntas" class="modal fade" tabindex="-1" role="dialog" runat="server">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span id="spnMenServTitulo" runat="server" class="modal-title">Selección de preguntas</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-48">
                                <div class="card">
                                    <div class="card-header alt-header">
                                        <h5 class="title">Lista de Preguntas</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row form-group">
                                            <div class="col-md-24 d-flex">
                                                <label for="txtFiltrPregunta" class="form-control-sm">Filtrar:</label>
                                                <div class="fill">
                                                    <input type="text" name="txtFiltrPregunta" id="txtFiltrPregunta" runat="server" class="form-control form-control-sm"
                                                        placeholder="Pregunta">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-48" id="divPreguntas" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        <button type="button" class="btn btn-primary" data-dismiss="modal" 
                            id="btnAltGuardar" runat="server" onserverclick="btnAltGuardar_Click">Guardar</button>
                    </div>
                </div>
            </div>
        </div>
        
        <div id="mdlVerPregunta" class="modal fade" tabindex="-1" role="dialog" runat="server">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span id="Span1" runat="server" class="modal-title">Vista de Pregunta</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-48">
                                <div class="card">
                                    <div class="card-header alt-header">
                                        <h5 class="title">Lista de Preguntas</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-48" id="divSelectPregunta" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
        
    </form>
</body>

</html>