<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OfertaLaboralPostular.aspx.vb" Inherits="Egresado_campus_OfertaLaboralPostular" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
 body {
            color: #2F4F4F;
            font-family: Verdana;
            font-size: 11px;
            padding-left:5px;
            padding-top:10px;                      
    }           
      
      .btn 
     {
        padding:4px;
        font-weight:bold;
        font-size :12px;   
        text-decoration:none;
        border:1px solid #C2C2C2;
        color:Black;
        cursor:hand;
     }          
      .style1
      {
            font-weight: bold;
            width: 53px;
        }
            .filaTitulo
     {
         color:White; background:#e33439; font-size:14px;padding:3px;
     }
            .style2
        {
            width: 53px;
        }
            </style>
</head>
<body>
    <form id="form1" runat="server">
           
         <table style="width: 100%" class="" id="" cellpadding="4" style="">  
            <tr><td class="filaTitulo" colspan="3">&nbsp;Postular a Oferta Laboral</td></tr>	  
              <tr><td class="style2"><br/></td></tr>
              <tr>
                <td class="style1">
                  <span class="hdetalle">De </span>
                </td>
                
                <td>
                  <asp:Label ID="lblDe" runat="server" CssClass="style7" BorderStyle="None"></asp:Label>
                    <asp:Label ID="lblCorreo" runat="server" Text="Label" Visible="False"></asp:Label>
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
              </tr>
             
              <tr>
                <td class="style1"> 
                  <span class="hdetalle">Para </span>
                </td>
                <td>
                  <asp:Label ID="lblPara" runat="server" CssClass="style7" Text=""></asp:Label>
                    <asp:Label ID="lblCorreoPara" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
              </tr>
              <tr>
                <td class="style1">
                  <span class="hdetalle">Asunto </span>
                </td>
                <td>
                    <asp:Label ID="lblNombreOferta" runat="server" 
                        CssClass="style7" Visible="False"></asp:Label>
                    <asp:Label ID="lblNombreOferta2" runat="server" 
                        CssClass="style7"></asp:Label>
                </td>
              </tr>
              <tr>
                <td class="style1">
                  <span class="hdetalle">Mensaje </span>
                </td>
                <td>
                  
                  <asp:TextBox ID="txtMensaje" TextMode="MultiLine" runat="server" Height="65px" 
                        Width="400px"></asp:TextBox>
                </td>
              </tr>
             
              <tr>
                <td class="style1">
                
                    &nbsp;<td>
                    <asp:HyperLink ID="lblCvOnLine" runat="server" Target="_blank">Ver CV Online</asp:HyperLink>
                </td>
              </tr>
               <tr>
              <td class="style2">
                      <br />
              </td>
              <td>
                    <asp:Button ID="btnEnviar0" runat="server" Text="Postular" CssClass="btn" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnEnviar" runat="server" Text="Cancelar" CssClass="btn"  />
              </td>
              </tr>
               <tr>
              <td colspan="2">
                  <asp:Label ID="lblAviso" runat="server" 
                      
                      
                      style="color: #CC0000; font-family: 'Trebuchet MS'; font-size: small; background-color: #FFFFFF;"></asp:Label>
                      <asp:HiddenField ID="hdOfeCode" runat="server" />
              &nbsp;</td>
              </tr>
              <tr>
              <td colspan=2 style="border-bottom:1px solid #e33439;">&nbsp;</td>
              </tr>
              </table>                                      
           
    <div>
    
    </div>
    </form>
</body>
</html>
