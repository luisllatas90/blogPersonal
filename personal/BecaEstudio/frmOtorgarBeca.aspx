<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmOtorgarBeca.aspx.vb" Inherits="BecaEstudio_frmOtorgarBeca" %>
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
  
 
        .style1
        {
            height: 24px;
        }
  
 
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div id="content">
    <table>      
        <tr>
            <td colspan="2" bgcolor="#EFF3FB" height="35px">
                <asp:Label ID="Label1" runat="server" 
                    Text="Solicitudes Registradas de Becas de Estudio por Rendimiento" style="font-weight: 700"></asp:Label>
                </td>                            
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>                            
        </tr>
        <tr>
            <td>Semestre Académico:</td>                
            <td><span class="combobox large"><asp:DropDownList ID="ddlCiclo" runat="server"></asp:DropDownList></span></td>
        </tr>
        <tr>
            <td class="style1">Carrera Profesional:</td>
            <td class="style1"><span class="combobox large"><asp:DropDownList ID="ddlEscuela" runat="server"></asp:DropDownList></span></td>
        </tr>
        <!--<tr>
            <td>Tipo de Beca Estudio</td>
            <td><span class="combobox large">-->
            <asp:DropDownList ID="ddlTipoBeca" runat="server" 
                    Visible="False"></asp:DropDownList>
        <!-- </span></td></tr>-->
        <tr>
            <td >
                <span class="combobox large">
                <asp:Button ID="btnBuscar" runat="server" Text="BUSCAR" CssClass="btnBuscar" 
                    Width="" Height="" />
                </span>
            </td>
            <td>
            <asp:Button ID="cmdExportar" runat="server"
                    Text="Exportar" 
                    onclientclick="document.all.tblmensaje.style.display='none'" 
                    CssClass="btnExportar" />
            </td>
        </tr>
        <tr>
            <td><br />
                <asp:Label ID="lblnumero" runat="server" Font-Bold="True" ForeColor="#3366CC"></asp:Label>
                <br /></td>
            <td align="right">
                <asp:Button ID="btnProcesar" runat="server" Text="PROCESAR BECAS" 
                    CssClass="btnProcesar" Visible="False"  />
            </td>
        </tr>
        <tr>
        <td colspan="2">
         <div>                         
             <asp:GridView ID="gvBecas" Width="100%" runat="server" DataKeyNames="codigo_bso"
                 AutoGenerateColumns="False" EmptyDataText="No se encontraron datos">
                 <Columns>      
                  
                                   
                      <asp:TemplateField HeaderText="" Visible="False" >
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
                        </HeaderTemplate>
                        <ItemTemplate>                
                            <asp:CheckBox ID="chkElegir" runat="server" />
                        </ItemTemplate>                        
                     </asp:TemplateField>                 
                     <asp:BoundField HeaderText="#">
                            <ItemStyle Width="2%" />
                   </asp:BoundField>
                     <asp:BoundField DataField="codigo_bso" HeaderText="CODIGO" Visible="False" />
                     <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIV." />
                     <asp:BoundField DataField="Estudiante" HeaderText="ESTUDIANTE" />
                     <asp:BoundField DataField="nombre_Cpf" HeaderText="CARRERA PROFESIONAL" />
                     <asp:BoundField DataField="descripcion_bec" HeaderText="BENEFICIO" 
                          Visible="False" />
                     <asp:BoundField DataField="ponderado_bso" HeaderText="PONDERADO" />
                     <asp:BoundField DataField="orden_bso" HeaderText="ORDEN" Visible="False" />                      
                 </Columns>
                 <EmptyDataTemplate>
                     <asp:Label ID="labelMensajeGrid" runat="server" Text="No se ha encontrado ninguna coincidencia con el criterio de búsqueda. " />
                  </EmptyDataTemplate>
                 <FooterStyle BackColor="White" ForeColor="#000066" />
                 <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                 <SelectedRowStyle BackColor="#FFFFB1" Font-Bold="True" ForeColor="Blue" />
                 <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                 <RowStyle Height="22px" />
             </asp:GridView>
             
             <br />
      </div>
      </td>
      </tr>  
      <tr>
        <td colspan="2">
            <asp:Button ID="btnConfirmar" runat="server" Text="PUBLICAR" 
                CssClass="btnPublicar" Visible="False"/>   
            </td>
      </tr>                 
    </table> 
    
   </div>   
      </form>
</body>
</html>
