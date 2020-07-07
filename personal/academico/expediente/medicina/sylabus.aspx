
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sylabus.aspx.vb" Inherits="medicina_sylabus" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
     <link rel="stylesheet" type="text/css" href="../../../../private/estilo.css"/>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
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
                if (confirm("¿Desea Activar el curso para el registro de Asistencias y Notas?")==true)
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
        .style2
        {
            height: 25px;
        }
        .style3
        {
            height: 18px;
        }
        .style4
        {
            height: 20px;
        }
        </style>
</head>
<body  style="margin:0,0,0,0">
    <form id="form1" runat="server">
    <div>
        <table style="height: 100%" width="100%">
            <tr>
                <td align="center" colspan="3" style="border-top: black 1px solid; font-weight: bold; font-size: 11pt; color: white; border-bottom: black 1px solid; font-family: verdana; height: 21px; background-color: firebrick; text-align: center">
                    Activación de Curso</td>
            </tr>
            <tr>
                <td class="style2">
                </td>
                <td class="style2">
                    <asp:HyperLink ID="LinkRegresar" runat="server" NavigateUrl="../../notas/profesor/miscursos2.asp"
                        Style="font-size: 8pt; color: saddlebrown; font-family: verdana">«« Regresar</asp:HyperLink></td>
                <td align="right" class="style2">
                    &nbsp;&nbsp;
                    <asp:Button ID="CmdActividades" runat="server" Text="Registrar Actividades" 
                        Width="145px" UseSubmitBehavior="False" CssClass="usatnuevo" />
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../../../images/ayuda.gif" OnClientClick="AbrirPopUp('ManualAsistenciayNotas.pdf'); return false;" ToolTip="Descargar Manual de Usuario" />&nbsp;
                    </td>
            </tr>
            <tr>
                <td colspan="3" 
                    style="font-weight: bold; font-size: 8pt; text-transform: capitalize; color: #003300; font-family: verdana; font-variant: normal; border-top: #660000 1px solid;" 
                    class="style3">
                    &nbsp; Profesor :
                    <asp:Label ID="LblProfesor" runat="server" style="font-size: 7pt; text-transform: uppercase; color: dimgray; font-family: verdana"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3" 
                    style="font-weight: bold; font-size: 8pt; text-transform: capitalize; color: #003300; font-family: verdana; font-variant: normal" 
                    class="style4">
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
                    <table  border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <table class="usatTablaInfo" cellpadding="5" cellspacing="4">
                                    <tr>
                                        <td width="80">
                                            Asignatura</td>
                                        <td width="400">
                                            <asp:Label ID="LblNombreCurso" runat="server" CssClass="usatCeldaMenuSubTitulo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Grupo Horario</td>
                                        <td>
                                            <asp:Label ID="LblGrupoHorario" runat="server" 
                                                CssClass="usatCeldaMenuSubTitulo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Fecha de Sylabus</td>
                                        <td>
                                            <asp:Label ID="LblFechaSylabus" runat="server" 
                                                CssClass="usatCeldaMenuSubTitulo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Docente</td>
                                        <td>
                                            <asp:Label ID="LblUsuarioRegistro" runat="server" 
                                                CssClass="usatCeldaMenuSubTitulo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Archivo Sylabus</td>
                                        <td>
                                            <asp:Label ID="LblArchivoSylabus" runat="server" 
                                                CssClass="usatCeldaMenuSubTitulo" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="CmdGuardar" runat="server" Text="      Activar Curso" 
                                    BackColor="Transparent" BorderColor="Black" BorderWidth="1px" Font-Bold="False" 
                                    CssClass="guardar2" Height="27px" Width="107px" /></td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" >
                    <span onclick="AbrirPopUp('ManualAsistenciayNotas.pdf')" style="font-size:medium;cursor:hand; color=#FF0F0F" > Nuevo Registro de Asistencia y Notas, <br />descargue el manual de usuario haciendo 
                    clic aquí. </span>   </td>
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
