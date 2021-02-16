<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmProgramarSustentacion.aspx.vb"
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
    <%--<link href="../../assets/css/font-awesome-datetimepicker.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../../assets/css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />--%>
     <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css" />
    <%-- ======================================================================================--%>

    <script src="../../assets/js/jquery.js" type="text/javascript"></script>

    <script src="../../assets/js/app.js" type="text/javascript"></script>

    <script type="text/javascript" src='../../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script src="../../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <link href="../../assets/font-awesome-4.7.0/css/font-awesome.css" rel="stylesheet"
        type="text/css" />
    <%-- ======================= Lista desplegable con busqueda =============================================--%>
  <%--  <link href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src='../../assets/js/jquery.js'></script>

    <script src="../../assets/js/bootstrap.min.js" type="text/javascript"></script>--%>
    <script src="../../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>

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
            $("#btnGuardar").click(function() {
                ValidarProgramar();
            })
            LoadingEstado();
            Calendario();
        });
        function Calendario() {
            $('#datetimepicker1').datetimepicker({
                locale: 'es'
            });
        }
        function LoadingEstado() {
            $("#ddlEstado").change(function() {
                fnLoading(true);
            });
        }
        function fnLoading(sw) {
            if (sw) {
                $('.piluku-preloader').removeClass('hidden');
            } else {
                $('.piluku-preloader').addClass('hidden');
            }
        }

        function ValidarProgramar() {
            if ($("#txtFecha").val() == "") {
                fnMensaje('error', 'Seleccione una fecha')
                return false;
            }
            if ($("#ddlTipoAmbiente").val() == "") {
                fnMensaje('error', 'Seleccione el tipo de ambiente')
                return false;
            }
            if ($("#ddlTipoAmbiente").val() == "FÍSICO" && $("#ddlAmbiente").val() == "0") {
                fnMensaje('error', 'Seleccione el ambiente')
                return false;
            }
            if ($("#ddlTipoAmbiente").val() == "VIRTUAL" && $("#txtAmbienteVirtual").val() == "") {
                fnMensaje('error', 'Ingrese link de ambiente virtual')
                return false;
            }
            if (!confirm("¿Está seguro que desea programar sustentación de tesis?")) {
                return false
            }
            fnLoading(true);
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
    </style>
</head>
<body class="">
    <div class="container-fluid">
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
                <asp:AsyncPostBackTrigger ControlID="gvTesis" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="selectedindexchanged" />
                <asp:AsyncPostBackTrigger ControlID="btnCerrar" />
                <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;
                font-size: 14px;">
                <b>Programar Sustentación de Tesis </b>
            </div>
            <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="updLista" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div runat="server" id="Lista">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" CssClass="col-sm-1 col-md-1 control-label">Estado</asp:Label>
                                    <div class="col-sm-5 col-md-5">
                                        <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem Value="P">PENDIENTES</asp:ListItem>
                                            <asp:ListItem Value="NP">NO PROGRAMADOS</asp:ListItem>
                                            <asp:ListItem Value="RE">POR REPROGRAMAR(JUSTIFICACIÓN ESTUDIANTE)</asp:ListItem>
                                            <asp:ListItem Value="RD">POR REPROGRAMAR(INCIDENTE DOCENTE)</asp:ListItem>
                                            <asp:ListItem Value="PR">PROGRAMADOS</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="form-group">
                                <div runat="server" id="lblmensaje">
                                </div>
                                <asp:GridView runat="server" ID="gvTesis" CssClass="table table-condensed" DataKeyNames="codigo_dta,codigo_Tes,codigo_pst,fecha,tipo,tipoambiente,codigo_amb,link,detalle,archivoresolucion,codigo_fac,tipoprogramacion"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="#" HeaderStyle-Width="3%">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        <asp:BoundField DataField="titulo_tes" HeaderText="TÍTULO" HeaderStyle-Width="50%" />
                                        <asp:BoundField DataField="alumno" HeaderText="BACHILLER(ES)" HeaderStyle-Width="45%" />
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnProgramar" runat="server" Text='<span class="fa fa-pencil-square-o"></span>'
                                                    CssClass="btn btn-info btn-sm btn-radius" ToolTip="Programar Sustentación" CommandName="Programar"
                                                    CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>' OnClientClick="fnLoading(true);">
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
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvTesis" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="selectedindexchanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnCerrar" />
                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="form-group" runat="server" id="DivProgramacion" visible="false">
                            <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                        </button>--%>
                                <h4 class="modal-title h5" id="H1">
                                    Programar sustentación
                                </h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <asp:HiddenField runat="server" ID="hdpst" Visible="false" Value="0" />
                                    <asp:HiddenField runat="server" ID="hdtes" Visible="false" Value="0" />
                                    <asp:HiddenField runat="server" ID="hdfac" Visible="false" Value="0" />
                                    <asp:HiddenField runat="server" ID="hdTipoProg" Visible="false" Value="NORMAL" />
                                    <asp:HiddenField runat="server" ID="hddta" Visible="false" Value="0" />
                                    <div runat="server" id="alumnos">
                                    </div>
                                    <br />
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
                                                Text="" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="Label6" CssClass="col-md-3 col-sm-3 control-label">Fecha de conformidad de asesor</asp:Label>
                                        <div class="col-md-3 col-sm-3">
                                            <asp:TextBox runat="server" ID="txtfechainforme" CssClass="form-control" Enabled="false"
                                                Text=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group" id="divJurados">
                                        <asp:Label runat="server" ID="Label5" CssClass="col-md-3 col-sm-3 control-label">Jurados</asp:Label>
                                        <div class="col-md-9 col-sm-9">
                                            <asp:GridView runat="server" ID="gvJurado" CssClass="table table-condensed" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField DataField="descripcion_tpi" HeaderText="Tipo" HeaderStyle-Width="20%" />
                                                    <asp:BoundField DataField="jurado" HeaderText="Jurado" HeaderStyle-Width="50%" />
                                                    <asp:BoundField DataField="telefono" HeaderText="Telefóno" HeaderStyle-Width="15%" />
                                                    <asp:BoundField DataField="correo" HeaderText="Correo" HeaderStyle-Width="15%" />
                                                </Columns>
                                                <HeaderStyle Font-Size="11px" Font-Bold="true" BackColor="#D9534F" ForeColor="white" />
                                                <RowStyle Font-Size="12px" />
                                                <EmptyDataTemplate>
                                                    <b>No se encontraron tesis</b>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblcalendario" runat="server" CssClass="col-md-3 col-sm-3 control-label">Fecha y Hora de Sustentación</asp:Label>
                                        <div class="col-sm-3 col-md-3" runat="server" id="Divfecha">
                                            <div class="input-group date" id="datetimepicker1">
                                                <asp:TextBox runat="server" ID="txtFecha" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                <span class="input-group-addon"><span class="ion ion-calendar"></span></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="col-md-3 col-sm-3 control-label">Tipo de Ambiente</asp:Label>
                                        <div class="col-md-3 col-sm-3">
                                            <asp:DropDownList runat="server" ID="ddlTipoAmbiente" CssClass="form-control" AutoPostBack="true">
                                                <asp:ListItem Value="">[ -- Seleccione -- ]</asp:ListItem>
                                                <asp:ListItem Value="FÍSICO">FÍSICO</asp:ListItem>
                                                <asp:ListItem Value="VIRTUAL">VIRTUAL</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel runat="server" ID="updDatosAmbiente" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <div id="DivFisico" runat="server" visible="false">
                                                    <asp:Label ID="Label32" runat="server" CssClass="col-md-3 col-sm-3 control-label">Ambiente</asp:Label>
                                                    <div class="col-md-6 col-sm-6">
                                                        <asp:DropDownList runat="server" ID="ddlAmbiente" CssClass="form-control">
                                                            <asp:ListItem Value="0">[ -- Seleccione -- ]</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div id="DivVirtual" runat="server" visible="false">
                                                    <asp:Label ID="Label7" runat="server" CssClass="col-md-3 col-sm-3 control-label">Ambiente</asp:Label>
                                                    <div class="col-md-6 col-sm-6">
                                                        <asp:TextBox runat="server" ID="txtAmbienteVirtual" CssClass="form-control" placeholder="Ingrese link de ambiente virtual"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div id="divDatosAmbiente" runat="server" visible="false">
                                                    <asp:Label ID="Label2" runat="server" CssClass="col-md-3 col-sm-3 control-label">Datos de ambiente</asp:Label>
                                                    <div class="col-md-6 col-sm-6">
                                                        <asp:TextBox runat="server" ID="txtDetalle" CssClass="form-control" placeholder="ID REUNION: 999 999 999 &#10; clave: asd412sd2"
                                                            TextMode="MultiLine" Rows="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlTipoAmbiente" EventName="Selectedindexchanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtFecha" EventName="TextChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <center>
                                    <%-- <asp:UpdatePanel runat="server" ID="updbotones" UpdateMode="conditional">
                                                    <ContentTemplate>--%>
                                    <asp:Button runat="server" ID="btnGuardar" CssClass="btn btn-primary" Text="Guardar"
                                        OnClientClick="return ValidarProgramar();" />
                                    <asp:Button runat="server" ID="btnCerrar" CssClass="btn btn-danger" Text="Cerrar"
                                        OnClientClick="fnLoading(true);" />
                                    <%-- <triggers>
                                                   </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="RowCommand" />
                                                    </Triggers>
                                                </asp:UpdatePanel>--%>
                                </center>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvTesis" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </form>
    </div>
</body>
</html>
