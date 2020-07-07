<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmModificarHorasDocenteCarga.aspx.vb" Inherits="academico_cargalectiva_administrarcargalectiva_frmModificarHorasDocenteCarga" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../../librerianet/private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../../../../librerianet/private/estiloweb.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 16px;
        }
    </style>
</head>
<body bgcolor="#EEEEEE">
    <form id="form1" runat="server">
    <div> 
       <table >
        <tr>
            <td  class="contornotabla" colspan="2" align="center" style="background-color: Silver;">                    
                    <asp:Label ID="Label6" runat="server" Text="Modificar Horas Asignadas" 
                        Font-Bold="True"></asp:Label>
                    
                </td>
        </tr>
        <tr >
            <td>
                <asp:Label ID="Label1" runat="server" Text="Docente: " Font-Bold="True"></asp:Label>
            </td>
            
            <td style="width:350px">
                <asp:Label ID="lblDocente" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
               
            </td>
            
            <td class="style1">
                <table>
                    <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Hrs. Asignadas"></asp:Label>
                            </td>
                            <td bgcolor="#F5F9FC">
                                <asp:TextBox ID="txtHoras" runat="server" Width="40px"   ></asp:TextBox>
                            </td>
                            <td >
                                 <asp:Label ID="Label3" runat="server" Text="Hrs Programadas."></asp:Label>
                            </td>
                            <td align="right">
                                <asp:TextBox ID="txtTh" runat="server" Enabled="False" Width="40px"></asp:TextBox>
                            </td>
                    </tr>
                </table>
                
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                    
                    <asp:Button ID="btnCancelar" runat="server" Text="Regresar" Width="100px" Height="22px" CssClass="salir" 
                    onclientclick="self.parent.tb_remove();" UseSubmitBehavior="False" />
                    
                <asp:Button ID="btnGuardar" runat="server"  Width="100px" Height="22px" Text="Guardar" CssClass="salir" />
            </td>
        </tr>
        
    </table>       
    </div>  
    </form>
</body>
</html>
