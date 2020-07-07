<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmVistadeHorario.aspx.vb" Inherits="librerianet_personal_frmVistadeHorario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <style type="text/css">


        .style4
        {
            width: 123px;
            height: 22px;
        }
        </style>
</head>
<body>
    <form id="form2" runat="server" style="font-family: Verdana; font-size: 11px">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td class="style7" valign="top">
                                <table style="width:100%;">
                                    <tr>
                                        <td valign="top" style="font-size: small" align="left">
                                Total Horas Semanales:                                 <asp:Label ID="lblHorasSemanales" runat="server" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="font-size: small">
                                            <asp:GridView ID="gvVistaHorario" runat="server" BorderStyle="Solid" 
                                                CellPadding="1" ForeColor="#333333" Font-Size="XX-Small">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    </table>
                            </td>
                            <td valign="top">
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvEditHorario" runat="server" CellPadding="4" 
                                                ForeColor="#333333" GridLines="None" 
                                                Caption="Distribución de carga Horaria">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="#AEC9FF" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                    <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvListaCambios" runat="server" CellPadding="4" 
                                                ForeColor="#333333" GridLines="None" 
                                                Caption="Histórico de envíos de horarios">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                    <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table bgcolor="White" style="width: 50%;" align="left">
                                                <tr>
                                                    <td class="style4">
                                                        Leyenda:</td>
                                                    <td bgcolor="Orange" class="style4" style="color: #000000">
                                                        Administrativo Institucional</td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="Green" class="style4">
                                                        Docencia</td>
                                                    <td bgcolor="Blue" class="style4" style="color: #FFFFFF">
                                                        Práctica Externa</td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="Gray" class="style1" style="color: #FFFFFF">
                                                        Asesoría de Tesis</td>
                                                    <td bgcolor="Yellow" class="style4">
                                                        Investigación</td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="Violet" class="style4">
                                                        Apoyo Administrativo en Escuela</td>
                                                    <td bgcolor="Brown" class="style4" style="color: #FFFFFF">
                                                        Apoyo Administrativo Facultad</td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="DarkTurquoise" class="style4" style="color: #000000">
                                                        Gestión Académica</td>
                                                    <td bgcolor="Lime" class="style4" style="color: #000000">
                                                        Horas asistenciales</td>
                                                </tr>
                                                </table>
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
