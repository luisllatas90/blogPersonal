<%@ Page Language="VB" AutoEventWireup="false" CodeFile="JustificacionDni.aspx.vb" Inherits="academico_JustificacionDni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script> 
    <script src="../../../private/jq/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        /*colocar esto en un bloque script
        ref: http://digitalbush.com/projects/masked-input-plugin/
        */

        $(document).ready(function() {
            jQuery(function($) {
                $("#TxtFechaH").mask("99/99/9999"); //.mask("(999)-999999");
                //   $("#txttelefono").mask("(999)-9999999");
                //   $("#txtcelular").mask("(999)-9999999");  
            });

        })
    </script>
</head>
<body bgcolor="#f0f0f0">
    <form id="form1" runat="server">
    <div>
    <p class="usatTitulo">AUTORIZACIÓN PARA ACTIVAR CAMPUS SIN DNI</p>
         
        <table style="width:100%;">
            <tr>
                <td colspan="3" style="width: 100%" class="usatCeldaTitulo">
                Estudiante</td>
            </tr>
            <tr>
                <td>
                <asp:DataList ID="dlEstudiante" runat="server" RepeatColumns="1" 
                    RepeatDirection="Horizontal" Width="100%" 
                    DataKeyField="codigo_Alu">
                    <ItemTemplate>
                        <table width="100%">
                            <tr>
                                <td align="center" rowspan="6" width="10%">
                                    <asp:Image ID="FotoAlumno" runat="server" Height="104px" Width="90px" 
                                        BorderColor="#666666" BorderWidth="1px" />
                                </td>
                                <td width="90%">
                                    <asp:Label ID="lblcodigo" runat="server" Text='<%# eval("codigouniver_alu") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="90%">
                                    <asp:Label ID="lblalumno" runat="server" Text='<%# eval("alumno") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="90%">
                                    <asp:Label ID="lblescuela" runat="server" Text='<%# eval("nombre_cpf") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="90%">
                                    <asp:Label ID="lblcicloingreso" runat="server" 
                                        Text='<%# "Ciclo de Ingreso: " + eval("cicloing_alu") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="90%">
                                    <asp:Label ID="lblplan" runat="server" Text='<%# eval("descripcion_pes") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="90%">
                                    <asp:Label ID="lbltipoNuevo" runat="server" Font-Bold="True" ForeColor="#000099" 
                                        Text='DNI'></asp:Label>
                                    &nbsp;<asp:Label ID="lblnrodocNuevo" runat="server" Font-Bold="True" ForeColor="#0000CC" 
                                        Text='<%# eval("dni") %>'></asp:Label>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
                </td>
            </tr>
            <tr>
                <td colspan="3" class="usatCeldaTitulo"> 
                    <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Fecha Final de Autorización :<asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtFechaH" 
                        ErrorMessage="Indique hasta que fecha se permitirá el acceso al campus" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
&nbsp;<asp:TextBox ID="TxtFechaH" runat="server" Width="90px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Observación :<asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                        runat="server" ControlToValidate="txtObservacion" 
                        ErrorMessage="Indique alguna observación de referencia para la autorización" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine" 
                        Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center">
    <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar_prp" 
        Text="          Guardar" Height="30px" ValidationGroup="Guardar" />
&nbsp;<asp:Button ID="cmdCancelar" runat="server" CssClass="noconforme1" 
        Text="       Cancelar" ValidationGroup="cancelar" Height="30px" Visible="True" />
                </td>
            </tr>
            <tr>
                <td>
        <asp:HiddenField ID="hddCodigo_alu" runat="server" />
                </td>
            </tr>
        </table>
    
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
    </form>
</body>
</html>
