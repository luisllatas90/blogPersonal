<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEvaluacionAdjuntarPortada.aspx.vb" Inherits="GenerarEvaluacion_frmEvaluacionAdjuntarPortada" %>

<!DOCTYPE html>
<html lang="en">

<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Adjuntar Portada para Evaluacion ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
    <link rel="stylesheet" href="../../../assets/toastr-2.1.4-7/toastr.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="../css/style.css">
    <link rel="stylesheet" href="../css/plantilla.css">
    <style>
        .custom-file-label::after{
        	content: "";
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="messagealert" id="divMensaje" runat="server">
            </div>
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Adjuntar Portada para Evaluación<img src="img/loading.gif" id="loadingGif"></h1>
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
                                        DataKeyNames="codigo_evl, codigo_cco, codigo_tev, nombre_evl" CssClass="table table-sm" GridLines="None">
                                        <Columns>
                                            <%--<asp:BoundField DataField="nro" HeaderText="NRO" />--%>
                                            <asp:BoundField DataField="descripcion_Cco" HeaderText="CENTRO COSTO" />
                                            <asp:BoundField DataField="nombre_tev" HeaderText="TIPO EVALUACIÓN" />
                                            <asp:BoundField DataField="nombre_evl" HeaderText="NOMBRE" />
                                            <asp:TemplateField HeaderText="VIRTUAL" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkVirtualEvl" runat="server" Checked='<%# Eval("virtual_evl") %>' Enabled ="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ADJUNTAR" ItemStyle-Width="15%" ItemStyle-HorizontalAlign=Center>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnAdjuntar" runat="server" CommandName="Adjuntar" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-primary" 
                                                        visible='<%# iif(Eval("virtual_evl"),"false","true") %>'
                                                        ToolTip = "Adjuntar Portada">
                                                        <span><i class="fa fa-upload"></i></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnMostrar" runat="server" CommandName="Mostrar" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CssClass="btn btn-sm btn-accion btn-info" 
                                                        visible='<%# iif(Eval("virtual_evl"),"false","true") %>'
                                                        ToolTip = "Mostrar Examen">
                                                        <span><i class="fa fa-search"></i></span>
                                                    </asp:LinkButton>
                                                    <%--<button type="button" id="btnEditar" runat="server" class="btn btn-sm btn-accion btn-secondary">
                                                        <i class="fa fa-file-upload"></i>
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
                            <h5 class="title">Adjuntar Portada</h5>
                        </div>
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
                                        <asp:FileUpload ID="fuEvaluacion" runat="server" CssClass="form-control input-sm" accept=".pdf" 
                                            Style="display: none;" onChange="FileSelected(this);"/>
                                    </div>
                                    <small class="text-muted"><em>Formatos permitidos: <strong>.pdf</strong></em></small>
                                    <%--<div class="custom-file">
                                        <input type="file" class="custom-file-input" id="customFile">
                                        <label class="custom-file-label" for="customFile">Elija Documento</label>
                                    </div>--%>
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
</body>
</html>
