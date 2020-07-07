<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DatosGenerales.aspx.vb" Inherits="Encuesta_Reportes_DatosGenerales" %>

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
                <td class="titulocel">
                    &nbsp;Acreditación Universitaria - Datos Generales &nbsp;
                    </td>
            </tr>
            <tr>
                <td bgcolor="#333333" height="1px">
                     </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<asp:Label ID="LblTotal" runat="server" ForeColor="#CC3300"></asp:Label>
&nbsp;<asp:Button ID="CmdExportar" runat="server" CssClass="ExportarAExcel" 
                        Text="     Exportar" Height="22px" Width="72px" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GvDatosGenerales" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="SqlDataSource1" Width="100%" DataKeyNames="codigo_aun" 
                        AllowPaging="True" PageSize="1000">
                        <Columns>
                            <asp:BoundField DataField="codigo_aun" HeaderText="codigo_aun" 
                                InsertVisible="False" ReadOnly="True" SortExpression="codigo_aun" 
                                Visible="False" />
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres" ReadOnly="True" 
                                SortExpression="Nombres" />
                            <asp:BoundField DataField="Fecha Nac" HeaderText="Fecha Nac" 
                                SortExpression="Fecha Nac" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Departamento Nac" HeaderText="Departamento Nac" 
                                SortExpression="Departamento Nac" />
                            <asp:BoundField DataField="Provincia Nac" HeaderText="Provincia Nac" 
                                SortExpression="Provincia Nac" />
                            <asp:BoundField DataField="Distrito Nac" HeaderText="Distrito Nac" 
                                SortExpression="Distrito Nac" />
                            <asp:BoundField DataField="Pais Proc" HeaderText="Pais Proc" 
                                SortExpression="Pais Proc" />
                            <asp:BoundField DataField="Colegio" HeaderText="Colegio" 
                                SortExpression="Colegio" />
                            <asp:BoundField DataField="Ubicacion Col" HeaderText="Ubicacion Col" 
                                SortExpression="Ubicacion Col" />
                            <asp:BoundField DataField="Tipo Col" HeaderText="Tipo Col" 
                                SortExpression="Tipo Col" />
                            <asp:BoundField DataField="Ciclo Ing" HeaderText="Semestre Ing" 
                                SortExpression="Ciclo Ing" />
                            <asp:BoundField DataField="Carrera Ingreso" HeaderText="Carrera Ingreso" 
                                SortExpression="Carrera Ingreso" />
                            <asp:BoundField DataField="Carrera Actual" HeaderText="Carrera Actual" 
                                SortExpression="Carrera Actual" />
                            <asp:BoundField DataField="Tipo Ingreso" HeaderText="Tipo Ingreso" 
                                SortExpression="Tipo Ingreso" />
                            <asp:BoundField DataField="Origen" HeaderText="Origen" 
                                SortExpression="Origen" />
                            <asp:BoundField DataField="Por Convenio" HeaderText="Por Convenio" 
                                SortExpression="Por Convenio" />
                            <asp:BoundField DataField="Modalidad Est" HeaderText="Modalidad Est" 
                                SortExpression="Modalidad Est" />
                            <asp:BoundField DataField="Edad" HeaderText="Edad" SortExpression="Edad" />
                            <asp:BoundField DataField="Sexo" HeaderText="Sexo" SortExpression="Sexo" />
                            <asp:CheckBoxField DataField="Preparación Casa" HeaderText="Preparación Casa" 
                                SortExpression="Preparación Casa" />
                            <asp:CheckBoxField DataField="Preparación Acad" HeaderText="Preparación Acad" 
                                SortExpression="Preparación Acad" />
                            <asp:BoundField DataField="Academia" HeaderText="Academia" 
                                SortExpression="Academia" />
                            <asp:CheckBoxField DataField="Preparación Escuela Pre" 
                                HeaderText="Preparación Escuela Pre" SortExpression="Preparación Escuela Pre" />
                            <asp:BoundField DataField="Escuela Pre" HeaderText="Escuela Pre" 
                                SortExpression="Escuela Pre" />
                            <asp:BoundField DataField="Habla Ingles" HeaderText="Habla Ingles" 
                                SortExpression="Habla Ingles" />
                            <asp:BoundField DataField="Lee Ingles" HeaderText="Lee Inglés" 
                                SortExpression="Lee Ingles" />
                            <asp:BoundField DataField="Escribe Ingles" HeaderText="Escribe Inglés" 
                                SortExpression="Escribe Ingles" />
                            <asp:BoundField DataField="Institucion Ingles" HeaderText="Institución Inglés" 
                                SortExpression="Institucion Ingles" />
                            <asp:BoundField DataField="Certificacion Ingles" 
                                HeaderText="Certificacion Ingles" SortExpression="Certificación Inglés" />
                            <asp:BoundField DataField="Institución Cert. Ingles" 
                                HeaderText="Institución Cert. Ingles" 
                                SortExpression="Institución Cert. Ingles" />
                            <asp:BoundField DataField="Habla Frances" HeaderText="Habla Francés" 
                                SortExpression="Habla Frances" />
                            <asp:BoundField DataField="Lee Frances" HeaderText="Lee Francés" 
                                SortExpression="Lee Frances" />
                            <asp:BoundField DataField="Escribe Frances" HeaderText="Escribe Francés" 
                                SortExpression="Escribe Frances" />
                            <asp:BoundField DataField="Institución Frances" 
                                HeaderText="Institución Frances" SortExpression="Institución Francés" />
                            <asp:BoundField DataField="Certificación Frances" 
                                HeaderText="Certificación Frances" SortExpression="Certificación Francés" />
                            <asp:BoundField DataField="Institución Cert. Frances" 
                                HeaderText="Institución Cert. Frances" 
                                SortExpression="Institución Cert. Frances" />
                            <asp:BoundField DataField="Habla Italiano" HeaderText="Habla Italiano" 
                                SortExpression="Habla Italiano" />
                            <asp:BoundField DataField="Lee Italiano" HeaderText="Lee Italiano" 
                                SortExpression="Lee Italiano" />
                            <asp:BoundField DataField="Escribe Italiano" HeaderText="Escribe Italiano" 
                                SortExpression="Escribe Italiano" />
                            <asp:BoundField DataField="Institución Italiano" 
                                HeaderText="Institución Italiano" SortExpression="Institución Italiano" />
                            <asp:BoundField DataField="Certificación Italiano" 
                                HeaderText="Certificación Italiano" SortExpression="Certificación Italiano" />
                            <asp:BoundField DataField="Institución Cert. Italiano" 
                                HeaderText="Institución Cert. Italiano" 
                                SortExpression="Institución Cert. Italiano" />
                            <asp:BoundField DataField="Otro Idioma Habla" HeaderText="Otro Idioma Habla" 
                                SortExpression="Otro Idioma Habla" />
                            <asp:BoundField DataField="Otro Idioma Lee" HeaderText="Otro Idioma Lee" 
                                SortExpression="Otro Idioma Lee" />
                            <asp:BoundField DataField="Otro Idioma Escribe" 
                                HeaderText="Otro Idioma Escribe" SortExpression="Otro Idioma Escribe" />
                            <asp:BoundField DataField="Institución Otro Idioma" 
                                HeaderText="Institución Otro Idioma" SortExpression="Institución Otro Idioma" />
                            <asp:BoundField DataField="Certificación Otro Idioma" 
                                HeaderText="Certificación Otro Idioma" 
                                SortExpression="Certificación Otro Idioma" />
                            <asp:BoundField DataField="Institución Cert. Otro Idioma" 
                                HeaderText="Institución Cert. Otro Idioma" 
                                SortExpression="Institución Cert. Otro Idioma" />
                            <asp:BoundField DataField="Es Discapacitado" HeaderText="Es Discapacitado" 
                                SortExpression="Es Discapacitado" />
                            <asp:BoundField DataField="Discapacidad" HeaderText="Discapacidad" 
                                SortExpression="Discapacidad" />
                            <asp:BoundField DataField="Tiene Seguro" HeaderText="Tiene Seguro" 
                                SortExpression="Tiene Seguro" />
                            <asp:BoundField DataField="Seguro" HeaderText="Seguro" 
                                SortExpression="Seguro" />
                            <asp:BoundField DataField="Religión que Profesa" 
                                HeaderText="Religión que Profesa" SortExpression="Religión que Profesa" />
                            <asp:BoundField DataField="Religión" HeaderText="Religión" 
                                SortExpression="Religión" />
                            <asp:CheckBoxField DataField="Bautizado" HeaderText="Bautizado" 
                                SortExpression="Bautizado" />
                            <asp:CheckBoxField DataField="Reconciliación" HeaderText="Reconciliación" 
                                SortExpression="Reconciliación" />
                            <asp:CheckBoxField DataField="Eucaristía" HeaderText="Eucaristía" 
                                SortExpression="Eucaristía" />
                            <asp:CheckBoxField DataField="Confirmación" HeaderText="Confirmación" 
                                SortExpression="Confirmación" />
                            <asp:CheckBoxField DataField="Unción de los Enfermos" 
                                HeaderText="Unción de los Enfermos" SortExpression="Unción de los Enfermos" />
                            <asp:CheckBoxField DataField="Matrimonio" HeaderText="Matrimonio" 
                                SortExpression="Matrimonio" />
                            <asp:CheckBoxField DataField="Orden Sacerdotal" HeaderText="Orden Sacerdotal" 
                                SortExpression="Orden Sacerdotal" />
                            <asp:BoundField DataField="Frec. Reconciliacion" 
                                HeaderText="Frec. Reconciliacion" SortExpression="Frec. Reconciliación" />
                            <asp:BoundField DataField="Frec. Eucaristia" HeaderText="Frec. Eucaristia" 
                                SortExpression="Frec. Eucaristia" />
                            <asp:BoundField DataField="Participa Grupo Parr." 
                                HeaderText="Participa Grupo Parr." SortExpression="Participa Grupo Parr." />
                            <asp:BoundField DataField="Grupo Parroquial" HeaderText="Grupo Parroquial" 
                                SortExpression="Grupo Parroquial" />
                            <asp:BoundField DataField="Estado Civil" HeaderText="Estado Civil" 
                                SortExpression="Estado Civil" />
                            <asp:BoundField DataField="Tiene Hijos" HeaderText="Tiene Hijos" 
                                SortExpression="Tiene Hijos" />
                            <asp:BoundField DataField="Num. Hijos" HeaderText="Num. Hijos" ReadOnly="True" 
                                SortExpression="Num. Hijos" />
                            <asp:BoundField DataField="Hijo 1" HeaderText="Hijo 1" 
                                SortExpression="Hijo 1" />
                            <asp:BoundField DataField="Sexo H1" HeaderText="Sexo H1" 
                                SortExpression="Sexo H1" />
                            <asp:BoundField DataField="Fecha Nac H1" HeaderText="Fecha Nac H1" 
                                SortExpression="Fecha Nac H1" />
                            <asp:BoundField DataField="Hijo 2" HeaderText="Hijo 2" 
                                SortExpression="Hijo 2" />
                            <asp:BoundField DataField="Sexo H2" HeaderText="Sexo H2" 
                                SortExpression="Sexo H2" />
                            <asp:BoundField DataField="Fecha Nac H2" HeaderText="Fecha Nac H2" 
                                SortExpression="Fecha Nac H2" />
                            <asp:BoundField DataField="Hijo 3" HeaderText="Hijo 3" 
                                SortExpression="Hijo 3" />
                            <asp:BoundField DataField="Sexo H3" HeaderText="Sexo H3" 
                                SortExpression="Sexo H3" />
                            <asp:BoundField DataField="Fecha Nac H3" HeaderText="Fecha Nac H3" 
                                SortExpression="Fecha Nac H3" />
                            <asp:BoundField DataField="Hijo 4" HeaderText="Hijo 4" 
                                SortExpression="Hijo 4" />
                            <asp:BoundField DataField="Sexo H4" HeaderText="Sexo H4" 
                                SortExpression="Sexo H4" />
                            <asp:BoundField DataField="Fecha Nac H4" HeaderText="Fecha Nac H4" 
                                SortExpression="Fecha Nac H4" />
                            <asp:BoundField DataField="Hijo 5" HeaderText="Hijo 5" 
                                SortExpression="Hijo 5" />
                            <asp:BoundField DataField="Sexo H5" HeaderText="Sexo H5" 
                                SortExpression="Sexo H5" />
                            <asp:BoundField DataField="Fecha Nac H5" HeaderText="Fecha Nac H5" 
                                SortExpression="Fecha Nac H5" />
                        </Columns>
                        <HeaderStyle BackColor="#336699" ForeColor="White" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="AUN_MatrizAcreditacionUniversitaria" 
                        SelectCommandType="StoredProcedure" ProviderName="System.Data.SqlClient">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="DG" Name="tipo" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
