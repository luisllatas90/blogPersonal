<%@ Page Language="VB" AutoEventWireup="false" CodeFile="datos.aspx.vb" Inherits="Academico_datos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>:: Reportes Administrativos ::</title>
     <link rel="STYLESHEET" href="css/estilos.css"/>
     <link rel="stylesheet" type="text/css" href="css/estiloimpresion.css" media="print"/>
</head>
<body topmargin="0" leftmargin="0" rightmargin="0">
    <form id="form1" runat="server">
     
    
  
        <asp:Panel CssClass='NoImprimir' ID="Panel" runat="server" BackColor="White" Height="300px" ScrollBars="Vertical"
            Style="z-index: 1; left: 0%; width: 100%; position: absolute; top: 60px; background-color: white" HorizontalAlign="Left">
            <table style="width: 100%;">
                <tr>
                    <td valign="top">
                        &nbsp;
                        <asp:Button ID="Button2" runat="server" Text="Ver Menu Reportes Personalizados" style="background-position-x: left; background-image: url(../images/run.gif); background-repeat: no-repeat; background-color: transparent" BorderStyle="None" Height="25px" Width="259px" ToolTip="Haga click en el boton para ver el menu de opciones de reporte." />
        <asp:Button ID="Button1" runat="server" Text="Mostrar Reporte" style="background-position-x: left; background-image: url(../images/lassists.gif); text-indent: 16pt; background-repeat: no-repeat" BackColor="White" BorderStyle="None" Height="23px" Width="181px" ToolTip="Haga click en el boton para mostrar el reporte seleccionado." />
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
                    Ciclo Academico :
                    <asp:DropDownList ID="DDLCiclo" runat="server" Style="font-size: 8pt; color: navy;
                        font-family: verdana" ToolTip="Ciclo Academico de estudio.">
                    </asp:DropDownList>
                    Mostrar como Principal :
                    <asp:DropDownList ID="DDLModo" runat="server" Style="font-size: 8pt; color: navy;
                        font-family: verdana" Width="152px" ToolTip="Seleccione el modo en que se mostraran los datos.">
                        <asp:ListItem Value="1">Carrera Profesional</asp:ListItem>
                        <asp:ListItem Value="2">Servicios</asp:ListItem>
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
                    <asp:Button ID="CmdMarcar2" runat="server" BackColor="White" BorderStyle="None" Style="background-position-x: left;
                        background-image: url(..//images/conforme_small.gif); background-repeat: no-repeat;
                        text-align: right" Text="Marcar Elementos" ToolTip="Seleccione un elemento de la lista inferior y luego haga click en este boton para marcar todos los elementos."
                        Width="123px" />
                    &nbsp;<asp:Button ID="CmdQuitar2" runat="server" BackColor="White" BorderStyle="None"
                        Style="background-position-x: left; background-image: url(../images/noconforme_small.gif);
                        background-repeat: no-repeat; text-align: right" Text="Quitar la Seleccion" ToolTip="Quita el marcado de los elementos segun la seleccion de la lista."
                        Width="132px" /></td>
                <td rowspan="1" style="width: 30%" valign="top">
                    <asp:Button ID="CmdMarcar3" runat="server" BackColor="White" BorderStyle="None" Style="background-position-x: left;
                        background-image: url(..//images/conforme_small.gif); background-repeat: no-repeat;
                        text-align: right" Text="Marcar Elementos" ToolTip="Seleccione un elemento de la lista inferior y luego haga click en este boton para marcar todos los elementos."
                        Width="123px" />
                    &nbsp;<asp:Button ID="CmdQuitar3" runat="server" BackColor="White" BorderStyle="None"
                        Style="background-position-x: left; background-image: url(../images/noconforme_small.gif);
                        background-repeat: no-repeat; text-align: right" Text="Quitar la Seleccion" ToolTip="Quita el marcado de los elementos segun la seleccion de la lista."
                        Width="132px" /></td>
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
                    <asp:TreeView ID="Arbol2" runat="server" ImageSet="Simple" NodeIndent="10">
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
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    
    </form>
</body>
</html>
