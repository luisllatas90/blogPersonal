<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmAgregarCecosAntiguosEnPOA.aspx.vb"
    Inherits="indicadores_POA_FrmAgregarCecosAntiguosEnPOA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel='stylesheet' href='../../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../../assets/css/material.css' />
    <link rel='stylesheet' href='../../assets/css/style.css?x=1' />

    <script type="text/javascript" src='../../assets/js/jquery.js'></script>

    <title>Activación de Centros de Costos</title>
    <style type="text/css">
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
            padding: 5px;
            vertical-align: middle;
        }
        .form-group
        {
            margin: 3px;
        }
        /*
        .form-horizontal .control-label
        {
            padding-top: 5px;
        }
        .i-am-new
        {
            z-index: 100;
        }*/.page_header
        {
            background-color: #FAFCFF;
        }
        .titulo_form
        {
            font-size: 14px;
            font-weight: bold;
            font-family: "Helvetica Neue" ,Helvetica,Arial,sans-serif;
            color: #337ab7;
        }
    </style>
</head>
<body>
    <div class="piluku-preloader text-center hidden">
        <div class="loader">
            Loading...</div>
    </div>
    <div class="wrapper">
        <div class="content">
            <div class="panel" id="PanelLista">
                <div class="panel-heading">
                    <h3 class="titulo_form">
                        Activación de Centros de Costo Antiguos en POA
                        <%--                <span class="panel-options"><a class="panel-refresh"
                    href="#"> <i class="icon ti-reload" onclick="">
                    </i></a><a class="panel-minimize" href="#"><i class="icon ti-angle-up"></i></a>
                </span>--%>
                    </h3>
                </div>
                <div class="panel-body">
                    <form id="form1" runat="server">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel runat="server" ID="upCecos" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 col-md-3 control-label ">
                                        Buscar Centro de costo</label>
                                    <div class="col col-sm-6 col-md-6">
                                        <asp:TextBox runat="server" ID="txtCeco" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Button runat="server" ID="btnBuscar" CssClass="btn btn-primary" Text="Buscar" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <div class="col col-sm-12 col-md-12">
                                        <asp:HiddenField runat="server" ID="hdCeco" Value="0" />
                                        <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Vertical" Width="100%">
                                            <asp:GridView ID="gvCeco" runat="server" AutoGenerateColumns="False" BorderColor="#628BD7"
                                                BorderStyle="Solid" BorderWidth="1px" CellPadding="4" DataKeyNames="codigo_cco"
                                                ForeColor="#333333" ShowHeader="False" Width="98%">
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:BoundField DataField="Nombre" HeaderText="Centro de Costo" />
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Button runat="server" ID="btnSeleccionar" CommandName="Seleccionar" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'
                                                                CssClass="btn btn-xs btn-orange btn-radius" Font-Size="10px" Font-Bold="true"
                                                                Text="Seleccionar" ToolTip="seleccionar"" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="5%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <b>No se encontraron Centros de Costo con el término de búsqueda</b>
                                                </EmptyDataTemplate>
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-12 col-md-12 label-primary" style="color: White; font-weight: 400;
                                        height: 30px; vertical-align: middle;">
                                        Activar Centro de costos antiguos en POA</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-md-3 control-label ">
                                        Centro de Costos</label>
                                    <div class="col-sm-9 col-md-9">
                                        <asp:Label runat="server" ID="lblcentrocostos" CssClass="control-label text-primary"
                                            Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="form-group">
                            <label class="col-md-3 control-label ">
                                Ejercicio Presupuestal</label>
                            <div class="col col-md-3">
                                <asp:DropDownList runat="server" ID="ddlEjercicio" CssClass="form-control" AutoPostBack="true">
                                    <asp:ListItem Value="">SELECCIONE AÑO</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <label class="col-md-3 control-label ">
                                Plan estrátegico (PEI/PEF)</label>
                            <div class="col col-md-6">
                                <asp:DropDownList runat="server" ID="ddlPei" CssClass="form-control" AutoPostBack="true">
                                    <asp:ListItem Value="">SELECCIONE PLAN</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-md-3 control-label ">
                                        Plan Operativo Anual (POA)</label>
                                    <div class="col col-md-6">
                                        <asp:DropDownList runat="server" ID="ddlPoa" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem Value="0">SELECCIONE POA</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-md-3 control-label ">
                                        Personal de Apoyo</label>
                                    <div class="col col-md-6">
                                        <asp:DropDownList runat="server" ID="ddlResponsable" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem Value="0">SELECCIONE RESPONSABLE</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div id="Mensaje" runat="server">
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlEjercicio" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlPei" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlPoa" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="form-group">
                            <div class="col col-md-9">
                                <center>
                                    <asp:Button runat="server" ID="btnCambiar" CssClass="btn btn-success" Text="Crear Centro Costo en POA"
                                        OnClientClick="return confirm('¿Esta Seguro que desea crear centro de costo en POA?.')" />
                                </center>
                            </div>
                        </div>
                    </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
