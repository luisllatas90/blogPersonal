<%@ Page Language="VB" AutoEventWireup="false" CodeFile="listadocentes.aspx.vb" Inherits="Consultas_listadocentes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
    <style type="text/css">

        .style1
        {
            height: 6px;
        }
        .style2
        {
            height: 14px;
        }
    </style>
</head>
<body alink="buttontext" link="buttontext" vlink="buttontext">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center" colspan="3">
                    <table>
                        <tr>
                            <td align="center" colspan="3" style="font-size: 8pt; color: navy; font-family: verdana">
                                Seleccione Área :
                                <asp:DropDownList ID="DDLCentroCotos" runat="server" AutoPostBack="True" Style="font-size: 8pt;
                                    color: navy; font-family: verdana; font-variant: normal" Width="411px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                &nbsp;<asp:Label ID="LblDepartamento" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="Medium" ForeColor="Navy"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:DataList ID="DataList2" runat="server" DataSourceID="DocentesLista" RepeatColumns="6"
                                    RepeatDirection="Horizontal" CellPadding="3" CellSpacing="6">
                                    <ItemTemplate>
                                        <table onmouseover="pintarcelda(this)" onmouseout="despintarcelda(this)" style="border-right: darkred 2px solid; border-top: darkred 2px solid; border-left: darkred 2px solid;
                                            width: 110px; border-bottom: darkred 2px solid; height: 160px" 
                                            cellspacing="0">
                                            <tr>
                                                <td style="height: 6px" valign="top">
                                                    &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ruta") %>'
                                                        Text='<%# Eval("Docente") %>' Font-Names="Arial" Font-Size="6pt" ForeColor="Navy"></asp:HyperLink></td>
                                            </tr>
                                            <tr>
                                                <td valign="bottom">
                                                    <asp:Image ID="Image1" runat="server" Height="65px" ImageUrl='<%# Eval("foto_per") %>'
                                                        Width="50px" BorderColor="Black" BorderStyle="Solid" ToolTip='<%# Eval("Docente") %>' /></td>
                                            </tr>
                                            <tr>
                                                <td class="style1" valign="bottom">
                                                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="7pt" 
                                                        ForeColor='<%# iif(Eval("descripcion_Est") = "ACTIVO",System.Drawing.Color.Blue ,System.Drawing.Color.Red ) %>' 
                                                        Text='<%# Eval("descripcion_Est") %>' Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2" valign="top">
                                                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="7pt" 
                                                        Text='<%# eval("descripcion_ded") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        &nbsp;
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
        <asp:ObjectDataSource ID="DocentesLista" runat="server" SelectMethod="DocentesDeparAcad"
            TypeName="Personal">
            <SelectParameters>
                <asp:ControlParameter ControlID="DDLCentroCotos" Name="codigo_dac" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
