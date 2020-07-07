<%@ Page Language="VB" AutoEventWireup="false" CodeFile="datospersonales.aspx.vb" Inherits="frmpersona" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <title>Registrar Persona</title>
	<link  href="private/expediente.css" rel="stylesheet" type="text/css"  />
    <link href="private/estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style6
        {
            font-size:11px;
            font-weight:normal;
         }
         .style7
        {
            font-size:12px;
            font-weight:bold;
         }
          
        .style9
        {
            width: 137px;
        }
        .style10
        {
            width: 145px;
        }
    </style>
  </head>
  <body>
	<table width="100%">
	  <tr><td  class="titulo_tabla">&nbsp;Datos Personales</td></tr>
	</table>
	<form id="frmPersona" runat="server"> 
    <table style="width: 100%" class="contornotabla" id="Table1" runat="server">                         
      <tr><td colspan="2"><span class="azul">&nbsp;Registra Datos Personales<br /></span></td></tr>
      <tr><td>&nbsp;</td></tr>
      <tr>
		<td class="style9">
		  <span class="titulo_items">Sexo</span>
		</td>
		<td>  
		  <asp:DropDownList ID="dpSexo" runat="server" SkinID="ComboObligatorio" 
		    ToolTip="Sexo" CssClass="datos_combo" Width="110px">
            <asp:ListItem Value="-1">&gt;&gt;Seleccione&lt;&lt;</asp:ListItem>
            <asp:ListItem Value="M">MASCULINO</asp:ListItem>
            <asp:ListItem Value="F">FEMENINO</asp:ListItem>
          </asp:DropDownList>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
		  <span class="titulo_items">Fecha Nacimiento</span>&nbsp;&nbsp;&nbsp;
		  <asp:DropDownList ID="dpdianac" runat="server" SkinID="ComboObligatorio" 
              ToolTip="Dia de Nacimiento" CssClass="datos_combo" Width="45px">
              <asp:ListItem Value="-1">Dia</asp:ListItem>
              <asp:ListItem Value="1">1</asp:ListItem>
              <asp:ListItem Value="2">2</asp:ListItem>
              <asp:ListItem Value="3">3</asp:ListItem>
              <asp:ListItem Value="4">4</asp:ListItem>
              <asp:ListItem Value="5">5</asp:ListItem>
              <asp:ListItem Value="6">6</asp:ListItem>
              <asp:ListItem Value="7">7</asp:ListItem>
              <asp:ListItem Value="8">8</asp:ListItem>
              <asp:ListItem Value="9">9</asp:ListItem>
              <asp:ListItem Value="10">10</asp:ListItem>
              <asp:ListItem Value="11">11</asp:ListItem>
              <asp:ListItem Value="12">12</asp:ListItem>
              <asp:ListItem Value="13">13</asp:ListItem>
              <asp:ListItem Value="14">14</asp:ListItem>
              <asp:ListItem Value="15">15</asp:ListItem>
              <asp:ListItem Value="16">16</asp:ListItem>
              <asp:ListItem Value="17">17</asp:ListItem>
              <asp:ListItem Value="18">18</asp:ListItem>
              <asp:ListItem Value="19">19</asp:ListItem>
              <asp:ListItem Value="20">20</asp:ListItem>
              <asp:ListItem Value="21">21</asp:ListItem>
              <asp:ListItem Value="22">22</asp:ListItem>
              <asp:ListItem Value="23">23</asp:ListItem>
              <asp:ListItem Value="24">24</asp:ListItem>
              <asp:ListItem Value="25">25</asp:ListItem>
              <asp:ListItem Value="26">26</asp:ListItem>
              <asp:ListItem Value="27">27</asp:ListItem>
              <asp:ListItem Value="28">28</asp:ListItem>
              <asp:ListItem Value="29">29</asp:ListItem>
              <asp:ListItem Value="30">30</asp:ListItem>
              <asp:ListItem Value="31">31</asp:ListItem>
            </asp:DropDownList>
		  <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="dpdianac"
          MaximumValue="31" MinimumValue="1" SetFocusOnError="True" Type="Integer" ErrorMessage="Seleccione dia de nacimiento">*</asp:RangeValidator>
            <asp:DropDownList ID="dpmesnac" runat="server" 
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
			<asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="dpmesnac" ErrorMessage="Seleccione mes de nacimiento" 
			  MaximumValue="12" MinimumValue="1" SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>
            <asp:DropDownList ID="DDlAnio" runat="server" 
              ToolTip="Año de Nacimiento" CssClass="datos_combo" Height="16px" Width="63px"></asp:DropDownList>
            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="DDlAnio"
              ErrorMessage="Seleccione Año de nacimiento" MaximumValue="2020" MinimumValue="1915"
              SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>
		  </td>
	  </tr>
	  <tr>
        <td class="style9">
          <span class="titulo_items">Nacionalidad</span>
        </td>
        <td>
            <asp:TextBox ID="txtPais" runat="server" Width="127px" CssClass="datos_combo"></asp:TextBox>
            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
            <span class="titulo_items">Estado Civil</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		    <asp:DropDownList ID="dpEstadoCivil" runat="server" SkinID="ComboObligatorio" Width="120px"
		      ToolTip="Estado Civil" CssClass="datos_combo">
              <asp:ListItem Value="-1">&gt;&gt;Seleccione&lt;&lt;</asp:ListItem>
              <asp:ListItem Value="SOLTERO">SOLTERO</asp:ListItem>
              <asp:ListItem Value="CASADO">CASADO</asp:ListItem>
              <asp:ListItem Value="VIUDO">VIUDO</asp:ListItem>
              <asp:ListItem Value="DIVORCIADO">DIVORCIADO</asp:ListItem>
            </asp:DropDownList>
		</td>
	  </tr>
	  <tr>
      <td class="style9">
	   <span class="titulo_items">Religión</span>
	 </td>
	 <td>
        <asp:DropDownList ID="dpReligion" runat="server" SkinID="ComboObligatorio" 
          ToolTip="Religion" CssClass="datos_combo" Width="110px">
          <asp:ListItem Value="-1">&gt;&gt;Seleccione&lt;&lt;</asp:ListItem>
          <asp:ListItem Value="CATOLICO">CATOLICO</asp:ListItem>
        <asp:ListItem Value="NO CATOLICO">NO CATOLICO</asp:ListItem>
        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		
        <span class="titulo_items">Último Sacramento</span>&nbsp;
		<asp:DropDownList ID="dpSacramento" runat="server" SkinID="ComboObligatorio" 
		  ToolTip="Ultimo Sacramento" CssClass="datos_combo" Width="125px">
		  <asp:ListItem Value="-1">&gt;&gt;Seleccione&lt;&lt;</asp:ListItem>
          <asp:ListItem Value="NINGUNO">NINGUNO</asp:ListItem>
          <asp:ListItem Value="BAUTISMO">BAUTISMO</asp:ListItem>
          <asp:ListItem Value="COMUNIÓN">COMUNIÓN</asp:ListItem>
          <asp:ListItem Value="CONFIRMACIÓN">CONFIRMACIÓN</asp:ListItem>
          <asp:ListItem Value="MATRIMONIO">MATRIMONIO</asp:ListItem>
          <asp:ListItem Value="ORDEN SACERDOTAL">ORDEN SACERDOTAL</asp:ListItem>
        </asp:DropDownList></td>
      </tr>
      <tr>
        <td class="style9"><span class="titulo_items">Tipo Documento</span></td>
        <td>
        <asp:DropDownList ID="dpTipoDocIdent" runat="server" SkinID="ComboObligatorio" 
          ToolTip="Tipo de Documento de Identidad" CssClass="datos_combo" Width="155px">
              <asp:ListItem Value="-1">&gt;&gt;Seleccione&lt;&lt;</asp:ListItem>
              <asp:ListItem Value="BOLETA MILITAR">BOLETA MILITAR</asp:ListItem>
              <asp:ListItem Value="CARNÉ DE EXTRANJERÍA">CARNÉ DE EXTRANJERÍA</asp:ListItem>
              <asp:ListItem Value="DNI">DNI</asp:ListItem>
              <asp:ListItem Value="PARTIDA DE NACIMIENTO">PARTIDA DE NACIMIENTO</asp:ListItem>
              <asp:ListItem Value="PASAPORTE">PASAPORTE</asp:ListItem>
              <asp:ListItem Value="RUC">RUC</asp:ListItem>
            </asp:DropDownList>&nbsp;Nº&nbsp;
            <asp:Label ID="lblDNI" runat="server" Text="Label" CssClass="style7"></asp:Label> 
        </td>
      </tr>
      <tr><td colspan="2"><hr /></td></tr>
      <tr>
        <td class="style9">
		  <span class="titulo_items">Lugar de Residencia</span>
		</td>
		<td>
            <asp:DropDownList ID="dpdepartamento" runat="server" AutoPostBack="True" Width="150px" CssClass="datos_combo"></asp:DropDownList>
            <asp:DropDownList ID="dpprovincia" runat="server" AutoPostBack="True" Width="150px" CssClass="datos_combo"></asp:DropDownList>
            <asp:DropDownList ID="dpdistrito" runat="server" AutoPostBack="True" Width="150px" CssClass="datos_combo"></asp:DropDownList>      
        </td>               
      </tr>
      <tr>
        <td class="style9">
		<span class="titulo_items">Dirección</span>
		</td>
		<td>
		<asp:TextBox ID="txtDir" runat="server" Width="500px" CssClass="datos_combo"></asp:TextBox>
        </td>         
      </tr>
      <tr>        
        <td class="style9">
		  <span class="titulo_items">Telefonos</span>
		</td>
		<td>
          <span><b>Fijo</b></span>&nbsp;<asp:TextBox ID="txtFijo" runat="server" Width="100px" CssClass="datos_combo"></asp:TextBox>&nbsp;
          <span><b>Celular 1</b></span>&nbsp;<asp:TextBox ID="txtCelular1" runat="server" Width="100px" CssClass="datos_combo"></asp:TextBox>&nbsp;
	      <span><b>Celular 2</b></span>&nbsp;<asp:TextBox ID="txtCelular2" runat="server" Width="100px" CssClass="datos_combo"></asp:TextBox>&nbsp;
	    </td>
      </tr>
	  <tr>
        <td class="style9">
          <span class="titulo_items">Email Personal</span>
        </td>
        <td>
            <asp:TextBox ID="txtEmailP" runat="server" Width="294px" CssClass="datos_combo"></asp:TextBox>
        </td>
	  </tr>
	  <tr>
        <td class="style9">
          <span class="titulo_items">Email Profesional</span>
        </td>
        <td>
            <asp:TextBox ID="txtEmailA" runat="server" Width="294px" CssClass="datos_combo"></asp:TextBox>
        </td>
      </tr>
 	  <tr>
        <td class="style9">       
          <span class="titulo_items">Fotografía</span>
        </td>
        <td>
		    <asp:FileUpload ID="fileFoto" runat="server" Width="379px" CssClass="datos_combo"/>
		    <asp:Image ID="Img" runat="server" ImageUrl="~/images/menus/prioridad_.gif" style="cursor: hand" /><span style="font-size: 8pt">(*.jpg. max 60 Kb)</span>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Tipo de archivo no permitido (*.jpeg, *.gif, *.png)" ControlToValidate="fileFoto"  ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF|.png|.PNG|.jpeg|.JPEG)$">
            </asp:RegularExpressionValidator>                            
        </td>
      </tr> 
    </table>
	<table style="width: 100%">
      <tr>
        <td align="right">
          <asp:Button ID="cmdGuardar" runat="server" Enabled="true" Text="Guardar" SkinID="BotonGuardar" />
        </td>
      </tr>        
    </table>
    <table style="width: 100%" class="contornotabla" id="tablaNombre" runat="server">                 
      <tr>
        <td colspan="2"><span class="azul">&nbsp;Visualizar Datos Personales<br /></span><br /><br />
        <asp:Image ID="FotoAlumno" runat="server" Height="107px" Width="89px" /><br></td>
      </tr>
	  <tr>
	    <td class="style10"><asp:HiddenField ID="hdcodigo_pso" runat="server" />
          <span class="style6"><b>Nombres y Apellidos</b></span>
        </td>
        <td>
          <asp:Label ID="lblNombres" runat="server" Text="Label" CssClass="style7"></asp:Label>&nbsp;
          <asp:Label ID="lblApellidoPat" runat="server" Text="Label" CssClass="style7"></asp:Label>&nbsp;
          <asp:Label ID="lblApellidoMat" runat="server" Text="Label" CssClass="style7"></asp:Label>&nbsp;&nbsp;
        </td>
	  </tr>
      <tr>
        <td class="style10">
		  <span class="style6"><b>Edad</b></span>
		</td>
		<td>
          <asp:Label ID="lblanionac" runat="server" Text="Label" CssClass="style7"></asp:Label>
        </td>
      </tr>
	  <tr>
	    <td class="style10">
		  <span class="style6"><b>Dirección</b></span>
		</td>
		<td>
		  <asp:Label ID="lblDir" runat="server" Text="Label" CssClass="style7"></asp:Label>
		</td>
	  </tr>
	  <tr>
	    <td class="style10">
		  <span class="style6"><b>Teléfono</b></span>
	    </td>
	    <td>
		  <asp:Label ID="LblFijo" runat="server" Text="Label" CssClass="style7"></asp:Label>
		</td>
	  </tr>
	  <tr>
	    <td class="style10">
		  <span class="style6"><b>E-mail</b></span>
		</td>
		<td>
		  <asp:Label ID="lblEmailP" runat="server" Text="Label" CssClass="style7"></asp:Label>
		</td>
	  </tr>
	  <tr>
	    <td class="style10">
		  <span class="style6"><b>Estado Civil</b></span>
		</td>
		<td>
		  <asp:Label ID="LblEstadoCivil" runat="server" Text="Label" CssClass="style7"></asp:Label>
		</td>
	  </tr>
	  <tr><td>&nbsp;</td></tr>
    </table>
    
    <asp:HiddenField ID="HdFileFoto" runat="server" />
    </form>
  </body>
</html>

