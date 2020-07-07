<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBecaDoble.aspx.vb" Inherits="BecaEstudio_frmOtorgarBeca" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/estilos.css" rel="stylesheet" type="text/css" />
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
  
 
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div id="content">
    <table>      
        <tr>
            <td colspan="2" bgcolor="#EFF3FB" height="35px">
                <asp:Label ID="Label1" runat="server" 
                    Text="Consultar Solicitud de Becas con Doble Beneficio" style="font-weight: 700"></asp:Label>
                </td>                            
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>                            
        </tr>
        <tr>
            <td>Seleccionar Ciclo</td>                
            <td><asp:DropDownList ID="ddlCiclo" runat="server"></asp:DropDownList>
                
                            
            </td>
             <td >
                
               <asp:Button ID="btnBuscar" runat="server" Text="BUSCAR" CssClass="btnBuscar" 
                    Width="" Height="" />   </td>
            
        </tr>
        <tr>
            <td><br />
                <asp:Label ID="lblnumero" runat="server" Font-Bold="True" ForeColor="#3366CC"></asp:Label>
                <br /></td>
            <td align="right">
                &nbsp;</td>
            <td>
            <asp:Button ID="cmdExportar" runat="server"
                    Text="Exportar" 
                    onclientclick="document.all.tblmensaje.style.display='none'" 
                    CssClass="btnExportar" Visible="False" />
            </td>
        </tr>
        <tr>
        <td colspan="3">
                                
             <asp:GridView ID="gvBecas" Width="100%" runat="server"
                 AutoGenerateColumns="False" EmptyDataText="No se encontraron datos" 
                 CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="codigo_bso" HeaderStyle-Font-Size="X-Small" Font-Size="X-Small">
                 <Columns>                  
                     <asp:BoundField HeaderText="#">
                     <ItemStyle Width="2%" />
                   </asp:BoundField>
                     <asp:BoundField DataField="codigo_bso" HeaderText="CODIGO" Visible="False" />
                     <asp:BoundField DataField="nombre_Cpf" HeaderText="ESCUELA PROFESIONAL" ItemStyle-Width="100px" />
                     <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIV." />
                     <asp:BoundField DataField="Alumno" HeaderText="ALUMNO" />
                     <asp:BoundField DataField="descripcion_bec" HeaderText="BECA" />
                     <asp:BoundField DataField="estado_bso" HeaderText="ESTADO" />
                     <asp:BoundField DataField="ponderado_bso" HeaderText="PONDERADO" />
                      <asp:CommandField EditText="Eliminar" HeaderText="Rechazar" 
                                ShowDeleteButton="True" ButtonType="Image" 
                                DeleteImageUrl="../../images/eliminar.gif" DeleteText="" >
                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                            </asp:CommandField>                    
                 </Columns>
                 <EmptyDataTemplate>
                     <asp:Label ID="labelMensajeGrid" runat="server" Text="No se ha encontrado ninguna coincidencia con el criterio de búsqueda." />
                  </EmptyDataTemplate>
                 <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                 <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                 <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                 <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                 <RowStyle Height="22px" BackColor="#F7F6F3" ForeColor="#333333" />
                 <EditRowStyle BackColor="#999999" />
                 <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
             </asp:GridView>
             
             <br />
      
      </td>
      </tr>  
      <tr>
        <td colspan="2">
            &nbsp;</td>
      </tr>                 
    </table> 
    
   </div>   
      </form>
</body>
</html>
