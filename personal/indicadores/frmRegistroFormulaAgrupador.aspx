<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistroFormulaAgrupador.aspx.vb" Inherits="indicadores_frmRegistroFormulaAgrupador" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
     <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
     <script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
     <link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />


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
        	padding-left:30px;
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
	        height:130px;	        
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
    
    
     <style type="text/css">
          .ajax__myTab .ajax__tab_header {
        font-family: verdana,tahoma,helvetica;
        font-size: 11px;
        border-bottom: solid 1px #999999
     }
     
    .ajax__myTab .ajax__tab_outer {
        padding-right: 4px;
        height: 21px;
        background-color: #C0C0C0;
        margin-right: 2px;
        border-right: solid 1px #666666;
        border-top: solid 1px #aaaaaa
     }
     
    .ajax__myTab .ajax__tab_inner {
        padding-left: 3px;
        background-color: #C0C0C0;
     }
     
    .ajax__myTab .ajax__tab_tab {
        height: 13px;
        padding: 4px;
        margin: 0;
     }
     
    .ajax__myTab .ajax__tab_hover .ajax__tab_outer {
        background-color: #cccccc
     }
     
    .ajax__myTab .ajax__tab_hover .ajax__tab_inner {
        background-color: #cccccc
     }
     
    .ajax__myTab .ajax__tab_hover .ajax__tab_tab {}
    
    .ajax__myTab .ajax__tab_active .ajax__tab_outer {
        background-color: #fff;
        border-left: solid 1px #999999;
     }
     
    .ajax__myTab .ajax__tab_active .ajax__tab_inner {
        background-color:#fff;
     }
     
    .ajax__myTab .ajax__tab_active .ajax__tab_tab {}
    
    .ajax__myTab .ajax__tab_body {
        font-family: verdana,tahoma,helvetica;
        font-size: 10pt;
        border: 1px solid #999999;
        border-top: 0;
        padding: 8px;
        background-color: #ffffff;
      }
    
    
    </style>   
        

    
