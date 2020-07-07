<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReporteConsultasHemeroteca.aspx.vb" Inherits="personal_academico_expediente_Biblioteca_ReporteConsultasHemeroteca" %>

<!--DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"-->

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <script src="../../../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    <link href="../private/estilo.css" type="text/css" rel="Stylesheet" />    
</head>
<body style="margin:0">
     <form id="form1" runat="server">
          <%  response.write(clsfunciones.cargacalendario) %>
     <div>
        <table style="width:100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table style="width:100%;" bgcolor="#F0F0F0">
                        <tr>
                            <td align="center" class="usatTitulousat" colspan="4">
                    <asp:Label ID="LblTitulo" runat="server" Text="REPORTE DE CONSULTAS - HEMEROTECA"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tipo de material</td>
                            <td>
                                <asp:DropDownList ID="CboTipoMaterial" runat="server" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:CheckBox ID="ChkFechas" runat="server" Text="Por fechas" 
                                    EnableViewState="False" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Tipo lector</td>
                            <td>
                                <asp:DropDownList ID="CboTipoLector" runat="server" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Fecha de Inicio:
                <asp:TextBox ID="TxtFechaIni" runat="server" Width="87px"></asp:TextBox>
                <input id="Button2" type="button"  class="cunia" 
                    onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFechaIni,'dd/mm/yyyy')" 
                    style="height: 22px" />&nbsp;
                            </td>
                            <td align="right">
                                <asp:Button ID="CmdConsultar" runat="server" CssClass="buscar" 
                                    Text="   Consultar" Width="85px" Height="20px" EnableViewState="False" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tipo de Préstamo</td>
                            <td>
                                <asp:DropDownList ID="CboTipoPrestamo" runat="server" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Fecha final:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                <asp:TextBox ID="TxtFechafin" runat="server" Width="87px"></asp:TextBox>
                <input id="Button3" type="button"  class="cunia" 
                    onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFechafin,'dd/mm/yyyy')" 
                    style="height: 22px" /></td>
                            <td align="right">
                                <asp:Button ID="CmdExportar" runat="server" Text="  Exportar" 
                    CssClass="Exportar" Width="85px" Height="20px" EnableViewState="False" />
                                      </td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="LblTotal" runat="server" ForeColor="#0000CC"></asp:Label>
&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="DgvDatos" runat="server" Width="100%" CellPadding="4" 
                        ForeColor="#333333" GridLines="None" EnableViewState="False">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            No se encontraron registros
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
