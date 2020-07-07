<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VerPresupuesto.aspx.vb" Inherits="presupuesto_areas_VerPresupuesto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script language="javascript" type="text/javascript" >
    function calcularsubtotal(id1,id2,precio)
    {
	    id2.value=id1.value * precio.value
	    //id2.value=math.round(id2.value*100)/100
    }
    function calcularvalores(ctotal,precio)
    {
	    var total=0;
	    var valctrl=0;
	    //alert(gvDetalleEjecucion_ctl02_valor2)
	    for (i=2; i<14; i++)
	    {   if (i<10) 
	            texto="document.form1.gvDetalleEjecucion_ctl0" + i + "_valor2.value"
	        else
	            texto="document.form1.gvDetalleEjecucion_ctl" + i + "_valor2.value"
		    if (eval(texto)!="")
		    {    
			    valctrl=parseFloat(eval(texto))
	    		     if (isNaN(valctrl))
	    			    {valctrl=0}
	    		      total+=eval(valctrl)
		    } 
	    }
	    ctotal.value=total * precio.value
  }
    </script>
    <style type="text/css">
        .style1
        {
            height: 17px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;" border="0" cellpadding="0" cellspacing="0" >
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                <tr class="usatEtiqOblig">
                <td class="CeldaImagen" colspan="2" height="30px" style="height:30px">
                    
                    &nbsp;DETALLE DE PRESUPUESTO</td>
            </tr>
                <tr>
                <td bgcolor="#999999" colspan="2">
                    </td>
            </tr>
                <tr class="usatEtiqOblig" style="height: 23px" bgcolor="#FFFFF2">
                <td>
                    
                    Código presupuesto:
                    <asp:Label ID="lblCodigo" runat="server" Text="Label" Font-Bold="False"></asp:Label>
                </td>
                <td>
                    Proceso:                     
                    <asp:Label ID="lblProceso" runat="server" 
                        Text="Label" Font-Bold="False"></asp:Label>
                </td>
            </tr>
            <tr class="usatEtiqOblig" style="height: 23px" bgcolor="#FFFFF2">
                <td>
                    Centro de costos:
                    <asp:Label ID="lblCecos" runat="server" Text="Label" Font-Bold="False"></asp:Label>
                </td>
                <td>
                    Estado:
                    <asp:Label ID="lblEstado" runat="server" Text="Label" Font-Bold="False"></asp:Label>
                </td>
            </tr>
            <tr class="usatEtiqOblig" style="height: 23px" bgcolor="#FFFFF2">
                <td>
                    Fecha de inicio:                     
                    <asp:Label ID="lblFechaIni" runat="server" 
                        Text="Label" Font-Bold="False"></asp:Label>
                </td>
                <td>
                    Fecha de fin:
                    <asp:Label ID="lblFechaFin" runat="server" Text="Label" Font-Bold="False"></asp:Label>
                </td>
            </tr>
            <tr class="usatEtiqOblig" bgcolor="#FFFFF2">
                <td style="height: 23px">
                    Observación:
                    <asp:Label ID="lblObservacion" runat="server" Text="Label" Font-Bold="False"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td bgcolor="#999999" colspan="2" height="1px">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="font-weight: 700" class="titulocel">
                    »
                    OBSERVACIONES:</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvobservaciones" runat="server" AutoGenerateColumns="False" 
                        Width="100%" CellPadding="3">
                        <Columns>
                            <asp:BoundField DataField="fecha_Rpr" 
                                HeaderText="Fecha y hora" />
                            <asp:BoundField DataField="Revisor" HeaderText="Revisor" />
                            <asp:BoundField DataField="observacion_Rpr" HeaderText="Observación" />
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label1" runat="server" ForeColor="#CC0000" 
                                Text="No se encontraron registros"></asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="TituloTabla" Height="20px" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="cmdEditar" runat="server" BackColor="White" 
                        BorderStyle="None" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td bgcolor="#999999" colspan="2" height="1px">
                </td>
            </tr>
            <tr class="titulocel">
                <td style="font-weight: bold">
                    » INGRESOS: TECHO
                    <asp:TextBox ID="lblTechoIngreso" runat="server" Text="0.00" Width="70px" 
                        BackColor="White" ReadOnly="True"></asp:TextBox>
                </td>
                <td style="font-weight: bold" align="right">
                    <asp:Button ID="cmdNuevoIng" runat="server" CssClass="agregar2" Text="Nuevo" 
                        Width="80px" Height="22px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvIngresos" runat="server" AutoGenerateColumns="False" 
                        Width="100%" DataKeyNames="codigo_Dpr" 
                        CellPadding="3">
                        <Columns>
                            <asp:BoundField DataField="Clase" HeaderText="Clase" />
                            <asp:BoundField DataField="CodItem" HeaderText="Cód. Item" />
                            <asp:BoundField DataField="DesEstandar" HeaderText="Descripción Estandar" />
                            <asp:BoundField DataField="DetDescripcion" HeaderText="Detalle Descripción" />
                            <asp:BoundField DataField="Unidad" HeaderText="Unidad" />
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                            <asp:BoundField DataField="PreUnitario" HeaderText="Prec. Unitario" 
                                DataFormatString="{0:#,###,##0.00}" />
                            <asp:BoundField DataField="subTotal" HeaderText="Sub Total" 
                                DataFormatString="{0:#,###,##0.00}" />
                            <asp:CommandField ButtonType="Image" 
                                DeleteImageUrl="~/images/Presupuesto/eliminar.gif" DeleteText="" 
                                EditImageUrl="~/images/Presupuesto/editar.gif" 
                                ShowEditButton="True" SelectText="" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:CommandField ButtonType="Image" 
                                DeleteImageUrl="~/images/Presupuesto/eliminar.gif" ShowDeleteButton="True" Visible="false" />
                            <asp:TemplateField>
                            <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="return confirm('¿Desea eliminar el registro?');" ImageUrl="~/images/Presupuesto/eliminar.gif" CommandName="Delete" />
                            </ItemTemplate>
                            </asp:TemplateField>                                
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label1" runat="server" ForeColor="#CC0000" 
                                Text="No se encontraron registros"></asp:Label>
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#FFFFCC" />
                        <HeaderStyle CssClass="TituloTabla" Height="20px" />
                    </asp:GridView>
                </td>
            </tr>
            <tr align="right">
                <td class="style1">
                    </td>
                <td style="font-weight: bold">
                    Total de ingresos:&nbsp;&nbsp;
                    <asp:Label ID="lblTotalIngresos" runat="server" BackColor="Silver" 
                        BorderStyle="Solid" BorderWidth="1px" Text="0.00" Width="70px"></asp:Label>
                </td>
            </tr>
            <tr align="right">
                <td class="style1">
                    &nbsp;</td>
                <td style="font-weight: bold">
                    &nbsp;</td>
            </tr>
            <tr>
                <td bgcolor="#999999" colspan="2" height="1px">
                </td>
            </tr>
            <tr class="titulocel">
                <td style="font-weight: bold">
                    » EGRESOS: TECHO&nbsp;                     
                    <asp:TextBox ID="lblTechoEgreso" 
                        runat="server" Text="0.00" Width="70px" BackColor="White" 
                        ReadOnly="True"></asp:TextBox>
                </td>
                <td style="font-weight: bold" align="right">
                    <asp:Button ID="cmdNuevoEgr" runat="server" CssClass="agregar2" Text="Nuevo" 
                        Width="80px" Height="22px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvEgresos" runat="server" AutoGenerateColumns="False" 
                        Width="100%" DataKeyNames="codigo_Dpr" 
                        CellPadding="3">
                        <Columns>
                            <asp:BoundField DataField="Clase" HeaderText="Clase" />
                            <asp:BoundField DataField="CodItem" HeaderText="Cód. Item" />
                            <asp:BoundField DataField="DesEstandar" HeaderText="Descripción Estandar" />
                            <asp:BoundField DataField="DetDescripcion" HeaderText="Detalle Descripción" />
                            <asp:BoundField DataField="Unidad" HeaderText="Unidad" />
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                            <asp:BoundField DataField="PreUnitario" HeaderText="Prec. Unitario" 
                                DataFormatString="{0:#,###,##0.00}" />
                            <asp:BoundField DataField="subTotal" HeaderText="Sub Total" 
                                DataFormatString="{0:#,###,##0.00}" />
                            <asp:CommandField ButtonType="Image" 
                                DeleteImageUrl="~/images/Presupuesto/eliminar.gif" DeleteText="" EditImageUrl="~/images/Presupuesto/editar.gif" 
                                ShowEditButton="True" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:CommandField ButtonType="Image" 
                                DeleteImageUrl="~/images/Presupuesto/eliminar.gif" ShowDeleteButton="True" Visible="false" />
                            <asp:TemplateField>
                            <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="return confirm('¿Desea eliminar el registro?');" ImageUrl="~/images/Presupuesto/eliminar.gif" CommandName="Delete" />
                            </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label1" runat="server" ForeColor="#CC0000" 
                                Text="No se encontraron registros"></asp:Label>
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#FFFFCC" />
                        <HeaderStyle CssClass="TituloTabla" Height="20px" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td align="right" style="font-weight: bold">
                    Total de egresos:&nbsp;&nbsp;&nbsp;                     
                    <asp:Label ID="lblTotalEgresos" runat="server" BackColor="Silver" 
                        BorderStyle="Solid" BorderWidth="1px" Width="70px">0.00</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td align="right" style="font-weight: bold">
                    <b>Ingresos - Egresos:                     
                    </b>                     
                    <asp:Label ID="lblIngMenosEgr" 
                        runat="server" BackColor="Silver" BorderWidth="1px" Text="0.00" 
                        Width="70px" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" 
                        Height="15px" ReadOnly="True"></asp:Label>
                </td>
            </tr>
            </table>
    
    </div>
    <asp:HiddenField ID="hddHabilitado" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeEditarItem" runat="server" 
        BackgroundCssClass="FondoAplicacion" BehaviorID="mpeEditarItem" 
        CancelControlID="cmdCerrar" PopupControlID="cmdPanelDatos" 
        TargetControlID="cmdEditar" Y="50">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="cmdPanelDatos" runat="server" BorderStyle="Solid" 
        BorderWidth="1px" CssClass="contornotabla" Width="50%">
        <table style="width:100%;" border="0" cellpadding="0" cellspacing="0" 
            class="contornotabla">
            <tr>
                <td bgcolor="#999999" height="1px" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TituloTabla">
                    EDITAR ITEM</td>
                <td align="right" class="TituloTabla">
                    <asp:Button ID="Button1" runat="server" Height="20px" Text="X" Width="20px" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#999999" height="1px" colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table align="center" width="95%">
                        <tr>
                            <td>
                                Código&nbsp;&nbsp; :
                                <asp:TextBox ID="lblCodigoo" runat="server" ReadOnly="True" Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Concepto:
                            </td>
                        </tr>
                        <tr>
                            <td height="20">
                                <asp:TextBox ID="lblConcepto" runat="server" ReadOnly="True" Rows="3" 
                                    TextMode="MultiLine" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="20">
                                Detalle&nbsp;&nbsp; :</td>
                        </tr>
                        <tr>
                            <td height="20">
                                <asp:TextBox ID="txtDetalle" runat="server" Rows="3" TextMode="MultiLine" 
                                    Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server" BackColor="White" 
                                    GroupingText="Opciones de registro">
                                    <asp:RadioButtonList ID="rblRegistro" runat="server" AutoPostBack="True">
                                        <asp:ListItem Selected="True" Value="C">Indicar cantidad</asp:ListItem>
                                        <asp:ListItem Value="P">Indicar importes</asp:ListItem>
                                    </asp:RadioButtonList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRegistro" runat="server" Text="Importe:" Width="49px"></asp:Label>
                                &nbsp;<asp:TextBox ID="txtRegistro" runat="server" Width="80px"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                    ControlToValidate="txtRegistro" 
                                    ErrorMessage="El campo importe o cantidad debe ser un valor mayor a cero" 
                                    Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtRegistro" 
                                    ErrorMessage="El valor importe / cantidad es obligatorio" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                &nbsp;&nbsp; Unidad:&nbsp;
                                <asp:TextBox ID="lblUnidad" runat="server" ReadOnly="True" Width="80px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="lblUnidad" 
                                    ErrorMessage="La unidad del concepto a registrar no puede ser vacío" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: 700">
                                Detalle de ejecución
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvDetalleEjecucion" runat="server" 
                                    AutoGenerateColumns="False" HorizontalAlign="Right" Width="70%" 
                                    CellPadding="3">
                                    <RowStyle Height="20px" />
                                    <Columns>
                                        <asp:BoundField DataField="mes" HeaderText="Mes" />
                                        <asp:BoundField HeaderText="Cantidad / Importe" />
                                    </Columns>
                                    <HeaderStyle CssClass="TituloTabla" Height="20px" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="font-weight: bold">
                                <asp:Button ID="cmdTotal" runat="server" style="height: 26px" Text="Total" 
                                    Width="60px" Visible="False" />
                                Total&nbsp;<asp:TextBox ID="txtTotal" runat="server" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar" Text="   Guardar" 
                                    ValidationGroup="Guardar" Width="75px" />
                                &nbsp;<asp:Button ID="cmdCerrar" runat="server" CssClass="cancelar" Text="Cerrar" 
                                    Width="75px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hddcodigocon" runat="server" />
        <asp:HiddenField ID="hddtipocon" runat="server" />
        <asp:HiddenField ID="hddtipo" runat="server" />
        <asp:HiddenField ID="hddiduni" runat="server" />
        <asp:HiddenField ID="hddcodigoDpr" runat="server" />
        <br />
    </asp:Panel>
    </form>
</body>
</html>
