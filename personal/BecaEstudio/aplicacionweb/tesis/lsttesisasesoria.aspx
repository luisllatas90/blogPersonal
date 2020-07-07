<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lsttesisasesoria.aspx.vb" Inherits="lsttesisasesoria" %>

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
<body style="margin:10px, 10px, 10px, 10px; background-color:#F0F0F0">
    <form id="form1" runat="server">
        <table style="width: 100%;" cellpadding="3" cellspacing="0">
            <tr>
                <td style="height: 5%;" class="usatTituloPagina">
                    Tesis asesoradas</td>
            </tr>
            <tr id="trLista">
                <td style="width: 100%;height: 95%;" valign="top">
                <table style="width: 100%; height:100%" cellpadding="2" cellspacing="0">
                    <tr>
                        <td align="left" style="border-top: black 1px solid; height: 15px;" 
                            valign="middle">
                            <b>Etapa de tesis:</b>
                            <asp:DropDownList ID="dpFase" runat="server" Font-Size="9px" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:DropDownList ID="dpEstado" runat="server" Font-Size="9px" Visible="False">
                            </asp:DropDownList>
                            &nbsp;<asp:Button ID="cmdBuscar" runat="server" CssClass="buscar1" 
                                Text="     Buscar" Visible="False" />
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
                <asp:BoundField DataField="fechaFin_tes" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Fecha Fin"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Revisión">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" 
                            NavigateUrl='<%# Eval("codigo_tes", "../../../librerianet/tesis/lstasesoriasprofesor.aspx?codigo_tes={0}&id=" & request.querystring("id")) %>' 
                            Target="_self" Text='Ingresar' 
                            Visible='<%# iif(eval("bloquearavance_tes")=true,false,true) %>'></asp:HyperLink>
                        <asp:Label ID="lblmensaje" runat="server" Font-Bold="True" ForeColor="Red" 
                            Text="[Bloqueado]" 
                            Visible='<%# eval("bloquearavance_tes") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                </asp:TemplateField>
            </Columns>
             <EmptyDataTemplate>
                <strong style="width: 100%; color: red; text-align: center">
                    <br />
                    <br />
                    No se registraron tesis registradas en esta etapa.</strong>
            </EmptyDataTemplate>
               <HeaderStyle CssClass="etabla" />
            </asp:GridView>
                              
    </form>
        <p>
            <b>Bloqueado</b>: Indica que la Dirección de Escuela ha bloqueado el registro de 
            asesoría. Posiblemente por la matrícula en el Seminario de Tesis 
            correspondiente.</p>
</body>
</html>

