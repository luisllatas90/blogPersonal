<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AcreditacionUniversitaria_generales.aspx.vb" Inherits="Encuesta_AcreditacionUniversitaria_generales" %>

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
            width: 124px;
        }
        .style2
        {
        }
        .style3
        {
            width: 64px;
        }
        .style5
        {
            width: 245px;
        }
        .style6
        {
            width: 220px;
        }
        .style7
        {
            width: 87px;
        }
    </style>
</head>
<body style="background-color: #E6EEEE">
    <form id="form1" runat="server">
    <div>
        <%  response.write(clsfunciones.cargacalendario) %>
        <table style="width:100%;">
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
                    <table cellpadding="2" 
                        cellspacing="3" class="ContornoTabla1; fondoblanco" width="100%">
                        <tr>
                            <td colspan="7">
                            <strong>I. CARACTERÍSTICAS GENERALES </strong></td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                1. Apellidos y Nombres:
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="TxtApellidoPat" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="TxtApellidoPat" 
                                    ErrorMessage="Apellido paterno es obligatorio" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:TextBox ID="TxtApellidoMat" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="TxtApellidoMat" 
                                    ErrorMessage="Apellido materno es obligatorio" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td class="style5">
                                <asp:TextBox ID="TxtNombres" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="TxtNombres" ErrorMessage="Nombre es obligatorio" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                Fecha de nacimiento:
                                <asp:TextBox ID="TxtFechaNac" runat="server" Width="80px"></asp:TextBox>
                              <input id="Button2" type="button"  class="cunia" 
                                    onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFechaNac,'dd/mm/yyyy')" 
                                    style="height: 22px" /><asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtFechaNac" 
                                    ErrorMessage="Fecha de nacimiento es obligatorio">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                            <td colspan="2">
&nbsp;Apellido paterno&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                Apellido materno</td>
                            <td class="style5">
                                Nombres</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                2. Lugar de nacimiento:</td>
                            <td colspan="2">
                                <asp:DropDownList ID="CboPais" runat="server" Width="258px" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" 
                                    ControlToValidate="CboPais" 
                                    ErrorMessage="Seleccione país donde nació" ValidationGroup="Guardar" 
                                    Enabled="False">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:DropDownList ID="CboDepartamento" runat="server" Width="216px" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RfvDepNac" runat="server" 
                                    ControlToValidate="CboDepartamento" 
                                    ErrorMessage="Seleccione departamento donde nació" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CvDepNac" runat="server" 
                                    ControlToValidate="CboDepartamento" 
                                    ErrorMessage="Debe elegir el Departamento donde nació" Operator="GreaterThan" 
                                    ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="CboProvincia" runat="server" Width="205px" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RfvProvNac" runat="server" 
                                    ControlToValidate="CboProvincia" 
                                    ErrorMessage="Seleccione provincia donde nació" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CvProvNac" runat="server" 
                                    ControlToValidate="CboProvincia" 
                                    ErrorMessage="Debe elegir la Provincia donde nació" Operator="GreaterThan" 
                                    ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="CboDistrito" runat="server" Width="229px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RfvDistNac" runat="server" 
                                    ControlToValidate="CboDistrito" ErrorMessage="Seleccione distrito donde nació" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CvDistNac" runat="server" 
                                    ControlToValidate="CboDistrito" 
                                    ErrorMessage="Debe elegir el distrito donde nació" Operator="GreaterThan" 
                                    ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                            <td colspan="2">
                                País </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                Departamento</td>
                            <td class="style5">
                                Provincia</td>
                            <td>
                                Distrito</td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                3. Institución Educativa donde terminó estudios secundarios:<asp:TextBox 
                                    ID="TxtInsEducativa" runat="server" Width="283px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                    ControlToValidate="TxtInsEducativa" 
                                    ErrorMessage="Institución educativa de secundaria es obligatorio" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
