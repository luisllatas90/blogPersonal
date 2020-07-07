<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCargaAcademicaDocente.aspx.vb" Inherits="academico_cargalectiva_administrarcargalectiva_frmCargaAcademicaDocente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../../librerianet/private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../../../../librerianet/private/estiloweb.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td >
                    
                    <asp:Label ID="lblMensaje" runat="server" Text="lblMensaje" ForeColor="Red"></asp:Label>
                    
                </td>
                <td align="right">
                
                     <asp:Button ID="btnCancelar" runat="server" Text="Regresar" Width="100px" Height="22px" CssClass="salir" 
                    onclientclick="self.parent.tb_remove();" UseSubmitBehavior="False" />
                
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" class="contornotabla" >
                    <asp:Label ID="Label5" runat="server" Text="Distribución de Carga Académica Actual" 
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Ciclo Académico: "></asp:Label></td>
                <td>
                    <asp:Label ID="lblCicloCac" runat="server" Text="lblCicloCac"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Docente: "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDocente" runat="server" Text="lblDocente"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Tipo Dedicación: "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDedicacion" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView Width="100%" ID="gvCargaAcademica" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3" Font-Size="Smaller">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField DataField="identificador_Cur" HeaderText="Identificador" />
                            <asp:BoundField DataField="nombre_Cpf" HeaderText="Carrera Profesional" />
                            <asp:BoundField DataField="nombre_Cur" 
                                HeaderText="Curso" />
                            <asp:BoundField DataField="grupoHor_Cup" HeaderText="Grupo" />
                            <asp:BoundField DataField="TFuncion" HeaderText="Función" />
                            <asp:BoundField DataField="totalHoras_Car" HeaderText="Horas" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
             
            <tr>
                <td>
                
                     <asp:Button ID="btnCancelar0" runat="server" Text="Regresar" Width="100px" 
                         Height="22px" CssClass="salir" 
                    onclientclick="self.parent.tb_remove();" UseSubmitBehavior="False" />
                
                </td>
                <td align="right">
                    Total:&nbsp;
                <asp:Label ID="lblTotalCac" runat="server" Text="00" ForeColor="#CC0000"></asp:Label>&nbsp;Hrs.</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
