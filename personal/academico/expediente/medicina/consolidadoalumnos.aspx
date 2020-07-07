<%@ Page Language="VB" AutoEventWireup="false" CodeFile="consolidadoalumnos.aspx.vb" Inherits="medicina_consolidadoalumnos" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Consolidado de Notas</title>
    <link rel="stylesheet" type="text/css" href="../../../../private/estilo.css"/>
</head>
<body style="margin:0,0,0,0">
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td colspan="3" style="border-top: black 1px solid; font-weight: bold; font-size: 11pt;
                    color: white; border-bottom: black 1px solid; font-family: verdana; height: 21px;
                    background-color: firebrick; text-align: center">
                    Consolidado de Notas</td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <asp:HyperLink ID="LinkRegresar" runat="server" Style="font-size: 8pt; font-family: verdana">«« Regresar</asp:HyperLink></td>
                <td align="right" colspan="2" style="font-size: 8pt; width: 1416px; color: #000000;
                    font-family: verdana">
                </td>
            </tr>
            <tr>
                <td colspan="3" style="border-top: #660000 1px solid; font-weight: bold; font-size: 8pt;
                    text-transform: capitalize; color: #003300; font-family: verdana; font-variant: normal">
                    &nbsp; &nbsp;Profesor :
                    <asp:Label ID="LblProfesor" runat="server" Style="text-transform: uppercase; color: dimgray;
                        font-family: verdana"></asp:Label></td>
            </tr>
            <tr style="font-size: 8pt">
                <td colspan="3" style="font-weight: bold; font-size: 8pt; text-transform: capitalize;
                    color: #003300; border-bottom: #660000 1px solid; font-family: verdana; font-variant: normal">
                    &nbsp; &nbsp;Asignatura :
                    <asp:Label ID="LblAsignatura" runat="server" Style="text-transform: uppercase; color: dimgray;
                        font-family: verdana"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="width: 90%">
                    &nbsp;</td>
                <td align="left">
                    </td>
            </tr>
            <tr>
                <td align="center" colspan="3" style="height: 47px">
                    &nbsp;<asp:Table ID="TblAlumnos" runat="server" Width="70%">
                    </asp:Table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
