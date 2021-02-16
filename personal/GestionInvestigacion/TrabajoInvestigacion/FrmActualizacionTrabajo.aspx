<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmActualizacionTrabajo.aspx.vb"
    Inherits="FrmActualizacionTrabajo" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Actualización de trabajo de bachiller para obtención de grado de Bachiller
    
    
    </title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel='stylesheet' href='../../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../../assets/css/style.css?x=1' />
    <link href="../../assets/font-awesome-4.7.0/css/font-awesome.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src='../../assets/js/jquery.js'></script>

    <script src="../../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <%-- ======================= Inicio Notificaciones ============================================= --%>

    <script type="text/javascript" src="../../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../../assets/js/noty/notifications-custom.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            LoadingSemestre();
            LoadingCarrera();
        })
        function fnLoading(sw) {
            if (sw) {
                $('.piluku-preloader').removeClass('hidden');
            } else {
                $('.piluku-preloader').addClass('hidden');
            }

        }
        function LoadingSemestre() {
            $("#ddlSemestre").change(function() {
                fnLoading(true);
            });
        }
        function LoadingCarrera() {
            $("#ddlCarrera").change(function() {
                fnLoading(true);
            });
        }
        function LoadingAsignatura() {
            $("#ddlAsignatura").change(function() {
                fnLoading(true);
            });
        }
        function Validar() {
            if ($("#txtTitulo").val().trim() == "") {
                fnMensaje("error", "Ingrese el título del trabajo de investigación")
                return false;
            }
            if ($("#ddlTipoTrabajo").val() == "0") {
                fnMensaje("error", "Ingrese el tipo de trabajo de investigación")
                return false;
            }
            if ($("#ddlLinea").val() == "0") {
                fnMensaje("error", "Seleccione la línea de investigación USAT del trabajo de investigación")
                return false;
            }
            if ($("#ddlArea").val() == "0") {
                fnMensaje("error", "Seleccione el área OCDE del trabajo de investigación")
                return false;
            }
            if ($("#ddlSubArea").val() == "0") {
                fnMensaje("error", "Seleccione la Subárea OCDE del trabajo de investigación")
                return false;
            }
            if ($("#ddlDisciplina").val() == "0") {
                fnMensaje("error", "Seleccione la disciplina OCDE del trabajo de investigación")
                return false;
            }
            if ($("#archivo").val() == "" && $("#hdcod").val() == "0") {
                fnMensaje("error", "Debe seleccionar un archivo de trabajo de investigación.");
                $("#archivo").focus();
                return false;
            }
            if ($("#archivo").val() != '') {

                if ($("#archivo")[0].files[0].size >= 20971520) {
                    fnMensaje("error", "solo se pueden adjuntar archivos de trabajo de investigación de máximo 20MB");
                    return false;
                }

                archivo = $("#archivo").val();
                //Extensiones Permitidas
                //        extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
                extensiones_permitidas = new Array(".pdf", ".rar", ".zip");
                //recupero la extensión de este nombre de archivo
                // recorto el nombre desde la derecha 4 posiciones atras (Ubicación de la Extensión)
                archivo = archivo.substring(archivo.length - 5, archivo.length);
                //despues del punto de nombre recortado
                extension = (archivo.substring(archivo.lastIndexOf("."))).toLowerCase();
                //compruebo si la extensión está entre las permitidas 
                permitida = false;
                for (var i = 0; i < extensiones_permitidas.length; i++) {
                    if (extensiones_permitidas[i] == extension) {
                        permitida = true;
                        break;
                    }
                }
                if (permitida == false) {
                    fnMensaje("error", "Solo puede adjuntar archivos de trabajo de investigación en formato de PDF, rar y zip");
                    return false;
                }
            }

            if ($("#archivorubrica").val() == "" && $("#hdcod").val() == "0") {
                fnMensaje("error", "Debe seleccionar un archivo de rúbrica de investigación.");
                $("#archivorubrica").focus();
                return false;
            }
            if ($("#archivorubrica").val() != '') {

                if ($("#archivorubrica")[0].files[0].size >= 20971520) {
                    fnMensaje("error", "solo se pueden adjuntar archivos de rúbrica de máximo 20MB");
                    return false;
                }

                archivo = $("#archivorubrica").val();
                //Extensiones Permitidas
                //        extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
                extensiones_permitidas = new Array(".pdf", ".rar", ".zip");
                //recupero la extensión de este nombre de archivo
                // recorto el nombre desde la derecha 4 posiciones atras (Ubicación de la Extensión)
                archivo = archivo.substring(archivo.length - 5, archivo.length);
                //despues del punto de nombre recortado
                extension = (archivo.substring(archivo.lastIndexOf("."))).toLowerCase();
                //compruebo si la extensión está entre las permitidas 
                permitida = false;
                for (var i = 0; i < extensiones_permitidas.length; i++) {
                    if (extensiones_permitidas[i] == extension) {
                        permitida = true;
                        break;
                    }
                }
                if (permitida == false) {
                    fnMensaje("error", "Solo puede adjuntar archivos de rúbrica en formato de PDF, rar y zip");
                    return false;
                }
            }
            
            if (!confirm("¿Está seguro que desea guardar los datos?")) {
                return false;
            }
            return true
        }
        function fnDescargar(id_ar) {
            var d = new Date();
            window.open("../../Descargar.aspx?Id=" + id_ar + "&h=" + d.getHours().toString() + d.getMinutes().toString() + d.getSeconds().toString());
        }
        function ValidarEliminar() {
            if ($("#hdacta").val() != "0") {
                fnMensaje("error", "Para poder eliminar el archivo de rúbrica no debe contar con un acta registrada");
                return false;
            }
            if (!confirm('¿Está seguro que desea eliminar rúbrica?')) {
                return false;
            }
            return true;
        }
    </script>

    <style type="text/css">
        body
        {
            padding-right: 0 !important;
        }
        .modal-open
        {
            overflow: inherit;
        }
        .form table th
        {
            text-align: center;
        }
        .form-group
        {
            margin: 6px;
        }
        .bootstrap-select .dropdown-toggle .filter-option
        {
            position: relative;
            padding-top: 0px;
            padding-bottom: 0px;
            padding-left: 0px;
        }
        .dropdown-menu open
        {
            min-width: 0px;
            max-width: 500px;
        }
        .form-control
        {
            color: Black;
        }
        .bootstrap-select .dropdown-toggle .filter-option
        {
            position: relative;
            padding-top: 0px;
            padding-bottom: 0px;
            padding-left: 0px;
        }
        .dropdown-menu open
        {
            min-width: 0px;
            max-width: 500px;
        }
        .table > thead > tr > th
        {
            color: White;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
        }
        .table > tbody > tr > td
        {
            color: black;
            vertical-align: middle;
        }
        .table tbody tr th
        {
            color: White;
            font-size: 11px;
            font-weight: bold;
            text-align: center;
        }
    </style>
