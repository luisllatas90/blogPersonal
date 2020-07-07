<%@ Page Language="VB" AutoEventWireup="false" CodeFile="vsttesisescuela.aspx.vb" Inherits="personal_academico_tesis_vsttesisescuela" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tesis por Carrera Profesional</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <style type="text/css">
    A:hover
    {
	    color: red;
	    text-decoration:underline;
    }
    a:Link
    {
        color: #0000FF;
        text-decoration: underline;
    }
     .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3 ; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px; 
       }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Tesis registradas por Carrera Profesional</p>
    <center>
        <table width="100%">
            <tr>
                <td align="center">
                <asp:Label ID="lblEstado" runat="server" Text="Carrera Profesional:"></asp:Label>
&nbsp;<asp:DropDownList ID="dpEscuela" runat="server" AutoPostBack="True" 
            Font-Size="7pt">
        </asp:DropDownList>
                            <asp:DropDownList ID="dpFase" runat="server" 
            Font-Size="9px" AutoPostBack="True">
                            </asp:DropDownList>
                &nbsp;<asp:Button ID="btnExportar" runat="server" Text="Exportar"  CssClass="btn"
                         />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Font-Names="Tahoma" 
                        Font-Size="8pt" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </center>    
                               <asp:GridView ID="GridView1" runat="server"
                                AutoGenerateColumns="False" DataKeyNames="codigo_Tes" 
            Width="100%" AllowSorting="True">
                                <Columns>
                                    <asp:BoundField HeaderText="N&#176;">
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:BoundField>
                                    <asp:HyperLinkField DataNavigateUrlFields="codigo_tes" 
                                    DataNavigateUrlFormatString="detalletesis.aspx?regresar=S&pagina=vsttesisescuela.aspx&codigo_tes={0}" 
                                    DataTextField="titulo_tes" HeaderText="Título" >
                                    <ItemStyle Width="45%" Font-Size="7pt" ForeColor="Blue" />
                                </asp:HyperLinkField>
                                    <asp:BoundField DataField="autorprincipal" HeaderText="Autor">
                                        <ItemStyle Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="asesor" HeaderText="Asesor">
                                        <ItemStyle HorizontalAlign="Center" Width="20%" Font-Size="9px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Asesorías">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" 
                                                NavigateUrl='<%# Eval("codigo_tes", "vstavancetesis.aspx?codigo_tes={0}") %>' 
                                                Text='<%# Cstr("Ver") %>' 
                                                Visible='<%# iif(eval("avances")=0,false,true) %>'></asp:HyperLink>
                                            <asp:Label ID="Label1" runat="server" Text="Ninguna" 
                                                Visible='<%# iif(eval("avances")=0,true,false) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="bloqueo" HeaderText="Bloqueado">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                </Columns>
                                <RowStyle BorderStyle="None" Height="17px" />
                                <EmptyDataTemplate>
                                    <strong style="width: 100%; color: red; text-align: center">
                                        <br />
                                        <br />
                                        No se han registrado tesis para la carrera profesional</strong>
                                </EmptyDataTemplate>
                                   <HeaderStyle BackColor="#628BD7" ForeColor="White" Height="25px" />
                            </asp:GridView>
    </form>
</body>
</html>
