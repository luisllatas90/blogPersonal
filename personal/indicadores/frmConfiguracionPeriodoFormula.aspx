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
    <div>
    <!---NUEVO DISEÑO -->
        <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
            <tr>
                <td bgcolor="#D1DDEF" colspan="6" height="30px">
                <b>
                    <asp:Label ID="Label8" runat="server" Text="Configuración Periodo Fórmula"></asp:Label></b></td>
            </tr>
            <tr>
                <td bgcolor="#DECE9C" colspan="6" height="10px">
                <b>
                    <asp:Label ID="Label10" runat="server" Text="Filtros" Font-Bold="True" 
                    ></asp:Label></b></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Filtrar Fórmulas ..."></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rbtTodos" Text="TODAS" runat="server" Checked="True" 
                                    GroupName="Opciones" AutoPostBack="True" />
                            </td>
                            <td>
                                <asp:RadioButton ID="rbtProcesadas" runat="server" Text="PROCESADAS" 
                                    GroupName="Opciones" AutoPostBack="True" Font-Bold="True" 
                                    ForeColor="#33CC33" />
                            </td>
                            <td>
                                <asp:RadioButton ID="rbtNoProcesadas" runat="server" Text="SIN PROCESO ACTUAL" 
                                    GroupName="Opciones" AutoPostBack="True" Font-Bold="True" 
                                    ForeColor="#FF9933" />
                            </td>
                            <td>
                                <asp:RadioButton ID="rbtErrores" runat="server" Text="CON ERRORES" 
                                    GroupName="Opciones" AutoPostBack="True" Font-Bold="True" 
                                    ForeColor="#FF3300" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Seleccione un Plan"></asp:Label>
                </td>
                <td>
                    
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
                    
                </td>
                
            </tr>
            <tr>
                <td>
                <asp:Label ID="Label9" runat="server" Text="Año / Objetivo"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAnioBus" Width="98px" runat="server" 
                        AutoPostBack="True" Font-Size="Smaller">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlListaObjetivos" Width="400px" runat="server" 
                        Font-Size="Smaller" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                <asp:Label ID="lblBuscar" runat="server" Text="Descripción del Indicador"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBuscar" runat="server" Width="500px" Font-Size="Smaller"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                
                    <asp:Button ID="cmdBuscar" runat="server" Text="   Buscar" 
                    CssClass="buscar2"  Width="112px"/> 
                    
                    <!--ValidationGroup="grupo2" -->
                   
                     
                    <asp:CustomValidator   ID="CustomValidator2" 
                                        runat="server" 
                                        ErrorMessage="Se encontraron palabras reservadas en la búsqueda de la Fórmula." 
                                        ControlToValidate="txtBuscar" 
                                        Display="Dynamic"                                     
                                        onservervalidate="CustomValidator2_ServerValidate"
                                        Text="*" ValidationGroup="grupo2"
                                        >
                    </asp:CustomValidator>   
                
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
                
                    </td>
            </tr>
            <tr>
                <td bgcolor="#DECE9C" colspan="6"  height="10px">
                <b>
                    <asp:Label ID="Label2" runat="server" 
                        Text="Seleccione el Periódo al que se asignarán las Fórmulas" 
                        Font-Bold="True" 
                         ></asp:Label></b></td>
            </tr>
            
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Periodo: "></asp:Label>
                </td>
                <td>
                    
		                         <asp:DropDownList ID="ddlPeriodo" runat="server" Width="500px" AutoPostBack="True" BackColor="#CCFFFF" Font-Size="Smaller"> </asp:DropDownList>
                    
                </td>
            </tr>
            
               <tr>
                <td colspan="5">
                    <hr width="100%" align="center" style="color: #0056b2;" />
                </td>
            </tr>
            
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="5">
                    <table>
                        <tr>
                            <td>
                            
                                    <asp:Image ID="Image2" runat="server" Height="16px" 
                                        ImageUrl="~/Images/bola_verde.gif" Width="16px" />  
                            
                            </td>
                            <td>
                                    <asp:Label ID="Label5" runat="server" Text="F. Procesadas"></asp:Label>
                            </td>
                            <td>
                            
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/bola_naranja.gif" />
                            
                            </td>
                            <td>
                            
                                    <asp:Label ID="Label3" runat="server" Text="F. Sin Procesar"></asp:Label>
                            
                            </td>
                              <td>
                            
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/bola_roja.gif" 
                                        Height="17px" />
                            
                            </td>
                            <td>
                            
                                    <asp:Label ID="Label6" runat="server" Text="F. Errores de Sintaxis/Valor"></asp:Label>
                            
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
        </table>
    
        <asp:Panel ID="pnlGeneral" runat="server">
            <table cellspacing="0" cellpadding="0" style="border-collapse: collapse;bordercolor: #111111;width:100%">
            <!--Tabs-->
            <tr>
		        <td class="pestanabloqueada" id="Td2" align="center" style="height:15px;width:15%" onclick="ResaltarPestana2('0','','');">
                    <asp:LinkButton ID="lnkConfiguracion" Text="Listado de Fórmulas" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue"></asp:LinkButton>
            </td>
                <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
    			<td class="pestanaresaltada" id="tab" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('1','','');">
                    <asp:LinkButton ID="lnkConsulta" Text="Listado de Fórmulas Asigandas Periodo" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue"></asp:LinkButton>
            </td>
	    		<td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
		    </tr>
		
		    
		<!-- Lista Formulas -->    
        
        <asp:Panel ID="pnlListaFormulas" runat="server"  Width="100%">
            <tr>
		            <td bgcolor="#D1DDEF" colspan="4" height="15px">
                    <b>
                    <asp:Label ID="Label11" runat="server" Text=""></asp:Label></b></td>
                    <div id="avisos" runat="server" style="height:20px; padding-top:2px; width: 98%;">
                        <asp:Image ID="Image1" runat="server" ImageUrl="../Images/beforelastchild.GIF"/>
                        <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>            
                    </div>
		   </tr>
		   <tr>
                <td colspan="4">
                        <asp:Panel ID="pnlOpcionesListaFormula" Visible="false" runat="server">
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
                                    CssClass="agregar2" ValidationGroup="grupo1"/> &nbsp;
                                    
                                    <asp:Button 
                                    ID="cmdCancelar" runat="server" Text="Limpiar" CssClass="regresar2" 
                                    Width="70px" />
                            </td>
                        </tr>
                    </table>
                        </asp:Panel>
                     
                 </td>
		   </tr>
		   <tr>
                <td colspan="4">
			            <asp:GridView ID="gvListaFormulas" 
                        runat="server" Width="100%" 
                        CellPadding="3" AutoGenerateColumns="False" 
                        DataKeyNames="Codigo" EmptyDataText="No se encontraron registros..." 
                        AllowPaging="False" Font-Size="Smaller" BackColor="White" 
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
        </asp:Panel>
    	
    	<!------------------------------------------------------------------------------------------------->
    	
    	    <!-- PANEL CONFIGURACION DE FORMULAS -->
    	    <asp:Panel ID="pnlFormulasConf" runat="server">
    	        
    	        
    	        <!-- Botones Procesa y Eliminar -->
    	        <tr>
                    <td colspan="4">
                        <asp:Panel ID="Panel1" Visible="true" runat="server">
                            <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0"> 
                                <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                                    <td align="right" style="width:20%" valign="middle">
                                            
                                            <asp:Button ID="btnProcesar" runat="server" Width="150px" ToolTip="Procesa los registros con el check activo"
                                            Text="    Procesar Fórmula" CssClass="boton" /> &nbsp;
                                            
                                            <asp:Button 
                                            ID="btnEliminar" runat="server" Text=" Eliminar Fórmula" ToolTip="Elimina los registros con el check activo"
                                            CssClass="eliminar" Width="150px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                 </td>
		   </tr>
		   <!-- fin botones procesar y eliminar -->
    	            
                    <tr>
                        <td colspan="4">
		                <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">

                                    <tr>
                                         <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        
                    <asp:Label ID="lblCodigo" runat="server" Text="CodPers" ForeColor="#66FF33" 
                        Visible="False"></asp:Label>
                
                </td>
                <td>
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
                    
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                         </td>
		                            </tr>
                                    <tr>
                                        <td style="height:100%;width:100%" valign="top" class="pestanarevez">
                                            <table cellpadding="3" cellspacing="0" style=" width:100%; border:1px solid #96ACE7" border="0">
                                                <tr>
                                                      <td style="text-align:center" >
                                                            <asp:GridView ID="gvPeriodoFormula" runat="server" Width="100%" CellPadding="3" AutoGenerateColumns="False" 
                            DataKeyNames="Codigo,codigo_pdo,codigo_for" 
                            EmptyDataText="No se encontro registros..." 
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
                                    ImageUrl="~/Images/processIND2t.png">
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
                                <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkHeader" runat="server"  onclick="MarcarCursos(this)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>                
                                            <asp:CheckBox ID="chkElegir01" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="5px" />
                                    </asp:TemplateField>
                            </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#284775" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                                                      </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>            
                                </table>
                        </td>
		            </tr>
		    </asp:Panel>
		    <!-- FIN PANEL CONFIGURACION DE FORMULAS-->
		</table>
        </asp:Panel>
   

    
    
		
		
		   </div>
    <!-------- new look ------------------------>
    
     <!-- Para avisos -->
    <div style="clear:both; height:5px"></div>
    
    
    <div style="clear:both; height:10px"></div>
    <!-------------------------------------------->
    
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
        

     <!---# Cambios 2013 #--->
    
     
     
     
     
     
    </form>
</body>
</html>
