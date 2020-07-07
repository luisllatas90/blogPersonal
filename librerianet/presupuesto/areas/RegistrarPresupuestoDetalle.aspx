<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RegistrarPresupuestoDetalle.aspx.vb" Inherits="presupuesto_areas_RegistrarPresupuestoDetalle"  EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script language="javascript" type="text/javascript">
    function calcularvalores(MontoTotal, Cantidad, precio)
    {
	    var total=0;
	    var valctrl=0;
	    //alert(gvDetalleEjecucion_ctl02_valor2)
	    for (i=1; i<=12; i++)
	    {   texto="document.form1.gvDetalleEjecucion_ctl02_valor" + i + ".value";
	        if (eval(texto)!="")
		    {    
			    valctrl=parseFloat(eval(texto))
	    		if (isNaN(valctrl))
	    		    {valctrl=0;}
	    		total+=eval(valctrl);
		    } 
	    }
	    Cantidad.value = total; 
	    MontoTotal.value = total * precio.value;
    }
    
    </script>
</head>
<body >
    <form id="form1" runat="server">
    <div>
    
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
    
        <table cellpadding="2" cellspacing="0" width="100%">
            <tr>
                <td class="TituloTabla" colspan="2" bgcolor="#6B91DC">
                    NUEVO REQUERIMIENTO</td>
            </tr>
            <tr bgcolor="#F5F9FC">
                <td width="15%">
                    Usuario que registra</td>
                <td>
                    <asp:Label ID="lblUsuario" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Cargo</td>
                <td>
                    <asp:Label ID="lblCargo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr bgcolor="#F5F9FC">
                <td>
                    Centro de costo</td>
                <td>
                    <asp:Label ID="lblCentroCostos" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr class="TituloTabla">
                <td colspan="2" 
                    style="font-weight: bold; width: 100%; background-color: #DAE4F3; color: #3366CC;">
                    Cabecera de requerimiento</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table style="width:100%;" border="0" cellpadding="2" 
    cellspacing="0">
                                <tr bgcolor="#F5F9FC">
                                    <td width="15%">
                                        Periodo presupuestal<asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                        runat="server" ControlToValidate="cboPeriodoPresu" 
                        ErrorMessage="Ha finalizado el proceso de elaboración de presupuesto" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboPeriodoPresu" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Programa presupuestal<asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                        runat="server" ControlToValidate="cboProgramaPresu" 
                        ErrorMessage="Seleccione el programa presupuestal" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" 
                        ControlToValidate="cboProgramaPresu" 
                        ErrorMessage="Seleccione el Programa Presupuestal que corresponde" 
                        Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboProgramaPresu" runat="server">
                                        </asp:DropDownList>
                                        &nbsp;<!--<a href='EjemplosProgramas.htm'>Ver ejemplos</a>--></td>
                                </tr>
                                <tr bgcolor="#F5F9FC">
                                    <td>
                                        Centro de costo<asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                        runat="server" ControlToValidate="cboCecos" 
                        ErrorMessage="Seleccione centro de costos" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator5" runat="server" 
                        ControlToValidate="cboCecos" ErrorMessage="Seleccione Centro de Costos" 
                        Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCecos" runat="server" Width="90px" BackColor="#F3F3F3" 
                        Visible="False"></asp:TextBox>
                                        <asp:DropDownList ID="cboCecos" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtBuscaCecos" runat="server" ValidationGroup="BuscarCecos"></asp:TextBox>
                                        <asp:ImageButton ID="ImgBuscarCecos" runat="server" 
                                            ImageUrl="~/images/busca.gif" ValidationGroup="BuscarCecos" />
                                        <asp:Label ID="lblTextBusqueda" runat="server" Text="(clic aquí)"></asp:Label>
                                    </td>
                                </tr>
                                <tr bgcolor="#F5F9FC">
                                    <td>
                                        &nbsp;</td>
                                    <td>
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
                                <tr bgcolor="#F5F9FC">
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
                                                    <asp:BoundField DataField="nombre" HeaderText="Centro de costos" />
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
                                    <td>
                                        Comentario</td>
                                    <td>
                                        <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" 
                        MaxLength="200" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr bgcolor="#F5F9FC">
                                    <td>
                                        Prioridad<asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
                        runat="server" ControlToValidate="rblPrioridad" 
                        ErrorMessage="Seleccione prioridad de requerimiento" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rblPrioridad" runat="server" 
                        RepeatDirection="Horizontal">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                        </asp:RadioButtonList>
                                        (Se&nbsp; define la primera vez que se registra un item en el programa 
                    presupuestal para cada centro de costos)</td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                        </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr class="TituloTabla">
                <td colspan="2" 
                    style="font-weight: bold; width: 100%; background-color: #DAE4F3; color: #3366CC;">
                    Detalle de requerimiento</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table width="100%" border="0" cellpadding="2" cellspacing="0">
                                <tr bgcolor="#F5F9FC">
                                    <td >
                                        Movimiento<asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                            runat="server" ControlToValidate="rblMovimiento" 
                                            ErrorMessage="Seleccione el movimiento que desea realizar" 
                                            ValidationGroup="BuscaItem">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td >                                    
                                        <asp:RadioButtonList ID="rblMovimiento" runat="server" 
                                            RepeatDirection="Horizontal" ValidationGroup="BuscaItem" 
                                            AutoPostBack="True">
                                            <asp:ListItem Value="I">Ingreso</asp:ListItem>
                                            <asp:ListItem Value="E">Egreso</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Item<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                            ControlToValidate="txtCodItem" 
                                            ErrorMessage="Busque el item que desea registrar" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                            ControlToValidate="txtConcepto" ErrorMessage="Selecione item a registrar" 
                                            ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtConcepto" runat="server" Width="500px" 
                                            ValidationGroup="BuscaItem"></asp:TextBox>
                                        <asp:ImageButton ID="ImgBuscarItems" runat="server" 
                                            ImageUrl="~/images/busca.gif" ValidationGroup="BuscaItem" />
                                        (clic aquí o presione enter)<asp:UpdateProgress ID="UpdateProgress1" runat="server" 
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
                                <tr bgcolor="#F5F9FC">
                                    <td width="15%">
                                        Comentario
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtComentarioReq" runat="server" MaxLength="100" 
                                            TextMode="MultiLine" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Sub prioridad<asp:RequiredFieldValidator ID="RequiredFieldValidator7" 
                                            runat="server" ControlToValidate="rblSubPrioridad" 
                                            ErrorMessage="Seleccione sub prioridad del item a registrar" 
                                            ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        &nbsp;</td>
                                    <td>
                                        <asp:RadioButtonList ID="rblSubPrioridad" runat="server" 
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr bgcolor="#F5F9FC">
                                    <td>
                                        Unidad de medida</td>
                                    <td>
                                        <asp:TextBox ID="lblUnidad" runat="server" BackColor="#F3F3F3" ReadOnly="True" 
                                            Width="90px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTexto" runat="server" Text="Precio Unitario (S/.)"></asp:Label>
                                        <asp:CompareValidator ID="CompareValidator1" 
                                            runat="server" ControlToValidate="txtPrecioUnit" 
                                            ErrorMessage="El precio unitario no puede ser cero" Operator="GreaterThan" 
                                            ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPrecioUnit" runat="server" Width="90px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr bgcolor="#F5F9FC">
                                    <td colspan="2">
                                        <asp:Label ID="lblValores" runat="server" Text="Cantidad" Font-Bold="True"></asp:Label>
                                        </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <div id="divConfirmar" align="center" 
                        style="position: absolute; top: 50%; left: 25%; visibility: hidden;">
                    
                    <asp:Panel ID="Panel2" runat="server" BorderColor="#6699FF" BorderStyle="Solid" 
                            BorderWidth="1px" Width="500px">
                        <div align="right" style="background-color: #3366FF">
                             <asp:Button ID="cmdCerrar" runat="server" BackColor="Red" Font-Bold="True" 
                             ForeColor="White" Height="20px" Text="x" Width="20px" />
                        </div>
                        <div style="background-color: #FFFFFF">
                            El concepto que esta intentando registrar ya existe en este presupuesto. ¿Desea 
                            confirmar y forzar a que este concepto se registre?<br />
                        </div>
                        <div style="background-color: #FFFFFF"> &amp;nbps;
                        </div>
                        <div align="center" style="background-color: #FFFFFF"> 
                            <asp:Button ID="cmdOK" runat="server" Text="Confirmar" Height="20px" 
                                Width="60px" />
                            &nbsp;&nbsp;
                            <asp:Button ID="cmdCancelar" runat="server" Height="20px" Text="Cancelar" 
                                Width="60px" />
                        </div>
                    </asp:Panel>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width:100%;">
                        <tr>
                            <td align="center">
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
                            </td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr bgcolor="#F5F9FC">
                <td>
                    &nbsp;Cantidad Anual <asp:CompareValidator ID="CompareValidator7" runat="server" 
                        ControlToValidate="txtCantidadAnual" 
                        ErrorMessage="La cantidad anual no puede ser cero" Operator="GreaterThan" 
                        ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtCantidadAnual" runat="server" Width="90px" 
                        BackColor="#F3F3F3" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Importe Anual<asp:CompareValidator ID="CompareValidator3" runat="server" 
                        ControlToValidate="txtImporteAnual" 
                        ErrorMessage="El importe anual no puede ser cero" Operator="GreaterThan" 
                        ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtImporteAnual" runat="server" Width="90px" 
                        BackColor="#F3F3F3" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                                <asp:Button ID="cmdRegresar" runat="server" Text="  Regresar" 
                                    BorderStyle="Outset" CssClass="regresar2" Width="80px" Font-Bold="True" />
                            &nbsp; <asp:Button ID="cmdGuardar" runat="server" Text="   Guardar" 
                                    BorderStyle="Outset" CssClass="guardar" Width="80px" 
                                    ValidationGroup="Guardar" />
                            &nbsp;&nbsp; </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="BuscaItem" />
                </td>
            </tr>
        </table>
    
    </div>
    <asp:TextBox ID="txtCodItem" runat="server" Visible="False"></asp:TextBox>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
            <asp:HiddenField ID="hddForzar" runat="server" Value="0" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="BuscarCecos" />
    </form>
</body>
</html>
