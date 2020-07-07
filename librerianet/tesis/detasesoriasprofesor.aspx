<%@ Page Language="VB" AutoEventWireup="false" CodeFile="detasesoriasprofesor.aspx.vb" Inherits="detasesoriasprofesor" %>
<!--
'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mensaje</title>
    
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" cellpadding="4" cellspacing="0" class="contornotabla">
            <tr>
                <td style="width:5%">
                    <img alt="pub" src="../../images/img5.gif" 
                        style="width: 32px; height: 32px" align="middle" /></td>
                <td style="width:60%; color: #333333; font-size: 14px; font-weight: bold;">
                    Comentarios</td>
                <td style="width:40%" align="right">
                    <asp:Button ID="cmdRegresar" runat="server" CssClass="regresar2" 
                        Text="Regresar" ValidationGroup="cancelar" />
&nbsp;</td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width:100%;">
            <tr>
                <td style="width:68%; height:5%">
                    <asp:Label ID="lblmensajebloqueo" runat="server" Font-Bold="True" 
                        ForeColor="Red" 
                        Text="&lt;img align='top' src='../../images/cerrar.gif'&gt;&amp;nbsp;Su fecha límite para responder al comentario ha caducado." 
                        Visible="False"></asp:Label>
                </td>
                <td id="tab" style="width:15%; height:5%; text-align:center" valign="bottom">
                    <asp:LinkButton ID="lnkDetalle" runat="server" ForeColor="Blue" 
                        CssClass="pestanaresaltada" Height="20px" ValidationGroup="cancelar" 
                        Width="100%">Vista Detalle</asp:LinkButton>
                </td>
                <td class="bordeinf" style="width:2%; height:5%; text-align:center">
                    &nbsp;</td>
                <td id="tab" style="width:15%; height:5%; text-align:center" valign="bottom">
                    <asp:LinkButton ID="lnkLista" runat="server" ForeColor="Blue" 
                        CssClass="pestanabloqueada" Height="20px" ValidationGroup="cancelar" 
                        Width="100%">Vista Lista</asp:LinkButton>
                </td>
            </tr>
            <tr bgcolor="#EEEEEE">
                <td style="border-style: solid none solid solid; border-width: 1px; border-color: #808080; width:68%; height:5%" >
                    <asp:Button ID="cmdNuevo" runat="server" Text="      Nuevo Mensaje" CssClass="agregar3" 
                        Width="110px" />
                &nbsp;<input id="cmdImprimir" class="imprimir3" type="button" value="   Imprimir" onclick="self.window.print()" /></td>
                <td style="width:15%; height:5%; text-align:center" class="bordeinf">
                    &nbsp;</td>
                <td style="width:2%; height:5%; text-align:center" class="bordeinf">
                    &nbsp;</td>
                <td style="width:15%; height:5%; text-align:center" class="bordederinf">
                    &nbsp;</td>
            </tr>
            </table>
                <br />
                <asp:GridView ID="grdComentarios" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="Black" DataKeyNames="codigo_ATes,asesorado,asesor,ruta_Ates" 
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
            Width="100%" BackColor="White">
                    <FooterStyle BackColor="#CCCC99" />
                    <RowStyle BackColor="#F7F7DE" />
                    <EmptyDataRowStyle CssClass="usatSugerencia" Font-Bold="True" />
                    <Columns>
                        <asp:BoundField HeaderText="#" >
                            <ItemStyle VerticalAlign="Top" Font-Bold="False" Font-Size="10pt" 
                                HorizontalAlign="Center" Width="5%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Comentario">
                            <ItemTemplate>
                                <table width="100%">
                                    <tr>
                                        <td width="5%">
                                            <asp:Image ID="foto" runat="server" Height="54px" Width="54px" />
                                        </td>
                                        <td valign="top" width="95%">
                                            <asp:Label ID="lblDe" runat="server" Font-Bold="True" Font-Size="10pt" 
                                                ForeColor="#0066CC" Text='<%# StrConv(eval("autor"),3) %>'></asp:Label>
                                            <br />
                                            <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="10pt" 
                                                Text='<%# eval("titulo_ates") %>'></asp:Label>
                                            <br />
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# eval("icono") %>' 
                                                Visible='<%# iif(IsDBNull(eval("ruta_Ates"))=true,false,true) %>' />
                                                
                                            <!-- <asp:HyperLink ID="lnkRuta11" runat="server" 
                                                NavigateUrl='<%# cstr("../../archivoscv/tesis/") + eval("ruta_Ates") %>' 
                                                Target='<%# cstr("_blank") %>' 
                                                Text='<%# StrConv(eval("descripcion_TATes"),3) %>' 
                                                Visible='<%# iif(IsDBNull(eval("ruta_Ates"))=true,false,true) %>'></asp:HyperLink>
                                            -->
                                            <asp:HyperLink ID="lnkRuta" runat="server" 
                                                NavigateUrl='<%# cstr("../../archivoscv/tesis/") + eval("ruta_Ates") %>' 
                                                Target='<%# cstr("_blank") %>' 
                                                Text='<%# StrConv(eval("descripcion_TATes"),3) %>' 
                                                Visible='<%# iif(IsDBNull(eval("ruta_Ates"))=true,false,true) %>'></asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblContenido" runat="server" Text='<%# eval("obs_ates") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle Width="65%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="fechareg_ates" HeaderText="Fecha" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="25%" />
                        </asp:BoundField>
                        <asp:CommandField ShowDeleteButton="True">
                            <ControlStyle Font-Size="X-Small" Font-Underline="True" ForeColor="#0066FF" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="5%" />
                        </asp:CommandField>
                    </Columns>
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <EmptyDataTemplate>
                        &nbsp;&nbsp;&nbsp;&nbsp; No se encontraron comentarios registrados
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:GridView ID="grdLista" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="Black" DataKeyNames="codigo_ATes,asesorado,asesor" 
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
            Width="100%" BackColor="White" Visible="False">
                    <FooterStyle BackColor="#CCCC99" />
                    <RowStyle BackColor="#F7F7DE" />
                    <EmptyDataRowStyle CssClass="usatSugerencia" Font-Bold="True" />
                    <Columns>
                        <asp:BoundField HeaderText="#" >
                            <ItemStyle VerticalAlign="Top" Font-Bold="False" Font-Size="10pt" 
                                HorizontalAlign="Center" Width="5%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Comentario">
                            <ItemTemplate>
                                <asp:Label ID="lblDe0" runat="server" Font-Bold="True" Font-Size="10pt" 
                                                ForeColor="#0066CC" Text='<%# StrConv(eval("autor"),3) %>'></asp:Label>
                                            <br />
                                            <asp:Label ID="lblTitulo0" runat="server" Font-Bold="True" Font-Size="10pt" 
                                                Text='<%# eval("titulo_ates") %>'></asp:Label>
                                            <br />
                                            <asp:Image ID="Image2" runat="server" ImageUrl='<%# eval("icono") %>' 
                                                Visible='<%# iif(IsDBNull(eval("ruta_Ates"))=true,false,true) %>' />
                                            <!-- <asp:HyperLink ID="lnkRuta00" runat="server" 
                                                NavigateUrl='<%# cstr("../../archivoscv/tesis/") + eval("ruta_Ates") %>' 
                                                Target='<%# cstr("_blank") %>' 
                                                Text='<%# StrConv(eval("descripcion_TATes"),3) %>' 
                                                Visible='<%# iif(IsDBNull(eval("ruta_Ates"))=true,false,true) %>'></asp:HyperLink>
                                            -->
                                            <asp:HyperLink ID="lnkRuta0" runat="server" 
                                                NavigateUrl='<%# cstr("../../archivoscv/tesis/") + eval("ruta_Ates") %>' 
                                                Target='<%# cstr("_blank") %>' 
                                                Text='<%# StrConv(eval("descripcion_TATes"),3) %>' 
                                                Visible='<%# iif(IsDBNull(eval("ruta_Ates"))=true,false,true) %>'></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle Width="75%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="fechareg_ates" HeaderText="Fecha" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20%" />
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <EmptyDataTemplate>
                        &nbsp;&nbsp;&nbsp;&nbsp; No se encontraron comentarios registrados
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <br />
    
    <asp:Panel ID="Panel1" runat="server" BackColor="#F0F0F0" 
        CssClass="contornotabla" Visible="False" Width="100%">
    <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar_prp" Text="         Enviar" />
