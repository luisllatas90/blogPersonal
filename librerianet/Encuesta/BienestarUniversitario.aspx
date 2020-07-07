<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BienestarUniversitario.aspx.vb" Inherits="Encuesta_BienestarUniversitario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    <script src="../../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <style type="text/css">

        .style3
        {
            width: 64px;
        }
        </style>
</head>
<body style="background-color: #E6EEEE">
    <form id="form1" runat="server">
         <%  response.write(clsfunciones.cargacalendario) %>
        <table border="0" width="100%">
            <tr>
                <td align="center" class="usatTitulousat">
                    Ficha de actualización de datos de ESTUDIANTES de Pre Grado<br />
                    Para Acreditación Universitaria</td>
            </tr>
            <tr>
                <td align="right">
                    <b>Semestre Académico: 2009-I</b></td>
            </tr>
            <tr>
                <td>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                            <table width="100%" class="ContornoTabla1; fondoblanco" style="width: 100%">
                                                <tr>
                                                    <td>
                                                     <strong>IV. BIENESTAR UNIVERSITARIO</strong></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td>
                                                                    34.- ¿Conoce Ud. si la Universidad tiene implementado programas de bienestar 
                                                                    universitario?</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                <table style="width:100%;">
                                    <tr>
                            <td class="style3">
                                <asp:RadioButtonList ID="RbtBienestar" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                ControlToValidate="RbtBienestar" 
                                    ErrorMessage="Indicar si conoce algún programa de bienestar universitario" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td valign="middle">
                                &nbsp;<asp:Label ID="LblBienestar" runat="server" Text="Cuales:"></asp:Label>
