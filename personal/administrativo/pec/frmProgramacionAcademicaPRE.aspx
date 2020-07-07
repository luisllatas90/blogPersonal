<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProgramacionAcademicaPRE.aspx.vb" Inherits="administrativo_pec_frmProgramacionAcademicaPRE" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

        function ejecutar() {
            window.open("frmProgramarHorarios.aspx", "_blank", "toolbar=no, scrollbars=no, resizable=no, top=350, left=500, width=500, height=300,menubar=no");
        }     
    </script>
    <style type="text/css">
        .style1
        {
            height: 30px;
        }
    
        .style2
        {
            color: #0000FF;
            font-weight: bold;
        }
    
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <%Response.Write(ClsFunciones.CargaCalendario)%> 
    <p class="usatTitulo">Programación Académica:&nbsp;
    <asp:DropDownList ID="dpCodigo_cac" runat="server" AutoPostBack="True">
    </asp:DropDownList>
    <!--<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true"  runat="server"></asp:ScriptManager>-->
    </p>
<table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
        <tr>
            <td style="height: 30px;">
                Centro de Costos:                                         
            </td>
            <td colspan="3" style="width:80%">
                <asp:DropDownList 
                                    ID="cboCecos" 
                                    runat="server" 
                                    AutoPostBack="True" 
                                    SkinID="ComboObligatorio" 
                                    Width="95%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="height: 30px; ">
                Plan:                 
                
                &nbsp;</td>
            <td colspan="3">
                <asp:DropDownList ID="dpCodigo_pes" Width="95%" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="height:30px" >
                Filtrado por:</td>
            <td style="width:20%">
                <asp:DropDownList ID="ddlFiltro" Width="100%" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="0">TODOS</asp:ListItem>
                    <asp:ListItem Value="1">CURSOS</asp:ListItem>
                    <asp:ListItem Value="2">NIVELACIÓN</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width:15%">
                Nombre del curso:</td>
            <td>
                <asp:TextBox ID="txtBusqueda" Width="91%" runat="server" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                
                <asp:Button ID="cmdVer" runat="server" Text="Consultar" 
                    CssClass="buscar2" />
                
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height:17px; font-weight: bold; font-size: xx-small;" bgcolor="#ECECFF">
            
                <asp:Label ID="lblMsjFechas" runat="server" Font-Bold="False" 
                    ForeColor="#000099" Text="Fechas definidas para la programación académica son:"></asp:Label>
&nbsp;<asp:Label ID="lblFechaIni" runat="server" ForeColor="#CC0000"></asp:Label>
&nbsp;<asp:Label ID="lblIntervalo" runat="server" Font-Bold="False" ForeColor="#000099" 
                    Text="al"></asp:Label>
