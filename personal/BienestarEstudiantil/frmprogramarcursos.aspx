<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmprogramarcursos.aspx.vb" Inherits="frmprogramarcursos" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Programación Académica de Formación Complementaria</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/PopCalendar.js"></script>
    <script type="text/javascript" language="javascript">
        function MarcarCursos(obj)
        {
           //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input') 
            for (var i = 0 ; i < arrChk.length ; i++){
                var chk = arrChk[i]
                //verificar si es Check
                if (chk.type == "checkbox"){
                    chk.checked = obj.checked
                    if (chk.id!=obj.id){
                        PintarFilaMarcada(chk.parentNode.parentNode,obj.checked)
                    }
                }
            }
        }
      
         
        function PintarFilaMarcada(obj,estado)
        {
            if (estado==true){
                obj.style.backgroundColor="#EBEBEB"//#395ACC
            }
            else{
                obj.style.backgroundColor="white"
            }
        }

        window.document.onkeydown = function(e) {
            if (!e) {
                e = event;
            }
            if (e.keyCode == 27) {
                lightbox_close();
            }
        }
        function lightbox_open() {
            window.scrollTo(0, 0);
            document.getElementById('light').style.display = 'block';
            document.getElementById('fade').style.display = 'block';
        }
        function lightbox_close() {
            document.getElementById('light').style.display = 'none';
            document.getElementById('fade').style.display = 'none';
        }
        function llamar() {
            window.open('frmprogramacursosbloques.aspx', 'popup_window', 'width=400,height=250,left=700,top=300,resizable=no,scrolling:yes');
        }
 
    </script>
    <style type="text/css">
        .style1
        {
            height: 30px
        }
        

    </style>
    </head>
<body>
    <form id="form1" runat="server">
 
    <%Response.Write(ClsFunciones.CargaCalendario)%> 
    <p class="usatTitulo">Programación Académica&nbsp;
                <asp:DropDownList ID="dpCodigo_cac" runat="server">
                </asp:DropDownList>
                <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true"  runat="server"></asp:ScriptManager>
    </p>
    <asp:HiddenField ID="hdte" runat="server" />
<table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" 
            border="0">
        <tr style="background-color: #6694e3; color:White">
            <td style="height: 30px; ">
                Carrera Profesional:
                <asp:DropDownList ID="dpCodigo_cpf" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp; Plan de estudios:
                <asp:DropDownList ID="dpCodigo_pes" runat="server">
                </asp:DropDownList>
&nbsp;Ciclo:
                <asp:DropDownList ID="dpCiclo_cur" runat="server">
                </asp:DropDownList>
