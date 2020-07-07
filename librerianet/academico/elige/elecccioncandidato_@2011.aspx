<%@ Page Language="VB" AutoEventWireup="false" CodeFile="elecccioncandidato.aspx.vb" Inherits="academico_votacionAlumno" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 80px;
        }
    </style>
        <script type="text/javascript">
            function pepote(f) {
                marcado = false;
                var nombre;
                for (a = 0; a < f.elements.length; a++) {
                    if (f[a].type == "radio") {
                        if (nombre != f[a].name) {
                            nombre = f[a].name;
                            for (aa = 0; f[a + aa].name == f[a].name; aa++) {
                                if (f[a + aa].checked) { marcado = true };
                            }
                            if (marcado == false) { alert(MensajeDeError); return false; }

                        }
                        marcado = false;
                    }

                }
            } 
     
    </script> 

    </head>
    <body>
    <script language="JavaScript" type="text/javascript">
        function click() {
            if (event.button == 2) {
                alert('Acceso no permitido');
            }
        }
        document.onmousedown = click
        //-->
    </script>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <table style="width:80%;" align="center">
            <tr>
                <td align="center" colspan="2" width="50%" 
                    style="color: #003399; font-weight: bold">
                    ELECCION DE DELEGADO FRATERNO: ESCUELA DE ING. DE SISTEMAS Y COMPUTACIÓN</td>
            </tr>
            <tr>
                <td align="center" width="50%" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" width="50%">
                    <asp:RadioButton ID="rbCandidato5" runat="server" GroupName="votacion" 
                        Text="VOTO EN BLANCO O NINGUNO" Font-Bold="True" ForeColor="#003399" 
                        Font-Size="10pt" Visible="False" />
                </td>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" width="50%">
                    <img alt="" src="candidatos/rafael.jpg" 
                        style="width: 161px; height: 183px" align="middle" /></td>
                <td align="center">
                    <img alt="" src="candidatos/Jessica.jpg" 
                        style="width: 151px; height: 178px" /></td>
            </tr>
            <tr>
                <td align="center" width="50%">
                    <asp:RadioButton ID="rbCandidato1" runat="server" GroupName="votacion" 
                        Text="GIANCARLO RAFAEL CÓRDOVA OLIDEN  " Font-Bold="True" 
                        ForeColor="#003399" Font-Size="10pt" />
                </td>
                <td align="center">
                    <asp:RadioButton ID="rbCandidato2" runat="server" GroupName="votacion" 
                        Text="JESSICA ALMENDRA NERIA COLMENARES " Font-Bold="True" 
                        ForeColor="#003399" Font-Size="10pt" />
                </td>
            </tr>
            <tr>
                <td align="center" width="50%">
                    &nbsp;</td>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" width="50%">
                    <img alt="" src="candidatos/angels.jpg" 
                        style="width: 161px; height: 183px" align="middle" /></td>
                <td align="center">
                    <img alt="" src="candidatos/yosselim.jpg" 
                        style="width: 161px; height: 183px" align="middle" /></td>
            </tr>
            <tr>
                <td align="center" width="50%">
                    <asp:RadioButton ID="rbCandidato3" runat="server" GroupName="votacion" 
                        Text="ÁNGEL JESÚS SAMILLAN BECERRA" Font-Bold="True" 
                        ForeColor="#003399" Font-Size="10pt" />
                </td>
                <td align="center">
                    <asp:RadioButton ID="rbCandidato4" runat="server" GroupName="votacion" 
                        Text="YOSSELIM MONTENEGRO FARFAN " Font-Bold="True" 
                        ForeColor="#003399" Font-Size="10pt" />
                </td>
            </tr>
            <tr>
                <td align="center" class="style1" colspan="2">
                    <asp:Label ID="lblMensaje3" runat="server" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#009933" 
                        Text="Si no selecciona un candidato su voto será considerado en blanco"></asp:Label>
                    <br />
                    <asp:Button ID="cmdVotar" runat="server" 
                        Text="VOTAR POR EL CANDIDATO SELECCIONADO" ValidationGroup="¿" 
                        UseSubmitBehavior="False" Font-Bold="True" Font-Size="Medium" 
                        Height="40px" Width="397px"  />

                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="X-Large" 
                        ForeColor="Red" Text="Ya ha efectuado su voto"></asp:Label>
                    <br />
                    <asp:Label ID="lblMensaje2" runat="server" Font-Bold="True" Font-Size="X-Large" 
                        ForeColor="Red" Text="Acceso no válido" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left">
                    &nbsp;</td>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
                <td align="center">
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="voto"  ClientValidationFunction="validateRadioButtonList('votacion')"> </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
                <td align="center">
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
