<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmprogramarcursosdpto.aspx.vb" Inherits="frmprogramarcursosdpto" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Programación Académica</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../../private/PopCalendar.js"></script>
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
    <p class="usatTitulo">Programación Académica&nbsp;
    <asp:DropDownList ID="dpCodigo_cac" runat="server">
    </asp:DropDownList>
    <!--<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true"  runat="server"></asp:ScriptManager>-->
    </p>
<table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" 
            border="0">
        <tr style="background-color: #6694e3; color:White">
            <td style="height: 30px; ">
                Dpto Académico:
                <asp:DropDownList ID="dpCodigo_Dac" runat="server">
                </asp:DropDownList>
                &nbsp;&nbsp;<asp:Button ID="cmdVer" runat="server" Text="Consultar" />
                <asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ControlToValidate="dpCodigo_Dac" 
                    ErrorMessage="Seleccione una opción para consultar" ForeColor="Yellow" 
                    Operator="GreaterThanEqual" ValueToCompare="0">Seleccione una opción para consultar</asp:CompareValidator>
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
                                                    ValidationGroup="Guardar" />
                                                &nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Curso</td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="dpCurso" runat="server" AutoPostBack="True">
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
                                                    Text="Grupo"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtGrupoHor_cup" runat="server" CssClass="cajas" 
                                                    MaxLength="20">A</asp:TextBox>
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
                                                <asp:TextBox ID="txtVacantes" runat="server" Columns="4" MaxLength="3">30</asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RqVacantes" runat="server" 
                                                    ControlToValidate="txtVacantes" 
                                                    ErrorMessage="Debe especificar el número vacantes por cada grupo horario" 
                                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                    ControlToValidate="txtVacantes" 
                                                    ErrorMessage="El número de vacantes no puede ser cero, especifique el número válido" 
                                                    Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>

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
                                                Registado</td>
                                            <td>
                                                <asp:Label ID="lblOperador" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                Sólo 1er Ciclo</td>
                                            <td> 
                                                <asp:CheckBox ID="ChkPrimerCiclo" runat="server"  />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Guardar" />
                                            </td>
                                        </tr>
                                        </table>
             <br />
             <table runat="server" ID="fraEquivalencias" cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%" visible="false">
                                        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                                            <td colspan="2" >Escuelas en las que se desarrolla la Asignatura</td>
                                            <td colspan="2" align="right" >
                                                <asp:Button ID="cmdDesagrupar" runat="server" Text="Desagrupar asignaturas" 
                                                    Visible="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:GridView ID="grwEquivalencias" runat="server" AutoGenerateColumns="False" 
                                                    CaptionAlign="Top" DataKeyNames="codigo_pes,codigo_cur,codigo_cup" Width="60%" 
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
                                                        <asp:BoundField DataField="nombre_cur" HeaderText="Curso">
                                                            <ItemStyle Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nombre_cpf" HeaderText="Escuela">
                                                            <ItemStyle Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="abreviatura_pes" HeaderText="Plan">
                                                            <ItemStyle Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="grupos" HeaderText="Grupos">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
              <br />
             <table runat="server" ID="fraProfesores" cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%" visible="false">
                                        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                                            <td >Asignación de Profesores al Grupo Horario</td>
                                            <td align="right" >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                Profesores adscritos:
                                                <asp:DropDownList ID="dpCodigo_per" runat="server">
                                                </asp:DropDownList>
                                                <asp:Button ID="cmdAsignarProfesor" runat="server" 
                                                    Text="Agregar nuevo Profesor" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
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
                                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="../../../images/eliminar.gif" 
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
             <table ID="fraGruposProgramados" runat="server" cellpadding="3" 
                                        cellspacing="0" style="border: 1px solid #C2CFF1; width:100%" visible="false">
                                        <tr style="background-color: #E8EEF7; font-weight: bold;">
                                            <td>
                                                <asp:Label ID="lblGrupos" runat="server" 
                                                    Text="Lista de Grupos Horario Programados"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="cmdAgregar" runat="server" Text="Nuevo grupo" />
                                            &nbsp;<asp:Button ID="cmdAgrupar" runat="server" Text="Agrupar asignaturas" />
                                            </td>
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
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkElegir" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:TemplateField>                                                    
                                                        <asp:BoundField DataField="nombre_cur" HeaderText="Asignatura">
                                                            <ItemStyle Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="escuelaplan" HeaderText="Escuela Principal">
                                                            <ItemStyle Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="fechainicio_cup" DataFormatString="{0:d}" 
                                                            HeaderText="Inicio">
                                                            <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="fechafin_cup" DataFormatString="{0:d}" 
                                                            HeaderText="Fin">
                                                            <ItemStyle Font-Size="7pt" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo">
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
                                                        <asp:CheckBoxField DataField="soloPrimerCiclo_cup" HeaderText="1er Ciclo" 
                                                            ReadOnly="True">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:CheckBoxField>
                                                        <asp:TemplateField HeaderText="Profesor" ItemStyle-VerticalAlign="Top">
                                                            <ItemTemplate>
                                                                <asp:BulletedList ID="lstProfesores" runat="server" DataTextField="profesor" 
                                                                    DataValueField="codigo_per" Font-Size="7pt">
                                                                </asp:BulletedList>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:ButtonField ButtonType="Image" CommandName="editar" 
                                                            ImageUrl="../../../images/editar.gif" Text="Editar" />
                                                        <asp:CommandField ShowDeleteButton="True" 
                                                            DeleteText="Eliminar" ButtonType="Image" 
                                                            DeleteImageUrl="../../../images/eliminar.gif">
                                                            <ControlStyle Font-Underline="True" />
                                                            <ItemStyle Font-Underline="True" ForeColor="Blue" 
                                                                HorizontalAlign="Center" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
                                                <asp:HiddenField ID="hdcodigo_cup" runat="server" Value="0" />
                                                <asp:HiddenField ID="hddCapacidadMin" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
            <!--
            </ContentTemplate>
            </asp:UpdatePanel>
            -->
    </form>
</body>
</html>
