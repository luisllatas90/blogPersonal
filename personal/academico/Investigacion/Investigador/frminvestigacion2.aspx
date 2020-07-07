<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frminvestigacion2.aspx.vb" Inherits="Investigador_frminvestigacion2" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
 <head runat="server">
  <title>Investigaciones :: Nuevo</title>
  <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
  <script language="JavaScript" src="../../../../private/funciones.js"></script>
  <script language="JavaScript" src="../../../../private/calendario.js"></script>
  <script language="JavaScript" src="../../../../private/tooltip.js"></script>
  <script type="text/javascript">
   function OcultarTabla(){
    if (document.form1.FileProyecto.value!=""){
     document.all.tblDatos.style.display="none"
     document.all.tblmensaje.style.display=""                
    }
   }
  </script>
 </head>
 <body style="margin-top:0px">
  <form id="form1" runat="server">
   <table id="tblDatos" style="width: 100%"  cellpadding="0" cellspacing="0">
    <tr>
     <td align="center" style="height: 29px; border-right: dimgray 1px solid; border-top: dimgray 1px solid; border-left: dimgray 1px solid; width: 50%; background-color: #f0f0f0; font-weight: bold;  color: highlight;">
      INVESTIGACIONES INTERNAS</td>
     <td onclick="javascript:location.href='frminvestigacionotros.aspx?id=<% response.write(request.querystring("ID")) %>'" 
      align="center" 
     <% if request.querystring("codigo_inv")<>"" then response.write("style='height: 29px; visibility:hidden; border-bottom: dimgray 1px solid;cursor:hand'") else response.write("style='height: 29px; border-bottom: dimgray 1px solid;cursor:hand'") %> 
     >
     INV. FINALIZADAS</td>
    </tr>
    <tr>
     <td align="left" colspan="2" 
      style="height: 15px; background-color: #f0f0f0; border-right: dimgray 1px solid; border-left: dimgray 1px solid; font-family: verdana; font-size: 8pt;">
      <table style="width:100%;">
       <tr>
        <td>
         <asp:Button ID="CmdGuardar" runat="server" CssClass="guardar_prp" Text="             Guardar y Siguiente"
          Width="153px" ToolTip="Guardar y continuar a agregar participantes" />
        </td>
        <td style="font-family: verdana; font-size: 7pt; color: #FF0000; padding-left: 20pt; font-weight: bold;">
         Podrá registrar investigaciones que no cuentan con perfil definido</td>
       </tr>
      </table>
     </td>
    </tr>
    <tr>
     <td colspan="2" align="center">
      <table style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
       width: 100%; border-bottom: black 1px solid; padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px;" cellpadding="0" cellspacing="0">
       <tr>
        <td style="font-size: 8pt; color: black; font-family: verdana; background-color: #ffdb00">
         <strong>Nuevo Proyecto de Investigación<br /><br />
         </strong>En caso de no contar con un Perfil de investigación, registrar desde aquí ingresando los
          datos que se solicitan en cada campo del formulario; Cuando tenga todos los datos conformes, haga 
          click en <strong class="azul">Guardar y Siguiente </strong>para continuar.</td>
       </tr>
      </table>
      <table style="width: 100%; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;" cellpadding="5" cellspacing="0">
       <tr>
        <td colspan="2" style="font-weight: bold; font-size: 11pt; color: navy; font-family: verdana;
         height: 28px; text-align: center">
         <asp:Label ID="LblTitulo" runat="server"></asp:Label></td>
       </tr>
       <tr>
        <td style="width: 251px">Título de Investigación</td>
        <td><asp:TextBox ID="TxtTitulo" runat="server" Width="441px" 
             style="border-right: black 1px solid; border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: navy; border-bottom: black 1px solid; font-family: verdana" 
             MaxLength="500" ToolTip="Titulo de la Investigacion (max 500 Caracteres)."></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtTitulo"
             ErrorMessage="Ingrese titulo de investigacion" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
       </tr>
       <tr >
        <td>Unidad de Investigación</td>
        <td><asp:DropDownList ID="DDlUnidad" runat="server" style="font-size: 8pt; text-transform: capitalize; color: navy; font-family: verdana" Width="441px" AutoPostBack="True">
            </asp:DropDownList></td>
       </tr>
       <tr>
        <td style="width: 251px">Área de Investigación</td>
        <td><asp:DropDownList ID="DDLArea" runat="server" style="font-size: 8pt; text-transform: capitalize; color: navy; font-family: verdana" Width="441px" AutoPostBack="True">
            </asp:DropDownList></td>
       </tr>
       <tr>
        <td style="width: 251px">Línea o Temática</td>
        <td><asp:DropDownList ID="DDLTematica" runat="server" style="font-size: 8pt; text-transform: capitalize; color: navy; font-family: verdana" Width="441px">
            </asp:DropDownList><asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="DDLTematica"
            ErrorMessage="Seleccione Línea o Temática de Investigación" MaximumValue="8000" MinimumValue="1"
            SetFocusOnError="True" Type="Integer">*</asp:RangeValidator></td>
       </tr>
       <tr >
        <td style="width: 251px">Duración de Investigación</td>
        <td style="height: 27px; font-weight: bold;">
         <asp:TextBox ID="TxtDuracion" runat="server" Width="28px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ForeColor="Navy" style="text-align: right"></asp:TextBox>&nbsp;<asp:DropDownList ID="DDLDuracion" runat="server" style="font-size: 8pt; text-transform: capitalize; color: navy; font-family: verdana" Width="166px">
          <asp:ListItem Value="-1">-- Seleccione Duracion --</asp:ListItem>
          <asp:ListItem Value="M">Meses</asp:ListItem>
          <asp:ListItem Value="A">A&#241;os</asp:ListItem>
         </asp:DropDownList>
         <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="DDLDuracion"
          ErrorMessage="Seleccione Duracion" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
        </td>
       </tr>
       <tr>
        <td style="width: 251px">Proyecto de Investigación
        <img src="../../../../images/menus/prioridad_.gif" 
         tooltip="<b>Subir proyecto</b><br>Haga click en examinar y seleccione un archivo para subir,<br> recuerde que solo puede subir archivos con extension *.zip,<br> *.rar, *.pdf, *.doc no mayor a 5 MB." style="cursor: help"/></td>
        <td valign="top">
         <asp:FileUpload ID="FileProyecto" runat="server" Width="429px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ForeColor="Navy" /><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="FileProyecto"
          ErrorMessage="Solo puede subir archivos con extension *.zip, *.rar, *.pdf, *.doc"
          SetFocusOnError="True" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.rar|.RAR|.zip|.ZIP|.doc|.DOC|.pdf|.PDF)$">*</asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="FileProyecto"
          ErrorMessage="Debe seleccionar un archivo para subir como proyecto." SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
       </tr>
       <tr>
        <td style="width: 251px">Zona de Influencia</td>
        <td valign="top">
         <asp:DropDownList ID="DDLAmbito" runat="server" style="font-size: 8pt; text-transform: capitalize; color: navy; font-family: verdana">
          <asp:ListItem Value="-1">-- Ambito --</asp:ListItem>
          <asp:ListItem Value="I">Internacional</asp:ListItem>
          <asp:ListItem Value="N">Nacional</asp:ListItem>
          <asp:ListItem Value="R">Regional</asp:ListItem>
          <asp:ListItem Value="L">Local</asp:ListItem>
         </asp:DropDownList>
         <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="DDLAmbito"
          ErrorMessage="Seleccione Ambito" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
          <asp:DropDownList ID="DDLPoblacion" runat="server" style="font-size: 8pt; text-transform: capitalize; color: navy; font-family: verdana">
          <asp:ListItem Value="-1">-- Poblaci&#243;n --</asp:ListItem>
          <asp:ListItem Value="U">Urbana</asp:ListItem>
          <asp:ListItem Value="R">Rural</asp:ListItem>
          <asp:ListItem Value="A">Ambos</asp:ListItem>
         </asp:DropDownList>
         <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="DDLPoblacion"
          ErrorMessage="Seleccione Poblacion" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator><br />
          <br />Detalle
         <asp:TextBox ID="TxtDetalle" runat="server" MaxLength="300" Style="border-right: black 1px solid;
          border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: navy;
          border-bottom: black 1px solid; font-family: verdana" ToolTip="Pequeño detalle en zona de inflencia"
          Width="396px"></asp:TextBox><br />&nbsp;</td>
       </tr>
       <tr>
        <td align="left" style="width: 112px; height: 2px">&nbsp;Costo</td>
        <td align="left" style="width: 257px; height: 2px;">
         <asp:TextBox ID="TxtCosto" runat="server" BorderStyle="Solid" BorderWidth="1px"
          Width="56px"  style="text-align: right" Height="19px" Font-Names="verdana" Font-Size="9pt"></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtCosto"
          ErrorMessage="Ingrese costo de investigacion" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
       </tr>
       <tr>
        <td align="left" style="width: 112px; height: 10px" >&nbsp;Financiamiento</td>
        <td align="left" style="height: 10px;" >
         <asp:DropDownList ID="DDLFinanciamiento" runat="server" Width="358px">
          <asp:ListItem Value="N">--- Seleccione Tipo de Financiamiento ---</asp:ListItem>
          <asp:ListItem Value="A">Autofinanciado</asp:ListItem>
          <asp:ListItem Value="U">Financiado por Universidad</asp:ListItem>
          <asp:ListItem Value="E">Financiado por Entidad Externa</asp:ListItem>
          <asp:ListItem Value="C">Financiamiento Compartido</asp:ListItem>
         </asp:DropDownList><asp:CompareValidator ID="CompareValidator4" runat="server"
          ErrorMessage="Seleccione tipo de financiamiento" Operator="NotEqual" SetFocusOnError="True"
          ValueToCompare="N" ControlToValidate="DDLFinanciamiento">*</asp:CompareValidator></td>
       </tr>
       <tr>
        <td colspan="2"></td>
       </tr>
      </table>
     </td>
    </tr>
   </table>
   <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="100%" height="100%" class="contornotabla">
    <tr>
	 <td width="100%" align="center" class="usatTitulo" bgcolor="#FEFFE1">Procesando<Br>
	  Por favor espere un momento...</Br>
	  <img alt='imagen' border="0" src="../../../../images/cargando.gif" width="209" height="20">
	 </td>
	</tr>
	<%--<tr>
     <td style="width: 251px">Investigación no cuenta con Perfil</td>
     <td valign="top"><asp:FileUpload ID="FilePerfil" runat="server" Width="429px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ForeColor="Navy" />                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FilePerfil"
      ErrorMessage=""
      SetFocusOnError="True" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.rar|.RAR|.zip|.ZIP|.doc|.DOC|.pdf|.PDF)$"></asp:RegularExpressionValidator></td>
    </tr>--%>
   </table>
   <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
    ShowSummary="False" />
  </form>
 </body>
</html>
