<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaJurado.aspx.vb"
    Inherits="GestionInvestigacion_FrmListaJurado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listado de Tesis donde Participa como Jurado</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />

    <script src="../assets/js/jquery.js" type="text/javascript"></script>

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        function fnDownload(id_ar) {
            window.open("DescargarArchivoTesis.aspx?Id=" + id_ar);
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
            box-shadow: none;
            border-color: #718FAB;
            height: 28px;
            font-weight: 300;
            color: black;
        }
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 4px;
            vertical-align: middle;
        }
        .table > tfoot > tr > th, .table > tbody > tr > td, .table > tfoot > tr > td
        {
            color: Black;
            border-color: Black;
        }
        .table > tbody > tr > th, .table > thead > tr > th, .table > thead > tr > td
        {
            color: White;
            text-align: center;
            vertical-align: middle; /*font-weight: bold;*/
        }
        .checkbox label
        {
            padding-left: 1px;
        }
        input[type="checkbox"] + label
        {
            color: Black;
        }
        #gvDetalleObservaciones > tbody > tr > td
        {
            padding: 1px;
        }
        .modal-open .modal
        {
            overflow-x: hidden;
            overflow-y: auto;
        }
        .modal
        {
            overflow: auto !important;
        }
    </style>
</head>
<body>
    <div class="wrapper">
        <div class="content">
            <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="page_header">
                <div class="pull-left">
                    <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Listado
                        de Tesis como Jurado</span>
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
                        <label class="col-md-1 control-label">
                            Semestre
                        </label>
                        <div class="col-md-2">
                            <asp:DropDownList runat="server" class="form-control" ID="ddlSemestre" AutoPostBack="true">
                                <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <label class="col-md-1 control-label">
                            Etapa
                        </label>
                        <div class="col-md-2">
                            <asp:DropDownList runat="server" class="form-control" ID="ddlEtapa" AutoPostBack="true">
                                <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                <asp:ListItem Value="I">INFORME</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <label class="col-md-1 control-label ">
                            Docente</label>
                        <div class="col-md-5">
                            <asp:UpdatePanel runat="server" ID="UpdatePanelAsesor">
                                <ContentTemplate>
                                    <asp:DropDownList runat="server" class="form-control" ID="ddlDocente" AutoPostBack="true">
                                        <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlSemestre" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlEtapa" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <div class="row">
                                <div runat="server" id="lblmensaje">
                                </div>
                                <asp:GridView runat="server" ID="gvTesisJurado" DataKeyNames="codigo_tes,codigo_cac,codigo_jur,codigo_eti,archivo,linkinforme"
                                    AutoGenerateColumns="False" CssClass="table table-responsive">
                                    <Columns>
                                        <asp:TemplateField HeaderText="N°" HeaderStyle-Width="3%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="ALUMNO" DataField="alumno" HeaderStyle-Width="20%"></asp:BoundField>
                                        <asp:BoundField HeaderText="TESIS" DataField="titulo_tes" HeaderStyle-Width="40%">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="ETAPA" DataField="nombre_Eti" HeaderStyle-Width="10%">
                                        </asp:BoundField>
                                        <%--<asp:BoundField HeaderText="ASESOR" DataField="asesor" HeaderStyle-Width="15%"></asp:BoundField>--%>
                                        <asp:BoundField HeaderText="TIPO JURADO" DataField="descripcion_Tpi" HeaderStyle-Width="11%">
                                        </asp:BoundField>
                                        <%--<asp:TemplateField HeaderText="ARCHIVO" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <a onclick="fnDownload('<%# Eval("archivo") %>')">Descargar</a>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="8%" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>--%>
                                        <asp:BoundField HeaderText="ARCHIVO" HeaderStyle-Width="7%" />
                                        <asp:TemplateField HeaderText="OBSERVACIONES" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <asp:Button runat="server" ID="Guardar" CommandName="Observar" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'
                                                        CssClass="btn btn-sm btn-info" Text="Registrar" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="9%" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="ACTA DE SUSTENTACIÓN" HeaderStyle-Width="7%" />
                                        <asp:BoundField HeaderText="LINK INFORME" HeaderStyle-Width="7%" />
                                    </Columns>
                                    <HeaderStyle BackColor="#E33439" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle"
                                        Font-Size="11px" />
                                    <RowStyle Font-Size="11px" />
                                    <EmptyDataTemplate>
                                        No se Encontraron Registros
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                            <div class="row">
                                <div class="modal fade" id="mdRegistro" runat="server" role="dialog" aria-labelledby="myModalLabel"
                                    style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                    <div runat="server" id="BloqueoModal" class='modal-backdrop fade in' style='height: 100%;'>
                                    </div>
                                    <div class="modal-dialog modal-lg" id="modalReg">
                                        <div class="modal-content">
                                            <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                                <asp:Button runat="server" ID="btnCerrarModal" CssClass="close" Text="x" />
                                                <h5 class="modal-title" id="myModalLabel3">
                                                    Registro de Observaciones</h5>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="Label2" runat="server" CssClass="col-md-2 control-label">Tipo</asp:Label>
                                                        <div class="col-md-4">
                                                            <asp:HiddenField runat="server" ID="hdcodObs" Value="0" Visible="false" />
                                                            <asp:HiddenField runat="server" ID="hdcodtes" Value="0" Visible="false" />
                                                            <asp:HiddenField runat="server" ID="hdcodjur" Value="0" Visible="false" />
                                                            <asp:HiddenField runat="server" ID="hdcodcac" Value="0" Visible="false" />
                                                            <asp:DropDownList runat="server" ID="ddlTIpo" CssClass="form-control">
                                                                <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                                                <asp:ListItem Value="1">OBSERVACIÓN DE FONDO</asp:ListItem>
                                                                <asp:ListItem Value="2">OBSERVACIÓN DE FORMA</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="Label1" runat="server" CssClass="col-md-2 control-label">Observación</asp:Label>
                                                        <div class="col-md-10">
                                                            <asp:TextBox CssClass="form-control" ID="txtObservacion" runat="server" TextMode="MultiLine" MaxLength="500"
                                                                Rows="3"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div runat="server" id="lblRegistro">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <center>
                                                        <asp:Button runat="server" ID="btnAgregar" Text="Agregar" CssClass="btn btn-primary" />
                                                        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="btn btn-danger" />
                                                    </center>
                                                </div>
                                                <div class="row">
                                                    <asp:GridView runat="server" ID="gvDetalleObservaciones" DataKeyNames="codigo_dot"
                                                        Font-Size="9" AllowPaging="true" PageSize="5" AutoGenerateColumns="False" CssClass="table table-sm table-responsive">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="FECHA" DataField="fecha_reg" HeaderStyle-Width="10%">
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="TIPO" DataField="tipo_dot" HeaderStyle-Width="15%"></asp:BoundField>
                                                            <asp:BoundField HeaderText="OBSERVACIÓN" DataField="descripcion_dot" HeaderStyle-Width="65%">
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="OPCIONES" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                                ItemStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:Button runat="server" ID="Eliminar" CommandName="Eliminar" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'
                                                                        CssClass="btn btn-xs btn-danger" Text="Eliminar" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="9%" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#E33439" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle"
                                                            Font-Size="11px" />
                                                        <RowStyle Font-Size="11px" />
                                                        <EmptyDataTemplate>
                                                            No se encontraron observaciones registradas
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                                <div class="row" style="display: none;">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="Label3" runat="server" CssClass="col-sm-4 col-md-2 control-label">Nota</asp:Label>
                                                        <div class="col-sm-2  col-md-2">
                                                            <asp:TextBox CssClass="form-control" ID="txtNota" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 col-md-2">
                                                            <asp:Button runat="server" ID="btnGuardarNota" Text="Guardar" CssClass="btn btn-sm btn-success" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--<div class="modal-footer">
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlDocente" EventName="SelectedIndexChanged" />
                            <%--           <asp:AsyncPostBackTrigger ControlID="cboAnio" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="btnGuardar" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
