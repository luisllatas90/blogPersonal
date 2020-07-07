<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PlanPiloto.aspx.vb" Inherits="Encuesta_CursosComplementarios_PlanPiloto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Encuesta de Cursos Complementarios elaborada por Desarrollo de Sistema</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td align="center" class="TituloTabla" 
                    style="font-size: medium; font-family: Arial, Helvetica, sans-serif;">
                    ENCUESTA: CURSOS COMPLEMENTARIOS</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <p >
                        La Dirección de Cursos Complementarios se encuentra desarrollando el <b>Plan Piloto</b> 
                        para aperturar el Centro de Idiomas de nuestra Universidad, el mismo que 
                        brindará un servicio de enseñanza de calidad y exigencia académica a los 
                        alumnos.  
                    </p>
                    <p>
                        Por este motivo necesitamos de tu tiempo y tu valiosa colaboración para desarrollar la 
                        siguiente encuesta:
                    </p>
                </td>
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
                                 <b>1.¿Que idioma te gustaría aprender? 
                            </b> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                               <asp:CheckBoxList ID="chkIdiomas" runat="server" RepeatDirection="Horizontal" 
                                    CellPadding="5" CellSpacing="5">
                                    <asp:ListItem>Alemán</asp:ListItem>
                                    <asp:ListItem>Francés</asp:ListItem>
                                    <asp:ListItem>Inglés</asp:ListItem>
                                    <asp:ListItem>Italiano</asp:ListItem>
                                    <asp:ListItem>Portugués</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <b>2.¿En qué horario podrías estudiar?</b></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;<b>De lunes a viernes</b></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="cboEnSemana" runat="server" CellPadding="5" 
                                    CellSpacing="10" RepeatColumns="3" AutoPostBack="True">
                                    <asp:ListItem Value="1">07:00 a.m. - 08:00 a.m.</asp:ListItem>
                                    <asp:ListItem Value="2">08:00 a.m. - 09:00 a.m.</asp:ListItem>
                                    <asp:ListItem Value="3">09:00 a.m. - 10:00 a.m.</asp:ListItem>
                                    <asp:ListItem Value="4">10:00 a.m. - 11:00 a.m.</asp:ListItem>
                                    <asp:ListItem Value="5">11:00 a.m. - 12:00 m.</asp:ListItem>
                                    <asp:ListItem Value="6">12:00 m. - 01:00 p.m.</asp:ListItem>
                                    <asp:ListItem Value="7">01:00 p.m. - 02:00 p.m.</asp:ListItem>
                                    <asp:ListItem Value="8">02:00 p.m. - 03:00 p.m.</asp:ListItem>
                                    <asp:ListItem Value="9">03:00 p.m. - 04:00 p.m.</asp:ListItem>
                                    <asp:ListItem Value="10">04:00 p.m. - 05:00 p.m.</asp:ListItem>
                                    <asp:ListItem Value="11">05:00 p.m. - 06:00 p.m.</asp:ListItem>
                                    <asp:ListItem Value="12">06:00 p.m. - 07:00 p.m.</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Los sábados</b></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="cboSabados" runat="server" CellPadding="5" 
                                    CellSpacing="10" RepeatColumns="3" AutoPostBack="True">
                                    <asp:ListItem Value="1">07:00 a.m. - 11:00 a.m.</asp:ListItem>
                                    <asp:ListItem Value="2">08:00 a.m. - 12:00 m.</asp:ListItem>
                                    <asp:ListItem Value="3">09:00 a.m. - 01:00 p.m.</asp:ListItem>
                                    <asp:ListItem Value="4">03:00 p.m. - 07:00 p.m.</asp:ListItem>
                                    <asp:ListItem Value="5">04:00 p.m. - 08:00 p.m.</asp:ListItem>
                                    <asp:ListItem Value="6">06:00 p.m. - 10:00 p.m.</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    La Dirección de Cursos Complementarios, agradece por anticipado tu colaboración.
                    <p align="right" style="font-weight: 700">
                        Muchas Gracias.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </p>
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;<asp:Button ID="cmdGuardar" runat="server" CssClass="guardar" 
                        Text="  Guardar" />
                </td>
            </tr>
            <tr>
                <td class="TituloTabla">
                    Desarrollo de sistemas - USAT</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
