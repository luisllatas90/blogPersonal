<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Planificar.aspx.vb" Inherits="Equipo_Planificar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script src="../private/calendario.js" language="javascript" type="text/javascript"></script>
    <script src="../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    <link href ="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href ="../private/estiloweb.css" rel="stylesheet" type="text/css" />
    <title>Planificar una solicitud</title>

</head>
<body>
    <%  response.write(clsfunciones.cargacalendario) %>
    <form id="frmasignar" runat="server">
    <div style="text-align: center">
        <table width="100%">
            <tr>
                <td class="TituloReq" colspan="3" rowspan="1">
                                <asp:Label ID="LblTitulo" runat="server" Text="Asignar tiempos"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" rowspan="1" style="width: 95%; text-align: center; height: 218px;">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="TABLE1">
                                    <tr>
                                        <td align="left" style="height: 6px" colspan="4"><asp:FormView ID="FormView1" 
                                                runat="server" DataKeyNames="id_sol" DataSourceID="ConsultarDatosdeunaSolicitud"
                                                DefaultMode="Edit" Height="307px" Width="100%">
                                            <EditItemTemplate>
                                                <table width="100%">
                                                        <tr>
                                                            <td style="height: 26px" valign="top">
                                            Solicitud</td>
                                                            <td style="height: 26px; font-size: 8pt; text-transform: uppercase;" 
                                                                valign="top">
                                                                :</td>
                                                            <td style="height: 26px; font-size: 8pt; text-transform: uppercase;">
                                                                <asp:TextBox ID="descripcion_solTextBox" runat="server" ReadOnly="True" Text='<%# Bind("descripcion_sol") %>'
                                                                    Width="425px" BorderColor="White" BorderStyle="None" BorderWidth="0px" 
                                                                    Rows="3" TextMode="MultiLine"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                            Prioridad</td>
                                                            <td style="font-size: 8pt; text-transform: uppercase; height: 26px">
                                                                :</td>
                                                            <td style="font-size: 8pt; text-transform: uppercase; height: 26px">
                                                                <asp:TextBox ID="prioridadTextBox" runat="server" ReadOnly="True" Text='<%# Bind("prioridad") %>'
                                                                    Width="150px" BorderColor="White" BorderStyle="None" BorderWidth="0px"></asp:TextBox>
                                                                <asp:Label ID="id_solLabel1" runat="server" Text='<%# Eval("id_sol") %>' Visible="False"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                            Tipo de solicitud</td>
                                                            <td style="font-size: 8pt; text-transform: uppercase; height: 26px">
                                                                :</td>
                                                            <td style="font-size: 8pt; text-transform: uppercase; height: 26px">
                                                                <asp:TextBox ID="descripcion_tsolTextBox" runat="server" ReadOnly="True" Text='<%# Bind("descripcion_tsol") %>'
                                                                    Width="400px" BorderColor="White" BorderStyle="None" BorderWidth="0px"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                            Área</td>
                                                            <td style="font-size: 8pt; text-transform: uppercase; height: 26px">
                                                                :</td>
                                                            <td style="font-size: 8pt; text-transform: uppercase; height: 26px">
                                                                <asp:TextBox ID="descripcion_ccoTextBox" runat="server" ReadOnly="True" Text='<%# Bind("descripcion_cco") %>'
                                                                    Width="400px" BorderColor="White" BorderStyle="None" BorderWidth="0px"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Fecha solicitada</td>
                                                            <td>
                                                                :</td>
                                                            <td>
                                                                <asp:TextBox ID="fecha_solTextBox" runat="server" ReadOnly="True" 
                                                                    Text='<%# Bind("fecha_sol", "{0:d}") %>' Width="84px" BorderColor="White" 
                                                                    BorderStyle="None" BorderWidth="0px"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Fecha de inicio</td>
                                                            <td>
                                                                :</td>
                                                            <td>
                                <asp:TextBox ID="FormView1_TxtFechaIni" runat="server" OnKeyDown="return false;"  Width="84px" Text='<%# Bind("fechaini_croa", "{0:d}") %>'   ></asp:TextBox><input id="Button1"
                                    class="cunia" onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.frmasignar.FormView1_FormView1_TxtFechaIni,'dd/mm/yyyy')" style="height: 22px" type="button"/>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FormView1_TxtFechaIni"
                                                ErrorMessage="Fecha de Inicio Requerida" ValidationGroup="Guardar">*</asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr style="color: #2f4f4f">
                                                            <td>
                                                                Fecha final</td>
                                                            <td>
                                                                :</td>
                                                            <td>
                                                                <asp:TextBox ID="FormView1_TxtFechaFin" runat="server" OnKeyDown="return false;" Width="84px" AutoPostBack="True" Text='<%# Bind("fechafin_croa", "{0:d}") %>'></asp:TextBox><input id="Button2"
                                    class="cunia" onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.frmasignar.FormView1_FormView1_TxtFechaFin,'dd/mm/yyyy')" style="height: 22px" type="button" />
                                                                <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="FormView1_TxtFechaFin" ErrorMessage="Fecha de Termino Requerida" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="FormView1_TxtFechaFin"
                                                ControlToValidate="FormView1_TxtFechaIni" ErrorMessage="Fecha de Inicio debe ser menor a Fecha de Término"
                                                Operator="LessThanEqual" Type="Date" ValidationGroup="Guardar">*</asp:CompareValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td height="55" valign="top">
                                            Observación</td>
                                                            <td valign="top">
                                                                :</td>
                                                            <td valign="top">
                                                                <asp:TextBox ID="TextBox1" runat="server" Height="45px" Width="425px" 
                                                                    ValidationGroup="Guardar" Text='<%# Bind("observacion_croa", "{0}") %>' 
                                                                    TextMode="MultiLine"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="background-color: #004182; height: 1px;" align="right">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3" height="40" valign="middle">
                                                                &nbsp;&nbsp;<asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                                                                    CssClass="guardar" Text="Guardar" ValidationGroup="Guardar" Width="85px" Height="25px"  />
                                                                &nbsp;
                                                                <asp:Button ID="CmdCancelar" runat="server" CssClass="cancelar" 
                                                                    OnClientClick="javascript: window.close(); return false;" Text="Cancelar" 
                                                                    Width="85px" Height="25px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    descripcion_sol:
                                                    <asp:TextBox ID="descripcion_solTextBox" runat="server" Text='<%# Bind("descripcion_sol") %>'>
                                                    </asp:TextBox><br />
                                                    prioridad:
                                                    <asp:TextBox ID="prioridadTextBox" runat="server" Text='<%# Bind("prioridad") %>'>
                                                    </asp:TextBox><br />
                                                    descripcion_tsol:
                                                    <asp:TextBox ID="descripcion_tsolTextBox" runat="server" Text='<%# Bind("descripcion_tsol") %>'>
                                                    </asp:TextBox><br />
                                                    descripcion_cco:
                                                    <asp:TextBox ID="descripcion_ccoTextBox" runat="server" Text='<%# Bind("descripcion_cco") %>'>
                                                    </asp:TextBox><br />
                                                    descripcion_apl:
                                                    <asp:TextBox ID="descripcion_aplTextBox" runat="server" Text='<%# Bind("descripcion_apl") %>'>
                                                    </asp:TextBox><br />
                                                    fecha_sol:
                                                    <asp:TextBox ID="fecha_solTextBox" runat="server" Text='<%# Bind("fecha_sol") %>'>
                                                    </asp:TextBox><br />
                                                    observacion_sol:
                                                    <asp:TextBox ID="observacion_solTextBox" runat="server" Text='<%# Bind("observacion_sol") %>'>
                                                    </asp:TextBox><br />
                                                    <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                        Text="Insertar">
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                        Text="Cancelar">
                                                    </asp:LinkButton>
                                                    <br />
                                                </InsertItemTemplate>
                                                <ItemTemplate>
                                                    id_sol:
                                                    <asp:Label ID="id_solLabel" runat="server" Text='<%# Eval("id_sol") %>'></asp:Label><br />
                                                    descripcion_sol:
                                                    <asp:Label ID="descripcion_solLabel" runat="server" Text='<%# Bind("descripcion_sol") %>'></asp:Label><br />
                                                    prioridad:
                                                    <asp:Label ID="prioridadLabel" runat="server" Text='<%# Bind("prioridad") %>'></asp:Label><br />
                                                    descripcion_tsol:
                                                    <asp:Label ID="descripcion_tsolLabel" runat="server" Text='<%# Bind("descripcion_tsol") %>'></asp:Label><br />
                                                    descripcion_cco:
                                                    <asp:Label ID="descripcion_ccoLabel" runat="server" Text='<%# Bind("descripcion_cco") %>'></asp:Label><br />
                                                    descripcion_apl:
                                                    <asp:Label ID="descripcion_aplLabel" runat="server" Text='<%# Bind("descripcion_apl") %>'></asp:Label><br />
                                                    fecha_sol:
                                                    <asp:Label ID="fecha_solLabel" runat="server" Text='<%# Bind("fecha_sol") %>'></asp:Label><br />
                                                    observacion_sol:
                                                    <asp:Label ID="observacion_solLabel" runat="server" Text='<%# Bind("observacion_sol") %>'></asp:Label><br />
                                                </ItemTemplate>
                                            </asp:FormView>
                                            <br />
                                            <asp:SqlDataSource ID="ConsultarDatosdeunaSolicitud" runat="server" ConnectionString="<%$ ConnectionStrings:CnxBDUSAT %>"
                                                SelectCommand="paReq_ConsultarPorSolicitud" 
                                                SelectCommandType="StoredProcedure" UpdateCommand="paReq_InsertarCronograma" 
                                                UpdateCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:QueryStringParameter Name="id_sol" QueryStringField="id_act"
                                                        Type="Int32" />
                                                    <asp:SessionParameter DefaultValue="s" Name="tipo" SessionField="tipo" 
                                                        Type="String" />
                                                </SelectParameters>
                                                <UpdateParameters>
                                                    <asp:Parameter Name="fechaini_croa" Type="DateTime" />
                                                    <asp:Parameter Name="fechafin_croa" Type="DateTime" />
                                                    <asp:Parameter Name="observacion_croa" Type="String" />
                                                    <asp:Parameter Name="id_sol" Type="Int32" />
                                                    <asp:Parameter Name="tipo" Type="String" />
                                                </UpdateParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                </table>
                    &nbsp;
                </td>
            </tr>
        </table>
  
    </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
        &nbsp;
    </form>
</body>
</html>
