<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lstasesoriasprofesor.aspx.vb" Inherits="lstasesoriasprofesor" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista de publicaciones</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
<style type="text/css">
        a:Active {
            color:Blue;
        }
        a:Link 
        {
        	color:Blue;
            text-decoration: underline;
        }
        a:Visited {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" cellpadding="4" cellspacing="0" class="contornotabla">
            <tr>
                <td style="width:5%">
                    <img alt="pub" src="../../images/menus/respondercomentario.gif" 
                        style="width: 32px; height: 32px" /></td>
                <td style="width:80%" class="usatTitulo">
                    Registro de Revisiones</td>
                <td style="width:15%">
                <asp:Button ID="CmdCancelar" runat="server" CssClass="usatSalir" 
                    Text="    Regresar" Height="25px" Width="80px" visible="false" />
                </td>
            </tr>
        </table>
        <br />
    <asp:Button ID="cmdNuevo" runat="server" CssClass="usatNuevo" Height="25px" 
        Text="Nueva Revisión" Width="120px" UseSubmitBehavior="False" />
        <br /><br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" DataKeyNames="codigo_Ates,codigo_Tes" 
            Width="100%" EnableTheming="False">
            <Columns>
                <asp:BoundField HeaderText="#">
                    <ItemStyle Width="5%" />
                </asp:BoundField>
                <asp:BoundField DataField="fechareg_ates" HeaderText="Fecha Registro" >
                    <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:HyperLinkField DataNavigateUrlFields="codigo_ates,codigo_tes,estado_ates" 
                    DataNavigateUrlFormatString="detasesoriasprofesor.aspx?codigo_ates={0}&amp;codigo_tes={1}&amp;estado_ates={2}" 
                    DataTextField="titulo_ates" HeaderText="Mensaje">
                    <ItemStyle Width="40%" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="autor" HeaderText="Empezado por" >
                    <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="respuestas" HeaderText="Respuestas" >
                    <ItemStyle Width="5%" />
                </asp:BoundField>
                <asp:BoundField DataField="Leido" HeaderText="Leído" >
                    <ItemStyle Width="5%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Bloquear Rpta.">
                    <ItemTemplate>
                        <asp:Label ID="lblestado" runat="server" 
                            Text='<%# iif(eval("estado_ates")=1,"No","Sí") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("estado_ates") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" Font-Size="12pt" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </form>
</body>
</html>
