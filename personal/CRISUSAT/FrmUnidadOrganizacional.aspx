<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmUnidadOrganizacional.aspx.vb"
    Inherits="CRISUSAT_FrmUnidadOrganizacional" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unidades Organizacionales</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />

    <script src="../assets/js/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        function MostrarModalJS() {
            window.location.href = '#modalHtml';
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
        /*==== Tabla =====*/table
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
        }*/.centrar
        {
            text-align: center;
        }
        .modalDialog
        {
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            background: rgba(180,180,180,0.8);
            z-index: 99999;
            opacity: 0;
            -webkit-transition: opacity 600ms ease-in;
            -moz-transition: opacity 600ms ease-in;
            transition: opacity 600ms ease-in;
            pointer-events: none;
        }
        .modalDialog:target
        {
            opacity: 1;
            pointer-events: auto;
        }
        .modalDialog > div
        {
            width: 400px;
            position: relative;
            margin: 10% auto;
            padding: 5px 20px 13px 20px;
            border-radius: 10px;
            background: #fff;
            background: -moz-linear-gradient(#fff, #999);
            background: -webkit-linear-gradient(#fff, #999);
            background: -o-linear-gradient(#fff, #999);
        }
        .close
        {
            background: #606061;
            color: #FFFFFF;
            line-height: 25px;
            position: absolute;
            right: -12px;
            text-align: center;
            top: -10px;
            width: 24px;
            text-decoration: none;
            font-weight: bold;
            -webkit-border-radius: 12px;
            -moz-border-radius: 12px;
            border-radius: 12px;
            -moz-box-shadow: 1px 1px 3px #000;
            -webkit-box-shadow: 1px 1px 3px #000;
            box-shadow: 1px 1px 3px #000;
        }
        .close:hover
        {
            background: #00d9ff;
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
                    <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Unidad
                        Organizacional</span>
                </div>
            </div>
            <div class="panel panel-piluku">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Unidades Organizacionales
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblUnidad" Text="Tipo de Unidad" CssClass="control-label col-md-2"></asp:Label>
                            <div class="col-md-4">
                                <asp:DropDownList runat="server" ID="ddTipoUnidad" CssClass="form-control" AutoPostBack="true">
                                    <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                    <asp:ListItem Value="F">Facultad</asp:ListItem>
                                    <asp:ListItem Value="D">Departamento Académico</asp:ListItem>
                                    <asp:ListItem Value="C">Carrera Profesional</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <%--<asp:TextBox runat="server" ID="txtRespuesta" Width="100%" TextMode="MultiLine" Rows="5"></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:GridView runat="server" ID="gvUnidadOrganizacional1" Width="100%" AutoGenerateColumns="false"
                            AllowSorting="True">
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="ID" />
                                <asp:BoundField HeaderText="cod_uo" DataField="cod_ou" />
                                <asp:BoundField HeaderText="UNIDAD ORGANIZACIONAL" DataField="Unidad" />
                                <asp:BoundField HeaderText="USATCRIS" DataField="UnidadPadre" />
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
                                    <asp:BoundField HeaderText="ID" DataField="codigo">
                                        <ItemStyle Width="7%" />
                                        <HeaderStyle CssClass="centrar" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="UNIDAD ORGANIZACIONAL" DataField="nombre">
                                        <ItemStyle Width="73%" />
                                        <HeaderStyle CssClass="centrar" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="USATCRIS" DataField="USATCRIS" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="10%" CssClass="centrar" />
                                        <HeaderStyle CssClass="centrar" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="IDCRIS" DataField="IDCRIS" HeaderStyle-HorizontalAlign="Center">
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
            <div id="modalHtml" class="modalDialog">
                <div>
                    <a href="#cerrar" title="Cerrar" class="close">X</a>
                    <h2>
                        Ventana de espera</h2>
                    <p>
                        Esta es una ventana de espera generada con HTML y CSS3.</p>
                </div>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