&nbsp;
                            </td>
                        </tr>
                                        <tr>
                            <td class="style2">
                                Ciudad donde término</td>
                            <td colspan="2">
                                <asp:DropDownList ID="CboDepartamentoEst" runat="server" Width="216px" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RfvDepEst" runat="server" 
                                    ControlToValidate="CboDepartamentoEst" 
                                    ErrorMessage="Seleccione departamento donde terminó estudios secundarios" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CvDepEst" runat="server" 
                                    ControlToValidate="CboDepartamentoEst" 
                                    ErrorMessage="Debe elegir el Departamento donde estudió" Operator="GreaterThan" 
                                    ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:DropDownList ID="CboProvinciaEst" runat="server" Width="205px" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RfvProvEst" runat="server" 
                                    ControlToValidate="CboProvinciaEst" 
                                    ErrorMessage="Seleccione provincia donde terminó estudios secundarios" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CvProvEst" runat="server" 
                                    ControlToValidate="CboProvinciaEst" 
                                    ErrorMessage="Debe elegir la provincia donde estució" Operator="GreaterThan" 
                                    ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="CboDistritoEst" runat="server" Width="229px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RfvDistEst" runat="server" 
                                    ControlToValidate="CboDistritoEst" 
                                    ErrorMessage="Seleccione distrito donde terminó estudios secundarios" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CvDistEst" runat="server" 
                                    ControlToValidate="CboDistritoEst" 
                                    ErrorMessage="Debe elegir el Distrito donde estudió" Operator="GreaterThan" 
                                    ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td>
                                &nbsp;</td>
                                        </tr>
                                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                            <td colspan="2">
                                Departamento</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                Provincia</td>
                            <td class="style5">
                                Distrito</td>
                            <td>
                                &nbsp;</td>
                                        </tr>
                        <tr>
                            <td colspan="5">
                                4. Tipo de Institución Educativa:
                                <asp:DropDownList ID="CboTipoColegio" runat="server" Width="185px">
                                    <asp:ListItem Value="1">Privado</asp:ListItem>
                                    <asp:ListItem Value="2">Estatal</asp:ListItem>
                                    <asp:ListItem Value="3">Estatal militar</asp:ListItem>
                                    <asp:ListItem Value="4">Centro no escolarizado</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator4" runat="server" 
                                    ControlToValidate="CboTipoColegio" 
                                    ErrorMessage="Tipo de Institución Educativa es obligatorio" 
                                    Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td class="style5">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                5. Semestre Acad. de Ingreso a la USAT:
                                <asp:DropDownList ID="CboCicloAcad" runat="server" Width="90px">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator5" runat="server" 
                                    ControlToValidate="CboCicloAcad" ErrorMessage="Ciclo de ingreso es obligatorio" 
                                    Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
&nbsp;--&gt; Especifique la Escuela Prof. a la que ingresó
                                <asp:DropDownList ID="CboEscuela" runat="server" Width="300px">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator6" runat="server" 
                                    ControlToValidate="CboEscuela" 
                                    ErrorMessage="Escuela a la que ingreso es obligatorio" Operator="GreaterThan" 
                                    ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                6. Escuela Profesional en la cual se está matriculando actualmente:
                                <asp:DropDownList ID="CboEscuelaActual" runat="server" Width="351px">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator7" runat="server" 
                                    ControlToValidate="CboEscuelaActual" 
                                    ErrorMessage="Escuela a la que ingreso es obligatorio" Operator="GreaterThan" 
                                    ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                7. Modalidad de ingreso a la USAT
                                <asp:DropDownList ID="CboIngreso" runat="server" Width="244px" 
                                    AutoPostBack="True">
                                    <asp:ListItem Value="1">Examen de admisión</asp:ListItem>
                                    <asp:ListItem Value="2">Escuela Pre Universitaria</asp:ListItem>
                                    <asp:ListItem Value="3">1er y 2do puesto de secundaria</asp:ListItem>
                                    <asp:ListItem Value="4">Selección preferente (PP&gt;14)</asp:ListItem>
                                    <asp:ListItem Value="5">Traslado externo</asp:ListItem>
                                    <asp:ListItem Value="6">Traslado interno</asp:ListItem>
                                    <asp:ListItem Value="7">Deportista calificado</asp:ListItem>
                                    <asp:ListItem Value="8">Bachiller internacional</asp:ListItem>
                                    <asp:ListItem Value="9">2da profesión</asp:ListItem>
                                    <asp:ListItem Value="10">Héroes del Cenepa</asp:ListItem>
                                    <asp:ListItem Value="11">Víctimas del terrorismo</asp:ListItem>
                                    <asp:ListItem Value="12">Internacional</asp:ListItem>
                                    <asp:ListItem Value="13">Ejercito peruano</asp:ListItem>
                                    <asp:ListItem Value="14">FAP</asp:ListItem>
                                    <asp:ListItem Value="15">Policía Nacional</asp:ListItem>
                                    <asp:ListItem Value="16">Escuela Militar de Chorrillos</asp:ListItem>
                                    <asp:ListItem Value="17">Escuela de enfermeras de la policía</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator8" runat="server" 
                                    ControlToValidate="CboIngreso" 
                                    ErrorMessage="Modalidad de ingreso es obligatorio" Operator="GreaterThan" 
                                    ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
