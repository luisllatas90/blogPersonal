<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmModificarAutores.aspx.vb"
    Inherits="frmModificarAutores" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Actualización de autores de tesis</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>

    <script src="../../../private/jq/jquery.js" type="text/javascript"></script>

    <script src="../../../private/jq/jquery.mascara.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">
        Actualización de autores de Tesis
        <asp:Label ID="lblcodigo" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px"
            ForeColor="Maroon"></asp:Label><asp:HiddenField ID="hdCodigo_Eti" runat="server"
                Value="4" />
    </p>
    <table width="100%" cellpadding="3" cellspacing="0">
        <tr>
            <td>
                <b>titulo</b>
            </td>
            <td>
                <asp:Label runat="server" ID="lblTitulo" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <b>Semestre Académico</b>
            </td>
            <td>
                <asp:DropDownList ID="ddlCiclo" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <b>Carrera Profesional</b>
            </td>
            <td>
                <asp:DropDownList ID="dpEscuela" runat="server" AutoPostBack="True" Width="600">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <b>Investigador</b>
            </td>
            <td>
                <asp:DropDownList ID="cboAutor" runat="server" DataTextField="alumno" DataValueField="codigo_alu"
                    Enabled="False">
                    <asp:ListItem Value='-1'>-- seleccione alumno --</asp:ListItem>
                </asp:DropDownList>
                <asp:CompareValidator runat="server" ID="rqAutor" ControlToValidate="cboAutor" ErrorMessage="Debe seleccionar un alumno"
                    Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
                <b>Motivo</b>
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtMotivo" TextMode="MultiLine" Rows="4" Width="500"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RqResolucion" runat="server" ControlToValidate="txtMotivo"
                    ErrorMessage="Debe ingresar el motivo de asignación de alumno">*</asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <p style="text-align: right">
        <asp:Button ID="cmdGuardar" runat="server" Text="  Agregar" CssClass="guardar" />
        &nbsp;<input id="cmdCancelar" type="button" value="Cerrar" onclick="self.parent.tb_remove()"
            class="salir" />&nbsp;</p>
    <asp:GridView ID="grwAutor" runat="server" AutoGenerateColumns="False" BorderColor="#628BD7"
        Width="100%" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" ForeColor="#333333"
        ShowHeader="true" DataKeyNames="codigo_RTes,codigo_alu">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="codigouniver_alu" HeaderText="Código" ItemStyle-Width="10%"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="alumno" HeaderText="Apellidos y Nombres" ItemStyle-Width="45%">
            </asp:BoundField>
            <asp:BoundField DataField="descripcion_Tpi" HeaderText="Tipo Participante" ItemStyle-Width="10%"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="motivo" HeaderText="motivo" ItemStyle-Width="25%"></asp:BoundField>
            <asp:CommandField ShowDeleteButton="True" ControlStyle-Width="5%"></asp:CommandField>
        </Columns>
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    </form>
</body>
</html>
