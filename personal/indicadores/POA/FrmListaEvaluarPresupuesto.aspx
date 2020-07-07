<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaEvaluarPresupuesto.aspx.vb" Inherits="indicadores_POA_FrmListaEvaluarPresupuesto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
     <script type="text/javascript" language="javascript">
         function confirma(id,per,ctf) {
             if (confirm("¿Desea Regresar Programa/Proyecto a Estado de Registro de Presupuesto? ")) 
             {
                 window.location.href = "procesa.aspx?Accion=CambiarInstancia&id=" + per + "&ctf=" + ctf + "&acp=" + id;
             }

         }
     </script>
     <style type="text/css">
         a:hover
        {
            color:Green;
            text-decoration:none;
            
        }
     </style>
     
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="hdfila" Value="-1" />
    <asp:HiddenField runat="server" ID="actividadPto" />
    <asp:HiddenField runat="server" ID="cecoPto" />
    <asp:HiddenField runat="server" ID="instanciaPto" />
    <asp:HiddenField runat="server" ID="estadoPto" />
    <asp:HiddenField runat="server" ID="codigoPto" />
    <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server"
            Text="Evaluación de Presupuesto de Programa/Proyecto"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%" id="tabla" runat="server">
        <tr style="height:30px;">
        <td width="140px" >Plan Estratégico</td>
        <td width="510px"><asp:DropDownList ID="ddlplan" runat="server" Width="500" AutoPostBack="true"></asp:DropDownList></td>
        <td width="50px"></td>
        <td width="140px">Ejercicio Presupuestal</td>
        <td><asp:DropDownList ID="ddlEjercicio" runat="server" Width="140" AutoPostBack="true"></asp:DropDownList></td>
        <td><asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="btnBuscar" /></td>
        </tr>
        <tr>
        <td>Plan Operativo Anual</td>
        <td>
        <asp:DropDownList ID="ddlPoa" runat="server" Width="500">
        <asp:ListItem Value="0">--SELECCIONE--</asp:ListItem>
        </asp:DropDownList> 
        </td>
        <td width="50px"></td>
        <td>Estado de Actividad</td>
         <td>
         <asp:DropDownList ID="ddlestado" runat="server" Width="140" >
<%--            <asp:ListItem Value="2">Pendientes</asp:ListItem>
            <asp:ListItem Value="4">Observados</asp:ListItem>
            <asp:ListItem Value="7">Aprobados</asp:ListItem>
            <asp:ListItem Value="T">Todos</asp:ListItem>--%>
        </asp:DropDownList>
        </td>
        <td></td>
        </tr>
        
        <tr style="height:15px">
        <%--
        <td>Situacion  </td>
        <td colspan="5">
            <img alt="" src="" style="background-color:#87CEEB" width="8px" height="8px" /><asp:Label ID="Label2" runat="server"> Pendiente</asp:Label>
            &nbsp;&nbsp;
            <img alt="" src="" style="background-color:#F08080" width="8px" height="8px" /><asp:Label ID="Label4" runat="server"> Observado</asp:Label>
            &nbsp;&nbsp;
            <img alt="" src="" style="background-color:#90EE90" width="8px" height="8px" /><asp:Label ID="Label5" runat="server"> Aprobado</asp:Label>
        </td>
        --%>
        </tr>
         <tr>
            <td colspan="6">
                <div runat="server" id="aviso">
                    <asp:Label ID="lblrpta" runat="server" Font-Bold="true"></asp:Label>
                </div>
            </td> 
        </tr>
        </table>
        <div id='TablaActividadesPto' width="100%" runat="server"></div>

        </div>
    </form>
</body>
</html>
