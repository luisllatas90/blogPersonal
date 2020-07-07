<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EncuestaCC2.aspx.vb" Inherits="Encuesta_CursosComplementarios_EncuestaCC2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    function validarOtroHorario(source, arguments)
    {   total =  parseInt(document.form1.hddTotal.value)
        for(i=0;i<total;i++)
        { 
            if (eval("document.form1.rblrpta2_" + i + ".checked")== true)
            {   valor = eval("document.form1.rblrpta2_" + i + ".value")
            }
        }
          
        if (valor == 7)
            if(form1.txtOtroHorario.value.length > 3)       
                arguments.IsValid=true
            else
                arguments.IsValid=false
        else
            arguments.IsValid=true   
    }
    </script>
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
        <table style="width:100%;">
            <tr>
                <td align="center" 
                    
                    style="font-size: medium; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold;" 
                    bgcolor="#669999">
                    ENCUESTA: CURSOS COMPLEMENTARIOS</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="justify">
                    La Dirección de Cursos Complementarios, tiene a bien informar que de lunes a 
                    jueves se desarrollará un <font style="font-size:x-small; font-weight: bold; color: #0033CC">CURSO BÁSICO DE FONÉTICA</font> del idioma inglés, el cual 
                    estará a cargo del <font style="font-size:x-small; font-weight: bold; color: #0033CC">Prof. Ty Hadman</font>, docente norteamericano. Con el deseo de 
                    brindar un horario propicio a su necesidad y tiempo, solicitamos su valiosa 
                    colaboración para desarrollar la siguiente encuesta:</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="width:95%;" align="center">
                        <tr>
                            <td>
                                 <b>1.¿Estarías interesado en llevar el curso? 
                            </b> 
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                     ControlToValidate="rblRpta1" 
                                     ErrorMessage="La primera pregunta es obligatoria, debe elegir una opción" 
                                     ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButtonList ID="rblRpta1" runat="server" 
                                    RepeatDirection="Horizontal" AutoPostBack="True">
                                    <asp:ListItem Value="S">SI</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <b>2.¿En qué horario podrías estudiar?</b><asp:RequiredFieldValidator 
                                    ID="valHorario" runat="server" ControlToValidate="rblrpta2" 
                                    ErrorMessage="La segunda pregunta es obligatoria, debe elegir una opción" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="rblrpta2" runat="server" CellPadding="5" 
                                    CellSpacing="10" RepeatColumns="4" AutoPostBack="True">
                                    <asp:ListItem Value="1">07:00 a.m. - 08:00 a.m.</asp:ListItem>
                                    <asp:ListItem Value="2">08:00 a.m. - 09:00 a.m.</asp:ListItem>
                                    <asp:ListItem Value="3">12:00 m. - 01:00 p.m.</asp:ListItem>
                                    <asp:ListItem Value="4">01:00 p.m. - 02:00 p.m.</asp:ListItem>
                                    <asp:ListItem Value="5">02:00 p.m. - 03:00 p.m.</asp:ListItem>
                                    <asp:ListItem Value="6">03:00 p.m. - 04:00 p.m.</asp:ListItem>
                                    <asp:ListItem Value="7">Otro horario</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>&nbsp;&nbsp; Especifíque otro horario en que podría estudiar
                                <asp:TextBox ID="txtOtroHorario" runat="server" Enabled="False"></asp:TextBox>
                                <asp:Label ID="lblValidarOtro" runat="server" ForeColor="Red" Text="*" 
                                    Visible="False"></asp:Label>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                    ErrorMessage="Si eligió la opción otro horario deberá especifiarlo" 
                                    ClientValidationFunction="validarOtroHorario" 
                                    ControlToValidate="txtOtroHorario" ValidationGroup="Guardar" 
                                    Enabled="False" Visible="False">*</asp:CustomValidator>
                                </b></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    La Dirección de Cursos Complementarios, agradece por anticipado tu colaboración. 
                    Muchas Gracias..<p align="right" style="font-weight: 700">
                        Muchas Gracias.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </p>
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;<asp:Button ID="cmdGuardar" runat="server" CssClass="guardar" 
                        Text="  Guardar" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#669999" class="style1">
                    Desarrollo de sistemas - USAT</td>
            </tr>
        </table>
    
        <asp:HiddenField ID="hddTotal" runat="server" Value="7" />
    
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
    
    </form>
</body>
</html>
