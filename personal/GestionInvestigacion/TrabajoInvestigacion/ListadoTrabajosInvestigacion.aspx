<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListadoTrabajosInvestigacion.aspx.vb"
    Inherits="ListadoTrabajosInvestigacion" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Trabajos de investigación/Artículos científicos para obtención de Bachiller
    
    
    
    
    </title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel='stylesheet' href='../../assets/css/bootstrap.min.css?x=1' />
    <link href="../../assets/font-awesome-4.7.0/css/font-awesome.css" rel="stylesheet"
        type="text/css" />
    <%-- ======================= Lista desplegable con busqueda =============================================--%>
    <link href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src='../../assets/js/jquery.js'></script>

    <script src="../../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Lista desplegable con busqueda =============================================--%>

    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js" type="text/javascript"></script>

    <script type="text/javascript">

        function MostrarModal(div) {
            $("#" + div).modal('show');
        }

        function CerrarModal(div) {
            $("#" + div).modal('hide');
        }
        function fnMensaje(typ, msje) {
            var n = noty({
                text: msje,
                type: typ,
                timeout: 3000,
                modal: false,
                dismissQueue: true,
                theme: 'defaultTheme'

            });
        }
        function fnLoading(sw) {
            if (sw) {
                $('.piluku-preloader').removeClass('hidden');
            } else {
                $('.piluku-preloader').addClass('hidden');
            }
            //console.log(sw);
        }
        function ValidarRubrica() {
            if ($("#archivorubrica").val() == '') {
                fnMensaje("error", "seleccione un archivo de rúbrica");
                return false;
            }
            if ($("#archivorubrica").val() != '') {

                if ($("#archivorubrica")[0].files[0].size >= 20971520) {
                    fnMensaje("error", "Solo se pueden adjuntar archivos de máximo 20MB");
                    return false;
                }
                archivo = $("#archivorubrica").val();
                //Extensiones Permitidas
                //        extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
                extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
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

                    fnMensaje("error", "Solo puede adjuntar archivos de proyecto en formato .pdf, .doc, .docx");

                    return false;
                }
            }
            if (!confirm('¿Está seguro que desea guardar rúbrica?')) {
                return false;
            }

            return true;
        }
        /*
        function ValidarActa() {
            if ($("#archivoacta").val() == '') {
                fnMensaje("error", "seleccione un archivo de acta");
                return false;
            }
            if ($("#archivoacta").val() != '') {

                if ($("#archivoacta")[0].files[0].size >= 20971520) {
                    fnMensaje("error", "Solo se pueden adjuntar archivos de máximo 20MB");
                    return false;
                }
                archivo = $("#archivoacta").val();
                //Extensiones Permitidas
                //        extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
                extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
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

                    fnMensaje("error", "Solo puede adjuntar archivos de proyecto en formato .pdf, .doc, .docx");

                    return false;
                }
            }
            if ($("#ddlCondicion").val() == "") {
                fnMensaje("error", "Debe seleccionar condición de Trabajo de investigación");
                return false;
            }
            if (!confirm('¿Está segúro que desea Guardar acta?')) {
                return false;
            }

            return true;

        }*/
        function fnDescargar(id_ar) {
            var d = new Date();
            window.open("../../Descargar.aspx?Id=" + id_ar + "&h=" + d.getHours().toString() + d.getMinutes().toString() + d.getSeconds().toString());
        }
        function Validar() {
            if ($("#ddlCondicion").val() == "") {
                fnMensaje("error", "Debe seleccionar condición de Trabajo de investigación");            
                return false;
            }
            if (!confirm("¿Está seguro que desea guardar y generar acta con la condición seleccionada?")) {
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
        table tbody tr th
        {
            text-align: center;
        }
    </style>
</head>
<body class="">
    <div class="container-fluid">
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;">
                <b>Trabajos de investigación/Artículos científicos para obtención de Bachiller </b>
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
                        <div class="col-sm-6 col-md-7">
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
                        <asp:Label ID="Label1" runat="server" CssClass="col-sm-1 col-md-1 control-label">Estado</asp:Label>
                        <div class="col-sm-3 col-md-2">
                            <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control">
                                <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <asp:LinkButton ID="btnConsultar" runat="server" Text='<span class="fa fa-search"></span>'
                                CssClass="btn btn-primary" ToolTip="Buscar"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-4 col-md-4">
                            <table>
                                <tr>
                                    <td style="width: 15%; padding: 4px;">
                                        <asp:LinkButton ID="btnDescargar1" runat="server" Text='' CssClass="btn btn-warning btn-sm btn-radius"
                                            Font-Size="11px" ToolTip="Descargar" OnClientClick="return false;">
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 85%;">
                                        <asp:Label ID="Label11" runat="server" CssClass="control-label">PENDIENTE</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <table>
                                <tr>
                                    <td style="width: 15%; padding: 4px;">
                                        <asp:LinkButton ID="LinkButton2" runat="server" Text='' CssClass="btn btn-sm btn-success btn-radius"
                                            Font-Size="11px" ToolTip="ATENDIDO" OnClientClick="return false;">
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 85%;">
                                        <asp:Label ID="Label19" runat="server" CssClass="control-label">ATENDIDO</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <br />
                <asp:UpdatePanel runat="server" ID="updGeneral" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="piluku-preloader text-center hidden">
                            <div class="loader">
                                Loading...</div>
                        </div>
                        <asp:UpdatePanel runat="server" ID="updGrilla" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="form-group">
                                    <div runat="server" id="lblmensaje">
                                    </div>
                                    <asp:GridView runat="server" ID="gvAlumnos" CssClass="table table-condensed" DataKeyNames="codigo_tba,codigo_cac,estado,codigo_tat"
                                        AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-Width="3%">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIVER" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="responsable" HeaderText="ESTUDIANTE" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="titulo_tba" HeaderText="TÍTULO" HeaderStyle-Width="35%" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnVer" runat="server" Text='<span class="fa fa-pencil-square-o"></span>'
                                                        CssClass="btn btn-info btn-sm" ToolTip="Ver" CommandName="Ver" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnRevision" runat="server" Text='<span class="fa fa-comment"></span>'
                                                        CssClass="btn btn-danger btn-sm" ToolTip="Revision" CommandName="Revision" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnBloquear" runat="server" Text='<span class="fa fa-lock"></span>'
                                                        CssClass="btn btn-primary btn-sm" ToolTip="Bloquear" CommandName="Bloquear" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnAutorizar" runat="server" Text='<span class="fa fa-check-circle"></span>'
                                                        CssClass="btn btn-success btn-sm" ToolTip="Autorizar" CommandName="Autorizar"
                                                        CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <HeaderStyle Font-Size="12px" Font-Bold="true" BackColor="#D9534F" ForeColor="white" />
                                        <RowStyle Font-Size="12px" />
                                        <EmptyDataTemplate>
                                            <b>No se encontraron alumnos</b>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="click" />
                            </Triggers>
                        </asp:UpdatePanel>
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
                                                        <asp:TextBox runat="server" ID="txtLinea" ReadOnly="true" CssClass="form-control"></asp:TextBox>
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
                                                            <%--<asp:Label ID="lblRubricaTexto" runat="server" CssClass="col-sm-3 col-md-3 control-label">Rúbrica</asp:Label>--%>
                                                            <div runat="server" id="DivRubrica">
                                                                <%--<div class="col-sm-2 col-md-2">
                                                                    <asp:LinkButton runat="server" ID="LinkButton1">Formato</asp:LinkButton>
                                                                </div>--%>
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
                                                            <%--<asp:Label ID="lblacta" runat="server" CssClass="col-sm-3 col-md-3 control-label">Acta</asp:Label>--%>
                                                            <%--<div runat="server" id="DivActa">
                                                                <div class="col-sm-2 col-md-2">
                                                                    <asp:LinkButton runat="server" ID="LinkButton3">Formato</asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="Label16" runat="server" CssClass="col-sm-2 col-md-2 control-label">Adjuntar Acta</asp:Label>
                                                                <div class="col-sm-5 col-md-5">
                                                                    <asp:FileUpload runat="server" ID="archivoacta" CssClass="form-control" />
                                                                    <ul>
                                                                        <li>Archivos permitidos: <span style="color: Red">.pdf</span></li>
                                                                        <li>Tamaño Máximo: <span style="color: Red">20 Mb</span></li>
                                                                    </ul>
                                                                </div>
                                                            </div>--%>
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
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnGuardar" />
                        <asp:AsyncPostBackTrigger ControlID="btnCerrar" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        </form>
    </div>
</body>
</html>
