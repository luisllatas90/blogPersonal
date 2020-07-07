<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistraCarreras.aspx.vb" Inherits="administrativo_pec_frmRegistraCarreras" Theme="Acero" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Registrar Escuelas Profesionales</title>
   <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>

</head>
<body>
    <form id="form1" runat="server">
       <p class="usatTitulo">Ficha de Registro de Estudios<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager></p>
        <table cellpadding="3" cellspacing="0" 
      style="border: 1px solid #C2CFF1; width:100%">
      <tr>
        <td bgcolor="#D1DDEF" colspan="2" height="30px">
          <b>Datos de Estudios</b></td>
      </tr>
      <tr>
        <td width="20%">
          Nombre</td>
        <td width="75%">
          <asp:TextBox ID="TxtNombreEscuela" runat="server" SkinID="CajaTextoObligatorio" 
            Width="302px"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="TxtNombreEscuela" ErrorMessage="Nombre de Escuela requerido" 
            SetFocusOnError="True" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr>
        <td width="20%">
          Abreviatura</td>
        <td width="75%">
          <asp:TextBox ID="TxtAbreviatura" runat="server" MaxLength="15" 
            SkinID="CajaTextoObligatorio" Width="78px"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="TxtAbreviatura" ErrorMessage="Abreviatura requerido" 
            SetFocusOnError="True" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr>
        <td width="20%">
          Vigencia</td>
        <td width="75%">
          <asp:CheckBox ID="ChkVigencia" runat="server" Text="Habilitar" />
        </td>
      </tr>
      <tr>
        <td width="20%">
          Tipo</td>
        <td width="75%">
          <asp:DropDownList ID="DDLTipoEscuela" runat="server" AutoPostBack="True" 
            SkinID="ComboObligatorio">
          </asp:DropDownList>
          <asp:RangeValidator ID="RangeValidator1" runat="server" 
            ControlToValidate="DDLTipoEscuela" ErrorMessage="Seleccione tipo de Escuela" 
            MaximumValue="1000" MinimumValue="0" SetFocusOnError="True" Type="Integer" 
            ValidationGroup="Guardar">*</asp:RangeValidator>
        </td>
      </tr>
      <tr>
        <td width="20%">
          Sub Tipo</td>
        <td width="75%">
          <asp:DropDownList ID="DDlSubTipo" runat="server" SkinID="ComboObligatorio">
          </asp:DropDownList>
          <asp:RangeValidator ID="RangeValidator2" runat="server" 
            ControlToValidate="DDlSubTipo" ErrorMessage="Selecciones sub Tipo" 
            MaximumValue="1000" MinimumValue="0" SetFocusOnError="True" Type="Integer" 
            ValidationGroup="Guardar">*</asp:RangeValidator>
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
      <asp:Button ID="CmdGuardar" runat="server" Text="Button" SkinID="BotonGuardar" 
        ValidationGroup="Guardar" />
      <asp:Button ID="CmdCancelar" runat="server" Text="Button" 
        SkinID="BotonCancelar" />
        </td>
      </tr>
    </table>
       <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
         ShowMessageBox="True" ValidationGroup="Guardar" ShowSummary="False" />
       <asp:HiddenField ID="HddCodigo_Cpf" runat="server" />
    </form>
</body>
</html>
