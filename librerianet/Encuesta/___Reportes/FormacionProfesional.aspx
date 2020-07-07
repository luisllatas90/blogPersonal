<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FormacionProfesional.aspx.vb" Inherits="Encuesta_Reportes_FormacionProfesional" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language="javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td class="titulocel">
                    &nbsp;Acreditación Universitaria - Formación Profesional</td>
            </tr>
            <tr>
                <td bgcolor="#333333" height="1px" >
                     </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblTotal" runat="server" ForeColor="#CC3300"></asp:Label>
&nbsp;<asp:Button ID="CmdExportar" runat="server" CssClass="ExportarAExcel" 
                        Text="     Exportar" Height="22px" Width="72px" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GvFormacion" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="SqlDataSource1" DataKeyNames="codigo_aun" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="codigo_aun" HeaderText="Codigo" 
                                InsertVisible="False" ReadOnly="True" SortExpression="codigo_aun" 
                                Visible="False" />
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres" ReadOnly="True" 
                                SortExpression="Nombres" />
                            <asp:BoundField DataField="Est. Enseñanza" HeaderText="Est. Enseñanza" ReadOnly="True" 
                                SortExpression="Est. Enseñanza" />
                            <asp:BoundField DataField="Est. Capacidad" HeaderText="Est. Capacidad" ReadOnly="True" 
                                SortExpression="Est. Capacidad" />
                            <asp:BoundField DataField="Silabus" HeaderText="Silabus" 
                                SortExpression="Silabus" ReadOnly="True" />
                            <asp:BoundField DataField="Eval. Aprendizaje" HeaderText="Eval. Aprendizaje" 
                                SortExpression="Eval. Aprendizaje" ReadOnly="True" />
                            <asp:BoundField DataField="Beneficios" HeaderText="Beneficios" 
                                SortExpression="Beneficios" ReadOnly="True" />
                            <asp:BoundField DataField="Ayuda" HeaderText="Ayuda" 
                                SortExpression="Ayuda" ReadOnly="True" />
                            <asp:BoundField DataField="Tutoria" HeaderText="Tutoria" 
                                SortExpression="Tutoria" ReadOnly="True" />
                        </Columns>
                        <HeaderStyle BackColor="#336699" ForeColor="White" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="AUN_MatrizAcreditacionUniversitaria" 
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="FP" Name="tipo" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
