<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmRegistraIdioma.aspx.vb" Inherits="FrmRegistraIdioma" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <title>Idiomas</title>
      <link href="private/estilo.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../jquery/jquery-ui.css" />
    <script src="../jquery/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../jquery/jquery-ui.js" type="text/javascript"></script>
    <script src="../jquery/centros.js" type="text/javascript"></script>

    <link  href="private/expediente.css" rel="stylesheet" type="text/css"  />
    <link href="private/estilo.css" rel="stylesheet" type="text/css" />
        
    
    <link  href="private/expediente.css" rel="stylesheet" type="text/css"  />
    <link href="private/estilo.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        body {
            color: #2F4F4F;
            font-family: "Trebuchet MS","Lucida Console",Arial,san-serif;
            }      
        .style18 {
            font-weight: normal;
            width: 147px;
            }
        .guardar {
            border:1px solid #C0C0C0; background:#FEFFE1 ; width:80; font-family:Tahoma; font-size:8pt; font-weight:bold; height:25
            }
        a { text-decoration:none; color:Black;}
        form {
            margin:0px;
            }
         #txtdescripcion {
            width: 483px;
            }
                .content
            {
                padding:5px;
                border:1px solid #e33439;
                background-color:#FEFFE1;
                font-family:Verdana;
                font-size:12px;        
            }
        .head_titulo
        { 
            font-weight:bold; background-color:#e33439; color:White;
            padding:4px;        
        }   
    </style>
  </head>
  <body>	
	<form id="form1" runat="server" style="">	      
		  <asp:Panel ID="PanelDJ" runat="server">
        <div id="divdj" runat="server">
        <div class="content">       
           <br />
           <span class="head_titulo">DECLARACIÓN JURADA</span>
            <br />
            <br />
           <br />
           <span class="">YO <b><asp:Label ID="lblnombre" runat="server" Text="Label"></asp:Label></b>&nbsp;declaro que:</span>
            <br />
           <br />
           <span class="">
           <b>1.-</b> Los datos que se consignan a continuación tienen  carácter de <b>DECLARACION JURADA</b>.<br /> 
           Solo el egresado es personalmente responsable de la veracidad de los mismos.      
           <br />
           <br />
            <br />
           <b>2.-</b> La USAT facilitará el envío de la información consignada en el CV a las empresas que soliciten egresados USAT 
            <br />
            <br />
           <br />
           <b>3.-</b>La información consignada en el CV deberá ser actualizada por el egresado, cada vez que ingrese al módulo <b>Bolsa de trabajo - alumniUSAT</b>.
              <br />
            <br />
           <br />              
               <asp:Button ID="btnAcepta" runat="server"  CssClass="guardar" Text="Acepto" />
               &nbsp;
               <asp:Button ID="btnNoAcepta" runat="server"  CssClass="guardar" Text="No Acepto" />
           </span>         
        
        </div></div>
      </asp:Panel>
                       
	  <asp:Panel ID="PanelRegistro" runat="server">  
    <table style="width: 100%" class="" id="" cellpadding="2">      
      <tr><td style="color:White; background:#e33439;padding:3px;" colspan=2>
          &nbsp;Registrar/Actualizar Idioma Extranjero</td></tr>          
      <tr>
        <td class="style18">Año</td>
        <td >Ingreso:&nbsp;
          <asp:DropDownList ID="dpAnioIngreso" runat="server" CssClass="datos_combo"
            ToolTip="Año de Ingreso" Width="90px">
          </asp:DropDownList>&nbsp;&nbsp;
          <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Checked="True" Text="Hasta la actualidad" />
        </td>
      </tr>
      <tr>
        <td class="style18">&nbsp;</td>
        <td >
          <asp:Label ID="Label1" runat="server" Text="Egreso:  " Visible="False"></asp:Label>&nbsp;&nbsp;
          <asp:DropDownList ID="dpAnioEgreso" runat="server" CssClass="datos_combo" 
            ToolTip="Año de Egreso" Visible="False" Width="90px">
          </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td class="style18">Idioma</td>
        <td >
          <asp:DropDownList ID="dpidioma" runat="server" SkinID="ComboObligatorio" 
		    ToolTip="Situación" CssClass="datos_combo" Width="110px">
            <asp:ListItem Value="-1">--Seleccione--</asp:ListItem>
            <asp:ListItem Value="CULMINADO">CULMINADO</asp:ListItem>
            <asp:ListItem Value="EN PROCESO">EN PROCESO</asp:ListItem>
          </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td class="style18">Tipo de Institución</td>
        <td >
          <asp:DropDownList ID="dptipoCE" runat="server" CssClass="datos_combo" 
                ToolTip="Tipo Institución" AutoPostBack="True"></asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td class="style18">Centro de Estudios</td>
        <td >
          <%--<asp:TextBox ID="txtcentroestudios" runat="server" Width="483px" CssClass="datos_combo"></asp:TextBox>--%>
            <asp:TextBox ID="txtcentro" runat="server" Width="362px" CssClass="datos_combo"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td class="style18">Nivel</td>
        <td >Lectura 
          <asp:DropDownList ID="dpNivel1" runat="server" SkinID="ComboObligatorio" 
		    ToolTip="Nivel de Lectura" CssClass="datos_combo" Width="90px" AutoPostBack="True" 
                Height="16px">
            <asp:ListItem Value="-1">--</asp:ListItem>
            <asp:ListItem Value="0">Básico</asp:ListItem>
            <asp:ListItem Value="1">Intermedio</asp:ListItem>
            <asp:ListItem Value="2">Avanzado</asp:ListItem>
          </asp:DropDownList>
          &nbsp;&nbsp;&nbsp;&nbsp;Escritura
          <asp:DropDownList ID="dpNivel2" runat="server" SkinID="ComboObligatorio" 
		    ToolTip="Nivel de Escritura" CssClass="datos_combo" Width="90px" AutoPostBack="True">
            <asp:ListItem Value="-1">--</asp:ListItem>
            <asp:ListItem Value="0">Básico</asp:ListItem>
            <asp:ListItem Value="1">Intermedio</asp:ListItem>
            <asp:ListItem Value="2">Avanzado</asp:ListItem>
          </asp:DropDownList>
          &nbsp;&nbsp;&nbsp;&nbsp;Habla
          <asp:DropDownList ID="dpNivel3" runat="server" SkinID="ComboObligatorio" 
		    ToolTip="Nivel de Habla" CssClass="datos_combo" Width="90px" AutoPostBack="True">
            <asp:ListItem Value="-1">--</asp:ListItem>
            <asp:ListItem Value="0">Básico</asp:ListItem>
            <asp:ListItem Value="1">Intermedio</asp:ListItem>
            <asp:ListItem Value="2">Avanzado</asp:ListItem>
          </asp:DropDownList>
        </td>
      </tr>
      <%--<tr>
        <td class="style18">Situación</td>
        <td colspan="2">
          <asp:DropDownList ID="dpSituacion" runat="server" SkinID="ComboObligatorio" 
		    ToolTip="Situación" CssClass="datos_combo" Width="110px" AutoPostBack="True">
            <asp:ListItem Value="-1">--Seleccione--</asp:ListItem>
            <asp:ListItem Value="CULMINADO">CULMINADO</asp:ListItem>
            <asp:ListItem Value="EN PROCESO">EN PROCESO</asp:ListItem>
          </asp:DropDownList>
        </td>
      </tr>--%>
      <tr>
        <td class="style18">
          <asp:HiddenField ID="codigo_FA" runat="server" Value="0" />       
        </td>
        <td ><br />
          <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar" Enabled="true" Text="Guardar" />
          <%--<asp:Button ID="cmdGuardar1" runat="server" CssClass="guardar" Enabled="true" Text="Cancelar" />--%>
        </td>
      </tr>
	  <tr>
        <td colspan="2">
          <asp:Label ID="lblMensajeFrm" runat="server" ForeColor="#CC0000" style="color: #003366; font-style: italic">
          </asp:Label>
        </td>
      </tr>
	</table>	 
	</asp:Panel>
    </form>
  </body>
</html>
