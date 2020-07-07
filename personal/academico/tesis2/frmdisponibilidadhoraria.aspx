<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmdisponibilidadhoraria.aspx.vb" Inherits="SysTesisInv_frmdisponibilidadhoraria" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" language="JavaScript" src="private/PopCalendar.js"></script>
    <script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <%Response.Write(ClsFunciones.CargaCalendario)%>
    <p class="usatTitulo">Registro de disponibilidad horaria<p/>
    
    <table width="80%" class="contornotabla">
        <tr>
            <td bgcolor="#6666FF" colspan="2" 
                style="color: #FFFFFF; font-weight: bold; font-size: 12px">
                Nueva disponibilidad horaria</td>
        </tr>
        <tr>
            <td>
                Fecha de Inicio</td>
            <td>
                <asp:TextBox ID="txtFechaInicio" runat="server"></asp:TextBox>
                </asp:TextBox>
                <asp:Button ID="Button2" runat="server" 
                    onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaInicio,'dd/mm/yyyy');return(false)" 
                    Text="..." CausesValidation="False" UseSubmitBehavior="False" />
            </td>
        </tr>
        <tr>
            <td>
                Fecha Fin</td>
            <td>
                <asp:TextBox ID="txtFechaFin" runat="server"></asp:TextBox>
                </asp:TextBox>
                <asp:Button ID="Button3" runat="server" 
                    onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaFin,'dd/mm/yyyy');return(false)" 
                    Text="..." CausesValidation="False" UseSubmitBehavior="False" />
            </td>
        </tr>
        <tr>
            <td>
                Día Inicio</td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem>LUNES</asp:ListItem>
                    <asp:ListItem>MARTES</asp:ListItem>
                    <asp:ListItem>MIERCOLES</asp:ListItem>
                    <asp:ListItem>JUEVES</asp:ListItem>
                    <asp:ListItem>VIERNES</asp:ListItem>
                    <asp:ListItem>SABADO</asp:ListItem>
                    <asp:ListItem>DOMINGO</asp:ListItem>
                </asp:DropDownList>
                    </td>
        </tr>
        <tr>
            <td>
                Día Fin</td>
            <td>
                <asp:DropDownList ID="DropDownList2" runat="server">
                    <asp:ListItem>LUNES</asp:ListItem>
                    <asp:ListItem>MARTES</asp:ListItem>
                    <asp:ListItem>MIERCOLES</asp:ListItem>
                    <asp:ListItem>JUEVES</asp:ListItem>
                    <asp:ListItem>VIERNES</asp:ListItem>
                    <asp:ListItem>SABADO</asp:ListItem>
                    <asp:ListItem>DOMINGO</asp:ListItem>
                </asp:DropDownList>
                    </td>
        </tr>
        <tr>
            <td>
                Hora Inicio</td>
            <td>
                <asp:DropDownList ID="HoraIni" runat="server">
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
                                m.</td>
        </tr>
        <tr>
            <td>
                Hora Fin</td>
            <td>
                <asp:DropDownList ID="HoraFin" runat="server">
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
                                m.</td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                                <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" />
                                &nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="Cerrar" 
                                    CausesValidation="False" onclientclick="window.close();return(false)" />
                            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <p class="usatTitulo">
        Calendario de actividades registradas</p>
<p style="text-align:center">
    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
        BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
        Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
        Width="70%">
        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
        <SelectorStyle BackColor="#CCCCCC" />
        <WeekendDayStyle BackColor="#FFFFCC" />
        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
        <OtherMonthDayStyle ForeColor="#808080" />
        <NextPrevStyle VerticalAlign="Bottom" />
        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
    </asp:Calendar>
    </p>
    </form>
    </body>
</html>
