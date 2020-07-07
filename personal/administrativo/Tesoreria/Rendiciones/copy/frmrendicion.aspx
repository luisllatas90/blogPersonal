<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmrendicion.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<script src="funciones.js"></script>
<link  rel ="stylesheet" href="estilo.css"/> 
<title>Tesoreria  USAT</title> 
    
<script language="javascript" type="text/javascript">

function MostrarDetalleRendicion(codigo_rend)
    {
        window.open("frmdetallerendicion.aspx?codigo_rend=" + codigo_rend,'ta',"toolbar=no,width=1200,height=750,scrollbars=true");
    }
</script>
</head>
<body style="color: #000000"  bgcolor="#EEEEEE">
<script language="javascript"  > 
   
</script>
    <form id="form1" runat="server" >    
        
        <table >
            <tr>
                <td  colspan="2" style="height: 15px"  class="usatCeldaTitulo">
                    
                    Rendiciones</td>
            </tr>
            <tr class="etabla">
                <td style="height: 21px" class ="usatCeldaTitulo">
                    Estado de Rendición :</td>
                <td style="width: 694px; height: 21px;"  align=left class="usatCeldaTitulo">
                    <asp:DropDownList ID="cboestado" runat="server" Width="320px" AutoPostBack="True"k="Tt-Names="Courier New" Font-Size="10pt">
                        <asp:ListItem Value="P">Pendientes</asp:ListItem>
                        <asp:ListItem Value="F">Finalizadas</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblmensaje" runat="server" Width="336px"></asp:Label></td>
            </tr>
            
        <tr>
        </tr>
        </table>
        
        
        
        <table width ="100%" bgcolor ="white"  height="100%">
            <tr  height="800px">
                <td style="height: 55px" valign=top>
        
                    <asp:GridView ID="lstinformacion" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            Font-Names="Courier New" Font-Size="10pt" Height="100%" ShowFooter="True" Style="background-repeat: no-repeat; text-align: left;  vertical-align: top"  Width="100%" BorderColor="Silver" BorderStyle="Solid">
            <Columns>
                <asp:BoundField DataField="codigo_rend">
                    <HeaderStyle CssClass="usatCeldaTitulo" />
                    <ControlStyle Font-Names="Courier New" Font-Size="10pt"/>
                    
                </asp:BoundField>
                <asp:BoundField DataField="descripcion_rub" HeaderText="Concepto">
                    <HeaderStyle CssClass="usatCeldaTitulo" />
                    <ControlStyle Font-Names="Courier New" />
                    <ItemStyle Font-Names="Courier New" Font-Size="10pt"/>
                    
                </asp:BoundField>
                <asp:BoundField DataField="descripcion_tdo" HeaderText="Tipo Doc"  >
                    <HeaderStyle  CssClass="usatCeldaTitulo"   />
                    <ItemStyle Font-Names="Courier New" Font-Size="10pt" />
                    
                </asp:BoundField>                
                <asp:BoundField DataField="documento" HeaderText="N&#186; Documento" >
                    <HeaderStyle CssClass="usatCeldaTitulo" />
                    <ItemStyle Font-Names="Courier New" Font-Size="10pt"/>
                    
                </asp:BoundField>
                <asp:BoundField DataField="importe_deg" HeaderText="Monto a Rendir">
                <HeaderStyle CssClass="usatCeldaTitulo" />
                    <ItemStyle Font-Names="Courier New" Font-Size="10pt"/>
                    
                </asp:BoundField>
                <asp:BoundField DataField="descripcion_tip" HeaderText="Moneda" >
                    <HeaderStyle CssClass="usatCeldaTitulo" />                    
                    <ItemStyle Font-Names="Courier New" Font-Size="10pt"/>
                </asp:BoundField>
                <asp:BoundField DataField="estado" HeaderText="Estado" >
                    <HeaderStyle CssClass="usatCeldaTitulo" />
                    <ItemStyle Font-Names="Courier New" Font-Size="10pt"/>
                    
                </asp:BoundField>
                <asp:BoundField DataField="fechagen_egr" HeaderText="Fecha" >
                <HeaderStyle CssClass="usatCeldaTitulo" />
                    <ItemStyle Font-Names="Courier New" Font-Size="10pt"/>
                    
                </asp:BoundField>
                <asp:BoundField DataField="usuarioreg_egr" HeaderText="Usuario">
                       <HeaderStyle CssClass="usatCeldaTitulo" />
                    <ItemStyle Font-Names="Courier New" Font-Size="10pt"/>
                    
                </asp:BoundField>
                
                <asp:BoundField DataField="descripcion_rend" HeaderText="Descripci&#243;n">                    
                    <HeaderStyle CssClass="usatCeldaTitulo" />
                    <ItemStyle Font-Names="Courier New" Font-Size="10pt"/>
                    
                </asp:BoundField>
                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/corregir.gif" SelectText="">                    
                    
                </asp:CommandField>
            </Columns>
            <RowStyle BorderStyle="Solid" />
        </asp:GridView>
        
                </td>
            </tr>
        </table>
        
    
    </form>
</body>
</html>
