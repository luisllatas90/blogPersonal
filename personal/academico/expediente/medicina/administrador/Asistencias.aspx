<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Asistencias.aspx.vb" Inherits="medicina_administrador_Asistencias" %>
<%@ Register Src="../ingreso.ascx" TagName="ingreso" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Página sin título</title>
    <link rel="stylesheet" type="text/css" href="../../../../../private/estilo.css"/>
    <script language="javascript" src="../../../../../private/tooltip.js" ></script>
    <script type="text/javascript">
    
    function enviacombo(combo)
    {
    <% response.write("pag='evaluacion.aspx?codigo_cac=" & request.querystring("codigo_cac") & "&codigo_syl=" & request.querystring("codigo_syl") & "&codigo_cup=" & request.querystring("codigo_cup") & "&codigo_per=" & request.querystring("codigo_per")  & "&nombre_per=" & request.querystring("nombre_per") & "&nombre_cur=" & request.querystring("nombre_cur") & "'") %>
    if (combo.value != 0)
        {
        location.href=pag + "&codigo_act=" + combo.value + "&nombre_act=" + combo.options[combo.selectedIndex].text ;
        }
        
    }   
        function validaenvio()
        {
            var marcado
            marcado = true
            numcontroles = parseInt(form1.HidenAlumnos.value)
            for (i=1;i<=numcontroles;i++)
                if (eval('form1.Ctrl' + i + '_ChkAsistio.disabled')==false)
                    if (eval('form1.Ctrl' + i + '_ChkAsistio.checked')==false)
                        marcado=false;
                
           if (marcado==false)
                if (confirm("Ud. tiene alumnos sin marcar. ¿Desea continuar? NO PODRA DESHACER LOS CAMBIOS")==true)
                    return true;
                else
                    return false;
            else
                if (confirm("Se dispone a enviar información. ¿Desea Continuar? NO PODRA DESHACER LOS CAMBIOS")==true)
                    return true;
                else
                    return false;
        }
    /*
        function marcartodos(chk)
            {
            numcontroles = parseInt(form1.HidenAlumnos.value)
            valor = chk.checked
            if (valor==true)
                {if (confirm("Si acepta se marcaran todas las casillas activas y se mostrara la hora actual. ¿Desea continuar?")==true)
                    {
                    var fecha = new Date();
                    for (i=1;i<=numcontroles;i++) {
                       if (eval('form1.Ctrl' + i + '_ChkAsistio.disabled')==false)
                                { eval("form1.Ctrl" + i + '_ChkAsistio.checked=true');        
                                  eval("form1.Ctrl" + i + '_TxtInicioHora.value=fecha.getHours();');        
                                  eval("form1.Ctrl" + i + '_TxtInicioMin.value=fecha.getMinutes();');
                                  eval("form1.Ctrl" + i + '_TxtInicioHora.disabled=false');        
                                  eval("form1.Ctrl" + i + '_TxtInicioMin.disabled=false');
                                  //eval("form1.Ctrl" + i + '_TxtObservaciones.disabled=false');
                                  }}
                    }
                 else
                    {  chk.checked=!valor }
                }
            else
                {if (confirm("Se perderán las marcaciones. ¿Desea continuar?")==true)
                    { for (i=1;i<=numcontroles;i++)
                        { if (eval('form1.Ctrl' + i + '_ChkAsistio.disabled')==false)
                                {eval("form1.Ctrl" + i + '_ChkAsistio.checked=false');
                                 eval("form1.Ctrl" + i + '_TxtInicioHora.value=0');        
                                 eval("form1.Ctrl" + i + '_TxtInicioMin.value=0');
                                 eval("form1.Ctrl" + i + '_TxtInicioHora.disabled=true');        
                                 eval("form1.Ctrl" + i + '_TxtInicioMin.disabled=true');
                                 //eval("form1.Ctrl" + i + '_TxtObservaciones.disabled=true');
                                 }}}
                 else
                    {  chk.checked=!valor }
                }
        }*/
    
        function habilita(ctrl) {          
            valor = ctrl.checked
            valor = !valor
            nomcontrol = ctrl.id.substr(0,parseInt(ctrl.id.length)-10)
            eval("form1." + nomcontrol + 'TxtInicioHora.disabled=' + valor);        
            eval("form1." + nomcontrol + 'TxtInicioMin.disabled=' + valor);        
            //eval("form1." + nomcontrol  + 'TxtObservaciones.disabled=' + valor);  
            if (valor==false)
               {    
                    var fecha = new Date();
                    eval("form1." + nomcontrol + 'TxtInicioHora.focus();');                      
                    eval("form1." + nomcontrol + 'TxtInicioHora.select();');
                    eval("form1." + nomcontrol + 'TxtInicioHora.value=fecha.getHours();');        
                    eval("form1." + nomcontrol + 'TxtInicioMin.value=fecha.getMinutes();');        
                }
            else
                {
                eval("form1." + nomcontrol + 'TxtInicioHora.value="0"');        
                eval("form1." + nomcontrol + 'TxtInicioMin.value="0"');        
                //eval("form1." + nomcontrol + 'TxtObservaciones.value=""');  
               }
        }
        
        function numeros()
            {
                var key=window.event.keyCode;//codigo de tecla.
                if (key < 46 || key > 57){//si no es numero 
                window.event.keyCode=0; }//anula la entrada de texto. 
            }
    </script>
