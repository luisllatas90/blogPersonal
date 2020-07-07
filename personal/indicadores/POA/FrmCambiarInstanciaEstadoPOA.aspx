<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmCambiarInstanciaEstadoPOA.aspx.vb"
    Inherits="indicadores_POA_FrmCambiarInstanciaEstadoPOA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cambiar Instancia Estado POA</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../assets/css/style.css?x=1' />

    <script src="../../assets/js/jquery.js" type="text/javascript"></script>

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
                    <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Cambiar
                        Instancia Estado POA</span>
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
                        <label class="col-md-3 control-label ">
                            Ejercicio Presupuestal</label>
                        <div class="col-md-2">
                            <asp:DropDownList runat="server" class="form-control" ID="ddlEjercicio" AutoPostBack="true">
                                <asp:ListItem Value="">--SELECCIONE--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-3 control-label ">
                            Plan Estrátegico (PEI/PEF)</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="ddlPlan" CssClass="form-control" AutoPostBack="true">
                                <asp:ListItem Value="">-- SELECCIONE --</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-3 control-label ">
                            Plan Operativo Anual(POA)
                        </label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="ddlPoa" CssClass="form-control" AutoPostBack="true">
                                <asp:ListItem Value="">-- SELECCIONE --</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-3 control-label ">
                            Programa/Proyecto
                        </label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="ddlActividadPoa" CssClass="form-control" AutoPostBack="true">
                                <asp:ListItem Value="">-- SELECCIONE --</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-3 control-label ">
                            Instancia - Estado Actual
                        </label>
                        <label class="col-md-6 control-label green">
                        </label>
                    </div>
                    <div class="row">
                        <label class="col-md-3 control-label ">
                            Nueva Instancia - Estado
                        </label>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlInstanciaEstado" CssClass="form-control"
                                AutoPostBack="true">
                                <asp:ListItem Value="">-- SELECCIONE --</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button runat="server" ID="btnGuardar" CssClass="btn btn-success" Text="Guardar" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </form>
    </div>
    </div>
</body>
</html>
