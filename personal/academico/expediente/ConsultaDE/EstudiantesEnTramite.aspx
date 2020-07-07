    <%@ Page Language="VB" AutoEventWireup="false" CodeFile="EstudiantesEnTramite.aspx.vb" Inherits="EstudiantesEnTramite" %>

<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="Stylesheet" type ="text/css" />
    <script type="text/javascript" src="../private/funciones.js" language ="javascript"></script>
    <style type="text/css">
        .style1
        {
            height: 19px;
        }
        .style2
        {
            text-align: left;
            font-size:13px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
                <table align="center" width="100%" height="100%" border="0" cellpadding="0" 
            cellspacing="0">
                    <tr align="center" >
                        <td align="justify" colspan="4"  >
                                    &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4" >
                                                Buscar por
                                                <asp:DropDownList ID="CmbBuscarpor" runat="server" 
                                            AutoPostBack="True">
                                                    <asp:ListItem Value="1">Apellidos y Nombres</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="2">Codigo 
                                            Universitario</asp:ListItem>
                                                </asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="TxtBuscar" runat="server" Width="50%"></asp:TextBox>
                                            &nbsp;<asp:Button ID="CmdBuscar" runat="server" Text="Buscar" CssClass="buscar" 
                                            Height="20px" Width="80px"  />
                        </td>
                        </tr>
                    <tr>
                        <td align="left" colspan="4" >
                            &nbsp;</td>
                        </tr>
                    <tr align="center" style="background-color: #3366CC; color: #FFFFFF;" >
                        <td align="center" style="height:20px;width:20%"%>
                            Cod. Universitario</td>
                        <td align="center" style="height:20px;width:30%" >
                            Apellidos y Nombres</td>
                        <td align="center" style="height:20px;width:30%">
                                        Carrera Profesional</td>
                        <td align="center" style="height:20px;width:20%">
                                        Estado </td>
                    </tr>
                    <tr align="center">
                        <td align="justify" colspan="4" valign="top" style="width:100%; height:150px" 
                            class="cajas3" >
                         <div id="listadiv" style="height:100%;width:100%" align="left" >
                                <asp:GridView ID="GvAlumnos" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="codigo_alu,codigo_pes" 
                                    GridLines="Horizontal" ShowHeader="False" 
                                    style="margin-right: 0px" BorderWidth="0px" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="Codigo_Alu" HeaderText="Codigo_Alu" 
                                            InsertVisible="False" ReadOnly="True" SortExpression="Codigo_Alu" 
                                            Visible="False" />
                                        <asp:BoundField DataField="Codigouniver_alu" 
                                            SortExpression="Codigouniver_alu" HeaderText="Cod Universitario">
                                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombres" HeaderText="Apellidos y Nombres" 
                                            SortExpression="nombres" ReadOnly="True" >
                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre_cpf" SortExpression="nombre_cpf" 
                                            HeaderText="Escuela">
                                            <ItemStyle Width="220px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estado_alu" 
                                            SortExpression="estado_alu" HeaderText="Estado">
                                            <ItemStyle Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo_pes" 
                                            SortExpression="codigo_pes" HeaderText="codigo_pes" Visible="False">
                                            <ItemStyle Width="80px" />
                                        </asp:BoundField>
                                        <asp:CommandField SelectText=" " ShowSelectButton="True">
                                            <ItemStyle Width="1px" />
                                        </asp:CommandField>
                                    </Columns>
                                    <SelectedRowStyle BackColor="#FFFFD2" />
                                </asp:GridView>
                                 </div>
                                </td>
                    </tr>
                    <tr align="center" >
                        <td align="left" colspan="4" valign="top">
                            &nbsp;</td>
                    </tr>
                    <tr align="center" >
                        <td align="justify" colspan="4" valign="top">
                            <div>
                                <asp:Panel ID="Panel1" runat="server" Visible="False">
                                    <table style="width:100%;">
                                        <tr>
                                            <td align="left" rowspan="8" valign="top">
                                                <asp:Image ID="ImgFoto" runat="server" Height="135px" Width="100px" 
                                                    BorderWidth="1px" />
                                            </td>
                                            <td align="left" colspan="2" valign="top">
                                                Código universitario:&nbsp;<asp:Label ID="LblCodigoUniv" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                                Apellidos y nombres:&nbsp;<asp:Label ID="LblNombres" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2" class="style1">
                                                Plan de estudios:&nbsp;<asp:Label ID="LblPlanEstudio" runat="server"></asp:Label>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                                Cantidad de cursos del plan:&nbsp;<asp:Label ID="LblNroCursosPlan" runat="server" 
                                                    ForeColor="#0000CC"></asp:Label>
                                                &nbsp;--&gt; Cantidad cusos aprobados:&nbsp;<asp:Label ID="LblNroCursosAprobados" 
                                                    runat="server" ForeColor="#0000CC"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                                Total de créditos del plan:&nbsp;<asp:Label ID="LblCreditosTot" runat="server" 
                                                    ForeColor="#0000CC"></asp:Label>
                                                &nbsp;--&gt; Créditos aprobados:&nbsp;<asp:Label ID="LblCreditosAprob" runat="server" 
                                                    ForeColor="#0000CC"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <i>Créditos obligatorios:</i>&nbsp;<asp:Label ID="LblCredObligatorios" 
                                                    runat="server" ForeColor="#0000CC"></asp:Label>
                                                &nbsp;<i>Créditos electivos:</i>
                                                <asp:Label ID="LblCredElectivos" runat="server" ForeColor="#0000CC"></asp:Label>
                                                &nbsp;<i>Créditos para egresar:
                                                <asp:Label ID="lblCreditosEgresar" runat="server" ForeColor="#0000CC"></asp:Label>
                                                </i>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td align="left">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="../images/bola_roja.gif" />
                                                &nbsp;Faltantes&nbsp;&nbsp;&nbsp;<asp:Image ID="Image2" runat="server" 
                                                    ImageUrl="../images/bola_verde.gif" />
                                                &nbsp;Aprobados&nbsp;&nbsp;&nbsp;<asp:Image ID="Image3" runat="server" 
                                                    ImageUrl="../images/bola_amar.gif" />
                                                &nbsp;Convalidados&nbsp;
                                                <asp:Image ID="Image4" runat="server" ImageUrl="../images/bola_naranja.gif" />
                                                &nbsp;Electivos no llevados&nbsp;
                                                <asp:Image ID="Image5" runat="server" ImageUrl="../images/bola_azul.gif" />
                                                Matriculados</td>
                                            <td align="right">
                                                <asp:Button ID="CmdVerHistorial" runat="server" CssClass="boton" 
                                                    Text="Otros cursos aprobados" UseSubmitBehavior="False" Width="150px" 
                                                    Visible="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="style2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" class="style2">
                            &nbsp;&nbsp;<b>≈: </b>significa que el curso tiene equivalencias, ubicarse en el 
                            nombre del curso para mostrar los datos del curso equivalente.<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;En caso, no se encuentren registradas las equivalencias correspondientes, 
                            coordinar con el área de <B>DIRECCIÓN ACADÉMICA</B></td>
                    </tr>
                    <tr>
                        <td colspan="4" class="style2">
                            &nbsp;</td>
                    </tr>
                    <tr align="center" >
                        <td align="justify" colspan="4" valign="top">
                            <asp:GridView ID="GvPlanMatricula" runat="server" 
                                Width="100%" EnableViewState="False" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="codigo_alu" HeaderText="Nro" />
                                    <asp:BoundField DataField="descripcion_cac" HeaderText="Sem. Acad." />                                    
                                    <asp:BoundField DataField="nombre_cur" HeaderText="Curso" />                         
                                    <asp:BoundField DataField="creditos_cur" HeaderText="Creditos" />
                                    <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" />
                                    <asp:BoundField DataField="electivo_cur" HeaderText="Electivo" />
                                    <asp:BoundField DataField="notafinal_dma" HeaderText="Nota Final" />
                                    <asp:BoundField HeaderText="Estado" />
                                    <asp:BoundField DataField="PlanEscuelaE" HeaderText="PlanEstudio" Visible="false" />
                                </Columns>
                                <HeaderStyle BackColor="#0066CC" ForeColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                    </table>
                
    <asp:HiddenField ID="HddCodigo_Alu" runat="server" />
     
                                    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" BackColor="White" 
                                    Overlay="False" Text="Se está procesando su información" 
                                    Title="Por favor espere" />
    </form>

</body>
</html>