&nbsp;<asp:Label ID="LblEspecifique" runat="server" Text="Especifique Univ. de Origen:" Visible="False"></asp:Label>
                                <asp:TextBox ID="TxtOrigen" runat="server" Visible="False"></asp:TextBox>
                                &nbsp;
                                <asp:Label ID="LblPorConvenio" runat="server" Text="Por convenio(*)" 
                                    Visible="False"></asp:Label>
                                <asp:TextBox ID="TxtPorConvenio" runat="server" Width="61px" Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                8. Modalidad de estudio                                 
                                <asp:DropDownList ID="CboModalidadEst" runat="server" Width="182px">
                                    <asp:ListItem Value="1">Presencial</asp:ListItem>
                                    <asp:ListItem Value="2">Semi-presencial</asp:ListItem>
                                    <asp:ListItem Value="3">A distancia</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator9" runat="server" 
                                    ControlToValidate="CboModalidadEst" 
                                    ErrorMessage="Modalidad de ingreso es obligatorio" Operator="GreaterThan" 
                                    ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td class="style5">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                9. Edad
                                <asp:TextBox ID="TxtEdad" runat="server" Width="41px" MaxLength="2">0</asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                    ControlToValidate="TxtEdad" ErrorMessage="Edad es obligatorio" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
&nbsp;años&nbsp; <b>
                                <asp:Label ID="LblDni" runat="server" Text="DNI"></asp:Label>
&nbsp;</b><asp:TextBox ID="TxtDni" runat="server" Width="97px" MaxLength="8"></asp:TextBox>
                                                    </td>
                            <td colspan="3">
                                &nbsp;</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                10. Sexo
                                <asp:RadioButtonList ID="RbtSexo" runat="server" RepeatDirection="Horizontal" 
                                    Width="179px">
                                    <asp:ListItem Value="M">Varón</asp:ListItem>
                                    <asp:ListItem Value="F">Mujer</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td valign="bottom">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                                ControlToValidate="RbtSexo" 
                                    ErrorMessage="Sexo es requerido" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td colspan="3">
                                &nbsp;</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="5" valign="top">
                                11. Preparación para el ingreso a la Universidad</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <table style="width:100%;">
                                    <tr>
                                        <td class="style6" valign="top">
                                            <asp:CheckBox ID="ChkEnCasa" runat="server" Text="Solo en casa" 
                                                ValidationGroup="Preparacion" />
                                            <br />
                                            <asp:CheckBox ID="ChkAcademias" runat="server" Text="Academias" 
                                                ValidationGroup="Preparacion" />
                                            <br />
                                            <asp:CheckBox ID="ChkCentroPre" runat="server" Text="Centro Pre Universitario" 
                                                ValidationGroup="Preparacion" />
                                        </td>
                                        <td>
                                            <br />
                                            <asp:Label ID="LblAcademia" runat="server" Text="Especifique en que academia"></asp:Label>
                                            &nbsp;                                 
                                            <asp:TextBox ID="TxtAcademia" runat="server"></asp:TextBox>
                                <br />
                                            <asp:Label ID="LblCentroPre" runat="server" 
                                                Text="Especifique en que Centro Pre"></asp:Label>
