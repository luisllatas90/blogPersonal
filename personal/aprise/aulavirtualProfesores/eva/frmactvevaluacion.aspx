<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmactvevaluacion.aspx.vb" Inherits="frmactvevaluacion" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resultados de Evaluación</title>
    <script language="javascript" src="../../../private/PopCalendar.js" type="text/javascript"></script>
    <link href="../../../private/estiloaulavirtual.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function OcultarTabla()
        {
            if (form1.txtdescripcion_aev.value!=""){
                document.all.form1.style.display="none"
                document.all.tblmensaje.style.display=""
            }
        }
     </script>
</head>
<body style="margin-top: 5px;background-color: #F2F2F2">
<form id="form1" runat="server">
<%Response.Write(ClsFunciones.CargaCalendario)%> 
<asp:Button ID="CmdGuardar" runat="server" CssClass="guardar" 
    Text="         Guardar" OnClientClick="OcultarTabla()"/>
<asp:Button ID="CmdCancelar" runat="server" CssClass="cerrar" 
    Text="         Cancelar" />
<br/><br/>
        <table style="width: 100%;" class="contornotabla" cellpadding="1" id="tblDatos">
                        <tr>
                            <td style="width: 23%">
                                <asp:Label ID="lblTitulo" runat="server" 
                                    Text="Denominación de Actividad a evaluar"></asp:Label>
                            </td>
                            <td>
                                &nbsp;<asp:TextBox ID="txtdescripcion_aev" runat="server" Font-Size="9pt" 
                                    Width="95%" MaxLength="255" TabIndex="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdescripcion_aev"
                                    ErrorMessage="Especifique la denominación de la actividad a evaluar.">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;Fecha Inicio</td>
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
                                <asp:TextBox ID="FechaIni" runat="server" Width="73px" Font-Size="9pt" 
                                    ReadOnly="True"></asp:TextBox>
                                <asp:Button ID="cmdInicio" runat="server" 
                    onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.FechaIni,'dd/mm/yyyy');return(false)" 
                    Text="..." CausesValidation="False" UseSubmitBehavior="False" />
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
                                <asp:TextBox ID="FechaFin" runat="server" Width="73px" Font-Size="9pt" 
                                    ReadOnly="True"></asp:TextBox>
                                <asp:Button ID="cmdFin" runat="server" 
                            onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.FechaFin,'dd/mm/yyyy');return(false)" 
                            Text="..." CausesValidation="False" UseSubmitBehavior="False" />
                
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="FechaFin"
                                    ErrorMessage="Fecha Final requerida">*</asp:RequiredFieldValidator></td>
                        </tr>
            <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Tipo de evaluación"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="dtmodo_aev" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="P">Parcial</asp:ListItem>
                                    <asp:ListItem Value="F">Final</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="dttipoval_aev" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="C">Cuantitativa</asp:ListItem>
                                    <asp:ListItem Value="L">Cualitativa</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;<asp:Label ID="Label4" runat="server" Text="Puntaje máximo"></asp:Label>
&nbsp;<asp:DropDownList ID="dtlimiteval_aev" runat="server">
                                </asp:DropDownList>
                                </td>
                        </tr>
            <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label2" runat="server" 
                                    Text="Especifique qué items se relacionan con este:" Visible="False"></asp:Label>
                            </td>
                        </tr>
            <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:CheckBoxList ID="chkcodigo_aev" runat="server" Visible="False">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
            <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label3" runat="server" 
                                    Text="¿Qué tipo de cálculo deseas realizar con los items relacionados?" 
                                    Visible="False"></asp:Label>
&nbsp;<asp:DropDownList ID="dtcodigo_tca" runat="server" Visible="False">
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
	    <td style="background-color: #FEFFE1" align="center" class="e1" >
	    Procesando<br/>
	    Por favor espere un momento...<br/>
	    <img src="../images/cargando.gif" width="209" height="20"/>
	    </td>
	    </tr>
    </table>
</body>
</html>

