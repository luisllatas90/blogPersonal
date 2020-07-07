<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptCargaAcademicaDocenteExceso.aspx.vb" Inherits="academico_cargalectiva_rptCargaAcademicaDocenteExceso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../librerianet/private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../../../librerianet/private/estiloweb.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="7"  align="center" class="contornotabla" >
                    <asp:Label ID="Label4" runat="server" 
                        Text="[ LISTA DE DOCENTES CON EXCESO DE CARGAR ACADEMICA ]" 
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Ciclo Académico"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlCicloAcademico" runat="server" AutoPostBack="True" 
                        Font-Size="Smaller">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Dedicación"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDedicacion" runat="server" AutoPostBack="True" 
                        Font-Size="Smaller">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Carrera Profesional"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCarreraProfesional" runat="server" AutoPostBack="True" 
                        Font-Size="Smaller">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" CssClass="buscar" Text="Buscar" />
                </td>
            </tr>
            <tr>
                <td colspan="7" align="right">
                    <asp:Label ID="lblNumreg" runat="server" ForeColor="Red" Text="00" 
                        Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:GridView ID="gvLista" runat="server" Width="100%" 
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" 
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="Smaller">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField DataField="codigo_Per" HeaderText="Codigo" />
                            <asp:BoundField DataField="docente" HeaderText="Docente" />
                            <asp:BoundField DataField="Descripcion_Ded" HeaderText="Tipo Descripcion" />
                            <asp:BoundField DataField="TotalHoras" HeaderText="Hrs." />
                            <asp:BoundField DataField="HorasPermitidas" HeaderText="H.Pr" />
                            <asp:BoundField DataField="HoraExceso" HeaderText="H.Ex" />
                            <asp:BoundField DataField="nombre_Cpf" HeaderText="Carrera Profesional" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
