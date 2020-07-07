<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistroInscripcionKermes.aspx.vb" Inherits="Inscripcion_frmRegistroInscripcion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inscripcion Alumno</title>
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
        .style1
        {
        }
        .style2
        {
            width: 70%;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>           
        <table>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblCentroCosto" runat="server" Text="EVENTO:" 
                        Font-Size="Medium" Font-Bold="True" ForeColor="#CC0000"></asp:Label></td>
            </tr>
            <tr>
                <td class="style2">
                    <table id="tblDatos" border="0" bordercolor="#111111" cellpadding="3" 
                        cellspacing="0" class="contornotabla" width="100%">
                        <tr>
                            <td rowspan="5" valign="top" width="10%">
                                <asp:Image ID="FotoAlumno" runat="server" Height="104px" Width="90px" />
                                <asp:HiddenField ID="hddcodigo_alu" runat="server" />
                            </td>
                            <td width="15%">
                                Código Universitario
                            </td>
                            <td class="usatsubtitulousuario" width="70%">
                                    &nbsp;<asp:Label ID="lblcodigo" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Apellidos y Nombres
                            </td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;<asp:Label ID="lblalumno" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Escuela Profesional</td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;<asp:Label ID="lblescuela" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Ciclo de Ingreso</td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;<asp:Label ID="lblcicloingreso" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Plan de Estudios</td>
                            <td class="usatsubtitulousuario" width="70%">
                            &nbsp;<asp:Label ID="lblPlan" runat="server"></asp:Label></td>
                        </tr>                
                    </table>
                    <br />
                    <table cellpadding="3" cellspacing="0" style="border-collapse: collapse; " bordercolor="#111111" width="100%" class="contornotabla">    
                        <tr>
                            <td width="100%" colspan="2" class="etabla" style="text-align: left" height="13">Datos de Inscripci&oacute;n</td>
                        </tr>
                      <tr>
                            <td class="style1">Servicio:</td>
                            <td width="69%" class="usatTitulousat">
                                <asp:DropDownList ID="cboServicio" 
                                runat="server" AutoPostBack="True">
                                </asp:DropDownList>  </td>
                      </tr>
                      <tr>
                        <td class="style1">Cantidad</td>
                        <td width="69%">
                            <asp:DropDownList ID="ddlCantidad" runat="server" AutoPostBack="True">
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
                        </td>
                      </tr>
                      <tr>
                        <td class="style1">Importe:</td>
                        <td width="69%">
                            <asp:Label ID="txtPrecio" runat="server" Font-Bold="True" Font-Size="Large" 
                                ForeColor="Red">S/. 25.00</asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td class="style1" colspan="2">
                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                                ForeColor="Blue" 
                                Text="La venta de tarjetas se realizará sólo a través del campus virtual y no habrá venta de entradas el mismo día del evento. El importe por la compra será descontada en una sola cuota en el mes de octubre sea por pensión académica o planilla, según corresponda. Una vez realizado el proceso de compra por campus virtual no habrá devoluciones de tarjetas, cancelación del proceso o devolución de dinero." 
                                TextAlign="Left" />
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Red" 
                                Text="ACEPTO LOS TÉRMINOS Y CONDICIONES"></asp:Label>
                          </td>
                      </tr>
                      <tr class="style1">
                        <td>
                            &nbsp;</td>
                        <td width="69%" class="rojo">
                            &nbsp;<asp:Button ID="btnRegistrar" runat="server" Text="         Registrar" Height="30px" 
                                Width="100px" CssClass="guardar_prp" ValidationGroup="datos" 
                                Enabled="False" />&nbsp;
                            <asp:Button ID="btnCancelar" runat="server" Text="  Cancelar" CssClass="noconforme1" 
                                Height="30px" Width="100px" />
                        </td>
                      </tr>
                    </table>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#CC0000" 
                        Text="Entradas adquiridas: "></asp:Label>
                    <asp:Label ID="lblEntradas" runat="server" Font-Bold="True" Font-Size="Large" 
                        ForeColor="Blue" Text="0"></asp:Label>
                   <br />
                <asp:GridView ID="gvDeudas" runat="server" Width="100%" 
                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                        GridLines="None">
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField HeaderText="Fecha Vencimiento" 
                            DataField="fechavencimiento_Deu" />
                        <asp:BoundField HeaderText="Servicio" DataField="descripcion_sco" />
                        <asp:BoundField HeaderText="Cargo (S/.)" DataField="montoTotal_Deu" />
                        <asp:BoundField HeaderText="Abono (S/.)" DataField="Abono" />
                        <asp:BoundField HeaderText="Saldo (S/.)" DataField="saldo_Deu" />
                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ValidationGroup="datos" />
                <br />
                <asp:HiddenField ID="Hdcodigo_alu" runat="server" />
                <asp:HiddenField ID="Hdcodigo_cac" runat="server" />      
                <asp:HiddenField ID="Hdcodigo_cco" runat="server" />   
                </div>
                </td>
                <td>
                    <table>
                        <tr>
                            <td width="5%">                            
                                <asp:Image ID="ImgEvento" runat="server" Width="299px" Height="352px" /></td>   
                        </tr>       
                    </table>
                </td>
            </tr>
        </table>            
    </form>
</body>
</html>
