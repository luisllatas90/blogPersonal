<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SeleccionarResponsable.aspx.vb" Inherits="Equipo_SeleccionarResponsable" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Asignar Responsable</title>
    <link href="../private/estilo.css" rel ="stylesheet" type ="text/css" />
    <link href="../private/estiloweb.css" rel ="stylesheet" type ="text/css" />
<script language ="javascript" type ="text/javascript" src ="../private/funcion.js" > </script>   
</head>
<body>
    <form id="frmResponsable" runat="server">
    
        <table width="100%">
            <tr>
                <td class="TituloReq" colspan="3" height="20" align="center">
                    Asignar responsable de la solicitud</td>
            </tr>
            <tr>
                <td colspan="1" valign="top">
                    SOLICITUD</td>
                <td colspan="1" valign="top">
                    :</td>
                <td valign="top">
                    <asp:Label ID="lblSolicitud" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="1" valign="top">
                    TIPO</td>
                <td colspan="1"  valign="top">
                    :</td>
                <td valign="top">
                    <asp:Label ID="lblTipo" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="1" valign="top">
                    PRIORIDAD</td>
                <td colspan="1"  valign="top">
                    :</td>
                <td valign="top">
                    <asp:Label ID="lblPrioridad" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="1" valign="top">
                    ÁREA SOLICITANTE</td>
                <td colspan="1"  style=" height: 15px"  valign="top">
                    :</td>
                <td valign="top">
                    <asp:Label ID="lblArea" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="1" valign="top">
                    MÓDULO</td>
                <td colspan="1" style=" height: 15px" valign="top">
                    :</td>
                <td valign="top">
                    <asp:Label ID="lblModulo" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="1" valign="top">
                    REGISTRADO POR</td>
                <td colspan="1" style=" height: 15px" valign="top">
                    :</td>
                <td valign="top">
                    <asp:Label ID="lblRegistradoPor" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3" style="height:1px; background-color: #003366"></td>
            </tr>
            <tr>
                <td colspan="3">
                    <strong>Responsables:</strong></td>
            </tr>
            <tr>
                <td colspan="3" valign="top" height="100px">
                    <asp:RadioButtonList ID="RblEquipo" runat="server">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Button ID="CmdGuardar" runat="server" CssClass="guardar" Text="Guardar" Width="85px" />
                    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="CmdCerrar" runat="server" CssClass="cerrar" Text="Cerrar" Width="85px" /></td>
            </tr>
        </table>
    
   
    </form>
</body>
</html>
