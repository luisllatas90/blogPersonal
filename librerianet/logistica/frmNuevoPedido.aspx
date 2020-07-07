<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmNuevoPedido.aspx.vb" Inherits="presupuesto_areas_RegistrarPresupuestoDetalle"  EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    
<script src="../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../private/jq/jquery.mascara.js" type="text/javascript"></script>
        
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
    
    
    

    /*colocar esto en un bloque script
    ref: http://digitalbush.com/projects/masked-input-plugin/
    */

    $(document).ready(function() {
        jQuery(function($) {
            $("#TxtFechaEsperada").mask("99/99/9999");

        });

    })

      function ClearFile(){
        var fu = $('#fuCargarArchivo')[0];  
        fu.value="";

        var fu2= fu.cloneNode(false);
        fu2.onchange= fu.onchange;
        fu.parentNode.replaceChild(fu2,fu);
    };
   
    </script>
  
    <style type="text/css">
        .style1
        {
            width: 11px;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 20px;
            padding-left: 10px;
            width: 520px;
            height: 160px;

          }
    </style>
  
</head>
<body style="margin-top:0" >
    <form id="form1" runat="server">
        <br />
        <%  response.write(clsfunciones.cargacalendario) %>&nbsp;<div>
    
        <table cellpadding="2" cellspacing="0" width="100%">
            <tr>
                <td colspan="3" bgcolor="#FF3300">
                    <asp:Label ID="Label1" runat="server" ForeColor="White" 
                        Text="NUEVO PEDIDO A LOGÍSTICA" Font-Bold="True"></asp:Label>
    
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
    
                </td>
            </tr>
            <tr>
                <td width="15%">
                    Proceso</td>
                <td colspan="2">
                    <asp:DropDownList ID="cboPeriodoPresu" runat="server" Enabled="False">
                    </asp:DropDownList>
                &nbsp;&nbsp;&nbsp; Almacen<asp:DropDownList ID="cboAlmacen" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr bgcolor="#F5F9FC">
                <td width="15%">
                    Usuario que registra</td>
                <td colspan="2">
                    <asp:Label ID="lblUsuario" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Cargo</td>
                <td colspan="2">
                    <asp:Label ID="lblCargo" runat="server"></asp:Label>
                    <asp:Label ID="lblCentroCostos" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr bgcolor="#F5F9FC">
                <td>
                    Centro de costo<asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                        runat="server" ControlToValidate="cboCecos" 
                        ErrorMessage="Seleccione centro de costos" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator5" runat="server" 
                        ControlToValidate="cboCecos" ErrorMessage="Seleccione centro de costos" 
                        Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                        <asp:CompareValidator ID="CompareValidator8" runat="server" 
                                            ControlToValidate="cboCecos" ErrorMessage="Seleccione centro de costos" 
                                            Operator="GreaterThan" ValidationGroup="Presupuesto" 
                        ValueToCompare="0">*</asp:CompareValidator>
                                    </td>
                <td colspan="2">
                                        <asp:TextBox ID="txtCecos" runat="server" Width="90px" BackColor="#F3F3F3" 
                        Visible="False"></asp:TextBox>
                                        <asp:DropDownList ID="cboCecos" runat="server" 
                        AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtBuscaCecos" runat="server" ValidationGroup="BuscarCecos" 
                                            style="width: 128px"></asp:TextBox>
                                        <asp:ImageButton ID="ImgBuscarCecos" runat="server" 
                                            ImageUrl="~/images/busca.gif" 
                        ValidationGroup="BuscarCecos" />
                                        <asp:Label ID="lblTextBusqueda" runat="server" 
                        Text="(clic aquí)"></asp:Label>
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" 
                                            AssociatedUpdatePanelID="UpdatePanel2">
                                            <ProgressTemplate>
                                                <font style="color:Blue">Procesando. Espere un momento...</font>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                            <asp:LinkButton ID="lnkBusquedaAvanzada" runat="server" 
                        ForeColor="Blue">Busqueda Avanzada</asp:LinkButton>
                                            <asp:GridView ID="gvCecos" 
    runat="server" AutoGenerateColumns="False" 
                                                BorderColor="#628BD7" 
    BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                                DataKeyNames="codigo_cco" 
    ForeColor="#333333" ShowHeader="False" 
                                                Width="98%">
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
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                                        <asp:Panel ID="Panel3" runat="server" Height="119px" 
                                            ScrollBars="Vertical" Width="100%">
                                        </asp:Panel>
                        </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Pedido</td>
                <td>
    &nbsp;
                    <asp:Label ID="txtPedido" runat="server" Font-Bold="True" Font-Size="Medium" 
                        Font-Underline="True" ForeColor="Black"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp; Pre aprobación Director Inmediato:
                    <asp:Label ID="txtAprobacionDirInmed" runat="server" Font-Bold="True"></asp:Label>
