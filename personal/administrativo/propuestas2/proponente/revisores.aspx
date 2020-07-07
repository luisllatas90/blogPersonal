<%@ Page Language="VB" AutoEventWireup="false" CodeFile="revisores.aspx.vb" Inherits="proponente_revisores" %>

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
    <table class="style1">
        <tr>
            <td valign="top" width="50%">
                <table class="style1">
                    <tr>
                        <td>
                <asp:GridView ID="dgvDirectores" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="SqlDataSource5" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="involucrado" HeaderText="Dirección de Area" 
                            InsertVisible="False" ReadOnly="True" SortExpression="involucrado" />
                        <asp:BoundField DataField="instancia_ipr" HeaderText="Instancia" 
                            ReadOnly="True" SortExpression="instancia_ipr" Visible="False" />
                        <asp:BoundField DataField="veredicto_ipr" 
                            SortExpression="veredicto_ipr" />
                        <asp:BoundField DataField="codigo_ipr" HeaderText="Cod" 
                            SortExpression="codigo_ipr" Visible="False" />
                    </Columns>
                    <HeaderStyle ForeColor="Blue" />
                </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                <asp:GridView ID="dgvConsejoFacultad" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="SqlDataSource1" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="involucrado" HeaderText="Consejo de Facultad" 
                            InsertVisible="False" ReadOnly="True" SortExpression="involucrado" />
                        <asp:BoundField DataField="instancia_ipr" HeaderText="Instancia" 
                            ReadOnly="True" SortExpression="instancia_ipr" Visible="False" />
                        <asp:BoundField DataField="veredicto_ipr" 
                            SortExpression="veredicto_ipr" />
                        <asp:BoundField DataField="codigo_ipr" HeaderText="Cod" 
                            SortExpression="codigo_ipr" Visible="False" />
                    </Columns>
                    <HeaderStyle ForeColor="Blue" />
                </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr align="left" style="line-height: 1px; width: 292px;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                <asp:GridView ID="dgvRevisoresFacultad" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="SqlDataSource2" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="involucrado" HeaderText="Revisores" 
                            InsertVisible="False" ReadOnly="True" SortExpression="involucrado" />
                        <asp:BoundField DataField="instancia_ipr" HeaderText="Instancia" 
                            ReadOnly="True" SortExpression="instancia_ipr" Visible="False" />
                        <asp:BoundField DataField="veredicto_ipr" 
                            SortExpression="veredicto_ipr" />
                        <asp:BoundField DataField="codigo_ipr" HeaderText="Cod" 
                            SortExpression="codigo_ipr" Visible="False" />
                    </Columns>
                    <HeaderStyle ForeColor="Blue" />
                </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                <asp:GridView ID="dgvRevisoresPostGrado" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="SqlDataSource6" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="involucrado" HeaderText="Postgrado" 
                            InsertVisible="False" ReadOnly="True" SortExpression="involucrado" />
                        <asp:BoundField DataField="instancia_ipr" HeaderText="Instancia" 
                            ReadOnly="True" SortExpression="instancia_ipr" Visible="False" />
                        <asp:BoundField DataField="veredicto_ipr" 
                            SortExpression="veredicto_ipr" />
                        <asp:BoundField DataField="codigo_ipr" HeaderText="Cod" 
                            SortExpression="codigo_ipr" Visible="False" />
                    </Columns>
                    <HeaderStyle ForeColor="Blue" />
                </asp:GridView>
                <asp:GridView ID="dgvRevisoresProfesionalizacion" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="SqlDataSource7" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="involucrado" HeaderText="Profesionalización" 
                            InsertVisible="False" ReadOnly="True" SortExpression="involucrado" />
                        <asp:BoundField DataField="instancia_ipr" HeaderText="Instancia" 
                            ReadOnly="True" SortExpression="instancia_ipr" Visible="False" />
                        <asp:BoundField DataField="veredicto_ipr" 
                            SortExpression="veredicto_ipr" />
                        <asp:BoundField DataField="codigo_ipr" HeaderText="Cod" 
                            SortExpression="codigo_ipr" Visible="False" />
                    </Columns>
                    <HeaderStyle ForeColor="Blue" />
                </asp:GridView>
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
                </table>
            </td>
            <td width="50%" valign="top">
                <table class="style1">
                    <tr>
                        <td>
                <asp:GridView ID="dgvConsejoFacultad0" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="SqlDataSource3" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="involucrado" HeaderText="Rectorado" 
                            InsertVisible="False" ReadOnly="True" SortExpression="involucrado" />
                        <asp:BoundField DataField="instancia_ipr" HeaderText="Instancia" 
                            ReadOnly="True" SortExpression="instancia_ipr" Visible="False" />
                        <asp:BoundField DataField="veredicto_ipr" 
                            SortExpression="veredicto_ipr" />
                        <asp:BoundField DataField="codigo_ipr" HeaderText="Cod" 
                            SortExpression="codigo_ipr" Visible="False" />
                    </Columns>
                    <HeaderStyle ForeColor="Blue" />
                </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                <asp:GridView ID="dgvRevisoresFacultad0" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="SqlDataSource4" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="involucrado" HeaderText="Consejo Universitario" 
                            InsertVisible="False" ReadOnly="True" SortExpression="involucrado" />
                        <asp:BoundField DataField="instancia_ipr" HeaderText="Instancia" 
                            ReadOnly="True" SortExpression="instancia_ipr" Visible="False" />
                        <asp:BoundField DataField="veredicto_ipr" 
                            SortExpression="veredicto_ipr" />
                        <asp:BoundField DataField="codigo_ipr" HeaderText="Cod" 
                            SortExpression="codigo_ipr" Visible="False" />
                    </Columns>
                    <HeaderStyle ForeColor="Blue" />
                </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                    SelectCommand="ConsultarResponsablesPropuesta" 
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="FC" Name="tipo" Type="String" />
                        <asp:QueryStringParameter DefaultValue="" Name="param1" 
                            QueryStringField="codigo_prp" Type="Int32" />
                        <asp:Parameter DefaultValue="0" Name="param2" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="ConsultarResponsablesPropuesta" 
        SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter DefaultValue="RF" Name="tipo" Type="String" />
                    <asp:QueryStringParameter DefaultValue="" Name="param1" 
                        QueryStringField="codigo_prp" Type="Int32" />
                    <asp:Parameter DefaultValue="0" Name="param2" Type="Int32" />
                </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="ConsultarResponsablesPropuesta" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="CK" Name="tipo" Type="String" />
            <asp:QueryStringParameter DefaultValue="" Name="param1" 
                QueryStringField="codigo_prp" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="param2" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="ConsultarResponsablesPropuesta" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="CC" Name="tipo" Type="String" />
            <asp:QueryStringParameter DefaultValue="" Name="param1" 
                QueryStringField="codigo_prp" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="param2" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="ConsultarResponsablesPropuesta" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="AD" Name="tipo" Type="String" />
            <asp:QueryStringParameter DefaultValue="" Name="param1" 
                QueryStringField="codigo_prp" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="param2" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource6" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="ConsultarResponsablesPropuesta" 
        SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter DefaultValue="EP" Name="tipo" Type="String" />
                    <asp:QueryStringParameter DefaultValue="" Name="param1" 
                        QueryStringField="codigo_prp" Type="Int32" />
                    <asp:Parameter DefaultValue="0" Name="param2" Type="Int32" />
                </SelectParameters>
    </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource7" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="ConsultarResponsablesPropuesta" 
        SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter DefaultValue="DP" Name="tipo" Type="String" />
                    <asp:QueryStringParameter DefaultValue="" Name="param1" 
                        QueryStringField="codigo_prp" Type="Int32" />
                    <asp:Parameter DefaultValue="0" Name="param2" Type="Int32" />
                </SelectParameters>
    </asp:SqlDataSource>
    </form>
</body>
</html>
