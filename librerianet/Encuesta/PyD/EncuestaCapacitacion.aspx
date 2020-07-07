<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EncuestaCapacitacion.aspx.vb" Inherits="librerianet_Encuesta_PyD_EncuestaCapacitacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../../../private/funciones.js"></script>
    <script language="javascript" type="text/javascript">
        function validaTemasInteres(source, arguments) 
        {  
            sw = 0; 
            for (i = 0; i < 7; i++) {
                if (eval("document.form1.cblTemas_" + i + ".checked") == true) {
                    sw++;
                }
            }

            if (sw == 0)
                arguments.IsValid = false;
            else
                arguments.IsValid = true; 
        }

        function validaOtroLugar(source, arguments) {
            if (eval("document.form1.rblLugar_2.checked") == true) {
                if (document.form1.txtOtro.value.length > 3)
                    arguments.IsValid = true;
                else
                    arguments.IsValid = false;
            }
            else {
                arguments.IsValid = true;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table align="center" cellpadding="1" cellspacing="0" class="contornotabla" 
            width="95%">
            <tr>
                <td align="center" style="font-weight: 700; color: #FFFFFF;" bgcolor="#3366CC" 
                    height="20">
                    ENCUESTA</td>
            </tr>
            <tr>
                <td style="background-color: #E6F7F7">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="background-color: #E6F7F7">
                    Escuela:
                    <asp:TextBox ID="txtEscuela" runat="server" Width="367px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                        ControlToValidate="txtEscuela" 
                        ErrorMessage="Indique la escuela a la que pertenece" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
&nbsp;Edad:
                    <asp:TextBox ID="txtEdad" runat="server" Width="54px" MaxLength="2"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                        ControlToValidate="txtEdad" ErrorMessage="Indique su edad" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
&nbsp;Ciclo:
                    <asp:DropDownList ID="cboCiclo" runat="server">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToValidate="cboCiclo" ErrorMessage="Indique el ciclo al que pertenece" 
                        Operator="GreaterThan" ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td style="background-color: #E6F7F7">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="background-color: #E6F7F7">
                    <b>INDICACIONES: </b>Lea con detenimiento y responda a las interrogantes que se 
                    presentan a continuación. Solo las secciones de su interés</td>
            </tr>
            <tr>
                <td style="background-color: #E6F7F7; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #AEE4FF;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    1. ¿Te gustaría fortalecer tus competencias profesionales recibiendo 
                    capacitaciones, talleres, conferencias o seminarios?<asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator10" runat="server" 
                        ControlToValidate="rblCompetencias" 
                        ErrorMessage="La pregunta 1 es obligatoria" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rblCompetencias" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem>Si</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    2. ¿Qué temas serían de tu interés?<b><asp:CustomValidator 
                        ID="CustomValidator1" runat="server" 
                                    ErrorMessage="La pregunta 2 es obligatoria" 
                                    ClientValidationFunction="validaTemasInteres" 
                        ValidationGroup="Guardar">*</asp:CustomValidator>
                                </b></td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBoxList ID="cblTemas" runat="server" RepeatColumns="2" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">Desarrollo de capacidades gerenciales</asp:ListItem>
                        <asp:ListItem Value="2">Comunicación y cultura empresarial</asp:ListItem>
                        <asp:ListItem Value="3">Marketing   comercial</asp:ListItem>
                        <asp:ListItem Value="4">Relaciones públicas</asp:ListItem>
                        <asp:ListItem Value="5">Marketing político</asp:ListItem>
                        <asp:ListItem Value="6">Negociación y resolución de conflictos</asp:ListItem>
                        <asp:ListItem Value="7">Coaching ejecutivo</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    3. ¿Cuánto tiempo estarías dispuesto a invertir para tu capacitación?<asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator11" runat="server" ControlToValidate="rblTiempo" 
                        ErrorMessage="La pregunta 3 es obligatoria" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rblTiempo" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">2 horas</asp:ListItem>
                        <asp:ListItem Value="2">4 horas</asp:ListItem>
                        <asp:ListItem Value="3">6 horas</asp:ListItem>
                        <asp:ListItem Value="4">8 horas</asp:ListItem>
                        <asp:ListItem Value="5">Las que sean necesarias</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    4. ¿Qué esperarías recibir en una capacitación para que llene tus expectativas?<asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtExpectativas" 
                        ErrorMessage="La pregunta 4 es obligatoria" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtExpectativas" runat="server" TextMode="MultiLine" 
                        Width="95%" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    5. ¿Qué requisitos o habilidades debe tener el expositor para que capte tu 
                    atención?<asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                        runat="server" ControlToValidate="txtAtencion" 
                        ErrorMessage="La pregunta 5 es obligatoria" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtAtencion" runat="server" TextMode="MultiLine" Width="95%" 
                        MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    6. ¿En qué época del año es más conveniente para ti llevar una capacitación? Por 
                    favor indica los meses:<asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                        runat="server" ControlToValidate="txtEpocaAnio" 
                        ErrorMessage="La pregunta 6 es obligatoria" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtEpocaAnio" runat="server" TextMode="MultiLine" Width="95%" 
                        MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    7. ¿Cuánto estarías dispuesto a pagar por una capacitación de acuerdo a la 
                    cantidad de horas?</td>
            </tr>
            <tr>
                <td>
                    Charla-conferencia (2 horas).…..S/.
                    <asp:TextBox ID="txtCharla" runat="server" Width="69px" MaxLength="6"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtCharla" 
                        ErrorMessage="Indique cuanto está dispuesto a pagar por una charla (2 hrs)" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Taller (4 horas)……………………....…S/.                     
                    <asp:TextBox ID="txtTaller" runat="server" Width="69px" MaxLength="6"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="txtTaller" 
                        ErrorMessage="Indique cuanto está dispuesto a pagar por una charla (4 hrs)" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Seminario (8 horas)…………………..S/.                     
                    <asp:TextBox ID="txtSeminario" runat="server" Width="69px" MaxLength="6"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ControlToValidate="txtCurso" 
                        ErrorMessage="Indique cuanto está dispuesto a pagar por una charla (8 hrs)" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Curso (16 horas)……………………....S/.
                    <asp:TextBox ID="txtCurso" runat="server" Width="69px" MaxLength="6"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                        ControlToValidate="txtCharla" 
                        ErrorMessage="Indique cuanto está dispuesto a pagar por una charla (16 hrs)" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    8. ¿Dónde preferiría recibir la capacitación?<asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator12" runat="server" ControlToValidate="rblLugar" 
                        ErrorMessage="la pregunta 8 es obligatoria" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rblLugar" runat="server" AutoPostBack="True">
                        <asp:ListItem Value="1">Sala de conferencias de un hotel</asp:ListItem>
                        <asp:ListItem Value="2">Auditorio de una universidad</asp:ListItem>
                        <asp:ListItem Value="3">Otro lugar</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Label ID="Label1" runat="server" Text="Especifique:"></asp:Label>
&nbsp;<asp:TextBox ID="txtOtro" runat="server" MaxLength="100"></asp:TextBox>
                                <b>
                                <asp:CustomValidator ID="CustomValidator2" runat="server" 
                                    ErrorMessage="Si eligió la opción otro lugar deberá especifiarlo" 
                                    ClientValidationFunction="validaOtroLugar" 
                        ValidationGroup="Guardar">*</asp:CustomValidator>
                                </b>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#3366CC">
                    <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                        ValidationGroup="Guardar" />
                </td>
            </tr>
        </table>
    
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
    </form>
</body>
</html>
