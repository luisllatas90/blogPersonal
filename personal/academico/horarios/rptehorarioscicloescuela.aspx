<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptehorarioscicloescuela.aspx.vb" Inherits="rptehorarioscicloescuela" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Asignar Horas de  Carga Académica</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script src="../../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../../private/jq/jquery.mascara.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        /*
        if(top.location==self.location)
        {location.href='../../tiempofinalizado.asp'} //El ../ depende de la ruta de la página
        */
        
         $(document).ready(function() {
            jQuery(function($) {
                $("#txtinicio").mask("99/99/9999");
                $("#txtfin").mask("99/99/9999");
            });
        })
    </script>
    <style type="text/css">
        .Marcado {
        background-color: #FFCC00;
        text-align:center;
        font-size: 6pt;
        }
        .otraEsc {
        background-color: #81DAF5;
        text-align:center;
        font-size: 6pt;
        }
        .etiquetaTabla {
        background-color: #EAEAEA;
        color: #0000FF;
        text-align:center;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Horarios registrados por ciclo & grupo horario. ciclo académico:
    <asp:DropDownList ID="dpCodigo_cac" runat="server" AutoPostBack="True">
    </asp:DropDownList>
    </p>
<table cellpadding="0" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
        <tr style="background-color: #6694e3; color:White">
            <td style="height: 30px; ">
                &nbsp;Escuela Profesional:
                <asp:DropDownList ID="dpEscuela" runat="server">
                </asp:DropDownList>
                &nbsp;Inicio:
                <asp:TextBox ID="txtinicio" runat="server" CssClass="cajas" MaxLength="10" 
                    Width="80px"></asp:TextBox>
&nbsp; Fin:
                <asp:TextBox ID="txtfin" runat="server" CssClass="cajas" MaxLength="10" 
                    Width="80px"></asp:TextBox>
&nbsp;<asp:Button ID="cmdBuscar" runat="server" Text="Buscar" 
                    CssClass="buscar2" Height="22px" Visible="False" />
                </td>
        </tr>
        </table>
    <br />
    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
    <br />
    <asp:DataList ID="dtCiclos" runat="server" CellPadding="3" RepeatColumns="2" 
        RepeatDirection="Horizontal">
        <ItemStyle VerticalAlign="Top" />
        <ItemTemplate>
            <table>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblCiclo" runat="server" Font-Bold="True" 
                            Text='<%# eval("ciclo_cur") %>'></asp:Label>
                        &nbsp;<b>CICLO&nbsp;&nbsp;&nbsp; Grupo: </b>
                        <asp:Label ID="lblGrupo" runat="server" Font-Bold="True" 
                            Text='<%# eval("grupohor_cup") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                    
                        <asp:Table ID="tblHorario" runat="server" BorderColor="#999999" 
                            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="0" 
                            GridLines="Both" Width="100%">
                            <asp:TableRow runat="server" CssClass="etiquetaTabla">
                                <asp:TableCell runat="server">HORA</asp:TableCell>
                                <asp:TableCell runat="server">LUNES</asp:TableCell>
                                <asp:TableCell runat="server">MARTES</asp:TableCell>
                                <asp:TableCell runat="server">MIERCOLES</asp:TableCell>
                                <asp:TableCell runat="server">JUEVES</asp:TableCell>
                                <asp:TableCell runat="server">VIERNES</asp:TableCell>
                                <asp:TableCell runat="server">SABADO</asp:TableCell>
                                <asp:TableCell runat="server">DOMINGO</asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    
                    </td>
                </tr>
            </table>
            <br />
        </ItemTemplate>
    </asp:DataList>
    </form>
</body>
</html>
