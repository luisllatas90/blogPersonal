<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmtesis.aspx.vb" Inherits="frmtesis" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Proyecto</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="private/PopCalendar.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
</head>
<body>
<form id="form1" runat="server">
   <%Response.Write(ClsFunciones.CargaCalendario)%>
   <p class="usatTituloPagina">Registro de Tesis Universitarias<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager></p>
    <table width="100%" cellpadding="3" cellspacing="0">       
        <tr>
            <td colspan="2">
            <table cellpadding="3" cellspacing="0" width="100%">
        <tr>
            <td>
                <b>Título: <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtTitulo" 
                    ErrorMessage="Debe ingresar el título de la tesis">*</asp:RequiredFieldValidator>
                            </b></td>
            <td colspan="3" align="right">
                <asp:Label ID="lblFase" runat="server" Font-Bold="True"></asp:Label> 
                <asp:Label ID="lblcodigo" runat="server" Font-Bold="True" Font-Names="Tahoma" 
                    Font-Size="14px" ForeColor="Maroon"></asp:Label><asp:HiddenField ID="hdCodigo_Eti" runat="server" Value="4" />
                            </td>
        </tr>
        <tr>
            <td colspan="4">
                                <asp:TextBox ID="txtTitulo" runat="server" CssClass="cajas2" 
                    MaxLength="500" Rows="3" Width="98%"></asp:TextBox>
                            </td>
        </tr>
        <tr>
            <td colspan="4">
                                <b>Formulación del Problema: <asp:RequiredFieldValidator ID="RqProblema" runat="server" 
                    ControlToValidate="txtProblema" ErrorMessage="Debe ingresar la Formulación del Problema">*</asp:RequiredFieldValidator>
                                </b>
                            </td>
        </tr>
        <tr>
            <td colspan="4">
                                <asp:TextBox ID="txtProblema" runat="server" CssClass="cajas2" 
                                    TextMode="MultiLine" MaxLength="1000" Rows="3" Width="98%"></asp:TextBox>
                            </td>
        </tr>
        <tr>
            <td colspan="4">
                <b>Resumen: </b>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtResumen" 
                    ErrorMessage="Debe ingresar el resumen de la tesis">*</asp:RequiredFieldValidator>
                            </td>
        </tr>
        <tr>
            <td colspan="4">
                                <asp:TextBox ID="txtResumen" runat="server" CssClass="cajas2" 
                                    TextMode="MultiLine" Height="200px" MaxLength="2000" Width="98%"></asp:TextBox>
                            </td>
        </tr>
        <tr>
            <td>
                <b>Fecha de aprobación</b></td>
            <td>
                <asp:TextBox ID="txtFechaInicio" runat="server" Width="100px" ForeColor="Navy" 
                    style="text-align: right" BackColor="#CCCCCC" MaxLength="12" 
                    AutoPostBack="True"></asp:TextBox>
                <asp:Button ID="cmdInicio" runat="server" 
                    onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaInicio,'dd/mm/yyyy');return(false)" 
                    Text="..." CausesValidation="False" UseSubmitBehavior="False" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtFechaInicio" 
                    ErrorMessage="Debe especificar la fecha de inicio">*</asp:RequiredFieldValidator>
            </td>
            <td align="right">
                <b>Fecha término</b></td>
            <td>
                <asp:TextBox ID="txtFechaFin" runat="server" Width="100px" ForeColor="Navy" 
                    style="text-align: right" BackColor="#CCCCCC" MaxLength="12"></asp:TextBox>
                <asp:Button ID="cmdFin" runat="server" 
                    onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaFin,'dd/mm/yyyy');return(false)" 
                    Text="..." CausesValidation="False" UseSubmitBehavior="False" />
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
                    runat="server" ControlToValidate="txtFechaInicio" 
                    ErrorMessage="Debe especificar la fecha de término">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator
                                                    ID="CompareValidator1" runat="server" 
                    ControlToCompare="txtFechaFin" ControlToValidate="txtFechaInicio"
                                                    
                    ErrorMessage="Fecha de Termino menor o igual a fecha de inicio." Operator="LessThan"
                                                    Type="Date">*</asp:CompareValidator>
                    </td>
        </tr>
        <tr>
            <td>
                <b>Enfoque Inv.</b></td>
            <td>
                <asp:DropDownList ID="dpenfoque" runat="server">
                    <asp:ListItem Value="NO DEFINIDO">--Seleccione--</asp:ListItem>
                    <asp:ListItem>CUANTITATIVO</asp:ListItem>
                    <asp:ListItem>CUALITATIVO</asp:ListItem>
                    <asp:ListItem>CUANTITATIVO-CUALITATIVO</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="right">
                <b>Nro. de Resolución</b></td>
            <td>
                <asp:TextBox ID="txtResolucion" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RqResolucion" runat="server" 
                    ControlToValidate="txtResolucion" 
                    ErrorMessage="Debe ingresar el Número de Resolución">*</asp:RequiredFieldValidator>
                    </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEtiquetaRegistrado" runat="server" Text="Registrado" 
                    Visible="False" style="font-weight: 700"></asp:Label>
            </td>
            <td colspan="3">
                <asp:Label ID="lblRegistrado" runat="server" Font-Bold="True"></asp:Label>
                            </td>
        </tr>
        </table> 
            </td>
        </tr>
        <tr>
            <td colspan="2">
                    <asp:UpdatePanel ID="uPAutores" runat="server">
                    <ContentTemplate>
                           <table style="width:100%">
                                    <tr>
                                        <td style="width:15%">
                                            <b>Autor</b>
                                        </td>
                                        <td style="width:85%">
                                            <asp:TextBox ID="txtAlumno" runat="server" CssClass="cajas2" MaxLength="50" 
                                                Width="50%"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="imgBuscarAutor" runat="server" ImageUrl="../../../images/menus/buscar_small12.gif" ValidationGroup="Cancelar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:15%">&nbsp;</td>
                                        <td style="width:85%">
                                        <asp:GridView ID="grwAutor" runat="server" AutoGenerateColumns="False" 
                                        BorderColor="#628BD7" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                        ForeColor="#333333" Width="50%" ShowHeader="False" 
                                        DataKeyNames="codigo_RTes">
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <Columns>
                                        <asp:TemplateField HeaderText="Estudiante">
                                        <ItemTemplate>
                                        <asp:Label ID="lblAlumno" runat="server" Text='<%# Bind("alumno") %>'></asp:Label>
                                        &nbsp;(<asp:Label ID="lblCodigo" runat="server" 
                                            Text='<%# Bind("codigouniver_alu") %>'></asp:Label>
                                        )<br />
                                        <asp:Label ID="lblEscuela" runat="server" Font-Italic="True" 
                                            Text='<%# eval("nombre_cpf") %>'></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cicloIng_alu" HeaderText="Ciclo Ingreso">
                                        </asp:BoundField>
                                        <asp:CommandField ShowDeleteButton="True">
                                        </asp:CommandField>
                                        </Columns>
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                        <asp:Panel ID="Panel1" runat="server" CssClass="contornotabla" Height="150px" 
                                                ScrollBars="Auto" Visible="False" Width="50%">
                                            <asp:GridView ID="grwAlumnos" runat="server" AutoGenerateColumns="False" 
                                                BorderColor="#628BD7" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                                DataKeyNames="codigo_alu" ForeColor="#333333" ShowHeader="False" Width="100%">
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Estudiante">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAlumno" runat="server" Font-Bold="True" ForeColor="#3366CC" 
                                                                Text='<%# Bind("alumno") %>'></asp:Label>
                                                            &nbsp;<asp:Label ID="lblCodigo" runat="server" 
                                                                Text='<%# Bind("codigouniver_alu") %>' Visible="False"></asp:Label>
                                                            <asp:Label ID="lblcodigo_alu" runat="server" Text='<%# eval("codigo_alu") %>' 
                                                                Visible="False"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblEscuela" runat="server" Font-Italic="True" 
                                                                Text='<%# eval("nombre_cpf") %>'></asp:Label>
                                                            &nbsp;- Ingreso:
                                                            <asp:Label ID="lblIngreso" runat="server" Font-Italic="True" 
                                                                Text='<%# eval("cicloing_alu") %>'></asp:Label>
                                                            <asp:Label ID="lblcodigo_fac" runat="server" Text='<%# eval("codigo_fac") %>' 
                                                                Visible="False"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="95%" />
                                                    </asp:TemplateField>
                                                    <asp:ButtonField ButtonType="Button" CommandName="codigo_alu" 
                                                        ImageUrl="~/images/anadir.gif" Text="Agregar">
                                                        <ControlStyle CssClass="agregar2" />
                                                        <ItemStyle Width="5%" />
                                                    </asp:ButtonField>
                                                </Columns>
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <b>No se encontraron estudiantes con el término de búsqueda</b>
                                                </EmptyDataTemplate>
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:15%"><b>Lineas de investigación</b></td>
                                        <td style="width:85%">
                                            <asp:DropDownList ID="cboLineas" 
                                                runat="server" DataTextField="nombre_are" DataValueField="codigo_are" 
                                                Width="50%" Enabled="False">
                                            </asp:DropDownList>
                                            &nbsp;<asp:Button ID="cmdAgregarLineas" runat="server" CssClass="agregar2" Text="   Agregar" ValidationGroup="Cancelar" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:15%">&nbsp;</td>
                                        <td style="width:85%">
                                        <asp:GridView ID="grdLineas" runat="server" AutoGenerateColumns="False" 
                                            BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                            DataKeyNames="codigo_are,guardado" ForeColor="#333333" ShowHeader="False" 
                                                Width="50%">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <Columns>
                                                <asp:BoundField DataField="nombre_are" HeaderText="Descripción">
                                                    <ItemStyle Width="95%" />
                                                </asp:BoundField>
                                                <asp:CommandField ShowDeleteButton="True">
                                                    <ControlStyle Font-Underline="True" />
                                                    <ItemStyle Font-Overline="False" Font-Size="X-Small" Font-Underline="True" 
                                                        ForeColor="#0066FF" Width="5%" />
                                                </asp:CommandField>
                                            </Columns>
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                     </ContentTemplate>
                    </asp:UpdatePanel>
            </td>
        </tr>
        
        <tr>
            <td  colspan="2">
                <asp:UpdatePanel ID="uPprofesores" runat="server">
                    <ContentTemplate>
                <table style="width:100%">
                    <tr>
                        <td style="width:15%"><b>Asesores</b></td>
                            <td style="width:85%">
                            <asp:TextBox ID="txtProfesor" runat="server" CssClass="cajas2" MaxLength="50" 
                                Width="50%"></asp:TextBox>
                            <asp:ImageButton ID="imgBuscarAsesor" runat="server" 
                                ImageUrl="../../../images/menus/buscar_small12.gif" 
                                ValidationGroup="Cancelar" />
                            &nbsp;<asp:Button ID="cmdAgregarAsesor" runat="server" CssClass="agregar2" 
                                Text="   Agregar" ValidationGroup="Cancelar" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:15%">&nbsp;</td>
                        <td style="width:85%" valign="top">
                            <asp:ListBox ID="cboProfesores" runat="server" DataTextField="docente" 
                                DataValueField="codigo_per" Height="150px" Visible="False" Width="50%">
                            </asp:ListBox>
                            <asp:GridView ID="grdAsesores" runat="server" AutoGenerateColumns="False" 
                                CellPadding="4" ForeColor="#333333" DataKeyNames="codigo_RTes" 
                                BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" Width="50%" 
                                            ShowHeader="False">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Asesor">
                                        <ItemTemplate>
                                            <asp:Label ID="lblasesor" runat="server" Font-Bold="True" 
                                                Text='<%# eval("docente") %>'></asp:Label>
                                            <br />
                                            <asp:Label ID="lblcategoria" runat="server" 
                                                Text='<%# eval("descripcion_tpe") %>'></asp:Label>
                                            <br />
                                            <asp:Label ID="lblDedicacion" runat="server" 
                                                Text='<%# eval("descripcion_ded") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="descripcion_Tpi" HeaderText="Función" >
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:CommandField 
                                        DeleteText="Quitar" ShowDeleteButton="True">
                                        <ControlStyle Font-Size="X-Small" Font-Underline="True" ForeColor="#0066FF" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                </Columns>
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        </table>          
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ShowMessageBox="True" ShowSummary="False" />
        <p style="text-align:right">
        <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" />
&nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" 
            ValidationGroup="Cancelar" />
            </p>
    </form>
    </body>
</html>
