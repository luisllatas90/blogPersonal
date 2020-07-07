<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FormacionProfesional.aspx.vb" Inherits="Encuesta_FormacionProfesional" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    <script src="../../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
</head>
<body style="background-color: #E6EEEE">
    <form id="form1" runat="server">
        <table style="width:100%;" border="0" cellpadding="5" cellspacing="0">
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
                                            <table class="ContornoTabla1; fondoblanco" style="width: 100%">
                                                <tr>
                                                    <td style="font-weight: bold">
                                                        III. FORMACIÓN PROFESIONAL</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td>
                                                                    27.- ¿Está Ud. de acuerdo con las estrategias de enseñanza aprendizaje aplicadas 
                                                                    en la Universidad?</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:RadioButtonList ID="RbtEnsenanza" runat="server" 
                                                                        RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1">Si totalmente</asp:ListItem>
                                                                        <asp:ListItem Value="2">Si solo en parte</asp:ListItem>
                                                                        <asp:ListItem Value="3">No estoy de acuerdo</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                ControlToValidate="RbtEnsenanza" 
                                    ErrorMessage="Indicar si esta de acuerdo con las estrategias de enseñanza" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    28.- ¿Está Ud de acuerdo con las estrategias para desarrollar su capacidad de 
                                                                    investigación aplicadas en la Universidad?</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:RadioButtonList ID="RbtDesarrollo" runat="server" 
                                                                        RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1">Si totalmente</asp:ListItem>
                                                                        <asp:ListItem Value="2">Si solo en parte</asp:ListItem>
                                                                        <asp:ListItem Value="3">No estoy de acuerdo</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                ControlToValidate="RbtDesarrollo" 
                                    ErrorMessage="Indicar si esta de acuerdo con las estrategis de desarrollo" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    29.- ¿Los sílabos de todas las asignaturas son dados a conocer el primer día de 
                                                                    clase?</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButtonList ID="RbtSilabus" runat="server" 
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Si totalmente</asp:ListItem>
                                                            <asp:ListItem Value="2">Si la mayoria</asp:ListItem>
                                                            <asp:ListItem Value="3">Si solo algunos profesores</asp:ListItem>
                                                            <asp:ListItem Value="4">Ninguno los entregan</asp:ListItem>
                                                        </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                ControlToValidate="RbtSilabus" 
                                    ErrorMessage="Indicar si el sílabus se da a conocer el primer dia de clases" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        30.- ¿Está Ud. satisfecho con el sistema de evaluación del aprendizaje que se 
                                                        aplica en la Universidad?</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButtonList ID="RbtEvaluacion" runat="server" 
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Si totalmente</asp:ListItem>
                                                            <asp:ListItem Value="2">Si solo en parte</asp:ListItem>
                                                            <asp:ListItem Value="3">No estoy de acuerdo</asp:ListItem>
                                                        </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                ControlToValidate="RbtEvaluacion" 
                                    ErrorMessage="Indicar su satisfacción con el sistema de evaluación" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        31.- Durante el 2008 señale el o los beneficios recibido por parte de la 
                                                        Universidad?</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButtonList ID="RbtBeneficios" runat="server" 
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Beca</asp:ListItem>
                                                            <asp:ListItem Value="2">Movilidad académica</asp:ListItem>
                                                            <asp:ListItem Value="3">Bolsa de trabajo</asp:ListItem>
                                                            <asp:ListItem Value="4">Pasantía</asp:ListItem>
                                                            <asp:ListItem Value="5">Ninguno</asp:ListItem>
                                                        </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                                ControlToValidate="RbtBeneficios" 
                                    ErrorMessage="Indicar que beneficio a recibido de la Universidad" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        32.- ¿Está Ud. satisfecho con la ayuda recibida por parte de la Universidad?</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButtonList ID="RbtAyuda" runat="server" 
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Si totalmente</asp:ListItem>
                                                            <asp:ListItem Value="2">Si solo en parte</asp:ListItem>
                                                            <asp:ListItem Value="3">No estoy satisfecho</asp:ListItem>
                                                        </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                                ControlToValidate="RbtAyuda" 
                                    ErrorMessage="Indicar su satisfacción con la ayuda recibida de la Universidad" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        33.- ¿Está Ud. satisfecho con el sistema de tutoria recibida por parte de la 
                                                        Universidad?</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButtonList ID="RbtTutoria" runat="server" 
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Si totalmente</asp:ListItem>
                                                            <asp:ListItem Value="2">Si solo en parte</asp:ListItem>
                                                            <asp:ListItem Value="3">No estoy satisfecho</asp:ListItem>
                                                        </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                                ControlToValidate="RbtTutoria" 
                                    ErrorMessage="Indicar su satisfacción con las tutorias" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                            </td>
                                        <td valign="top" width="30px">
                                <table align="left" border="0" cellpadding="0" cellspacing="0">
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
                                                src="../images/acreditacionu/formacionprofesional_r.gif" /> </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <a href='BienestarUniversitario.aspx?menu= <%=Request.querystring("menu")%>&amp;id=<%=Request.querystring("id")%>&amp;ctf=<%=Request.querystring("ctf")%>'>
                                            <img alt="Idiomas y otros cursos" border="0" 
                                                src="../images/acreditacionu/bienestaruniversitario.gif" /></a></td>
                                    </tr>
                                </table>
                                            </td>
                                                    </tr>
                                                <tr>
                                                    <td align="right">
                                                        Paso 3:
                
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
