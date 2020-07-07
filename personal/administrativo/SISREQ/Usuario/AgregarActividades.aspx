<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AgregarActividades.aspx.vb" Inherits="AgregarActividades" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href ="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type ="text/css" href ="../private/estiloweb.css" />
    <title>Página sin título</title>
    <script src="../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    <script src="../../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
</head>
<body>
    <form id="frmActividades" runat="server">
    <%  response.write(clsfunciones.cargacalendario) %>
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr class="TituloReq" >
                <td style="font-weight: bold; text-transform: uppercase;" align="left" 
                    height="20" >
                    Registro de Actividades</td>
                <td style="font-weight: bold;" align="right">
                                            <asp:LinkButton ID="LnkVolver" runat="server" Font-Bold="True" 
                                                Font-Underline="False" ForeColor="White">««Regresar</asp:LinkButton>
                &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                                            &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="2">
                    <table width="95%">
                        <tr>
                            <td valign="top" width="100" align="left" style="font-weight: bold">
                                Descripción</td>
                            <td style="height: 15px" valign="top">
                                :</td>
                            <td style="height: 15px" align="left">
                                <asp:TextBox ID="TxtActividad" runat="server" Width="98%" Rows="3" 
                                    TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="TxtActividad" 
                                    ErrorMessage="El campo descripción no debe ser vacío" ValidationGroup="Agregar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 15px; font-weight: bold;" valign="top" align="left">
                                Responsable</td>
                            <td style="height: 15px" valign="top">
                                :</td>
                            <td>
                                <asp:RadioButtonList ID="RbtResponsable" runat="server">
                                </asp:RadioButtonList>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                    ControlToValidate="RbtResponsable" 
                                    ErrorMessage="Debe asignar un responsable para esta actividad" 
                                    Operator="GreaterThanEqual" Type="Integer" ValidationGroup="Agregar" 
                                    ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 15px; font-weight: bold;" valign="top" align="left">
                                Fecha Inicio</td>
                            <td style="height: 15px" valign="top">
                                :</td>
                            <td style="height: 15px" align="left">
                                <asp:TextBox ID="TxtFini" runat="server" Width="100px"></asp:TextBox>
                                <input id="Button1" type="button"  class="cunia" onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.frmActividades.TxtFini,'dd/mm/yyyy')" style="height: 22px" /><asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtFini" 
                                    ErrorMessage="Debe especificar una fecha de inicio" ValidationGroup="Agregar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 15px; font-weight: bold;"  valign="top" align="left">
                                Fecha Final</td>
                            <td style="height: 15px" valign="top">
                                :</td>
                            <td style="height: 15px" align="left">
                                <asp:TextBox ID="TxtFfin" runat="server" Width="100px"></asp:TextBox>
                                <input id="Button2" type="button"  class="cunia" 
                                    onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.frmActividades.TxtFfin,'dd/mm/yyyy')" 
                                    style="height: 22px" /><asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtFfin" 
                                    ErrorMessage="Debe especificar una fecha final" ValidationGroup="Agregar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 15px; font-weight: bold;" class="style1" valign="top" 
                                align="left">
                                Observación</td>
                            <td style="height: 15px" valign="top">
                                :</td>
                            <td style="height: 15px" align="left">
                                <asp:TextBox ID="TxtObservacion" runat="server" Rows="3" TextMode="MultiLine" 
                                    Width="98%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="height: 1px; background-color: #004182;" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="2">
                    <table width="95%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="height: 15px">
                                &nbsp;</td>
                            <td style="height: 15px">
                                &nbsp;</td>
                            <td style="height: 15px" align="right">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 15px">
                                &nbsp;</td>
                            <td style="height: 15px">
                                &nbsp;</td>
                            <td style="height: 15px" align="right">
                                <asp:ImageButton ID="ImgAdd" runat="server" BorderColor="#999999" ImageUrl="~/images/anadir.gif" 
                                    style="height: 17px" ToolTip="Agregar" ValidationGroup="Agregar" />
&nbsp;&nbsp;<asp:ImageButton ID="ImgActualizar" runat="server" ImageUrl="~/images/actualizar.jpg" />
&nbsp;
                                <asp:ImageButton ID="ImgQuit" runat="server" BorderColor="#999999" 
                                    BorderStyle="Solid" 
                                    ImageUrl="~/images/eliminar.gif" ToolTip="quitar" />
&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 15px">
                                &nbsp;</td>
                            <td style="height: 15px">
                                &nbsp;</td>
                            <td style="height: 15px" align="right">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="GvActividades" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="id_act,id_solequ" style="text-align: center; margin-right: 0px;" 
                                    Width="100%" GridLines="Horizontal">
                                    <Columns>
                                        <asp:BoundField DataField="id_act" HeaderText="id_act" InsertVisible="False" 
                                            ReadOnly="True" SortExpression="id_act" Visible="False" />
                                        <asp:BoundField DataField="descripcion_act" HeaderText="Actividad" 
                                            SortExpression="descripcion_act">
                                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="id_solequ" 
                                            SortExpression="id_solequ" Visible="False" >
                                            <HeaderStyle Width="0px" />
                                            <ItemStyle ForeColor="White" Width="0px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="persona" HeaderText="Responsable" ReadOnly="True" 
                                            SortExpression="persona" >
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fechaini_croa" DataFormatString="{0:dd/MM/yyyy}" 
                                            HeaderText="fecha de inicio" SortExpression="fechaini_croa" />
                                        <asp:BoundField DataField="fechafin_croa" DataFormatString="{0:dd/MM/yyyy}" 
                                            HeaderText="fecha final" SortExpression="fechafin_croa" />
                                        <asp:BoundField DataField="observacion_croa" HeaderText="Observación" 
                                            SortExpression="observacion_croa" >
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:CommandField SelectText="" ShowSelectButton="True" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="No se encontraron registros" 
                                            ForeColor="Red"></asp:Label>
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <HeaderStyle CssClass="titulocel" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="right">
                                            &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
    
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Agregar" />
    </form>
</body>
</html>
