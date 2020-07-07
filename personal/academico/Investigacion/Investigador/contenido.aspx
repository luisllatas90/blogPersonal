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
	    if (document.form1.CmdAvance!=undefined)
			document.form1.CmdAvance.disabled=false;
			
		if (document.form1.CmdProyecto!=undefined)
			document.form1.CmdProyecto.disabled=false;
			
		if (document.form1.CmdAvances!=undefined)
			document.form1.CmdAvances.disabled=false;
		
		if (document.form1.CmdInforme!=undefined)
			document.form1.CmdInforme.disabled=false;

		if (document.form1.CmdResumen!=undefined)
			document.form1.CmdResumen.disabled=false;
	}

    function ModificarInvestigacion(Tipo, id)
    {	
        var codigo_Inv=document.form1.txtelegido.value
        codigo_Inv=codigo_Inv.substring(4,codigo_Inv.length)
        if (Tipo==1) {AbrirPopUp('frmInvestigacion2.aspx?id='+ id + '&codigo_Inv='+codigo_Inv,'480','650') }
        //if (Tipo==1) {AbrirPopUp('frmInvestigacion1.aspx?id='+ id + '&codigo_Inv='+codigo_Inv,'480','650') }
        if (Tipo==2) {AbrirPopUp('frmInvestigacion3.aspx?id='+ id + '&codigo_Inv='+codigo_Inv,'250','550') }
        if (Tipo==3) {AbrirPopUp('frmsubirarchivo.aspx?etapa_Inv=Informe&codigo_Inv='+codigo_Inv,'205','510') }
    }
	
    function SubirProyecto(Estado1,Estado2,Tipo,Instancia,Menu,Nombre)
    {
        etapa_Inv="Proyecto"
        var Codigo=document.form1.txtelegido.value
        Codigo=Codigo.substring(4,Codigo.length)
        AbrirPopUp("frminvestigacion3.aspx?id="+ <%response.write(request.querystring("ID")) %> +"&etapa_Inv="+etapa_Inv+"&codigo_Inv="+Codigo+"&Estado1="+Estado1+"&accion="+etapa_Inv+"&menu="+Menu+"&nombre="+Nombre+"&instancia="+Instancia+"&Tipo="+Tipo+"&Estado2="+Estado2,'250','550')
    }	
    
    function SubirAvances()
    {
        etapa_Inv="Avance"
        var Codigo=document.form1.txtelegido.value
        Codigo=Codigo.substring(4,Codigo.length)
        //Codigo=Codigo.substring(2,0)
        AbrirPopUp("frmsubirarchivo.aspx?id=" + <% response.write(request.Querystring("id")) %> + "&etapa_Inv="+etapa_Inv+"&codigo_Inv="+Codigo,'250','550')
    }	
    
    function SubirInforme()
    {
        etapa_Inv="Informe"
        var Codigo=document.form1.txtelegido.value
        Codigo=Codigo.substring(4,Codigo.length)
        //Codigo=Codigo.substring(2,0)
        AbrirPopUp("frmsubirarchivo.aspx?id=" + <% response.write(request.Querystring("id")) %> + "&etapa_Inv="+etapa_Inv+"&codigo_Inv="+Codigo,'250','550')
    }	
    
    function SubirResumen()
    {   etapa_Inv="Resumen"
        var Codigo=document.form1.txtelegido.value
        Codigo=Codigo.substring(4,Codigo.length)
        //Codigo=Codigo.substring(2,0)
        AbrirPopUp("frmsubirarchivo.aspx?id=" + <% response.write(request.Querystring("id")) %> +"&etapa_Inv="+etapa_Inv+"&codigo_Inv="+Codigo,'250','550')
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
       <td align="left" style="height: 13px">
        <asp:Button ID="CmdNuevo" runat="server" Text="           Nuevo" Width="68px" CssClass="nuevo1" ToolTip="Registrar una nueva Investigación" />
        <asp:Button ID="CmdNuevo_v1" runat="server" Text="           Nuevo" Width="68px" CssClass="nuevo1" ToolTip="Registrar un nuevo Proyecto de Investigación" />
        <asp:Button ID="CmdNuevo_v2" runat="server" Text="           Nuevo" Width="68px" CssClass="nuevo1" ToolTip="Registrar un nuevo Informe de Investigación" />
        <asp:Button ID="CmdActualizar" runat="server" Text="           Actualizar" Width="95px" CssClass="enviaryrecibir1" ToolTip="Actualizar el listado de investigaciones" />
        <asp:Button ID="CmdModificar" runat="server" Text="           Modificar" Width="84px" CssClass="modificar_1" ToolTip="Modificar una investigación observada" />
        <asp:Button ID="CmdProyecto" runat="server" Text="           Subir Proyecto" Width="118px" CssClass="conforme1" ToolTip="Subir un Proyecto de Investigación" />
        <asp:Button ID="CmdAvance" runat="server" Text="           Subir Avance" Width="112px" CssClass="conforme1" ToolTip="Subir un Avance de Investigación" />
        <asp:Button ID="CmdAvances" runat="server" Text="           Subir Avances" Width="112px" CssClass="conforme1" ToolTip="Subir Avances de Investigación" />
        <asp:Button ID="CmdInforme" runat="server" Text="           Subir Informe" Width="113px" CssClass="conforme1" ToolTip="Subir un informe de investigación" />
        <asp:Button ID="CmdResumen" runat="server" Text="           Subir Resumen" Width="117px" CssClass="conforme1" ToolTip="Subir resumen de investigación" /></td>
        <td align="right" style="height: 13px">
        <img alt="" src="../../../../images/19.gif" style="" /><asp:LinkButton 
        ID="LinkFormato0" runat="server" Font-Underline="True" ForeColor="#000066" 
        onclientclick="AbrirPopUp2('../DirectorInvestigacion/convocatorias/Manual_investigador.pdf',600,700,1,1)">Manual 
        del Investigador</asp:LinkButton>
        <img alt="" src="../../../../images/19.gif" style="" /><asp:LinkButton 
         ID="LinkFormato" runat="server" Font-Underline="True" ForeColor="#000066" 
         onclientclick="AbrirPopUp2('../DirectorInvestigacion/convocatorias/Formato_Investigacion.pdf',600,700,1,1)">Formatos </asp:LinkButton>
       </td>
      </tr>
     </table>
    </td>
   </tr>
   <tr>
    <td style="width: 100%; height: 9px;" valign="top"><table style="width: 100%" cellpadding="0" cellspacing="0">
   <tr>
    <td align="left" colspan="4" style="border-top: black 1px solid; height: 38px; width: 100%;" valign="middle">
    &nbsp;<asp:Label ID="LblTitulo" runat="server" Font-Bold="True" Font-Size="X-Small"></asp:Label>&nbsp;</td>
   </tr>
   <tr >
       <td colspan="6" style="border-bottom: black 1px solid; height: 220px;
        background-color: white; border-right: black 1px solid; border-left: black 1px solid; border-top: black 1px solid;" 
        valign="top" align="center">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True"
         AutoGenerateColumns="False" DataKeyNames="codigo_Inv"
         GridLines="None" Width="100%" AllowSorting="True" DataSourceID="Investigaciones">
        <Columns>
         <asp:TemplateField HeaderText="N&#176;">
          <ItemStyle HorizontalAlign="Center" Width="30px" ForeColor="Black" />
          <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
         </asp:TemplateField>
         <asp:BoundField DataField="codigo_Inv" HeaderText="codigo_Inv" 
          InsertVisible="False" ReadOnly="True" SortExpression="codigo_Inv" Visible="False" />
         <asp:BoundField DataField="titulo_Inv" HeaderText="Nombre de la Investigaci&#243;n"
          SortExpression="titulo_Inv">
          <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
         </asp:BoundField>
         <asp:BoundField DataField="fechaInicio_Inv" DataFormatString="{0:dd-MM-yyyy}" 
         HeaderText="Fecha Ingreso" HtmlEncode="False" SortExpression="fechaInicio_Inv">
          <ItemStyle HorizontalAlign="Center" Width="100px" />
          <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
         </asp:BoundField>
         <asp:BoundField DataField="descripcion_Cco" 
          HeaderText="Departamento Presentado" SortExpression="descripcion_Cco">
          <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
         </asp:BoundField>
         <asp:BoundField DataField="codigo_Per" HeaderText="codigo_Per" 
          SortExpression="codigo_Per" Visible="False" />
         </Columns>
         <RowStyle BorderStyle="None" Height="20px" />
         <HeaderStyle BackColor="#F0F0F0" BorderColor="Black" BorderStyle="Solid" 
          BorderWidth="1px" Height="20px" />
         <PagerStyle BackColor="Silver" HorizontalAlign="Center" />
         <EmptyDataTemplate>
          <strong style="width: 100%; color: red; text-align: center">
          <br /><br />No tiene registrada una investigación, haga click en nuevo para ingresar una.</strong>
         </EmptyDataTemplate>
         </asp:GridView>&nbsp; &nbsp; &nbsp;
        </td>
       </tr>
      </table>&nbsp;</td>
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
    <asp:ObjectDataSource ID="Investigaciones" runat="server" SelectMethod="ConsultarInvPorEstado" TypeName="Investigacion">
    <SelectParameters>
     <asp:Parameter DefaultValue="1" Name="tipo" Type="String" />
     <asp:QueryStringParameter DefaultValue="" Name="codigo_per" QueryStringField="id" Type="Int32" />
     <asp:QueryStringParameter Name="estado" QueryStringField="tipo" Type="Int32" />
     <asp:QueryStringParameter Name="etapa" QueryStringField="estado" Type="Int32" />
     <asp:Parameter DefaultValue="NO" Name="titulo" Type="String" />
    </SelectParameters>
   </asp:ObjectDataSource>
  </form>
 </body>
</html>