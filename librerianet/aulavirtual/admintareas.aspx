<%@ Page Language="VB" AutoEventWireup="false" CodeFile="admintareas.aspx.vb" Inherits="admintareas" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administración de tareas</title>
    <style fprolloverstyle>A:hover {color: #FF0000; text-decoration: underline;}</style>
    <style type="text/css">
        
        .contornodiv
        {
            border: 1px solid #666666;
        }

        .listaCombo
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 8pt;
        }

    </style>
     <script src="../../private/funciones.js"></script>
    <script language="javascript" type="text/javascript">
        function AbrirAcciones(tipo,ruta)
        {
                switch(tipo)
	            {
		            case 1://Descargar
		                window.open(ruta)
		                break
            		case 2://Nueva versión
			            AbrirPopUp(ruta,"300","550")
			            break
            		case 3://Detalles
			            AbrirPopUp(ruta,"500","600","yes","yes","yes")
			            break
			        case 4://Eliminar
			            if (confirm("Está seguro que desea eliminar el trabajo enviado")==true){
			                location.href=ruta
			            }
			            break		            
			    }
        }
    </script>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css">
</head>
<body>
    
    <form id="form1" runat="server">
    <table style="width:100%;height:95%;" border="0" cellpadding="0" cellspacing="0" align="center">
        <tr>
          <td style="width:70%;height:5%;" class="usattitulo">Administración de tareas</td>
          <td style="width:30%;height:5%; text-align:right">
              <asp:Button ID="cmdEnviar" runat="server" Text="Enviar trabajo" 
                  UseSubmitBehavior="False" />
&nbsp;<input id="Button1" type="button" class="boton" value="Ayuda" onclick="AbrirPopUp('ayuda.html','500','500')" />
              </td>
        </tr>
        <tr>
          <td style="width:70%;height:5%;" valign="top">
          <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="10pt" 
                  Text="Label"></asp:Label>
          </td>
          <td style="width:30%;height:5%;text-align:right">
          Mostrar
              trabajos:<asp:DropDownList ID="dtTipo" runat="server" AutoPostBack="True">
                  <asp:ListItem Value="T">Todos</asp:ListItem>
                  <asp:ListItem Value="P">Por revisar</asp:ListItem>
                  <asp:ListItem Value="R">Revisados</asp:ListItem>
              </asp:DropDownList>
          </td>
        </tr>
        <tr>
            <td class="contornodiv" style="width:100%;height:80%; background-color:White" 
                colspan="2">
            <div id="listadiv" style="height:100%">
            <asp:TreeView ID="trw" runat="server" Font-Names="Verdana" Font-Size="8pt">
                <HoverNodeStyle Font-Strikeout="False" BackColor="#FFCC66" />
            </asp:TreeView>
            </div>
            </td>
        </tr>
    </table>
     <p>
         <asp:Label ID="lblMensaje" runat="server" CssClass="usatsugerencia"
             Font-Bold="True" Font-Size="10pt" ForeColor="Red" 
             Text="&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;No se han encontrado trabajos enviados" 
             Visible="False" Width="100%"></asp:Label>
    </p>
     </form>
</body>
</html>
