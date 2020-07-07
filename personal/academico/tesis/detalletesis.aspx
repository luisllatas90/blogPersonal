<%@ Page Language="VB" AutoEventWireup="false" CodeFile="detalletesis.aspx.vb" Inherits="personal_academico_tesis_detalletesis" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Detalle de tesis</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <style type="text/css">
        .titulobloque
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
            font-weight: bold;
            border-top-style: solid;
            border-width: 1px;
            border-color: #808080;
        }
    </style>
</head>
<body bgcolor="#eeeeee">
    <form id="form1" runat="server">
    <table width="100%" cellpadding="3" cellspacing="0">
        <tr>
            <td class="titulobloque" colspan="3">
                <table width="100%">
                    <tr>
                        <td style="width:60%"><asp:Label ID="lblTituloPagina" runat="server" 
                                Visible="False" Font-Bold="True" Font-Size="13pt" ForeColor="#003399">Ficha de Tesis</asp:Label></td>
                        <td style="width:40%" align="right">
                            <input id="cmdImprimir" class="imprimir3" type="button" value="  Imprimir" onclick="window.print()" />
                            <asp:Button ID="CmdCancelar" runat="server" CssClass="cerrar3" Text="        Regresar" Height="25px" Visible="False" 
                    onclientclick="return(history.back(-1))" UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width:5%" class="titulobloque" align="right">
                1.</td>
            <td style="width:65%" class="titulobloque">
                Datos informativos</td>
            <td style="width:30%" class="titulobloque">
                <asp:Label ID="lblcodigoReg" runat="server" Font-Bold="True" Font-Names="Tahoma" 
                    Font-Size="14px" ForeColor="Maroon" Height="20px" Width="100%"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td style="width:5%" >
                &nbsp;</td>
            <td colspan="2" style="width:95%">
            <table cellpadding="3" cellspacing="0" width="100%">
            <tr>
                <td>Fase</td>
                <td colspan="3"><asp:Label ID="lblFase" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Título</td>
                <td colspan="3"><asp:Label ID="lblTitulo" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Formulación del problema</td>
                <td colspan="3"><asp:Label ID="lblProblema" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Resumen</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4"><asp:Label ID="lblResumen" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Fecha inicio</td>
                <td><asp:Label ID="lblFechaInicio" runat="server"></asp:Label></td>
                <td align="right">Fecha término</td>
                <td><asp:Label ID="lblFechaFin" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Registrado por</td>
                <td colspan="3"><asp:Label ID="lblRegistrado" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            </table> 
           </td>
        </tr>
        <tr>
            <td style="width:5%" class="titulobloque" align="right">
                &nbsp;2.</td>
            <td style="width:65%" class="titulobloque">
                Autor (es)</td>
            <td style="width:30%" class="titulobloque">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width:5%">
                &nbsp;</td>
            <td style="width:85%" colspan="2">
                <asp:DataList ID="dlAutores" runat="server" RepeatColumns="2" 
                    RepeatDirection="Horizontal" Width="100%" 
                    DataKeyField="codigo_Rtes">
                    <ItemTemplate>
                        <table width="100%">
                            <tr>
                                <td align="center" rowspan="4" width="10%">
                                    <asp:Image ID="FotoAlumno" runat="server" Height="104px" Width="90px" />
                                </td>
                                <td width="90%">
                                    <asp:Label ID="lblcodigo" runat="server" 
                                        Text='<%# eval("codigouniver_alu") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="90%">
                                    <asp:Label ID="lblalumno" runat="server" Text='<%# eval("alumno") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="90%">
                                    <asp:Label ID="lblescuela" runat="server" Text='<%# eval("nombre_cpf") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="90%">
                                    <asp:Label ID="lblcicloingreso" runat="server" 
                                        Text='<%# "Ciclo de Ingreso: " + eval("cicloing_alu") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>        
        <tr>
            <td style="width:5%" class="titulobloque" align="right">
                3.</td>
            <td style="width:65%" class="titulobloque">
                Lineas de investigación</td>
            <td style="width:30%" class="titulobloque" align="right">
                <img alt="" src="../../../images/menos.gif" style="cursor: pointer" onClick="MostrarTabla(document.all.BloqueLineas,this,'../../../images/')" />
            </td>
        </tr>
        
        <tr id="BloqueLineas">
            <td style="width:5%">&nbsp;</td>
            <td colspan="2">
                                <asp:BulletedList ID="lstLineas" runat="server" DataTextField="nombre_are" 
                                    DataValueField="codigo_are">
                                </asp:BulletedList>
                                </td>
        </tr>
        <tr>
        <td class="titulobloque" colspan="2">
            <asp:Label ID="lblEtapas" runat="server" Text="&amp;nbsp;&amp;nbsp;4. Etapas" 
                Visible="False"></asp:Label>
                </td>
        <td style="width:30%" class="titulobloque" align="right">
            <asp:ImageButton ID="CmdEstados" runat="server" 
                ImageUrl="../../../images/menos.gif" 
                onclientclick="MostrarTabla(document.all.BloqueEstado,this,'../../../images/');return(false)" 
                Visible="False" /></td>
        </tr>
        <tr id="BloqueEstado">
            <td style="width:5%">&nbsp;</td>
            <td colspan="2">
    <asp:GridView ID="grdEstados" runat="server" AutoGenerateColumns="False" CellPadding="4" 
        DataKeyNames="codigo_Etes,codigo_Eti" ForeColor="#333333" Width="100%">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="fechaAprobacion_Etes" 
                HeaderText="Fecha Aprobación" >
                <ItemStyle Font-Size="8pt" Width="20%" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="nombre_Eti" HeaderText="Etapa" >
                <ItemStyle Font-Size="8pt" Width="25%" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Registrado por">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("OpRegistro") %>'></asp:Label>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text='<%# eval("obs_Etes") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Font-Size="8pt" Width="60%" />
            </asp:TemplateField>
        </Columns>
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
                                </td>
        </tr>
        <tr>
            <td style="width:5%" class="titulobloque" align="right">
                5.</td>
            <td style="width:65%" class="titulobloque">
                Asesor / Asesorías</td>
            <td style="width:30%" class="titulobloque" align="right">
            <img alt="" src="../../../images/menos.gif" style="cursor: pointer" onClick="MostrarTabla(document.all.BloqueAsesor,this,'../../../images/')" />
            </td>
        </tr>
        <tr id="BloqueAsesor">
            <td style="width:5%">
                &nbsp;</td>
            <td colspan="2">
                <asp:DataList ID="dlAsesores" runat="server" RepeatColumns="2" 
                    RepeatDirection="Horizontal" Width="100%" 
                    DataKeyField="codigo_Rtes">
                    <ItemTemplate>
                        <table width="100%" cellpadding="3" cellspacing="0" class="contornotabla">
                            <tr>
                                <td align="center" rowspan="5" width="13%">
                                    <asp:Image ID="FotoAlumno" runat="server" Height="104px" 
                                        ImageUrl='<%# eval("foto_per") %>' Width="90px" />
                                    <br />
                                </td>
                                <td width="87%">
                                    <asp:Label ID="lblasesor" runat="server" Font-Bold="True" ForeColor="#CC6600" 
                                        Text='<%# eval("docente") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="87%">
                                    <asp:Label ID="lblcategoria" runat="server" 
                                        Text='<%# eval("descripcion_tpe") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="87%">
                                    <asp:Label ID="lblDedicacion" runat="server" 
                                        Text='<%# eval("descripcion_ded") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="87%">
                                    <asp:Label ID="lblTipo" runat="server" Text='<%# eval("descripcion_tpi") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="87%">
                                    <asp:Label ID="lblemail" runat="server" Text='<%# eval("email_per") %>' 
                                        ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
                </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2">
                <asp:GridView ID="gvListaArchivos" runat="server" CellPadding="4" 
                    ForeColor="#333333" GridLines="None" Width="100%" 
                    Caption="Listado de Asesorías" Font-Bold="true" Font-Size=X-Small>
                    <RowStyle BackColor="#EFF3FB" HorizontalAlign=Center/>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF"/>
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>

