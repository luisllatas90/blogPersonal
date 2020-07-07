<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaPreInscritos.aspx.vb" Inherits="administrativo_pec2_frmListaPreInscritos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" language="javascript">
        function MarcarCursos(obj){
           //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input');
            for (var i = 0 ; i < arrChk.length ; i++){
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox"){
                    chk.checked = obj.checked;
                    if (chk.id!=obj.id){
                        PintarFilaMarcada(chk.parentNode.parentNode,obj.checked)
                    }
                }
            }
        }
               
        function PintarFilaMarcada(obj,estado){
            if (estado==true){
                obj.style.backgroundColor="#FFE7B3"
            } else{
                obj.style.backgroundColor="white"
            }
        }        
    </script>
</head>
<body>
    <form id="form1" runat="server">
   
        <table>
            <tr>
                <td>Estado Pre-Inscrito:
                </td>
                <td><asp:DropDownList ID="ddlEstado" runat="server">
                    </asp:DropDownList>
                </td>
                <td><asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="120px" Height="22px" CssClass="usatBuscar" /></td>
            </tr>
        </table>     
        <br />
        <asp:GridView ID="gvPreInscritos" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="codigo_pins,modo_pins,codigo_pso" Width="100%">
        <Columns>            
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkElegir" runat="server"  />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="5%" />
            </asp:TemplateField>
            <asp:BoundField DataField="codigo_pins" HeaderText="Codigo" Visible="false" />
            <asp:BoundField DataField="modo_pins" HeaderText="Modo" Visible="false" />
            <asp:BoundField DataField="NombreCompleto" HeaderText="Participante" />
            <asp:BoundField DataField="fechareg_pins" HeaderText="Fecha Ins." />
            <asp:BoundField DataField="estado_pins" HeaderText="Estado" />            
            <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="True" />
            <asp:BoundField DataField="codigo_pso" HeaderText="codigo_pso" 
                Visible="False" />
        </Columns>
        <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
        <RowStyle Height="22px" />
    </asp:GridView>
    <br />
        <asp:Button ID="btnInscribir" runat="server" Text="Inscribir" Width="110px" Height="22px" CssClass="usatGuardar" />
        &nbsp;&nbsp;<asp:Button ID="btnEliminarPreInscrito" runat="server" Text="Eliminar Pre-Inscritos"  Width="150px" Height="22px" CssClass="usatEliminar" />
        <asp:HiddenField ID="HdCodigo_Cco" runat="server" />


    </form>
</body>
</html>
