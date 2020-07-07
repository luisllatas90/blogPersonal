<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmRegistrarIncidente.aspx.vb"
    Inherits="FrmProgramarSustentacion" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Programar sustentación de Tesis</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../assets/css/style.css?x=1' />
    <%-- ======================= Fecha y Hora =============================================--%>
    <link href="../../assets/css/font-awesome-datetimepicker.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../../assets/css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <%-- ======================================================================================--%>

    <script src="../../assets/js/jquery.js" type="text/javascript"></script>

    <script src="../../assets/js/app.js" type="text/javascript"></script>

    <script type="text/javascript" src='../../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script src="../../assets/js/bootstrap.min.js" type="text/javascript"></script>

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

    <%-- ======================= Fecha y Hora =============================================--%>

    <script src="../../assets/js/moment-with-locales.js?x=1" type="text/javascript"></script>

    <script src="../../assets/js/bootstrap-datetimepicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#datetimepicker1').datetimepicker({
                locale: 'es'
            });
            $('#datetimepicker2').datetimepicker({
                locale: 'es',
                format: 'L'
            });
            $("#ddlTipoAmbiente").change(function() {
                $("#DivFisico").hide();
                $("#DivVirtual").hide();
                if ($("#ddlTipoAmbiente").val() == "F") {
                    $("#DivFisico").show();
                } else if ($("#ddlTipoAmbiente").val() == "V") {
                    $("#DivVirtual").show();
                }
            })
        })
        function MostrarModal(div) {
            $("#" + div).modal('show');
        }
        function MostrarAsesoria() {
            $("#DivAsesorias").show();
            $("#Lista").hide();
        }
        function MostrarCalificar() {
            $("#divCalificar").show();
            $("#Lista").hide();
        }
        function CerrarModal(div) {
            $("#" + div).modal('hide');
        }
        function OcultarAsesoria() {
            $("#Lista").show();
            $("#DivAsesorias").hide();
        }
        function OcultarAsesoria() {
            $("#Lista").show();
            $("#divCalificar").hide();
        }
        function MostrarRegistroIncidente() {
            $("#DivRegistroIncidente").show();
            $("#Lista").hide();
        }
        function OcultarRegistroIncidente() {
            $("#Lista").show();
            $("#DivRegistroIncidente").hide();
        }

        //        function fnMensaje(typ, msje) {
        //            var n = noty({
        //                text: msje,
        //                type: typ,
        //                timeout: 3000,
        //                modal: false,
        //                dismissQueue: true,
        //                theme: 'defaultTheme'

        //            });
        //        }
        //        function fnLoading(sw) {
        //            if (sw) {
        //                $('.piluku-preloader').removeClass('hidden');
        //            } else {
        //                $('.piluku-preloader').addClass('hidden');
        //            }
        //            //console.log(sw);
        //        }

        //        function fnDescargar(id_ar) {
        //            var d = new Date();
        //            window.open("../../Descargar.aspx?Id=" + id_ar + "&h=" + d.getHours().toString() + d.getMinutes().toString() + d.getSeconds().toString());
        //        }
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
        }
        table tbody tr th
        {
            text-align: center;
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
            z-index: 99999;
        }
    </style>
