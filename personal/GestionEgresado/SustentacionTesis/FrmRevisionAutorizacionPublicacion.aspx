<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmRevisionAutorizacionPublicacion.aspx.vb"
    Inherits="FrmRevisionAutorizacionPublicacion" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Revisión autorización publicación</title>
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

    <script type="text/javascript">
        $(document).ready(function() {

            fnLoading(false);
            /*$("#btnGuardar").click(function() {
            ValidarProgramar();
            })
            LoadingEstado();
            Calendario();*/

        });
        /* function Calendario() {
        $('#datetimepicker1').datetimepicker({
        locale: 'es'
        });
        }*/

        function LoadingEstado() {
            $("#ddlEstado").change(function() {
                fnLoading(true);
            });
            $("#btnRechazar").click(function() {
                $("#mdRechazar").modal("show");
                $("#txtMotivoRechazo").val("");
                $("#lblcontador").text("0");
            })
            $("#txtMotivoRechazo").keyup(function() {
                var texto = $("#txtMotivoRechazo").val()
                if (texto.length > 300) {
                    texto = texto.substr(0, 300)
                    $("#txtMotivoRechazo").val(texto);
                    fnMensaje('error', 'Puede ingresar como máximo 300 caracteres')
                }
                $("#lblcontador").text(texto.length);

            })
        }
        function fnLoading(sw) {
            if (sw) {
                $('.piluku-preloader').removeClass('hidden');
            } else {
                $('.piluku-preloader').addClass('hidden');
            }
        }
        function fnDescargar(url) {
            var d = new Date();
            window.open(url + "&h=" + d.getHours().toString() + d.getMinutes().toString() + d.getSeconds().toString());
        }
        function ValidarConformidad() {
            if (!confirm('¿Está seguro que desea dar conformidad a autorización?')) {
                return false;
            }
            fnLoading(true)
            return true;
        }
        function ValidarRechazar() {
            if ($("#txtMotivoRechazo").val() == "") {
                fnMensaje('error', 'Ingrese motivo de rechazo')
                return false;
            }
            if (!confirm('¿Está seguro que desea rechazar autorizaciones registradas?')) {
                return false;
            }
            fnLoading(true)
            return true;
        }

        /*
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
        if (!confirm("Está seguro que desea programar sustentación de tesis?")) {
        return false
        }
        fnLoading(true);
        return true;
        }
        */
        function fnConfirmarAnulacion() {
            if (confirm("¿Está seguro que desea eliminar la autorización de publicación?")) {
                fnLoading("true");
            } else {
                return false;
            }
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
                <asp:AsyncPostBackTrigger ControlID="btnConformidad" />
                <asp:AsyncPostBackTrigger ControlID="btnEnviarRechazar" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;
                font-size: 14px;">
                <b>Revisión de autorización publicación </b>
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
                                            <asp:ListItem Value="A">AUTORIZADOS</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="form-group">
                                <div runat="server" id="lblmensaje">
                                </div>
                                <asp:GridView runat="server" ID="gvTesis" CssClass="table table-condensed" DataKeyNames="codigo_apu,codigo_Tes,codigo_dta,codigo_pst,documento,informe,codigo_test"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="#" HeaderStyle-Width="3%">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        <asp:BoundField DataField="titulo" HeaderText="TÍTULO" HeaderStyle-Width="54%" />
                                        <asp:BoundField DataField="autores" HeaderText="AUTOR(ES)" HeaderStyle-Width="40%" />
                                        <%--<asp:BoundField DataField="cantidadintegrantes_apu" HeaderText="N° AUTORES" HeaderStyle-Width="10%" />--%>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDescargar" runat="server" Text='<span class="fa fa-download"></span>'
                                                    CssClass="btn btn-primary btn-sm btn-radius" ToolTip="Descargar informe de tesis">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnVer" runat="server" Text='<span class="fa fa-eye"></span>'
                                                    CssClass="btn btn-info btn-sm btn-radius" ToolTip="Ver Datos de autorización"
                                                    CommandName="VerDatos" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'
                                                    OnClientClick="fnLoading(true);">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnAnular" runat="server" Text='<span class="fa fa-close"></span>'
                                                    CssClass="btn btn-danger btn-sm btn-radius" ToolTip="Anular autorización" CommandName="Anular"
                                                    CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>' OnClientClick="return fnConfirmarAnulacion();">
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
                        <asp:AsyncPostBackTrigger ControlID="btnConformidad" />
                        <asp:AsyncPostBackTrigger ControlID="btnEnviarRechazar" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="form-group" runat="server" id="DivAutorizacion" visible="false">
                            <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                        </button>--%>
                                <h4 class="modal-title h5" id="H1">
                                    Revisión de autorización
                                </h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <asp:HiddenField runat="server" ID="hdapu" Visible="false" Value="0" />
                                    <asp:HiddenField runat="server" ID="hdpst" Visible="false" Value="0" />
                                    <asp:HiddenField runat="server" ID="hdtes" Visible="false" Value="0" />
                                    <asp:HiddenField runat="server" ID="hdtest" Visible="false" Value="0" />
                                    <asp:HiddenField runat="server" ID="hddta" Visible="false" Value="0" />
                                    <div class="form-group">
                                        <asp:Label ID="Label17" runat="server" CssClass="col-sm-3 col-md-3 control-label">Título</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:TextBox runat="server" ID="txtTitulo" ReadOnly="true" TextMode="MultiLine" Rows="3"
                                                Text="" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label15" runat="server" CssClass="col-sm-3 col-md-3 control-label">Carrera Profesional</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:TextBox runat="server" ID="txtCarrera" ReadOnly="true" CssClass="form-control"
                                                Text=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label6" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                            For="txtLinea">Linea de Investigación USAT:</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:TextBox runat="server" ID="txtlinea" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%--<div class="form-group">
                                        <asp:Label ID="Label11" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                            For="txtArea">Área OCDE</asp:Label>
                                        <div class="col-sm-3 col-md-3">
                                            <asp:TextBox runat="server" ID="txtarea" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="Label12" runat="server" CssClass="col-sm-2 col-md-2 control-label"
                                            For="txtSubArea">Sub Área OCDE</asp:Label>
                                        <div class="col-sm-4 col-md-4">
                                            <asp:TextBox runat="server" ID="txtsubarea" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                    <div class="form-group">
                                        <asp:Label ID="Label19" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                            For="txtDisciplina">Línea OCDE(Disciplina):</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:TextBox runat="server" ID="txtdisciplina" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" CssClass="col-sm-3 col-md-3 control-label">Asesor</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:TextBox runat="server" ID="txtasesor" ReadOnly="true" CssClass="form-control"
                                                Text=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="col-sm-3 col-md-3 control-label">ORCID Asesor</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:TextBox runat="server" ID="txtorcid" ReadOnly="true" CssClass="form-control"
                                                Text=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <div runat="server" id="alumnos">
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <center>
                                    <%-- <asp:UpdatePanel runat="server" ID="updbotones" UpdateMode="conditional">
                                                    <ContentTemplate>--%>
                                    <asp:Button runat="server" ID="btnConformidad" CssClass="btn btn-sm btn-primary btn-radius"
                                        Text="Dar Conformidad" OnClientClick="return ValidarConformidad();" />
                                    <asp:Button runat="server" ID="btnRechazar" CssClass="btn btn-sm btn-warning btn-radius"
                                        Text="Rechazar Autorizacion(es)" OnClientClick="return false;" />
                                    <asp:Button runat="server" ID="btnCerrar" CssClass="btn btn-sm btn-danger btn-radius"
                                        Text="Cerrar" OnClientClick="fnLoading(true);" />
                                    <%-- <triggers>
                                                   </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="RowCommand" />
                                                    </Triggers>
                                                </asp:UpdatePanel>--%>
                                </center>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="modal fade" id="mdRechazar" role="dialog" aria-labelledby="myModalLabel"
                                style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                            </button>
                                            <h4 class="modal-title" id="H3">
                                                <label id="Label13">
                                                    Rechazar Autorización(es) de Publicación</label>
                                            </h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:Label ID="Label4" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                        For="txtLinea">Motivo de Rechazo:</asp:Label>
                                                    <div class="col-sm-9 col-md-9">
                                                        <asp:TextBox runat="server" ID="txtMotivoRechazo" CssClass="form-control" TextMode="MultiLine"
                                                            MaxLength="300" Rows="6" placeholder="Ingrese el motivo de rechazo, este motivo será enviado por correo al egresado"></asp:TextBox>
                                                        <asp:Label runat="server" ID="lblcontador" CssClass="control-label" Font-Bold="true">0</asp:Label>
                                                        <asp:Label runat="server" ID="label5" CssClass="control-label" Font-Bold="true" > de 300 caracteres permitidos</asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <asp:Button runat="server" ID="btnEnviarRechazar" class="btn btn-sm btn-warning btn-radius"
                                                Text="Rechazar" OnClientClick="return ValidarRechazar();" />
                                            <button type="button" id="Button2" class="btn btn-sm btn-danger btn-radius" style="display: inline-block"
                                                data-dismiss="modal">
                                                Cerrar</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvTesis" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnConformidad" />
                        <asp:AsyncPostBackTrigger ControlID="btnEnviarRechazar" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </form>
    </div>
</body>
</html>
