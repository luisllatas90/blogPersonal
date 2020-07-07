<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTableroControl.aspx.vb" Inherits="indicadores_frmTableroControl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
    <script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
    <link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />
            
    <style type="text/css">
        .usatFormatoCampo
        {
        	float:left; 
        	font-weight:bold;        	     	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:10px;
        	/*width:200px;*/
        	width:20%        	
        }
        
        .usatFormatoCampoAncho
        {
        	float:left; 
        	font-weight:bold;        	       	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:65%;
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
	        height:100px;	        
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
        	left: 10px;
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
        
        .borde-redondeado {
	        border-radius:30px;
	        -moz-boder-radius:30px; 
	        -webkit-border-radius:30px;
	        behavior: url(../css/border-radius.htc);	        	        
	        width:70px;
	        height:40px;
	        padding-top:5px;
	        text-align:center;
        }
        
        /********************************/   
                        
    </style>

</head>
<body>
    <form id="form1" runat="server">
    
    <div class="usatTituloAzul">Tablero de Control
         <a href="#" onclick="apprise('Puede visualizar el avance de los objetivos.');">
            <img src="../Images/info.png" style="border:0" alt="Ayuda."/>
        </a>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
     <!-- Para avisos -->
    <div style="clear:both; height:5px"></div>
    
    <div id="avisos" runat="server" style="height:25px; padding-top:2px; width: 50%">
    <asp:Image ID="Image1" runat="server" ImageUrl="../Images/beforelastchild.GIF"/>
    <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>            
    </div>
    <div style="clear:both; height:10px"></div>
    <!-------------------------------------------->
    
    <div class="usatPanel">        
        <asp:Label ID="lblSubtitulo" runat="server" Text="Datos de selección" 
            Width="120px"></asp:Label>
        <div>
            <div class="usatFormatoCampo">
            <asp:Label ID="lblCco" runat="server" Text="Seleccione Centro de Costos"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
              <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>--%>
                        <asp:DropDownList ID="ddlCco" runat="server" Width="100%">
                        </asp:DropDownList>
                        
                         <asp:CompareValidator  ID="CompareValidator1" 
                                        runat="server" 
                                        ErrorMessage="Debe seleccionar una Centro de Costos." 
                                        ControlToValidate="ddlCco" 
                                        Display="Dynamic" 
                                        Operator="NotEqual" 
                                        ValidationGroup="grupo1" 
                                        ValueToCompare="0"
                                        Text="*"
                                        >
                        </asp:CompareValidator>
                     <%--</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvSubvariable" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>     --%>
            </div>
            <div style="clear:both; height:5px;"></div>        
        </div>    
        <div>
            <div class="usatFormatoCampo">
                <asp:Label ID="lblPeriodo" runat="server" Text="Seleccione Periodo"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>--%>
                        <asp:DropDownList ID="ddlPeriodo" runat="server" Width="100%" 
                            AutoPostBack="false">
                        </asp:DropDownList>
                        
                        <asp:CompareValidator  ID="CompareValidator2" 
                                        runat="server" 
                                        ErrorMessage="Debe seleccionar un Periodo." 
                                        ControlToValidate="ddlPeriodo" 
                                        Display="Dynamic" 
                                        Operator="NotEqual" 
                                        ValidationGroup="grupo1" 
                                        ValueToCompare="0"
                                        Text="*"
                                        >
                         </asp:CompareValidator>
                                        
                    <%--</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvSubvariable" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>--%>
                
            </div>
            <div style="clear:both; height:5px;"></div>
        </div>
        
        <div>
            <div class="usatFormatoCampo">
                <asp:Label ID="lblPlan" runat="server" Text="Seleccione el Plan"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
               <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>--%>
                        <asp:DropDownList ID="ddlPlan" runat="server" Width="100%" 
                            AutoPostBack="false">
                        </asp:DropDownList>
                        
                         <asp:CompareValidator  ID="CompareValidator3" 
                                        runat="server" 
                                        ErrorMessage="Debe seleccionar un Plan." 
                                        ControlToValidate="ddlPlan" 
                                        Display="Dynamic" 
                                        Operator="NotEqual" 
                                        ValidationGroup="grupo1" 
                                        ValueToCompare="0"
                                        Text="*"
                                        >
                        </asp:CompareValidator>
                                        
                    <%--</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvSubvariable" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>--%>
                
            </div>
            <div style="clear:both; height:5px;"></div>
        </div>    
                
    </div>
    
    <div>    
        
        <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0"> 
            <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                <td style="width:80%">
                    </td>
                        <td align="right" style="width:20%">
                                <asp:Button ID="cmdBuscar" runat="server" Text="   Buscar" 
                                    CssClass="guardar2" ValidationGroup="grupo1"/> &nbsp;<asp:Button 
                                    ID="cmdCancelar" runat="server" Text="Limpiar" CssClass="regresar2" 
                                    Width="70px" />
                </td>
            </tr>
            
             <tr style="background-color: #E8EEF7; font-weight: bold">
                <td colspan="2">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        DisplayMode="BulletList" ShowSummary="true" HeaderText="Errores encontrados:" 
                        BorderWidth="0" ValidationGroup="grupo1" Font-Bold="False" 
                        ForeColor="#FF0066"/>
                                        
                </td>
            </tr>
        </table>
        <br />       
    </div>
    
    <div id="contenedor" runat="server">
        
    </div>                                           
    
    </form>
</body>
</html>
