<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmmatricula_cursoprogramado.aspx.vb" Inherits="frmmatricula_cursoprogramado" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Matricula Cursos Especiales</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>
 <style type="text/css" >
        .MensajeOk {
            background-color:Blue;
            color:White;
        }
        .MensajeError {
            background-color:red;
            color:White;
        }
    </style>

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
    <p class="usatTitulo">Matricula por Curso Programado</p>
<table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" 
            border="0">
        <tr>
            <td style="height: 30px; ">
                &nbsp;Ciclo Académico: &nbsp;<asp:DropDownList ID="dpCodigo_cac" runat="server">
    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="height: 30px; ">
                Centro de Costos:                                         <asp:DropDownList ID="cboCecos" runat="server" AutoPostBack="True" 
                                            SkinID="ComboObligatorio" Width="500px">
                                        </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="height: 30px; ">
                Plan de Estudio:
                <asp:DropDownList ID="dpCodigo_pes" runat="server">
                </asp:DropDownList>
                &nbsp;<asp:Button ID="cmdVer" runat="server" Text="Consultar" 
                    CssClass="buscar2" Visible="False" />
            </td>
        </tr>
        </table>
           <asp:HiddenField ID="HdCronograma" runat="server" />
           <asp:Label ID="lblMensaje"  runat="server" Text=""></asp:Label>
        <table runat="server" ID="fraAlumnosPlan" cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%" visible="false">
                                        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                                            <td style="width:80%">
                                                <asp:Label ID="lblnombre_cur" runat="server" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td align="right" style="width:20%">
                                                <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                                                    CssClass="guardar2" />
                                                &nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="Regresar" 
                                                    CssClass="regresar2" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
						                    <span class="rojo">No se permiten matrículas si el estudiante está INACTIVO, 
                                                verificar si el estudiante tiene deuda (Máximo 1 deudas vencidas) y evaluar su matricula </span>
                                                <br />
						                    <br />
						                        <asp:HiddenField ID="hdBloqueo" runat="server" />
                                                <asp:GridView ID="grwAlumnosPlan" runat="server" AutoGenerateColumns="False"  CssClass="table table-bordered bs-table datatable" 
                                                    CaptionAlign="Top" DataKeyNames="codigo_alu" Width="100%" 
                                                    EnableModelValidation="True" BorderStyle="None" CellPadding="1">
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
                                                            <ItemStyle Font-Size="7pt" Width="30%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="email_alu" HeaderText="Email" />
                                                        <asp:BoundField HeaderText="Estado">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Deuda">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="numeroDeudas" HeaderText="No. Deudas" />
                                                        <asp:BoundField HeaderText="Matriculado" >
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="tieneconvenio" HeaderText="Convenio" />
                                                        <asp:BoundField DataField="CursoAprobado" HeaderText="Curso Aprobado" >
                                                        <ItemStyle Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CumpleRecquisito" HeaderText="Cumple Requisito" >
                                                        <ItemStyle Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CantReq" HeaderText="Nro.Requisitos" 
                                                            Visible="False" />
                                                    </Columns>
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
                                                <br />
                                                Nota: Para realizar retiros de estudiantes en la asignatura, debe efectuarlo a través de la opción [Matrícula, Por estudiante]
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
                                                        <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo Curso">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="descripcion_cac" HeaderText="Ciclo Programado">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="descripcion_pes" HeaderText="Plan Estudio" />
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
                                                            ImageUrl="../../../images/download.gif" Text="Editar" 
                                                            HeaderText="Matricular" >
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:ButtonField>
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