&nbsp;<asp:Button ID="cmdBuscar" runat="server" Text="Mostrar" ValidationGroup="Cancelar" 
                    Enabled="False" />
            </td>
        </tr>
        <tr>
            <td>
            <asp:UpdatePanel ID="upPlanCurso" runat="server">
            <ContentTemplate>
             <table width="100%">
                        <tr>
                            <td style="width: 40%" valign="top">
                                <div id="listadiv" style="width:100%; height:400px; border:1px solid #99BAE2; ">
                                <asp:HiddenField ID="hdCodigo_Cur" runat="server" />
                                        <asp:GridView ID="grwCursosPlan" runat="server" AutoGenerateColumns="False" 
                                            BorderStyle="Solid" CellPadding="2" DataKeyNames="codigo_cur,grupos" 
                                            GridLines="Horizontal" Width="100%">
                                            <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                                            <Columns>
                                                <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo">
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nombre_cur" HeaderText="Curso">
                                                    <ItemStyle Width="80%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="grupos" HeaderText="Grupos">
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </asp:BoundField>
                                                <asp:CommandField SelectText="Programar" ShowSelectButton="True">
                                                    <ControlStyle Font-Underline="True" />
                                                    <ItemStyle Font-Underline="True" ForeColor="Blue" Width="10%" />
                                                </asp:CommandField>
                                            </Columns>
                                            <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" 
                                                CssClass="usatsugerencia" Font-Bold="True" ForeColor="Red" />
                                            <EmptyDataTemplate>
                                                &nbsp;&nbsp;&nbsp;&nbsp; No se encontraron asignaturas en el Plan de Estudios seleccionado
                                            </EmptyDataTemplate>
                                            <HeaderStyle BackColor="#E8EEF7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                BorderWidth="1px" ForeColor="#3366CC" />
                                            <SelectedRowStyle BackColor="#6699FF" ForeColor="White" />
                                        </asp:GridView>
				</div>
                                <asp:HiddenField ID="hddAgregar" runat="server" />
                                <asp:HiddenField ID="hddModificar" runat="server" />
                                <asp:HiddenField ID="hddEliminar" runat="server" />
                            </td>
                            <td style="width: 60%" valign="top">
                                    <table runat="server" style="border: 1px solid #C2CFF1; width:100%" cellpadding="3" 
                                        cellspacing="0" bgcolor="#FFFFCC" ID="fraDetalleCurso" visible="false">
                                        <tr>
                                            <td colspan="3" style="background-color: #E8EEF7; font-weight: bold;">
                                                Datos de la asignatura</td>
                                            <td style="background-color: #E8EEF7; font-weight: bold;">
                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                                                    AssociatedUpdatePanelID="upPlanCurso" DisplayAfter="3000">
                                                    <ProgressTemplate>
                                                        <img alt="" src="../../../images/cargando2.gif" 
                                                            style="width: 21px; height: 20px" /><span class="rojo">Cargando...</span>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Código</td>
                                            <td colspan="3">
                                                <asp:Label ID="lblcodigo" runat="server"></asp:Label>
                                                .
                                                <asp:Label ID="lblelectivo" runat="server" Text="Curso Electivo" 
                                                    Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Asignatura</td>
                                            <td colspan="3">
                                                <asp:Label ID="lblasignatura" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Créditos</td>
                                            <td>
                                                <asp:Label ID="lblCrd" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                                Tipo:</td>
                                            <td>
                                                <asp:Label ID="lblTipo" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Horas</td>
                                            <td colspan="3">
                                                Teoría:
                                                <asp:Label ID="lblHT" runat="server"></asp:Label>
                                                &nbsp;Práctica:&nbsp;<asp:Label ID="lblHP" runat="server"></asp:Label>
                                                &nbsp;Laboratorio:
                                                <asp:Label ID="lblHL" runat="server"></asp:Label>
                                                Asesoría:
                                                <asp:Label ID="lblHA" runat="server"></asp:Label>
                                                . TOTAL:
                                                <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Departamento Académico</td>
                                            <td colspan="3">
                                                <asp:Label ID="lblDpto" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hdcodigo_dac" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    
                                    <table runat="server" id="fraDetalleGrupoHorario" cellpadding="3" 
                                        cellspacing="0" style="border: 1px solid #C2CFF1; width:100%" visible="false">
                                        
                                        <tr style="background-color: #E8EEF7; font-weight: bold;">
                                            <td colspan="3">
                                                Programar Nuevo Grupo Horario</td>
                                            <td align="right">
                                                <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                                                    ValidationGroup="Guardar" />
                                                &nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Fecha</td>
                                            <td>
                                                <asp:Label ID="lblFecha" runat="server" EnableViewState="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblgrupohor_cup" runat="server" EnableViewState="False" 
                                                    Text="Grupo"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtGrupoHor_cup" runat="server" MaxLength="20" 
                                                    CssClass="cajas">A</asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RqGrupoHorario" runat="server" 
                                                    ControlToValidate="txtGrupoHor_cup" 
                                                    ErrorMessage="Debe especificar la denonimación del grupo horario" 
                                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Nro. Grupos</td>
                                            <td>
                                                <asp:TextBox ID="txtGrupos" runat="server" Columns="3" MaxLength="2" 
                                                    CssClass="cajas">1</asp:TextBox>
                                                    
                                                <asp:RequiredFieldValidator ID="RqGrupos" runat="server" 
                                                    ControlToValidate="txtGrupos" 
                                                    ErrorMessage="Debe especificar el número de grupos horario" 
                                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                Vacantes</td>
                                            <td>
                                                <asp:TextBox ID="txtVacantes" runat="server" Columns="3" MaxLength="3" 
                                                    ValidationGroup="Guardar"></asp:TextBox>
                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                    ControlToValidate="txtVacantes" ErrorMessage="Vacantes Requerida" 
                                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="ValidaVacante" runat="server" 
                                                    ControlToValidate="txtVacantes" ErrorMessage="Valor de Vacante mayor a Cero" 
                                                    MaximumValue="10000" MinimumValue="0" Type="Integer" ValidationGroup="Guardar">*</asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>   
                                            <td>
                                                Fecha Inicio</td>
                                            <td>
                                                <asp:TextBox ID="txtInicio" runat="server" BackColor="#CCCCCC" Font-Size="8pt" 
                                                    ForeColor="Navy" MaxLength="12" style="text-align: right" Columns="12"></asp:TextBox>
                                                <asp:Button ID="cmdInicio" runat="server" CausesValidation="False" 
                                                    Font-Size="7pt" 
                                                    onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtInicio,'dd/mm/yyyy');return(false)" 
                                                    Text="..." UseSubmitBehavior="False" />
                                                <asp:RequiredFieldValidator ID="RqFechaInicio" runat="server" 
                                                    ControlToValidate="txtInicio" 
                                                    ErrorMessage="Debe especificar la fecha de inicio" 
                                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                Fecha Fin</td>
                                            <td>
                                                <asp:TextBox ID="txtFin" runat="server" BackColor="#CCCCCC" Font-Size="8pt" 
                                                    ForeColor="Navy" MaxLength="12" style="text-align: right" Columns="12"></asp:TextBox>
                                                <asp:Button ID="cmdFin" runat="server" CausesValidation="False" Font-Size="7pt" 
                                                    onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFin,'dd/mm/yyyy');return(false)" 
                                                    Text="..." UseSubmitBehavior="False" />
                                                &nbsp;<asp:RequiredFieldValidator ID="RqFechaFin" runat="server" 
                                                    ControlToValidate="txtFin" 
                                                    ErrorMessage="Debe especificar la fecha de término" 
                                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CRqFechaFin" runat="server" 
                                                    ControlToCompare="txtFin" ControlToValidate="txtInicio" 
                                                    ErrorMessage="Fecha de Termino menor o igual a fecha de inicio." 
                                                    Operator="LessThan" Type="Date" ValidationGroup="Guardar">*</asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Fecha Retiro</td>
                                            <td>
                                                <asp:TextBox ID="txtRetiro" runat="server" BackColor="#CCCCCC" Font-Size="8pt" 
                                                    ForeColor="Navy" MaxLength="12" style="text-align: right" Columns="12"></asp:TextBox>
                                                <asp:Button ID="cmdRetiro" runat="server" CausesValidation="False" 
                                                    Font-Size="7pt" 
                                                    onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtRetiro,'dd/mm/yyyy');return(false)" 
                                                    Text="..." UseSubmitBehavior="False" />
                                                &nbsp;<asp:RequiredFieldValidator ID="RqFechaRetiro" runat="server" 
                                                    ControlToValidate="txtRetiro" 
                                                    ErrorMessage="Debe especificar la fecha límite para retiro del curso." 
                                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CRqFechaRetiro" runat="server" 
                                                    ControlToCompare="txtFin" ControlToValidate="txtInicio" 
                                                    ErrorMessage="La Fecha de retiro debe ser menor o igual que la fecha fin" 
                                                    Operator="LessThan" Type="Date" ValidationGroup="Guardar">*</asp:CompareValidator>
                                            </td>
                                            <td>
                                                Estado</td>
                                            <td>
                                                <asp:DropDownList ID="dpestado_cup" runat="server" Enabled="False">
                                                    <asp:ListItem Value="1">Abierto</asp:ListItem>
                                                    <asp:ListItem Value="0">Cerrado</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Registrado</td>
                                            <td>
                                                <asp:Label ID="lblOperador" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                Sólo 1er Ciclo</td>
                                            <td>
                                                <asp:CheckBox ID="ChkPrimerCiclo" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Múltiples Escuelas</td>
                                            <td>
                                                <asp:CheckBox ID="chkMultiple" runat="server" 
                                                    ToolTip="Activar si en este cursos ingresar alumnos de diferentes escuelas" />
                                                <br />
                                            </td>
                                            <td>Turno</td>
                                            <td>
                                                <asp:DropDownList ID="cboTurno" runat="server" Height="18px" Width="100px">
                                                    <asp:ListItem Value="M" Text="Mañana"></asp:ListItem>
                                                    <asp:ListItem Value="T" Text="Tarde"></asp:ListItem>
                                                    <asp:ListItem Value="N" Text="Noche"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Nro. de Bloques</td>
                                            <td>
                                                <asp:TextBox ID="txtBloques" runat="server" MaxLength="2" Width="60px" ReadOnly=true Visible=false>1</asp:TextBox>
                                                
                                                
                                                <asp:Button ID="Button1" Enabled="false" runat="server" Text="Editar" OnClientClick="javascript:llamar();return false;"></asp:Button>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                    ValidationGroup="Guardar" />
                                            </td>
                                        </tr>
                                        <tr style="background-color: #E8EEF7; font-weight: bold; ">
                                            <td colspan="4" class="style1">
                                                &nbsp;<asp:Label ID="lblProfesores" runat="server"></asp:Label>
                                                &nbsp;
                                                <asp:Button ID="cmdNuevoContrato" runat="server" CssClass="agregar2" 
                                                    Text="      Nuevo Profesor" Width="100px" Enabled="False" 
                                                    Visible="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="width: 100%">
                                                <asp:Panel ID="fraProfesores" runat="server" ScrollBars="Vertical" Width="100%" Height="200px" 
                                                    Visible="False">
                                                 
                                                         <asp:CheckBoxList ID="chkProfesor" runat="server" Font-Size="7pt" 
                                                             RepeatColumns="2" Width="100%">
                                                         </asp:CheckBoxList>
                                                     
                                                 </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr runat="server" ID="fraEquivalencias" style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                                            <td colspan="4" >
                                                Marque las &quot;Asignaturas equivalentes de Planes anteriores&quot; que se agruparán con 
                                                esta asignatura</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:GridView ID="grwEquivalencias" runat="server" AutoGenerateColumns="False" 
                                                    BorderStyle="Solid" CaptionAlign="Top" DataKeyNames="codigo_ceq,codigo_curE,codigo_PesE" 
                                                    GridLines="Horizontal" Width="100%">
                                                    <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <!--<asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />-->
                                                            </HeaderTemplate>
                                                            <EditItemTemplate>
                                                                <asp:CheckBox ID="chkElegir" runat="server" Checked='<%# Bind("estado") %>' /> 
                                                            </EditItemTemplate>                                                            
                                                            <ItemTemplate>
                                                            
                                                                <asp:CheckBox ID="chkElegir" runat="server" Checked='<%# Bind("estado") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="nombre_curE" HeaderText="Curso">
                                                            <ItemStyle Font-Size="7pt" Width="65%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="abreviatura_cpfE" HeaderText="Escuela">
                                                            <ItemStyle Font-Size="7pt" Width="15%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="abreviatura_pesE" HeaderText="Plan">
                                                            <ItemStyle Font-Size="7pt" Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="total" HeaderText="Grupos">
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
                                                <asp:Label ID="lblMensajeGrupos" runat="server" Font-Bold="False" 
                                                    Font-Size="11pt" ForeColor="Red" Visible="False">Esta asignatura ha sido 
                                                Programada por otra Escuela Profesional.</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table ID="fraGruposProgramados" runat="server" cellpadding="3" 
                                        cellspacing="0" style="border: 1px solid #C2CFF1; width:100%" visible="false">
                                        <tr style="background-color: #E8EEF7; font-weight: bold;">
                                            <td>
                                                <asp:Label ID="lblGrupos" runat="server" 
                                                    Text="Lista de Grupos Horario Programados"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="cmdAgregar" runat="server" Text="Agregar" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="grwGruposProgramados" runat="server" 
                                                    AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Top" 
                                                    DataKeyNames="codigo_cup" Width="100%" BorderColor="Silver">
                                                    <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo">
                                                            <ItemStyle Font-Size="7pt" Width="5%" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="vacantes_cup" HeaderText="Vacantes">
                                                            <ItemStyle Font-Size="7pt" Width="5%" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="fechainicio_cup" DataFormatString="{0:d}" 
                                                            HeaderText="Inicio">
                                                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="fechafin_cup" DataFormatString="{0:d}" 
                                                            HeaderText="Fin">
                                                            <ItemStyle Width="12%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="abreviaturaA_cpf" HeaderText="Agrupado">
                                                            <ItemStyle Width="50%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="total_mat" HeaderText="Inscritos">
                                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="estado_cup" HeaderText="Estado">
                                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:CheckBoxField DataField="SoloPrimerCiclo_cup" Text="1er Ciclo" />
                                                        <asp:ButtonField ButtonType="Image" CommandName="editar" 
                                                            ImageUrl="../../../images/editar.gif" Text="Editar" />
                                                        <asp:CommandField ShowDeleteButton="True" 
                                                            DeleteText="Eliminar" ButtonType="Image" 
                                                            DeleteImageUrl="../../../images/eliminar.gif">
                                                            <ControlStyle Font-Underline="True" />
                                                            <ItemStyle Width="3%" Font-Underline="True" ForeColor="Blue" 
                                                                HorizontalAlign="Center" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
                                                <asp:HiddenField ID="hddcodigo_cup" runat="server" Value="0" />
                                                <asp:HiddenField ID="hddCapacidadMin" runat="server" />
                                                <asp:HiddenField ID="hddMatriculados" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                            </td>
                        </tr>
                    </table>
       
        
              
                    
            </ContentTemplate>
            </asp:UpdatePanel>
            </td>
        </tr>
        </table>
        

       
                                   
    
        

       
                                   
    </form>
</body>
</html>
