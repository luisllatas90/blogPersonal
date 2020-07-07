<%@ Page Language="VB" AutoEventWireup="false" CodeFile="vsttesisfacultad.aspx.vb" Inherits="vsttesisfacultad" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
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
    	color:Maroon;
        text-decoration: underline;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Tesis registradas en la diversas Faculades de la Universidad</p>
    <center>
    <p style="text-align:center; font-weight: bold;">
    Etapa: 
        <asp:DropDownList ID="dpFase" runat="server" AutoPostBack="True">
        </asp:DropDownList>
    </p>
    <asp:DataList ID="DataList1" runat="server" CellPadding="3" RepeatColumns="2" 
            DataKeyField="codigo_fac" CellSpacing="4">
        <ItemTemplate>
            <table style="width:100%;" cellpadding="4" cellspacing="0">
                <tr>
                    <td align="center" bgcolor="#E4E4E4">
                        <asp:Label ID="lblFacultad" runat="server" Font-Bold="True" 
                            Text='<%# eval("nombre_fac") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            ShowHeader="False" Width="100%" GridLines="Horizontal">
                            <Columns>
                                <asp:TemplateField HeaderText="Lista">
                                    <ItemTemplate>
                                        <img alt="" src="../../../images/menus/Okey_s.gif" height="10px" width="10px" />
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" />
                                </asp:TemplateField>
                                <asp:HyperLinkField DataNavigateUrlFields="codigo_cpf" 
                                    DataNavigateUrlFormatString="vsttesisescuela.aspx?modo=F&amp;codigo_cpf={0}" 
                                    DataTextField="nombre_cpf" HeaderText="Escuela" >
                                    <ItemStyle Width="87%" Font-Size="7pt" ForeColor="Blue" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="tesisregistradas" HeaderText="Tesis">
                                    <ItemStyle ForeColor="Blue" Width="10%" HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
        <ItemStyle VerticalAlign="Top" CssClass="contornotabla_azul" />
    </asp:DataList>
    </center>
    </form>
</body>
</html>
