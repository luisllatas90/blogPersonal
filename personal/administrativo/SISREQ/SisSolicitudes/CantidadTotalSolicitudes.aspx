<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CantidadTotalSolicitudes.aspx.vb" Inherits="SisSolicitudes_CantidadTotalSolicitudes" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <script src="../../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <style type="text/css">
    .ExportarAWord{border:1px solid #C0C0C0; background:#FEFFE1 url('../images/exportaraword.gif') no-repeat 0% 80%; width:70; font-family:Tahoma; font-size:8pt; font-weight:bold; height:25}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>
                    <b>Cantidad de Solicitudes Registradas: </b>
                    <asp:Label ID="LblTotal" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Ciclo Acacémico: <asp:DropDownList ID="cboCicloAcad" runat="server" 
                        AutoPostBack="True">
                </asp:DropDownList>
                &nbsp; Escuela profesional:
                    <asp:DropDownList ID="cboEscuela" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <asp:GridView ID="GvAsunto" runat="server" GridLines="Horizontal" 
                        DataKeyNames="codigo_tas,nroasuntos">
                        <Columns>
                            <asp:CommandField SelectText="" ShowSelectButton="True" />
                        </Columns>
                        <SelectedRowStyle BackColor="#FFFFCC" />
                        <HeaderStyle BackColor="#4182CD" ForeColor="White" />
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
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="center" style="font-weight: 700">
                    DETALLE DE SOLICITUDES REGISTRADAS HASTA LA FECHA                     <asp:Label ID="LblFecha" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                <asp:Button ID="CmdExportar" runat="server" Text="Exportar" 
                    CssClass="ExportarAWord" Width="70px" />
                </td>
            </tr>
            <tr>
                <td>
                <asp:Table ID="TblSolicitudes" runat="server" BorderColor="Black" 
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="0" CellSpacing="0" 
                    GridLines="Horizontal" Width="100%" BackColor="White">
                </asp:Table>
                </td>
            </tr>
        </table>
    
    </div>
    <asp:HiddenField ID="HddNroAsunto" runat="server" />
    <asp:HiddenField ID="HddAsunto" runat="server" />
    </form>
</body>
</html>
