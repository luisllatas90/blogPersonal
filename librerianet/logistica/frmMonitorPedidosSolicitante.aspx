<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMonitorPedidosSolicitante.aspx.vb" Inherits="logistica_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Monitor de Pedidos de Logística</title>
    <link href="../private/estilo.css"rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloctrles.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../private/jq/jquery.mascara.js" type="text/javascript"></script>
    <script type ="text/javascript" >
      function ClearFile(){
        var fu = $('#fuCargarArchivo')[0];  
        fu.value="";

        var fu2= fu.cloneNode(false);
        fu2.onchange= fu.onchange;
        fu.parentNode.replaceChild(fu2,fu);
    };
    </script>
</head>
<body>
    <form id="form1" runat="server">
<table id="tblMarcos" style="height: 100%; width: 100%; margin-right: 0px" 
        align="right" class="contornotabla">
<tr  ><td>
    Ver mis pedidos en:&nbsp;&nbsp; <asp:DropDownList ID="cboInstancia" runat="server" AutoPostBack="True">
    </asp:DropDownList>
            </td>
            <td align="right">
    <asp:Button ID="cmdEnviar" runat="server" Text="  Enviar" 
                                    BorderStyle="Outset" CssClass="salir" Width="87px" 
                        Height="26px" Visible="False" />
    <asp:Button ID="cmdEliminar" runat="server" Text="Eliminar" 
                                    BorderStyle="Outset" CssClass="eliminar" Width="87px" 
                        Height="26px" Visible="False" />
    <%--treyes 05/07/2016--%>
    <%--<asp:Button ID="cmdClonar" runat="server" Text="Duplicar" 
                                    BorderStyle="Outset" CssClass="guardar" Width="95px" 
                        Height="26px" />--%>
            </td>
</tr>

<tr height="180"><td valign="top" class="contornotabla" colspan="2" >
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="LOG_ConsultarPedidos" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="SO" Name="tipo" Type="String" />
            <asp:QueryStringParameter DefaultValue="0" Name="codigo_per" 
                QueryStringField="ID" Type="Int32" />
            <asp:ControlParameter ControlID="cboInstancia" DefaultValue="1" 
                Name="instancia" PropertyName="SelectedValue" Type="Int32" />
            <asp:Parameter DefaultValue="P" Name="veredicto" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    Lista de pedidos:<asp:GridView ID="gvPedidos" runat="server" Width="100%" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
        DataKeyNames="Num,codigo_cco" DataSourceID="SqlDataSource1" ForeColor="#333333" 
        GridLines="None" BorderColor="White" BorderWidth="1px">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="Num" HeaderText="Num" InsertVisible="False" 
                ReadOnly="True" SortExpression="Num" />
            <asp:BoundField DataField="Persona" HeaderText="Persona" ReadOnly="True" 
                SortExpression="Persona" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" />
            <asp:BoundField DataField="CeCo" HeaderText="CeCo" SortExpression="CeCo" />
            <asp:BoundField DataField="Importe (S/.)" HeaderText="Importe (S/.)" 
                SortExpression="Importe (S/.)" />
            <asp:BoundField DataField="Estado" HeaderText="Estado" 
                SortExpression="Estado" />
            <asp:CommandField SelectText="" ShowSelectButton="True" />
            <asp:BoundField DataField="codigo_cco" HeaderText="codigo_cco" 
                Visible="False" />
        </Columns>
        <FooterStyle Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#003399" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    </td></tr>
<tr><td colspan="2">
    
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
    
            <asp:GridView ID="gvDetallePedido" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" DataKeyNames="codigo_dpe,modoDistribucion_Dpe" 
                ForeColor="#333333" GridLines="None" Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField DataField="descripcionart" HeaderText="Artículo" 
                        SortExpression="descripcionart" />
                    <asp:BoundField DataField="descripcion_cco" HeaderText="CeCo" 
                        SortExpression="descripcion_cco" />
                    <asp:BoundField DataField="precioreferencial_dpe" HeaderText="Precio" 
                        SortExpression="precioreferencial_dpe">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cantidad_dpe" HeaderText="Cantidad" 
                        SortExpression="cantidad_dpe">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="subtotal" HeaderText="Subtotal" ReadOnly="True" 
                        SortExpression="subtotal">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Observacion_dpe" HeaderText="Observación" 
                        SortExpression="Observacion_dpe" />
                    <asp:CheckBoxField DataField="presupuestado" HeaderText="Presup.">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:CheckBoxField>
                    <asp:BoundField DataField="descripcionEstado_Eped" HeaderText="Estado" />
                    <asp:CommandField ShowDeleteButton="True">
                        <ItemStyle ForeColor="#CC3300" HorizontalAlign="Center" />
                    </asp:CommandField>
                    <asp:CommandField SelectText="Distribuir" ShowSelectButton="false">
                        <ItemStyle ForeColor="#0000CC" HorizontalAlign="Center" />
                    </asp:CommandField>
                    <asp:CommandField ShowEditButton="True">
                        <ItemStyle ForeColor="#009900" />
                    </asp:CommandField>
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
    
                </td></tr>
