<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistroInscripcion.aspx.vb" Inherits="Inscripcion_frmRegistroInscripcion" %>

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
            width: 25%;
        }
        .style2
        {
            width: 70%;
        }
        .style3
        {
            width: 25%;
            height: 25px;
        }
        .style4
        {
            color: #FF0000;
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>           
        <table>
            <tr>
                <td colspan="3"><asp:Label ID="lblCentroCosto" runat="server" Text="EVENTO:" 
                        Font-Size="Medium"></asp:Label></td>
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
                                    &nbsp;<asp:Label ID="lblcodigo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Apellidos y Nombres
                            </td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;<asp:Label ID="lblalumno" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Escuela Profesional</td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;<asp:Label ID="lblescuela" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Ciclo de Ingreso</td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;<asp:Label ID="lblcicloingreso" runat="server"></asp:Label>
                            </td>
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
                        <td width="69%">
                            &nbsp;</td>
                      </tr>
                      <tr>
                            <td class="style1">Precio por particpante:</td>
                            <td width="69%" class="usatTitulousat">
                            <asp:Label ID="txtPrecio" runat="server"></asp:Label>
                            </td>
                        <td width="69%">
                            &nbsp;</td>
                      </tr>
                      <tr>
                        <td class="style1">Cantidad</td>
                        <td width="69%">
                            <asp:TextBox ID="txtCantidad" runat="server" Width="89px" 
                                ValidationGroup="datos" MaxLength="1" Enabled="False">1</asp:TextBox>
                            <asp:DropDownList ID="ddlCantidad" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtCantidad" 
                                ErrorMessage="Ingrese una cantidad mayor o igual a 1" ValidationGroup="datos"></asp:RequiredFieldValidator>
                        </td>
                      </tr>
                      <tr>
                          <td class="style3">Partes</td>
                          <td width="69%" class="style4">
			                  <asp:DropDownList ID="cboPartes" runat="server">
                              </asp:DropDownList>
                                </td>
                      </tr>          
                      <tr class="style1">
                        <td>
                            <asp:Button ID="btnRegistrar0" runat="server" 
                                Text="Generar Cod. de Confirmación" Height="30px" 
                                Width="214px" CssClass="guardar_prp" ValidationGroup="datos" /></td>
                        <td width="69%" class="rojo">
                            &nbsp;<asp:Label ID="lblcodigoconfirmacion" runat="server" 
                                Text="Ingrese el código de autorización que le ha sido remitido a su cuenta de correo:" 
                                Visible="False"></asp:Label><asp:TextBox ID="txtCodigoConfirmacion" runat="server" MaxLength="20" 
                                Visible="False"></asp:TextBox><asp:Button ID="btnRegistrar" runat="server" Text="Confirmar" Height="30px" 
                                Width="100px" CssClass="guardar_prp" ValidationGroup="datos" 
                                Visible="False" />&nbsp;
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="noconforme1" 
                                Height="30px" Width="100px" />
                        </td>
                      </tr>
                    </table>
                   <br />
                <asp:GridView ID="gvDeudas" runat="server" Width="100%" 
                        AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField HeaderText="Fecha Vencimiento" DataField="Fecha" />
                        <asp:BoundField HeaderText="Servicio" DataField="descripcion_sco" />
                        <asp:BoundField HeaderText="Cargo (S/.)" DataField="montoTotal_Deu" />
                        <asp:BoundField HeaderText="Abono (S/.)" DataField="Abono" />
                        <asp:BoundField HeaderText="Saldo (S/.)" DataField="saldo_Deu" />
                    </Columns>
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
                                <asp:Image ID="ImgEvento" runat="server" Width="299px" Height="561px" /></td>   
                        </tr>       
                    </table>
                </td>
            </tr>
        </table>            
    </form>
</body>
</html>
