<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Listado_investigaciones.aspx.vb" Inherits="Listado_investigaciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
    <script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../../../private/tooltip.js"></script>
    <script type="text/javascript" language="JavaScript" src="../private/validarnotas.js"></script>
    <title>Página sin título</title>
</head>
<body style="margin:0px, 0px, 0px, 0px;">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;" bgcolor="#F0F0F0">
            <tr>
                <td colspan="2" height="35px">
                    <asp:Label ID="Label1" runat="server" Text="Persona"></asp:Label>
                    :
                    <asp:DropDownList ID="cboPersonal" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" height="1" bgcolor="Black">
                    </td>
            </tr>
            <tr>
                <td height="25" style="font-size: Small; font-weight: bold;">
                    Listado de investigaciones realizadas a la fecha </td>
                <td align="right">
                    &nbsp;<asp:Button ID="cmdExportar" runat="server" BackColor="White" BorderStyle="Solid" 
                        BorderWidth="1px" CssClass="excel" Font-Bold="True" Height="26px" 
                        Text="Exportar" Width="97px" />
&nbsp;&nbsp; </td>
            </tr>
            </table>
    
    </div>
                    <asp:GridView ID="gvInvestigaciones" runat="server" AutoGenerateColumns="False" 
                        CellPadding="3" DataSourceID="SqlDataSource1" 
                        GridLines="Horizontal" Width="100%" BackColor="White" 
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
        HorizontalAlign="Center" EnableViewState="False">
                        <RowStyle ForeColor="#666666" />
                        <Columns>
                            <asp:BoundField DataField="titulo_inv" HeaderText="Título" />
                            <asp:BoundField DataField="fechainicio_inv" HeaderText="Fecha Inicio" 
                                DataFormatString="{0:dd/MM/yyyy}" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_lin" HeaderText="Linea de investigación" 
                                HtmlEncode="False" />
                            <asp:BoundField DataField="descripcion_ein" HeaderText="Estados">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_eti" HeaderText="Tipo Investigación">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="INV_ConsultarInvestigacionesPorDocente" 
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="cboPersonal" Name="codigo_per" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
    </form>
</body>
</html>