&nbsp;<asp:TextBox ID="TxtCentroPre" runat="server"></asp:TextBox>
&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                12. Nivel de dominio de otros idiomas</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <table border="1" cellpadding="0" cellspacing="0">
                                    <tr valign="top">
                                        <td rowspan="2">
                                            Idioma</td>
                                        <td colspan="3">
                                            1. Nivel de dominio</td>
                                        <td rowspan="2">
                                            2. Institución donde
                                            <br />
                                            estudio el idioma</td>
                                        <td rowspan="2">
                                            3. Tiene alguna certificación
                                            <br />
                                            internacional
                                        </td>
                                        <td rowspan="2">
                                            4. Especifique la
                                            <br />
                                            certificación que posee</td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            Habla</td>
                                        <td>
                                            Lee</td>
                                        <td>
                                            Escribe</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            1. Ingles</td>
                                        <td>
                                            <asp:DropDownList ID="CboHablaIng" runat="server">
                                                <asp:ListItem Value="4">Elegir</asp:ListItem>
                                                <asp:ListItem Value="0">Nulo</asp:ListItem>
                                                <asp:ListItem Value="1">Bajo</asp:ListItem>
                                                <asp:ListItem Value="2">Medio</asp:ListItem>
                                                <asp:ListItem Value="4">Alto</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CboLeeIng" runat="server">
                                                <asp:ListItem Value="4">Elegir</asp:ListItem>
                                                <asp:ListItem Value="0">Nulo</asp:ListItem>
                                                <asp:ListItem Value="1">Bajo</asp:ListItem>
                                                <asp:ListItem Value="2">Medio</asp:ListItem>
                                                <asp:ListItem Value="4">Alto</asp:ListItem>                                            
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CboEscribeIng" runat="server">
                                                                                            <asp:ListItem Value="4">Elegir</asp:ListItem>
                                                                                            <asp:ListItem Value="0">Nulo</asp:ListItem>
                                                <asp:ListItem Value="1">Bajo</asp:ListItem>
                                                <asp:ListItem Value="2">Medio</asp:ListItem>
                                                <asp:ListItem Value="4">Alto</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtInstitucionIng" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                <asp:RadioButtonList ID="RbtCertificacionIng" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtCertificacionIng" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2. Frances</td>
                                        <td>
                                            <asp:DropDownList ID="CboHablaFra" runat="server">
                                                <asp:ListItem Value="4">Elegir</asp:ListItem>
                                                <asp:ListItem Value="0">Nulo</asp:ListItem>
                                                <asp:ListItem Value="1">Bajo</asp:ListItem>
                                                <asp:ListItem Value="2">Medio</asp:ListItem>
                                                <asp:ListItem Value="4">Alto</asp:ListItem>                                            
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CboLeeFra" runat="server">
                                                <asp:ListItem Value="4">Elegir</asp:ListItem>
                                                <asp:ListItem Value="0">Nulo</asp:ListItem>
                                                <asp:ListItem Value="1">Bajo</asp:ListItem>
                                                <asp:ListItem Value="2">Medio</asp:ListItem>
                                                <asp:ListItem Value="4">Alto</asp:ListItem>                                            
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CboEscribeFra" runat="server">
                                                <asp:ListItem Value="4">Elegir</asp:ListItem>
                                                <asp:ListItem Value="0">Nulo</asp:ListItem>
                                                <asp:ListItem Value="1">Bajo</asp:ListItem>
                                                <asp:ListItem Value="2">Medio</asp:ListItem>
                                                <asp:ListItem Value="4">Alto</asp:ListItem>                                            
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtInstitucionFra" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                <asp:RadioButtonList ID="RbtCertificacionFra" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtCertificacionFra" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            3. Italiano</td>
                                        <td>
                                            <asp:DropDownList ID="CboHablaIta" runat="server">
                                                <asp:ListItem Value="4">Elegir</asp:ListItem>
                                                <asp:ListItem Value="0">Nulo</asp:ListItem>
                                                <asp:ListItem Value="1">Bajo</asp:ListItem>
                                                <asp:ListItem Value="2">Medio</asp:ListItem>
                                                <asp:ListItem Value="4">Alto</asp:ListItem>                                            
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CboLeeIta" runat="server">
                                                <asp:ListItem Value="4">Elegir</asp:ListItem>
                                                <asp:ListItem Value="0">Nulo</asp:ListItem>
                                                <asp:ListItem Value="1">Bajo</asp:ListItem>
                                                <asp:ListItem Value="2">Medio</asp:ListItem>
                                                <asp:ListItem Value="4">Alto</asp:ListItem>                                            
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CboEscribeIta" runat="server">
                                                <asp:ListItem Value="4">Elegir</asp:ListItem>
                                                <asp:ListItem Value="0">Nulo</asp:ListItem>
                                                <asp:ListItem Value="1">Bajo</asp:ListItem>
                                                <asp:ListItem Value="2">Medio</asp:ListItem>
                                                <asp:ListItem Value="4">Alto</asp:ListItem>                                            
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtInstitucionIta" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                <asp:RadioButtonList ID="RbtCertificacionIta" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtCertificacionIta" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            4. Otro:<asp:TextBox ID="TxtOtroIdioma" runat="server" Width="80px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CboHablaOtr" runat="server">
                                                <asp:ListItem Value="4">Elegir</asp:ListItem>
                                                <asp:ListItem Value="0">Nulo</asp:ListItem>
                                                <asp:ListItem Value="1">Bajo</asp:ListItem>
                                                <asp:ListItem Value="2">Medio</asp:ListItem>
                                                <asp:ListItem Value="4">Alto</asp:ListItem>                                            
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CboLeeOtr" runat="server">
                                                <asp:ListItem Value="0">Nulo</asp:ListItem>
                                                <asp:ListItem Value="1">Bajo</asp:ListItem>
                                                <asp:ListItem Value="2">Medio</asp:ListItem>
                                                <asp:ListItem Value="4">Alto</asp:ListItem>                                            
                                                <asp:ListItem Value="4">Elegir</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CboEscribeOtr" runat="server">
                                                <asp:ListItem Value="4">Elegir</asp:ListItem>
                                                <asp:ListItem Value="0">Nulo</asp:ListItem>
                                                <asp:ListItem Value="1">Bajo</asp:ListItem>
                                                <asp:ListItem Value="2">Medio</asp:ListItem>
                                                <asp:ListItem Value="4">Alto</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtInstitucionOtr" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                <asp:RadioButtonList ID="RbtCertificacionOtr" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtCertificacionOtr" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                13. ¿Presenta alguna discapacidad? </td>
                            <td class="style5">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table style="width:100%;">
                                    <tr>
                            <td>
                                <asp:RadioButtonList ID="RbtDiscapacidad" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                                ControlToValidate="RbtDiscapacidad" 
                                    ErrorMessage="Discapacidad es requerido" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td valign="top" id="TdDiscapacidad">
                                &nbsp;
                                <asp:Label ID="LblDiscapacidad" runat="server" Text="Especifique:"></asp:Label>
                                <asp:TextBox ID="TxtDiscapacidad" runat="server" Width="396px"></asp:TextBox>
                            </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                14. ¿Cuenta con algún seguro estudiantil?</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table style="width:100%;">
                                    <tr>
                            <td valign="top">
                                <asp:RadioButtonList ID="RbtSeguro" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                                ControlToValidate="RbtSeguro" 
                                    ErrorMessage="Seguro estudiantil es requerido" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td valign="top">
                                &nbsp;<asp:Label ID="LblSeguro" runat="server" Text="Especifique:"></asp:Label>
                                <asp:TextBox ID="TxtSeguro" runat="server" Width="393px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                15. ¿Profesa Ud. alguna religión?</td>
                            <td colspan="3">
                                &nbsp;</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table style="width:100%;">
                                    <tr>
                                        <td class="style7">
                                            <asp:RadioButtonList ID="RbtReligion" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                                ControlToValidate="RbtReligion" 
                                    ErrorMessage="Religion es requerido" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td valign="top">
                                            <asp:DropDownList ID="CboReligion" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="1">Católica</asp:ListItem>
                                                <asp:ListItem Value="2">Otra</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:Label ID="LblReligion" runat="server" Text="Especifique:"></asp:Label>
                                <asp:TextBox ID="TxtReligion" runat="server" Width="393px"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                16. <font style="size:auto"><b>Sólo para los católicos.</b></font> ¿Señale con un check cuáles de los siguientes 
                                sacramentos ha recibido Ud.?</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:CheckBoxList ID="ChkSacramentos" runat="server" Width="306px">
                                    <asp:ListItem Value="1">Bautizo</asp:ListItem>
                                    <asp:ListItem Value="2">Reconciliación</asp:ListItem>
                                    <asp:ListItem Value="2">Eucaristía y Primera Comunión</asp:ListItem>
                                    <asp:ListItem Value="4">Confirmación</asp:ListItem>
                                    <asp:ListItem Value="5">Unción de los enfermos</asp:ListItem>
                                    <asp:ListItem Value="6">Matrimonio por la Iglesia</asp:ListItem>
                                    <asp:ListItem Value="7">Orden Sacerdotal</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td colspan="3">
                                &nbsp;</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                17. Señale con un check los sacramentos que frecuenta</td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <table border="1" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="style1">
                                            Sacramento</td>
                                        <td>
                                            Frecuencia</td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            Reconciliación</td>
                                        <td>
                                            <asp:RadioButtonList ID="RbtReconciliacion" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1">Siempre</asp:ListItem>
                                                <asp:ListItem Value="2">Algunas veces</asp:ListItem>
                                                <asp:ListItem Value="3">Rara vez</asp:ListItem>
                                                <asp:ListItem Value="4">Nunca</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                                ControlToValidate="RbtReconciliacion" 
                                    ErrorMessage="Indicar frecuencia de reconciliación" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            Eucaristía</td>
                                        <td>
                                            <asp:RadioButtonList ID="RbtEucaristia" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1">Siempre</asp:ListItem>
                                                <asp:ListItem Value="2">Algunas veces</asp:ListItem>
                                                <asp:ListItem Value="3">Rara vez</asp:ListItem>
                                                <asp:ListItem Value="4">Nunca</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                                ControlToValidate="RbtEucaristia" 
                                    ErrorMessage="Indicar frecuencia de eucaristía" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                18. ¿Participa en algún grupo parroquial o movimiento de la iglesia?</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table style="width:100%;">
                                    <tr>
                            <td>
                                <asp:RadioButtonList ID="RbtIglesia" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                                                ControlToValidate="RbtIglesia" 
                                    ErrorMessage="Indicar si pertence a un grupo parroquial" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td valign="top">
                                &nbsp;<asp:Label ID="LblIglesia" runat="server" Text="Especifique cuál:"></asp:Label>
                                <asp:TextBox ID="TxtIglesia" runat="server" Width="393px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                19. ¿Cuál es su estado civil?
                                <asp:DropDownList ID="CboEstadoCivil" runat="server" Width="110px">
                                    <asp:ListItem Value="1">Soltero</asp:ListItem>
                                    <asp:ListItem Value="2">Casado</asp:ListItem>
                                    <asp:ListItem Value="3">Conviviente</asp:ListItem>
                                    <asp:ListItem Value="4">Separado</asp:ListItem>
                                    <asp:ListItem Value="5">Divorciado</asp:ListItem>
                                    <asp:ListItem Value="6">Viudo</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style5">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                20. ¿Tiene hijos?</td>
                            <td colspan="3">
                                &nbsp;</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table style="width:100%;">
                                    <tr>
                            <td class="style3">
                                <asp:RadioButtonList ID="RbtHijos" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                                                ControlToValidate="RbtHijos" 
                                    ErrorMessage="Indicar si tiene hijos" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td valign="top">
                                &nbsp;<asp:Label ID="LblHijos" runat="server" Text="Cuantos:"></asp:Label>
                                <asp:TextBox ID="TxtNroHijos" runat="server" Width="49px" MaxLength="2">0</asp:TextBox>
                                <br />
                                <br />
                                <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <table border="1" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            Nro</td>
                                        <td>
                                            Nombres y Apellidos</td>
                                        <td>
                                            Sexo</td>
                                        <td>
                                            Fecha de nacimiento</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblNroHijo1" runat="server" Text="1.-"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtNombreHijo1" runat="server" Width="261px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="RbtSexoHijo1" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem>F</asp:ListItem>
                                                <asp:ListItem>M</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFechaHijo1" runat="server" Width="92px"></asp:TextBox>
                              <input id="Button3" type="button"  class="cunia" 
                                    onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFechaHijo1,'dd/mm/yyyy')" 
                                    style="height: 22px" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblNroHijo2" runat="server" Text="2.-"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtNombreHijo2" runat="server" Width="261px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="RbtSexoHijo2" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem>F</asp:ListItem>
                                                <asp:ListItem>M</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFechaHijo2" runat="server" Width="92px"></asp:TextBox>
                              <input id="Button4" type="button"  class="cunia" 
                                    onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFechaHijo2,'dd/mm/yyyy')" 
                                    style="height: 22px" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblNroHijo3" runat="server" Text="3.-"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtNombreHijo3" runat="server" Width="261px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="RbtSexoHijo3" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem>F</asp:ListItem>
                                                <asp:ListItem>M</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFechaHijo3" runat="server" Width="92px"></asp:TextBox>
                              <input id="Button5" type="button"  class="cunia" 
                                    onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFechaHijo3,'dd/mm/yyyy')" 
                                    style="height: 22px" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblNroHijo4" runat="server" Text="4.-"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtNombreHijo4" runat="server" Width="261px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="RbtSexoHijo4" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem>F</asp:ListItem>
                                                <asp:ListItem>M</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFechaHijo4" runat="server" Width="92px"></asp:TextBox>
                              <input id="Button6" type="button"  class="cunia" 
                                    onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFechaHijo4,'dd/mm/yyyy')" 
                                    style="height: 22px" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblNroHijo5" runat="server" Text="5.-"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtNombreHijo5" runat="server" Width="261px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="RbtSexoHijo5" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem>F</asp:ListItem>
                                                <asp:ListItem>M</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFechaHijo5" runat="server" Width="92px"></asp:TextBox>
                              <input id="Button7" type="button"  class="cunia" 
                                    onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFechaHijo5,'dd/mm/yyyy')" 
                                    style="height: 22px" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        </table>
                            </td>
                            <td valign="top">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <a href='AcreditacionUniversitaria_generales.aspx?menu= <%=Request.querystring("menu")%>&amp;id=<%=Request.querystring("id")%>&amp;ctf=<%=Request.querystring("ctf")%>'>
                                            <img alt="Datos personales" border="0" 
                                                src="../images/acreditacionu/datospersonales_r.gif" /></a></td>
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
                                                src="../images/acreditacionu/bienestaruniversitario.gif" /> </a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Paso 1:
                                <asp:Button ID="CmdGuardar" runat="server" Text="Guardar -&gt; Siguiente" 
                                    Width="140px" CssClass="boton" ValidationGroup="Guardar" />
                            </td>
                            <td valign="top">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="right" width="100%">
                                &nbsp;</td>
            </tr>
        </table>
    
    </div>
                    <table style="width:100%; visibility:hidden" cellpadding="2" cellspacing="3">
                        <tr>
                            <td valign="top">
                                Especifique:        Especifique:                 &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="middle">
                                Nombre:
                                <asp:TextBox ID="TxtNombreHijo" runat="server"></asp:TextBox>