</head>
<body class="">
    <div class="container-fluid">
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;
                font-size: 14px;">
                <b>Listado de Programación Sustentación de Tesis </b>
            </div>
            <div class="panel-body">
                <div id="Lista">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="col-sm-1 col-md-1 control-label">Estado</asp:Label>
                                <div class="col-sm-3 col-md-2">
                                    <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control">
                                        <asp:ListItem Value="P">PENDIENTES</asp:ListItem>
                                        <asp:ListItem Value="OP">POR SUSTENTAR</asp:ListItem>
                                        <asp:ListItem Value="PE">SUSTENTADAS OBSERVADAS</asp:ListItem>
                                        <asp:ListItem Value="S">SUSTENTADAS</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <asp:Label ID="Label1" runat="server" CssClass="col-sm-1 col-md-1 control-label">Fecha</asp:Label>
                                <div class="col-sm-3 col-md-3">
                                    <div class="input-group date" id="datetimepicker2">
                                        <asp:TextBox runat="server" ID="txtFecha" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon"><span class="ion ion-calendar"></span></span>
                                    </div>
                                </div>
                                <%--      <div class="col-sm-1 col-md-1">
                            <asp:LinkButton ID="btnConsultar" runat="server" Text='<span class="fa fa-search"></span>'
                                CssClass="btn btn-primary" ToolTip="Buscar"></asp:LinkButton>
                        </div>--%>
                            </div>
                        </div>
                    </div>
                    <br />
                    <%--<asp:UpdatePanel runat="server" ID="updGeneral" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:UpdatePanel runat="server" ID="updGrilla" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>--%>
                    <div class="form-group">
                        <table id="tbTest" class="table table-condensed" style="width: 100%; font-size: 12px;"
                            cellspacing="0" border="1">
                            <thead style="background: #E33439; color: White; font-weight: bold;">
                                <tr>
                                    <th style="width: 5%;">
                                        #
                                    </th>
                                    <th style="width: 60%;">
                                        Título
                                    </th>
                                    <th style="width: 30%;">
                                        Bachiller(es)
                                    </th>
                                    <th style="width: 5%;">
                                        Opciones
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        1
                                    </td>
                                    <td>
                                        LA POSIBILIDAD DE ABRIR NUEVOS PROCEDIMIENTOS SANCIONADORES CONCLUIDOS POR CADUCIDAD
                                        Y LA POSIBLE LESIÓN AL PRINCIPIO DE INTERDICCIÓN DE LA ARBITRARIEDAD.
                                    </td>
                                    <td>
                                        DEL VALLE MERINO RONNY JEANPIERRE
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton3" runat="server" Text='<span class="fa fa-list"></span>'
                                            CssClass="btn btn-danger btn-sm btn-radius" ToolTip="Incidente" OnClientClick="MostrarRegistroIncidente();return false;">
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        2
                                    </td>
                                    <td>
                                        Título de prueba 2
                                    </td>
                                    <td>
                                        Guevara Cieza Fernanda
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<span class="fa fa-list"></span>'
                                            CssClass="btn btn-danger btn-sm btn-radius" ToolTip="Incidente" OnClientClick="MostrarRegistroIncidente();return false;">
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        3
                                    </td>
                                    <td>
                                        Título de prueba 3
                                    </td>
                                    <td>
                                        Castro Saavedra Carlos
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton2" runat="server" Text='<span class="fa fa-list"></span>'
                                            CssClass="btn btn-danger btn-sm btn-radius" ToolTip="Incidente" OnClientClick="MostrarRegistroIncidente();return false;">
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br />
                    <div class="form-group">
                        <div runat="server" id="lblmensaje">
                        </div>
                        <asp:GridView runat="server" ID="gvAlumnos" CssClass="table table-condensed" DataKeyNames="codigo_tba,codigo_cac,estado,codigo_tat,bloqueo,autoriza"
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
                                        <asp:LinkButton ID="btnProgramar2" runat="server" Text='<span class="fa fa-pencil-square-o"></span>'
                                            CssClass="btn btn-info btn-sm" ToolTip="Programar" CommandName="Ver" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
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
            </div>
            <%--   </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="click" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
            <div class="form-group" id="DivRegistroIncidente" style="display: none;">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                        </button>--%>
                    <h4 class="modal-title" id="H2">
                        Registrar Incidente
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <asp:Label ID="Label10" runat="server" CssClass="col-sm-3 col-md-3 control-label">Código Universitario</asp:Label>
                            <div class="col-sm-3 col-md-2">
                                <asp:HiddenField runat="server" ID="HiddenField1" Value="0" />
                                <asp:HiddenField runat="server" ID="HiddenField2" Value="0" />
                                <asp:TextBox runat="server" ID="TextBox5" ReadOnly="true" CssClass="form-control"
                                    Text="131EP42557"></asp:TextBox>
                            </div>
                            <asp:Label ID="Label11" runat="server" CssClass="col-sm-2 col-md-1 control-label">Bachiller</asp:Label>
                            <div class="col-sm-4 col-md-6">
                                <asp:TextBox runat="server" ID="TextBox6" ReadOnly="true" CssClass="form-control"
                                    Text="DEL VALLE MERINO RONNY JEANPIERRE"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label12" runat="server" CssClass="col-sm-3 col-md-3 control-label">Teléfono</asp:Label>
                            <div class="col-sm-3 col-md-2">
                                <asp:TextBox runat="server" ID="TextBox7" ReadOnly="true" CssClass="form-control"
                                    Text="999999999"></asp:TextBox>
                            </div>
                            <asp:Label ID="Label13" runat="server" CssClass="col-sm-2 col-md-1 control-label">Correo</asp:Label>
                            <div class="col-sm-4 col-md-6">
                                <asp:TextBox runat="server" ID="TextBox8" ReadOnly="true" CssClass="form-control"
                                    Text="rdelvalle@gmail.com"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label16" runat="server" CssClass="col-sm-3 col-md-3 control-label">Carrera Profesional</asp:Label>
                            <div class="col-sm-9 col-md-9">
                                <asp:TextBox runat="server" ID="TextBox9" ReadOnly="true" CssClass="form-control"
                                    Text="DERECHO"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label18" runat="server" CssClass="col-sm-3 col-md-3 control-label">Título</asp:Label>
                            <div class="col-sm-9 col-md-9">
                                <asp:TextBox runat="server" ID="TextBox10" ReadOnly="true" TextMode="MultiLine" Rows="3"
                                    Text="LA POSIBILIDAD DE ABRIR NUEVOS PROCEDIMIENTOS SANCIONADORES CONCLUIDOS POR CADUCIDAD Y LA POSIBLE LESIÓN AL PRINCIPIO DE INTERDICCIÓN DE LA ARBITRARIEDAD."
                                    CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group" id="div2">
                            <asp:Label runat="server" ID="Label2" CssClass="col-md-3 col-sm-3 control-label">Asistente faltante</asp:Label>
                            <div class="col-sm-6 col-md-6">
                                <asp:DropDownList runat="server" ID="ddlResponsable" CssClass="form-control">
                                    <asp:ListItem Value="">[ --SELECCIONE--]</asp:ListItem>
                                    <asp:ListItem Value="">Alarcon Guevara Jose Fernando - JURADO PRESIDENTE</asp:ListItem>
                                    <asp:ListItem Value="">Alayo Guevara Jose Fernando - JURADO SECRETARIO</asp:ListItem>
                                    <asp:ListItem Value="">Castillo Guevara Jose Fernando - JURADO VOCAL</asp:ListItem>
                                    <asp:ListItem Value="">DEL VALLE MERINO RONNY JEANPIERRE - EGRESADO</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label20" runat="server" CssClass="col-sm-3 col-md-3 control-label">detalle de Incidente</asp:Label>
                            <div class="col-sm-9 col-md-9">
                                <asp:TextBox runat="server" ID="TextBox11" TextMode="MultiLine" Rows="3" Text="jurado presidente no pudo asistir"
                                    CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <%-- <asp:UpdatePanel runat="server" ID="updbotones" UpdateMode="conditional">
                                                    <ContentTemplate>--%>
                            <asp:Button runat="server" ID="Button1" CssClass="btn btn-primary" Text="Guardar" />
                            <asp:Button runat="server" ID="Button2" CssClass="btn btn-danger" Text="Cerrar" OnClientClick="OcultarRegistroIncidente();return false;" />
                            <%-- <triggers>
                                                   </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="RowCommand" />
                                                    </Triggers>
                                                </asp:UpdatePanel>--%>
                        </center>
                    </div>
                    <div class="form-group">
                        <table id="Table1" class="table table-condensed" style="width: 100%; font-size: 12px;"
                            cellspacing="0" border="1">
                            <thead style="background: #E33439; color: White; font-weight: bold;">
                                <tr>
                                    <th style="width: 3%;">
                                        #
                                    </th>
                                    <th style="width: 20%;">
                                        Tipo Jurado
                                    </th>
                                    <th style="width: 40%;">
                                        Asistente faltante
                                    </th>
                                    <th style="width: 40%;">
                                        detalle
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        1
                                    </td>
                                    <td>
                                        JURADO - PRESIDENTE
                                    </td>
                                    <td>
                                        Alarcon Guevara Jose Fernando
                                    </td>
                                    <td>
                                        jurado presidente no pudo asistir
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <%--            </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnGuardar" />
                        <asp:AsyncPostBackTrigger ControlID="btnCerrar" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>--%>
            </div>
        </form>
    </div>
</body>
</html>
