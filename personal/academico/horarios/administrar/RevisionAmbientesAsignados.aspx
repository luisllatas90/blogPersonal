<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RevisionAmbientesAsignados.aspx.vb" Inherits="academico_horarios_administrar_RevisionAmbientesAsignados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
    <script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
    <title>asignar ambientes</title>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td style="height: 30px; " class="usatTitulo">
                    Asignar ambientes reales</td>
            </tr>
            <tr>
                <td style="height: 30px; ">
                    <b>Tipo Ambiente
                    </b>
                    <asp:DropDownList ID="dpTipoAmbiente" runat="server">
                    </asp:DropDownList>
&nbsp; <b>Estado de ambiente</b>
                    <asp:DropDownList ID="dpEstado" runat="server">
                        <asp:ListItem Value="0">Todos</asp:ListItem>
                        <asp:ListItem Value="A">(A) Asignados</asp:ListItem>
                        <asp:ListItem Value="R">(R) Por Reasignar</asp:ListItem>
                        <asp:ListItem Selected="True" Value="P">(P) Pendientes</asp:ListItem>
                    </asp:DropDownList>
            </td>
            </tr>
            <tr>
                <td style="height: 30px; ">
                    <b>Semestre académico&nbsp;</b>
                <asp:DropDownList ID="dpCodigo_cac" runat="server">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator3" runat="server" 
                    ControlToValidate="dpCodigo_cac" 
                    ErrorMessage="Seleccione Ciclo Académico" 
                    Operator="GreaterThanEqual" ValueToCompare="0" ValidationGroup="Consultar">*</asp:CompareValidator>
                    &nbsp; <b>Departamento Académico</b>
                <asp:DropDownList ID="dpCodigo_Dac" runat="server">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ControlToValidate="dpCodigo_Dac" 
                    ErrorMessage="Seleccione Departamento Académico" 
                    Operator="GreaterThanEqual" ValueToCompare="0" ValidationGroup="Consultar">*</asp:CompareValidator>
                    &nbsp; <asp:Button ID="cmdVer" runat="server" Text="Consultar" 
                        ValidationGroup="Consultar" Width="75px" />
                </td>
            </tr>
            <tr>
                <td style="background-color: #0066CC; height:1px">
                </td>
            </tr>
            <tr style="border-color: #DDE8F7; background-color: #CCE6FF">
                <td>
                    <asp:Button ID="cmdAsignar" runat="server" Text="Asignar" Width="75px" />
                    <asp:Button ID="cmdReasignar" runat="server" Text="Reasignar" Width="75px" />
                    <asp:Button ID="cmdLiberar" runat="server" Text="Liberar" Width="75px" />
&nbsp;Marcar según
                    <asp:DropDownList ID="dpMarcarPor" runat="server" AutoPostBack="True">
                        <asp:ListItem Value="-1">--Seleccione--</asp:ListItem>
                        <asp:ListItem Value="0">Inscritos</asp:ListItem>
                        <asp:ListItem Value="1">Vacantes -&gt; Capacidad (%)</asp:ListItem>
                        <asp:ListItem Value="2">Inscritos -&gt; Vacantes -&gt; Capacidad (%)</asp:ListItem>
                        <asp:ListItem Value="3">Cursos primer ciclo</asp:ListItem>
                        <asp:ListItem Value="4">Todos</asp:ListItem>
                    </asp:DropDownList>
&nbsp;<asp:DropDownList ID="cboSigno" runat="server">
                        <asp:ListItem>=</asp:ListItem>
                        <asp:ListItem>&lt;=</asp:ListItem>
                        <asp:ListItem>&gt;=</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtCompararCon" runat="server" Width="46px"></asp:TextBox>
&nbsp;<asp:Button ID="cmdMarcar" runat="server" Text="Marcar" ValidationGroup="Marcar" 
                        Width="75px" />
                </td>
            </tr>
            <tr>
                 <td style="background-color: #0066CC; height:1px">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvCursosProgramados" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="codigo_cup,codigo_lho" 
                        Width="100%">
                        <Columns>
                            <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkElegir" runat="server" Checked='<%# Bind("Habilitar") %>' />
                            </EditItemTemplate>                                                            
                            <ItemTemplate>
                                <asp:CheckBox ID="chkElegir" runat="server" Checked='<%# Bind("Habilitar") %>' />
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="escuelaplan" HeaderText="Escuela Principal" 
                                HtmlEncode="False" />
                            <asp:BoundField DataField="nombre_Cur" HeaderText="Asignatura" />
                            <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="soloPrimerCiclo_cup" HeaderText="1er Ciclo">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vacantes_cup" HeaderText="Vacantes">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="total_mat" HeaderText="Matriculados">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="estado_cup" HeaderText="Estado Curso">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AmbienteReal" HeaderText="Ambiente" />
                            <asp:BoundField DataField="capacidad_Amb" HeaderText="Capacidad">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="horario" HeaderText="Horario">
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="estadoHorario_lho" HeaderText="Estado Ambiente">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/eliminar.gif" 
                                HeaderText="Liberar" ShowDeleteButton="True">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                        </Columns>
                        <HeaderStyle BackColor="#3366FF" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Consultar" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
        ValidationGroup="Marcar" />
    </form>
</body>
</html>
