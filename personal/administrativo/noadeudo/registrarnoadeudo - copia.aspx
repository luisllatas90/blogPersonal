<%@ Page Language="VB" AutoEventWireup="false" CodeFile="registrarnoadeudo.aspx.vb" Inherits="noadeudo_registrarnoadeudo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrar solicitud de constancia de no adeudos</title>
     <link href="../../../private/estilo.css"rel="stylesheet" type="text/css" /> 
    <link href="../../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <link href="../../../private/estiloctrles.css" rel="stylesheet" type="text/css" /> 
    <style type="text/css">
        .style1
        {
            width: 124px;
        }
        .style2
        {
            width: 291px;
        }
        .style3
        {
            width: 124px;
            height: 17px;
        }
        .style4
        {
            width: 291px;
            height: 17px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table align="center" style="width: 70%;">
        <tr>
            <td align="center" class="usatTitulo">
                Registrar solicitud de constancia de no adeudo</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Buscar por código Universitario:&nbsp;&nbsp; <asp:TextBox ID="txtCodigoUniversitario" runat="server"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtcodigo_alu" ErrorMessage="Debe seleccionar un alumno" 
                    ValidationGroup="correo">*</asp:RequiredFieldValidator>
                &nbsp;
                <asp:Button ID="cmdConsultarDatos" runat="server" Text="Buscar" 
                    CssClass="Buscar" Height="29px" Width="110px" />
            </td>
        </tr>
        <tr>
            <td>
                <hr class="aprobar" noshade="noshade" />
            </td>
        </tr>
        <tr>
            <td class="contornotabla">
                <table style="width:100%;">
                    <tr>
                        <td class="style1">
                            Cod</td>
                        <td class="style2">
                            :&nbsp;&nbsp;
                            <asp:TextBox ID="txtcodigo_alu" runat="server" BorderStyle="None" 
                                Enabled="False" ValidationGroup="correo"></asp:TextBox>
                        </td>
                        <td rowspan="7" align="center">
                            <asp:Image ID="FotoAlumno" runat="server" Height="140px" Width="122px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Código Universitario</td>
                        <td class="style2">
                            :&nbsp;&nbsp;
                            <asp:Label ID="lblCodigoUniversitario" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Apellidos y Nombres</td>
                        <td class="style2">
                            :&nbsp;&nbsp;
                            <asp:Label ID="lblApellidosNombres" runat="server"></asp:Label>
                            </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Esuela Profesional</td>
                        <td class="style2">
                            :&nbsp;&nbsp;
                            <asp:Label ID="lblEscuelaProfesional" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            Ciclo Ingreso</td>
                        <td class="style4">
                            :&nbsp;&nbsp;
                            <asp:Label ID="lblCicloIngreso" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            Plan</td>
                        <td class="style4">
                            :&nbsp;&nbsp;
                            <asp:Label ID="lblPlan" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                Correo electrónico</td>
                        <td class="style4">
                            :&nbsp;&nbsp; <asp:TextBox ID="txtCorreo" runat="server" Width="235px" 
                                ValidationGroup="correo"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtCorreo" 
                                ErrorMessage="Ingrese la dirección de correo actual" ValidationGroup="correo">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server" Font-Bold="False" Font-Size="Small" 
                    ForeColor="Red" Visible="False"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ShowMessageBox="True" ValidationGroup="correo" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="cmdAceptar" runat="server" Text="Aceptar" CssClass="guardar" 
                    Height="25px" Width="100px" Enabled="False" ValidationGroup="correo" />
&nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" CssClass="guardar" 
                    Height="25px" Width="100px" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
