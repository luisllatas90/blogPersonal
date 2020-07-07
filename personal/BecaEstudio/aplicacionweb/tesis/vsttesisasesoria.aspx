<%@ Page Language="VB" AutoEventWireup="false" CodeFile="vsttesisasesoria.aspx.vb" Inherits="lsttesisasesoria" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Tesis para asesoría</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>

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
<body style="margin:10px, 10px, 10px, 10px; ">
    <form id="form1" runat="server">
        <table style="width: 100%;" cellpadding="3" cellspacing="0">
            <tr>
                <td style="height: 5%;" class="usatTitulo">
                    Tesis asesoradas</td>
                <td style="height: 5%;" class="usatTituloPagina" align="right">
                <asp:Button ID="CmdCancelar" runat="server" CssClass="usatSalir" 
                    Text="    Regresar" Height="25px" Width="80px" />
                </td>
            </tr>
            <tr id="trLista">
                <td style="width: 100%;height: 95%;" valign="top" colspan="2">
                <table style="width: 100%; height:100%" cellpadding="2" cellspacing="0">
                    <tr>
                        <td align="left" valign="middle">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle">
                        <table width="100%" cellpadding="3" cellspacing="0" class="contornotabla">
                            <tr>
                                <td align="center" rowspan="5" width="13%">
                                    <asp:Image ID="foto" runat="server" Height="104px" 
                                       Width="90px" />
                                    <br />
                                </td>
                                <td width="40%">
                                    <asp:Label ID="lblasesor" runat="server" Font-Bold="True" ForeColor="#CC6600"></asp:Label>
                                </td>
                                <td width="1%" align="right" rowspan="5" valign="top">
                                    &nbsp;</td>
                                <td width="47%" align="right" rowspan="5" valign="top">
                                    <img id="img1" align="right" border="0" height="130" 
                                        src="images/etiqasesoria.jpg" width="22" />
                                    <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%">
                                    <asp:Label ID="lblcategoria" runat="server" 
                                        ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%">
                                    <asp:Label ID="lblemail" runat="server"  
                                        ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="40%">
                                    &nbsp;</td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle">
                            <b>Etapa de tesis:</b>
                            <asp:DropDownList ID="dpFase" runat="server" Font-Size="9px" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:DropDownList ID="dpEstado" runat="server" Font-Size="9px" Visible="False">
                            </asp:DropDownList>
                            &nbsp;<asp:ImageButton ID="cmdBuscar" runat="server" 
                                ImageUrl="~/images/menus/buscar_small12.gif" Visible="False" />
                        </td>
                    </tr>
                    </table>
                   </td>
            </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_Tes" 
            Width="100%" CellPadding="3" CssClass="contornotabla">
            <Columns>
                <asp:BoundField HeaderText="N&#176;">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                </asp:BoundField>
                <asp:HyperLinkField DataNavigateUrlFields="codigo_tes" 
                    DataNavigateUrlFormatString="detalletesis.aspx?regresar=S&amp;codigo_tes={0}" 
                    HeaderText="Título" Target="_self" DataTextField="titulo_tes">
                    <ItemStyle Width="45%" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="autorprincipal" HeaderText="Autor Principal">
                    <ItemStyle Width="25%" />
                </asp:BoundField>
                 <asp:BoundField DataField="fechaReg_tes" DataFormatString="{0:yyyy}" HeaderText="Fecha Reg."
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="fechaFin_tes" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Fecha Fin"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="10%" />
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
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
             <EmptyDataTemplate>
                <strong style="width: 100%; color: red; text-align: center">
                    <br />
                    <br />
                    No se encontraron tesis registradas en esta etapa.</strong>
            </EmptyDataTemplate>
               <HeaderStyle CssClass="etabla" />
            </asp:GridView>
                              
    </form>
</body>
</html>

