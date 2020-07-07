<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RPT_SeguimientoPropuestas.aspx.vb"
    Inherits="administrativo_propuestas2_proponente_RPT_SeguimientoPropuestas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../estilo.css" rel="stylesheet" type="text/css" />  
        
    <script type="text/javascript" src="../funciones.js"> </script>

    <script src="../../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        function HabilitarBoton(tipo, fila) {
            document.form1.txtelegido.value = fila.id
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h4>
            SEGUIMIENTO DE PROPUESTAS
        </h4>
    </div>
    <table style="width: 100%; margin-bottom: 229px;" align="left">
        <tr>
            <td class="contornotabla" valign="top" width="100%" colspan="6">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td bgcolor="#F0F0F0" class="bordeinf" width="20%">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 10%;">
                                        Ejercicio Presupuestal:
                                    </td>
                                    <td style="width: 40%;">
                                        <asp:DropDownList ID="ddl_EjercicioPresupuestal" runat="server" Height="21px" Width="30%">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 10%;">
                                        Plan Operativo Anual:
                                    </td>
                                    <td style="width: 40%;">
                                        <asp:DropDownList ID="ddl_POA" runat="server" Height="21px" Width="90%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <%--<td>
                                        Actividad POA:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_ActividadPOA" runat="server" Height="21px" Width="90%">
                                        </asp:DropDownList>
                                    </td>--%>
                                    <td>
                                        Instancia:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_InstanciaPropuesta" runat="server" Height="21px" Width="30%">
                                            <asp:ListItem Value="0">-- TODOS -- </asp:ListItem>
                                            <asp:ListItem Value="P">Proponente</asp:ListItem>
                                            <%--<asp:ListItem Value="D">Director</asp:ListItem>--%>
                                            <asp:ListItem Value="F">Facultad</asp:ListItem>
                                            <asp:ListItem Value="A">Administración</asp:ListItem>
                                            <%--<asp:ListItem Value="I">Especialista</asp:ListItem>
                                            <asp:ListItem Value="C">Coordinador</asp:ListItem>
                                            <asp:ListItem Value="O">Presupuesto</asp:ListItem>
                                            <asp:ListItem Value="T">Secretario</asp:ListItem>--%>
                                            <asp:ListItem Value="K">Rectorado</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Tipo de Propuesta:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_TipoPropuesta" runat="server" Height="21px" Width="90%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <%--<asp:ListItem Value="D">Director</asp:ListItem>--%>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:Button ID="cmd_Consultar" runat="server" CssClass="nuevo1" Height="47px" Text="        Consultar"
                                            Width="120px" />
                                    &nbsp;&nbsp;
                                        <asp:Button ID="cmd_Exportar" runat="server" CssClass="editar1" Height="47px" Text="        Exportar"
                                            Width="120px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="dgvPropuestas" runat="server" Width="100%" AutoGenerateColumns="False"
                    PageSize="15" CellPadding="2" ForeColor="#333333" Font-Size="Smaller">
                    <RowStyle Height="20px" />
                    <Columns>
                        <asp:BoundField DataField="codigo_Prp" HeaderText="ID">
                            <ItemStyle Width="3%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechainicio_Ipr" HeaderText="FECHA">
                            <ItemStyle Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre_prp" HeaderText="PROPUESTA">
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipoPropuesta" HeaderText="TIPO DE PROPUESTA">
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RegistraPropuesta" HeaderText="PROPONENTE">
                            <ItemStyle Width="18%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="areasPoa" HeaderText="AREAS POA">
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="instancia_Prp" HeaderText="INSTANCIA">
                            <ItemStyle Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechainiciopropuesta_dev" HeaderText="FECHA INI" DataFormatString="{0:d}">
                            <ItemStyle Width="7%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechafinpropuesta_dev" HeaderText="FECHA FIN" DataFormatString="{0:d}">
                            <ItemStyle Width="7%" />
                        </asp:BoundField>
                       <%-- <asp:BoundField DataField="estado_Prp" HeaderText="ESTADO">
                            <ItemStyle Width="5%" />
                        </asp:BoundField>--%>
                        <asp:CommandField HeaderText="ASIGNAR" ShowSelectButton="True" ButtonType="Image"
                            SelectImageUrl="../../../Images/previo.gif" SelectText="ASIGNAR">
                            <HeaderStyle Width="3%" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:CommandField>
                    </Columns>
                    <HeaderStyle BackColor="#666666" ForeColor="White" Height="22px" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td bgcolor="#F0F0F0" class="contornotabla" colspan="6">
                <asp:Panel ID="Panel1" runat="server" Height="100%">
                    <asp:HiddenField ID="txtelegido" runat="server" />
                    <asp:Label ID="lbl_numeroItems" runat="server" Font-Bold="True"></asp:Label>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
