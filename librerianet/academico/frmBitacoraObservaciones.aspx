<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBitacoraObservaciones.aspx.vb" Inherits="academico_frmBitacoraObservaciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id ="diverrores" runat="server"></div>
    <div>
    
        <asp:GridView ID="grwListaObservaciones" runat="server" Width="100%" CellPadding="4" 
                            ForeColor="#333333" AutoGenerateColumns="False" 
                            DataKeyNames="codigoTabla_mca" AllowPaging="True">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField HeaderText="Nro" />
                                <asp:BoundField DataField="alumno" HeaderText="Alumno">
                                </asp:BoundField>
                                <asp:BoundField DataField="observusuario_Alu" HeaderText="Observación">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Fecha"  DataField="fecha_mca" />            
                                <asp:BoundField DataField="usuario" HeaderText="Usuario" />         
                            </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>                               
    </div>
    </form>
</body>
</html>
