<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmjurado.aspx.vb" Inherits="frmjurado" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resultado de la investigación</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    </head>
<body bgcolor="#eeeeee">
    <form id="form1" runat="server">
    <p class="usatTitulo">Propuesta de Jurado<p/>
    <table width="100%" cellpadding="3" class="contornotabla">
        <tr>
                            <td width="20%">
                                Fecha Registro</td>
                            <td>
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
        <tr>
                            <td width="20%">
                                Presidente</td>
                            <td>
                                ARROLLO ULLOA, MAX</td>
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="..." />
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                    <asp:ListItem>Por aprobar</asp:ListItem>
                                    <asp:ListItem>Aprobado</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                Secretario</td>
                            <td>
                                ALONSO PEREZ, EDUARDO</td>
                            <td>
                                <asp:Button ID="Button2" runat="server" Text="..." />
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList2" runat="server">
                                    <asp:ListItem>Por aprobar</asp:ListItem>
                                    <asp:ListItem>Aprobado</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                Vocal / Asesor</td>
                            <td>
                                OTAKE OLLAMA, LUIS</td>
                            <td>
                                <asp:Button ID="Button3" runat="server" Text="..." />
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList3" runat="server">
                                    <asp:ListItem>Por aprobar</asp:ListItem>
                                    <asp:ListItem>Aprobado</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                Observación</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtobs" runat="server" CssClass="cajas2" Height="34px" 
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" width="100%" align="center">
                                <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" />
                                &nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="Cerrar" 
                                    CausesValidation="False" onclientclick="window.close();return(false)" />
                            </td>
                        </tr>
                    
        </tr>
        </table>
    </form>
</body>
</html>
