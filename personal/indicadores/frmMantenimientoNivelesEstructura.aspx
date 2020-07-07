<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMantenimientoNivelesEstructura.aspx.vb" Inherits="indicadores_frmMantenimientoNivelesEstructura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mantenimiento de Niveles de Estructura</title>
<link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
<script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
<script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
<link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .usatFormatoCampo
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:5px;        	
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
        	padding-top:5px;        
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:600px;
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
	        height:160px;	        
	        max-height:300PX;
	        -moz-border-radius: 15px; 
	        /*padding-top:10px;*/	  	            
        }
        
        .usatPanel2
        {        
            border: 1px solid #C0C0C0;	        
	        height:110px;	        	        
	        -moz-border-radius: 15px	        
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
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	/*width:60px;        */
        	text-align:center;
        }
        
         #lblMetas
        {
        	position:relative;
        	top: -10px;
        	left: 20px;
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="usatTituloAzul">Añadir Niveles de Estructura para Variables
    <a href="#" onclick="apprise('La estructura de las Variables mantiene la siguiente jerarquía: Variable, Subvariale, Dimensión y Subdimensión. En esta pantalla podrás ingresar los niveles para los tres últimos elementos.');">
        <img src="../Images/info.png" style="border:0" alt="Ayuda."/>
    </a>
    </div>
    
   <!-- Para avisos -->
    <div style="clear:both; height:5px"></div>
    
    <div id="avisos" runat="server" style="height:25px; padding-top:2px; width: 50%">
    <asp:Image ID="Image1" runat="server" ImageUrl="../Images/beforelastchild.GIF"/>
    <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>            
    </div>
    <div style="clear:both; height:10px"></div>
    <!-------------------------------------------->
    
    <div class="usatPanel">
        <asp:Label ID="lblSubtitulo" runat="server" Text="Niveles de las Subvariables" 
            Width="180px"></asp:Label>                
                                         
        <div>
            <div class="usatFormatoCampo">
                <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblDescripcionAux" runat="server" Text="Descripción"></asp:Label>
            </div>
                                  
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtDescripcionAux" runat="server" Width="97%" 
                    ValidationGroup="grupo1" Font-Size="Smaller" MaxLength="100"></asp:TextBox> 
                
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                            runat="server" 
                                            ControlToValidate="txtDescripcionAux" 
                                            ErrorMessage="Debe ingresar la Descripción." 
                                            ValidationGroup="grupo1" 
                                            Display="Dynamic"
                                            Text="*"
                                            > </asp:RequiredFieldValidator>                                          
                    
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                              runat="server" 
                                              ControlToValidate="txtDescripcionAux" 
                                              Display="Dynamic" 
                                              ErrorMessage="Solo puede ingresar letras, números y los símbolos (.) y (-) en la Descripción." 
                                              ValidationExpression="^[a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\-\.\s]*" 
                                              ValidationGroup="grupo1" 
                                              SetFocusOnError="true" 
                                              EnableTheming="True"
                                              Text="*"
                                              > </asp:RegularExpressionValidator>
                                      
                <asp:CustomValidator ID="CustomValidator1" 
                                    runat="server" 
                                    ErrorMessage="Se encontraron palabras reservadas en la descripción del Nivel de Subvariable." 
                                    ControlToValidate="txtDescripcionAux" 
                                    Display="Dynamic" 
                                    ValidationGroup="grupo1"
                                    onservervalidate="CustomValidator1_ServerValidate"
                                    Text="*"
                                    >
               </asp:CustomValidator>          
            </div>  
            <div style="clear:both"></div>                        
        </div>
        
        <div>
            <div class="usatFormatoCampo">
                <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text="Abreviatura"></asp:Label>
            </div>
                                  
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtAbreviaturaAux" runat="server" Width="15px" 
                    ValidationGroup="grupo1" Font-Size="Smaller" MaxLength="2"></asp:TextBox> 
                
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                            runat="server" 
                                            ControlToValidate="txtAbreviaturaAux" 
                                            ErrorMessage="Debe ingresar la Abreviatura." 
                                            ValidationGroup="grupo1" 
                                            Display="Dynamic"
                                            Text="*"
                                            > </asp:RequiredFieldValidator>                                          
                    
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                                              runat="server" 
                                              ControlToValidate="txtAbreviaturaAux" 
                                              Display="Dynamic" 
                                              ErrorMessage="Solo puede ingresar letras, números y los símbolos (.) y (-) en la Abreviatura." 
                                              ValidationExpression="^[a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\-\.\s]*" 
                                              ValidationGroup="grupo1" 
                                              SetFocusOnError="true" 
                                              EnableTheming="True"
                                              Text="*"
                                              > </asp:RegularExpressionValidator>
                                      
                <asp:CustomValidator ID="CustomValidator3" 
                                    runat="server" 
                                    ErrorMessage="Se encontraron palabras reservadas en la Abreviatura del Nivel de Subvariable." 
                                    ControlToValidate="txtDescripcionAux" 
                                    Display="Dynamic" 
                                    ValidationGroup="grupo1"
                                    onservervalidate="CustomValidator1_ServerValidate"
                                    Text="*"
                                    >
               </asp:CustomValidator>          
            </div>  
            <div style="clear:both"></div>                        
        </div>
                        
        <div style="height:100px">
           <div class="usatFormatoCampo" style="height:120px; width:200px">            
                <asp:Label ID="Label3" runat="server" Text="Configuración de Niveles de Dimensión"></asp:Label>
            </div>            
            <div class="usatFormatoCampoAncho" style="width:585px">
                
                         <asp:GridView ID="gvNivelesDim" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="Codigo,Abreviatura,codigo_naux" CellPadding="4" 
                                    Width="100%" BackColor="White" BorderColor="White" 
                    Font-Bold="False">
                                    <RowStyle BackColor="White" ForeColor="#333333" BorderColor="White" />
                                    <Columns>                                        
                                        <asp:TemplateField HeaderText="CHECK">
                                             <ItemTemplate>
                                                <asp:CheckBox ID="chkSeleccion2" runat="server" Width="5px" AutoPostBack="true"/>
                                             </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:BoundField DataField="Codigo" HeaderText="Codigo" Visible="False" />
                                        
                                        <asp:BoundField DataField="Abreviatura" HeaderText="Abreviatura" Visible="False" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                    
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>                    
            </div>
            <div style="float:left; padding-top:20px;">
                <asp:Label ID="lblND" runat="server" Text=""></asp:Label>
            </div>   
                  
        </div>  
                          
    </div>
    <div style="clear:both; height:10px;"></div>
       
    <div>    
        
        <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0"> 
            <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">    
                    <td style="width:80%">
                
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        DisplayMode="BulletList" ShowSummary="true" HeaderText="Errores encontrados:" 
                        BorderWidth="0" ValidationGroup="grupo1" Font-Bold="False" 
                        ForeColor="#FF0066"/>
                    </td>
                                
                       <td align="right" style="width:20%" valign="top">
                                
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
        <asp:Label ID="lblConsulta" runat="server" Text="Consulta de Niveles de Estructura" Width="160px"></asp:Label>
        <div style="height:50px;">
            <div class="usatFormatoCampo">
                <asp:Label ID="lblBuscar" runat="server" Text="Ingrese el nombre del nivel"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho" style="padding-top:0px">            
                <asp:TextBox ID="txtBuscar" runat="server" Width="97%" Font-Size="Smaller"></asp:TextBox>
                
                <!-- Para Validar Caja de Busqueda -->
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator9" 
                                            runat="server" 
                                            ControlToValidate="txtBuscar" 
                                            Display="Dynamic" 
                                            ErrorMessage="Ingrese carácteres válidos para buscar el Indicador." 
                                            ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ ,.]{3,}" 
                                            ValidationGroup="grupo2" 
                                            SetFocusOnError="true" 
                                            EnableTheming="True"
                                            Text="*"
                                            >
                </asp:RegularExpressionValidator>
                
                <asp:CustomValidator   ID="CustomValidator2" 
                                    runat="server" 
                                    ErrorMessage="Se encontraron palabras reservadas en la búsqueda del Indicador." 
                                    ControlToValidate="txtBuscar" 
                                    Display="Dynamic"                                     
                                    onservervalidate="CustomValidator2_ServerValidate"
                                    Text="*" ValidationGroup="grupo2"
                                    >
                </asp:CustomValidator>   
                
                <!------------------------------------------------------------------------------------>
                 
            </div>
            <div>
            </div>
            <div style="padding-left:85px; float:left">
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
                        <asp:GridView ID="gvIndicadores" runat="server" Width="100%" CellPadding="4" 
                            ForeColor="#333333" AutoGenerateColumns="False" 
                            
                            DataKeyNames="Codigo,codigo_obj,DesdeOptimo,HastaOptimo,DesdeAmbar,HastaAmbar,DesdeRojo,HastaRojo,CodigoPerspectiva">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField HeaderText="N°" >
                                <ItemStyle Width="15px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Codigo" DataField="Codigo" Visible="False" />                                               
                                <asp:BoundField DataField="AbreviaturaObj" HeaderText="Objetivo" />
                                <asp:BoundField HeaderText="Indicador" DataField="Descripcion" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>                                                                
                                <asp:BoundField DataField="MetaIndicador" HeaderText="Meta Indicador" />
                                <asp:BoundField DataField="Ponderacion" HeaderText="Ponderación" />
                                <asp:BoundField DataField="codigo_obj" HeaderText="codigo_obj" 
                                    Visible="False" />
                                <asp:BoundField DataField="DesdeOptimo" HeaderText="DesdeOptimo" 
                                    Visible="False" />
                                <asp:BoundField DataField="HastaOptimo" HeaderText="HastaOptimo" 
                                    Visible="False" />
                                <asp:BoundField DataField="DesdeAmbar" HeaderText="DesdeAmbar" 
                                    Visible="False" />
                                <asp:BoundField DataField="HastaAmbar" HeaderText="HastaAmbar" 
                                    Visible="False" />
                                <asp:BoundField DataField="DesdeRojo" HeaderText="DesdeRojo" Visible="False" />
                                <asp:BoundField DataField="HastaRojo" HeaderText="HastaRojo" Visible="False" />
                                <asp:BoundField DataField="CodigoPerspectiva" HeaderText="CodigoPerspectiva" 
                                    Visible="False" />
                                <asp:BoundField DataField="MetaMaxima" HeaderText="Meta Máxima" />
                                <asp:BoundField DataField="MetaPromedio" HeaderText="Meta Promedio" />
                                <asp:BoundField DataField="MetaMinima" HeaderText="Meta Mínima" />
                                <asp:BoundField DataField="TipoDato" HeaderText="Tipo Dato" />
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
                    </td>
                </tr>
            </table>
            
        </div>
    </div>
    </form>
</body>
</html>
