<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmModificarHorasLectivas.aspx.vb" Inherits="personal_frmModificarHorasLectivas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modificar Horas Lectivas</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>
	<link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
	<script src="../../private/funciones.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td align="center">
                    &nbsp;<asp:Label ID="Label5" runat="server" Text="Modificar Horas Lectivas" 
                        Font-Bold="True" Font-Size="Small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td style="width:60px">
                             
                                            <asp:Label ID="Label2" runat="server" Text="DIA"></asp:Label>
                             
                                        </td>
                                        <td style="width:150px">
                                        
                                            <asp:Label ID="lblDIA" runat="server" ForeColor="#0000CC" Text="[------]"></asp:Label>
                                        
                                        </td>
                                        
                                        <td style="width:110px">
                                            <asp:Label ID="Label6" runat="server" Text="Hras Lectivas"></asp:Label>
                                        </td>
                                        <td align="center" style="width:60px" >
                                            <asp:Label ID="lblHLH" runat="server" Text="0" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td style="width:110px">
                                            <asp:Label ID="Label7" runat="server" Text="Hras Academicas"></asp:Label>
                                        </td>
                                        <td align="center" style="width:60px">
                                            <asp:Label ID="lblHCA" runat="server" Text="0" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td></td>
                                    </table>
                                    <table>
                                    </tr>
                                    <tr>
                                        <td style="width:100px">
                                            <asp:Label ID="Label3" runat="server" Text="Hra Inicio"></asp:Label>
                                        </td>
                                 <td>
                                     <asp:DropDownList ID="ddlHoraIni" runat="server">
                                    <asp:ListItem Value="--Seleccionar--">--Seleccionar--</asp:ListItem>
                                    </asp:DropDownList>
                                
                                 </td>
                              
                                <td style="width:150px">
                                
                                        <asp:Label ID="Label4" runat="server" Text="Hra Fin"></asp:Label>
                                
                                        </td>
                                        <td>
                                
                                <asp:DropDownList ID="ddlHoraFin" runat="server">
                                    <asp:ListItem>--Seleccionar--</asp:ListItem>
                                </asp:DropDownList>
                                
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="btnAgregar" runat="server" CssClass="agregar2" 
                                                Text="  Guardar" Font-Bold="True" />
                                        </td>
                                        <td>
                                        
                    <input id="cmdCancelar" type="button" value="    Cerrar" onclick="self.parent.tb_remove()" class="salir" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblMensaje" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td></td>
                            <td>
                             
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                
                                &nbsp;</td>
                            <td>
                                
                                &nbsp;</td>
                            <td>
                                
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                    <asp:GridView ID="gvHrsLista" runat="server" AutoGenerateColumns="False"  Width="100%" 
                                        BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                                        CellPadding="4" Font-Size="Smaller" GridLines="Horizontal" 
                                        DataKeyNames="codigo_hop">
                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                        <Columns>
                                            <asp:BoundField DataField="codigo_hop" HeaderText="CODIGO"  >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="diahop" HeaderText="DIA" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="horainicio_hop" HeaderText="H.INICIO" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="horafin_hop" HeaderText="H.FIN" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" ButtonType="Image" 
                                                SelectImageUrl="../../images/ok.gif" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:CommandField>
                                            <asp:CommandField DeleteImageUrl="../../images/eliminar.gif" DeleteText="" 
                                                ShowDeleteButton="True" ButtonType="Image" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:CommandField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                            </td>
                            <td>    
                                <table>
                                    <tr>
                                        <td valign="top"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
          </tr>
        </table>
    </div>
    </form>
</body>
</html>
