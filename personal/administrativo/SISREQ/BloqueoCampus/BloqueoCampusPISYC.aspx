<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BloqueoCampusPISYC.aspx.vb" Inherits="Consultas_BloqueoCampusPISYC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table cellpadding="0" style="width:100%;" border="0" cellspacing="0">
            <tr>
                <td>
                    Edición de programa:&nbsp;
                    <asp:DropDownList ID="cboPrograma" runat="server">
                        <asp:ListItem Value="1">I Programa</asp:ListItem>
                        <asp:ListItem Value="2">II Programa</asp:ListItem>
                        <asp:ListItem Value="3">III Programa</asp:ListItem>
                    </asp:DropDownList>
&nbsp;<asp:Button ID="cmdConsultar" runat="server" Text="Consultar" Width="70px" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td bgcolor="#FFC875" height="20" 
                    style="border-style: solid none none none; border-width: 1px; border-color: #808080">
                    |
                    <asp:LinkButton ID="lnkActualizar" runat="server" ForeColor="#333333">Actualizar estado</asp:LinkButton>
                    |</td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvEstudiantes" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="codigo_alu,estadoDeuda_Alu,bloqueoDocumento_Alu" Width="100%" 
                        EnableSortingAndPagingCallbacks="True">
                        <Columns>
                            <asp:BoundField HeaderText="Nro" />
                            <asp:BoundField DataField="estadoDeuda_Alu" HeaderText="Bloqueo por deuda" />
                            <asp:BoundField DataField="bloqueoDocumento_Alu" 
                                HeaderText="Bloqueo Por Doc." />
                            <asp:BoundField DataField="codigoUniver_alu" HeaderText="Cód. Universitario" />
                            <asp:BoundField DataField="password_Alu" HeaderText="Clave" />
                            <asp:BoundField DataField="ALUMNO" HeaderText="Alumno" />
                            <asp:BoundField DataField="EdicionProgramaEspecial_Alu" 
                                HeaderText="Edición Programa" />
                            <asp:BoundField DataField="estadoActual_Alu" HeaderText="Estado " />
                        </Columns>
                        <HeaderStyle BackColor="#FF9900" ForeColor="White" />
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
