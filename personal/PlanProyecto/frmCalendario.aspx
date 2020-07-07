<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCalendario.aspx.vb" Inherits="PlanProyecto_frmCalendario" %>

<%@ Register assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
    <link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />    
    <script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>	
    <link rel='stylesheet' type='text/css' href='fullcalendar/fullcalendar.css' />
    <link rel='stylesheet' type='text/css' href='fullcalendar/fullcalendar.print.css' media='print' />
    
    <script type='text/javascript' src='jquery/jquery-1.5.2.min.js'></script>
    <script type='text/javascript' src='jquery/jquery-ui-1.8.11.custom.min.js'></script>
    <script type='text/javascript' src='fullcalendar/fullcalendar.min.js'></script>    
    <script type='text/javascript'>
        function updateEventOnDropResize(cod, dia) {
            PageMethods.fn_ActualizaFecha(cod, dia);
        }        

        $(document).ready(function() {
            $('#calendar').fullCalendar({
                editable: true,
                eventClick: function(event) {                
                    currentUpdateEvent = event;
                    eventToUpdate = {
                        id: currentUpdateEvent.id                        
                    };
                    window.open("frmRegistraActividad.aspx?apr=" + eventToUpdate.id, "", "toolbar=no, location=no, directories=no, status=no, menubar=no, resizable=no, copyhistory=no, width=700, height=500'");
                },
                events: "JsonResponse1.ashx",
                eventDrop: function(event, delta) {
                    currentUpdateEvent = event;
                    eventToUpdate = { id: currentUpdateEvent.id };
                    updateEventOnDropResize(eventToUpdate.id, delta);
                },
                loading: function(bool) {
                    if (bool) $('#loading').show();
                    else $('#loading').hide();
                }
            });
        });
    </script>        
</head>
<body>
    <form id="form1" runat="server">        
    <table width="100%">        
        <tr>
            <td style="width: 15%">
                <asp:Label ID="Label1" runat="server" Text="Calendario:"></asp:Label></td>
            <td>
                <asp:DropDownList ID="dpProyecto" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;                
                <a href='frmRegistraActividad.aspx?KeepThis=true&TB_iframe=true&height=600&width=650&modal=true' title='Registro de Actividades' class='thickbox'>
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar Actividad" CssClass="agregar2" Height="22px" Width="100px" /> 
                </a>                
                &nbsp;
                <a href='frmListaGrupo.aspx?KeepThis=true&TB_iframe=true&height=500&width=600&modal=true' title='Registro de Actividades' class='thickbox'>
                    <asp:Button ID="btnListar" runat="server" Text="Listar Grupos" CssClass="buscar2" Width="100px" Height="22px" /> &nbsp;
                </a>                
            </td>
            <td align="right">
                <asp:Button ID="btnRefrescar" runat="server" Text="Refrescar" CssClass="salir" Width="100px" Height="22px" />
            </td>
        </tr>
    </table>     
     <div id="calendar">
    </div> 
    </form>
</body>
</html>
