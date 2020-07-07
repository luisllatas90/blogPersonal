<%@ Page Language="VB" AutoEventWireup="false" CodeFile="presentacion_agenda.aspx.vb" Inherits="secretarias_presentacion_intro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../estilo.css"rel="stylesheet" type="text/css">
<script type="text/javascript" src="../funciones.js"> </script>
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblReunion" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    -
                    <asp:Label ID="lblFecha" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                &nbsp;-</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblFacultad" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Size="Large" ForeColor="#993333" 
                        Text="AGENDA:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
    <asp:GridView ID="dgvDetalle" runat="server" DataSourceID="SqlDataSource1" 
        AutoGenerateColumns="False" CellPadding="3" CellSpacing="5" Font-Size="Large" 
                        GridLines="None" ShowHeader="False">
        <RowStyle Font-Size="Large" ForeColor="#003366" />
        <Columns>
            <asp:BoundField DataField="codigo_prp" HeaderText="Cod" 
                SortExpression="codigo_prp" >
                <ItemStyle ForeColor="White" />
            </asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="codigo_prp,id_rec" 
                DataNavigateUrlFormatString="datospropuesta.aspx?codigo_prp={0}&amp;id_rec={1}" 
                DataTextField="nombre_Prp" HeaderText="Propuesta" SortExpression="nombre_Prp">
                <ItemStyle Font-Size="Large" />
            </asp:HyperLinkField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="PRP_ConsultarSesionesConsejos" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="AG" Name="tipo" Type="String" />
            <asp:QueryStringParameter DefaultValue="" Name="codigo_per" 
                QueryStringField="id_rec" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
