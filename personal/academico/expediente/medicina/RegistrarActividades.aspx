<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RegistrarActividades.aspx.vb" Inherits="medicina_RegistrarActividades" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Actividades</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    /* .... */
    .FondoAplicacion
    {
        background-color: Gray;
        filter: alpha(opacity=60);
        opacity: 0.6;
    }
        </style>
    <script type="text/javascript">
         /**************************************************************
         Máscara de entrada. Script creado por Tunait! (21/12/2004)
         Si quieres usar este script en tu sitio eres libre de hacerlo con la condición de que permanezcan intactas estas líneas, osea, los créditos.
         No autorizo a distribuír el código en sitios de script sin previa autorización
         Si quieres distribuírlo, por favor, contacta conmigo.
         Ver condiciones de uso en http://javascript.tunait.com/
         tunait@yahoo.com 
         ****************************************************************/
         var patron = new Array(2, 2, 4)
         //var patron2 = new Array(1, 3, 3, 3, 3)
         var patron2 = new Array(2, 2)
        function mascara(d,sep,pat,nums){
        if(d.valant != d.value){
	        val = d.value
	        largo = val.length
	        val = val.split(sep)
	        val2 = ''
	        for(r=0;r<val.length;r++){
		        val2 += val[r]	
	        }
	        if(nums){
		        for(z=0;z<val2.length;z++){
			        if(isNaN(val2.charAt(z))){
				        letra = new RegExp(val2.charAt(z),"g")
				        val2 = val2.replace(letra,"")
			        }
		        }
	        }
	        val = ''
	        val3 = new Array()
	        for(s=0; s<pat.length; s++){
		        val3[s] = val2.substring(0,pat[s])
		        val2 = val2.substr(pat[s])
	        }
	        for(q=0;q<val3.length; q++){
		        if(q ==0){
			        val = val3[q]
		        }
		        else{
			        if(val3[q] != ""){
				        val += sep + val3[q]
				        }
		        }
	        }
	        d.value = val
	        d.valant = val
	        }
        }
    </script>        
      </head>
    <body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width:100%" cellpadding="3" cellspacing="0" border="0">
        <tr>
            <td  >
            </td>
            <td >
                &nbsp;</td>
        </tr>
        <tr >
            <td >
                Curso</td>
            <td >
                <asp:Label ID="lblcurso" runat="server" CssClass="usatCeldaMenuSubTitulo"></asp:Label>
            </td>
        </tr>
        <tr >
            <td>
                Cronograma</td>
            <td>
                Fecha Inicio:
                <asp:Label ID="lblInicio" runat="server" CssClass="usatCeldaMenuSubTitulo" ></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp; Fecha Fin:
                <asp:Label ID="lblFin" runat="server" CssClass="usatCeldaMenuSubTitulo" ></asp:Label>
            </td>
        </tr>
        <tr >
            <td colspan="2">
                <hr class="usatTablaInfo"  />
            </td>
        </tr>
         <tr >
            <td colspan="2">
                 <asp:Button ID="CmdRegresar" runat="server" CssClass="salir" 
                    Text="      Regresar" 
                    Width="78px" Height="25px" />            
                 <asp:Label ID="lblAviso" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
             </td>
        </tr>
        <tr>
            <td valign="top" width="200" align="center" class="Selected">
                Fechas de Clases</td>
            <td align="center" width="100%" valign="top" rowspan="2">
   
    <table style="width:100%" cellpadding="3" border="0" cellspacing="0">
        <tr>
            <td bgcolor="#E9E4C7" align="center">
                <asp:Label ID="LblDiaSeleccionado" runat="server" 
                    CssClass="usatTitulousat"></asp:Label>
   
            </td>
        </tr>
        <tr>
            <td class="usatCeldaTotal" bgcolor="#FFFF99" align="left">
                <img alt="" src="../images/librohoja.gif" style="width: 12px; height: 15px" />
                Actividades o Clases</td>
        </tr>
        <tr>
            <td class="usatCeldaTotal" align="right">
                &nbsp;&nbsp;<asp:DropDownList ID="DDLEstadoAct" runat="server" AutoPostBack="True" 
                    CssClass="usatLinkCelda" Height="26px" Width="154px">
                    <asp:ListItem Value="0">--- Todas Segun Estado ---</asp:ListItem>
                    <asp:ListItem Value="N">Asistencia sin Registrar</asp:ListItem>
                    <asp:ListItem Value="S">Asistencia Registrada</asp:ListItem>
                </asp:DropDownList>
                            </td>
        </tr>
        <tr>
            <td >
                <asp:GridView ID="DgvRegDelDia" runat="server" AutoGenerateColumns="False" 
                    GridLines="Horizontal" Width="100%" DataKeyNames="codigo_act">
                    <RowStyle Height="22px" />
                    <Columns>
                        <asp:BoundField DataField="codigo_act" HeaderText="Cod." >
                            <ItemStyle Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion_Act" HeaderText="Actividad" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaini_act" DataFormatString="{0:dd-MM-yyyy}" 
                            HeaderText="Fecha" HtmlEncode="False" >
                            <ItemStyle HorizontalAlign="Center" Width="85px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaini_act" DataFormatString="{0:HH:mm}" 
                            HeaderText="H.I." HtmlEncode="False" >
                            <ControlStyle BackColor="#3366FF" />
                            <HeaderStyle BackColor="#3366FF" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Width="35px" BackColor="#3366FF" 
                                ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechafin_act" DataFormatString="{0:HH:mm}" 
                            HeaderText="H.F." >
                            <HeaderStyle BackColor="#FF9933" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Width="35px" BackColor="#FF9933" 
                                ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="considerarnota_act" HeaderText="C.N.">
                            <HeaderStyle BackColor="#CCFF99" ForeColor="Black" />
                            <ItemStyle HorizontalAlign="Center" Width="25px" ForeColor="Black" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estadorealizado_act" HeaderText="A.R." >
                            <HeaderStyle BackColor="#009999" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Width="25px" ForeColor="Black" />
                        </asp:BoundField>
                        <asp:CommandField ShowEditButton="True" ButtonType="Image" 
                            EditImageUrl="../images/editar.gif" HeaderImageUrl="../images/editar.gif">
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Image" 
                            DeleteImageUrl="~/images/menus/Eliminar_s.gif" 
                            HeaderImageUrl="~/images/menus/Eliminar_s.gif">
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                        </asp:CommandField>
                        <asp:HyperLinkField HeaderText="A. N." 
                            Text="Asistencia" DataNavigateUrlFields="codigo_cup,codigo_act,codigo_syl" 
                            DataNavigateUrlFormatString="ingresanotas.aspx?codigo_cup={0}&amp;codigo_act={1}&amp;codigo_syl={2}">
                            <HeaderStyle BackColor="#CCCCFF" />
                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                        </asp:HyperLinkField>
                    </Columns>
                    <EmptyDataTemplate>
                        <center style="width:100%; border-width:1px; background:#FEFFE1" >
                        <span ><b>
                            <br />
                            No ha registrado una actividad para el día seleccionado, elija
                        <br />
                            una de las alternativas en la parte siguiente y luego clic en Nueva Actividad.
                            <br />
                            En su defecto elija el día en el CALENDARIO de la izquierda para registrar una 
                            actividad o Asistencia.</b>&nbsp;
                            <br />
                            <br></br>
                            </span></center>
                    </EmptyDataTemplate>
                    <HeaderStyle Height="22px" BackColor="#EBEBE0" />
                </asp:GridView>
  
            </td>
        </tr>
          <tr>
            <td class="usatCeldaTotal" align="center">
                                                                <asp:Label ID="LblMensaje" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </td>
        </tr>
        
          <tr>
            <td class="usatCeldaTotal" align="left">
                Elija una de las alternativas siguientes para Agregar una actividad.
                </td>
        </tr>
        
        <tr>
            <td align="left">
                
                        <asp:RadioButtonList ID="RbHoras" runat="server" BackColor="#E8F3FF" 
                    BorderColor="#3366FF" BorderStyle="Solid" BorderWidth="1px" Width="100%">
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="usatCeldaTotal" align="right">
                
                        <asp:Button ID="CmdNuevo" runat="server" 
                    Text="Nueva Actividad" CssClass="usatnuevo" 
                    ToolTip="Seleccione una de las alternativas y luego clic para registrar una actividad." 
                    Width="115px" Height="27px" Enabled="False" />
                
            </td>
        </tr>
              </table>
            </td>
        </tr>
        <tr>
            <td valign="top" width="200">
                <asp:Calendar ID="CalHorario" runat="server" SelectedDate="2007-08-20" 
                    Width="200px"></asp:Calendar>
                <br />
                <table style="width:100%;" class="cajas3">
                    <tr>
                        <td align="center" colspan="2" class="Selected">
                            Leyenda de cuadros
                            <br />
                            (parte derecha)</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td bgcolor="#3366FF" style="color: #FFFFFF">
                            H.I.</td>
                        <td>
                            Hora de Inicio</td>
                    </tr>
                    <tr>
                        <td bgcolor="#FF9933" style="color: #FFFFFF">
                            H.F.</td>
                        <td>
                            Hora Final</td>
                    </tr>
                    <tr>
                        <td bgcolor="#CCFF99">
                            C.N.</td>
                        <td>
                            Considera Nota en la actividad.</td>
                    </tr>
                    <tr>
                        <td bgcolor="#009999">
                            A.R.</td>
                        <td>
                            Actividad Realizada</td>
                    </tr>
                    <tr>
                        <td bgcolor="#CCCCFF">
                            A.N.</td>
                        <td>
                            Registrar Asistencia y Notas</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <br />
                
                <table style="width:100%;" class="cajas3" >
                    <tr>
                        <td align="center" class="Selected">
                            Consultas y Reportes</td>
                    </tr>
                    <tr>
                        <td>
                            <img alt="" src="../images/rpta.GIF" style="width: 16px; height: 11px" />
                            <asp:HyperLink ID="HpLinkAsistencia" runat="server" 
                                NavigateUrl="~/medicina/reportes/reporteasistencia.aspx">Asistencias</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img alt="" src="../images/rpta.GIF" style="width: 16px; height: 11px" />
                            <asp:HyperLink ID="HpLinkNotas" runat="server" 
                                NavigateUrl="reportenotas.aspx">Notas Ingresadas</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img alt=" " src="../images/rpta.GIF" style="width: 16px; height: 11px" />
                            <asp:HyperLink ID="HpLinkResumen" runat="server" 
                                NavigateUrl="reportenotas.aspx">Consolidado por Alumno</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img alt=" " src="../images/rpta.GIF" style="width: 16px; height: 11px" />
                            <asp:HyperLink ID="HpLinkAlumnos" runat="server" 
                                NavigateUrl="reportes/reporteAlumnos.aspx">Listado de alumnos</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    </table>
                <br />
                <asp:Button ID="CmdNuevoInv" runat="server" BorderStyle="None" 
                    CssClass="SelOff" Enabled="False" />
                <asp:HiddenField ID="HddFecha" runat="server" />
            </td>
        </tr>
        </table>

    <cc1:ModalPopupExtender ID="MPEActividad" runat="server"
        CancelControlID="cmdCancelar"
        PopupControlID="PanelDatos"
        TargetControlID="CmdNuevoInv"  BackgroundCssClass="FondoAplicacion" Y="0" />
                        <asp:Panel ID="PanelDatos" runat="server" Height="300px" 
                            Width="500px">
                            <table border="2" cellpadding="0" cellspacing="0" 
                                style="width: 100%; height: 100%;">
                                <tr>
                                    <td>
                                        <table bgcolor="White" cellpadding="0" cellspacing="0" 
                                            style="width: 100%; height: 100%;">
                                            <tr>
                                                <td align="left" bgcolor="#5C7CB1" height="20" 
                                                    style="color: #FFFFFF; font-weight: bold;">
                                                    &nbsp;&nbsp; Registrar Actividad</td>
                                                <td align="right" bgcolor="#5C7CB1" height="20">
                                                    <asp:ImageButton ID="CmdCancelar" runat="server" 
                                                        ImageUrl="../images/menus/salir_prp.gif" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height:300px" valign="top">
                                                    <table cellpadding="4" style="width:100%;">
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Nombre Actividad&nbsp;&nbsp; :</td>
                                                            <td>
                                                                <asp:TextBox ID="TxtActividad" runat="server" 
                                                                    CssClass="cajas3" MaxLength="300" Width="334px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                    ControlToValidate="TxtActividad" ErrorMessage="Nombre de Actividad requerido" 
                                                                    ValidationGroup="guardar">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Temas a tratar :</td>
                                                            <td>
                                                                <asp:TextBox ID="TxtTemas" runat="server" CssClass="cajas3" MaxLength="500" 
                                                                    TextMode="MultiLine" Width="334px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Fecha&nbsp; :</td>
                                                            <td>
                                                                <asp:Label ID="LblFecha" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Hora Inicio&nbsp; :</td>
                                                            <td>
                                                                <asp:TextBox ID="TxtHoraInicio" runat="server" 
                                                                    CssClass="cajas3" MaxLength="5" Width="37px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                    ControlToValidate="TxtHoraInicio" ErrorMessage="Hora inicio requerida" 
                                                                    ValidationGroup="guardar">*</asp:RequiredFieldValidator>
                                                                Ej. 08:00 ó&nbsp; 17:50&nbsp;&nbsp; (Formato de 24 horas)</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Hora Fin&nbsp; :</td>
                                                            <td>
                                                                <asp:TextBox ID="TxtHoraFin" runat="server" CssClass="cajas3" MaxLength="5" 
                                                                    Width="37px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                                    ControlToValidate="TxtHoraFin" ErrorMessage="Hora termino requerida" 
                                                                    ValidationGroup="guardar">*</asp:RequiredFieldValidator>
                                                                Ej. 08:00 ó&nbsp; 17:50&nbsp;&nbsp; (Formato de 24 horas)</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Tipo de Actividad&nbsp; :</td>
                                                            <td>
                                                                <asp:DropDownList ID="DDLTipoActividad" runat="server" CssClass="cajas3" 
                                                                    Width="194px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Registra Asistencia:</td>
                                                            <td>
                                                                <asp:CheckBox ID="ChkAsistencia" runat="server" Text="Habilitar" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Registrar Notas : </td>
                                                            <td>
                                                                <asp:CheckBox ID="ChkHabilitar" runat="server" Text="Habilitar" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Ponderación&nbsp; :</td>
                                                            <td>
                                                                <asp:DropDownList ID="DDLPonderacion" runat="server" CssClass="cajas3" 
                                                                    Height="18px" Width="34px">
                                                                    <asp:ListItem>1</asp:ListItem>
                                                                    <asp:ListItem>2</asp:ListItem>
                                                                    <asp:ListItem>3</asp:ListItem>
                                                                    <asp:ListItem>4</asp:ListItem>
                                                                    <asp:ListItem>5</asp:ListItem>
                                                                    <asp:ListItem>6</asp:ListItem>
                                                                    <asp:ListItem>7</asp:ListItem>
                                                                    <asp:ListItem>8</asp:ListItem>
                                                                    <asp:ListItem>9</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:Button ID="CmdGuardar" runat="server" CssClass="guardar" Height="23px" 
                                                                    Text="    Guardar" ValidationGroup="guardar" />
                                                                <asp:HiddenField ID="HddCodigo_act" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="guardar" />
    </form>
</body>
</html>
