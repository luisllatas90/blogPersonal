﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmcontenidocursovirtual.aspx.vb" Inherits="aulavirtual_frmcontenidocursovirtual"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Registro de temas</title>
    <script type="text/javascript" src="../../private/calendario.js"></script>
    <link rel="STYLESHEET"  href="../../private/estilo.css"/>
    <script type="text/javascript">
        function OcultarTabla()
        {
            if (form1.TxtNombre.value!="" && form1.TxtOrden.value!=""){
                document.all.form1.style.display="none"
                document.all.tblmensaje.style.display=""
            }
        }
        function ContarTextArea(campo)
        {
            var limite=3000
            var lblcontador=document.all.tdContador
            
            if (campo.value.length >limite){
                campo.value = campo.value.substring(0, limite);
                alert( 'Este campo sólo aceptar hasta '  + limite + ' caracteres');
                return false;
            }
            else{
                lblcontador.innerHTML="&nbsp;&nbsp;" + eval(limite-campo.value.length) + ' caracteres.'
            }
        }
        
        function validarnumero()
        {
            if (event.keyCode < 45 || event.keyCode > 57)
            {event.returnValue = false}
        }
     </script>
</head>
<body style="margin-top: 5px;background-color: #F2F2F2">
<form id="form1" runat="server">
<asp:Button ID="CmdGuardar" runat="server" CssClass="guardar_prp" Text="        Guardar" OnClientClick="OcultarTabla()"/>
<asp:Button ID="CmdCancelar" runat="server" CssClass="noconforme1" OnClientClick="javascript: window.close(); return false;" Text="        Cancelar" />
<br/><br/>
        <table style="width: 100%;" class="contornotabla" cellpadding="1" id="tblDatos">
            <tr>
                           <td style="width: 23%">&nbsp;Orden</td>
                            <td>
                                &nbsp;<asp:TextBox ID="TxtOrden" runat="server" Font-Size="9pt" MaxLength="2" Width="20px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtOrden"
                                    ErrorMessage="Especifique orden del tema.">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 23%">&nbsp;Título</td>
                            <td>
                                &nbsp;<asp:TextBox ID="TxtNombre" runat="server" Font-Size="9pt" Width="95%" MaxLength="255" TabIndex="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtNombre"
                                    ErrorMessage="Especifique Titulo del tema.">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>&nbsp;Fecha Inicio</td>
                            <td>
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
                        <tr>
                            <td>
                                &nbsp;Fecha Fin</td>
                            <td>
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
                                    ErrorMessage="Fecha Final requerida">*</asp:RequiredFieldValidator>
                                    </td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="2" align="right">&nbsp;Descripción del tema<br />
                                &nbsp;<asp:TextBox ID="TxtComentario" runat="server" Height="170px" TextMode="MultiLine"
                                    Width="98%" Font-Size="9pt" MaxLength="2000" TabIndex="2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" id="tdContador" class="rojo" align="right">&nbsp;</td>
                            <td align="right" class="azul">
                                <asp:Label id="LblPublicacion" runat="server" Text="¿Quién visualizará este tema?"></asp:Label>&nbsp;
                                <asp:DropDownList ID="DDLPermiso" runat="server">
                                    <asp:ListItem Value="1">Todos los participantes</asp:ListItem>
                                    <asp:ListItem Value="2">Algunos participantes</asp:ListItem>
                                    <asp:ListItem Value="3">S&#243;lo el Profesor</asp:ListItem>
                                </asp:DropDownList>
                             </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" />
                                <asp:Label ID="LblMensaje" runat="server" Font-Size="12pt" ForeColor="Red" Font-Bold="True"></asp:Label>&nbsp;</td>
                        </tr>
        </table>
    </form>
    <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none;height:100%;width:100%;"  class="contornotabla">
	    <tr>
	    <td style="background-color: #FEFFE1" align="center" class="usatTitulo" >
	    Procesando<br/>
	    Por favor espere un momento...<br/>
	    <img src="../../images/cargando.gif" width="209" height="20"/>
	    </td>
	    </tr>
    </table>
</body>
</html>
