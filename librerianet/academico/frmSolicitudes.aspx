<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSolicitudes.aspx.vb" Inherits="academico_frmSolicitudes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
        <asp:GridView ID="gvDatos" runat="server" Width="100%" 
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Fecha_Sol" HeaderText="FECHA" />
                <asp:BoundField DataField="codigouniver_alu" HeaderText="COD. UNIV." />
                <asp:BoundField DataField="alumno" HeaderText="ALUMNO" />
                <asp:BoundField DataField="abreviatura_cpf" HeaderText="ESCUELA PROFESIONAL" />
                <asp:BoundField DataField="Estado" HeaderText="ESTADO" />
            </Columns>
            <HeaderStyle BackColor="#0066CC" ForeColor="White" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
