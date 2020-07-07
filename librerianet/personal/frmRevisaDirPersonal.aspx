<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRevisaDirPersonal.aspx.vb" Inherits="librerianet_personal_frmRevisaDirPersonal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <script src="../../private/funciones.js" type="text/javascript"></script>
  
    <title>Página sin título</title>
    <style type="text/css">



        .style5
        {
            height: 30px;
        }
        .style4
        {
            width: 123px;
            height: 22px;
        }
        .style6
        {
            height: 39px;
        }
        .style2
        {
            width: 602px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server" style="font-family: verdana; font-size: 11px">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td style="font-weight: bold" class="style6">
                    Seleccione un trabajador:
                    <asp:DropDownList ID="ddlPersonal" runat="server" AutoPostBack="True" 
                        Height="22px" Width="402px">
                    </asp:DropDownList>
                </td>
                <td align="right" style="font-weight: bold" class="style6">
                    <asp:Button ID="cmdActivarBiblioteca" runat="server" 
                        Text="Activar Biblioteca" />
                    <asp:Button ID="cmdActivarCCSalud" runat="server" Text="Act. CC Salud" />
                    <asp:Button ID="cmdActivarCIS" runat="server" Text="Act. CIS" />
                    <asp:Button ID="cmdActivar" runat="server" Text="Activar" />
                </td>
            </tr>
            <tr>
                <td style="font-weight: bold">
                    <asp:TextBox ID="txtId" runat="server" Visible="False"></asp:TextBox>
                </td>
                <td align="right" style="font-weight: bold">
                                <asp:Button ID="cmdBorrar" runat="server" Text="Borrar Horario" />
                    &nbsp;
                    <asp:Button ID="btnEnviar" runat="server" Text="Finalizar y Enviar" />
                </td>
            </tr>
            <tr>
                <td style="font-weight: bold">
                    <asp:Label ID="lblObservacion" runat="server" Font-Size="Large" 
                        ForeColor="#FF3300"></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="OBS" Width="150px" />
                </td>
                <td align="right" style="font-weight: bold">
                                Obs:
                                <asp:TextBox ID="txtObservacion" runat="server" Width="251px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtObservacion" 
                                    ErrorMessage="Ingrese la observación correspondiente al horario" 
                                    ValidationGroup="OBS">*</asp:RequiredFieldValidator>
                                <asp:Button ID="cmdObservar" runat="server" Text="Observar" 
                                    ValidationGroup="OBS" />
                                &nbsp;
                    </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width: 100%;">
                        <tr>
                            <td class="style5" style="font-weight: bold" colspan="2">
                                <hr />
                            </td>
                            <td>
                                &nbsp;</tr>
                        <tr>
                            <td class="style5" style="font-size: small">
                                            Vigencia del Horario:<asp:DropDownList ID="cboSemana" runat="server" 
                                                AutoPostBack="True" Height="20px" Width="100px">
                                                <asp:ListItem Value="0">Semestre</asp:ListItem>
                                                <asp:ListItem Value="1">Semana 1</asp:ListItem>
                                                <asp:ListItem Value="2">Semana 2</asp:ListItem>
                                                <asp:ListItem Value="3">Semana 3</asp:ListItem>
                                                <asp:ListItem Value="4">Semana 4</asp:ListItem>
                                                <asp:ListItem Value="5">Semana 5</asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <asp:Label ID="lblFechas" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                            <td style="font-weight: bold">
                                Registro de horario <td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table style="width:100%;">
                                    <tr>
                                        <td valign="top" style="font-size: small">
                                Total Horas Semanales:                                 <asp:Label ID="lblHorasSemanales" runat="server" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="font-size: small" align="left">
                                Total Horas Mensuales:                                 <asp:Label ID="lblHorasMensuales" 
                                                runat="server" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:GridView ID="gvVistaHorario" runat="server" BorderStyle="Solid" 
                                                CellPadding="1" ForeColor="#333333" Font-Size="XX-Small">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="border: 1px solid #000000; font-size: x-small">
                                            <table bgcolor="White" style="width: 90%;" align="center">
                                                <tr>
                                                    <td class="style4">
                                                        Leyenda:</td>
                                                    <td bgcolor="Orange" class="style4" style="color: #000000">
                                                        Administrativo Institucional</td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="Green" class="style4">
                                                        Docencia</td>
                                                    <td bgcolor="Blue" class="style4" style="color: #FFFFFF">
                                                        Práctica Externa</td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="Gray" class="style1" style="color: #FFFFFF">
                                                        Asesoría de Tesis</td>
                                                    <td bgcolor="Yellow" class="style4">
                                                        Investigación</td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="Violet" class="style4">
                                                        Apoyo Administrativo en Escuela/Facultad</td>
                                                    <td bgcolor="Brown" class="style4" style="color: #FFFFFF">
                                                        Tutoría</td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="DarkTurquoise" class="style4" style="color: #000000">
                                                        Gestión Académica</td>
                                                    <td bgcolor="Lime" class="style4" style="color: #000000">
                                                        Horas asistenciales CIS/Clinica Usat</td>
                                                </tr>
                                                </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                    <table style="border: 1px solid #000000; width:100%; background-color: #FFFFCC;">
                        <tr>
                            <td class="style1" style="font-weight: bold" colspan="2">
                                +
                    Datos Generales<asp:Label ID="lblMensaje" runat="server" Font-Bold="True" 
                        Font-Size="Large" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td rowspan="5">
                                <asp:Image ID="imgFoto" runat="server" Height="122px" Width="113px" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" style="font-weight: bold">
                                Nombre</td>
                            <td class="style2">
                                <asp:Label ID="lblNombre" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" style="font-weight: bold">
                                Centro Costos</td>
                            <td class="style2">
                                <asp:Label ID="lblCeco" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" style="font-weight: bold">
                                Dedicación</td>
                            <td class="style2" style="font-weight: bold">
                                <asp:Label ID="lblDedicacion" runat="server" Text="Label" Font-Bold="False"></asp:Label>
                                &nbsp; &nbsp; Tipo
                                <asp:Label ID="lblTipo" runat="server" Text="Label" Font-Bold="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" style="font-weight: bold">
                                Fecha ingreso</td>
                            <td class="style2" style="font-weight: bold">
                                <asp:Label ID="lblFechaIngreso" runat="server" Text="Label" Font-Bold="False"></asp:Label>
                            &nbsp; Nro. Horas Semanales&nbsp;&nbsp;
                                <asp:TextBox ID="lblHoras" runat="server" Width="53px"></asp:TextBox>
                                &nbsp;</td>
                        </tr>
                    </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                            <td style="font-weight: bold">
                                +
                                Registro de horario:
                                <asp:Label ID="Label7" runat="server" Font-Bold="False" ForeColor="Red" 
                                    
                                    
                                    Text="[Puede elegir uno de los cuatro horarios predefinidos o registrar un horario personalizado]"></asp:Label>
                                                </tr>
                                                <tr>
                            <td style="border: 1px solid #0000FF; font-weight: bold; background-color: #DDEEFF;">
                                - Horarios predefinidos<br />
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" ForeColor="Red" 
                                    
                                    Text="[Haga clic sobre el botón del horario que desea asumir para el presente ciclo (Administrativo institucional)]" 
                                    ToolTip="El tipo de hora que se considerará será el: Administrativo Institucional, para definir otro tipo de hora utilice el registro de Horario personalizado."></asp:Label>
                                <br />
                                <table style="width: 100%; font-weight: normal;">
                                    <tr>
                                        <td>
                                            &nbsp;
                                            <asp:Label ID="Label4" runat="server" ForeColor="#339933" 
                                                Text="- Horario 1: Lunes - Viernes de 08:00 - 13:00 | 13:45 - 16:45"></asp:Label>
                                                
                                        </td>
                                        <td>
                                           <asp:Button ID="cmdHorario1" runat="server" Text="Horario 1" ForeColor="#339933" 
                                                style="height: 26px" /></td>
                                        <td>

                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;<asp:Label ID="Label5" runat="server" ForeColor="Blue" 
                                                Text="- Horario 2: Lunes - Viernes de 08:00 - 13:45 | 14:30 - 16:45"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Button ID="cmdHorario2" runat="server" Text="Horario 2" ForeColor="Blue" /></td>

                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;<asp:Label ID="Label6" runat="server" ForeColor="#CC00CC" 
                                                Text="- Horario 3: Lunes - Viernes de 08:00 - 14:00 | 14:45 - 16:45"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Button ID="cmdHorario3" runat="server" Text="Horario 3" 
                                                ForeColor="#CC00CC" /></td>

                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;<asp:Label ID="Label8" runat="server" ForeColor="#663300" 
                                                Text="- Horario 4: Lunes - Viernes de 08:00 - 14:15 | 15:00 - 16:45"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Button ID="cmdHorario4" runat="server" Text="Horario 4" 
                                                ForeColor="#663300" /></td>

                                    </tr>

                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>

                                </table>
                                                </tr>
                                                <tr>
                            <td style="font-weight: bold">
                                &nbsp;</tr>
                                                <tr>
                            <td style="border: 1px solid #0000FF; font-weight: bold; background-color: #DDEEFF;">
                                - Horario personalizado<br />
                                <asp:Label ID="Label3" runat="server" Font-Bold="False" ForeColor="Red" 
                                    
                                    Text="[Especifique la información correspondiente  al horario y haga clic en Añadir]"></asp:Label>
                                <br />
                                            <table style="width:100%;">
                                                <tr>
                                                    <td width="25%">
                                                        Día</td>
                                                    <td width="25%">
                                                        <asp:DropDownList ID="ddlDia" runat="server">
                                                            <asp:ListItem Value="LU">Lunes</asp:ListItem>
                                                            <asp:ListItem Value="MA">Martes</asp:ListItem>
                                                            <asp:ListItem Value="MI">Miercoles</asp:ListItem>
                                                            <asp:ListItem Value="JU">Jueves</asp:ListItem>
                                                            <asp:ListItem Value="VI">Viernes</asp:ListItem>
                                                            <asp:ListItem Value="SA">Sábado</asp:ListItem>
                                                            <asp:ListItem Value="DO">Domingo</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="25%">
                                                        Tipo</td>
                                                    <td width="25%">
                                                        <asp:DropDownList ID="ddlTipo" runat="server" AutoPostBack="True">
                                                            <asp:ListItem Value="D">Docencia</asp:ListItem>
                                                            <asp:ListItem Value="P">Práctica Externa</asp:ListItem>
<asp:ListItem Value="T">Asesoría de Tesis</asp:ListItem>
                                                            <asp:ListItem Value="I">Investigación</asp:ListItem>
                                                            <asp:ListItem Value="A">Administrativo Institucional</asp:ListItem>
                                                            <asp:ListItem Value="E">Apoyo Administrativo en Escuela</asp:ListItem>
                                                            <asp:ListItem Value="F">Apoyo Administrativo en Facultad</asp:ListItem>
                                                            <asp:ListItem Value="G">Gestión Académica</asp:ListItem>
                                                            <asp:ListItem Value="H">Horas Asistenciales</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Inicio</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlHoraInicio" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        Fin</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlHoraFin" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblEscuela" runat="server" Text="Escuela"></asp:Label>
                                                        <asp:Label ID="lblFacultad" runat="server" Text="Facultad" Visible="False"></asp:Label>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:DropDownList ID="ddlEscuela" runat="server" Width="100%">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlFacultad" runat="server" Width="100%" Visible="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text="Encargo" 
                                                            ToolTip="Encargo administrativo"></asp:Label>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtEncEscuela" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Resolución</td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtResEscuela" runat="server"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnAceptar" runat="server" Text="Añadir" />
                                                    </td>
                                                </tr>
                                                </table>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="CmdReporteHorasTesis" runat="server" CausesValidation="False" 
                                              Text="Hrs. Asesoria Tesis" UseSubmitBehavior="False" />
                                            <asp:GridView ID="gvEditHorario" runat="server" CellPadding="4" 
                                                ForeColor="#333333" GridLines="None" 
                                                Caption="Distribución de carga Horaria">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:CommandField ShowDeleteButton="True" />
                                                </Columns>
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="#AEC9FF" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                    <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvListaCambios" runat="server" CellPadding="4" 
                                                ForeColor="#333333" GridLines="None" 
                                                Caption="Histórico de envíos de horarios">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
