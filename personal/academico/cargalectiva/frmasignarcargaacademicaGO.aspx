<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmasignarcargaacademicaGO.aspx.vb" Inherits="frmasignarcargaacademicaGO" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Asignación de Carga Académica</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
        <script type="text/javascript" language="javascript">
        function PintarFilaMarcada(obj,estado)
        {
            if (estado==true){
                obj.style.backgroundColor="#EBEBEB"//#395ACC
            }
            else{
                obj.style.backgroundColor="white"
            }
        }
        
        function MostrarCaja(tbl)
        {
            if (document.getElementById(tbl).style.display=="none"){
                document.getElementById(tbl).style.display=""
            }
            else{
                document.getElementById(tbl).style.display="none"
            }
        }
    </script>
    <style type="text/css">
    /* .... */
    .FondoAplicacion
    {
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
        .style1
        {
            width: 50%;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Asignación de Carga Académica según Programación&nbsp;
                <asp:DropDownList ID="dpCodigo_cac" runat="server" AutoPostBack="true">
                </asp:DropDownList>
                <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true"  runat="server"></asp:ScriptManager>
    </p>
<table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" 
            border="0">
        <tr style="background-color: #6694e3; color:White">
            <td style="height: 30px; "><asp:Label ID="lblnombre_dac" runat="server" Font-Bold="True" 
                    Font-Names="Verdana" Font-Size="8pt" 
                    
                    Text="Ud. no está registrado como Director de Dpto. Académico. Consultar con la Of. de Personal."></asp:Label>
                <asp:HiddenField ID="hdcodigo_dac" runat="server" Value="0" />
                <asp:HiddenField ID="hdAgregar" runat="server" Value="false" />
                <asp:HiddenField ID="hdModificar" runat="server" Value="false" />
                <asp:HiddenField ID="hdEliminar" runat="server" Value="false" />
                <asp:HiddenField ID="hdCodigo_Cup" runat="server" Value="0" />
            </td>
            <td style="height: 30px; ">
                                                <asp:DropDownList ID="dpCodigo_cpf" runat="server">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="dpEstado" runat="server">
                                                    <asp:ListItem Value="0">Cursos SIN Docente asignado</asp:ListItem>
                                                    <asp:ListItem Value="1">Cursos CON Docente asignado</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Button ID="cmdBuscar" runat="server" CssClass="buscar2" Height="22px" 
                                                    Text="   Buscar" Width="60px" />
            </td>
        </tr>
        </table>
    <br />

<asp:GridView ID="grwGruposProgramados" runat="server" 
                                                        AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="Solid" 
                                                        CellPadding="2" DataKeyNames="codigo_cup" Width="100%">
                                                        <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="#" />
                                                            <asp:TemplateField HeaderText="Asignatura">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblnombre_cur" runat="server" Text='<%# Bind("nombre_cur") %>'></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblidentificador_cur" runat="server" Font-Italic="True" 
                                                                        Text='<%# Bind("identificador_cur") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo">
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Escuela">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEscuela" runat="server" ForeColor="#0066FF" 
                                                                        Text='<%# Bind("abreviatura_cpf") %>'></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblPlan" runat="server" Font-Italic="True" ForeColor="#006666" 
                                                                        Text='<%# Bind("abreviatura_pes") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20%" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo">
                                                                <ItemStyle Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="creditos_cur" HeaderText="Crd">
                                                                <ItemStyle Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="horasteo_cur" HeaderText="HT">
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="horasase_cur" HeaderText="HA">
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="horaslab_cur" HeaderText="HL">
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="horaspra_cur" HeaderText="HP">
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="totalhoras_cur" HeaderText="TH">
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="estado_cup" HeaderText="Estado">
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="vacantes_cup" HeaderText="Vacantes">
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Profesor" ItemStyle-VerticalAlign="Top">
                                                            <ItemTemplate>
                                                                <asp:BulletedList ID="lstProfesores" runat="server" DataTextField="docente" 
                                                                    DataValueField="codigo_per" Font-Size="7pt">
                                                                </asp:BulletedList>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top"></ItemStyle>
                                                        </asp:TemplateField>
                                                            <asp:CommandField  ButtonType="Image" SelectImageUrl="../../../images/menu0.gif" 
                                                                SelectText="" ShowSelectButton="True" HeaderText=""  >
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="codigo_cup" ReadOnly="True" Visible="False">
                                                            <ItemStyle Font-Size="Smaller" ForeColor="White" Width="0px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" 
                                                            CssClass="usatsugerencia" Font-Bold="True" ForeColor="Red" />
                                                        <EmptyDataTemplate>
                                                            &nbsp;&nbsp;&nbsp;&nbsp; No se encontraron grupos horario registrados según los criterios 
                                                            seleccionados
                                                        </EmptyDataTemplate>
                                                        <HeaderStyle BackColor="#E8EEF7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                            BorderWidth="1px" ForeColor="#3366CC" />
                                                        <SelectedRowStyle BackColor="#6699FF" ForeColor="White" />
                                                    </asp:GridView>
                                            
    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="10pt" 
        ForeColor="Red"
        Text="&lt;img src=&quot;../../../images/bloquear.gif&quot;&gt;&amp;nbsp;El Acceso para Agregar/Modificar la Carga Académica ha finalizado."></asp:Label>
&nbsp;<asp:LinkButton ID="lnkSolicitar" runat="server" Font-Underline="True" 
        ForeColor="Blue" onclientclick="MostrarCaja('trPermiso');return(false)" 
        Visible="False" Font-Size="10pt">Haga clic aquí para [Solicitar Acceso]</asp:LinkButton>
&nbsp;<table cellpadding="3" cellspacing="0" style="border: 1px solid #808080; width:60%; border-collapse: collapse; background-color: #E5E5E5;display:none" ID="trPermiso">
            <tr>
                <td valign="top">
                    Indique el motivo por el cual solicita el Acceso.<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtmotivo" 
                        ErrorMessage="Debe ingresar un motivo para Cambio de Carga Académica" 
                        SetFocusOnError="True" ValidationGroup="MotivoCambio">*</asp:RequiredFieldValidator><br />
                    <asp:TextBox ID="txtmotivo" runat="server" CssClass="cajas" MaxLength="255" 
                        Rows="3" TextMode="MultiLine" Width="98%"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="cmdEnviar" runat="server" CssClass="guardar2" 
                        Text="    Enviar" ValidationGroup="MotivoCambio" />&nbsp;El acceso lo habilitará ViceRectorado Académico por un lapso de 24 horas.</td>
            </tr>
        </table>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        BorderStyle="Solid" BorderWidth="1px" />
    <br />
                                                <asp:Button ID="cmdPopUp" runat="server" Text="Abrir" style="display:none" />

            <br />
    <asp:Panel ID="fraProfesores" runat="server" Height="600px" 
        ScrollBars="Auto" Width="98%" CssClass="contornotabla">
        <table cellpadding="3" cellspacing="0" style="width:100%; border-collapse: collapse">
            <tr style="background-color: #3366FF; color: #FFFFFF; font-size: 14px; font-weight: bold;">
                <td style="width: 3%; ">
                    <asp:Button ID="cmdCancelar" runat="server" Text="X" 
                        ValidationGroup="Ninguna" />
                </td>
                <td style="width: 97%;">
                    Carga Académica</td>
            </tr>
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td valign="top" style="width: 97%">
                    <table style="width:100%;">
                        <tr>
                            <td class="style1">
                                Asignatura:
                                <asp:Label ID="lblcurso" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>
                            </td>
                            <td rowspan="2">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Blue" 
                                    Text="Horario del Curso:"></asp:Label>
                                <asp:BulletedList ID="bllHorarioCurso" runat="server" 
                                    ToolTip="Horario del Curso">
                                </asp:BulletedList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Grupo Horario:
                                <asp:Label ID="lblgrupo" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td valign="top" style="width: 97%; background-color: #E8EEF7;">
                    <asp:Label ID="lblGrupoProfesores" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td style="width: 97%;" valign="top">
                    <asp:Button ID="cmdAgregar" runat="server" CssClass="agregar2" 
                        onclientclick="MostrarCaja('trNuevo');return(false)" 
                        Text="      Agregar un profesor..." UseSubmitBehavior="False" 
                        Width="130px" Visible="False" />
                    &nbsp;Asignar Profesor:<asp:DropDownList ID="dpCodigo_per" runat="server" 
                        Width="350px" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" 
                        ControlToValidate="dpCodigo_per" ErrorMessage="Seleccione el Profesor" 
                        MaximumValue="500000" MinimumValue="1" Type="Double" 
                        ValidationGroup="NuevoProfesor">*</asp:RangeValidator>
                    <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar2" 
                        Text="    Guardar" ValidationGroup="NuevoProfesor" />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td style="width: 97%;" valign="top">
                    <asp:Label ID="lblCarga" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="lblMensajeCruce" runat="server" Font-Bold="True" 
                        Font-Size="Small" ForeColor="Red"></asp:Label>
                    <br />                    
                    <asp:GridView ID="dgvCruceHorarioDocente" runat="server" 
                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                        GridLines="None" Width="70%">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundField DataField="Escuela" HeaderText="Escuela" />
                            <asp:BoundField DataField="Curso" HeaderText="Curso" />
                            <asp:BoundField DataField="Grupo" HeaderText="Grupo" />
                            <asp:BoundField DataField="Día" HeaderText="Día" />
                            <asp:BoundField DataField="Inicio" HeaderText="Inicio" />
                            <asp:BoundField DataField="Fin" HeaderText="Fin" />
                            <asp:BoundField DataField="CruceInicio" HeaderText="CruceInicio" />
                            <asp:BoundField DataField="CruceFin" HeaderText="CruceFin" />
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td valign="top" style="width: 97%">
                    <asp:DataList ID="dlProfesores" runat="server" DataKeyField="codigo_per" 
                        RepeatColumns="4" RepeatDirection="Horizontal" Width="98%" 
                        GridLines="Vertical">
                        <ItemTemplate>
                            <table cellpadding="3" cellspacing="0" class="contornotabla" width="100%">
                                <tr>
                                    <td align="center" rowspan="6" width="13%">
                                        <asp:Image ID="FotoProfesor" runat="server" Height="104px" 
                                            ImageUrl='<%# "../" & eval("foto_per") %>' Width="90px" />
                                        <br />
                                    </td>
                                    <td width="87%">
                                        <asp:Label ID="lblprofesor" runat="server" Font-Bold="True" ForeColor="#CC6600" 
                                            Text='<%# eval("docente") %>'></asp:Label>
                                        <asp:HiddenField ID="hdCodigo_per" runat="server" 
                                            Value='<%# eval("codigo_per") %>' />
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
                                        <asp:Label ID="lblTipo" runat="server" Text='<%# eval("descripcion_fun") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="87%">
                                        Horas: Clase:
                                        <asp:Label ID="lblClase" runat="server" Text='<%# eval("totalHorasAula") %>'></asp:Label>
                                        &nbsp;. Asesoría:
                                        <asp:Label ID="lblAsesoria" runat="server" 
                                            Text='<%# eval("totalHorasAsesoria") %>'></asp:Label>
                                        . Total:
                                        <asp:Label ID="lblHoras" runat="server" Text='<%# eval("totalHoras_Car") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="87%">
                                        <asp:Button ID="cmdQuitar" runat="server" BorderColor="#999999" 
                                            BorderStyle="Solid" BorderWidth="1px" CausesValidation="False" 
                                            CommandName="delete" CssClass="eliminar2" Text="Quitar" Width="70px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="87%">
                                        <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="True" 
                                            ForeColor="Blue" Visible="False">Más detalles...</asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td style="width: 97%; background-color: #E8EEF7; font-weight: bold;" 
                    valign="top">
                    PROFESORES SUGERIDOS PARA LA ASIGNATURA
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                        Text="(Clic sobre el nombre del profesor para seleccionar)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td style="width: 97%; " 
                    valign="top">
                    <asp:BulletedList ID="blstProfesoresSugeridos" runat="server"  
                        BulletStyle="Numbered" DisplayMode="LinkButton" BorderStyle="None">
                    </asp:BulletedList>
                </td>
            </tr>
        </table>
    </asp:Panel>
                                   
    <cc1:ModalPopupExtender ID="mpeFicha" runat="server"
        CancelControlID="cmdCancelar"
        PopupControlID="fraProfesores"
        TargetControlID="cmdPopUp"  BackgroundCssClass="FondoAplicacion" Y="50" />
       
    </form>
</body>
</html>
