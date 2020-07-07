<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmNuevaSubastaInversa.aspx.vb" Inherits="librerianet_logistica_frmNuevaSubastaInversa" EnableEventValidation="false" %>

<%@ Register assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Nueva Subasta Inversa</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    
<script src="../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../private/jq/jquery.mascara.js" type="text/javascript"></script>
        
    <style type="text/css">
        TBODY {
	display: table-row-group;
}
a:Link {
	color: #000000;
	text-decoration: none;
}
        </style>
        
        <script language="javascript" type="text/javascript">
        
            $(document).ready(function() {
                jQuery(function($) {
                    $("#txtFechaInicio").mask("99/99/9999");

                });
            })

            $(document).ready(function() {
                jQuery(function($) {
                    $("#txtFechaFin").mask("99/99/9999");

                });
            })

        </script>
</head>
<body>
    <form id="form1" runat="server">
    <%  response.write(clsfunciones.cargacalendario) %>
        <asp:ScriptManager ID="smNuevaSubastaInversa" runat="server">
        </asp:ScriptManager>
        
            <table border="0" style="width:100%">
            
            <tr>
                <td>
                <asp:UpdatePanel ID="upCabecera" runat="server">
                    <ContentTemplate>
                    <table border="0" 
                            style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;">
                        <tr>
                            <td colspan="5" 
                                style="width: 100%; background-color: #e8eef7; color: #3366CC; font-weight: bold;">
                                &nbsp; Nueva Subasta Inversa</td>
                        </tr>
                        <tr>
                            <td>
                            &nbsp;
                                </td>
                            <td colspan="4">
                                </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitUsuario" runat="server" Text="Usuario que Registra" 
                                    Width="164px"></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lblUsuario" runat="server" Width="500px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitCargoUsuario" runat="server" 
                        Text="Cargo Usuario" Width="164px"></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lblUsuarioCargo" runat="server" Width="500px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:HiddenField ID="hfcodSubasta" runat="server" />
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:HiddenField ID="hfidUsuario" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="hfcodCategoria" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitFechaInicio" runat="server" 
                        Text="Fecha Inicio" Width="164px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaInicio" runat="server" Width="150px" 
                                    ValidationGroup="Subasta"></asp:TextBox>
                                <input id="btnFechaInicio" onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaInicio,'dd/mm/yyyy')" class="cunia" type="button" /><asp:RequiredFieldValidator 
                                    ID="rfvFechaInicio" runat="server" ControlToValidate="txtFechaInicio" 
                                    ErrorMessage="Debe de ingresar la Fecha de Inicio" ValidationGroup="Enviar">*</asp:RequiredFieldValidator></td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td rowspan="2" align="right" valign="top">
                                <asp:Button ID="btnGuardar" runat="server" BorderStyle="Outset" 
                                    CssClass="salir" Height="26px" Text="Enviar" Width="87px" 
                                    ValidationGroup="Enviar" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitFechaFin" runat="server" Text="Fecha Fin" Width="164px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaFin" runat="server" 
                                    ValidationGroup="Subasta" Width="150px"></asp:TextBox>
                                <input ID="btnFechaFin" class="cunia" 
                                    onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaFin,'dd/mm/yyyy')" 
                                    type="button" /><asp:RequiredFieldValidator ID="rfvFechaFin" runat="server" 
                                    ControlToValidate="txtFechaFin" ErrorMessage="Debe de ingresar la Fecha de Fin" 
                                    ValidationGroup="Enviar">*</asp:RequiredFieldValidator></td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                                                <tr>
                            <td valign="top">
                                <asp:Label ID="lblTitDescripcion" runat="server" Text="Descripción" 
                                    Width="164px"></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtDescripcion" runat="server" Height="32px" 
                                    TextMode="MultiLine" Width="98%"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                    ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" 
                                    ErrorMessage="Debe de ingresar la Descripción" ValidationGroup="Enviar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                <asp:Label ID="lblTitListadoCategoria" runat="server" 
                                    Text="Listado de Categorias" Width="164px"></asp:Label>
                            </td>
                            <td>
                    <asp:DropDownList ID="ddlListadoCategoria" runat="server" AutoPostBack="True" 
                                    ValidationGroup="Subasta" Width="200px">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="cvCategoria" runat="server" 
                                            ControlToValidate= "ddlListadoCategoria"
                                            ErrorMessage="Seleccione Categoría" Operator="GreaterThan" 
                                            ValidationGroup="Enviar" ValueToCompare="0">*</asp:CompareValidator>
                                
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                        <asp:CheckBox ID="chkMostrarPrecioBase" runat="server" 
                                    Text="Mostrar Precio Base" 
    TextAlign="Left" AutoPostBack="True" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkMostrarMejorOferta" runat="server" 
                                    Text="Mostrar Mejor Oferta" TextAlign="Left" />
                            </td>
                        </tr>
                        
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnGuardar" />
                    </Triggers>
                    </asp:UpdatePanel>
                    
                    </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="upDetalle" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlDetalle" runat="server" Visible="False">
                        
                    <table >
                        <tr>
                            <td style="width: 50%; background-color: #e8eef7; color: #3366CC; font-weight: bold;">
                                Proveedores</td>
                            <td>
                                &nbsp;</td>
                            <td style="width: 50%; background-color: #e8eef7; color: #3366CC; font-weight: bold;">
                                Artículos</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlProveedores" runat="server" Height="170px" ScrollBars="Auto">
                                    <asp:GridView ID="gvProveedores" runat="server" 
                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" Width="98%" 
                                        BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px">
                                    <Columns>
                                        <asp:BoundField DataField="idPro" HeaderText="Cod" />
                                        <asp:BoundField DataField="nombrePro" 
                                            HeaderText="Nombre" />
                                        <asp:BoundField DataField="rucPro" 
                                            HeaderText="R.U.C." />
                                        <asp:BoundField DataField="direccionPro" 
                                            HeaderText="Dirección" Visible="False" >
                                        </asp:BoundField>
                                        <asp:BoundField DataField="telefonoPro" HeaderText="Teléfono" Visible="False" />
                                        <asp:BoundField DataField="faxPro" 
                                            HeaderText="Fax" Visible="False" >
                                        </asp:BoundField>
                                        <asp:BoundField DataField="emailPro" 
                                            HeaderText="Email" Visible="False" >
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSeleccionar" runat="server" Checked='<%# Bind("sel") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
            No se han registrado Proveedores con la Categoría seleccionada.
        </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" />
                                    
                                </asp:GridView>
                            
                                </asp:Panel>
                            
                            </td>
                            <td>
                                </td>
                            <td>
                                <asp:Panel ID="pnlArticulos" runat="server" Height="170px" ScrollBars="Auto">
                                
                                    <asp:GridView ID="gvArticulos" runat="server" AutoGenerateColumns="False" 
                                        CellPadding="4" ForeColor="#333333" Width="95%"
                                        BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px">
                                        <Columns>
                                            <asp:BoundField DataField="codigo_Ped" HeaderText="CodPed" />
                                            <asp:BoundField DataField="idArt">
                                                
                                            </asp:BoundField>
                                            <asp:BoundField DataField="descripcionArt" HeaderText="Artículo" />
                                            <asp:BoundField DataField="cantidadPedida" HeaderText="Pedido">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cantidadAtendida" HeaderText="Atendida">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cantidadComprar" HeaderText="Comprar">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSeleccionar" runat="server" AutoPostBack="True"  Checked='<%# Bind("sel") %>' 
                                                        oncheckedchanged="chkSeleccionar_CheckedChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
            No se han registrado Artículos con la Categoría seleccionada
        </EmptyDataTemplate>
                                        <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" />
                                        
                                    </asp:GridView>
                            
                                </asp:Panel>
                            
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            
                                <asp:Panel ID="pnlDetalleInferior" runat="server" Visible ="false">
                                
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 100%; background-color: #e8eef7; color: #3366CC; font-weight: bold;">
                                            Artículos Seleccionados</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlDetalleSubasta" runat="server" Height="200px" 
                                                ScrollBars="Auto">
                                                <asp:GridView ID="gvDetalleSubasta" runat="server" AutoGenerateColumns="False" 
                                                    CellPadding="4" ForeColor="#333333" Width="95%"
                                                    BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px">
                                                    <Columns>
                                                        <asp:BoundField DataField="idArt" HeaderText="idArt" />
                                                        <asp:BoundField DataField="descripcionArt" HeaderText="Artículo" />
                                                        <asp:BoundField DataField="cantidad" HeaderText="Comprar">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Total ">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtTotalComprar" runat="server" CssClass="monto" 
                                                                    Text='<%# Bind("total") %>' Width="80px" ></asp:TextBox>

                                                                <asp:RequiredFieldValidator ID="rfvTotal" runat="server" 
                                                                    ControlToValidate="txtTotalComprar" 
                                                                    ErrorMessage="Debe de ingresar el monto Total" ValidationGroup="Enviar">*</asp:RequiredFieldValidator>

                                                                <asp:RangeValidator ID="rvTotal" runat="server" 
                                                                    ControlToValidate="txtTotalComprar" 
                                                                    ErrorMessage="Debe ingresar un monto mayor a cero" MaximumValue="99999999" 
                                                                    MinimumValue="1" ValidationGroup="Enviar">*</asp:RangeValidator>

                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Precio Base">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPrecioBase" runat="server" CssClass="monto" 
                                                                     Width="80px" Text='<%# Bind("precioBase") %>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvPrecioBase" runat="server" 
                                                                    ControlToValidate="txtPrecioBase" 
                                                                    ErrorMessage="Debe de ingresar el precio base" ValidationGroup="Enviar">*</asp:RequiredFieldValidator>

                                                                <asp:RangeValidator ID="rvPrecioBase" runat="server" 
                                                                    ControlToValidate="txtPrecioBase" 
                                                                    ErrorMessage="Debe ingresar un precio mayor a cero" MaximumValue="99999999" 
                                                                    MinimumValue="1" ValidationGroup="Enviar">*</asp:RangeValidator>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    
                                                    <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" />
                                                    
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                </asp:Panel>
                                <br />
                            </td>
                        </tr>
                    </table>
                    
                        </asp:Panel>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                
                </td>
            </tr>
            </table>
    
    </div>
    <asp:ValidationSummary ID="vsSubastaInversa" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Enviar" />
    </form>
</body>
</html>
