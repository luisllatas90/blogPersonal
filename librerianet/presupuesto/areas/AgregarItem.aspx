<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AgregarItem.aspx.vb" Inherits="presupuesto_areas_AgregarItem" %>

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
</head>
<body>
    <form id="form1" runat="server" style="height:100%">
    <div style="width: 100%; height: 100%">
    
        <table style="width:100%; height: 100%;"  cellpadding="0" cellspacing="0" 
            border="0">
            <tr>
                <td valign="top" width="50%">
                    <table style="width:100%;" cellpadding="0">
                        <tr>
                            <td style="font-weight: 700; height: 25px;" class="CeldaImagen">
                                BUSCAR CONCEPTO</td>
                        </tr>
                        <tr>
                            <td>
                                Clase&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                                <asp:DropDownList ID="cboClase" runat="server" AutoPostBack="True" 
                                    Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Línea&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                                <asp:DropDownList ID="CboLinea" runat="server" AutoPostBack="True" 
                                    Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Familia&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                                <asp:DropDownList ID="CboFamilia" runat="server" AutoPostBack="True" 
                                    Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Sub Familia:
                                <asp:DropDownList ID="cboSubFamilia" runat="server" AutoPostBack="True" 
                                    Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Concepto&nbsp;&nbsp; :                                <asp:TextBox ID="txtConcepto" runat="server" Width="65%"></asp:TextBox>
&nbsp;
                                <asp:Button ID="cmdBuscar" runat="server" CssClass="Buscar" Text="      Buscar" 
                                    Width="65px" Height="22px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Resultados :</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvItems" runat="server" AllowPaging="True" 
                                    DataKeyNames="codigocon,tipo,IdUni" DataSourceID="ObjConceptos" 
                                    PageSize="20" Width="100%" CellPadding="3">
                                    <RowStyle Height="20px" />
                                    <Columns>
                                        <asp:CommandField SelectText="" ShowSelectButton="True" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label2" runat="server" Text="No se encontraron registros" 
                                            ForeColor="Red"></asp:Label>
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <HeaderStyle CssClass="TituloTabla" Height="20px" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" width="1px" bgcolor="#4182CD">
                  </td>
                <td valign="top">
                    <table style="width:100%;">
                        <tr>
                            <td class="CeldaImagen" style="height: 25px">
                                <b>DETALLE DE EJECUCIÓN</b></td>
                        </tr>
                        <tr>
                            <td>
                                Código&nbsp;&nbsp; :                                 
                                <asp:TextBox ID="lblCodigo" runat="server" Width="70px" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Concepto:
                                </td>
                        </tr>
                        <tr>
                            <td height="20">
                                <asp:TextBox ID="lblConcepto" runat="server" Width="100%" ReadOnly="True" 
                                    Rows="3" TextMode="MultiLine"></asp:TextBox>
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
                                <asp:Panel ID="Panel1" runat="server" GroupingText="Opciones de registro" 
                                    BackColor="White">
                                    <asp:RadioButtonList ID="rblRegistro" runat="server" AutoPostBack="True">
                                        <asp:ListItem Value="C" Selected="True">Indicar cantidad</asp:ListItem>
                                        <asp:ListItem Value="P">Indicar importes</asp:ListItem>
                                    </asp:RadioButtonList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRegistro" runat="server" Text="Precio Unitario:" Width="49px"></asp:Label>
&nbsp;<asp:TextBox ID="txtRegistro" runat="server" Width="80px"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                    ControlToValidate="txtRegistro" 
                                    ErrorMessage="El campo precio unitario o cantidad debe ser un valor mayor a cero" 
                                    Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtRegistro" 
                                    ErrorMessage="El valor Precio Unit/Cantidad es obligatorio" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                &nbsp;&nbsp; Unidad:&nbsp;
                                <asp:TextBox ID="lblUnidad" runat="server" Width="80px" ReadOnly="True"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ErrorMessage="La unidad del concepto a registrar no puede ser vacío" 
                                    ControlToValidate="lblUnidad" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Detalle de ejecución
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvDetalleEjecucion" runat="server" AutoGenerateColumns="False" 
                                    Width="70%" CellPadding="3">
                                    <RowStyle Height="20px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Mes" DataField="mes" />
                                        <asp:BoundField HeaderText="Cantidad / Importe" />
                                    </Columns>
                                    <HeaderStyle CssClass="TituloTabla" Height="20px" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold">
                                <asp:Button ID="cmdTotal" runat="server" style="height: 26px" Text="Total" 
                                    Width="60px" Visible="False" />
                                Total
&nbsp;<asp:TextBox ID="txtTotal" runat="server" Width="100px"></asp:TextBox>
                            &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="cmdGuardar" runat="server" Text="   Guardar" CssClass="guardar" 
                                    Width="75px" ValidationGroup="Guardar" Height="22px" />
&nbsp;<asp:Button ID="cmdCerrar" runat="server" Text="  Retornar" CssClass="cancelar" Width="75px" 
                                    Height="22px" />
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
                </td>
            </tr>
        </table>
    
    </div>
    <asp:ObjectDataSource ID="ObjConceptos" runat="server" 
        SelectMethod="ConsultarConceptos" TypeName="ClsPresupuesto">
        <SelectParameters>
            <asp:QueryStringParameter Name="tipo" QueryStringField="tipo" Type="String" />
            <asp:ControlParameter ControlID="cboClase" Name="clase" 
                PropertyName="SelectedValue" Type="Int16" />
            <asp:ControlParameter ControlID="CboLinea" Name="linea" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="CboFamilia" Name="familia" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="cboSubFamilia" Name="subfamilia" 
                PropertyName="SelectedValue" Type="Int32" DefaultValue="" />
            <asp:ControlParameter ControlID="txtConcepto" DefaultValue=" " Name="concepto" 
                PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