<tr height="550px" class="" valign="top"><td class="contornotabla" colspan="2">
    <table width="100%">
    <tr>
    <td style="color: #0000FF">
        &nbsp;<tr>
    <td style="color: #0000FF">
        Pedido N°
      <asp:TextBox ID="txtPedido" runat="server" Enabled="False"></asp:TextBox>
                        &nbsp;Ver:
        <asp:LinkButton ID="lnkDatos" runat="server" ForeColor="#0000CC">Datos Generales</asp:LinkButton>
        |<asp:LinkButton ID="lnkRevisiones" runat="server" ForeColor="#0000CC">Revisiones</asp:LinkButton>
    </td>
    <td style="color: #0000FF" align="right">
        <asp:TextBox ID="txtDetPresup" 
                                        runat="server" Width="103px" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtCodigoDpe" runat="server" Width="116px" Visible="False"></asp:TextBox>
        Proceso:
                    <asp:DropDownList ID="cboPeriodoPresu" runat="server" 
            Enabled="False">
                    </asp:DropDownList>
                &nbsp;Estado:<asp:DropDownList ID="cboEstado" runat="server" Enabled="False">
                    </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td style="color: #0000FF" colspan="2">
                    <hr />
    </td>
    </tr>
    <tr>
    <td colspan="2">
        <asp:Panel ID="pnlDatos" runat="server" Visible="False">
            <asp:Label ID="lblTitTotal" runat="server" ForeColor="Red" 
                Text="Total del pedido (S/.):"></asp:Label>
            <asp:Label ID="lblTotalDetalle" runat="server" Font-Bold="True" 
                Font-Size="10pt" Font-Underline="True" ForeColor="#0033CC" Text="0.00"></asp:Label>
            <br />
            <asp:Panel ID="pnlDistribuir" runat="server" ForeColor="#FF3300" 
                Visible="False">
                <br />
                Distribución de:
                <asp:Label ID="lblNombreItem" runat="server" Font-Size="10pt" 
                    ForeColor="#0033CC"></asp:Label>
                <br />
                <asp:Label ID="lblCeCoDist" runat="server" Text="Centro Costos"></asp:Label>
                <asp:CompareValidator ID="CompareValidator7" runat="server" 
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
                <asp:Button ID="cmdDistribuir" runat="server" BorderStyle="Outset" 
                    Text="Distribuir" ValidationGroup="Distribuir" />
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
                <asp:Label ID="lblTotalItem" runat="server" Font-Bold="True" Font-Size="10pt" 
                    Font-Underline="True" ForeColor="#0033CC">0.00</asp:Label>
                &nbsp;Distribuido:
                <asp:Label ID="lblDistribuidoItem" runat="server" Font-Bold="True" 
                    Font-Size="10pt" Font-Underline="True" ForeColor="#0033CC">0.00</asp:Label>
                &nbsp;Por Distribuir:
                <asp:Label ID="lblPorDistribuir" runat="server" Font-Bold="True" 
                    Font-Size="10pt" Font-Underline="True"></asp:Label>
                <br />
                <div>
                </div>
                <asp:TextBox ID="txtCodItem" runat="server" Visible="False"></asp:TextBox>
                <asp:HiddenField ID="hddForzar" runat="server" Value="0" />
            </asp:Panel>
            <hr />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="2" cellspacing="0" style="width:100%;">
                        <tr>
                            <td align="left" class="style1">
                                Centro de costo<asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                                    runat="server" ControlToValidate="cboCecos" 
                                    ErrorMessage="Seleccione centro de costos" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator5" runat="server" 
                                    ControlToValidate="cboCecos" ErrorMessage="Seleccione centro de costos" 
                                    Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator8" runat="server" 
                                    ControlToValidate="cboCecos" ErrorMessage="Seleccione centro de costos" 
                                    Operator="GreaterThan" ValidationGroup="Presupuesto" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCecos" runat="server" BackColor="#F3F3F3" Visible="False" 
                                    Width="90px"></asp:TextBox>
                                <asp:DropDownList ID="cboCecos" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td>
                                <asp:LinkButton ID="lnkBusquedaAvanzada" runat="server" ForeColor="Blue" 
                                    Visible="False">Busqueda Avanzada</asp:LinkButton>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" 
                                    AssociatedUpdatePanelID="UpdatePanel2">
                                    <ProgressTemplate>
                                        <font style="color:Blue">Procesando. Espere un momento...</font>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Panel ID="Panel3" runat="server" Height="150px" ScrollBars="Vertical" 
                                    Width="100%">
                                    <asp:GridView ID="gvCecos" runat="server" AutoGenerateColumns="False" 
                                        BorderColor="#628BD7" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                        DataKeyNames="codigo_cco" ForeColor="#333333" ShowHeader="False" Width="98%">
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
                            <td align="left" colspan="2">
                                <asp:Panel ID="pnlPresupuesto" runat="server" ForeColor="Red" 
                                    HorizontalAlign="Left" Visible="False">
                                    Seleccione el ítem presupuestado que desea pedir:<asp:GridView ID="gvPresupuesto" runat="server" AutoGenerateColumns="False" 
                                        CellPadding="4" DataKeyNames="codigo_dpr,codigo_Ppr,codigo_Art" 
                                        ForeColor="#333333" GridLines="Horizontal" Width="90%">
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#E3EAEB" />
                                        <Columns>
                                            <asp:BoundField DataField="descripcion_Ppr" HeaderText="Prog. Presupuestal" />
                                            <asp:BoundField DataField="DesEstandar" HeaderText="Item" />
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
                                            <asp:CommandField ShowSelectButton="True" />
                                        </Columns>
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                        <tr bgcolor="#F5F9FC">
                            <td>
                                Prog. presupuestal<asp:CompareValidator ID="CompareValidator6" runat="server" 
                                    ControlToValidate="cboProgramaPresu" ErrorMessage="Seleccione programa presupuestal" 
                                    Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td>
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
                                        CellPadding="4" DataKeyNames="codigocon,tipo,iduni,especificaCantidad" 
                                        ForeColor="Black" Width="98%" BorderColor="#CCCCCC" BorderStyle="None" 
                                        BorderWidth="1px" ShowHeader="False" BackColor="White" 
                                        GridLines="Horizontal">
                                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                        <Columns>
                                            <asp:BoundField DataField="codigo" HeaderText="Código" />
                                            <asp:BoundField DataField="concepto" HeaderText="Concepto" />
                                            <asp:BoundField DataField="unidad" HeaderText="Unidad" />
                                            <asp:BoundField DataField="precio" HeaderText="Precio" >
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" />
                                        </Columns>
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
                                Detalle/Justificación
                            </td>
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
                                    <span runat ="server" visible ="false" id="Especificaciones"><img src="../images/attachment.png" /><asp:HyperLink  ID ="hlAdjunto" runat ="server" Text="" Enabled ="false" ></asp:HyperLink></span>
                                        <asp:FileUpload ID="fuCargarArchivo" runat="server" size="50" ContentEditable="False" ToolTip ="Se admiten archivos .doc,.xls,.docx,.xlsx,.jpg,.png,.pdf,.zip  Tam. máx 4MB" />
                                        <a href="#" onclick="ClearFile();return false;" ><img src="../images/delete.png" border="0" alt="Link to this page"></a> 
                                        &nbsp;<asp:RegularExpressionValidator ID="revUpload" runat="server" ErrorMessage="Formato de archivo incorrecto" ControlToValidate="fuCargarArchivo" ValidationExpression= "^.+(.zip|.ZIP|.doc|.docx|.xls|.xlsx|.jpg|.png|.pdf)$" ValidationGroup ="Guardar" />
                                        
                                    </td>
                                </tr>
                        <tr bgcolor="#F5F9FC">
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="lblUnidad" runat="server" BackColor="#F3F3F3" ReadOnly="True" 
                                    Visible="false" Width="90px"></asp:TextBox>
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
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblValores" runat="server" Font-Bold="True" Text="Cantidad"></asp:Label>
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
                                    ID="RequiredFieldValidator11" runat="server" 
                                    ControlToValidate="TxtFechaEsperada" ErrorMessage="Ingrese fecha de nacimiento" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                &nbsp;</td>
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
            <asp:Button ID="cmdGuardar" runat="server" BorderStyle="Outset" 
                CssClass="guardar" Height="26px" Text="   Guardar detalle"
                ValidationGroup="Guardar" Width="148px" CausesValidation ="true" OnClientClick =" if (Page_ClientValidate()) {if(document.getElementById('hlAdjunto').innerHTML !='' && document.getElementById('fuCargarArchivo').value!='' ){ return confirm('Se reemplazará el archivo adjunto existente. ¿Desea continuar?');}}"/>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="pnlRevision" runat="server" Visible="False">
        <table width="100%">
        <tr>
        <td width="50%" valign="top">
            <asp:GridView ID="gvRevisiones" runat="server" Caption="Revisiones del pedido" 
                CaptionAlign="Left" CellPadding="4" ForeColor="#333333" GridLines="None" 
                Width="95%">
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#E3EAEB" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            </td>
        <td width="50%" valign="top">
            <asp:GridView ID="gvEstados" runat="server" Caption="Evolución del estado" 
                CaptionAlign="Left" CellPadding="4" ForeColor="#333333" GridLines="None" 
                Width="95%">
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#E3EAEB" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            </td>
        </table>
            <br />
            <asp:GridView ID="gvObservaciones" runat="server" 
                Caption="Observaciones al pedido" CaptionAlign="Left" CellPadding="4" 
                ForeColor="#333333" GridLines="None" Width="97.5%">
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#E3EAEB" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <br />
        </asp:Panel>
    </td>
    </tr>
    </table>
    </td></tr>
</table>
    </form>
</body>
</html>
