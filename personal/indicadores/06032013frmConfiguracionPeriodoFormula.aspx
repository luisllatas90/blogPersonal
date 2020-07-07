<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConfiguracionPeriodoFormula.aspx.vb" Inherits="Indicadores_Formularios_frmConfiguracionPeriodoFormula" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="../javascript/funciones.js"  type="text/javascript" language="JavaScript"/>
    <script src="../javascript/funciones.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
    <script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
    <link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />
    
    <script src="../javascript/jq/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../javascript/jq/lbox/thickbox.js" type="text/javascript"></script>
    <link  href="../javascript/jq/lbox/thickbox.css" rel="stylesheet" type="text/css" media="screen" />
    
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
        	height:85px;
        	width:500px; 
        	
        	       	
        }
        
        .usatFormatoCampo2
        {
        	float:right; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:85px;
        	width:205px;
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
        	padding-left:60px;
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
        	padding-left:60px;
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
	        height:50px;	        
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
        
        #lblConsultax
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
      <div class="usatTituloAzul">Configuración del Periodo Formula
    <a href="#" onclick="apprise('Permite configurar una determinada Fórmula para un Periodo.');">
        <img src="../Images/info.png" style="border:0" alt="Ayuda."/>
    </a>
    </div>
     
    <%--<div class="usatDescripcionTitulo">Configuración.</div>--%>
    
    
     <!-- Para avisos -->
    <div style="clear:both; height:5px"></div>
    
    <div id="avisos" runat="server" style="height:20px; padding-top:2px; width: 60%;">
    <asp:Image ID="Image1" runat="server" ImageUrl="../Images/beforelastchild.GIF"/>
    <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>            
    </div>
    <div style="clear:both; height:10px"></div>
    <!-------------------------------------------->
    
    <div class="usatPanel">
        <asp:Label ID="lblSubtitulo" runat="server" Text="Datos de registro"></asp:Label>
 
         <div style="margin-top:10px;">
            <div class="usatFormatoCampo1">
                <asp:Label ID="lblCodigo" runat="server" Text="CodPers" ForeColor="#66FF33" 
                    Visible="False"></asp:Label>
            <asp:Label ID="lblPlan" runat="server" Text="Seleccione un Perido"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                &nbsp;<asp:DropDownList ID="ddlPeriodo" runat="server" Width="500px" 
                    AutoPostBack="True" BackColor="#CCFFFF" Font-Size="Smaller">
                </asp:DropDownList>
                <asp:CompareValidator 
                    ID="CompareValidator1" 
                    runat="server" 
                    ErrorMessage="(*) Seleccione un Periodo de la lista desplegable."
                    ControlToValidate="ddlPeriodo" 
                    Display="Dynamic"
                    Text="<strong>*</strong>" 
                    SetFocusOnError="true" 
                    Type="String" 
                    Operator="GreaterThan"
                    ValueToCompare="P0" 
                    ValidationGroup="grupo1">
                    
                </asp:CompareValidator>
             </div>
            <div style="clear:both; height:1px;"></div>        
        </div>  
       
    </div>
    
    <div class="usatPanelConsulta">
    <asp:Label ID="lblConsultax" runat="server" Text="Consulta de Fórmulas" Width="160px"></asp:Label>
    <div style="height:28px;">
            <div class="usatFormatoCampo1">
                <asp:Label ID="Label4" runat="server" Text="Seleccione un Plan"></asp:Label>
            </div>
               <div class="usatFormatoCampoAncho">
                    <asp:DropDownList ID="ddlPlan" Width="500px" runat="server" AutoPostBack="True" 
                        Font-Size="Smaller">
                    </asp:DropDownList>
                    
                        <asp:CompareValidator   ID="CompareValidator5" 
                                        runat="server" 
                                        ErrorMessage="Debe seleccionar el Plan." 
                                        ControlToValidate="ddlPlan" 
                                        Display="Dynamic" 
                                        Operator="NotEqual" 
                                        ValidationGroup="grupo2" 
                                        ValueToCompare="0"
                                        Text="*"
                                        > </asp:CompareValidator> 
                </div>
           <div style="clear:both; height:1px;"></div>       
    </div>
    <div style="height:28px;">
            <div class="usatFormatoCampo1">
                <asp:Label ID="Label9" runat="server" Text="Año / Objetivo"></asp:Label>
            </div>
               <div class="usatFormatoCampoAncho">
                    <asp:DropDownList ID="ddlAnioBus" Width="98px" runat="server" 
                        AutoPostBack="True" Font-Size="Smaller">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlListaObjetivos" Width="400px" runat="server" 
                        Font-Size="Smaller" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
           <div style="clear:both; height:1px;"></div>       
    </div>
        
    
     <div style="height:28px;">
            <div class="usatFormatoCampo1">
                <asp:Label ID="lblBuscar" runat="server" Text="Descripción del Indicador"></asp:Label>
            </div>
               <div class="usatFormatoCampoAncho">
                    <asp:TextBox ID="txtBuscar" runat="server" Width="500px" Font-Size="Smaller"></asp:TextBox>
                    
                    <!-- Para Validar Caja de Busqueda -->
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                                                runat="server" 
                                                ControlToValidate="txtBuscar" 
                                                Display="Dynamic" 
                                                ErrorMessage="Ingrese carácteres válidos para buscar la Fórmula." 
                                                ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ ,.]{3,}" 
                                                ValidationGroup="grupo2" 
                                                SetFocusOnError="true" 
                                                EnableTheming="True"
                                                Text="*"
                                                >
                    </asp:RegularExpressionValidator>
                
                    <asp:CustomValidator   ID="CustomValidator2" 
                                        runat="server" 
                                        ErrorMessage="Se encontraron palabras reservadas en la búsqueda de la Fórmula." 
                                        ControlToValidate="txtBuscar" 
                                        Display="Dynamic"                                     
                                        onservervalidate="CustomValidator2_ServerValidate"
                                        Text="*" ValidationGroup="grupo2"
                                        >
                    </asp:CustomValidator>   
                
                    <!------------------------------------------------------------------------------------>
                    
                    <asp:Button ID="cmdBuscar" runat="server" Text="   Buscar" 
                    CssClass="buscar2" ValidationGroup="grupo2"/> <!-- Para Validar Caja de Busqueda -->
                </div>
           <div style="clear:both; height:20px;"></div>       
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
     <table cellpadding="3" cellspacing="0" style=" width:100%; border:1px solid #96ACE7" border="0">
            <tr>
                <td style="text-align:center" >
                    <asp:GridView ID="gvListaFormulas" 
                        runat="server" Width="100%" 
                        CellPadding="3" AutoGenerateColumns="False" 
                        DataKeyNames="Codigo" EmptyDataText="No se encontraron fórmulas registradas." 
                        AllowPaging="True" Font-Size="Smaller" BackColor="White" 
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                        <RowStyle ForeColor="#000066" />
                        <EmptyDataRowStyle ForeColor="#FF0066" />
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
                            <asp:BoundField HeaderText="N°" Visible="False" >
                                <ItemStyle Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Periodo_pla" HeaderText="PLAN">
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="CODIGO" DataField="Codigo" Visible="False" >
                            <ItemStyle Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Prs" HeaderText="PRS">
                            <ItemStyle HorizontalAlign="Justify" Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="OBJ" DataField="Obj" >
                                <ItemStyle HorizontalAlign="Center" Width="10px" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Indicador" HeaderText="INDICADOR" >
                                <ItemStyle Width="120px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="año_ind" HeaderText="AÑO" >
                            <ItemStyle Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ponderacion_ind" HeaderText="POND." >
                            <ItemStyle Width="15px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Formula" HeaderText="FORMULA" >
                            <ItemStyle HorizontalAlign="Left" Width="200px" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Sintaxis" >
                            <ItemStyle Width="2.5%" />
                            </asp:BoundField>
                            <asp:CommandField ShowDeleteButton="True" 
                                DeleteImageUrl="~/Images/eliminar.gif" ButtonType="Image" >
                                <ItemStyle Width="2.5%" />
                            </asp:CommandField>
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
    <div>    
        
        <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0"> 
            <tr style="background-color: #E8EEF7; font-weight: bold; height: 20px">
                <td style="width:80%">
                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        DisplayMode="BulletList" ShowSummary="true" HeaderText="Errores encontrados:" 
                        BorderWidth="0" ValidationGroup="grupo1" Font-Bold="False" 
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
     <div style="height:100%;">
            <div class="usatFormatoCampo">
                <asp:Label ID="Label1" runat="server" Text="Fórmulas Configuradas para el Periodo Seleccionado"></asp:Label>
                
            </div>
            
            <div class="usatFormatoCampo2">
                <div>
                                    <asp:Label ID="Label7" runat="server" Height="20px" Text="LEYENDA:  "></asp:Label></div>
                                <div>
                                    <asp:Image ID="Image2" runat="server" Height="16px" 
                                        ImageUrl="~/Images/bola_verde.gif" Width="16px" />  
                                    <asp:Label ID="Label5" runat="server" Text=" Fórmula Procesada."></asp:Label>
                                </div>
                                <div> 
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/bola_naranja.gif" />
                                    <asp:Label ID="Label3" runat="server" Text="Fórmula No Procesada. "></asp:Label>
                                </div>
                                <div>
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/bola_roja.gif" 
                                        Height="17px" />
                                    <asp:Label ID="Label6" runat="server" Text="Fórmula Con Errores de Sintaxis. "></asp:Label>
                                </div>
                
                
            </div>
            
            <div>
     <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
            <tr>
                <td style="text-align:center">
                <asp:GridView ID="gvPeriodoFormula" runat="server" Width="100%" CellPadding="3" AutoGenerateColumns="False" 
                        DataKeyNames="Codigo,codigo_pdo,codigo_for" 
                        EmptyDataText="No se encontro ninguna fórmula configurada para el periodo seleccionado." 
                        PageSize="15" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" 
                        BorderWidth="1px">
                        <RowStyle ForeColor="#000066" />
                        <EmptyDataRowStyle ForeColor="#FF0066" />
                        <Columns>
                            <asp:BoundField HeaderText="N°" >
                            <ItemStyle Width="15px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="CODIGO" DataField="codigo" />
                            <asp:BoundField DataField="Periodo_pla" HeaderText="PLAN" />
                            <asp:BoundField DataField="abreviatura" HeaderText="PRS.">
                                <ItemStyle HorizontalAlign="Left" Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="abreviatura_obj" HeaderText="OBJ.">
                                <ItemStyle HorizontalAlign="Left" Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Indicador" HeaderText="INDICADOR">
                                <ItemStyle HorizontalAlign="Left" Width="350px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Periodo" HeaderText="PERIODO">
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="FÓRMULA" DataField="Formula" >
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DescripcionValor_vf" HeaderText="FORMULA-VALOR" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ValorFormula_vf" HeaderText="VALOR" >
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:CommandField ShowSelectButton="True" 
                                SelectImageUrl="~/Images/tabla.gif" ButtonType="Image" >
                            <ItemStyle Width="15px" />
                            </asp:CommandField>
                            <asp:BoundField DataField="procesado_fp">
                                <ItemStyle Width="20px" />
                            </asp:BoundField>
                            <asp:ButtonField ButtonType="Image" CommandName="Procesar" Text="Procesar" 
                                HeaderText="PROCESAR" ImageUrl="~/Images/processIND2t.png">
                                <ItemStyle ForeColor="#009900" Width="40px" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            </asp:ButtonField>
                            <asp:CommandField ShowDeleteButton="True" 
                                DeleteImageUrl="~/Images/eliminar.gif" ButtonType="Image" >
                            <ItemStyle Width="15px" />
                            </asp:CommandField>
                            <asp:BoundField HeaderText="FECHAREGISTRO" DataField="FechaRegistro" 
                                Visible="False" >
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
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
