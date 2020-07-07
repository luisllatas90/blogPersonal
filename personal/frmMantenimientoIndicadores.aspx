<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMantenimientoIndicadores.aspx.vb" Inherits="Indicadores_Formularios_frmMantenimientoIndicadores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mantenimiento de Indicadores</title>
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
	        height:210px;	        
	        max-height:300PX;
	        -moz-border-radius: 15px; 
	        /*padding-top:10px;*/	  	            
        }
        
        .usatPanel2
        {        
            border: 1px solid #C0C0C0;	        
	        height:150px;	        	        
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
    <div class="usatTituloAzul">Añadir Indicadores
    <a href="#" onclick="apprise('Añade un nuevo Indicador.');">
        <img src="../Images/info.png" style="border:0" alt="Ayuda."/>
    </a>
    </div>
    
   <!-- Para avisos -->
    <div style="clear:both; height:5px"></div>
    
    <div id="avisos" runat="server" style="height:25px; padding-top:2px; width: 70%">
    <asp:Image ID="Image1" runat="server" ImageUrl="../Images/beforelastchild.GIF"/>
    <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>            
    </div>
    <div style="clear:both; height:10px"></div>
    <!-------------------------------------------->
    
    <div class="usatPanel">
        <asp:Label ID="lblSubtitulo" runat="server" Text="Datos de registro" 
            Width="120px"></asp:Label>
        
        <div>
            <div class="usatFormatoCampo">
            <asp:Label ID="Label10" runat="server" Text="Plan"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:DropDownList ID="ddlPlan" runat="server" Width="98%" 
                    AutoPostBack="True">
                </asp:DropDownList>    
                
                <asp:CompareValidator   ID="CompareValidator3" 
                                        runat="server" 
                                        ErrorMessage="Debe seleccionar el Plan." 
                                        ControlToValidate="ddlPlan" 
                                        Display="Dynamic" 
                                        Operator="NotEqual" 
                                        ValidationGroup="grupo1" 
                                        ValueToCompare="0"
                                        Text="*"
                                        > </asp:CompareValidator> 
                            
            </div>            
            <div style="clear:both; height:2px;"></div>                    
        </div>
        
        <div>
            <div class="usatFormatoCampo">
            <asp:Label ID="Label3" runat="server" Text="Perspectiva"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                
                <asp:DropDownList ID="ddlPerspectiva" runat="server" Width="98%" 
                    AutoPostBack="True">
                </asp:DropDownList>    
                
                <asp:CompareValidator   ID="CompareValidator1" 
                                        runat="server" 
                                        ErrorMessage="Debe seleccionar la Perspectiva." 
                                        ControlToValidate="ddlPerspectiva" 
                                        Display="Dynamic" 
                                        Operator="NotEqual" 
                                        ValidationGroup="grupo1" 
                                        ValueToCompare="0"
                                        Text="*"
                                        > </asp:CompareValidator> 
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlPlan" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>            
            </div>            
            <div style="clear:both; height:2px;"></div>                    
        </div>                  
                
        <div>
            <div class="usatFormatoCampo">
            <asp:Label ID="lblObjetivo" runat="server" Text="Objetivo"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <asp:DropDownList ID="ddlObjetivos" runat="server" Width="98%">
                </asp:DropDownList>   
                
                 <asp:CompareValidator   ID="CompareValidator7" 
                                        runat="server" 
                                        ErrorMessage="Debe seleccionar el Objetivo." 
                                        ControlToValidate="ddlObjetivos" 
                                        Display="Dynamic" 
                                        Operator="NotEqual" 
                                        ValidationGroup="grupo1" 
                                        ValueToCompare="0"
                                        Text="*"
                                        > </asp:CompareValidator>              
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlPerspectiva" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>               
            </div>           
            <div style="clear:both; height:2px;"></div>                    
        </div>   
                                         
        <div>
            <div class="usatFormatoCampo">
                <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción"></asp:Label>
            </div>
                                  
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtDescripcion" runat="server" Width="97%" 
                    ValidationGroup="grupo1" Font-Size="Smaller" MaxLength="8000"></asp:TextBox> 
                
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                            runat="server" 
                                            ControlToValidate="txtDescripcion" 
                                            ErrorMessage="Debe ingresar la Descripción." 
                                            ValidationGroup="grupo1" 
                                            Display="Dynamic"
                                            Text="*"
                                            > </asp:RequiredFieldValidator>                                          
                    
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                              runat="server" 
                                              ControlToValidate="txtDescripcion" 
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
                                    ErrorMessage="Se encontraron palabras reservadas en la descripción del Indicador." 
                                    ControlToValidate="txtDescripcion" 
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
                <asp:Label ID="lblPonderacion" runat="server" Text="Ponderación (%)"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtPonderacion" runat="server" ValidationGroup="grupo1" 
                    Width="97%" Font-Size="Smaller"></asp:TextBox>                
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                            runat="server" 
                                            ControlToValidate="txtPonderacion" 
                                            ErrorMessage="Debe ingresar la Ponderación." 
                                            ValidationGroup="grupo1" 
                                            Display="Dynamic"
                                            Text="*"
                                            > </asp:RequiredFieldValidator>                                                     
                    
              <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                                              runat="server" 
                                              ControlToValidate="txtPonderacion" 
                                              Display="Dynamic" 
                                              ErrorMessage="La Ponderación solo admite números enteros." 
                                              ValidationExpression="[1-9][0-9]*"
                                              ValidationGroup="grupo1" 
                                              SetFocusOnError="true" 
                                              EnableTheming="True"
                                              Text="*"
                                              > </asp:RegularExpressionValidator>--%>
             
             
             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                                                  runat="server" 
                                                  ControlToValidate="txtPonderacion" 
                                                  Display="Dynamic" 
                                                  ErrorMessage="Ingrese solo números en la Ponderación." 
                                                  ValidationExpression="^(\-)?(?=.*[0-9].*$)\d*(?:\.\d+)?$"
                                                  ValidationGroup="grupo1" 
                                                  SetFocusOnError="true" 
                                                  EnableTheming="True"
                                                  Text="*"
                                                  > </asp:RegularExpressionValidator>
                                                  
            </div>

            <div style="clear:both; height:0px;"></div>
        </div>     
        
        <div>
            <div class="usatFormatoCampo">
                <asp:Label ID="Label8" runat="server" Text="Meta"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho" style="width:110px;">
                <asp:TextBox ID="txtMeta" runat="server" ValidationGroup="grupo1" 
                    Width="47%" Font-Size="Smaller"></asp:TextBox>                
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" 
                                            runat="server" 
                                            ControlToValidate="txtMeta" 
                                            ErrorMessage="Debe ingresar la Meta." 
                                            ValidationGroup="grupo1" 
                                            Display="Dynamic"
                                            Text="*"
                                            > </asp:RequiredFieldValidator>                                                     
                    
             <asp:RegularExpressionValidator ID="RegularExpressionValidator10" 
                                                  runat="server" 
                                                  ControlToValidate="txtMeta" 
                                                  Display="Dynamic" 
                                                  ErrorMessage="Ingrese solo números en la Meta del Indicador." 
                                                  ValidationExpression="^(\-)?(?=.*[0-9].*$)\d*(?:\.\d+)?$"
                                                  ValidationGroup="grupo1" 
                                                  SetFocusOnError="true" 
                                                  EnableTheming="True"
                                                  Text="*"
                                                  > </asp:RegularExpressionValidator>
            </div>
            
            <div class="usatFormatoCampo" style="width:70px">
                <asp:Label ID="Label9" runat="server" Text="Tipo de Dato"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho" style="width:140px;padding-left:12px">
                <asp:DropDownList ID="ddlTipoDato" runat="server" ValidationGroup="group1" 
                Font-Size="Smaller">
                    <asp:ListItem Value="-">--SELECCIONE--</asp:ListItem>
                    <asp:ListItem>Cantidad</asp:ListItem>
                    <asp:ListItem>Porcentaje</asp:ListItem>
                    <asp:ListItem>Ratio</asp:ListItem>
                </asp:DropDownList>                
                
               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator10" 
                                            runat="server" 
                                            ControlToValidate="txtMeta" 
                                            ErrorMessage="Debe ingresar la Meta." 
                                            ValidationGroup="grupo1" 
                                            Display="Dynamic"
                                            Text="*"
                                            > </asp:RequiredFieldValidator>      --%>                                               
                
                <asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ErrorMessage="Seleccione un Tipo de Dato" ControlToValidate="ddlTipoDato" 
                    Display="Dynamic" Operator="NotEqual" ValidationGroup="grupo1" 
                    ValueToCompare="-">*</asp:CompareValidator>               
            </div>
            
            <div class="usatFormatoCampo" style="width:70px">
                <asp:Label ID="Label14" runat="server" Text="Del año"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho" style="width:130px;padding-left:12px">
                <asp:DropDownList ID="ddlAños" runat="server" ValidationGroup="group1" 
                Font-Size="Smaller"></asp:DropDownList>                
                           
                <asp:CompareValidator ID="CompareValidator4" runat="server" 
                    ErrorMessage="Seleccione el año de la Meta" ControlToValidate="ddlAños" 
                    Display="Dynamic" Operator="NotEqual" ValidationGroup="grupo1" 
                    ValueToCompare="0">*</asp:CompareValidator>               
            </div>


            <div style="clear:both; height:0px;"></div>
        </div>    
        
        <div>
            <div class="usatFormatoCampo">
                <asp:Label ID="Label17" runat="server" Text="Basal"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho" style="width:110px;">
                <asp:TextBox ID="txtBasal" runat="server" ValidationGroup="grupo1" 
                    Width="47%" Font-Size="Smaller"></asp:TextBox>                
                
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" 
                                            runat="server" 
                                            ControlToValidate="txtBasal" 
                                            ErrorMessage="Debe ingresar el Basal." 
                                            ValidationGroup="grupo1" 
                                            Display="Dynamic"
                                            Text="*"
                                            > </asp:RequiredFieldValidator>     --%>                                                
                    
             <asp:RegularExpressionValidator ID="RegularExpressionValidator11" 
                                                  runat="server" 
                                                  ControlToValidate="txtBasal" 
                                                  Display="Dynamic" 
                                                  ErrorMessage="Ingrese solo números en el Basal del Indicador." 
                                                  ValidationExpression="^(\-)?(?=.*[0-9].*$)\d*(?:\.\d+)?$"
                                                  ValidationGroup="grupo1" 
                                                  SetFocusOnError="true" 
                                                  EnableTheming="True"
                                                  Text="*"
                                                  > </asp:RegularExpressionValidator>
            </div>
                        
            <div style="clear:both; height:0px;"></div>
        </div>    
                          
    </div>                                     
    
    <div style="clear:both; height:10px;"></div>
    <div class="usatPanel2">
        <asp:Label ID="lblMetas" runat="server" Text="Parámetros de Semaforización" Width="200px"></asp:Label>
        
        <div>
            <div class="usatFormatoCampo">
            <asp:Label ID="Label16" runat="server" Text="Comportamiento del Indicador"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
               
                <asp:DropDownList ID="ddlDireccionEscalaMetas" runat="server" Width="500px">
                    <asp:ListItem Selected="True" Value="-">--SELECCIONE--</asp:ListItem>
                    <asp:ListItem>Ascendente</asp:ListItem>
                    <asp:ListItem>Descendente</asp:ListItem>
                </asp:DropDownList>
                
                <asp:CompareValidator 
                    ID="CompareValidator6" 
                    runat="server" 
                    ErrorMessage="Seleccione la Dirección de Escala de Metas."
                    ControlToValidate="ddlDireccionEscalaMetas" 
                    Display="Dynamic"
                    Text="<strong>*</strong>" 
                    SetFocusOnError="true" 
                    Type="String" 
                    Operator="NotEqual"
                    ValueToCompare="-" 
                    ValidationGroup="grupo1"></asp:CompareValidator>
               
            </div>                                   
            <div style="clear:both; height:2px"></div>        
        </div>
        
        <div>
            <div class="usatFormatoCampo">
            <asp:Label ID="Label2" runat="server" Text="Status máximo (%)"></asp:Label>
            </div>
            <div> 
                <div style="float:left; width:80px; padding-top:5px; padding-left:30px">
                    <asp:Label ID="Label1" runat="server" Text="Mínimo valor" Font-Bold="False"></asp:Label></div>                    
               <div style="float:left;">
                   <asp:TextBox ID="txtDesdeOptimo" runat="server" Font-Size="Smaller"></asp:TextBox>
                   
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                                            runat="server" 
                                            ControlToValidate="txtDesdeOptimo" 
                                            ErrorMessage="Debe ingresar el Mínimo Valor para la Meta Máxima." 
                                            ValidationGroup="grupo1" 
                                            Display="Dynamic"
                                            Text="*"
                                            > </asp:RequiredFieldValidator>  
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
                                                  runat="server" 
                                                  ControlToValidate="txtDesdeOptimo" 
                                                  Display="Dynamic" 
                                                  ErrorMessage="Ingrese solo números en el Mínimo Valor de la Meta Máxima." 
                                                  ValidationExpression="^(\-)?(?=.*[0-9].*$)\d*(?:\.\d+)?$"
                                                  ValidationGroup="grupo1" 
                                                  SetFocusOnError="true" 
                                                  EnableTheming="True"
                                                  Text="*"
                                                  > </asp:RegularExpressionValidator>
                
               </div>               
               <div style="float:left; width:130px;"></div>  
               <div style="float:left; width:80px; padding-top:5px">
                   <asp:Label ID="Label7" runat="server" Text="Máximo valor" Font-Bold="False"></asp:Label></div>
               <div style="float:left;">
                   <asp:TextBox ID="txtHastaOptimo" runat="server" Font-Size="Smaller"></asp:TextBox>
                   
                   <asp:RequiredFieldValidator   ID="RequiredFieldValidator4" 
                                            runat="server" 
                                            ControlToValidate="txtHastaOptimo" 
                                            ErrorMessage="Debe ingresar el Máximo Valor para la Meta Máxima." 
                                            ValidationGroup="grupo1" 
                                            Display="Dynamic"
                                            Text="*"
                                            > </asp:RequiredFieldValidator>                                                     
                    
              <asp:RegularExpressionValidator   ID="RegularExpressionValidator4" 
                                                runat="server" 
                                                ControlToValidate="txtHastaOptimo" 
                                                Display="Dynamic" 
                                                ErrorMessage="Ingrese solo números en el Máximo Valor de la Meta Máxima." 
                                                ValidationExpression="^(\-)?(?=.*[0-9].*$)\d*(?:\.\d+)?$"
                                                ValidationGroup="grupo1" 
                                                SetFocusOnError="true" 
                                                EnableTheming="True"
                                                Text="*"
                                                > </asp:RegularExpressionValidator> 
                    
             </div>                
            </div>                                   
            <div style="clear:both; height:2px"></div>        
        </div> 
                
        <div>                	
            <div class="usatFormatoCampo">
            
                <asp:Label ID="Label11" runat="server" Text="Status promedio (% meta)"></asp:Label>
            </div>
            <div> 
                <div style="float:left; width:80px; padding-top:5px; padding-left:30px">
                    <asp:Label ID="Label12" runat="server" Text="Mínimo valor" Font-Bold="False"></asp:Label></div>                    
               <div style="float:left;">
                   <asp:TextBox ID="txtDesdeAmbar" runat="server" Font-Size="Smaller"></asp:TextBox>
                
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
                                                runat="server" 
                                                ControlToValidate="txtDesdeAmbar" 
                                                ErrorMessage="Debe ingresar el Mínimo Valor para la Meta Promedio." 
                                                ValidationGroup="grupo1" 
                                                Display="Dynamic"
                                                Text="*"
                                                > </asp:RequiredFieldValidator>                                                     
                        
                    <asp:RegularExpressionValidator   ID="RegularExpressionValidator5" 
                                                    runat="server" 
                                                    ControlToValidate="txtDesdeAmbar" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="Ingrese solo números en el Mínimo Valor de la Meta Promedio" 
                                                    ValidationExpression="^(\-)?(?=.*[0-9].*$)\d*(?:\.\d+)?$"
                                                    ValidationGroup="grupo1" 
                                                    SetFocusOnError="true" 
                                                    EnableTheming="True"
                                                    Text="*"
                                                    > </asp:RegularExpressionValidator>
                
               </div>               
               <div style="float:left; width:130px;"></div>  
               <div style="float:left; width:80px; padding-top:5px">
                   <asp:Label ID="Label13" runat="server" Text="Máximo valor" Font-Bold="False"></asp:Label></div>
               <div style="float:left;">
                   <asp:TextBox ID="txtHastaAmbar" runat="server" Font-Size="Smaller"></asp:TextBox>                                                       
                   
                   <asp:RequiredFieldValidator  ID="RequiredFieldValidator6" 
                                                runat="server" 
                                                ControlToValidate="txtHastaAmbar" 
                                                ErrorMessage="Debe ingresar el Máximo Valor para la Meta Promedio." 
                                                ValidationGroup="grupo1" 
                                                Display="Dynamic"
                                                Text="*"
                                                > </asp:RequiredFieldValidator>                                                     
                        
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" 
                                                  runat="server" 
                                                  ControlToValidate="txtHastaAmbar" 
                                                  Display="Dynamic" 
                                                  ErrorMessage="Ingrese solo números en el Máximo Valor de la Meta Promedio." 
                                                  ValidationExpression="^(\-)?(?=.*[0-9].*$)\d*(?:\.\d+)?$"
                                                  ValidationGroup="grupo1" 
                                                  SetFocusOnError="true" 
                                                  EnableTheming="True"
                                                  Text="*"
                                                  > </asp:RegularExpressionValidator>
                    
             </div>                
            </div>
                                   
            <div style="clear:both; height:2px"></div>        
        </div>                   
               
        <div>
            <div class="usatFormatoCampo">
            <asp:Label ID="Label4" runat="server" Text="Status mínimo (% basal)"></asp:Label>
            </div>
            <div> 
                <div style="float:left; width:80px; padding-top:5px; padding-left:30px">
                    <asp:Label ID="Label5" runat="server" Text="Mínimo valor" Font-Bold="False"></asp:Label></div>                    
               <div style="float:left;">
                   <asp:TextBox ID="txtDesdeRojo" runat="server" Font-Size="Smaller"></asp:TextBox>
                   
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator7" 
                                            runat="server" 
                                            ControlToValidate="txtDesdeRojo" 
                                            ErrorMessage="Debe ingresar el Mínimo Valor para la Meta Mínima." 
                                            ValidationGroup="grupo1" 
                                            Display="Dynamic"
                                            Text="*"
                                            > </asp:RequiredFieldValidator>                                                     
                    
                    <asp:RegularExpressionValidator   ID="RegularExpressionValidator7" 
                                                runat="server" 
                                                ControlToValidate="txtDesdeRojo" 
                                                Display="Dynamic" 
                                                ErrorMessage="Ingrese solo números en el Mínimo Valor de la Meta Mínima." 
                                                ValidationExpression="^(\-)?(?=.*[0-9].*$)\d*(?:\.\d+)?$"
                                                ValidationGroup="grupo1" 
                                                SetFocusOnError="true" 
                                                EnableTheming="True"
                                                Text="*"
                                                > </asp:RegularExpressionValidator>
                
               </div>               
               <div style="float:left; width:130px;"></div>  
               <div style="float:left; width:80px; padding-top:5px">
                   <asp:Label ID="Label6" runat="server" Text="Máximo valor" Font-Bold="False"></asp:Label></div>
               <div style="float:left;">
                   <asp:TextBox ID="txtHastaRojo" runat="server" Font-Size="Smaller"></asp:TextBox>
                
                    <asp:RequiredFieldValidator  ID="RequiredFieldValidator8" 
                                            runat="server" 
                                            ControlToValidate="txtHastaRojo" 
                                            ErrorMessage="Debe ingresar el Máximo Valor para la Meta Mínima." 
                                            ValidationGroup="grupo1" 
                                            Display="Dynamic"
                                            Text="*"
                                            > </asp:RequiredFieldValidator>                                                     
                    
                    <asp:RegularExpressionValidator   ID="RegularExpressionValidator8" 
                                                runat="server" 
                                                ControlToValidate="txtHastaRojo" 
                                                Display="Dynamic" 
                                                ErrorMessage="Ingrese solo números en el Máximo Valor de la Meta Mínima." 
                                                ValidationExpression="^(\-)?(?=.*[0-9].*$)\d*(?:\.\d+)?$"
                                                ValidationGroup="grupo1" 
                                                SetFocusOnError="true" 
                                                EnableTheming="True"
                                                Text="*" Width="120px"
                                                ></asp:RegularExpressionValidator>                 
                    <div style="clear:both; height:10px"></div>       
             </div>                
                     
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
        <asp:Label ID="lblConsulta" runat="server" Text="Consulta de Indicadores" Width="160px"></asp:Label>
        <div style="height:25px;">
        
             <div class="usatFormatoCampo">
                <asp:Label ID="Label15" runat="server" Text="Seleccione el Plan"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho" style="padding-top:0px">   
                <asp:DropDownList ID="ddlPlan2" runat="server" Width="98%">
                </asp:DropDownList>        
                
                <asp:CompareValidator   ID="CompareValidator5" 
                                        runat="server" 
                                        ErrorMessage="Debe seleccionar el Plan." 
                                        ControlToValidate="ddlPlan2" 
                                        Display="Dynamic" 
                                        Operator="NotEqual" 
                                        ValidationGroup="grupo2" 
                                        ValueToCompare="0"
                                        Text="*"
                                        > </asp:CompareValidator> 
                                        
                <!------------------------------------------------------------------------------------>
                 
            </div>
        
        </div>
        <div>
                 <div class="usatFormatoCampo">
                <asp:Label ID="lblBuscar" runat="server" Text="Ingrese el nombre del indicador"></asp:Label>
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
                <div style="padding-left:85px; float:left">
                    <asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="buscar" 
                        ValidationGroup="grupo2" /> <!-- Para Validar Caja de Busqueda --> 
                </div>
        </div>
        
        <div style="height:30px;">
        
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
                            
                            
                            DataKeyNames="Codigo,codigo_obj,DesdeOptimo,HastaOptimo,DesdeAmbar,HastaAmbar,DesdeRojo,HastaRojo,CodigoPerspectiva,codigo_pla,año_ind">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField HeaderText="N°" >
                                <ItemStyle Width="15px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Codigo" DataField="Codigo" Visible="False" />                                               
                                <asp:BoundField DataField="descripcionplan" HeaderText="Plan" /> 
                                <asp:BoundField DataField="AbreviaturaObj" HeaderText="Objetivo" />
                                <asp:BoundField HeaderText="Indicador" DataField="Descripcion" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>                                                                
                                <asp:BoundField DataField="basal_ind" HeaderText="Basal" />
                                <asp:BoundField DataField="MetaIndicador" HeaderText="Meta Indicador" />
                                <asp:BoundField DataField="TipoDato" HeaderText="Tipo Dato" />
                                
                                <asp:BoundField HeaderText="Año de la Meta" DataField="año_ind" />
                                
                                <asp:BoundField DataField="direccionescala" HeaderText="Dirección Escala de Metas" />
                                
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
                                <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistro" />                                
                                 <asp:BoundField DataField="codigo_pla" HeaderText="codigo_pla" 
                                    Visible="False" />
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
