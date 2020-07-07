<%@ Page Language="VB" AutoEventWireup="false" CodeFile="contenido.aspx.vb" Inherits="_Contenido" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Investigacion</title>
    <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript"  language="JavaScript" src="../../../../private/funciones.js"></script>
    <script type="text/javascript"  language="JavaScript" src="../../../../private/tooltip.js"></script>
    <script type="text/javascript" >
    
   function AbrirInvestigacion(pagina)
	{
	    var codigo_Inv= form1.txtelegido.value
		var Tipo= form1.txtTipo.value
		var Estado= form1.txtEstado.value
		var Menu= form1.txtMenu.value
		if (form1.txtelegido.value!="" || form1.txtelegido.value!=0)
			 fradetalle.location.href=pagina + "?codigo_Inv=" + codigo_Inv.substring(4,codigo_Inv.length) + "&Tipo=" + Tipo +"&Estado=" + Estado + "&Menu=" +Menu
	}

  function ActivarBotonModificar()
  	{
    	if (document.form1.CmdModificar!=undefined)	
    	document.form1.CmdModificar.disabled=false
	}
	
  function ActivarSubir()
	{ 	
	    if (document.form1.CmdAprobar!=undefined)
			document.form1.CmdAprobar.disabled=false;
			
		if (document.form1.CmdDesaprobar!=undefined)
			document.form1.CmdDesaprobar.disabled=false;
			
		if (document.form1.CmdObservar!=undefined)
			document.form1.CmdObservar.disabled=false;
				
	}

    function ModificarInvestigacion(Tipo, id)
    {	
        var codigo_Inv=document.form1.txtelegido.value
        codigo_Inv=codigo_Inv.substring(4,codigo_Inv.length)
        if (Tipo==1) {AbrirPopUp('frmInvestigacion1.aspx?id='+ id+ '&codigo_Inv='+codigo_Inv,'480','650') }
        if (Tipo==2) {AbrirPopUp('frmInvestigacion3.aspx?codigo_Inv='+codigo_Inv,'250','550') }
        if (Tipo==3) {AbrirPopUp('frmsubirarchivo.aspx?etapa_Inv=Informe&codigo_Inv='+codigo_Inv,'250','650') }
    }
    
    function Decreto()
    {
        var Codigo = document.form1.txtelegido.value;
        Codigo=Codigo.substring(4,Codigo.length);
        AbrirPopUp("frmsubirarchivo.aspx?modo=obs&codigo_Inv="+Codigo,'250','550')
    }
	
    function Observar()
    {
      etapa_Inv="Proyecto"
      var Codigo = document.form1.txtelegido.value
      Codigo=Codigo.substring(4,Codigo.length)
      AbrirPopUp("agregacomentario.aspx?modo=obs&codigo_Inv="+Codigo,'250','550')
    }
    </script>
    
