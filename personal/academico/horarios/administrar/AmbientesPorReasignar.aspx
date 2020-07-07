<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AmbientesPorReasignar.aspx.vb" Inherits="academico_horarios_administrar_AmbientesPorReasignar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
    <script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;" cellpadding="1" cellspacing="0">
            <tr>
                <td class="usatTitulo" colspan="2">
                    Reasignar Ambientes</td>
            </tr>
            <tr>
                <td colspan="2">
                    <b>Semestre Académico</b>&nbsp;
                <asp:DropDownList ID="dpCodigo_cac" runat="server">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator3" runat="server" 
                    ControlToValidate="dpCodigo_cac" 
                    ErrorMessage="Seleccione Ciclo Académico" 
                    Operator="GreaterThanEqual" ValueToCompare="0" ValidationGroup="Consultar">*</asp:CompareValidator>
                    &nbsp;&nbsp; <b>Tipo Ambiente</b>
                    <asp:DropDownList ID="dpTipoAmbiente" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <b>Departamento Académico:</b>
                <asp:DropDownList ID="dpCodigo_Dac" runat="server">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ControlToValidate="dpCodigo_Dac" 
                    ErrorMessage="Seleccione Departamento Académico" 
                    Operator="GreaterThanEqual" ValueToCompare="0" ValidationGroup="Consultar">*</asp:CompareValidator>
                    &nbsp;<asp:Button ID="cmdVer" runat="server" Text="Consultar" 
                        ValidationGroup="Consultar" style="width: 83px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="chkFiltrar" runat="server" Text="Filtrar por capacidad" />
                </td>
            </tr>
            <tr>
                <td height="250px" valign="top" style="border: 1px solid #C0C0C0" colspan="2">
                    <asp:Panel ID="Panel1" runat="server" Height="250px">
                        <asp:GridView ID="gvCursosProgramados" runat="server" 
                        AutoGenerateColumns="False" 
    DataKeyNames="codigo_cup,codigo_lho,dia_Lho,nombre_Hor,horaFin_Lho" Width="100%">
                            <Columns>
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
                                <asp:BoundField DataField="total_mat" HeaderText="Inscritos">
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
                                <asp:CommandField ShowSelectButton="True" />
                            </Columns>
                            <SelectedRowStyle BackColor="#CADBFF" />
                            <HeaderStyle BackColor="#0066CC" ForeColor="White" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="background-color: #E6F2FF">
                    <b>Ambientes disponibles:</b>                     <asp:DropDownList ID="dpAmbientes" 
                        runat="server" AutoPostBack="True">
                    </asp:DropDownList>
&nbsp;</td>
                <td style="background-color: #E6F2FF" align="right">
                    <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" />
&nbsp;&nbsp; </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:GridView ID="gvHorarioSemana" runat="server" Width="90%">
                        <HeaderStyle BackColor="#0469B5" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
