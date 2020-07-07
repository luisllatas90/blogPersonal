<%@ Page Language="VB" AutoEventWireup="false" CodeFile="seguimientoAdmin.aspx.vb" Inherits="administrativo_propuestas2_proponente_seguimientoAdmin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../estilo.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../funciones.js"> </script>
<script type="text/javascript" language="javascript" src="../lytebox.js"></script>
<link rel="stylesheet" href="../lytebox.css" type="text/css" media="screen" />
<script type="text/javascript"> 
    function HabilitarBoton(tipo,fila)
  	{
  	    document.form1.txtelegido.value=fila.id
  	  	SeleccionarFila();
    	AbrirPestana(0,'datospropuesta.aspx')
    	if (document.form1.cmdDerivar!=undefined){
            document.form1.cmdDerivar.disabled=false
        }    	
	}
	
    function AbrirPestana(tab,pagina)
    {

        if (document.form1.txtelegido.value>0){
	        ResaltarPestana(tab,'','')
	        fradetalle.location.href=pagina + "?codigo_prp=" + document.form1.txtelegido.value + "&codigo_per=<%=request.querystring("id")%>&seg=1"
	    }
    }


</script>
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            height: 13px;
        }
        .style2
        {
            border-left: 1px solid #808080;
            border-right: 1px solid #808080;
            border-top: 1px solid #808080;
            color: #0000FF;
            background-color: #EEEEEE;
            font-weight: bold;
            cursor: hand;
            height: 31px;
        }
        .style3
        {
            border-left-width: 1;
            border-right-width: 1;
            border-top-width: 1;
            border-bottom: 1px solid #808080;
            height: 31px;
            width: 0%;
        }
        .style5
        {
            height: 18px;
        }
        .style7
        {
            width: 100%;
        }
        .style4
        {
            border: 1px solid #808080;
            background-color: #E1F1FB;
            cursor: hand;
            height: 31px;
        }
        </style>
    </head>
<body bgcolor="#F0F0F0">
    <form id="form1" runat="server">
   <table style="width: 100%; height: 100%" align="left">
            <tr>
                <td class="contornotabla" valign="top" width="100%" colspan="6">
                    <table  width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                          <td bgcolor="#F0F0F0" class="bordeinf" width="20%"><table style="width:100%;">
                            <tr>
                              <td class="style7">
                                  <b>Seguimiento de propuestas</b> <br/>
                                  Personal: <asp:TextBox ID="txtPersonal" runat="server" Width="40%"></asp:TextBox>
                        <asp:GridView ID="dgvPropuestas" runat="server" 
                            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="codigo_Prp"                             
    GridLines="Horizontal" PageSize="7" 
                            Width="100%" AllowPaging="True">
                            <RowStyle Height="26px" />
                            <Columns>
                                <asp:BoundField DataField="codigo_Prp" HeaderText="codigo_Prp" 
                                    InsertVisible="False" ReadOnly="True" SortExpression="codigo_Prp" 
                                    Visible="False" />
                                <asp:BoundField DataField="nombre_prp" HeaderText="Propuesta" 
                                    SortExpression="nombre_prp" />
                                <asp:BoundField DataField="fechainicio_Ipr" HeaderText="Fecha Inicio" 
                                    SortExpression="fechainicio_Ipr" />
                                <asp:BoundField DataField="Proponente" HeaderText="Proponente" ReadOnly="True" 
                                    SortExpression="Proponente" />
                                <asp:BoundField DataField="codigo_Ipr" HeaderText="codigo_Ipr" 
                                    InsertVisible="False" ReadOnly="True" SortExpression="codigo_Ipr" 
                                    Visible="False" />
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="10pt" 
                                    ForeColor="Red" Text="No se encontraron registros."></asp:Label>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#666666" ForeColor="White" />
                        </asp:GridView>
                                  &nbsp; 
                                  Instancia: <asp:DropDownList ID="ddlInstanciaPropuesta" runat="server" AutoPostBack="True" Width="25%">
                                      <asp:ListItem Value="F">Consejo de Facultad</asp:ListItem>
                                      <asp:ListItem Value="K">Rectorado</asp:ListItem>
                                      <asp:ListItem Value="C">Consejo Universitario</asp:ListItem>
                                      <asp:ListItem Value="">Todos</asp:ListItem>
                                    </asp:DropDownList>&nbsp; 
                                  <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" />
                                  <br />                                  
                                  <asp:Label ID="lblSecretaria" runat="server"></asp:Label>
                              </td>                                                            
                              
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <asp:Label ID="lblMensaje" runat="server" BorderColor="Red" Font-Bold="True" 
                                    ForeColor="Red"></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="txtelegido" runat="server" />
                                <asp:HiddenField ID="HdUsuario" runat="server" />
                                <asp:Label ID="txtfacultad" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="contornotabla" height="250" valign="top" colspan="6">
                    <asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Vertical">
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td width="20%" class="style5" ></td>
                <td width="1%"  class="style5"></td>
		        <td width="20%" class="style5"></td>
                <td width="1%"  class="style5"></td>
		        <td width="20%" class="style5">&nbsp;</td>
                <td width="38%"  class="style5" style="color: #FF0000"></td>
            </tr>
            <tr>
                <td class="style2" id="tab0" width="20%" 
                    onClick="AbrirPestana(0,'datospropuesta.aspx')" >
                    Datos Generales
                </td>
                <td width="1%" class="style3"></td>
		        <td class="style4" id="tab" width="20%" onClick="AbrirPestana(1,'revisores.aspx')">
                    Revisión
                        </td>
                <td class="style3" align="right" width="1%"></td>
                <td class="style4" id="tab1" width="20%" 
                    onClick="AbrirPestana(2,'comentarios.aspx')">
                    Comentarios
                </td>  
                <td width="38%" > 
                    <table >
                        <tr>
                            <td align="center">
                                &nbsp;</td>
                            <td align="center" width="25%" style="width: 0%; color: #FF0000;">
                                &nbsp;</td>
                            <td align="center" width="25%" style="width: 17%">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="contornotabla" height="300" valign="top" colspan="6" align="left">
                <iframe id="fradetalle" marginheight="0" marginwidth="0" frameborder="0" 
                        scrolling="auto" src="datospropuesta.aspx" width="100%" height="300" >
                </iframe>
                                                
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
