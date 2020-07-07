<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmCreditoOdontologia.aspx.vb" Inherits="administrativo_FrmCreditoOdontologia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 15%">
                <asp:Label ID="Label1" runat="server" Text="Tipo Estudio:"></asp:Label></td>
            <td style="width: 30%">            
                <asp:DropDownList ID="cboTipoEstudio" runat="server" AutoPostBack="True">
                    <asp:ListItem Text="PRE GRADO" Value=2></asp:ListItem>
                    <asp:ListItem Text="SEGUNDA ESPECIALIDAD" Value=8></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 15%">Carrera Profesional:</td>
            <td style="width: 40%">
                <asp:DropDownList ID="cboEscuela" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCiclo" runat="server" Text="Ciclo de ingreso:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="cboCicloIngreso" runat="server">
                </asp:DropDownList>
            </td>
            <td colspan="2">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" />
            </td>            
        </tr>
    </table>    
    <br />
    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label> 
    <asp:GridView ID="gvDetalle" runat="server" Width="100%" 
        AutoGenerateColumns="False" DataKeyNames="codigo_Alu" >
        <Columns>
            <asp:BoundField DataField="codigo_Alu" HeaderText="codigo_Alu" 
                Visible="False" />
            <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Cod. Univ." />
            <asp:BoundField DataField="NombreAlumno" HeaderText="Estudiante" />
            <asp:BoundField DataField="disponible" HeaderText="Disponible" >            
                <HeaderStyle BackColor="#66FF99" ForeColor="Black" />
                <ItemStyle BackColor="#CCFFCC" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="TotalCargo" HeaderText="Total Cargos" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="TotalAbono" HeaderText="Total Abonos" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="PendientePago" HeaderText="Pendiente Pago" >            
                <HeaderStyle Font-Bold="True" ForeColor="#003366" />
                <ItemStyle Font-Bold="True" ForeColor="#003366" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="saldoPensiones" HeaderText="Saldo Pensiones">
                <HeaderStyle BackColor="#99CCFF" ForeColor="Black" />
                <ItemStyle BackColor="#DDF4FF" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="cargosPensiones" HeaderText="Cargos Pensiones">
                <HeaderStyle BackColor="#99CCFF" ForeColor="Black" />
                <ItemStyle BackColor="#DDF4FF" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Crédito">
                <ItemStyle HorizontalAlign="Right" />
                <ItemTemplate>
                    <asp:TextBox ID="txtLimite" EnableViewState="true" Width="70px" Height="20px" runat="server" Text='<%#  DataBinder.Eval(Container, "DataItem.credito_alu") %>'>
                    </asp:TextBox>
                </ItemTemplate>            
            </asp:TemplateField>            
        </Columns>
        <HeaderStyle BackColor="#e33439" ForeColor="White" Height="25px" />                
    </asp:GridView>        
    <br />
    <asp:Button ID="btnGuardar" runat="server" Text="Actualizar Limite de Crédito" />    
    </form>
</body>
</html>