&nbsp;<asp:Label ID="LblFechaFin" runat="server" ForeColor="#CC0000"></asp:Label>
            
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
                                                <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                                                    CssClass="guardar2" />
                                                &nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="Regresar" 
                                                    CssClass="regresar2" />
                                                &nbsp;</td>
                                            <td align="right">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="rojo" colspan="4">
                                                (*) Defina correctamente las fechas de inicio y fin para vigencia del curso en 
                                                el Aula Virtual.</td>
                                            <td class="style2">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Curso</td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="dpCurso" runat="server">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblnombre_cur" runat="server"></asp:Label>
                                            </td>
                                            <td rowspan="5">
                                                <asp:GridView ID="gridHorario" runat="server" AutoGenerateColumns="False"
                                                CaptionAlign="Top" DataKeyNames="codigo_lho, codigo_amb" 
                                                    BorderStyle="None" CellPadding="3" 
                                                    GridLines="Horizontal" Visible="False">
                                                     <RowStyle BorderColor="#C2CFF1" />
                                                    <Columns>
                                                        <asp:BoundField DataField="codigo_lho" HeaderText="codigo_lho" Visible="false">
                                                            <ItemStyle Font-Underline="false" ForeColor="#0066CC" />                                                                                                            
                                                       </asp:BoundField>
                                                       
                                                        <asp:BoundField DataField="dia_Lho" HeaderText="Día">
                                                              <ItemStyle Font-Underline="false" ForeColor="#0066CC" />                                                                                                            
                                                       </asp:BoundField>
                                                        
                                                        <asp:BoundField DataField="fechaIni_lho" HeaderText="Fecha" >
                                                              <ItemStyle Font-Underline="false" ForeColor="#0066CC" />                                                                                                            
                                                       </asp:BoundField>
                                                        <asp:BoundField DataField="nombre_hor" HeaderText="Inicio" >
                                                              <ItemStyle Font-Underline="false" ForeColor="#0066CC" />                                                                                                            
                                                       </asp:BoundField>
                                                        <asp:BoundField DataField="horaFin_Lho" HeaderText="Fin" >
                                                              <ItemStyle Font-Underline="false" ForeColor="#0066CC" />                                                                                                            
                                                       </asp:BoundField>
                                                         <asp:BoundField DataField="codigo_amb" HeaderText="codigo_amb" Visible="false" >
                                                              <ItemStyle Font-Underline="false" ForeColor="#0066CC" />                                                                                                            
                                                       </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Ambiente">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlAmbiente" runat="server">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                    <div style="color:#3266DB; background-color:#E8EEF7; padding:5px; font-style:italic;">
                                                     No se ha registrado horario para este curso
                                                    </div>
                                                    </EmptyDataTemplate>
                                                     <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
                                                <br />
                                                <asp:Button ID="cmdRegistrarHorario" runat="server" Text="Registrar Horario" 
                                                    CssClass="horario2" Width="120px" Visible="False" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="cmdGuardarAmbiente" runat="server" Text="Asignar Ambiente" 
                                                    CssClass="guardar2" Width="120px" Visible="False" />
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
                                                    MaxLength="20" Visible="true">A</asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RqGrupoHorario" runat="server" 
                                                    ControlToValidate="txtGrupoHor_cup" 
                                                    ErrorMessage="Debe especificar la denonimación del grupo horario">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                Vacantes</td>
                                            <td class="style1">
                                                <asp:TextBox ID="txtVacantes" runat="server" Columns="4" MaxLength="3">30</asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RqVacantes" runat="server" 
                                                    ControlToValidate="txtVacantes" 
                                                    ErrorMessage="Debe especificar el número vacantes por cada grupo horario">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td class="style1">
                                                </td>
                                            <td class="style1">
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
                                                <asp:Label ID="lblNiv" runat="server" Text="Nivelación:"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="chkNivelacion" ToolTip="Habilitar el Check para indicar que el curso es de NIVELACIÓN" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Registado</td>
                                            <td colspan="3">
                                                <asp:Label ID="lblOperador" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMsjHorarioAmbiente" runat="server" 
                                                    style="font-style: italic; color: #336699; background-color: #FFFFFF"></asp:Label>
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
              <br />
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
                                                        <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nombre_cur" HeaderText="Asignatura">
                                                            <ItemStyle Font-Size="7pt" />
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
                                                        <asp:CheckBoxField DataField="nivelacion_cup" HeaderText="Niv." />
                                                        <asp:TemplateField HeaderText="Profesor" ItemStyle-VerticalAlign="Top">
                                                            <ItemTemplate>
                                                                <asp:BulletedList ID="lstProfesores" runat="server" DataTextField="docente" 
                                                                    DataValueField="codigo_per" Font-Size="7pt">
                                                                </asp:BulletedList>
                                                            </ItemTemplate>

                                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                                        </asp:TemplateField>
                                                        
                                                         
                                                         <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <table style="width:100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:HiddenField ID="hfcodigo_cup"  runat="server" value='<%# Bind("codigo_cup") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton 
                                                                                    ID="ibtnInvitar" 
                                                                                    runat="server" 
                                                                                    ToolTip="Invitar al Curso" 
                                                                                    Visible='<%# iif(Eval("nivelacion_cup") = "1", "True", "False") %>'
                                                                                    ImageUrl="../../../images/invitar.png" 
                                                                                    onclick="ibtnInvitar_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="3%" HorizontalAlign="Center" VerticalAlign="Middle" />
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
