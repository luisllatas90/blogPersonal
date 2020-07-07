<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmcambiartitulotesis.aspx.vb" Inherits="personal_academico_tesis_frmcambiartitulotesis" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cambiar Título de tesis</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    
    <table width="100%">
        <tr>
            <td style="width:60%" class="usatTitulo">Bitácora de cambios</td>
            <td style="width:40%">
            <table width="100%" cellpadding="3" cellspacing="0" style="border: 1px solid #507CD1; background-color: #eff3fb;">
             <tr>
                <td style=" width:15%"><b>Código</b></td>
                <td style=" width:75%">
                    <asp:TextBox ID="txtTermino" runat="server" 
                        CssClass="cajas2" MaxLength="20"></asp:TextBox></td>
                <td style=" width:10%"><asp:Button ID="cmdBuscar" runat="server" Text="    Buscar" CssClass="buscar_prp_small" /></td>
            </tr>
            </table>
            </td>
        </tr>
    </table>
        <asp:DataList ID="DataList1" runat="server" Width="100%">
            <ItemTemplate>
                <table cellpadding="3" cellspacing="0" width="100%" 
                    style="border-style: none none solid none; border-width: 1px; border-color: #000099">
                    <tr>
                        <td>
                            <asp:Label ID="lblFase" runat="server" Text='<%# eval("descripcion_eti") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" 
                                Text='<%# eval("titulo_tes") %>'></asp:Label>
                            <br />
                            <asp:Label ID="lblProblema" runat="server" 
                        Font-Italic="True" Text='<%# eval("problema_tes") %>'></asp:Label>
                            <br />
                            <asp:Label ID="lblResumen" runat="server" Text='<%# eval("resumen_tes") %>'></asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
    </form>
</body>
</html>
