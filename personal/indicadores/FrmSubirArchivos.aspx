<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmSubirArchivos.aspx.vb" Inherits="indicadores_FrmSubirArchivos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
            <asp:DropDownList ID="cboPlan" runat="server" Width="100%">
            </asp:DropDownList>
        </div>
    </div>
    <div>
        <div id="Div1" class="usatFormatoCampo">
            Facultad
        </div>
        <div class="usatFormatoCampoAncho">
            <asp:DropDownList ID="cboFacultad" runat="server" Width="100%" AutoPostBack="True">
            </asp:DropDownList>
        </div>
    </div>
    <div>
        <div id="fila2" class="usatFormatoCampo">
            Titulo
        </div>
        <div class="usatFormatoCampoAncho">
            <asp:TextBox ID="txtTitulo" runat="server" Width="100%"></asp:TextBox>
        </div>
    </div>    
    <div>
        <div id="fila3" class="usatFormatoCampo">
            Descripci&oacute;n
        </div>
        <div class="usatFormatoCampoAncho2">
            <asp:TextBox ID="txtDescripcion" runat="server" Width="100%" MaxLength="300" 
                TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div>
        <div id="fila4" class="usatFormatoCampo">
            Archivo
        </div>
        <div class="usatFormatoCampoAncho">
            <asp:FileUpload ID="FileUpload1" runat="server" Width="100%" />
        </div>
    </div>
    <div>
        <div id="fila5" class="usatFormatoCampo">
            <asp:Button ID="btnSubirArchivo" runat="server" Text="Guardar" Width="100px" Height="24px" CssClass="guardar2" />
        </div>
        <div class="usatFormatoCampoAncho">
            <asp:Label ID="lblAviso" runat="server" Text="" CssClass="mensajeError"></asp:Label><br />
            <asp:Label ID="lblExito" runat="server" Text="" CssClass="mensajeExito"></asp:Label>
        </div>
        
    </div>
    <br /><br /><br />
    <div>
        <asp:GridView ID="gvArchivos" runat="server" Width="100%" 
            AutoGenerateColumns="False" DataKeyNames="codigo_ppe"
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">            
            <RowStyle Height="20px" ForeColor="#000066" />
            <Columns>
                <asp:BoundField DataField="codigo_ppe" HeaderText="codigo_ppe" 
                    Visible="False" />
                <asp:BoundField DataField="nombre_ppe" HeaderText="Nombre" />
                <asp:BoundField DataField="descripcioon_ppe" HeaderText="Descripcion" />
                <asp:BoundField DataField="archivo_ppe" HeaderText="Descargar" >
                    <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:CommandField ShowDeleteButton="True">
                    <ItemStyle Width="10%" />
                </asp:CommandField>
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