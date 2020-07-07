<%@ Page Language="VB" AutoEventWireup="false" CodeFile="matriculaano.aspx.vb" Inherits="matriculaano" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

<style type="text/css" >
#textovertical {
writing-mode: tb-rl;
filter: fliph flipv;
} 
</style> 


    <title>:: Reportes Administrativos ::</title>
     <link rel="STYLESHEET" href="css/estilos.css"/>
     <link rel="stylesheet" type="text/css" href="css/estiloimpresion.css" media="print"/>
</head>
<body topmargin="0" leftmargin="0" rightmargin="0" >
<center>
    <form id="form1" runat="server">
     
    
  
        <asp:Panel CssClass='NoImprimir' ID="Panel" runat="server" BackColor="White" Height="300px" ScrollBars="Vertical"
            Style="z-index: 1; left: 0%; width: 100%; position: absolute; top: 60px; background-color: white" HorizontalAlign="Left">
            <table style="width: 100%;">
                <tr>
                    <td valign="top"     >
                        &nbsp;
                        <asp:Button ID="Button2" runat="server" Text="Ver Menu Reportes Personalizados" style="background-position-x: left; background-image: url(../images/run.gif); background-repeat: no-repeat; background-color: transparent; text-align: right;" BorderStyle="None" Height="25px" Width="233px" ToolTip="Haga click en el boton para ver el menu de opciones de reporte." />
                        &nbsp;<asp:Button ID="Button1" runat="server" Text="Mostrar Reporte" style="background-position-x: left; background-image: url(../images/lassists.gif); text-indent: 16pt; background-repeat: no-repeat; text-align: right;" BackColor="White" BorderStyle="None" Height="23px" Width="122px" ToolTip="Haga click en el boton para mostrar el reporte seleccionado." />&nbsp;
                        &nbsp;
                        <asp:Button ID="CmdGraf" runat="server" Text="Mostrar Grafico" OnClientClick="javascript: window.open('grafico.aspx?tipo=1','grafico1','scrollbars=yes, Height=600, witdh=400, resizable=yes'); return false;" style="background-image: url(../images/Volume Manager.gif); background-repeat: no-repeat; text-align: right" BackColor="White" BorderColor="White" BorderStyle="None" Height="23px" ToolTip="Haga click aqui para generar un grafico de acuerdo a los datos obtenidos." Width="118px" />&nbsp;
                        <asp:Button ID="CmdSaveGraf" runat="server" Text="Guardar Grafico" OnClientClick="javascript: window.open('grafico2.aspx?tipo=1','grafico1','scrollbars=yes, Height=600, witdh=400, resizable=yes'); return false;" style="background-image: url(../images/disco.gif); background-repeat: no-repeat; text-align: right" BackColor="White" BorderColor="White" BorderStyle="None" Height="23px" ToolTip="Haga click aqui para que genere un grafico que pueda grabarlo en algun dispositivo de almacenamiento fisico." Width="118px" />
                        <hr style="border-right: olive 2px solid; border-top: olive 2px solid; border-left: olive 2px solid;
                            border-bottom: olive 2px solid; background-color: olive" />
                        </td>
                </tr>
                <tr>
                    <td valign="top">
        <table style="width: 100%">
            <tr>
                <td colspan="3" rowspan="1" style="font-size: 8pt; color: navy; font-family: verdana"
                    valign="top">
                    Modalidad de Ingreso : &nbsp;<asp:DropDownList ID="DDLModalidad" runat="server" Style="font-size: 8pt; color: navy;
                        font-family: verdana" ToolTip="Ciclo Academico de estudio.">
                    </asp:DropDownList>
                    &nbsp; Mostrar como principal : &nbsp;
                    <asp:DropDownList ID="DDLModo" runat="server" Style="font-size: 8pt; color: navy;
                        font-family: verdana" ToolTip="Seleccione el modo en que se mostraran los datos."
                        Width="152px">
                        <asp:ListItem Value="1">Ciclo Academico</asp:ListItem>
                        <asp:ListItem Value="2">Carrera Profesional</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td rowspan="1" style="width: 30%" valign="top">
                    <asp:Button ID="CmdMarcar1" runat="server" BackColor="White" BorderStyle="None" Style="background-position-x: left;
                        background-image: url(..//images/conforme_small.gif); background-repeat: no-repeat;
                        text-align: right" Text="Marcar Elementos" ToolTip="Seleccione un elemento de la lista inferior y luego haga click en este boton para marcar todos los elementos."
                        Width="123px" />
                    &nbsp;<asp:Button ID="CmdQuitar1" runat="server" BackColor="White" BorderStyle="None"
                        Style="background-position-x: left; background-image: url(../images/noconforme_small.gif);
                        background-repeat: no-repeat; text-align: right" Text="Quitar la Seleccion" ToolTip="Quita el marcado de los elementos segun la seleccion de la lista."
                        Width="132px" /></td>
                <td rowspan="1" style="width: 30%" valign="top">
                    <asp:Button ID="CmdMarcar3" runat="server" BackColor="White" BorderStyle="None" Style="background-position-x: left;
                        background-image: url(..//images/conforme_small.gif); background-repeat: no-repeat;
                        text-align: right" Text="Marcar Elementos" ToolTip="Seleccione un elemento de la lista inferior y luego haga click en este boton para marcar todos los elementos."
                        Width="123px" />&nbsp;<asp:Button ID="CmdQuitar3" runat="server" BackColor="White" BorderStyle="None"
                        Style="background-position-x: left; background-image: url(../images/noconforme_small.gif);
                        background-repeat: no-repeat; text-align: right" Text="Quitar la Seleccion" ToolTip="Quita el marcado de los elementos segun la seleccion de la lista."
                        Width="132px" /></td>
                <td rowspan="1" style="width: 30%" valign="top">
                    &nbsp;<asp:Button ID="CmdMarcar2" runat="server" BackColor="White" BorderStyle="None" Style="background-position-x: left;
                        background-image: url(..//images/conforme_small.gif); background-repeat: no-repeat;
                        text-align: right" Text="Marcar Elementos" ToolTip="Seleccione un elemento de la lista inferior y luego haga click en este boton para marcar todos los elementos."
                        Width="123px" Visible="False" />
                    &nbsp;&nbsp;
                    <asp:Button ID="CmdQuitar2" runat="server" BackColor="White" BorderStyle="None"
                        Style="background-position-x: left; background-image: url(../images/noconforme_small.gif);
                        background-repeat: no-repeat; text-align: right" Text="Quitar la Seleccion" ToolTip="Quita el marcado de los elementos segun la seleccion de la lista."
                        Width="132px" Visible="False" /></td>
            </tr>
            <tr>
                <td rowspan="3" valign="top" style="width: 33%">
                    <asp:TreeView ID="Arbol1" runat="server" ImageSet="Simple" NodeIndent="10">
            <ParentNodeStyle Font-Bold="False" />
            <HoverNodeStyle Font-Underline="True" ForeColor="#DD5555" />
            <SelectedNodeStyle Font-Underline="True" ForeColor="#DD5555" HorizontalPadding="0px"
                VerticalPadding="0px" />
            <NodeStyle Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" HorizontalPadding="0px"
                NodeSpacing="0px" VerticalPadding="0px" />
        </asp:TreeView>
                </td>
                <td rowspan="3" valign="top" style="width: 33%">
                    <asp:TreeView ID="Arbol3" runat="server" ImageSet="Simple" NodeIndent="10">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#DD5555" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#DD5555" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <NodeStyle Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" HorizontalPadding="0px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </td>
                <td rowspan="3" valign="top" style="width: 33%">
                    <asp:TreeView ID="Arbol2" runat="server" ImageSet="Simple" NodeIndent="10" Visible="False">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#DD5555" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#DD5555" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <NodeStyle Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" HorizontalPadding="0px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
        </table>
                    <asp:DropDownList
                        ID="DropDownList1" runat="server" Style="font-size: 8pt; color: navy; font-family: verdana"
                        Width="55px" Visible="False">
                        <asp:ListItem Value="10">10%</asp:ListItem>
                        <asp:ListItem Value="20">20%</asp:ListItem>
                        <asp:ListItem Value="30">30%</asp:ListItem>
                        <asp:ListItem Value="40">40%</asp:ListItem>
                        <asp:ListItem Value="50">50%</asp:ListItem>
                        <asp:ListItem Value="60">60%</asp:ListItem>
                        <asp:ListItem Value="70">70%</asp:ListItem>
                        <asp:ListItem Value="80">80%</asp:ListItem>
                        <asp:ListItem Value="90">90%</asp:ListItem>
                        <asp:ListItem Selected="True" Value="100">100%</asp:ListItem>
                        <asp:ListItem Value="120">120%</asp:ListItem>
                        <asp:ListItem Value="150">150%</asp:ListItem>
                        <asp:ListItem Value="200">200%</asp:ListItem>
                    </asp:DropDownList>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/previo.gif" Height="17px" Width="18px" BackColor="White" BorderColor="Black" ToolTip="Seleccione el porcentaje de vista del reporte" Visible="False" /></td>
                </tr>
            </table>
        </asp:Panel>
        &nbsp;</form>
    </center>
</body>
</html>
