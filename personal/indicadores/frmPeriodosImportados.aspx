<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPeriodosImportados.aspx.vb" Inherits="indicadores_frmPeriodosImportados" %>

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
        
        
        #lblSubtitulo
        {
        	position:absolute;
        	top: 64px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:170px;        
        	text-align:center;
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
        	width:170px;        
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
    <div class="usatTituloAzul">Notificaciones de Importación</div>
      <div class="usatDescripcionTitulo">
        <asp:Button ID="Button1" runat="server" Text="   Regresar" 
            CssClass="regresar2"  onclientclick="self.parent.tb_remove();" UseSubmitBehavior="False" />
    </div>
    <div style="clear:both; height:10px"></div>
    <div class="usatPanelConsulta">
        <asp:Label ID="lblSubtitulo" runat="server" Text="Periodos Importados"></asp:Label>
     <div style="clear:both; height:10px"></div>
     <div>
     <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
            <tr>
                <td style="text-align:center">
                    <asp:GridView ID="gvListaPeriodosImportados" runat="server" Width="100%" 
                        CellPadding="4" AutoGenerateColumns="False" 
                        DataKeyNames="Codigo" 
                        EmptyDataText="No se encontraron registros." BackColor="White" 
                        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" GridLines="Vertical">
                        <RowStyle BackColor="White" ForeColor="#003399" />
                        <EmptyDataRowStyle ForeColor="#FF0066" />
                        <Columns>
                            <asp:BoundField HeaderText="Codigo" DataField="Codigo" Visible="False" />
                            <asp:BoundField HeaderText="Descripción de Variable" DataField="Descripcion" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Periodo" DataField="Periodo" />
                            <asp:BoundField DataField="Valor" HeaderText="Total" />
                        </Columns>
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
   </div>
   
   <div class="usatPanelConsulta">
        <asp:Label ID="lblSubtitulo2" runat="server" Text="Notificaciones"></asp:Label>
     <div>
     <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
            <tr>
                <td style="text-align:center">
                    <asp:GridView ID="gvNotificaciones" runat="server" Width="100%" 
                        CellPadding="4" AutoGenerateColumns="False" 
                        DataKeyNames="Codigo" 
                        EmptyDataText="No se encontraron registros." BackColor="White" 
                        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                        GridLines="Vertical">
                        <RowStyle BackColor="White" ForeColor="#003399" />
                        <EmptyDataRowStyle ForeColor="#FF0066" />
                        <Columns>
                            <asp:BoundField HeaderText="Codigo" DataField="Codigo" Visible="False" />
                            <asp:BoundField DataField="Variable" HeaderText="Descripción de Variable" />
                            <asp:BoundField HeaderText="Notificación" DataField="Notificacion" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Periodo" DataField="Periodo" />
                        </Columns>
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
   </div>
    
    </form>
</body>
</html>
