﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PagosExamenExtraordinario.aspx.vb" Inherits="administrativo_controlpagos_PagosExamenExtraordinario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pagos Examen Extraordinario</title>
     <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="width:100%;">
            <tr class="usatTitulo">
                <td>
                    REPORTE DE PAGOS EXAMEN EXTRAORDINARIO</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Ciclo Académico:
                    <asp:DropDownList ID="cboCiclo" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvwPagos" runat="server">
                        <HeaderStyle BackColor="#3366CC" Font-Size="X-Small" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