&nbsp;<asp:TextBox ID="TxtBienestar" runat="server" Width="514px"></asp:TextBox>
                                <br />
                                        <br />
                                        <br />
                                        <br />
                                        </td>
                                    </tr>
                                </table>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    35.- ¿Cómo valora Ud. su satisfacción con los siguientes programas implementados 
                                                                    por la Universidad?</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table style="width:100%;">
                                                                        <tr>
                                                                            <td>
                                                                                Programa</td>
                                                                            <td>
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                1. Atención médica primaria</td>
                                                                            <td>
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:RadioButtonList ID="RbtAtencion" runat="server" 
                                                                                    RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Value="0">No lo se o no aplica</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Muy Satisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Insatisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="3">Ni satisfecho ni insatisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="4">Satisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="5">Muy satisfecho</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                ControlToValidate="RbtAtencion" 
                                    ErrorMessage="Indicar su satisfacción con la atención médica" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                2. Psicología</td>
                                                                            <td>
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:RadioButtonList ID="RbtPsicologia" runat="server" 
                                                                                    RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Value="0">No lo se o no aplica</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Muy Satisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Insatisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="3">Ni satisfecho ni insatisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="4">Satisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="5">Muy satisfecho</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                ControlToValidate="RbtPsicologia" 
                                    ErrorMessage="Indicar su satisfacción con la psicología" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                3. Pedagogía</td>
                                                                            <td>
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:RadioButtonList ID="RbtPedagogia" runat="server" 
                                                                                    RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Value="0">No lo se o no aplica</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Muy Satisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Insatisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="3">Ni satisfecho ni insatisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="4">Satisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="5">Muy satisfecho</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                ControlToValidate="RbtPedagogia" 
                                    ErrorMessage="Indicar su satisfacción con la pedagogía" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                4. Asistencia social</td>
                                                                            <td>
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:RadioButtonList ID="RbtAsistencia" runat="server" 
                                                                                    RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Value="0">No lo se o no aplica</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Muy Satisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Insatisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="3">Ni satisfecho ni insatisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="4">Satisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="5">Muy satisfecho</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                                ControlToValidate="RbtAsistencia" 
                                    ErrorMessage="Indicar su satisfacción con la asistencia social" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                5. Deportes</td>
                                                                            <td>
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:RadioButtonList ID="RbtDeportes" runat="server" 
                                                                                    RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Value="0">No lo se o no aplica</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Muy Satisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Insatisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="3">Ni satisfecho ni insatisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="4">Satisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="5">Muy satisfecho</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                                ControlToValidate="RbtDeportes" 
                                    ErrorMessage="Indicar su satisfacción con los deportes" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                6. Actividades culturales y de esparcimiento</td>
                                                                            <td>
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:RadioButtonList ID="RbtActividades" runat="server" 
                                                                                    RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Value="0">No lo se o no aplica</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Muy Satisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Insatisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="3">Ni satisfecho ni insatisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="4">Satisfecho</asp:ListItem>
                                                                                    <asp:ListItem Value="5">Muy satisfecho</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                                ControlToValidate="RbtActividades" 
                                    ErrorMessage="Indicar su satisfacción con las actividades culturales" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    36.- ¿Cómo valora Ud. su satisfacción con los servicios de?</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="1" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    Servicio</td>
                                                                <td>
                                                                    Nivel de satisfacción</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    1. Biblioteca</td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="RbtBiblioteca" runat="server" 
                                                                        RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="0">No lo se o no aplica</asp:ListItem>
                                                                        <asp:ListItem Value="1">Muy Satisfecho</asp:ListItem>
                                                                        <asp:ListItem Value="2">Insatisfecho</asp:ListItem>
                                                                        <asp:ListItem Value="3">Ni satisfecho ni insatisfecho</asp:ListItem>
                                                                        <asp:ListItem Value="4">Satisfecho</asp:ListItem>
                                                                        <asp:ListItem Value="5">Muy satisfecho</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                                ControlToValidate="RbtBiblioteca" 
                                    ErrorMessage="Indicar su satisfacción con los servicios de biblioteca" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    2. Biblioteca virtual</td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="RbtBibliotecaVirtual" runat="server" 
                                                                        RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="0">No lo se o no aplica</asp:ListItem>
                                                                        <asp:ListItem Value="1">Muy Satisfecho</asp:ListItem>
                                                                        <asp:ListItem Value="2">Insatisfecho</asp:ListItem>
                                                                        <asp:ListItem Value="3">Ni satisfecho ni insatisfecho</asp:ListItem>
                                                                        <asp:ListItem Value="4">Satisfecho</asp:ListItem>
                                                                        <asp:ListItem Value="5">Muy satisfecho</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                                ControlToValidate="RbtBibliotecaVirtual" 
                                    ErrorMessage="Indicar su satisfacción con las bibliotecas virtuales" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                </table>
                                                    </td>
                                                    <td valign="top" width="30px">
                                <table align="left">
                                    <tr>
                                        <td>
                                            <a href='AcreditacionUniversitaria_generales.aspx?menu= <%=Request.querystring("menu")%>&amp;id=<%=Request.querystring("id")%>&amp;ctf=<%=Request.querystring("ctf")%>'>
                                            <img alt="Datos personales" border="0" 
                                                src="../images/acreditacionu/datospersonales.gif" /></a></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <a href='Investigacion.aspx?menu= <%=Request.querystring("menu")%>&amp;id=<%=Request.querystring("id")%>&amp;ctf=<%=Request.querystring("ctf")%>'>
                                            <img alt="Perfil profesional" border="0" 
                                                src="../images/acreditacionu/investigacion.gif" /> </a>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="left" >
                                            <a href='FormacionProfesional.aspx?menu= <%=Request.querystring("menu")%>&amp;id=<%=Request.querystring("id")%>&amp;ctf=<%=Request.querystring("ctf")%>'>
                                            <img alt="Títulos y grados académicos" border="0" 
                                                src="../images/acreditacionu/formacionprofesional.gif" /> </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <a href='BienestarUniversitario.aspx?menu= <%=Request.querystring("menu")%>&amp;id=<%=Request.querystring("id")%>&amp;ctf=<%=Request.querystring("ctf")%>'>
                                            <img alt="Idiomas y otros cursos" border="0" 
                                                src="../images/acreditacionu/bienestaruniversitario_r.gif" /> </a>
                                        </td>
                                    </tr>
                                </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        Paso 4:
                
                                <asp:Button ID="CmdGuardar" runat="server" Text="Guardar -&gt; Siguiente" 
                                    Width="140px" CssClass="boton" ValidationGroup="Guardar" />
                
                                                    </td>
                                                    <td valign="top">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                </td>
            </tr>
            <tr>
                <td align="right" width="100%">
                                &nbsp;</td>
            </tr>
        </table>
    
    <div>
    
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
    </form>
</body>
</html>
