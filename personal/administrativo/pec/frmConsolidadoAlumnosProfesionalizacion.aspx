<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsolidadoAlumnosProfesionalizacion.aspx.vb" Inherits="frmConsolidadoAlumnosProfesionalizacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    
    <!--<link href="Styles/estilo.css" rel="stylesheet" type="text/css" />-->
    <!--<script src="JavaScript/funciones.js" type="text/javascript"></script>-->
    
    <link rel="stylesheet" type="text/css" href="../../../private/estilo.css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    
    <script type="text/javascript" language="javascript">
       function PintarFilaMarcada(obj, estado) {
           if (estado == true) {
               obj.style.backgroundColor = "#FFE7B3"
           }
           else {
               obj.style.backgroundColor = "white"
           }
       }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
            <tr>
                <td bgcolor="#D1DDEF" colspan="2" height="30px">
                    <b>
                        <asp:Label ID="Label1" runat="server" Text="CONSOLIDADO DE ALUMNOS - PROGRAMA DE PROFESIONALIZACIÓN"></asp:Label>
                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table>
                        <tr>
                            <td style="width:10%">
                                <asp:Label ID="Label2" runat="server" Text="Carrera Profesional"></asp:Label>
                            </td>
                            <td style="width:80%; margin-left: 40px;">
                                <asp:DropDownList ID="ddlProgramas" Width="100%" runat="server" 
                                    BackColor="#CCFFFF" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            
                        </tr>
                        <tr>
                            <td style="width:30%" align="left">
                                <asp:Label ID="Label3" runat="server" Text="Alumnos Activos"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkActivos" runat="server" AutoPostBack="True" 
                                    Checked="True" />
                            </td>
                        </tr>
                    </table>
                </td>
                
            </tr>   
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvLista" runat="server" Width="100%" 
                        Font-Size="XX-Small">
                        <EmptyDataTemplate>
                           No se encontraron participantes en el curso
                        </EmptyDataTemplate>
                        <FooterStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#3366CC" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#3366CC" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#FFFFFF" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
