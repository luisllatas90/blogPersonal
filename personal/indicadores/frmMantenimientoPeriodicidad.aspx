<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMantenimientoPeriodicidad.aspx.vb" Inherits="Indicadores_Formularios_frmMantenimientoPeriodicidad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
    <script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
    <link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .usatFormatoCampo
        {        	
        	float:left; 
        	font-weight:bold;
        	padding-top:30px;        	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:200px;    
        }
        
        .usatFormatoCampoAncho
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:25px;        	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	/*width:585px;*/
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
	        height: 80px;	        
	        -moz-border-radius: 15px; 
	        
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
        	position:absolute;
        	top: 70px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:120px;        
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
    <div class="usatTituloAzul">Añadir Periodicidad
    <a href="#" onclick="apprise('Añade una nueva periodicidad.');">
        <img src="../Images/info.png" style="border:0" alt="Ayuda."/>
    </a>
    </div>
    <!-- Para avisos -->
    <div style="clear:both; height:5px"></div>
    
    <div id="avisos" runat="server" style="height:25px; padding-top:2px; width: 50%;">
    <asp:Image ID="Image1" runat="server" ImageUrl="../Images/beforelastchild.GIF"/>
    <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>            
    </div>
    <div style="clear:both; height:10px"></div>
    <!-------------------------------------------->
    
    <div class="usatPanel">
        <asp:Label ID="lblSubtitulo" runat="server" Text="Datos de registro"></asp:Label>
        <div class="usatFormatoCampo">
            <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:"></asp:Label></div>
        <div class="usatFormatoCampoAncho">
            <asp:TextBox ID="txtDescripcion" runat="server" Width="83%" MaxLength="200"></asp:TextBox>
            
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtDescripcion" 
                ErrorMessage="Debe ingresar la Descripción." ValidationGroup="grupo1">*
                </asp:RequiredFieldValidator>
                
                 <asp:RegularExpressionValidator    ID="RegularExpressionValidator1" 
                                                    runat="server" 
                                                    ControlToValidate="txtDescripcion" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="Ingrese carácteres válidos en la Descripción." 
                                                    ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ ,.]{3,}" 
                                                    ValidationGroup="grupo1" 
                                                    SetFocusOnError="true" 
                                                    EnableTheming="True"
                                                    Text = "*"
                                                    >
                </asp:RegularExpressionValidator>
                                      
               <asp:CustomValidator ID="CustomValidator1" 
                                    runat="server" 
                                    ErrorMessage="Se encontraron palabras reservadas en la Descripción de la Periodicidad." 
                                    ControlToValidate="txtDescripcion" 
                                    Display="Dynamic" 
                                    ValidationGroup="grupo1"
                                    onservervalidate="CustomValidator1_ServerValidate"
                                    Text="*"
                                    >
               </asp:CustomValidator>                                        
        </div>
    </div>
    
    <div>    
        
        <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0"> 
            <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                <td style="width:80%">
                
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        DisplayMode="BulletList" ShowSummary="true" HeaderText="Errores encontrados:" 
                        BorderWidth="0" ValidationGroup="grupo1" Font-Bold="False" 
                        ForeColor="#FF0066"/>
                        
                    </td>
                        <td align="right" style="width:20%">
                        
                                <asp:Button ID="cmdGuardar" runat="server" Text="   Guardar" 
                                    CssClass="guardar2" ValidationGroup="grupo1" /> &nbsp;<asp:Button 
                                    ID="cmdCancelar" runat="server" Text="Limpiar" CssClass="regresar2" 
                                    Width="70px" />
                </td>
            </tr>
        </table>
        <br />       
    </div>
    
    <div class="usatPanelConsulta">
        <asp:Label ID="lblConsulta" runat="server" Text="Consulta de Periodicidad" 
            Width="170px"></asp:Label>
        <div style="height:50px">
            <div class="usatFormatoCampo">
                <asp:Label ID="lblBuscar" runat="server" Text="Ingrese parámetro de búsqueda"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtBuscar" runat="server" Width="83%"></asp:TextBox>
                
                <!-- Para Validar Caja de Busqueda -->
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                                            runat="server" 
                                            ControlToValidate="txtBuscar" 
                                            Display="Dynamic" 
                                            ErrorMessage="Ingrese carácteres válidos para buscar la Periodicidad." 
                                            ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ ,.]{3,}" 
                                            ValidationGroup="grupo2" 
                                            SetFocusOnError="true" 
                                            EnableTheming="True"
                                            Text="*"
                                            >
                </asp:RegularExpressionValidator>
                
                <asp:CustomValidator   ID="CustomValidator2" 
                                    runat="server" 
                                    ErrorMessage="Se encontraron palabras reservadas en la búsqueda de la Periodicidad." 
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
     <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
            <tr>
                <td style="text-align:center">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                                        
                    <asp:GridView ID="gvPeriodicidad" runat="server" Width="100%" CellPadding="4" 
                        ForeColor="#333333" GridLines="both" AutoGenerateColumns="False" 
                        DataKeyNames="Codigo">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField HeaderText="N°" />
                            <asp:BoundField HeaderText="Codigo" DataField="Codigo" Visible="False" />
                            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                            <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistro" />
                            <asp:CommandField ShowSelectButton="True" 
                                SelectImageUrl="../images/editar.gif" ButtonType="Image" />
                            <asp:CommandField ShowDeleteButton="True" 
                                DeleteImageUrl="../images/eliminar.gif" ButtonType="Image" />
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                    
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
   </div>
    </form>
</body>
</html>
