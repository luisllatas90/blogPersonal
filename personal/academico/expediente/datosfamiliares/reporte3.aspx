<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reporte3.aspx.vb" Inherits="datosfamiliares_reporte3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
        .style2
        {
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width:100%;">
        <tr>
            <td align="center" colspan="2" 
                style="font-family: Verdana; font-size: small; color: #800000; font-weight: bold;">
                Reporte de Personal Casado que no ha registrado sus datos familiares</td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td valign="top" width="50%">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" CaptionAlign="Left" 
                    CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" 
                    GridLines="None" HorizontalAlign="Center" PageSize="50" 
                    DataKeyNames="codigo_per">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" Font-Names="Verdana" Font-Size="10pt" 
                        ForeColor="#333333" HorizontalAlign="Left" />
                    <Columns>
                        <asp:BoundField DataField="codigo_per" HeaderText="codigo_per" InsertVisible="False" 
                            ReadOnly="True" SortExpression="codigo_per" Visible="False" />
                        <asp:BoundField DataField="apellidoPat_Per" HeaderText="Ap. Paterno" 
                            SortExpression="apellidoPat_Per" />
                        <asp:BoundField DataField="apellidoMat_Per" HeaderText="Ap. Materno" 
                            SortExpression="apellidoMat_Per" />
                        <asp:BoundField DataField="nombres_Per" HeaderText="Nombres" 
                            SortExpression="nombres_Per" />
                        <asp:BoundField DataField="correo" HeaderText="Correo" ReadOnly="True" 
                            SortExpression="correo" />
                    </Columns>
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </td>
            <td valign="top">
                <table style="width: 100%; font-family: Verdana; font-size: small;" width="95%">
                    <tr>
                        <td colspan="3">
                            Envío de correo:</td>
                    </tr>
                    <tr>
                        <td class="style2">
                            De:</td>
                        <td class="style2">
                        </td>
                        <td class="style2">
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" align="center" colspan="3">
                            <asp:Label ID="lblRemitente" runat="server"></asp:Label>
                            &lt;<asp:Label ID="lblRemitenteCorreo" runat="server"></asp:Label>
                            &gt;</td>
                    </tr>
                    <tr>
                        <td class="style1" colspan="3">
                            Para:</td>
                    </tr>
                    <tr>
                        <td class="style1" align="center" colspan="3">
                            <asp:Label ID="lbldestinatario" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Asunto:</td>
                        <td class="style1">
                            &nbsp;</td>
                        <td class="style1">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1" align="center" colspan="3">
                            <asp:TextBox ID="txtAsunto" runat="server" Width="372px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Mensaje:</td>
                        <td class="style1">
                            &nbsp;</td>
                        <td class="style1">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:TextBox ID="txtMensaje" runat="server" Height="215px" TextMode="MultiLine" 
                                Width="367px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red" 
                                Text="Se enviaron las notificaciones satisfactoriamente" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Button ID="cmdEnviar" runat="server" Text="Enviar Correo" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                    SelectCommand="FAM_NoActualizoDatosFamiliares" 
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="NO" Name="tipo" Type="String" />
                        <asp:Parameter DefaultValue="0" Name="codigo_per" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
