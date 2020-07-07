﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="trabajosenviados.aspx.vb" Inherits="trabajosenviados" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administración de tareas</title>
     <link rel="STYLESHEET"  href="../../private/estilo.css" />
     <script type="text/javascript" src="../../private/funciones.js"></script>
     <style fprolloverstyle>A:hover {color: #FF0000; text-decoration: underline;}</style>
</head>
<body>
    
    <form id="form1" runat="server">
    <table style="width:100%;" border="0" cellpadding="0" cellspacing="0" align="center">
        <tr>
          <td style="width:60%;" class="usattitulo">Envio de trabajos</td>
          <td style="width:40%;text-align:right">
          &nbsp;<asp:Button ID="cmdRegresar" runat="server" Text="Regresar" />
          &nbsp;<asp:Button ID="cmdEnviar" runat="server" Text="Enviar trabajo" 
                  UseSubmitBehavior="False" Visible="False" />
          </td>
        </tr>
        <tr>
          <td style="width:60%;height:50px;" valign="top">
          <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="10pt" 
                  Text="Label"></asp:Label>
          </td>
          <td style="width:40%;height:50px;text-align:right" valign="middle">
          Mostrar:<asp:DropDownList ID="dtTipo" runat="server" AutoPostBack="True">
                  <asp:ListItem Value="T">Todos</asp:ListItem>
                  <asp:ListItem Value="P">Por revisión del profesor</asp:ListItem>
                  <asp:ListItem Value="R">Revisados por el profesor</asp:ListItem>
              </asp:DropDownList>
          </td>
        </tr>

        <tr>
            <td style="width:100%;background-color:White" 
                colspan="2" valign="top">
                <asp:DataList ID="DataList1" runat="server" BackColor="White" 
                    BorderColor="#DEDFDE" CellPadding="4" ForeColor="Black" 
                    BorderStyle="None" GridLines="Horizontal" Width="100%">
                    <FooterStyle BackColor="#CCCC99" />
                    <AlternatingItemTemplate>
                        <table style="width:100%;" >
                            <tr>
                                <td style="width:15%">
                                    Fecha Envio</td>
                                <td style="width:55%">
                                    : <asp:Label ID="Label1" runat="server" Text='<%# eval("fechareg") %>'></asp:Label>
                                </td>
                                <td style="width:30%; text-align:right">
                                <asp:Label ID="Label4" runat="server" Text='<%# eval("estadorevision") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:15%">
                                    Registrado por</td>
                                <td style="width:85%" colspan="2">
                                    : <asp:Label ID="Label2" runat="server" Text='<%# eval("nombreusuario") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:15%; vertical-align:top">
                                    Comentarios</td>
                                <td style="width:85%;vertical-align:top" colspan="2">
                                    <asp:Label ID="Label3" runat="server" Text='<%# eval("obs") %>'></asp:Label></td>
                            </tr>
                            
                            <tr>
                                <td colspan="3" style="width:100%; text-align:right; ">
                                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="True" 
                                        ForeColor="#0000CC" NavigateUrl='<%# "http://www.usat.edu.pe/campusvirtual/archivoscv/" + eval("archivo") %>' 
                                        Target='<%# "_blank" %>' Text='<%# "Descargar archivo" %>' 
                                        Visible='<%# iif(IsDBNull(eval("archivo"))=true,false,true) %>'></asp:HyperLink>
                                </td>
                            </tr>                            
                            
                        </table>
                    </AlternatingItemTemplate>
                    <AlternatingItemStyle BackColor="White" BorderColor="#666666" 
                        BorderStyle="Solid" BorderWidth="1px" />
                    <ItemStyle BackColor="#F7F7DE" BorderColor="#666666" BorderStyle="Solid" 
                        BorderWidth="1px" />
                    <SelectedItemStyle BackColor="#CE5D5A" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <ItemTemplate>
                        <table style="width:100%;" >
                            <tr>
                                <td style="width:15%">
                                    Fecha Envio</td>
                                <td style="width:55%">
                                    : <asp:Label ID="Label1" runat="server" Text='<%# eval("fechareg") %>'></asp:Label>
                                </td>
                                <td style="width:30%;text-align:right">
                                <asp:Label ID="Label4" runat="server" Text='<%# eval("estadorevision") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:15%">
                                    Registrado por</td>
                                <td style="width:85%" colspan="2">
                                    : <asp:Label ID="Label2" runat="server" Text='<%# eval("nombreusuario") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:15%; vertical-align:top">
                                    Comentarios</td>
                                <td style="width:85%;vertical-align:top" colspan="2">
                                    &nbsp;<asp:Label ID="Label3" runat="server" Text='<%# eval("obs") %>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="3" style="width:100%; text-align:right; ">
                                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="True" 
                                        ForeColor="#0000CC" 
                                        NavigateUrl='<%# "http://www.usat.edu.pe/campusvirtual/archivoscv/" + eval("archivo") %>' 
                                        Target='<%# "_blank" %>' Text='<%# "Descargar archivo" %>' 
                                        Visible='<%# iif(IsDBNull(eval("archivo"))=true,false,true) %>'></asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
        <p>
            <asp:Label ID="lblMensaje" runat="server" CssClass="usatsugerencia"
                Font-Bold="True" Font-Size="10pt" ForeColor="Red" 
                Text="&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;No se han encontrado trabajos enviados" 
                Visible="False" Width="100%"></asp:Label>
        </p>
     </form>
</body>
</html>
