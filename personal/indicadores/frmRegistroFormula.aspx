<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistroFormula.aspx.vb" Inherits="Indicadores_Formularios_frmRegistroFormula" %>

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
         .menuseleccionado
            {
                background-color: #FFCC66;
                border: 1px solid #808080;  
            }   
            
            .menuporelegir
            {
                border: 1px solid #808080;
                background-color: #FFCC66;
            }
     </style>
        
       
    <style type="text/css">
        .usatFormatoCampo
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:50px;
        	color:#27333c;
        	font-family: Arial;
        	height:15px;
        	width:200px;        	
        }
        
        .usatPanelConsultaF
        {
         /*border: 1px solid #C0C0C0;	        	        */
            border: 1px solid #96ACE7;	
	        /*-moz-border-radius: 15px; */
/*	        padding-top:10px;*/
	        margin-top:20px; 
	        padding-bottom:10px;	
	        background-color: #FFFFFF;
	        height:125px;		
        }
        
        .usatFormatoCampoFormula
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:30px;
        	width:200px;        	
        }
        
        .usatFormatoCampoAnchoFormula
        {
        	float:left; 
        	font-weight:bold;
        	/*padding-top:10px;        	*/
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:30px;
        	width:94%;
        	/*border:1px solid red;*/
        }
        
        .usatFormatoCampoAncho
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:15px;
        	width:600px;
        	/*border:1px solid red;*/
        }
        
        .usatTituloAzul {
	        font-family: Arial;
	        font-size: 12pt;
	        font-weight: bold;
	        color: #3063c5;
            height: 19px;
            width: 831px;
            
        }
        
        .usatDescripcionTitulo  
        {
        	font-family: Arial;
	        font-size: 10pt;	        
	        color: #27333c;
        }
        
        .usatPanelFormula
        {        
            border: 1px solid #C0C0C0;	        
	        height:80px;	        
	        -moz-border-radius: 15px; 
	        /*padding-top:10px;*/	        
        }
        
        .usatPanel
        {        
            border: 1px solid #C0C0C0;	        
	        height:169px;	        
	        -moz-border-radius: 15px; 
	        /*padding-top:10px;*/	        
        }
        
        
        .usatPanel2
        {        
            border: 1px solid #C0C0C0;	        
	        height:100%;	        
	        -moz-border-radius: 15px; 
	        /*padding-top:10px;*/	        
        }
        
        .usatPanelConsultaFormula
        {
        	 /*border: 1px solid #C0C0C0;	        	        */
            border: 1px solid #96ACE7;	
	        /*-moz-border-radius: 15px; */
/*	        padding-top:10px;*/
	        margin-top:20px; 
	        padding-bottom:10px;	
	        background-color: #E8EEF7;
	        height:25px;
        	
        
        
        }
        
        .usatPanelConsulta
        {        
            /*border: 1px solid #C0C0C0;	        	        */
            border: 1px solid #96ACE7;	
	        /*-moz-border-radius: 15px; */
/*	        padding-top:10px;*/
	        margin-top:20px; 
	        padding-bottom:10px;	
	        background-color: #E8EEF7;
	        height:30px;
	    }
	    
	    .usatPanelConsultaErr
	    {        
            /*border: 1px solid #C0C0C0;	        	        */
            border: 1px solid #96ACE7;	
	        /*-moz-border-radius: 15px; */
/*	        padding-top:10px;*/
	        margin-top:20px; 
	        padding-bottom:10px;	
	        background-color: #E8EEF7;
	        height:15px;
	    }
	    
        #lblFormula
        {
        	position:relative;
        	top: -10px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:200px;        
        	text-align:center;
        
        
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
        	width:150px;        
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
        	top: 190px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:220px;        
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
    <div class="usatTituloAzul">Registro de Fórmulas
    <a href="#" onclick="apprise('Permite registrar una determinada fórmula para un indicador.');">
        <img src="../Images/info.png" style="border:0" alt="Ayuda."/>
    </a>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
    </div>
     <!-- Para avisos -->
    <div style="clear:both; height:5px"></div>
    
    <div id="avisos" runat="server" style="height:25px; padding-top:2px; width: 60%;">
    <asp:Image ID="Image1" runat="server" ImageUrl="../Images/beforelastchild.GIF"/>
    <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>            
    </div>
    <div style="clear:both; height:10px"></div>
    <!-------------------------------------------->
    
    
    <div class="usatPanel">
        <asp:Label ID="lblSubtitulo" runat="server" Text="Filtrar Indicador"></asp:Label>
        <div style="margin-top:10px;">
        <div class="usatFormatoCampo">
            <asp:Label ID="Label9" runat="server" Text="Seleccione un Plan"></asp:Label>
               <asp:Label ID="lblCodigo" runat="server" Text="Label" ForeColor="#FF3399" 
                    Visible="False"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:DropDownList ID="ddlPlan" runat="server" Width="580px" 
                    AutoPostBack="True" Font-Size="Smaller" BackColor="#CCFFFF">
                </asp:DropDownList>
                <asp:CompareValidator 
                    ID="CompareValidator4" 
                    runat="server" 
                    ErrorMessage="(*)  Seleccione el Plan." 
                    ControlToValidate="ddlPlan" 
                    Display="Dynamic" 
                    Operator="NotEqual" 
                    SetFocusOnError="true" 
                    ValidationGroup="grupo1" 
                    ValueToCompare="0"
                    text="*"
                    ></asp:CompareValidator>                 
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        
         <div style="margin-top:5px;">
        <div class="usatFormatoCampo">
            <asp:Label ID="Label10" runat="server" Text="Seleccione un Centro Costo"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:DropDownList ID="ddlCentroCostoPlan" runat="server" Width="580px" 
                    AutoPostBack="True" Font-Size="Smaller" BackColor="#CCFFFF">
                </asp:DropDownList>
                 <asp:CompareValidator 
                    ID="CompareValidator5" 
                    runat="server" 
                    ErrorMessage="(*)  Seleccione un Centro de Costo." 
                    ControlToValidate="ddlCentroCostoPlan" 
                    Display="Dynamic" 
                    Operator="NotEqual" 
                    SetFocusOnError="true" 
                    ValidationGroup="grupo1" 
                    ValueToCompare="0"
                    text="*"
                    ></asp:CompareValidator>                   
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        
        <div style="margin-top:5px;">
        <div class="usatFormatoCampo">
            <asp:Label ID="Label8" runat="server" Text="Seleccione una Perspectiva"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:DropDownList ID="ddlPerspectiva" runat="server" Width="364px" 
                    AutoPostBack="True" Font-Size="Smaller" BackColor="#CCFFFF">
                </asp:DropDownList>
                <asp:CompareValidator 
                    ID="CompareValidator6" 
                    runat="server" 
                    ErrorMessage="(*)  Seleccione una perspectiva." 
                    ControlToValidate="ddlPerspectiva" 
                    Display="Dynamic" 
                    Operator="NotEqual" 
                    SetFocusOnError="true" 
                    ValidationGroup="grupo1" 
                    ValueToCompare="0"
                    text="*"
                    ></asp:CompareValidator>                 
                &nbsp;                
                <asp:Label ID="Label11" runat="server" Text="Seleccione Año"></asp:Label>
                <asp:DropDownList ID="ddlAnio" runat="server" Width="120px" 
                    AutoPostBack="True" Font-Size="Smaller" BackColor="#FFFFCC">
                </asp:DropDownList>                
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        
        <div style="margin-top:5px;">
        <div class="usatFormatoCampo">
            <asp:Label ID="Label12" runat="server" Text="Seleccione Objetivo"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:DropDownList ID="ddlObjetivo" runat="server" Width="580px" 
                    AutoPostBack="True" Font-Size="Smaller" BackColor="#CCFFFF">
                </asp:DropDownList>
                 <asp:CompareValidator 
                    ID="CompareValidator7" 
                    runat="server" 
                    ErrorMessage="(*)  Seleccione una perspectiva." 
                    ControlToValidate="ddlObjetivo" 
                    Display="Dynamic" 
                    Operator="NotEqual" 
                    SetFocusOnError="true" 
                    ValidationGroup="grupo1" 
                    ValueToCompare="0"
                    text="*"
                    ></asp:CompareValidator>                     
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        
        <div style="margin-top:5px;">
        <div class="usatFormatoCampo">
            <asp:Label ID="Label13" runat="server" Text="Seleccione Indicador"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:DropDownList ID="ddlIndicadores" runat="server" Width="580px" 
                    AutoPostBack="True" Font-Size="Smaller" BackColor="#CCFFFF">
                </asp:DropDownList>
                <asp:CompareValidator 
                    ID="CompareValidator8" 
                    runat="server" 
                    ErrorMessage="(*)  Seleccione un Indicador." 
                    ControlToValidate="ddlIndicadores" 
                    Display="Dynamic" 
                    Operator="NotEqual" 
                    SetFocusOnError="true" 
                    ValidationGroup="grupo1" 
                    ValueToCompare="0"
                    text="*"
                    ></asp:CompareValidator>                  
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        
    </div>
    
    <div class="usatPanelConsultaFormula">
        
        <div style="height:33px;">
            <div class="usatFormatoCampo">
                <asp:Label ID="Label4" runat="server" Text="Ingrese descripción del Indicador"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                    <asp:TextBox ID="txtBuscarIndicador" runat="server" Width="450px" ></asp:TextBox>
                    
                    <!-- Para Validar Caja de Busqueda -->
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                                                runat="server" 
                                                ControlToValidate="txtBuscarIndicador" 
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
                                        ControlToValidate="txtBuscarIndicador" 
                                        Display="Dynamic"                                     
                                        onservervalidate="CustomValidator2_ServerValidate"
                                        Text="*" ValidationGroup="grupo2"
                                        >
                    </asp:CustomValidator>   
                
                    <!------------------------------------------------------------------------------------>
                    
                    <asp:Button ID="cmdBuscarIndi" runat="server" Text="   Buscar" 
                        CssClass="buscar" Width="106px" ValidationGroup="grupo2"/> <!-- Para Validar Caja de Busqueda -->
            </div>
        </div>
        
    </div>
    
    <!-- Para Validar Caja de Busqueda -->
        
        <div style="clear:both; height:10px"></div>
        <div style="padding-left:25px">                            
                <table cellpadding="3" cellspacing="0"> 
                    <tr style="font-weight: bold">
                                             
                            <td style="width:70%">
                                <div>
                                    <asp:Label ID="Label7" runat="server" Height="20px" Text="LEYENDA:  "></asp:Label></div>
                                <div>
                                    <asp:Image ID="Image2" runat="server" Height="16px" 
                                        ImageUrl="~/Images/bola_verde.gif" Width="16px" /> 
                                    <asp:Label ID="Label5" runat="server" Text=" Fórmula Procesada."></asp:Label>
                                </div>
                                <div> 
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/bola_naranja.gif" />
                                    <asp:Label ID="Label1" runat="server" Text="Fórmula No Procesada. "></asp:Label>
                                </div>
                                <div>
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/bola_roja.gif" 
                                        Height="17px" />
                                    <asp:Label ID="Label6" runat="server" Text="Fórmula Con Errores de Sintaxis. "></asp:Label>
                                </div>
                            </td>
                                  <td style="width:30%">
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
    <br />
         <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
                <tr>                
                    <td style="text-align:center" align="center">
                        <asp:GridView ID="gvFormulas" runat="server" Width="100%" CellPadding="3" 
                            EmptyDataText="No se encontraron fórmulas registradas." AutoGenerateColumns="False" 
                            
                            DataKeyNames="Codigo,codigo_ind,codigo_obj,codigo_ppla,FormulaC,codigo_pla,codigo_Cco,codigo_pers,año_ind" AllowPaging="True" 
                            PageSize="5" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" 
                            BorderWidth="1px" Font-Size="Smaller">
                            <RowStyle ForeColor="#000066" />
                            <EmptyDataRowStyle ForeColor="#FF0066" />
                            <Columns>
                                <asp:BoundField HeaderText="N°" >
                                <ItemStyle Width="10px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Codigo" DataField="Codigo" Visible="False" />                                               
                                <asp:BoundField DataField="Periodo_pla" HeaderText="Periodo_pla" />
                                <asp:BoundField DataField="descripcion_Cco" HeaderText="descripcion_Cco" />
                                <asp:BoundField DataField="descripcion_pers" HeaderText="descripcion_pers" />
                                <asp:BoundField DataField="año_ind" HeaderText="año_ind" />
                                <asp:BoundField HeaderText="Indicador" DataField="Indicador" >
                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                </asp:BoundField>                                                                
                                <asp:BoundField DataField="Formula" HeaderText="Formula" >
                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FormulaC" 
                                    HeaderText="FormulaC" Visible="False" >
                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="codigo_ind" DataField="codigo_ind" Visible="False" >
                                <ItemStyle Width="15px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codigo_obj" HeaderText="codigo_obj" 
                                    Visible="False" >
                                <ItemStyle Width="15px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codigo_ppla" HeaderText="codigo_ppla" 
                                    Visible="False" >
                                <ItemStyle Width="15px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Sintaxis">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:CommandField ShowSelectButton="True" 
                                    SelectImageUrl="../images/editar.gif" ButtonType="Image" 
                                    SelectText="Modificar" >
                                <ItemStyle HorizontalAlign="Center" Width="10px" VerticalAlign="Middle" />
                                </asp:CommandField>
                                <asp:CommandField ShowDeleteButton="True" 
                                    DeleteImageUrl="../images/eliminar.gif" ButtonType="Image" >
                                <HeaderStyle Width="15px" />
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                                </asp:CommandField>
                                <asp:BoundField DataField="codigo_pla" HeaderText="codigo_pla" 
                                    Visible="False" />
                                <asp:BoundField DataField="codigo_Cco" HeaderText="codigo_Cco" 
                                    Visible="False" />
                                <asp:BoundField DataField="codigo_pers" HeaderText="codigo_pers" 
                                    Visible="False" />
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
     <div class="usatPanelConsultaF">
     <asp:Label ID="lblFormula" runat="server" Text="Construcción de la Fórmula"></asp:Label>
        <%--<div style="margin-top:1px;">--%>
           <div>
            <div class="usatFormatoCampoAnchoFormula">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtFormula" runat="server" Width="100%" Height="25px" 
                    Font-Bold="True" Font-Size="Medium" BackColor="#FFFFCC" ForeColor="Black"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator1" 
                    runat="server" 
                    ErrorMessage="(*) Favor de crear la formula usando el tablero de Operaciones y la Lista de variables." 
                    ControlToValidate="txtFormula" 
                    ValidationGroup="grupo1" 
                    SetFocusOnError="true"
                    EnableClientScript="true"  
                    text="*"
                    >
                 </asp:RequiredFieldValidator>    
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cmdCancelar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                
                <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #" border="1"> 
                    <tr style="background-color: #E8EEF7; font-weight: bold; height: 20px">
                    <td style="width:50%" align="center">
                        <div>
                            
                                <asp:ImageButton ID="ImageButton7" runat="server" 
                                    ImageUrl="../Iconos/key_1.png" Height="43px" />
                                <asp:ImageButton ID="ImageButton8" runat="server" 
                                    ImageUrl="../Iconos/key_2.png" BackColor="#E8EEF7" Height="43px" />
                                <asp:ImageButton ID="ImageButton9" runat="server" 
                                    ImageUrl="../Iconos/key_3.png" BackColor="#E8EEF7" Height="43px" />
                                <asp:ImageButton ID="ImageButton4" runat="server" 
                                                        ImageUrl="../Iconos/key_4.png" 
                                BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton5" runat="server" 
                                                        ImageUrl="../Iconos/key_5.png" 
                                BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton6" runat="server" 
                                                        ImageUrl="../Iconos/key_6.png" 
                                BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton1" runat="server" 
                                                        ImageUrl="../Iconos/key_7.png" 
                                BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton2" runat="server" 
                                                        ImageUrl="../Iconos/key_8.png" 
                                BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton3" runat="server" 
                                                        ImageUrl="../Iconos/key_9.png" 
                                BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton10" runat="server" 
                                                        ImageUrl="../Iconos/key_0.png" 
                                BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton17" runat="server" 
                                                        ImageUrl="../Iconos/key_punto.png" 
                                BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton13" runat="server" 
                                                        ImageUrl="../Iconos/icono_parentesis_derecha.png" 
                                BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton14" runat="server" 
                                                        
                                ImageUrl="../Iconos/icono_parentesis_izquierda.png" BackColor="#E8EEF7" 
                                    Height="43px" />
                                                    <asp:ImageButton ID="ImageButton22" runat="server" 
                                                        ImageUrl="../Iconos/key_dash.png" 
                                BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton23" runat="server" 
                                                        ImageUrl="../Iconos/key_plus.png" 
                                BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton11" runat="server" 
                                                        ImageUrl="../Iconos/keydiv.png" 
                                BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton12" runat="server" 
                                                        ImageUrl="../Iconos/key_star.png" 
                                BackColor="#E8EEF7" Height="43px" />
                            
                        </div>
                    </td>
                
            </tr>
                         
            
        </table>
                
            </div>
            <div style="clear:both; height:5px;" >              
            </div>        
        </div>  
        
            
    </div>

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
                                    CssClass="guardar2" ValidationGroup="grupo1" Width="106px" /> &nbsp;<asp:Button 
                                    ID="cmdCancelar" runat="server" Text="     Borrar Formula" CssClass="regresar2" 
                                    Width="106px" />
                </td>
                <td style="width:100%">
                </td>
            </tr>
                         
            
        </table>
        <div class="cuadroIndicaciones">
            <asp:Label ID="lblIndicaciones" runat="server" 
             ForeColor="#0000CC">(*) Importante: Para el correcto registro de la Fórmula se le solicita usar la lista de variables y el tablero de Operaciones. Favor de verificar la Fórmula creada para su correcto cálculo.</asp:Label></div>   
    
    
    
    <!-- <div class="usatPanelConsulta">
            <div class="usatFormatoCampo">
                <asp:Label ID="lblBuscar" runat="server" Text="Filtrar Lista de Variables"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtBuscar" runat="server" Width="600px"></asp:TextBox>
                 <asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="buscar" /> 
            </div>
        </div> -->
    
    
    
      <div class="usatPanel2">
        <div style="width:100%; margin-top:10px" >
             <div style="float:left; width: 100%;">
                 <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
                        <tr>
                            <td style="text-align:center; font-weight:bold;">Lista de Variables</td>                       
                        </tr>
                        <tr>
                            <td style="text-align:left" align="left">
                               <asp:TreeView ID="treePrueba" runat="server" MaxDataBindDepth="4" 
                                            ExpandDepth="1" Font-Size="XX-Small">
                                            <Nodes>
                                                <asp:TreeNode PopulateOnDemand="True" Text="LISTA PRINCIPAL" Value="Lista" 
                                                    SelectAction="Expand">
                                                </asp:TreeNode>
                                            </Nodes>
                                            <HoverNodeStyle CssClass="menuporelegir" />
                                        </asp:TreeView>
                            </td>
                    </tr>
                </table>            
            </div>
        
            <div style="float:left; width:2%; text-align:center">                                                            
                <br /> 
            </div>
        
            <div style="clear:both; height:5px;">
            <asp:TextBox ID="txtFormulaHide" runat="server" Width="100%" 
             BackColor="#CCFFCC" Visible="False" Height="23px"></asp:TextBox>
            </div>
        </div>
        </div>
    </form>
</body>
</html>
