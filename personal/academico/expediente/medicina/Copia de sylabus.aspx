
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Copia de sylabus.aspx.vb" Inherits="copia_de_medicina_sylabus" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
     <link rel="stylesheet" type="text/css" href="../../../../private/estilo.css"/>
    <script type="text/javascript" src="../../../../private/funciones.js"></script>
    <script type="text/javascript">
    function enviacombo()
        {   
            <%response.write("pag='codigo_cac="& request.querystring("codigo_cac") &"&codigo_syl=" & me.HidenCodigoSyl.value & "&codigo_per=" & request.querystring("codigo_per") & "&codigo_cup=" & request.querystring("codigo_cup")& "&nombre_per="& request.querystring("nombre_per") & "&nombre_cur=" & request.querystring("nombre_cur") & "'")%>
                                 
            if (form1.DDLAccion.value==1)
                { 
                  location.href='evaluacion.aspx?' + pag
                  return false; }
            else 
                if (form1.DDLAccion.value==2)
                    {location.href='salida.aspx?' + pag
                     return false; }
                else
                    if (form1.DDLAccion.value==3)
                        {location.href='listaevaluaciones.aspx?' + pag
                         return false;  } 
                    else
                        {location.href='consolidadoalumnos.aspx?' + pag
                        return false;  } 
            }
    
    function validaenvio()
        {  if (form1.HidenCodigoSyl.value=="")
                if (confirm("¿Desea Registrar el Sylabus?")==true)
                    return true;
                else
                    return false;
            else
                if (confirm("¿Desea guardar los cambios hechos al Sylabus?")==true)
                    return true;
                else
                    return false;}
     
    </script>
    <style type="text/css">
        .style1
        {
            height: 19px;
        }
    </style>
