<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BusquedaAlumno.aspx.vb" Inherits="medicina_administrador_BusquedaAlumno" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <script type="text/javascript"  language="JavaScript" src="../../private/funciones.js"></script>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
        <script language="javascript" type="text/javascript" >
    function AsignarValores(v1, v2, v3, v4, v5, v6)
     {  
        //document.form1.CmdEvaluaciones.disabled=false;
        document.form1.CmdAsistencias.disabled=false;
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
            <td colspan="2" style="font-weight: bold;
                    font-size: small; font-family: Verdana;
                    ">
                Mostrar notas por estudiante</td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr >
            <td colspan="2">
                Busqueda por:                 <asp:DropDownList ID="DdlBusqueda" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="1">Codigo Universitario</asp:ListItem>
                    <asp:ListItem Value="2">Apellidos y Nombres</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:TextBox ID="TxtBusqueda" runat="server" Width="300px"></asp:TextBox>
&nbsp;
                <asp:Button ID="CmdBuscar" runat="server" CssClass="buscar" Height="22px" 
                    Text="Buscar" Width="85px" />
            </td>
        </tr>
        <tr >
            <td colspan="2">
                &nbsp;&nbsp;&nbsp; 
                <hr />
            </td>
        </tr>
        <tr >
            <td colspan="2">
                <span style="width:90%; text-align:left; display: none;" id="mensajedetalle" 
                    class="usatsugerencia">&&nbsp; &nbsp;&nbsp;&nbsp;Seleccione un curso para realizar una operacion.</span></td>
        </tr>
        <tr >
            <td colspan="2" align="center" height="220" valign="top" 
                style="border: 1px solid #C0C0C0">
                <asp:Panel ID="Panel1" runat="server" Height="220px" HorizontalAlign="Left" 
                    ScrollBars="Vertical">
                    <asp:GridView ID="GridAlumnos" runat="server" 
                    AutoGenerateColumns="False" DataKeyNames="codigo_alu" 
    Width="100%">
                        <Columns>
                            <asp:BoundField DataField="codigo_alu" HeaderText="codigo_alu" 
                            InsertVisible="False" ReadOnly="True" SortExpression="codigo_alu" 
                            Visible="False" />
                            <asp:BoundField DataField="codigoUniver_alu" HeaderText="Cod. Universitario" 
                            SortExpression="codigoUniver_alu">
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="alumno" HeaderText="Apellidos y Nombres" 
                            ReadOnly="True" SortExpression="alumno">
                                <ItemStyle HorizontalAlign="Left" Width="500px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_cpf" HeaderText="Carrera Profesional" />
                            <asp:BoundField DataField="estadoActual_Alu" HeaderText="Estado">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="estadoDeuda_Alu" HeaderText="Tiene Deuda">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:CommandField SelectText="" ShowSelectButton="True" />
                        </Columns>
                        <EmptyDataTemplate>
                            <label style="color:Red">
                            No se encontro ninguna coincidencia</label>
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#FFFFCC" />
                        <HeaderStyle BackColor="#6699FF" ForeColor="White" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr >
            <td>
        <asp:HiddenField ID="txtelegido" runat="server" /> 
            </td>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr >
            <td colspan="2">
                <asp:Panel ID="pnlDetalle" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td colspan="2" style="font-weight: bold;
                    font-size:10pt; color:firebrick; font-family: verdana;
                    height:21px;  " >
                                Detalle del Alumno</td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2" class="textoMed">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                <b>Ciclo académico:</b>
                                <asp:DropDownList ID="DdlCicloAcad" runat="server" AutoPostBack="True" 
                    Enabled="true">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:Button ID="CmdConsolidado" runat="server" Text="Ver consolidado" CssClass="modificar" 
                    Font-Size="Smaller" Height="20px" Width="125px" />
                                &nbsp;<asp:Button ID="CmdAsistencias" runat="server" Text="Asistencias y Notas" CssClass="aprobar" 
                    Font-Size="Smaller" Height="20px" Width="125px" />
                            </td>
                        </tr>
                        <tr>
                            <td width="300">
                                <b>Código Universitario:</b>&nbsp;
                                <asp:Label ID="LblCodUniv" runat="server"></asp:Label>
                            </td>
                            <td>
                                <b>Apellidos y Nombres: </b>
                                <asp:Label ID="LblNombres" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr >
                            <td colspan="2" class="textoMed" >
                                <span style="width:90%; text-align:left; display: none;" id="mensajedetalle0" 
                    class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Seleccione un curso para realizar una operacion.</span></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" height="150">
                                <asp:GridView ID="GridBoleta" runat="server" AutoGenerateColumns="False" 
                    GridLines="Horizontal" Width="100%" DataKeyNames="codigo_cup,codigo_syl,codigo_dma">
                                    <Columns>
                                        <asp:BoundField DataField="identificador_cur" HeaderText="Código" 
                            SortExpression="identificador_cur">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre_cur" HeaderText="Curso" 
                            SortExpression="nombre_cur">
                                            <ItemStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="profesor" HeaderText="Profesor" />
                                        <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo" 
                            SortExpression="grupohor_cup">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cantAsis" HeaderText="Asistencias" ReadOnly="True" 
                            SortExpression="cantAsis">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cantTard" HeaderText="Tardanzas" ReadOnly="True" 
                            SortExpression="cantTard">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cantFalt" HeaderText="Faltas" ReadOnly="True" 
                            SortExpression="cantFalt">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:CommandField SelectText="" ShowSelectButton="True" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Size="10pt" 
                            ForeColor="Red" Text="No se encontraron registros"></asp:Label>
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle BackColor="#EAEAEA" />
                                    <HeaderStyle BackColor="#C0CEFE" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
