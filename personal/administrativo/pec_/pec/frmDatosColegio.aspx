<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDatosColegio.aspx.vb" Inherits="administrativo_pec_frmDatosColegio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Datos de Institución educativa</title>
    <style type="text/css">

table {
	font-family: Trebuchet MS;
	font-size: 8pt;
}
TBODY {
	display: table-row-group;
}
tr {
	font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
	font-size: 8pt;
	color: #2F4F4F;
}
select {
	font-family: Verdana;
	font-size: 8.5pt;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table cellpadding="3" cellspacing="0" 
      style="border: 1px solid #C2CFF1; width:100%">
      <tr>
        <td bgcolor="#D1DDEF" colspan="2" height="30px">
          <b>Datos de Colegio</b></td>
      </tr>
      <tr>
        <td width="20%">
            Pais:</td>
        <td width="75%">
            <asp:DropDownList ID="cboPais" runat="server" AutoPostBack="True" Width="125px">
            </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td width="20%">
            Departamento:</td>
        <td width="75%">
            <asp:DropDownList ID="cboDpto" runat="server" AutoPostBack="True" Width="125px">
            </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td width="20%">
            Provincia:</td>
        <td width="75%">
            <asp:DropDownList ID="cboProv" runat="server" AutoPostBack="True" Width="125px">
            </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td width="20%">
            Distrito:</td>
        <td width="75%">
            <asp:DropDownList ID="cboDist" runat="server" AutoPostBack="True" Width="125px">
            </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td width="20%">
            Tipo:</td>
        <td width="75%">
            <asp:DropDownList ID="cboTipo" runat="server" AutoPostBack="True" Width="125px">
                <asp:ListItem Value="-1">-- Seleccione --</asp:ListItem>
                <asp:ListItem Value="Pública">Pública</asp:ListItem>
                <asp:ListItem>Privada</asp:ListItem>
                <asp:ListItem>Militar</asp:ListItem>
            </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td width="20%">
            Nivel: </td>
        <td width="75%">
            <asp:DropDownList ID="cboNivel" runat="server" Width="160px">
                <asp:ListItem>Secundaria</asp:ListItem>
                <asp:ListItem>Secundaria de Adultos</asp:ListItem>
            </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td width="20%">Categoria:</td>
        <td width="75%">
            <asp:DropDownList ID="cboCategoria" runat="server" Width="160px" 
                AutoPostBack="True">
            </asp:DropDownList>&nbsp;
            <asp:Label ID="lblCategoria" runat="server" Text=""></asp:Label>
</td>
      </tr>
      <tr>
        <td width="20%">
          Nombre:</td>
        <td width="75%">
          <asp:TextBox ID="txtNombre" runat="server" SkinID="CajaTextoObligatorio" 
            Width="302px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtNombre" 
                ErrorMessage="Debe ingresar el nombre del colegio" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr>
        <td width="20%">
            &nbsp;</td>
        <td width="75%">
          <asp:Label ID="LblMensaje" runat="server"></asp:Label>
        </td>
      </tr>
      <tr>
        <td width="20%">
            &nbsp;</td>
        <td width="75%">
      <asp:Button ID="CmdGuardar" runat="server" Text="Guardar" SkinID="BotonGuardar" 
        ValidationGroup="Guardar" Height="22px" Width="100px" />
      &nbsp;<asp:Button ID="cmdCancelar" runat="server" SkinID="BotonSalir" 
                                Text="Cerrar" ValidationGroup="Salir" 
                                Height="22px" Width="100px" EnableTheming="True" />
        </td>
      </tr>
    </table>
    
    </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
    </form>
</body>
</html>
