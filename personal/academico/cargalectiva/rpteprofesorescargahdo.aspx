<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rpteprofesorescargahdo.aspx.vb" Inherits="academico_cargalectiva_rpteprofesorescargahdo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Consultar Carga Academica</title>
    <link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
    <style type="text/css">
        .style2
        {
            height: 17px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
        <tr>
              <td width="97%" height="5%" colspan="2" class="usatTitulo">Lista de Docentes con 
                  Carga Académica</td>
        </tr>
        <tr>
            <td width="22%" height="5%">Semestre Académico</td>
            <td class="style2">
                <asp:DropDownList ID="dpCodigo_cac" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="22%" height="5%">Departamento Académico</td>
            <td>
                <asp:DropDownList ID="dpCodigo_dac" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="22%" height="5%">&nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" CssClass="buscar2" Text="Consultar" 
                    Height="21px" Width="86px" />
                <asp:Button ID="Button2" runat="server" CssClass="buscar2" Text="Exportar" Height="21px" Width="86px" />
            </td>
        </tr>
        <tr>
            <td width="22%" height="5%">&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="22%" height="5%" colspan="2">
 <asp:GridView ID="dataCursos" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" ForeColor="Black">
        <RowStyle BackColor="#F7F7DE" />
             <Columns>
                       
                 <asp:BoundField  HeaderText="Nro" />
                 <asp:BoundField DataField="docente" HeaderText="Docente" />
                       
            </Columns>                       
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
                </td>
                </tr>
                </table>
                
   
                </form>
                
</body>
</html>

