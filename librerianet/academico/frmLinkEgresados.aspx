<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmLinkEgresados.aspx.vb" Inherits="academico_frmLinkEgresados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Datos de Egresado</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .claseBoton
        {
            width:30%;
            background:#e1f1fb;
            /* Borde superior */
            border-top-color:Black;
            border-top-style:solid;
            border-top-width:1px;
            /* Borde izquierdo */
            border-left-color:Black;
            border-left-style:solid;
            border-left-width:1px;
            /* Borde derecho */
            border-right-color:Black;
            border-right-style:solid;
            border-right-width:1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div> 
        <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
        <table width="100%">
            <tr>
                <td colspan="2" style="background:#26758c; color:White"> <b>DATOS DEL ALUMNO</b> </td>
            </tr>
            <tr>
                <!-- <td rowspan="4" width="100" height="118">
                    <asp:Image ID="imgAlumno" runat="server" Width="100%" Height="100%" />
                </td> -->
                <td width="30%">                    
                    <asp:Label ID="Label1" runat="server" Text="Nombre:"></asp:Label></td>
                <td>
                    <asp:Label ID="lblNombre" runat="server" ForeColor="#000082" Font-Bold="True"></asp:Label></td>
            </tr>            
            <tr>                
                <td width="30%">
                    <asp:Label ID="Label3" runat="server" Text="Carrera Profesional:" 
                        Font-Bold="False" ></asp:Label></td>
                <td>
                    <asp:Label ID="lblCarrera" runat="server" ForeColor="#000082" Font-Bold="True"></asp:Label></td>
            </tr>           
            <tr>
                <td width="30%">
                    <asp:Label ID="Label4" runat="server" Text="Ciclo Ingreso:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCicloIngreso" runat="server" Text="" ForeColor="#000082" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="30%">
                    <asp:Label ID="Label2" runat="server" Text="Estado:"></asp:Label></td>
                <td>
                    <asp:Label ID="lblEstado" runat="server" Text="" ForeColor="#000082" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table> 
        <br />
        <table width="100%" style="height:600px">
            <tr>
                <td align="center" class="claseBoton">
                    <asp:LinkButton ID="lnkHistorial" runat="server" Font-Size="Small">Historial</asp:LinkButton></td>
                <td style="width:5%"></td>
                <td align="center" class="claseBoton">
                    <asp:LinkButton ID="lnkCursos" runat="server" Font-Size="Small">Cursos Faltantes</asp:LinkButton></td>
                <td style="width:5%"></td>
                <td align="center" class="claseBoton">
                    <asp:LinkButton ID="lnkSolicitudes" runat="server" Font-Size="Small">Solicitud</asp:LinkButton></td>                
            </tr>
            <tr>
                <td colspan="5" style="border-top-color:Black; border-top-style:solid; border-top-width:1px">
                <iframe id="fraDetalle" runat="server" width="100%" style="height:600px; border-top-style:none; border-top-width:0px">
                </iframe>
                </td>
            </tr>
        </table>  
    </div>    
    <asp:HiddenField ID="HdCodigo_Alu" runat="server" />
    </form>
</body>
</html>

