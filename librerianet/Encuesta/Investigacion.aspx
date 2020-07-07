<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Investigacion.aspx.vb" Inherits="Encuesta_Investigacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    <script src="../../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 62px;
        }
        .style3
        {
            width: 64px;
        }
    </style>
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
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                <table class="ContornoTabla1; fondoblanco" width="100%">
                                    <tr>
                                        <td style="font-family: Verdana; font-size: small; font-weight: bold;">
                                            Los ingresantes en el semestre 2009-I terminar, los otros estudiantes continuar</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong> II.- INVESTIGACIÓN </strong></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        21.- ¿Ha participado Ud. en algún proyecto de investigación reconocido por la 
                                                        Dirección de Investigación de la Universidad?</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                <table style="width:100%;">
                                    <tr>
                            <td class="style1" >
                                <asp:RadioButtonList ID="RbtParticipo" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                ControlToValidate="RbtParticipo" 
                                    ErrorMessage="Indicar si participó en un trabajo de investigación" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td valign="middle" id="TDProyectos">
                                &nbsp;<asp:Label ID="LblProyecto" runat="server" Text="Cuantos:"></asp:Label>
                                <asp:TextBox ID="TxtNroProyectos" runat="server" Width="35px" MaxLength="3">0</asp:TextBox>
                                <br />
                                        <br />
                                        <asp:Label ID="LblPase23" runat="server" 
                                    Text="--&gt; Pase a la pregunta 23"></asp:Label>
                                <br />
                                <br />
                                        </td>
                                    </tr>
                                </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-family: Verdana; font-size: small; font-weight: bold;">
                                                        (Solo si ha realizado alguna investigación responder pregunta 22, caso contrario 
                                                        pase a la pregunta 23)</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        22.- ¿Cómo fue su participación en el último proyecto que participó?</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="2">
                                                            <tr>
                                                                <td style="text-decoration: underline" id="TDTitulo">
                                                                    Titulo del proyecto</td>
                                                                <td style="text-decoration: underline">
                                                                    1. Modo de participación (*)</td>
                                                                <td style="text-decoration: underline">
                                                                    2. Año que se realizó</td>
                                                                <td style="text-decoration: underline">
                                                                    3. Meses que duro</td>
                                                                <td style="text-decoration: underline">
                                                                    4. Quien o que institución financió el proyecto</td>
                                                                <td style="text-decoration: underline">
                                                                    5. Medio de verificación</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="TxtTituloPro" runat="server" Width="268px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="CboModoParticipacion" runat="server" Width="140px" 
                                                                        AutoPostBack="True">
                                                                        <asp:ListItem Value="1">Colaborador</asp:ListItem>
                                                                        <asp:ListItem Value="2">Coautor</asp:ListItem>
                                                                        <asp:ListItem Value="3">Autor</asp:ListItem>
                                                                        <asp:ListItem Value="4">Otro (especifique)</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:DropDownList ID="CboAnio" runat="server" Width="78px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:DropDownList ID="CboMes" runat="server" Width="55px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtFinancio" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="CboMedioVer" runat="server">
                                                                        <asp:ListItem Value="1">Publicado</asp:ListItem>
                                                                        <asp:ListItem Value="2">Informe inteno</asp:ListItem>
                                                                        <asp:ListItem Value="3">Medio electrónico</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtModoParticipacion" runat="server" Visible="False"></asp:TextBox>
                                                                    <asp:Label ID="LblModoParticipacion" runat="server" ForeColor="Red" Text="*" 
                                                                        Visible="False"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            23.- ¿Está Ud. satisfecho con el sistema de evaluación de la investigación por 
                                            parte de la universidad?</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="RbtEvaluacion" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1">Si totalmente</asp:ListItem>
                                                <asp:ListItem Value="2">Si solo en parte</asp:ListItem>
                                                <asp:ListItem Value="3">No estoy de acuerdo</asp:ListItem>
                                                <asp:ListItem Value="4">No conozco el sistema de evaluación</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                ControlToValidate="RbtEvaluacion" 
                                    ErrorMessage="Indicar su satisfacción respecto al sistema de evaluación" 
                                                ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            24.- ¿Ud. ha participado en algún evento de difusión de la investigación 
                                            desarrollada por algún miembro de la Universidad?</td>
                                    </tr>
                                    <tr>
                                        <td>
                                <table style="width:100%;">
                                    <tr>
                            <td class="style3">
                                <asp:RadioButtonList ID="RbtDifusion" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                ControlToValidate="RbtDifusion" 
                                    ErrorMessage="Indicar el evento de difusión en el que participó" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td valign="middle">
                                <asp:Label ID="LblDifusion" runat="server" Text="Especifique el Proyecto:"></asp:Label>
