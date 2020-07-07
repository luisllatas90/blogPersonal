<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEstadoCuentaEscuela.aspx.vb" Inherits="librerianet_academico_frmEstadoCuentaEscuela" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

select {
	font-family: Verdana;
	font-size: 8.5pt;
}
    </style>
</head>
<body>
    <form id="form1" runat="server" style="font-size: 8pt; font-family: Verdana;">
    <div>
    
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small" 
                    ForeColor="#003399" Text="Deudas de alumnos por escuela"></asp:Label>
                <br />
                <br />
    
                Ciclo:
&nbsp;<asp:DropDownList ID="dpCodigo_cac" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
                Escuela Profesional:
                <asp:DropDownList ID="dpCodigo_cpf" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp; &nbsp;<asp:Button ID="cmdExportar" runat="server" Text="Exportar" />
                <br />
        <br />
        <asp:GridView ID="gvConsulta" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" 
            GridLines="None">
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Código" 
                    SortExpression="codigoUniver_Alu" />
                <asp:BoundField DataField="Alumno" HeaderText="Alumno" ReadOnly="True" 
                    SortExpression="Alumno" />
                <asp:BoundField DataField="nombre_cpf" HeaderText="Carrera" 
                    SortExpression="nombre_cpf" />
                <asp:BoundField DataField="cicloIng_Alu" HeaderText="Ciclo Ingreso" 
                    SortExpression="cicloIng_Alu" />
                <asp:BoundField DataField="R" HeaderText="R" ReadOnly="True" 
                    SortExpression="R" />
                <asp:BoundField DataField="P1" HeaderText="Pens. 1" SortExpression="P1" />
                <asp:BoundField DataField="P2" HeaderText="Pens. 2" SortExpression="P2" />
                <asp:BoundField DataField="P3" HeaderText="Pens. 3" SortExpression="P3" />
                <asp:BoundField DataField="P4" HeaderText="Pens. 4" SortExpression="P4" />
                <asp:BoundField DataField="P5" HeaderText="Pens. 5" SortExpression="P5" />
                <asp:BoundField DataField="DeudaTotal" HeaderText="DeudaTotal" ReadOnly="True" 
                    SortExpression="DeudaTotal" />
                <asp:BoundField DataField="VR" HeaderText="VR" ReadOnly="True" 
                    SortExpression="VR" />
                <asp:BoundField DataField="V1" HeaderText="Venc. 1" ReadOnly="True" 
                    SortExpression="V1" />
                <asp:BoundField DataField="V2" HeaderText="Venc. 2" ReadOnly="True" 
                    SortExpression="V2" />
                <asp:BoundField DataField="V3" HeaderText="Venc. 3" ReadOnly="True" 
                    SortExpression="V3" />
                <asp:BoundField DataField="V4" HeaderText="Venc. 4" ReadOnly="True" 
                    SortExpression="V4" />
                <asp:BoundField DataField="V5" HeaderText="Venc. 5" ReadOnly="True" 
                    SortExpression="V5" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
            SelectCommand="consutarPensionesCicloAcademicoWeb" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="dpCodigo_cac" DefaultValue="0" 
                    Name="codigo_cac" PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="dpCodigo_cpf" DefaultValue="0" 
                    Name="codigo_cpf" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
