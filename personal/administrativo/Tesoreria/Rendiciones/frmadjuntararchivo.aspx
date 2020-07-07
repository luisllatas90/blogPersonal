<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmadjuntararchivo.aspx.vb" Inherits="frmadjuntararchivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link  rel ="stylesheet" href="estilo.css"/>
    <title>Página sin título</title>
<%
    Dim codigo_dren2 As Integer
    codigo_dren2 = Me.Request.QueryString("codigo_dren")
 %>
    <script language="javascript">
    
        function mostraradjuntar()
            {
                window.open('frmsubirarchivo.aspx?codigo_dren=<%=codigo_dren2%>','frmsubirarchivo','toolbar=no, width=600, height=200');
            }
    </script>
     
</head>
<body bgcolor="white">
    <form id="form1" runat="server">    
        <table style="width: 100%">
            <tr  class ="usatCeldaTitulo">
                <td  align="left" style="height: 26px; width: 8%;">                   &nbsp;<asp:Label ID="lblmensaje" runat="server" ForeColor="White" Width="50%" Font-Names="Courier New" Font-Size="10pt" Font-Bold="False" Height="32px"></asp:Label></td>
                <td align=right style="height: 26px; width: 4%;" >
                    <input id="cmdadjunta" type="button" value="Adjuntar"  onclick="mostraradjuntar()" style="width: 168px; background-color: lemonchiffon; display :<%=Mostrarfinalizar%>"  /></td>
            </tr>
        </table>
        <table style="width: 100%; height: 20%;" bgcolor ="white" >
            <tr>
                <td colspan="3" style="height: 50px" align=left >
                    <asp:GridView ID="lstinformacion" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        Font-Names="Courier New" Font-Size="9pt" Height="1px" ShowFooter="True" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="codigo_ddr" HeaderText="ID">
                                <ItemStyle  BackColor="White"/>
                                <HeaderStyle CssClass="usatCeldaTitulo" Width="10%" Font-Names="Courier New" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_ddr" HeaderText="Tipo">
                            <ItemStyle  BackColor="White"/>
                             <HeaderStyle CssClass="usatCeldaTitulo" Width="10%"/>
                             </asp:BoundField>
                            
                            <asp:BoundField DataField="icono_ddr" HeaderText="icono">
                            <ItemStyle  BackColor="White"/>
                             <HeaderStyle CssClass="usatCeldaTitulo" Width="10%"/>
                             </asp:BoundField>
                            <asp:BoundField HeaderText="Tama&#241;o">
                            <ItemStyle BackColor="White" />
                            <HeaderStyle CssClass="usatCeldaTitulo" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_ddr" HeaderText="Descripci&#243;n">
                                <ItemStyle BackColor="White" />
                                <HeaderStyle CssClass="usatCeldaTitulo" Width="53%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Fecha reg.">
                            <ItemStyle  BackColor="White"/>
                             <HeaderStyle CssClass="usatCeldaTitulo" Width="10%"/>
                             </asp:BoundField>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/eliminar.gif" DeleteText=""
                                InsertText="" NewText="" SelectImageUrl="~/eliminar.gif" ShowDeleteButton="True" />
                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/download.gif" ShowSelectButton="True" />
                        </Columns>
                        <RowStyle BorderStyle="Solid" />
                        <HeaderStyle Font-Bold="False" Font-Names="Courier New" Font-Size="Larger" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="height: 34px" colspan="2">
                    <asp:HiddenField ID="hfinformacion" runat="server" />
                    &nbsp;
                    </td>
                <td style="width: 4px; height: 34px">
                </td>
            </tr>
        </table>
    
    
    
    </form>
</body>
</html>
