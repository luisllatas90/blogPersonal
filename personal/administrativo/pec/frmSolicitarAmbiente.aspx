<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSolicitarAmbiente.aspx.vb" Inherits="administrativo_pec_frmSolicitarAmbiente" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
<title></title>
     
    <style type="text/css">
       
        .tit
        {
            background-color: #E8EEF7; font-weight: bold;  padding: 10px 10px 10px 0px;
            }
        .style1
        {
            color: #FF0000;
        }
    </style>
     
    </head>
<body>
    <form id="form1" runat="server">
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock" Text="Su solicitud esta siendo procesada..." Title="Por favor espere" />
    <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:70%" >
        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td colspan="7"><b>Buscar Disponibilidad de Ambientes: </b></td>
            
        </tr>
        <tr>
            <td colspan="6"><b>
                <asp:Label ID="Label1" runat="server" style="color: #0099CC" Text="Label"></asp:Label>
                </b></td>
            <td>&nbsp;</td>            
        </tr>
        <tr>
            <td><b>Fecha <span class="style1">*</span></b></td>
            <td>
                <b>Audio</b></td>
            
            <td>
                <b>Video</b></td>
            
            <td>
                <b>Sillas</b></td>
            
            <td>
                <b>Distribucion</b></td>
            <td>
                <b>Ventilación</b></td>
            <td>
                <b>Otros</b></td>
            
            <td>
                &nbsp;</td>
            
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlHorarios" runat="server">
                </asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="ddlAudio" runat="server"></asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="ddlVideo" runat="server"></asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="ddlSillas" runat="server"></asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="ddlDis" runat="server"></asp:DropDownList>
            
            <td><asp:DropDownList ID="ddlVenti" runat="server"></asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="ddlOtros" runat="server"></asp:DropDownList>
            </td>
            <td><asp:Button CssClass="buscar2" ID="Button1" runat="server" style="height: 26px" Text="Buscar" Width="79px" /></td>
        </tr>
        <tr>
            <td colspan="7">
                <img alt="" longdesc="Aula" src="image/door.png" 
                    style="width: 16px; height: 16px" />Ambiente será asignado inmediatamente.<br />
                <img alt="" longdesc="Preferencial" src="image/star.png" 
                    style="width: 16px; height: 16px" />Ambiente será asignado según 
                disponibilidad de ambiente. (Máx en 48h)<br />
                (<span class="style1">*</span>) Se listan los horarios registrados que no tienen 
                ambiente asignado.</td>
        </tr>
        </table>
        <br />
        <table  cellpadding="1" cellspacing="0" style="border: 1px solid #C2CFF1; width:70%">
        <tr  style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td colspan="6">                
            <b>Listado de ambientes según filtros de búsqueda </b>
            </td>            
        </tr>
        <tr>
            <td colspan="6"><br />
                <asp:GridView ID="gridAmbientes" runat="server" BorderStyle="solid" 
                    CellPadding="3" GridLines="both" BackColor="White" BorderColor="#C2CFF1" 
                    BorderWidth="1px" Width="65%" DataKeyNames="accion"  >
                    <RowStyle BorderColor="#C2CFF1" HorizontalAlign="Center" BackColor="White" ForeColor="#333333" Font-Size="X-Small" />
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <PagerStyle BackColor="#C2CFF1" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C2CFF1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#E8EEF7" Font-Bold="True" ForeColor="#587ECB" />
                    <EmptyDataTemplate>
                    <div><i>No se encontraron ambientes.</i></div>
                    </EmptyDataTemplate>
               </asp:GridView>
            </td>            
        </tr>
        <tr>
            <td colspan="6"></td>
        </tr>
        <tr>
            <td colspan="6">&nbsp;</td>
        </tr>
         <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td colspan="6">                
            <b>También puedes solicitar</b>
            </td>            
        </tr>
        <tr>
            <td colspan="6"><br />
                <asp:GridView ID="gridotrosAmbientes" runat="server" BorderStyle="solid" 
                    CellPadding="3" GridLines="both" BackColor="White" BorderColor="#C2CFF1" 
                    BorderWidth="1px" Width="65%" DataKeyNames="accion"  >
                    <RowStyle BorderColor="#C2CFF1" HorizontalAlign="Center" BackColor="White" ForeColor="#333333" Font-Size="X-Small" />
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <PagerStyle BackColor="#C2CFF1" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C2CFF1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#E8EEF7" Font-Bold="True" ForeColor="#587ECB" />
                    <EmptyDataTemplate>
                    <div><i>No se encontraron ambientes.</i></div>
                    </EmptyDataTemplate>
               </asp:GridView>
            </td>            
        </tr>
    </table>    
    </form>
</body>
</html>
