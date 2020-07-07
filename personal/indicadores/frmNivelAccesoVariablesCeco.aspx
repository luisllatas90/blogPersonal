<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmNivelAccesoVariablesCeco.aspx.vb" Inherits="indicadores_frmNivelAccesoVariablesCeco" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
     <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
    <script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
    <link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />	
    
    <script src="../javascript/jq/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../javascript/jq/lbox/thickbox.js" type="text/javascript"></script>
    <link  href="../javascript/jq/lbox/thickbox.css" rel="stylesheet" type="text/css" media="screen" />
    

    <script src="../javascript/ScrollableGrid.js" type="text/javascript"></script>
     <script type="text/javascript" language="javascript">
         $(document).ready(function() {
         $('#<%=gvListaCecos.ClientID %>').Scrollable();
         }
        )
    </script>
    
     <script type="text/javascript" language="javascript">
         $(document).ready(function() {
         $('#<%=gvJerarquiaVariables.ClientID %>').Scrollable();
         }
        )
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
    
     <style type="text/css">
         
         .DataGridFixedHeader
         {
            position: relative;
            top: expression(this.offsetParent.scrollTop-3); /*this works fine with IE only, but FireFox seems to be ignoring this*/
         }
         
        .usatFormatoCampo
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:11px;
        	width:200px;        	
        }
        
        .usatFormatoCampoCeco
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:400px;
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
        	height:11px;
        	width:183px;
        	/*border:1px solid red;*/
        }
        
        .usatFormatoCampoAnchoCeco
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:150px;
        	width:746px;
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
	        /*height:490px;	        */
	        height:185px;
	        max-height:600PX;
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
             width: 184px;
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
        
        .mensajeAviso
        {
        	border: 1px solid #FACC2E;
        	background-color: #F2F5A9;        	
        	padding-top:2px;        	
        	-moz-border-radius: 15px;
        	padding-left:25px        	
        }
        
        /********************************/   
                        
         #form1
         {
             height: 581px;
         }
                        
    </style>
       
    
