<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="gerencialNet_Default" %>
<%@ Register Assembly="DundasWebOlapDataProviderAdomdNet" Namespace="Dundas.Olap.Data.AdomdNet" TagPrefix="DODPN" %>
<%@ Register Assembly="DundasWebUIControls" Namespace="Dundas.Olap.WebUIControls" TagPrefix="DOCWC" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
<center>
        <DOCWC:OlapClient ID="OlapClient1" runat="server" Height="644px" Width="908px">
            <ToolbarSettings>
                <Maximize Description="Ver vista completa" />
            </ToolbarSettings>
            <ToolbarStyle BackColor="White" BorderColor="" BorderWidth="" PageColor="" ToolbarStyle="Flat1" />
        </DOCWC:OlapClient>
 </center>    
        <DODPN:AdomdNetDataProvider ID="CnxOlap" runat="server">
        </DODPN:AdomdNetDataProvider>
    </form>
</body>
</html>
