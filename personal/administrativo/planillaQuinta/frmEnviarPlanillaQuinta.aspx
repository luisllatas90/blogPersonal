<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEnviarPlanillaQuinta.aspx.vb" Inherits="librerianet_planillaQuinta_frmEnviarPlanillaQuinta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>
                            <table style="width:100%;" class="contornotabla">
                                <tr>
                                    <td>
                                        Ejercicio Presupuestal</td>
                                    <td>
                                        <asp:DropDownList ID="cboPeriodoPresu" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Planilla</td>
                                    <td>
                                        <asp:DropDownList ID="cboPlanilla" runat="server" AutoPostBack="True">
                                            <asp:ListItem Value="134">Planilla 1</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        &nbsp;</td>
                                    <td>
                                        <asp:Button ID="cmdEnviar" runat="server" Text="Enviar todas las planillas a Dirección de Personal" 
                                            Width="320px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="margin-left: 80px">
                                        &nbsp;</td>
                                </tr>
                            </table>
                </td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
