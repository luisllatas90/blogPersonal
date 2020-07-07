<%@ Page Language="VB" AutoEventWireup="false" CodeFile="calendario.aspx.vb" Inherits="librerianet_aulavirtual_calendario" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calendario de Actividades</title>
    <style type="text/css">

table {
	font-family: Trebuchet MS;
	font-size: 8pt;
}
TBODY {
	display: table-row-group;
}
tr {
	font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
	font-size: 8pt;
	color: #2F4F4F;
    }
        </style>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/PopCalendar.js"></script>
        
</head>
<body>
    <form id="form1" runat="server" >
    <%Response.Write(ClsFunciones.CargaCalendario)%>    
    <p class="usatTitulo">Calendario de actividades</p>
    
    <table style="border: 1px solid #808080; width:100%; background-color: #EEEEEE;">
        <tr>
            <td style="width:20%">
    <asp:Button ID="cmdAgregar" runat="server" CssClass="agregar3" Text="   Agregar" />
            </td>
            <td style="width:80%" align="right">
                <asp:Label ID="lbltotal" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
            </td>
            
        </tr>
    </table>
    <br />
                                    <table style="width:100%">
                                        <tr>
                                            <td style="width: 30%" valign="top">
                                    <asp:Calendar ID="Calendar1" runat="server" 
        BackColor="White" BorderColor="#6699FF" Font-Names="Times New Roman" 
        Font-Size="10pt" ForeColor="Black" Height="220px" 
        Width="100%" DayNameFormat="Shortest" NextPrevFormat="ShortMonth">
                                        <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                                        <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" 
                                            Font-Size="8pt" ForeColor="#333333" Width="1%" />
                                        <TodayDayStyle BackColor="#CCCC99" />
                                        <OtherMonthDayStyle ForeColor="#999999" />
                                        <DayStyle Width="14%" />
                                        <NextPrevStyle Font-Size="8pt" ForeColor="#003399" />
                                        <DayHeaderStyle Font-Bold="True" Font-Size="7pt" BackColor="#CCCCCC" 
                                            ForeColor="#333333" Height="10pt" />
                                        <TitleStyle BackColor="#D2E1FF" 
                                            Font-Bold="True" Font-Size="13pt" ForeColor="#003399" Height="14pt" />
    </asp:Calendar>
    
                                                <br />
                                                <asp:HiddenField ID="hdIdcursoVirtual" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdIdUsuario" runat="server" />
                                                <asp:HiddenField ID="hdIdAgenda" runat="server" />
                                                <asp:HiddenField ID="hdcodigo_tfu" runat="server" />
                                                <br />
                                                <asp:Label ID="lblmensaje" runat="server" CssClass="rojo" Font-Bold="True"></asp:Label>
                                                <br />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                    ShowMessageBox="True" ShowSummary="False" />
                                            </td>
                                            <td style="width: 70%" valign="top">
                                                <asp:DataList ID="DataList1" runat="server" BorderColor="#6699FF" 
                                                    BorderStyle="Solid" BorderWidth="1px" GridLines="Horizontal" Width="100%" 
                                                    DataKeyField="idagenda">
                                                    <HeaderTemplate>
                                                        Lista de actividades
                                                    </HeaderTemplate>
                                                    <HeaderStyle BackColor="#D2E1FF" Font-Bold="True" ForeColor="Black" 
                                                        Height="25px" />
                                                    <ItemTemplate>
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td rowspan="3" style="width: 3%" valign="top">
                                                                    <img alt="" src="../../images/menu3.gif" /></td>
                                                                <td style="width: 93%">
                                                                    <asp:Label ID="lblcategoria" runat="server" Font-Bold="True" 
                                                                        Text='<%# eval("nombrecategoria") %>'></asp:Label>
                                                                    :
                                                                    <asp:Label ID="lbltitulo" runat="server" Font-Bold="True" ForeColor="Maroon" 
                                                                        Text='<%# eval("tituloagenda") %>'></asp:Label>
                                                                    .
                                                                    <asp:Label ID="lbldescripcion" runat="server" Text='<%# eval("descripcion") %>'></asp:Label>
                                                                    . <b>Lugar</b>:
                                                                    <asp:Label ID="lbllugar" runat="server" Text='<%# eval("nombrearea") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 93%; font-weight: 500; font-style: italic;">
                                                                    Registrado por:
                                                                    <asp:Label ID="lblnombreusuario" runat="server" 
                                                                        Text='<%# eval("nombreusuario") %>'></asp:Label>
                                                                    &nbsp;| Desde:<asp:Label ID="lblfechainicio" runat="server" 
                                                                        Text='<%# eval("fechainicio") %>'></asp:Label>
                                                                    &nbsp; hasta&nbsp;
                                                                    <asp:Label ID="lblfechafin" runat="server" Text='<%# eval("fechafin") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 93%">
                                                                    &nbsp;<asp:LinkButton ID="lnkModificar" runat="server" CommandName="Editar" 
                                                                        Font-Bold="False" Font-Underline="True" ForeColor="#0000CC" 
                                                                        Visible="<%# iif(me.hdcodigo_tfu.value=1,true,false) %>">Modificar</asp:LinkButton>
                                                                    &nbsp;&nbsp;<asp:Label ID="lbldivision" runat="server" Text="|" 
                                                                        Visible="<%# iif(me.hdcodigo_tfu.value=1,true,false) %>"></asp:Label>
                                                                    &nbsp;&nbsp;<asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" 
                                                                        Font-Bold="False" Font-Underline="True" ForeColor="#0000CC" 
                                                                        onclientclick="return confirm('¿Esta seguro que desea eliminar esta actividad?')" 
                                                                        Visible="<%# iif(me.hdcodigo_tfu.value=1,true,false) %>">Eliminar</asp:LinkButton>
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <asp:Panel ID="fraNuevo" runat="server" Visible="False">
                                                    <table style="width:100%; border: 1px solid #6699FF" cellpadding="3" cellspacing="0">
                                                        <tr>
                                                            <td style="background-color: #D2E1FF; font-weight: bold; width: 100%; height: 25px;" 
                                                                colspan="2">
                                                                Registrar nueva actividad</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 15%">
                                                                Categoría</td>
                                                            <td style="width: 85%">
                                                                <asp:DropDownList ID="cboidcategoria" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 15%; ">
                                                                Tíulo</td>
                                                            <td style="width: 85%">
                                                                <asp:TextBox ID="txttituloagenda" runat="server" CssClass="cajas" 
                                                                        Text='<%# eval("tituloagenda") %>' MaxLength="255" Width="95%"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                                    ControlToValidate="txttituloagenda" 
                                                                    ErrorMessage="Ingrese el título de la actividad">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 15%">
                                                                Descripción</td>
                                                            <td style="width: 85%">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                                                    ControlToValidate="txtdescripcion" 
                                                                    ErrorMessage="Ingrese la descripción de la actividad.">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="width: 100%">
                                                                <asp:TextBox ID="txtdescripcion" runat="server" CssClass="cajas" Rows="4" 
                                                                        Text='<%# eval("descripcion") %>' TextMode="MultiLine" 
                                                                    MaxLength="1000" Width="95%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 15%">
                                                                Lugar</td>
                                                            <td style="width: 85%">
                                                                <asp:TextBox ID="txtlugar" runat="server" CssClass="cajas" 
                                                                        Text='<%# eval("lugar") %>' MaxLength="100" Width="80%"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                                                    ControlToValidate="txtlugar" 
                                                                    ErrorMessage="Ingrese el lugar dónde se realizará la actividad.">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;Inicio</td>
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
                                                                <asp:TextBox ID="txtFechaInicio" runat="server" BackColor="#CCCCCC" 
                                                                    ForeColor="Navy" MaxLength="12" style="text-align: right" Width="100px"></asp:TextBox>
                                                                <asp:Button ID="cmdInicio" runat="server" CausesValidation="False" 
                                                                    onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaInicio,'dd/mm/yyyy');return(false)" 
                                                                    Text="..." UseSubmitBehavior="False" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                                    ControlToValidate="txtFechaInicio" 
                                                                    ErrorMessage="Debe especificar la fecha de inicio">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;Fin</td>
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
                                                                <asp:TextBox ID="txtFechaFin" runat="server" BackColor="#CCCCCC" 
                                                                    ForeColor="Navy" MaxLength="12" style="text-align: right" Width="100px"></asp:TextBox>
                                                                <asp:Button ID="cmdFin" runat="server" CausesValidation="False" 
                                                                    onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaFin,'dd/mm/yyyy');return(false)" 
                                                                    Text="..." UseSubmitBehavior="False" />
                                                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                                    ControlToValidate="txtFechaFin" 
                                                                    ErrorMessage="Debe especificar la fecha de término">*</asp:RequiredFieldValidator>
                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                                    ControlToCompare="txtFechaFin" ControlToValidate="txtFechaInicio" 
                                                                    ErrorMessage="Fecha de Termino menor o igual a fecha de inicio." 
                                                                    Operator="LessThanEqual" Type="Date">*</asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="2" style="width: 100%">
                                                                <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar2" Text="Guardar" />
                                                                &nbsp;
                                                                <asp:Button ID="cmdCancelar" runat="server" CssClass="regresar2" 
                                                                    Text="Cancelar" ValidationGroup="salir" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
    </table>
    </form>
</body>
</html>
