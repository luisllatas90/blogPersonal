<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmCambiarResponsablePOA.aspx.vb"
    Inherits="indicadores_POA_FrmCambiarResponsablePOA" %>

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

    <title>Cambiar Responsable POA</title>
    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
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
            margin: 5px;
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
                        Cambiar Responsable POA
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
                    <div class="row">
                        <div class="form-group">
                            <label class="col-md-3 control-label ">
                                Ejercicio Presupuestal</label>
                            <div class="col col-md-3">
                                <asp:DropDownList runat="server" ID="ddlEjercicio" CssClass="form-control" AutoPostBack="true">
                                    <asp:ListItem Value="">--Seleccione--</asp:ListItem>
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
                                    <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-md-3 control-label ">
                                        Plan Operativo Anual (POA)</label>
                                    <div class="col col-md-6">
                                        <asp:DropDownList runat="server" ID="ddlPoa" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem Value="0">--SELECCIONE--</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">
                                        Responsable Actual</label>
                                    <label class="col-md-6 control-label" style="font-weight:bold;color:Green" id="lblResponsablePOA" runat="server">
                                    </label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-md-3 control-label ">
                                        Nuevo Responsable</label>
                                    <div class="col col-md-6">
                                        <asp:DropDownList runat="server" ID="ddlResponsable" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem Value="0">--SELECCIONE--</asp:ListItem>
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
                                    <asp:Button runat="server" ID="btnCambiar" CssClass="btn btn-primary" Text="Cambiar Responsable"
                                        OnClientClick="return confirm('¿Esta Seguro que Desea Cambiar Responsable?.')" />
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
