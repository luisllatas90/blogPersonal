<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmVerDatosImportados.aspx.vb" Inherits="Indicadores_Formularios_frmVerDatosImportados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" src="../javascript/calendario.js"></script>
    
    <style type="text/css">
        .usatFormatoCampo
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:20px;        	
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
        	height:28px;
        	width:700px;
        	/*border:1px solid red;*/
        }
        
        .usatTituloAzul {
	        font-family: Arial;
	        font-size: 12pt;
	        font-weight: bold;
	        color: #3063c5;
            height: 10px;
            width: 831px;
            height: 25px;
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
	        height:100%;	        
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
        	top: 80px;
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
                        
    </style>
    

</head>
<body>
    <form id="form1" runat="server">
    <div class="usatTituloAzul">Lista de Resultados Importados</div>
    <div class="usatDescripcionTitulo">
        <asp:Button ID="Button1" runat="server" Text="   Regresar" 
            CssClass="regresar2"  onclientclick="self.parent.tb_remove();" UseSubmitBehavior="False" />
    </div>
    <div class="cuadroMensajes"><asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label></div>
    
    <div>    
        
        <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0"> 
            <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                <td style="width:100%">
                     <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
                        <tr>
                            <td style="text-align:center; font-weight:bold;">Lista de Variables </td>                       
                        </tr>
                        <tr>
                            <td style="text-align:left" align="left">
                                        <asp:TreeView ID="treePrueba" runat="server" MaxDataBindDepth="4" 
                                            ExpandDepth="1" Font-Size="XX-Small">
                                            <Nodes>
                                                <asp:TreeNode PopulateOnDemand="True" Text="RESULTADOS " Value="Lista" 
                                                    SelectAction="Expand"></asp:TreeNode>
                                            </Nodes>
                                        </asp:TreeView>
                            </td>
                    </tr>
                </table>            
                    
                </td>
            </tr>
        </table>
        <br />       
    </div>
    
    </form>
</body>
</html>