&nbsp; </td>
                            <td rowspan="3" valign="middle">
                                <asp:GridView ID="GvwHijos" runat="server" AutoGenerateColumns="False">
                                    <RowStyle Height="15px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Nro" />
                                        <asp:BoundField HeaderText="Nombre" />
                                        <asp:BoundField HeaderText="Sexo" />
                                        <asp:BoundField HeaderText="Fecha de nacimiento" />
                                    </Columns>
                                </asp:GridView>
                                            </td>
                        </tr>
                        <tr>
                            <td valign="middle">
                                Sexo:
                                <asp:RadioButton ID="RbtSiHi" runat="server" GroupName="Hijos" Text="Si" />
&nbsp;<asp:RadioButton ID="RbtNoHj" runat="server" GroupName="Hijos" Text="No" />
&nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="middle">
                                Fecha de nacimiento
                                <asp:TextBox ID="TxtFechaNacHijo" runat="server" Width="76px" Enabled="False"></asp:TextBox>
                              <input id="Button1" type="button"  class="cunia" onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFechaNacHijo,'dd/mm/yyyy')" style="height: 22px" />
                                <asp:Button ID="CmdAgregar" runat="server" Text="Agregar" />
                                            </td>
                        </tr>
                    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
    </form>
</body>
</html>
