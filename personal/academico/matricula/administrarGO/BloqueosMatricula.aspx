<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BloqueosMatricula.aspx.vb" Inherits="academico_matricula_administrar_BloqueosMatricula" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="../../../../private/estilo.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td >
                    La estudiantes tiene <b> BLOQUEOS</b> para matricula por los siguientes motivos:</td>
                <td align="right">
                    <asp:ImageButton ID="imgCerrar" runat="server" 
                        ImageUrl="../../../Images/cerrar.gif" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="gvBloqueos" runat="server" AutoGenerateColumns="False" 
                        Width="98%">
                        <Columns>
                            <asp:BoundField DataField="mensaje_blo" HeaderText="Motivo" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="acudirA_blo" HeaderText="Acudir A" />
                            <asp:BoundField DataField="fechaVence_blo" HeaderText="Fecha Venc. Bloqueo" />
                        </Columns>
                        <HeaderStyle BackColor="#0066CC" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
