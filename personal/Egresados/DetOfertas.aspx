<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DetOfertas.aspx.vb" Inherits="Egresado_DetOfertas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>
	<link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
    <style type="text/css">
        .style2
        {
        }
        .style3
        {
            width: 139px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div> 
        <table width="100%">
            <tr style="height:25px">
                <td colspan="5" align="center">
                    <asp:LinkButton ID="LnkEmpresa" runat="server" Font-Bold="True"
                        Font-Size="small" Font-Underline="True" ForeColor="#006666"></asp:LinkButton>
                </td>                
            </tr>
            <tr style="height:20px">
                <td class="style2"><b>Título:</b></td>
                <td colspan="4">
                    <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label>
                </td>                
            </tr>
            <tr style="height:20px">
                <td class="style2"><b>Descripción:</b></td>
                <td colspan="4">
                    <asp:Label ID="lblDescripcion" runat="server" Text=""></asp:Label>
                </td>                
            </tr>
            <tr style="height:20px">
                <td class="style2"><b>Requisitos:</b></td>
                <td colspan="4">
                    <asp:Label ID="lblRequisitos" runat="server" Text=""></asp:Label>
                </td>                
            </tr>
            <tr style="height:20px">
                <td class="style2"><b>Departamento:</b></td>
                <td>
                    <asp:Label ID="lblDpto" runat="server"></asp:Label>
                </td>
                <td style="width:5%">&nbsp;</td>
                <td class="style3"><b>Lugar:</b></td>
                <td style="width:30%">
                    <asp:Label ID="lblLugar" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="height:20px">
                <td class="style2"><b>Tipo de Trabajo:</b></td>
                <td>
                    <asp:Label ID="lblTrabajo" runat="server" Text=""></asp:Label>
                </td>
                <td style="width:5%">&nbsp;</td>
                <td class="style3"><b>Duración:</b></td>
                <td style="width:30%">
                    <asp:Label ID="lblDuracion" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="height:20px">
                <td class="style2"><b>Contacto:</b></td>
                <td>
                    <asp:Label ID="lblContactos" runat="server" Text=""></asp:Label>
                </td>
                <td style="width:5%">&nbsp;</td>
                <td class="style3"><b>Teléfono:</b></td>
                <td style="width:30%">
                    <asp:Label ID="lblTelefono" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="height:20px">
                <td class="style2"><b>Correo</b></td>
                <td>
                    <asp:Label ID="lblCorreo" runat="server" Text=""></asp:Label>
                </td>
                <td style="width:5%">&nbsp;</td>
                <td class="style3"><b>Sector:</b></td>
                <td style="width:30%">
                    <asp:Label ID="lblSector" runat="server" Text=""></asp:Label>                    
                </td>
            </tr>
            <tr style="height:20px">
                <td class="style2"><b>Web:</b></td>
                <td>
                    <asp:Label ID="lblWeb" runat="server" Text=""></asp:Label>
                </td>
                <td style="width:5%">&nbsp;</td>
                <td class="style3"><b>Inicio:</b></td>
                <td style="width:30%">
                    <asp:Label ID="lblInicio" runat="server"></asp:Label>                    
                </td>
            </tr>
            <tr style="height:20px">
                <td class="style2"><b>Postular vía:</b></td>
                <td>
                    <asp:Label ID="lblmodo" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td class="style3"><b>Fin:</b></td>
                <td>
                    <asp:Label ID="lblFin" runat="server"></asp:Label>                    
                </td>
            </tr>
            <tr style="height:20px">
                <td class="style2"><b>Estado:</b></td>
                <td>
                    <asp:Label ID="lblEstado" runat="server"></asp:Label>                    
                </td>
                <td>&nbsp;</td>
                <td class="style3"><b>Visible:</b></td>
                <td>                    
                    <asp:Label ID="lblVisible" runat="server"></asp:Label>                    
                </td>
            </tr>
            <tr style="height:20px">
                <td class="style2" colspan="2"><b>Disponible para:</b></td>
                <td>&nbsp;</td>
                <td class="style3">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:GridView ID="gvwCarreras" runat="server" Width="50%" 
                        AutoGenerateColumns="False" GridLines="None" ShowHeader="False">
                        <Columns>
                            <asp:BoundField DataField="codigo_ofc" HeaderText="Cod. Det." Visible="False" >
                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_Cpf" HeaderText="Carrera Profesional" />
                        </Columns>
                        <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
                        <RowStyle Height="22px" />
                    </asp:GridView>
                </td>                
            </tr>
        </table>   
    </div>
    <asp:HiddenField ID="HdCodigo_ofe" runat="server" />
    <asp:HiddenField ID="HdCodigo_pro" runat="server" />
    </form>
</body>
</html>
