<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConfiguracionPlanPerspectiva.aspx.vb" Inherits="Indicadores_Formularios_frmConfiguracionPlanPerspectiva" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
     <link href="../javascript/funciones.js"  type="text/javascript" language="JavaScript"/>
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
	            if (chk.type == "checkbox" ) {
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
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:500px;        	
        }
        
        .usatFormatoCampox
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-right:auto;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:500px;        	
        }
        
        .usatFormatoCampo1
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:50px;
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
        	padding-left:50px;
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
            height: 245px;
          }
        
        .usatPanelConfiguracion
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
        	top: 215px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:150px;        
        	text-align:center;
        }
        
         #lblConsulta2
        {
        	position:relative;
        	top: -20px;        	
        	left: 19px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	/*width:60px;        */
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
     <div class="usatTituloAzul">Configuración Plan-Perspectiva
    <a href="#" onclick="apprise('Permite configurar las perspectivas para un plan seleccionado.');">
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
            <div style="clear:both; height:1px;"></div>        
        </div>   
         <div style="margin-top:10px;">
            <div class="usatFormatoCampo1">
                <asp:Label ID="lblCodPers" runat="server" Text="CodPers" ForeColor="#66FF33" 
                    Visible="False"></asp:Label>
            <asp:Label ID="lblPlan" runat="server" Text="Seleccione un Plan"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                &nbsp;<asp:DropDownList ID="ddlPlan" runat="server" Width="500px" 
                    AutoPostBack="True">
                </asp:DropDownList>
                <asp:CompareValidator 
                    ID="CompareValidator1" 
                    runat="server" 
                    ErrorMessage="(*) Seleccione un Plan de la lista desplegable."
                    ControlToValidate="ddlPlan" 
                    Display="Dynamic"
                    Text="<strong>*</strong>" 
                    SetFocusOnError="true" 
                    Type="Integer" 
                    Operator="GreaterThan"
                    ValueToCompare="0" 
                    ValidationGroup="grupo1"> </asp:CompareValidator>
             </div>
            <div style="clear:both; height:1px;"></div>        
        </div>  
                 <div style="margin-top:10px;">
            <div class="usatFormatoCampo1">
                <asp:Label ID="lblCodigo" runat="server" Text="lblCodigo" ForeColor="#FF0066" 
                    Visible="False"></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="Seleccione un Centro Costo"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                &nbsp;<asp:DropDownList ID="ddlCentroCosto" runat="server" Width="500px">
                </asp:DropDownList>
                <asp:CompareValidator 
                    ID="CompareValidator2" 
                    runat="server" 
                    ErrorMessage="(*) Seleccione un Centro de Costo de la lista desplegable."
                    ControlToValidate="ddlCentroCosto" 
                    Display="Dynamic"
                    Text="<strong>*</strong>" 
                    SetFocusOnError="true" 
                    Type="Integer" 
                    Operator="GreaterThan"
                    ValueToCompare="0" 
                    ValidationGroup="grupo1"> </asp:CompareValidator>
             </div>
            <div style="clear:both; height:1px;"></div>        
        </div>         
    </div>
    
    <div class="usatPanelConsulta">
         <asp:Label ID="lblConsulta" runat="server" Text="Consulta de Perspectivas" Width="160px"></asp:Label>
     <div style="height:45px;">
            <div class="usatFormatoCampo1">
                <asp:Label ID="lblBuscar" runat="server" Text="Buscar Perspectiva"></asp:Label>
            </div>
               <div class="usatFormatoCampoAncho">
                &nbsp;<asp:TextBox ID="txtBuscar" runat="server" Width="500px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                    ControlToValidate="txtBuscar" Display="Dynamic" 
                    ErrorMessage="Ingrese carácteres válidos" 
                    ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ ,.]{3,}" 
                    text="*"
                    ValidationGroup="grupo3" SetFocusOnError="true" EnableTheming="True"></asp:RegularExpressionValidator>
                  
                     <asp:CustomValidator 
                    ID="CustomValidator4" 
                    runat="server" 
                    ErrorMessage="(*) Se encontraron palabras reservadas en la descripción del periodo." 
                    controltovalidate="txtBuscar" 
                    onservervalidate="CustomValidator4_ServerValidate"
                    ValidationGroup="grupo3" 
                    Text="*"
                    >
                </asp:CustomValidator>
                
                 <asp:Button ID="cmdBuscar" runat="server" Text="   Buscar" CssClass="buscar2" 
                       ValidationGroup="grupo3" />
                </div>
           <div style="clear:both; height:20px;"></div>       
        </div>
    
    <div>
     <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
            <tr>
                <td style="text-align:center">
                    <asp:GridView ID="gvListaPerspectivas" runat="server" Width="100%" CellPadding="4" 
                        ForeColor="#333333" AutoGenerateColumns="False" 
                        DataKeyNames="Codigo" EmptyDataText="No se encontraron registros." 
                        AllowPaging="True" PageSize="5">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <EmptyDataRowStyle ForeColor="#FF0066" />
                        <Columns>
                            <asp:BoundField HeaderText="N°" Visible="False" >
                                <ItemStyle Width="15px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server"  onclick="MarcarCursos(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>                
                                        <asp:CheckBox ID="chkElegir" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="15px" />
                                </asp:TemplateField>
                            <asp:BoundField HeaderText="Cod" DataField="Codigo" >
                            <ItemStyle Width="15px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" >
                                <ItemStyle Width="155px" />
                            </asp:BoundField>
                            <asp:CommandField ShowDeleteButton="True" 
                                DeleteImageUrl="../Images/eliminar.gif" ButtonType="Image" >
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
        </table>
    </div>
     
   </div>
   <div>    
        
        <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0"> 
            <tr style="background-color: #E8EEF7; font-weight: bold; height: 100%">
                <td style="width:80%">
                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        DisplayMode="BulletList" ShowSummary="true" HeaderText="Errores encontrados:" 
                        BorderWidth="0" ValidationGroup="grupo1" Font-Bold="False" 
                        ForeColor="#FF0066"/>
                     <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                        DisplayMode="BulletList" ShowSummary="true" HeaderText="Errores de Búsqueda Encontrados:" 
                        BorderWidth="0" ValidationGroup="grupo3" Font-Bold="False" 
                        ForeColor="#FF0066"/>
                </td>
                
                        <td align="right" style="width:20%" valign="top">
                                <asp:Button ID="cmdGuardar" runat="server" Text="    Agregar" 
                                    CssClass="agregar2" ValidationGroup="grupo1"/> &nbsp;<asp:Button 
                                    ID="cmdCancelar" runat="server" Text="Limpiar" CssClass="regresar2" 
                                    Width="70px" />
                        </td>
                
            </tr>
        </table>
        <br />       
    </div>
   <div class="usatPanelConfiguracion">
     <asp:Label ID="lblConsulta2" runat="server" Text="Configuración Plan Perspectiva" Width="200px"></asp:Label>
        
    <div>
     <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
            <tr>
                <td style="text-align:center">
                    <asp:GridView ID="gvPlanPerspectiva" runat="server" Width="100%" CellPadding="4" 
                        ForeColor="#333333" AutoGenerateColumns="False" 
                        DataKeyNames="Codigo,codigo_pla,codigo_cco,codigo_pers" 
                        EmptyDataText="No se encontro ninguna perspectiva configurada para este plan." 
                        PageSize="15">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <EmptyDataRowStyle ForeColor="#FF0066" />
                        <Columns>
                            <asp:BoundField HeaderText="N°" >
                            <ItemStyle Width="15px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Codigo" DataField="codigo" Visible="False" />
                            <asp:BoundField HeaderText="Plan" DataField="Planes" >
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Centro Costo" DataField="CentroCosto" >
                            <ItemStyle HorizontalAlign="Left" Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Perspectiva" HeaderText="Perspectiva" >
                            <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" >
                            <ItemStyle Width="160px" />
                            </asp:BoundField>
                            <asp:CommandField ShowSelectButton="True" 
                                SelectImageUrl="~/Indicadores/images/editar.gif" ButtonType="Image" 
                                Visible="False" >
                            <ItemStyle Width="15px" />
                            </asp:CommandField>
                            <asp:CommandField ShowDeleteButton="True" 
                                DeleteImageUrl="~/Images/eliminar.gif" ButtonType="Image" >
                            <ItemStyle Width="15px" />
                            </asp:CommandField>
                            <asp:BoundField DataField="codigo_pla" HeaderText="codigo_pla" Visible="False">
                            <ItemStyle Width="15px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_cco" HeaderText="codigo_cco" Visible="False">
                            <ItemStyle Width="15px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_pers" HeaderText="codigo_pers" 
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
