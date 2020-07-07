<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaCecosConfiguradosVariables.aspx.vb" Inherits="indicadores_frmListaCecosConfiguradosVariables" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
        <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
        <script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
        <link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />	
        
         
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
                  if (chk.type == "checkbox") {
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
        	padding-top:20px;        	
        	padding-left:40px;
        	color:#27333c;
        	font-family: Arial;
        	height:7px;
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
        	height:7px;
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
	        height: 68px;	        
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
        	top: 125px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:158px;        
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
    <div>
        <div class="usatTituloAzul">Centros de Costo Configurados par la Variable
            <a href="#" onclick="apprise('Lista de Centro de Costo.');">
                <img src="../Images/info.png" style="border:0" alt="Ayuda."/>
            </a>
        </div>
         <div style="clear:both;  height:5px"></div>
    
         <div id="avisos" runat="server" style="height:25px; padding-top:2px; width: 97.5%;">
            <asp:Image ID="Image1" runat="server" ImageUrl="../Images/beforelastchild.GIF"/>
            <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>            
         </div>
         <br />
         <div style="clear:both; height:3px"></div>
         
         <div id="Div1" runat="server" style="height:20px; float:right; padding-top:2px;  width: 100%;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button 
                            ID="btnCancelar" 
                            runat="server" 
                            Text="Regresar" 
                            Width="121px" 
                            Height="22px" 
                            CssClass="salir" 
                            onclientclick="self.parent.tb_remove(); window.location.reload" 
                            UseSubmitBehavior="False" ForeColor="Blue" />              
         </div>
               
         
         <div style="clear:both; height:14px"></div>
         
         <div class="usatPanel">
            <asp:Label ID="lblSubtitulo" runat="server" Text="Información de Variable "></asp:Label>
            <div style="margin-top:1px;">
                <div class="usatFormatoCampo">
                <asp:Label ID="Label3" runat="server" Text="Codigo"></asp:Label>
                </div>
                <div class="usatFormatoCampoAncho">
                    <asp:TextBox ID="txtCodigo" runat="server" Width="64px" Font-Size="XX-Small" 
                        MaxLength="10" Enabled="False" ForeColor="Red"></asp:TextBox>
                    
                </div>
                <div style="clear:both; height:1px;"></div>        
            </div>
            <div style="padding:0px;">
                <div class="usatFormatoCampo">
                <asp:Label ID="Label2" runat="server" Text="Nombre Variable"></asp:Label>
                </div>
                <div class="usatFormatoCampoAncho">
                    <asp:TextBox ID="txtNombre" runat="server" Width="450px" Font-Size="XX-Small" 
                        MaxLength="10" Enabled="False" ForeColor="Blue"></asp:TextBox>
                    
                </div>
                <div style="clear:both; height:1px;"></div>        
            </div>
            
        </div>
        
        <div style="margin-top:1px;">
                <div class="usatFormatoCampo">
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminación" CssClass="eliminar2" 
                        Width="121px" />
                </div>
                <div class="usatFormatoCampoAncho" style="width:230px">
                    
                </div>
                <div class="usatFormatoCampoAncho" style="width:60px;height:15px;">
                    
                </div>
                <div class="usatFormatoCampoAncho" style="width:100px; padding-left:12px" >
                                     
                </div>
                <div style="clear:both; height:1px;"></div>        
            </div>
               
             
        <br />
        <br />
        <div class="usatPanelConsulta" style="padding-top:10px"> 
        <asp:Label ID="lblConsulta" runat="server" Text="Lista Centros de Costo" Width="160px"></asp:Label>
     
        <div style="padding-top:1px">
         <table cellpadding="3" cellspacing="0" style="width: 99%; border:1px solid #96ACE7" border="0">
                <tr>                
                    <td style="text-align:center" align="center">
                        <asp:GridView ID="gvListaCecos" runat="server" Width="100%" CellPadding="3" 
                            EmptyDataText="No se encontraron registros." AutoGenerateColumns="False" 
                            DataKeyNames="codigo" BackColor="White" BorderColor="#CCCCCC" 
                            BorderStyle="None" BorderWidth="1px">
                            <RowStyle ForeColor="#000066" />
                            <EmptyDataRowStyle ForeColor="#FF0066" />
                            <Columns>
                                <asp:BoundField HeaderText="Nº" >
                                
                                  <ItemStyle HorizontalAlign="Center" Width="10px" />
                                </asp:BoundField>
                                
                                  <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server"  onclick="MarcarCursos(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>                
                                        <asp:CheckBox ID="chkElegir" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5px" />
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="codigo" HeaderText="codigo_acc" Visible="False" />
                                <asp:BoundField DataField="codigo_Cco" HeaderText="Código" >
                                <ItemStyle Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion_Cco" 
                                    HeaderText="Descripción Centro Costo" >
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:CommandField ShowDeleteButton="True" ButtonType="Image" 
                                    DeleteImageUrl="../images/eliminar.gif" >
                                <ItemStyle Width="30px" />
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
    </div>
    
      
            
                
    </div>
    </form>
</body>
</html>
