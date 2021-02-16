<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmResumenBancoPregunta.aspx.vb" Inherits="BancoPreguntas_frmResumenBancoPregunta" ValidateRequest="false" %>

<!DOCTYPE html>
<html lang="en">

<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Registro de Preguntas ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-4.1/css-custom/bootstrap.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-select-1.13.1/css/bootstrap-select.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/fontawesome-5.2/css/all.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-datepicker/css/bootstrap-datepicker.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/toastr-2.1.4-7/toastr.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/summernote-0.8.18/summernote-bs4.min.css>
    <!-- Estilos propios -->
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_CSS %>/style.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_CSS %>/resumenBancoPreguntas.css>
</head>

<body>
    <form id="form" runat="server">
        <asp:ScriptManager ID="scm" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpParams" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:HiddenField ID="hddCod" Value="" runat="server" />
                <asp:HiddenField ID="hddTipoVista" Value="" runat="server" />
                <asp:HiddenField ID="hddParamsToastr" Value="" runat="server" />
                <!-- Parámetros para mensajes desde el servidor -->
                <asp:HiddenField ID="hddMenServMostrar" Value="false" runat="server" />
                <asp:HiddenField ID="hddMenServRpta" Value="1" runat="server" />
                <asp:HiddenField ID="hddMenServTitulo" Value="" runat="server" />
                <asp:HiddenField ID="hddMenServMensaje" Value="" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Registro de Preguntas<img src="<%=ClsGlobales.PATH_IMG %>/loading.gif" id="loadingGif"></h1>
                    <hr>
                </div>
            </div>
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav-resumen-tab" data-toggle="tab" href="#nav-resumen" role="tab" aria-controls="nav-resumen" aria-selected="true">Resumen</a>
                    <a class="nav-item nav-link disabled" id="nav-simple-tab" data-toggle="tab" href="#nav-simple" role="tab" aria-controls="nav-simple" aria-selected="false">Pregunta
                        Única</a>
                    <a class="nav-item nav-link disabled" id="nav-compuesta-tab" data-toggle="tab" href="#nav-compuesta" role="tab" aria-controls="nav-compuesta" aria-selected="false">Pregunta
                        Agrupada</a>
                </div>
            </nav>
            <div class="tab-content" id="nav-tabContent">
                <div class="tab-pane fade show active" id="nav-resumen" role="tabpanel" aria-labelledby="nav-resumen-tab">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="title">Filtros</h5>
                        </div>
                        <div class="card-body">
                            <div class="row form-group">
                                <div class="col-sm-16 d-flex">
                                    <label for="cmbFiltroCompetencia" class="form-control-sm">Competencia:</label>
                                    <div class="fill">
                                        <asp:DropDownList ID="cmbFiltroCompetencia" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker" data-live-search="true">
                                            <asp:ListItem Text="-- SELECCIONE --" Value="0" />
                                            <asp:ListItem Text="COMPRENSIÓN" Value="1" />
                                            <asp:ListItem Text="REDACCIÓN" Value="2" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-32 text-right">
                                    <asp:UpdatePanel ID="udpAcciones" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-light">
                                                <i class="fa fa-search"></i>
                                                <span class="text">Listar</span>
                                            </button>
                                            <button type="button" id="btnRegistrarSimple" runat="server" class="btn btn-accion btn-info">
                                                <i class="fa fa-plus"></i>
                                                <span class="text">Pregunta Única</span>
                                            </button>
                                            <button type="button" id="btnRegistrarCompuesta" runat="server" class="btn btn-accion btn-success">
                                                <i class="fa fa-plus"></i>
                                                <span class="text">Pregunta Agrupada</span>
                                            </button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-48">
                                    <asp:UpdatePanel ID="udpGrvList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div id="divGrvList" runat="server">
                                                <hr class="separador">
                                                <asp:GridView ID="grvList" runat="server" AutoGenerateColumns="false" CssClass="table table-sm" ShowFooter="true" GridLines="None"
                                                    OnRowCreated="grvList_OnRowCreated" FooterStyle-CssClass="tfooter-light">
                                                    <Columns>
                                                        <asp:BoundField DataField="nombre_com" HeaderText="COMPETENCIA" />
                                                        <asp:BoundField DataField="nombre_scom" HeaderText="SUB - COMPETENCIA" />
                                                        <asp:BoundField DataField="nombre_ind" HeaderText="INDICADOR" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="cantidad_basica" HeaderText="BÁSICA" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="cantidad_intermedia" HeaderText="INTERMEDIA" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="cantidad_avanzada" HeaderText="AVANZADA" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:TemplateField HeaderText="TOTALES" ItemStyle-HorizontalAlign=Center ItemStyle-CssClass="font-weight-bold">
                                                            <ItemTemplate>
                                                                <%# Eval("cantidad_basica") + Eval("cantidad_intermedia") + Eval("cantidad_avanzada") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="thead-dark" />
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="nav-simple" role="tabpanel" aria-labelledby="nav-simple-tab">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="title">Registro de Pregunta Única</h5>
                        </div>
                        <div class="card-body">
                            <asp:UpdatePanel ID="udpSimple" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-48">
                                            <h1 class="title">Datos Generales:</h1>
                                            <hr>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="cmbCompetenciaSimple" class="form-control-sm col-md-5">Competencia:</label>
                                        <div class="col-md-10">
                                            <asp:DropDownList ID="cmbCompetenciaSimple" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker">
                                            </asp:DropDownList>
                                        </div>
                                        <label for="cmbSubCompetencia" class="form-control-sm col-md-6">Sub Competencia:</label>
                                        <div class="col-md-10">
                                            <asp:UpdatePanel ID="udpSubCompetenciaSimple" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="cmbSubCompetenciaSimple" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectpicker"
                                                        data-live-search="true">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <label for="cmbIndicador" class="form-control-sm col-md-4">Indicador:</label>
                                        <div class="col-md-10">
                                            <asp:UpdatePanel ID="udpIndicadorSimple" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="cmbIndicadorSimple" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                                        data-live-search="true">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="cmbComplejidadSimple" class="form-control-sm col-md-5">Complejidad:</label>
                                        <div class="col-md-10">
                                            <asp:DropDownList ID="cmbComplejidadSimple" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker">
                                                <asp:ListItem Text="-- SELECCIONE --" Value="-1" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-48">
                                            <h1 class="title">Pregunta y Alternativas:</h1>
                                            <hr>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="txtPreguntaSimple" class="form-control-sm col-md-5">Pregunta:</label>
                                        <div class="col-md-42">
                                            <asp:TextBox ID="txtPreguntaSimple" runat="server" TextMode="multiline" Columns="70" Rows="10" CssClass="form-control form-control-sm"
                                                placeholder="Ingrese la pregunta" />
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class="form-control-sm col-md-5">Alternativas:</label>
                                        <div class="col-md-42">
                                            <asp:UpdatePanel ID="udpAlternativasSimple" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <div class="nav nav-tabs" id="nav-tab-simple" role="tablist">
                                                        <asp:Repeater id="rpHeaderAltSimple" runat="server">
                                                            <ItemTemplate>
                                                                <a class='nav-item nav-link<%# IIF(Eval("i") = 1, " active", "") %>' id='nav-alt-tab' data-toggle="tab"
                                                                    href='#nav-alt-simple<%# Eval("i") %>' role="tab" aria-controls='nav-alt<%# Eval("i") %>'
                                                                    aria-selected='<%# IIF(Eval("i") = 1, "true", "false") %>'>Alternativa <%# Eval("i") %></a>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                    <div class="tab-content" id="nav-tab-alternativas-simple">
                                                        <asp:Repeater id="rpBodyAltSimple" runat="server">
                                                            <ItemTemplate>
                                                                <div class='tab-pane fade <%# IIF(Eval("i") = 1, " show active", "") %>' id='nav-alt-simple<%# Eval("i") %>' role="tabpanel"
                                                                    aria-labelledby='nav-alt<%# Eval("i") %>-tab'>
                                                                    <div class="card">
                                                                        <div class="card-body">
                                                                            <div class="row">
                                                                                <div class="col-md-30">
                                                                                    <asp:TextBox ID="txtAlternativaSimple" runat="server" TextMode="multiline" Columns="70" Rows="4"
                                                                                        CssClass="form-control form-control-sm" placeholder='<%# Eval("i") %>' />
                                                                                </div>
                                                                                <div class="col-md-18">
                                                                                    <div class="custom-control custom-radio custom-control-inline">
                                                                                        <input type="radio" id="rbtRespuestaSimple" class="custom-control-input" runat="server"
                                                                                            value='<%# Eval("i") %>'>
                                                                                        <label class="custom-control-label form-control-sm" for="rbtRespuestaSimple">
                                                                                            Marcar como alternativa correcta</label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <hr class="separador">
                                    <div class="row form-group mb-2">
                                        <div class="col-md-20 offset-md-5">
                                            <button type="button" id="btnFakeCancelarSimple" class="btn btn-accion btn-light">
                                                <i class="fa fa-ban"></i>
                                                <span class="text">Cancelar</span>
                                            </button>
                                            <button type="button" id="btnCancelarSimple" runat="server" class="d-none"></button>
                                            <button type="button" id="btnFakeGuardarSimple" class="btn btn-accion btn-primary">
                                                <i class="fa fa-save"></i>
                                                <span class="text">Guardar</span>
                                            </button>
                                            <button type="button" id="btnGuardarSimple" runat="server" class="d-none"></button>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="nav-compuesta" role="tabpanel" aria-labelledby="nav-compuesta-tab">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="title">Registro de Pregunta Agrupada</h5>
                        </div>
                        <div class="card-body">
                            <asp:UpdatePanel ID="udpCompuesta" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-48">
                                            <h1 class="title">Datos Generales:</h1>
                                            <hr>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="txtPreguntaCompuesta" class="form-control-sm col-md-5">Enunciado:</label>
                                        <div class="col-md-42">
                                            <asp:TextBox ID="txtEnunciadoCompuesta" runat="server" TextMode="multiline" Columns="70" Rows="10" CssClass="form-control form-control-sm"
                                                placeholder="Ingrese el enunciado" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-48">
                                            <h1 class="title">Configuración de Preguntas:</h1>
                                            <hr>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="txtCantidadPreguntas" class="form-control-sm col-md-5">N° de Preguntas:</label>
                                        <div class="col-md-6 d-flex">
                                            <asp:TextBox ID="txtCantidadPreguntas" runat="server" CssClass="form-control form-control-sm text-center only-digits" placeholder="Cantidad" Maxlength="2" />
                                        </div>
                                        <div class="col-md-4 d-flex">
                                            <asp:LinkButton type="button" id="btnGenerarPreguntasCompuesta" runat="server" class="btn btn-accion btn-success">
                                                <i class="fa fa-plus"></i>
                                                <span class="text">Generar</span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-48">
                                            <asp:UpdatePanel ID="udpPreguntasCompuesta" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <div id="divPreguntasCompuesta" runat="server">
                                                        <hr class="separador">
                                                        <div class="row">
                                                            <label class="form-control-sm col-md-5">Preguntas:</label>
                                                            <div class="col-md-36">
                                                                <asp:GridView ID="grvPreguntasCompuesta" runat="server" AutoGenerateColumns="false" CssClass="table table-sm" GridLines="None">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="i" HeaderText="N°" ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Center />
                                                                        <asp:TemplateField HeaderText="COMPETENCIA" ItemStyle-HorizontalAlign=Center ItemStyle-CssClass="col-competencia">
                                                                            <ItemTemplate>
                                                                                <span class="competencia">--</span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="SUB COMPETENCIA" ItemStyle-HorizontalAlign=Center ItemStyle-CssClass="col-sub-competencia">
                                                                            <ItemTemplate>
                                                                                <span class="sub-competencia">--</span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="INDICADOR" ItemStyle-HorizontalAlign=Center ItemStyle-CssClass="col-indicador">
                                                                            <ItemTemplate>
                                                                                <span class="indicador">--</span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="COMPLEJIDAD" ItemStyle-HorizontalAlign=Center ItemStyle-CssClass="col-complejidad">
                                                                            <ItemTemplate>
                                                                                <span class="complejidad">--</span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="OPERACIONES" ItemStyle-Width="20%" ItemStyle-HorizontalAlign=Center>
                                                                            <ItemTemplate>
                                                                                <button type="button" id="btnEditar" runat="server" class="btn btn-sm btn-accion btn-primary">
                                                                                    <i class="fa fa-edit"></i>
                                                                                    <span class="text">Editar</span>
                                                                                </button>
                                                                                <div class="modal fade" tabindex="-1" role="dialog" data-backdrop="static" data-index='<%# Eval("i") %>'>
                                                                                    <div class="modal-dialog modal-xl" role="document">
                                                                                        <div class="modal-content">
                                                                                            <div class="modal-header">
                                                                                                <span class="modal-title">Ingrese la pregunta</span>
                                                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                                                    <span aria-hidden="true">&times;</span>
                                                                                                </button>
                                                                                            </div>
                                                                                            <div class="modal-body">
                                                                                                <div class="card">
                                                                                                    <div class="card-header alt-header">
                                                                                                        <h1 class="modal-custom-title">Pregunta <span class="index"></span> de <span
                                                                                                                class="total"></span> </h1>
                                                                                                    </div>
                                                                                                    <div class="card-body">
                                                                                                        <div class="row form-group mt-2">
                                                                                                            <label for="cmbCompetenciaCompuesta" class="form-control-sm col-md-5">Competencia:</label>
                                                                                                            <div class="col-md-10">
                                                                                                                <asp:DropDownList ID="cmbCompetenciaCompuesta" runat="server"
                                                                                                                    OnSelectedIndexChanged="cmbCompetenciaCompuesta_SelectedIndexChanged"
                                                                                                                    AutoPostBack="True" CssClass="form-control form-control-sm selectpicker">
                                                                                                                </asp:DropDownList>
                                                                                                            </div>
                                                                                                            <label for="cmbSubCompetencia" class="form-control-sm col-md-6">Sub
                                                                                                                Competencia:</label>
                                                                                                            <div class="col-md-10">
                                                                                                                <asp:UpdatePanel ID="udpSubCompetenciaCompuesta" runat="server" UpdateMode="Conditional"
                                                                                                                    ChildrenAsTriggers="false">
                                                                                                                    <ContentTemplate>
                                                                                                                        <asp:DropDownList ID="cmbSubCompetenciaCompuesta" runat="server"
                                                                                                                            OnSelectedIndexChanged="cmbSubCompetenciaCompuesta_SelectedIndexChanged"
                                                                                                                            AutoPostBack="True" CssClass="form-control form-control-sm selectpicker"
                                                                                                                            data-live-search="true">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </ContentTemplate>
                                                                                                                </asp:UpdatePanel>
                                                                                                            </div>
                                                                                                            <label for="cmbIndicador" class="form-control-sm col-md-5">Indicador:</label>
                                                                                                            <div class="col-md-10">
                                                                                                                <asp:UpdatePanel ID="udpIndicadorCompuesta" runat="server" UpdateMode="Conditional"
                                                                                                                    ChildrenAsTriggers="false">
                                                                                                                    <ContentTemplate>
                                                                                                                        <asp:DropDownList ID="cmbIndicadorCompuesta" runat="server" AutoPostBack="false"
                                                                                                                            CssClass="form-control form-control-sm selectpicker"
                                                                                                                            data-live-search="true">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </ContentTemplate>
                                                                                                                </asp:UpdatePanel>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row form-group">
                                                                                                            <label for="cmbComplejidadCompuesta" class="form-control-sm col-md-5">Complejidad:</label>
                                                                                                            <div class="col-md-10">
                                                                                                                <asp:DropDownList ID="cmbComplejidadCompuesta" runat="server" AutoPostBack="false"
                                                                                                                    CssClass="form-control form-control-sm selectpicker">
                                                                                                                    <asp:ListItem Text="-- SELECCIONE --" Value="-1" />
                                                                                                                </asp:DropDownList>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row form-group">
                                                                                                            <label for="txtPreguntaCompuesta" class="form-control-sm col-md-5">Pregunta:</label>
                                                                                                            <div class="col-md-41">
                                                                                                                <asp:TextBox ID="txtPreguntaCompuesta" runat="server" TextMode="multiline" Columns="70"
                                                                                                                    Rows="10" CssClass="form-control form-control-sm"
                                                                                                                    placeholder="Ingrese la pregunta" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row form-group">
                                                                                                            <label class="form-control-sm col-md-5">Alternativas:</label>
                                                                                                            <div class="col-md-41">
                                                                                                                <asp:UpdatePanel ID="udpAlternativasCompuesta" runat="server" UpdateMode="Conditional"
                                                                                                                    ChildrenAsTriggers="false">
                                                                                                                    <ContentTemplate>
                                                                                                                        <div class="nav nav-tabs" id="nav-tab-compuesta" role="tablist">
                                                                                                                            <asp:Repeater id="rpHeaderAltCompuesta" runat="server">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <a class='nav-item nav-link<%# IIF(Eval("j") = 1, " active", "") %>'
                                                                                                                                        data-toggle="tab"
                                                                                                                                        href='#nav-alt-compuesta<%# Eval("k") & Eval("j") %>' role="tab"
                                                                                                                                        aria-controls='nav-alt<%# Eval("k") & Eval("j") %>'
                                                                                                                                        aria-selected='<%# IIF(Eval("j") = 1, "true", "false") %>'>Alternativa
                                                                                                                                        <%# Eval("j") %></a>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:Repeater>
                                                                                                                        </div>
                                                                                                                        <div class="tab-content" id="nav-tab-alternativas-compuesta">
                                                                                                                            <asp:Repeater id="rpBodyAltCompuesta" runat="server">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <div class='tab-pane fade <%# IIF(Eval("j") = 1, " show active", "") %>'
                                                                                                                                        id='nav-alt-compuesta<%# Eval("k") & Eval("j") %>'
                                                                                                                                        role="tabpanel"
                                                                                                                                        aria-labelledby='nav-alt<%# Eval("k") & Eval("j") %>-tab'>
                                                                                                                                        <div class="card">
                                                                                                                                            <div class="card-body">
                                                                                                                                                <div class="row">
                                                                                                                                                    <div class="col-md-32">
                                                                                                                                                        <asp:TextBox ID="txtAlternativaCompuesta"
                                                                                                                                                            runat="server" TextMode="multiline"
                                                                                                                                                            Columns="70" Rows="4"
                                                                                                                                                            CssClass="form-control form-control-sm"
                                                                                                                                                            placeholder='<%# Eval("j") %>' />
                                                                                                                                                    </div>
                                                                                                                                                    <div class="col-md-16">
                                                                                                                                                        <div
                                                                                                                                                            class="custom-control custom-radio custom-control-inline">
                                                                                                                                                            <input type="radio"
                                                                                                                                                                id="rbtRespuestaCompuesta"
                                                                                                                                                                class="custom-control-input"
                                                                                                                                                                runat="server"
                                                                                                                                                                value='<%# Eval("k") & Eval("j") %>'>
                                                                                                                                                            <label
                                                                                                                                                                class="custom-control-label form-control-sm"
                                                                                                                                                                for="rbtRespuestaCompuesta">
                                                                                                                                                                Marcar como alternativa correcta</label>
                                                                                                                                                        </div>
                                                                                                                                                    </div>
                                                                                                                                                </div>
                                                                                                                                            </div>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:Repeater>
                                                                                                                        </div>
                                                                                                                    </ContentTemplate>
                                                                                                                </asp:UpdatePanel>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="modal-footer">
                                                                                                <button type="button" class="btn btn-primary aceptar" data-dismiss="modal">Aceptar</button>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="thead-dark" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <hr class="separador">
                                    <div class="row form-group mb-2">
                                        <div class="col-md-20 offset-md-5">
                                            <button type="button" id="btnFakeCancelarCompuesta" class="btn btn-accion btn-light">
                                                <i class="fa fa-ban"></i>
                                                <span class="text">Cancelar</span>
                                            </button>
                                            <button type="button" id="btnCancelarCompuesta" runat="server" class="d-none"></button>
                                            <button type="button" id="btnFakeGuardarCompuesta" class="btn btn-accion btn-primary">
                                                <i class="fa fa-save"></i>
                                                <span class="text">Guardar</span>
                                            </button>
                                            <button type="button" id="btnGuardarCompuesta" runat="server" class="d-none"></button>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="nav-editor" role="tabpanel" aria-labelledby="nav-editor-tab">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="title">Registro / Edición</h5>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-48">
                                    <h1 class="title">Sub título</h1>
                                    <hr>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label for="divPlantilla" class="form-control-sm col-md-5">Campo 7:</label>
                                <div class="col-md-36">
                                    <asp:HiddenField ID="txtPlantilla" runat="server" />
                                    <div id="divPlantilla" class="summernote"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlMensajeServidor" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span id="spnMenServTitulo" runat="server" class="modal-title">Respuesta del Servidor</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div id="divMenServMensaje" class="alert alert-warning" runat="server"></div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlMensajeClienteConfirmar" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span class="modal-title">Confirmar Operación</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div id="divMensajeConfirmar"></div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" id="btnConfCancelar" data-dismiss="modal">Cancelar</button>
                        <button type="button" class="btn btn-primary" id="btnConfContinuar">Continuar</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlVistaPreviaPreguntaSimple" class="modal fade md-preguntas" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-xl" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span class="modal-title">Revisión y Confirmación</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="card">
                            <div class="card-header dark-header">
                                <h5 class="title">Pregunta Simple</h5>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-48 texto"></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-48">
                                        <ul class="list-group list-sm alternativas"></ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="alert alert-danger mt-2">
                            <div class="custom-control custom-checkbox">
                                <input type="checkbox" class="custom-control-input" id="chkContinuarSimple">
                                <label class="custom-control-label" for="chkContinuarSimple">
                                    <b>Al confirmar la operación se registrarán los datos y estos ya no podrán ser modificados. ¿Está seguro de continuar?</b>
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" id="btnVistaPrevCancelarSimple" data-dismiss="modal">Cancelar</button>
                        <button type="button" class="btn btn-primary" id="btnVistaPrevContinuarSimple">Confirmar</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlVistaPreviaPreguntaCompuesta" class="modal fade md-preguntas" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-xl" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span class="modal-title">Revisión y Confirmación</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="card">
                            <div class="card-header dark-header">
                                <h5 class="title">Enunciado</h5>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-48 enunciado"></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-48 preguntas">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="alert alert-danger mt-2">
                            <div class="custom-control custom-checkbox">
                                <input type="checkbox" class="custom-control-input" id="chkContinuarCompuesta">
                                <label class="custom-control-label" for="chkContinuarCompuesta">
                                    <b>Al confirmar la operación se registrarán los datos y estos ya no podrán ser modificados. ¿Está seguro de continuar?</b>
                                </label>
                            </div>
                        </div>
                        <div class="d-none template-pregunta">
                            <div class="row">
                                <div class="col-md-48">
                                    <div class="card">
                                        <div class="card-header alt-header">
                                            <h5 class="title">Pregunta N°</h5>
                                        </div>
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-md-48 texto"></div>
                                                <div class="col-md-48">
                                                    <ul class="list-group list-sm alternativas"></ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" id="btnVistaPrevCancelarCompuesta" data-dismiss="modal">Cancelar</button>
                        <button type="button" class="btn btn-primary" id="btnVistaPrevContinuarCompuesta">Confirmar</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- Scripts externos -->
    <script src="<%=ClsGlobales.PATH_ASSETS %>/jquery/jquery-3.3.1.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-4.1/js/bootstrap.bundle.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-select-1.13.1/js/i18n/defaults-es_ES.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/toastr-2.1.4-7/toastr.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/summernote-0.8.18/summernote-bs4.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/summernote-0.8.18/lang/summernote-es-ES.min.js"></script>
    <!-- Scripts propios -->
    <script src="<%=ClsGlobales.PATH_JS %>/funciones.js"></script>
    <script src="<%=ClsGlobales.PATH_JS %>/resumenBancoPreguntas.js?x=2"></script>

    <script type="text/javascript">
        var controlId = ''
        var controlsId = [];

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id;
            controlsId = [];

            alternarLoadingGif('global', false);

            switch (controlId) {
                case 'btnListar':
                    controlsId.push(controlId);
                    break;
                case 'btnRegistrarSimple':
                case 'btnRegistrarCompuesta':
                    controlsId.push(controlId);
                    break;
                // PREGUNTA SIMPLE
                case 'btnCancelarSimple':
                    controlsId.push(controlId);
                    controlsId.push('btnFakeGuardarSimple');
                    break;
                case 'btnGuardarSimple':
                    controlsId.push(controlId);
                    controlsId.push('btnVistaPrevContinuarSimple');
                    controlsId.push('btnVistaPrevCancelarSimple');
                    break;
                // PREGUNTA SIMPLE
                case 'btnGenerarPreguntasCompuesta':
                    controlsId.push(controlId);
                    break;
                case 'btnCancelarCompuesta':
                    controlsId.push(controlId);
                    controlsId.push('btnFakeGuardarCompuesta');
                    controlsId.push('btnCancelarCompuesta');
                    break;
                case 'btnGuardarCompuesta':
                    controlsId.push(controlId);
                    controlsId.push('btnVistaPrevContinuarCompuesta');
                    controlsId.push('btnVistaPrevCancelarCompuesta');
                    break;
            }

            // Desactivo los botones
            controlsId.forEach(function (el) {
                atenuarBoton(el, false);
            });
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
            var error = args.get_error();
            if (error) {
                mostrarMensajeModal(-1, error);

                controlsId.forEach(function (el) {
                    atenuarBoton(el, true);
                });
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (sender, args) {
            var updatedPanels = args.get_panelsUpdated();

            var udpFormUpdated = false
            for (var i = 0; i < updatedPanels.length; i++) {
                var udpPanelId = updatedPanels[i].id;

                switch (udpPanelId) {
                    case 'udpSubCompetenciaSimple':
                    case 'udpIndicadorSimple':
                        initFormSelectPickers('S');
                        break;
                    case 'udpAlternativasSimple':
                        configRadioNames('S');
                        break;
                    case 'udpIndicadorCompuesta':
                        initFormSelectPickers('C');
                        break;
                }

                if (udpPanelId.indexOf('udpSubCompetenciaCompuesta') > -1
                    || udpPanelId.indexOf('udpIndicadorCompuesta')) {
                    initSelectPickersContainer(udpPanelId);
                }
            }
        });

        Sys.Application.add_load(function () {
            alternarLoadingGif('global', true);

            // Vuelvo a activar los botones
            controlsId.forEach(function (el) {
                atenuarBoton(el, true);
            });

            switch (controlId) {
                case 'btnCancelarSimple':
                    accionConfirmadaFinalizada();
                    break;
                case 'btnGuardarSimple':
                    checkRptaGuardar('S');
                    break;
                case 'btnCancelarCompuesta':
                    accionConfirmadaFinalizada();
                    break;
                case 'btnGuardarCompuesta':
                    checkRptaGuardar('C');
                    break;
            }

            verificarParametros('TAB|MEN_SERV|TOASTR');
            hideGrvTooltips();
        });    
    </script>
</body>

</html>