<%@ Page Language="VB" AutoEventWireup="false" CodeFile="promedioTercioQuinto.aspx.vb"
    Inherits="academico_estudiante_promedioTercioQuinto" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cuadro de meritos - USAT</title>
    <link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
</head>
<body>
    <form id="form1" runat="server">
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
    <div class="usatTitulo">
        CUADRO DE MÉRITOS
        <asp:LinkButton ID="lnkTercioSup" runat="server">Tercio Superior</asp:LinkButton>
        &nbsp;|
        <asp:LinkButton ID="lnkQuintoSup" runat="server">Quinto Superior</asp:LinkButton>
        <br />
    </div>
    <div>
        <br />
        <asp:GridView ID="gvCuadro" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
            Width="100%">
            <Columns>
                <asp:BoundField HeaderText="Nro" />
                <asp:BoundField DataField="codigoUniver_Alu" HeaderText="codigo Univer." />
                <asp:BoundField DataField="alumno" HeaderText="Alumno" />
                <asp:BoundField DataField="nrosemestres" HeaderText="N° semestres" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="promedio" HeaderText="Promedio" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Nrodesaprobados" HeaderText="Nro desaprobados" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="creditosAprobados" HeaderText="Cred. Aprobados" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <HeaderStyle BackColor="#0066CC" ForeColor="White" />
        </asp:GridView>
        <asp:GridView ID="gvCuadroNuevo" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
            Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="Nro">
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="codigoUniver_Alu" HeaderText="CÓDIGO UNIVER." />
                <asp:BoundField DataField="ALUMNO" HeaderText="ALUMNO" />
                <asp:BoundField DataField="SemestresCursados" HeaderText="N° semestres" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CreditosMatriculados" HeaderText="CRED. MATRICULADOS"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CreditosAprobados" HeaderText="CRED. APROBADOS" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="DIFE. CREDITOS" HeaderText="DIFE. CREDITOS" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PPA" HeaderText="PROMEDIO" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <HeaderStyle BackColor="#0066CC" ForeColor="White" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
