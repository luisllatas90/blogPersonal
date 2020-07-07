<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReporteAlumnosRetirados.aspx.vb" Inherits="Biblioteca_ReporteAlumnosRetirados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../private/estilo.css" type="text/css" rel="Stylesheet" />    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>
                    Semeste:
                    <asp:DropDownList ID="cboCicloAcad" runat="server">
                    </asp:DropDownList>
&nbsp;
                    <asp:Button ID="cmdConsultar" runat="server" CssClass="buscar" Text="Consultar" 
                        Width="80px" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="numero_Sol" HeaderText="Número Solicitud" 
                                SortExpression="numero_Sol" />
                            <asp:BoundField DataField="fecha_Sol" DataFormatString="{0:dd/MM/yyyy}" 
                                HeaderText="Fecha Solicitud" SortExpression="fecha_Sol" />
                            <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Cod. Universitario" 
                                SortExpression="codigoUniver_Alu" />
                            <asp:BoundField DataField="alumno" HeaderText="Alumno" 
                                SortExpression="alumno" />
                            <asp:BoundField DataField="nombre_Cpf" HeaderText="Carrera Profesional" 
                                SortExpression="nombre_Cpf" />
                            <asp:BoundField DataField="Tipo_Asunto" HeaderText="Tipo Asunto" 
                                ReadOnly="True" SortExpression="Column1" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado Evaluación" 
                                ReadOnly="True" SortExpression="Estado" />
                            <asp:BoundField DataField="estado_Sol" HeaderText="Estado Solicitud" 
                                SortExpression="estado_Sol" />
                        </Columns>
                        <HeaderStyle BackColor="#3366CC" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
