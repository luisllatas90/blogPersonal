<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reuniones_POA.aspx.vb" Inherits="secretarias_reuniones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../estilo.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../funciones.js"> </script>
<script type="text/javascript"> 
    function HabilitarBoton(tipo,fila)
  	{
  	    document.form1.txtelegido.value=fila.id  	      	  
    	if (document.form1.cmdModificar!=undefined){
            document.form1.cmdModificar.disabled=false 
            document.form1.cmdPresentacion.disabled=false 
            }            
    	SeleccionarFila();
    	AbrirPestana(0,'datosreunion.aspx')
	}
	
    function AbrirPestana(tab,pagina)
    {

        if (document.form1.txtelegido.value>0){
	        ResaltarPestana(tab,'','')
	        fradetalle.location.href=pagina + "?id_rec=" + document.form1.txtelegido.value + "&codigo_per=<%=request.querystring("id")%>"
	    }
    }

</script>
<script  type="text/javascript">
	function verPresentacion2(){
		alert('HOLA');
		/*var codigo_rec=document.all.txtelegido.value
		
		codigo_rec=codigo_rec.substring(4,codigo_rec.length)	
		
		day = new Date();
		id = day.getTime();
		var URL="presentacion_intro.aspx?codigo_rec=" + codigo_rec	
		eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,fullscreen=yes');");
		*/
	}
</script>
    <title>Página sin título</title>
    <style type="text/css">

        .style1
        {
            height: 13px;
        }
        .style5
        {
            height: 18px;
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
                              <td width="40%"><asp:Button ID="cmdNuevo" runat="server" CssClass="nuevo1" Height="47px" 
                                    Text="   Nuevo" Width="92px" />                          
                                  <asp:Button ID="cmdModificar" runat="server" CssClass="modificar_1" Height="47px" 
                                    Text="   Modificar" Width="92px" Enabled="False" />                            
                                  <asp:Button ID="cmdPresentacion" runat="server" CssClass="presentacion" Height="47px" 
                                    Text="              Presentación" Width="142px" Enabled="False" />                            
                              </td>
                              <td width="15%" style="font-weight: bold"> 
                                  <asp:Button ID="cmdModificar0" runat="server" CssClass="ayuda_prp" Height="47px" 
                                    Text="       Ayuda" Width="92px" />                            
                                  </td>
                              <td width="15%" style="width: 20%" align="right">Secretaría de:</td>
                              <td width="15%"><asp:DropDownList ID="ddlInstanciaRevision" runat="server" AutoPostBack="True" 
                                                BackColor="#FFFF99">
                                  <asp:ListItem Value="F">Consejo de Facultad</asp:ListItem>
                                  <asp:ListItem Value="K">Rectorado</asp:ListItem>
                                  <asp:ListItem Value="C">Consejo Universitario</asp:ListItem>
                                </asp:DropDownList>
                              </td>
                              <td width="15%">&nbsp;</td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                            <td class="style1">
                                  <asp:HiddenField ID="txtelegido" runat="server" />
                                </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="contornotabla" height="250" valign="top" colspan="6">
                    <asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Vertical">
                        <asp:GridView ID="dgvPropuestas" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="id_Rec" 
                            DataSourceID="SqlDataSource1" GridLines="Horizontal" PageSize="7" 
                            Width="100%">
                            <RowStyle Height="26px" />
                            <Columns>
                                <asp:BoundField DataField="id_Rec" HeaderText="id_Rec" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="id_Rec" Visible="False" />
                                <asp:BoundField DataField="agenda_Rec" HeaderText="Descripción" 
                                    SortExpression="agenda_Rec" />
                                <asp:BoundField DataField="fecha_Rec" HeaderText="Fecha" 
                                    SortExpression="fecha_Rec" />
                                <asp:BoundField DataField="lugar_Rec" HeaderText="Lugar" 
                                    SortExpression="lugar_Rec" />
                                <asp:BoundField DataField="grabacion_rec" HeaderText="grabacion_rec" 
                                    SortExpression="grabacion_rec" Visible="False" />
                                <asp:BoundField DataField="acta_rec" HeaderText="acta_rec" 
                                    SortExpression="acta_rec" Visible="False" />
                                <asp:BoundField DataField="tipo_rec" HeaderText="tipo_rec" 
                                    SortExpression="tipo_rec" Visible="False" />
                                <asp:BoundField DataField="estado_rec" HeaderText="estado_rec" 
                                    SortExpression="estado_rec" Visible="False" />
                                <asp:BoundField DataField="codigo_Fac" HeaderText="codigo_Fac" 
                                    SortExpression="codigo_Fac" Visible="False" />
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="10pt" 
                                    ForeColor="Red" Text="No se encontraron registros."></asp:Label>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#666666" ForeColor="White" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                            SelectCommand="PRP_ConsultarSesionesConsejos" 
                            SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlInstanciaRevision" Name="tipo" 
                                    PropertyName="SelectedValue" Type="String" />
                                <asp:QueryStringParameter Name="codigo_per" QueryStringField="ID" 
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td width="20%" class="style5" ></td>
                <td width="1%"  class="style5"></td>
		        <td width="20%" class="style5"></td>
                <td width="1%"  class="style5"></td>
		        <td width="20%" class="style5">&nbsp;</td>
                <td width="38%"  class="style5"></td>
            </tr>
            <tr>
                <td class="style2" id="tab" width="20%" >
                    Reunión de Consejo                 </td>
                <td width="1%"></td>
		        <td id="tab0" width="20%" >
                    &nbsp;</td>
                <td align="right" width="1%"></td>
                <td id="tab1" width="20%">
                    &nbsp;</td>  
                <td width="38%"> 
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="contornotabla" height="300" valign="top" colspan="6" align="left">
                <iframe id="fradetalle" marginheight="0" marginwidth="0" frameborder="0" 
                        scrolling="auto" width="100%" height="300" name="fradetalle" >
                </iframe>
                                                
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
