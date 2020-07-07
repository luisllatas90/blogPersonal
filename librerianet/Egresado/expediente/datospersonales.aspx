<%@ Page Language="VB" AutoEventWireup="false" CodeFile="datospersonales.aspx.vb" Inherits="frmpersona" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <title>Registrar Persona</title>
	<link  href="private/expediente.css?x=8" rel="stylesheet" type="text/css"  />
    <link href="private/estilo.css" rel="stylesheet" type="text/css" />
    <script src="../jquery/fancy/jquery.js" type="text/javascript"></script>
    <script src="../jquery/fancy/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <link href="../jquery/fancy/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" media="screen" />
<script>
  
   // function CallFancyBox_DeclaracionJurada() {
    $(".CallFancyBox_DeclaracionJurada").live("click", function() {
            $.fancybox({
            'width': '80%',
                'height': '300px',
                'autoScale': true,
                'type': 'inline',
                'href': '#divdj'
            });
            return false;   
          });
          // }
          function acepta() {
             frmPersona.submit();
          }
</script>
    <style type="text/css">
        body{overflow:hidden;}
         .style7
        {
            font-size:12px;
            font-weight:bold;
            color:#002B56;
         }
          
        .style9
        {
            width: 137px;
        }
    .content
    {
        padding:2px;
        border:1px solid #e33439;
        background-color:#FEFFE1;
       font-family:Verdana;
      font-size:10px;
        
    }
    .head_titulo
    { 
        font-weight:bold; background-color:#e33439; color:White;
        padding:4px;
        
    }
   
    </style>
  </head>
  <body>
	<table cellpadding="0" cellspacing="0" class="" style="width: 100%;">
	  <tr><td style="color:White; background:#e33439; font-size:14px;padding:3px;">&nbsp;Datos Personales</td></tr>
	</table><br/>
	
	<form id="frmPersona" runat="server"> 
	
    <table style="width: 100%" class="" id="Table1" runat="server">                         
      <tr><td colspan="3">
        <span class="azul">
        <asp:Label ID="lblNombres" runat="server" Text="Label" CssClass="style7"></asp:Label>&nbsp;
          <asp:Label ID="lblApellidoPat" runat="server" Text="Label" CssClass="style7"></asp:Label>&nbsp;
          <asp:Label ID="lblApellidoMat" runat="server" Text="Label" CssClass="style7"></asp:Label>&nbsp;&nbsp;
        </span></td>
      </tr>
     
      <tr>
		<td width="136" class="style9">
		  <span class="titulo_items">Sexo</span>		</td>
		<td width="830">  
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
              SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>		  </td>
	    <td width="110" rowspan="4"><asp:Image ID="FotoAlumno" runat="server" 
                Height="100px" Width="90px" BorderColor="Black" BorderWidth="1px" /></td>
      </tr>
	  <tr>
        <td class="style9">
          <span class="titulo_items">Nacionalidad</span>        </td>
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
            </asp:DropDownList>		</td>
      </tr>
	  <tr>
      <td class="style9">
	   <span class="titulo_items">Religión</span>	 </td>
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
            <asp:Label ID="lblDNI" runat="server" Text="Label" CssClass="style7"></asp:Label>        </td>
      </tr>
      <tr><td colspan="3"><hr /></td></tr>
      <tr>
        <td class="style9"><span class="titulo_items">Lugar de Residencia</span></td>
		<td colspan="2">
            <asp:DropDownList ID="dpdepartamento" runat="server" AutoPostBack="True" Width="150px" ToolTip="Departamento" CssClass="datos_combo"></asp:DropDownList>
            <asp:DropDownList ID="dpprovincia" runat="server" AutoPostBack="True" Width="150px" ToolTip="Provincia" CssClass="datos_combo"></asp:DropDownList>
            <asp:DropDownList ID="dpdistrito" runat="server" AutoPostBack="True" Width="150px" ToolTip="Distrito" CssClass="datos_combo"></asp:DropDownList>        </td>               
      </tr>
      <tr>
        <td class="style9"><span class="titulo_items">Dirección</span></td>
		<td colspan="2">
		  <asp:TextBox ID="txtDir" runat="server" Width="500px" CssClass="datos_combo"></asp:TextBox>
		</td>
      </tr>
      <tr>        
        <td class="style9">
		  <span class="titulo_items">Telefonos</span>		</td>
		<td colspan="2">
          <span><b>Fijo</b></span>&nbsp;<asp:TextBox ID="txtFijo" runat="server" Width="100px" CssClass="datos_combo"></asp:TextBox>&nbsp;
          <span><b>Celular 1</b></span>&nbsp;<asp:TextBox ID="txtCelular1" runat="server" Width="100px" CssClass="datos_combo"></asp:TextBox>&nbsp;
	      <span><b>Celular 2</b></span>&nbsp;<asp:TextBox ID="txtCelular2" runat="server" Width="100px" CssClass="datos_combo"></asp:TextBox>&nbsp;	    </td>
      </tr>
	  <tr>
        <td class="style9">
          <span class="titulo_items">Email Personal</span>        </td>
        <td colspan="2">
            <asp:TextBox ID="txtEmailP" runat="server" Width="294px" CssClass="datos_combo"></asp:TextBox>        </td>
	  </tr>
	  <tr>
        <td class="style9">
          <span class="titulo_items">Email Profesional</span>        </td>
        <td colspan="2">
            <asp:TextBox ID="txtEmailA" runat="server" Width="294px" CssClass="datos_combo"></asp:TextBox>        </td>
      </tr>
 	  <tr>
        <td class="style9">       
          <span class="titulo_items">Fotografía</span>        </td>
        <td colspan="2">
		    <asp:FileUpload ID="fileFoto" runat="server" Width="379px" CssClass="datos_combo"/>
		    <asp:Image ID="Img" runat="server" ImageUrl="~/images/menus/prioridad_.gif" style="cursor: hand" /><span style="font-size: 8pt">(*.jpg. max 60 Kb)</span>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Tipo de archivo no permitido (*.jpeg, *.gif, *.png)" ControlToValidate="fileFoto"  ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF|.png|.PNG|.jpeg|.JPEG)$">            </asp:RegularExpressionValidator>        </td>
      </tr>
      <tr>
        <td colspan="2">
          <asp:Label ID="lblMensajeFrm" runat="server" ForeColor="#CC0000" style="color: #ff0000; font-style: italic">
          </asp:Label>
        </td>
      </tr> 
    </table>
	<table style="width: 100%">
      <tr>
        <td align="right">
          <asp:Button ID="cmdGuardar" runat="server" 
                CssClass="guardar  CallFancyBox_DeclaracionJurada" Enabled="true" 
                Text="Continuar" SkinID="BotonGuardar" />
          
        </td>
      </tr>      
    </table>    
    <asp:HiddenField ID="hdcodigo_pso" runat="server" />
    <asp:HiddenField ID="HdFileFoto" runat="server" />  
       
