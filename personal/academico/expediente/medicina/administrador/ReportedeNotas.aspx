<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportedeNotas.aspx.vb" Inherits="medicina_administrador_ReportedeNotas" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Página sin título</title>
    <script language="javascript" src="../../../../../private/tooltip.js" ></script>
    <link rel="stylesheet" type="text/css" href="../../../../../private/estilo.css"/>
           
    <script type="text/javascript" language="javascript">
    
    function activaguardar(caja)
        {
            if (parseFloat(caja.value) == parseFloat(caja.Tag))
                {
                form1.HddContCaja.value = parseInt(form1.HddContCaja.value) - 1
                caja.style.backgroundColor='#FFFFFF'
                    if (parseInt(form1.HddContCaja.value)==0)
                form1.cmdGuardar.disabled=true
                }
            else    
                {
                 form1.HddContCaja.value = parseInt(form1.HddContCaja.value) + 1
                 form1.cmdGuardar.disabled=false
                 caja.style.backgroundColor='#FFFF80'
                }
        }
    
        function numeros()
            {
                var key=window.event.keyCode;//codigo de tecla.
                if (key < 46 || key > 57){//si no es numero 
                window.event.keyCode=0; }//anula la entrada de texto. 
            }
     </script>
</head>
<body style="margin:0,0,0,0">
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td colspan="3" style="border-top: black 1px solid; font-weight: bold; font-size: 11pt;
                    color: white; border-bottom: black 1px solid; font-family: verdana; height: 21px;
                    background-color: firebrick; text-align: center">
                    Reporte de Notas</td>
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
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" style="height: 47px" align="center">
                    <asp:Label ID="LblEvaluacion" runat="server" Font-Bold="True"></asp:Label>&nbsp;<br />
                    <br />
                    <asp:Table ID="TblAlumnos" runat="server" Width="70%">
                    </asp:Table>
               
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HidenAlumnos" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
        <asp:HiddenField ID="HddContCaja" runat="server" Value="0" />
    
    </div>
    </form>
</body>
</html>
