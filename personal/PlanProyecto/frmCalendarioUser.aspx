<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCalendarioUser.aspx.vb" Inherits="PlanProyecto_frmCalendario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />        
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
                eventClick: function(event) {                
                    currentUpdateEvent = event;
                    eventToUpdate = {
                        id: currentUpdateEvent.id                        
                    };
                    window.open('frmResumenActividad.aspx?apr=' + eventToUpdate.id, "", "toolbar=no, location=no, directories=no, status=no, menubar=no, resizable=no, copyhistory=no, width=700, height=500'");
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
            </td>
            <td align="right">
                <asp:Button ID="btnRefrescar" runat="server" Text="Refrescar" CssClass="salir" Width="100px" Height="22px" />
            </td>
        </tr>
    </table>     
     <div id="calendar">
    </div> 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    </form>
</body>
</html>
