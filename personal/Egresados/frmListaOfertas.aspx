<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaOfertas.aspx.vb" Inherits="Egresado_frmListaOfertas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
    <style type="text/css">
        #fradetalle
        {
            height: 100%;
        }
    </style>
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
</head>
<body>
    <form id="form1" runat="server">
    
    
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
        <tr>
            <td colspan="2" style="width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #0099FF;" 
                height="40px" bgcolor="#E6E6FA">
            <asp:Label ID="lblTitulo" runat="server" Text="Ofertas Laborales" 
                Font-Bold="True" Font-Size="11pt"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
        </tr>
        <tr>
            <td style="width:12%">
                <asp:Label ID="Label1" runat="server" Text="Carrera Profesional:"></asp:Label></td>
            <td>
                <asp:DropDownList ID="dpCarrera" runat="server" Height="20px" Width="350px">
                </asp:DropDownList>                
                &nbsp;&nbsp;&nbsp;&nbsp; Estado &nbsp;&nbsp; 
                <asp:DropDownList ID="dpEstado" runat="server" Width="120px">                    
                    <asp:ListItem Value="A" Text="ACTIVA"></asp:ListItem>
                    <asp:ListItem Value="R" Text="POR REVISAR"></asp:ListItem>
                    <asp:ListItem Value="D" Text="DESACTIVADA"></asp:ListItem>                    
                    <asp:ListItem Value="E" Text="RECHAZADA"></asp:ListItem>
                </asp:DropDownList>&nbsp;&nbsp;
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="usatBuscar" 
                    Width="109px" Height="21px" />    
            </td>          
        </tr>
    </table>
    
    <br />
    <asp:Button ID="btnNuevo" runat="server" Text="Nueva Oferta" CssClass="agregar2" Width="110px" Height="22px" />
    &nbsp;
    <asp:Button ID="btnModificar" runat="server" Text="Modificar Oferta" CssClass="modificar2" Width="120px" Height="22px" />
    <br /><br />
    <asp:GridView ID="gvwOfertas" runat="server" Width="100%" DataKeyNames="codigo_ofe" 
        AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True">
        <Columns>
            <asp:BoundField DataField="codigo_ofe" HeaderText="codigo_ofe" 
                Visible="False" />                
            <asp:BoundField DataField="nombrePro" HeaderText="EMPRESA" >
                    <ItemStyle Width="23%" />
            </asp:BoundField>
            <asp:BoundField DataField="titulo_ofe" HeaderText="OFERTA" >
                <ItemStyle Width="20%" />
            </asp:BoundField>
            <asp:BoundField DataField="contacto_ofe" HeaderText="CONTACTO" >
                <ItemStyle Width="16%" />
            </asp:BoundField>
            <asp:BoundField DataField="telefonocontacto_ofe" HeaderText="TELEFONO" >
                <ItemStyle Width="7%" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="fechaReg_ofe" HeaderText="FECHA REG" >
                <ItemStyle Width="10%" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:CommandField EditText="Activar" HeaderText="ACTIVAR" 
                ShowEditButton="True" ButtonType="Image" 
                EditImageUrl="../../images/ok.gif" >
                <ItemStyle Width="8%" HorizontalAlign="Center" />
            </asp:CommandField>
            <asp:CommandField EditText="Eliminar" HeaderText="RECHAZAR" 
                ShowDeleteButton="True" ButtonType="Image" 
                DeleteImageUrl="../../images/eliminar.gif" DeleteText="" >
                <ItemStyle Width="8%" HorizontalAlign="Center" />
            </asp:CommandField>
            <asp:CommandField HeaderText="DETALLES" SelectText="Ver" 
                ShowSelectButton="True" ButtonType="Image" 
                SelectImageUrl="../../images/previo.gif" >                                
                <ItemStyle Width="8%" HorizontalAlign="Center" />
            </asp:CommandField>
        </Columns>
        <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
        <RowStyle Height="22px" />
    </asp:GridView>
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
            <td colspan="3" class="pestanarevez" style="height: 500px; width: 100%">
                <iframe id="fradetalle" width="100%" border="0" frameborder="0" runat="server" 
                    visible="False">
	            </iframe>  
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdCodigo_Ofe" runat="server" />
    </form>
</body>
</html>
