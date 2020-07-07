<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSubirResolucion.aspx.vb" Inherits="investigacion_frmAgregarEditarComite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        body
        {
           /* background: #eef0f6;*/
            font-family: Verdana, helvetica, arial, sans-serif;
	        font-size: 68.75%;
         }
         table 
         { color:#203360;
             }
         th 
          { text-align:center;
                                
                 }
            
        .style2
        {
            height: 6px;
        }
        .style3
        {
            height: 7px;
        }
        .btn
        {
            background-color:#203360;
            border-left:1px solid #FFFFFF;
            border-right:1px solid #FFFFFF;
            color:white;
            text-align:center;
            vertical-align:middle;
            height: 25px;
            width: 80px;
            cursor:hand;
       }
       
        .style4
        {
            height: 25px;
        }
       
    .guardar_prp {
	border: 0px solid #C0C0C0;
	background: #f0f0f0 url('../images/menus/guardar_1.gif') no-repeat 0% 80%;
	font-family: Tahoma;
	font-size: 8pt;
	font-weight: bold;
	height: 35;
	cursor: hand;
            text-align: center;
        }
       
        .style1
        {
            text-align: center;
        }
       
        .style5
        {
            width: 342px;
        }
        .style6
        {
            height: 6px;
            width: 342px;
        }
       
    </style>
</head>
<body>
     <form id="form1" runat="server">
            <table style="border:#c2cff1 1px solid;" cellpadding=0px;>
                <tr>
                    <td colspan="2" style=" background:#d1ddef;color:#2f4f4f" class="style4">
                        <b>Registrar Resolución de Investigación</b></td>
                    
                </tr>
                <tr>
                    <td colspan="2" class="style3">
                        &nbsp;</td>
                    
                </tr>
                <tr>
                    <td>
                        Número de Resolución</td>
                    <td class="style5">
                        
                        <asp:TextBox ID="txtNroResolucion" runat="server" 
                            Width="123px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Adjuntar Archivo</td>
                    <td class="style5">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                       
                </tr>
                <tr>
                    <td class="style2">&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    <td class="style6">
                        </td>
                </tr>
                <tr>
                    <td class="style1" colspan="2">
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btbGuardar" runat="server" Text="       Guardar" CssClass="guardar_prp" Height="35px" Width="100px"  />
                     &nbsp;&nbsp;&nbsp;&nbsp;
                  <!--      <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn"/>-->
                  
                    </td>
                </tr>
            </table>       
    </form>
</body>
</html>

