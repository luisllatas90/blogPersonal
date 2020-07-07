<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmMoverDetalleActividad.aspx.vb"
    Inherits="indicadores_POA_FrmMoverDetalleActividad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />

    <script src="Jquery/jquery-1.12.3.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {

        });
    </script>

    <style type="text/css">
        .nombre_poa
        {
            color: #468847;
            font-weight: bold;
            padding-top: 5px;
            padding-bottom: 5px;
        }
        .nombre_prog
        {
            color: #aa6708;
            font-weight: bold;
            padding-top: 5px;
            padding-bottom: 5px;
        }
        .nombre_dap
        {
            color: Purple;
            font-weight: bold;
            padding-top: 5px;
            padding-bottom: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server" Text="Mover Actividad a otro Programa/Proyecto"></asp:Label>
    </div>
    <div class="contorno_poa">
        <div>
            <table style="width: 100%">
                <tr style="height: 30px">
                    <td style="width: 20%">
                        Plan Operativo Anual:
                    </td>
                    <td>
                        <asp:Label ID="lblpoa" runat="server" CssClass="nombre_poa"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        Programa/Proyecto:
                    </td>
                    <td>
                        <asp:Label ID="lblacp" runat="server" CssClass="nombre_prog"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        Actividad:
                    </td>
                    <td>
                        <asp:Label ID="lbldap" runat="server" CssClass="nombre_dap"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        Nuevo Programa/Proyecto:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProgProy" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%">
            <tr>
                <td colspan="2">
                    <div runat="server" id="aviso">
                        <asp:Label ID="lblrpta" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button runat="server" ID="btnRegresar" CssClass="btnRegresar" Text="  Regresar" />
                    <asp:Button runat="server" ID="btnGuardar" CssClass="btnGuardar" Text="   Guardar" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
