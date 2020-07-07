<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmHorario.aspx.vb" Inherits="frmHorario" %>

<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Horario</title>
    <style type="text/css">

        .style1
        {
            width: 107px;
        }
        .style2
        {
            width: 602px;
        }
        .style4
        {
            width: 123px;
            height: 22px;
        }
        .style5
        {}
    </style>
</head>
<body>
    <form id="form1" runat="server" style="font-family: Verdana; font-size: 11px">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td style="font-weight: bold">
                    Datos Generales<asp:Label ID="lblMensaje" runat="server" Font-Bold="True" 
                        Font-Size="Large" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                </td>
                <td align="right" style="font-weight: bold">
                    <asp:Button ID="btnEnviar" runat="server" Text="Finalizar y Enviar" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width:100%;">
                        <tr>
                            <td class="style1" style="font-weight: bold">
                                Nombre</td>
                            <td class="style2">
                                <asp:Label ID="lblNombre" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td rowspan="3">
                                <asp:Image ID="imgFoto" runat="server" Height="122px" Width="113px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" style="font-weight: bold">
                                Centro Costos</td>
                            <td class="style2">
                                <asp:Label ID="lblCeco" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" style="font-weight: bold">
                                Dedicación</td>
                            <td class="style2" style="font-weight: bold">
                                <asp:Label ID="lblDedicacion" runat="server" Text="Label" Font-Bold="False"></asp:Label>
&nbsp; Nro. Horas Semanales&nbsp;&nbsp;
                                <asp:TextBox ID="lblHoras" runat="server" Width="53px"></asp:TextBox>
&nbsp;&nbsp; Tipo
                                <asp:Label ID="lblTipo" runat="server" Text="Label" Font-Bold="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width: 100%;">
                        <tr>
                            <td class="style5" style="font-weight: bold">
                                Total Horas Semanales:                                 <asp:Label ID="lblHorasSemanales" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="font-weight: bold">
                                Registro de horario <td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table style="width:100%;">
                                    <tr>
                                        <td valign="top">
                                            <asp:GridView ID="gvVistaHorario" runat="server" BorderStyle="Solid" 
                                                CellPadding="4" ForeColor="#333333">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </td>
                                        <td align="left" valign="top">
                                            <table bgcolor="White" style="width: 51%;">
                                                <tr>
                                                    <td class="style4">
                                                        &nbsp;</td>
                                                    <td bgcolor="Yellow" class="style4">
                                                        Investigación</td>
                                                </tr>
                                                <tr>
                                                    <td class="style4">
                                                        &nbsp;</td>
                                                    <td bgcolor="Green" class="style4">
                                                        Docencia</td>
                                                </tr>
                                                <tr>
                                                    <td class="style4">
                                                        &nbsp;</td>
                                                    <td bgcolor="Violet" class="style4">
                                                        Apoyo Escuela</td>
                                                </tr>
                                                <tr>
                                                    <td class="style4">
                                                        &nbsp;</td>
                                                    <td bgcolor="Orange" class="style4">
                                                        Administrativo</td>
                                                </tr>
                                                <tr>
                                                    <td class="style4">
                                                        &nbsp;</td>
                                                    <td bgcolor="Blue" class="style4" style="color: #FFFFFF">
                                                        Práctica Externa</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td width="25%">
                                                        Día</td>
                                                    <td width="25%">
                                                        <asp:DropDownList ID="ddlDia" runat="server">
                                                            <asp:ListItem Value="LU">Lunes</asp:ListItem>
                                                            <asp:ListItem Value="MA">Martes</asp:ListItem>
                                                            <asp:ListItem Value="MI">Miercoles</asp:ListItem>
                                                            <asp:ListItem Value="JU">Jueves</asp:ListItem>
                                                            <asp:ListItem Value="VI">Viernes</asp:ListItem>
                                                            <asp:ListItem Value="SA">Sábado</asp:ListItem>
                                                            <asp:ListItem Value="DO">Domingo</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="25%">
                                                        Tipo</td>
                                                    <td width="25%">
                                                        <asp:DropDownList ID="ddlTipo" runat="server">
                                                            <asp:ListItem Value="D">Docencia</asp:ListItem>
                                                            <asp:ListItem Value="A">Administrativo</asp:ListItem>
                                                            <asp:ListItem Value="E">Apoyo Escuela</asp:ListItem>
                                                            <asp:ListItem Value="P">Práctica Externa</asp:ListItem>
                                                            <asp:ListItem Value="I">Investigación</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Inicio</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlHoraInicio" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        Fin</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlHoraFin" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Escuela</td>
                                                    <td colspan="3">
                                                        <asp:DropDownList ID="ddlEscuela" runat="server" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text="Encargo" 
                                                            ToolTip="Encargo administrativo"></asp:Label>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtEncEscuela" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Resolución</td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtResEscuela" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td colspan="2">
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvEditHorario" runat="server" CellPadding="4" 
                                                ForeColor="#333333" GridLines="None" 
                                                Caption="Distribución de carga Horaria">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:CommandField ShowDeleteButton="True" />
                                                </Columns>
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="#AEC9FF" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
