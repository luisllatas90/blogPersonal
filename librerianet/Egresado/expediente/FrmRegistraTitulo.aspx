<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmRegistraTitulo.aspx.vb" Inherits="FrmRegistraTitulo" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <title>Formación Académica</title>
    <link href="private/estilo.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../jquery/jquery-ui.css" />
    <script src="../jquery/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../jquery/jquery-ui.js" type="text/javascript"></script>
    <script src="../jquery/universidades.js" type="text/javascript"></script>
         
    <link  href="private/expediente.css" rel="stylesheet" type="text/css"  />
    <link href="private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {
            $(".contador").each(function() {
                var longitud = $(this).val().length;
                if (longitud == 0) {
                    longitud = 160;
                }
                $(this).parent().find('#longitud_textarea').html('<i>Quedan: ' + longitud + ' caracteres</i>');
                $(this).keyup(function() {
                    var nueva_longitud = $(this).val().length;
                    $(this).parent().find('#longitud_textarea').html('<i>Quedan: ' + (160 - nueva_longitud) + ' caracteres</i>');
                    if (nueva_longitud >= "160") {
                        $('#longitud_textarea').css('color', '#ff0000');
                        var text = $(".contador").val();
                        var new_text = text.substr(0, 160);
                        $(".contador").val(new_text);
                        var nueva_longitud = $(this).val().length;
                        $(this).parent().find('#longitud_textarea').html('<i>Quedan: ' + (160 - nueva_longitud) + ' caracteres</i>');
                    }
                });
            });

        });
    </script>
    <style type="text/css">
        body {
            color: #2F4F4F;
            font-family: "Trebuchet MS","Lucida Console",Arial,san-serif;
        }      
        .style18
        {
            font-weight: normal;
            width: 147px;
        }
        .guardar {
            border:1px solid #C0C0C0; 
            background:#FEFFE1 ; 
            width:80; font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold; height:25
        }
        
        a { text-decoration:none; color:Black;}
     
        form { margin:0px; }
 
        #txtdescripcion {
            width: 483px;
        }
 
        .content {
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
          &nbsp;Registrar/Actualizar Título Profesional</td></tr>          
              <tr>
                <td class="style18">Año</td>
                <td class="style21">Ingreso:&nbsp;
                  <asp:DropDownList ID="dpAnioIngreso" runat="server" CssClass="datos_combo"
                        ToolTip="Año de Ingreso" Width="63px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
<asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Checked="True" 
                        Text="Hasta la actualidad" />
                  </td>
              </tr>
                 <tr>
                     <td class="style18">&nbsp;</td>
                     <td class="style21">
                         <asp:Label ID="Label1" runat="server" Text="Egreso:  " Visible="False"></asp:Label>
                         &nbsp;&nbsp;
                         <asp:DropDownList ID="dpAnioEgreso" runat="server" CssClass="datos_combo"
                             ToolTip="Año de Egreso" Visible="False" Width="90px">
                         </asp:DropDownList>
                     </td>
                 </tr>
              <tr>
                <td class="style18"><asp:Label ID="LblFT" runat="server" Text="Fecha de Titulación" Visible="false"></asp:Label></td>
                <td class="style21">
		            <asp:DropDownList ID="dpdiagrad" runat="server" SkinID="ComboObligatorio" 
              ToolTip="Dia de Titulación" CssClass="datos_combo" Width="45px">
              <asp:ListItem Value="-1">Dia</asp:ListItem>
              <asp:ListItem Value="01">1</asp:ListItem>
              <asp:ListItem Value="02">2</asp:ListItem>
              <asp:ListItem Value="03">3</asp:ListItem>
              <asp:ListItem Value="04">4</asp:ListItem>
              <asp:ListItem Value="05">5</asp:ListItem>
              <asp:ListItem Value="06">6</asp:ListItem>
              <asp:ListItem Value="07">7</asp:ListItem>
              <asp:ListItem Value="08">8</asp:ListItem>
              <asp:ListItem Value="09">9</asp:ListItem>
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
		  <%--<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="dpdiagrad"
          MaximumValue="31" MinimumValue="1" SetFocusOnError="True" Type="Integer" ErrorMessage="Seleccione dia de Titulación">*</asp:RangeValidator>--%>
            <asp:DropDownList ID="dpmesgrad" runat="server" 
              ToolTip="Mes de Titulación" CssClass="datos_combo" Width="90px">
              <asp:ListItem Value="-1">Mes</asp:ListItem>
              <asp:ListItem Value="01">Enero</asp:ListItem>
              <asp:ListItem Value="02">Febrero</asp:ListItem>
              <asp:ListItem Value="03">Marzo</asp:ListItem>
              <asp:ListItem Value="04">Abril</asp:ListItem>
              <asp:ListItem Value="05">Mayo</asp:ListItem>
              <asp:ListItem Value="06">Junio</asp:ListItem>
              <asp:ListItem Value="07">Julio</asp:ListItem>
              <asp:ListItem Value="08">Agosto</asp:ListItem>
              <asp:ListItem Value="09">Setiembre</asp:ListItem>
              <asp:ListItem Value="10">Octubre</asp:ListItem>
              <asp:ListItem Value="11">Noviembre</asp:ListItem>
              <asp:ListItem Value="12">Diciembre</asp:ListItem>
            </asp:DropDownList>
			<%--<asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="dpmesgrad" ErrorMessage="Seleccione mes de Titulación" 
			  MaximumValue="12" MinimumValue="1" SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>--%>
            <asp:DropDownList ID="DDlAnio" runat="server" 
              ToolTip="Año de Titulación" CssClass="datos_combo" Height="16px" Width="63px"></asp:DropDownList>
            <%--<asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="DDlAnio"
              ErrorMessage="Seleccione Año de Titulación" MaximumValue="2020" MinimumValue="1915"
              SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>--%>
             <asp:Label ID="Lblopcional" runat="server" CssClass="rojo" Text="&nbsp;&nbsp;&nbsp;* Registro Opcional" Visible="false"></asp:Label>
                </td>
              </tr>
              <tr>
                <td class="style18">Título Profesional</td>
                <td class="style21">
                  <asp:TextBox ID="txtGrado" runat="server" Width="290px" CssClass="datos_combo"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td class="style18">Universidad</td>
                <td class="style21">
                    <asp:TextBox ID="txtUniversidad" runat="server" Width="483px" CssClass="datos_combo"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td class="style18">Procedencia</td>
                <td class="style21">
                    <asp:DropDownList ID="dpProcedencia" runat="server" SkinID="ComboObligatorio" 
		                ToolTip="Procedencia" CssClass="datos_combo" Width="110px">
                        <asp:ListItem Value="-1">- Seleccione -</asp:ListItem>
                        <asp:ListItem Value="Nacional">Nacional</asp:ListItem>
                        <asp:ListItem Value="Extranjera">Extranjera</asp:ListItem>
                    </asp:DropDownList> 
                    <%--<span class="titulo_items">&nbsp;&nbsp;&nbsp;Situación</span>&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="dpSituacion" runat="server" SkinID="ComboObligatorio" 
		                ToolTip="Situación" CssClass="datos_combo" Width="110px">
                        <asp:ListItem Value="-1">&gt;&gt;Seleccione&lt;&lt;</asp:ListItem>
                        <asp:ListItem Value="CULMINADO">CULMINADO</asp:ListItem>
                        <asp:ListItem Value="EN PROCESO">EN PROCESO</asp:ListItem>
                    </asp:DropDownList>--%>
                 </td>
              </tr>
              <tr>
                 <td class="style18">
                   <asp:HiddenField ID="codigo_FA" runat="server" Value="0" />       
                 </td>
                 <td class="style21"><br />
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
