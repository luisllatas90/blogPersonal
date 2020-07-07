<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmplanestudio.aspx.vb" Inherits="planestudio" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

.usatNuevo {
	border: 1px solid #C0C0C0;
	background: #FEFFE1 url('../../images/nuevo.gif') no-repeat 0% 80%;
	font-family: Tahoma;
	font-size: 8pt;
	font-weight: bold;
	height: 25;
}
    </style>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
                                    <table runat="server" id="fraDetallePEC" cellpadding="3" 
                                        cellspacing="0" style="border: 1px solid #C2CFF1; width:100%" visible="false">
                                        
                                        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                                            <td >Módulos del Programa</td>
                                            <td align="right" >
                                                <asp:Button ID="cmdNuevoModulo" runat="server" Text="    Nuevo" 
                                                    ValidationGroup="AgregarModulos" CssClass="usatNuevo" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="grwModulosPEC" runat="server" 
                                                    AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Top" 
                                                    DataKeyNames="codigo_cup" Width="100%" BorderColor="Silver" 
                                                    EnableModelValidation="True" CellPadding="2">
                                                    <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" 
                                                        HorizontalAlign="Center" />
                                                    <EditRowStyle BackColor="#FFFF66" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Nro">
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="dpciclo_cur" runat="server" 
                                                                    SelectedValue='<%# eval("ciclo_cur") %>'>
                                                                    <asp:ListItem>1</asp:ListItem>
                                                                    <asp:ListItem>2</asp:ListItem>
                                                                    <asp:ListItem>3</asp:ListItem>
                                                                    <asp:ListItem>4</asp:ListItem>
                                                                    <asp:ListItem>5</asp:ListItem>
                                                                    <asp:ListItem>6</asp:ListItem>
                                                                    <asp:ListItem>7</asp:ListItem>
                                                                    <asp:ListItem>8</asp:ListItem>
                                                                    <asp:ListItem>9</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="dpciclo_cur0" runat="server">
                                                                    <asp:ListItem>1</asp:ListItem>
                                                                    <asp:ListItem>2</asp:ListItem>
                                                                    <asp:ListItem>3</asp:ListItem>
                                                                    <asp:ListItem>4</asp:ListItem>
                                                                    <asp:ListItem>5</asp:ListItem>
                                                                    <asp:ListItem>6</asp:ListItem>
                                                                    <asp:ListItem>7</asp:ListItem>
                                                                    <asp:ListItem>8</asp:ListItem>
                                                                    <asp:ListItem>9</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblciclo_cur" runat="server" Text='<%# eval("ciclo_cur") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Denominación">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtnombre_cur" runat="server" CssClass="cajas" 
                                                                    Text='<%# eval("nombre_cur") %>' Width="98%" MaxLength="500"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RqDenominacionF" runat="server" 
                                                                    ControlToValidate="txtnombre_cur" 
                                                                    ErrorMessage="Debe especificar la denominación del Módulo" 
                                                                    ValidationGroup="ValidarEditarModulo">*</asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtnombre_cur0" runat="server" CssClass="cajas" 
                                                                    Width="98%" MaxLength="500"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RqDenominacionF0" runat="server" 
                                                                    ControlToValidate="txtnombre_cur" 
                                                                    ErrorMessage="Debe especificar la denominación del Módulo" 
                                                                    ValidationGroup="ValidarAgregarModulo">*</asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnombre_cur" runat="server" Text='<%# eval("nombre_cur") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Font-Size="7pt" HorizontalAlign="Left" Width="55%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Crd.">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtcreditos_cur" runat="server" CssClass="cajas" 
                                                                    Text='<%# eval("creditos_cur") %>' Width="25px" MaxLength="3"></asp:TextBox>
                                                                <asp:RangeValidator ID="RqCrd" runat="server" 
                                                                    ControlToValidate="txtcreditos_cur" 
                                                                    ErrorMessage="Debe especificar los créditos del módulo (entre 0 y 100)" 
                                                                    MaximumValue="100" MinimumValue="0" Type="Integer" 
                                                                    ValidationGroup="ValidarEditarModulo">*</asp:RangeValidator>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtcreditos_cur0" runat="server" CssClass="cajas" 
                                                                    Width="25px" MaxLength="2">0</asp:TextBox>
                                                                <asp:RangeValidator ID="RqCrdF" runat="server" 
                                                                    ControlToValidate="txtcreditos_cur" 
                                                                    ErrorMessage="Debe especificar los créditos del módulo (entre 0 y 100)" 
                                                                    MaximumValue="100" MinimumValue="0" Type="Integer" 
                                                                    ValidationGroup="ValidarAgregarModulo">*</asp:RangeValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcreditos_cur" runat="server" 
                                                                    Text='<%# eval("creditos_cur") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HT">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtht" runat="server" CssClass="cajas" 
                                                                    Text='<%# eval("horasteo_cur") %>' Width="25px" MaxLength="3"></asp:TextBox>
                                                                <asp:RangeValidator ID="RqHT" runat="server" ControlToValidate="txtht" 
                                                                    ErrorMessage="Debe especificar el Nro de Horas Teóricas (1-300)" 
                                                                    MaximumValue="300" MinimumValue="1" Type="Integer" 
                                                                    ValidationGroup="ValidarEditarModulo">*</asp:RangeValidator>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtht0" runat="server" CssClass="cajas" 
                                                                    Width="27px" MaxLength="3">0</asp:TextBox>
                                                                <asp:RangeValidator ID="RqHTF" runat="server" ControlToValidate="txtht" 
                                                                    ErrorMessage="Debe especificar el Nro de Horas Teóricas (1-300)" 
                                                                    MaximumValue="300" MinimumValue="1" Type="Integer" 
                                                                    ValidationGroup="ValidarAgregarModulo">*</asp:RangeValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblht" runat="server" Text='<%# eval("horasteo_cur") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HI">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txthi" runat="server" Text='<%# eval("horaspra_cur") %>' 
                                                                    CssClass="cajas" MaxLength="3" Width="27px"></asp:TextBox>
                                                                <asp:RangeValidator ID="RqHI" runat="server" ControlToValidate="txthi" 
                                                                    ErrorMessage="Debe especificar correctamente el Nro de Horas de Investigación (0-300)" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Integer" 
                                                                    ValidationGroup="ValidarEditarModulo">*</asp:RangeValidator>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txthi0" runat="server" CssClass="cajas" 
                                                                    Width="27px" MaxLength="3" Text="0"></asp:TextBox>
                                                                <asp:RangeValidator ID="RqHIF" runat="server" ControlToValidate="txthi" 
                                                                    ErrorMessage="Debe especificar correctamente el Nro de Horas de Investigación (0-300)" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Integer" 
                                                                    ValidationGroup="ValidarAgregarModulo">*</asp:RangeValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblhi" runat="server" Text='<%# eval("horaspra_cur") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HA">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtha" runat="server" CssClass="cajas" 
                                                                    Text='<%# eval("horasAse_cur") %>' Width="25px" MaxLength="3"></asp:TextBox>
                                                                <asp:RangeValidator ID="RqHA" runat="server" ControlToValidate="txtha" 
                                                                    ErrorMessage="Debe especificar correctamente el Nro de Horas de Asesoría (0-300)" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Integer" 
                                                                    ValidationGroup="ValidarEditarModulo">*</asp:RangeValidator>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtha0" runat="server" CssClass="cajas" 
                                                                    Width="27px" MaxLength="3">0</asp:TextBox>
                                                                <asp:RangeValidator ID="RqHAF" runat="server" ControlToValidate="txtha" 
                                                                    ErrorMessage="Debe especificar correctamente el Nro de Horas de Asesoría (0-300)" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Integer" 
                                                                    ValidationGroup="ValidarAgregarModulo">*</asp:RangeValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblha" runat="server" Text='<%# eval("horasAse_cur") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HEP">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txthep" runat="server" CssClass="cajas" 
                                                                    Text='<%# eval("horaslab_cur") %>' Width="27px" MaxLength="3"></asp:TextBox>
                                                                <asp:RangeValidator ID="RqHEP" runat="server" ControlToValidate="txthep" 
                                                                    ErrorMessage="Debe especificar correctamente el Nro de Horas de Estudio Personal (0-300)" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Integer" 
                                                                    ValidationGroup="ValidarEditarModulo">*</asp:RangeValidator>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txthep0" runat="server" CssClass="cajas" 
                                                                    Width="27px" MaxLength="3" Text="0"></asp:TextBox>
                                                                <asp:RangeValidator ID="RqHEPF" runat="server" ControlToValidate="txthep" 
                                                                    ErrorMessage="Debe especificar correctamente el Nro de Horas de Estudio Personal (0-300)" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Integer" 
                                                                    ValidationGroup="ValidarAgregarModulo">*</asp:RangeValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblhep" runat="server" Text='<%# eval("horaslab_cur") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TH">
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="imgGuardar" runat="server" 
                                                                    ImageUrl="../../images/guardar.gif" onclick="imgGuardar_Click" 
                                                                    ValidationGroup="ValidarAgregarModulo" />
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblth" runat="server" Text='<%# eval("totalhoras_cur") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowEditButton="True" ButtonType="Image" 
                                                            CancelImageUrl="../../images/salir.gif" EditImageUrl="../../images/editar.gif" 
                                                            UpdateImageUrl="../../images/guardar.gif" 
                                                            ValidationGroup="ValidarEditarModulo" >
                                                        <ItemStyle Width="5%" />
                                                        </asp:CommandField>
                                                        <asp:CommandField ShowDeleteButton="True" 
                                                            DeleteText="Eliminar" ButtonType="Image" 
                                                            DeleteImageUrl="../../images/eliminar.gif">
                                                            <ControlStyle Font-Underline="True" />
                                                            <ItemStyle Font-Underline="True" ForeColor="Blue" 
                                                                HorizontalAlign="Center" Width="5%" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
                                                <br />
                                                <asp:Label ID="lbltotales" runat="server" Font-Bold="True" Font-Names="Verdana" 
                                                    Font-Size="8pt" ForeColor="#3366DB"></asp:Label>
                                                <asp:ValidationSummary ID="ValidarEditarModulo" runat="server" 
                                                    ShowMessageBox="True" ShowSummary="False" 
                                                    ValidationGroup="ValidarEditarModulo" />
                                                <asp:ValidationSummary ID="ValidarAgregarModulo" runat="server" 
                                                    ShowMessageBox="True" ShowSummary="False" 
                                                    ValidationGroup="ValidarAgregarModulo" />
                                                </td>
                                        </tr>
                                    </table>
    </form>
</body>
</html>
