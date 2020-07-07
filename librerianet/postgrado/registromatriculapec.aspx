<%@ Page Language="VB" AutoEventWireup="false" CodeFile="registromatriculapec.aspx.vb" Inherits="registromatriculapec" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Programas de Educación Contínua: PEC</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script src="../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../private/jq/jquery.mascara.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function PintarFilaMarcada(obj, estado) {
            if (estado == true) {
                obj.style.backgroundColor = "#FFE7B3"
            }
            else {
                obj.style.backgroundColor = "white"
            }
        }
    </script>
    </head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Matricula en Programas de PostGrado</p>
<table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" 
            border="0">
        <tr style="background-color: #6694e3; color:White">
            <td style="height: 30px; ">
                Programa:
                <asp:DropDownList ID="dpPrograma" runat="server" Width="70%">
                </asp:DropDownList>
                                            &nbsp;<asp:Button ID="cmdVer" runat="server" Text="Consultar" 
                    CssClass="buscar2" Height="20px" />
                <asp:Button ID="cmdExportar" runat="server" CssClass="excel2" Height="20px" 
                    Text="Importar" Visible="False" />
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
                                                    CssClass="usatnuevo" Visible="False" />
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
                                                        
                                                        <asp:BoundField DataField="codigouniver_alu" HeaderText="Código">
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
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="false" 
                                                                    CommandName="editar" Font-Underline="True" ForeColor="#3333FF" Text="Editar"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="imgGuardar" runat="server" 
                                                                    ImageUrl="../../images/guardar.gif" onclick="imgGuardar_Click" 
                                                                    ValidationGroup="ValidarAgregarModulo" />
                                                            </FooterTemplate>
                                                            <ControlStyle Font-Underline="True" />
                                                            <ItemStyle Font-Underline="True" ForeColor="#3333FF" HorizontalAlign="Center" 
                                                                Width="5%" />
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
    </form>
</body>
</html>

