<%@ Page Language="VB" AutoEventWireup="false" CodeFile="exportarcurso.aspx.vb" Inherits="aulavirtual_exportarcurso" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Exportar</title>
    <style fprolloverstyle>A:hover {color: #FF0000; text-decoration: underline;}</style>
    <style type="text/css">
        
        body
        {
            font-family: "Trebuchet MS", "Lucida Console", Arial, san-serif;
            color: Black;
            font-size: 8pt;
            margin-top:0pt;
            margin-left:0pt;
            margin-right:0pt;
        }
        #listadiv {
        position: relative;
        width: 100%;
        top: 0;
        left: 0;
        overflow: auto;
        }
        .contorno
        {
            border: 1px solid #666666;
        }
        .barratitulo
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 20px;
            color: #FFFFFF;
            font-weight: bold;
            text-align: center;
            background-image: url('cabecera.jpg');
        }
    </style>
</head>
<body>
    
    <form id="form1" runat="server" style="background-color: #ECE9D8">
    <table style="width:80%;height:100%;" border="0" cellpadding="0" cellspacing="0" align="center">
        <tr class="barratitulo">
            <td style="width:80%;height:18%;">
            <asp:Label ID="lbltitulo" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td class="contorno" style="width:100%;height:82%; background-color:White">
            <div id="listadiv" style="height:100%">
            <asp:TreeView ID="trw" runat="server" Font-Names="Verdana" Font-Size="8pt">
            </asp:TreeView>
            </div>
            </td>
        </tr>
    </table>
     </form>
</body>
</html>
