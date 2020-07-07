<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmresultadoinvestigacion.aspx.vb" Inherits="frmresultadoinvestigacion" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resultado de la investigación</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>s
<body bgcolor="#eeeeee">
    <form id="form1" runat="server">
    <p class="usatTitulo">Resultado de la investigación<p/>
    <table width="100%" cellpadding="3" class="contornotabla">
        <tr>
            <td width="20%" align="center">
                <asp:Panel ID="Panel1" runat="server" Visible="False">
                <h5 class="azul">
                    ¿Está conforme con la evaluación registrada por el asesor según la fase de la 
                    investigación?
                        <br /><br />
                        <asp:Button ID="cmdSi" runat="server" Text="Sí" />
                        &nbsp;<asp:Button ID="cmdNo" runat="server" Text="No" />
                    </h5>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td width="20%">
                <asp:Panel ID="Panel2" runat="server">
                    <table class="style1">
                        <tr>
                            <td width="20%">
                                Fecha Registro</td>
                            <td>
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </td>
                            <td>
                                Nro Doc.</td>
                            <td>
                                <asp:TextBox ID="txtDocResultado" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                Resultado</td>
                            <td>
                                <asp:DropDownList ID="dpResultado" runat="server">
                                    <asp:ListItem Value="A">APROBADO</asp:ListItem>
                                    <asp:ListItem Value="D">DESAPROBADO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                Nota</td>
                            <td>
                                <asp:TextBox ID="txtNota" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                Observación</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtObs" runat="server" CssClass="cajas2" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                Adjuntar</td>
                            <td colspan="3">
                                <asp:FileUpload ID="flArchivo" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" width="100%" align="center">
                                <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" />
                                &nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="Cerrar" 
                                    CausesValidation="False" onclientclick="window.close();return(false)" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
