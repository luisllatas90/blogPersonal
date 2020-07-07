<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptConsultaGeneralPre.aspx.vb" Inherits="aulavirtual_adminaula_rptEstadisticaUso" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
  <title></title>
  
  
  
    
    <style type="text/css">
        body
        { font-family:Trebuchet MS;
          font-size:11px;
          cursor:hand;
          background-color:#F0F0F0;
          padding:25px;
        }
        
        .celda1
        {
            width: 20%;
            background:white;
            padding:10px;
            border:1px solid #808080;
            border-right:0px;
            color:#2F4F4F;
            font-weight:bold;
            
        }
        .celda2
        {
            width: 80%;
            background:white;
            padding:10px;
            border:1px solid #808080;
            border-left:0px;
            color:#2F4F4F;
            font-weight:bold;
        }
       .celda3
       {    width: 80%;
            background:white;
            padding:10px;
            border:1px solid #808080;                  
            color:#2F4F4F;
            font-weight:bold;
       }
       
       #celdaGrid
       {
          color:#5D7B9D;   padding:5px;font-weight:bold; font-style:italic;
       }
      .celdaGrid
       {
          color:#5D7B9D;   padding:5px;font-weight:bold; font-style:italic;
       }
       .titulo
       { 
           font-weight:bold; font-size: 10px; 
       }
       .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3 ; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:5px; 
       }
        .style1
        {
            border-right: 1px solid #808080;
            border-top: 1px solid #808080;
            border-bottom: 1px solid #808080;
            background: white;
            padding: 10px;
            border-left: 0px;
            color: #2F4F4F;
            font-weight: bold;
        }
        
        .sinborderTop
        { border-top:0px;
            
            }
            
                .sinborderBottom
        { border-bottom:0px;
            
            }
    </style>
  
</head>
<body>
    <form id="form1" runat="server">
    <h3>Consulta Participantes Escuela Pre</h3>
     <table runat="server" cellpadding="0" cellspacing="0" id="tabla">    
   
    <tr>       
        <td class="celda1 sinborderBottom"> Ciclo Ingreso:</td>
        <td class="style1 sinborderBottom"><asp:DropDownList ID="ddlCiclo" runat="server"></asp:DropDownList>
        </td>                      
    </tr>
    <tr>       
        <td class="celda1 sinborderTop"><asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="btn"  /></td>
        <td class="style1 sinborderTop"><asp:Button ID="btnExportar" runat="server" Text="Exportar"   CssClass="btn"  /></td>               
    </tr>
    </table>
    <table>
    <tr>
    <td> <br />
    <asp:GridView ID="GridView1" runat="server" CellPadding="3" ForeColor="#333333">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
         </asp:GridView>
    </td>
    </tr>
         
    </table>    
    </form>
</body>
</html>
