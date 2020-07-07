<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptPostulaciones.aspx.vb" Inherits="Egresados_rptPostulaciones" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style>
     body
        { font-family:Trebuchet MS;
          font-size:11.5px;
          cursor:hand;
          background-color:white;	
        }
     .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3 ; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px; 
       }
    TBODY {
	display: table-row-group;
}
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock" Text="Su solicitud esta siendo procesada..." Title="Por favor espere" />
     <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%" >
     <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td colspan="7"><h3>Reporte de Postulaciones</h3></td>            
        </tr>
        <tr>
        
        <td><asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="btn" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnExportar" runat="server" Text="Exportar" CssClass="btn" /></td>
        </tr>
    
        
      
   
    <tr><td><br />
    
   </td>
    
    <tr><td>
        <asp:GridView ID="gwData" runat="server" AutoGenerateColumns="False" 
                CaptionAlign="Top" BorderStyle="None" CellPadding="4" 
             AlternatingRowStyle-BackColor="#F7F6F4"  >
              <RowStyle BorderColor="#C2CFF1" />
            <Columns>
                <asp:BoundField DataField="orden" HeaderText="# ORDEN" />
                <asp:BoundField DataField="fechaReg_ofe" HeaderText="FECHA PUBLICACIÓN" />
                <asp:BoundField DataField="nombrePro" HeaderText="EMPRESA" />
                <asp:BoundField DataField="rucPro" HeaderText="RUC" />
                <asp:BoundField DataField="correocontacto_ofe" HeaderText="CORREO CONTACTO" />
                <asp:BoundField DataField="titulo_ofe" HeaderText="PUESTO" />
                <asp:BoundField DataField="fechaInicioAnuncio" HeaderText="FECHA INICIO" />
                <asp:BoundField DataField="fechaFinAnuncio" HeaderText="FECHA DE FIN" />
                <asp:BoundField DataField="lugar_ofe" HeaderText="CIUDAD" />
                <asp:BoundField DataField="nro" HeaderText="# POSTULACIONES" >
                <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
                        <EmptyDataTemplate>
                 <div style="color:#3266DB; background-color:#E8EEF7; padding:5px; font-style:italic;">
                     No se encontraron registros.
                 </div>
             </EmptyDataTemplate>
             <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#3366CC" />
             <AlternatingRowStyle BackColor="#F7F6F4" />
        </asp:GridView>
    </td></tr>
 </table>
    </form>
</body>
</html>
