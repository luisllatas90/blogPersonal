<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaActividadesPOA-bk.aspx.vb"
    Inherits="indicadores_POA_PROTOTIPOS_Registrar_POA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>

    <%--<link href="../../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />--%>
    <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="hd_seleccion_codigoacp" Value="-1" />
    <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server" Text="Programas Y Proyectos"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%">
            <tr>
                <td width="100px">
                    Plan Estratégico
                </td>
                <td width="510px">
                    <asp:DropDownList ID="ddlPlan" runat="server" Width="500">
                    </asp:DropDownList>
                </td>
                <td>
                    Ejercicio Presupuestal
                </td>
                <td>
                    <asp:DropDownList ID="ddlEjercicio" runat="server" Width="140">
                    </asp:DropDownList>
                </td>
                <td>
                    Estado
                </td>
                <td>
                    <asp:DropDownList ID="ddlestado" runat="server">
                        <asp:ListItem Value="T">Todos</asp:ListItem>
                        <asp:ListItem Value="P">Pendientes</asp:ListItem>
                        <asp:ListItem Value="A">Asignados</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="btnBuscar" />
                </td>
            </tr>
        </table>
        <br />
        <div style="font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; font-size: 13px;
            color: #337ab7; padding-bottom: 10px; font-weight: bold">
            <asp:Label ID="Label3" runat="server" Text="Planes Operativos"></asp:Label>
        </div>
        <asp:GridView ID="dgvpoa" runat="server" Width="100%" AutoGenerateColumns="False"
            DataKeyNames="codigo_poa,responsable_poa" HeaderStyle-Height="20px" CellPadding="4">
            <Columns>
                <asp:BoundField HeaderText="PLAN OPERATIVO ANUAL" DataField="nombre_poa">
                    <HeaderStyle Width="40%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="RESPONSABLE" DataField="responsable">
                    <HeaderStyle Width="25%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="EJERCICIO" DataField="descripcion_ejp">
                    <HeaderStyle Width="5%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="INGRESOS" DataField="limite_ingreso" DataFormatString="{0:N}">
                    <HeaderStyle Width="9%" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="EGRESOS" DataField="limite_egreso" DataFormatString="{0:N}">
                    <HeaderStyle Width="9%" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="EXCEDENTE" DataField="utilidad" DataFormatString="{0:N}">
                    <HeaderStyle Width="9%" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:CommandField HeaderText="ASIGNAR" ShowSelectButton="True" ButtonType="Image"
                    SelectImageUrl="../../images/previo.gif" SelectText="ASIGNAR">
                    <HeaderStyle Width="3%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:BoundField DataField="codigo_poa" HeaderText="codigo_poa" Visible="False" />
            </Columns>
            <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <EmptyDataTemplate>
                No se Encontraron Registros</EmptyDataTemplate>
        </asp:GridView>
        <br />
        <asp:Label ID="lblMensajeFormulario" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <div id="DetProgramasProyectos" visible="false" runat="server">
            <asp:Label ID="Label2" runat="server" Text="Detalle de Programas y Proyectos" Style="font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif;
                font-size: 13px; color: #337ab7; padding-bottom: 10px; font-weight: bold"></asp:Label>
            <br />
            <table width="100%">
                <tr>
                    <td>
                        <asp:Button ID="btnNuevo" runat="server" Text="   Nuevo Programa/Proyecto" CssClass="btnNuevo"
                            Width="220px" />
                    </td>
                    <td style="text-align: right">
                        <img alt="" src="" style="background-color: #FFFFFF; border: solid 1px #000000" width="7px"
                            height="7px" /><asp:Label ID="Label6" runat="server"> Proceso de Registro</asp:Label>
                        &nbsp;&nbsp;
                        <img alt="" src="" style="background-color: #87CEEB" width="8px" height="8px" /><asp:Label
                            runat="server"> Enviado a Planificación</asp:Label>
                        &nbsp;&nbsp;
                        <img alt="" src="" style="background-color: #F08080" width="8px" height="8px" /><asp:Label
                            ID="Label4" runat="server"> Observado Por Planificación</asp:Label>
                        &nbsp;&nbsp;
                        <img alt="" src="" style="background-color: #90EE90" width="8px" height="8px" /><asp:Label
                            ID="Label5" runat="server"> Aprobado Por Planificación</asp:Label>
                    </td>
                </tr>
                <tr style="height: 5px;">
                    <td colspan="2">
                        <div id="aviso" runat="server">
                            <asp:Label ID="lblmensaje" runat="server" Text="" Font-Bold="True"></asp:Label>
                        </div>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan='2'>
                        <asp:GridView ID="dgvactividades" runat="server" Width="100%" CellPadding="4" DataKeyNames="codigo_acp"
                            AutoGenerateColumns="False" ShowFooter="True">
                            <Columns>
                                <asp:BoundField HeaderText="CODIGO IEP" DataField="codigo_iep" Visible="false" />
                                <asp:BoundField HeaderText="ACTIVIDAD" DataField="resumen_acp">
                                    <HeaderStyle Width="45%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="TIPO ACTIVIDAD" DataField="descripcion_tac" ControlStyle-Width="40px" />
                                <asp:BoundField HeaderText="RESPONSABLE" DataField="responsable">
                                    <HeaderStyle Width="33%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="INGRESOS(S/.)" DataField="ingresos_acp" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="EGRESOS(S/.)" DataField="egresos_acp" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="EJERCICIO" DataField="descripcion_ejp">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectText="Editar"
                                    SelectImageUrl="../../../images/editar_poa.png" HeaderText="EDITAR">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="ENVIAR" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" AlternateText="Enviar"
                                            CommandName="Edit" ImageUrl="../../images/inv_paso.png" Text="Editar" OnClientClick="return confirm('¿Esta Seguro que Desea Enviar Actividad?.')" />
                                    </ItemTemplate>
                                    <ControlStyle Height="17px" Width="17px" />
                                    <HeaderStyle Width="3%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ELIMINAR" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" AlternateText="Eliminar"
                                            CommandName="Delete" ImageUrl="../../Images/menus/noconforme_small.gif" Text="Eliminar"
                                            OnClientClick="return confirm('¿Esta Seguro que Desea Eliminar Actividad?.')" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CERRAR">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="CerrarProy" name="CerrarProy" runat="server" CausesValidation="False"
                                            ImageUrl="../../Images/candadoabierto.png" Width="20px" Height="20px" ToolTip="Cerrar Proyecto"
                                            OnClick="ibtnCerrarProy_Click" OnClientClick="return confirm('¿Desea Cerrar El Proyecto?.')"
                                            Text="Cerrar Proyecto" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblfilas_dap" runat="server" Text=""></asp:Label>
            <br />
        </div>
    </div>
    </form>
</body>
</html>
