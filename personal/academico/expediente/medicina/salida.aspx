
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="salida.aspx.vb" Inherits="medicina_salida" %>

<%@ Register Src="salida.ascx" TagName="salida" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link rel="stylesheet" type="text/css" href="../../../../private/estilo.css"/>
    <script src="../../../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript">
    
    function enviacombo(combo)
    {
    <% response.write("pag='salida.aspx?codigo_cac="& request.querystring("codigo_cac") &"&codigo_syl=" & request.querystring("codigo_syl") & "&codigo_cup=" & request.querystring("codigo_cup") & "&codigo_per=" & request.querystring("codigo_per")  & "&nombre_per=" & request.querystring("nombre_per") & "&nombre_cur=" & request.querystring("nombre_cur") & "'") %>
    if (combo.value != 0)
        {
        location.href=pag + "&codigo_act=" + combo.value + "&nombre_act=" + combo.options[combo.selectedIndex].text ;
        }
        
    }   

        function validaenvio()
        {
            if (confirm("Se dispone a enviar información. ¿Desea Continuar? NO PODRA DESHACER LOS CAMBIOS")==true)
                return true;
            else
                return false;
        }
    </script>
    <style type="text/css">


.cunia  	{border-right:1px solid #91A9DB; border-top:1px solid #91A9DB; border-bottom:1px solid #91A9DB; background-position: 0% 0%; width:17px;height:19px; background-image:url('../images/cunia.gif'); background-repeat:no-repeat;}
	
    </style>
</head>
<body style="margin:0,0,0,0">
     <%  response.write(clsfunciones.cargacalendario) %>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td colspan="3" style="border-top: black 1px solid; font-weight: bold; font-size: 11pt;
                    color: white; border-bottom: black 1px solid; font-family: verdana; height: 21px;
                    background-color: firebrick; text-align: center">
                    Registrar Observaciones de Actividad</td>
            </tr>
            <tr>
                <td width="50%">
                    &nbsp;
                    <asp:HyperLink ID="LinkRegresar" runat="server" Style="font-size: 8pt; font-family: verdana">«« Regresar</asp:HyperLink></td>
                <td align="right" colspan="2" style="font-size: 8pt; width: 1416px; color: #000000;
                    font-family: verdana">
                    Actividades para Hoy :&nbsp;
                    <asp:TextBox ID="TxtFecha" runat="server" Width="70px" ValidationGroup="Ver"></asp:TextBox>
                     <input id="Button3" type="button"  class="cunia" 
                    onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFecha,'dd/mm/yyyy')" 
                    style="height: 22px" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                        runat="server" ControlToValidate="TxtFecha" 
                        ErrorMessage="Seleccione la fecha de registro" ValidationGroup="Ver">*</asp:RequiredFieldValidator>
                    &nbsp;<asp:ImageButton ID="ImgVer" runat="server" 
                        ImageUrl="~/images/menus/buscar_small.gif" ValidationGroup="Ver" />
                    &nbsp;<asp:DropDownList ID="DDLActividades" runat="server" Style="border-right: black 1px solid;
                        border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: black;
                        border-bottom: black 1px solid; font-family: verdana; background-color: #fffaf0">
                    </asp:DropDownList>&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" style="border-top: #660000 1px solid; font-weight: bold; font-size: 8pt;
                    text-transform: capitalize; color: #003300; font-family: verdana; font-variant: normal">
                    &nbsp; &nbsp;Profesor :
                    <asp:Label ID="LblProfesor" runat="server" Style="text-transform: uppercase; color: dimgray;
                        font-family: verdana"></asp:Label></td>
            </tr>
            <tr style="font-size: 8pt">
                <td colspan="3" style="font-weight: bold; font-size: 8pt; text-transform: capitalize;
                    color: #003300; border-bottom: #660000 1px solid; font-family: verdana; font-variant: normal">
                    &nbsp; &nbsp;Asignatura :
                    <asp:Label ID="LblAsignatura" runat="server" Style="text-transform: uppercase; color: dimgray;
                        font-family: verdana"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="width: 90%">
                    <asp:Label ID="LblMensaje" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="MediumBlue" Font-Size="11pt"></asp:Label>
                </td>
                <td align="right">
                    <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar2" Height="27px" Text="     Guardar"
                        Width="70px" /></td>
            </tr>
            <tr>
                <td colspan="3">
                    <table id="Table2" runat="server" border="0" cellpadding="0" cellspacing="0" style="height: 29px"
                        width="100%">
                        <tr>
                            <td align="center" style="border-top: black 1px solid; font-weight: bold; font-size: 8pt;
                                border-left: black 1px solid; width: 30px; color: white; border-bottom: black 1px solid;
                                font-family: verdana; height: 28px; background-color: #b22222">
                    N°</td>
                            <td align="center" style="border-top: black 1px solid; font-weight: bold; font-size: 8pt;
                                color: white; border-bottom: black 1px solid; font-family: verdana; height: 28px;
                                background-color: #b22222">
                    Apellidos y Nombres</td>
                            <td align="center" style="border-top: black 1px solid; font-weight: bold; font-size: 8pt;
                                width: 100px; color: white; border-bottom: black 1px solid; font-family: verdana;
                                height: 28px; background-color: #b22222">
                    Hora Ingreso<br />
                                (HH:mm)</td>
                            <td align="center" style="border-top: black 1px solid; font-weight: bold; font-size: 8pt;
                                width: 100px; color: white; border-bottom: black 1px solid; font-family: verdana;
                                height: 28px; background-color: #b22222">
                                Condición</td>
                            <td align="center" style="border-right: black 1px solid; border-top: black 1px solid;
                                font-weight: bold; font-size: 8pt; width: 250px; color: white; border-bottom: black 1px solid;
                                font-family: verdana; height: 28px; background-color: #b22222">
                    Observaciones</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
        <asp:HiddenField ID="HidenAlumnos" runat="server" />
    </form>
</body>
</html>
