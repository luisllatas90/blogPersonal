<%@ Page Language="VB" AutoEventWireup="false" CodeFile="datosfamiliares.aspx.vb" Inherits="_Default" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
<link href="private/estilos.css" rel="stylesheet"  type="text/css" /> 
    <style type="text/css">
        .style1
        {
            height: 18px;
        }
        .style2
        {
            height: 18px;
            width: 16px;
        }
        .style3
        {
            width: 16px;
        }
        .style4
        {
            height: 18px;
            width: 910px;
        }
        .style5
        {
            width: 910px;
        }
        .style7
        {
            height: 35px;
        }
        .style8
        {
            height: 40px;
        }
        .style9
        {
            width: 100%;
        }
        .style10
        {
            width: 91px;
        }
        .style11
        {
            height: 34px;
        }
        .style12
        {
            height: 34px;
            width: 910px;
        }
    </style>
</head>
<body>
<br>
    <form id="frmDatos" runat="server">
    <table style="width:90%;" class="contornotabla" align="center" cellpadding="0" 
        cellspacing="0">
        <tr>
            <td colspan="3" class="style7" bgcolor="#EFF3FB">
                <SPAN class="e1">&nbsp;&nbsp; DATOS FAMILIARES </SPAN></td>
        </tr>
        <tr>
            <td colspan="3" bgcolor="#999999" height="1">
                </td>
        </tr>
        <tr>
            <td colspan="3" bgcolor="#FFFFCC" class="style8" valign="top">
                <table class="style9">
                    <tr>
                        <td class="style10" valign="top">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#990000" 
                                Text="Instrucciones:"></asp:Label>
                        </td>
                        <td>
                            Este módulo permite el registro de cónyuge e hijos del personal USAT y será 
                            utilizada para fines administrativos:<br />
                            . Para agregar un familiar a la lista deberá hacer clic en el botón
                            <asp:Label ID="Label2" runat="server" Font-Underline="True" ForeColor="Blue" 
                                Text="Nuevo Registro de Familiar"></asp:Label>
                            <br />
                            . Para modificar los datos de un familiar hacer clic en la imagen
                            <asp:Image ID="Image1" runat="server" 
                                ImageUrl="../../../../images/editar.gif" />
&nbsp;(Modificar Datos)</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" bgcolor="#A0A3A7" height="1">
                </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td class="style4" align="right">
                            &nbsp;</td>
            <td class="style1" width="16">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style11">
                </td>
            <td class="style12" align="right">
                            <asp:Button ID="cmdAceptar" runat="server" Text="Nuevo Registro de Familiar" 
                                CssClass="cmdprocesarXLS" Width="154px" />
            </td>
            <td class="style11">
                </td>
        </tr>
        <tr>
            <td class="style2">
                </td>
            <td class="style4" align="center">
                &nbsp;</td>
            <td class="style1">
                </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style4">
                <asp:GridView ID="dgvDatos" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField DataField="codigo_dhab" HeaderText="codigo_dhab" 
                            SortExpression="codigo_dhab" Visible="False">
                            <HeaderStyle Font-Size="12px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="apellidoPaterno_dhab" HeaderText="Apellido Paterno" 
                            SortExpression="apellidoPaterno_dhab">
                            <HeaderStyle Font-Size="12px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="apellidoMaterno_dhab" HeaderText="Apellido Materno" 
                            SortExpression="apellidoMaterno_dhab">
                            <HeaderStyle Font-Size="12px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombres_dhab" HeaderText="Nombres" 
                            SortExpression="nombres_dhab">
                            <HeaderStyle Font-Size="12px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre_vfam" HeaderText="Vínculo" 
                            SortExpression="nombre_vfam">
                            <HeaderStyle Font-Size="12px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Sexo" HeaderText="Sexo" ReadOnly="True" 
                            SortExpression="Sexo">
                            <HeaderStyle Font-Size="12px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EsUSAT" HeaderText="Es USAT" ReadOnly="True" 
                            SortExpression="EsUSAT">
                            <HeaderStyle Font-Size="12px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha Nacimiento" 
                            ReadOnly="True" SortExpression="FechaNacimiento">
                            <HeaderStyle Font-Size="12px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FechaMatrimonio" HeaderText="Fecha Matrimonio" 
                            ReadOnly="True" SortExpression="FechaMatrimonio">
                            <HeaderStyle Font-Size="12px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Estudios" HeaderText="Estudios" ReadOnly="True" 
                            SortExpression="Estudios">
                            <HeaderStyle Font-Size="12px" />
                        </asp:BoundField>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
            <td class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style5" align="left">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style5" align="left">
                <asp:Label ID="lblCantidad" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </form>
    </body>
</html>