</head>
<body  style="margin:0,0,0,0">
    <form id="form1" runat="server">
    <div>
        <table style="height: 100%" width="100%">
            <tr>
                <td align="center" colspan="3" style="border-top: black 1px solid; font-weight: bold; font-size: 11pt; color: white; border-bottom: black 1px solid; font-family: verdana; height: 21px; background-color: firebrick; text-align: center">
                    Registro de Sylabus</td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:HyperLink ID="LinkRegresar" runat="server" NavigateUrl="../../notas/profesor/miscursos.asp"
                        Style="font-size: 8pt; color: saddlebrown; font-family: verdana">«« Regresar</asp:HyperLink></td>
                <td align="right">
                    &nbsp;<asp:DropDownList ID="DDLAccion" runat="server" style="border-right: black 1px solid; border-top: black 1px solid; font-weight: normal; font-size: 8pt; border-left: black 1px solid; color: black; border-bottom: black 1px solid; font-family: verdana; background-color: #fffaf0">
                        <asp:ListItem Value="0">----- Accion a Realizar ----</asp:ListItem>
                    </asp:DropDownList>&nbsp;
                    <asp:Button ID="CmdImportar" runat="server" Text="Importar Sylabus" BackColor="FloralWhite" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Width="113px" />
                    <asp:Button ID="CmdActividades" runat="server" Text="Registrar Actividades" BackColor="FloralWhite" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Width="145px" UseSubmitBehavior="False" />
                    <asp:Button ID="CmdEvaluaciones" runat="server" Text="Registrar Evaluaciones" BackColor="FloralWhite" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Width="141px" UseSubmitBehavior="False" />
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../../../images/ayuda.gif" OnClientClick="AbrirPopUp('manualNotasMedicina.pdf'); return false;" ToolTip="Descargar Manual de Usuario" />&nbsp;
                    </td>
            </tr>
            <tr>
                <td colspan="3" style="font-weight: bold; font-size: 8pt; text-transform: capitalize; color: #003300; font-family: verdana; font-variant: normal; border-top: #660000 1px solid;">
                    &nbsp; Profesor :
                    <asp:Label ID="LblProfesor" runat="server" style="font-size: 7pt; text-transform: uppercase; color: dimgray; font-family: verdana"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3" style="font-weight: bold; font-size: 8pt; text-transform: capitalize; color: #003300; font-family: verdana; font-variant: normal">
                    &nbsp; Asignatura :
                    <asp:Label ID="LblAsignatura" runat="server" style="font-size: 7pt; text-transform: uppercase; color: dimgray; font-family: verdana"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3" style="font-weight: bold; font-size: 8pt; text-transform: capitalize; color: #003300; font-family: verdana; font-variant: normal; border-top: #660000 1px solid; height: 28px;">
                    &nbsp; Datos de Registro de Sylabus : &nbsp;<asp:Label ID="LblRegistro" runat="server" style="font-size: 7pt; text-transform: uppercase; font-family: verdana"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3" valign="top">
                    <table style="width: 566px" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="3" style="border-right: black 1px solid; border-top: black 1px solid; font-weight: bold; font-size: 8pt; border-left: black 1px solid; border-bottom: black 1px solid; font-family: verdana; height: 27px; background-color: floralwhite">
                                &nbsp;I. Duración&nbsp;
                                <asp:DropDownList ID="DDLDuracion" runat="server" Style="border-right: black 1px solid;
                                    border-top: black 1px solid; font-weight: normal; font-size: 8pt; border-left: black 1px solid;
                                    color: midnightblue; border-bottom: black 1px solid; font-family: verdana; background-color: #fffaf0">
                                </asp:DropDownList>
                                Semanas.</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="border-right: black 1px solid; border-top: black 1px solid; font-weight: bold; font-size: 8pt; border-left: black 1px solid; font-family: verdana; height: 20px; background-color: floralwhite">
                                &nbsp; II. Significatividad<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="TxtImportancia" Display="Dynamic" ErrorMessage="Ingrese informacion en Importancia"
                                    SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                            <td style="height: 20px">
                                &nbsp;&nbsp; 
                            </td>
                            <td style="border-right: black 1px solid; border-top: black 1px solid; font-weight: bold; font-size: 8pt; border-left: black 1px solid; font-family: verdana; height: 20px; background-color: floralwhite">
                                &nbsp; III. Competencias<asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="TxtRelevancia" Display="Dynamic" ErrorMessage="Ingrese informacion en competencias"
                                    SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TxtImportancia" runat="server" Height="83px" TextMode="MultiLine"
                                    Width="318px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" MaxLength="8000"></asp:TextBox></td>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtRelevancia" runat="server" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" Height="83px" MaxLength="8000"
                                    TextMode="MultiLine" Width="318px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="style1">
                                </td>
                            <td class="style1">
                            </td>
                            <td class="style1">
                                </td>
                        </tr>
                        <tr>
                            <td style="border-right: black 1px solid; border-top: black 1px solid; font-weight: bold; font-size: 8pt; border-left: black 1px solid; font-family: verdana; height: 20px; background-color: floralwhite">
                                &nbsp; IV. Objetivos Curriculares<asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server" ControlToValidate="TxtAplicabilidad" Display="Dynamic" ErrorMessage="Ingrese informacion en objetivos curriculares"
                                    SetFocusOnError="True" Enabled="False">*</asp:RequiredFieldValidator></td>
                            <td style="height: 20px">
                            </td>
                            <td style="border-right: black 1px solid; border-top: black 1px solid; font-weight: bold; font-size: 8pt; border-left: black 1px solid; font-family: verdana; height: 20px; background-color: floralwhite">
                                &nbsp; V. Contenidos Generales<asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator4" runat="server"
                                    ControlToValidate="TxtContenido" Display="Dynamic" ErrorMessage="Ingrese informacion en contenidos generales"
                                    SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TxtAplicabilidad" runat="server" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" Height="83px" MaxLength="8000"
                                    TextMode="MultiLine" Width="318px"></asp:TextBox></td>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtContenido" runat="server" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" Height="83px" MaxLength="8000"
                                    TextMode="MultiLine" Width="318px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="border-right: black 1px solid; border-top: black 1px solid; font-weight: bold; font-size: 8pt; border-left: black 1px solid; font-family: verdana; height: 20px; background-color: floralwhite">
                                &nbsp; VI.
                                Trabajos de Investigación<asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                    runat="server" ControlToValidate="TxtInvestigacion" Display="Dynamic" ErrorMessage="Ingrese informacion en Investigacion"
                                    SetFocusOnError="True" Enabled="False">*</asp:RequiredFieldValidator></td>
                            <td style="height: 20px">
                            </td>
                            <td style="border-right: black 1px solid; border-top: black 1px solid; font-weight: bold; font-size: 8pt; border-left: black 1px solid; font-family: verdana; height: 20px; background-color: floralwhite">
                                &nbsp; VII.
                                Metodología<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                    ControlToValidate="TxtMetodologia" Display="Dynamic" ErrorMessage="Ingrese informacion en metodología"
                                    SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TxtInvestigacion" runat="server" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" Height="83px" MaxLength="8000"
                                    TextMode="MultiLine" Width="318px"></asp:TextBox></td>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtMetodologia" runat="server" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" Height="83px" MaxLength="8000"
                                    TextMode="MultiLine" Width="318px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="border-right: black 1px solid; border-top: black 1px solid; font-weight: bold; font-size: 8pt; border-left: black 1px solid; font-family: verdana; height: 20px; background-color: floralwhite">
                                &nbsp; VIII.
                                Evaluación<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                    ControlToValidate="TxtEvaluacion" Display="Dynamic" ErrorMessage="Ingrese información en Evaluación"
                                    SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                            <td style="height: 20px">
                            </td>
                            <td style="border-right: black 1px solid; border-top: black 1px solid; font-weight: bold; font-size: 8pt; border-left: black 1px solid; font-family: verdana; height: 20px; background-color: floralwhite">
                                &nbsp; IX. Bibliografía<asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator8" runat="server"
                                    ControlToValidate="TxtBibliografia" Display="Dynamic" ErrorMessage="Ingrese informacion en Bibliografía"
                                    SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TxtEvaluacion" runat="server" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" Height="83px" MaxLength="8000"
                                    TextMode="MultiLine" Width="318px"></asp:TextBox></td>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtBibliografia" runat="server" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" Height="83px" MaxLength="8000"
                                    TextMode="MultiLine" Width="318px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td align="right">
                                <asp:Button ID="CmdGuardar" runat="server" Text="      Guardar" BackColor="Transparent" BorderColor="Black" BorderWidth="1px" Font-Bold="False" CssClass="guardar2" Height="27px" Width="71px" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
                    <asp:HiddenField ID="HidenCodigoSyl" runat="server" />
    </form>
</body>
</html>
