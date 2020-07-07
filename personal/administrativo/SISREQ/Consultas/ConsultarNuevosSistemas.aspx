<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConsultarNuevosSistemas.aspx.vb" Inherits="Consultas_ConsultarNuevosSistemas" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link rel="stylesheet" type ="text/css" href ="../private/estilo.css" />
    <link rel="stylesheet" type ="text/css" href ="../private/estiloweb.css" />
    <script src="../../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" height="200" valign="top">
    
                    <asp:Panel ID="Panel1" runat="server" Height="100%">
                        <asp:GridView ID="GvNewModulo" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="id_sol" Width="98%" GridLines="Horizontal">
                            <RowStyle Height="25px" />
                            <Columns>
                                <asp:BoundField DataField="id_sol" HeaderText="id_sol" InsertVisible="False" 
                    ReadOnly="True" SortExpression="id_sol" Visible="False" />
                                <asp:BoundField DataField="descripcion_sol" HeaderText="Solicitud" 
                    SortExpression="descripcion_sol">
                                    <ItemStyle Width="50%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_sol" DataFormatString="{0:dd/MM/yyyy}" 
                    HeaderText="Fecha" SortExpression="fecha_sol" />
                                <asp:BoundField DataField="descripcion_tsol" HeaderText="descripcion_tsol" 
                    SortExpression="descripcion_tsol" Visible="False" />
                                <asp:BoundField DataField="descripcion_cco" HeaderText="Área" 
                    SortExpression="descripcion_cco">
                                    <ItemStyle Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="personal" HeaderText="Solicitante" ReadOnly="True" 
                    SortExpression="personal">
                                    <ItemStyle Width="20%" />
                                </asp:BoundField>
                                <asp:CommandField SelectText="" ShowSelectButton="True" />
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                                    Text="No se encontraron registros"></asp:Label>
                            </EmptyDataTemplate>
                            <SelectedRowStyle BackColor="#FFFFCC" />
                            <HeaderStyle CssClass="TituloReq" />
                        </asp:GridView>
                    </asp:Panel>
    
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 1px; background-color: #004182;">
                </td>
            </tr>
            <tr>
                <td align="right">
    
                <asp:Button ID="CmdExportar" runat="server" Text="      Exportar" 
                    CssClass="ExportarAExcel" Width="85px" Height="25px" Visible="False" />
            &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
    
                    <asp:Table ID="TblCronograma" runat="server" CellPadding="0" CellSpacing="0" 
                        Width="98%">
                    </asp:Table>
    
                </td>
            </tr>
        </table>
    
    </div>
    <asp:HiddenField ID="HddSolicitud" runat="server" />
    </form>
</body>
</html>
