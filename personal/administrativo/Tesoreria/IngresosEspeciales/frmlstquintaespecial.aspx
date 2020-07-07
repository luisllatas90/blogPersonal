<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmlstquintaespecial.aspx.vb" Inherits="frmlstquintaespecial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="funciones.js"></script>
    <title>Página sin título</title>
    <style type="text/css">
        .style11
        {
            font-size: medium;
            font-family: Arial;
            text-align: left;
        }
        .style12
        {
            width: 100%;
        }
        .style13
        {
            width: 1217px;
            font-family: Arial;
            font-size: x-small;
            color: #FFFFFF;
            font-weight: 700;
            background-color: #731827;
        }
        .style15
        {
            font-family: Arial;
            font-size: x-small;
            color: #0000FF;
        }
        .style16
        {
            text-decoration: underline;
        }
        .style17
        {
            height: 7px;
        }
        </style>
        <script  type ="text/javascript">
            function AbrirNuevoRegistro()
                {
                    window.open('frmregistroingresoporquinta.aspx','','toolbar=no, width=800, height=300');
                }
                function Acciones (x)
                {
                    if (confirm ('Desea anular el informe de este programa'))
                        {
                            window.open ("frmprocesar.aspx?" + x,'','toolbar=no, width=50, height=50'); 
                         }
                }
         </script>
