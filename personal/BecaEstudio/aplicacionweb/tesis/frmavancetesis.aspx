<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmavancetesis.aspx.vb" Inherits="AvanceTesis" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Registro de Comentarios/ Avances/ Informes </title>
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
	<script type="text/javascript" language="javascript">
        function OcultarTabla()
        {
            if (document.all.FileArchivo!=undefined){
                document.all.form1.style.display="none"
                document.all.tblmensaje.style.display=""
            }
        }
     </script>
     <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin-top: 5px;background-color: #F2F2F2">
<form id="form1" runat="server">
<asp:Button ID="cmdGuardar" runat="server" CssClass="guardar_prp" Text="         Enviar" />
<asp:Button ID="cmdCancelar" runat="server" CssClass="noconforme1" OnClientClick="javascript: window.close(); return false;" Text="        Cancelar" />
<br/><br/>
<table style="width: 100%;" class="contornotabla" cellpadding="3" id="tblDatos">
            <tr>
                            <td width="30%">Fecha de Registro</td>
                            <td width="70%">
                            <asp:Label ID="lblFecha" runat="server"></asp:Label>
                            </td>
                        </tr>
            <tr>
                            <td width="30%">Tipo de Registro</td>
                            <td width="70%">
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
                            <td width="30%" colspan="2" style="width: 100%">
                               <asp:FileUpload ID="FileArchivo" runat="server" Font-Size="9pt" Width="95%" />
                               <asp:RegularExpressionValidator ID="ValidarTipoArchivo"
                                        runat="server" ControlToValidate="FileArchivo" ErrorMessage="Solo puede subir archivos con extension *.zip, *.rar, *.pdf, *.doc"
                                        SetFocusOnError="True" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.rar|.RAR|.zip|.ZIP|.doc|.DOC|.pdf|.PDF)$">*</asp:RegularExpressionValidator>
                              <asp:RequiredFieldValidator
                                            ID="ValidarSubir" runat="server" ControlToValidate="FileArchivo" ErrorMessage="Debe seleccionar un archivo para subir."
                                            SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
            <tr>
                            <td width="30%" colspan="2" style="width: 100%" align="right">
                                <asp:HyperLink ID="lnkSugerencias" runat="server" Font-Underline="True" 
                                    ForeColor="#CC0000" NavigateUrl="ayuda/reducirimagen.htm" Target="_blank">Haga 
                                clic aquí para ver más sugerencias</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" colspan="2">Asunto
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
                            <td width="100%" colspan="2">Comentarios<asp:RequiredFieldValidator
                                            ID="ValidarComentario" runat="server" 
                                    ControlToValidate="TxtComentario" ErrorMessage="Debe ingresar el comentario"
                                            SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            </td>
			</tr>
			<tr>
                            <td width="100%" colspan="2">
                                <asp:TextBox ID="TxtComentario" runat="server" Height="150px" TextMode="MultiLine"
                                    Width="98%" Font-Size="9pt" MaxLength="1000"></asp:TextBox></td>
                        </tr>
			<tr>
                            <td width="100%" colspan="2" align="right" id="lblcontador">
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" align="center" colspan="2">
                                <asp:Label ID="LblMensaje" runat="server" Font-Size="11pt" ForeColor="Red"></asp:Label>&nbsp;</td>
            </tr>
        </table>
	<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
    </form>
    <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none;height:100%;width:100%;"  class="contornotabla">
	    <tr>
	    <td style="background-color: #FEFFE1" align="center" class="usatTitulo" >
	    Procesando<br/>
	    Por favor espere un momento...<br/>
	    <img src="../../../images/cargando.gif" width="209" height="20"/>
	    </td>
	    </tr>
    </table>
</body>
</html>
