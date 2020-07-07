<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaConvocatoriaProveedor.aspx.vb" Inherits="logistica_frmListaConvocatoriaProveedor" debug="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista de Convocatorias</title>
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
                                &nbsp;&nbsp; Búsqueda de Subasta Inversa&nbsp;</td>
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
                                <asp:HiddenField ID="idPro" runat="server" Value="550" />
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
            <td>
                <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                                      
                           <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;">
                        <tr>
                            <td style="background-color: #e8eef7; color: #3366CC; font-weight: bold;" 
                                colspan="6" height="20px">
                                &nbsp;&nbsp; Subasta Inversa&nbsp;</td>
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
                        </tr>
                        <tr>
                            <td>
                                Registro:</td>
                            <td>
                                <asp:Label ID="lblRegistro" runat="server"></asp:Label>
                            </td>
                            <td>
                                Categoría:</td>
                            <td>
                                <asp:Label ID="lblCategoria" runat="server"></asp:Label>
                            </td>
                            <td>
                                Vigencia:</td>
                            <td>
                                <asp:Label ID="lblVigencia" runat="server"></asp:Label>
                            </td>
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

                        </tr>
                     </table>
                     
                </ContentTemplate>
                </asp:UpdatePanel> 
            </td>
            </tr>
            <tr>
            <td>
                
                           <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;">
                        <tr>
                            <td style="background-color: #e8eef7; color: #3366CC; font-weight: bold;" 
                                colspan="6" height="20px">
                                &nbsp;&nbsp; Documentos</td>
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
                        </tr>
                        <tr>
                            <td>
                                Nombre:</td>
                            <td>
                                                                    <asp:TextBox ID="txtNombreFile" 
                                    runat="server"></asp:TextBox>
                            </td>
                            <td>
                                Archivo:</td>
                            <td>
                                                                    <asp:FileUpload ID="FileArchivo" 
                                    runat="server" BorderStyle="Solid" 
                                                                        BorderWidth="1px" />
                                                                    <asp:Button ID="btnAgregar" runat="server" BackColor="#EFEFEF" 
                                                                        BorderColor="#0099FF" 
                                    BorderStyle="Ridge" Text="+" Height="20px" />
                            </td>
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
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">
                                                                    <asp:UpdatePanel ID="upDocumentos" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:GridView ID="gvFile" runat="server" AutoGenerateColumns="False" 
                                                                        BorderColor="#99BAE2" 
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                                                        
    ForeColor="#333333" Width="80%" AllowPaging="True" GridLines="Horizontal" PageSize="5">
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ibtnEnviar" runat="server" ImageUrl="~/images/back.gif" 
                                                                                                CssClass= '<%# iif(Eval("estado") = "E", "hidden", "") %>' 
                                                                                                ToolTip ="Enviar Documento" onclick="ibtnEnviar_Click" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="1%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:ImageField DataImageUrlField="extension" DataImageUrlFormatString="~/images/ext/{0}.gif"
                                                                                        HeaderText="" ConvertEmptyStringToNull="False">
                                                                                        <HeaderStyle Width="1%" />
                                                                                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                    </asp:ImageField>
                                                                                    <asp:HyperLinkField DataNavigateUrlFields="documento" 
                                                                                        DataNavigateUrlFormatString="{0}"
                                                                                        DataTextField="nombre" HeaderText="Documento" Target="_blank" />
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:HiddenField ID="hfRuta" runat="server" 
                                                                                                value = '<%# Eval("rutafisica") %>'/>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="1%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ibtnEliminar" runat="server" 
                                                                                        ImageUrl="~/images/eliminar.gif" onclick="ibtnEliminar_Click" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="1%" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                                            BorderWidth="1px" ForeColor="#3366CC" />
                                                                            </asp:GridView>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                            </td>
                            
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

                        </tr>
                     </table>
            </td>
            </tr>
            <tr>
                <td height="220px" valign="top">
                    <asp:UpdatePanel ID="upDatosSubasta" runat="server">
                        <ContentTemplate>
                       
                                <asp:Menu
                                        ID="Menu1"
                                        runat="server"
                                        Orientation="Horizontal"
                                        StaticEnableDefaultPopOutImage="False"
                                        OnMenuItemClick="Menu1_MenuItemClick">
                                    <Items>
                                        <asp:MenuItem ImageUrl="../images/seArticulosSubasta.JPG" Text=" " Value="0"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="../images/unsNegociacion.JPG" Text=" " Value="1"></asp:MenuItem>
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
                                                                <asp:HiddenField ID="hfCodSubasta" runat="server" /></td>
                                                            <td align="right">
                                                                
                                                                <asp:Button ID="btnGuardar" runat="server" BackColor="#EFEFEF" 
                                                                    BorderColor="#0099FF" BorderStyle="Ridge" Text="Guardar" 
                                                                    ValidationGroup="Guardar" />
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
                                                                </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6">
                                                                <asp:GridView ID="gvArticulos" runat="server" AutoGenerateColumns="False" 
                                                                    BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                                                    ForeColor="#333333" Width="100%">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="idArt" HeaderText="ID" />
                                                                        <asp:BoundField DataField="descripcionArt" HeaderText="Artículo" />
                                                                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="precioBase" HeaderText="Precio Base">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Precio x Unidad">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtPrecioXUnidad" runat="server" CssClass="monto" Width="70px" 
                                                                                    Text = '<%# Eval("monto") %>'></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvTotal" runat="server" 
                                                                                    ControlToValidate="txtPrecioXUnidad" 
                                                                                    ErrorMessage="Debe ingresar el Precio por Unidad" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                                                <asp:RangeValidator ID="rvPrecioXUnidad" runat="server" 
                                                                                    ControlToValidate="txtPrecioXUnidad" 
                                                                                    ErrorMessage="Bebe ingresar un monto válido" MaximumValue="99999999" 
                                                                                    MinimumValue="0" ValidationGroup="Guardar">*</asp:RangeValidator>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
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
                                                    <table  style="width: 96%;">

                                                        <tr>
                                                            <td align="center" colspan="6">
                                                                <asp:Button ID="btnAgregarMensaje" runat="server" BackColor="#EFEFEF" 
                                                                    BorderColor="#0099FF" BorderStyle="Ridge" Text="Dejar mensaje" />
                                                                </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="6">
                                                                <asp:Panel ID="pnlMensaje" runat="server" BorderColor="#EFEFEF" 
                                                                    BorderWidth="1px" Visible="False" Width="70%">
                                                                    <table style="width:100%;">
                                                                        <tr>
                                                                            <td align="left">
                                                                                &nbsp;</td>
                                                                            <td align="left">
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                Asunto:</td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtAsunto" runat="server" Height="16px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                Mensaje:</td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtMensaje" runat="server" Height="52px" TextMode="MultiLine" 
                                                                                    Width="98%"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                &nbsp;</td>
                                                                            <td align="left">
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" colspan="2">
                                                                                <table style="width:50%;">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ibtnGuardarMensaje" runat="server" Height="25px" 
                                                                                                ImageUrl="~/images/dikette.gif" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ibtnEliminar" runat="server" 
                                                                                                ImageUrl="~/images/eliminar.gif" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                &nbsp;</td>
                                                                            <td align="left">
                                                                                &nbsp;</td>
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
                                                            <td colspan="6" align="left">
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
                                                    <asp:Label ID="asunto" runat="server" Font-Bold="True" 
                                                        ForeColor="Black" Text='<%# Eval("asunto") %>' />
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
                                                     </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                       
                                    </asp:View>
                                </asp:MultiView>
                                

                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnGuardar" />
                        </Triggers>
                    </asp:UpdatePanel>

                </td>
            </tr>
        </table>
    
    </div>
        <asp:ValidationSummary ID="vsListaConvocatoria" runat="server" 
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
    </form>
</body>
</html>
