<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CursosElectivosArquitectura.aspx.vb" Inherits="Encuesta_Escuela_CursosElectivosArquitectura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
     function ValidarSeleccion(source, arguments) {
         var total;
         var nro;
         //var valor;
         //var texto;
         nro = 0;
         total = 26;
         eval("document.form1.txtSeleccionados.value = ''");
         for (i = 1; i <= total; i++) {
             if (eval("document.form1.chkOpcion" + i + ".checked") == true) {
                 //valor = eval("document.form1.chkOpcion" + i + ".id");
                 //texto = eval("document.form1.txtSeleccionados.value") + "|" + valor;
                 //eval("document.form1.txtSeleccionados.value = '" + texto + "'");
                 nro = nro + 1;
             }
         } 
         //alert(eval("document.form1.txtSeleccionados.value"));
         if (nro >1 && nro<= 4)
             arguments.IsValid = true;
         else
             arguments.IsValid = false; 
     }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;" cellspacing="0">
            <tr>
                <td style="text-align: center" bgcolor="#F3F3F3">
                    <b style="font-size: small">ENCUESTA ARQUITECTURA</b></td>
            </tr>
            <tr>
                <td bgcolor="#F3F3F3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td bgcolor="#F3F3F3">
                    Marque de 1 a 4 cursos que desea que se programen para el ciclo de verano 
                    2011-0:<asp:CustomValidator ID="CustomValidator1" runat="server" 
                        ClientValidationFunction="ValidarSeleccion" 
                        ErrorMessage="Debe seleccionar entre 1 y 4 opciones" 
                        ValidationGroup="Guardar">*</asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td bgcolor="#F3F3F3">
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td width="48%" 
                                style="border: 1px solid #C0C0C0; color: #000000; font-weight: bold; font-family: Tahoma; background-color: #CAED81">
                                Ciclo I                             </td>
                            <td>
                                &nbsp;</td>
                            <td width="48%" 
                                style="border: 1px solid #C0C0C0; color: #000000; font-weight: bold; font-family: Tahoma; background-color: #CAED81">
                                Ciclo II</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkOpcion1" runat="server" Text="Técnicas Graficas I" 
                                    ValidationGroup="Guardar" />
                                <br />
                                <asp:CheckBox ID="chkOpcion2" runat="server" Text="Teoría y composición" />
                                <br />
                                <asp:CheckBox ID="chkOpcion3" runat="server" 
                                    Text="Metodología del trabajo intelectual" />
                                <br />
                                <asp:CheckBox ID="chkOpcion4" runat="server" Text="Geometría descriptiva" />
                                <br />
                                <asp:CheckBox ID="chkOpcion5" runat="server" Text="Matemática Básica" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:CheckBox ID="chkOpcion6" runat="server" Text="Fundamentos Urbanos" />
                                <br />
                                <asp:CheckBox ID="chkOpcion7" runat="server" Text="Técnicas Graficas II" />
                                <br />
                                <asp:CheckBox ID="chkOpcion8" runat="server" 
                                    Text="Teoría de la arquitectura" />
                                <br />
                                <asp:CheckBox ID="chkOpcion9" runat="server" Text="Comunicación" />
                                <br />
                                <asp:CheckBox ID="chkOpcion10" runat="server" 
                                    Text="Fundamentos estructurales" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #C0C0C0; color: #000000; font-weight: bold; font-family: Tahoma; background-color: #CAED81">
                                Ciclo III</td>
                            <td>
                                &nbsp;</td>
                            <td style="border: 1px solid #C0C0C0; color: #000000; font-weight: bold; font-family: Tahoma; background-color: #CAED81">
                                Ciclo IV</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkOpcion11" runat="server" Text="Filosofía" />
                                <br />
                                <asp:CheckBox ID="chkOpcion12" runat="server" 
                                    Text="Morfología del terreno" />
                                <br />
                                <asp:CheckBox ID="chkOpcion13" runat="server" Text="Fundamentos de gestión" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:CheckBox ID="chkOpcion14" runat="server" 
                                    Text="Antropología filosófica" />
                                <br />
                                <asp:CheckBox ID="chkOpcion15" runat="server" 
                                    Text="Metodología de la investigación científica" />
                                <br />
                                <asp:CheckBox ID="chkOpcion16" runat="server" 
                                    Text="Materiales de construcción" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #C0C0C0; color: #000000; font-weight: bold; font-family: Tahoma; background-color: #CAED81">
                                Ciclo V</td>
                            <td>
                                &nbsp;</td>
                            <td style="border: 1px solid #C0C0C0; color: #000000; font-weight: bold; font-family: Tahoma; background-color: #CAED81">
                                Ciclo VI</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkOpcion17" runat="server" Text="Fe y cultura " />
                                <br />
                                <asp:CheckBox ID="chkOpcion18" runat="server" 
                                    Text="Procedimientos constructivos" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:CheckBox ID="chkOpcion19" runat="server" Text="Diseño estructural I " />
                                <br />
                                <asp:CheckBox ID="chkOpcion20" runat="server" 
                                    Text="Estadística y probabilidades" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                                </td>
                            <td>&nbsp;
                                </td>
                            <td>&nbsp;
                                </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #C0C0C0; color: #000000; font-weight: bold; font-family: Tahoma; background-color: #CAED81">
                                Ciclo VII</td>
                            <td>
                                &nbsp;</td>
                            <td style="border: 1px solid #C0C0C0; color: #000000; font-weight: bold; font-family: Tahoma; background-color: #FFFF99">
                                Cursos Electivos</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:CheckBox ID="chkOpcion21" runat="server" Text="Moral y ética" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:CheckBox ID="chkOpcion22" runat="server" Text="Fotografía" />
                                <br />
                                <asp:CheckBox ID="chkOpcion23" runat="server" Text="Pintura y escultura" />
                                <br />
                                <asp:CheckBox ID="chkOpcion24" runat="server" Text="Taller de restauración" />
                                <br />
                                <asp:CheckBox ID="chkOpcion25" runat="server" Text="Estructuras en bambú" />
                                <br />
                                <asp:CheckBox ID="chkOpcion26" runat="server" Text="Ecotec" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" height="1" style="visibility: hidden">
                                <asp:TextBox ID="txtSeleccionados" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" Width="75px" 
                        ValidationGroup="Guardar" />
&nbsp;<asp:Button ID="cmdCerrar" runat="server" Text="Cerrar" Width="75px" />
                </td>
            </tr>
        </table>
    
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
    </form>
</body>
</html>
