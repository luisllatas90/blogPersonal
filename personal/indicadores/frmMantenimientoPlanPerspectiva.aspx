<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMantenimientoPlanPerspectiva.aspx.vb" Inherits="Indicadores_Formularios_frmMantenimientoPlanPerspectiva" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="../javascript/funciones.js"  type="text/javascript" language="JavaScript"/>
    <script type="text/javascript" src="../javascript/calendario.js"></script>  
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
    <script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
    <link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />
            

    <script src="../javascript/colorPicker.js" type="text/javascript"></script>
    <link href="../css/colorPicker.css" rel="stylesheet" type="text/css" />
    
	
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
        	width:200px;        	
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
        
        .usatPanel
        {        
            border: 1px solid #C0C0C0;	        
	        height:200px;	        
	        max-height:300PX;
	        -moz-border-radius: 15px; 
	        /*padding-top:10px;*/	        
        }
        
        .usatPanel1
        {        
            border: 1px solid #C0C0C0;	        
	        height:115px;	        
	        max-height:300PX;
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
        
        #lblPlan
        {
        	position:relative;
        	top: -10px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:120px;        
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
    
    <div class="usatTituloAzul">Añadir Perspectiva
    <a href="#" onclick="apprise('Añade una nueva Perspectiva.');">
        <img src="../Images/info.png" style="border:0" alt="Ayuda."/>
    </a>
    </div>
    <!-- Avisos-->
    <div style="clear:both; height:5px"></div>
    
    <div id="avisos" runat="server" style="height:25px; padding-top:2px; width: 80%;">
    <asp:Image ID="Image1" runat="server" ImageUrl="../Images/beforelastchild.GIF"/>
    <asp:Label ID="lblMensajePers" runat="server" Visible="False"></asp:Label>            
    </div>
    <div style="clear:both; height:10px"></div>
    <!-------------------------------------------->
    
    <div class="usatPanel1">
        <asp:Label ID="lblSubtitulo" runat="server" Text="Datos de registro"></asp:Label>
        <div style="margin-top:10px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="Label7" runat="server" Text="Abreviatura"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtAbreviatura" runat="server" Width="80px" 
                    Font-Size="Small" MaxLength="10"></asp:TextBox>
                <asp:CustomValidator 
                    ID="CustomValidator1" 
                    runat="server" 
                    ErrorMessage="Se encontraron palabras reservadas en la descripción del periodo." 
                    controltovalidate="txtAbreviatura" 
                    onservervalidate="CustomValidator2_ServerValidate"
                    ValidationGroup="grupo1" 
                    Text="*">
                </asp:CustomValidator>
                  
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                    ControlToValidate="txtAbreviatura" Display="Dynamic" 
                    ErrorMessage="Ingrese carácteres válidos" 
                    ValidationExpression="^[a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\-\.\(\)\s]*"
                    text="*"
                    ValidationGroup="grupo1" SetFocusOnError="true" EnableTheming="True">
                 </asp:RegularExpressionValidator>
                  
                 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                                            runat="server" 
                                            ErrorMessage="Debe ingresar una abreviatura para la Perspectiva" 
                                            ControlToValidate="txtAbreviatura" 
                                            ValidationGroup="grupo1" 
                                            SetFocusOnError="true"
                                            EnableClientScript="true"  
                                            Text="*" 
                                            >
                </asp:RequiredFieldValidator>

                                   
                  
                &nbsp;&nbsp;&nbsp;

                                   
                  
                </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        <div style="margin-top:1px;">
            <div class="usatFormatoCampo">

                                   
                  
                <asp:Label ID="Label6" runat="server" Text="Seleccione un color"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtColor"  runat="server" onclick="startColorPicker(this)" 
                   onkeyup="maskedHex(this)" Width="80px" TabIndex="1" MaxLength="6"></asp:TextBox>
                  <asp:CustomValidator 
                    ID="CustomValidator5" 
                    runat="server" 
                    ErrorMessage="Debe seleccionar un color" 
                    controltovalidate="txtColor" 
                    onservervalidate="CustomValidator2_ServerValidate"
                    ValidationGroup="grupo1" 
                    Text="*">
                </asp:CustomValidator>
               &nbsp;
                 
               <asp:Label ID="Label8" runat="server" 
                    Text="(*) Click en la Caja de texto para seleccionar un color." 
                    Font-Bold="False" ForeColor="#FF0066"></asp:Label>
                  
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="lblCodigopers" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblDescripcion" runat="server" Text="Descripción"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtDescripcionPers" runat="server" Width="500px" 
                    Font-Size="Small" MaxLength="200" TabIndex="2"></asp:TextBox>
                <asp:CustomValidator 
                    ID="CustomValidator2" 
                    runat="server" 
                    ErrorMessage="Se encontraron palabras reservadas en la descripción del periodo." 
                    controltovalidate="txtDescripcionPers" 
                    onservervalidate="CustomValidator2_ServerValidate"
                    ValidationGroup="grupo1" 
                    Text="*">
                </asp:CustomValidator>
                  
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ControlToValidate="txtDescripcionPers" Display="Dynamic" 
                    ErrorMessage="Ingrese carácteres válidos" 
                    ValidationExpression="^[a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\-\.\(\)\s]*"
                    text="*"
                    ValidationGroup="grupo1" SetFocusOnError="true" EnableTheming="True">
                 </asp:RegularExpressionValidator>
                  
                 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                            runat="server" 
                                            ErrorMessage="Debe ingresar la Descripción para la Perspectiva" 
                                            ControlToValidate="txtDescripcionPers" 
                                            ValidationGroup="grupo1" 
                                            SetFocusOnError="true"
                                            EnableClientScript="true"  
                                            Text="*" 
                                            >
                </asp:RequiredFieldValidator>
                                   
                  
            </div>
            <div style="clear:both; height:1px;"></div>        
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
                                <asp:Button ID="cmdGuardarPers" runat="server" Text="   Guardar" 
                                    CssClass="guardar2" ValidationGroup="grupo1"/> &nbsp;<asp:Button 
                                    ID="cmdCancelar" runat="server" Text="Limpiar" CssClass="regresar2" 
                                    Width="70px" />
                        </td>
            </tr>
        </table>
        <br />       
    </div>
    
    <div class="usatTituloAzul">Añadir Plan
    <a href="#" onclick="apprise('Añade un nuevo Plan.');">
        <img src="../Images/info.png" style="border:0" alt="Ayuda."/>
    </a>
    </div>
  
 <!-- Avisos-->
    <div style="clear:both; height:5px"></div>
    
    <div id="avisos2" runat="server" style="height:25px; padding-top:2px; width: 80%;">
    <asp:Image ID="Image2" runat="server" ImageUrl="../Images/beforelastchild.GIF"/>
    <asp:Label ID="lblMensajePlan" runat="server" Visible="False"></asp:Label>            
    </div>
    <div style="clear:both; height:15px"></div>
    <!-------------------------------------------->
    
    <div class="usatPanel">
        <asp:Label ID="lblPlan" runat="server" Text="Datos de registro"></asp:Label>
        <div style="margin-top:10px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="lblCodigoPlan" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="Label4" runat="server" Text="Descripción"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtDescripcionplan" runat="server" Width="500px" 
                    Font-Size="Small" MaxLength="50"></asp:TextBox>
                   <asp:CustomValidator 
                    ID="CustomValidator3" 
                    runat="server" 
                    ErrorMessage="Se encontraron palabras reservadas en la descripción del periodo." 
                    controltovalidate="txtDescripcionplan" 
                    onservervalidate="CustomValidator3_ServerValidate"
                    ValidationGroup="grupo2" 
                    Text="*">
                </asp:CustomValidator>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtDescripcionplan" Display="Dynamic" 
                    ErrorMessage="Ingrese carácteres válidos" 
                    ValidationExpression="^[a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\-\.\s]*"
                    text="*"
                    ValidationGroup="grupo2" SetFocusOnError="true" EnableTheming="True">
                 </asp:RegularExpressionValidator>
                    
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                            runat="server" 
                                            ErrorMessage="Debe ingresar la Descripción para el Plan." 
                                            ControlToValidate="txtDescripcionplan" 
                                            ValidationGroup="grupo2" 
                                            SetFocusOnError="true"
                                            EnableClientScript="true"  
                                            Text="*" 
                                            >
                </asp:RequiredFieldValidator>
                
                               
                
           
                </div>
            <div style="clear:both; height:1px;"></div>        
        </div>    
        
        <div>
            <div class="usatFormatoCampo">
                <asp:Label ID="Label9" runat="server" Text="Abreviatura (<< Informe >>)"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtAbreviatura_pla" Width="500px" 
                    Font-Size="Small" MaxLength="200" runat="server"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                                            runat="server" 
                                            ErrorMessage="Debe ingresar la Abreviatura" 
                                            ControlToValidate="txtAbreviatura_pla" 
                                            ValidationGroup="grupo2" 
                                            SetFocusOnError="true"
                                            EnableClientScript="true"  
                                            Text="*" 
                                            >
                </asp:RequiredFieldValidator>
                    
            </div>
            <div style="clear:both; height:1px;"></div>
        </div> 
        
        
        <div>
            <div class="usatFormatoCampo">
                <asp:Label ID="lblVigenciaPlan" runat="server" Text="Vigencia del Plan"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:CheckBox ID="chkVigencia" runat="server" Text="Inactivo" 
                    AutoPostBack="True" />
                    
            </div>
            <div style="clear:both; height:1px;"></div>
        </div>    
        
        <div>
            <div class="usatFormatoCampo">
            <asp:Label ID="Label1" runat="server" Text="Fecha Inicio"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtFechaInicio" runat="server"></asp:TextBox>
                <input onclick="MostrarCalendario('txtFechaInicio')" type="button" value="..." /><asp:RequiredFieldValidator 
                    ID="reqfv01" 
                    runat="server" 
                    ErrorMessage="Ingrese o seleccione  una fecha de inicio para el periodo" 
                    ControlToValidate="txtFechaInicio" 
                    ValidationGroup="grupo2" 
                    SetFocusOnError="true"
                    EnableClientScript="true"  
                    text="*"
                    >
                    
                </asp:RequiredFieldValidator><asp:RegularExpressionValidator 
                    ID="RegEx01" 
                    runat="server" 
                    ControlToValidate="txtFechaInicio" 
                    Display="Static" 
                    ErrorMessage="ngrese un formato valido de fecha.[dd/mm/aaaa]"
                    ValidationExpression="\d{1,2}\/\d{1,2}/\d{4}" 
                    ValidationGroup="grupo2" 
                    SetFocusOnError="true" 
                    text="*">
                </asp:RegularExpressionValidator><asp:CompareValidator 
                    ID="CompareValidator2" 
                    runat="server" 
                    ControlToValidate="txtFechaInicio" 
                    ValidationGroup="grupo1" 
                    SetFocusOnError="true" 
                    Operator="DataTypeCheck" Type="Date"
                    text="*"
                    ErrorMessage=" Ingrese un formato valido de fecha Inicio.[dd/mm/aaaa]">
                </asp:CompareValidator></div>
            <div style="clear:both; height:1px;"></div>
        </div>
        
        <div>
            <div class="usatFormatoCampo">
            <asp:Label ID="Label2" runat="server" Text="Fecha Fin"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtFechaFin" runat="server"></asp:TextBox>
                <input onclick="MostrarCalendario('txtFechaFin')" type="button" value="..." /><asp:RequiredFieldValidator 
                    ID="reqf02" 
                    runat="server" 
                    ErrorMessage="Ingrese o seleccione  una fecha de inicio para el periodo" 
                    ControlToValidate="txtFechaFin" 
                    ValidationGroup="grupo2" 
                    SetFocusOnError="true"
                    EnableClientScript="true"  
                    text="*"
                    >
                    
                </asp:RequiredFieldValidator><asp:RegularExpressionValidator 
                    ID="reqEx02" 
                    runat="server" 
                    ControlToValidate="txtFechaFin" 
                    Display="Static" 
                    ErrorMessage="Ingrese un formato valido de fecha.[dd/mm/aaaa]"
                    ValidationExpression="\d{1,2}\/\d{1,2}/\d{4}" 
                    ValidationGroup="grupo2" 
                    SetFocusOnError="true" 
                    text="*">
                </asp:RegularExpressionValidator><asp:CompareValidator 
                    ID="CompareValidator1" 
                    runat="server" 
                    ControlToValidate="txtFechaFin" 
                    ValidationGroup="grupo1" 
                    SetFocusOnError="true" 
                    Operator="DataTypeCheck" Type="Date"
                    text="*"
                    ErrorMessage=" Ingrese un formato valido de fecha Inicio.[dd/mm/aaaa]">
                </asp:CompareValidator></div>
        </div>
        
    </div>
    
    <div >    
        
        <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0"> 
            <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                <td style="width:80%">
                
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                        DisplayMode="BulletList" ShowSummary="true" HeaderText="Errores encontrados:" 
                        BorderWidth="0" ValidationGroup="grupo2" Font-Bold="False" 
                        ForeColor="#FF0066"/>
                    </td>
                        <td align="right" valign ="top"  style="width:20%">
                                <asp:Button ID="cmdGuardarPlan" runat="server" Text="   Guardar" 
                                    CssClass="guardar2" ValidationGroup="grupo2"/> &nbsp;<asp:Button 
                                    ID="Button2" runat="server" Text="Limpiar" CssClass="regresar2" 
                                    Width="70px" />
                        </td>
            </tr>
        </table>
        <br />       
    </div>
    
    <div class="usatPanelConsulta">
    
        <div>
            <div class="usatFormatoCampo">
                <asp:Label ID="Label5" runat="server" Text="Opciones de Búsqueda"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:RadioButton ID="rbtPerspectiva" Text="Por Perspectivas" runat="server" 
                    Checked="True" GroupName="OpcionesBusqueda" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rbtPlan" Text="Por Planes" runat="server" 
                    GroupName="OpcionesBusqueda" />
                
            </div>
            <div style="clear:both; height:5px;"></div>
        </div>
        
        <div style="height:50px;">
            <div class="usatFormatoCampo">
                <asp:Label ID="Label3" runat="server" Text="Ingrese descripción del objetivo"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                    <asp:TextBox ID="txtBuscar" runat="server" Width="500px"  ></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                    ControlToValidate="txtBuscar" Display="Dynamic" 
                    ErrorMessage="Ingrese carácteres válidos" 
                    ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ ,.]{3,}" 
                    text="*"
                    ValidationGroup="grupo3" SetFocusOnError="true" EnableTheming="True"></asp:RegularExpressionValidator>
                  
                     <asp:CustomValidator 
                    ID="CustomValidator4" 
                    runat="server" 
                    ErrorMessage="Se encontraron palabras reservadas en la descripción del periodo." 
                    controltovalidate="txtBuscar" 
                    onservervalidate="CustomValidator4_ServerValidate"
                    ValidationGroup="grupo3" 
                    Text="*"
                    >
                </asp:CustomValidator>
                    <asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="buscar" 
                        ValidationGroup="grupo3" /> 
            </div>
        </div>
        
        <div>
         <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
                <tr>                
                    <td style="text-align:center" align="center">
                        <asp:GridView ID="gvRegistrosPers" runat="server" Width="100%" CellPadding="4" 
                            ForeColor="#333333" AutoGenerateColumns="False" 
                            DataKeyNames="Codigo,abreviatura" AllowPaging="True">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField HeaderText="N°" >
                                    <ItemStyle Width="15px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Codigo" DataField="Codigo" Visible="False" />                                               
                                <asp:BoundField DataField="abreviatura" HeaderText="Abr." />
                                <asp:BoundField DataField="Color" HeaderText="Color" />
                                <asp:BoundField HeaderText="Descripción" DataField="Descripcion" >
                                    <ItemStyle HorizontalAlign="Left" Width="800px" />
                                </asp:BoundField>                                                                
                                <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistro" >
                                    <ItemStyle Width="155px" />
                                </asp:BoundField>
                                <asp:CommandField ShowSelectButton="True" 
                                    SelectImageUrl="../images/editar.gif" ButtonType="Image" 
                                    SelectText="Modificar" >
                                    <ItemStyle Width="15px" />
                                </asp:CommandField>
                                <asp:CommandField ShowDeleteButton="True" 
                                    DeleteImageUrl="../images/eliminar.gif" ButtonType="Image" >
                                    <ItemStyle Width="15px" />
                                </asp:CommandField>
                            </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                        <asp:GridView ID="gvRegistroPlan" runat="server" Width="100%" CellPadding="4" 
                            ForeColor="#333333" AutoGenerateColumns="False" 
                            DataKeyNames="Codigo" AllowPaging="True" >
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField HeaderText="N°" >
                                    <ItemStyle Width="15px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Codigo" DataField="Codigo" Visible="False" />                                               
                                <asp:BoundField HeaderText="Plan" DataField="Periodo" >
                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>                                                                
                                <asp:BoundField HeaderText="Vigencia" DataField="Vigencia" >
                                    <ItemStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FechaInicio" HeaderText="Fecha Inicio" >
                                    <ItemStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FechaFin" HeaderText="Fecha Fin" >
                                    <ItemStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" >
                                    <ItemStyle Width="155px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="abreviatura_pla" HeaderText="Abreviatura" />
                                <asp:CommandField ShowSelectButton="True" 
                                    SelectImageUrl="../images/editar.gif" ButtonType="Image" 
                                    SelectText="Modificar" >
                                    <ItemStyle Width="15px" />
                                </asp:CommandField>
                                <asp:CommandField ShowDeleteButton="True" 
                                    DeleteImageUrl="../images/eliminar.gif" ButtonType="Image" >
                                    <ItemStyle Width="15px" />
                                </asp:CommandField>
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
                
                 <tr>                
                    <td style="text-align:left" align="left">
                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
                        DisplayMode="BulletList" ShowSummary="true" HeaderText="Errores encontrados:" 
                        BorderWidth="0" ValidationGroup="grupo3" Font-Bold="False" 
                        ForeColor="#FF0066"/>
                        &nbsp;</td>
                </tr>
            </table>
            
        </div>
    </div>
    </form>
</body>
</html>
