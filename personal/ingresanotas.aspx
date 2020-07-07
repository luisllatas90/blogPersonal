<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ingresanotas.aspx.vb" Inherits="medicina_ingresanotas" %>
<%@ Register Src="controles/CtrlAsistencia.ascx" TagName="ingreso" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
  <script language="javascript">
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
                                  eval("form1.Ctrl" + i + '_TxtInicioHora.value=form1.HddHoraIni.value;');        
                                  eval("form1.Ctrl" + i + '_TxtInicioMin.value=form1.HddMinIni.value;');
                                  eval("form1.Ctrl" + i + '_TxtInicioHora.disabled=false');        
                                  eval("form1.Ctrl" + i + '_TxtInicioMin.disabled=false');
                                  eval("form1.Ctrl" + i + '_DDLEst.disabled=false');
                                  if (eval("form1.Ctrl" + i + '_TxtNota')!=undefined)
                                    eval("form1.Ctrl" + i + '_TxtNota.disabled=false');
                                  eval("form1.Ctrl" + i + '_DDLEst.value="A"');
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
                                 eval("form1.Ctrl" + i + '_DDLEst.disabled=true');
                                 if (eval("form1.Ctrl" + i + '_TxtNota')!=undefined) 
                                    eval("form1.Ctrl" + i + '_TxtNota.disabled=true');
                                 eval("form1.Ctrl" + i + '_DDLEst.value="F"');
                                 }}}
                 else
                    {  chk.checked=!valor }
                }
        }
        
         function habilita(ctrl) {          
         
         
            valor = ctrl.checked
            valor = !valor
            nomcontrol = ctrl.id.substr(0,parseInt(ctrl.id.length)-10)
            eval("form1." + nomcontrol + 'TxtInicioHora.disabled=' + valor);        
            eval("form1." + nomcontrol + 'TxtInicioMin.disabled=' + valor);        
            eval("form1." + nomcontrol + 'DDLEst.disabled=' + valor);        
            if (eval("form1." + nomcontrol + 'TxtNota') != undefined)
                eval("form1." + nomcontrol + 'TxtNota.disabled=' + valor);        
            
            if (valor==false)
               {    
                    var fecha = new Date();
                    eval("form1." + nomcontrol + 'TxtInicioHora.focus();');                      
                    eval("form1." + nomcontrol + 'TxtInicioHora.select();');
                    eval("form1." + nomcontrol + 'TxtInicioHora.value=form1.HddHoraIni.value;');        
                    eval("form1." + nomcontrol + 'TxtInicioMin.value=form1.HddMinIni.value;');        
                    eval("form1." + nomcontrol + 'DDLEst.value="A"');
                }
            else
                {
                eval("form1." + nomcontrol + 'TxtInicioHora.value="0"');        
                eval("form1." + nomcontrol + 'TxtInicioMin.value="0"');
                eval("form1." + nomcontrol + 'DDLEst.value="F"');
               }
          
        }
      
      function validaenvio()
        {
            var marcado, sw
            marcado = true
            sw = 0
            
            numcontroles = parseInt(form1.HidenAlumnos.value)
            if (numcontroles != 0)
            { for (i=1;i<=numcontroles;i++)
                if (eval('form1.Ctrl' + i + '_ChkAsistio.disabled')==false)
                    if (eval('form1.Ctrl' + i + '_ChkAsistio.checked')==false)
                        marcado=false;
            } else { sw=1; } 
            
           if (sw==0)    
           {
           if (marcado==false)
                if (confirm("¿Está seguro que desea guardar la información?")==true)
                    return true;
                else
                    return false;
            else
                if (confirm("¿Está seguro que desea guardar la información?") == true)
                    return true;
                else
                    return false;
           }
        }
      function numeros()
            {
                var key=window.event.keyCode;//codigo de tecla.
                if (key < 46 || key > 57){//si no es numero 
                window.event.keyCode=0; }//anula la entrada de texto. 
            }
      function validaestado(combo)
        {
            nomcontrol = combo.id.substr(0,parseInt(combo.id.length)-7)
            valor =  eval("form1." + nomcontrol + '_ChkAsistio.checked;');            
            if (valor == true && combo.value=='F')
                {
                combo.value = "A"
                alert('Si desea registrar como FALTO desmarque al check')
                }
        }
        
  </script>
  
