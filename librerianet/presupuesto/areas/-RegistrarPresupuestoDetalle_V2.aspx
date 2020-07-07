<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RegistrarPresupuestoDetalle_V2.aspx.vb"
    Inherits="presupuesto_areas_RegistrarPresupuestoDetalle_V2" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estiloWeb_V2.css" rel="stylesheet" type="text/css" />

    <script src="../../private/funciones.js" type="text/javascript" language="javascript"></script>

    <script language="javascript" type="text/javascript">
        function calcularvalores(MontoTotal, Cantidad, precio, mesIni, mesFin) {
            var total = 0;
            var valctrl = 0;
            for (i = mesIni; i <= mesFin; i++) {
                texto = "document.form1.gvDetalleEjecucion_ctl02_valor" + i + ".value";
                if (eval(texto) != "") {
                    valctrl = parseFloat(eval(texto))
                    if (isNaN(valctrl))
                    { valctrl = 0; }
                    total += eval(valctrl);
                }
            }
            Cantidad.value = total;
            MontoTotal.value = total * precio.value;
            MontoTotal.value = Math.round(MontoTotal.value * 100) / 100;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table class="pagina" cellpadding="8" cellspacing="0">
            <tr>
                <td class="tituloPagina">
                    <asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <%--Inicio Datos Usuario--%>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td width="20%">
                                Usuario que registra
                            </td>
                            <td width="80%">
                                <asp:Label ID="lblUsuario" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--Fin Datos Usuario--%>
            <%--Inicio Cabecera--%>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="cabeceraDatos" colspan="2">
                                Cabecera de Presupuesto
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <contenttemplate>
                                     <table width="100%" border="0" cellpadding="2" cellspacing="0">
                                            <tr>
                                                <td width="20%">
                                                    Periodo Presupuestal
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                                    runat="server" ControlToValidate="cboPeriodoPresu" 
                                                    ErrorMessage="Seleccione periodo presupuestal" 
                                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td width="80%">
                                                    <asp:DropDownList ID="cboPeriodoPresu" runat="server" AutoPostBack ="true" OnSelectedIndexChanged ="cboPeriodoPresu_SelectedIndexChanged" >
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                Plan Operativo
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblnombrepoa" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <%--Centro de Costo--%> 
                                                    Programa / Proyecto <%--treyes 28/11/2016 se modifica a solicitud de esaavedra--%>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                                                    runat="server" ControlToValidate="cboCecos" 
                                                    ErrorMessage="Seleccione centro de costos" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator5" runat="server" 
                                                    ControlToValidate="cboCecos" ErrorMessage="Seleccione Centro de Costos" 
                                                    Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>--%>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                                                    runat="server" ControlToValidate="cboCecos" 
                                                    ErrorMessage="Seleccione Programa / Proyecto" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator5" runat="server" 
                                                    ControlToValidate="cboCecos" ErrorMessage="Seleccione Programa / Proyecto" 
                                                    Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                                </td>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtCecos" runat="server" Width="90px" BackColor="#F3F3F3" Visible="False"></asp:TextBox>
                                                    <asp:DropDownList ID="cboCecos" runat="server" AutoPostBack="True" OnSelectedIndexChanged ="cboCecos_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtBuscaCecos" runat="server" ValidationGroup="BuscarCecos"></asp:TextBox>
                                                    <asp:ImageButton ID="ImgBuscarCecos" runat="server" 
                                                        ImageUrl="~/images/busca.gif" ValidationGroup="BuscarCecos" />
                                                    <asp:Label ID="lblTextBusqueda" runat="server" Text="(clic aquí)"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    &nbsp;
                                                </td>
                                                <td width="80%">
                                                    <asp:LinkButton ID="lnkBusquedaAvanzada" runat="server" ForeColor="Blue">Busqueda 
                                                    Avanzada</asp:LinkButton>
                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" 
                                                        AssociatedUpdatePanelID="UpdatePanel2">
                                                        <ProgressTemplate>
                                                            <font style="color:Blue">Procesando. Espere un momento...</font>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="Panel3" runat="server" Height="150px" ScrollBars="Vertical" 
                                                        Width="100%">
                                                        <asp:GridView ID="gvCecos" runat="server" AutoGenerateColumns="False" 
                                                            BorderColor="#628BD7" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                                            DataKeyNames="codigo_cco" ForeColor="#333333" 
                                                            ShowHeader="False" Width="98%">
                                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#EFF3FB" />
                                                            <Columns>
                                                                <asp:BoundField DataField="codigo_cco" HeaderText="Código" />
                                                                <%--<asp:BoundField DataField="descripcion_cco" HeaderText="Centro de costos" />--%> <%--treyes 28/11/2016 se modifica a solicitud de esaavedra--%>
                                                                <asp:BoundField DataField="resumen_acp" HeaderText="Programa / Proyecto" /> <%--treyes 28/11/2016 se modifica a solicitud de esaavedra--%>
                                                                <asp:CommandField ShowSelectButton="True" />
                                                            </Columns>
                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                            <EmptyDataTemplate>
                                                                <b>No se encontraron items con el término de búsqueda</b>
                                                            </EmptyDataTemplate>
                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan ="2">
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                         <table width="100%" border="0" cellpadding="4" cellspacing="0">
                                                            <tr>
                                                                <td width="20%">
                                                                    Actividad
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="cboActividad" 
                                                                        ErrorMessage="Seleccione Actividad" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                                    <asp:CompareValidator ID="CompareValidator6" runat="server" 
                                                                        ControlToValidate="cboActividad" ErrorMessage="Seleccione Actividad" 
                                                                        Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                                                </td>
                                                                <td width="80%">
                                                                    <asp:DropDownList ID="cboActividad" runat="server" AutoPostBack ="true" OnSelectedIndexChanged ="cboActividad_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblMsj" runat="server" Text="(*) Se muestran las actividades que requieren presupuesto según POA. De no figurar comunicarse con Dirección de Planificación."></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr id = "trIngEgr" runat ="server" >
                                                                <td width="20%">Meta / Tope Presupuestal
                                                                </td>
                                                                <td width="80%">
                                                                
                                                                 <table cellspacing ="2" style="border-collapse:collapse">
                                                                    <tr >
                                                                        <th width="60px"></th>
                                                                        <th width="120px" class="celdaTitulo">Meta / Tope</th>
                                                                        <th width="120px" class="celdaTitulo">Disponible</th>
                                                                        <th width="210px" class="celdaTitulo">Observación</th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="celdaTitulo">Ingresos</td>
                                                                        <td class="celdaDatos" align ="right"><asp:Label ID="lblTopeIng" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                                                                        <td class="celdaDatos" align ="right"><asp:Label ID="lblDisponibleIng" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label></td>
                                                                        <td class="celdaDatos"><asp:Label ID="lblObservacionIng" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="celdaTitulo">Egresos</td>
                                                                        <td class="celdaDatos" align ="right"><asp:Label ID="lblTopeEgr" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                                                                        <td class="celdaDatos" align ="right"><asp:Label ID="lblDisponibleEgr" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label></td>
                                                                        <td class="celdaDatos"><asp:Label ID="lblObservacionEgr" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                                
                                                                <%--<table>
                                                                <tr>
                                                                <td>
                                                                    <table style="border-spacing:2px;border-collapse: collapse;">
                                                                        <tr >
                                                                            <th width="60px"></th>
                                                                            <th width="120px" class="celdaTitulo">Meta</th>
                                                                            <th width="120px" class="celdaTitulo">Disponible</th>
                                                                            <th width="210px" class="celdaTitulo">Observación</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="60px" class="celdaTitulo">Ingresos</td>
                                                                            <td width="120px" class="celdaDatos"><asp:Label ID="lblTopeIng" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                                                                            <td width="120px" class="celdaDatos"><asp:Label ID="lblDisponibleIng" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label></td>
                                                                            <td width="210px" class="celdaDatos"><asp:Label ID="lblObservacionIng" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                </tr>
                                                                
                                                                <tr>
                                                                <td>
                                                                    <table style="border-spacing:2px; border-collapse: collapse;" >
                                                                        <tr >
                                                                            <th width="60px"></th>
                                                                            <th width="120px" class="celdaTitulo">Tope</th>
                                                                            <th width="120px" class="celdaTitulo">Disponible</th>
                                                                            <th width="210px" class="celdaTitulo">Observación</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="60px" class="celdaTitulo">Egresos</td>
                                                                            <td width="120px" class="celdaDatos"><asp:Label ID="lblTopeEgr" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                                                                            <td width="120px" class="celdaDatos"><asp:Label ID="lblDisponibleEgr" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label></td>
                                                                            <td width="210px" class="celdaDatos"><asp:Label ID="lblObservacionEgr" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                </tr>
                                                                </table>--%>

                                                                </td>
                                                            </tr>
                                                            </table> 
                                                        </ContentTemplate> 
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cboCecos" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                       </table>
                                    </contenttemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--Fin Cabecera--%>
            <%--Inicio Detalle--%>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="cabeceraDatos" colspan="2">
                                Detalle de Presupuesto
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <contenttemplate>
                                    <table width="100%" border="0" cellpadding="2" cellspacing="0">
                                        <tr bgcolor="#F5F9FC">
                                            <td width="20%">
                                                Movimiento<asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                                    runat="server" ControlToValidate="rblMovimiento" 
                                                    ErrorMessage="Seleccione el movimiento que desea realizar" 
                                                    ValidationGroup="BuscaItem">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td width="80%">                                    
                                                <asp:RadioButtonList ID="rblMovimiento" runat="server" 
                                                    RepeatDirection="Horizontal" ValidationGroup="BuscaItem" 
                                                    AutoPostBack="True">
                                                    <asp:ListItem Value="I">Ingreso</asp:ListItem>
                                                    <asp:ListItem Value="E">Egreso</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="20%">
                                                Item
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                    ControlToValidate="txtCodItem" 
                                                    ErrorMessage="Busque el item que desea registrar" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                                    ControlToValidate="txtConcepto" ErrorMessage="Selecione item a registrar" 
                                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td width="80%">
                                                <asp:TextBox ID="txtConcepto" runat="server" Width="500px" 
                                                    ValidationGroup="BuscaItem"></asp:TextBox>
                                                <asp:ImageButton ID="ImgBuscarItems" runat="server" 
                                                    ImageUrl="~/images/busca.gif" ValidationGroup="BuscaItem" />
                                                (clic aquí o presione enter)
                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                                                    AssociatedUpdatePanelID="UpdatePanel1">
                                                    <ProgressTemplate>
                                                        <font style="color:Blue">Procesando. Espere un momento...</font>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="Panel1" runat="server" Height="150px" ScrollBars="Vertical" 
                                                    Width="100%">
                                                    <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" 
                                                        BorderColor="#628BD7" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                                        DataKeyNames="codigocon,tipo,iduni,especificaCantidad" ForeColor="#333333" 
                                                        ShowHeader="False" Width="98%">
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <Columns>
                                                            <asp:BoundField DataField="codigo" HeaderText="Código" />
                                                            <asp:BoundField DataField="concepto" HeaderText="Concepto" />
                                                            <asp:BoundField DataField="unidad" HeaderText="Unidad" />
                                                            <asp:BoundField DataField="precio" HeaderText="Precio" />
                                                            <asp:CommandField ShowSelectButton="True" />
                                                        </Columns>
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <EmptyDataTemplate>
                                                            <b>No se encontraron items con el término de búsqueda</b>
                                                        </EmptyDataTemplate>
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="20%">
                                                Comentario
                                            </td>
                                            <td width="80%">
                                                <asp:TextBox ID="txtComentarioReq" runat="server" MaxLength="100" 
                                                    TextMode="MultiLine" Width="90%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="20%">
                                                Unidad de Medida
                                            </td>
                                            <td width="80%">
                                            <asp:Label ID="lblUnidad" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="20%">
                                                <asp:Label ID="lblTexto" runat="server" Text="Precio Unitario (S/.)"></asp:Label>
                                                <asp:CompareValidator ID="CompareValidator1" 
                                                    runat="server" ControlToValidate="txtPrecioUnit" 
                                                    ErrorMessage="El precio unitario no puede ser cero" Operator="GreaterThan" 
                                                    ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                            </td>
                                            <td width="80%">
                                                <asp:TextBox ID="txtPrecioUnit" runat="server" Width="90px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblValores" runat="server" Text="Cantidad" Font-Bold="True"></asp:Label>
                                                </td>
                                        </tr>
                                    </table>
                                </contenttemplate>
                                </asp:UpdatePanel>
                                <div id="divConfirmar" align="center" style="position: absolute; top: 50%; left: 25%;
                                    visibility: hidden;">
                                    <asp:Panel ID="Panel2" runat="server" BorderColor="#6699FF" BorderStyle="Solid" BorderWidth="1px"
                                        Width="500px">
                                        <div align="right" style="background-color: #3366FF">
                                            <asp:Button ID="cmdCerrar" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White"
                                                Height="20px" Text="x" Width="20px" />
                                        </div>
                                        <div style="background-color: #FFFFFF; padding: 10px;">
                                            El concepto que esta intentando registrar ya existe en este presupuesto. ¿Desea
                                            confirmar y forzar a que este concepto se registre?<br />
                                        </div>
                                        <div align="center" style="background-color: #FFFFFF; padding: 0px 0px 10px;">
                                            <asp:Button ID="cmdOK" runat="server" Text="Confirmar" Height="20px" Width="80px" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="cmdCancelar" runat="server" Height="20px" Text="Cancelar" Width="80px" />
                                        </div>
                                    </asp:Panel>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <contenttemplate>
                            <asp:GridView ID="gvDetalleEjecucion" runat="server" AutoGenerateColumns="False" 
                                Width="100%">
                                <RowStyle Height="20px" />
                                <Columns>
                                    <asp:BoundField HeaderText="Meses" DataField="cantidad" />
                                    <asp:BoundField HeaderText="Ene" />
                                    <asp:BoundField HeaderText="Feb" />
                                    <asp:BoundField HeaderText="Mar" />
                                    <asp:BoundField HeaderText="Abr" />
                                    <asp:BoundField HeaderText="May" />
                                    <asp:BoundField HeaderText="Jun" />
                                    <asp:BoundField HeaderText="Jul" />
                                    <asp:BoundField HeaderText="Ago" />
                                    <asp:BoundField HeaderText="Set" />
                                    <asp:BoundField HeaderText="Oct" />
                                    <asp:BoundField HeaderText="Nov" />
                                    <asp:BoundField HeaderText="Dic" />
                                </Columns>
                                <HeaderStyle Height="20px" BackColor="#C4D7F4" />
                            </asp:GridView>
                            <asp:HiddenField ID="hddMesIni" runat="server" Value="0" />
                            <asp:HiddenField ID="hddMesFin" runat="server" Value="0" />
                            </contenttemplate>
                                    <triggers>
                                <asp:AsyncPostBackTrigger ControlID="cboActividad" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="cboCecos" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="gvCecos" EventName="SelectedIndexChanged" />
                                
                            </triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                Cantidad Total
                                <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="txtCantidadAnual"
                                    ErrorMessage="La cantidad anual no puede ser cero" Operator="GreaterThan" ValidationGroup="Guardar"
                                    ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td width="80%">
                                <asp:TextBox ID="txtCantidadAnual" runat="server" Width="90px" BackColor="#F3F3F3"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                Importe Total
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtImporteAnual"
                                    ErrorMessage="El importe anual no puede ser cero" Operator="GreaterThan" ValidationGroup="Guardar"
                                    ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td width="80%">
                                <asp:TextBox ID="txtImporteAnual" runat="server" Width="90px" BackColor="#F3F3F3"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--Fin Detalle--%>
            <%--Inicio Botones--%>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <asp:Button ID="cmdRegresar" runat="server" Text="Regresar" BorderStyle="Outset"
                                    CssClass="volver" Font-Bold="True" />
                                &nbsp;
                                <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" BorderStyle="Outset" CssClass="guardar"
                                    ValidationGroup="Guardar" />
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="BuscaItem" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--Fin Botones--%>
        </table>
    </div>
    <asp:TextBox ID="txtCodItem" runat="server" Visible="False"></asp:TextBox>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Guardar" />
    <asp:HiddenField ID="hddForzar" runat="server" Value="0" />
    <asp:HiddenField ID="hddCodigo_acp" runat="server" Value="0" />
    <asp:HiddenField ID="hddCodigo_poa" runat="server" Value="0" />
    <asp:HiddenField ID="hddId" runat="server" Value="0" />
    <asp:HiddenField ID="hddCodigo_dpr" runat="server" Value="0" />
    <asp:HiddenField ID="hdmontoaEditar" runat="server" Value="0" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="BuscarCecos" />
    </form>
</body>
</html>
