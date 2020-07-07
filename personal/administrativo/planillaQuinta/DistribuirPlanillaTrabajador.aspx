<%@ Page EnableEventValidation="false" Language="VB" AutoEventWireup="false" CodeFile="DistribuirPlanillaTrabajador.aspx.vb" Inherits="planillaQuinta_DistribuirPlanillaTrabajador" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="height: 100%">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width: 100%; height: 100%;">
            <tr>
                <td valign="top" width="25%">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                Planilla</td>
                            <td>
                                        <asp:DropDownList ID="cboPlanilla" runat="server" AutoPostBack="True">
                                            <asp:ListItem Value="134">Planilla 1</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                        </tr>
                        <tr>
                            <td>
                                Unidad de gestión</td>
                            <td>
                                <asp:DropDownList ID="cboUnidadGestion" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Trabajador</td>
                            <td>
                                <asp:TextBox ID="txtTrabajador" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="cmdBuscar" runat="server" Text="Buscar" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel4" runat="server" Height="350px" ScrollBars="Vertical">
                                    <asp:GridView ID="gvTrabajador" runat="server" 
    AutoGenerateColumns="False" 
    DataKeyNames="codigo_per,descripcion_ded,descripcion_tpe,foto" Width="95%" Font-Size="8pt">
                                        <Columns>
                                            <asp:BoundField DataField="trabajador" 
            HeaderText="Trabajador" />
                                            <asp:BoundField DataField="distribuido" 
            HeaderText="Distribuido" >
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" >
                                                <ItemStyle ForeColor="Blue" />
                                            </asp:CommandField>
                                        </Columns>
                                        <SelectedRowStyle BackColor="#FFFFB7" />
                                        <HeaderStyle BackColor="#FF9933" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td bgcolor="White" width="1px" 
                    
                    style="border-left-style: solid; border-left-width: thin; border-left-color: #FFCC66;">
                    &nbsp;</td>
                <td valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%;" cellspacing="0">
                                <tr>
                                    <td style="font-weight: 700" bgcolor="#FFCC66" colspan="3">
                                        Datos Generales:</td>
                                </tr>
                                <tr>
                                    <td valign="middle">
                                        Trabajador
                                        <asp:Label ID="lblCodTrabajador" runat="server" Height="15px" Width="50px" ></asp:Label>
                                        <asp:Label ID="lblTrabajador" runat="server" Height="15px"></asp:Label>
                                    </td>
                                    <td align="center" colspan="2" rowspan="5">
                                        <asp:Image ID="imgFoto" runat="server" Height="100px" Width="80px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle">
                                        Tipo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblTipo" runat="server" Height="15px" Width="150px"></asp:Label>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle">
                                        Dedicación&nbsp;
                                        <asp:Label ID="lblDedicacion" runat="server" Height="15px" 
                                            Width="250px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="font-weight: 700" bgcolor="#FFCC66" colspan="3">
                                        Distribución:</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        Centro de Costos:<asp:CompareValidator ID="CompareValidator4" runat="server" 
                                            ControlToValidate="cboCecos" ErrorMessage="Ingrese centro de costos" 
                                            Operator="GreaterThan" ValidationGroup="Agregar" ValueToCompare="0">*</asp:CompareValidator>
&nbsp; <asp:DropDownList ID="cboCecos" runat="server" AutoPostBack="True" Width="80%">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtBuscaCecos" runat="server" ValidationGroup="BuscarCecos" 
                                            Width="200px"></asp:TextBox>
                                        <asp:ImageButton ID="ImgBuscarCecos" runat="server" 
                                            ImageUrl="~/images/busca.gif" ValidationGroup="BuscarCecos" />
                                        <asp:Label ID="lblTextBusqueda" runat="server" Text="(clic aquí)"></asp:Label>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:LinkButton ID="lnkBusquedaAvanzada" 
                runat="server" ForeColor="Blue">Busqueda 
                                        Avanzada</asp:LinkButton>
                                        &nbsp;<asp:UpdateProgress ID="UpdateProgress3" runat="server" 
                                            AssociatedUpdatePanelID="UpdatePanel1">
                                            <ProgressTemplate>
                                                <font style="color:Blue">Procesando. Espere un momento...</font>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Panel ID="Panel3" runat="server" 
                Height="150px" ScrollBars="Vertical" 
                                            Width="100%" Visible="False">
                                            <asp:GridView ID="gvCecos" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" DataKeyNames="codigo_cco" ForeColor="#333333" GridLines="None" 
                                                ShowHeader="False" Width="98%">
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundField DataField="codigo_cco" HeaderText="Código" />
                                                    <asp:BoundField DataField="nombre" HeaderText="Centro de costos" />
                                                    <asp:CommandField ShowSelectButton="True">
                                                        <ItemStyle ForeColor="Red" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <b>No se encontraron items con el término de búsqueda</b>
                                                </EmptyDataTemplate>
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#999999" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Porcentaje:<asp:CompareValidator ID="CompareValidator5" runat="server" 
                                            ControlToValidate="cboCecos" ErrorMessage="Ingrese centro de costos" 
                                            Operator="GreaterThan" ValidationGroup="Agregar" ValueToCompare="0">*</asp:CompareValidator>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                        <asp:TextBox ID="txtPorcentaje" runat="server" Width="83px"></asp:TextBox>
                                        &nbsp;%
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hddCodigo_ddplla" runat="server" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        Observación:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                        <asp:TextBox ID="txtObservacion" runat="server" Width="333px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="cmdAgregar" runat="server" Text="Agregar" 
                                            ValidationGroup="Agregar" />
                                        &nbsp;<asp:Label ID="lblMsj" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:GridView ID="gvDistribucion" runat="server" AutoGenerateColumns="False" 
                                            DataKeyNames="codigo_ddplla,codigo_cco" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="descripcion_cco" HeaderText="Centro de costos" />
                                                <asp:BoundField DataField="distribuido" HeaderText="Distribuido" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="observacion_ddplla" HeaderText="Observacion" />
                                                <asp:CommandField SelectText="Editar" ShowSelectButton="True" >
                                                    <ItemStyle ForeColor="Blue" />
                                                </asp:CommandField>
                                                <asp:CommandField ShowDeleteButton="True" >
                                                    <ItemStyle ForeColor="Blue" />
                                                </asp:CommandField>
                                            </Columns>
                                            <HeaderStyle BackColor="#FF9900" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="3">
                                        Total:
                                        <asp:Label ID="lblTotalItem" runat="server" Font-Bold="True" Font-Size="10pt" 
                                            Font-Underline="True" ForeColor="#0033CC">0.00</asp:Label>
                                        &nbsp;Distribuido:
                                        <asp:Label ID="lblDistribuidoItem" runat="server" Font-Bold="True" 
                                            Font-Size="10pt" Font-Underline="True" ForeColor="#0033CC">0.00</asp:Label>
                                        &nbsp;Por Distribuir:
                                        <asp:Label ID="lblPorDistribuir" runat="server" Font-Bold="True" 
                                            Font-Size="10pt" Font-Underline="True" ForeColor="Blue">0.00</asp:Label>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                            ShowMessageBox="True" ShowSummary="False" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
