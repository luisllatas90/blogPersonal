<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmConformidadAutorizacionPublicacion.aspx.vb"
    Inherits="FrmConformidadAutorizacionPublicacion" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Revisión de Tesis para sustentación </title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../assets/css/style.css?x=1' />

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

    <script type="text/javascript">

        function MostrarModal(div) {
            $("#" + div).modal('show');
        }
        function MostrarAsesoria() {
            $("#DivAsesorias").show();
            $("#Lista").hide();
        }
        function CerrarModal(div) {
            $("#" + div).modal('hide');
        }
        function OcultarAsesoria() {
            $("#Lista").show();
            $("#DivAsesorias").hide();
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
                <b>Conformidad de autorización para publicación de tesis</b>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" CssClass="col-sm-1 col-md-1 control-label">Estado</asp:Label>
                        <div class="col-sm-3 col-md-2">
                            <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control">
                                <asp:ListItem Value="P">PENDIENTES</asp:ListItem>
                                <asp:ListItem Value="C">AUTORIZADOS</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <%--      <div class="col-sm-1 col-md-1">
                            <asp:LinkButton ID="btnConsultar" runat="server" Text='<span class="fa fa-search"></span>'
                                CssClass="btn btn-primary" ToolTip="Buscar"></asp:LinkButton>
                        </div>--%>
                    </div>
                </div>
                <br />
                <%--<asp:UpdatePanel runat="server" ID="updGeneral" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:UpdatePanel runat="server" ID="updGrilla" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>--%>
                <div class="form-group" id="Lista">
                    <table id="tbTest" class="table table-condensed" style="width: 100%; font-size: 12px;"
                        cellspacing="0" border="1">
                        <thead style="background: #E33439; color: White; font-weight: bold;">
                            <tr>
                                <th style="width: 5%;">
                                    #
                                </th>
                                <th style="width: 45%;">
                                    Título
                                </th>
                                <th style="width: 40%;">
                                    Bachiller(es)
                                </th>
                                <%--<th style="width: 10%;">
                                    Tipo Acceso
                                </th>
                                <th style="width: 10%;">
                                    Restricción
                                </th>
                                <th style="width: 10%;">
                                    Declaración Jurada
                                </th>--%>
                                <th style="width: 10%;">
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
                                <%--                <td>
                                    Embargado
                                </td>
                                <td>
                                    12 Meses
                                </td>--%>
                                <td>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<span class="fa fa-eye"></span>'
                                        CssClass="btn btn-info btn-sm btn-radius" ToolTip="Conformidad" OnClientClick="MostrarAsesoria();return false;">
                                    </asp:LinkButton>
                                    <%--</td>
                                <td>--%>
                                    <asp:LinkButton ID="btnConforme" runat="server" Text='<span class="fa fa-check"></span>'
                                        CssClass="btn btn-success btn-sm btn-radius" ToolTip="Conformidad">
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
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
                                    <asp:LinkButton ID="btnVer" runat="server" Text='<span class="fa fa-pencil-square-o"></span>'
                                        CssClass="btn btn-info btn-sm" ToolTip="Ver" CommandName="Ver" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
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
                <%--   </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="click" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                <div class="form-group" id="DivAsesorias" style="display: none;">
                    <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                        <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                        </button>--%>
                        <h4 class="modal-title" id="H1">
                            Datos de Autorización de Publicación
                        </h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <span id="Span1" class="col-sm-3 col-md-3 control-label" for="txtAutores">Bachiler(es)</span>
                                <div class="col-sm-9 col-md-9">
                                    <textarea name="txtAutores" rows="2" cols="20" readonly="readonly" id="txtAutores"
                                        class="form-control">- DEL VALLE MERINO RONNY JEANPIERRE</textarea>
                                </div>
                            </div>
                            <div class="form-group">
                                <span id="Span2" class="col-sm-3 col-md-3 control-label" for="txtFacultad">Facultad</span>
                                <div class="col-sm-3 col-md-3">
                                    <input name="txtFacultad" type="text" value="DERECHO" readonly="readonly" id="txtFacultad"
                                        class="form-control">
                                </div>
                                <span id="Span4" class="col-sm-2 col-md-2 control-label" for="txtCarrera">Carrera Profesional</span>
                                <div class="col-sm-4 col-md-4">
                                    <input name="txtCarrera" type="text" value="DERECHO" readonly="readonly" id="txtCarrera"
                                        class="form-control">
                                </div>
                            </div>
                            <div class="form-group">
                                <span id="Span5" class="col-sm-3 col-md-3 control-label" for="txtLinea">Linea de Investigación
                                    USAT:</span>
                                <div class="col-sm-9 col-md-9">
                                    <input name="txtLinea" type="text" readonly="readonly" id="txtLinea" class="form-control">
                                </div>
                            </div>
                            <div class="form-group">
                                <span id="Span6" class="col-sm-3 col-md-3 control-label" for="txtArea">Área OCDE</span>
                                <div class="col-sm-3 col-md-3">
                                    <input name="txtArea" type="text" readonly="readonly" id="txtArea" class="form-control">
                                </div>
                                <span id="Span7" class="col-sm-2 col-md-2 control-label" for="txtSubArea">Sub Área OCDE</span>
                                <div class="col-sm-4 col-md-4">
                                    <input name="txtSubArea" type="text" readonly="readonly" id="txtSubArea" class="form-control">
                                </div>
                            </div>
                            <div class="form-group">
                                <span id="Span8" class="col-sm-3 col-md-3 control-label" for="txtDisciplina">Disciplina
                                    OCDE:</span>
                                <div class="col-sm-9 col-md-9">
                                    <input name="txtDisciplina" type="text" readonly="readonly" id="txtDisciplina" class="form-control">
                                </div>
                            </div>
                            <div class="form-group">
                                <span id="Label17" class="col-sm-3 col-md-3 control-label">Título</span>
                                <div class="col-sm-9 col-md-9">
                                    <textarea name="txtTitulo" rows="3" cols="20" readonly="readonly" id="txtTitulo"
                                        class="form-control">LA POSIBILIDAD DE ABRIR NUEVOS PROCEDIMIENTOS SANCIONADORES CONCLUIDOS POR CADUCIDAD Y LA POSIBLE LESIÓN AL PRINCIPIO DE INTERDICCIÓN DE LA ARBITRARIEDAD.</textarea>
                                </div>
                            </div>
                            <div class="form-group">
                                <span id="Label6" class="col-md-3 col-sm-3 control-label">Fecha de conformidad de asesor</span>
                                <div class="col-md-3 col-sm-3">
                                    <input name="txtfechainforme" type="text" value="04/06/2020" id="txtfechainforme"
                                        disabled="disabled" class="form-control">
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="col-md-3 col-sm-3 control-label">Informe de tesis</asp:Label>
                                <div class="col-sm-6 col-md-6">
                                    <div class="input-group date" id="Div2">
                                        <asp:Button runat="server" ID="Button1" CssClass="btn btn-sm btn-info btn-radius"
                                            Text="Descargar informe final" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" id="divJurados">
                                <span id="Label5" class="col-md-3 col-sm-3 control-label">Jurados</span>
                                <div class="col-md-9 col-sm-9">
                                    <table id="Table1" class="table table-condensed" style="width: 100%; font-size: 12px;"
                                        cellspacing="0" border="1">
                                        <thead style="background: #E33439; color: White; font-weight: bold;">
                                            <tr>
                                                <th style="width: 5%;">
                                                    #
                                                </th>
                                                <th style="width: 30%;">
                                                    Tipo Jurado
                                                </th>
                                                <th style="width: 65%;">
                                                    Jurado
                                                </th>
                             
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    1
                                                </td>
                                                <td>
                                                    PRESIDENTE
                                                </td>
                                                <td>
                                                    Alarcon Guevara Jose Fernando
                                                </td>
                                              
                                            </tr>
                                            <tr>
                                                <td>
                                                    2
                                                </td>
                                                <td>
                                                    SECRETARIO
                                                </td>
                                                <td>
                                                    Alayo Guevara Jose Fernando
                                                </td>
                                              
                                            </tr>
                                            <tr>
                                                <td>
                                                    3
                                                </td>
                                                <td>
                                                    VOCAL
                                                </td>
                                                <td>
                                                    Castillo Guevara Jose Fernando
                                                </td>
                                                
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="form-group">
                                <span id="lblcalendario" class="col-md-3 col-sm-3 control-label">Fecha y Hora de Sustentación</span>
                                <div class="col-sm-3 col-md-3">
                                    <div class="input-group date" id="datetimepicker1">
                                        <input name="txtFecha" type="text" id="txtFecha" class="form-control">
                                        <span class="input-group-addon"><span class="ion ion-calendar"></span></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="col-sm-3 col-md-3 control-label">Tipo de acceso</asp:Label>
                                <div class="col-sm-3 col-md-2">
                                    <asp:HiddenField runat="server" ID="hdcac" Value="0" />
                                    <asp:HiddenField runat="server" ID="hdcod" Value="0" />
                                    <asp:TextBox runat="server" ID="txtcodigouniver" ReadOnly="true" CssClass="form-control"
                                        Text="Embargado"></asp:TextBox>
                                </div>
                                <asp:Label ID="Label14" runat="server" CssClass="col-sm-2 col-md-1 control-label">Periodo</asp:Label>
                                <div class="col-sm-4 col-md-4">
                                    <asp:TextBox runat="server" ID="txtestudiante" ReadOnly="true" CssClass="form-control"
                                        Text="12 meses"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="col-md-3 col-sm-3 control-label">Declaración Jurada</asp:Label>
                                <div class="col-sm-6 col-md-6">
                                    <div class="input-group date" id="Div1">
                                        <asp:Button runat="server" ID="btnDescargar" CssClass="btn btn-sm btn-info btn-radius"
                                            Text="Descargar declaración jurada" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <%-- <asp:UpdatePanel runat="server" ID="updbotones" UpdateMode="conditional">
                                                    <ContentTemplate>--%>
                            <asp:Button runat="server" ID="btnCerrar" CssClass="btn btn-danger" Text="Cerrar"
                                OnClientClick="OcultarAsesoria();return false;" />
                            <%-- <triggers>
                                                   </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="RowCommand" />
                                                    </Triggers>
                                                </asp:UpdatePanel>--%>
                        </center>
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
