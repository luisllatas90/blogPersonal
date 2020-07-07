<%@ Page Language="VB" AutoEventWireup="false" CodeFile="personales.aspx.vb" Inherits="personales" title="Campus Virtual :: Hoja de Vida" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css" />
  <link  href="private/expediente.css" rel="stylesheet" type="text/css"  />
      <script type="text/javascript" src="private/expediente.js"></script>
    <style>
        .tab_pasar
        {
            font-weight: bold;
            font-size: 8pt;
            background: white;
            background-image: url(images/boton_sobre.gif);
            background-repeat: repeat-x;
            font-family: Tahoma;
            color: black;
            vertical-align: middle;
            text-align: center;
        }
        .tab_normal
        {
            font-size: 8pt;
            background: white;
            background-image: url(images/boton_normal.gif);
            background-repeat: repeat-x;
            font-family: Tahoma;
            color: black;
            vertical-align: middle;
            text-align: center;
        }
       
        .style1
        {
            width: 100%;
        }
       
        .style2
        {
            height: 22px;
        }
       
    body{ font-family: "Trebuchet MS", "Lucida Console", Arial, san-serif;
	color: Black;font-size:8pt;
	font: normal;
	}
       
        .style3
        {
            font-size: 8pt;
            color: black;
            font-family: verdana;
            width: 197px;
            height: 21px;
        }
        .style4
        {
            font-size: 8pt;
            color: black;
            font-family: verdana;
            height: 21px;
        }
       
    </style>
<title>Campus Virtual : Hoja de Vida</title>
<script type="text/javascript">
function religion(){
if (eval("document.form1.DDLReligion.value=='Catolico'"))
    eval("document.form1.DDLSacramento.disabled=false")
else 
    eval("document.form1.DDLSacramento.disabled=true")
}
</script>


</head>
<body>
<div id="divmensaje"></div>
<script type="text/javascript" src="div.js"></script>

