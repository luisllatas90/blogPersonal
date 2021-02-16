<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmSeguimientoSustentacion.aspx.vb"
    Inherits="FrmSeguimientoSustentacion" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Seguimiento sustentación de Tesis</title>
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
            /* $("#ddlTipoAmbiente").change(function() {
            $("#DivFisico").hide();
            $("#DivVirtual").hide();
            if ($("#ddlTipoAmbiente").val() == "F") {
            $("#DivFisico").show();
            } else if ($("#ddlTipoAmbiente").val() == "V") {
            $("#DivVirtual").show();
            }
            })
            LoadingEstado();*/
            fnLoading(false);

        });
        function fnLoading(sw) {
            if (sw) {
                $('.piluku-preloader').removeClass('hidden');
            } else {
                $('.piluku-preloader').addClass('hidden');
            }
        }
        /*
        function LoadingEstado() {
        $("#ddlEstado").change(function() {
        fnLoading(true);
        });
        }


        function fnDescargar(url) {
        var d = new Date();
        window.open(url + "&h=" + d.getHours().toString() + d.getMinutes().toString() + d.getSeconds().toString());
        }

        function Calendario() {
        $('#datetimepicker2').datetimepicker({
        locale: 'es',
        format: 'L'
        });
        }


        function ValidarGuardar() {
        if ($("#ddlAsistente").val() == "") {
        fnMensaje('error', 'Seleccione asistente faltante')
        return false;
        }
        if ($("#txtdetalle").val() == "") {
        fnMensaje('error', 'Ingrese detalle de incidente')
        return false;
        }
        if (!confirm("Está seguro que desea guardar incidente?")) {
        return false
        }
        fnLoading(true);
        return true;
        }*/
        function DescargarArchivo(IdArchivo) {

            window.open("../../Descargar.aspx?Id=" + IdArchivo);


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
    <form id="form1" runat="server" enctype="multipart/form-data">
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
            <%-- <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvTesis" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="btnCerrar" />--%>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="container-fluid">
        <asp:UpdatePanel runat="server" ID="updlista" UpdateMode="Conditional">
            <ContentTemplate>
                <div runat="server" id="Lista">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;
                            font-size: 14px;">
                            <b>Consulta para sustentación de tesis</b>
                        </div>
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server" CssClass="col-sm-4 col-md-3 control-label">DNI/Cód. Universitario/Apellidos y nombres</asp:Label>
                                    <div class="col-sm-5 col-md-3">
                                        <asp:TextBox runat="server" ID="txtBusqueda" CssClass="form-control" placeholder="Ingrese DNI/Código Universitario/Apellidos y nombres"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-md-1">
                                        <asp:LinkButton runat="server" ID="btnBuscar" CssClass="btn btn-sm btn-primary btn-radius"
                                            OnClientClick="fnLoading(true);" Text="<i class='fa fa-search'></i> Buscar">
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div style="max-height: 300px; overflow: auto;">
                                <div class="form-group">
                                    <asp:GridView runat="server" ID="gvAlumnos" CssClass="table table-condensed" DataKeyNames="codigo_alu"
                                        AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="nroDocIdent_Alu" HeaderText="DNI" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIVER." HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="alumno" HeaderText="ESTUDIANTE" HeaderStyle-Width="42%" />
                                            <asp:BoundField DataField="nombre_Cpf" HeaderText="PROGRAMA DE ESTUDIOS" HeaderStyle-Width="25%" />
                                            <asp:BoundField DataField="estadoactual" HeaderText="ESTADO" HeaderStyle-Width="5%"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="VER" HeaderStyle-Width="8%" ShowHeader="false">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:LinkButton ID="btnReprogramar" runat="server" Text='<span class="fa fa-list"></span>'
                                                            CssClass="btn btn-danger btn-sm btn-radius" ToolTip="Seleccionar" CommandName="Seleccionar"
                                                            OnClientClick="fnLoading(true);" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                        </asp:LinkButton>
                                                    </center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle Font-Size="11px" Font-Bold="true" BackColor="#D9534F" ForeColor="white" />
                                        <RowStyle Font-Size="12px" />
                                        <EmptyDataTemplate>
                                            <b>No se encontraron resultados</b>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscar" />
                <asp:AsyncPostBackTrigger ControlID="btnatras" />
                <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="rowcommand" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server" ID="upDatos" UpdateMode="Conditional">
            <ContentTemplate>
                <div runat="server" id="Datos">
                    <div class="row">
                        <div class="form-group" style="text-align: center;">
                            <asp:Button runat="server" ID="btnatras" CssClass="btn btn-sm btn-danger btn-radius"
                                Text="<< Atrás" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <asp:Label ID="lbladvertencia" runat="server" CssClass="col-sm-12 col-md-12 control-label text-danger"
                                Font-Bold="true" Font-Size="16px"></asp:Label>
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;
                            font-size: 14px;">
                            <b>DATOS DE LA TESIS</b>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group">
                                    <asp:HiddenField runat="server" ID="hdtes" Value="0" />
                                    <asp:Label ID="Label1" runat="server" CssClass="col-sm-3 col-md-2 control-label">Título</asp:Label>
                                    <div class="col-sm-8 col-md-9">
                                        <asp:TextBox runat="server" ID="txttitulo" CssClass="form-control" TextMode="MultiLine"
                                            Font-Bold="true" ReadOnly="true" Rows="4"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" CssClass="col-sm-3 col-md-2 control-label">Autor(es)</asp:Label>
                                    <div class="col-sm-8 col-md-9">
                                        <asp:TextBox runat="server" ID="txtautores" CssClass="form-control" TextMode="MultiLine"
                                            Font-Bold="true" ReadOnly="true" Rows="4"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server" CssClass="col-sm-3 col-md-2 control-label">Fecha de aprobación de proyecto</asp:Label>
                                    <div class="col-sm-2 col-md-2">
                                        <asp:TextBox runat="server" ID="txtfechaproyecto" CssClass="form-control" Font-Bold="true"
                                            ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <asp:Label ID="Label4" runat="server" CssClass="col-sm-2 col-md-2 control-label">Tiempo(Dias)</asp:Label>
                                    <div class="col-sm-1 col-md-1">
                                        <asp:TextBox runat="server" ID="txttiempo" CssClass="form-control" Font-Bold="true"
                                            ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <asp:Label ID="Label5" runat="server" CssClass="col-sm-1 col-md-1 control-label">Vigente</asp:Label>
                                    <div class="col-sm-1 col-md-1">
                                        <asp:TextBox runat="server" ID="txtvigente" CssClass="form-control" Font-Bold="true"
                                            ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="form-group text-center">
                                    <h4 class="label label-primary" style="font-size: 15px;">
                                        Trámites realizados</h4>
                                </div>
                            </div>
                            <div class="row">
                                <div style="max-height: 1500px; overflow: auto;">
                                    <div class="form-group">
                                        <asp:GridView runat="server" ID="gvTramitesPrevios" CssClass="table table-condensed"
                                            DataKeyNames="codigo_alu,codigo_dta,codigo_Trl,fecha_cin" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="estudiante" HeaderText="ESTUDIANTE" HeaderStyle-Width="33%" />
                                                <asp:BoundField DataField="glosaCorrelativo_trl" HeaderText="COD. TRÁMITE" HeaderStyle-Width="15%" />
                                                <asp:BoundField DataField="descripcion_ctr" HeaderText="TRÁMITE" HeaderStyle-Width="33%" />
                                                <asp:BoundField DataField="fecha_reg" HeaderText="FECHA" HeaderStyle-Width="9%" />
                                                <asp:BoundField DataField="estado_trl" HeaderText="ESTADO" HeaderStyle-Width="5%"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField HeaderText="VER" HeaderStyle-Width="5%" ShowHeader="false">
                                                    <ItemTemplate>
                                                        <center>
                                                            <asp:LinkButton ID="btnReprogramar" runat="server" Text='<span class="fa fa-list"></span>'
                                                                CssClass="btn btn-primary btn-sm btn-radius" ToolTip="Ver detalle" CommandName="Detalle"
                                                                OnClientClick="fnLoading(true);" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                            </asp:LinkButton>
                                                        </center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Font-Size="10px" Font-Bold="true" BackColor="#337ab7" ForeColor="white"
                                                BorderColor="White" />
                                            <RowStyle Font-Size="11px" />
                                            <EmptyDataTemplate>
                                                <b>No se encontraron trámites</b>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;
                            font-size: 14px;">
                            <b>DATOS DE LA CONFORMIDAD DE TESIS</b>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label ID="Label13" runat="server" CssClass="col-sm-3 col-md-2 control-label">Asesor</asp:Label>
                                    <div class="col-sm-8 col-md-8">
                                        <asp:TextBox runat="server" ID="txtasesor" CssClass="form-control" Font-Bold="true"
                                            ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label ID="Label6" runat="server" CssClass="col-sm-3 col-md-2 control-label">Fecha de envío de tesis al asesor</asp:Label>
                                    <div class="col-sm-2 col-md-2">
                                        <asp:TextBox runat="server" ID="txtfechaenviotesis" CssClass="form-control" Font-Bold="true"
                                            ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <asp:Label ID="Label8" runat="server" CssClass="col-sm-2 col-md-1 control-label">Tiempo</asp:Label>
                                    <div class="col-sm-1 col-md-1">
                                        <asp:TextBox runat="server" ID="txttiempoasesor" CssClass="form-control" Font-Bold="true"
                                            ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label ID="Label9" runat="server" CssClass="col-sm-3 col-md-2 control-label">Observaciones</asp:Label>
                                    <div class="col-sm-1 col-md-1">
                                        <asp:TextBox runat="server" ID="txttieneobservacionasesor" CssClass="form-control"
                                            Font-Bold="true" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <asp:Label ID="Label10" runat="server" CssClass="col-sm-3 col-md-2 control-label">fecha de ultima observación</asp:Label>
                                    <div class="col-sm-2 col-md-2">
                                        <asp:TextBox runat="server" ID="txtfechaultimaobservacionasesor" CssClass="form-control"
                                            Font-Bold="true" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label ID="Label11" runat="server" CssClass="col-sm-3 col-md-2 control-label">Conformidad</asp:Label>
                                    <div class="col-sm-1 col-md-1">
                                        <asp:TextBox runat="server" ID="txttieneconformidadasesor" CssClass="form-control"
                                            Font-Bold="true" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <asp:Label ID="Label12" runat="server" CssClass="col-sm-3 col-md-2 control-label">fecha de conformidad de asesor</asp:Label>
                                    <div class="col-sm-2 col-md-2">
                                        <asp:TextBox runat="server" ID="txtfechaconformidadasesor" CssClass="form-control"
                                            Font-Bold="true" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;
                            font-size: 14px;">
                            <b>DATOS DEL TRÁMITE DE SUSTENTACIÓN</b>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div style="max-height: 1500px; overflow: auto;">
                                    <div class="form-group">
                                        <asp:GridView runat="server" ID="gvTramitesSustentacion" CssClass="table table-condensed"
                                            DataKeyNames="codigo_alu,codigo_dta" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="estudiante" HeaderText="ESTUDIANTE" HeaderStyle-Width="33%" />
                                                <asp:BoundField DataField="glosaCorrelativo_trl" HeaderText="COD. TRÁMITE" HeaderStyle-Width="15%" />
                                                <asp:BoundField DataField="descripcion_ctr" HeaderText="TRÁMITE" HeaderStyle-Width="33%" />
                                                <asp:BoundField DataField="fecha_reg" HeaderText="FECHA" HeaderStyle-Width="9%" />
                                                <asp:BoundField DataField="estado_trl" HeaderText="ESTADO" HeaderStyle-Width="5%"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField HeaderText="VER" HeaderStyle-Width="5%" ShowHeader="false">
                                                    <ItemTemplate>
                                                        <center>
                                                            <asp:LinkButton ID="btnReprogramar" runat="server" Text='<span class="fa fa-list"></span>'
                                                                CssClass="btn btn-primary btn-sm btn-radius" ToolTip="Ver detalle" CommandName="Detalle"
                                                                OnClientClick="fnLoading(true);" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                            </asp:LinkButton>
                                                        </center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Font-Size="10px" Font-Bold="true" BackColor="#337ab7" ForeColor="white"
                                                BorderColor="White" />
                                            <RowStyle Font-Size="11px" />
                                            <EmptyDataTemplate>
                                                <b>No se encontraron trámites de sustentación</b>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="updDetallesustentacion" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="form-group">
                                            <asp:Label ID="Label14" runat="server" CssClass="col-sm-2 col-md-2 control-label">Trámite</asp:Label>
                                            <div class="col-sm-2 col-md-2">
                                                <asp:TextBox runat="server" ID="txtcodigotramite" CssClass="form-control" Font-Bold="true"
                                                    ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <asp:Label ID="Label15" runat="server" CssClass="col-sm-2 col-md-1 control-label">Fecha de registro</asp:Label>
                                            <div class="col-sm-2 col-md-2">
                                                <asp:TextBox runat="server" ID="txtestadotramite" CssClass="form-control" Font-Bold="true"
                                                    ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <asp:Label ID="Label16" runat="server" CssClass="col-sm-2 col-md-2 control-label">Estado</asp:Label>
                                            <div class="col-sm-2 col-md-3">
                                                <asp:TextBox runat="server" ID="txtfechatramite" CssClass="form-control" Font-Bold="true"
                                                    ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group" id="DetalleTramitesustentacion" runat="server">
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="gvTramitesSustentacion" EventName="rowcommand" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div runat="server" id="DetalleTramitePrevio">
                    <div class="panel panel-info" id="DivDetalleTramite" runat="server">
                        <div class="panel-heading">
                            Seguimiento de Tr&aacute;mite
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div id="Div1" class="col-md-6 panel" runat="server">
                                    <div id="Div2" class="panel panel-default" runat="server">
                                        <div class="panel-heading" style="font-weight: bold">
                                            INFORMACI&Oacute;N DEL ESTUDIANTE</div>
                                        <div class="panel-body">
                                            <table style="width: 100%" class="table table-bordered bs-table">
                                                <tr>
                                                    <th style="background-color: AliceBlue; font-weight: bold; width: 30%; font-size: 10pt;
                                                        color: #000000; text-align: left;">
                                                        ID del Tr&aacute;mite
                                                    </th>
                                                    <td style="background-color: Linen; font-size: 10pt">
                                                        <asp:Label ID="lblInfEstNumero" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="background-color: AliceBlue; font-weight: bold; width: 30%; font-size: 10pt;
                                                        color: #000000; text-align: left;">
                                                        Carrera Profesional
                                                    </th>
                                                    <td style="background-color: Linen; font-size: 10pt">
                                                        <asp:Label ID="lblInfEstEscuela" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="background-color: AliceBlue; font-weight: bold; width: 30%; font-size: 10pt;
                                                        color: #000000; text-align: left;">
                                                        Apellidos y Nombres
                                                    </th>
                                                    <td style="background-color: Linen; font-size: 10pt">
                                                        <asp:Label ID="lblInfEstAlumno" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="background-color: AliceBlue; font-weight: bold; width: 30%; font-size: 10pt;
                                                        color: #000000; text-align: left;">
                                                        DNI
                                                    </th>
                                                    <td style="background-color: Linen; font-size: 10pt">
                                                        <asp:Label ID="lblInfEstDocIdent" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="background-color: AliceBlue; font-weight: bold; width: 30%; font-size: 10pt;
                                                        color: #000000; text-align: left;">
                                                        Fecha Nacimiento
                                                    </th>
                                                    <td style="background-color: Linen; font-size: 10pt">
                                                        <asp:Label ID="lblInfEstFechaNac" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="background-color: AliceBlue; font-weight: bold; width: 30%; font-size: 10pt;
                                                        color: #000000; text-align: left;">
                                                        Email
                                                    </th>
                                                    <td style="background-color: Linen; font-size: 10pt">
                                                        <asp:Label ID="lblInfEstEmail" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="background-color: AliceBlue; font-weight: bold; width: 30%; font-size: 10pt;
                                                        color: #000000; text-align: left;">
                                                        Tel&eacute;fono
                                                    </th>
                                                    <td style="background-color: Linen; font-size: 10pt">
                                                        <asp:Label ID="lblInfEstTelefono" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="background-color: AliceBlue; font-weight: bold; width: 30%; font-size: 10pt;
                                                        color: #000000; text-align: left;">
                                                        Estado Actual
                                                    </th>
                                                    <td style="background-color: Linen; font-size: 10pt">
                                                        <asp:Label ID="lblInfEstEstado" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="background-color: AliceBlue; font-weight: bold; width: 30%; font-size: 10pt;
                                                        color: #000000; text-align: left;">
                                                        Tiene Deuda
                                                    </th>
                                                    <td style="background-color: Linen; font-size: 10pt">
                                                        <asp:Label ID="lblInfEstTieneDeuda" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="background-color: AliceBlue; font-weight: bold; width: 30%; font-size: 10pt;
                                                        color: #000000; text-align: left;">
                                                        Beneficio de Beca
                                                    </th>
                                                    <td style="background-color: Linen; font-size: 10pt">
                                                        <asp:Label ID="lblInfEstBeneficio" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="background-color: AliceBlue; font-weight: bold; width: 30%; font-size: 10pt;
                                                        color: #000000; text-align: left;">
                                                        Responsable de Pago
                                                    </th>
                                                    <td style="background-color: Linen; font-size: 10pt">
                                                        <asp:Label ID="lblInfEstRespPago" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="background-color: AliceBlue; font-weight: bold; width: 30%; font-size: 10pt;
                                                        color: #000000; text-align: left;">
                                                        Direcci&oacute;n responsable de pago
                                                    </th>
                                                    <td style="background-color: Linen; font-size: 10pt">
                                                        <asp:Label ID="lblInfEstDirRespPago" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 panel" id="ifrAccion" runat="server">
                                    <div id="Div3" class="panel panel-default" runat="server">
                                        <div class="panel-heading" style="font-weight: bold">
                                            INFORMACI&Oacute;N DEL TR&Aacute;MITE</div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label class="control-label" style="margin-left: 10px;">
                                                            Tr&aacute;mite:</label>
                                                        <div class="col-md-12">
                                                            <asp:Label ID="lblTramite" runat="server" CssClass="form-control" Enabled="false"
                                                                Height="80px"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label class="control-label" style="margin-left: 10px;" for="lblTramiteDescripcion">
                                                            Descripci&oacute;n Tr&aacute;mite:</label>
                                                        <div class="col-md-12">
                                                            <asp:Label ID="lblTramiteDescripcion" runat="server" CssClass="form-control" Height="140px"
                                                                Enabled="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label class="control-label" style="margin-left: 10px;">
                                                            Fecha Tr&aacute;mite:</label>
                                                        <div class="col-md-12">
                                                            <asp:Label ID="lblFechaTramite" runat="server" CssClass="form-control" Enabled="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label class="control-label" style="margin-left: 10px;">
                                                            Fecha Pago Tr&aacute;mite:</label>
                                                        <div class="col-md-12">
                                                            <asp:Label ID="lblFechaPago" runat="server" CssClass="form-control" Enabled="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--
                                                <div class="row" id="trcicloacad" runat="server">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label class="control-label" style="margin-left: 10px;" id="lblSemestreMatriculado"
                                                                runat="server">
                                                                &Uacute;ltimo semestre matriculado:</label>
                                                            <div class="col-md-12">
                                                                <asp:DropDownList ID="ddlCiclo" runat="server" CssClass="form-control" Font-Bold="true"
                                                                    Enabled="false">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label class="control-label" style="margin-left: 10px;" id="lblInfoAdicional" runat="server">
                                                            Informaci&oacute;n adicional de la solicitud del tr&aacute;mite:</label>
                                                        <div class="col-md-12">
                                                            <div class="table-responsive">
                                                                <asp:GridView ID="gDatosAdicional" runat="server" AutoGenerateColumns="False" DataKeyNames="tabla,valorcampo"
                                                                    class="table table-bordered bs-table" Width="100%">
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="INFORMACIÓN" DataField="tabla" />
                                                                        <asp:BoundField HeaderText="DETALLE" DataField="valorcampo" />
                                                                        <asp:BoundField HeaderText="OBSERVACION ADICIONAL" DataField="observacion" />
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                                                        Font-Size="12px" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%-- <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label class="control-label" style="margin-left: 10px;" id="lblTotalSemestre" runat="server">
                                                                Total de Semestres:</label>
                                                            <div class="col-md-12">
                                                                <asp:Label ID="lblNumSemestre" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                            </div>
                                            <%--<div class="ryow">
                                                    <div class="panel" id="ifrRetCiclo" runat="server">
                                                        <div id="Div4" class="panel panel-default" runat="server">
                                                            <div class="panel-heading">
                                                                <i class="glyphicon glyphicon-calendar"></i>ULTIMA FECHA ASISTENCIA
                                                            </div>
                                                            <div class="panel-body">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="form-group">
                                                                            <label class="control-label" for="txtUltimaFechaAsistencia" style="margin-left: 15px">
                                                                                Fecha: <i class="glyphicon glyphicon-pencil"></i>
                                                                            </label>
                                                                            <div class="col-md-12">
                                                                                <asp:TextBox ID="txtUltimaFechaAsistencia" runat="server" CssClass="form-control"
                                                                                    placeholder="dd/mm/yyyy" Enabled="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <!--Lista de cursos matriculados y ultima asistencia -->
                                                                        <asp:GridView ID="gvCursosMatriculadosAsistencia" runat="server" AutoGenerateColumns="False"
                                                                            DataKeyNames="codigo_Cup" class="table table-bordered bs-table" Width="100%"
                                                                            Font-Size="X-Small">
                                                                            <Columns>
                                                                                <asp:BoundField HeaderText="Curso" DataField="nombre_Cur" />
                                                                                <asp:BoundField HeaderText="Grupo" DataField="grupoHor_Cup" />
                                                                                <asp:BoundField HeaderText="Ultima Fecha Asistencia" DataField="ultimafechaasi" />
                                                                            </Columns>
                                                                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                                                                Font-Size="12px" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="panel-body">
                                    <div class="row">
                                        <div id="Div5" class="col-md-12 panel" runat="server">
                                            <div id="Div6" class="panel panel-default" runat="server">
                                                <div class="panel-heading" style="font-weight: bold">
                                                    EVALUADORES DEL TRÁMITE</div>
                                                <div class="panel-body">
                                                    <div class="form-group">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="gvFlujo" runat="server" Width="100%" DataKeyNames="codigo_dta"
                                                                CssClass="table table-bordered bs-table datatable" AutoGenerateColumns="False"
                                                                ShowHeader="true">
                                                                <Columns>
                                                                    <asp:BoundField DataField="personal" HeaderText="EVALUADORES" />
                                                                    <asp:BoundField DataField="descripcion_Tfu" HeaderText="PERFIL DE EVALUADOR" />
                                                                    <asp:BoundField DataField="estado" HeaderText="ESTADO" />
                                                                    <asp:BoundField DataField="estadoAprobacion" HeaderText="RESULTADO" />
                                                                    <asp:BoundField DataField="fechaModestado_dft" HeaderText="FECHA DE EVALUACIÓN" />
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    No se encontraron registros!
                                                                </EmptyDataTemplate>
                                                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                                                    Font-Size="11px" />
                                                                <RowStyle Font-Size="11px" />
                                                                <EditRowStyle BackColor="#ffffcc" />
                                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
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
                        <div class="panel-footer">
                            <asp:Button ID="btnCerrar" runat="server" Text="Salir" class="btn btn-danger" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="rowcommand" />
                <asp:AsyncPostBackTrigger ControlID="gvTramitesPrevios" EventName="rowcommand" />
                <asp:AsyncPostBackTrigger ControlID="btnatras" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
