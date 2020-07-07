<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmImportarDatosVariable.aspx.vb" Inherits="Indicadores_Formularios_frmImportarDatosVariable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="../css/cssUpdateProgress.css" rel="stylesheet" type="text/css" />

    <script src="../javascript/jq/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../javascript/jq/lbox/thickbox.js" type="text/javascript"></script>
    <link  href="../javascript/jq/lbox/thickbox.css" rel="stylesheet" type="text/css" media="screen" />
    
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
    <script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
    <link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />
    
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
        	padding-left:60px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:100px;        	
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
        	width:400px;
        	/*border:1px solid red;*/
        }
        
        .usatTituloAzul {
	        font-family: Arial;
	        font-size: 12pt;
	        font-weight: bold;
	        color: #3063c5;
            height: 19px;
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
	        height:135px;	        
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
        	top: 75px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:120px;        
        	text-align:center;
            right: 960px;
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
    <script src="../javascript/jsUpdateProgress.js" type="text/javascript"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <div class="usatTituloAzul">Importar Valores Variables
        <a href="#" onclick="apprise('Importa los valores de las variables seleccionada.');">
            <img src="../Images/info.png" style="border:0" alt="Ayuda."/>
        </a>
    </div>
    
    <!-- Para avisos -->
    <div style="clear:both; height:5px"></div>
    
    <div id="avisos" runat="server" style="height:20px; padding-top:2px; width: 98%;">
    <asp:Image ID="Image1" runat="server" ImageUrl="../Images/beforelastchild.GIF"/>
    <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>            
    </div>
    <div style="clear:both; height:10px"></div>
    <!-------------------------------------------->

    
     <div class="usatPanel">
        <asp:Label ID="lblSubtitulo" runat="server" Text="Parámetros"></asp:Label>
        <div style="margin-top:20px;">
            <div class="usatFormatoCampo">
                <asp:Label ID="Label1" runat="server" Text="Categoría"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:DropDownList ID="ddlCategoria" runat="server" Width="350px" 
                    AutoPostBack="True">
                </asp:DropDownList>
                 <asp:CompareValidator 
                    ID="CompareValidator2" 
                    runat="server" 
                    ErrorMessage="(*) Seleccione una categoria de la lista desplegable"
                    ControlToValidate="ddlCategoria" 
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
        
        <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
                <asp:Label ID="lblPeriocidad" runat="server" Text="Periodicidad"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:DropDownList ID="ddlPeriodicidad" runat="server" Width="350px" 
                    AutoPostBack="True">
                </asp:DropDownList>
                 <asp:CompareValidator 
                    ID="CompareValidator1" 
                    runat="server" 
                    ErrorMessage="(*) Seleccione una periodicidad de la variable de la lista desplegable."
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
          <div>
            <div class="usatFormatoCampo">
                <asp:Label ID="lblPeriodicidad" runat="server" Text="Periodo "></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                                          <asp:DropDownList ID="ddlSemestre" runat="server" Width="350px">
                         </asp:DropDownList>
                            <asp:CompareValidator 
                    ID="CompareValidator3" 
                    runat="server" 
                    ErrorMessage="(*) Seleccione un periodo de la lista desplegable."
                    ControlToValidate="ddlSemestre" 
                    Display="Dynamic"
                    Text="<strong>*</strong>" 
                    SetFocusOnError="true" 
                    Type="String" 
                    Operator="GreaterThan"
                    ValueToCompare="P0" 
                    ValidationGroup="grupo1">
                    
                </asp:CompareValidator>
                  
            
                         
                   
                                
            </div>
            <div style="clear:both; height:5px;"></div>
        </div>    
          
         <div>
            <div class="usatFormatoCampo">
            </div>
            <div class="usatFormatoCampoAncho">
            </div>
            <div style="clear:both; height:5px;"></div>
        </div>  
        <div>    
        <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0"> 
            <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                <td style="width:80%" valign="top">
                    <asp:Button ID="cmdValores" runat="server" Text="    Valores" 
                        CssClass="usatBuscar" Width="100px" /><asp:ValidationSummary 
                            ID="ValidationSummary1" runat="server" 
                            DisplayMode="BulletList" ShowSummary="true" HeaderText="Errores encontrados:" 
                            BorderWidth="0" ValidationGroup="grupo1" Font-Bold="False" 
                            ForeColor="#FF0066"/>
                
                </td>
                        <td align="right" style="width:20%" valign="top">
                                    <asp:Button 
                                        ID="cmdGuardar" 
                                        runat="server" 
                                        Text="   Importar" 
                                        CssClass="nuevo" 
                                        OnClick="Save_Click" 
                                        UseSubmitBehavior="false"
                                        ValidationGroup="grupo1" 
                                        Width="100px"/> 
                             
                         <asp:Button ID="cmdCancelar" runat="server" Text="Limpiar" CssClass="regresar2" 
                         Width="100px" TabIndex="5" />            
                        
               &nbsp;</td>
            </tr>
        </table>
        <br />       
    </div>
        
        <div>
         <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
                <tr>
                    <td style="text-align:center" align="center">
                        
                                <asp:GridView ID="gvVariable" runat="server" Width="100%" CellPadding="4" 
                            ForeColor="#333333" AutoGenerateColumns="False" 
                            DataKeyNames="Codigo" EmptyDataText="No se encontraron registros.">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EmptyDataRowStyle ForeColor="#FF0066" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server"  onclick="MarcarCursos(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>                
                                        <asp:CheckBox ID="chkElegir" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
                                <asp:BoundField HeaderText="Descripción" DataField="Descripcion" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Periodicidad" HeaderText="Periodicidad" />
                                <asp:BoundField DataField="FechaImportacion" HeaderText="Fecha Importacion" />
                                <asp:BoundField DataField="existebd_var" HeaderText="Existe Valor" />
                                <asp:BoundField DataField="scriptimp_var" HeaderText="Script Creado" 
                                    Visible="False" />
                                
                                <asp:CommandField HeaderText="Estructura" ShowSelectButton="True" 
                                    ButtonType="Image" Visible="False" />
                                
                                <asp:CommandField ButtonType="Image" HeaderText="Ver" />
                                
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
