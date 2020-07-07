<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListadoConsultas.aspx.vb"
    Inherits="ListadoConsultas" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Registro de Grados y titulos acádemicos del Personal USAT</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel='stylesheet' href='../assets/css/bootstrap.min.css?x=1' />
    <%-- ======================= Fecha y Hora =============================================--%>
    <link href="../assets/css/font-awesome-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <link href="../assets/font-awesome-4.7.0/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <%-- ======================= Lista desplegable con busqueda =============================================--%>
    <link href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src='../assets/js/jquery.js'></script>

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <%-- ======================= Fecha y Hora =============================================--%>

    <script src="../assets/js/moment-with-locales.js?x=1" type="text/javascript"></script>

    <script src="../assets/js/bootstrap-datetimepicker.js" type="text/javascript"></script>

    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Lista desplegable con busqueda =============================================--%>

    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            Calendario();
        })
        function MostrarModal(div) {
            $("#" + div).modal('show');
        }
        function Calendario() {
            $('#datetimepicker1').datetimepicker({
                locale: 'es',
                format: 'L'

            });
        }

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
        function fnDescargar(id_ar) {
            var d = new Date();
            window.open("../DescargarArchivo.aspx?Id=" + id_ar + "&idt=28&h=" + d.getHours().toString() + d.getMinutes().toString() + d.getSeconds().toString());
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
    </style>
</head>
<body class="">
    <div class="container-fluid">
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>Consultas de estudiantes Modalidad Virtual </b>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label ID="lblCarrera" runat="server" CssClass="col-sm-2 col-md-2 control-label"
                            for="ddlCarrera">Tipo de estudio</asp:Label>
                        <div class="col-sm-3 col-md-2">
                            <asp:DropDownList runat="server" ID="ddlTipoEstudio" CssClass="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <asp:Label ID="Label5" runat="server" CssClass="col-sm-2 col-md-2 control-label"
                            for="ddlCarrera">Carrera Profesional</asp:Label>
                        <asp:UpdatePanel runat="server" ID="updCarrera" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="col-sm-5 col-md-5">
                                    <asp:DropDownList runat="server" ID="ddlCarrera" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlTipoEstudio" EventName="selectedindexchanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" CssClass="col-sm-2 col-md-2 control-label">Estado</asp:Label>
                        <div class="col-sm-3 col-md-2">
                            <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control" AutoPostBack="true">
                                <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <asp:Label ID="Label6" runat="server" CssClass="col-sm-2 col-md-2 control-label"
                            for="ddlCarrera">Ingrese Texto</asp:Label>
                        <div class="col-sm-4 col-md-5">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" ID="txtBusqueda" CssClass="form-control" MaxLength="100"
                                        placeholder="ID Consulta/Código Univer./Apellidos y nombres"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlTipoEstudio" EventName="selectedindexchanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlCarrera" EventName="selectedindexchanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="selectedindexchanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <asp:LinkButton ID="btnConsultar" runat="server" Text='<span class="fa fa-search"></span>'
                                CssClass="btn btn-primary" ToolTip="Buscar"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12 col-md-12  col-sm-offset-3 col-md-offset-4">
                            <div style="border: solid 1pt; width: 10px; height: 12px; float: left; background-color: Red;
                                vertical-align: middle; display: inline">
                            </div>
                            <div style="float: left">
                                &nbsp;<asp:Label ID="Label7" runat="server"> Pendiente</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                            </div>
                            <div id="DivColorDerivar" runat="server">
                                <div style="border: solid 1pt; width: 10px; height: 12px; float: left; background-color: Yellow;
                                    vertical-align: middle">
                                </div>
                                <div style="float: left">
                                    &nbsp;
                                    <asp:Label ID="Label8" runat="server"> Derivado</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                </div>
                            </div>
                            <div style="border: solid 1pt; width: 10px; height: 12px; float: left; background-color: Green;
                                vertical-align: middle">
                            </div>
                            <div style="float: left">
                                &nbsp;
                                <asp:Label ID="Label9" runat="server"> Finalizado</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <asp:UpdatePanel runat="server" ID="updListaIncidencias" UpdateMode="Conditional"
                    ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="form-group">
                            <asp:UpdatePanel runat="server" ID="updGrilla" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <td style="text-align: right; vertical-align: middle">
                                        <asp:GridView runat="server" ID="gvIncidencias" CssClass="table table-condensed"
                                            DataKeyNames="codigo_inc,codigo_alu,estado_inc,codigo_tfu,codigo_test" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="glosacorrelativo_inc" HeaderText="CÓDIGO" HeaderStyle-Width="10%" />
                                                <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIVER" HeaderStyle-Width="7%" />
                                                <asp:BoundField DataField="alumno" HeaderText="ESTUDIANTE" HeaderStyle-Width="35%" />
                                                <asp:BoundField DataField="fecha" HeaderText="FECHA Y HORA" HeaderStyle-Width="11%" />
                                                <asp:BoundField DataField="asunto_inc" HeaderText="ASUNTO" HeaderStyle-Width="25%" />
                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnVer" runat="server" Text='<span class="fa fa-pencil-square-o"></span>'
                                                            CssClass="btn btn-info btn-sm" ToolTip="Ver" CommandName="Ver" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Font-Size="12px" Font-Bold="true" BackColor="#D9534F" ForeColor="white" />
                                            <RowStyle Font-Size="12px" />
                                            <EmptyDataTemplate>
                                                <b>No se encontraron Incidencias</b>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnRespuesta" EventName="click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnGuardarDerivar" EventName="click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="row">
                            <div class="modal fade" id="mdEditar" role="dialog" aria-labelledby="myModalLabel"
                                style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog modal-lg" id="Div2">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                            </button>
                                            <h4 class="modal-title" id="H1">
                                                Consulta Alumno
                                            </h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <asp:HiddenField runat="server" ID="hdi" Value="0" />
                                                    <%--<asp:HiddenField runat="server" ID="hdfa" Value="0" />
                                                <asp:HiddenField runat="server" ID="hdc" Value="0" />--%>
                                                    <asp:Label ID="Label12" runat="server" CssClass="col-sm-3 col-md-2 control-label">Código</asp:Label>
                                                    <div class="col-sm-3 col-md-2">
                                                        <asp:TextBox runat="server" ID="txtCodigo" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 col-md-2">
                                                    </div>
                                                    <asp:Label ID="Label13" runat="server" ReadOnly="true" CssClass="col-sm-2 col-md-2 control-label">Fecha</asp:Label>
                                                    <div class="col-sm-3 col-md-3">
                                                        <asp:TextBox runat="server" ID="txtFechaRegistro" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label4" runat="server" CssClass="col-sm-3 col-md-2 control-label">Código Universitario</asp:Label>
                                                    <div class="col-sm-3 col-md-2">
                                                        <asp:TextBox runat="server" ID="txtcodigouniver" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 col-md-2">
                                                    </div>
                                                     <asp:Label ID="Label19" runat="server" ReadOnly="true" CssClass="col-sm-2 col-md-2 control-label">Doc Ident</asp:Label>
                                                    <div class="col-sm-3 col-md-3">
                                                        <asp:TextBox runat="server" ID="txtDocIdent" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label14" runat="server" CssClass="col-sm-3 col-md-2 control-label">Estudiante</asp:Label>
                                                    <div class="col-sm-9 col-md-9">
                                                        <asp:TextBox runat="server" ID="txtestudiante" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label15" runat="server" CssClass="col-sm-3 col-md-2 control-label">Carrera Profesional</asp:Label>
                                                    <div class="col-sm-9 col-md-9">
                                                        <asp:TextBox runat="server" ID="txtCarrera" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label10" runat="server" CssClass="col-sm-3 col-md-2 control-label">Teléfono</asp:Label>
                                                    <div class="col-sm-3 col-md-2">
                                                        <asp:TextBox runat="server" ID="txttelefono" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="Label11" runat="server" CssClass="col-sm-1 col-md-2 control-label">Correo</asp:Label>
                                                    <div class="col-sm-5 col-md-5">
                                                        <asp:TextBox runat="server" ID="txtemail" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label16" runat="server" CssClass="col-sm-3 col-md-2 control-label">Asunto</asp:Label>
                                                    <div class="col-sm-9 col-md-9">
                                                        <asp:TextBox runat="server" ID="txtAsunto" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label17" runat="server" CssClass="col-sm-3 col-md-2 control-label">Descripción</asp:Label>
                                                    <div class="col-sm-9 col-md-9">
                                                        <asp:TextBox runat="server" ID="txtDescripcion" ReadOnly="true" TextMode="MultiLine"
                                                            Rows="5" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label18" runat="server" CssClass="col-sm-3 col-md-2 control-label">Adjunto</asp:Label>
                                                    <div class="col-sm-5 col-md-5">
                                                        <asp:LinkButton runat="server" ID="lblArchivo"></asp:LinkButton>
                                                    </div>
                                                </div>
                                                <asp:UpdatePanel runat="server" ID="updResponder" UpdateMode="conditional">
                                                    <ContentTemplate>
                                                        <div class="form-group" id="DivResponder" visible="false" runat="server">
                                                            <asp:Label ID="Label2" runat="server" CssClass="col-sm-3 col-md-2 control-label">Respuesta</asp:Label>
                                                            <div class="col-sm-9 col-md-9">
                                                                <asp:TextBox runat="server" ID="txtRespuesta" TextMode="MultiLine" Rows="5" CssClass="form-control" MaxLength="5000"
                                                                    placeholder="Ingrese detalle"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnRespuesta" EventName="click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="conditional">
                                                <ContentTemplate>
                                                    <center>
                                                        <asp:Button runat="server" ID="btnRespuesta" CssClass="btn btn-primary" Text="Responder" />
                                                        <asp:Button runat="server" ID="btnDerivar" CssClass="btn btn-warning" Text="Derivar"
                                                            Visible="false" />
                                                        <asp:Button runat="server" ID="btnCerrar" CssClass="btn btn-danger" Text="Cerrar" />
                                                    </center>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnRespuesta" EventName="click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnGuardarDerivar" EventName="click" />
                                                    <asp:AsyncPostBackTrigger ControlID="gvIncidencias" EventName="RowCommand" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <asp:GridView runat="server" ID="gvDetalle" CssClass="table table-condensed" DataKeyNames=""
                                                            AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="respuesta_din" HeaderText="RESPUESTA" HeaderStyle-Width="40%" />
                                                                <asp:BoundField DataField="usuario" HeaderText="USUARIO" HeaderStyle-Width="24%" />
                                                                <asp:BoundField DataField="descripcion_tfu" HeaderText="INSTANCIA" HeaderStyle-Width="15%" />
                                                                <asp:BoundField DataField="fecha" HeaderText="FECHA" HeaderStyle-Width="13%" />
                                                                <asp:BoundField DataField="estado" HeaderText="ESTADO" HeaderStyle-Width="8%" />
                                                            </Columns>
                                                            <HeaderStyle Font-Size="10px" Font-Bold="true" BackColor="#D9534F" ForeColor="white"
                                                                HorizontalAlign="Center" />
                                                            <RowStyle Font-Size="10px" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="conditional">
                            <ContentTemplate>
                                <div class="modal fade" id="mdDerivar" role="dialog" aria-labelledby="myModalLabel"
                                    style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                    <div class="modal-dialog modal-lg" id="Div3">
                                        <div class="modal-content">
                                            <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                    <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                                </button>
                                                <h4 class="modal-title" id="H2">
                                                    Seleccione Área a Derivar
                                                </h4>
                                            </div>
                                            <div class="modal-body">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label3" runat="server" CssClass="col-sm-3 col-md-2 control-label">Área</asp:Label>
                                                        <div class="col-sm-9 col-md-8">
                                                            <asp:DropDownList runat="server" ID="ddlInstancia" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <center>
                                                    <asp:Button runat="server" ID="btnGuardarDerivar" CssClass="btn btn-success" Text="Derivar"
                                                        OnClientClick="return confirm('¿Está seguro que desea derivar la incidencia/consulta?');" />
                                                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn btn-danger" Text="Cerrar"
                                                        data-dismiss="modal" />
                                                </center>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnGuardarDerivar" EventName="click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoEstudio" EventName="selectedindexchanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlCarrera" EventName="selectedindexchanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="selectedindexchanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCerrar" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="gvIncidencias" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        </form>
    </div>
</body>
</html>
