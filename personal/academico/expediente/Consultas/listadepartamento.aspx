<%@ Page Language="VB" AutoEventWireup="false" CodeFile="listadepartamento.aspx.vb" Inherits="Consultas_listadepartamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link rel="STYLESHEET"  href="../private/estilo.css"/>
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
                            <td align="right" style="width:20%">
                                <asp:Label ID="Label3" runat="server" Text="Estado Planilla: "></asp:Label>
                            </td>
                            <td align="left" colspan="2" style="width:50%">
                                <asp:DropDownList ID="ddlEstadoPlla"  runat="server" Width="70%" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width:20%">
                                
                                <asp:Label ID="Label4" runat="server" Text="Tipo Personal: "></asp:Label>
                                
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlTipoPersonal" Width="70%" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width:20%">
                                <asp:Label ID="Label5" runat="server" Text="Area: "></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                 <asp:DropDownList ID="DDLDepAcad" runat="server"
                                    AutoPostBack="True" Width="70%">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3" >
                                &nbsp;Se encontraron
                                <asp:Label ID="LblCantidad" runat="server" Font-Bold="True"></asp:Label>
                                trabajadores.</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:DataList ID="DataList1" runat="server" DataSourceID="DocentesLista" RepeatColumns="6"
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
    
    </div>
        <asp:ObjectDataSource ID="DocentesLista" runat="server" SelectMethod="DocentesDeparAcad"
            TypeName="Personal">
            <SelectParameters>
                <asp:ControlParameter ControlID="DDLDepAcad" DefaultValue="0" Name="codigo_dac" PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="ddlTipoPersonal" DefaultValue="0" Name="Codigo_Tpe" PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="ddlEstadoPlla" DefaultValue="0" Name="estado_Per" PropertyName="SelectedValue" Type="Int32" />
                
            </SelectParameters>
        </asp:ObjectDataSource>
        &nbsp;
    </form>
</body>
</html>
