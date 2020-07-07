<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EncuestaBoletin.aspx.vb" Inherits="Encuesta_PyD_EncuestaBoletin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../../../private/funciones.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td align="center" bgcolor="#3366CC" height="17" style="color: #FFFFFF">
                    <b>ENCUESTA: BOLETÍN INFORMATIVO</b></td>
            </tr>
            <tr>
                <td height="1">
                     </td>
            </tr>
            <tr>
                <td bgcolor="#003399" height="1">
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    1. ¿Te interesaría recibir un boletín informativo de la Universidad?<asp:RequiredFieldValidator ID="rfvPreg1" 
                        runat="server" ControlToValidate="rblInteresado" 
                        ErrorMessage="La pregunta 1 es obligatoria" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rblInteresado" runat="server" 
                        RepeatDirection="Horizontal" Width="145px" AutoPostBack="True">
                        <asp:ListItem>Si</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    2. ¿Qué temas te gustaría que contenga este boletín?<asp:RequiredFieldValidator 
                        ID="rfvPreg2" runat="server" ControlToValidate="rblTemas" 
                        ErrorMessage="La pregunta 2 es obligatoria" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rblTemas" runat="server" AutoPostBack="True">
                        <asp:ListItem Value="1">Actividades académicas, deportivas y culturales de interés estudiantes</asp:ListItem>
                        <asp:ListItem Value="2">Sólo actividades  académicas estudiantes</asp:ListItem>
                        <asp:ListItem Value="3">Sólo actividades deportivas estudiantes</asp:ListItem>
                        <asp:ListItem Value="4">Sólo actividades culturales estudiantes</asp:ListItem>
                        <asp:ListItem Value="5">Otros</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:TextBox ID="txtOtro" runat="server" Enabled="False" Width="441px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtOtro" runat="server" 
                        ControlToValidate="txtOtro" Enabled="False" 
                        ErrorMessage="Si marcó la opción otros en la pregunta 2, deberá especificar un tema" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    3. ¿Dé que manera te gustaría recibir el boletín?<asp:RequiredFieldValidator ID="rfvPreg3" 
                        runat="server" ControlToValidate="rblMedio" 
                        ErrorMessage="La pregunta 3 es obligatoria" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rblMedio" runat="server" RepeatDirection="Horizontal" 
                        AutoPostBack="True">
                        <asp:ListItem Value="1">Impresa</asp:ListItem>
                        <asp:ListItem Value="2">Correos electrónicos</asp:ListItem>
                        <asp:ListItem Value="3">Campus virtual</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    4. ¿Cuántas páginas desearía que tenga este boletín para tu mejor lectura?<asp:RequiredFieldValidator 
                        ID="rfvPreg4" runat="server" ControlToValidate="rblPaginas" 
                        ErrorMessage="La pregunta 4 es obligatoria" ValidationGroup="Guardar" 
                        Enabled="False">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rblPaginas" runat="server" CellSpacing="4" 
                        RepeatDirection="Horizontal" Enabled="False">
                        <asp:ListItem>2 Pág.</asp:ListItem>
                        <asp:ListItem>4 Pág.</asp:ListItem>
                        <asp:ListItem>8 Pág.</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    5. ¿Cada cuánto tiempo te gustaría leerlo?<asp:RequiredFieldValidator 
                        ID="rfvPreg5" runat="server" ControlToValidate="rblTiempo" 
                        ErrorMessage="La pregunta 5 es obligatoria" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rblTiempo" runat="server" 
                        RepeatDirection="Horizontal" Width="302px">
                        <asp:ListItem>Quincenal</asp:ListItem>
                        <asp:ListItem>Mensual</asp:ListItem>
                        <asp:ListItem>Bimestral</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td bgcolor="#003399" height="1">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                        ValidationGroup="Guardar" Width="70px" style="height: 26px" />
&nbsp;<asp:Button ID="cmdCerrar" runat="server" onclientclick="window.close()" Text="Cerrar" 
                        Width="70px" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#003399" height="1">
                </td>
            </tr>
        </table>
    
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
    </form>
</body>
</html>
