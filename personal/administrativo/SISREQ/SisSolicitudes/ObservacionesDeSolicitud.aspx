<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ObservacionesDeSolicitud.aspx.vb" Inherits="SisSolicitudes_ObservacionesDeSolicitud" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

    <br />

    <asp:GridView ID="GvObservaciones" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="codigo_sol" DataSourceID="SqlDataSource1" Width="100%">
        <RowStyle Height="20px" />
        <Columns>
            <asp:BoundField DataField="codigo_sol" HeaderText="codigo_sol" 
                InsertVisible="False" ReadOnly="True" SortExpression="codigo_sol" 
                Visible="False" />
            <asp:BoundField DataField="persona" HeaderText="Evaluador que observó" 
                ReadOnly="True" SortExpression="persona">
                <ItemStyle Width="20%" />
            </asp:BoundField>
            <asp:BoundField DataField="observacion_eob" HeaderText="Observación" 
                SortExpression="observacion_eob">
                <ItemStyle Width="50%" />
            </asp:BoundField>
            <asp:BoundField DataField="instancia_eob" HeaderText="Instancia Observada" 
                SortExpression="instancia_eob">
                <ItemStyle Width="20%" />
            </asp:BoundField>
            <asp:BoundField DataField="estado_eob" HeaderText="Estado" 
                SortExpression="estado_eob">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="codigo_eva" HeaderText="codigo_eva" 
                SortExpression="codigo_eva" Visible="False" />
        </Columns>
        <HeaderStyle CssClass="celdatitulo" Height="15px" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="SOL_ConsultarSolicitudesObservadas" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="4" Name="tipo" Type="Int32" />
            <asp:QueryStringParameter DefaultValue="" Name="codigo" 
                QueryStringField="codigo_sol" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="nivel" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    </form>
</body>
</html>
