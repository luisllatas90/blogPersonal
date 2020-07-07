<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EncuestaTemasDeCertificacion.aspx.vb" Inherits="Encuesta_Escuela_TemasDeCertificacion" %>

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
            width: 763px;
        }
        .style2
        {
            width: 762px;
        }
        .style3
        {
            width: 504px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    
        <table style="width:100%;">
            <tr>
                <td align="center" 
                    
                    style="border-width: 1px; font-size: medium; font-family: Arial, Helvetica, sans-serif; color: #000000; font-weight: bold; border-top-style: solid; border-bottom-style: solid; border-top-color: #C0C0C0; border-bottom-color: #C0C0C0;" 
                    bgcolor="#FFFF66">
                    ENCUESTA: CURSOS PARA CERTIFICACIÓN</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="justify" style="font-size: small">
                    <span ><font face="Calibri">Estimado estudiante por 
                    favor responda en orden de prioridad del 1 al 7 (siendo 1 la mayor 
                    prioridad)&nbsp;qué certificaciones le interesaría estudiar:</font></span></td>
            </tr>
            <tr>
                <td>
                    <table align="center" style="width: 95%;">
                        <tr>
                            <td class="style3">
                                <span ><font>Cisco (CCNA)</font></span></td>
                            <td>
                                <asp:DropDownList ID="cboCisco" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="0">-</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="cboCisco" 
                                    ErrorMessage="Eliga un valor del 1 al 7 para Cisco" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                <div>
                                    <span ><font>Oracle (Base de Datos)</font></span></div>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboOracle" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="0">-</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="cboOracle" 
                                    ErrorMessage="Eliga un valor del 1 al 7 para Oracle" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                <div>
                                    <span ><font>Java (Programación)</font></span></div>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboJava" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="0">-</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="cboJava" ErrorMessage="Eliga un valor del 1 al 7 para Java" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                <div>
                                    <span ><font>SIEMON (Cableado estructurado)</font></span></div>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboSiemon" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="0">-</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                    ControlToValidate="cboSiemon" 
                                    ErrorMessage="Eliga un valor del 1 al 7 para SIEMON" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                <div>
                                    <span ><font>LPI (Linux)</font></span></div>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboLpi" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="0">-</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                    ControlToValidate="cboLpi" ErrorMessage="Eliga un valor del 1 al 7 para LPI" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                <div>
                                    <span ><font>ITIL (Gestión de Servicios Tecnológicos)</font></span></div>
                            </td>
                            <td >
                                <asp:DropDownList ID="cboItil" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="0">-</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                    ControlToValidate="cboItil" ErrorMessage="Eliga un valor del 1 al 7 para ITIL" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                <span ><font>Microsoft (Programación)</font></span></td>
                            <td>
                                <asp:DropDownList ID="cboMicrosoft" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="0">-</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                    ControlToValidate="cboMicrosoft" 
                                    ErrorMessage="Eliga un valor del 1 al 7 para Microsoft" 
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
            <tr>
                <td>
                    La Dirección de Escuela de Ingeniería de Sistemas y Computación agradece por anticipado tu colaboración. 
                    Muchas Gracias..<p align="right" style="font-weight: 700">
                        Muchas Gracias.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </p>
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;<asp:Button ID="cmdGuardar" runat="server" CssClass="guardar" 
                        Text="  Guardar" ValidationGroup="Guardar" Height="20px" Width="80px" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#FFFF66" class="style1" 
                    style="border-width: 1px; color: #000000; border-top-style: solid; border-bottom-style: solid; border-top-color: #C0C0C0; border-bottom-color: #C0C0C0;">
                    Desarrollo de sistemas - USAT</td>
            </tr>
        </table>
    
        <asp:HiddenField ID="hddTotal" runat="server" Value="7" />
    
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
    
    </form>
</body>
</html>
