<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmmatriculaprogramaespecial.aspx.vb" Inherits="frmmatriculaprogramaespecial" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Matricula Cursos Especiales</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/PopCalendar.js"></script>
    <script type="text/javascript" language="javascript">
        function MarcarCursos(obj)
        {
           //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input');
            for (var i = 0 ; i < arrChk.length ; i++){
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox"){
                    chk.checked = obj.checked;
                    if (chk.id!=obj.id){
                        PintarFilaMarcada(chk.parentNode.parentNode,obj.checked)
                    }
                }
            }
        }
      
         
        function PintarFilaMarcada(obj,estado)
        {
            if (estado==true){
                obj.style.backgroundColor="#FFE7B3"
            }
            else{
                obj.style.backgroundColor="white"
            }
        }        
    </script>
    </head>
<body>
    <form id="form1" runat="server">
    <%Response.Write(ClsFunciones.CargaCalendario)%> 
    <p class="usatTitulo">Matricula Programas de Profesionalización. Ciclo:&nbsp;
    <asp:DropDownList ID="dpCodigo_cac" runat="server">
    </asp:DropDownList>
    <!--<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true"  runat="server"></asp:ScriptManager>-->
    </p>
<table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" 
            border="0">
        <tr style="background-color: #6694e3; color:White">
            <td style="height: 30px; ">
                Programa:
                <asp:DropDownList ID="dpCodigo_pes" runat="server">
                </asp:DropDownList>
                &nbsp;<asp:Button ID="cmdVer" runat="server" Text="Consultar" 
                    CssClass="buscar2" />
            </td>
        </tr>
        </table>
            <!--<asp:UpdatePanel ID="upPlanCurso" runat="server">
            <ContentTemplate>-->
             <table runat="server" id="fraDetalleGrupoHorario" cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%" visible="false">
                                        
                                        <tr style="background-color: #E8EEF7; font-weight: bold;">
                                            <td colspan="3">
                                                Datos del Curso Programado</td>
                                            <td align="right">
                                                <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" Enabled="False" 
                                                    CssClass="guardar2" />
                                                &nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="Regresar" 
                                                    CssClass="regresar2" />
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="rojo" colspan="4">
                                                (*) Defina correctamente las fechas de inicio y fin para vigencia del curso en 
                                                el Aula Virtual.</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Curso</td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="dpCurso" runat="server">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblnombre_cur" runat="server"></asp:Label>
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
                                                    Text="Grupo" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtGrupoHor_cup" runat="server" CssClass="cajas" 
                                                    MaxLength="20" Visible="False">A</asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RqGrupoHorario" runat="server" 
                                                    ControlToValidate="txtGrupoHor_cup" 
                                                    ErrorMessage="Debe especificar la denonimación del grupo horario">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Vacantes</td>
                                            <td>
                                                <asp:TextBox ID="txtVacantes" runat="server" Columns="4" MaxLength="3">30</asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RqVacantes" runat="server" 
                                                    ControlToValidate="txtVacantes" 
                                                    ErrorMessage="Debe especificar el número vacantes por cada grupo horario">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
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
                                                    ErrorMessage="Debe especificar la fecha de inicio">*</asp:RequiredFieldValidator>
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
                                                    ErrorMessage="Debe especificar la fecha de término">*</asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CRqFechaFin" runat="server" 
                                                    ControlToCompare="txtFin" ControlToValidate="txtInicio" 
                                                    ErrorMessage="Fecha de Termino menor o igual a fecha de inicio." 
                                                    Operator="LessThan" Type="Date">*</asp:CompareValidator>
                                                <asp:TextBox ID="txtRetiro" runat="server" BackColor="#CCCCCC" Font-Size="8pt" 
                                                    ForeColor="Navy" MaxLength="12" style="text-align: right" Columns="12" 
                                                    Visible="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Estado</td>
                                            <td>
                                                <asp:DropDownList ID="dpestado_cup" runat="server" Enabled="False">
                                                    <asp:ListItem Value="1">Abierto</asp:ListItem>
                                                    <asp:ListItem Value="0">Cerrado</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Registado</td>
                                            <td colspan="3">
                                                <asp:Label ID="lblOperador" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        </table>
             <br />
             <table runat="server" ID="fraProfesores" cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%" visible="false">
                                        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                                            <td >Asignación de Profesores al Grupo Horario</td>
                                           
                                        </tr>
                                        <tr>
                                            <td>
                                                Profesor:
                                                <asp:DropDownList ID="dpCodigo_per" runat="server">
                                                </asp:DropDownList>
                                                <asp:Button ID="cmdAsignarProfesor" runat="server" 
                                                    Text="Agregar" Visible="False" CssClass="agregar2" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grwProfesores" runat="server" AutoGenerateColumns="False" 
                                                    CaptionAlign="Top" DataKeyNames="codigo_per" Width="60%" 
                                                    EnableModelValidation="True" BorderStyle="None" CellPadding="3" 
                                                    GridLines="Horizontal">
                                                    <RowStyle BorderColor="#C2CFF1" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        <asp:BoundField DataField="docente" HeaderText="Profesor">
                                                            <ItemStyle Font-Underline="True" ForeColor="#0066CC" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="descripcion_fun" HeaderText="Función">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="totalHorasAula" HeaderText="Hrs. Clase">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="totalHorasAsesoria" HeaderText="Hrs. Asesoría">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="totalHoras_Car" HeaderText="Total Hrs.">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="../../images/eliminar.gif" 
                                                            ShowDeleteButton="True">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>             
              <br />
             <table runat="server" ID="fraAlumnosPlan" cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%" visible="false">
                                        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                                            <td >Estudiantes según el Plan de Estudios y versión del Programa</td>
                                            <td align="right" >&nbsp;<asp:DropDownList ID="dpVersion" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
						<span class="rojo">Para realizar retiros de estudiantes en la asignatura, debe efectuarlo a través de la Of. Evaluación y Registros</span>
						<br />
                                                <asp:GridView ID="grwAlumnosPlan" runat="server" AutoGenerateColumns="False" 
                                                    CaptionAlign="Top" DataKeyNames="codigo_alu" Width="60%" 
                                                    EnableModelValidation="True" BorderStyle="None" CellPadding="1" 
                                                    GridLines="None">
                                                    <RowStyle BorderColor="#C2CFF1" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkElegir" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="#">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="codigouniver_alu" HeaderText="Código">
                                                            <ItemStyle Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="alumno" HeaderText="Apellidos y Nombres">
                                                            <ItemStyle Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Estado">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Deuda">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
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
                                                <asp:Button ID="cmdAgregar" runat="server" Text="Nuevo grupo" 
                                                    CssClass="agregar2" Width="100px" />
                                            &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="grwGruposProgramados" runat="server" 
                                                    AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Top" 
                                                    DataKeyNames="codigo_cup" Width="100%" BorderColor="Silver" 
                                                    EnableModelValidation="True" CellPadding="2">
                                                    <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        <asp:BoundField DataField="nombre_cur" HeaderText="Asignatura">
                                                            <ItemStyle Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="fechainicio_cup" HeaderText="Inicio" 
                                                            DataFormatString="{0:d}">
                                                            <ItemStyle Font-Size="7pt" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="fechafin_cup" DataFormatString="{0:d}" 
                                                            HeaderText="Fin">
                                                            <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="vacantes_cup" HeaderText="Vacantes">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="total_mat" HeaderText="Inscritos">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="estado_cup" HeaderText="Estado">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Profesor" ItemStyle-VerticalAlign="Top">
                                                            <ItemTemplate>
                                                                <asp:BulletedList ID="lstProfesores" runat="server" DataTextField="docente" 
                                                                    DataValueField="codigo_per" Font-Size="7pt">
                                                                </asp:BulletedList>
                                                            </ItemTemplate>

                                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:ButtonField ButtonType="Image" CommandName="editar" 
                                                            ImageUrl="../../images/editar.gif" Text="Editar" />
                                                        <asp:CommandField ShowDeleteButton="True" 
                                                            DeleteText="Eliminar" ButtonType="Image" 
                                                            DeleteImageUrl="../../images/eliminar.gif">
                                                            <ControlStyle Font-Underline="True" />
                                                            <ItemStyle Font-Underline="True" ForeColor="Blue" 
                                                                HorizontalAlign="Center" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
                                                <asp:HiddenField ID="hdcodigo_cup" runat="server" Value="0" />
                                            </td>
                                        </tr>
                                    </table>
            <!--
            </ContentTemplate>
            </asp:UpdatePanel>
            -->
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