</head>
<body class="">
    <div class="container-fluid">
        <form id="form1" runat="server" enctype="multipart/form-data">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="updLoading">
            <ContentTemplate>
                <div class="piluku-preloader text-center hidden">
                    <div class="loader">
                        Loading...</div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlSemestre" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlCarrera" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="rowcommand" />
                <asp:AsyncPostBackTrigger ControlID="btnCerrar" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;">
                <h5>
                    <b>Actualizar trabajos de investigación para obtención de Bachiller </b>
                </h5>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label ID="lblSemestre" runat="server" CssClass="col-sm-1 col-md-1 control-label"
                            for="ddlSemestre">Semestre</asp:Label>
                        <div class="col-sm-3 col-md-2">
                            <asp:DropDownList runat="server" ID="ddlSemestre" CssClass="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <asp:Label ID="Label5" runat="server" CssClass="col-sm-1 col-md-1 control-label"
                            for="ddlCarrera">Escuela</asp:Label>
                        <asp:UpdatePanel runat="server" ID="updCarrera" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="col-sm-6 col-md-7">
                                    <asp:DropDownList runat="server" ID="ddlCarrera" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlSemestre" EventName="selectedindexchanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label6" runat="server" CssClass="col-sm-1 col-md-1 control-label">Asignatura</asp:Label>
                        <div class="col-sm-10 col-md-10">
                            <asp:UpdatePanel runat="server" ID="updAsignatura" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:DropDownList runat="server" ID="ddlAsignatura" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlCarrera" EventName="selectedindexchanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlSemestre" EventName="selectedindexchanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <asp:LinkButton ID="btnConsultar" runat="server" Text='<span class="fa fa-search"></span>'
                                CssClass="btn btn-primary btn-radius" ToolTip="Buscar" OnClientClick="fnLoading(true);"></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <br />
                <%--                <asp:UpdatePanel runat="server" ID="updGeneral" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        --%>
                <asp:UpdatePanel runat="server" ID="updGrilla" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div runat="server" id="divLista">
                            <div class="form-group" runat="server">
                            </div>
                            <div class="form-group" runat="server">
                                <div runat="server" id="lblmensaje">
                                </div>
                                <asp:GridView runat="server" ID="gvAlumnos" CssClass="table table-condensed" DataKeyNames="codigo_tba,codigo_cac,codigo_alu"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" HeaderStyle-Width="2%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIVER" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="alumno" HeaderText="ESTUDIANTE" HeaderStyle-Width="36%" />
                                        <asp:BoundField DataField="titulo_tba" HeaderText="TÍTULO" HeaderStyle-Width="50%" />
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="2%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnVer" runat="server" Text='<span class="fa fa-pencil-square-o"></span>'
                                                    CssClass="btn btn-info btn-sm btn-radius" ToolTip="Registrar" CommandName="Ver"
                                                    CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>' OnClientClick="fnLoading(true);">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle Font-Size="12px" Font-Bold="true" BackColor="#D9534F" ForeColor="white" />
                                    <RowStyle Font-Size="12px" />
                                    <EmptyDataTemplate>
                                        <b>No se encontraron alumnos</b>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="ddlSemestre" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlCarrera" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlAsignatura" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="rowcommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnCerrar" EventName="click" />
                        <asp:PostBackTrigger ControlID="btnGuardar" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel runat="server" ID="updMantenimiento" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="panel panel-default" runat="server" id="DivMantenimiento" visible="false">
                            <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;">
                                <b>Trabajo de investigación para obtención de Bachiller </b>
                            </div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <asp:HiddenField runat="server" ID="hdcac" Value="0" />
                                    <asp:HiddenField runat="server" ID="hdalu" Value="0" />
                                    <asp:HiddenField runat="server" ID="hdcod" Value="0" />
                                    <asp:HiddenField runat="server" ID="hdrubrica" Value="0" />
                                    <asp:HiddenField runat="server" ID="hdacta" Value="0" />
                                    <div runat="server" id="lblMensajeRegistro">
                                    </div>
                                    <div runat="server" id="Alumnos">
                                    </div>
                                    <%--  <div class="form-group">
                                        <asp:Label ID="Label15" runat="server" CssClass="col-sm-3 col-md-3 control-label">Carrera Profesional</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:TextBox runat="server" ID="txtCarrera" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                    <div class="form-group">
                                        <asp:Label ID="Label17" runat="server" CssClass="col-sm-3 col-md-3 control-label">Título</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:TextBox runat="server" ID="txtTitulo" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="col-sm-3 col-md-3 control-label">Tipo de trabajo</asp:Label>
                                        <div class="col-sm-6 col-md-6">
                                            <asp:DropDownList runat="server" ID="ddlTipoTrabajo" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label7" runat="server" CssClass="col-sm-3 col-md-3 control-label">Línea de investigación USAT</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlLinea" CssClass="form-control" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlLinea" EventName="selectedindexchanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label8" runat="server" CssClass="col-sm-3 col-md-3 control-label">Área OCDE</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:UpdatePanel runat="server" ID="updArea" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlArea" CssClass="form-control" AutoPostBack="true">
                                                        <asp:ListItem Value="0">[-- SELECCIONE --]</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlLinea" EventName="selectedindexchanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label9" runat="server" CssClass="col-sm-3 col-md-3 control-label">SubÁrea OCDE</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlSubArea" CssClass="form-control" AutoPostBack="true">
                                                        <asp:ListItem Value="0">[-- SELECCIONE --]</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlLinea" EventName="selectedindexchanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlArea" EventName="selectedindexchanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label10" runat="server" CssClass="col-sm-3 col-md-3 control-label">Disciplina OCDE</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlDisciplina" CssClass="form-control">
                                                        <asp:ListItem Value="0">[-- SELECCIONE --]</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlLinea" EventName="selectedindexchanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlArea" EventName="selectedindexchanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlSubArea" EventName="selectedindexchanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label18" runat="server" CssClass="col-sm-3 col-md-3 control-label">Archivo</asp:Label>
                                        <div class="col-sm-4 col-md-4">
                                            <asp:FileUpload runat="server" ID="archivo" />
                                            <ul>
                                                <li>Archivos permitidos: <span style="color: Red">.pdf, .rar, .zip</span></li>
                                                <li>Tamaño Máximo: <span style="color: Red">20 Mb</span></li>
                                            </ul>
                                        </div>
                                        <div class="col-sm-4 col-md-4" id="divArchivoTrabajo" runat="server">
                                            <ul>
                                                <li><span style="color: Red; font-weight: bold;">No cuenta con archivo de trabajo</span></li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="col-sm-3 col-md-3 control-label">Rúbrica</asp:Label>
                                        <div class="col-sm-4 col-md-4">
                                            <asp:FileUpload runat="server" ID="archivorubrica" />
                                            <ul>
                                                <li>Archivos permitidos: <span style="color: Red">.pdf, .rar, .zip</span></li>
                                                <li>Tamaño Máximo: <span style="color: Red">20 Mb</span></li>
                                            </ul>
                                        </div>
                                        <div class="col-sm-2 col-md-2" id="divArchivoRubrica" runat="server">
                                            <ul>
                                                <li><span style="color: Red; font-weight: bold;">No cuenta con archivo de rúbrica</span></li>
                                            </ul>
                                        </div>
                                        <div class="col-sm-1 col-md-1">
                                            <asp:LinkButton runat="server" ID="btnEliminarRubrica" CssClass="btn btn-sm btn-danger btn-radius"
                                                Visible="false" Text="<span class='fa fa-close'></span>" ToolTip="Eliminar rúbrica"
                                                OnClientClick="return ValidarEliminar();">
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" CssClass="col-sm-3 col-md-3 control-label">Acta</asp:Label>
                                        <div class="col-sm-2 col-md-2" id="divArchivoActa" runat="server">
                                            <ul>
                                                <li><span style="color: Red; font-weight: bold;">No cuenta con archivo de acta</span></li>
                                            </ul>
                                        </div>
                                        <div class="col-sm-1 col-md-1">
                                            <asp:LinkButton runat="server" ID="btnEliminarActa" CssClass="btn btn-sm btn-danger btn-radius"
                                                Visible="false" Text="<span class='fa fa-close'></span>" ToolTip="Eliminar Acta"
                                                OnClientClick="return confirm('¿Está seguro que desea eliminar acta?')">
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <center>
                                    <asp:UpdatePanel runat="server" ID="updbotones" UpdateMode="conditional">
                                        <ContentTemplate>
                                            <asp:Button runat="server" ID="btnGuardar" CssClass="btn btn-primary" Text="Guardar"
                                                OnClientClick="return Validar();" />
                                            <asp:Button runat="server" ID="btnCerrar" CssClass="btn btn-danger" Text="Cerrar"
                                                OnClientClick="fnLoading(true);" />
                                            <triggers>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="RowCommand" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </center>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlSemestre" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlCarrera" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlAsignatura" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="rowcommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnEliminarRubrica" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="btnEliminarActa" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCerrar" EventName="click" />
                        <asp:PostBackTrigger ControlID="btnGuardar" />
                    </Triggers>
                </asp:UpdatePanel>
                <%--
            <div class="form-group">
                <div class="modal fade" id="mdEditar" role="dialog" aria-labelledby="myModalLabel"
                    style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                    <div class="modal-dialog modal-lg" id="Div2">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                    <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                </button>
                                <h4 class="modal-title" id="H1">
                                    Datos Trabajo de investigación
                                </h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" CssClass="col-sm-3 col-md-3 control-label">Código Universitario</asp:Label>
                                        <div class="col-sm-3 col-md-2">
                                            <asp:HiddenField runat="server" ID="hdcac" Value="0" />
                                            <asp:HiddenField runat="server" ID="hdcod" Value="0" />
                                            <asp:TextBox runat="server" ID="txtcodigouniver" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="Label14" runat="server" CssClass="col-sm-2 col-md-1 control-label">Estudiante</asp:Label>
                                        <div class="col-sm-4 col-md-6">
                                            <asp:TextBox runat="server" ID="txtestudiante" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label15" runat="server" CssClass="col-sm-3 col-md-3 control-label">Carrera Profesional</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:TextBox runat="server" ID="txtCarrera" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label17" runat="server" CssClass="col-sm-3 col-md-3 control-label">Título</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:TextBox runat="server" ID="txtTitulo" ReadOnly="true" TextMode="MultiLine" Rows="3"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="col-sm-3 col-md-3 control-label">Tipo de trabajo</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:TextBox runat="server" ID="txtTipo" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label7" runat="server" CssClass="col-sm-3 col-md-3 control-label">Línea de investigación USAT</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:DropDownList runat="server" ID="ddlLinea" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label8" runat="server" CssClass="col-sm-3 col-md-3 control-label">Área OCDE</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:TextBox runat="server" ID="txtArea" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label9" runat="server" CssClass="col-sm-3 col-md-3 control-label">SubÁrea OCDE</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:TextBox runat="server" ID="txtSubArea" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label10" runat="server" CssClass="col-sm-3 col-md-3 control-label">Disciplina OCDE</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:TextBox runat="server" ID="txtDisciplina" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label18" runat="server" CssClass="col-sm-3 col-md-3 control-label">Archivo</asp:Label>
                                        <div class="col-sm-5 col-md-5" id="divArchivoTrabajo" runat="server">
                                        </div>
                                    </div>
                                    <asp:UpdatePanel runat="server" ID="updarchivos" UpdateMode="conditional">
                                        <ContentTemplate>
                                            <div class="form-group">
                                               <div runat="server" id="DivRubrica">
                                
                                                    <asp:Label ID="Label12" runat="server" CssClass="col-sm-3 col-md-3 control-label">Adjuntar Rúbrica</asp:Label>
                                                    <div class="col-sm-8 col-md-8">
                                                        <asp:FileUpload runat="server" ID="archivorubrica" CssClass="form-control" />
                                                        <ul>
                                                            <li>Archivos permitidos: <span style="color: Red">.pdf, .doc, .docx</span></li>
                                                            <li>Tamaño Máximo: <span style="color: Red">20 Mb</span></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 col-md-3">
                                                </div>
                                                <div class="col-sm-2 col-md-2" runat="server" id="divArchivoRubrica">
                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" id="condicion">
                                                <asp:Label ID="Label2" runat="server" CssClass="col-sm-3 col-md-3 control-label">Condición</asp:Label>
                                                <div class="col-sm-4 col-md-4">
                                                    <asp:DropDownList runat="server" ID="ddlCondicion" CssClass="form-control">
                                                        <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                        <asp:ListItem Value="A">APROBADO</asp:ListItem>
                                                        <asp:ListItem Value="D">DESAPROBADO</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
        
                                                <div class="col-sm-3 col-md-3">
                                                </div>
                                                <div class="col-sm-2 col-md-2" runat="server" id="DivArchivoActa">
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="RowCommand" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <center>
                                    <asp:UpdatePanel runat="server" ID="updbotones" UpdateMode="conditional">
                                        <ContentTemplate>
                                            <asp:Button runat="server" ID="btnGuardar" CssClass="btn btn-primary" Text="Guardar"
                                                OnClientClick="return Validar();" />
                                            <asp:Button runat="server" ID="btnCerrar" CssClass="btn btn-danger" Text="Cerrar" />
                                            <triggers>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="RowCommand" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </ContentTemplate>
            <triggers>
                        <asp:PostBackTrigger ControlID="btnGuardar" />
                        <asp:AsyncPostBackTrigger ControlID="btnCerrar" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="RowCommand" />
                    </triggers>
            </asp:UpdatePanel>--%>
            </div>
        </div>
        </form>
    </div>
</body>
</html>
