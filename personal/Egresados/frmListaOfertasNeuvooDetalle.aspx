<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaOfertasNeuvooDetalle.aspx.vb" Inherits="Egresados_frmListaOfertasNeuvooDetalle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>
	<link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
    <style type="text/css">
        .style3
        {
            width: 63px;
        }
        .style4
        {
            font-weight: bold;
            width: 120px;
        }
        .style5
        {
            width: 120px;
        }
        .style6
        {
            width: 5%;
        }
        .style7
        {
            width: 318px;
        }
        .style8
        {
            width: 202px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
    <div> 
        <table width="100%">
            <tr style="height:25px">
                <td colspan="6" align="center">
                    <asp:LinkButton ID="LnkEmpresa" runat="server" Font-Bold="True"
                        Font-Size="small" Font-Underline="True" ForeColor="#006666"></asp:LinkButton>
                </td>                
            </tr>
            <tr style="height:20px">
                <td class="style5"><b>Título:</b></td>
                <td colspan="5">
                    <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label>
                </td>                
            </tr>
            <tr style="height:20px">
                <td class="style5"><b>Descripción:</b></td>
                <td colspan="5">
                    <asp:Label ID="lblDescripcion" runat="server" Text=""></asp:Label>
                </td>                
            </tr>
            <tr style="height:20px">
                <td class="style5"><b>Departamento:</b></td>
                <td class="style7">
                    <asp:Label ID="lblDpto" runat="server"></asp:Label>
                </td>
                <td class="style6">&nbsp;</td>
                <td class="style3"><b>Lugar:</b></td>
                <td class="style8">
                    <asp:Label ID="lblLugar" runat="server" Text=""></asp:Label>
                </td>
                <td style="width:30%">
                    &nbsp;</td>
            </tr>
            <tr style="height:20px">
                <td class="style5"><b>Bolsa de trabajo:</b></td>
                <td class="style7">
                    <asp:Label ID="lblsource" runat="server"></asp:Label>
                </td>
                <td class="style6">&nbsp;</td>
                <td class="style3"><b>Inicio:</b></td>
                <td class="style8">
                    <asp:Label ID="lblInicio" runat="server"></asp:Label>                    
                </td>
                <td style="width:30%">
                    &nbsp;</td>
            </tr>
            <tr style="height:20px">
                <td class="style5">&nbsp;</td>
                <td class="style7">
                    <asp:Label ID="lblWeb1" runat="server"></asp:Label>
                </td>
                <td class="style6">&nbsp;</td>
                <td class="style3">&nbsp;</td>
                <td class="style8">
                    &nbsp;</td>
                <td style="width:30%">
                    &nbsp;</td>
            </tr>
            <tr style="height:20px">
                <td class="style4">Web:</td>
                <td colspan="5">
                    <asp:HyperLink ID="hyper_web" runat="server">HyperLink</asp:HyperLink>
                </td>
            </tr>
            </table>   
    </div>
    <asp:HiddenField ID="HdCodigo_ofe" runat="server" />
    </form>
</body>
</html>
