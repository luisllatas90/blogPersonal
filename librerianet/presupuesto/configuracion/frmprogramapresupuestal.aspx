<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmprogramapresupuestal.aspx.vb" Inherits="frmprogramapresupuestal" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Programas Presupuestales</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript"  language="JavaScript" src="../../private/funciones.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Registro de Programas Presupuestales</p>
    <p class="rojo">
        <asp:Label ID="lblmensaje" runat="server"></asp:Label>
    </p>
    <p>
        <table style="border: 1px solid #99BAE2; width:70%; border-collapse: collapse; " cellpadding="3" cellspacing="0">
            <tr>
                <td style="width: 100%; background-color: #e8eef7; color: #3366CC; font-weight: bold;">
                    Agregar nuevo Programa Presupuestal</td>
            </tr>
            <tr>
                
                <td style="width: 100%">
     <asp:TextBox ID="txtdescripcion_ppr" runat="server" CssClass="Cajas" 
                        Width="300px"></asp:TextBox>
                        <asp:Button ID="cmdGuardarNuevo" runat="server" CssClass="imgGuardar" 
        Text="   Guardar" ValidationGroup="ValNuevo" Font-Bold="False" />
                    <asp:RequiredFieldValidator ID="RqValidar" runat="server" 
                        ControlToValidate="txtdescripcion_ppr" 
                        SetFocusOnError="True" ValidationGroup="ValNuevo" 
        EnableViewState="False" 
        ErrorMessage="*">Ingrese la denominación del Programa Presupuestal</asp:RequiredFieldValidator>
    
                </td>
            </tr>
        </table>
    <br />
    <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="False" 
        CellPadding="3" Width="70%" DataKeyNames="codigo_ppr" 
        BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px">
        <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" />
        <Columns>
            <asp:TemplateField HeaderText="#">
                <ItemStyle Width="5%" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Descripción">
                <EditItemTemplate>
                    <asp:TextBox ID="txtdescripcion_ppr" runat="server" 
                        Text='<%# Bind("descripcion_ppr") %>' CssClass="cajas" Width="95%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtdescripcion_ppr" 
                        ErrorMessage="Debe ingresar la denominación del Programa Presupuestal" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                    <asp:Label ID="lblcodigo_ppr" runat="server" Text='<%# eval("codigo_ppr") %>' 
                        Visible="False"></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbldescripcion_ppr" runat="server" Text='<%# Bind("descripcion_ppr") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="85%" />
            </asp:TemplateField>
            
            
            <asp:CommandField ShowEditButton="True" >
                <ItemStyle Width="5%" />
            </asp:CommandField>
            <asp:CommandField ShowDeleteButton="True" >
                <ItemStyle Width="5%" />
            </asp:CommandField>
        </Columns>
        <FooterStyle BackColor="#e8eef7" ForeColor="#3366CC" />
        <EmptyDataTemplate>
            No se han registrado Programas Presupuestales
        </EmptyDataTemplate>
        <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" />
    </asp:GridView>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
