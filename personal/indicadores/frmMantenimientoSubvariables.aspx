﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMantenimientoSubvariables.aspx.vb" Inherits="Indicadores_Formularios_frmMantenimientoSubvariables" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
<script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
<script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
<link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />

  <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
        
        <script type="text/javascript" language="javascript">
            function PintarFilaElegida(obj) {
                if (obj.style.backgroundColor == "white") {
                    obj.style.backgroundColor = "#E6E6FA"//#395ACC
                }
                else {
                    obj.style.backgroundColor = "white"
                }
            }
            function cmdNuevo_onclick() {

            }
    </script>
    
 
    <style type="text/css">
        .usatFormatoCampo
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	/*width:200px;*/
        	width:20%;
        }
        
        .usatFormatoCampoAncho
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:65%;
        	/*border:1px solid red;*/
        }
        
        .usatTituloAzul {
	        font-family: Arial;
	        font-size: 12pt;
	        font-weight: bold;
	        color: #3063c5;            
            width: 831px;
            height: 20px;
        }
               
        .usatPanel
        {        
            border: 1px solid #C0C0C0;	        
	        height:190px;	        
	        -moz-border-radius: 15px; 
	        /*padding-top:10px;*/	        
        }
        
        .usatPanelConsulta
        {        
            border: 1px solid #C0C0C0;	        	        
	        /*-moz-border-radius: 15px; */
	        padding-top:10px;
	        margin-top:10px; 
	        padding-bottom:10px;	              
        }
        
        #lblSubtitulo
        {
        	position:relative;
        	top: -10px;
        	left: 10px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;        	        
        	text-align:center;
        }
                
         #lblConsulta
        {
        	position:relative;
        	top: -20px;
        	left: 10px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;        	      
        	text-align:center;
        }
        
        /***** Para avisos **************/
        .mensajeError
        {
        	border: 1px solid #e99491;
        	background-color: #fed8d5;        	
        	padding-top:2px;        	
        	-moz-border-radius: 15px;
        	padding-left:25px;  	
        }
        
         .mensajeExito
        {
        	border: 1px solid #488e00;
        	background-color:#c5f4b5;        	
        	padding-top:2px;        	
        	-moz-border-radius: 15px;
        	padding-left:25px;        	
        }
        
        /********************************/   
                        
    </style>
       
