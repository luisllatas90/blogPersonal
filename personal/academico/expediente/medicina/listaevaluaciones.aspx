<%@ Page Language="VB" AutoEventWireup="false" CodeFile="listaevaluaciones.aspx.vb" Inherits="medicina_listaevaluaciones" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link rel="stylesheet" type="text/css" href="../../../../private/estilo.css"/>
    <script language="javascript" src="../../../../private/funciones.js" ></script>
   <script type="text/javascript" language="javascript">
        function cambiarDisplay(id,id2) 
			{
	          if (!document.getElementById) 
			  	return false;
	          fila = document.getElementById(id);
	          if (fila.style.display != "none")
			  	{
				id2.cells[0].innerHTML="<img src='../../../../images/mas.gif'>"
	            fila.style.display = "none"; //ocultar fila 
	          	} 
			  else 
			  	{
				id2.cells[0].innerHTML="<img src='../../../../images/menos.gif'>"
	            fila.style.display = ""; //mostrar fila 
	          	}
    	    }
</script>
  
</head>
<body style="margin:0,0,0,0">
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td colspan="3" style="border-top: black 1px solid; font-weight: bold; font-size: 11pt;
                    color: white; border-bottom: black 1px solid; font-family: verdana; height: 21px;
                    background-color: firebrick; text-align: center">
                    Evaluaciones</td>
            </tr>
            <tr>
                <td style="width: 183px">
                    &nbsp;
                    <asp:HyperLink ID="LinkRegresar" runat="server" Style="font-size: 8pt; font-family: verdana">«« Regresar</asp:HyperLink></td>
                <td align="right" colspan="2" style="font-size: 8pt; width: 1416px; color: #000000;
                    font-family: verdana">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" style="border-top: #660000 1px solid; font-weight: bold; font-size: 8pt;
                    text-transform: capitalize; color: #003300; font-family: verdana; font-variant: normal">
                    &nbsp; &nbsp;Profesor :
                    <asp:Label ID="LblProfesor" runat="server" Style="text-transform: uppercase; color: dimgray;
                        font-family: verdana"></asp:Label></td>
            </tr>
            <tr style="font-size: 8pt">
                <td colspan="3" style="font-weight: bold; font-size: 8pt; text-transform: capitalize;
                    color: #003300; border-bottom: #660000 1px solid; font-family: verdana; font-variant: normal">
                    &nbsp; &nbsp;Asignatura :
                    <asp:Label ID="LblAsignatura" runat="server" Style="text-transform: uppercase; color: dimgray;
                        font-family: verdana"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="3" style="height: 32px; font-weight: bold; text-decoration: underline;" valign="bottom">
                    Lista de Evaluaciones para Ingreso y/o Actualización de Notas</td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Table ID="TblActividades" runat="server" CellPadding="0" CellSpacing="4" Width="90%">
                    </asp:Table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