</head>
<body style="margin:10px, 10px, 10px, 10px; background-color:#F0F0F0">
    <form id="form1" runat="server">
    <div style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px; background-color: #f0f0f0;">
        <table style="width: 100%; height: 620px;" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3" style="height: 5px;" align="center">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td align="left" colspan="4" style="height: 13px">
                                &nbsp;&nbsp;<asp:Button ID="CmdActualizar" runat="server" CssClass="enviaryrecibir1"
                                    Text="           Actualizar" ToolTip="Actualizar el listado de investigaciones"
                                    Width="95px" />&nbsp;
                                <asp:Button ID="CmdAprobar" runat="server" Text="          Aprobar" Width="86px" CssClass="conforme1" ToolTip="Aprobar una investigacion" />
                                <asp:Button ID="CmdDesaprobar" runat="server" Text="          Desaprobar" Width="110px" CssClass="noconforme1" ToolTip="Desaprobar una investigacion" />
                                <asp:Button ID="CmdObservar" runat="server" Text="           Observar" Width="96px" CssClass="editar1" ToolTip="Observar una investigación" /></td>
                        </tr>
                    </table>
                    </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 9px;" valign="top" align="center"><table style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" colspan="4" style="border-top: black 1px solid; height: 38px;" valign="middle">
                            &nbsp;<asp:Label ID="LblTitulo" runat="server" Font-Bold="True" Font-Size="X-Small"></asp:Label>&nbsp;</td>
                        <td align="right" colspan="1" style="border-top: black 1px solid; height: 38px" valign="middle">
                            <asp:TextBox ID="TxtBusqueda" runat="server" Width="251px"></asp:TextBox>
                            <asp:Button ID="CmdBuscar" runat="server" Text="       Buscar" Width="65px" CssClass="buscar1" ToolTip="Observar una investigación" /></td>
                    </tr>
                    <tr >
                        <td colspan="7" 
                            style="border-bottom: black 1px solid; height: 220px;
                            background-color: white; border-right: black 1px solid; border-left: black 1px solid; border-top: black 1px solid;" 
                            valign="top" align="center">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" DataKeyNames="codigo_Inv"
                                GridLines="None" Width="100%" AllowSorting="True" DataSourceID="Investigaciones">
                                <Columns>
                                    <asp:TemplateField HeaderText="N&#176;">
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="codigo_Inv" HeaderText="codigo_Inv" InsertVisible="False"
                                        ReadOnly="True" SortExpression="codigo_Inv" Visible="False" />
                                    <asp:BoundField DataField="titulo_Inv" HeaderText="Nombre de la Investigaci&#243;n"
                                        SortExpression="titulo_Inv">
                                        <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fechaInicio_Inv" DataFormatString="{0:dd-MM-yyyy}" HeaderText="FechaIngreso"
                                        HtmlEncode="False" SortExpression="fechaInicio_Inv">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion_Cco" 
                                        HeaderText="Departamento Presentado" SortExpression="descripcion_Cco">
                                        <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="codigo_Per" HeaderText="codigo_Per" SortExpression="codigo_Per"
                                        Visible="False" />
                                </Columns>
                                <RowStyle BorderStyle="None" Height="21px" />
                                <HeaderStyle BackColor="#F0F0F0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                    Height="20px" />
                                <PagerStyle BackColor="Silver" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <strong style="width: 100%; color: red; text-align: center">
                                        <br />
                                        <br />
                                        No existen registradas investigaciones en esta sección.</strong>
                                </EmptyDataTemplate>
                            </asp:GridView>
                            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
                        </td>
                    </tr>
                    </table>
                    &nbsp;<asp:Label ID="LblMensaje" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3">
                
                <table cellspacing="0" cellpadding="0" style="border-collapse: collapse; height: 100%;" bordercolor="#111111" width="100%">
      <tr>
        <td class="pestanaresaltada_1" id="tab" align="center" width="20%" onClick="ResaltarPestana_1('0','','');AbrirInvestigacion('datos_investigacion.aspx')" style="height: 32px" >Datos Generales</td>
        <td width="1%" class="bordeinf" style="height: 32px" >&nbsp;</td>
        <td class="pestanabloqueada" id="tab" align="center" width="20%" onClick="ResaltarPestana_1('1','','');AbrirInvestigacion('responsables_investigacion.aspx')" style="height: 32px">Responsables</td>
        <td width="1%" class="bordeinf" style="height: 32px" >&nbsp;</td>
		<td class="pestanabloqueada" id="tab" align="center" width="20%" onClick="ResaltarPestana_1('2','','');AbrirInvestigacion('comentarios_investigacion.aspx')" style="height: 32px">Comentarios</td>
        <td width="45%" class="bordeinf" style="height: 32px">&nbsp;</td>
      </tr>
      <tr>
        <td width="100%" valign="top" colspan="6" class="pestanarevez" style="height: 127%"><span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Seleccione una Investigaci&oacute;n para visualizar su detalle</span>
            <iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0" scrolling="yes" style="height: 90%"> </iframe>
            </td>
      </tr>
    </table>
                    </td>
            </tr>
        </table>
    
    </div>
                                <asp:HiddenField  ID="txtelegido" runat="server" />
    <asp:HiddenField ID="txtTipo" runat="server" />
    <asp:HiddenField ID="txtEstado" runat="server" />
    <asp:HiddenField ID="txtMenu" runat="server" />
        <asp:ObjectDataSource ID="Investigaciones" runat="server" SelectMethod="ConsultarInvPorEstado"
            TypeName="Investigacion">
            <SelectParameters>
                <asp:Parameter DefaultValue="5" Name="tipo" Type="String" />
                <asp:QueryStringParameter DefaultValue="" Name="codigo_per" QueryStringField="id"
                    Type="Int32" />
                <asp:QueryStringParameter Name="estado" QueryStringField="tipo" Type="Int32" />
                <asp:QueryStringParameter Name="etapa" QueryStringField="estado" Type="Int32" />
                <asp:ControlParameter ControlID="TxtBusqueda" DefaultValue="NO" Name="titulo" PropertyName="Text"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
