<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmevaluaciones.aspx.vb" Inherits="medicina_frmevaluaciones" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <script type="text/javascript" src="../../../../private/calendario.js"></script>
    <link   href="../../../../private/estilo.css" rel="stylesheet" type="text/css"/>
    
    <script type="text/javascript" language="javascript">
    function numeros()
            {
                var key=window.event.keyCode;//codigo de tecla.
                if (key < 46 || key > 57){//si no es numero 
                window.event.keyCode=0; }//anula la entrada de texto. 
            }
     
     function activacajas(chek,orden)
        {
            if (chek.checked==true)
                {
                    eval('form1.TxtNombre_' + orden + '.disabled= false') 
                    eval('form1.TxtFechaIni_' + orden + '.disabled= false')
                    eval('form1.TxtPeso_' + orden + '.disabled= false') 
                    eval('form1.ChkEst_' + orden + '.disabled= false') 
                    eval('form1.CmdFechaIni_' + orden + '.disabled= false') 
                }
             else
                {
                    eval('form1.TxtNombre_' + orden + '.disabled= true') 
                    eval('form1.TxtFechaIni_' + orden + '.disabled= true')
                    eval('form1.TxtPeso_' + orden + '.disabled= true') 
                    eval('form1.ChkEst_' + orden + '.disabled= true') 
                    eval('form1.CmdFechaIni_' + orden + '.disabled= true') 
                }
            
        }
     
     function validaenvio(source,arguments)
        {
            var varBandera
            varBandera = false;
            for (i=1;i<=5;i++)
                {
                    if (eval('form1.ChkVer_' + i + '.checked') == true)
                        {
                            varBandera = true;
                            if (eval('form1.TxtNombre_' + i + '.value')=='')
                                varBandera = false
                            if (eval('form1.TxtFechaIni_' + i + '.value')=='')
                                varBandera = false
                            if (eval('form1.TxtPeso_' + i + '.value')=='')
                                varBandera = false
                        }
                }
            if (varBandera == true)
                arguments.IsValid = true
            else
                arguments.IsValid = false
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: black 2px solid;
            padding-right: 5px; border-top: black 2px solid; padding-left: 5px; padding-bottom: 5px;
            margin: 3px; border-left: black 2px solid; padding-top: 5px; border-bottom: black 2px solid;
            background-color: beige; height: 100%;" width="100%">
            <tr>
                <td colspan="3" rowspan="3" valign="top">
                    <table width="100%">
                        <tr>
                            <td align="center" colspan="5" style="font-weight: bold; font-size: 12px">
                                <asp:Label ID="LblTitulos" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12pt"
                                    ForeColor="#804040"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; font-size: 12px;" align="center">
                                Act.</td>
                            <td style="font-weight: bold; font-size: 12px;" align="center">
                                Nombre Actividad</td>
                            <td style="font-weight: bold; font-size: 12px" align="center">
                                Fecha</td>
                            <td style="font-weight: bold; font-size: 12px" align="center">
                                Ponderación</td>
                            <td style="font-weight: bold; font-size: 12px" align="center">
                                Activo</td>
                        </tr>
                        <tr>
                            <td style="width: 26px">
                                <asp:CheckBox ID="ChkVer_1" runat="server" /></td>
                            <td style="width: 330px">
                                <asp:TextBox ID="TxtNombre_1" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" Width="325px"></asp:TextBox></td>
                            <td align="center">
                                <input id="CmdFechaIni_1" class="cunia" onclick="MostrarCalendario('TxtFechaIni_1')" type="button" />
                                <asp:TextBox ID="TxtFechaIni_1" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" Width="70px"></asp:TextBox></td>
                            <td align="center">
                                <asp:TextBox ID="TxtPeso_1" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" MaxLength="6" Style="text-align: center"
                                    Width="44px"></asp:TextBox></td>
                            <td align="center"><asp:CheckBox ID="ChkEst_1" runat="server" Checked="True" /></td>
                        </tr>
                        <tr>
                            <td style="width: 26px">
                                <asp:CheckBox ID="ChkVer_2" runat="server" /></td>
                            <td style="width: 330px">
                                <asp:TextBox ID="TxtNombre_2" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" Width="325px"></asp:TextBox></td>
                            <td align="center">
                                <input id="CmdFechaIni_2" class="cunia" onclick="MostrarCalendario('TxtFechaIni_2')" type="button" />
                                <asp:TextBox ID="TxtFechaIni_2" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" Width="70px"></asp:TextBox></td>
                            <td align="center">
                                <asp:TextBox ID="TxtPeso_2" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" MaxLength="6" Style="text-align: center"
                                    Width="44px"></asp:TextBox></td>
                            <td align="center"><asp:CheckBox ID="ChkEst_2" runat="server" Checked="True" /></td>
                        </tr>
                        <tr>
                            <td style="width: 26px">
                                <asp:CheckBox ID="ChkVer_3" runat="server" /></td>
                            <td style="width: 330px">
                                <asp:TextBox ID="TxtNombre_3" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" Width="325px"></asp:TextBox></td>
                            <td align="center">
                                <input id="CmdFechaIni_3" class="cunia" onclick="MostrarCalendario('TxtFechaIni_3')" type="button" />
                                <asp:TextBox ID="TxtFechaIni_3" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" Width="70px"></asp:TextBox></td>
                            <td align="center">
                                <asp:TextBox ID="TxtPeso_3" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" MaxLength="6" Style="text-align: center"
                                    Width="44px"></asp:TextBox></td>
                            <td align="center"><asp:CheckBox ID="ChkEst_3" runat="server" Checked="True" /></td>
                        </tr>
                        <tr>
                            <td style="width: 26px">
                                <asp:CheckBox ID="ChkVer_4" runat="server" /></td>
                            <td style="width: 330px">
                                <asp:TextBox ID="TxtNombre_4" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" Width="325px"></asp:TextBox></td>
                            <td align="center">
                                <input id="CmdFechaIni_4" class="cunia" onclick="MostrarCalendario('TxtFechaIni_4')" type="button" />
                                <asp:TextBox ID="TxtFechaIni_4" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" Width="70px"></asp:TextBox></td>
                            <td align="center">
                                <asp:TextBox ID="TxtPeso_4" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" MaxLength="6" Style="text-align: center"
                                    Width="44px"></asp:TextBox></td>
                            <td align="center"><asp:CheckBox ID="ChkEst_4" runat="server" Checked="True" /></td>
                        </tr>
                        <tr>
                            <td style="width: 26px">
                                <asp:CheckBox ID="ChkVer_5" runat="server" /></td>
                            <td style="width: 330px">
                                <asp:TextBox ID="TxtNombre_5" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" Width="325px"></asp:TextBox></td>
                            <td align="center">
                                <input id="CmdFechaIni_5" class="cunia" onclick="MostrarCalendario('TxtFechaIni_5')" type="button" />
                                <asp:TextBox ID="TxtFechaIni_5" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" Width="70px"></asp:TextBox></td>
                            <td align="center">
                                <asp:TextBox ID="TxtPeso_5" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" MaxLength="6" Style="text-align: center"
                                    Width="44px"></asp:TextBox></td>
                            <td align="center"><asp:CheckBox ID="ChkEst_5" runat="server" Checked="True" /></td>
                        </tr>
                        <tr>
                            <td style="width: 26px">
                            </td>
                            <td style="width: 330px">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td align="center">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 26px">
                            </td>
                            <td style="width: 330px">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td align="center">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="5" style="font-weight: bold; height: 17px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="5" style="font-weight: bold; height: 17px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="5" style="font-weight: bold; height: 17px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="5" style="font-weight: bold; height: 17px">
                                &nbsp;<asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="validaenvio"
                                    ErrorMessage="Falta completar algunos datos.">*</asp:CustomValidator></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="5">
                                <asp:Button ID="CmdGuardar" runat="server" CssClass="guardar2" Text="     Guardar"
                                    Width="70px" /></td>
                        </tr>
                    </table>
                                <center><asp:Label ID="LblMensaje" runat="server"></asp:Label></center>
                                </td>
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
        </table>
    
    </div>
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
