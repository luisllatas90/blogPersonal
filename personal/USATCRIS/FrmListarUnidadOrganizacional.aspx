<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListarUnidadOrganizacional.aspx.vb"
    Inherits="USATCRIS_FrmListarUnidadOrganizacional" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />

    <script src="../assets/js/jquery.js" type="text/javascript"></script>

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
            margin: 4px;
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
            <div class="page_header">
                <div class="pull-left">
                    <i class="icon ti-import page_header_icon"></i><span class="main-text">Unidades Organizacionales</span>
                    <p class="text">
                        Unidades Organizacionales - DSpaceCris USAT
                    </p>
                </div>
                <div class="right pull-right">
                </div>
            </div>
            <div class="panel panel-piluku" id="PanelLista">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Unidades Organizacionales
                    </h3>
                </div>
                <div class="panel-body">
                    <form id="form1" runat="server">
                   <div class="row">
                        <div class="col-md-12">
                            <asp:GridView runat="server" ID="gvUnidadOrg" AutoGenerateColumns="False" CssClass="table table-responsive"
                                Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                    <asp:BoundField DataField="Director" HeaderText="Director" />
                                    <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" />
                                    <asp:BoundField DataField="Dependencia" HeaderText="Dependencia" />
                                </Columns>
                                <HeaderStyle BackColor="#E33439" ForeColor="White" />
                                <EmptyDataTemplate>
                                    No se Encontraron Registros
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <b>Investigadores</b>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView runat="server" ID="gvInvestigadores" AutoGenerateColumns="False" CssClass="table table-responsive"
                                Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="Investigador" HeaderText="Investigador" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                    <asp:BoundField DataField="Orcid" HeaderText="Orcid" />
                                </Columns>
                                <HeaderStyle BackColor="#E33439" ForeColor="White" />
                                <EmptyDataTemplate>
                                    No se Encontraron Registros
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
