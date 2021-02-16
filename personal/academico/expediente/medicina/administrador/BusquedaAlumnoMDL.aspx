<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BusquedaAlumnoMDL.aspx.vb"
    Inherits="medicina_administrador_BusquedaAlumno" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>

    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>

    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">


        function AsignarValores(v1, v2, v3, v4, v5, v6) {
            //document.form1.CmdEvaluaciones.disabled=false;
            document.form1.CmdAsistencias.disabled = false;
            document.form1.HddProfesor.value = v1;
            document.form1.HddCurso.value = v2;
            document.form1.HddDetalleMat.value = v3;
            document.form1.HddSylabus.value = v4;
            document.form1.HddAlumno.value = v5;
            document.form1.HddCiclo.value = v6;
        }
        /*
        function redirectEvaluacion()
        { 
        parent.location.href = 'reporteasistenciaynotas.aspx?codigo_cup=' + document.form1.txtelegido.value.substr(4) + '&nombre_cur=' + document.form1.HddCurso.value + '&nombre_per=' + document.form1.HddProfesor.value + '&codigo_syl=' + document.form1.HddSylabus.value + '&codigo_dma=' + document.form1.HddDetalleMat.value + '&codu=' + document.form1.HddCodalu.value + '&codigo_sem=' + document.form1.HddCiclo.value
        }
        
        function redirectAsistencia()
        {
        parent.location.href = 'reporteasistenciaynotas.aspx?codigo_cup=' + document.form1.txtelegido.value.substr(4) + 
        '&nombre_cur=' + document.form1.HddCurso.value + 
        '&nombre_per=' + document.form1.HddProfesor.value + 
        '&codigo_syl=' + document.form1.HddSylabus.value + 
        '&codigo_dma=' + document.form1.HddDetalleMat.value + 
        '&codu=' + document.form1.HddCodalu.value + 
        '&codigo_sem=' + document.form1.HddCiclo.value +
        '&nom=' + document.form1.HddNombres.value
        }*/
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="font-weight: bold; font-size: small; font-family: Verdana;">
                Mostrar Asistencias y Notas Parciales por Estudiante
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Código Universitario o Apellidos o Nombres<br />
                &nbsp;<asp:TextBox ID="TxtBusqueda" runat="server" Width="300px"></asp:TextBox>
                &nbsp; &nbsp;&nbsp;Asistencia y Notas del Ciclo
                <asp:DropDownList ID="DdlCicloAcad" runat="server">
                </asp:DropDownList>
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="CmdBuscar" runat="server" CssClass="buscar" Height="22px" Text="Buscar"
                    Width="85px" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridAlumnos" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_alu"
                    Width="100%" Style="text-align: center">
                    <Columns>
                        <asp:BoundField DataField="codigo_alu" HeaderText="codigo_alu" InsertVisible="False"
                            ReadOnly="True" SortExpression="codigo_alu" Visible="False" />
                        <asp:BoundField DataField="codigoUniver_alu" HeaderText="Cod. Universitario" SortExpression="codigoUniver_alu">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="alumno" HeaderText="Apellidos y Nombres" ReadOnly="True"
                            SortExpression="alumno">
                            <ItemStyle HorizontalAlign="Left" Width="500px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre_cpf" HeaderText="Carrera Profesional" />
                        <asp:BoundField DataField="estadoActual_Alu" HeaderText="Estado">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estadoDeuda_Alu" HeaderText="Tiene Deuda">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:HyperLinkField HeaderText="Asistencias" Text="Ver" />
                        <asp:HyperLinkField HeaderText="Notas" Text="Ver" />
                    </Columns>
                    <EmptyDataTemplate>
                        <label style="color: Red">
                            No se encontro ninguna coincidencia</label>
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="#FFFFCC" />
                    <HeaderStyle BackColor="#6699FF" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="txtelegido" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
