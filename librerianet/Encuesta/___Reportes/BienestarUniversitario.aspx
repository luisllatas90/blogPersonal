<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BienestarUniversitario.aspx.vb" Inherits="Encuesta_Reportes_BienestarUniversitario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language="javascript"></script>
    <style type="text/css">
        .style1
        {
            height: 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td class="titulocel" colspan="3">
                    &nbsp;Acreditación Universitaria - Bienestar Universitario &nbsp;
&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td bgcolor="#333333" colspan="3" class="style1">
                     </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<asp:Label ID="LblTotal" runat="server" ForeColor="#CC3300"></asp:Label>
&nbsp;<asp:Button ID="CmdExportar" runat="server" CssClass="ExportarAExcel" 
                        Text="     Exportar" Height="22px" Width="72px" />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="GvInvestigacion" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="SqlDataSource1" DataKeyNames="codigo_aun" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="codigo_aun" HeaderText="Código" 
                                SortExpression="codigo_aun" InsertVisible="False" ReadOnly="True" 
                                Visible="False" />
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres" ReadOnly="True" 
                                SortExpression="Nombres" />
                            <asp:BoundField DataField="Conoce P. Bienestar" HeaderText="Conoce P. Bienestar" 
                                SortExpression="Conoce P. Bienestar" ReadOnly="True" />
                            <asp:BoundField DataField="Prog. Bienestar" HeaderText="Prog. Bienestar" 
                                SortExpression="Prog. Bienestar" />
                            <asp:BoundField DataField="Atención Medica" HeaderText="Atención Medica" 
                                SortExpression="Atención Medica" ReadOnly="True" />
                            <asp:BoundField DataField="Psicología" HeaderText="Psicología" 
                                SortExpression="Psicología" ReadOnly="True" />
                            <asp:BoundField DataField="Pedagogía" HeaderText="Pedagogía" 
                                SortExpression="Pedagogía" ReadOnly="True" />
                            <asp:BoundField DataField="Asistencia Social" HeaderText="Asistencia Social" 
                                SortExpression="Asistencia Social" ReadOnly="True" />
                            <asp:BoundField DataField="Deportes" HeaderText="Deportes" 
                                SortExpression="Deportes" ReadOnly="True" />
                            <asp:BoundField DataField="Culturales" HeaderText="Culturales" ReadOnly="True" 
                                SortExpression="Culturales" />
                            <asp:BoundField DataField="Biblioteca" HeaderText="Biblioteca" 
                                SortExpression="Biblioteca" ReadOnly="True" />
                            <asp:BoundField DataField="Biblioteca Virtual" HeaderText="Biblioteca Virtual" 
                                SortExpression="Biblioteca Virtual" ReadOnly="True" />
                        </Columns>
                        <HeaderStyle BackColor="#336699" ForeColor="White" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="AUN_MatrizAcreditacionUniversitaria" 
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="BU" Name="tipo" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
