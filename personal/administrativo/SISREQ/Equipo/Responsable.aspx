<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Responsable.aspx.vb" Inherits="Responsable" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../private/estilo.css" rel ="stylesheet" type ="text/css" />
    <link href="../private/estiloweb.css" rel ="stylesheet" type ="text/css" />
    <script language="javascript" type="text/javascript">
    function validarlista(source, arguments)
    {
        if (document.frmresponsable.LstAsignados.length == 0 )
            arguments.IsValid = false
        else
            arguments.IsValid = true
    }
    </script>
    <title>Asignar Responsable</title>
</head>
<body>
    <form id="frmresponsable" runat="server">
    <div style="text-align: center">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" 
                    height="25" class="TituloReq">
                                Asignar Equipo Responsable de la Solicitud</td>
            </tr>
            <tr>
                <td align="center">
                                <img alt="" src="../images/raya980b.gif" width="100%" /></td>
            </tr>
            <tr>
                <td align="center">
                    <table align="center" width="100%">
                        <tr>
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="3" align="center">
                                <table width="100%" align="center" cellpadding="3" cellspacing="3" >
                                    <tr>
                                        <td width="140">
                                            <b>SOLICITUD</b></td>
                                        <td style="width: 5px; height: 15px">
                                            :</td>
                                        <td>
                                            <asp:Label ID="LblSolicitud" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>PRIORIDAD&nbsp;</b></td>
                                        <td style="width: 5px; height: 15px">
                                            :</td>
                                        <td>
                                            <asp:Label ID="LblPrioridad" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>TIPO DE SOLICITUD</b></td>
                                        <td style="width: 5px; height: 15px;">
                                            :</td>
                                        <td>
                                            <asp:Label ID="LblTipoSol" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>ÁREA</b></td>
                                        <td style="width: 5px; height: 18px">
                                            :</td>
                                        <td>
                                            <asp:Label ID="LblArea" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>REGISTRADO POR</b></td>
                                        <td style="width: 5px; height: 18px">
                                            :</td>
                                        <td>
                                            <asp:Label ID="LblRegistradoPor" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height:1px; background-color: #003366">
                                </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 15px; width: 734px;">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table align="center">
                                    <tr>
                                        <td style="width: 304px; height: 25px;">
                                            Personal del Área</td>
                                        <td style="width: 65px; height: 25px;">
                                        </td>
                                        <td style="height: 25px">
                                            Equipo Responsable</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 304px;">
                                            <asp:ListBox ID="LstEquipo" runat="server" Height="143px" SelectionMode="Multiple" Width="289px"></asp:ListBox></td>
                                        <td style="width: 65px; text-align: center">
                                            <asp:Button ID="cmdAgregar" runat="server" Font-Bold="True" Text=">>" /><br />
                                            <br />
                                            <asp:Button ID="CmdQuitar" runat="server" Font-Bold="True" Text="<<" /></td>
                                        <td>
                                            <asp:ListBox ID="LstAsignados" runat="server" Height="143px" SelectionMode="Multiple" Width="289px"></asp:ListBox>
                                            <asp:CustomValidator ID="CvAsignado" runat="server" ErrorMessage="Usted debe seleccionar por lo menos un miembro del equipo" ClientValidationFunction="validarlista" ValidationGroup="Guardar">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 304px">
                                        </td>
                                        <td style="width: 65px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="Guardar" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="CmdGuardar" runat="server" Text="Guardar" Width="88px" ValidationGroup="Guardar" CssClass="Guardar" />
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                <input id="CmdCancelar" style="width: 88px" type="button" value="Cancelar" onclick="javascript:location.href='AsignarResponsable.aspx'" class="Cancelar" /></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 15px; width: 734px;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
