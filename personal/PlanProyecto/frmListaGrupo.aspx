<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaGrupo.aspx.vb" Inherits="PlanProyecto_frmListaGrupo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" Height="22px" Width="100px" CssClass="salir" />&nbsp;    
    <asp:Button ID="btnConfigurar" runat="server" Text="Configurar Grupo" Height="22px" Width="150px" CssClass="usatModificar" /><br /><br />
    <asp:Label ID="lblProyecto" runat="server" Height="22px" Font-Bold="True"></asp:Label><br />
    <asp:GridView ID="gvGrupo" runat="server" Width="100%" 
        AutoGenerateColumns="False" DataKeyNames="codigo_gpr">
        <Columns>
            <asp:BoundField DataField="codigo_gpr" HeaderText="codigo_gpr" 
                Visible="False" />
            <asp:BoundField DataField="nombre_gpr" HeaderText="Grupo" />
            <asp:BoundField DataField="fechaInicio_gpr" HeaderText="Inicia" />
            <asp:BoundField DataField="fechaFin_gpr" HeaderText="Fin" />
            <asp:CommandField ButtonType="Image" HeaderText="Ver" 
                SelectImageUrl="../../images/ok.gif" SelectText="" ShowSelectButton="True">
                <ItemStyle HorizontalAlign="Center" Width="10%" />
            </asp:CommandField>
            <asp:CommandField ButtonType="Image" DeleteImageUrl="../../images/eliminar.gif" 
                HeaderText="Eliminar" ShowDeleteButton="True" >
                <ItemStyle HorizontalAlign="Center" Width="10%" />
            </asp:CommandField>            
        </Columns>
        <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
        <RowStyle Height="22px" />
    </asp:GridView><br />    
    <asp:Label ID="lblMensaje" runat="server" Height="22px" Font-Bold="True" 
        ForeColor="Red"></asp:Label><br />
    </form>
</body>
</html>