</head>
<body>
    <form id="form1" runat="server">
  
    <table style="width:100%;">
        <tr>
            <td colspan="2" >
                <table style="width:100%;">
                    <tr>
                        <td>
                            &nbsp;&nbsp; &nbsp;<asp:ImageButton ID="cmdInicio" runat="server" 
                                ImageUrl="../images/inicioReloj.png" 
                                ToolTip="Registrar Inicio de Actividad" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td align="right" 
                            style="background-image: url('../../../../../images/inicio.gif')">
                            &nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ID="cmdFin" runat="server" 
                                ImageUrl="../images/finReloj.png" 
                                ToolTip="Registrar Fin de Actividad" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Curso</td>
                        <td>
                            <asp:Label ID="LblCurso" runat="server" CssClass="usatCeldaMenuSubTitulo"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Actividad</td>
                        <td>
                            <asp:Label ID="LblActividad" runat="server" CssClass="usatCeldaMenuSubTitulo"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Fecha </td>
                        <td>
                            <asp:Label ID="LblFechaIniTer" runat="server" CssClass="usatCeldaMenuSubTitulo"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <hr class="usatTablaInfo"  />
            </td>
        </tr>
        <tr>
            <td align="left">
                    <asp:Button ID="CmdRegresar" runat="server" CssClass="salir" 
                        Text="    Regresar" Width="81px" />
            </td>
            <td align="right">
                    <asp:Button ID="cmdGuardarArriba" runat="server" Text="     Guardar" 
                        CssClass="guardar2" Height="27px" Width="70px" /></td>
        </tr>
    </table>
    
  <table  runat="server" width="100%" border="0" cellpadding="0" cellspacing="0" 
        style="height: 29px" id="Table1" class="Selected">
            <tr>
                <td style="width: 30px; color: #FFFFFF; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #FFFFFF;" 
                    align="center">
                    N°</td>
                <td align="center" 
                    style="color: #FFFFFF; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #FFFFFF;">
                    Apellidos y Nombres</td>
                <td align="center" width="35" 
                    style="color: #FFFFFF; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #FFFFFF;">
                    Asistió<asp:CheckBox ID="ChkTodos" runat="server" />
                    </td>
                <td align="center" width="75" 
                    style="color: #FFFFFF; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #FFFFFF;">
                    &nbsp;Ingreso<br />
                    (HH:mm)</td>
                <td align="center" width="40" 
                    style="color: #FFFFFF; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #FFFFFF;">
                    Estado</td>
                <td align="center" width="65" 
                    style="color: #FFFFFF; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #FFFFFF;">
                    Nota</td>
                <td align="center" width="230" 
                    style="color: #FFFFFF; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #FFFFFF;">
                    Observaciones</td>
            </tr>
        </table>
        
    <asp:Table ID="TblRegistro" runat="server" CellPadding="0" CellSpacing="0" 
        Width="100%">
        
        
        
        <asp:TableRow ID="TblFila" runat="server">
            <asp:TableCell ID="TblCelda" runat="server" ColumnSpan="7"></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
  
    <asp:HiddenField ID="HidenAlumnos" runat="server" />
  
    <asp:HiddenField ID="HddHoraIni" runat="server" />
    <asp:HiddenField ID="HddMinIni" runat="server" />
  
    <asp:HiddenField ID="HddFecha" runat="server" />
  
    <asp:HiddenField ID="HddConsideraAsistencia" runat="server" />
  
  <table  runat="server" width="100%" border="0" cellpadding="0" cellspacing="0" 
        style="height: 29px"  >
            <tr>
                <td align="right" >
                    <asp:Button ID="cmdGuardarAbajo" runat="server" Text="     Guardar" 
                        CssClass="guardar2" Height="27px" Width="70px" />
                    <br />
                    </td>
            </tr>
        </table>
        
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
        
    </form>
</body>
</html>
