<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmasignarprofesor.aspx.vb" Inherits="academico_frmasignarprofesor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Asignar profesor según Curso del Dpto.</title>
        <link href="../../private/estilo.css" rel="stylesheet" type="text/css">
<script type="text/javascript" language="javascript">
	function VerFila(fila)
	{
		if (fila.length==undefined){
			if (fila.style.display=="none"){
				fila.style.display=""
			}
			else{
				fila.style.display="none"
			}
		}
		else{
			for (var i=0;i<fila.length;i++){
				var item=fila[i]
				
				if (item.style.display=="none"){
					item.style.display=""
				}
				else{
					item.style.display="none"
				}
			}
		}
	}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <table border=0 width="100%">
        <tr>
            <td>
                Código</td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Escuelas Profesionales &amp; Planes de Estudio</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Table ID="tblAsignacion" runat="server" Width="100%" 
                    style="border-collapse: collapse" border="1" bordercolor="Gray">
                    <asp:TableRow runat="server" CssClass="etabla">
                        <asp:TableCell runat="server" Width="5%">Grupo</asp:TableCell>
                        <asp:TableCell runat="server" Width="25%">Escuela Profesional</asp:TableCell>
                        <asp:TableCell runat="server" Width="25%">Plan de Estudios</asp:TableCell>
                        <asp:TableCell runat="server" Width="30%">Profesor</asp:TableCell>
                        <asp:TableCell runat="server" Width="10%">Estado</asp:TableCell>
                        <asp:TableCell runat="server" Width="5%">Acción</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
