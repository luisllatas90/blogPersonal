<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmEntregarMaterial.aspx.vb" Inherits="administrativo_pec_FrmEntregarMaterial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" src="../../../private/funciones.js"></script>        
    <style type="text/css">
        .0
        {
            background-color: #E6E6FA;
        }
        .1
        {
            background-color: #FFFCBF;
        }
        .2
        {
            background-color: #D9ECFF;
        }
        .3
        {
            background-color: #C7E0CE;
        }
        
        .5
        {
            background-color: #FFCC00;
        }
        .6
        {
            background-color: #F8C076;
        }
        .4
        {
            background-color: #CCFF66;
        }
        </style>        
        <script type="text/javascript">
            function seleccionaFoco() {
                if (event.keyCode == 13) {
                    event.keyCode = 9;
                    //document.form1.btnEntregar.click;
                }
            } 
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
        <tr>
            <td colspan="2" 
                style="background:#E6E6FA; width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #0099FF;" height="40px">
                <asp:Label ID="lblTitulo" runat="server" Text="Entrega de Material" 
                    Font-Bold="True" Font-Size="11pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:15%">
                <asp:Label ID="Label1" runat="server" Text="Centro de Costos:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddpCentroCostos" runat="server" Width="80%" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    
    <table width="100%">
        <tr>
            <td colspan="3">
                
                </td>
        </tr>
        <tr>
            <td style="width:15%">
                <asp:Label ID="Label2" runat="server" Text="Fecha:"></asp:Label>
            </td>
            <td style="width:35%">
                <asp:Calendar ID="calFecha" runat="server" Width="250px"></asp:Calendar>
            </td>
            <td rowspan="5" valign="top">
                <asp:GridView ID="gvMateriales" runat="server" Width="100%" 
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkElegir" runat="server" Checked="true"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo_mev" HeaderText="Codigo">                        
                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion_Mat" HeaderText="Material" />
                    </Columns>
                    <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="22px" />                
                    <RowStyle Height="20px" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="width:15%">
                <asp:Label ID="Label3" runat="server" Text="Documento:"></asp:Label>
            </td>
            <td style="width:35%">
                <asp:DropDownList ID="ddpDocumento" runat="server" Width="250px">                
                </asp:DropDownList>
            </td>            
        </tr>
        <tr>
            <td style="width:15%">
                <asp:Label ID="Label4" runat="server" Text="No. Documento:"></asp:Label>
            </td>
            <td style="width:35%">            
                <asp:TextBox ID="txtDocumento" runat="server" ></asp:TextBox>
            </td>            
        </tr>
        <tr>
            <td style="width:15%">
                &nbsp;</td>
            <td valign="top" style="width:35%">
                <asp:Button ID="btnEntregar" runat="server" Text="Entregar Material" CssClass="agregar2" Width="120px" Height="22px" /></td>
                                
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblGuardo" runat="server" Font-Bold="True" Font-Size="20pt" 
                    ForeColor="Blue"></asp:Label> <br />
                <asp:Label ID="lblAviso" runat="server" Font-Bold="True" 
                    ForeColor="Red" Font-Size="20pt"></asp:Label>
            </td>                                
        </tr>
    </table>
    </div>
    <asp:HiddenField ID="HdPermisos" runat="server" />
    <asp:HiddenField ID="Hd_CicloAcademico" runat="server" />
    </form>
</body>
</html>
