﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmAutorizaTramiteTitulo.aspx.vb"
    Inherits="GradosYTitulos_FrmAutorizaTramiteTitulo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Autorizar Trámite de Titulo a Estudiante</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />

    <script src="../assets/js/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        /*
        function Checkall(Checkbox) {
        var gvAlumnos = document.getElementById("<%= gvAlumnos.ClientID %>");
        for (i = 1; i < gvAlumnos.rows.length; i++) {
        gvAlumnos.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
        }
        */
    </script>

    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 4px;
        }
        .content .main-content
        {
            padding-right: 15px;
        }
        .content
        {
            margin-left: 0px;
        }
        .form-control
        {
            border-radius: 0px;
            box-shadow: none;
            border-color: #d9d9d9;
            height: 28px;
            font-weight: 300; /* line-height: 40px; */
            color: black;
        }
        .form-group
        {
            margin: 6px;
        }
        .form-horizontal .control-label
        {
            padding-top: 5px;
        }
        /*
        .i-am-new
        {
            z-index: 100;
        }*/.page_header
        {
            background-color: #FAFCFF;
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
            vertical-align: middle;
            font-weight: bold;
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
                    <i class="icon ti-check page_header_icon"></i><span class="main-text">Autorizar Trámite
                        de Título</span>
                    <%--<p class="text">
                        Autorizar Trámite de Título
                    </p>--%>
                </div>
                <%--<div class="right pull-right">
                    <ul class="right_bar">
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Headings</li>
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Inline
                            Text Elements</li>
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;alignment
                            Classes</li>
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;List
                            Types &amp; Groups</li>
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;and more...</li>
                    </ul>
                </div>--%>
            </div>
            <div class="panel-piluku">
                <div class="row">
                    <label class="col-md-2 control-label ">
                        Sesion de Consejo</label>
                    <div class="col-md-3">
                        <asp:DropDownList runat="server" class="form-control" ID="cboSesion" AutoPostBack="true">
                            <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <label class="col-md-2 control-label ">
                        Carrera Profesional</label>
                    <div class="col-md-5">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <asp:DropDownList runat="server" class="form-control" ID="cboCarrera">
                                    <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cboSesion" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row">
                    <label class="col-md-2 control-label ">
                        DNI / Apellidos y Nombres</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtBusqueda" class="form-control" placeholder="DNI / Apellidos y Nombres"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                    </div>
                    <div class="col-md-1">
                        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" CssClass="btn btn-primary"
                            OnClick="btnBuscar_Click" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="panel panel-piluku" id="PanelLista">
                    <%-- <div class="panel-heading">
                        <h3 class="panel-title">
                            Estudiantes
                                           <span class="panel-options"><a class="panel-refresh"
                    href="#"> <i class="icon ti-reload" onclick="">
                    </i></a><a class="panel-minimize" href="#"><i class="icon ti-angle-up"></i></a>
                </span>
                        </h3>
                    </div>--%>
                    <div class="panel-body">
                        <div class="row">
                            <asp:UpdatePanel ID="Upd2" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12" id="divMensaje" runat="server">
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAutorizar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                    <ContentTemplate>
                                        <div style="text-align: right; color: Black;">
                                            <b>
                                                <asp:Label runat="server" ID="lblContadorSeleccionado" Font-Bold="true" ForeColor="blue"></asp:Label>
                                                <asp:Label runat="server" ID="lblcontador" Font-Bold="true"></asp:Label>
                                            </b>
                                        </div>
                                        <asp:GridView runat="server" ID="gvAlumnos" DataKeyNames="codigo_egr" AutoGenerateColumns="False"
                                            CssClass=" table table-responsive">
                                            <Columns>
                                                <asp:TemplateField HeaderText="N°">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="DENOMINACIÓN" DataField="descripcion_dgt"></asp:BoundField>
                                                <asp:BoundField HeaderText="EGRESADO" DataField="egresado"></asp:BoundField>
                                                <asp:BoundField HeaderText="N° RESOLUCIÓN" DataField="NroResolucion_egr"></asp:BoundField>
                                                <%--<asp:BoundField HeaderText="AUTORIZAR" DataField="autoriza"></asp:BoundField>--%>
                                                <asp:TemplateField>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkall" AutoPostBack="true" OnCheckedChanged="chckchanged" runat="server" /></HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkAutorizar" runat="server" EnableViewState="true" Checked='<%#Convert.ToBoolean(Eval("autoriza")) %>'
                                                            AutoPostBack="true" OnCheckedChanged="lnkContar_Click"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#E33439" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <EmptyDataTemplate>
                                                No se Encontraron Registros
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:PostBackTrigger ControlID="btnBuscar" />--%>
                                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row">
                            <asp:Button runat="server" ID="btnAutorizar" Text="Actualizar" OnClick="btnAutorizar_Click"
                                CssClass="btn btn-success" />
                        </div>
                    </div>
                </div>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