&nbsp;<asp:TextBox ID="TxtProyDifusion" runat="server" Width="244px"></asp:TextBox>
                                &nbsp;<asp:Label ID="LblAutordifusion" runat="server" Text="autor(s):"></asp:Label>
                                &nbsp;
                                <asp:TextBox ID="TxtAutorDifusion" runat="server" Width="225px"></asp:TextBox>
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
                                            25.- ¿Ud. ha participado en algún evento de discusión de la investigación 
                                            desarrollada por algún miembro de la Universidad?</td>
                                    </tr>
                                    <tr>
                                        <td>
                                <table style="width:100%;">
                                    <tr>
                            <td class="style3">
                                <asp:RadioButtonList ID="RbtDiscusion" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                ControlToValidate="RbtDiscusion" 
                                    ErrorMessage="Indicar el evento de discusión en el que participó" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td valign="middle">
                                <asp:Label ID="LblDiscusion" runat="server" Text="Especifique el Proyecto:"></asp:Label>
&nbsp;<asp:TextBox ID="TxtProyDiscusion" runat="server" Width="244px"></asp:TextBox>
                                &nbsp;<asp:Label ID="LblAutordiscusion" runat="server" Text="autor(s):"></asp:Label>
                                &nbsp;
                                <asp:TextBox ID="TxtAutorDiscusion" runat="server" Width="225px"></asp:TextBox>
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
                                            26.- ¿Conoce Ud. los procedimientos con los cuales adquiere sus derechos de 
                                            propiedad intelectual sobre lo creado como resultado de su investigaciíon?</td>
                                    </tr>
                                    <tr>
                                        <td>
                                <table style="width:100%;">
                                    <tr>
                            <td class="style1"  >
                                <asp:RadioButtonList ID="RbtPropIntelectual" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                                ControlToValidate="RbtPropIntelectual" 
                                    
                                    ErrorMessage="Indicar los procedimientos de propiedad intelectual que conoce" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td valign="middle">
                                <asp:Label ID="LblPropiedad" runat="server" Text="Especifique:"></asp:Label>
&nbsp;<asp:TextBox ID="TxtPropintelectual" runat="server" Width="429px"></asp:TextBox>
                                &nbsp;<br />
                                <br />
                                <br />
                                <br />
                                        </td>
                                    </tr>
                                </table>
                                            </td>
                                    </tr>
                                    </table>
    
                                        </td>
                                        <td valign="top">
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
                                                src="../images/acreditacionu/investigacion_r.gif" /> </a>
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
                                                src="../images/acreditacionu/bienestaruniversitario.gif" /></a></td>
                                    </tr>
                                </table>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                
                                Paso 2:
                
                                <asp:Button ID="CmdGuardar" runat="server" Text="Guardar -&gt; Siguiente" 
                                    Width="140px" CssClass="boton" ValidationGroup="Guardar" />
                
                                            </td>
                                            <td valign="top">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
    
            </td>
            <tr>
            <td align="right">
                
                                &nbsp;</td>
            </tr>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
    </form>
</body>
</html>
