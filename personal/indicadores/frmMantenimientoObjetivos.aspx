<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMantenimientoObjetivos.aspx.vb" Inherits="Indicadores_Formularios_frmMantenimientoObjetivos" %>

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
	<script type="text/javascript" language="javascript">

	    function MarcarCursos(obj) {
	        //asignar todos los controles en array
	        var arrChk = document.getElementsByTagName('input');
	        for (var i = 0; i < arrChk.length; i++) {
	            var chk = arrChk[i];
	            //verificar si es Check
	            if (chk.type == "checkbox") {
	                chk.checked = obj.checked;
	                if (chk.id != obj.id) {
	                    PintarFilaMarcada(chk.parentNode.parentNode, obj.checked)
	                }
	            }
	        }
	    }

	    function PintarFilaMarcada(obj, estado) {
	        if (estado == true) {
	            obj.style.backgroundColor = "#FFE7B3"
	        } else {
	            obj.style.backgroundColor = "white"
	        }
	    }

	   
    </script>
	
	
      <style type="text/css">
        .usatFormatoCampo
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:20px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:170px;        	
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
        	width:530px;
        	/*border:1px solid red;*/
        }
        
        
        .usatFormatoCampoAnchoEjes
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:120px;
        	width:530px;
        	/*border:1px solid red;*/
        }
        
        .usatTituloAzul {
	        font-family: Arial;
	        font-size: 12pt;
	        font-weight: bold;
	        color: #3063c5;
            height: 19px;
            width: 831px;
            height: 18px;
        }
        
        .usatDescripcionTitulo  
        {
        	font-family: Arial;
	        font-size: 10pt;	        
	        color: #27333c;
        }
        
        .usatPanel
        {        
            border: 1px solid #C0C0C0;	        
	        height:185px;	        
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
        
        .usatPanel3
        {        
            border: 1px solid #C0C0C0;	        
	        height:206px;	        	        
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
        	position:absolute;
        	top: 68px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:120px;        
        	text-align:center;
        }
        
        .cuadroMensajes
        {
        	/*background-color:Red;*/
        	height:30px;
        	padding-top:10px;        	
        }
        
         #lblConsulta
        {
        	position:absolute;
        	top: 460px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:150px;        
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
        
        #lblEjes
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
    <div class="usatTituloAzul">Añadir Objetivos
    <a href="#" onclick="apprise('Añade un nuevo Objetivo..');">
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
        <div style="margin-top:10px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="Label3" runat="server" Text="Abreviatura"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtAbreviatura" runat="server" Width="70px" Font-Size="Smaller" 
                    MaxLength="10"></asp:TextBox>
                
                <asp:CustomValidator 
                    ID="CustomValidator3" 
                    runat="server" 
                    ErrorMessage="Se encontraron palabras reservadas en la abreviatura." 
                    controltovalidate="txtAbreviatura" 
                    onservervalidate="CustomValidator3_ServerValidate"
                    ValidationGroup="grupo1" 
                    Text="*">
                </asp:CustomValidator>
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                            runat="server" 
                                            ErrorMessage="Debe ingresar la Abreviatura" 
                                            ControlToValidate="txtDescripcion" 
                                            ValidationGroup="grupo1" 
                                            SetFocusOnError="true"
                                            EnableClientScript="true"  
                                            Text="*" 
                                            >
                </asp:RequiredFieldValidator>
                
                <asp:RegularExpressionValidator 
                    ID="RegularExpressionValidator2" runat="server" 
                    ControlToValidate="txtAbreviatura" Display="Dynamic" 
                    ErrorMessage="Ingrese carácteres válidos"
                    ValidationExpression="^[a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\-\.\s]*"
                    ValidationGroup="grupo1" 
                    SetFocusOnError="true" 
                    EnableClientScript="true" 
                    EnableTheming="True"
                    text="*">
                </asp:RegularExpressionValidator>
                    
                <asp:CustomValidator 
                    ID="CustomValidator4" 
                    runat="server" 
                    ErrorMessage="Ingresar valores válidos"
                    ControlToValidate="txtAbreviatura" 
                    Display="Dynamic"
                    ValidationGroup="grupo1"
                    SetFocusOnError="true"
                    EnableClientScript="true"  
                    Text="*"
                    >
                </asp:CustomValidator>  
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblDescripcion" runat="server" Text="Descripción"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtDescripcion" runat="server" Width="500px" Font-Size="Smaller" 
                    MaxLength="800"></asp:TextBox>
                
                <!--
                <asp:CustomValidator 
                    ID="CustomValidator2" 
                    runat="server" 
                    ErrorMessage="Se encontraron palabras reservadas en la descripción del periodo." 
                    controltovalidate="txtDescripcion" 
                    onservervalidate="CustomValidator2_ServerValidate"
                    ValidationGroup="grupo1" 
                    Text="*">
                </asp:CustomValidator>
                -->
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                            runat="server" 
                                            ErrorMessage="Debe ingresar la Descripción" 
                                            ControlToValidate="txtDescripcion" 
                                            ValidationGroup="grupo1" 
                                            SetFocusOnError="true"
                                            EnableClientScript="true"  
                                            Text="*" 
                                            >
                </asp:RequiredFieldValidator>
                
                <asp:RegularExpressionValidator 
                    ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtDescripcion" Display="Dynamic" 
                    ErrorMessage="Ingrese carácteres válidos"
                    ValidationExpression="^[a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\-\.\s]*"
                    ValidationGroup="grupo1" 
                    SetFocusOnError="true" 
                    EnableClientScript="true" 
                    EnableTheming="True"
                    text="*">
                </asp:RegularExpressionValidator>
                    
                <asp:CustomValidator 
                    ID="CustomValidator1" 
                    runat="server" 
                    ErrorMessage="Ingresar valores válidos"
                    ControlToValidate="txtDescripcion" 
                    Display="Dynamic"
                    ValidationGroup="grupo1"
                    SetFocusOnError="true"
                    EnableClientScript="true"  
                    Text="*"
                    >
                </asp:CustomValidator>  
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>    
        <div>
            <div style="clear:both; height:5px;"></div>
        </div>
        <div>
            <div class="usatFormatoCampo">
                <asp:Label ID="lblPerspectivaplan" runat="server" Text="Perspectiva - Plan"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:DropDownList ID="ddlPerspectivaplan" runat="server" Width="500px" 
                    Font-Size="XX-Small">
                </asp:DropDownList>
                
                <asp:CompareValidator 
                    ID="CompareValidator1" 
                    runat="server" 
                    ErrorMessage="Seleccione la Dirección de la Escala de Metas"
                    ControlToValidate="ddlPerspectivaplan" 
                    Display="Dynamic"
                    Text="<strong>*</strong>" 
                    SetFocusOnError="true" 
                    Type="Integer" 
                    Operator="GreaterThan"
                    ValueToCompare="0" 
                    ValidationGroup="grupo1">
                    
                </asp:CompareValidator>
                    
            </div>
            <div style="clear:both; height:5px;"></div>
        </div>    
        
        <div>
            <div class="usatFormatoCampo">
                <asp:Label ID="Label8" runat="server" Text="Meta (%)"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho" style="width:230px">
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
                <asp:Label ID="Label14" runat="server" Text="Del año"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho" style="width:130px;padding-left:12px">
                <asp:DropDownList ID="ddlAños" runat="server" ValidationGroup="group1" 
                Font-Size="Smaller" Width="118px"></asp:DropDownList>                
                           
                <asp:CompareValidator ID="CompareValidator4" runat="server" 
                    ErrorMessage="Seleccione el año de la Meta" ControlToValidate="ddlAños" 
                    Display="Dynamic" Operator="NotEqual" ValidationGroup="grupo1" 
                    ValueToCompare="0">*</asp:CompareValidator>               
            </div>

            <div style="clear:both; height:0px;"></div>
        </div>                   
    </div>
    
    <div style="clear:both; height:10px;"></div>  <!-- div de separacion-->        
    
    <div class="usatPanel2"> <!--  DIV del Panel de Registro de Metas -->
        <asp:Label ID="lblMetas" runat="server" Text="Parámetros de Semaforización" Width="200px">                    
        </asp:Label>
        
        <div>
            <div class="usatFormatoCampo">
            <asp:Label ID="Label9" runat="server" Text="Comportamiento del Objetivo"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
               
                <asp:DropDownList ID="ddlDireccionEscalaMetas" runat="server" Width="500px" 
                    Font-Size="Smaller">
                    <asp:ListItem Selected="True" Value="-">--SELECCIONE--</asp:ListItem>
                    <asp:ListItem>Ascendente</asp:ListItem>
                    <asp:ListItem>Descendente</asp:ListItem>
                </asp:DropDownList>
                
                <asp:CompareValidator 
                    ID="CompareValidator2" 
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
                <div style="float:left; width:80px; padding-top:10px; padding-left:30px">
                    <asp:Label ID="lblmin" runat="server" Text="Mínimo valor" Font-Bold="False"></asp:Label>
                </div>                    
               <div style="float:left;padding-top:5px">
                   <asp:TextBox ID="txtDesdeOptimo" runat="server" Font-Size="Smaller" 
                       BackColor="#CCFF66"></asp:TextBox>
                   
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                                            runat="server" 
                                            ControlToValidate="txtDesdeOptimo" 
                                            ErrorMessage="Debe ingresar el Mínimo Valor para la Meta Máxima." 
                                            ValidationGroup="grupo1" 
                                            Display="Dynamic"
                                            Text="*"
                                            > </asp:RequiredFieldValidator>  
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" 
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
               <div style="float:left; width:95px;"></div>  
               <div style="float:left; width:80px; padding-top:10px">
                   <asp:Label ID="Label7" runat="server" Text="Máximo valor" Font-Bold="False"></asp:Label>
               </div>
               <div style="float:left;padding-top:5px">
                   <asp:TextBox ID="txtHastaOptimo" runat="server" Font-Size="Smaller" 
                       BackColor="#CCFF66"></asp:TextBox>
                   
                   <asp:RequiredFieldValidator   ID="RequiredFieldValidator4" 
                                            runat="server" 
                                            ControlToValidate="txtHastaOptimo" 
                                            ErrorMessage="Debe ingresar el Máximo Valor para la Meta Máxima." 
                                            ValidationGroup="grupo1" 
                                            Display="Dynamic"
                                            Text="*"
                                            > </asp:RequiredFieldValidator>                                                     
                    
                    <asp:RegularExpressionValidator   ID="RegularExpressionValidator5" 
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
            
                <asp:Label ID="Label11" runat="server" Text="Status promedio (%)"></asp:Label>
            </div>
            <div> 
                <div style="float:left; width:80px; padding-top:10px; padding-left:30px">
                    <asp:Label ID="Label12" runat="server" Text="Mínimo valor" Font-Bold="False"></asp:Label></div>                    
               <div style="float:left;padding-top:5px">
                   <asp:TextBox ID="txtDesdeAmbar" runat="server" Font-Size="Smaller" 
                       BackColor="#FFCC00"></asp:TextBox>
                
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
                                                runat="server" 
                                                ControlToValidate="txtDesdeAmbar" 
                                                ErrorMessage="Debe ingresar el Mínimo Valor para la Meta Promedio." 
                                                ValidationGroup="grupo1" 
                                                Display="Dynamic"
                                                Text="*"
                                                > </asp:RequiredFieldValidator>                                                     
                        
                    <asp:RegularExpressionValidator   ID="RegularExpressionValidator6" 
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
               <div style="float:left; width:95px;"></div>  
               <div style="float:left; width:80px; padding-top:10px">
                   <asp:Label ID="Label13" runat="server" Text="Máximo valor" Font-Bold="False"></asp:Label></div>
               <div style="float:left;padding-top:5px">
                   <asp:TextBox ID="txtHastaAmbar" runat="server" Font-Size="Smaller" 
                       BackColor="#FFCC00"></asp:TextBox>                                                       
                   
                   <asp:RequiredFieldValidator  ID="RequiredFieldValidator6" 
                                                runat="server" 
                                                ControlToValidate="txtHastaAmbar" 
                                                ErrorMessage="Debe ingresar el Máximo Valor para la Meta Promedio." 
                                                ValidationGroup="grupo1" 
                                                Display="Dynamic"
                                                Text="*"
                                                > </asp:RequiredFieldValidator>                                                     
                        
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator7" 
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
            <asp:Label ID="Label4" runat="server" Text="Status mínimo (%)"></asp:Label>
            </div>
            <div> 
                <div style="float:left; width:80px; padding-top:10px; padding-left:30px">
                    <asp:Label ID="Label5" runat="server" Text="Mínimo valor" Font-Bold="False"></asp:Label></div>                    
               <div style="float:left;padding-top:5px">
                   <asp:TextBox ID="txtDesdeRojo" runat="server" Font-Size="Smaller" 
                       BackColor="#FF5050"></asp:TextBox>
                   
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator7" 
                                            runat="server" 
                                            ControlToValidate="txtDesdeRojo" 
                                            ErrorMessage="Debe ingresar el Mínimo Valor para la Meta Mínima." 
                                            ValidationGroup="grupo1" 
                                            Display="Dynamic"
                                            Text="*"
                                            > </asp:RequiredFieldValidator>                                                     
                    
                    <asp:RegularExpressionValidator   ID="RegularExpressionValidator8" 
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
               <div style="float:left; width:95px;"></div>  
               <div style="float:left; width:80px; padding-top:10px">
                   <asp:Label ID="Label6" runat="server" Text="Máximo valor" Font-Bold="False"></asp:Label></div>
               <div style="float:left;padding-top:5px; width: 96px; height: 21px;">
                   <asp:TextBox ID="txtHastaRojo" runat="server" Font-Size="Smaller" 
                       BackColor="#FF5050"></asp:TextBox>
                
                    <asp:RequiredFieldValidator  ID="RequiredFieldValidator8" 
                                            runat="server" 
                                            ControlToValidate="txtHastaRojo" 
                                            ErrorMessage="Debe ingresar el Máximo Valor para la Meta Mínima." 
                                            ValidationGroup="grupo1" 
                                            Display="Dynamic"
                                            Text="*"
                                            > </asp:RequiredFieldValidator>                                                     
                    
                    <asp:RegularExpressionValidator   ID="RegularExpressionValidator9" 
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
    </div>    
    
    <!---->
    
    <br />
    <div class="usatPanel3"> <!--  DIV del Panel de Registro de Metas -->
        <asp:Label ID="lblEjes" runat="server" Text="Configuración de Ejes" Width="150px">                    
        </asp:Label>
        
        <div>
            <div class="usatFormatoCampo">
            </div>
            <div class="usatFormatoCampoAnchoEjes">
               <table width="100%">
                <tr>
                    <td>
                             <asp:GridView ID="gvListaEjes" Width="100%" runat="server" BackColor="White" 
                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                                 CellPadding="3" AutoGenerateColumns="False" Font-Bold="False" 
                                 DataKeyNames="codigo_eje">
                                 <RowStyle ForeColor="#000066" />
                                 <Columns>
                                     <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkHeader" runat="server"  onclick="MarcarCursos(this)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>                
                                            <asp:CheckBox ID="chkElegir" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="5px" />
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="nombre_eje" HeaderText="Descripción" />
                                     <asp:BoundField DataField="codigo_eje" HeaderText="Codigo" Visible="False" />
                                 </Columns>
                                 <FooterStyle BackColor="White" ForeColor="#000066" />
                                 <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                 <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                 <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
               </table>
                
               
               
               
                
               
            </div>                                   
            <div style="clear:both; height:2px"></div>        
        </div>
        
        <div>
            <div class="usatFormatoCampo">
            </div>
            <div style="clear:both; height:2px"></div>        
        </div> 
                
        <div>                	
            <div class="usatFormatoCampo">
            
            </div>
            <div> 
                <div style="float:left; width:80px; padding-top:10px; padding-left:30px">
                    </div>                    
               <div style="float:left; width:95px;"></div>  
               <div style="float:left; width:80px; padding-top:10px">
                   </div>
            </div>
                                   
            <div style="clear:both; height:2px"></div>        
        </div>                   
               
        <div>
            <div class="usatFormatoCampo">
            </div>
            <div> 
                <div style="float:left; width:80px; padding-top:10px; padding-left:30px">
                    </div>                    
               <div style="float:left;padding-top:5px">
                
               </div>               
               <div style="float:left; width:95px;"></div>  
               <div style="float:left; width:80px; padding-top:10px">
                   </div>
               <div style="float:left;padding-top:5px; width: 96px; height: 21px;">
                    <div style="clear:both; height:10px"></div>       
             </div>                
                     
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
                        <td align="right" valign ="top"  style="width:20%">
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
    
        <div style="height:25px;">
        
             <div class="usatFormatoCampo">
                <asp:Label ID="Label15" runat="server" Text="Seleccione el Plan"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho" style="padding-top:0px">   
                <asp:DropDownList ID="ddlPlan2" runat="server" Width="98%" AutoPostBack="True" 
                    Font-Size="Smaller">
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
        
        <div style="height:25px;">
        
             <div class="usatFormatoCampo">
                <asp:Label ID="Label10" runat="server" Text="Seleccione Perspectiva"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho" style="padding-top:0px">   
                                        
                <!------------------------------------------------------------------------------------>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlPersBus" runat="server" ValidationGroup="group1" 
                        Font-Size="Smaller" Width="67%"></asp:DropDownList>                
                        &nbsp;&nbsp; <asp:Label ID="Label16" runat="server" Text="Año"></asp:Label>
                        &nbsp;<asp:DropDownList ID="ddlAnioBus" runat="server" ValidationGroup="group1" 
                        Font-Size="Smaller" Width="118px"></asp:DropDownList>                
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlPlan2" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                  
            </div>
        
        </div>
        
        <div style="height:65px;">
            <div class="usatFormatoCampo">
                <asp:Label ID="lblBuscar" runat="server" Text="Ingrese descripción del objetivo"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                    <asp:TextBox ID="txtBuscar" runat="server" Width="500px" ></asp:TextBox>
                    
                    <!-- Para Validar Caja de Busqueda -->
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
                                                runat="server" 
                                                ControlToValidate="txtBuscar" 
                                                Display="Dynamic" 
                                                ErrorMessage="Ingrese carácteres válidos para buscar el Objetivo." 
                                                ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ ,.]{3,}" 
                                                ValidationGroup="grupo2" 
                                                SetFocusOnError="true" 
                                                EnableTheming="True"
                                                Text="*"
                                                >
                    </asp:RegularExpressionValidator>
                
                    <asp:CustomValidator   ID="CustomValidator5" 
                                        runat="server" 
                                        ErrorMessage="Se encontraron palabras reservadas en la búsqueda del Objetivo." 
                                        ControlToValidate="txtBuscar" 
                                        Display="Dynamic"                                     
                                        onservervalidate="CustomValidator2_ServerValidate"
                                        Text="*" ValidationGroup="grupo2"
                                        >
                    </asp:CustomValidator>   
                
                    <!------------------------------------------------------------------------------------>
                    
                    <asp:Button ID="btnBuscar" runat="server" Text="   Buscar" 
                    CssClass="buscar" ValidationGroup="grupo2"/> <!-- Para Validar Caja de Busqueda -->
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
        
        <!------------------------------------------------------------------------------------>
        
        <div>
         <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
                <tr>                
                    <td style="text-align:center" align="center">
                        <asp:GridView ID="gvObjetivos" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="Codigo,codigo_ppla,DesdeOptimo,HastaOptimo,DesdeAmbar,HastaAmbar,DesdeRojo,HastaRojo,direccionescala_obj" 
                            EmptyDataText="No se encontraron registros para ese criterio de búsqueda." 
                            CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" 
                            BorderWidth="1px">
                            <RowStyle ForeColor="#000066" />
                            <EmptyDataRowStyle ForeColor="#FF0066" />
                            <Columns>
                                <asp:BoundField HeaderText="N°" >
                                <ItemStyle Width="15px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Codigo" DataField="Codigo" Visible="False" />                                               
                                <asp:BoundField HeaderText="Plan Perspectiva" DataField="PlanPerspectiva" >
                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                </asp:BoundField>                                                                
                                <asp:BoundField DataField="Abreviatura" HeaderText="Abr." />
                                <asp:BoundField DataField="Objetivo" HeaderText="Objetivos" >
                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CentroCosto" 
                                    HeaderText="Centro Costo" >
                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MetaObjetivo" HeaderText="Meta Objetivo" />                                                                
                                <asp:BoundField DataField="año_obj" HeaderText="Del Año" />
                                <asp:BoundField DataField="direccionescala" HeaderText="Dir." >
                                <ItemStyle Width="10px" />
                                </asp:BoundField>
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
                                
                                <asp:BoundField DataField="MetaMaxima" HeaderText="Meta Máxima" />
                                <asp:BoundField DataField="MetaPromedio" HeaderText="Meta Promedio" />
                                <asp:BoundField DataField="MetaMinima" HeaderText="Meta Mínima" />                                                                
                                <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistro" 
                                    Visible="False" >
                                <ItemStyle Width="155px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codigo_ppla" HeaderText="codigo_ppla" 
                                    Visible="False" />
                                <asp:CommandField ShowSelectButton="True" 
                                    SelectImageUrl="../images/editar.gif" ButtonType="Image" 
                                    SelectText="Modificar" >
                                <ItemStyle HorizontalAlign="Center" Width="15px" />
                                </asp:CommandField>
                                <asp:CommandField ShowDeleteButton="True" 
                                    DeleteImageUrl="../images/eliminar.gif" ButtonType="Image" >
                                <HeaderStyle Width="15px" />
                                </asp:CommandField>
                                <asp:BoundField DataField="direccionescala_obj" 
                                    HeaderText="direccionescala_obj" Visible="False" />
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#284775" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            
        </div>
        </div>
    </form>
</body>
</html>
