<%@ Page Language="VB" AutoEventWireup="false" CodeFile="avanzadas.aspx.vb" Inherits="Consultas_avanzadas" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link rel="STYLESHEET"  href="../private/estilo.css"/>
<title>Página sin título</title>
<script>
    function pintarcelda(tabla)
        {
            tabla.style.backgroundColor='#E4CE74';
        }
        
    function despintarcelda(tabla)
        {
            tabla.style.backgroundColor='#FFFFFF';
        }
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center" colspan="3">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="3" style="border-right: black 1px solid; border-top: black 1px solid;
                                font-size: 8pt; border-left: black 1px solid; color: navy; border-bottom: black 1px solid;
                                font-family: verdana; text-align: left; font-variant: normal">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="center" colspan="2" style="border-right: 1px solid; border-top: 1px solid;
                                            font-size: 10pt; border-left: 1px solid; color: white; border-bottom: black 1px solid;
                                            font-family: VERDANA; height: 19px; background-color: saddlebrown">
                                            BUSQUEDA AVANZADA</td>
                                    </tr>
                                    <tr>
                                        <td style="height: 13px">
                                        </td>
                                        <td style="height: 13px; width: 382px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 13px">
                                &nbsp;Departamento Académico</td>
                                        <td style="height: 13px; width: 382px;">
                                            :
                                            <asp:DropDownList ID="DDLDepAcad" runat="server" Style="font-size: 8pt; color: navy; font-family: verdana" Width="371px">
                                </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;Título Profesional de</td>
                                        <td style="width: 382px">
                                            :
                                            <asp:TextBox ID="TxtTitulo" runat="server" Style="border-right: black 1px solid;
                                                border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: navy;
                                                border-bottom: black 1px solid; font-family: verdana" Width="365px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 13px">
                                            &nbsp;Grado Académico de</td>
                                        <td style="height: 13px; width: 382px;">
                                            :
                                            <asp:TextBox ID="TxtGrado" runat="server" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                font-size: 8pt; border-left: black 1px solid; color: navy; border-bottom: black 1px solid;
                                                font-family: verdana" Width="365px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;Otros Estudios en</td>
                                        <td style="width: 382px">
                                            :
                                            <asp:TextBox ID="TxtOtros" runat="server" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                font-size: 8pt; border-left: black 1px solid; color: navy; border-bottom: black 1px solid;
                                                font-family: verdana" Width="365px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;Estado Planilla</td>
                                        <td style="height: 13px; width: 382px;">
                                            :
                                            <asp:DropDownList ID="DDLEstado" runat="server" Style="font-size: 8pt; color: navy; font-family: verdana" Width="100px">
                                
                                </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 36px" valign="top">
                                            &nbsp;<asp:CheckBox ID="ChkIdi" runat="server" Text="Estudios de Idiomas" /></td>
                                        <td align="right" style="height: 36px; width: 382px;">
                                            <asp:Button ID="Button1" runat="server" CssClass="buscar1" Text="Buscar" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Width="93px" Height="27px" />&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="3" style="font-size: 8pt; color: navy; font-family: verdana">
                                <br />
                                &nbsp;Se encontraron
                                <asp:Label ID="LblCantidad" runat="server" Font-Bold="True"></asp:Label>
                                coincidencias a su búsqueda.</td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:DataList ID="DataList1" runat="server" RepeatColumns="5"
                                    RepeatDirection="Horizontal" CellPadding="3" CellSpacing="6">
                                    <ItemTemplate>
                                        <table onmouseover="pintarcelda(this)" onmouseout="despintarcelda(this)" style="border-right: darkred 2px solid; border-top: darkred 2px solid; border-left: darkred 2px solid;
                                            width: 97px; border-bottom: darkred 2px solid; height: 120px">
                                            <tr>
                                                <td style="height: 6px" valign="top" align="center">
                                                    &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ruta") %>'
                                                        Text='<%# Eval("Docente") %>' Font-Names="Arial" Font-Size="6pt" ForeColor="Navy"></asp:HyperLink></td>
                                            </tr>
                                            <tr>
                                                <td valign="bottom" align="center">
                                                    <asp:Image ID="Image1" runat="server" Height="65px" ImageUrl='<%# Eval("foto") %>'
                                                        Width="50px" BorderColor="Black" BorderStyle="Solid" ToolTip='<%# Eval("Docente") %>' /></td>
                                            </tr>
                                        </table>
                                        &nbsp;
                                    </ItemTemplate>
                                </asp:DataList>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
    
    </div>
        &nbsp;&nbsp;
    </form>
</body>
</html>
