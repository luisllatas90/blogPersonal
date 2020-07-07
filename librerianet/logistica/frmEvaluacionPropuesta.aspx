<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEvaluacionPropuesta.aspx.vb" Inherits="logistica_frmEvaluacionPropuesta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Evaluación Propuesta</title>
        <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    
    <script src="../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../private/jq/jquery.mascara.js" type="text/javascript"></script>
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
    <style type="text/css">
        .style1
        {
            width: 51px;
        }
        .style2
        {
            height: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <%  response.write(clsfunciones.cargacalendario) %>
    <div>
    
        <asp:ScriptManager ID="smEvaluacionPropuesta" runat="server">
        </asp:ScriptManager>
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:UpdatePanel ID="upCabeceraBusqueda" runat="server">
                        <ContentTemplate>
                            <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;">
                                <tr>
                                    <td colspan="8" height="20px" 
                                        style="background-color: #e8eef7; color: #3366CC; font-weight: bold;">
                                        &nbsp;&nbsp; Evaluación de Subasta Inversa</td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTitCategoria" runat="server" Text="Categoría"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlListadoCategoria" runat="server" 
                                            ValidationGroup="Subasta" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTitFechaInicio" runat="server" Text="Fecha Inicio"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFechaInicio" runat="server" 
                                            ValidationGroup="Subasta"></asp:TextBox>
                                        <input ID="btnFechaInicio" class="cunia" 
                                            onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaInicio,'dd/mm/yyyy')" 
                                            type="button" /></td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTitFechaFin" runat="server" Text="Fecha Fin"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFechaFin" runat="server" 
                                            ValidationGroup="Subasta"></asp:TextBox>
                                        <input ID="btnFechaFin" class="cunia" 
                                            onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaFin,'dd/mm/yyyy')" 
                                            type="button" /></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hfCodSubasta" runat="server" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImgBuscarSubasta" runat="server" 
                                            ImageUrl="~/images/busca.gif" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td height="200px" valign="top">
                    <asp:UpdatePanel ID="upSubasta" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvSubastas" runat="server" 
                        AutoGenerateColumns="False" CellPadding="4" 
    ForeColor="#333333" Width="100%" 
                                        BorderColor="#99BAE2" 
    BorderStyle="Solid" BorderWidth="1px" 
                                AllowPaging="True" PageSize="5" GridLines="Horizontal">
                                <Columns>
                                    <asp:BoundField DataField="codSubasta" HeaderText="Cod" >
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="desCategoria" HeaderText="Categoría" >
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecRegistro" 
                                            HeaderText="Registro" >
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecInicio" 
                                            HeaderText="Inicio" >
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecFin" 
                                            HeaderText="Fin" >
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Precio Base">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkPrecioBase" runat="server" 
                                                    Checked = '<%# Bind("precioBase") %>' Enabled ="false"/>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mejor Oferta">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkMejorOferta" runat="server" 
                                                    Checked ='<%# Bind("mejorOferta") %>' Enabled ="false"/>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="evaluacion" HeaderText="Estado" >
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfIdPro" runat="server" Value ='<%# Bind("idPro") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField SelectText="" ShowSelectButton="True" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No se han registrado Subastas Inversas con los criterios ingresados.
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" 
                                        BorderStyle="Solid" BorderWidth="1px" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
            </tr>
            <tr>
                <td height="220px" valign="top">
                                    <asp:UpdatePanel ID="upDatosSubasta" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                       
                                <asp:Menu
                                        ID="Menu1"
                                        runat="server"
                                        Orientation="Horizontal"
                                        StaticEnableDefaultPopOutImage="False"
                                        OnMenuItemClick="Menu1_MenuItemClick">
                                    <Items>
                                        <asp:MenuItem ImageUrl="../images/seMontoArticulo.JPG" Text=" " Value="0"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="../images/unsOfertaProveedor.JPG" Text=" " Value="1"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="../images/unsNegociacion.JPG" Text=" " Value="2"></asp:MenuItem>
                                    </Items>
                                </asp:Menu>

                                <asp:MultiView 
                                    ID="MultiView1"
                                    runat="server"
                                    ActiveViewIndex="0"  >
                                   <asp:View ID="Tab1" runat="server"  >
                                        
                                           <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;" width="100%" height="100%" cellpadding=0 cellspacing=0>
                                            <tr valign="top">
                                                <td class="TabArea" style="width: 100%">
                                                    <asp:Panel ID="pnlTab1" runat="server" Height = "213px" ScrollBars="Vertical">
                                                        <table style="width: 96%;">
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Registro:</td>
                                                                <td>
                                                                    <asp:Label ID="lblRegistroTab1" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    Categoría:</td>
                                                                <td>
                                                                    <asp:Label ID="lblCategoriaTab1" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    Vigencia:</td>
                                                                <td>
                                                                    <asp:Label ID="lblVigenciaTab1" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">
                                                                    <asp:GridView ID="gvArticulos" runat="server" AutoGenerateColumns="False" 
                                                                        BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                                                        ForeColor="#333333" Width="100%">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="nombrePro" HeaderText="Proveedor" />
                                                                            <asp:BoundField DataField="descripcionArt" HeaderText="Artículo" />
                                                                            <asp:BoundField DataField="cantidad" HeaderText="Cantidad">
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="precioBase" HeaderText="Precio Base">
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="monto" HeaderText="Precio X Unidad">
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="total" HeaderText="Total">
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                        <EmptyDataTemplate>
                                                                            No se han registrado Proveedores con la Categoría seleccionada.
                                                                        </EmptyDataTemplate>
                                                                        <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                                            BorderWidth="1px" ForeColor="#3366CC" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel> 
                                                </td>
                                            </tr>
                                        </table>
                                            
                                        
                                     </asp:View>
                                    <asp:View ID="Tab2" runat="server">                                    
                                        <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;" width="100%" height="100%" cellpadding=0 cellspacing=0>
                                            <tr valign="top">
                                                <td class="TabArea" style="width: 100%">
                                                <asp:Panel ID="pnlTab2" runat="server" Height = "213px" ScrollBars="Vertical">
                                                    <table style="width: 96%;">
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Registro:</td>
                                                            <td>
                                                                <asp:Label ID="lblRegistroTab2" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                Categoría:</td>
                                                            <td>
                                                                <asp:Label ID="lblCategoriaTab2" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                Vigencia:</td>
                                                            <td>
                                                                <asp:Label ID="lblVigenciaTab2" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                <asp:HiddenField ID="hfFilas" runat="server" />
                                                            </td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td align="right">
                                                                <asp:ImageButton ID="ibtnGrabar" runat="server" ImageUrl="~/images/dikette.gif" 
                                                                    ToolTip="Grabar" Width="30px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6">
                                                                <asp:GridView ID="gvOfertaResumen" runat="server" AutoGenerateColumns="False" 
                                                                    BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                                                    ForeColor="#333333" Width="100%">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="idPro" HeaderText="id" />
                                                                        <asp:BoundField DataField="nombrePro" HeaderText="Proveedor" />
                                                                        <asp:BoundField DataField="cantArt" HeaderText="Artículos" />
                                                                        <asp:BoundField DataField="cantXArt" HeaderText="Cantidad" >
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="monto" HeaderText="Importe" >
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="total" HeaderText="Total">
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Documentos">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ibtnVer" runat="server" Height="15px" 
                                                                                    ImageUrl="~/images/busca.gif" onclick="ibtnVer_Click" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Seleccionar" >
                                                                            <ItemTemplate>
                                                                                <asp:Literal ID="rbtnMarkup" runat="server"></asp:Literal>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" align ="center">
                                                            <asp:Button ID="btnRegresar" runat="server" BackColor="#EFEFEF" 
                                                                    BorderColor="#0099FF" BorderStyle="Ridge" Text="Regresar" Visible="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6"  align ="center">
                                                                <asp:GridView ID="gvDocumentos" runat="server" AutoGenerateColumns="False" 
                                                                    BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                                                    ForeColor="#333333" Width="50%" Visible ="false">
                                                                    <Columns>
                                                                        <asp:ImageField DataImageUrlField="extension" DataImageUrlFormatString="~/images/ext/{0}.gif"
                                                HeaderText="" ConvertEmptyStringToNull="False">
                                                                            <HeaderStyle Width="1%" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                        </asp:ImageField>
                                                                        <asp:HyperLinkField DataNavigateUrlFields="documento" 
                                                                            DataNavigateUrlFormatString="{0}"
                                                                            DataTextField="nombre" HeaderText="Documento" Target="_blank" />
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                                </asp:GridView>      
                                                            </td>
                                                        </tr>
                                                    </table>
                                                     </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                       
                                    </asp:View>
                                    
                                      <asp:View ID="View1" runat="server">                                    
                                        <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;" width="100%" height="100%" cellpadding=0 cellspacing=0>
                                            <tr valign="top">
                                                <td class="TabArea" style="width: 100%">
                                                <asp:Panel ID="pnlTab3" runat="server" Height = "213px" ScrollBars="Vertical">
                                                    <table style="width: 96%;">
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Registro:</td>
                                                            <td>
                                                                <asp:Label ID="lblRegistroTab3" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                Categoría:</td>
                                                            <td>
                                                                <asp:Label ID="lblCategoriaTab3" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                Vigencia:</td>
                                                            <td>
                                                                <asp:Label ID="lblVigenciaTab3" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" align="center" class="style2">
                                                                <asp:Button ID="btnAgregarMensaje" runat="server" BackColor="#EFEFEF" 
                                                                    BorderColor="#0099FF" BorderStyle="Ridge" Text="Dejar mensaje" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6">
                                                                <table style="width:100%;">
                                                                    <tr>
                                                                        <td align="center" colspan="6">
                                                                            <asp:Panel ID="pnlMensaje" runat="server" BorderColor="#EFEFEF" 
                                                                                BorderWidth="1px" Visible="False" Width="70%">
                                                                                <table style="width:100%">
                                                                                    <tr>
                                                                                        <td align="left" class="style1">
                                                                                            &nbsp;</td>
                                                                                        <td align="left">
                                                                                            &nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" class="style1">
                                                                                            Proveedor:</td>
                                                                                        <td align="left">
                                                                                            <asp:DropDownList ID="ddlProveedorSubasta" runat="server" Width="97%">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" class="style1">
                                                                                            Asunto:</td>
                                                                                        <td align="left">
                                                                                            <asp:TextBox ID="txtAsunto" runat="server" Height="17px" Width="98%"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" class="style1" valign="top">
                                                                                            Mensaje:</td>
                                                                                        <td align="left">
                                                                                            <asp:TextBox ID="txtMensaje" runat="server" Height="52px" TextMode="MultiLine" 
                                                                                                Width="98%"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="center" colspan="2" valign="top">
                                                                                            <table style="width: 50%;">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:ImageButton ID="ibtnGuardarMensaje" runat="server" Height="25px" 
                                                                                                            ImageUrl="~/images/dikette.gif" />
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:ImageButton ID="ibtnEliminar" runat="server" Height="20px" 
                                                                                                            ImageUrl="~/images/eliminar.gif" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" colspan="6">
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" colspan="6">
                                                                            Mensajes por proveedor:</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" colspan="6">
                                                                            <asp:DropDownList ID="ddlProveedorMensaje" runat="server" AutoPostBack="True" 
                                                                                Width="50%">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" colspan="6">
                                                                            <asp:DataList ID="dlMensajes" runat="server">
                                                                                <ItemTemplate>
                                                                                    <table class="contornotabla">
                                                                                        <tr>
                                                                                            <td width="2%">
                                                                                                <img alt="" src="../images/rpta.GIF" /></td>
                                                                                            <td width="58%">
                                                                                                Fecha:
                                                                                                <asp:Label ID="lblFecha" runat="server" ForeColor="Black" 
                                                                                                    Text='<%# Eval("fecRegistro") %>' />
                                                                                            </td>
                                                                                            <td rowspan="3" valign="top" width="40%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td>
                                                                                                De:<asp:Label ID="de" runat="server" ForeColor="Black" 
                                                                                                    Text='<%# iif(Eval("registra") = "per", Eval("personal"), Eval("proveedor")) %>' />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td>
                                                                                                Para:<asp:Label ID="para" runat="server" ForeColor="Black" 
                                                                                                    Text='<%# iif(Eval("registra") = "per", Eval("proveedor"), Eval("personal")) %>' />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                &nbsp;</td>
                                                                                            <td>
                                                                                                Asunto:
                                                                                                <asp:Label ID="asunto" runat="server" Font-Bold="True" ForeColor="Black" 
                                                                                                    Text='<%# Eval("asunto") %>' />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td colspan="2">
                                                                                                <asp:Label ID="comentario" runat="server" ForeColor="#003399" 
                                                                                                    Text='<%# Eval("comentario") %>' />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <br />
                                                                                </ItemTemplate>
                                                                            </asp:DataList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                     </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                       
                                    </asp:View>
                                </asp:MultiView>
                                

                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="ibtnGrabar" />
                                        </Triggers>
                    </asp:UpdatePanel>

                    </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
