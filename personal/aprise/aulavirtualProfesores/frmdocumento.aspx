<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmdocumento.aspx.vb" Inherits="aulavirtual_frmdocumento" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <script src="../../private/calendario.js"></script>
    <link rel="STYLESHEET"  href="../../private/estilo.css"/>
    <script>
        function OcultarTabla()
        {
            if (document.form1.FileArchivo.value!="")
            {
                document.all.tblDatos.style.display="none"
                document.all.tblmensaje.style.display=""                
           }
        }
     </script>
</head>
<body>
    <form id="form1" runat="server">
	<!-- <h4 class="rojo">El archivo estará publicado hasta el 31 de Diciembre <%=year(now)+1%></h4> -->
        <table style="width: 100%" id="tblDatos">
            <tr>
                <td colspan="3" rowspan="3" style="font-size: 11pt; color: #330000; font-family: verdana; font-variant: normal; font-weight: bold;">
                    &nbsp;Datos del Documento</td>
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
            <tr>
                <td colspan="3" rowspan="1">
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 23%">
                                &nbsp;Nombre</td>
                            <td colspan="2">
                                &nbsp;<asp:TextBox ID="TxtNombre" runat="server" Font-Size="9pt" Width="435px" MaxLength="255"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtNombre"
                                    ErrorMessage="Especifique nombre de documento.">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;<asp:Label ID="LblUbicacion" runat="server" Text="Ubicacion de Archivo"></asp:Label></td>
                            <td colspan="2">
                                &nbsp;<asp:FileUpload ID="FileArchivo" runat="server" Font-Size="9pt" Width="435px" />
                                <asp:RequiredFieldValidator ID="ValidarSubir" runat="server" ControlToValidate="FileArchivo"
                                    ErrorMessage="Seleccione el documento a publicar">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr runat="server" id="trfechainicio">
                            <td>
                                &nbsp;Publicado desde </td>
                            <td colspan="2">
                                &nbsp;<asp:DropDownList ID="HoraIni" runat="server">
                                    <asp:ListItem>00</asp:ListItem>
                                    <asp:ListItem>01</asp:ListItem>
                                    <asp:ListItem>02</asp:ListItem>
                                    <asp:ListItem>03</asp:ListItem>
                                    <asp:ListItem>04</asp:ListItem>
                                    <asp:ListItem>05</asp:ListItem>
                                    <asp:ListItem>06</asp:ListItem>
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem>08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                    <asp:ListItem>17</asp:ListItem>
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>21</asp:ListItem>
                                    <asp:ListItem>22</asp:ListItem>
                                    <asp:ListItem>23</asp:ListItem>
                                </asp:DropDownList>
                                h.
                                <asp:DropDownList ID="MinIni" runat="server">
                                    <asp:ListItem>00</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>45</asp:ListItem>
                                </asp:DropDownList>
                                m.
                                <asp:TextBox ID="FechaIni" runat="server" Width="73px" Font-Size="9pt"></asp:TextBox><input type="button" class="cunia" onclick="MostrarCalendario('FechaIni')" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FechaIni"
                                    ErrorMessage="Fecha de Inicio requerida">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr runat="server" id="trfechafin">
                            <td>
                                &nbsp;Publicado hasta </td>
                            <td colspan="2">
                                &nbsp;<asp:DropDownList ID="HoraFin" runat="server">
                                    <asp:ListItem>00</asp:ListItem>
                                    <asp:ListItem>01</asp:ListItem>
                                    <asp:ListItem>02</asp:ListItem>
                                    <asp:ListItem>03</asp:ListItem>
                                    <asp:ListItem>04</asp:ListItem>
                                    <asp:ListItem>05</asp:ListItem>
                                    <asp:ListItem>06</asp:ListItem>
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem>08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                    <asp:ListItem>17</asp:ListItem>
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>21</asp:ListItem>
                                    <asp:ListItem>22</asp:ListItem>
                                    <asp:ListItem>23</asp:ListItem>
                                </asp:DropDownList>
                                h.
                                <asp:DropDownList ID="MinFin" runat="server">
                                    <asp:ListItem>00</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>45</asp:ListItem>
                                </asp:DropDownList>
                                m.
                                <asp:TextBox ID="FechaFin" runat="server" Width="73px" Font-Size="9pt"></asp:TextBox><input type="button" class="cunia" onclick="MostrarCalendario('FechaFin')" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="FechaFin"
                                    ErrorMessage="Fecha Final requerida">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;Comentario</td>
                            <td colspan="2">
                                &nbsp;<asp:TextBox ID="TxtComentario" runat="server" Height="75px" TextMode="MultiLine"
                                    Width="435px" Font-Size="9pt" MaxLength="255"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2" style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; height: 30px; background-color: lemonchiffon">
                                &nbsp; Permitir que&nbsp;
                                <asp:DropDownList ID="DDLPermiso" runat="server">
                                    <asp:ListItem Value="1">Todos los usuarios</asp:ListItem>
                                    <asp:ListItem Value="2">Algunos usuarios</asp:ListItem>
                                    <asp:ListItem Value="3">Usuario Actual</asp:ListItem>
                                </asp:DropDownList>
                                visualizen este recurso.</td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <asp:Label ID="LblMensaje" runat="server" Font-Size="10pt" ForeColor="Red" Font-Bold="True"></asp:Label>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="6" class="rojo">
                <h5>
                Descargue el software para convertir a los formatos compatibles (ZIP, PDF, RAR):
                <br>
                <a href="http://ufpr.dl.sourceforge.net/project/sevenzip/7-Zip/9.12/7z912.exe" target="_blank"/>[Descargue 7ZIP]</a>  
                <a href="http://www.primopdf.com/" target="_blank"/>[Descargue Primo PDF]</a>
                <a href="http://es.pdf24.org/download/pdf24-creator.exe" target="_blank"/>[Descargue PDF Converter]</a>
                </h5>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3" rowspan="1">
                    <asp:Button ID="CmdGuardar" runat="server" CssClass="guardar" Text="     Guardar"
                        Width="77px" OnClientClick="OcultarTabla()"/>&nbsp;
                    <asp:Button ID="CmdCancelar" runat="server" CssClass="salir" OnClientClick="javascript: window.close(); return false;"
                        Text="    Cancelar" Width="78px" /></td>
            </tr>
        </table>
    
        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
            ShowMessageBox="True" ShowSummary="False" />
        <asp:HiddenField ID="HidenArchivo" runat="server" />
    </form>
    <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="100%" height="100%" class="contornotabla">
	    <tr>
	    <td width="100%" align="center" class="usatTitulo" bgcolor="#FEFFE1">
	    Procesando<br>
	    Por favor espere un momento...<br>
	    <img border="0" src="../../images/cargando.gif" width="209" height="20">
	    </td>
	    </tr>
    </table>
</body>
</html>
