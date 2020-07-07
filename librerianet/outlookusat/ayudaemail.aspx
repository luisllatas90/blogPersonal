<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ayudaemail.aspx.vb" Inherits="librerianet_outlookusat_ayudaemail" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        La dirección de Tecnologías de la Información - USAT ha procedido con la creación 
de su cuenta de correo institucional la misma que le servirá para mantener 
contacto con las diversas áreas de la universidad así mismo será el medio 
oficinal para la comunicación con sus respectivos docentes.<br />
        La cuenta de correo asignada a usted es:
            <br />
           Windows live ID:<span style="mso-tab-count:
1"><br />
            <asp:Label ID="lblusuario" runat="server" Font-Bold="True" Font-Names="Verdana" 
                Font-Size="14pt" ForeColor="Red"></asp:Label>
            <br />
            </span>La contraseña inicial es:<br />
                <asp:Label ID="lblClave" runat="server" Font-Bold="True" Font-Size="14pt" 
                    ForeColor="Red"></asp:Label>
            </b>

        <span lang="ES-PE" style="mso-ansi-language:ES-PE">
        <br />
        Para activar su cuenta debe 
        ingresar a: <a href="https://www.outlook.com/estudiante.usat.edu.pe" target="_blank">
        https://www.outlook.com/estudiante.usat.edu.pe</a> debe ingresar los datos antes indicados (Windows 
        live ID y Contraseña) y luego debe llenar un formulario similar al siguiente:<p>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/outlookusat/img.JPG" />
            <span>
            <br />
            Luego de la activación usted podrá ingresar a su cuenta de correo a través de la 
            dirección<b>:
            <a href="https://www.outlook.com/estudiante.usat.edu.pe" target="_blank">https://www.outlook.com/estudiante.usat.edu.pe</a></b><p>
                Cualquier consulta puede comunicarse con nosotros a través del correo
                <b>
                <a href="mailto:soportetecnico@usat.edu.pe" target="_blank">soportetecnico@usat.edu.pe</a>.<br />
                </b>
                <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="14pt" 
                    ForeColor="Blue"></asp:Label>
                <br />
                Atentamente,<br>
                <br></br>
                Gregorio Manuel León Tenorio<br />
                Director de Tecnologías de la Información- USAT</br>
            </span></p>
            </span>
    </asp:Panel>


    </form>
</body>
</html>
