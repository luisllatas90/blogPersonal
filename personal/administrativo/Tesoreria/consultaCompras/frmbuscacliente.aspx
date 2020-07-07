<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmbuscacliente.aspx.vb" Inherits="frmbuscacliente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type ="text/javascript" src="funciones.js">
    </script>
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 102px;
        }
        .style3
        {
            width: 221px;
        }
        .style4
        {
            color: #FFFFFF;
            font-family: "Courier New";
            font-size: small;
        }
    </style>
    <script type="text/javascript" >
            function mostrarConsultaComprasProveedor(Nombre,Cod  )
                {
                    if (window.opener.document.all.pnlconsultarporproveedor.visible=true )
                        {
                            window.opener.document.all.txtcodigo_tcl.value=Cod;
                            window.opener.document.form1.txtproveedor.value=Nombre;   
                            window.opener.document.all.hdtxtcodigo_tcl.value=Cod + '//'  + Nombre;
                            //window.close() ;
                        }
                    
                }
            
    
    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div style="border-top: 1px outset #ABCDF3; background-color: #993300; border-left-color: #ABCDF3; border-left-width: 1px; border-right-color: #ABCDF3; border-right-width: 1px; border-bottom-color: #ABCDF3; border-bottom-width: 1px;" 
        class="style4">
    
        &nbsp;Búsqueda de clientes :</div>
    <table class="style1" style="border: 1px solid #000000">
        <tr>
            <td class="style2">
                <asp:Label ID="Label1" runat="server" Text="Nombres :" 
                    style="font-family: 'Courier New'; font-size: small"></asp:Label>
            </td>
            <td class="style3">
                <asp:TextBox ID="txtnombre" runat="server" Width="446px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="cmdbuscar" runat="server" Text="Buscar" BorderColor="White" 
                    BorderStyle="Ridge" Font-Names="Courier New" 
                    style="color: #FFFFFF; background-color: #3399FF" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                    <div style="overflow : auto; height: 363px; width: 758px;">
                    <asp:GridView ID="lstinformacion" runat="server" 
                    AutoGenerateColumns="False" Width ="99%" Height="16px">
                        <Columns>
                            <asp:BoundField DataField="codigo_tcl" HeaderText="codigo_tcl" 
                                SortExpression="codigo_tcl">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" 
                                    ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres" 
                                SortExpression="Nombres">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" 
                                    ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo" HeaderText="tipo" SortExpression="tipo">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" 
                                    ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificador" HeaderText="identificador" 
                                SortExpression="identificador">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" 
                                    ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                   </div> 
                    </td>
        </tr>
        </table>
    </form>
</body>
</html>
