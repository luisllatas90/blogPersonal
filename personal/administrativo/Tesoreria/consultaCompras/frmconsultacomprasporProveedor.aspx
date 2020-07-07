<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmconsultacomprasporProveedor.aspx.vb" Inherits="frmconsultacomprasporProveedor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type ="text/javascript" src="funciones.js">
    </script>
    <script type ="text/javascript" src="calendario.js"></script>
    <link  rel ="stylesheet" href=estilo.css/> 
    <title>Sistema de Tesorería</title>
    <style type="text/css">

        .style2
        {
            height: 53px;
        }
        .style3
        {
            color: blue;
            font-family: Arial;
        }
        .style4
        {
            height: 9px;
        }
    </style>
    <script type="text/javascript" >
        function HabilitarIntervalo(x)
            {
                alert (x.checked);
                //return 0
                
                        window.document.form1.txtfechainicial.enabled=true;
                        window.document.form1.txtfechafinal.enabled=true; 
                
            }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
        <table width ="100%" style="border: 1px solid #000000">
            <tr>
                <td>
                    <asp:Panel ID="Panel1" runat="server" Height="20px" Width="1217px" 
                        style="background-color: #993300; color: #FFFFCC;">
                    <asp:RadioButton ID="opconsultarnumero" runat="server" 
                            Text="Consultar por Proveedor" Font-Names="Arial" Font-Size="8pt" 
                            GroupName="condicion" Checked="True" 
                            
                            style="color: #FFFFFF; font-size: small; font-family: 'Courier New', Courier, 'espacio sencillo';" /></asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="style4">
    
    
                    <hr style="color: #FF9900" />
    
    
                </td>
            </tr>
            <tr>
                <td class="style2">
    
    
                    <asp:Panel ID="pnlconsultarporproveedor" runat="server" Height="31px" 
                        Width="1190px" BorderStyle="Solid" BorderWidth="1px">
                        <asp:Label ID="Label2" runat="server" Text="Proveedor :" 
                            Width="93px" Font-Names="Arial" Font-Size="8pt" Height="22px" 
                            
                            style="color: #0000FF; font-family: 'Courier New', Courier, 'espacio sencillo';"></asp:Label>
                           <asp:TextBox ID="txtcodigo_tcl" runat="server" Enabled="False" 
                            Font-Names="Arial" Font-Size="X-Small" Width="43px"></asp:TextBox>
                        <asp:TextBox ID="txtproveedor" runat="server" Enabled="False" 
                            Font-Names="Arial" Font-Size="X-Small" Width="309px"></asp:TextBox>
                        <asp:Button ID="cmbuscacliente" runat="server" Height="21px" Text="..." 
                            style="width: 26px" 
                            onclientclick="window.open('frmbuscacliente.aspx','t','toolbar=no, width=800, height=500')" />
                        <asp:Button ID="cmdbuscarporproveedor" runat="server" Height="23px" 
                            Text="Buscar" BackColor="#0099FF" BorderColor="White" BorderStyle="Ridge" 
                            ForeColor="White" style="font-family: 'Courier New'" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkfechas" runat="server" 
                            ForeColor="Blue" Height="23px" 
                            Text="Intervalo fechas:" Width="160px" AutoPostBack="True" 
                            style="font-family: 'Courier New'" />
                        &nbsp; &nbsp;&nbsp;
                        <asp:TextBox ID="txtfechainicial" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                        
                        <input id="Button1" onclick="MostrarCalendario('txtfechainicial')"
                        type="button" />
                        
                        <asp:Label ID="Label4" runat="server" ForeColor="Blue" Height="20px" 
                            Text="al" Width="41px"></asp:Label>
                        <asp:TextBox ID="txtfechafinal" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                        &nbsp;<input ID="Button2" onclick="MostrarCalendario('txtfechafinal')" 
                            type="button" />
                        
            </asp:Panel>
    
    
                </td>
            </tr>
            <tr>
                <td style="height: 141px; width : 99%">
                <div style="overflow : auto; height : 696px ;width: 99%"><img src ="iconos/atencion.gif" />
                    <asp:Label ID="lblobservacion" runat="server" BackColor="#FFFFD7" 
                        BorderColor="#FFFFCC" Font-Names="Arial" Font-Size="X-Small" ForeColor="#CC3300" 
                        Text="No existen elementos a mostrar" Width="1191px" 
                        style="color: #FFFFFF; font-family: 'Courier New'; font-size: small; background-color: #993300"></asp:Label>
                    <asp:GridView ID="lstinformacion" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_rco" Width ="98%">
                        <Columns>
                            <asp:BoundField DataField="codigo_rco" HeaderText="Id" InsertVisible="False"
                                ReadOnly="True" SortExpression="codigo_rco">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_tdo" HeaderText="Tipo Documento" SortExpression="descripcion_tdo">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechadoc_rco" HeaderText="Fecha">
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numerodoc_rco" HeaderText="Número" SortExpression="numerodoc_rco">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombres" HeaderText="Proveedor" ReadOnly="True" SortExpression="nombres">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_tip" HeaderText="Moneda" SortExpression="descripcion_tip" >
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="igvcompra_rco" HeaderText="IGV" SortExpression="igvcompra_rco">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="totalcompra_rco" HeaderText="Total Compra" SortExpression="totalcompra_rco">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="totalfisico_rco" HeaderText="Total Físico" SortExpression="totalfisico_rco">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valorcompra_rco" HeaderText="Valor compra" SortExpression="valorcompra_rco">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_tcom" HeaderText="Tipo Compra" SortExpression="descripcion_tcom">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:ImageField DataImageUrlField="codigo_rco" 
                                DataImageUrlFormatString="~/iconos/Atencion.gif" 
                                AlternateText="Visualizar Cargos">
                            </asp:ImageField>
                            <asp:ImageField DataImageUrlField="codigo_rco" DataImageUrlFormatString="~/iconos/buscar.gif">
                            </asp:ImageField>
                        </Columns>
                    </asp:GridView>
                   
                    </td>
            </tr>
            </table>
    
    
                        <asp:HiddenField ID="hdtxtcodigo_tcl" runat="server" />
    
    
    </form>
    <p class="style3"><img src=
        "iconos/atencion.gif" /> recuerde que los documentos con tipo de compra &quot;Rendición&quot;, no generan cargos en 
        e Sistema de Tesorería</p>
</body>
</html>
