<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReporteUtilizacionNotasAsistencias.aspx.vb" Inherits="personal_academico_expediente_medicina_reportes_ReporteUtilizacionNotasAsistencias" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

        .style1
        {
            color: blue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="cmdExportar" />
        </Triggers>       
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td>
                        Ciclo Académico&nbsp;&nbsp;&nbsp; :
                        <asp:DropDownList ID="cboCicloAcad" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Escuela Profesional:
                        <asp:DropDownList ID="cboEscuela" 
                        runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="cmdConsultar" runat="server" Text="Consultar" />
                        &nbsp;<asp:Button ID="cmdExportar" runat="server" Text="Exportar" />
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                            AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <span class="style1">Espere un momento... la consulta se 
                    esta procesando<img alt="" src="../../../../../librerianet/images/loading.gif" /> </span>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvNotas" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="nombre_Cpf" HeaderText="Carrera" />
                                <asp:BoundField DataField="Personal" HeaderText="Personal" />
                                <asp:BoundField DataField="nombre_Cur" HeaderText="Curso" />
                                <asp:BoundField DataField="grupoHor_Cup" HeaderText="G.H." />
                                <asp:BoundField DataField="Curso_Activo" HeaderText="Estado activación" />
                                <asp:BoundField DataField="Actividad_reg" HeaderText="Nro Act. Registradas" />
                                <asp:BoundField DataField="Actividad_real" HeaderText="Nro. Act. Realizadas" />
                                <asp:BoundField DataField="fechaUltimaNota" DataFormatString="{0:dd/MM/yyyy}" 
                                HeaderText="Fecha Últ. &lt;br&gt;Nota Registrada" HtmlEncode="False" />
                                <asp:BoundField DataField="NroNotas" HeaderText="Nro. Notas" />
                                <asp:BoundField DataField="FechaMaxActividad" DataFormatString="{0:dd/MM/yyyy}" 
                                HeaderText="Fecha Últ. &lt;br&gt;Actividad Registrada" HtmlEncode="False" />
                                <asp:BoundField DataField="fechaUltimaAsist" DataFormatString="{0:dd/MM/yyyy}" 
                                HeaderText="Fecha Últ. &lt;br&gt;Asistencia" HtmlEncode="False" />
                                <asp:BoundField DataField="NroAsistencias" HeaderText="Nro. Asistencias" />
                            </Columns>
                            <HeaderStyle BackColor="#FF9900" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
