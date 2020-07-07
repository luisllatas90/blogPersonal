<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConfiguracionVariableCentroCosto.aspx.vb" Inherits="Indicadores_Formularios_frmConfiguracionVariableCentroCosto" %>

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
            height: 20px;
            width: 831px;            
        }
                
        .usatPanel
        {        
            border: 1px solid #C0C0C0;	        
	        height:70px;	        
	        -moz-border-radius: 15px; 	              
        }
                
        .usatPanel2
        {        
            border: 1px solid #C0C0C0;	        
	        height:470px;	        
	        -moz-border-radius: 15px; 
	        /*padding-top:10px;*/	        
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
        	top: -10px;
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
        	padding-top:10px;        	
        	-moz-border-radius: 15px;
        	padding-left:20px;        	
        }
        
        /********************************/                        
    </style>       
</head>
<body>
    <form id="form1" runat="server">
    <div class="usatTituloAzul">Configurar Centro de Costo y Variables
    <a href="#" onclick="apprise('Asigna un Centro de Costo a las Variables.');">
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
        <asp:Label ID="lblSubtitulo" runat="server" Text="Seleccione la variable" Width="150px"></asp:Label>
        <div>
            <div class="usatFormatoCampo">
            <asp:Label ID="lblVariable" runat="server" Text="Variable"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:DropDownList ID="ddlVariable" runat="server" Width="88%" 
                    AutoPostBack="True">
                </asp:DropDownList>    
                            
                <asp:CompareValidator ID="CompareValidator1" 
                                      runat="server" 
                                      ErrorMessage="Debe seleccionar una Variable." 
                                      ControlToValidate="ddlVariable" 
                                      Display="Dynamic" 
                                      Operator="NotEqual" 
                                      ValidationGroup="grupo1" 
                                      ValueToCompare="%"
                                      Text="*"
                                      >
                </asp:CompareValidator> 
            </div>
            <div style="clear:both; height:5px;"></div>        
        </div>           
    </div> 
    
    <div style="height:15px;"></div>        
    
    <div class="usatPanel2">
        <asp:Label ID="lblConsulta" runat="server" Text="Seleccione los Centros de Costos" Width="220px"></asp:Label>
       
        <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0"> 
            <tr style="background-color: #E8EEF7; font-weight: bold">    
                <td style="width:100%">
                      <div>
                        <div>
                            <div class="usatFormatoCampo">
                                <asp:Label ID="lblBuscar" runat="server" 
                                    Text="Filtrar el Listado de Centros de Costos"></asp:Label>
                            </div>
                            <div class="usatFormatoCampoAncho">
                                <asp:TextBox ID="txtCentroCosto" runat="server" Width="87%"></asp:TextBox>
                                
                                <!-- Para Validar Caja de Busqueda -->
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                                            runat="server" 
                                            ControlToValidate="txtCentroCosto" 
                                            Display="Dynamic" 
                                            ErrorMessage="Ingrese carácteres válidos para buscar el Centro de Costo." 
                                            ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ ,.]{3,}" 
                                            ValidationGroup="grupo1" 
                                            SetFocusOnError="true" 
                                            EnableTheming="True"
                                            Text="*"
                                            >
                </asp:RegularExpressionValidator>
                
                <asp:CustomValidator   ID="CustomValidator2" 
                                    runat="server" 
                                    ErrorMessage="Se encontraron palabras reservadas en la búsqueda del Centro de Costo." 
                                    ControlToValidate="txtCentroCosto" 
                                    Display="Dynamic"                                     
                                    onservervalidate="CustomValidator2_ServerValidate"
                                    Text="*" ValidationGroup="grupo1"
                                    >
                </asp:CustomValidator>   
                
                <!------------------------------------------------------------------------------------>
                                
                                 <asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="buscar" 
                                    ValidationGroup="grupo1" /> 
                            </div>                            
                        </div> 
                    </div> 
                </td>
            </tr>
            <tr style="background-color: #E8EEF7; font-weight: bold">    
                    <td style="width:80%; padding-left:30px">
                        <div style="height:10px"></div>
                        
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                            DisplayMode="BulletList" ShowSummary="true" HeaderText="Errores encontrados:" 
                            BorderWidth="0" ValidationGroup="grupo1" Font-Bold="False" 
                            ForeColor="#FF0066"/>
                        </td>                                                      
            </tr>
        </table>                                     
        
        <div style="height:5px;"></div>
        <div style="width:100%; margin-top:10px">
             <div style="float:left; width: 40%;">
                 <!-- <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0"> -->
                 <table cellpadding="3" cellspacing="0" style="width: 100%; border="0">
                        <tr>
                            <td style="text-align:center; font-weight:bold;">Listado de Centro de Costos</td>
                        </tr>
                        <tr>
                           <td style="text-align:center" align="center">
                                <asp:GridView ID="gvCCO1" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="codigo_cco" CellPadding="4" 
                                    Width="100%" ForeColor="#333333" GridLines="Both" AllowPaging="True">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        
                                        <asp:TemplateField HeaderText="CHECK">
                                             <ItemTemplate>
                                                <asp:CheckBox ID="chkSeleccion" runat="server" Width="5px" />
                                             </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="N°" />
                                        <asp:BoundField DataField="codigo_cco" HeaderText="Codigo" Visible="False" />                                        
                                        <asp:BoundField DataField="descripcion_Cco" HeaderText="DESCRIPCION" />
                                    
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
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
        
            <div style="float:left; width:19%; text-align:center">                                                            
                <asp:Button ID="btnAgregar" runat="server" Text="   &gt;&gt;    " 
                    CssClass="agregar" ValidationGroup="grupo1" />                                     
                <br /> 
                <asp:Button ID="btnQuitar" runat="server" Text="   &lt;&lt;    " 
                    CssClass="agregar" ValidationGroup="grupo1" /> 
            </div>
        
            <div style="float:left; width:40%">
             <!-- <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7"> -->
             <table cellpadding="3" cellspacing="0" style="width: 100%; border="0">
                    <tr>
                            <td style="text-align:center; font-weight:bold;">Centros de Costos Configurados para la Variable</td>
                        </tr>
                    <tr>
                        <td style="text-align:center" align="center">
                            <asp:GridView ID="gvCCO2" runat="server" AutoGenerateColumns="False"  Width="100%"
                                    CellPadding="4" DataKeyNames="Codigo" ForeColor="#333333" 
                                GridLines="both">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="CHECK">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSeleccion" runat="server" />
                                             </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="N°" />
                                        <asp:BoundField DataField="Codigo" HeaderText="Codigo" Visible="False" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
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
            <div style="clear:both; height:5px;"></div>
        </div>
    </div>
    </form>
</body>
</html>
