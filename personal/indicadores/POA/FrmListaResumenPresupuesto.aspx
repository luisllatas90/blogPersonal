<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaResumenPresupuesto.aspx.vb"
    Inherits="indicadores_POA_FrmListaEvaluarPresupuesto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />

    <script src="Jquery/jquery-1.12.3.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#btnExportar").on('click', function(e) {
                var tblHTML = $("#TablaActividadesPto").html();
                tblHTML = escape(tblHTML);
                $('#HdData').val(tblHTML);
            });

        });
    </script>

    <style type="text/css">
        .celda_combinada
        {
            border-color: rgb(169,169,169);
            border-style: solid;
            border-width: 1px;
            font-size: 10px;
            vertical-align: middle;
        }
        a:hover
        {
            color: Green;
            text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="hdfila" Value="-1" />
    <asp:HiddenField runat="server" ID="actividadPto" />
    <asp:HiddenField runat="server" ID="cecoPto" />
    <asp:HiddenField runat="server" ID="instanciaPto" />
    <asp:HiddenField runat="server" ID="estadoPto" />
    <asp:HiddenField runat="server" ID="codigoPto" />
    <asp:HiddenField runat="server" ID="HdData" />
    <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server" Text="Resumen de Presupuesto de Programa/Proyecto"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%" id="tabla" runat="server">
            <tr style="height: 30px;">
                <td width="140px">
                    Plan Estratégico
                </td>
                <td width="510px">
                    <asp:DropDownList ID="ddlplan" runat="server" Width="500" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td width="50px">
                </td>
                <td width="140px">
                    Ejercicio Presupuestal
                </td>
                <td>
                    <asp:DropDownList ID="ddlEjercicio" runat="server" Width="140" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="btnBuscar" />
                </td>
            </tr>
            <tr>
                <td>
                    Plan Operativo Anual
                </td>
                <td>
                    <asp:DropDownList ID="ddlPoa" runat="server" Width="500">
                        <asp:ListItem Value="0">--SELECCIONE--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="4">
                </td>
                <%--   <td>Estado de Actividad</td>
         <td>
         <asp:DropDownList ID="ddlestado" runat="server" Width="140" >
          <asp:ListItem Value="2">Pendientes</asp:ListItem>
            <asp:ListItem Value="4">Observados</asp:ListItem>
            <asp:ListItem Value="7">Aprobados</asp:ListItem>
            <asp:ListItem Value="T">Todos</asp:ListItem>
        </asp:DropDownList>
        </td>
        <td></td>--%>
            </tr>
            <tr style="height: 15px">
                <td colspan='5' align="left">
                    <table>
                        <tr>
                            <td>
                                <b>TOPE PRESUPUESTADO: Indica el límite asignado por el área de Presupuesto/Finanzas.</b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>MONTO REGISTRADO: Indica el monto total registrado a través del módulo de Presupuesto.</b>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:Button ID="btnExportar" name="btnExportar" CssClass="btnExporta" runat="server"
                        Text="   Exportar" />
                </td>
            </tr>
        </table>
        <div id='TablaActividadesPto' width="100%" runat="server">
        </div>
    </div>
    </form>
</body>
</html>
