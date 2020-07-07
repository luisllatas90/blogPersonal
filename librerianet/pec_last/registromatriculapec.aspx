<%@ Page Language="VB" AutoEventWireup="false" CodeFile="registromatriculapec.aspx.vb" Inherits="registromatriculapec" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Programas de Educación Contínua: PEC</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>
	<link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
    </head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Matricula en Programas de Educación Contínua</p>
<table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" 
            border="0">
        <tr style="background-color: #6694e3; color:White">
            <td style="height: 30px; ">
                Programa:
                <asp:DropDownList ID="dpPrograma" runat="server" Width="70%">
                </asp:DropDownList>
                                            &nbsp;<asp:Button ID="cmdVer" runat="server" Text="   Consultar" 
                    CssClass="buscar2" Height="20px" />
            </td>
        </tr>
        </table>
                                    <table cellpadding="3" 
                                        cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
                                        <tr style="background-color: #E8EEF7; font-weight: bold;">
                                            <td>
                                                Participantes matriculados en el Programa</td>
                                            <td align="right">
                                                <asp:Button ID="cmdAgregar" runat="server" Text="    Nuevo" 
                                                    CssClass="agregar2" Visible="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblmensaje" runat="server" CssClass="rojo" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="grwParticipantes" runat="server" 
                                                    AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Top" 
                                                    DataKeyNames="codigo_alu" Width="100%" BorderColor="Silver" 
                                                    EnableModelValidation="True" CellPadding="2">
                                                    <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                                                    <EditRowStyle BackColor="#FFFF66" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        
                                                        <asp:BoundField DataField="codigouniver_alu" HeaderText="Cód. Pago*">
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        
                                                        <asp:TemplateField HeaderText="Apellido Paterno">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtap" runat="server" CssClass="cajas" Text='<%# eval("apellidopat_alu") %>' MaxLength="100" Width="96%"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe ingresar el Apellido Paterno" Text="*" ValidationGroup="EdicionParticipante" ControlToValidate="txtap"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtap" runat="server" CssClass="cajas" 
                                                                    Text='<%# eval("apellidopat_alu") %>' MaxLength="100" Width="96%"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe ingresar el Apellido Paterno" Text="*" ValidationGroup="NuevoParticipante" ControlToValidate="txtap"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblap" runat="server" Text='<%# eval("apellidopat_alu") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="25%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Apellido Materno">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtam" runat="server" CssClass="cajas" Text='<%# eval("apellidomat_alu") %>' MaxLength="100" Width="96%"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe ingresar el Apellido Materno" Text="*" ValidationGroup="EdicionParticipante" ControlToValidate="txtam"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtam" runat="server" CssClass="cajas" 
                                                                    Text='<%# eval("apellidomat_alu") %>' MaxLength="100" Width="96%"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe ingresar el Apellido Materno" Text="*" ValidationGroup="NuevoParticipante" ControlToValidate="txtam"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblam" runat="server" Text='<%# eval("apellidomat_alu") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="25%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nombres">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtn" runat="server" CssClass="cajas" Text='<%# eval("nombres_alu") %>' MaxLength="100" Width="96%"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Debe ingresar los Nombres" Text="*" ValidationGroup="EdicionParticipante" ControlToValidate="txtn"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtn" runat="server" CssClass="cajas" 
                                                                    Text='<%# eval("nombres_alu") %>' MaxLength="100" Width="96%"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Debe ingresar los Nombres" Text="*" ValidationGroup="NuevoParticipante" ControlToValidate="txtn"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbln" runat="server" Text='<%# eval("nombres_alu") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="25%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sexo">
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="dpsexo" runat="server">
                                                                    <asp:ListItem Value="M">M</asp:ListItem>
                                                                    <asp:ListItem Value="F">F</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsexo_alu" runat="server" Text='<%# eval("sexo_alu") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="dpsexo" runat="server">
                                                                    <asp:ListItem Value="M">M</asp:ListItem>
                                                                    <asp:ListItem Value="F">F</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </FooterTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>                                                        
                                                        <asp:TemplateField HeaderText="DNI">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtdni" runat="server" MaxLength="11" Width="90px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RqDNI" runat="server" 
                                                                    ControlToValidate="txtdni" ErrorMessage="Debe ingresar el DNI" Text="*" 
                                                                    ValidationGroup="EdicionParticipante"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="dptipo" runat="server">
                                                                    <asp:ListItem Value="DNI">DNI</asp:ListItem>
                                                                    <asp:ListItem Value="CARNÉ DE EXTRANJERÍA">C.EXTRANG.</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtdni" runat="server" MaxLength="11" Width="90px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RqDNI" runat="server" 
                                                                    ControlToValidate="txtdni" ErrorMessage="Debe ingresar el DNI" Text="*" 
                                                                    ValidationGroup="NuevoParticipante"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldni" runat="server" Text='<%# eval("nrodocident_alu") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Editar">
                                                            <ItemTemplate>
                                                                
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                               
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="imgGuardar" runat="server" 
                                                                    ImageUrl="../../images/guardar.gif" onclick="imgGuardar_Click" 
                                                                    ValidationGroup="ValidarAgregarModulo" />
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="True" 
                                                            DeleteText="Eliminar">
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
                                            </td>
                                        </tr>
                                    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" 
        ValidationGroup="EdicionParticipante" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="NuevoParticipante" />
    <p>(*). Después de registrar a los participantes y asignadas las deudas, indicar al mismo, que debe ir a caja a cancelar, utilizando el código de pago. </p>
    </form>
</body>
</html>

