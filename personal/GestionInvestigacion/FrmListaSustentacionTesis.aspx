<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaSustentacionTesis.aspx.vb"
    Inherits="GestionInvestigacion_FrmListaSustentacionTesis" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Listado de Alumnos para Sustentación de Tesis</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <%--<meta http-equiv="Pragma" content="no-cache" />--%>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />
    <%-- ======================= Fecha y Hora =============================================--%>
    <link href="../assets/css/font-awesome-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <%-- ======================================================================================--%>

    <script src="../assets/js/jquery.js" type="text/javascript"></script>

    <%--<script src='../assets/js/bootstrap-datepicker.js' type="text/javascript"></script>-->

    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Fecha y Hora =============================================--%>

    <script src="../assets/js/moment-with-locales.js?x=1" type="text/javascript"></script>

    <script src="../assets/js/bootstrap-datetimepicker.js" type="text/javascript"></script>

    <%-- 
    fuente : https://eonasdan.github.io/bootstrap-datetimepicker/
    ======================================================================================
    --%>

    <script type="text/javascript">
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
            console.log(sw);
            if (sw) {
                $('.piluku-preloader').removeClass('hidden');
                console.log("mostrar");

            } else {
                $('.piluku-preloader').addClass('hidden');
                console.log("ocultar");
            }
            //console.log(sw);
        }


        function fnDescargar(id_ar) {
            var d = new Date();
            window.open("DescargarArchivoTesis.aspx?Id=" + id_ar + "&h=" + d.getHours().toString() + d.getMinutes().toString() + d.getSeconds().toString());
        }

        function Validar() {
            if ($("#ArchivoInforme").val() != '') {

                if ($("#ArchivoInforme")[0].files[0].size >= 20971520) {

                    fnMensaje("error", "solo se pueden adjuntar archivos de informe con tamaño maximo de 20MB");
                    return false;
                }

                archivo = $("#ArchivoInforme").val();
                //Extensiones Permitidas
                //        extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
                extensiones_permitidas = new Array(".pdf",".rar");
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
                    //            fnMensaje("error", "Solo puede Adjuntar Archivos de Informe en Formato de Word(.doc,.docx) o PDF");
                    fnMensaje("error", "Solo puede adjuntar archivos de informe en formatos de PDFy .RAR");
                    return false;
                }
            }

            if ($("#archivoResolucion").val() != '') {

                if ($("#archivoResolucion")[0].files[0].size >= 20971520) {

                    fnMensaje("error", "solo se pueden adjuntar archivos de Resolución con tamaño maximo de 20MB");
                    return false;
                }

                archivo = $("#archivoResolucion").val();
                //Extensiones Permitidas
                //        extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
                extensiones_permitidas = new Array(".pdf", ".rar");
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
                    //            fnMensaje("error", "Solo puede Adjuntar Archivos de Informe en Formato de Word(.doc,.docx) o PDF");
                    fnMensaje("error", "Solo puede adjuntar archivos de resolución en formatoS de PDF Y .RAR");
                    return false;
                }
            }

            if ($("#archivoActa").val() != '') {

                if ($("#archivoActa")[0].files[0].size >= 20971520) {

                    fnMensaje("error", "solo se pueden adjuntar archivos de acta con tamaño maximo de 20MB");
                    return false;
                }

                archivo = $("#archivoActa").val();
                //Extensiones Permitidas
                //        extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
                extensiones_permitidas = new Array(".pdf", ".rar");
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
                    //            fnMensaje("error", "Solo puede Adjuntar Archivos de Informe en Formato de Word(.doc,.docx) o PDF");
                    fnMensaje("error", "Solo puede adjuntar archivos de acta en formato de PDFY .RAR");
                    return false;
                }
            }
            
            if (confirm("¿Esta seguro que desea actualizar Tesis")) {
                return true
            } else {
                return false
            }
            return true;
        }
        function FnConfirmarEliminar() {
            if (confirm("¿Esta seguro que desea eliminar jurado?")) {
                return true
            } else {
                return false
            }
        }

        $(document).ready(function() {
            $('#datetimepicker1').datetimepicker({
                locale: 'es'
            });
            $('#datetimepicker2').datetimepicker({
                locale: 'es',
                format: 'L'
            });
            $('#datetimepicker3').datetimepicker({
                locale: 'es',
                format: 'L'
            });
        })

        function Calendario() {
            $('#datetimepicker1').datetimepicker({
                locale: 'es'
            });
            $('#datetimepicker2').datetimepicker({
                locale: 'es',
                format: 'L'
            });
            $('#datetimepicker3').datetimepicker({
                locale: 'es',
                format: 'L'
            });
        }
        function GenerarPDF(tipo) {
            var ruta = "GenerarPDF.aspx?tipo=" + tipo;
            window.open(ruta, "_blank");
        }
        function AbrirPestana(ruta) {
            window.open(ruta, "_blank");
        }
      
    </script>

    <style type="text/css">
        .content
        {
            margin-left: 0px;
        }
        .page_header
        {
            background-color: #FAFCFF;
        }
        .form-control
        {
            border-radius: 0px;
            box-shadow: none; /*border-color: #718FAB;*/
            height: 30px;
            font-weight: 300;
            color: black;
            border: 1px solid #ccc;
        }
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 5px;
            vertical-align: middle;
        }
        .table > tfoot > tr > th, .table > tbody > tr > td, .table > tfoot > tr > td
        {
            color: rgb(51, 51, 51);
            border-color: rgb(51, 51, 51);
        }
        .table > tbody > tr > th, .table > thead > tr > th, .table > thead > tr > td
        {
            color: White;
            text-align: center;
            vertical-align: middle; /*font-weight: bold;*/
            font-weight: bold;
        }
        .checkbox label
        {
            padding-left: 1px;
        }
        input[type="checkbox"] + label
        {
            color: Black;
        }
        :-ms-input-placeholder.form-control
        {
            line-height: 0px;
        }
        .form-group
        {
            margin: 3px;
        }
        #gvJurados tbody tr th, tbody tr td
        {
            padding: 5px;
        }
        #datetimepicker1 a
        {
            color: #337ab7;
            font-weight: bold;
            vertical-align: middle;
        }
        #datetimepicker2 a
        {
            color: #337ab7;
            font-weight: bold;
            vertical-align: middle;
        }
    </style>
