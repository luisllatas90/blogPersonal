<%@ Page Language="VB" AutoEventWireup="false" CodeFile="importasylabus.aspx.vb" Inherits="medicina_importasylabus" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Importar Sylabus</title>
     <link   href="../../../../private/estilo.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
           <table width="100%" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; background-color: #f5f5dc" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3" rowspan="3">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3" style="font-weight: bold; font-size: 11pt; color: #804040; font-family: verdana" align="center">
                    &nbsp;Importar Sylabus</td>
            </tr>
            <tr>
                <td colspan="3" align="center" valign="top">
                    <table width="95%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="3" rowspan="3" style="font-weight: bold; font-size: 8pt; color: #2f4f4f; font-family: verdana">
                                Indicaciones</td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                            <td colspan="3" rowspan="1" style="border-right: black 1px solid; padding-right: 5px; border-top: black 1px solid; padding-left: 5px; font-size: 8pt; padding-bottom: 5px; border-left: black 1px solid; color: midnightblue; padding-top: 5px; border-bottom: black 1px solid; font-family: verdana; background-color: yellow; text-align: justify">
                                Seleccione el ciclo académico, a continuación se mostrarán la relación de cursos
                                en lo cual haya registrado un sylabus, esto le permitirá copiar la información como
                                sylabus actual. Si desea importar tambien las actividades marque la casilla inferior.</td>
                        </tr>
                    </table>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" style="font-weight: bold; font-size: 8pt; color: #2f4f4f; font-family: verdana">
                    &nbsp; Ciclo Académico :
                    <asp:DropDownList ID="DDLCiclo" runat="server" AutoPostBack="True" style="border-right: black 1px solid; border-top: black 1px solid; font-weight: normal; font-size: 8pt; border-left: black 1px solid; color: black; border-bottom: black 1px solid; font-family: verdana; background-color: #f5f5dc">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="3" style="height: 150px; padding-left: 5px;" valign="top">
                    <asp:RadioButtonList ID="RbSylabus" runat="server" style="font-size: 8pt; color: #660000; font-family: verdana">
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="padding-left: 5px; font-size: 8pt; font-family: verdana">
                    <asp:CheckBox ID="ChkActividades" runat="server" Text="Incluir actividades." /></td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="padding-left: 5px; font-size: 8pt; font-family: verdana">
                    <asp:CheckBox ID="ChkEvaluaciones" runat="server" Text="Incluir Evaluaciones." /></td>
            </tr>
            <tr>
                <td align="center" colspan="3" style="height: 32px">
                    &nbsp;<asp:Label ID="LblMensaje" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Button ID="CmdGuardar" runat="server" Text="    Guardar" CssClass="guardar2" Height="25px" Width="72px" /></td>
            </tr>
        </table>
                    &nbsp;</td>
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
        </table>
    </form>
</body>
</html>
