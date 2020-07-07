<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmpermisosrecursos.aspx.vb" Inherits="sistema_frmpermisosrecursos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="private/Estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">    
    <div id="ListaDocente">                 
        <asp:ListBox ID="LstDocente" runat="server" Width="100%" Height="100%" 
            Font-Size="Small"></asp:ListBox>                
    </div>    
    <div id="Permisos">
        Buscar permisos en:
        <asp:DropDownList ID="cboModo" runat="server">
            <asp:ListItem Text="Escuelas Profesionales" Value="1"></asp:ListItem>
            <asp:ListItem Text="Ambientes USAT" Value="2"></asp:ListItem>
            <asp:ListItem Text="Concepto de Servicios" Value="5"></asp:ListItem>
            <asp:ListItem Text="Centro de Costos" Value="8"></asp:ListItem>
            <asp:ListItem Text="Departamento Académico" Value="10"></asp:ListItem>
        </asp:DropDownList>
        &nbsp;<asp:Button ID="btnBuscar" runat="server" Text="Buscar" />
    </div>
    <div id="Aviso">
        ACCESO PARA ADMINISTRAR / CONSULTAR ESCUELAS PROFESIONALES
        <asp:GridView ID="dgvCentroCosto" runat="server" Width="100%" 
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Codigo" HeaderText="Codigo" >
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" >                
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="Marca" HeaderText="marca" >
                    <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:CommandField ButtonType="Image" HeaderText="Activar" 
                    EditImageUrl="~/sistema/private/ok.gif" ShowEditButton="True">                    
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:CommandField>
                <asp:CommandField ButtonType="Image" 
                    DeleteImageUrl="~/sistema/private/eliminar.gif" HeaderText="Quitar" 
                    ShowDeleteButton="True">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>        
    </div>    
    <div id="ListaCCO">        
    </div>
    <div id="Error">
        <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Names="Tahoma" 
            Font-Size="Small" ForeColor="Red"></asp:Label> 
    </div>
    <asp:HiddenField ID="hdPersonal" runat="server" />    
    </form>
</body>
</html>
