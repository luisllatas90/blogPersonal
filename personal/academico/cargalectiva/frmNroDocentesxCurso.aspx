<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmNroDocentesxCurso.aspx.vb" Inherits="academico_cargalectiva_frmNroDocentesxCurso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
    <link rel="stylesheet" type="text/css" href="../../../private/estilo.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>    
        <table style="width: 100%;">
            <tr>
                <td>Semestre académico: </td>
                <td>
                    <asp:DropDownList ID="cboCiclo" runat="server">
                    </asp:DropDownList>
                </td>
                <td> Carrera Profesional: </td>
                <td><asp:DropDownList ID="cboEscuela" runat="server">
                    </asp:DropDownList></td>
            </tr>  
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>                    
                </td>                                
                <td colspan="2" align="right">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" />
                </td>
            </tr>                                
        </table>
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" />                   
        <asp:GridView ID="gvDocente" runat="server" Width="100%" 
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="codigo_Cup" HeaderText="COD." />
                <asp:BoundField DataField="ciclo_Cur" HeaderText="CICLO" />
                <asp:BoundField DataField="nombre_Cur" HeaderText="CURSO" />
                <asp:BoundField DataField="grupoHor_Cup" HeaderText="GRUPO" />                
                <asp:BoundField DataField="docente" HeaderText="DOCENTE" />
                <asp:BoundField DataField="nrodocentes" HeaderText="SOLICITADO" />
                <asp:TemplateField HeaderText="Nro. DOCENTE">
                <ItemTemplate>
                    <asp:TextBox ID="txtDocentes" EnableViewState="true" Width="70px" Height="20px" runat="server" Text='<%#  DataBinder.Eval(Container, "DataItem.docentes") %>'>
                    </asp:TextBox>
                </ItemTemplate>                    
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#702121" ForeColor="White" Height="25px" />                
        </asp:GridView>    
    </div>
    <asp:HiddenField ID="hdAux" runat="server" />
    </form>
</body>
</html>
