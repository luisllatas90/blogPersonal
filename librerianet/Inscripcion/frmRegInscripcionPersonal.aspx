<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegInscripcionPersonal.aspx.vb" Inherits="Inscripcion_frmRegInscripcionPersonal" %>

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
        .style1
        {
            width: 25%;
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
                <td colspan="3"><asp:Label ID="lblCentroCosto" runat="server" Text="EVENTO:" 
                        Font-Size="Medium"></asp:Label></td>
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
                    <table cellpadding="3" cellspacing="0" style="border-collapse: collapse; " bordercolor="#111111" width="100%" class="contornotabla">    
                        <tr>
                            <td width="100%" colspan="2" class="etabla" style="text-align: left" height="13">Datos de Inscripci&oacute;n</td>
                        </tr>
                      <tr>
                            <td class="style1">Servicio:</td>
                            <td width="69%" class="usatTitulousat">
                                <asp:DropDownList ID="cboServicio" 
                                runat="server" AutoPostBack="True">
                                </asp:DropDownList>               
                            </td>
                      </tr>
                      <tr>
                        <td class="style1">Precio:</td>
                        <td width="69%">
                            <asp:TextBox ID="txtPrecio" runat="server" Width="89px" 
                                Enabled="False"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                          <td class="style1">Partes</td>
                          <td width="69%" class="rojo">
			                  <asp:DropDownList ID="cboPartes" runat="server">
                              </asp:DropDownList>
                                </td>
                      </tr>          
                      <tr class="style1">
                        <td>&nbsp;</td>
                        <td width="69%" class="rojo">
                            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" Height="30px" 
                                Width="100px" CssClass="guardar_prp" />&nbsp;
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="noconforme1" 
                                Height="30px" Width="100px" />
                        </td>
                      </tr>
                    </table>
                   <br />
                <asp:GridView ID="gvDeudas" runat="server" Width="100%" 
                        AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField HeaderText="Fecha Registro" DataField="Fecha" />
                        <asp:BoundField HeaderText="Servicio" DataField="descripcion_sco" />
                        <asp:BoundField HeaderText="Cargo (S/.)" DataField="montoTotal_Deu" />
                        <asp:BoundField HeaderText="Abono (S/.)" DataField="Abono" />
                        <asp:BoundField HeaderText="Saldo (S/.)" DataField="saldo_Deu" />
                    </Columns>
                </asp:GridView>
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
                                <asp:Image ID="ImgEvento" runat="server" Width="299px" Height="561px" /></td>   
                        </tr>       
                    </table>
                </td>
            </tr>
        </table> 
    </div>
    </form>
</body>
</html>
