<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDetalleFormulaPeriodo.aspx.vb" Inherits="indicadores_frmDetalleFormulaPeriodo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
     <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="../javascript/funciones.js"  type="text/javascript" language="JavaScript"/>
    <script src="../javascript/funciones.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
    <script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
    <link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />
    
    <script src="../javascript/jq/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../javascript/jq/lbox/thickbox.js" type="text/javascript"></script>
    <link  href="../javascript/jq/lbox/thickbox.css" rel="stylesheet" type="text/css" media="screen" />
    
   <style type="text/css">
        .usatFormatoCampo
        {        	
        	float:left; 
        	font-weight:bold;
        	padding-top:20px;        	
        	padding-left:40px;
        	color:#27333c;
        	font-family: Arial;
        	height:10px;
        	width:200px;    
        }
        
        .usatFormatoCampoAncho
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:15px;        	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:10px;
        	width:500px;
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
	        height: 200px;	        
	        -moz-border-radius: 15px; 
	        
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
        	top: 70px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:120px;        
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
        
         .usatPanel2
        {        
            border: 1px solid #C0C0C0;	        
	        height:150px;	        	        
	        -moz-border-radius: 15px	        
	        /*padding-top:10px;*/	        
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
         <div class="usatTituloAzul">Detalle de la Fórmula - Periodo
            <a href="#" onclick="apprise('Muestra el detalle de la Fórmula.');">
                <img src="../Images/info.png" style="border:0" alt="Ayuda."/>
            </a>
        </div>
                
         
            <div style="clear:both;  height:5px"></div>
         
            <div runat="server" style="height:20px; float:right; padding-top:2px;  width: 100%;">
                    <asp:Button 
                            ID="btnCancelar" 
                            runat="server" 
                            Text="Regresar" 
                            Width="100px" 
                            Height="22px" 
                            CssClass="salir" 
                            onclientclick="self.parent.tb_remove();" 
                            UseSubmitBehavior="False" />              
            </div>
            <div style="clear:both; height:14px"></div>
        
        
        <div class="usatPanel">
            <asp:Label ID="lblSubtitulo" runat="server" Text="Información Básica"></asp:Label>
            <div style="margin-top:1px;">
                <div class="usatFormatoCampo">
                <asp:Label ID="Label3" runat="server" Text="Nombre del Plan"></asp:Label>
                </div>
                <div class="usatFormatoCampoAncho">
                    <asp:TextBox ID="txtNombrePlan" runat="server" Width="450px" Font-Size="XX-Small" 
                        MaxLength="10" Enabled="False"></asp:TextBox>
                    
                </div>
                <div style="clear:both; height:1px;"></div>        
            </div>
            <div style="padding:0px;">
                <div class="usatFormatoCampo">
                <asp:Label ID="Label2" runat="server" Text="Centro Costo"></asp:Label>
                </div>
                <div class="usatFormatoCampoAncho">
                    <asp:TextBox ID="txtNombreCeco" runat="server" Width="450px" Font-Size="XX-Small" 
                        MaxLength="10" Enabled="False"></asp:TextBox>
                    
                </div>
                <div style="clear:both; height:1px;"></div>        
            </div>
            <div style="padding:0px;">
                <div class="usatFormatoCampo">
                <asp:Label ID="Label4" runat="server" Text="Nombre Perspectiva"></asp:Label>
                </div>
                <div class="usatFormatoCampoAncho">
                    <asp:TextBox ID="txtNombrePerspectiva" runat="server" Width="450px" Font-Size="XX-Small" 
                        MaxLength="10" Enabled="False"></asp:TextBox>
                    
                </div>
                <div style="clear:both; height:1px;"></div>        
            </div>
            <div style="margin-top:1px;">
                <div class="usatFormatoCampo">
                <asp:Label ID="Label5" runat="server" Text="Nombre del Objetivo"></asp:Label>
                </div>
                <div class="usatFormatoCampoAncho">
                    <asp:TextBox ID="txtNombreObjetivo" runat="server" Width="450px" Font-Size="XX-Small" 
                        MaxLength="10" Enabled="False"></asp:TextBox>
                    
                </div>
                <div style="clear:both; height:1px;"></div>        
            </div>
             <div style="margin-top:1px;">
                <div class="usatFormatoCampo">
                <asp:Label ID="Label6" runat="server" Text="Nombre del Indicador"></asp:Label>
                </div>
                <div class="usatFormatoCampoAncho">
                    <asp:TextBox ID="txtNombreIndicador" runat="server" Width="450px" Font-Size="XX-Small" 
                        MaxLength="10" Enabled="False"></asp:TextBox>
                    
                </div>
                <div style="clear:both; height:1px;"></div>        
            </div>
            <div style="margin-top:1px;">
                <div class="usatFormatoCampo">
                <asp:Label ID="Label1" runat="server" Text="Fórmula"></asp:Label>
                </div>
                <div class="usatFormatoCampoAncho" style="width:230px">
                    <asp:TextBox ID="txtFormula" runat="server" Width="220px" Font-Size="XX-Small" 
                        MaxLength="10" Enabled="False"></asp:TextBox>
                    
                </div>
                <div class="usatFormatoCampoAncho" style="width:60px;height:15px;">
                    <asp:Label ID="Label7" runat="server" Text="Valor"></asp:Label>
                    
                </div>
                <div class="usatFormatoCampoAncho" style="width:100px; padding-left:12px" >
                                     
                    <asp:TextBox ID="txtValor" runat="server" Width="118px" Enabled="False" 
                        Font-Size="XX-Small"></asp:TextBox>
                                     
                </div>
                <div style="clear:both; height:1px;"></div>        
            </div>
        </div>
        
        
        <div class="usatPanelConsulta" style="padding-top:10px"> 
        <asp:Label ID="lblConsulta" runat="server" Text="Detalle de la Formula" Width="160px"></asp:Label>
            
        
        <div style="padding-top:1px">
         <table cellpadding="3" cellspacing="0" style="width: 99%; border:1px solid #96ACE7" border="0">
                <tr>                
                    <td style="text-align:center" align="center">
                        <asp:GridView ID="gvDetalleFormula" runat="server" Width="100%" CellPadding="4" 
                            ForeColor="#333333" AutoGenerateColumns="False" 
                            EmptyDataText="No se encontraron registros.">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EmptyDataRowStyle ForeColor="#FF0066" />
                            <Columns>
                                <asp:BoundField HeaderText="N°" >
                                <ItemStyle Width="15px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Periodicidad" DataField="tmp_descripcion_peri" >                                               
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tmp_descripcion_cat" HeaderText="Categoría" > 
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tmp_nombre_var" HeaderText="Variable/Indicador" >
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Etiqueta" DataField="tmp_codigo_var" >
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                                                
                                <asp:BoundField DataField="tmp_Valor" HeaderText="Valor" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
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
