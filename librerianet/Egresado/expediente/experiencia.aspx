<%@ Page Language="VB" AutoEventWireup="false" CodeFile="experiencia.aspx.vb" Inherits="frmpersona" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <title>Experiencia Profesional</title>
	<link  href="private/expediente.css" rel="stylesheet" type="text/css"  />
    <link href="private/estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style6
        {
            font-size: 11px;
            font-weight: normal;
            width: 483px;
        }
         .style14
        {
            width: 483px;
        }
        .style15
        {
            width: 254px;
        }
        .style17
        {
            font-size: 11px;
            font-weight: normal;
            width: 254px;
        }
    </style>
  </head>
  <body>	
	  <form id="form1" runat="server">
	<table cellpadding="0" cellspacing="0" class="tabla_personal" style="width: 100%;">
	  <tr><td  class="titulo_tabla">&nbsp;Experiencia Laboral</td></tr>
	</table><br />
	<table style="width: 100%" class="contornotabla" id="Table1" runat="server">
      <tr><td class="style15">&nbsp;</td></tr>
      <tr>
        <td class="style17"><b>¿Cuentas con experiencia Laboral?</b></td>
        <td>                           
          <asp:RadioButtonList ID="experiencia" runat="server" Width="120px" 
                RepeatDirection="Horizontal" AutoPostBack="True" style="margin-left: 0px">
            <asp:ListItem Text="Si" Value="S"></asp:ListItem>
  		    <asp:ListItem Text="No" Value="N" Selected></asp:ListItem>
		  </asp:RadioButtonList>                                                   
        </td>
      </tr>
     </table><hr />
     <table style="width: 100%" class="contornotabla" id="Table2" runat="server">
      <tr><td colspan="2"><span class="azul">&nbsp;Registrar Experiencia laboral</span><br /></td></tr>
      <tr>
        <td class="style6">&nbsp;Fechas</td>
        <td>
          Inicio 
            &nbsp;&nbsp;
            <asp:DropDownList ID="dpMInicio" runat="server" 
              ToolTip="Mes de Nacimiento" CssClass="datos_combo" Width="90px">
              <asp:ListItem Value="-1">Mes</asp:ListItem>
              <asp:ListItem Value="1">Enero</asp:ListItem>
              <asp:ListItem Value="2">Febrero</asp:ListItem>
              <asp:ListItem Value="3">Marzo</asp:ListItem>
              <asp:ListItem Value="4">Abril</asp:ListItem>
              <asp:ListItem Value="5">Mayo</asp:ListItem>
              <asp:ListItem Value="6">Junio</asp:ListItem>
              <asp:ListItem Value="7">Julio</asp:ListItem>
              <asp:ListItem Value="8">Agosto</asp:ListItem>
              <asp:ListItem Value="9">Setiembre</asp:ListItem>
              <asp:ListItem Value="10">Octubre</asp:ListItem>
              <asp:ListItem Value="11">Noviembre</asp:ListItem>
              <asp:ListItem Value="12">Diciembre</asp:ListItem>
            </asp:DropDownList>
			<asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="dpMInicio" ErrorMessage="Seleccione mes de nacimiento" 
			  MaximumValue="12" MinimumValue="1" SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>
			  
            &nbsp;
            <asp:DropDownList ID="DDlAnioI" runat="server" 
              ToolTip="Año Inicio" CssClass="datos_combo" Height="16px" Width="63px"></asp:DropDownList>
            <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="DDlAnioI"
              ErrorMessage="Seleccione Año Inicio" MaximumValue="2020" MinimumValue="1915"
              SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>
            &nbsp;&nbsp;
          Fin
            <asp:DropDownList ID="dpMFin" runat="server" 
              ToolTip="Mes de Nacimiento" CssClass="datos_combo" Width="90px">
              <asp:ListItem Value="-1">En Curso</asp:ListItem>
              <asp:ListItem Value="1">Enero</asp:ListItem>
              <asp:ListItem Value="2">Febrero</asp:ListItem>
              <asp:ListItem Value="3">Marzo</asp:ListItem>
              <asp:ListItem Value="4">Abril</asp:ListItem>
              <asp:ListItem Value="5">Mayo</asp:ListItem>
              <asp:ListItem Value="6">Junio</asp:ListItem>
              <asp:ListItem Value="7">Julio</asp:ListItem>
              <asp:ListItem Value="8">Agosto</asp:ListItem>
              <asp:ListItem Value="9">Setiembre</asp:ListItem>
              <asp:ListItem Value="10">Octubre</asp:ListItem>
              <asp:ListItem Value="11">Noviembre</asp:ListItem>
              <asp:ListItem Value="12">Diciembre</asp:ListItem>
            </asp:DropDownList>
			<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="dpMFin" ErrorMessage="Seleccione mes de nacimiento" 
			  MaximumValue="12" MinimumValue="1" SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>
            
            <asp:DropDownList ID="DDlAnioF" runat="server" 
              ToolTip="Año Fin" CssClass="datos_combo" Height="16px" Width="75px"></asp:DropDownList>
            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="DDlAnioF"
              ErrorMessage="Seleccione Año Fin" MaximumValue="2020" MinimumValue="1915"
              SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>
        </td>
      </tr>
      <tr>
        <td class="style6">Institución/Empresa</td>
        <td>
            <asp:TextBox ID="txtinstitucion" runat="server" Width="502px" CssClass="datos_combo"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td class="style6">Ciudad</td>
        <td>
            <asp:TextBox ID="txtciudad" runat="server" Width="497px" CssClass="datos_combo"></asp:TextBox>
          </td>
      </tr>
      <tr>
        <td class="style6">Breve Descripción del Cargo desempeñado</td>
        <td>
           <asp:TextBox ID="txtdescripcion" runat="server" MaxLength="160" 
                TextMode="MultiLine" Width="600px" CssClass="datos_combo" 
             BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" 
             Height="55px"></asp:TextBox>
        </td>
      </tr>
      <tr>
         <td class="style14"></td>
         <td class="rojo">
         &nbsp; Max. 160 caracteres.</td>
       </tr>
      <tr>
        <td class="style6">Tipo de Sector</td>
        <td>
           &nbsp;<asp:TextBox ID="txtsector" runat="server" CssClass="datos_combo"></asp:TextBox>
          </td>
      </tr>
      <tr>
        <td class="style6">Área</td>
        <td>
           &nbsp;<asp:TextBox ID="txtarea" runat="server" CssClass="datos_combo"></asp:TextBox>
          </td>
      </tr>
      <tr><td class="style14">&nbsp;</td></tr>
	 </table>
	 <table style="width: 100%">
      <tr>
        <td colspan="2" align="center">
          <br /><br /><asp:Button ID="cmdGuardar" runat="server" Enabled="true" 
          Text="Guardar" SkinID="BotonGuardar" />   
        </td>
      </tr>
     </table>
  </form>
 </body>
 </html>
     