<asp:Button ID="cmdCancelar" runat="server" CssClass="noconforme1" 
            Text="        Cancelar" ValidationGroup="cancelar" />
<br/><br/>
<table style="width: 100%;" class="contornotabla" cellpadding="3" id="tblDatos">
            <tr>
                            <td width="20%">Fecha de Registro</td>
                            <td width="80%">
                            <asp:Label ID="lblFecha" runat="server"></asp:Label>
                            </td>
                        </tr>
            <tr>
                            <td width="20%">Tipo</td>
                            <td width="80%">
                            <asp:DropDownList ID="dpTipo" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                            </td>
                        </tr>
            <tr>
                            <td width="30%" colspan="2" style="width: 100%">
                                <asp:Label ID="LblUbicacion" runat="server" 
                                    Text="Ubicación del Archivo (5 megas máximo). Formato .ZIP, .RAR, .PDF, .DOC" 
                                    ForeColor="#0000CC"></asp:Label>
                            </tr>
            <tr>
                            <td colspan="2" style="width: 100%">
                               <asp:FileUpload ID="FileArchivo" runat="server" Font-Size="9pt" Width="95%" />
                               <asp:RegularExpressionValidator ID="ValidarTipoArchivo"
                                        runat="server" ControlToValidate="FileArchivo" ErrorMessage="Solo puede subir archivos con extension *.zip, *.rar, *.pdf, *.doc"
                                        SetFocusOnError="True" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.rar|.RAR|.zip|.ZIP|.doc|.DOC|.pdf|.PDF)$" Visible="False">*</asp:RegularExpressionValidator>
                              <asp:RequiredFieldValidator
                                            ID="ValidarSubir" runat="server" ControlToValidate="FileArchivo" ErrorMessage="Debe seleccionar un archivo para subir."
                                            SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" colspan="2">Asunto:
                                <asp:RequiredFieldValidator ID="ValidarTitulo" runat="server" 
                                    ControlToValidate="txtTitulo" 
                                    ErrorMessage="Debe ingresar el asunto del comentario" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            </td>
			</tr>
			<tr>
                            <td width="100%" colspan="2">
                                <asp:TextBox ID="txtTitulo" runat="server" CssClass="Cajas2" MaxLength="255" 
                                    Width="100%"></asp:TextBox>
                            </td>
                        </tr>
			<tr>
                <td colspan="2" width="100%">
                    Contenido<asp:RequiredFieldValidator ID="ValidarComentario" runat="server" 
                        ControlToValidate="TxtComentario" ErrorMessage="Debe ingresar el comentario" 
                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                </td>
            </tr>
			<tr>
                            <td width="100%" colspan="2">
                                <asp:TextBox ID="TxtComentario" runat="server" CssClass="Cajas2" 
                                    Font-Size="9pt" Height="150px" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" align="right" colspan="2" ID="lblcontador">
                                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" width="100%">
                    <asp:Label ID="LblMensaje" runat="server" Font-Size="11pt" ForeColor="Red"></asp:Label>
                    &nbsp;</td>
            </tr>
        </table>
	<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
    </asp:Panel>
    <asp:HiddenField ID="hdId" runat="server" />
    <asp:HiddenField ID="hdCodigo_tes" runat="server" Value="0" />
    </form>
</body>
</html>