<form id="form1" runat="server">
<center>
    <table class="style1" width="100%" cellpadding="0" cellspacing="0" border="0" >
        <tr>
            <td width="75%" align="left" valign="top">
    <table cellpadding="0" cellspacing="0" border="0" 
                    style="border-right: 1px solid; border-top: goldenrod 1px solid; border-left: 1px solid; border-bottom: 1px solid" 
                    class="tabla_personal" align="left" width="100%">
        <tr>
            <td style="height: 29px;" class="titulo_tabla">
                &nbsp;Registro de
                Datos Personales</td>
            <td style="height: 29px;" class="titulo_tabla" align="right">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" 
                    Font-Size="10pt" ForeColor="White" Text="Paso 01 de 07"></asp:Label>
                &nbsp;&nbsp;&nbsp; </td>
        </tr>
        <tr>
            <td align="center" style="border-bottom: gold 1px solid; height: 29px" 
                colspan="2">
                &nbsp;&nbsp;
                <asp:Label ID="LblPaterno" runat="server" Text="Label" Font-Bold="True" Font-Size="Small" ForeColor="Black"></asp:Label>
                            <asp:Label ID="LblMaterno" runat="server" Text="Label" Font-Bold="True" Font-Size="Small" ForeColor="Black"></asp:Label>
                            <asp:Label ID="LblNombres" runat="server" Text="Label" Font-Bold="True" ForeColor="Black" Font-Size="Small"></asp:Label></td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <table>
                    <tr>
                        <td style="height: 360px; padding-right: 10px; padding-left: 10px; padding-bottom: 10px; border-top-width: 1px; border-left-width: 1px; border-left-color: black; border-bottom-width: 1px; border-bottom-color: black; border-top-color: black; border-right-width: 1px; border-right-color: black;"
                            valign="top">
                <table style="width: 544px" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center" colspan="2" style="font-size: 10pt; text-transform: none; color: darkred;
                            font-family: Arial; height: 21px">
                            &nbsp;<asp:Image ID="imgfoto" runat="server" BorderColor="Black" BorderWidth="1px" Height="135px" />
                            <hr style="border-right: darkred 1px solid; border-top: darkred 1px solid; border-left: darkred 1px solid; border-bottom: darkred 1px solid; height: 1px" />
                        </td>
                    </tr>
                    <tr bgcolor="#FFFFCC">
                        <td colspan="2" align="left" style="width: 197px;" class="titulo_items">
                            <asp:Label ID="Label11" runat="server" Text="Última Actualización" 
                                Visible="False"></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblFechaActualización" runat="server" Text="04/07/2012" 
                                ForeColor="Red" Visible="False"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    
                    <tr bgcolor="#FFFFCC">
                        <td align="left" style="width: 197px;" class="titulo_items">
                            &nbsp;</td>
                        <td align="left">
                            <asp:CheckBox ID="chkEstadoRevision" runat="server" Font-Bold="True" 
                                Text="Revisado por la dirección de Personal" Font-Names="Verdana" 
                                Font-Size="10px" ForeColor="#0033CC" Enabled="False" Visible="False" />
                                        &nbsp;&nbsp;
                                        <asp:Label ID="lblFechaRevision" runat="server" Text="04/07/2012" 
                                ForeColor="Red" Visible="False"></asp:Label>
                                        <br />
                            <asp:Label ID="Label12" runat="server" Font-Names="Verdana" Font-Size="9px" 
                                Text="Se le solicita tener actualizados sus datos para fines administrativos." 
                                ForeColor="Blue" Font-Bold="True"></asp:Label>
                                        </td>
                    </tr>
                    
                      <tr>
                        <td align="left" colspan="2"><hr style="border-right: darkred 1px solid; border-top: darkred 1px solid; border-left: darkred 1px solid; border-bottom: darkred 1px solid; height: 1px" />
                        </td>
                    </tr>
                    <tr bgcolor="#FFFFCC">
                        <td align="left" colspan="2" class="titulo_items">
                            <asp:Label ID="Label10" runat="server" ForeColor="Blue" 
                                Text="Información Básica "></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Sexo</td>
                        <td align="left" style="font-size: 10pt; width: 438px; color: darkred; font-family: Arial;
                            height: 21px" title="Fecha de Nacimiento">
                            &nbsp;<asp:DropDownList ID="DDLSexo" runat="server" ToolTip="Sexo" CssClass="datos_combo">
                                <asp:ListItem Selected="True" Value="-1">Sexo</asp:ListItem>
                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                <asp:ListItem Value="F">Femenino</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="DDLSexo"
                                ErrorMessage="Seleccione Sexo" ValidationExpression="[Masculino, Femenino]" SetFocusOnError="True">*</asp:RegularExpressionValidator>
                            &nbsp;<asp:Label 
                                ID="Label2" runat="server" CssClass="titulo_items" Text="Fecha Nacimiento"></asp:Label>
                            &nbsp;<asp:DropDownList ID="DDlDia" runat="server" ToolTip="Dia de Nacimiento" CssClass="datos_combo">
                                <asp:ListItem Value="-1">Dia</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>31</asp:ListItem>
                            </asp:DropDownList><asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="DDlDia"
                                MaximumValue="31" MinimumValue="1" SetFocusOnError="True" Type="Integer" ErrorMessage="Seleccione dia de nacimiento">*</asp:RangeValidator>
                            <asp:DropDownList ID="DDLMes" runat="server" ToolTip="Mes de Nacimiento" 
                                CssClass="datos_combo" Height="16px" Width="61px">
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
                            </asp:DropDownList><asp:RangeValidator ID="RangeValidator2" runat="server"
                                ControlToValidate="DDLMes" ErrorMessage="Seleccione mes de nacimiento" MaximumValue="12"
                                MinimumValue="1" SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>
                            <asp:DropDownList ID="DDlAño" runat="server" ToolTip="Año de Nacimiento" 
                                CssClass="datos_combo" Height="16px" Width="63px">
                            </asp:DropDownList><asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="DDlAño"
                                ErrorMessage="Seleccione Año de nacimiento" MaximumValue="2020" MinimumValue="1915"
                                SetFocusOnError="True" Type="Integer">*</asp:RangeValidator></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Nacionalidad</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial; height: 21px; width: 438px;">
                            &nbsp;<asp:DropDownList ID="DDlNacionalidad" runat="server" 
                                ToolTip="Nacionalidad" CssClass="datos_combo" Height="16px" Width="130px">
                            </asp:DropDownList>&nbsp;&nbsp;
                            <asp:Label 
                                ID="Label4" runat="server" CssClass="titulo_items" Text="Estado Civil"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="DDLCivil" runat="server" 
                                ToolTip="Estado Civil" CssClass="datos_combo" Height="16px" Width="120px">
                                <asp:ListItem Value="1">-- Estado Civil --</asp:ListItem>
                                <asp:ListItem Value="SOLTERO">Soltero</asp:ListItem>
                                <asp:ListItem Value="CASADO">Casado</asp:ListItem>
                                <asp:ListItem Value="VIUDO">Viudo</asp:ListItem>
                                <asp:ListItem Value="DIVORCIADO">Divorciado</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="DDLCivil"
                                ErrorMessage="Seleccione estado civil" Operator="NotEqual" ValueToCompare="1">*</asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Religión</td>
                        <td align="left" class="titulo_items">
                            &nbsp;<asp:DropDownList ID="DDLReligion" runat="server" 
                                ToolTip="Tipo de Religion" CssClass="datos_combo" Height="16px" Width="130px">
                                <asp:ListItem Value="1">-- Religion --</asp:ListItem>
                                <asp:ListItem>Catolico</asp:ListItem>
                                <asp:ListItem>No Catolico</asp:ListItem>
                            </asp:DropDownList><asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="DDLReligion"
                                ErrorMessage="Seleccione religion" Operator="NotEqual" ValueToCompare="1">*</asp:CompareValidator>
                            Último Sacramento &nbsp;<asp:DropDownList ID="DDLSacramento" runat="server" 
                                ToolTip="Ultimo Sacramento" CssClass="datos_combo" Height="16px" Width="120px">
                                <asp:ListItem>Ninguno</asp:ListItem>
                                <asp:ListItem>Bautismo</asp:ListItem>
                                <asp:ListItem>Comunion</asp:ListItem>
                                <asp:ListItem>Confirmacion</asp:ListItem>
                                <asp:ListItem>Matrimonio</asp:ListItem>
                                <asp:ListItem>Sacerdote</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Tipo Documento</td>
                        <td align="left" class="titulo_items">
                            &nbsp;<asp:DropDownList ID="DDLDocumento" runat="server" CssClass="datos_combo">
                                <asp:ListItem>DNI</asp:ListItem>
                                <asp:ListItem>L/E</asp:ListItem>
                                <asp:ListItem>CIP</asp:ListItem>
                                <asp:ListItem>Carnet extranjeria</asp:ListItem>
                                <asp:ListItem>Pasaporte</asp:ListItem>
                                <asp:ListItem>LM</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;Nº 
                            <asp:Label ID="LblDocumento" runat="server" Text="Label" Width="117px" 
                                Font-Size="X-Small" ForeColor="Red" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Grupo Sanguíneo</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;<asp:DropDownList ID="DDLSangre" runat="server" ToolTip="Grupo Sanguineo" CssClass="datos_combo">
                                <asp:ListItem Value="-1">Sin Precisar</asp:ListItem>
                                <asp:ListItem>A Rh -</asp:ListItem>
                                <asp:ListItem>A Rh+</asp:ListItem>
                                <asp:ListItem>B Rh -</asp:ListItem>
                                <asp:ListItem>B Rh +</asp:ListItem>
                                <asp:ListItem>O -</asp:ListItem>
                                <asp:ListItem>O +</asp:ListItem>
                                <asp:ListItem>AB +</asp:ListItem>
                                <asp:ListItem>AB -</asp:ListItem>
                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label16" runat="server" Font-Size="11px" ForeColor="Black" 
                                Text="Número de Hijos"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;
                            <asp:TextBox ID="txtNumeroHijos" runat="server" Width="118px" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                ToolTip="Email principal" CssClass="datos_combo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            RUC</td>
                        <td align="left" class="titulo_items">
                        &nbsp;<asp:TextBox ID="txtRuc" runat="server" Width="128px" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                ToolTip="Email principal" CssClass="datos_combo" MaxLength="11"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Nivel Educativo</td>
                        <td align="left" class="titulo_items">
                             &nbsp;<asp:DropDownList ID="ddlNivelEducativo" Width="98%" runat="server" AutoPostBack="True" 
                                ToolTip="Nivel educativo logrado" CssClass="datos_combo" 
                                 Font-Size="Smaller">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="border-right: darkred 1px solid; border-top: darkred 1px solid; border-left: darkred 1px solid; border-bottom: darkred 1px solid; height: 1px" />
                        </td>
                    </tr>
                     <tr bgcolor="#FFFFCC">
                        <td align="left" colspan="2" class="titulo_items">
                            <asp:Label ID="Label9" runat="server" ForeColor="Blue" 
                                Text="Información necesaria para la SUNAT"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#FFFFCC">
                        <td align="left" colspan="2" class="titulo_items">
                            <asp:Label ID="Label17" runat="server" ForeColor="#00CC00" 
                                
                                Text="Ref.Essalud: Indicador Centro Asistencia Essalud. Sólo Asegurados Essalud." 
                                Font-Size="9px"></asp:Label>
                                <br />
                        </td>
                        
                    </tr>
                    <tr bgcolor="#FFFFCC">
                        <td align="left" colspan="2" class="titulo_items">
                            <asp:Label ID="Label18" runat="server" ForeColor="Blue" 
                                Text="------------------------------------------------------------------------------------------------------------" 
                                Font-Size="9px"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#FFFFCC">
                        <td align="left" colspan="2" class="titulo_items">
                            <asp:Label ID="Label13" runat="server" ForeColor="#FF0066" 
                                Text="Dirección Nº 1  (*) Datos Oblogatorios"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Ubigeo</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;<asp:DropDownList ID="ddlDepartamento1" Width="130px" runat="server" 
                                AutoPostBack="True" ToolTip="Departamento" CssClass="datos_combo" 
                                Font-Size="Smaller">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlProvincia1" Width="110px" runat="server" 
                                ToolTip="Provincia" AutoPostBack="True" CssClass="datos_combo" 
                                Font-Size="Smaller">
                            </asp:DropDownList> 
                            <asp:DropDownList ID="ddlDistrito1" runat="server" Width="110px" 
                                CssClass="datos_combo" Font-Size="Smaller">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="DDLDistrito"
                                ErrorMessage="Seleccione Lugar de residencia" Operator="NotEqual" SetFocusOnError="True"
                                ValueToCompare="0">*</asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Tipo de Via</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;<asp:DropDownList ID="ddlTipoVia1" Width="357px" runat="server" CssClass="datos_combo">
                            </asp:DropDownList>
                            </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Nombre Via</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;
                            <asp:TextBox ID="txtnombreVia_Per1" runat="server" Width="349px" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                ToolTip="Email principal" CssClass="datos_combo"></asp:TextBox>
                            </td>
                    </tr>
                     <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Número</td>
                        <td align="left" class="titulo_items">
                            &nbsp;<asp:TextBox ID="txtnumeroVia_Per1" runat="server" Width="102px" 
                                BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                style="text-align: right" ToolTip="Telefono domicilio" 
                                CssClass="datos_combo"></asp:TextBox>
                            &nbsp;Interior 
                            <asp:TextBox ID="txtinteriorVia_Per1" runat="server" Width="75px" 
                                BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                style="text-align: right" ToolTip="Telefono celular" CssClass="datos_combo"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="chkRefEssalud1" Text="Ref. Essalud" runat="server" 
                                ForeColor="Black" Font-Size="9px" />
                         </td>
                    </tr>
                     <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Tipo Zona</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;<asp:DropDownList ID="ddlTipoZona1" Width="330px" runat="server" 
                                CssClass="datos_combo">
                            </asp:DropDownList>
                            </td>
                    </tr>
                     <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Nombre Zona</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;
                            <asp:TextBox ID="txtnombreZona_Per1" runat="server" Width="345px" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                ToolTip="Email principal" CssClass="datos_combo"></asp:TextBox>
                            </td>
                    </tr>
                     <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Referencia</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;
                            <asp:TextBox ID="txtreferenciaZona_Per1" runat="server" Width="345px" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                ToolTip="Email principal" CssClass="datos_combo"></asp:TextBox>
                            </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr style="border-right: darkred 1px solid; border-top: darkred 1px solid; border-left: darkred 1px solid; border-bottom: darkred 1px solid; height: 1px" />
                        </td>
                    </tr>
                     <tr bgcolor="#FFFFCC">
                        <td align="left" colspan="2" class="titulo_items">
                            <asp:Label ID="Label14" runat="server" ForeColor="#FF0066" 
                                Text="Dirección Nº 2 (opcional)"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Ubigeo</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;<asp:DropDownList ID="ddlDepartamento2" Width="130px" runat="server" 
                                AutoPostBack="True" ToolTip="Departamento" CssClass="datos_combo" 
                                Font-Size="Smaller">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlProvincia2" Width="110px" runat="server" ToolTip="Provincia" AutoPostBack="True" CssClass="datos_combo">
                            </asp:DropDownList> 
                            <asp:DropDownList ID="ddlDistrito2" runat="server" Width="110px" CssClass="datos_combo">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="DDLDistrito"
                                ErrorMessage="Seleccione Lugar de residencia" Operator="NotEqual" SetFocusOnError="True"
                                ValueToCompare="0">*</asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Tipo de Via</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;<asp:DropDownList ID="ddlTipoVia2" Width="357px" runat="server" 
                                CssClass="datos_combo" ToolTip="Tipo Via">
                            </asp:DropDownList>
                            </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Nombre Via</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            
                            <asp:TextBox ID="txtnombreVia_Per2" runat="server" Width="349px" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                ToolTip="Nombre de la Via" CssClass="datos_combo"></asp:TextBox>
                            </td>
                    </tr>
                     <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Número</td>
                        <td align="left" class="titulo_items">
                            &nbsp;<asp:TextBox ID="txtnumeroVia_Per2" runat="server" Width="102px" 
                                BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                style="text-align: right" ToolTip="Número" CssClass="datos_combo"></asp:TextBox>
                            &nbsp;Interior 
                            <asp:TextBox ID="txtinteriorVia_Per2" runat="server" Width="75px" 
                                BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                style="text-align: right" ToolTip="Interior" CssClass="datos_combo"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="chkRefEssalud2" Text="Ref. Essalud" runat="server" 
                                ForeColor="Black" Font-Size="9px" 
                                ToolTip="La direccion es la considerada para Essalud" />
                         </td>
                    </tr>
                     <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Tipo Zona</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;<asp:DropDownList ID="ddlTipoZona2" Width="357px" runat="server" 
                                CssClass="datos_combo" ToolTip="Tipo de Zona">
                            </asp:DropDownList>
                            </td>
                    </tr>
                     <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Nombre Zona</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;
                            <asp:TextBox ID="txtnombreZona_Per2" runat="server" Width="349px" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                ToolTip="Nombre de la Zona" CssClass="datos_combo"></asp:TextBox>
                            </td>
                    </tr>
                     <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Referencia</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;
                            <asp:TextBox ID="txtreferenciaZona_Per2" runat="server" Width="349px" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                ToolTip="Luegar de referencia" CssClass="datos_combo"></asp:TextBox>
                            </td>
                    </tr>
                     <tr>
                        <td align="left" colspan="2" style="height: 2px"><hr style="border-right: darkred 1px solid; border-top: darkred 1px solid; border-left: darkred 1px solid; border-bottom: darkred 1px solid; height: 1px" />
                        </td>
                    </tr>
                     <tr bgcolor="#FFFFCC">
                        <td align="left" colspan="2" class="titulo_items">
                            <asp:Label ID="Label15" runat="server" ForeColor="Blue" 
                                Text="Datos administrativos para USAT"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Lugar de Residencia</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;<asp:DropDownList ID="DDLDepartamento" runat="server" AutoPostBack="True" ToolTip="Departamento" CssClass="datos_combo">
                            </asp:DropDownList>
                                                       <asp:DropDownList ID="DDLProvincia" runat="server" ToolTip="Provincia" AutoPostBack="True" CssClass="datos_combo">
                            </asp:DropDownList> 
                            <asp:DropDownList ID="DDLDistrito" runat="server" CssClass="datos_combo">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="DDLDistrito"
                                ErrorMessage="Seleccione Lugar de residencia" Operator="NotEqual" SetFocusOnError="True"
                                ValueToCompare="0">*</asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Dirección</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;<asp:TextBox ID="TxtDireccionPer" runat="server" Width="397px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ForeColor="Navy" ToolTip="Direccion de residencia" CssClass="datos_combo"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="Direcrequerir" runat="server" ControlToValidate="TxtDireccionPer" ErrorMessage="Direccion personal requerida">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Teléfonos</td>
                        <td align="left" class="titulo_items">
                            &nbsp;Domicilio 
                            <asp:TextBox ID="TxtTelDomicilio" runat="server" Width="75px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" style="text-align: right" ToolTip="Telefono domicilio" CssClass="datos_combo"></asp:TextBox>
                            Celular 
                            <asp:TextBox ID="TxtTelCelular" runat="server" Width="75px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" style="text-align: right" ToolTip="Telefono celular" CssClass="datos_combo"></asp:TextBox>
                            Trabajo 
                            <asp:TextBox ID="TxtTelTrabajo" runat="server" Width="52px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ToolTip="Telefono Trabajo" CssClass="datos_combo"></asp:TextBox>
                            -<asp:TextBox
                                ID="TxtAnexo" runat="server" Width="32px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ToolTip="Anexo trabajo" CssClass="datos_combo"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Email Principal</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;<asp:TextBox ID="TxtMail1" runat="server" Width="220px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ToolTip="Email principal" CssClass="datos_combo"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtMail1"
                                SetFocusOnError="True" ErrorMessage="Email Principal Requerido">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegEmail1" runat="server" ControlToValidate="TxtMail1"
                                ErrorMessage="Email principal no valido" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Email Alternativo</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;<asp:TextBox ID="TxtMail2" runat="server" Width="220px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ToolTip="Email alternativo" CssClass="datos_combo"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegMail2" runat="server" ControlToValidate="TxtMail2"
                                ErrorMessage="Email secundario no valido" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Fotografía</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;<asp:FileUpload ID="FuFoto" runat="server" Width="299px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ToolTip="Fotografia" CssClass="datos_combo" /><asp:Image ID="Img" runat="server" ImageUrl="~/images/menus/prioridad_.gif" style="cursor: hand" /><span style="font-size: 8pt">
                            (*.jpg. max 60 Kb)</span><asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                runat="server" ControlToValidate="FuFoto" ErrorMessage="Solo puede ingresar archivos con extension *.jpg"
                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpg|.JPG|.png|.PNG)$">*</asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" style="height: 2px"><hr style="border-right: darkred 1px solid; border-top: darkred 1px solid; border-left: darkred 1px solid; border-bottom: darkred 1px solid; height: 1px" />
                        </td>
                    </tr>
                    <tr bgcolor="#FFFFCC">
                        <td align="left" colspan="2" class="titulo_items">
                            <asp:Label ID="Label7" runat="server" ForeColor="Blue" 
                                Text="En caso de emergencia comunicarse con la siguiente persona"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Nombres</td>
                        <td align="left" style="height: 21px">
                            &nbsp;<asp:TextBox ID="TxtEmerNombre" runat="server" Width="314px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ToolTip="Perona a llamar en caso de emergencia" MaxLength="80" CssClass="datos_combo"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Dirección</td>
                        <td align="left" style="height: 21px">
                            &nbsp;<asp:TextBox ID="TxtEmerDireccion" runat="server" Width="314px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ToolTip="Direccion de persona para casos de emergencia" MaxLength="80" CssClass="datos_combo"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Teléfono</td>
                        <td align="left" style="height: 21px">
                            &nbsp;<asp:TextBox ID="TxtEmerTelefono" runat="server" Width="126px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ToolTip="Telfono a llamar en caso de emergencia" MaxLength="30" CssClass="datos_combo"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="left" class="titulo_items" colspan="2">
                            <hr style="border-right: darkred 1px solid; border-top: darkred 1px solid; border-left: darkred 1px solid; border-bottom: darkred 1px solid; height: 1px" />
                        </td>
                     </tr>
                     <tr bgcolor="#FFFFCC">
                        <td align="left" colspan="2" class="titulo_items">
                            &nbsp;<asp:Label ID="Label6" runat="server" ForeColor="#3333FF" 
                                Text="Información de Cuenta Sueldo"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Entidad Financiera
                        </td>
                        <td align="left" style="height: 21px"  class="titulo_items">
                            <asp:DropDownList ID="ddlEntidadFinancieraCuenta" Width="200px" runat="server" 
                                CssClass="datos_combo" Font-Size="Smaller" AutoPostBack="True">
                            </asp:DropDownList>
                         &nbsp;Moneda&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            <asp:DropDownList ID="ddlTipoMonedaSueldo" Width="120px" CssClass="datos_combo" 
                                runat="server" Font-Size="Smaller">
                                <asp:ListItem Value="S">SOLES</asp:ListItem>
                                <asp:ListItem Value="D">DOLARES</asp:ListItem>
                                <asp:ListItem Value="E">EUROS</asp:ListItem>
                            </asp:DropDownList>
                         </td>
                    </tr>
                    <tr>
                        <td align="left" class="style3">
                            Número de Cuenta</td>
                        <td align="left"  class="style4">
                        <asp:TextBox ID="txtNumerocuentaSueldo" runat="server" Width="195px" 
                                BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                ToolTip="Perona a llamar en caso de emergencia" MaxLength="80" 
                                CssClass="datos_combo" Enabled="False"></asp:TextBox>
                            &nbsp;&nbsp;Tipo Cuenta <asp:DropDownList ID="ddlTipoCuentaSueldo" 
                                Width="120px" CssClass="datos_combo" runat="server" Font-Size="Smaller">
                                <asp:ListItem Value="1">AHORRO</asp:ListItem>
                                <asp:ListItem Value="2">CUENTA CORRIENTE</asp:ListItem>
                                <asp:ListItem Value="3">OTROS</asp:ListItem>
                            </asp:DropDownList>
                         </td>
                    </tr>     
                     <tr bgcolor="#FFFFCC">
                        <td align="left" colspan="2" class="titulo_items">
                            &nbsp;<asp:Label ID="Label8" runat="server" ForeColor="#3333FF" Text="Información de Cuenta C.T.S"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Entidad Financiera
                        </td>
                        <td align="left" style="height: 21px"  class="titulo_items">
                            <asp:DropDownList ID="ddlEntidadFinancieraCts" Width="200px" runat="server" 
                                CssClass="datos_combo" Font-Size="Smaller">
                            </asp:DropDownList>
                         &nbsp;Moneda&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            <asp:DropDownList ID="ddlTipoMonedaCts" Width="120px" CssClass="datos_combo" 
                                runat="server" Font-Size="Smaller">
                                <asp:ListItem Value="S">SOLES</asp:ListItem>
                                <asp:ListItem Value="D">DOLARES</asp:ListItem>
                                <asp:ListItem Value="E">EUROS</asp:ListItem>
                            </asp:DropDownList>
                         </td>
                    </tr>
                     <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Número de Cuenta</td>
                        <td align="left" style="height: 21px"  class="titulo_items">
                        <asp:TextBox ID="txtNumerocuentaCts" runat="server" Width="195px" 
                                BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                ToolTip="Perona a llamar en caso de emergencia" MaxLength="80" 
                                CssClass="datos_combo" Enabled="False"></asp:TextBox>
                            &nbsp;&nbsp;</td>
                    </tr>    
                    </tr>
                     <tr>
                        <td align="left" class="titulo_items" colspan="2">
                            <hr style="border-right: darkred 1px solid; border-top: darkred 1px solid; border-left: darkred 1px solid; border-bottom: darkred 1px solid; height: 1px" />
                        </td>
                    </tr>
                     <tr bgcolor="#FFFFCC">
                        <td align="left" colspan="2" class="titulo_items">
                            &nbsp;<asp:Label ID="Label5" runat="server" ForeColor="#3333FF" Text="Datos Seguridad Social"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Régimen de Salud</td>
                        <td align="left" style="height: 21px"  class="titulo_items">
                            <asp:DropDownList ID="ddlRegimenSalud" Width="200px" runat="server" 
                                CssClass="datos_combo" Font-Size="Smaller" AutoPostBack="True">
                            </asp:DropDownList>
                            &nbsp;SCTR Salud&nbsp;<asp:DropDownList ID="ddlSCTRSalud" Width="120px" CssClass="datos_combo" 
                                runat="server" Font-Size="Smaller">
                                <asp:ListItem Value="0">NINGUNO</asp:ListItem>
                                <asp:ListItem Value="1">ESSALUD</asp:ListItem>
                                <asp:ListItem Value="2">EPS</asp:ListItem>
                            </asp:DropDownList>
                         </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            E.Prestadora Salud
                        </td>
                        <td align="left" style="height: 21px"  class="titulo_items">
                            <asp:DropDownList ID="ddlCodigoEPS" Width="200px" runat="server" 
                                CssClass="datos_combo" Font-Size="Smaller">
                            </asp:DropDownList>
                         &nbsp;Código AFP&nbsp; 
                            <asp:DropDownList ID="ddlAfps" Width="120px" 
                                CssClass="datos_combo" runat="server" Enabled="False" Font-Size="Smaller">
                            </asp:DropDownList>
                         </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Reg.Pensionario</td>
                        <td align="left" style="height: 21px"  class="titulo_items">
                            <asp:DropDownList ID="ddlRegPension" Width="200px" runat="server" 
                                CssClass="datos_combo" Font-Size="Smaller" AutoPostBack="True">
                            </asp:DropDownList>
                            &nbsp;Nº CUSSP&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtCussp" runat="server" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                ToolTip="" MaxLength="80" 
                                CssClass="datos_combo" Width="116px" Enabled="False"></asp:TextBox>
                         </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Situación EPS
                        </td>
                        <td align="left" style="height: 21px"  class="titulo_items">
                            <asp:DropDownList ID="ddlSituacionEPS" Width="200px" runat="server" 
                                CssClass="datos_combo" Font-Size="Smaller">
                            </asp:DropDownList>
                            &nbsp;
                         </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 197px;" class="titulo_items">
                            F. Rég.Pensionario</td>
                        <td align="left" style="height: 21px"  class="titulo_items">
                            <asp:TextBox ID="txtfecinpensionario" runat="server" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                ToolTip="" MaxLength="80" 
                                CssClass="datos_combo" Width="116px" Enabled="False"></asp:TextBox>
                         &nbsp; Fecha Ingreso Institución&nbsp; 
                            <asp:TextBox ID="txtfecinusat" runat="server" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                ToolTip="" MaxLength="80" 
                                CssClass="datos_combo" Width="116px" Enabled="False"></asp:TextBox>
                         </td>
                    </tr>
                    
                    <tr>
                        <td align="left" class="titulo_items" colspan="2">
                            <hr style="border-right: darkred 1px solid; border-top: darkred 1px solid; border-left: darkred 1px solid; border-bottom: darkred 1px solid; height: 1px" />
                        </td>
                    </tr>
                    <tr bgcolor="#FFFFCC">
                        <td align="left" style="width: 197px;" class="titulo_items">
                            Juramento Fidelidad</td>
                        <td align="left">
                            <asp:CheckBox ID="chkFirmoCarta" runat="server" Font-Bold="True" 
                                Text="¿FIRMÓ JURAMENTO DE FIDELIDAD?" Font-Names="Verdana" 
                                Font-Size="11px" ForeColor="#0033CC" />
                                        <br />
                            <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="9px" 
                                Text="(Marque el check si firmó el Juramento de Fidelidad)" 
                                ForeColor="#FF0066"></asp:Label>
                                        </td>
                    </tr>
                </table>
                            </td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td align="right" style="border-top: gold 1px solid; height: 40px" colspan="2">
                <asp:ValidationSummary 
                    ID="ListaErrores" 
                    runat="server" 
                    ShowMessageBox="True" 
                    DisplayMode="List" 
                    ShowSummary="False" />
                &nbsp; &nbsp; &nbsp;<asp:Button ID="CmdGuardar" runat="server" CssClass="tab_normal"
                    Height="26px" Text="Siguiente &gt;&gt;" Width="86px" />&nbsp;
            </td>
        </tr>
    </table>
                </td>
                <td align="left" valign="top" width="25%">
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td width="20%">
                                <table align="left">
                                    <tr>
                                        <td>
                                        <a href="personales.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>"> 
                                            <img border=0 alt="Datos personales" src="images/hojavida/datospersonales_r.gif" /></td>
                                        </a>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="perfil.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>"> 
                                            <img border=0 alt="Perfil profesional" src="images/hojavida/perfilprofesional.gif" />
                                        </a>
                                        </td>
                                        
                                    </tr>
                                    <tr align="left">
                                        <td align="left" class="style2">
                                        <a href="educacionuniversitaria.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">                                         
                                            <img border=0 alt="Títulos y grados académicos" src="images/hojavida/titulosygrados.gif" />
                                        </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="idiomas.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">                                         
                                            <img border=0 alt="Idiomas y otros cursos" src="images/hojavida/idiomasyotros.gif" />
                                        </a>    
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="experiencia.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">                                         
                                            <img border=0 alt="Experiencia laboral y asistencia a eventos" src="images/hojavida/exepriencia.gif" />
                                        </a>    
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="distinciones.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">                                         
                                            <img border=0 alt="Distinciones y honores" src="images/hojavida/distinciones.gif" />
                                        </a>    
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="otros.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">
                                            <img border=0 alt="Otros datos" src="images/hojavida/otrosdatos.gif" />
                                        </a>
                                            </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="80%" valign="top">
                                <table cellpadding="3" cellspacing="3" class="tabla_personal" 
                                    style="background-color: #FFFFCC; border: 1px solid #000080">
                                    <tr>
                                        <td align="left">
                                            <font face="Arial" size="2"><span style="FONT-SIZE: 10pt; FONT-FAMILY: Arial">
                                            <b>Estimado trabajador:</b><br />
                                            <br />
                                            Sírvase completar esta información para actualizar su Ficha Personal.<br />
                                            <br />
                                            Esta información será verificada y utilizada por la Universidad, para fines 
                                            académicos y administrativos.<br />
                                            <br />
                                            Muchas Gracias</span></font></td>
                                    </tr>
                                </table>
                                    </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>
   </form>
   </body>
   </html>
   


