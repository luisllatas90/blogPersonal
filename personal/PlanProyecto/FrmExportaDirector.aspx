<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmExportaDirector.aspx.vb" Inherits="PlanProyecto_FrmExportaDirector" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td align="right">
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>        
        <tr>
            <td id="calendario" runat="server">            
            
            </td>
        </tr>
    </table> 
    <asp:HiddenField ID="hfCalendario" runat="server" />   
    <asp:HiddenField ID="hfAnio" runat="server" />
    </form>
</body>
</html>