<input type="hidden" value="0" id="aceptaDj" name="aceptaDj" runat="server"/>
<div style="display: none;">
    <div id="divdj" runat="server">
        <div class="content">       
           <br />
           <span class="head_titulo">DECLARACIÓN JURADA</span>
            <br />
           <br />
           <span class="head_name">YO <b><asp:Label ID="lblnombre" runat="server" Text="Label"></asp:Label></b>&nbsp;declaro que:</span>
            <br />
           <br />
           <span class="content_item">
           <b>1.-</b> Los datos que se consignan a continuación tienen  carácter de <b>DECLARACION JURADA</b>.<br /> 
           Solo el egresado es personalmente responsable de la veracidad de los mismos.      
           <br />
           <br />
           <b>2.-</b> La USAT facilitará el envío de la información consignada en el CV a las empresas que soliciten egresados USAT 
            <br />
           <br />
           <b>3.-</b>La información consignada en el CV deberá ser actualizada por el egresado, cada vez que ingrese al módulo <b>Bolsa de trabajo - alumniUSAT</b>.
              <br />
           <br />              
           </span>         
           <input type="button" class="guardar" value="Acepto" onclick="acepta();" />  
           <input type="button" class="guardar" value="No Acepto" onclick="javascript:jQuery.fancybox.close();" />          
        </div>
    </div>
</div>                   
    </form>
  </body>
</html>