</head>
<body bgcolor="#d2d2d2" style="background-color: #FFFFFF">
    <form id="form1" runat="server">
    <div>
    
                        <table class="style12">
                            <tr>
                                <td class="style13" colspan="2" style="height: 16px">
                                    <span style="font-size: 8pt; font-family: Courier New">
                                    Usuario Actual: 
                                        <asp:Label ID="lblusuario" runat="server" Width="296px"></asp:Label>&nbsp;&nbsp;&nbsp; Sesión 
                                    iniciada el : 13/05/2008 12:45 pm &nbsp; &nbsp;&nbsp; <span style="font-size: 9pt">
                                            Sistema de Tesorería</span> - Módulo Ingresos de 
                                    Personal</span>&nbsp;</td>
                            </tr>
                        </table>
                        <hr style="color: #FF9900" />
    
                        <br />
    
        <table class="style12">
            <tr>
                <td style="height: 21px; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; width: 1396px; border-bottom: black 1px solid;" colspan="4">
                    <span style="font-size: 9pt; font-family: Courier New"><strong>Seleccionar programa
                        :</strong></span><asp:DropDownList
                        ID="cboprograma" runat="server" AutoPostBack="True" Width="760px" Font-Names="Courier New" ToolTip="Programas disponibles">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 1396px" colspan="4">
                    <asp:Button ID="cmdagregar" runat="server" BackColor="#0099FF" 
                        BorderColor="White" ForeColor="White" onclientclick="AbrirNuevoRegistro()" 
                        Text="Registrar Informe" BorderStyle="Ridge" style="background-color: #70B4E1" ToolTip="Para añadir un informe para el programa seleccionado" />
                    &nbsp; &nbsp;<asp:Button ID="Button1" runat="server" BackColor="#0099FF" 
                        BorderColor="White" ForeColor="White" 
                        Text="Exportar Excel" BorderStyle="Ridge" style="background-color: #70B4E1" /></td>
            </tr>
            <tr>
                <td class="style17" style="width: 1396px" colspan="4"> 
    
                        </td>
            </tr>
            <tr height = 80%>
                <td colspan="4" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; width: 1396px; border-bottom: black 1px solid">
    <div  style="overflow :scroll; height :650px" >
                        <asp:GridView ID="lstinformacion" runat="server" 
                        AutoGenerateColumns="False" CssClass="style11" 
                            Height="16px" Width="99%"  DataKeyNames="codigo_pro">
                            <FooterStyle 
                                VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="codigo_ipr" HeaderText="Id">
                                    <HeaderStyle Font-Size="X-Small" BackColor="#F0F0F0" ForeColor="Blue" />
                                    <ItemStyle Font-Size="X-Small" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Programa" DataField="descripcion_pro" >
                                    <HeaderStyle Font-Size="X-Small" BackColor="#F0F0F0" ForeColor="Blue" />
                                    <ItemStyle Font-Size="X-Small" Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Rubro" DataField="descripcion_rub">
                                    <HeaderStyle Font-Size="X-Small" BackColor="#F0F0F0" ForeColor="Blue" />
                                    <ItemStyle Font-Size="X-Small" Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion_tip" HeaderText="Moneda">
                                    <HeaderStyle Font-Size="X-Small" BackColor="#F0F0F0" ForeColor="Blue" />
                                    <ItemStyle Font-Size="XX-Small"  Width ="5%"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion_Tplla" HeaderText="Tipo Planilla">
                                    <HeaderStyle Font-Size="X-Small" BackColor="#F0F0F0" ForeColor="Blue" />
                                    <ItemStyle Font-Size="X-Small"  Width ="15%"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="a&#241;o_ipr" HeaderText="A&#241;o">
                                    <HeaderStyle Font-Size="X-Small" BackColor="#F0F0F0" ForeColor="Blue" />
                                    <ItemStyle Font-Size="X-Small"  Width ="6%"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre_mes" HeaderText="Mes">
                                    <HeaderStyle Font-Size="X-Small" BackColor="#F0F0F0" ForeColor="Blue" />
                                    <ItemStyle Font-Size="X-Small" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Estado" DataField="estado_ipr" >
                                    <HeaderStyle Font-Size="X-Small" BackColor="#F0F0F0" ForeColor="Blue" />
                                    <ItemStyle Font-Size="X-Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="importe_ipr" HeaderText="Importe Total">
                                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right"  Width="8%" ForeColor="Maroon" />
                                    <HeaderStyle BackColor="#F0F0F0" Font-Size="X-Small" ForeColor="Blue" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion_ipr" HeaderText="Descripci&#243;n">
                                    <HeaderStyle BackColor="#F0F0F0" Font-Size="X-Small" ForeColor="Blue" />
                                    <ItemStyle Font-Names="Nina" Font-Size="X-Small" Width="320px" />
                                </asp:BoundField>
                                <asp:ImageField DataImageUrlField="codigo_ipr" 
                                    DataImageUrlFormatString="~/iconos/buscar.gif" AlternateText ="Click para visualizar/ modificar este informe">
                                    <HeaderStyle BackColor="#F0F0F0" />
                                </asp:ImageField>
                                <asp:ImageField DataImageUrlField="codigo_IPR" DataImageUrlFormatString="~/iconos/eliminar.gif" AlternateText ="Click para anular este informe">
                                </asp:ImageField>
                            </Columns>
                        </asp:GridView>
                        </div>
                    </td>
            </tr>
            <tr>
                <td colspan="4" style="background-color: #EFEFF7; width: 1396px;">
                    &nbsp;<span class="style15"><span class="style16" style="font-size: 9pt; font-family: Courier New">Estados &gt;&gt;   <br />
                    <tr>
                        <td colspan="4" style="width: 1396px; height: 29px; background-color: #ffffcc; vertical-align: middle;"><img src =
                            "iconos/buscar.gif" />
                            <span style="font-size: 9pt; font-family: Courier New">Estados &gt;&gt;&nbsp; <strong>
                                <span style="color: #993333">[Edición :</span><span style="color: blue"> </span>
                            </strong><span style="color: blue">aún sujeto modificación</span><strong><span style="color: #993333">]&nbsp;
                                [Disponible &nbsp;:</span></strong> <span style="color: blue">No sujeto a modificaciòn,
                                    listo para ser procesado y generar cargos </span><span style="color: #993333"><strong>
                                        ] [ Procesado : </strong></span><span style="color: blue">Cargos generados</span>
                            </span><strong><span style="font-size: 9pt; color: #993333; font-family: Courier New">
                                ]<br />
                                &gt; Desarrollo de Sistemas USAT</span></strong></td>
                    </tr>
    </form>
</body>
</html>
