﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegInscripcionKermesPersonal.aspx.vb" Inherits="Inscripcion_frmRegInscripcionPersonal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inscripcion de Personal</title>
    <link href="../../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

.contornotabla {
	border: 1px solid #808080;
	background-color: #FFFFFF;
}
.contornotabla {
	border: 1px solid #808080;
	background-color: #FFFFFF;
}
table {
	font-family: Trebuchet MS;
	font-size: 8pt;
}
TBODY {
	display: table-row-group;
}
tr {
	font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
	font-size: 8pt;
	color: #2F4F4F;
	cursor: pointer;
}
.usatsubtitulousuario {
	color: #000080;
	font-weight: bold;
}
        .style2
        {
            width: 70%;
        }
        .style4
        {
            width: 192px;
            font-weight: bold;
            color: #000000;
        }
        .style5
        {
            width: 152px;
            text-align: center;
        }
        .style8
        {
            width: 192px;
            font-weight: bold;
            color: #FFFFFF;
            text-align: center;
            height: 31px;
            background-color: #0066FF;
        }
        .style9
        {
            color: #FFFFFF;
            font-weight: bold;
            height: 31px;
            text-align: center;
            background-color: #0066FF;
        }
        .style10
        {
            width: 86px;
            font-weight: bold;
            color: #FFFFFF;
            text-align: center;
            height: 31px;
            background-color: #0066FF;
        }
        .style11
        {
            width: 86px;
            font-weight: bold;
            text-align: center;
        }
        .style12
        {
            color: #FFFFFF;
            font-weight: bold;
            width: 118px;
            text-align: center;
            height: 31px;
            background-color: #0066FF;
        }
        .style13
        {
            width: 118px;
            text-align: center;
            background-color: #99CCFF;
        }
        .style14
        {
            width: 152px;
            font-weight: bold;
            color: #FFFFFF;
            height: 31px;
            text-align: center;
            background-color: #0066FF;
        }
        .style15
        {
            text-align: right;
            background-color: #99CCFF;
        }
        .style17
        {
            width: 152px;
            background-color: #FFFFCC;
        }
        .style18
        {
            width: 86px;
            font-weight: bold;
            text-align: center;
            background-color: #FFFFCC;
        }
        .style19
        {
            width: 192px;
            font-weight: bold;
            color: #000000;
            background-color: #FFFFCC;
        }
    </style>
    <script type="text/javascript">
        function confirmar() {            
            return confirm('¿Desea comprar esta entrada?');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
    <table>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblCentroCosto" runat="server" Text="EVENTO:" 
                        Font-Size="Medium" Font-Bold="True" ForeColor="Maroon"></asp:Label></td>
            </tr>
            <tr>
                <td class="style2">
                    <table id="tblDatos" border="0" bordercolor="#111111" cellpadding="3" 
                        cellspacing="0" class="contornotabla" width="100%">
                        <tr>
                            <td rowspan="4" valign="top" width="10%">
                                <asp:Image ID="FotoPersonal" runat="server" Height="104px" Width="90px" />
                            </td>
                            <td width="15%">
                                Personal:
                            </td>
                            <td class="usatsubtitulousuario" width="70%">
                                    &nbsp;<asp:Label ID="lblPersonal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Apellidos y Nombres
                            </td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;<asp:Label ID="lblNombrePersonal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Doc. Identidad</td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;<asp:Label ID="lblDocumento" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Estado Civil</td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;<asp:Label ID="lblCivil" runat="server"></asp:Label>
                            </td>
                        </tr>                                                             
                    </table>
                    <br />
                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                                ForeColor="Blue" 
                                Text="Clic aquí para confirmar &gt;&gt;" 
                                TextAlign="Left" />
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Red" 
                                Text="ACEPTO LOS TÉRMINOS Y CONDICIONES"></asp:Label>
                    <br />
                   <br />
                    <asp:Panel ID="Panel1" runat="server" Enabled="False">
                        <table style="width:100%;">
                            <tr>
                                <td class="style8">
                                    Tipo</td>
                                <td class="style10">
                                    Precio por Entrada</td>
                                <td class="style14">
                                    Entradas a Comprar</td>
                                <td class="style12">
                                    Entradas Compradas</td>
                                <td class="style9">
                                    SubTotal (S/.)</td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    Tarjeta Personal USAT</td>
                                <td class="style11">
                                    S/. 25.00</td>
                                <td class="style5">
                                    <asp:DropDownList ID="txtCantidad1" runat="server" Font-Bold="False">
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtCantidad1_" runat="server" 
                Enabled="False" style="font-weight: 700; text-align: center" Width="40px" Visible="False">1</asp:TextBox>
                                    &nbsp;
                                    <asp:Button ID="Button1" runat="server" Text="Comprar" Width="71px" 
                                        OnClientClick="javascript:return confirmar();"/>
                                </td>
                                <td class="style13">
                                    <asp:Label ID="lblCantidad1" runat="server" 
                Font-Bold="True" ForeColor="Black" Text="0"></asp:Label>
                                </td>
                                <td class="style15">
                                    <asp:Label ID="lblSubtotal1" runat="server" 
                Font-Bold="True" ForeColor="Black" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    &nbsp;</td>
                                <td class="style11">
                                    &nbsp;</td>
                                <td class="style5">
                                    <asp:DropDownList ID="txtCantidad2" runat="server" 
                Font-Bold="False" Visible="False">
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:Button ID="Button2" runat="server" Text="Comprar" 
                                        onclientclick="javascript:return confirmar();" Visible="False" />
                                </td>
                                <td class="style13">
                                    <asp:Label ID="lblCantidad2" runat="server" 
                Font-Bold="True" ForeColor="Black" Text="0" Visible="False"></asp:Label>
                                </td>
                                <td class="style15">
                                    <asp:Label ID="lblSubtotal2" runat="server" 
                Font-Bold="True" ForeColor="Black" Text="0" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    &nbsp;</td>
                                <td class="style11">
                                    &nbsp;</td>
                                <td class="style5">
                                    <asp:DropDownList ID="txtCantidad3" runat="server" 
                Font-Bold="False" Visible="False">
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:Button ID="Button3" runat="server" Text="Comprar" 
                                        onclientclick="javascript:return confirmar();" Visible="False" />
                                </td>
                                <td class="style13">
                                    <asp:Label ID="lblCantidad3" runat="server" 
                Font-Bold="True" ForeColor="Black" Text="0" Visible="False"></asp:Label>
                                </td>
                                <td class="style15">
                                    <asp:Label ID="lblSubtotal3" runat="server" 
                Font-Bold="True" ForeColor="Black" Text="0" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style19">
                                    &nbsp;</td>
                                <td class="style18">
                                    &nbsp;</td>
                                <td class="style17">
                                    &nbsp;</td>
                                <td class="style13">
                                    <asp:Label ID="lblCantidad4" runat="server" 
                Font-Bold="True" Font-Size="Medium" ForeColor="Black" Text="0" Visible="False"></asp:Label>
                                </td>
                                <td class="style15">
                                    <asp:Label ID="lblSubtotal4" runat="server" 
                Font-Bold="True" Font-Size="Medium" ForeColor="Black" Text="0" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                <br />
                <asp:HiddenField ID="Hdcodigo_per" runat="server" />
                <asp:HiddenField ID="Hdcodigo_cac" runat="server" />      
                <asp:HiddenField ID="Hdcodigo_cco" runat="server" />   
                </div>
                </td>
                <td>
                    <table>
                        <tr>
                            <td width="5%">                            
                                <asp:Image ID="ImgEvento" runat="server" 
                                    
                                    ImageUrl="../../librerianet/inscripcion/afiches/terminoscondiciones.png" /></td>   
                        </tr>       
                    </table>
                </td>
            </tr>
        </table> 
    </div>
    </form>
</body>
</html>
