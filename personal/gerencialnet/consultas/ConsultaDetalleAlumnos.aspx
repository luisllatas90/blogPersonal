<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConsultaDetalleAlumnos.aspx.vb" Inherits="consultas_ConsultaDetalleAlumnos" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="../css/estilos.css" rel="stylesheet" type="text/css" />
    <title>Consulta de Datos de Estudiantes</title>

</head>
<body>
    <form id="form1" runat="server">

            <table id="tblData"  width="95%" border="0" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="font-size: 8pt; vertical-align: middle; font-family: Verdana; height: 30px; background-color: steelblue; text-align: center; font-weight: bold; color: white; font-variant: small-caps; border-bottom: 1px solid;" title="Consulta de Datos de Estudiantes">
                        Consulta de Datos de Estudiantes</td>
                </tr>
                <tr>
                    <td style="font-size: 8pt; color: black; border-bottom: black 1px solid; font-family: Verdana; height: 181px; width:100%">
                        <div style="text-align: left">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 50%">
                                <tr>
                                    <td style="width: 100%; height: 20px; text-align: left; font-size: 8pt; color: black; font-family: Verdana; font-variant: small-caps;">
                        Seleccione los criterios de búsqueda:</td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <table border="0" cellpadding="3" cellspacing="0" style="width: 100%; text-align: left">
                                            <tr>
                                                <td style="height: 20px; text-align: left; font-size: 8pt; color: black; font-family: Verdana;">
                                                    Carrera</td>
                                                <td style="width: 20%; height: 30px; text-align: left">
                                                    <asp:DropDownList ID="ddlCarrera" runat="server" Width="100px">
                                                    </asp:DropDownList></td>
                                                <td style="height: 20px; text-align: left; font-size: 8pt; color: black; font-family: Verdana;">
                                                    Ciclo. Matrícula</td>
                                                <td style="width: 20%; height: 30px; text-align: left">
                                                    <asp:DropDownList ID="ddlCicloMat" runat="server" Width="100px">
                                                    </asp:DropDownList></td>
                                                <td style="height: 20px; text-align: left; font-size: 8pt; color: black; font-family: Verdana;">
                                                    Ciclo Ingreso</td>
                                                <td style="width: 20%; height: 30px">
                                                    <asp:DropDownList ID="ddlCicloIng" runat="server" Width="100px">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px;
                                                    text-align: left; font-size: 8pt; color: black; font-family: Verdana;">
                                                    Sexo</td>
                                                <td style="border-bottom-width: 1px; border-bottom-color: #3366cc; width: 10%; height: 30px;
                                                    text-align: left">
                                                    <asp:DropDownList ID="ddlsexo" runat="server" Width="100px">
                                                        <asp:ListItem Value="-1">--- Todos ---</asp:ListItem>
                                                        <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                        <asp:ListItem Value="F">Femenino</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td style="height: 20px;
                                                    text-align: left; font-size: 8pt; color: black; font-family: Verdana;">
                                                    Condición</td>
                                                <td style="border-bottom-width: 1px; border-bottom-color: #3366cc; width: 10%; height: 30px;
                                                    text-align: left">
                                                    <asp:DropDownList ID="ddlCondicion" runat="server" Width="100px">
                                                        <asp:ListItem Value="-1">--- Todos ---</asp:ListItem>
                                                        <asp:ListItem Value="I">Ingresante</asp:ListItem>
                                                        <asp:ListItem Value="P">Postulante</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td style="height: 20px;
                                                    text-align: left; font-size: 8pt; color: black; font-family: Verdana;">
                                                    Nombre</td>
                                                <td style="border-bottom-width: 1px; border-bottom-color: #3366cc; width: 10%; height: 30px;
                                                    text-align: left">
                                                    <asp:TextBox ID="txtNombre" runat="server" Width="100px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold; height: 20px; text-align: left; font-size: 8pt; color: black; font-family: Verdana;">
                                                    Colegio</td>
                                                <td style="width: 20%; height: 30px; text-align: left">
                                                    <asp:DropDownList ID="ddlDepartamento" runat="server" AutoPostBack="True" Width="100px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 15%; height: 30px; text-align: left">
                                                    <asp:DropDownList ID="ddlProvincia" runat="server" AutoPostBack="True" Width="100px">
                                                    </asp:DropDownList></td>
                                                <td style="width: 20%; height: 30px; text-align: left">
                                                    <asp:DropDownList ID="ddlDistrito" runat="server" AutoPostBack="True" Width="100px">
                                                    </asp:DropDownList></td>
                                                <td colspan="2" style="height: 30px; text-align: left">
                                                    <asp:DropDownList ID="ddlColegio" runat="server" Width="250px">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%; height: 30px; text-align: left">
                                                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Size="Smaller" Visible="False">Ver Estadísticos</asp:HyperLink></td>
                                                <td style="width: 20%; height: 30px; text-align: left">
                                                </td>
                                                <td style="width: 15%; height: 30px; text-align: left">
                                                </td>
                                                <td style="width: 20%; height: 30px; text-align: left">
                                                </td>
                                                <td style="width: 15%; height: 30px; text-align: left">
                                                    <asp:Button ID="CmdConsultar" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                        Height="24px" Style="font-size: 8pt; background: url(../images/previo.gif) white no-repeat left center;
                                                        color: black; font-family: Verdana" Text="   Consultar" Width="80px" /></td>
                                                <td style="width: 20%; height: 30px; text-align: left">
                                                    <asp:Button ID="cmdExportar" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                        Height="24px" Style="font-size: 8pt; background: url(../images/cubo.jpg) white no-repeat left center;
                                                        color: black; font-family: Verdana" Text="    Exportar" Width="80px" CausesValidation="False" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" style=" width:100%">
                        &nbsp;<asp:GridView AllowPaging="True" AllowSorting="True" DataSourceID="DetalleAlumno" ForeColor="White" ID="GridView1" PageSize="5" runat="server" >
                            <RowStyle CssClass="DatosConsulta" />
                            <PagerStyle BorderColor="Black" Font-Names="Verdana" Font-Size="6pt" ForeColor="ActiveCaption" />
                            <HeaderStyle CssClass="titulos" Height="25px" />
                            <AlternatingRowStyle CssClass="nombrefila" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>

                        <asp:ObjectDataSource ID="DetalleAlumno" runat="server" SelectMethod="ConsultaDatosDetalladoAlumnos"
                            TypeName="clsReportes">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="PR" Name="tipo" Type="String" />
                                <asp:ControlParameter ControlID="ddlCicloMat" DefaultValue="" Name="codigo_cac" PropertyName="SelectedValue"
                                    Type="String" />
                                <asp:ControlParameter ControlID="ddlCicloIng" Name="cicloIng_Alu" PropertyName="SelectedValue"
                                    Type="String" />
                                <asp:ControlParameter ControlID="ddlsexo" Name="sexo" PropertyName="SelectedValue"
                                    Type="String" />
                                <asp:ControlParameter ControlID="ddlColegio" Name="codigo_col" PropertyName="SelectedValue"
                                    Type="String" />
                                <asp:ControlParameter ControlID="ddlCarrera" Name="codigo_cpf" PropertyName="SelectedValue"
                                    Type="String" />
                                <asp:ControlParameter ControlID="ddlCondicion" Name="CONDICION" PropertyName="SelectedValue"
                                    Type="String" />
                                <asp:ControlParameter ControlID="txtNombre" DefaultValue=" " Name="INGRESANTE" PropertyName="Text"
                                    Type="String" />
                                <asp:ControlParameter ControlID="txtNombre" DefaultValue=" " Name="NOMBRE" PropertyName="Text"
                                    Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
        <br />
        &nbsp;
    </form>
</body>
</html>
