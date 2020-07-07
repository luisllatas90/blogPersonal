<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmNivelAccesoVariablesCeco2.aspx.vb" Inherits="indicadores_frmNivelAccesoVariablesCeco2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
     <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
     <script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
     <link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />
     
     
     <!--<link href="../Style/estilo.css" rel="stylesheet" type="text/css" />-->
     <!--
        Este evento es para el treeview para que se puedan seleccionar todos los hijos.
        onclick="SelectAllChildNodes();" 
     -->
     
     
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
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                            <asp:Label ID="Label4" runat="server" Text="Seleccione el Centro Costo"></asp:Label></b>
                                <asp:HiddenField ID="hdfCodigo_Ceco" runat="server" Value="0" />
                            </td>
                        </tr>
		                <tr>
		                    <td>
                                <asp:Label ID="Label1" runat="server" Text="Unidad del Negocio"></asp:Label>
		                    </td>
		                    <td>
		                        <asp:DropDownList ID="ddlUnidadNegocio" Width="500px" runat="server" 
                                BackColor="#FFFFCC" Font-Size="Smaller" AutoPostBack="True">
                                </asp:DropDownList>
		                    </td>
		                </tr>
		                <tr>
		                    <td>
                                <asp:Label ID="Label5" runat="server" Text="SubUnidad del Negocio"></asp:Label>
                            </td>
		                    <td>
                                <asp:DropDownList ID="ddlSubunidadNegocio" runat="server" 
                                    BackColor="#FFFFCC" Font-Size="Smaller" Width="500px" AutoPostBack="True">
                                </asp:DropDownList>
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
                                                        <asp:GridView   ID="gvListaCecos" 
                                    runat="server" 
                                    AutoGenerateColumns="False" 
                                    BorderColor="Silver" 
                                    BorderStyle="Solid" 
                                    CaptionAlign="Top" 
                                    CellPadding="2" 
                                    EnableModelValidation="True" 
                                    Width="100%" 
                                    
                                    DataKeyNames="codigo" AllowPaging="True" Font-Size="XX-Small" PageSize="5">
                        <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                        <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" ForeColor="Red" />
                        <Columns>
                            <asp:BoundField HeaderText="#" Visible="False">
                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Codigo" HeaderText="ID">
                            <ItemStyle Font-Size="7pt" Width="15px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Descripcion" 
                                HeaderText="NOMBRE DEL CENTRO DE COSTO A CONFIGURAR" >
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:CommandField ShowSelectButton="True" SelectText="" />
                        </Columns>
                                                            <SelectedRowStyle BackColor="#D1DDF1" />
                        <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                            BorderWidth="1px" ForeColor="#3366CC" />
                    </asp:GridView>
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
		            <td colspan="3">
                        <asp:Button ID="btnAsignar" runat="server" Text="Guardar Configuración" 
                            CssClass="boton" />
		            </td>
		        </tr>
		        <tr>
                    <td style="height:100%;width:100%" valign="top" colspan="12" class="pestanarevez">
			            <asp:TreeView 
			                    ID="treePrueba" 
			                    ShowCheckBoxes="All" 
			                    runat="server" 
			                    MaxDataBindDepth="4" 
			                    ExpandDepth="1" 
			                    ShowLines="true"
			                    Font-Size="XX-Small">
                                <Nodes>
                                    <asp:TreeNode PopulateOnDemand="True" Text="LISTA DE VARIABLES" Value="Lista" 
                                        SelectAction="Expand">
                                    </asp:TreeNode>
                                </Nodes>
                               
                       </asp:TreeView>  
		            </td>
                
    	    </tr>
            </asp:Panel>
          
            <asp:Panel ID="pnlConsulta" runat="server">
                <tr>
		            <td colspan="3">
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Variables" 
                            CssClass="boton" />
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
                            <asp:BoundField DataField="Usuarios" HeaderText="Apellidos y Nombres" />
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
