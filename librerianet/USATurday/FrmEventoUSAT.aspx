<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmEventoUSAT.aspx.vb" Inherits="MKTE_FrmEventoUSAT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
        }
        .style3
        {
            width: 208px;
            background-color: #0066FF;
        }
        .style4
        {
            width: 208px;
        }
        .style5
        {
            width: 208px;
            height: 34px;
            background-color: #0066FF;
        }
        .style6
        {
            height: 34px;
        }
        .style7
        {
            width: 208px;
            height: 32px;
            background-color: #0066FF;
        }
        .style8
        {
            height: 32px;
        }
        .style9
        {
            width: 208px;
            height: 42px;
            background-color: #0066FF;
        }
        .style10
        {
            height: 42px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table class="style1">
        <tr>
            <td class="style2" colspan="2">
                <img src="banner.jpg" height="300px" width="700px" />
            </td>
        </tr>
        <tr>
            <td class="style5" style="color: #FFFFFF">
                Escuela profesional de su interés:</td>
            <td class="style6">
                <asp:DropDownList ID="ddlEscuela" runat="server" Height="25px" Width="334px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Administración de empresas</asp:ListItem>
                    <asp:ListItem>Administración Hotelera y de Servicios</asp:ListItem>
                    <asp:ListItem>Contabilidad</asp:ListItem>
                    <asp:ListItem>Economía</asp:ListItem>
                    <asp:ListItem>Derecho</asp:ListItem>
                    <asp:ListItem>Comunicación</asp:ListItem>
                    <asp:ListItem>Educación Primaria</asp:ListItem>
                    <asp:ListItem>Educación Secundaria</asp:ListItem>
                    <asp:ListItem>Filosofía y Teología</asp:ListItem>
                    <asp:ListItem>Arquitectura</asp:ListItem>
                    <asp:ListItem>Ingeniería de Sistemas y Computación</asp:ListItem>
                    <asp:ListItem>Ingeniería Industrial</asp:ListItem>
                    <asp:ListItem>Ingeniería Civil Ambiental</asp:ListItem>
                    <asp:ListItem>Ingeniería Mecánica Eléctrica</asp:ListItem>
                    <asp:ListItem>Ingeniería Naval</asp:ListItem>
                    <asp:ListItem>Medicina</asp:ListItem>
                    <asp:ListItem>Enfermería</asp:ListItem>
                    <asp:ListItem>Odontología</asp:ListItem>
                    <asp:ListItem>Psicología</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="ddlEscuela" 
                    ErrorMessage="Debe seleccionar la escuela de su interés!!!">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style3" style="color: #FFFFFF">
                Nombres Completos:</td>
            <td>
                <asp:TextBox ID="txtNombres" runat="server" MaxLength="100" Width="337px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtNombres" 
                    ErrorMessage="Debe ingresar su nombre completo!!!">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style3" style="color: #FFFFFF">
                Apellidos Completos:</td>
            <td>
                <asp:TextBox ID="txtApellidos" runat="server" MaxLength="100" Width="337px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtApellidos" 
                    ErrorMessage="Debe ingresar sus apellidos completos!!!">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style3" style="color: #FFFFFF">
                Email:</td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" MaxLength="30" Width="337px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtEmail" ErrorMessage="Debe ingresar su email !!!">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtEmail" ErrorMessage="Debe ingresar un email válido !!!" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="style3" style="color: #FFFFFF">
                Dirección:</td>
            <td>
                <asp:TextBox ID="txtDireccion" runat="server" MaxLength="100" Width="335px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="txtDireccion" ErrorMessage="Debe ingresar su dirección !!!">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style3" style="color: #FFFFFF">
                Teléfono Fijo:</td>
            <td>
                <asp:TextBox ID="txtTelefFijo" runat="server" MaxLength="30" Width="207px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3" style="color: #FFFFFF">
                Teléfono Celular:</td>
            <td>
                <asp:TextBox ID="txtTelefCelular" runat="server" Width="203px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style7" style="color: #FFFFFF">
                Grado de Estudios:</td>
            <td class="style8">
                <asp:DropDownList ID="ddlGradoEstudios" runat="server" Height="25px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Estudiante de 5to Secundaria</asp:ListItem>
                    <asp:ListItem>Egresado de Secundaria </asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="ddlGradoEstudios" 
                    ErrorMessage="Debe seleccionar su grado de estudios actual !!!">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style9" style="color: #FFFFFF">
                Como se enteró del evento:</td>
            <td class="style10">
                <asp:DropDownList ID="ddlMedioComunicacion" runat="server" Height="25px" 
                    Width="196px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Facebook</asp:ListItem>
                    <asp:ListItem>Web USAT</asp:ListItem>
                    <asp:ListItem>Email</asp:ListItem>
                    <asp:ListItem>Amigos</asp:ListItem>
                    <asp:ListItem>TV</asp:ListItem>
                    <asp:ListItem>Radio</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="ddlMedioComunicacion" 
                    ErrorMessage="Debe seleccionar la primera forma como se enteró  del evento !!!">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td>
                <br />
                <asp:Button ID="btnGrabar" runat="server" Text="Registrar mi participación" 
                    Width="194px" />
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td>
                <asp:Label ID="lblMensaje" runat="server" ForeColor="#FF3300"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <div>
    
    </div>
    </form>
</body>
</html>
