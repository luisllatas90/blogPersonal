<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DetAlumnos.aspx.vb" Inherits="Egresado_DetAlumnos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
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
    <div> Tipo:   
        <asp:DropDownList ID="cboEgresado" runat="server">            
            <asp:ListItem Value="TO">---- Todos ----</asp:ListItem>
            <asp:ListItem Value="E">Egresado</asp:ListItem>
            <asp:ListItem Value="B">Bachiller</asp:ListItem>            
            <asp:ListItem Value="T">Titulado</asp:ListItem>
            <asp:ListItem Value="C">créditos > 120</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" Width="100px" CssClass="buscar2" Height="22px" />
        &nbsp;
    <asp:Label ID="lblNumRegistros" runat="server" Font-Bold="True"></asp:Label>
        <asp:GridView ID="gvwAlumnos" runat="server" Width="100%" 
            AutoGenerateColumns="False" PageSize="15" 
            DataKeyNames="codigo_pso">
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
                <asp:BoundField DataField="codigo_pso" HeaderText="codigo_pso"/>
                <asp:BoundField DataField="NombreCompleto" HeaderText="Egresado" />
                <asp:BoundField DataField="nombre_Cpf" HeaderText="Profesión" />
                <asp:BoundField HeaderText="Año" DataField="FechaEgresado">
                    <ItemStyle Width="7%" HorizontalAlign="Center"  />
                </asp:BoundField>
                <asp:BoundField DataField="Email" HeaderText="Correo" />
                <asp:BoundField DataField="Telefono" HeaderText="Telefono">
                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Ficha">
                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Historial">
                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                </asp:TemplateField>
                <asp:BoundField DataField="codigo_alu" HeaderText="codigo_alu" />
            </Columns>
            <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
            <RowStyle Height="22px" />
        </asp:GridView>
    </div>
    <br />
    <br />
    <asp:Button ID="btnEnviar" runat="server" Text="Enviar Oferta" 
        CssClass="guardar" Height="22px" Width="110px" />
    <br />
    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="#006666"></asp:Label>
    <asp:HiddenField ID="HdCodigo_ofe" runat="server" />
    <asp:HiddenField ID="HdListaCarreras" runat="server" />
    </form>
</body>
</html>
