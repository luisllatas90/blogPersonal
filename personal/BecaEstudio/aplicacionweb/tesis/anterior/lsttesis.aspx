<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lsttesis.aspx.vb" Inherits="lsttesis" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Investigacion</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
    function HabilitarBoton(tipo,fila)
  	{
  	    document.form1.txtelegido.value=fila.id
  	    
    	if (document.form1.cmdModificar!=undefined){
            document.form1.cmdModificar.disabled=false     
    	}
    	if (document.form1.cmdEliminar!=undefined){
            document.form1.cmdEliminar.disabled=false
    	}
    	SeleccionarFila();
    	AbrirPestana(0,'detalletesis.aspx');
	}
	
    function AbrirPestana(tab,pagina)
    {
        if (document.form1.txtelegido.value>0){
	        ResaltarPestana(tab,'','')
	        document.all.mensajedetalle.style.display="none"
	        fradetalle.location.href=pagina + "?codigo_tes=" + document.form1.txtelegido.value + "&codigo_per=<%=request.querystring("id")%>"
	    }
    }
    
    </script>
</head>
<body style="margin:10px, 10px, 10px, 10px; background-color:#F0F0F0">
    <form id="form1" runat="server">
        <table style="width: 100%; height: 650px;" cellpadding="3" cellspacing="0">
            <tr>
                <td colspan="3" style="height: 5%;">
                <asp:Button ID="cmdNuevo" runat="server" CssClass="enviarpropuesta"
                    Text="           Nuevo" ToolTip="Registrar nuevo Proyecto de Tesis"
                    Width="95px" />&nbsp;
                <asp:Button ID="cmdModificar" runat="server" Text="            Modificar" 
                    Width="90px" CssClass="nuevocomentario" ToolTip="Modificar datos de tesis" 
                    Enabled="False" />
                <asp:Button ID="cmdEliminar" runat="server" Text="       Eliminar" 
                    Width="110px" CssClass="noconforme1" ToolTip="Eliminar tesis" 
                    Enabled="False" Visible="False" />
                 </td>
            </tr>
            <tr id="trLista">
                <td style="width: 100%;height: 50%;" valign="top">
                <table style="width: 100%; height:100%" cellpadding="2" cellspacing="0">
                    <tr>
                        <td align="left" style="border-top: black 1px solid; height: 5%;" 
                            valign="middle">
                            <asp:DropDownList ID="dpFase" runat="server" Font-Size="9px">
                            </asp:DropDownList>
                            <asp:DropDownList ID="dpEstado" runat="server" Font-Size="9px" Visible="False">
                            </asp:DropDownList>
                            &nbsp;<asp:DropDownList ID="dpEscuela" runat="server" Font-Size="9px">
                            </asp:DropDownList>
                            <asp:Button ID="cmdBuscar" runat="server" CssClass="buscar1" 
                                Text="     Buscar" />
                        </td>
                    </tr>
                    <tr>
                        <td class="contornotabla" valign="top" align="center">
                        <table style="width:100%; height:100%" cellpadding="3" cellspacing="0">
                             <tr class="etabla">
                             <td style="width:5%; height:10%" align="center">Nro</td>
                             <td style="width:45%;height:10%" align="center">Título</td>
                             <td style="width:20%; height:10%" align="center">Autor</td>
                             <td style="width:25%;height:10%">Asesor</td>
                             <td style="width:5%; height:10%">Bloq</td>
                             </tr>
                             <tr><td colspan="5" style="height:90%">
                             <div id="listadiv" style=" height:100%; width:100%">
                               <asp:GridView ID="GridView1" runat="server"
                                AutoGenerateColumns="False" DataKeyNames="codigo_Tes"
                                GridLines="Horizontal" Width="100%" AllowSorting="True" ShowHeader="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="N&#176;">
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="codigo_Inv" HeaderText="codigo_tes" InsertVisible="False"
                                        ReadOnly="True" SortExpression="codigo_tes" Visible="False" />
                                    <asp:BoundField DataField="titulo_tes" HeaderText="Nombre de la Tesis"
                                        SortExpression="titulo_tes">
                                        <ItemStyle Width="45%" Font-Size="7pt" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="autorprincipal" HeaderText="Autor principal">
                                        <ItemStyle Width="20%" Font-Size="7pt" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="asesor" HeaderText="Asesor">
                                        <ItemStyle HorizontalAlign="Center" Width="25%" Font-Size="7pt" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="bloqueo" HeaderText="Bloqueado">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                </Columns>
                                <RowStyle BorderStyle="None" Height="17px" />
                                <PagerStyle BackColor="Silver" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <strong style="width: 100%; color: red; text-align: center">
                                        <br />
                                        <br />
                                        No existen registradas investigaciones en esta Etapa.</strong>
                                </EmptyDataTemplate>
                            </asp:GridView>
                             </div>
                             </td>
                              </tr>
                            </table>      
                        </td>
                    </tr>
                    </table>
                    <asp:Label ID="LblMensaje" runat="server"></asp:Label></td>
            </tr>
            <tr id="trDetalle">
                <td colspan="3" style="height: 45%;">
                      <table cellspacing="0" cellpadding="0" style="border-collapse: collapse; height: 100%;" bordercolor="#111111" width="100%">
                      <tr style="height: 12%; text-align:center">
                        <td class="pestanaresaltada" id="tab" width="20%" onClick="AbrirPestana(0,'detalletesis.aspx')">Datos Generales</td>
                        <td width="1%" class="bordeinf">&nbsp;</td>
		                <td class="pestanabloqueada" id="tab" width="20%" onClick="AbrirPestana(1,'detalleetapatesis.aspx')">
                            Etapas</td>
                        <td width="59%" class="bordeinf" align="right">
                        <img border="0" src="../../../images/maximiza.gif" style="cursor:hand" ALT="Maximizar ventana" onClick="Maximizar(this,'../../../','100%','65%')" />
                        </td>
                      </tr>
                      <tr style="height: 88%">
                        <td width="100%" valign="top" colspan="4" class="pestanarevez"><span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Seleccione una Investigaci&oacute;n para visualizar su detalle</span>
                            <iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0" scrolling="yes" style="height: 100%"> </iframe>
                            </td>
                      </tr>
                    </table>
                 </td>
            </tr>
        </table>
    <asp:HiddenField  ID="txtelegido" runat="server" />
    <asp:HiddenField ID="hdcodigo_per" Value="0" runat="server" />    
    </form>
</body>
</html>

