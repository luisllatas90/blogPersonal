<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmDescargarArchivo.aspx.vb" Inherits="indicadores_FrmDescargarArchivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Descarga de archivos</title>
    <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <style type="text/css">
        .titulo
        {
            width: 100%;
            font-family: Arial;
            font-size: large;            
        }
        .usatFormatoCampo
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;      	
        	width:20%;
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
        	width:70%;
        	/*border:1px solid red;*/
        }
        
        .usatFormatoCampoAncho2
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:40px;
        	width:70%;
        	/*border:1px solid red;*/
        }
        
         .mensajeError
        {
        	border: 1px solid #e99491;
        	background-color: #fed8d5;        	
        	padding-top:2px;        	
        	-moz-border-radius: 15px;
        	padding-left:25px;  	
            padding-right: 25px;
        }
        
         .mensajeExito
        {
        	border: 1px solid #488e00;
        	background-color:#c5f4b5;        	
        	padding-top:2px;        	
        	-moz-border-radius: 15px;
        	padding-left:25px;        	
        	padding-right: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="fila1" class="usatFormatoCampo">
            Plan
        </div>
        <div class="usatFormatoCampoAncho">
            <asp:DropDownList ID="cboPlan" runat="server" Width="100%" AutoPostBack="True">
            </asp:DropDownList>
        </div>
    </div>
    <br /><br />
    <div>    
        <asp:GridView ID="gvArchivos" runat="server" Width="100%" 
            AutoGenerateColumns="False" 
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">            
            <RowStyle Height="20px" ForeColor="#000066" />
            <Columns>                
                <asp:BoundField DataField="nombre_ppe" HeaderText="Nombre" />
                <asp:BoundField DataField="descripcioon_ppe" HeaderText="Descripcion" />
                <asp:BoundField DataField="archivo_ppe" HeaderText="Descargar" >
                    <ItemStyle Width="10%" />
                </asp:BoundField>                
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#284775" Font-Bold="True" ForeColor="White" Height="22px" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
