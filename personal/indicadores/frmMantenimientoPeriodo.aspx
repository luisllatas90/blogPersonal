<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMantenimientoPeriodo.aspx.vb" Inherits="Indicadores_Formularios_frmMantenimientoPeriodo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" src="../javascript/calendario.js"></script>
    <script src="../javascript/funciones.js" type="text/javascript"></script>
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
        	width:700px;
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
        
        .usatDescripcionTitulo  
        {
        	font-family: Arial;
	        font-size: 10pt;	        
	        color: #27333c;
        }
        
        .usatPanel
        {        
            border: 1px solid #C0C0C0;	        
	        height:175px;	        
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
        
        #lblSubtitulo2
        {
        	position:relative;
        	top: -20px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:120px;        
        	text-align:center;
        	
        	
        	}
        
        #lblSubtitulo
        {
        	position:absolute;
        	top: 65px;
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
           
            
        /***** Para avisos **************/
        .mensajeError
        {
        	border: 1px solid #e99491;
        	background-color: #fed8d5;        	
        	padding-top:2px;        	
        	-moz-border-radius: 15px;
        	padding-left:20px;  	
        }
        
         .mensajeExito
        {
        	border: 1px solid #488e00;
        	background-color:#c5f4b5;        	
        	padding-top:2px;        	
        	-moz-border-radius: 15px;
        	padding-left:20px;        	
        }
        
        /********************************/                          
                        
    </style>     
       
</head>
<body>
    <form id="form1" runat="server">
    <div class="usatTituloAzul">Añadir Periodo
    <a href="#" onclick="apprise('Añade un nuevo periodo.');">
        <img src="../Images/info.png" style="border:0" alt="Ayuda."/>
    </a>
    <asp:ScriptManager ID="ScriptManager1" 
            runat="server">
    </asp:ScriptManager>
    
    </div>
    <div class="usatDescripcionTitulo"></div>
    
     <!-- Para avisos -->
    <div style="clear:both; height:5px"></div>
    
    <div id="avisos" runat="server" style="height:20px; padding-top:2px; width: 850px;">
    <asp:Image ID="Image1" runat="server" ImageUrl="../Images/beforelastchild.GIF"/>
    <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>            
    </div>
    <div style="clear:both; height:10px"></div>
    <!-------------------------------------------->
    
    
     <div class="usatPanel">
        <asp:Label ID="lblSubtitulo" runat="server" Text="Datos de registro"></asp:Label>
        <div style="margin-top:10px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="Label4" runat="server" Text="Seleccione la Periodicidad"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
            <asp:DropDownList ID="ddlPeriodicidad" runat="server" Width="600px" 
                    AutoPostBack="True">
                </asp:DropDownList>
                <asp:CompareValidator 
                    ID="CompareValidator1" 
                    runat="server" 
                    ErrorMessage="(*) Seleccione la periodicidad de la lista desplegable."
                    ControlToValidate="ddlPeriodicidad" 
                    Display="Dynamic"
                    Text="<strong>*</strong>" 
                    SetFocusOnError="true" 
                    Type="Integer" 
                    Operator="GreaterThan"
                    ValueToCompare="0" 
                    ValidationGroup="grupo1">
                    
                </asp:CompareValidator>
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        
        <div style="margin-top:10px;">
            <div class="usatFormatoCampo">
             <asp:Label ID="lblCodigo" runat="server"></asp:Label>
            <asp:Label ID="lblCategoria" runat="server" Text="Descripción"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                
                            <asp:TextBox ID="txtPeriodo" runat="server" Width="595px" MaxLength="200" 
                    TabIndex="1"></asp:TextBox>
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator1" 
                    runat="server" 
                    ErrorMessage="(*) Ingrese la descripcion del periodo" 
                    ControlToValidate="txtPeriodo" 
                    ValidationGroup="grupo1" 
                    SetFocusOnError="true"
                    EnableClientScript="true"  
                    text="*"
                    >
                    
                </asp:RequiredFieldValidator>
                
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtPeriodo" Display="Dynamic" 
                     Text="<strong>*</strong>"
                    ErrorMessage="Debe escribir solo numeros y letras."  
                    ValidationExpression="^[a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\-\.\s]*"
                    ValidationGroup="grupo1" SetFocusOnError="true" EnableTheming="True">
                  </asp:RegularExpressionValidator>
               
                 <asp:CustomValidator 
                    ID="CustomValidator1" 
                    runat="server" 
                    ErrorMessage="(*) Ingresar valores válidos"
                    ControlToValidate="txtPeriodo" 
                    Display="Dynamic"
                    ValidationGroup="grupo1"
                    SetFocusOnError="true"
                    EnableClientScript="true"  
                    Text="*"
                    >
                </asp:CustomValidator> 
                                     
                <asp:CustomValidator 
                    ID="CustomValidator2" 
                    runat="server" 
                    ErrorMessage="(*) Se encontraron palabras reservadas en la descripción del periodo." 
                    controltovalidate="txtPeriodo" 
                    onservervalidate="CustomValidator2_ServerValidate"
                    ValidationGroup="grupo1" 
                    Text="*">
                </asp:CustomValidator>
                            
                
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>   
         <div style="margin-top:10px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="Label1" runat="server" Text="Fecha Inicio"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtFechaInicio" runat="server" TabIndex="2" MaxLength="10"></asp:TextBox>
                <input onclick="MostrarCalendario('txtFechaInicio')" type="button" value="..." />
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator2" 
                    runat="server" 
                    ErrorMessage="(*) Ingrese o seleccione  una fecha de inicio para el periodo" 
                    ControlToValidate="txtFechaInicio" 
                    ValidationGroup="grupo1" 
                    SetFocusOnError="true"
                    EnableClientScript="true"  
                    text="*"
                    >
                    
                </asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator 
                    ID="RegularExpressionValidator2" 
                    runat="server" 
                    ControlToValidate="txtFechaInicio" 
                    Display="Static" 
                    ErrorMessage="(*) Ingrese un formato valido de fecha inicio.[dd/mm/aaaa]"
                    ValidationExpression="\d{1,2}\/\d{1,2}/\d{4}" 
                    ValidationGroup="grupo1" 
                    SetFocusOnError="true" 
                    text="*">
                </asp:RegularExpressionValidator>
                <asp:CompareValidator 
                    ID="CompareValidator2" 
                    runat="server" 
                    ControlToValidate="txtFechaInicio" 
                    ValidationGroup="grupo1" 
                    SetFocusOnError="true" 
                    Operator="DataTypeCheck" Type="Date"
                    text="*"
                    ErrorMessage=" Ingrese un formato valido de fecha Inicio.[dd/mm/aaaa]">
                </asp:CompareValidator>
                
                
                
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>  
         <div style="margin-top:10px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="Label2" runat="server" Text="Fecha Final"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtFechaFinal" runat="server" TabIndex="3" MaxLength="10"></asp:TextBox>
                <input onclick="MostrarCalendario('txtFechaFinal')" type="button" value="..." />
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator3" 
                    runat="server" 
                    ErrorMessage="(*) Ingrese o seleccione  una fecha fin para el periodo" 
                    ControlToValidate="txtFechaFinal" 
                    ValidationGroup="grupo1" 
                    SetFocusOnError="true"
                    EnableClientScript="true"  
                    text="*"
                    >
                    
                </asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator 
                    ID="RegularExpressionValidator3" 
                    runat="server" 
                    ControlToValidate="txtFechaFinal" 
                    Display="Dynamic" 
                    ErrorMessage="(*) Ingrese un formato valido de fecha final.[dd/mm/aaaa]"
                    ValidationExpression="\d{1,2}\/\d{1,2}/\d{4}"
                    ValidationGroup="grupo1" 
                    SetFocusOnError="true" 
                    EnableClientScript="true" 
                    EnableTheming="True"
                    text="*">
                </asp:RegularExpressionValidator>
                                <asp:CompareValidator 
                    ID="CompareValidator3" 
                    runat="server" 
                    ControlToValidate="txtFechaFinal" 
                    ValidationGroup="grupo1" 
                    SetFocusOnError="true" 
                    Operator="DataTypeCheck" Type="Date"
                    text="*"
                    ErrorMessage=" Ingrese un formato valido de fecha final.[dd/mm/aaaa]">
                </asp:CompareValidator>
            </div>
            <div style="clear:both; height:5px;"></div>        
        </div> 
    </div>
    
    <div> 
        
        <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0"> 
            <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                <td style="width:80%">
                    <asp:ValidationSummary 
                        ID="ValidationSummary1" 
                        runat="server" 
                        DisplayMode="BulletList" 
                        ShowSummary="true" 
                        HeaderText="Errores encontrados:" 
                        BorderWidth="0" 
                        ValidationGroup="grupo1" 
                        Font-Bold="False" 
                        ForeColor="#FF0066"/>
                        
                    </td>
                        <td align="right" style="width:20%" valign="top">
                                <asp:Button ID="cmdGuardar" runat="server" Text="   Guardar" 
                                    CssClass="guardar2" ValidationGroup="grupo1" TabIndex="4" /> &nbsp;<asp:Button 
                                    ID="cmdCancelar" runat="server" Text="Limpiar" CssClass="regresar2" 
                                    Width="70px" TabIndex="5" />
                </td>
            </tr>
        </table>
        <br />       
    </div>
  <div class="usatPanelConsulta">
  <asp:Label ID="lblSubtitulo2" runat="server" Text="Consulta de Periodos"></asp:Label>
     <div style="height:65px;">
            <div class="usatFormatoCampo">
                <asp:Label ID="lblBuscar" runat="server" Text="Ingrese descripción del periodo"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                    <asp:TextBox ID="txtBuscar" runat="server" Width="595px" ></asp:TextBox>
                    
                    <!-- Para Validar Caja de Busqueda -->
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator4" 
                                                runat="server" 
                                                ControlToValidate="txtBuscar" 
                                                Display="Dynamic" 
                                                ErrorMessage="Ingrese carácteres válidos para buscar el Periodo." 
                                                ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ ,.]{3,}" 
                                                ValidationGroup="grupo2" 
                                                SetFocusOnError="true" 
                                                EnableTheming="True"
                                                Text="*"
                                                >
                    </asp:RegularExpressionValidator>
                
                    <asp:CustomValidator   ID="CustomValidator3" 
                                        runat="server" 
                                        ErrorMessage="Se encontraron palabras reservadas en la búsqueda del Periodo." 
                                        ControlToValidate="txtBuscar" 
                                        Display="Dynamic"                                     
                                        onservervalidate="CustomValidator2_ServerValidate"
                                        Text="*" ValidationGroup="grupo2"
                                        >
                    </asp:CustomValidator>   
                    
                    <!------------------------------------------------------------------------------------>
                
                    <asp:Button ID="btnBuscar" runat="server" Text="   Buscar" 
                    CssClass="buscar" ValidationGroup="grupo2" /> <!-- Para Validar Caja de Busqueda --> 
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
                    <asp:GridView ID="gvPeriodicidad" runat="server" Width="100%" CellPadding="4" 
                        ForeColor="#333333" AutoGenerateColumns="False" 
                        DataKeyNames="Codigo,codigo_peri" AllowPaging="True" 
                        EmptyDataText="No se encontraron registros.">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <EmptyDataRowStyle ForeColor="#FF0066" />
                        <Columns>
                            <asp:BoundField HeaderText="N°" />
                            <asp:BoundField HeaderText="Codigo" DataField="Codigo" Visible="False" />
                            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Fecha Inicio" DataField="FechaInicio" />
                            <asp:BoundField DataField="FechaFin" HeaderText="Fecha Fin" />
                            <asp:BoundField DataField="descripcion_peri" HeaderText="Periodicidad" />
                            <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                            <asp:CommandField ShowSelectButton="True" 
                                SelectImageUrl="~/Images/editar.gif" ButtonType="Image" />
                            <asp:CommandField ShowDeleteButton="True" 
                                DeleteImageUrl="~/Images/eliminar.gif" ButtonType="Image" />
                            <asp:BoundField DataField="codigo_peri" HeaderText="codigo_peri" 
                                Visible="False" />
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
