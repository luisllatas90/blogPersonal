<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmPersona.aspx.vb"
    Inherits="CRISUSAT_FrmUnidadOrganizacional" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Personas</title>
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
        /*==== Tabla =====*/
        table
        {
            font-family: Trebuchet MS;
            font-size: 8pt;
        }
        tbody tr
        {
            font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
            font-size: 8pt;
            color: #2F4F4F;
            padding: 3px;
        }
        tbody td
        {
            padding: 3px;
        }
        thead tr
        {
            font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
            font-size: 9px;
            font-weight: bold;
            border: rgb(169,169,169) 1px solid;
            color: white;
            background-color: #3871b0;
            cursor: auto;
            text-align: center;
            vertical-align: middle;
            padding: 2px;
        }
        thead tr th
        {
            font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
            font-size: 9px;
            font-weight: bold;
            border: rgb(169,169,169) 1px solid;
            color: white;
            background-color: #3871b0;
            cursor: auto;
            text-align: center;
            vertical-align: middle;
            padding: 2px;
        }
        .celda_combinada
        {
            border-color: rgb(169,169,169);
            border-style: solid;
            border-width: 1px;
        }
        /*==== Fin Tabla =====*//*
        .checkbox label
        {
            padding-left: 1px;
        }
        input[type="checkbox"] + label
        {
            color: Black;
        }
        #btnORCID
        {
            border: 1px solid #D3D3D3;
            padding: .3em;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 1px 1px 3px #999;
            cursor: pointer;
            color: #995;
            font-weight: bold;
            line-height: 24px;
            vertical-align: middle;
        }
        #btnORCID:hover
        {
            border: 1px solid #338caf;
            color: #338caf;
        }
        #orcid-id-icon
        {
            display: block;
            margin: 0 .5em 0 0;
            padding: 0;
            float: left;
            width: 24px;
            height: 24px;
        }
        .btnIr
        {
            border: 1px solid #D3D3D3;
            padding: .3em;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 1px 1px 3px #999;
            cursor: pointer;
            color: #339CFF;
            font-weight: bold;
            font-size: .9em;
            line-height: 24px;
            vertical-align: middle;
        }
        .btnIr:hover
        {
            border: 1px solid #338caf;
            color: #338caf;
        }
        :-ms-input-placeholder.form-control
        {
            line-height: 0px;
        }*/
        .centrar
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <div class="wrapper">
        <div class="content">
            <form id="form1" runat="server">
            <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>--%>
            <div class="page_header">
                <div class="pull-left">
                    <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Persona</span>
                </div>
            </div>
            <div class="panel panel-piluku">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Personas
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblUnidad" Text="Tipo de Unidad" CssClass="control-label col-md-2"></asp:Label>
                            <div class="col-md-4">
                                <asp:DropDownList runat="server" ID="ddTipoPersona" CssClass="form-control" AutoPostBack="true">
                                    <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                    <asp:ListItem Value="A">Alumno</asp:ListItem>
                                    <asp:ListItem Value="C">Colaborador</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtRespuesta" Width="100%" TextMode="MultiLine" Rows="5"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:GridView runat="server" ID="gvUnidadOrganizacional1" Width="100%" 
                            AllowSorting="True">
                            <Columns>
        
                                <%--<asp:BoundField HeaderText="USATCRIS" DataField="UnidadPadre" />--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Button runat="server" ID="btnExportar" CssClass="btn btn-primary" Text="Exportar" />
                            </div>
                        </div>
                        <div class="row">
                            <asp:GridView runat="server" ID="gvUnidadOrganizacional" Width="100%" AutoGenerateColumns="False"
                                CellPadding="3">
                                <Columns>
                                    <asp:BoundField HeaderText="ID" DataField="usatID">
                                        <ItemStyle Width="7%" />
                                        <HeaderStyle CssClass="centrar" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Persona" DataField="persona">
                                        <ItemStyle Width="73%" />
                                        <HeaderStyle CssClass="centrar" />
                                    </asp:BoundField>
                                    <%--<asp:BoundField HeaderText="USATCRIS" DataField="USATCRIS" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="10%" CssClass="centrar" />
                                        <HeaderStyle CssClass="centrar" />
                                    </asp:BoundField>--%>
                                    <asp:BoundField HeaderText="IDCRIS" DataField="cod_rp" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="10%" CssClass="centrar" />
                                        <HeaderStyle CssClass="centrar" />
                                    </asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No se Encontraron Registros
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#E33439" ForeColor="White" Height="25px" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
