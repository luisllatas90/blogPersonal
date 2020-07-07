<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCursoComentarios.aspx.vb" Inherits="academico_cargalectiva_frmCursoComentarios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblCurso" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
        <asp:GridView ID="gvComentarios" runat="server" AutoGenerateColumns="False" Width="100%">
            <Columns>
                <asp:BoundField DataField="codigo_edd" HeaderText="codigo_edd" 
                    Visible="False" />
                <asp:BoundField DataField="respuestaTexto_edd" HeaderText="Comentarios" />
            </Columns>            
        </asp:GridView>
    
    </div>
    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="Small" 
        ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
