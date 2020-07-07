<%@ Page Language="VB" AutoEventWireup="false" CodeFile="vsttesisasesor.aspx.vb" Inherits="personal_academico_tesis_vsttesisasesor" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tesis por asesor</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    A:hover
    {
	    color: red;
	    text-decoration:underline;
    }
    A:Active
    {
    	color:Blue;
    	text-decoration:underline;
    }
    a:Link 
    {
    	color:Blue;
        text-decoration: underline;
        }
    a:Visited 
    {
    	color:Green;
        text-decoration: underline;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Tesis registradas por asesor</p>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="15%">
                <b>Dpto. Acad:</b></td>
            <td width="73%">
    <asp:DropDownList ID="dpDpto" runat="server" Font-Size="7pt">
    </asp:DropDownList>
                <asp:DropDownList ID="dpVista" runat="server">
                    <asp:ListItem Value="0">Vistas en miniatura</asp:ListItem>
                    <asp:ListItem Value="1">Detalles</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlCiclo" runat="server">
                </asp:DropDownList>
            </td>
            <td width="12%">
            &nbsp;<asp:ImageButton ID="cmdBuscar" runat="server" ImageAlign="Top" 
                    ImageUrl="~/images/menus/buscar_small12.gif" />
                <asp:ImageButton ID="cmdExportar" runat="server" ImageAlign="Top" 
                    ImageUrl="~/images/ext/xls.gif" Visible="False" />
            </td>
        </tr>
    </table>
    <center>
    <asp:DataList ID="DataList1" runat="server" RepeatColumns="3" 
        RepeatDirection="Horizontal" DataKeyField="codigo_per" Visible="False" 
                CellPadding="4" CellSpacing="3">
        <ItemTemplate>
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td align="center" rowspan="4" width="20%" valign="top">
                        <asp:Image ID="imgFoto" runat="server" Height="90px" 
                            ImageUrl='<%# eval("foto_per") %>' Width="80px" />
                    </td>
                    <td width="80%">
                        <asp:Label ID="lblAsesor" runat="server" ForeColor="Maroon" 
                            Text='<%# eval("asesor") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="80%">
                        <asp:Label ID="lblTipo" runat="server" Font-Size="9px" 
                            Text='<%# StrConv(eval("descripcion_tpe") + "-" + eval("descripcion_ded"),3) %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="80%" valign="top">
                        <asp:GridView ID="grdTesis" runat="server" AutoGenerateColumns="False" 
                            CellPadding="4" ForeColor="#333333">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" Font-Size="7pt" />
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="codigo_per,codigo_eti,codigo_Cac" 
                                    DataNavigateUrlFormatString="vsttesisasesoria.aspx?codigo_per={0}&amp;codigo_eti={1}&amp;codigo_cac={2}"
                                    DataTextField="nombre_eti" HeaderText="Título" >
                                    <ItemStyle Width="60%" Font-Size="7pt" ForeColor="Blue" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="totaltesis" HeaderText="Total">
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="asesorias" HeaderText="Asesorías">
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="7pt" 
                                ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td width="80%">
                        &nbsp;</td>
                </tr>
            </table>
        </ItemTemplate>
        <ItemStyle CssClass="contornotabla_azul" />
    </asp:DataList>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="3" ForeColor="#333333">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:BoundField DataField="asesor" HeaderText="Asesor">
                    <ItemStyle Font-Size="7pt" />
                </asp:BoundField>
                <asp:BoundField DataField="descripcion_ded" HeaderText="Tipo" />
                <asp:BoundField DataField="descripcion_tpe" HeaderText="Dedicación">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="nombre_eti" HeaderText="Etapa de Tesis">
                    <ItemStyle Font-Size="7pt" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="totaltesis" HeaderText="Tesis">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="asesorias" HeaderText="Asesorias">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="bloqueo" HeaderText="Estado" />
            </Columns>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </center>
</form>
</body>
</html>
