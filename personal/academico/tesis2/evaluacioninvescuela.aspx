<%@ Page Language="VB" AutoEventWireup="false" CodeFile="evaluacioninvescuela.aspx.vb" Inherits="evaluacioninvescuela" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Evaluación de tesis</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Conformidad de evaluación de investigaciones<p/>
    <table width="100%" cellpadding="3">
        <tr>
            <td style="width:20%" align="right">
                Estado de Investigación:</td>
            <td style="width:80%">
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="0">Proceso</asp:ListItem>
                    <asp:ListItem Value="1">Finalizado</asp:ListItem>
                </asp:DropDownList>
                    </td>
        </tr>
        <tr>
            <td colspan="2" class="contornotabla_azul" bgcolor="#FFCC66">
                Listado de investigaciones</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="nro" HeaderText="#" />
                        <asp:BoundField DataField="fechareg" HeaderText="Fecha Registro" />
                        <asp:BoundField DataField="codigo" HeaderText="Código Inv." />
                        <asp:BoundField DataField="titulo" HeaderText="Titulo de investigación" />
                        <asp:BoundField DataField="autor" HeaderText="Autor" />
                        <asp:BoundField DataField="asesor" HeaderText="Asesor (es)" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                        <asp:BoundField DataField="fase" HeaderText="Fase" />
                    </Columns>
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                <table style="width:100%; border-collapse:collapse" cellpadding="3" cellspacing="0" border="1">
                    <tr style="background-color:#5D7B9D;color:White">
                        <th >#</th>
                        <th >
                            Fecha de registro</th>
                        <th >
                            Código Inv</th>
                        <th >
                            Título de Investigación</th>
                        <th >
                            Autor</th>
                        <th >
                            Asesor</th>
                        <th >
                            Estado</th>
                        <th >
                            Fase</th>
                    </tr>
                    <tr class="selected">
                        <td>
                           1</td>
                        <td>
                            <%=Date.Today%></td>
                        <td>
                            <%=Session("codigo")%>&nbsp;</td>
                        <td>
                            <%=Session("titulo")%>&nbsp;</td>
                        <td>
                            <%=Session("autor")%>&nbsp;</td>
                        <td>
                            <%=Session("asesor")%>&nbsp;</td>
                        <td>
                            <%=Session("estado")%>&nbsp;</td>
                        <td>
                            <%=Session("fase")%>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    </table>
            </td>
        </tr>
                <tr>
            <td colspan="2" class="contornotabla_azul" bgcolor="#FFCC66">
                Detalle de investigación seleccionada</td>
                </tr>
                <tr>
            <td colspan="2">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="fechareg" HeaderText="Fecha Registro" />
                        <asp:BoundField DataField="codigo" HeaderText="Código Inv." />
                        <asp:BoundField DataField="titulo" HeaderText="Titulo de investigación" />
                        <asp:BoundField DataField="autor" HeaderText="Autor" />
                        <asp:BoundField DataField="asesor" HeaderText="Asesor (es)" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                        <asp:BoundField DataField="fase" HeaderText="Fase" />
                    </Columns>
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                <table style="width:100%; border-collapse:collapse" cellpadding="3" cellspacing="0" border="1">
                    <tr style="background-color:#5D7B9D;color:White">
                        <th >
                            Fecha de registro</th>
                        <th >
                            Fase Inv.</th>
                        <th >
                            Tipo</th>
                        <th >
                            Apellidos y Nombres</th>
                        <th >
                            Resultado</th>
                        <th >
                            Nota</th>
                        <th >
                            Observación</th>
                    </tr>
                    <tr class="selected">
                        <td>
                            <%=Session("notafecha")%></td>
                        <td>
                            <%=Session("notafase")%>&nbsp;</td>
                        <td>
                            <%=Session("notatipo")%>&nbsp;</td>
                        <td>
                            <%=Session("notaautor")%>&nbsp;</td>
                        <td>
                            <%=Session("notaresultado")%>&nbsp;</td>
                        <td>
                            <%=Session("nota")%>&nbsp;</td>
                        <td>
                            <%=Session("notaobs")%>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <%=Session("notafecha2")%></td>
                        <td>
                            <%=Session("notafase2")%>&nbsp;</td>
                        <td>
                            <%=Session("notatipo2")%>&nbsp;</td>
                        <td>
                            <%=Session("notaautor2")%>&nbsp;</td>
                        <td>
                            <%=Session("notaresultado2")%>&nbsp;</td>
                        <td>
                            <%=Session("nota2")%>&nbsp;</td>
                        <td>
                            <%=Session("notaobs2")%>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <%=Session("notafecha3")%></td>
                        <td>
                            <%=Session("notafase3")%>&nbsp;</td>
                        <td>
                            <%=Session("notatipo3")%>&nbsp;</td>
                        <td>
                            <%=Session("notaautor3")%>&nbsp;</td>
                        <td>
                            <%=Session("notaresultado3")%>&nbsp;</td>
                        <td>
                            <%=Session("nota3")%>&nbsp;</td>
                        <td>
                            <%=Session("notaobs3")%>&nbsp;</td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>      
        </table>
        <fieldset style="padding: 2">
<legend><b>Registrar conformidad de resultado de la investigación</b></legend>
                <asp:Button ID="cmdNotaProyecto" runat="server" 
                    onclientclick="AbrirPopUp('frmresultadoinvestigacion.aspx?tipo=P&instancia=D','350','550');return(false)" 
                    Text="1. Proyecto de Inv." CausesValidation="False" 
                    UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="cmdNotaEjecucion" runat="server" 
                    onclientclick="AbrirPopUp('frmresultadoinvestigacion.aspx?tipo=E&instancia=D','350','550');return(false)" 
                    Text="2. Ejecución de Inv." CausesValidation="False" 
                    UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="cmdNotaInforme" runat="server" 
                    onclientclick="AbrirPopUp('frmresultadoinvestigacion.aspx?tipo=I&instancia=D','350','550');return(false)" 
                    Text="3. Informe de Investigación" CausesValidation="False" 
                    UseSubmitBehavior="False" />
&nbsp;<asp:Button ID="cmdNotaInforme0" runat="server" 
                    Text="Imprimir Acta de Conformidad" CausesValidation="False" 
                    UseSubmitBehavior="False" />
</fieldset>
        
    </form>
</body>
</html>
