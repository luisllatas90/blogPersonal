<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProcesamientoResultados.aspx.vb" Inherits="frmProcesamientoResultados" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Cargar Hoja de Respuestas ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
    <link rel="stylesheet" href="../../../assets/toastr-2.1.4-7/toastr.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="../css/style.css">

    <style>
        .custom-file-label::after 
        {
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
        <div class="container-fluid">
            <div class="messagealert" id="divMensaje" runat="server">
            </div>
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Cargar Hoja de Respuestas<img src="../img/loading.gif" id="loadingGif"></h1>
                    <hr>
                </div>
            </div>
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="navlistatab" data-toggle="tab" href="#nav-lista" role="tab" aria-controls="nav-lista" 
                        aria-selected="true" runat="server">Lista</a>
                    <a class="nav-item nav-link disabled" id="navmantenimientotab" data-toggle="tab" href="#nav-mantenimiento" role="tab" aria-controls="nav-mantenimiento"
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
                                    <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-light" onserverclick="btnListar_Click">
                                        <i class="fa fa-search"></i>
                                        <span class="text">Listar</span>
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-48">
                                    <hr class="separador">
                                    <a href="../Docs/PLANTILLA_RESPUESTAS.csv" id="lnkPlantilla" class="float-right mb-2 mr-2" download>Descargar Plantilla</a>
                                    <asp:GridView ID="grvEvaluaciones" runat="server" AutoGenerateColumns="false" 
                                        DataKeyNames="codigo_evl, codigo_cco, codigo_tev, nombre_evl, idArchivoCompartido, NombreArchivo, virtual_evl" 
                                        CssClass="table table-sm" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="descripcion_Cco" HeaderText="CENTRO DE COSTOS" />
                                            <asp:BoundField DataField="nombre_tev" HeaderText="TIPO DE EVALUACIÓN" />
                                            <asp:BoundField DataField="nombre_evl" HeaderText="EVALUACIÓN" />
                                            <asp:TemplateField HeaderText="VIRTUAL" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkVirtualEvl" runat="server" Checked='<%# Eval("virtual_evl") %>' Enabled ="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ESTADO" ItemStyle-Width="15%" ItemStyle-HorizontalAlign=Center>
                                                <ItemTemplate>
                                                    <i class="fas fa-2x fa-check-square text-success" title="Archivo de notas cargado" runat="server" Visible='<%# IIF(Eval("idArchivoCompartido")>0,"True","False") %>'></i>
                                                    <i class="fas fa-2x fa-ban text-danger" title="No se ha cargado el archivo de notas" runat="server" Visible='<%# IIF(Eval("idArchivoCompartido")<0,"True","False") %>'></i>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OPERACIONES" ItemStyle-Width="15%" ItemStyle-HorizontalAlign=Center>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnAdjuntar" runat="server" CommandName="Adjuntar" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-primary" ToolTip = "Adjuntar">
                                                        <span><i class="fa fa-upload"></i></span>
                                                    </asp:LinkButton>
                                                    <%--<button type="button" id="btnEditar" runat="server" class="btn btn-sm btn-accion btn-primary">
                                                        <i class="fas fa-upload"></i>
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
                                <div class="col-sm-48">
                                    <nav>
                                        <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                            <a class="nav-item nav-link active" id="navdatosgeneralestab" data-toggle="tab" href="#navdatosgenerales" role="tab"
                                                aria-controls="nav-datos-generales" aria-selected="true" runat="server">Datos Generales</a>
                                            <a class="nav-item nav-link" id="navrespuestastab" data-toggle="tab" href="#navrespuestas" role="tab" aria-controls="nav-respuestas"
                                                aria-selected="false" runat="server">Respuestas</a>
                                        </div>
                                    </nav>
                                    <div class="tab-content" id="nav-tabContent">
                                        <div class="tab-pane fade show active" id="navdatosgenerales" role="tabpanel" aria-labelledby="nav-datos-generales-tab" runat="server">
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-md-48">
                                                            <h1 class="title">Ingrese los datos y cargue el archivo a procesar</h1>
                                                            <hr>
                                                        </div>
                                                    </div>
                                                    <div class="row form-group">
                                                        <label for="cmbCentroCostos" class="form-control-sm col-md-6">Centro de Costos:</label>
                                                        <div class="col-md-18">
                                                            <asp:DropDownList ID="cmbCentroCostos" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                                data-live-search="true">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row form-group">
                                                        <label for="cmbTipoEvaluacion" class="form-control-sm col-md-6">Tipo de Evaluación:</label>
                                                        <div class="col-md-10">
                                                            <asp:DropDownList ID="cmbTipoEvaluacion" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                                data-live-search="true">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row form-group">
                                                        <label for="txtEvaluacion" class="form-control-sm col-md-6">Evaluación:</label>
                                                        <div class="col-md-10">
                                                            <asp:TextBox ID="txtEvaluacion" runat="server" CssClass="form-control form-control-sm" placeholder="Nombre" />
                                                            <%--<asp:DropDownList ID="cmbEvaluacion" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                                data-live-search="true">
                                                            </asp:DropDownList>--%>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="fluEvaluacion" class="form-control-sm col-md-6">Archivo:</label>
                                                        <div class="col-sm-18">
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
                                                            
                                                            <%--<label id="lblFuArchivo" runat="server" style="font-style: normal; font-size: small;
                                                                font-weight: normal">
                                                                <input id="btnFuArchivo" type="button" value="Seleccionar archivo" runat="server" />
                                                                <span id="spnFile" runat="server">No se eligió anexo de la asignatura</span>
                                                            </label>
                                                            <asp:FileUpload ID="fuArchivo" runat="server" CssClass="form-control input-sm" AllowMultiple="true"
                                                                Accept=".pdf" Style="display: none;" onChange="FileSelected(this);" />--%>
                                                            
                                                            
                                                        </div>
                                                    </div>
                                                    <div class="row form-group">
                                                        <div class="col-md-42 offset-md-6">
                                                           <%-- <span class="badge badge-secondary">500 Alumnos inscritos</span>
                                                            <span class="badge badge-info">490 Alumnos asistieron</span>
                                                            <span class="badge badge-primary">490 Registros encontrados</span>
                                                            <span class="badge badge-success">24500 Respuestas cargadas</span>--%>
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
                                        <div class="tab-pane fade active" id="navrespuestas" role="tabpanel" aria-labelledby="nav-respuestas-tab" runat="server">
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <div class="col-md-48">
                                                                <h1 class="title">Selección de Preguntas</h1>
                                                                <hr>
                                                            </div>
                                                        </div>
                                                        <div class="row form-group">
                                                            <div class="col-md-20 offset-md-6">
                                                                <button type="button" id="btnConfirmar" runat="server" class="btn btn-accion btn-success" onserverclick="btnConfirmar_Click">
                                                                    <i class="fa fa-check"></i>
                                                                    <span class="text">Confirmar</span>
                                                                </button>
                                                            </div>
                                                        </div>
                                                        <div class="row form-group">
                                                            <div class="col-sm-48">
                                                                <div class="table-responsive">
                                                                    <asp:GridView ID="grvResultados" runat="server" AutoGenerateColumns="false" 
                                                                        DataKeyNames="codigo_elu, codigo_alu" CssClass="table table-sm" GridLines="None"
                                                                        OnRowDataBound="grvResultados_OnRowDataBound">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIVER." ItemStyle-HorizontalAlign=Center/>
                                                                            <asp:BoundField DataField="nroDocIdent_Alu" HeaderText="NRO. DOC. IDENT." ItemStyle-HorizontalAlign=Center />
                                                                            <asp:BoundField DataField="nombreCompleto" HeaderText="Apellidos y Nombres"  />
                                                                            <%--<asp:BoundField DataField="codPregunta" HeaderText="CÓD. PREGUNTA" ItemStyle-HorizontalAlign=Center />
                                                                            <asp:BoundField DataField="dificultad" HeaderText="NIVEL DE COMPLEJIDAD" ItemStyle-HorizontalAlign=Center />--%>
                                                                            <%--<asp:TemplateField HeaderText="OPERACIONES" ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Center>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="btnSeleccionar" runat="server" CommandName="Seleccionar" 
                                                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                                                        CssClass="btn btn-sm btn-accion btn-primary" ToolTip = "Seleccionar">
                                                                                        <span><i class="fa fa-edit"></i></span>
                                                                                    </asp:LinkButton>
                                                                                    <asp:LinkButton ID="btnMostrar" runat="server" CommandName="Mostrar" 
                                                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                                                        CssClass="btn btn-sm btn-accion btn-info" ToolTip = "Mostrar"
                                                                                        visible = <%# IIF(Eval("codigo_prv")=-1, "false", "true") %>>
                                                                                        <span><i class="fa fa-search"></i></span>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
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