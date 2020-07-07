<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAprobacionSubasta.aspx.vb" Inherits="logistica_frmAprobacionSubasta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Busqueda de Subasta Inversa</title>
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

        function mostrarArticulos() {
            var pnlArticulos = document.getElementById("pnlArticulos");
            var pnlProveedores = document.getElementById("pnlProveedores");
            pnlArticulos.style.visibility = "visible";
            pnlProveedores.style.visibility = "hidden";
        }
        
        function mostrarProveedores() {
            var pnlArticulos = document.getElementById("pnlArticulos");
            var pnlProveedores = document.getElementById("pnlProveedores");
            pnlArticulos.style.visibility = "hidden";
            pnlProveedores.style.visibility = "visible";
        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 179px;
        }
        .style2
        {
            width: 170px;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <%  response.write(clsfunciones.cargacalendario) %>
    <asp:ScriptManager ID="smBusquedaSubastaInversa" runat="server">
        </asp:ScriptManager>
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:UpdatePanel ID="upCabeceraBusqueda" runat="server">
                    <ContentTemplate>
                     <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;">
                        <tr>
                            <td style="background-color: #e8eef7; color: #3366CC; font-weight: bold;" 
                                colspan="8" height="20px">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Aprobar Subasta</td>
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
                                <asp:Label ID="lblTitFechaInicio" runat="server" 
                        Text="Fecha Inicio"></asp:Label>
                                </td>
                            <td>
                                <asp:TextBox ID="txtFechaInicio" runat="server" Height="15px" 
                                    ValidationGroup="Subasta"></asp:TextBox>
                                <input id="btnFechaInicio" 
                                    onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaInicio,'dd/mm/yyyy')" 
                                    class="cunia" type="button" /></td>
                            <td>
                                </td>
                            <td>
                                <asp:Label ID="lblTitFechaFin" runat="server" Text="Fecha Fin"></asp:Label>
                                </td>
                            <td>
                                <asp:TextBox ID="txtFechaFin" runat="server" Height="15px" 
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
                                <asp:HiddenField ID="hfCodSubasta" runat="server" />
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
                <td height="230px" valign="top">
                    <asp:UpdatePanel ID="upResultadoBusqueda" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvSubastas" runat="server" 
                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" Width="100%" 
                                        BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" 
                                AllowPaging="True" PageSize="5" GridLines="Horizontal">
                                <Columns>
                                    <asp:BoundField DataField="codSubasta" HeaderText="Cod" />
                                    <asp:BoundField DataField="desCategoria" HeaderText="Categoría" />
                                    <asp:BoundField DataField="fecRegistro" 
                                            HeaderText="Registro" />
                                    <asp:BoundField DataField="fecInicio" 
                                            HeaderText="Inicio" />
                                    <asp:BoundField DataField="fecFin" 
                                            HeaderText="Fin" ></asp:BoundField>
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
                                    <asp:BoundField DataField="estado" HeaderText="Estado" />
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
                <td height="190px" valign="top">
                                  
                    <asp:UpdatePanel ID="upDatosSubasta" runat="server">
                        <ContentTemplate>
                       <asp:Panel ID="pnlDetalle" runat="server" CssClass ="hidden">  
                                <asp:Menu
                                        ID="Menu1"
                                        Width="80%"
                                        runat="server"
                                        Orientation="Horizontal"
                                        StaticEnableDefaultPopOutImage="False"
                                        OnMenuItemClick="Menu1_MenuItemClick">
                                    <Items>
                                        <asp:MenuItem ImageUrl="../images/seArticulosSubasta.JPG" Text=" " Value="0"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="../images/unsProveedoresParticapantes.JPG" Text=" " Value="1"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="../images/unsPedidosIncluidos.JPG" Text=" " Value="2"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="../images/unsAprobarSubasta.JPG" Text=" " Value="3"></asp:MenuItem>
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
                                                    <asp:Panel ID="pnlTab1" runat="server" Height = "186px" ScrollBars="Vertical">
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
                                                            <td>&nbsp;
                                                                
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>&nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6">
                                                                <asp:GridView ID="gvArticulos" runat="server" AutoGenerateColumns="False" 
                                                                    BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                                                    ForeColor="#333333" Width="100%"  GridLines ="Horizontal">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="idArt" HeaderText="ID" />
                                                                        <asp:BoundField DataField="descripcionArt" HeaderText="Artículo" />
                                                                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="total" HeaderText="Total">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="precioBase" HeaderText="Precio Base">
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
                                                <asp:Panel ID="pnlTab2" runat="server" Height = "186px" ScrollBars="Vertical">
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
                                                            <td colspan="6">
                                                                <asp:GridView ID="gvProveedores" runat="server" AutoGenerateColumns="False" 
                                                                    BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                                                    ForeColor="#333333" Width="100%" GridLines ="Horizontal">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="idPro" HeaderText="Cod" />
                                                                        <asp:BoundField DataField="nombrePro" HeaderText="Nombre" />
                                                                        <asp:BoundField DataField="rucPro" HeaderText="R.U.C." />
                                                                        <asp:BoundField DataField="direccionPro" HeaderText="Dirección" />                                                                        
                                                                        <asp:TemplateField HeaderText="">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hfEmailPro" runat="server" Value ='<%# Bind("emailPro") %>' />                                                                                
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="telefonoPro" HeaderText="Teléfono" Visible="False" />
                                                                        <asp:BoundField DataField="faxPro" HeaderText="Fax" Visible="False" />
                                                                        <asp:BoundField DataField="emailPro" HeaderText="Email" Visible="False" />
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
                                    <asp:View ID="Tab3" runat="server">
                                    
                                        <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;" width="100%" height="100%" cellpadding=0 cellspacing=0>
                                            <tr valign="top">
                                                <td class="TabArea" style="width: 100%">
                                                <asp:Panel ID="pnlTab3" runat="server" Height = "186px" ScrollBars="Vertical">
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
                                                            <td colspan="6">
                                                                <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="False" 
                                                                    BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                                                    ForeColor="#333333" Width="100%" AllowPaging="True" PageSize="5"  GridLines ="Horizontal">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="codigo_Ped" HeaderText="Código" />
                                                                        <asp:BoundField DataField="descripcionArt" HeaderText="Artículo" />
                                                                        <asp:BoundField DataField="cantidadPedida" HeaderText="Pedida">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="cantidadAtendida" HeaderText="Atendida">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="cantidadComprar" HeaderText="Comprar">
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
                                    <asp:View ID="Tab4" runat="server">
                                    
                                        <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;" width="100%" height="100%" cellpadding=0 cellspacing=0>
                                            <tr valign="top">
                                                <td class="TabArea" style="width: 100%">
                                                <asp:Panel ID="pnlTab4" runat="server" Height = "186px" ScrollBars="Vertical">
                                                    <table style="width: 96%;">
                                                        <tr>
                                                            <td class="style2">
                                                                &nbsp;</td>
                                                            <td class="style1">
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style2">
                                                                &nbsp;</td>
                                                            <td class="style1">
                                                                Categoría:</td>
                                                            <td>
                                                                <asp:Label ID="lblCategoriaTab4" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style2">
                                                                &nbsp;</td>
                                                            <td class="style1">
                                                                Registro:</td>
                                                            <td>
                                                                <asp:Label ID="lblRegistroTab4" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style2">
                                                                &nbsp;</td>
                                                            <td class="style1">
                                                                Fecha Inicio:</td>
                                                            <td>
                                                                <asp:Label ID="lblFecInicioTab4" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style2">
                                                                &nbsp;</td>
                                                            <td class="style1">
                                                                Fecha Fin:</td>
                                                            <td>
                                                                <asp:Label ID="lblFecFinTab4" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style2">
                                                                &nbsp;</td>
                                                            <td class="style1">
                                                                Cantidad de Artículos:</td>
                                                            <td>
                                                                <asp:Label ID="lblCantArticulos" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style2">
                                                                &nbsp;</td>
                                                            <td class="style1">
                                                                Proveedores:</td>
                                                            <td>
                                                                <asp:Label ID="lblProveedores" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style2">
                                                                &nbsp;</td>
                                                            <td class="style1">
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="4">
                                                                <asp:Button ID="btnAprobar" runat="server" BackColor="#E8EEF7" 
                                                                    BorderColor="#0099FF" BorderStyle="Ridge" Text="Publicar Subasta" />
                                                            </td>
                                                        </tr>
                                                        </table>
                                                        </asp:Panel>
                                                        </table>
                                                        </asp:View>
                                                        
                                </asp:MultiView>
                           </asp:Panel>     

                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnAprobar" />
                        </Triggers>
                    </asp:UpdatePanel>
                
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
