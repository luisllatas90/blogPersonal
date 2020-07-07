<%@ Page Language="VB" AutoEventWireup="false" CodeFile="seguimiento_POA_v1.aspx.vb" Inherits="proponente_contenido" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../estilo.css?x=2222" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../funciones.js"> </script>
    <script type="text/javascript" language="javascript" src="../lytebox.js"></script>
    <link rel="stylesheet" href="../lytebox.css" type="text/css" media="screen" />
    <script type="text/javascript"> 
        function HabilitarBoton(tipo,fila)
  	    {
  	        document.form1.txtelegido.value=fila.id
  	  	    SeleccionarFila();
    	    AbrirPestana(0,'datospropuesta_POA.aspx')
       	    //if (document.form1.cmdDerivar!=undefined){
       	    if (tipo=='M'){
       	        //alert(tipo)
                document.form1.cmdDerivar.disabled=false
            }else{
                //alert(tipo)
                document.form1.cmdDerivar.disabled=true
            }
            //}    	
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
            width: 490px;
        }
        .style8
        {
            width: 56px;
        }
        .style4
        {
            border: 1px solid #808080;
            background-color: #E1F1FB;
            cursor: hand;
            height: 31px;
        }
        .style9
        {
            height: 250px;
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
                                  <b>Seguimiento de Propuestas</b><asp:HiddenField ID="txtelegido" runat="server" />
                                  <asp:Label ID="lblUsuario" runat="server" Visible="False"></asp:Label>
                                  <asp:Label ID="lblSecretaria" runat="server"></asp:Label>
                              </td>
                              <td style="font-weight: bold" class="style8">&nbsp;</td>
                              <td width="15%" style="width: 20%">&nbsp;</td>
                              <td width="15%" style="text-align: rigInstacia</td>
                              <td width="15%"><asp:DropDownList ID="ddlInstanciaPropuesta" runat="server" AutoPostBack="True">
                                  <asp:ListItem Value="F">Consejo de Facultad</asp:ListItem>
                                  <asp:ListItem Value="K">Rectorado</asp:ListItem>
                                  <asp:ListItem Value="A">Consejo Administrativo</asp:ListItem>
                                    </asp:DropDownList>
                              </td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                            <td class="style1"></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" SelectCommand="PRP_ConsultarSeguimiento_v1" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="S" Name="tipo" Type="String" />
                                        <asp:ControlParameter ControlID="txtfacultad" DefaultValue="" Name="codigo_per" PropertyName="Text" Type="Int32" />
                                        <asp:Parameter DefaultValue="x" Name="instanciaInvolucrado" Type="String" />
                                        <asp:ControlParameter ControlID="ddlInstanciaPropuesta" DefaultValue="" Name="instanciaPropuesta" PropertyName="SelectedValue" Type="String" />
                                        <%--<asp:Parameter DefaultValue="x" Name="estadoPrp" Type="String" />--%>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <asp:Label ID="txtfacultad" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="contornotabla" height="250" valign="top" colspan="6">
                    <asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Vertical">
                        <asp:GridView ID="dgvPropuestas" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" DataKeyNames="codigo_Prp" DataSourceID="SqlDataSource1"
                            GridLines="Horizontal" PageSize="7" Width="100%">
                            <RowStyle Height="22px" />
                            <Columns>
                                <asp:BoundField DataField="codigo_Prp" HeaderText="ID" SortExpression="codigo_Prp" />
                                <asp:BoundField DataField="nombre_prp" HeaderText="Propuesta" SortExpression="nombre_prp" />
                                <asp:BoundField DataField="tipoPropuesta" HeaderText="Tipo de Propuesta" SortExpression="tipoPropuesta" />
                                <asp:BoundField DataField="fechainicio_Ipr" HeaderText="Fecha Inicio" SortExpression="fechainicio_Ipr" />
                                <asp:BoundField DataField="Proponente" HeaderText="Proponente" ReadOnly="True" SortExpression="Proponente"
                                    Visible="False" />
                                <asp:BoundField DataField="codigo_Ipr" HeaderText="codigo_Ipr" InsertVisible="False"
                                    ReadOnly="True" SortExpression="codigo_Ipr" Visible="False" />
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Red"
                                    Text="No se encontraron registros."></asp:Label>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#666666" ForeColor="White" Height="22px" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        
            <tr>
                <td width="20%" class="style5" ></td>
                <td width="1%"  class="style5"></td>
		        <td width="20%" class="style5"></td>
                <td width="1%"  class="style5"></td>
		        <td width="20%" class="style5">&nbsp;</td>
                <td width="38%" class="style5" style="color: #FF0000">Atención: La opción DERIVAR enviará la propuesta a Rectoradorado</td>
            </tr>
            <tr>
                <td class="style2" id="tab0" width="20%" onClick="AbrirPestana(0,'datospropuesta_POA.aspx')" >Datos Generales</td>
                <td width="1%" class="style3"></td>
		        <td class="style4" id="tab"  width="20%" onClick="AbrirPestana(1,'revisores_POA.aspx')">Revisión</td>
                <td class="style3" align="right" width="1%"></td>
                <td class="style4" id="tab1" width="20%" onClick="AbrirPestana(2,'comentarios_POA.aspx')">Comentarios</td>  
                <td width="38%" > 
                    <table >
                        <tr>
                            <td align="right">
                                <asp:Button ID="cmdResolucion" runat="server" CssClass="nresolucion" 
                                    Height="47px" Text="          Resolución" Width="110px" Enabled="False" 
                                    Visible="False" />
                            </td>
                            <td align="center" width="25%" style="width: 0%; color: #FF0000;">&nbsp;</td>
                            <td align="center" width="25%" style="width: 17%">
                                <asp:Button ID="cmdDerivar" runat="server" CssClass="enviarpropuesta" 
                                    Height="47px" Text="          Derivar" Width="90px" Enabled="False" 
                                    Visible="False" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="contornotabla" height="300" valign="top" colspan="6" align="left">
                    <iframe id="fradetalle" marginheight="0" marginwidth="0" frameborder="0" scrolling="auto" src="datospropuesta_POA.aspx" width="100%" height="300" ></iframe>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                   <table align="center" >
                        <tr>
                            <td >
                                <asp:GridView ID="dgv_Presupuesto" runat="server"  Width="100%" AutoGenerateColumns="False" Visible="False" HorizontalAlign="Center" ShowFooter="True" >
                                   <Columns>
                                        <asp:BoundField DataField="tipo" HeaderText="Tipo" >
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="actividad" HeaderText="Actividad" />
                                        <asp:BoundField DataField="item" HeaderText="Item" >
                                        <HeaderStyle Width="400px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" >
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="precio" HeaderText="Precio"  DataFormatString="{0:C2}" >
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="subtotal" HeaderText="SubTotal"  DataFormatString="{0:C2}" >
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" /> 
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />                                     
                                </asp:GridView>
                            </td>
                        </tr>
                   </table>
                </td>
            </tr>      
        </table>
    </form>
</body>
</html>
