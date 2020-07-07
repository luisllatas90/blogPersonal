<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EnviarCorreoCambioAmbiente.aspx.vb" Inherits="academico_horarios_administrar_EnviarCorreoTest" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <style>
     body
        { font-family:Trebuchet MS;
          font-size:12px;
          cursor:hand;
          background-color:white;	 
        }
     .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3 ; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px; 
       }
    TBODY {
	display: table-row-group;
}
	
        #txtDesde, #txtHasta
        {
            background-color: #C9DDF5;
        }
          </style>
</head>
<body>
    <form id="form1" runat="server">
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock" Text="Procesando..." Title="Por favor espere" />
    <div>
        <h3>Enviar Notificación de Cambio de Ambiente</h3>
        Ciclo Académico:
        <asp:DropDownList ID="ddlCiclo" runat="server" AutoPostBack="True" 
           >
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Enviar Notficación" CssClass="btn"/>
        <br />
        <br />
        <asp:Label ID="lblExisten" runat="server" Text="Existen"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" 
            Width="85%" AutoGenerateColumns="False" DataKeyNames="codigo_lho">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="fechareg_Lho" HeaderText="Registrado" />
                <asp:BoundField DataField="Estimado" HeaderText="Docente" />
                <asp:BoundField DataField="CorreoA" HeaderText="CorreoA" Visible="False" />
                <asp:BoundField DataField="CC1" HeaderText="CC1" Visible="False" />
                <asp:BoundField DataField="CC2" HeaderText="CC2" Visible="False" />
                <asp:BoundField DataField="Curso" HeaderText="Curso" />
                <asp:BoundField DataField="Horario" HeaderText="Horario" />
                <asp:BoundField DataField="Escuela" HeaderText="Carrera profesional" />
                <asp:BoundField DataField="ambAnterior" HeaderText="Ambiente Anterior" />
                <asp:BoundField DataField="ambActual" HeaderText="Ambiente Actual" />
                <asp:BoundField DataField="codigo_lho" HeaderText="codigo_lho" 
                    Visible="False" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnDescartar" runat="server" Text="Descartar"                             
                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                            CommandName="Descartar" CssClass="btn" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