&nbsp;&nbsp; Centro de Costo:
                    <asp:Label ID="txtAreaAprobInmed" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr bgcolor="#F5F9FC">
                <td>
                    Estado</td>
                <td>
                    <asp:DropDownList ID="cboEstado" runat="server" Enabled="False">
                    </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr class="TituloTabla">
                <td colspan="3" 
                    
                    
                    style="font-weight: bold; background-color: #FF3300; color: #FFFFFF;">
                    Detalle de pedido a Logística:&nbsp;&nbsp;&nbsp;
                                            <asp:GridView ID="gvDetallePedido" 
    runat="server" AutoGenerateColumns="False" CellPadding="4" 
                                    
    DataKeyNames="codigo_dpe,modoDistribucion_Dpe,iduni,codigo_Cco" ForeColor="#333333" 
                                GridLines="None" Width="100%">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:BoundField DataField="descripcionart" 
                                            HeaderText="Artículo" SortExpression="descripcionart" />
                                                    <asp:BoundField DataField="descripcion_cco" 
                                            HeaderText="CeCo" SortExpression="descripcion_cco" />
                                                    <asp:BoundField DataField="precioreferencial_dpe" 
                                            HeaderText="Precio" SortExpression="precioreferencial_dpe" >
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="descripcionuni" HeaderText="Unidad" />
                                                    <asp:BoundField DataField="cantidad_dpe" 
                                            HeaderText="Cantidad" SortExpression="cantidad_dpe" >
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="subtotal" 
                                            HeaderText="Subtotal" ReadOnly="True" SortExpression="subtotal" >
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Observacion_dpe" 
                                            HeaderText="Observación" SortExpression="Observacion_dpe" />
                                                    <asp:CommandField ShowDeleteButton="True" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    </asp:CommandField>
                                                    <asp:CommandField SelectText="Distribuir" 
                                            ShowSelectButton="True" Visible="false" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="iduni" HeaderText="iduni" Visible="False" />
                                                </Columns>
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" 
                                        ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" 
                                        HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" 
                                        ForeColor="#333333" />
                                                <HeaderStyle BackColor="#FF3300" Font-Bold="True" 
                                        ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                    <asp:Label ID="lblTitLista" runat="server" ForeColor="#FF3300" 
                        Text="Detalle registrado del pedido:" Visible="False" Font-Bold="True" 
                                    Font-Size="10pt"></asp:Label>
                        </td>
            </tr>
            <tr>
                <td colspan="3" align="left">
                                            &nbsp;</td>
            </tr>
                    <tr>
                <td colspan="3" align="left">
                            <asp:Label ID="lblTitTotal" runat="server" ForeColor="Red" 
                                Text="Total del pedido (S/.):" Visible="False"></asp:Label>
                            <asp:Label ID="lblTotalDetalle" runat="server" Font-Bold="True" 
                                ForeColor="#0033CC" Text="0.00" Visible="False" Font-Size="10pt" 
                                Font-Underline="True"></asp:Label>
                </td>
                    </tr>
                    <tr>
                <td colspan="3" align="left">
                    &nbsp;</td>
                    </tr>
                    <tr>
                <td colspan="3">
                            <asp:Panel ID="pnlDistribuir" runat="server" Visible="False" 
                        ForeColor="#FF3300">
                                Distribuir Item:
                                <asp:Label ID="lblNombreItem" runat="server" ForeColor="#0033CC" 
                                    Font-Size="10pt"></asp:Label>
                                <br />
                                Centro Costos<asp:CompareValidator ID="CompareValidator7" runat="server" 
                                    ControlToValidate="cboCecosEjecucion" 
                                    ErrorMessage="Seleccione el centro de costos a distribuir" 
                                    Operator="GreaterThan" ValidationGroup="Distribuir" ValueToCompare="0">*</asp:CompareValidator>
                                &nbsp;<asp:DropDownList ID="cboCecosEjecucion" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                &nbsp;<asp:Label ID="lblModoDistribucion" runat="server" Font-Size="12px" 
                                    ForeColor="Red"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                    ControlToValidate="txtCantidadDistribucion" 
                                    ErrorMessage="Ingrese la distribución" ValidationGroup="Distribuir">*</asp:RequiredFieldValidator>
                                &nbsp;<asp:TextBox ID="txtCantidadDistribucion" runat="server" Width="52px"></asp:TextBox>
                                &nbsp;
                                <asp:Button ID="cmdDistribuir" runat="server" Text="Distribuir" 
                                    ValidationGroup="Distribuir" BorderStyle="Outset" />
                                <asp:TextBox ID="txtCodigo_Ecc" runat="server" Visible="False" Width="26px"></asp:TextBox>
                                <asp:TextBox ID="txtCodigoDetalle" runat="server" Visible="False" Width="26px"></asp:TextBox>
                                <asp:GridView ID="gvDistribucion" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" DataKeyNames="codigo_ecc,codigo_Cco" ForeColor="#333333" 
                                    GridLines="None" Width="90%">
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#E3EAEB" />
                                    <Columns>
                                        <asp:BoundField DataField="Descripcion_cco" HeaderText="CeCo" />
                                        <asp:BoundField DataField="cantidad" HeaderText="Distribución">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="modoDistribucion_Dpe" HeaderText="Modo">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Precio" HeaderText="Precio">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:CommandField ShowDeleteButton="True">
                                           <ItemStyle HorizontalAlign="Center" />
                                        </asp:CommandField>
                                        <asp:CommandField SelectText="Editar" ShowSelectButton="True" />
                                    </Columns>
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                                Total:
                                <asp:Label ID="lblTotalItem" runat="server" ForeColor="#0033CC" 
                                    Font-Bold="True" Font-Size="10pt" Font-Underline="True">0.00</asp:Label>
                                &nbsp;Distribuido:
                                <asp:Label ID="lblDistribuidoItem" runat="server" ForeColor="#0033CC" 
                                    Font-Bold="True" Font-Size="10pt" Font-Underline="True">0.00</asp:Label>
                                &nbsp;Por Distribuir:
                                <asp:Label ID="lblPorDistribuir" runat="server" 
                                    Font-Bold="True" Font-Size="10pt" Font-Underline="True"></asp:Label>
                            </asp:Panel>
                </td>
                    </tr>
            <tr>
                <td colspan="3" align="left">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="left">
                    
                    <div id="divConfirmar" align="center" 
                        style="position: absolute; top: 50%; left: 25%; visibility: hidden;">
                    
                    <asp:Panel ID="Panel2" runat="server" BorderColor="#6699FF" BorderStyle="Solid" 
                            BorderWidth="1px" Width="500px">
                        <div align="right" style="background-color: #3366FF">
                             <asp:Button ID="cmdCerrar" runat="server" BackColor="Red" Font-Bold="True" 
                             ForeColor="White" Height="20px" Text="x" Width="20px" />
                        </div>
                        <div style="background-color: #FFFFFF">
                            El concepto que esta intentando registrar ya existe en este pedido. ¿Desea 
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
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table style="width:100%;" border="0" cellpadding="2" 
    cellspacing="0">


                                <tr>
                                    <td align="left">
                                        <asp:Panel ID="pnlPresupuesto" runat="server" ForeColor="Red" 
                                            HorizontalAlign="Left" Visible="False">
                                            Seleccione el ítem presupuestado que desea pedir:<asp:TextBox ID="txtDetPresup" 
                                                runat="server" Visible="False" Width="128px"></asp:TextBox>
                                            <br />
                                            <asp:HiddenField ID="hdDisponible" runat="server" Value="0"></asp:HiddenField>
                                            <asp:Panel ID="pnlListaPresup" runat="server" Height="150px" 
                                                ScrollBars="Vertical" style="margin-bottom: 0px">
                                                <asp:GridView ID="gvPresupuesto" runat="server" AutoGenerateColumns="False" 
                                                    CellPadding="4" DataKeyNames="codigo_dpr,codigo_Ppr,codigo_Art,IdUni" 
                                                    ForeColor="Black" GridLines="Horizontal" Width="90%" BackColor="White">
                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle BackColor="White" ForeColor="Black" />
                                                    <Columns>
                                                        <asp:BoundField DataField="descripcion_Ppr" HeaderText="Prog. Presupuestal" />
                                                        <asp:BoundField DataField="DesEstandar" HeaderText="Item" />
                                                        <asp:BoundField DataField="IdUni" HeaderText="iduni" Visible="False" />
                                                        <asp:BoundField DataField="Unidad" HeaderText="Unidad" />
                                                        <asp:BoundField DataField="DetDescripcion" HeaderText="Detalle Item" />
                                                        <asp:BoundField DataField="PreUnitario" HeaderText="Precio Unit.">
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad">
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Disponible" HeaderText="Disponible">
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                          
                                                        <%--treyes 04/07/2016--%>
                                                        <asp:TemplateField >
                                                            <ItemTemplate>
                                                            <asp:HyperLink ID="hlnkVerDetalle" runat="server" NavigateUrl="#" >Ver Detalle</asp:HyperLink>
                                                            <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnlVerDetalle1" TargetControlID="hlnkVerDetalle"
                                                                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                                            </cc1:ModalPopupExtender>
                                                            <asp:Panel ID="pnlVerDetalle1" runat="server" CssClass="modalPopup" align="center" style = "display:none">
                                                                <table cellspacing="0" cellpadding="2" border="1" style="color:Black;background-color:White;width:90%;border-collapse:collapse;">
                                                                        <tr style="color:White;background-color:#FF3300;font-weight:bold;"><th colspan = 2><%# Eval("DesEstandar") %></th></tr>
                                                                        <tr >
                                                                            <td>Presupuestado Acumulado</td>
                                                                            <td> <%# Eval("PresAcum") %></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td>Saldo Ejecutado Acumulado</td>
                                                                            <td> <%# Eval("SalEjeAcum") %> (- Pedido: <%# Eval("Pedido") %>)</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Saldo Presupuestado Ejecutable</td>
                                                                            <td> <%# Eval("SalPreEje") %> (- Fechas:  <%# Eval("MesIni")%> - <%# Eval("MesFin") %>: <%# Eval("PresAnt") %>)</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Transferido</td>
                                                                            <td> <%# Eval("transferido_dpr") %></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Disponible</td>
                                                                            <td style ="font-weight:bold"> <%# Eval("Disponible") %></td>
                                                                        </tr>
                                                                </table>
                                                                <br />
                                                                <asp:Button ID="btnClose" runat="server" Text="Salir" />
                                                            </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField >
                                                        
                                                        <asp:CommandField ShowSelectButton="True" />
                                                    </Columns>
                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#99CCFF" Font-Bold="True" ForeColor="Black" />
                                                    <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="White" ForeColor="Black" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </asp:Panel>
                                    </td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
  
                             </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table style="width:100%; background-color: #FFFFFF;">
                        <tr>
                            <td align="left">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table width="100%" border="0" cellpadding="2" cellspacing="0">
                                <tr bgcolor="#F5F9FC">
                                    <td >
                                        Prog. presupuestal<asp:CompareValidator ID="CompareValidator6" runat="server" 
                                            ControlToValidate="cboProgramaPresu" 
                                            ErrorMessage="Seleccione programa presupuestal" Operator="GreaterThan" 
                                            ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                    </td>
                                    <td >                                    
                                        <asp:DropDownList ID="cboProgramaPresu" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:RadioButtonList ID="rblMovimiento" runat="server" AutoPostBack="True" 
                                            RepeatDirection="Horizontal" ValidationGroup="BuscaItem" Visible="false">
                                            <asp:ListItem Value="I">Ingreso</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="E">Egreso</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel4" runat="server">
                                            Item<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                ControlToValidate="txtCodItem" 
                                                ErrorMessage="Busque el item que desea registrar" ValidationGroup="BuscaItem">*</asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                                ControlToValidate="txtConcepto" ErrorMessage="Selecione item a registrar" 
                                                ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                                                AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    <font style="color:Blue">Procesando. Espere un momento...</font>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtConcepto" runat="server" ValidationGroup="BuscaItem" 
                                            Width="500px"></asp:TextBox>
                                        <asp:ImageButton ID="ImgBuscarItems" runat="server" 
                                            ImageUrl="~/images/busca.gif" ValidationGroup="BuscaItem" />
                                        (clic aquí o presione enter)</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="Panel1" runat="server" Height="150px" ScrollBars="Vertical" 
                                            Width="100%">
                                            <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" 
                                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                                DataKeyNames="codigocon,tipo,iduni,especificaCantidad" ForeColor="Black" 
                                                ShowHeader="False" Width="98%" BackColor="White" GridLines="Horizontal">
                                                <Columns>
                                                    <asp:BoundField DataField="codigo" HeaderText="Código" />
                                                    <asp:BoundField DataField="concepto" HeaderText="Concepto" />
                                                    <asp:BoundField DataField="unidad" HeaderText="Unidad" />
                                                    <asp:BoundField DataField="precio" HeaderText="Precio">
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:CommandField ShowSelectButton="True" />
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                                <EmptyDataTemplate>
                                                    <b>No se encontraron items con el término de búsqueda</b>
                                                </EmptyDataTemplate>
                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr bgcolor="#F5F9FC">
                                    <td width="15%">
                                        Detalle o justificación</td>
                                    <td>
                                        <asp:TextBox ID="txtComentarioReq" runat="server" MaxLength="100" 
                                            TextMode="MultiLine" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15%">
                                        Especificaciones técnicas</td>
                                    <td>
                                        <asp:TextBox ID="txtEspecificaciones" runat="server" MaxLength="100" 
                                            TextMode="MultiLine" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:FileUpload ID="fuCargarArchivo" runat="server" size="50" ToolTip ="Se admiten archivos .doc,.xls,.docx,.xlsx,.jpg,.png,.pdf,.zip  Tam. máx 4MB"/>
                                        <a href="#" onclick="ClearFile();return false;" ><img src="../images/delete.png" border="0" alt="Link to this page"></a> 
                                        &nbsp;<asp:RegularExpressionValidator ID="revUpload" runat="server" ErrorMessage="Formato de archivo incorrecto" ControlToValidate="fuCargarArchivo" ValidationExpression= "^.+(.zip|.ZIP|.doc|.docx|.xls|.xlsx|.jpg|.png|.pdf)$" ValidationGroup ="Guardar" />
       
                                    </td>
                                </tr>

                                <tr bgcolor="#F5F9FC">
                                    <td>
                                        </td>
                                    <td>
                                        <asp:TextBox ID="lblUnidad" runat="server" BackColor="#F3F3F3" 
                                            Width="90px" Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr bgcolor="#F5F9FC">
                                    <td>
                                        <asp:Label ID="lblTexto" runat="server" Text="Precio Unitario (S/.)"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                            ControlToValidate="txtPrecioUnit" ErrorMessage="Ingrese un precio válido" 
                                            ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPrecioUnit" runat="server" Width="90px"></asp:TextBox>
                                        <asp:Label ID="lblDescripcionUnidad" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblValores" runat="server" Text="Cantidad" Font-Bold="True"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                            ControlToValidate="txtCantidad" ErrorMessage="Ingrese una cantidad válida" 
                                            ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCantidad" runat="server" Width="90px"></asp:TextBox>
                                    </td>                                        
                                </tr>
                              
                                <tr bgcolor="#F5F9FC">
                                    <td>
                                        Fecha</td>
                                    <td>
                                        <asp:TextBox ID="TxtFechaEsperada" runat="server" Width="80px"></asp:TextBox>
                                        <input ID="Button2" class="cunia" 
                                            onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFechaEsperada,'dd/mm/yyyy')" 
                                            style="height: 22px" type="button" /><asp:RequiredFieldValidator 
                                            ID="RequiredFieldValidator11" runat="server" ControlToValidate="TxtFechaEsperada" 
                                            ErrorMessage="Ingrese fecha de nacimiento" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        &nbsp;</td>
                                </tr>
                                <tr bgcolor="#F5F9FC">
                                    <td>Despachar a</td>
                                    <td>
                                        <asp:DropDownList ID="cboPerDespachar" runat="server">
                                        </asp:DropDownList>
                                    </td>    
                                </tr>
                                <tr bgcolor="#F5F9FC">
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:RadioButtonList ID="rblModoDistribucion" runat="server" 
                                            AutoPostBack="True" RepeatDirection="Horizontal" 
                                            ValidationGroup="BuscaItem" Visible="False">
                                            <asp:ListItem Selected="True" Value="C">Cantidad</asp:ListItem>
                                            <asp:ListItem Value="P">Porcentaje</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                              
                            </table>
                                </ContentTemplate>
                    </asp:UpdatePanel>
                    
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="cmdGuardar" runat="server" Text="   Guardar detalle" 
                                    BorderStyle="Outset" CssClass="guardar" Width="148px" 
                                    ValidationGroup="Guardar" Height="26px" />
                            &nbsp;<asp:Button ID="cmdEnviar" runat="server" Text="  Enviar" 
                                    BorderStyle="Outset" CssClass="salir" Width="87px" 
                        Height="26px" Visible="False" />                                
            <asp:TextBox ID="txtNuevoPedido" runat="server" Visible="False">0</asp:TextBox>
                            </td>
                        </tr>
                        </table>
                </td>
            </tr>
            &nbsp; &nbsp; 
                            &nbsp;&nbsp;</table>
    
    </div>
    <asp:TextBox ID="txtCodItem" runat="server" Visible="False"></asp:TextBox>
            <asp:TextBox ID="detPedNuevo" runat="server" Visible="False">0</asp:TextBox>
            <asp:HiddenField ID="hddForzar" runat="server" Value="0" />
            <asp:HiddenField ID="HdUnidad" runat="server" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="BuscaItem" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="BuscarCecos" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
    <asp:ValidationSummary ID="ValidationSummary4" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Distribuir" />
    <asp:ValidationSummary ID="ValidationSummary5" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Presupuesto" />
                            </br>

    </form>
</body>
</html>