</head>
<body>
    <form id="form1" runat="server">
    <div class="usatTituloAzul">Añadir Subvariables
         <a href="#" onclick="apprise('Añade un nueva Subvariable.');">
            <img src="../Images/info.png" style="border:0" alt="Ayuda."/>
        </a>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
     <!-- Para avisos -->
    <div style="clear:both; height:5px"></div>
    
    <div id="avisos" runat="server" style="height:25px; padding-top:2px; width: 50%">
    <asp:Image ID="Image1" runat="server" ImageUrl="../Images/beforelastchild.GIF"/>
    <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>            
    </div>
    <div style="clear:both; height:10px"></div>
    <!-------------------------------------------->
    
    <div class="usatPanel">        
        <asp:Label ID="lblSubtitulo" runat="server" Text="Datos de registro" 
            Width="120px"></asp:Label>
        <div style="margin-top:10px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="lblCco" runat="server" Text="Centro de Costos"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
              <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>--%>
                        <asp:DropDownList ID="ddlCco" runat="server" Width="100%">
                        </asp:DropDownList>
                     <%--</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvSubvariable" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>     --%>
            </div>
            <div style="clear:both; height:5px;"></div>        
        </div>    
        <div>
            <div class="usatFormatoCampo">
                <asp:Label ID="lblVariable" runat="server" Text="Variable"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>--%>
                        <asp:DropDownList ID="ddlVariable" runat="server" Width="100%" 
                    AutoPostBack="True">
                </asp:DropDownList>
                    <%--</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvSubvariable" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>--%>
                
            </div>
            <div style="clear:both; height:5px;"></div>
        </div>
        
        <div>
            <div class="usatFormatoCampo">
                <asp:Label ID="lblFacultad" runat="server" Text="Facultad"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
               <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>--%>
                        <asp:DropDownList ID="ddlFacultad" runat="server" Width="100%" 
                    AutoPostBack="True">
                </asp:DropDownList>
                    <%--</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvSubvariable" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>--%>
                
            </div>
            <div style="clear:both; height:5px;"></div>
        </div>    
        
        <div style="height:50px;">
            <div>
                 <div class="usatFormatoCampo">
                <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción Subvariable:"></asp:Label>
                 </div>
                <div class="usatFormatoCampoAncho">
                  <%--  <asp:TextBox ID="txtVariable" runat="server" Width="70%" Enabled="False" 
                        Font-Size="Smaller"></asp:TextBox>--%>
                          
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                             <asp:TextBox ID="txtDescripcion" runat="server" Width="99%" 
                        ValidationGroup="grupo1" Font-Size="Smaller" Enabled="False"></asp:TextBox>                
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlVariable" 
                                EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlFacultad" 
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>    
                </div>
            </div>
           
            <div style="top:260px; left:270px; position:absolute">
               <%-- <div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtDescripcion" ErrorMessage="(*) Debe ingresar la Descripción" 
                    ValidationGroup="grupo1" Display="Dynamic"></asp:RequiredFieldValidator>                                          
                </div>--%>
                
               <%-- <div>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtDescripcion" Display="Dynamic" 
                    ErrorMessage="(*) Ingrese carácteres válidos" ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ ,.]{3,}" 
                    ValidationGroup="grupo1" SetFocusOnError="true" EnableTheming="True"></asp:RegularExpressionValidator>  
                </div> --%>
                
              <%--  <div>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" 
                    ErrorMessage="(*) Ingresar valores válidos" ControlToValidate="txtDescripcion" 
                    Display="Dynamic" ValidationGroup="grupo1"></asp:CustomValidator>   
                </div>--%>
                                      
                <div>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ErrorMessage="(*) Debe seleccionar una Variable" 
                    ControlToValidate="ddlVariable" Display="Dynamic" Operator="NotEqual" 
                    ValidationGroup="grupo1" ValueToCompare="%"></asp:CompareValidator>
                </div>
                
                <div>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ErrorMessage="(*) Debe seleccionar una Facultad" 
                    ControlToValidate="ddlFacultad" Display="Dynamic" Operator="NotEqual" 
                    ValidationGroup="grupo1" ValueToCompare="0"></asp:CompareValidator>
                </div>
            </div>
        </div>           
    </div>
    
    <div>    
        
        <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0"> 
            <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                <td style="width:80%">
                    </td>
                        <td align="right" style="width:20%">
                                <asp:Button ID="cmdGuardar" runat="server" Text="   Guardar" 
                                    CssClass="guardar2" ValidationGroup="grupo1"/> &nbsp;<asp:Button 
                                    ID="cmdCancelar" runat="server" Text="Limpiar" CssClass="regresar2" 
                                    Width="70px" />
                </td>
            </tr>
        </table>
        <br />       
    </div>
    
    <!-- <div style="border:1px solid red;"> -->
    <div class="usatPanelConsulta">
        <asp:Label ID="lblConsulta" runat="server" Text="Consulta de Subvariables" 
            Width="180px"></asp:Label>
        <div style="height:50px;">
            <div class="usatFormatoCampo">
                <asp:Label ID="lblBuscar" runat="server" Text="Ingrese parámetro de búsqueda"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtBuscar" runat="server" Width="86%"></asp:TextBox>
                
                <!-- Para Validar Caja de Busqueda -->
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                                            runat="server" 
                                            ControlToValidate="txtBuscar" 
                                            Display="Dynamic" 
                                            ErrorMessage="Ingrese carácteres válidos" 
                                            ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ ,.]{3,}" 
                                            ValidationGroup="grupo2" 
                                            SetFocusOnError="true" 
                                            EnableTheming="True"
                                            Text="*"
                                            >
                </asp:RegularExpressionValidator>
                
                <asp:CustomValidator   ID="CustomValidator2" 
                                    runat="server" 
                                    ErrorMessage="Se encontraron palabras reservadas en la búsqueda de la Subvariable." 
                                    ControlToValidate="txtBuscar" 
                                    Display="Dynamic"                                     
                                    onservervalidate="CustomValidator2_ServerValidate"
                                    Text="*" ValidationGroup="grupo2"
                                    >
                </asp:CustomValidator>   
                
                <!------------------------------------------------------------------------------------>
                
                 <asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="buscar" 
                 ValidationGroup="grupo2" /> <!-- Para Validar Caja de Busqueda -->
            </div>
        </div>
        
        <!-- Para Validar Caja de Busqueda -->
        
        <div style="clear:both; height:10px"></div>
        <div style="padding-left:25px">                            
                <table cellpadding="3" cellspacing="0"> 
                    <tr style="font-weight: bold">
                        <td style="width:80%">
                        
                         <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                                DisplayMode="BulletList" ShowSummary="true" HeaderText="Errores encontrados en la Búsqueda:" 
                                BorderWidth="0" ValidationGroup="grupo2" Font-Bold="False" 
                                ForeColor="#FF0066"/>
                        
                            </td>                               
                    </tr>
                </table>
                <br />       
            </div>                    
        <div>
        
        <!------------------------------------------------------------------------------------>
        
        <div>
         <table cellpadding="3" cellspacing="0" style="width: 99%; border:1px solid #96ACE7" border="0">
                <tr>
                    <td style="text-align:center" align="center">
                       <%-- <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>   --%>                 
                        <asp:GridView ID="gvSubvariable" runat="server" Width="100%" CellPadding="3" AutoGenerateColumns="False" 
                            DataKeyNames="Codigo,CodigoCC,CodigoVar,CodigoFac,PertenceFormula" BackColor="White" 
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                            <RowStyle ForeColor="#000066" />
                            <Columns>
                                <asp:BoundField HeaderText="N°" >
                                <ItemStyle Width="15px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Codigo" DataField="Codigo" />                                                               
                                <asp:BoundField DataField="Variable" HeaderText="Variable" />
                                <asp:BoundField HeaderText="Subvariable" DataField="Descripcion" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>                                                                  
                                <asp:BoundField DataField="CentroCostos" HeaderText="Centro de Costos" />
                                <asp:BoundField DataField="ExisteValor" HeaderText="Existe Valor" />
                                <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistro" />
                                <asp:BoundField DataField="CodigoCC" HeaderText="CodigoCC" Visible="False" />
                                <asp:BoundField DataField="CodigoVar" HeaderText="CodigoVar" Visible="False" />
                                <asp:BoundField DataField="CodigoFac" HeaderText="CodigoFac" Visible="False" />
                                <asp:CommandField ShowSelectButton="True" 
                                    SelectImageUrl="../images/editar.gif" ButtonType="Image" />
                                <asp:CommandField ShowDeleteButton="True" 
                                    DeleteImageUrl="../images/eliminar.gif" ButtonType="Image" />
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#284775" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                        <%--</ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cmdGuardar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="gvSubvariable" EventName="RowDeleted" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </td>
                </tr>
            </table>
            
        </div>
    </div>
    </form>
</body>
</html>
