<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSolicitarAmbienteH.aspx.vb" Inherits="administrativo_pec_frmSolicitarAmbiente" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

<title></title>
     
    <style type="text/css">
         body
        { font-family:Trebuchet MS;
          font-size:11.5px;
          cursor:hand;
          background-color:white;	
        }
        .tit
        {
            background-color: #E8EEF7; font-weight: bold;  padding: 10px 10px 10px 0px;
            }
        .style1
        {
            color: #FF0000;
        }
             .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3 ; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px; 
       }
        .style2
        {
            width: 117px;
        }
    </style>
     
    </head>
<body>
    <form id="form1" runat="server">
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock" Text="Buscando ambientes disponibles..." Title="Por favor espere" />
    <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:70%" >
        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td colspan="7"><b>- Segundo Paso: Buscar Disponibilidad de Ambientes&nbsp;&nbsp;&nbsp; </b></td>
        </tr>
        <tr>
            <td colspan="6"><b>
                <asp:Label ID="Label1" runat="server" style="color: #0099CC" Text="Label" 
                    Visible="False"></asp:Label>
                </b></td>
            <td class="style2">&nbsp;</td>            
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
                <b>Distribución</b></td>
            <td>
                <b>Ventilación</b></td>
            <td class="style2">
                <b>Otros</b></td>
            
            <td>
                &nbsp;</td>
            
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlHorarios" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="ddlAudio" runat="server"></asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="ddlVideo" runat="server"></asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="ddlSillas" runat="server"></asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="ddlDis" runat="server"></asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="ddlVenti" runat="server"></asp:DropDownList>
            </td>
            <td class="style2"><asp:DropDownList ID="ddlOtros" runat="server"></asp:DropDownList>            
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <b>Tipo de Ambiente</b></td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td class="style2">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlTipoAmbiente" runat="server">
                </asp:DropDownList>
            </td>
            <td><asp:Button CssClass="btn" ID="Button1" runat="server" Text="Buscar" 
                    Width="79px" Height="25px" /></td>
            <td><asp:Button ID="btnCancelar0" Width="79px" Height="25px"
                    runat="server" CssClass="btn" 
                    Text="Regresar" />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td class="style2">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="8">
                <img alt="" longdesc="Aula" src="images/door.png" 
                    style="width: 16px; height: 16px" />Ambiente será asignado inmediatamente. 
                No para días domingo.<br />
                <img alt="" longdesc="Preferencial" src="images/star.png" 
                    style="width: 16px; height: 16px" />Ambiente será asignado según 
                disponibilidad de ambiente. (Máx en 48h)
                (<span class="style1">*</span>) Se listan los horarios registrados que no tienen 
                ambiente asignado.</td>
        </tr>
        </table>
        <br />
        <table  cellpadding="1" cellspacing="0" style="border: 1px solid #C2CFF1; width:70%">
        <tr  style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td>                
            <b>Listado de ambientes según filtros de búsqueda </b>
            </td>            
        </tr>
        <tr>
            <td><br />
                <asp:GridView ID="gridAmbientes" runat="server" BorderStyle="solid" 
                    CellPadding="2" GridLines="both" BackColor="White" BorderColor="#C2CFF1" 
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
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
         </table>    
    </form>
</body>
</html>
