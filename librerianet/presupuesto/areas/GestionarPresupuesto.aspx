<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestionarPresupuesto.aspx.vb"
    Inherits="presupuesto_areas_GestionarPresupuesto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estiloWeb_V2.css" rel="stylesheet" type="text/css" />
    <script src="../../private/funciones.js" type="text/javascript" language="javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="pagina" cellpadding="8" cellspacing="0">
        <tr>
            <td colspan="2" class="tituloPagina">
                Gestionar Presupuesto
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="cmdNuevo" runat="server" Text=" Nuevo Presupuesto" BorderStyle="Outset"
                    CssClass="nuevo" />
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td width="20%" rowspan="2">
                            Periodo Presupuestal:
                            <asp:DropDownList ID="cboPeriodoPresu" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboPeriodoPresu_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="20%" rowspan="2">
                            Estado:
                            <asp:DropDownList ID="cboEstado" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboEstado_SelectedIndexChanged">
                                <asp:ListItem Text="Pendientes" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Asignados" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Todos" Value="2" Selected="selected"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="60%">
                            <img alt="" src="" style="background-color: #E9FFB1; border: solid 1px #000000" width="7px"
                                height="7px" /><asp:Label ID="Label6" runat="server"> Pendiente</asp:Label>&nbsp;&nbsp;
                            <img alt="" src="" style="background-color: #FFFFFF; border: solid 1px #000000" width="7px"
                                height="7px" /><asp:Label ID="Label1" runat="server"> Proceso de Registro</asp:Label>&nbsp;&nbsp;
                            <img alt="" src="" style="background-color: #63B3CB; border: solid 1px #000000" width="7px"
                                height="7px" /><asp:Label ID="Label2" runat="server"> Enviado a Dir. Área</asp:Label>&nbsp;&nbsp;
                            <img alt="" src="" style="background-color: #E17171; border: solid 1px #000000" width="7px"
                                height="7px" /><asp:Label ID="Label3" runat="server"> Observado por Dir. Área</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img alt="" src="" style="background-color: #97D8FD; border: solid 1px #000000" width="7px"
                                height="7px" /><asp:Label ID="Label4" runat="server"> Enviado a Dir. Finanzas</asp:Label>&nbsp;&nbsp;
                            <img alt="" src="" style="background-color: #EE9D71; border: solid 1px #000000" width="7px"
                                height="7px" /><asp:Label ID="Label5" runat="server"> Observado por Dir. Finanzas</asp:Label>&nbsp;&nbsp;
                            <img alt="" src="" style="background-color: #AAF0C2; border: solid 1px #000000" width="7px"
                                height="7px" /><asp:Label ID="Label7" runat="server"> Aprobado por Dir. Finanzas</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" rowspan="2" colspan="3">
                            Seleccionar POA:
                            <asp:DropDownList ID="ddlPoa" runat="server" AutoPostBack="True" Style="font-size: small"
                                Width="500" OnSelectedIndexChanged="ddlPoa_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:HiddenField ID="hddCodigo_Per" runat="server" />
                <asp:HiddenField ID="hddTipo" runat="server" />
                <asp:HiddenField ID="HddPoa" runat="server" />
                <asp:GridView ID="gvPresupuesto" runat="server" AutoGenerateColumns="False" DataSourceID="sqlPresupuesto"
                    DataKeyNames="codigo,codigo_cco,tipo,resumen_acp,nombre_poa" Width="100%">
                    <Columns>
                        <%--<asp:BoundField DataField="CENTRO COSTOS" HeaderText="CENTRO COSTOS" SortExpression="CENTRO COSTOS" />--%>
                        <%--treyes 28/11/2016 se modifica a solicitud de esaavedra--%>
                        <asp:BoundField DataField="nombre_poa" HeaderText="PLAN OPERATIVO" SortExpression="nombre_poa" />
                        <asp:BoundField DataField="resumen_acp" HeaderText="PROGRAMA / PROYECTO" SortExpression="resumen_acp" />
                        <%--treyes 28/11/2016 se modifica a solicitud de esaavedra--%>
                        <asp:BoundField DataField="responsable" HeaderText="RESPONSABLE" SortExpression="responsable" />
                        <asp:BoundField DataField="INGRESOS" HeaderText="INGRESOS (S/.)" SortExpression="INGRESOS"
                            DataFormatString="{0:#,###,##0.00}" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EGRESOS" HeaderText="EGRESOS (S/.)" SortExpression="EGRESOS"
                            DataFormatString="{0:#,###,##0.00}" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UTILIDAD" HeaderText="UTILIDAD (S/.)" SortExpression="UTILIDAD"
                            DataFormatString="{0:#,###,##0.00}" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="INSTANCIA" HeaderText="INSTANCIA" SortExpression="INSTANCIA" />
                        <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" SortExpression="ESTADO" />
                        <asp:CommandField ButtonType="Image" DeleteText="" EditImageUrl="~/images/Presupuesto/previo.gif"
                            ShowEditButton="True" SelectText="" HeaderText="VER">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:CommandField>
                        <asp:TemplateField HeaderText="ENVIAR">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEnviar" runat="server" ImageUrl="~/images/Presupuesto/adelante.gif"
                                    CommandName="Select" OnClientClick="return confirm('¿Desea enviar el Presupuesto a evaluación de la Dirección de Área?');" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <strong>No se encontrarón registros</strong>
                    </EmptyDataTemplate>
                    <%-- <SelectedRowStyle BackColor="#E3F9FF" />--%>
                    <HeaderStyle CssClass="tituloTabla" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:SqlDataSource ID="sqlPresupuesto" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>"
                    SelectCommand="PRESU_ConsultarPresupuestoPorResponsable" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="cboPeriodoPresu" Name="codigo_Ejp" PropertyName="SelectedValue"
                            Type="Int32" />
                        <asp:ControlParameter ControlID="hddCodigo_Per" Name="codigo_Per" PropertyName="Value"
                            Type="Int32" />
                        <asp:ControlParameter ControlID="hddTipo" Name="tipo" PropertyName="Value" Type="Int32" />
                        <asp:ControlParameter ControlID="HddPoa" Name="codigo_poa" PropertyName="Value" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
