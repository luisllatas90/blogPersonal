<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmregistrarexpedientes.aspx.vb" Inherits="recdo_frmregistrarexpedientes" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Expedientes</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   <p class="usatTitulo">
       <asp:Label ID="lblTitulo" runat="server" Text="Registro de Expedientes para Rectorado"></asp:Label></p>
   <p style="text-align:right">Buscar Expediente: 
       <asp:DropDownList ID="lstExpedientes" runat="server">
       </asp:DropDownList>
       <asp:ImageButton ID="imgBuscar" runat="server" 
           ImageUrl="../../images/buscar.gif" ValidationGroup="Cancelar" />
   </p>
    <asp:HiddenField ID="hdIdArchivo" runat="server" Value="0" />
    <table style="width:100%" class="contornotabla">
        <tr>
            <td>
                Número de Expediente</td>
            <td>
                <asp:TextBox ID="txtnumeroexpediente" runat="server" MaxLength="15"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtnumeroexpediente" 
                    ErrorMessage="Ingrese el número de expediente">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Tipo</td>
            <td>
                <asp:DropDownList ID="dpTipo" runat="server">
                </asp:DropDownList>
&nbsp;Número
                <asp:TextBox ID="txtnumeroTipo" runat="server" MaxLength="15"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Fecha<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtFecha" ErrorMessage="Ingrese la Fecha">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtHora" ErrorMessage="Ingrese la Hora">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtFecha" runat="server" MaxLength="10"></asp:TextBox>
&nbsp;Hora:
                <asp:TextBox ID="txtHora" runat="server" MaxLength="5"></asp:TextBox>
&nbsp;Formato 24 horas. Ej. 15:23</td>
        </tr>
        <tr>
            <td>
                Procedencia</td>
            <td>
                <asp:DropDownList ID="dpProcedencia" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Asunto<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtasunto" ErrorMessage="Ingrese el asunto">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtasunto" runat="server" Rows="2" 
                    Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Observaciones</td>
            <td>
                <asp:TextBox ID="txtObs" runat="server" Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" class="usatTitulo">Movimientos del Expediente<asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCargo" 
                    ErrorMessage="Ingrese el número de cargo de envio" 
                    ValidationGroup="Movimientos">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Dirigido a:</td>
            <td>
                <asp:DropDownList ID="dpDirigido" runat="server" ValidationGroup="Movimientos">
                </asp:DropDownList>
            &nbsp;Cargo:
                <asp:TextBox ID="txtCargo" runat="server" ValidationGroup="Movimientos"></asp:TextBox>
            &nbsp; Obs:
                <asp:TextBox ID="txtObsCargo" runat="server" ValidationGroup="Movimientos"></asp:TextBox>
                <asp:ImageButton ID="imgAgregar" runat="server" 
                    ImageUrl="../../images/agregar.gif" ValidationGroup="Movimientos" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
        <asp:GridView ID="grwMovimientos" runat="server" AutoGenerateColumns="False" 
            CellPadding="3" DataKeyNames="idmovimiento" Width="80%">
            <Columns>
<asp:BoundField HeaderText="Fecha" DataField="fechamovimiento"></asp:BoundField>
                <asp:BoundField DataField="horamovimiento" HeaderText="Hora" />
                <asp:BoundField HeaderText="Dirigido a" DataField="areadestino" />
                <asp:BoundField HeaderText="Número Cargo" DataField="numcargo" />
                <asp:BoundField HeaderText="Obs" DataField="motivo" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
            <HeaderStyle BackColor="#7AA9E2" ForeColor="Maroon" />
        </asp:GridView>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ShowMessageBox="True" ShowSummary="False" />
            </td>
        </tr>
        <tr style="background-color:Gray">
            <td colspan="2" style="text-align:right">
                <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" />
            </td>
        </tr>
    </table>
</form>
</body>
</html>