</head>
<body>
    <div class="piluku-preloader text-center hidden">
        <!--<div class="progress">
                <div class="indeterminate"></div>
                </div>-->
        <div class="loader">
            Loading...</div>
    </div>
    <div class="wrapper">
        <div class="content">
            <form id="frmBusqueda" runat="server" enctype="multipart/form-data">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <busyboxdotnet:busybox id="BusyBox1" runat="server" showbusybox="OnLeavingPage" image="Clock"
                text="Su solicitud está siendo procesada..." title="Por favor espere" />
            <div class="page_header">
                <div class="pull-left">
                    <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Listado
                        de Alumnos para Sustentación de Tesis</span>
                </div>
            </div>
            <div class="panel panel-piluku">
                <%--<div class="panel-heading">
                    <h3 class="panel-title">
                        Listado de asesorías
                    </h3>
                </div>--%>
                <div class="panel-body">
                    <div class="row">
                        <label class="col-md-2 col-sm-2 control-label">
                            Tipo de Estudio
                        </label>
                        <div class="col-md-2 col-sm-3">
                            <asp:DropDownList runat="server" class="form-control" ID="ddlTipoEstudio" AutoPostBack="true">
                                <asp:ListItem Value="">[ -- Seleccione -- ]</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <label class="col-md-1 col-sm-1 control-label ">
                            Escuela</label>
                        <div class="col-md-6 col-sm-6">
                            <asp:UpdatePanel runat="server" ID="UpdatePanelCarrera" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList runat="server" class="form-control" ID="ddlCarrera">
                                        <asp:ListItem Value="">[ -- Seleccione -- ]</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlTipoEstudio" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-2 col-sm-2 control-label">
                            Etapa
                        </label>
                        <div class="col-md-2 col-sm-3">
                            <asp:DropDownList runat="server" class="form-control" ID="ddlEstado">
                                <asp:ListItem Value="">[ -- Seleccione -- ]</asp:ListItem>
                                <asp:ListItem Value="P">PENDIENTE</asp:ListItem>
                                <asp:ListItem Value="F">FINALIZADO</asp:ListItem>
                                <asp:ListItem Value="T">TODOS</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <label class="col-md-1 col-sm-1 control-label ">
                            Buscar</label>
                        <div class="col-md-5 col-sm-4">
                            <asp:TextBox runat="server" ID="txtBusqueda" CssClass="form-control" placeholder="DNI/Código Universitario/Apellidos y Nombres"></asp:TextBox>
                        </div>
                        <div class="col-md-1 col-sm-2">
                            <asp:Button runat="server" ID="btnConsultar" CssClass="btn btn-primary btn-radius"
                                Text="Consultar" />
                        </div>
                    </div>
                    <div class="row">
                        <asp:UpdatePanel runat="server" ID="UpdatePanelLista" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div runat="server" id="Listado">
                                    <div runat="server" id="lblmensaje">
                                    </div>
                                    <asp:GridView runat="server" ID="gvAlumnos" DataKeyNames="codigo_alu,codigo_tes,titulo_tes,informe,resolucion,acta,NroExpediente"
                                        AutoGenerateColumns="False" CssClass="table table-responsive">
                                        <Columns>
                                            <asp:TemplateField HeaderText="N°" HeaderStyle-Width="3%">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Codigo Univer." DataField="codigoUniver_Alu" HeaderStyle-Width="8%">
                                                <HeaderStyle Width="8%" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Alumno" DataField="Alumno" HeaderStyle-Width="35%">
                                                <HeaderStyle Width="35%" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Tesis" DataField="Titulo_Tes" HeaderStyle-Width="50%">
                                                <HeaderStyle Width="50%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Opciones" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-VerticalAlign="Middle">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <asp:Button runat="server" ID="btnVer" CommandName="Ver" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'
                                                            CssClass="btn btn-xs btn-orange btn-radius" Text="Ver" />
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Width="4%" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#E33439" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle"
                                            Font-Size="11px" />
                                        <RowStyle Font-Size="11px" />
                                        <EmptyDataTemplate>
                                            No se Encontraron Registros
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                                <div runat="server" id="Detalle">
                                    <div class="panel panel-piluku">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">
                                                Sustentación de Tesis</h3>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:Label ID="lbnombre" runat="server" CssClass="col-sm-2 col-md-2 control-label">Alumno</asp:Label>
                                                    <asp:Label runat="server" ID="lblAlumno" CssClass="col-sm-7 col-md-6 control-label text-primary"
                                                        Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="Label1" runat="server" CssClass="col-sm-1 col-md-2 control-label">Código Univ.</asp:Label>
                                                    <asp:Label runat="server" ID="lblCodigoUniversitario" CssClass="col-sm-2 col-md-1 control-label text-primary"
                                                        Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:HiddenField runat="server" Value="0" ID="hdCA" Visible="false" />
                                                    <asp:HiddenField runat="server" Value="0" ID="hdCodTes" Visible="false" />
                                                    <asp:HiddenField runat="server" Value="0" ID="hdExpediente" />
                                                    <asp:Label ID="lbtitulo" runat="server" CssClass="col-sm-3 col-md-2 control-label">Tesis:</asp:Label>
                                                    <div class="col-sm-9 col-md-10">
                                                        <asp:TextBox runat="server" ID="txtTitulo" CssClass="form-control" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                    <%--<asp:Label runat="server" ID="lblTesis" CssClass="col-md-10 control-label text-danger" Font-Bold="true"></asp:Label>--%>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-md-2 col-sm-3 control-label">
                                                        Informe Final(PDF,RAR):</label>
                                                    <div class="col-sm-4">
                                                        <asp:FileUpload runat="server" ID="ArchivoInforme" />
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:LinkButton runat="server" ID="lkbDescargarInforme"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row label-danger form-group">
                                                <h4>
                                                    <label class="label">
                                                        Jurado</label>
                                                </h4>
                                            </div>
                                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdJurados">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <asp:Label runat="server" ID="lblDpto" CssClass="col-sm-3 col-md-2 control-label">Departamento académico</asp:Label>
                                                        <div class="col-sm-4 col-md-5">
                                                            <asp:DropDownList runat="server" ID="ddlDepartamento" CssClass="form-control" AutoPostBack="true">
                                                                <asp:ListItem Value="">[ -- Seleccione -- ]</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <asp:Label runat="server" ID="lblDocente" CssClass="col-sm-2 col-md-1 control-label">Docente</asp:Label>
                                                        <asp:UpdatePanel runat="server" UpdateMode="conditional" ID="updddlDocente">
                                                            <ContentTemplate>
                                                                <div class="col-sm-3 col-md-4">
                                                                    <asp:DropDownList runat="server" ID="ddlDocente" CssClass="form-control">
                                                                        <asp:ListItem Value="">[ -- Seleccione -- ]</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlDepartamento" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="row">
                                                        <asp:Label runat="server" ID="lblRol" CssClass="col-sm-3 col-md-2 control-label">Rol</asp:Label>
                                                        <div class="col-sm-4 col-md-3">
                                                            <asp:DropDownList runat="server" ID="ddlTipoParticipante" CssClass="form-control">
                                                                <asp:ListItem Value="">[ -- Seleccione -- ]</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2 col-md-1">
                                                            <asp:Button runat="server" ID="btnAgregar" CssClass="btn btn-success btn-radius"
                                                                Text="Agregar" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <div class="col--md-12">
                                                                <asp:GridView ID="gvJurados" Width="100%" runat="server" ForeColor="#333333" DataKeyNames="codigo_jur,codigo_Per,codigo_Tpi"
                                                                    GridLines="None" AutoGenerateColumns="false">
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="Docente" DataField="Docente">
                                                                            <HeaderStyle Width="60%" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Rol" DataField="descripcion_tpi">
                                                                            <HeaderStyle Width="20%" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Estado" DataField="apruebadirector">
                                                                            <HeaderStyle Width="15%" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Quitar" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                                            ItemStyle-VerticalAlign="Middle">
                                                                            <ItemTemplate>
                                                                                <asp:Button runat="server" ID="btnQuitar" CommandName="Quitar" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'
                                                                                    CssClass="btn btn-xs btn-danger btn-radius" Text="x" Font-Bold="true" ToolTip="Eliminar"
                                                                                    OnClientClick="return FnConfirmarEliminar()" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="5%" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                    <EditRowStyle BackColor="#999999" />
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                    <HeaderStyle BackColor="#a2d1f7" HorizontalAlign="Center" />
                                                                    <EmptyDataTemplate>
                                                                        No cuenta con Jurados Asignados</EmptyDataTemplate>
                                                                </asp:GridView>
                                                                <asp:HiddenField runat="server" ID="hdValidaResolucion" Value="0" />
                                                                <asp:HiddenField runat="server" ID="hdValidarActa" Value="0" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="row label-danger">
                                                <h4>
                                                    <label class="label">
                                                        Fecha, Lugar, Resolución y Acta de Sustentación</label>
                                                </h4>
                                            </div>
                                            <div class="row">
                                                <asp:Label runat="server" ID="Label6" CssClass="col-md-2 col-sm-3 control-label">Fecha de Informe de asesor</asp:Label>
                                                <div class="col-md-3 col-sm-3">
                                                    <div class="input-group date" id="datetimepicker3">
                                                        <asp:TextBox runat="server" ID="txtfechainforme" CssClass="form-control"></asp:TextBox>
                                                        <span class="input-group-addon"><span class="ion ion-calendar"></span></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <asp:Label runat="server" ID="lblNroReso" CssClass="col-md-2 col-sm-3 control-label">N° Resolución</asp:Label>
                                                <div class="col-md-3 col-sm-3">
                                                    <asp:TextBox runat="server" ID="txtNroResolucion" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <asp:Label runat="server" ID="Label2" CssClass="col-md-2 col-sm-3 control-label">Fecha de Resolución</asp:Label>
                                                <div class="col-md-3 col-sm-3">
                                                    <div class="input-group date" id="datetimepicker2">
                                                        <asp:TextBox runat="server" ID="txtFechaResolucion" CssClass="form-control"></asp:TextBox>
                                                        <span class="input-group-addon"><span class="ion ion-calendar"></span></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:Label ID="Label3" runat="server" CssClass="col-md-2 col-sm-3 control-label">Resolución</asp:Label>
                                                    <div class="col-sm-3">
                                                        <asp:UpdatePanel runat="server" ID="updGenerarResolucion">
                                                            <ContentTemplate>
                                                                <asp:Button runat="server" ID="btnGenerarResolucion" CssClass="btn btn-green btn-radius"
                                                                    Text="Generar Resolución" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnGenerarResolucion" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <label class="col-md-3 col-sm-2 control-label">
                                                        Adjuntar resolución(PDF,RAR):</label>
                                                    <div class="col-sm-4">
                                                        <asp:FileUpload runat="server" ID="archivoResolucion" />
                                                        <asp:LinkButton runat="server" ID="lbkDescargarResolucion"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <asp:Label ID="lblcalendario" runat="server" CssClass="col-md-2 col-sm-3 control-label">Fecha y Hora de Sustentación</asp:Label>
                                                <div class="col-sm-3 col-md-3">
                                                    <div class="form-group">
                                                        <div class="input-group date" id="datetimepicker1">
                                                            <asp:TextBox runat="server" ID="txtFecha" CssClass="form-control"></asp:TextBox>
                                                            <span class="input-group-addon"><span class="ion ion-calendar"></span></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:Label ID="Label4" runat="server" CssClass="col-md-2 col-sm-3 control-label">Ambiente</asp:Label>
                                                <div class="col-md-3 col-sm-3">
                                                    <asp:DropDownList runat="server" ID="ddlAmbiente" CssClass="form-control">
                                                        <asp:ListItem Value="">[ -- Seleccione -- ]</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:Label runat="server" CssClass="col-md-2 col-sm-3 control-label">Acta de Aprobación:</asp:Label>
                                                    <div class="col-sm-3">
                                                        <asp:UpdatePanel runat="server" ID="updGenerarActa">
                                                            <ContentTemplate>
                                                                <asp:Button runat="server" ID="btnGenerarActa" CssClass="btn btn-orange btn-radius"
                                                                    Text="Generar Acta" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnGenerarActa" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div id="SubirActaInforme">
                                                        <label class="col-md-3 col-sm-2 control-label">
                                                            Adjuntar Acta de Aprobación(PDF):</label>
                                                        <div class="col-sm-3">
                                                            <asp:FileUpload runat="server" ID="archivoActa" />
                                                            <asp:LinkButton runat="server" ID="lkbDescargarActa"></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" id="Div2">
                                                <center>
                                                    <asp:Button runat="server" ID="btnGuardar" Text="Guardar" CssClass="btn btn-primary btn-radius"
                                                        OnClientClick="return Validar()" />
                                                    <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="btn btn-danger btn-radius" />
                                                </center>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnGuardar" />
                                <asp:AsyncPostBackTrigger ControlID="btnConsultar" />
                                <asp:AsyncPostBackTrigger ControlID="btnCancelar" />
                                <asp:AsyncPostBackTrigger ControlID="gvAlumnos" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
