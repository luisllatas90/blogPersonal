<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmReporteNotas.aspx.vb" Inherits="academico_notas_frmReporteNotas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../../../../private/estilo.css" />
    <link href="scrolling.css" rel="stylesheet" type="text/css" />
    <script src="../../../../private/calendario.js"></script>
 </head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td style="width:20%">
                    <asp:Label ID="Label1" runat="server" Text="Ciclo Académico:"></asp:Label>
                </td>
                <td style="width:25%">
                    <asp:DropDownList ID="cboCiclo" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="width:20%">
                    <asp:Label ID="Label2" runat="server" Text="Escuela Profesional"></asp:Label>
                </td>
                <td style="width:35%">
                    <asp:DropDownList ID="cboEscuela" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>            
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Desde:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFechaInicio" runat="server"></asp:TextBox>
                    <input onclick="MostrarCalendario('txtFechaInicio')" type="button" value="..." />
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Hasta:"></asp:Label>
                </td>
                <td>                   
                    <asp:TextBox ID="txtFechaFin" runat="server"></asp:TextBox>
                    <input onclick="MostrarCalendario('txtFechaFin')" type="button" value="..." />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Curso"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtCurso" runat="server" Width="90%"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Button ID="btnMostrar" runat="server" Text="Mostrar" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="Small" 
        ForeColor="Red"></asp:Label>
    <div style="width: 100%; height: 250px; z-index: 1; 
	    top: 15px; position: relative" class="Marco"> 
    <asp:GridView ID="gvCursos" runat="server" Width="100%" 
        AutoGenerateColumns="False" DataKeyNames="codigo_cup">
        <Columns>
            <asp:BoundField DataField="codigo_cup" HeaderText="codigo_cup" 
                Visible="False" />
            <asp:BoundField DataField="nombre_Cpf" HeaderText="Escuela Profesional" />
            <asp:BoundField DataField="identificador_cur" HeaderText="Identificador" />
            <asp:BoundField DataField="nombre_cur" HeaderText="Curso" />
            <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo" />
            <asp:BoundField DataField="docente" HeaderText="Docente" />
            <asp:BoundField DataField="tipo_fun" HeaderText="Coordinador" />
            <asp:BoundField DataField="impresion_cup" HeaderText="Impreso" />
            <asp:CommandField EditText="Ver" ShowEditButton="True" />
        </Columns>        
        <HeaderStyle BackColor="#dfdba4" ForeColor="Black" Height="25px" />                
        <RowStyle Height="20px" />
    </asp:GridView>
    </div>
    <iframe id="fraDetalle" width="100%" height="700px" border="0" frameborder="0" runat="server">
    </iframe>
    </form>
</body>
</html>
