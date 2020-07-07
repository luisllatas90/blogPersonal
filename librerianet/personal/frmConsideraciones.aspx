<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsideraciones.aspx.vb" Inherits="personal_frmConsideraciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .style7
        {
        }
 
        .style1
        {
        }
        .style4
        {
            width: 123px;
            height: 22px;
        }
        .style8
        {
            height: 20px;
        }
        .style22
        {
            width: 50%;
        }
        .style23
        {
            width: 115px;
        }
        .style24
        {
            width: 91px;
        }
        .style25
        {
            width: 91px;
            height: 15px;
        }
        .style26
        {
            height: 15px;
        }
        .style27
        {
            width: 7px;
        }
        .style28
        {
            height: 15px;
            width: 7px;
        }
        .style30
        {
            width: 121px;
            height: 15px;
        }
        .style31
        {
            width: 121px;
        }
        .style20
        {
            width: 616px;
        }
        .style10
        {
            width: 298px;
        }
        .style32
        {
            width: 180px;
            font-size: x-small;
        }
        .style34
        {
            font-size: x-small;
        }
        .style35
        {
            width: 73px;
        }
        .style36
        {
            width: 171px;
        }
        .style37
        {
            width: 149px;
        }
        .style38
        {
            width: 193px;
            height: 28px;
        }
        .style39
        {
            height: 213px;
        }
        .style40
        {
            width: 100%;
            height: 32px;
        }
        .style41
        {
            height: 28px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 233px">
    
                                            <asp:Label ID="lblConsideraciones" runat="server" 
                                                Text="Consideraciones" 
            Font-Bold="True" Font-Names="Verdana" Font-Size="Small"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblAvisoEnvio" 
            runat="server" ForeColor="Red" 
                                                
                                                
                                                
            Text="- Haga Clic en Finalizar y Enviar cuando haya culminado con el registro del horario, caso contrario no será considerado para el control de sus marcaciones." 
            Font-Names="Verdana" Font-Size="Smaller"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="lblAvisoEnvio0" 
            runat="server" ForeColor="Red" 
                                                
                                                
                                                
            Text="- El horario de labores desde: 08:00 a 16:45, fuera de ese horario sólo serán permitidas la horas de docencia." 
            Font-Names="Verdana" Font-Size="Smaller"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblObservacion" runat="server" Font-Size="Large" 
                                                ForeColor="#FF6600" Visible="False"></asp:Label>
    
    </div>
    </form>
</body>
</html>