</head>
<body>
     <form id="form1" runat="server">
    <div class="usatTituloAzul">Fórmula Indicador - Agrupador
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
                <asp:Label ID="lblCodigo" runat="server" Text="Label" ForeColor="#FF3399" 
                    Visible="False"></asp:Label>
            <asp:Label ID="lblVariable" runat="server" Text="Plan Perspectiva Vigente"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:DropDownList ID="ddlPerspectivaplan" runat="server" Width="600px" 
                    AutoPostBack="True">
                </asp:DropDownList>                
                  <asp:CompareValidator 
                    ID="CompareValidator1" 
                    runat="server" 
                    ErrorMessage="(*)  Seleccione el plan vigente de la lista desplegable." 
                    ControlToValidate="ddlPerspectivaplan" 
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
        
        <div style="margin-top:10px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="Label2" runat="server" Text="Seleccione un Objetivo"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                     <asp:DropDownList ID="ddlObjetivos" runat="server" Width="600px" 
                    AutoPostBack="True">
                </asp:DropDownList>                
               <asp:CompareValidator 
                    ID="CompareValidator2" 
                    runat="server" 
                    ErrorMessage="(*)  Seleccione un objetivo de las lista desplegable." 
                    ControlToValidate="ddlObjetivos" 
                    Display="Dynamic" 
                    Operator="NotEqual" 
                    SetFocusOnError="true" 
                    ValidationGroup="grupo1" 
                    ValueToCompare="0"
                    text="*"
                    ></asp:CompareValidator> 
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlPerspectivaplan" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        
        <div style="margin-top:10px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="Label3" runat="server" Text="Seleccione un Indicador"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                          <asp:DropDownList ID="ddlIndicador" runat="server" Width="600px">
                </asp:DropDownList>                
                  <asp:CompareValidator 
                    ID="CompareValidator3" 
                    runat="server" 
                    ErrorMessage="(*)  Seleccione un indicador de las lista desplegable." 
                    ControlToValidate="ddlIndicador" 
                    Display="Dynamic" 
                    Operator="NotEqual" 
                    ValidationGroup="grupo1" 
                    SetFocusOnError="true" 
                    ValueToCompare="0"
                    text="*"
                    ></asp:CompareValidator> 
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlObjetivos" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
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
                    <asp:TextBox ID="txtBuscarIndicador" runat="server" Width="400px" ></asp:TextBox>
                    
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
                        <asp:GridView ID="gvFormulas" runat="server" Width="100%" CellPadding="4" 
                            EmptyDataText="No se encontraron fórmulas registradas." 
                            ForeColor="#333333" AutoGenerateColumns="False" 
                            DataKeyNames="Codigo,codigo_ind,codigo_obj,codigo_ppla,FormulaC" AllowPaging="True" 
                            PageSize="5">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EmptyDataRowStyle ForeColor="#FF0066" />
                            <Columns>
                                <asp:BoundField HeaderText="N°" >
                                <ItemStyle Width="10px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Codigo" DataField="Codigo" Visible="False" />                                               
                                <asp:BoundField HeaderText="Indicador" DataField="Indicador" >
                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                </asp:BoundField>                                                                
                                <asp:BoundField DataField="Formula" HeaderText="Formula" >
                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
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
                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                </asp:BoundField>
                                <asp:CommandField ShowSelectButton="True" 
                                    SelectImageUrl="../images/editar.gif" ButtonType="Image" 
                                    SelectText="Modificar" >
                                <ItemStyle HorizontalAlign="Center" Width="15px" />
                                </asp:CommandField>
                                <asp:CommandField ShowDeleteButton="True" 
                                    DeleteImageUrl="../images/eliminar.gif" ButtonType="Image" >
                                <HeaderStyle Width="15px" />
                                <ItemStyle Width="15px" />
                                </asp:CommandField>
                            </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#FF0066" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
                    <tr style="background-color: #E8EEF7; font-weight: bold; height: 15px">
                    <td style="width:50%" align="center">
                 
                                                    <asp:ImageButton ID="ImageButton7" runat="server" 
                                                        ImageUrl="../Iconos/key_1.png" 
                        BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton8" runat="server" 
                                                        ImageUrl="../Iconos/key_2.png" 
                        BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton9" runat="server" 
                                                        ImageUrl="../Iconos/key_3.png" 
                        BackColor="#E8EEF7" Height="43px" />
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
                                                        ImageUrl="../Iconos/key_punto.png" BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton13" runat="server" 
                                                        ImageUrl="../Iconos/icono_parentesis_derecha.png" BackColor="#E8EEF7" 
                                                        Height="43px" />
                                                    <asp:ImageButton ID="ImageButton14" runat="server" 
                                                        ImageUrl="../Iconos/icono_parentesis_izquierda.png" BackColor="#E8EEF7" 
                                                        Height="43px" />
                                                    <asp:ImageButton ID="ImageButton22" runat="server" 
                                                        ImageUrl="../Iconos/key_dash.png" BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton23" runat="server" 
                                                        ImageUrl="../Iconos/key_plus.png" BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton11" runat="server" 
                                                        ImageUrl="../Iconos/keydiv.png" BackColor="#E8EEF7" Height="43px" />
                                                    <asp:ImageButton ID="ImageButton12" runat="server" 
                                                        ImageUrl="../Iconos/key_star.png" BackColor="#E8EEF7" Height="43px" />
                 
                 </td>
                
            </tr>
                         
            
        </table>
            <br />    
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
        <div style="width:100%; margin-top:5px" >
             <div style="float:left; width: 100%; height: 100%;">
                 <table cellpadding="3" cellspacing="0" style="width: 100%; border:0px solid #96ACE7" border="0">
                        <tr>
                            <td style="text-align:left" align="left">
                               
                                <br />
                               
                                <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" cssClass="ajax__myTab">
                                    <cc1:TabPanel runat="server" HeaderText="Lista Indicadores" ID="TabPanel1">
                                            
                                    <HeaderTemplate>
Lista Indicadores
</HeaderTemplate>
<ContentTemplate> 
                                                    
                                            
                                                    
                                                    
                                            
                                                    
 
                                                    
                                            
                                                    
                                                    
                                            
                                                    
                                                    
                                            
                                                    
                                                    
                                            
                                                    
 
                                                    
                                            
                                                    
                                                    
                                            
                                                    
                                                    
                                            
                                                    
                                                    
                                            
                                                    
 
                                                    
                                            
                                                    
                                                    
                                            
                                                    
                                                    
                                            
                                                    
                                                    
                                            
                                                    
 
                                                    
                                            
                                                    
                                                    
                                            
                                                    <asp:TreeView ID="treeInd" runat="server" MaxDataBindDepth="4" 
                                                        ExpandDepth="1" Font-Size="XX-Small" ><Nodes>
<asp:TreeNode PopulateOnDemand="True" Text="LISTA PRINCIPAL INDICADORES" Value="Lista" 
                                                                        SelectAction="Expand"></asp:TreeNode>
</Nodes>

<HoverNodeStyle CssClass="menuporelegir" />
</asp:TreeView>
                                
                                            

                                
                                            
</ContentTemplate>
                                            
</cc1:TabPanel>
                                    
                                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Lista Variables">
                                        <ContentTemplate> 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               
 
                                               <asp:TreeView ID="treePrueba" runat="server" MaxDataBindDepth="4" 
                                                    ExpandDepth="1" Font-Size="XX-Small">
                                                <Nodes>
                                                    <asp:TreeNode PopulateOnDemand="True" Text="LISTA PRINCIPAL VARIABLES" Value="Lista" 
                                                        SelectAction="Expand">
                                                    </asp:TreeNode>
                                                </Nodes>
                                                    <HoverNodeStyle CssClass="menuporelegir" />
                                                </asp:TreeView>
                                        
                                        
</ContentTemplate>
                                    




</cc1:TabPanel>
                                </cc1:TabContainer>
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
