<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaOfertasNeuvoo.aspx.vb" Inherits="Egresados_frmListaOfertasNeuvoo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

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
select {
	font-family: Verdana;
	font-size: 8.5pt;
}
.usatBuscar {
	border: 1px solid #C0C0C0;
	background: #FEFFE1 url('../../images/previo.gif') no-repeat 0% 80%;
	font-family: Tahoma;
	font-size: 8pt;
	font-weight: bold;
	height: 25;
}
.agregar2 {
	border: 1px solid #666666;
	background: #FEFFE1 url('../../images/anadir.gif') no-repeat 0% center;
	width: 80;
	font-family: Tahoma;
	font-size: 8pt;
	height: 20;
	cursor: hand;
}
.modificar2 {
	border: 1px solid #666666;
	background: #FEFFE1 url('../../images/editar.gif') no-repeat 0% center;
	width: 80;
	font-family: Tahoma;
	font-size: 8pt;
	height: 20;
	cursor: hand;
}
a:Link {
	color: #000000;
	text-decoration: none;
}
.pestanabloqueada {
	border: 1px solid #808080;
	background-color: #E1F1FB;
	cursor: hand;
}
.pestanarevez {
	border-left: 1px solid #808080;
	border-right: 1px solid #808080;
	border-top-width: 1;
	border-bottom: 1px solid #808080;
}
        #fradetalle
        {
            height: 100%;
        }
        .style1
        {
            width: 10%;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
    
    
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
        <tr>
            <td colspan="2" style="width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #0099FF;" 
                height="40px" bgcolor="#E6E6FA">
            <asp:Label ID="lblTitulo" runat="server" Text="Ofertas Laborales Neuvoo" 
                Font-Bold="True" Font-Size="11pt"></asp:Label> </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label2" runat="server" Text="Oferta Laboral:"></asp:Label></td>
            <td>
                <asp:TextBox ID="txt_tituloTrabajo" runat="server" Width="603px"></asp:TextBox>
            </td>          
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label3" runat="server" Text="Pais:"></asp:Label></td>
            <td>
                <asp:DropDownList ID="dpPais" runat="server" Width="173px" Height="20px">                    
                </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label5" runat="server" Text="Ubicación:"></asp:Label>&nbsp;<asp:TextBox 
                    ID="txt_ubicacion" runat="server" Width="325px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="usatBuscar" 
                    Width="109px" Height="21px" />    
            </td>          
        </tr>
    </table>
    
    <br />
    &nbsp;
    <asp:Button ID="btnModificar" runat="server" Text="Modificar Oferta" 
        CssClass="modificar2" Width="130px" Height="22px" />
    &nbsp;&nbsp;
            <br />
    <br />
                <asp:Label ID="lbl_msgbox" runat="server" Font-Bold="True" 
        ForeColor="Red"></asp:Label>
    <asp:GridView ID="gvwNeuvoo" runat="server" Width="100%" DataKeyNames="jobkey" 
        AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True">
        <Columns>
            <asp:BoundField DataField="jobkey" HeaderText="ID" Visible="False" />
            <asp:BoundField DataField="company" HeaderText="EMPRESA">
            <ItemStyle Width="20%" />
            </asp:BoundField>
            <asp:BoundField DataField="jobtitle" HeaderText="OFERTA">
            <ItemStyle Width="28%" />
            </asp:BoundField>
            <asp:BoundField DataField="formattedLocation" HeaderText="UBICACIÓN">
            <ItemStyle Width="18%" />
            </asp:BoundField>
            <asp:BoundField DataField="source" HeaderText="BOLSA">
            <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="datetime" HeaderText="FECHA Y HORA" />
            <asp:CommandField ButtonType="Image" HeaderText="DETALLES" 
                SelectImageUrl="../../images/previo.gif" SelectText="Ver" 
                ShowSelectButton="True">
            <ItemStyle HorizontalAlign="Center" Width="8%" />
            </asp:CommandField>
        </Columns>
        <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
        <RowStyle Height="22px" />
    </asp:GridView>
    <br />
    <br />
    <table cellspacing="0" cellpadding="0" style="border-collapse: collapse; bordercolor: #111111;width:100%">
        <tr>
            <td class="pestanabloqueada" id="tab" align="center" style="height:25px;width:100%" onclick="ResaltarPestana2('0','','');">
                <asp:LinkButton ID="lnkDetalles" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue" Font-Size="Small">Detalles de Oferta</asp:LinkButton></td>
           <!-- <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
            <td class="pestanabloqueada" id="tab" align="center" style="height:25px;width:49.5%" onclick="ResaltarPestana2('0','','');">
                <asp:LinkButton ID="lnkAlumnos" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue" Font-Size="Small">Egresados</asp:LinkButton></td>-->
        </tr>
        <tr>
            <td class="pestanarevez" style="height: 500px; width: 100%">
                <iframe id="fradetalle" width="100%" border="0" frameborder="0" runat="server" 
                    visible="False">
	            </iframe>  
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="Hdjobkey" runat="server" />
    </form>
</body>
</html>
