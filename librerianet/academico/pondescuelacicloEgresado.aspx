<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pondescuelacicloEgresado.aspx.vb" Inherits="pondescuelaciclo" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consultar Promedio Ponderado</title>
        <link href="../../private/estilo.css" rel="stylesheet" type="text/css">
        <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <style type="text/css">
        span
        {
            cursor: pointer
        }
        
        a:Link
        {
        	color: #000000;
	        text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usattitulo">Cuadro de Méritos Estudiante por Escuela Profesional</p>
    <table cellpadding="3" cellspacing="0" style="width:100%;" id="tblCriterios">
        <tr>
            <td height="5%" width="20%">Escuela Profesional</td>
            <td height="5%" width="80%">
                <asp:DropDownList ID="dpEscuela" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="5%" width="20%">Semestre Egreso:</td>
            <td height="5%" width="80%">
                &nbsp;&nbsp;
                <asp:DropDownList ID="dpCiclo" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            &nbsp;&nbsp;Según:
                <asp:DropDownList ID="dpFiltro" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="C">Cuadro Completo</asp:ListItem>
                    <asp:ListItem Value="T">Tercio Superior</asp:ListItem>
                    <asp:ListItem Value="Q">Quinto Superior</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="5%" width="20%">&nbsp;</td>
            <td height="5%" width="80%">
                <asp:Button ID="cmdExportar" runat="server" CssClass="excel2" 
                    Text="  Exportar" 
                    onclientclick="document.all.tblmensaje.style.display='none'" />
            </td>
        </tr>
        <tr>
            <td height="5%" width="20%" colspan="2" style="width: 100%">
                <asp:Label ID="Label2" runat="server" ForeColor="Red" 
                    Text="- Para el cálculo del promedio se consideran los créditos efectivamente cursados, se excluyen: Convalidaciones, exámenes extraordinarios.  (Art. 43)"></asp:Label>
                <br />
                <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                    Text="- Se excluye del cuadro de mérito: Estudiantes que no se encuentran dentro del periodo regular de acuerdo al plan de estudios. (Art. 45)"></asp:Label>
                <br />
                <asp:Label ID="Label3" runat="server" ForeColor="Red" 
                    Text="- Para la ubicación del cuadro de mérito en el caso que dos estudiantes tengan igual promedio ponderado acumulado se preferirá aquel estudiante que no desaprobó ninguna asignatura. (Art. 45)"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="5%" width="100%" colspan="2">
                <!--<div id="listadiv" style="height:400px" class="contornotabla">-->
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" AllowSorting="True" 
                    BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" Width="100%" 
                    DataSourceID="objPonderado" DataKeyNames="codigo_alu">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField HeaderText="#">
                            <ItemStyle Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codigouniver_alu" HeaderText="Código" 
                            SortExpression="codigouniver_alu">
                            <HeaderStyle Font-Underline="True" />
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="alumno" HeaderText="Apellidos y Nombres" 
                            SortExpression="alumno">
                            <HeaderStyle Font-Underline="True" />
                            <ItemStyle Width="40%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Ciclo de Ingreso" DataField="cicloing_alu" 
                            SortExpression="cicloing_alu">
                            <HeaderStyle Font-Underline="True" />
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Ponderado" HeaderText="Ponderado Acumulado" 
                            SortExpression="Ponderado" DataFormatString="{0:F2}">
                            <HeaderStyle Font-Underline="True" />
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="totalCreditosMatriculados" 
                            HeaderText="Créd. Matriculados Acumulados" 
                            SortExpression="totalCreditosMatriculados">
                            <HeaderStyle Font-Underline="True" />
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PonderadoCiclo" HeaderText="Ponderado Semestre" 
                            SortExpression="PonderadoCiclo">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Consultas">
                            <ItemTemplate>
                                <asp:Label ID="lblHistorial" runat="server" Font-Underline="True" 
                                    ForeColor="Blue" Text="Historial"></asp:Label>
                                &nbsp;|
                                <asp:Label ID="lblBeca" runat="server" Font-Underline="True" ForeColor="Blue" 
                                    Text="Beneficio"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <p class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp; No se han encontrado registros con el criterio 
                            seleccionado</p>
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:ObjectDataSource ID="objPonderado" runat="server" 
                    SelectMethod="ConsultarPromedioOficialEgresado" TypeName="Academico">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="dpEscuela" DefaultValue="0" Name="param1" 
                            PropertyName="SelectedValue" Type="String" />
                        <asp:ControlParameter ControlID="dpCiclo" DefaultValue="0" Name="param3" 
                            PropertyName="SelectedValue" Type="String" />
                        <asp:ControlParameter ControlID="dpFiltro" DefaultValue="C" Name="param4" 
                            PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <!--</div>-->
            </td>
        </tr>
        </table>
    </form>
    <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="100%" height="100%" class="contornotabla">
	    <tr>
	    <td width="100%" align="center" class="usatTitulo" bgcolor="#FEFFE1">
	    Procesando<br>
	    Por favor espere un momento...<br>
	    <img border="0" src="../../images/cargando.gif" width="209" height="20">
	    </td>
	    </tr>
    </table>
</body>
</html>
