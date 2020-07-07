<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmNivelAccesoVariablesPlanCeco.aspx.vb" Inherits="indicadores_frmNivelAccesoVariablesPlanCeco" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Configuración Variables por Plan</title>
     <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
     <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
     <script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
     <link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />
     
     <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    
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
               }
               else {
                   obj.style.backgroundColor = "white"
               }
           }        
    </script>
        
        <script type="text/javascript" language="javascript">
            function SelectAllChildNodes() {
                //debugger;
                var obj = window.event.srcElement;
                var treeNodeFound = false;

                var checkedState;
                if (obj.tagName == "INPUT" && obj.type == "checkbox") {
                    var treeNode = obj;
                    checkedState = treeNode.checked;
                    do {
                        obj = obj.parentElement;
                    } while (obj.tagName != "TABLE")

                    var parentTreeLevel = obj.rows[0].cells.length;
                    var parentTreeNode = obj.rows[0].cells[0];
                    var tables = obj.parentElement.getElementsByTagName("TABLE");
                    var numTables = tables.length;
                    if (numTables >= 1) {
                        for (iCount = 0; iCount < numTables; iCount++) {
                            if (tables[iCount] == obj) {
                                treeNodeFound = true;
                                iCount++;
                                if (iCount == numTables) {
                                    return;
                                }
                            }
                            if (treeNodeFound == true) {
                                var childTreeLevel = tables[iCount].rows[0].cells.length;
                                if (childTreeLevel > parentTreeLevel) {
                                    var cell = tables[iCount].rows[0].cells[childTreeLevel - 1];
                                    var inputs = cell.getElementsByTagName("INPUT");
                                    inputs[0].checked = checkedState;
                                }
                                else {
                                    return;
                                }
                            }
                        }
                    }
                }
            }
       </script>

        
         <script type="text/javascript">

             $(document).ready(function() {
                 var chkBox = $("input[id$='ChkAll']");
                 chkBox.click(
                function() {
                 $("#gvListaVar INPUT[type='checkbox']")
                    .attr('checked', chkBox
                    .is(':checked'));
                });

                 // To deselect CheckAll when a GridView CheckBox
                 // is unchecked

                $("#gvListaVar INPUT[type='checkbox']").click(
                    function(e) {
                        if (!$(this)[0].checked) {
                            chkBox.attr("checked", false);
                        }
                    });
             });
    
        </script>
    
    
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
     
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
        <table cellspacing="0" cellpadding="0" style="border-collapse: collapse;bordercolor: #111111;width:100%">
		    <tr>
            <td style="height:100%;width:100%" valign="top" colspan="12" class="pestanarevez">
                    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
                        <tr>
                            <td bgcolor="#D1DDEF" height="30px" colspan="2">
                                <b>
                            <asp:Label ID="Label4" runat="server" Text="Configuración de Centros de Costos por Plan Estratégico"></asp:Label></b>
                            </td>
                        </tr>
		                <tr>
		                    <td>
                                <asp:Label ID="Label1" runat="server" Text="Plan Estratégico: "></asp:Label>
		                    </td>
		                    <td>
		                        <asp:DropDownList ID="ddlPlan" Width="500px" runat="server" 
                                BackColor="#FFFFCC" Font-Size="Smaller" AutoPostBack="True">
                                </asp:DropDownList>
		                    </td>
		                </tr>
		                <tr>
		                    <td>
                                <asp:Label ID="Label5" runat="server" Text="Año"></asp:Label>
                            </td>
		                    <td>
                                <asp:DropDownList ID="ddlAnio" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:HiddenField ID="HiddenField1" Value=0 runat="server" />
                            </td>
		                </tr>
		                <tr>
		                    <td colspan="2">
		                        <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
		                            <tr>
                                        <td bgcolor="#FFFFCC" height="15px" >
                                            <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
                                                <tr>
                                                    <td style="width:10%">
                                                        <asp:Button ID="btnBuscarCecos" runat="server" CssClass="buscar" Text=" Buscar" 
                                                            Width="100%" />
                                                    </td>
                                                    <td style="width:90%">
                                                        <asp:TextBox ID="txtBusceco"  Width="99%" runat="server" Font-Size="8pt"></asp:TextBox>
                                                    </td>
                                                </tr>   
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label2" runat="server" Text="Lista de centro de Costos: "></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                         <asp:Panel ID="Panel3" runat="server" Height="150px" 
                                            ScrollBars="Vertical" Width="100%">
                                            <asp:GridView ID="gvCecos" runat="server" 
                                                AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="None" 
                                                BorderWidth="1px" CellPadding="3" DataKeyNames="codigo_cco" 
                                                ShowHeader="False" Width="98%" BackColor="White">
                                                <FooterStyle BackColor="White" 
                                                    ForeColor="#000066" />
                                                <RowStyle ForeColor="#000066" />
                                                <Columns>
                                                    <asp:BoundField DataField="codigo_cco" 
                                                        HeaderText="Código" />
                                                    <asp:BoundField DataField="descripcion_cco" 
                                                        HeaderText="Centro de costos" />
                                                    <asp:CommandField ShowSelectButton="True" SelectText="" />
                                                </Columns>
                                                <PagerStyle BackColor="White" ForeColor="#000066" 
                                                    HorizontalAlign="Left" />
                                                <EmptyDataTemplate>
                                                    <b>No se encontraron items con el término de búsqueda</b>
                                                </EmptyDataTemplate>
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" 
                                                    ForeColor="White" />
                                                <HeaderStyle BackColor="#006699" Font-Bold="True" 
                                                    ForeColor="White" />
                                            </asp:GridView>
                                        </asp:Panel>
                                                    </td>
                                                </tr>   
                                            </table>
                                        </td>
                                     </tr>
		                        </table>
		                    </td>
		                </tr>
		                
		    </table>    
            </td>
	    </tr>
		</table>
		
		
		
		<table cellspacing="0" cellpadding="0" style="border-collapse: collapse;bordercolor: #111111;width:100%">
                <tr>
		    <td class="pestanabloqueada" id="Td2" align="center" style="height:15px;width:15%" onclick="ResaltarPestana2('0','','');">
                    <asp:LinkButton ID="lnkConfiguracion" Text="Configuración de Variables" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue"></asp:LinkButton>
            </td>
            <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			<td class="pestanaresaltada" id="tab" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('1','','');">
                    <asp:LinkButton ID="lnkConsulta" Text="Consulta de Variables Configuradas" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue"></asp:LinkButton>
            </td>
			<td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			<td class="pestanaresaltada" id="Td1" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('1','','');">
                    <asp:LinkButton ID="lnkPersnalCeco" Text="Consulta Personal Centro Costo" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue"></asp:LinkButton>
            </td>
		</tr>
           <asp:Panel ID="pnlConf" runat="server">
                <tr>
                    <td colspan="5">
                        <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
                            <tr>
                                <td bgcolor="#D1DDEF" height="15px" colspan="3">
                                    <br />
                                    <b>
                                        <asp:Label ID="lblInformacion" Width="100%" runat="server" Text=""></asp:Label></b>
                                </td>
                            </tr>
                                <tr>
                                     <td colspan="3">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rbtVistaGeneral" Text="Vista Lista" runat="server" 
                                                        Checked="True" GroupName="vista" AutoPostBack="True" />
                                                </td>
                                                <td>
                                                
                                                    <asp:RadioButton ID="rbtVistaArbol" Text="Vista en Árbol" runat="server" 
                                                        GroupName="vista" AutoPostBack="True" />
                                                
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnAsignar" runat="server" Text="Guardar Configuración" CssClass="boton" />            
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>            
                        </table>
                    </td>
		        </tr>
		        <tr>
		            <td style="height:100%;width:100%" valign="top" colspan="5">
		                <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
		                    <asp:Panel ID="pnlOpcionesTreeview" runat="server">
		                        <tr>
		                            <td style="width:10%">
                                        <asp:CheckBox ID="chkTreeview" Text="Expandir" runat="server" AutoPostBack="True" />            
                                    </td>
                                </tr>
		                    </asp:Panel>
		                   <asp:Panel ID="pnlOpcionesGirdview" runat="server">
		                        <tr>
		                            <td>
                                        <asp:Label ID="Label7" runat="server" Text="Opciones: "></asp:Label>
		                            </td>
		                            <td>
		                                <table>
		                                    <tr>
		                                        <td>
                                                    <asp:RadioButton ID="rbtTodos" runat="server" Text="Todos" 
                                                        GroupName="FiltroEstados" Checked="True" AutoPostBack="True" />
		                                        </td>
		                                        <td>
                                                    <asp:RadioButton ID="rbtAsignados" runat="server" Text="Asiganados" 
                                                        GroupName="FiltroEstados" AutoPostBack="True" />
		                                        </td>
		                                        <td>
                                                    <asp:RadioButton ID="rbtNoAsignados" runat="server" text="Por Asignar" 
                                                        GroupName="FiltroEstados" AutoPostBack="True" />
		                                        </td>
		                                    </tr>
		                                </table>
		                            </td>
		                        </tr>
		                        <tr>
		                                <td style="width:10%">
                                                <asp:Button ID="btnBuscarVariable" runat="server" CssClass="buscar" Text=" Buscar" 
                                                            Width="100%" />
		                                </td> 
		                                <td style="width:60%">
                                                <asp:TextBox ID="txtBusqueda"  Width="97%" runat="server"></asp:TextBox>
		                                </td>           
                                     
		                        </tr>
		                    </asp:Panel>
		                </table>
		            </td>
		        </tr>
		        <tr>
		        <asp:Panel ID="pnlListaVariablesTreeview" runat="server">
                    <td style="height:100%;width:100%" valign="top" colspan="5" 
                        class="pestanarevez">
                        
                                <asp:TreeView 
			                        ID="treePrueba" 
			                        ShowCheckBoxes="All" 
			                        runat="server" 
			                        MaxDataBindDepth="4" 
			                        onclick="SelectAllChildNodes();"
			                        ExpandDepth="1" 
			                        ShowLines="true"
			                        Font-Size="XX-Small" ForeColor="#3366FF">
                                    <Nodes>
                                        <asp:TreeNode PopulateOnDemand="True" Text="LISTADO DE VARIABLES DE PLAN" Value="Lista" 
                                            SelectAction="Expand">
                                        </asp:TreeNode>
                                    </Nodes>
                                    <HoverNodeStyle CssClass="menuporelegir" />
                                </asp:TreeView>  
                    </td>
                </asp:Panel>
    	        </tr>
    	        <tr>
    	            <td style="height:100%;width:100%" valign="top" colspan="5">
    	                <asp:Panel ID="pnlListaVariablesGridview" runat="server" Height="400px" 
                                            ScrollBars="Vertical" Width="100%">
                                            <asp:GridView ID="gvListaVar" runat="server" BorderColor="#CCCCCC" BorderStyle="None" 
                                                BorderWidth="1px" CellPadding="3" Width="98%" BackColor="White" 
                                                AutoGenerateColumns="False" DataKeyNames="ID_var">
                                                <Columns>
                                                    <asp:TemplateField>            
                                                    <HeaderTemplate>                
                                                        <asp:CheckBox ID="ChkAll" runat="server" />            
                                                    </HeaderTemplate>            
                                                    <ItemTemplate>                
                                                        <asp:CheckBox ID="chkSel" class="chkSel" runat="server" />           
                                                        </ItemTemplate>        
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ID_var" HeaderText="ID" />
                                                    <asp:BoundField DataField="nombre_var" 
                                                        HeaderText="Descripción de la Variable" />
                                                    <asp:BoundField DataField="EstadoVar" HeaderText="Estado" />
                                                </Columns>
                                                <FooterStyle BackColor="White" 
                                                    ForeColor="#000066" />
                                                <RowStyle ForeColor="#000066" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" 
                                                    HorizontalAlign="Left" />
                                                <EmptyDataTemplate>
                                                    <b>No se encontraron items con el término de búsqueda</b>
                                                </EmptyDataTemplate>
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" 
                                                    ForeColor="White" />
                                                <HeaderStyle BackColor="#006699" Font-Bold="True" 
                                                    ForeColor="White" HorizontalAlign="Left" />
                                            </asp:GridView>
                        </asp:Panel>
    	            </td>
    	        </tr>
            </asp:Panel>
            <asp:Panel ID="pnlConsulta" runat="server">
                <tr>
		            <td colspan="5">
		            <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
                            <tr>
                                <td bgcolor="#D1DDEF" height="15px" colspan="3">
                                    <br />
                                    <b>
                                    <asp:Label ID="Label3" Width="100%" runat="server" Text=""></asp:Label></b>
                                </td>
                            </tr>
                                <tr>
                                     <td colspan="3">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Variables" CssClass="boton" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkTreeview2" Text="Expandir" runat="server" 
                                                        AutoPostBack="True" />           
                                                </td>
                                            </tr>
                                        </table>
                                        
		                            </td>
		                        </tr>
                                <tr>
                                    <td style="height:100%;width:100%" valign="top" colspan="12" class="pestanarevez">
                     
                                            <asp:TreeView 
			                                ID="treePrueba2" 
			                                ShowCheckBoxes="All" 
			                                runat="server" 
			                                onclick="SelectAllChildNodes();"
			                                MaxDataBindDepth="4" 
			                                ExpandDepth="1" 
			                                ShowLines="true"
			                                Font-Size="XX-Small">
                                            <Nodes>
                                                <asp:TreeNode 
                                                    PopulateOnDemand="True" 
                                                    Text="LISTA DE VARIABLES CONFIGURADAS" Value="Lista" 
                                                    SelectAction="Expand">
                                                </asp:TreeNode>
                                            </Nodes>
                                            
                                            </asp:TreeView>      
                                </td>
                    </tr>            
                        </table>
                    </td>
		        </tr>
		    </asp:Panel>
            <asp:Panel ID="pnlListaPersonal" runat="server">
                <tr>
                    <td colspan="5">
                        <asp:GridView   
                                    ID="gvPersonal" 
                                    runat="server" 
                                    AutoGenerateColumns="False" 
                                    BorderColor="Silver" 
                                    BorderStyle="Solid" 
                                    CaptionAlign="Top" 
                                    CellPadding="2" 
                                    EnableModelValidation="True" 
                                    Width="100%">
                                    <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" ForeColor="Red" />
                        <Columns>
                            <asp:TemplateField Visible="False">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkElegir" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="#">
                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_Per" HeaderText="ID">
                            <ItemStyle Font-Size="7pt" Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Usuarios" HeaderText="Apellidos y Nombres" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                            BorderWidth="1px" ForeColor="#3366CC" />
                    </asp:GridView>    
                </td>
                </tr>
		    </asp:Panel>
		</table>
    </div>
    </form>
</body>
</html>
