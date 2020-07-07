<%@ Page Language="VB" AutoEventWireup="false" CodeFile="administrar.aspx.vb" Inherits="medicina_administrador_administrar" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <script type="text/javascript"  language="JavaScript" src="../../../../../private/funciones.js"></script>
    <link href="../../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript"> 
        function ActivaControles(fila)
            {   //document.form1.CmdActividades.disabled=false;
                //document.form1.CmdEvaluaciones.disabled=false;
                //document.form1.CmdAsistencia.disabled=false;
                //document.form1.CmdObservaciones.disabled=false;
                //document.form1.CmdRegNotas.disabled=false;
                document.form1.CmdProgramacion.disabled=false;
                document.form1.CmdEvaluacion.disabled=false;
                document.form1.txtcurso.value = fila.cells(2).innerText + ' '  + '(Grupo ' + fila.cells(3).innerText + ')'
                document.form1.txtprofesor.value = fila.cells(6).innerText 
            }
    </script>
</head>
<body style="margin:0,0,0,0"> 
    <form id="form1" runat="server">
    
        
        <table style="height: 11%" width="100%">
            <tr>
                <td align="center" colspan="4" style="border-top: black 1px solid; font-weight: bold;
                    font-size: 11pt; color: white; border-bottom: black 1px solid; font-family: verdana;
                    height: 21px; background-color: firebrick; text-align: center">
                    Panel del Administrador</td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="1" style="border-top: #660000 1px solid; font-weight: bold; font-size: 8pt;
                    text-transform: capitalize; color: #003300; font-family: verdana; font-variant: normal; height: 22px;">
                    &nbsp;&nbsp; Ciclo Academico</td>
                <td colspan="3" style="border-top: #660000 1px solid; font-weight: bold; font-size: 8pt;
                    text-transform: capitalize; color: #003300; font-family: verdana; font-variant: normal; height: 22px;">
                    :
                    <asp:DropDownList ID="DDLCiclo" runat="server" AutoPostBack="True">
                    </asp:DropDownList></td>
            </tr>
            <tr style="font-size: 8pt">
                <td colspan="1" style="font-weight: bold; font-size: 8pt; text-transform: capitalize;
                    color: #003300; font-family: verdana; font-variant: normal;">
                    &nbsp;&nbsp; Escuela Profesional</td>
                <td colspan="3" style="font-weight: bold; font-size: 8pt; text-transform: capitalize;
                    color: #003300; font-family: verdana; font-variant: normal">
                    :
                    <asp:DropDownList ID="DDLEscuela" runat="server" AutoPostBack="True">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="4" style="border-top: #660000 1px solid; font-weight: bold; font-size: 8pt;
                    text-transform: capitalize; color: #003300; font-family: verdana; height: 28px;
                    font-variant: normal">
                    &nbsp; &nbsp;&nbsp; &nbsp;<asp:Button ID="CmdActividades" runat="server" 
                        BackColor="FloralWhite" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" Text="Registrar Actividades"
                        Width="145px" Enabled="False" Visible="False" />
                    <asp:Button ID="CmdEvaluaciones" runat="server" BackColor="FloralWhite" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" Text="Registrar Evaluaciones"
                        Width="141px" Enabled="False" Visible="False" />
                    <asp:Button ID="CmdAsistencia" runat="server" BackColor="FloralWhite" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" Text="Registro de Asistencias"
                        Width="141px" Enabled="False" Visible="False" />
                    <asp:Button ID="CmdObservaciones" runat="server" BackColor="FloralWhite" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" Text="Registro de Observaciones"
                        Width="141px" Enabled="False" Visible="False" />
                    <asp:Button ID="CmdRegNotas" runat="server" BackColor="FloralWhite" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" Text="Registro de Notas"
                        Width="141px" Enabled="False" Visible="False" />
                    <asp:Button ID="CmdProgramacion" runat="server" BackColor="FloralWhite" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" Text="Ver Asistencia"
                        Width="141px" Enabled="False" />
                    <asp:Button ID="CmdEvaluacion" runat="server" BackColor="FloralWhite" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" Text="Ver Notas"
                        Width="141px" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4" valign="top">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4" valign="top">
                <span style="width:90%; text-align:left" id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Seleccione un curso para realizar una operacion.</span>
                    <asp:GridView ID="GridCursos" runat="server" AutoGenerateColumns="False" CellPadding="1"
                        DataSourceID="Cursos" GridLines="None" Width="90%" BorderColor="Black" 
                        BorderStyle="Solid" BorderWidth="1px" EnableViewState="False">
                        <Columns>
                            <asp:BoundField DataField="codigo_cup" HeaderText="codigo_cup" SortExpression="codigo_cup"
                                Visible="False" />
                            <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" SortExpression="ciclo_cur">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificador_cur" HeaderText="Codigo" SortExpression="identificador_cur">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_cur" HeaderText="Curso" HtmlEncode="False" ReadOnly="True"
                                SortExpression="nombre_cur" />
                            <asp:BoundField DataField="grupohor_cup" HeaderText="G.H." SortExpression="grupohor_cup">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="creditos_cur" HeaderText="Cred." SortExpression="creditos_cur">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="totalhoras_cur" HeaderText="T.H." SortExpression="totalhoras_cur">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="profesor" HeaderText="Profesor" ReadOnly="True" SortExpression="profesor" />
                            <asp:BoundField DataField="matriculados" HeaderText="Mat." ReadOnly="True" SortExpression="matriculados">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Estado">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle Font-Names="Verdana" Font-Size="8pt" Height="28px" />
                        <HeaderStyle BackColor="#DFDBA4" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"
                            Height="25px" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="Cursos" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>"
                        SelectCommand="ConsultarHorarios" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="10" Name="tipo" Type="String" />
                            <asp:ControlParameter ControlID="DDLCiclo" DefaultValue="33" Name="param1" PropertyName="SelectedValue"
                                Type="String" />
                            <asp:ControlParameter ControlID="DDLEscuela" DefaultValue="24" Name="param2" PropertyName="SelectedValue"
                                Type="String" />
                            <asp:Parameter DefaultValue="0" Name="param3" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    &nbsp; &nbsp;
                    &nbsp; &nbsp;
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="txtelegido" runat="server" />      <!-- Codigo_cup del curso -->
        <asp:HiddenField ID="txtcurso" runat="server" />        <!-- Curso que se ha seleccionado -->     
        <asp:HiddenField ID="txtprofesor" runat="server" />     <!-- Profesor del curso que se ha seleccionado -->
        <asp:HiddenField ID="txtMenu" runat="server" />
   
   <iframe name="fradetalle" style="visibility:hidden; width: 142px; height: 19px;"></iframe>
    </form>
</body>
</html>