</head>
<body style="margin:0,0,0,0">
      <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td style="border-top: black 1px solid; font-weight: bold; font-size: 11pt; color: white; border-bottom: black 1px solid; font-family: verdana; height: 21px; background-color: firebrick; text-align: center" colspan="3">
                    Reporte de Asistencias</td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <asp:HyperLink ID="LinkRegresar" runat="server" style="font-size: 8pt; font-family: verdana" >«« Regresar</asp:HyperLink></td>
                <td colspan="2" style="font-size: 8pt; color: #000000; font-family: verdana; width: 1416px;" align="right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="border-top: #660000 1px solid; font-weight: bold; font-size: 8pt; text-transform: capitalize; color: #003300; font-family: verdana; font-variant: normal" colspan="3">
                    &nbsp; &nbsp;Profesor :
                    <asp:Label ID="LblProfesor" runat="server" Style="text-transform: uppercase;
                        color: dimgray; font-family: verdana"></asp:Label></td>
            </tr>
            <tr style="font-size: 8pt">
                <td colspan="3" style="font-weight: bold; font-size: 8pt; text-transform: capitalize;
                    color: #003300; font-family: verdana; font-variant: normal; border-bottom: #660000 1px solid;">
                    &nbsp; &nbsp;Asignatura :
                    <asp:Label ID="LblAsignatura" runat="server" Style="text-transform: uppercase;
                        color: dimgray; font-family: verdana"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="width: 90%">
                    <asp:Label ID="LblMensaje" runat="server" Font-Bold="True" ForeColor="MediumBlue" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                </td>
                <td align="right" > 
                    <asp:Button ID="cmdGuardar" runat="server" Text="     Guardar" 
                        CssClass="guardar2" Height="27px" Width="70px" Visible="False" /></td>
            </tr>
            <tr>
                <td colspan="3">
        <table runat="server" width="100%" border="0" cellpadding="0" cellspacing="0" style="height: 29px" id="Table1">
            <tr>
                <td style="width: 30px; border-top: black 1px solid; font-weight: bold; font-size: 8pt; border-left: black 1px solid; color: white; border-bottom: black 1px solid; font-family: verdana; height: 28px; background-color: #b22222;" align="center">
                    N°<img src="../../../../../images/help.gif" tooltip="<b>Asistencia</b><br>- En la parte superior derecha seleccione la actividad en la cual desea registrar asistencia. <br>- Marque la casilla que corresponde al alumno cuando el mismo haya asistido a la actividad programada (puede editar la hora y minuto de ingreso), caso contrario no marque la casilla.<br>- Si desea puede marcar como asistido a todos los participantes haciendo clic en el boton ASISTIO en la parte superior de las casillas.<br>- Recuerde que la información <B>INGRESADA NO SE PODRA MODIFICAR, </B> las casillas automáticamente se deshabilitarán." style="cursor: help" /></td>
                <td align="center" style="border-top: black 1px solid; font-weight: bold; font-size: 8pt; color: white; border-bottom: black 1px solid; font-family: verdana; height: 28px; background-color: #b22222">
                    Apellidos y Nombres</td>
                <td style="width: 40px; border-top: black 1px solid; font-weight: bold; font-size: 8pt; color: white; border-bottom: black 1px solid; font-family: verdana; height: 28px; background-color: #b22222;" align="center">
                    Estado<asp:CheckBox ID="ChkTodos" runat="server" Enabled="False" 
                        Visible="False" />
                    </td>
                <td style="width: 100px; border-top: black 1px solid; font-weight: bold; font-size: 8pt; color: white; border-bottom: black 1px solid; font-family: verdana; height: 28px; background-color: #b22222;" align="center">
                    Hora Ingreso<br />
                    (HH:mm)</td>
            </tr>
        </table>
                </td>
            </tr>
        </table><asp:HiddenField ID="HidenAlumnos" runat="server" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
