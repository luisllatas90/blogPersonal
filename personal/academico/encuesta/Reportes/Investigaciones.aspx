<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Investigaciones.aspx.vb" Inherits="Encuesta_Reportes_Investigaciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../../../private/funciones.js" type ="text/javascript" language="javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td class="titulocel" colspan="3">
                    &nbsp;Acreditación Universitaria - Investigación &nbsp;
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td bgcolor="#333333" height="1px" colspan="3">
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
                        DataSourceID="SqlDataSource1" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="codigo_aun" HeaderText="Codigo" 
                                SortExpression="codigo_aun" Visible="False" />
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres" ReadOnly="True" 
                                SortExpression="Nombres" />
                            <asp:BoundField DataField="Participó en Inv." HeaderText="Participó en Inv." 
                                SortExpression="Participó en Inv." ReadOnly="True" />
                            <asp:BoundField DataField="Num. Proyectos" HeaderText="Num. Proyectos" 
                                SortExpression="Num. Proyectos" ReadOnly="True" />
                            <asp:BoundField DataField="Título Inv." HeaderText="Título Inv." 
                                SortExpression="Título Inv." ReadOnly="True" />
                            <asp:BoundField DataField="Modo Participación" HeaderText="Modo Participación" 
                                SortExpression="Modo Participación" ReadOnly="True" />
                            <asp:BoundField DataField="Año" HeaderText="Año" 
                                SortExpression="Año" ReadOnly="True" />
                            <asp:BoundField DataField="Duración" HeaderText="Duración" 
                                SortExpression="Duración" ReadOnly="True" />
                            <asp:BoundField DataField="Quien Financió" HeaderText="Quien Financió" 
                                SortExpression="Quien Financió" ReadOnly="True" />
                            <asp:BoundField DataField="Medio de Ver." HeaderText="Medio de Ver." ReadOnly="True" 
                                SortExpression="Medio de Ver." />
                            <asp:BoundField DataField="Satisfecho Eval" HeaderText="Satisfecho Eval" 
                                SortExpression="Satisfecho Eval" ReadOnly="True" />
                            <asp:BoundField DataField="Participó E. Difusión" HeaderText="Participó E. Difusión" 
                                SortExpression="Participó E. Difusión" ReadOnly="True" />
                            <asp:BoundField DataField="Proyecto Difusión" HeaderText="Proyecto Difusión" 
                                SortExpression="Proyecto Difusión" />
                            <asp:BoundField DataField="Autores Difusión" HeaderText="Autores Difusión" 
                                SortExpression="Autores Difusión" />
                            <asp:BoundField DataField="Participó E. Discusión" HeaderText="Participó E. Discusión" 
                                SortExpression="Participó E. Discusión" ReadOnly="True" />
                            <asp:BoundField DataField="Proyecto Discusión" HeaderText="Proyecto Discusión" 
                                SortExpression="Proyecto Discusión" />
                            <asp:BoundField DataField="Autores Discusión" HeaderText="Autores Discusión" 
                                SortExpression="Autores Discusión" />
                            <asp:BoundField DataField="Conoce Prop. Intelectual" 
                                HeaderText="Conoce Prop. Intelectual" SortExpression="Conoce Prop. Intelectual" 
                                ReadOnly="True" />
                            <asp:BoundField DataField="Propiedad Intelectual" 
                                HeaderText="Propiedad Intelectual" SortExpression="Propiedad Intelectual" />
                        </Columns>
                        <HeaderStyle BackColor="#336699" ForeColor="White" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="AUN_MatrizAcreditacionUniversitaria" 
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="INV" Name="tipo" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