</head>
<body>
    <form id="form1" runat="server">
         <div class="usatTituloAzul">Configurar el Nivel de Acceso Variables
            <a href="#" onclick="apprise('Configuración de variables.');">
                <img src="../Images/info.png" style="border:0" alt="Ayuda."/>
            </a>
          </div>
        
         <!-- Para avisos -->
        <div style="clear:both; height:5px"></div>
        
         <div id="avisos" runat="server" style="height:25px; padding-top:2px; width: 97.5%;">
            <asp:Image ID="Image1" runat="server" ImageUrl="../Images/beforelastchild.GIF"/>
            <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>            
             <asp:Label ID="lblCodigoCeco" runat="server" Font-Bold="True" ForeColor="Blue" 
                 Visible="False"></asp:Label>
         </div>
         <br />
         <div style="clear:both; height:10px"></div>
         
         <div class="usatPanel">
        <asp:Label ID="lblSubtitulo" runat="server" Text="Filtro Centro Costo / Variable"></asp:Label>
         
         <div>
                <div class="usatFormatoCampo">
                    <asp:Label ID="Label3" runat="server" Text="Seleccione Unidad del Negocio"></asp:Label>
                </div>
            <div class="usatFormatoCampoAncho">
               
                <asp:DropDownList ID="ddlUnidadNegocio" Width="500px" runat="server" 
                    BackColor="#FFFFCC" Font-Size="Smaller" AutoPostBack="True">
                </asp:DropDownList>
                
                <asp:CompareValidator 
                    ID="CompareValidator1" 
                    runat="server" 
                    ErrorMessage="Seleccione la Dirección de Escala de Metas."
                    ControlToValidate="ddlUnidadNegocio" 
                    Display="Dynamic"
                    Text="<strong>*</strong>" 
                    SetFocusOnError="true" 
                    Type="String" 
                    Operator="NotEqual"
                    ValueToCompare="-" 
                    ValidationGroup="grupo1"></asp:CompareValidator>
               
            </div>                                   
            <div style="clear:both; height:2px"></div>        
        </div>   
        
        
        
        
        
        
        <div>
                <div class="usatFormatoCampo">
                    <asp:Label ID="Label8" runat="server" Text="Seleccione SubUnidad del Negocio"></asp:Label>
                </div>
            <div class="usatFormatoCampoAncho">
               
                <asp:DropDownList ID="ddlSubunidadNegocio" Width="500px" runat="server" 
                    BackColor="#FFFFCC" Font-Size="Smaller" AutoPostBack="True">
                </asp:DropDownList>
                
                <asp:CompareValidator 
                    ID="CompareValidator2" 
                    runat="server" 
                    ErrorMessage="Seleccione la Dirección de Escala de Metas."
                    ControlToValidate="ddlSubunidadNegocio" 
                    Display="Dynamic"
                    Text="<strong>*</strong>" 
                    SetFocusOnError="true" 
                    Type="String" 
                    Operator="NotEqual"
                    ValueToCompare="-" 
                    ValidationGroup="grupo1"></asp:CompareValidator>
               
            </div>                                   
            <div style="clear:both; height:2px"></div>        
        </div>
        
        <div>
                <div class="usatFormatoCampo">
                    <asp:Label ID="Label9" runat="server" Text="Buscar Centro Costo"></asp:Label>
                </div>
            <div class="usatFormatoCampoAncho">
               
                <asp:TextBox ID="txtBusceco"  Width="490px" runat="server"></asp:TextBox>
               
            </div>                                   
            <div style="clear:both; height:2px"></div>        
        </div>
        <div style="clear:both; height:10px"></div>        
        <div>
            <div class="usatFormatoCampo">
            </div>
            <div> 
                <div style="float:left; width:80px; padding-top:5px; padding-left:30px">
                    <asp:Button ID="btnBuscarCecos" runat="server" Text=" Buscar"  
                        CssClass="buscar"  Width="100%" />
                </div>                    
               <div style="float:left; width:130px;"></div>  
               <div style="float:left; width:80px; padding-top:5px">
                   </div>
            </div>                                   
            <div style="clear:both; height:2px"></div>        
        </div>
           
           <div>
                <div class="usatFormatoCampo">
                    <asp:Label ID="Label16" runat="server" Text="Seleccione Categoria"></asp:Label>
                </div>
            <div class="usatFormatoCampoAncho">
               
                <asp:DropDownList ID="ddlCategoria" Width="500px" runat="server" 
                    BackColor="#CCFFFF" Font-Size="Smaller" AutoPostBack="True">
                </asp:DropDownList>
                
                <asp:CompareValidator 
                    ID="CompareValidator6" 
                    runat="server" 
                    ErrorMessage="Seleccione la Dirección de Escala de Metas."
                    ControlToValidate="ddlCategoria" 
                    Display="Dynamic"
                    Text="<strong>*</strong>" 
                    SetFocusOnError="true" 
                    Type="String" 
                    Operator="NotEqual"
                    ValueToCompare="-" 
                    ValidationGroup="grupo1"></asp:CompareValidator>
               
            </div>                                   
            <div style="clear:both; height:2px"></div>        
        </div>
        
        <div>
                <div class="usatFormatoCampo">
                    <asp:Label ID="Label1" runat="server" Text="Seleccione una variable"></asp:Label>
                </div>
            <div class="usatFormatoCampoAncho">
               
                <asp:DropDownList ID="ddlVariable" Width="500px" runat="server" 
                    BackColor="#CCFFFF" Font-Size="Smaller" AutoPostBack="True">
                </asp:DropDownList>
                
                <asp:CompareValidator 
                    ID="CompareValidator3" 
                    runat="server" 
                    ErrorMessage="Seleccione la Dirección de Escala de Metas."
                    ControlToValidate="ddlVariable" 
                    Display="Dynamic"
                    Text="<strong>*</strong>" 
                    SetFocusOnError="true" 
                    Type="String" 
                    Operator="NotEqual"
                    ValueToCompare="-" 
                    ValidationGroup="grupo1"></asp:CompareValidator>
               
            </div>                                   
            <div style="clear:both; height:2px"></div>        
        </div>
        
        <div style="clear:both; height:20px"></div>  
        
        <div>
            <div style="clear:both; height:2px"></div>        
        </div>
        
        <div>
                <div class="usatFormatoCampo">
                    <asp:Button ID="btnAsignar" runat="server" Text="Asignar Centro Costo" 
                        CssClass="agregar2" ForeColor="Blue" />
                </div>
            <div class="usatFormatoCampoAncho">
                  <asp:Button ID="btnEliminarCecos" runat="server" Text="Eliminar Centro Costo" 
                      CssClass="agregar2" ForeColor="Red" />
            </div> 
            <div class="usatFormatoCampoAncho">
                  <asp:Button ID="btnTodo" runat="server" Text="  Todas las Variables" 
                      CssClass="agregar2" Width="150px" ForeColor="Blue" />
            </div>
            
            <div style="clear:both; height:2px"></div>        
        </div>
        
    </div>
    <br />
    <br />
    <div>
        <div style="clear:both; height:20px"></div> 
            <div class="usatFormatoCampoAnchoCeco">
                    <asp:GridView ID="gvListaCecos"  runat="server" Width="90%" BackColor="White" 
                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                    AutoGenerateColumns="False" Font-Bold="False" 
                    
                    Font-Size="Smaller" DataKeyNames="Codigo" AllowPaging="True" PageSize="5">
                    <RowStyle ForeColor="#000066" />
                    
                    <Columns>
                       
                        <asp:BoundField DataField="Codigo" HeaderText="Codigo" Visible="False" />
                        <asp:BoundField HeaderText="Nº" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                        <asp:CommandField ShowSelectButton="True" ButtonType="Image" 
                            SelectImageUrl="~/Images/checkCeco.png" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:CommandField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#284775" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
               </div>                        
                          
            <div style="clear:both; height:40px"></div>  
            
                <div class="usatFormatoCampoAnchoCeco">
                    <asp:GridView ID="gvJerarquiaVariables"  runat="server" Width="100%" BackColor="White" 
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" 
                        Font-Size="XX-Small" AutoGenerateColumns="False" 
                        DataKeyNames="Codigo,Nivel">
                    <RowStyle ForeColor="#000066" />
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
                              <asp:CommandField ButtonType="Image" 
                                  SelectImageUrl="~/Images/lista_cecos_variable.png" 
                                  SelectText="Ver Centro de Costos" ShowSelectButton="True">
                              <ItemStyle HorizontalAlign="Center" />
                              </asp:CommandField>
                            <asp:BoundField DataField="Codigo" HeaderText="Cod" >
                              <ItemStyle Width="10px" />
                              </asp:BoundField>
                            <asp:BoundField DataField="Nivel" HeaderText="Niv" >
                                <ItemStyle Width="5px" />
                              </asp:BoundField>
                            <asp:BoundField DataField="Variable" HeaderText="Variable" />
                            <asp:BoundField DataField="Subvariable" HeaderText="Subvariable" />
                            <asp:BoundField DataField="Dimension" HeaderText="Dimension" />
                            <asp:BoundField DataField="Subdimension" HeaderText="Subdimension" />
                            <asp:ButtonField ButtonType="Image" CommandName="Addceco" 
                                ImageUrl="~/Images/add_ceco.png" Text="Agregar Centro Costo" 
                                  Visible="False" >
                              <ItemStyle HorizontalAlign="Center" />
                              </asp:ButtonField>
                            <asp:BoundField HeaderText="Centro Costo" DataField="CentroCosto" 
                                  Visible="False" />
                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Images/delete_ceco.png" 
                                Text="Eliminar Centro Costo" CommandName="EliminaCeco" Visible="False" >
                              <ItemStyle HorizontalAlign="Center" />
                              </asp:ButtonField>
                        </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#284775" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </div>
        </div>
        
    </form>
</body>
</html>
