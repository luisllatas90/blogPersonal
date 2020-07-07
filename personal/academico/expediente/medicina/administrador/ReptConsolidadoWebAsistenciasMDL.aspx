<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReptConsolidadoWebAsistenciasMDL.aspx.vb" Theme="Acero" Inherits="ReptConsolidadoWebAsistenciasMDL" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1
        {
            text-align: center;
        }
    </style>
</head>
<body style="margin:0,0,0,0>
    <form id="form1" runat="server">
    
    <rsweb:ReportViewer ID="ReptConsolidadoWebAsistenciasMDL" runat="server" Font-Names="Verdana" 
      Font-Size="8pt" Height="90%" ProcessingMode="Remote" Width="100%" 
        ShowDocumentMapButton="False" ShowPageNavigationControls="False" 
        ShowRefreshButton="False" >
      <ServerReport  
        ReportServerUrl="http://localhost:80/rptusat"  />
    </rsweb:ReportViewer>
  
    <center>
    
    <asp:Button ID="CmdSalir" runat="server" Text="Regresar" SkinID="BotonSalir" />
    
    </center>
    </form>


   </body>
</html>
