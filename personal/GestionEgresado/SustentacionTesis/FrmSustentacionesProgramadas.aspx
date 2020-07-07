<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmSustentacionesProgramadas.aspx.vb"
    Inherits="FrmSustentacionesProgramadas" Title="Untitled Page" %>

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
            fnLoading(false);

            $("#ddlEstado").change(function() {
                fnLoading(true);
            })

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

        function fnDescargar(url) {
            var d = new Date();
            window.open(url + "&h=" + d.getHours().toString() + d.getMinutes().toString() + d.getSeconds().toString());
        }
        function fnLoading(sw) {

            if (sw) {
                $('.piluku-preloader').removeClass('hidden');
                console.log("mostrar");
            } else {
                $('.piluku-preloader').addClass('hidden');
                console.log("ocultar");
            }

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
            vertical-align: middle;
        }
        .table tbody tr th
        {
            color: White;
            font-size: 11px;
            font-weight: bold;
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
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updLoading" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="piluku-preloader text-center">
                <div class="loader">
                    Loading...</div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvTesis" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="btnAtras" />
            <asp:AsyncPostBackTrigger ControlID="btnCerrar" />
            <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="container-fluid">
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;
                font-size: 14px;">
                <b>Sustentaciones Programadas</b>
            </div>
            <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="updLista" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div runat="server" id="Lista">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server" CssClass="col-sm-1 col-md-1 control-label">Estado</asp:Label>
                                    <div class="col-sm-4 col-md-3">
                                        <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem Value="P">PENDIENTES</asp:ListItem>
                                            <asp:ListItem Value="PS">POR SUSTENTAR</asp:ListItem>
                                            <asp:ListItem Value="SO">SUSTENTADAS OBSERVADAS</asp:ListItem>
                                            <asp:ListItem Value="SC">SUSTENTADAS CALIFICADAS</asp:ListItem>
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
                            <br />
                            <%--<asp:UpdatePanel runat="server" ID="updGeneral" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:UpdatePanel runat="server" ID="updGrilla" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>--%>
                            <div class="form-group">
                                <div runat="server" id="Div2">
                                </div>
                                <asp:HiddenField runat="server" ID="hdjur" Value="0" />
                                <asp:HiddenField runat="server" ID="hdPst" Value="0" />
                                <asp:HiddenField runat="server" ID="hdtes" Value="0" />
                                <asp:GridView runat="server" ID="gvTesis" CssClass="table table-condensed" DataKeyNames="codigo_Tes,titulo_tes,codigo_pst,codigo_jur,archivofinal,resolucion,codigo_cst,observado"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="#" HeaderStyle-Width="3%">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        <asp:BoundField DataField="titulo_tes" HeaderText="TÍTULO" HeaderStyle-Width="40%" />
                                        <asp:BoundField DataField="Responsables" HeaderText="BACHILLER(ES)" HeaderStyle-Width="25%" />
                                        <asp:BoundField DataField="promedio" HeaderText="PROMEDIO" HeaderStyle-Width="7%"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="escala" HeaderText="ESCALA" HeaderStyle-Width="10%" />
                                        <asp:TemplateField HeaderText="OPCIONES" HeaderStyle-Width="14%" ShowHeader="false">
                                            <ItemTemplate>
                                                <%--  <asp:LinkButton ID="btnDescargar" runat="server" Text='<span class="fa fa-download"></span>'
                                                    CssClass="btn btn-info btn-sm btn-radius" ToolTip="Descargar Tesis" CommandName="Descargar"
                                                    CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                </asp:LinkButton>--%>
                                                <asp:LinkButton ID="btnResolucion" runat="server" Text='<span class="fa fa-download"></span>'
                                                    CssClass="btn btn-info btn-sm btn-radius" ToolTip="Descargar Resolución" CommandName="Resolucion"
                                                    CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnCalificar" runat="server" Text='<span class="fa fa-pencil"></span>'
                                                    CssClass="btn btn-success btn-sm btn-radius" ToolTip="Calificar" CommandName="Calificar"
                                                    OnClientClick="fnLoading(true);" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnObservaciones" runat="server" Text='<span class="fa fa-comment"></span>'
                                                    CssClass="btn btn-warning btn-sm btn-radius" ToolTip="Observaciones" CommandName="Observaciones"
                                                    OnClientClick="fnLoading(true);" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                    <HeaderStyle Font-Size="11px" Font-Bold="true" BackColor="#D9534F" ForeColor="white" />
                                    <RowStyle Font-Size="12px" />
                                    <EmptyDataTemplate>
                                        <b>No se encontraron tesis</b>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
<%--                            <div class="form-group">
                                <table id="tbTest" class="table table-condensed" style="width: 100%; font-size: 12px;"
                                    cellspacing="0" border="1">
                                    <thead style="background: #E33439; color: White; font-weight: bold;">
                                        <tr>
                                            <th style="width: 3%;">
                                                #
                                            </th>
                                            <th style="width: 37%;">
                                                Título
                                            </th>
                                            <th style="width: 23%;">
                                                Bachiller(es)
                                            </th>
                                            <th style="width: 8%;">
                                                Promedio
                                            </th>
                                            <th style="width: 15%;">
                                                Escala
                                            </th>
                                            <th style="width: 12;">
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
                                                18
                                            </td>
                                            <td>
                                                Sobresaliente (18-19)
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnDescargar" runat="server" Text='<span class="fa fa-download"></span>'
                                                    CssClass="btn btn-info btn-sm btn-radius" ToolTip="Descargar Resolución">
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton8" runat="server" Text='<span class="fa fa-download"></span>'
                                                    CssClass="btn btn-danger btn-sm btn-radius" ToolTip="Descargar Acta">
                                                </asp:LinkButton>
                                     
                                                <asp:LinkButton ID="LinkButton15" runat="server" Text='<span class="fa fa-comment"></span>'
                                                    CssClass="btn btn-warning btn-sm btn-radius" ToolTip="Observaciones" OnClientClick="MostrarAsesoria();return false;">
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
                                                Pendiente
                                            </td>
                                            <td>
                                                Pendiente
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton1" runat="server" Text='<span class="fa fa-download"></span>'
                                                    CssClass="btn btn-info btn-sm btn-radius" ToolTip="Descargar Resolución">
                                                </asp:LinkButton>
                                       
                                                <asp:LinkButton ID="LinkButton12" runat="server" Text='<span class="fa fa-pencil"></span>'
                                                    CssClass="btn btn-success btn-sm btn-radius" ToolTip="Calificar" OnClientClick="MostrarCalificar();return false;">
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
                                                Pendiente
                                            </td>
                                            <td>
                                                Pendiente
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton2" runat="server" Text='<span class="fa fa-download"></span>'
                                                    CssClass="btn btn-info btn-sm btn-radius" ToolTip="Descargar Resolución">
                                                </asp:LinkButton>
                                    
                                                <asp:LinkButton ID="LinkButton3" runat="server" Text='<span class="fa fa-pencil"></span>'
                                                    CssClass="btn btn-success btn-sm btn-radius" ToolTip="Calificar" OnClientClick="MostrarCalificar();return false;">
                                                </asp:LinkButton>
                                      
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>--%>
                            <br />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvTesis" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="selectedindexchanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnAtras" />
                        <asp:AsyncPostBackTrigger ControlID="btnCerrar" />
                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="form-group" id="DivAsesorias" runat="server" visible="false">
                        <div class="panel panel-piluku">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="form-group text-center">
                                        <asp:Button runat="server" ID="btnAtras" CssClass="btn btn-sm btn-danger" Text="Atras"
                                            OnClientClick="fnLoading(true);" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label ID="Label12" runat="server" CssClass="col-sm-3 col-md-2 control-label"
                                            For="txtObservacion">Título</asp:Label>
                                        <div class="col-sm-8 col-md-8">
                                            <asp:TextBox runat="server" ID="txtTituloObservacion" CssClass="form-control" TextMode="MultiLine"
                                                Rows="5" Enabled="false" Text=""></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label ID="Label18" runat="server" CssClass="col-md-2 col-sm-3 control-label">Informe de tesis</asp:Label>
                                        <div class="col-sm-3 col-md-3">
                                            <asp:LinkButton ID="btnArchivoTesis" runat="server" Text='<span class="fa fa-download"></span> Descargar tesis'
                                                CssClass="btn btn-info btn-sm btn-radius" ToolTip="Descargar" OnClientClick="return false;">
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label ID="Label19" runat="server" CssClass="col-md-2 col-sm-3 control-label">Conformidad de Tesis</asp:Label>
                                        <div class="col-sm-3 col-md-3">
                                            <asp:LinkButton ID="btnConformidad" runat="server" Text='<span class="fa fa-check"></span> Conformidad'
                                                CssClass="btn btn-success btn-sm btn-radius" ToolTip="Conformidad" OnClientClick="return false;">
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label ID="Label16" runat="server" CssClass="col-sm-3 col-md-2 control-label"
                                            For="txtObservacion">Observaciones</asp:Label>
                                        <div class="col-sm-7 col-md-8">
                                            <asp:TextBox runat="server" ID="txtObservacion" CssClass="form-control" TextMode="MultiLine"
                                                Rows="4" MaxLength="1000"></asp:TextBox>
                                            <br>
                                            (*) Máximo 1000 Caracteres
                                        </div>
                                        <div class="col-sm-2 col-md-2">
                                            <asp:Button runat="server" ID="btnGuardarObservacion" CssClass="btn btn-sm btn-primary"
                                                OnClientClick="return confirm('¿Está seguro que desea registrar observación?')"
                                                Text="Guardar" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="panel-body timeline-block">
                                        <!--Timeline-->
                                        <div id="LineaDeTiempo" runat="server">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvTesis" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="btnAtras" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                <ContentTemplate>
                    </div>
                    <div class="form-group" id="divCalificar" runat="server" visible="false">
                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                            <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                        </button>--%>
                            <h4 class="modal-title" id="H1">
                                Calificar sustentación de Tesis
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-horizontal">
                                <div id="Alumnos" runat="server">
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label15" runat="server" CssClass="col-sm-3 col-md-3 control-label">Carrera Profesional</asp:Label>
                                    <div class="col-sm-9 col-md-9">
                                        <asp:TextBox runat="server" ID="txtCarrera" ReadOnly="true" CssClass="form-control"
                                            Text=""></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label17" runat="server" CssClass="col-sm-3 col-md-3 control-label">Título</asp:Label>
                                    <div class="col-sm-9 col-md-9">
                                        <asp:TextBox runat="server" ID="txtTitulo" ReadOnly="true" TextMode="MultiLine" Rows="3"
                                            CssClass="form-control" Text=""></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label6" CssClass="col-md-3 col-sm-3 control-label">Fecha de Informe de asesor</asp:Label>
                                    <div class="col-md-3 col-sm-3">
                                        <asp:TextBox runat="server" ID="txtfechainforme" CssClass="form-control" Enabled="false"
                                            Text=""></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblNroReso" CssClass="col-md-3 col-sm-3 control-label">N° Resolución</asp:Label>
                                    <div class="col-md-3 col-sm-3">
                                        <asp:TextBox runat="server" ID="txtNroResolucion" CssClass="form-control" Enabled="false"
                                            Text=""></asp:TextBox>
                                    </div>
                                    <asp:Label runat="server" ID="Label2" CssClass="col-md-2 col-sm-3 control-label">Fecha de Resolución</asp:Label>
                                    <div class="col-md-3 col-sm-3">
                                        <div class="input-group date" id="Div1">
                                            <asp:TextBox runat="server" ID="txtFechaResolucion" CssClass="form-control" Enabled="false"
                                                Text=""></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblcalendario" runat="server" CssClass="col-md-3 col-sm-3 control-label">Fecha y Hora de Sustentación</asp:Label>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:TextBox runat="server" ID="txtFechaSustentacion" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server" CssClass="col-md-3 col-sm-3 control-label">Tipo de Ambiente</asp:Label>
                                    <div class="col-md-3 col-sm-3">
                                        <asp:TextBox runat="server" ID="txttimpoambiente" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label32" runat="server" CssClass="col-md-3 col-sm-3 control-label">Ambiente</asp:Label>
                                    <div class="col-md-6 col-sm-6">
                                        <asp:TextBox runat="server" ID="txtAmbiente" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label14" runat="server" CssClass="col-md-3 col-sm-3 control-label">Detalle de ambiente</asp:Label>
                                    <div class="col-md-5 col-sm-5">
                                        <asp:TextBox runat="server" ID="txtDetalle" CssClass="form-control" Enabled="false"
                                            TextMode="MultiLine" Rows="4"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label11" runat="server" CssClass="col-md-3 col-sm-3 control-label">Informe de tesis</asp:Label>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:LinkButton ID="btnArchivo" runat="server" Text='<span class="fa fa-download"></span> Descargar tesis'
                                            CssClass="btn btn-info btn-sm btn-radius" ToolTip="Descargar" OnClientClick="return false;">
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label9" runat="server" CssClass="col-md-3 col-sm-3 control-label">Tipo Jurado</asp:Label>
                                    <div class="col-md-4 col-sm-4">
                                        <asp:TextBox runat="server" ID="txtTipoJurado" CssClass="form-control" Text="" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label10" runat="server" CssClass="col-md-3 col-sm-3 control-label">Jurado</asp:Label>
                                    <div class="col-md-6 col-sm-6">
                                        <asp:TextBox runat="server" ID="txtJurado" CssClass="form-control" Text="" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label13" runat="server" CssClass="col-sm-3 col-md-3 control-label">Nota</asp:Label>
                                    <div class="col-sm-2 col-md-2">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlNota">
                                            <asp:ListItem Value="">[-- Seleccione --]</asp:ListItem>
                                            <asp:ListItem Value="0">0</asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                            <asp:ListItem Value="13">13</asp:ListItem>
                                            <asp:ListItem Value="14">14</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="16">16</asp:ListItem>
                                            <asp:ListItem Value="17">17</asp:ListItem>
                                            <asp:ListItem Value="18">18</asp:ListItem>
                                            <asp:ListItem Value="19">19</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label8" runat="server" CssClass="col-sm-3 col-md-3 control-label">Observaciones</asp:Label>
                                    <div class="col-sm-9 col-md-9">
                                        <asp:CheckBox runat="server" ID="chkobservaciones" Checked="true" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server" CssClass="col-sm-3 col-md-3 control-label">Descripción de Observacion</asp:Label>
                                    <div class="col-sm-9 col-md-9">
                                        <asp:TextBox runat="server" ID="txtObservacionSustentacion" TextMode="MultiLine"
                                            Rows="3" CssClass="form-control" Text=""></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <center>
                                    <asp:Button runat="server" ID="btnGuardar" CssClass="btn btn-primary" Text="Guardar"
                                        OnClientClick="return confirm('¿Está seguro que desea guardar la calificación, luego no podra ser cambiada?');" />
                                    <asp:Button runat="server" ID="btnCerrar" CssClass="btn btn-danger" Text="Cerrar"
                                        OnClientClick="fnLoading(true);" />
                                </center>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvTesis" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="btnCerrar" />
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
