﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmEvaluarAsignacionJuradoSustentacion.aspx.vb"
    Inherits="GestionInvestigacion_FrmEvaluarAsignacionJuradoSustentacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Evaluación de Jurados asignados en Tesis</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />

    <script src="../assets/js/jquery.js" type="text/javascript"></script>

    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <script type="text/javascript">
        function SelectAllCheckboxes(chk) {
            $('#<%=gvJurados.ClientID %>').find("input:checkbox").each(function() {
                if (this != chk) {
                    this.checked = chk.checked;
                }
            });
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
            padding: 4px;
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
                        de Jurados asignados a Tesis</span>
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
                        <label class="col-md-2 col-sm-2 control-label ">
                            Tipo de Estudio
                        </label>
                        <div class="col-md-3 col-sm-4">
                            <asp:DropDownList runat="server" class="form-control" ID="ddlTipoEstudio" AutoPostBack="true">
                                <asp:ListItem Value="">[ -- Seleccione -- ]</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <label class="control-label col-md-2 col-sm-1">
                            Carrera Profesional</label>
                        <div class="col-md-5 col-sm-5">
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
                        <label class="col-md-2 col-sm-2 control-label ">
                            Búsqueda</label>
                        <div class="col-md-6 col-sm-4">
                            <asp:TextBox runat="server" ID="txtAlumno" CssClass="form-control" placeholder="DNI/Código universitario/Apellidos y Nombres"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-2">
                            <asp:Button runat="server" ID="btnConsultar" CssClass="btn btn-primary" Text="Consultar" />
                        </div>
                        <div class="col-md-1 col-sm-2">
                        </div>
                        <div class="col-md-1 col-sm-1">
                            <asp:Button runat="server" ID="btnAprobar" CssClass="btn btn-success" Text="Aprobar"
                                OnClientClick="return confirm('¿Desea actualizar el Jurado?.')" />
                        </div>
                    </div>
                    <div class="row">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <div runat="server" id="lblmensaje">
                                </div>
                                <asp:GridView runat="server" ID="gvJurados" DataKeyNames="codigo_Tes,codigo_jur,codigo_per,codigo_tpi"
                                    AutoGenerateColumns="False" CssClass="table table-responsive">
                                    <Columns>
                                        <asp:BoundField HeaderText="TESIS" DataField="titulo_tes" HeaderStyle-Width="34%">
                                            <HeaderStyle Width="34%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Responsables" DataField="Responsables" HeaderStyle-Width="18%">
                                            <HeaderStyle Width="18%" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" onclick="SelectAllCheckboxes(this)" /></HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAutorizar" runat="server" EnableViewState="true" Checked='<%#Convert.ToBoolean(Eval("apruebadirector_jur")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="JURADO" HeaderStyle-Width="23%" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlDocente" CssClass="form-control input-sm">
                                                    <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderStyle Width="23%" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ROL" HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlRol" CssClass="form-control input-sm">
                                                    <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderStyle Width="16%" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OPCIONES" HeaderStyle-Width="9%" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <asp:Button runat="server" ID="btnActualizar" CommandName="Actualizar" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'
                                                        CssClass="btn btn-xs btn-orange" Text="Actualizar" OnClientClick="return confirm('¿Desea actualizar el Jurado?.')" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="9%" />
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
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnConsultar" />
                                <asp:AsyncPostBackTrigger ControlID="btnAprobar" />
                                <%--           <asp:AsyncPostBackTrigger ControlID="cboAnio" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="btnGuardar" />--%>
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
