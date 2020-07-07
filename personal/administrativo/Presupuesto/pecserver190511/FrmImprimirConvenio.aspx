<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmImprimirConvenio.aspx.vb" Inherits="administrativo_pec_FrmImprimirConvenio" Theme="Acero" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin:0,0,0,0>
    <form id="form1" runat="server">
   
    <rsweb:ReportViewer ID="RptConvenio" runat="server" Font-Names="Verdana" 
      Font-Size="8pt" Height="90%" ProcessingMode="Remote" Width="100%" 
      ShowDocumentMapButton="False" ShowFindControls="False" 
      ShowPromptAreaButton="False" ShowRefreshButton="False">
      <ServerReport  
        ReportServerUrl="http://LOCALHOST:80/rptusat"  />
    </rsweb:ReportViewer>
  
<center>
    <asp:Button ID="CmdSalir" runat="server" Text="Button" SkinID="BotonSalir" />
    </center>
    </form>

   </body>
</html>
