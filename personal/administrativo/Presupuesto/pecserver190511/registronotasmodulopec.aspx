<%@ Page Language="VB" AutoEventWireup="false" CodeFile="registronotasmodulopec.aspx.vb" Inherits="registronotasmodulopec" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Acta de Registro de Notas por Módulo</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>    
    <script src="../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../private/jq/jquery.mascara.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function PintarFilaMarcada(obj, estado) {
            if (estado == true) {
                obj.style.backgroundColor = "#FFE7B3"
            }
            else {
                obj.style.backgroundColor = "white"
            }
        }

        $(document).ready(function() {
            jQuery(function($) {
                $("#txtInicio").mask("99/99/9999");
                $("#txtFin").mask("99/99/9999");
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Acta de Registro de Notas</p>
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">                                        
                                        <tr style="background-color: #E8EEF7; font-weight: bold;">
                                            <td colspan="3">
                                                Datos del Módulo</td>
                                            <td align="right">
                                                <input id="cmdImprimir" class="imprimir2" title="Imprimir" type="button" 
                                                    value="Imprimir" onclick="window.print()" />
                                                <input id="cmdCerrar" class="eliminar2" title="Cerrar" type="button" 
                                                    value="Cerrar" onclick="top.window.close()" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Programa</td>
                                            <td colspan="3" style="width: 85%">
                                                <asp:Label ID="lblDescripcion_pes" runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Denominación</td>
                                            <td colspan="3" style="width: 85%">
                                                <asp:Label ID="lblnombre_cur" runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">Profesor</td>
                                            <td colspan="3" style="width: 85%">
                                                <asp:DropDownList ID="dpProfesor" runat="server" CssClass="cajas" Width="95%">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblProfesor" runat="server" Font-Bold="True" ForeColor="Green" 
                                                    Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Fecha Inicio</td>
                                            <td style="width: 10%">
                                                <asp:TextBox ID="txtInicio" runat="server" CssClass="cajas" Width="90px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RqFechaInicio" runat="server" 
                                                    ControlToValidate="txtInicio" 
                                                    ErrorMessage="Debe especificar la fecha de inicio del Módulo">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td align="right" style="width: 10%">
                                                Fecha Fin:</td>
                                            <td style="width: 70%">
                                                <asp:TextBox ID="txtFin" runat="server" CssClass="cajas" Width="90px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RqFechaFin" runat="server" 
                                                    ControlToValidate="txtFin" 
                                                    ErrorMessage="Debe especificar la fecha de término del Módulo">*</asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CRqFechaFin" runat="server" 
                                                    ControlToCompare="txtFin" ControlToValidate="txtInicio" 
                                                    ErrorMessage="Fecha de Termino menor o igual a fecha de inicio." 
                                                    Operator="LessThan" Type="Date">*</asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                <asp:HiddenField ID="hdestadonota_cup" runat="server" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                    ShowMessageBox="True" ShowSummary="False" />
                                            </td>
                                            <td style="width: 85%; color: blue;" valign="top" colspan="3">
                                            <ul>
                                                <li>El registro de notas finales se hace en una sola acción. Asigne notas entre 0 y 
                                                20 para Aprobado (A) o Desaprobado(D); y -1 cuando la nota está Pendiente (P).</li>
                                                <li>Si el profesor no se encuentra en la lista, seleccionar al Coordinador del Programa.</li>
                                                <asp:Label ID="lblmensaje" runat="server" CssClass="rojo" Font-Bold="True"></asp:Label>
                                            </ul>
                                                </td>
                                        </tr>
                                        </table>
    <br />
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">                                        
                                        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                                            <td >Lista de participantes Matriculados en el Módulo</td>
                                            <td align="right">
                                                <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar2" Text="Guardar" />
                                                </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="grwParticipantes" runat="server" 
                                                    AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Top" 
                                                    DataKeyNames="codigo_alu,codigo_dma" BorderColor="Silver" 
                                                    EnableModelValidation="True" CellPadding="2" 
                                                    Width="100%">
                                                    <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" 
                                                        HorizontalAlign="Center" />
                                                    <EditRowStyle BackColor="#FFFF66" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Nro.">
                                                        <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="codigouniver_alu" HeaderText="Código" >
                                                        <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="participante" HeaderText="Apellidos y Nombres" >
                                                        <ItemStyle HorizontalAlign="Left" Width="55%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="estado_dma" HeaderText="Estado" >
                                                        <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Nota Final">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtNota" runat="server" MaxLength="2" 
                                                                    Text='<%# bind("notafinal_dma") %>' Width="50%" 
                                                                    AutoCompleteType="Disabled" CssClass="cajas" Font-Names="Verdana"></asp:TextBox><asp:Label ID="lblNota" runat="server" Text='<%# Bind("notafinal_dma") %>' 
                                                                    Visible="False"></asp:Label>
                                                                <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                                                    ControlToValidate="txtNota" 
                                                                    ErrorMessage="Debe ingresar notas en 0 y 20 para Aprobar o Desaprobar; ó -1 para notas pendientes" 
                                                                    MaximumValue="20" MinimumValue="-1" Type="Double">*</asp:RangeValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                    ControlToValidate="txtNota" 
                                                                    ErrorMessage="Debe ingresar un valor en la nota. Si no desea evaluarlo aún, asigne: -1">*</asp:RequiredFieldValidator>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="8%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="condicion_dma" HeaderText="Condición">
                                                        <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:ButtonField ButtonType="Image" ImageUrl="../../images/editar.gif" 
                                                            Text="Editar" HeaderText="Modificar" CommandName="Modificar" 
                                                            Visible="False">
                                                        <ItemStyle Width="5%" />
                                                        </asp:ButtonField>
                                                        <asp:ButtonField ButtonType="Image" HeaderText="Retiro" 
                                                            ImageUrl="../../images/bloquear.gif" Text="Retiro" CommandName="Retirar" 
                                                            Visible="False" />
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        No se encontraron participantes en el curso
                                                    </EmptyDataTemplate>
                                                    <FooterStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
                                                </td>
                                        </tr>
                                    </table>
    <br />
    <asp:Label ID="lblA" runat="server" Font-Bold="True" Font-Names="Verdana" 
        Font-Size="10pt" ForeColor="Blue"></asp:Label>
&nbsp;|
    <asp:Label ID="lblD" runat="server" Font-Bold="True" Font-Names="Verdana" 
        Font-Size="10pt" ForeColor="Red"></asp:Label>
&nbsp;|
    <asp:Label ID="lblP" runat="server" Font-Bold="True" Font-Names="Verdana" 
        Font-Size="10pt" ForeColor="Green"></asp:Label>
    
    
    </form>
</body>
</html>
