<%@ Page Language="VB" AutoEventWireup="false" CodeFile="vstrecursospublicados.aspx.vb" Inherits="vstrecursospublicados"%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recursos Publicados</title>
    <style fprolloverstyle>A:hover {color: #FF0000; text-decoration: underline;}</style>
    <style type="text/css">
        
        body
        {
            font-family: "Trebuchet MS", "Lucida Console", Arial, san-serif;
            color: Black;
            font-size: 8pt;
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
        }
    </style>
</head>
<body bgcolor="#e5e5e5">
    <form id="form1" runat="server">
    <table style="width:80%; height:95%" border="0" cellpadding="0" cellspacing="0" align="center">
        <tr class="barratitulo">
            <td style="width:100%;height:10%">
            <asp:Label ID="lbltitulo" runat="server" Text="Label" ForeColor="#3366CC"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td style="width:80%;height:2%;font-size: 12px;" align="right">
                <asp:HiddenField ID="hdIdCursoVirtual" runat="server" Value="0" />
                <asp:HiddenField ID="hdCodigo_apl" runat="server" Value="0" />
                <asp:HiddenField ID="hpSP" runat="server" Value="DI_RecursosCursoVirtual_vA" />
                Recursos publicados:&nbsp;
                <asp:DropDownList ID="cboTipoRecurso" runat="server" 
                    Font-Size="8pt">
                    <asp:ListItem Value="D">Documentos</asp:ListItem>
                    <asp:ListItem Value="T">Trabajos</asp:ListItem>
                </asp:DropDownList>
            &nbsp;<asp:ImageButton ID="imgBuscar" runat="server" 
                    ImageUrl="../../images/menus/buscar_small12.gif" />
                <asp:ImageButton ID="imgExportar" runat="server" 
                    ImageUrl="../../images/minimiza.gif" />
            </td>
        </tr>
        <tr>
            <td class="contorno" style="width:100%;height:93%; background-color:White">
            <div id="listadiv" style="width:100%;height:100%">
            <asp:TreeView ID="trw" runat="server" Font-Names="Verdana" Font-Size="8pt" 
                    ShowExpandCollapse="False">
                <NodeStyle NodeSpacing="2px" />
            </asp:TreeView>
            </div>
            </td>
        </tr>
    </table>
     </form>
</body>
</html>
