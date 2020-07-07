<%@ Page Language="VB" AutoEventWireup="false" CodeFile="separarcita.aspx.vb" Inherits="separarcita" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript">
        function AbrirCita(){
            var alto=window.screen.Height-90
            var ancho=window.screen.Width-20
            var izq = (screen.width-400)/2
            var arriba= (screen.height-350)/2

            var ventana=window.open('frmregistrarcita.aspx',"cita","height=350,width=450,statusbar=no,scrollbars=no,top=" + arriba + ",left=" + izq + ",resizable=no,toolbar=no,menubar=no");
            //ventana.location.href=pagina
            ventana=null
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Separar cita según disponibilidad del asesor<p class="usatsugerencia" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Haga clic en las fechas marcadas que el asesor ha dispuesto para solicitar cita </p>
    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
        BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
        Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
        Width="70%">
        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
        <SelectorStyle BackColor="#CCCCCC" />
        <WeekendDayStyle BackColor="#FFFFCC" />
        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
        <OtherMonthDayStyle ForeColor="#808080" />
        <NextPrevStyle VerticalAlign="Bottom" />
        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
    </asp:Calendar>
   
    </form>
    <p class="usatTitulo">
        Citas registradas</p>
    <table style="width:100%; border-collapse:collapse" cellpadding="3" cellspacing="0" border="1">
        <tr style="background-color:#5D7B9D;color:White">
            <th>Tipo</th>
            <th>
                Fecha Reg</th>
            <th>
                Asunto cita</th>
            <th>
                Estado</th>
            <th>
                Inicio - Fin</th>
                
        </tr>
        <tr>
            <td>
                <%=Session("tipocita")%></td>
            <td>
               <%=Session("fechacita")%></td>
            <td>
                <%=Session("asuntocita")%></td>
            <td>
                 <%=Session("estadocita")%></td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </body>
</html>
