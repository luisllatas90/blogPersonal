<%@ Page Language="VB" AutoEventWireup="false" CodeFile="contenido.aspx.vb" Inherits="proponente_contenido" %>

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
  	   // alert(document.form1.txtelegido.value)
  	   
    	if (document.form1.cmdModificar!=undefined){
            document.form1.cmdModificar.disabled=false 
            if (document.form1.cmdConforme!=undefined){
                document.form1.cmdConforme.disabled=false 
                document.form1.cmdNoConforme.disabled=false 
                document.form1.cmdObservado.disabled=false 
                
                if (document.form1.cmdDerivar!=undefined){
                    document.form1.cmdDerivar.disabled=false
                }
            }
            
    	}
        if (document.form1.ddlInstanciaRevision.value!='P'){
        document.form1.cmdModificar.disabled=true;
        }
    	SeleccionarFila();
    	AbrirPestana(0,'datospropuesta.aspx')
	}
	
    function AbrirPestana(tab,pagina)
    {

        if (document.form1.txtelegido.value>0){
	        ResaltarPestana(tab,'','')
	        fradetalle.location.href=pagina + "?codigo_prp=" + document.form1.txtelegido.value + "&codigo_per=<%=request.querystring("id")%>"
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
        .style4
        {
            border: 1px solid #808080;
            background-color: #E1F1FB;
            cursor: hand;
            height: 31px;
        }
        .style5
        {
            height: 18px;
        }
        .style6
        {
            width: 100%;
        }
        .style7
        {
            width: 490px;
        }
        .style8
        {
            width: 56px;
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
                                  <asp:Button ID="cmdNuevo" runat="server" CssClass="nuevo1" Height="47px" 
                                    Text="        Nuevo" Width="76px" />                          
                                  <asp:Button ID="cmdModificar" runat="server" CssClass="modificar_1" Height="47px" 
                                    Text="        Modificar" Width="81px" Enabled="False" />                            
                    <asp:Button ID="cmdEnviar" runat="server" Text="        Enviar" 
                        CssClass="enviarpropuesta" Height="47px" Width="83px" />
                                  <asp:Button ID="cmdModificar0" runat="server" CssClass="ayuda_prp" Height="47px" 
                                    Text="          Ayuda" Width="79px" />                            
                                  <asp:HiddenField ID="txtelegido" runat="server" />
                                  <asp:Label ID="lblUsuario" runat="server" Visible="False"></asp:Label>
                                  <asp:Label ID="lblSecretaria" runat="server"></asp:Label>
                              </td>
                              <td style="font-weight: bold" class="style8"> Revisar propuestas como:</td>
                              <td width="15%" style="width: 20%"><asp:DropDownList ID="ddlInstanciaRevision" runat="server" AutoPostBack="True" 
                                                BackColor="#FFFF99">
                                  <asp:ListItem Value="P">Mis Propuestas</asp:ListItem>
                                  <asp:ListItem Value="D">Dirección de Area</asp:ListItem>
                                  <asp:ListItem Value="F">Consejo de Facultad</asp:ListItem>
                                  <asp:ListItem Value="K">Rectorado</asp:ListItem>
                                  <asp:ListItem Value="C">Consejo Universitario</asp:ListItem>
                                </asp:DropDownList>
                              </td>
                              <td width="15%"><asp:Label ID="lblEtiquetaInstancia" runat="server" 
                                                Text="Instancia en la que se encuentra la propuesta:"></asp:Label>
                                  <asp:Label ID="lblEtiquetaEstado" runat="server" Text="Estado de Revisi&oacute;n:"></asp:Label>
                              </td>
                              <td width="15%"><asp:DropDownList ID="ddlInstanciaPropuesta" runat="server" AutoPostBack="True">
                                  <asp:ListItem Value="P">Nuevas Propuestas</asp:ListItem>
                                  <asp:ListItem Value="D">Dirección de Área</asp:ListItem>
                                  <asp:ListItem Value="F">Consejo de Facultad</asp:ListItem>
                                  <asp:ListItem Value="K">Rectorado</asp:ListItem>
                                  <asp:ListItem Value="C">Consejo Universitario</asp:ListItem>
                                </asp:DropDownList>
                                  <asp:DropDownList ID="ddlEstadoRevision" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="P">Pendiente</asp:ListItem>
                                    <asp:ListItem Value="C">Conformes</asp:ListItem>
                                    <asp:ListItem Value="O">Observadas</asp:ListItem>
                                    <asp:ListItem Value="N">No Conformes</asp:ListItem>
                                  </asp:DropDownList>
                              </td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                            <td class="style1">
                                </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                    SelectCommand="PRP_ConsultarPropuesta" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlInstanciaRevision" DefaultValue="P" 
                                            Name="tipo" PropertyName="SelectedValue" Type="String" />
                                        <asp:QueryStringParameter DefaultValue="" Name="codigo_per" 
                                            QueryStringField="id" Type="Int32" />
                                        <asp:Parameter DefaultValue="P" Name="instanciaInvolucrado" Type="String" />
                                        <asp:ControlParameter ControlID="ddlInstanciaPropuesta" DefaultValue="" 
                                            Name="instanciaPropuesta" PropertyName="SelectedValue" Type="String" />
                                        <asp:ControlParameter ControlID="ddlEstadoRevision" DefaultValue="P" 
                                            Name="estadoPrp" PropertyName="SelectedValue" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="contornotabla" height="250" valign="top" colspan="6">
                    <asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Vertical">
                        <asp:GridView ID="dgvPropuestas" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="codigo_Prp" 
                            DataSourceID="SqlDataSource1" 
    GridLines="Horizontal" PageSize="7" 
                            Width="100%">
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
                                    SortExpression="Proponente" Visible="False" />
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
                <td class="style2" id="tab" width="20%" onClick="AbrirPestana(0,'datospropuesta.aspx')" >
                    Datos Generales
                </td>
                <td width="1%" class="style3"></td>
		        <td class="style4" id="tab" width="20%" onClick="AbrirPestana(1,'revisores.aspx')">
                    Revisión
                        </td>
                <td class="style3" align="right" width="1%"></td>
                <td class="style4" id="tab" width="20%" onClick="AbrirPestana(2,'comentarios.aspx')">
                    Observaciones
                </td>  
                <td width="38%" class="style3"> 
                    <table class="style6">
                        <tr>
                            <td align="center" width="25%">
                                <asp:Button ID="cmdConforme" runat="server" CssClass="conforme1" Height="47px" 
                                    Text="          Conforme" Width="92px" Enabled="False" />
                                </td>
                            <td align="center" width="25%">
                                <asp:Button ID="cmdObservado" runat="server" CssClass="nuevocomentario" Height="47px" 
                                    Text="           Observado" Width="98px" Enabled="False" />
                                </td>
                            <td align="center" width="25%" style="width: 0%">
                                <asp:Button ID="cmdNoConforme" runat="server" CssClass="noconforme1" Height="47px" 
                                    Text="           No Conforme" Width="110px" Enabled="False" />
                                </td>
                            <td align="center" width="25%" style="width: 17%">
                                <asp:Button ID="cmdDerivar" runat="server" CssClass="enviarpropuesta" Height="47px" 
                                    Text="          Derivar" Width="90px" Enabled="False" />
                                </td>
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
