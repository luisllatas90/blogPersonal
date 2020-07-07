<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmregistrarevento.aspx.vb" Inherits="frmregistrarevento" Theme="Acero" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Programas de Educación Contínua: PEC</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script src="../../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../../private/jq/jquery.mascara.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            //Marcara de Fechas
            jQuery(function($) {
                $("#txtFechaInicio").mask("99/99/9999");
                $("#txtFechaFin").mask("99/99/9999");
            });
            //Cálculo de utilidad
            $("#txtUtilidad").val($("#txtingresostotalesproyectada_dev").val() - $("#txtegresostotalesproyectada_dev").val());
            
            //Calcular utilidad al terminar de registrar el EGRESO
            $("#txtegresostotalesproyectada_dev").blur(function() {
                if ($("#txtegresostotalesproyectada_dev").val() > $("#txtegresostotalesproyectada_dev").val()) {
                    alert("Los egresos no pueden ser mayor a los ingresos")
                }
                else {
                    $("#txtUtilidad").val($("#txtingresostotalesproyectada_dev").val() - $("#txtegresostotalesproyectada_dev").val());
                }
            });

            //Mostrar bloque si el check está marcado
            if ($("#chkgestionanotas_dev").is(':checked')==true){
                $('#trGeneraNotas').show();
            }
            else {
                $('#trGeneraNotas').hide();
            }
            //Mostrar bloque al hacer clic
            $("#chkgestionanotas_dev").click(function() {
                var checked_status = this.checked;
                if (checked_status == true) {
                    $("#trGeneraNotas").show();
                }
                else {
                    $("#trGeneraNotas").hide();
                }
            });

        })
    </script>
    </head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Ficha de Registro de Eventos<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager></p>
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
    <tr>
    <td bgcolor="#D1DDEF" colspan="2" height="30px">
                    <b>Datos informativos</b></td>
    </tr>
    <tr>
    <td width="15%" >
                    Centro de costo<asp:RegularExpressionValidator ID="RegularExpressionValidator13" 
                        runat="server" ControlToValidate="cboCecos" 
                        ErrorMessage="Seleccione el Centro de Costos" ValidationExpression="^\d+$">*</asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                        runat="server" ControlToValidate="cboCecos" 
                        ErrorMessage="Seleccione centro de costos" ValidationGroup="Guardar">*</asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator5" runat="server" 
                        ControlToValidate="cboCecos" ErrorMessage="Seleccione centro de costos" 
                        Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator><asp:CompareValidator ID="CompareValidator8" runat="server" 
                                            ControlToValidate="cboCecos" ErrorMessage="Seleccione centro de costos" 
                                            Operator="GreaterThan" ValidationGroup="Presupuesto" 
                        ValueToCompare="0">*</asp:CompareValidator></td>
                <td width="85%">
                                        <asp:TextBox ID="txtCecos" runat="server" Width="90px" BackColor="#F3F3F3" 
                        Visible="False" Height="22px"></asp:TextBox>
                                        <asp:DropDownList ID="cboCecos" runat="server" SkinID="ComboObligatorio" 
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtBuscaCecos" runat="server" ValidationGroup="BuscarCecos" 
                                            style="width: 128px" Visible="False"></asp:TextBox>
                                        <asp:ImageButton ID="ImgBuscarCecos" runat="server" 
                                            ImageUrl="../../../images/buscar.gif" 
                        ValidationGroup="BuscarCecos" Visible="False" />
                                        &nbsp;<asp:LinkButton ID="lnkBusquedaAvanzada" runat="server" 
                        ForeColor="Blue" ValidationGroup="Buscar">Busqueda Avanzada</asp:LinkButton></td>
    </tr>
    <tr>
    <td width="15%" >
                                        <asp:UpdateProgress ID="upProcesando" runat="server">
                                            <ProgressTemplate>
                                                <font style="color:Blue">Procesando. Espere un momento...</font>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        </td>
                <td width="85%">
                                        <asp:Panel ID="Panel3" runat="server" Height="150px" 
                                            ScrollBars="Vertical" Width="100%" Visible="False">
                                            <asp:GridView ID="gvCecos" runat="server" 
                                                AutoGenerateColumns="False" BorderColor="#628BD7" BorderStyle="Solid" 
                                                BorderWidth="1px" CellPadding="4" DataKeyNames="codigo_cco" ForeColor="#333333" 
                                                ShowHeader="False" Width="98%">
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" 
                                                    ForeColor="White" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:BoundField DataField="codigo_cco" 
                                                        HeaderText="Código" />
                                                    <asp:BoundField DataField="nombre" 
                                                        HeaderText="Centro de costos" />
                                                    <asp:CommandField ShowSelectButton="True" />
                                                </Columns>
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" 
                                                    HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <b>No se encontraron items con el término de búsqueda</b>
                                                </EmptyDataTemplate>
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" 
                                                    ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" 
                                                    ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </asp:Panel>
                </td>
    </tr>
    <tr>
    <td width="15%" >
                    Nro. Resolución<asp:RequiredFieldValidator ID="RqResolucion" runat="server" 
                                                    ControlToValidate="txtresolucion_dev" 
                                                    
                                                    
                                                    ErrorMessage="Debe especificar el Número de Resolución de Aprobación de Secretaría General" 
                                                    >*</asp:RequiredFieldValidator>
                                    </td>
                <td width="85%">
                                                <asp:TextBox ID="txtresolucion_dev" runat="server" CssClass="cajas" 
                                                    SkinID="CajaTextoObligatorio" Height="22px" Width="128px"></asp:TextBox>
                                                &nbsp;</td>
    </tr>
    <td width="15%" >
                    &nbsp;</td>
                <td width="85%">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
    </tr>
    <tr>
    <td bgcolor="#D1DDEF" colspan="2" height="30" width="100%">
                    <b>Datos de la propuesta</td>
    </tr>
        <tr>
    <td width="15%" >
                    Fecha inicio<asp:RequiredFieldValidator ID="RqFechaInicio" runat="server" 
                                                    ControlToValidate="txtFechaInicio" 
                                                    
                                                    
                                                    ErrorMessage="Debe especificar la fecha de inicio" 
                                                    >*</asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" 
                        ControlToValidate="txtFechaInicio" 
                        ErrorMessage="La fecha inicio tiene un valor incorrecto" 
                        MaximumValue="31/12/2050" MinimumValue="01/01/1920" Type="Date">*</asp:RangeValidator>
                                    </td>
                <td width="85%">
                                                <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="cajas" 
                                                    MaxLength="12" Width="80px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                &nbsp;&nbsp;
                                        Fecha&nbsp; fin:                                                 
                                                <asp:TextBox ID="txtFechaFin" runat="server" CssClass="cajas" 
                                                    MaxLength="12" Width="80px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RqFechaFin" runat="server" 
                                                    ControlToValidate="txtFechaFin" 
                                                    
                                                    
                                                    ErrorMessage="Debe especificar la fecha fin" 
                                                    >*</asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                                    ControlToValidate="txtFechaFin" 
                                                    ErrorMessage="La fecha fin tiene un valor incorrecto" MaximumValue="31/12/2050" 
                                                    MinimumValue="01/01/1920" Type="Date">*</asp:RangeValidator>
                </td>
        </tr>
        <tr>
    <td width="15%" >
                    Coordinador General<asp:RegularExpressionValidator ID="RegularExpressionValidator14" 
                        runat="server" ControlToValidate="cbocoordinadorgral" 
                        ErrorMessage="Seleccione al Coordinador General" ValidationExpression="^\d+$">*</asp:RegularExpressionValidator>
                                    </td>
                <td width="85%">
                                                <asp:DropDownList ID="cbocoordinadorgral" runat="server" 
                                                    SkinID="ComboObligatorio">
                                                </asp:DropDownList>
                &nbsp; Remuneración total: S/.
                                        <asp:TextBox ID="txtremuneraciontotalcoordinadorgral" runat="server" 
                                            MaxLength="7" Width="60px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" 
                                                    runat="server" ControlToValidate="txtremuneraciontotalcoordinadorgral" 
                                                    ErrorMessage="El valor de la remuneración total del coordinador general, es incorrecto." 
                                                    ValidationExpression="^\d+(\.\d\d)?$">*</asp:RegularExpressionValidator>
                </td>
        </tr>
        <tr>
    <td width="15%" >
                    Coordinador de Apoyo<asp:RegularExpressionValidator ID="RegularExpressionValidator15" 
                        runat="server" ControlToValidate="cbocoordinadorapoyo" 
                        ErrorMessage="Seleccione el Coordinador de Apoyo" 
                        ValidationExpression="^\d+$">*</asp:RegularExpressionValidator>
                                    </td>
                <td width="85%">
                                                <asp:DropDownList ID="cbocoordinadorapoyo" runat="server" 
                                                    SkinID="ComboObligatorio">
                                                </asp:DropDownList>
                &nbsp; Remuneración total: S/.
                                        <asp:TextBox ID="txtremuneraciontotalcoordinadorapoyo" runat="server" 
                                            MaxLength="7" Width="60px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" 
                                                    runat="server" ControlToValidate="txtremuneraciontotalcoordinadorapoyo" 
                                                    ErrorMessage="El valor de la remuneración total del coordinador de apoyo, es incorrecto." 
                                                    ValidationExpression="^\d+(\.\d\d)?$">*</asp:RegularExpressionValidator>
                </td>
        </tr>
        <tr>
    <td width="15%" >
                    Nro. participantes<asp:RequiredFieldValidator ID="RqParticipantes" runat="server" 
                                                    ControlToValidate="txtnroparticipantes" 
                                                    ErrorMessage="Debe especificar el número de participantes" 
                                                    >*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" 
                        runat="server" ControlToValidate="txtnroparticipantes" 
                        ErrorMessage="El valor del número de participantes es incorrecto." 
                        ValidationExpression="^\d+$">*</asp:RegularExpressionValidator>
                                    </td>
                <td width="85%">
                                        <asp:TextBox ID="txtnroparticipantes" runat="server" MaxLength="4" Width="40px" 
                                            SkinID="CajaTextoObligatorio"></asp:TextBox>
                </td>
        </tr>
        <tr>
    <td width="15%" >
                    Precios por participante</td>
                <td width="85%">
                                        Al contado: S/.
                                        <asp:TextBox ID="txtpreciounitcontado" runat="server" MaxLength="5" 
                                            Width="60px" Height="22px" SkinID="CajaTextoObligatorio"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="RqPrecioContado" runat="server" 
                                                    ControlToValidate="txtpreciounitcontado" 
                                                    
                                                    
                                                    ErrorMessage="Debe especificar el precio al contado." 
                                                    >*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                                            runat="server" ControlToValidate="txtpreciounitcontado" 
                                            ErrorMessage="El precio al contado no es válido. Especifique los valores correctos" 
                                            ValidationExpression="^\d+(\.\d\d)?$">*</asp:RegularExpressionValidator>
                                        &nbsp; Financiado: S/.
                                        <asp:TextBox ID="txtpreciounitfinanciado" runat="server" MaxLength="5" 
                                            Width="60px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RqPrecioFinanciado" runat="server" 
                                                    ControlToValidate="txtpreciounitfinanciado" 
                                                    
                                                    
                                                    ErrorMessage="Debe especificar el precio financiado" 
                                                    >*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
                                            runat="server" ControlToValidate="txtpreciounitfinanciado" 
                                            ErrorMessage="El precio financiado no es válido. Especifique los valores correctos" 
                                            ValidationExpression="^\d+(\.\d\d)?$">*</asp:RegularExpressionValidator>
&nbsp;Cuota inicial: S/.
                                        <asp:TextBox ID="txtmontocuotainicial" runat="server" MaxLength="5" 
                                            Width="60px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RqCuotainicial" runat="server" 
                                                    ControlToValidate="txtmontocuotainicial" 
                                                    
                                                    
                                                    ErrorMessage="Debe especificar el monto de cuota inicial" 
                                                    >*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                                            ControlToValidate="txtmontocuotainicial" 
                                            ErrorMessage="La cuota inicial no es válida. Especifique los valores correctos" 
                                            ValidationExpression="^\d+(\.\d\d)?$">*</asp:RegularExpressionValidator>
&nbsp;Cuotas:
                                        <asp:DropDownList ID="cbonrocuotas" runat="server" SkinID="ComboObligatorio">
                                        </asp:DropDownList>
                </td>
        </tr>
        <tr>
    <td width="15%" >
                    Porcentaje Descuentos</td>
                <td width="85%">
                                        Personal USAT:
                                        <asp:TextBox ID="txtpreciopersonalusat" runat="server" MaxLength="5" 
                                            Width="40px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                                        %&nbsp;<asp:RequiredFieldValidator ID="RqCuotainicial0" runat="server" 
                                                    ControlToValidate="txtpreciopersonalusat" 
                                                    
                                                    
                                                    ErrorMessage="Debe especificar el porcentaje de descuento &quot;Personal USAT&quot;" 
                                                    >*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" 
                                            runat="server" ControlToValidate="txtpreciopersonalusat" 
                                            ErrorMessage="El porcentaje de descuentos para el &quot;Personal USAT&quot; es incorrecto." 
                                            ValidationExpression="^\d+(\.\d\d)?$">*</asp:RegularExpressionValidator>
&nbsp;&nbsp;&nbsp; Alumno USAT:
                                        <asp:TextBox ID="txtprecioalumno" runat="server" MaxLength="5" 
                                            Width="40px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                                        %<asp:RequiredFieldValidator ID="RqAlumnoUSAT" runat="server" 
                                                    ControlToValidate="txtprecioalumno" 
                                                    
                                                    
                                                    ErrorMessage="Debe especificar el porcentaje de descuento &quot;Alumno USAT&quot;" 
                                                    >*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" 
                                            runat="server" ControlToValidate="txtprecioalumno" 
                                            ErrorMessage="El porcentaje de descuento &quot;Alumno USAT&quot; es incorrecto." 
                                            ValidationExpression="^\d+(\.\d\d)?$">*</asp:RegularExpressionValidator>
                                        &nbsp;&nbsp;Egresado USAT:
                                        <asp:TextBox ID="txtprecioegresado" runat="server" MaxLength="5" 
                                            Width="40px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                                        %
                                                <asp:RequiredFieldValidator ID="RqAlumnoUSAT1" runat="server" 
                                                    ControlToValidate="txtprecioegresado" 
                                                    
                                                    
                                                    ErrorMessage="Debe especificar el porcentaje de descuento &quot;Egresado USAT&quot;" 
                                                    >*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" 
                                            ControlToValidate="txtprecioalumno" 
                                            ErrorMessage="El porcentaje de descuento &quot;Egresado USAT&quot; es incorrecto." 
                                            ValidationExpression="^\d+(\.\d\d)?$">*</asp:RegularExpressionValidator>
                &nbsp; Corporativo:
                                        <asp:TextBox ID="txtpreciocorportativo" runat="server" MaxLength="5" 
                                            Width="40px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                                        %
                                                <asp:RequiredFieldValidator ID="RqAlumnoUSAT0" runat="server" 
                                                    ControlToValidate="txtpreciocorportativo" 
                                                    
                                                    
                                                    ErrorMessage="Debe especificar el porcentaje de descuento &quot;Corporativo&quot;" 
                                                    >*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
                                            ControlToValidate="txtprecioalumno" 
                                            ErrorMessage="El porcentaje de descuentos &quot;Corporativo&quot; es incorrecto." 
                                            ValidationExpression="^\d+(\.\d\d)?$">*</asp:RegularExpressionValidator>
                </td>
        </tr>
        <tr>
    <td width="15%" >
                    Proyección Total</td>
                <td width="85%">
                                        Ingresos:
                                        S/.
                                        <asp:TextBox ID="txtingresostotalesproyectada_dev" runat="server" MaxLength="6" 
                                            Width="60px" SkinID="CajaTextoObligatorio"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RqIngresos" runat="server" 
                                                    ControlToValidate="txtingresostotalesproyectada_dev" 
                                                    
                                                    
                                                    ErrorMessage="Debe especificar el ingreso proyectado" 
                                                    >*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" 
                                            ControlToValidate="txtingresostotalesproyectada_dev" 
                                            ErrorMessage="El valor de los ingresos proyectos en incorrecto." 
                                            ValidationExpression="^\d+(\.\d\d)?$">*</asp:RegularExpressionValidator>
&nbsp;&nbsp;
                                        Egresos: S/.
                                        <asp:TextBox ID="txtegresostotalesproyectada_dev" runat="server" MaxLength="6" 
                                            Width="60px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RqEgresos" runat="server" 
                                                    ControlToValidate="txtegresostotalesproyectada_dev" 
                                                    ErrorMessage="Debe especificar los egresos proyectados" 
                                                    >*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" 
                                            ControlToValidate="txtegresostotalesproyectada_dev" 
                                            ErrorMessage="El valor de los egresos proyectos es incorrecto." 
                                            ValidationExpression="^\d+(\.\d\d)?$">*</asp:RegularExpressionValidator>
            &nbsp;&nbsp;&nbsp;&nbsp; Utilidad: S/.
                                        <asp:TextBox ID="txtUtilidad" runat="server" BorderColor="#333333" 
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" 
                                            ReadOnly="True" SkinID="CajaTextoSinMarco"></asp:TextBox>
            </td>
        </tr>
        <tr>
    <td width="15%" >
                    Horarios</td>
                <td width="85%">
                                                <asp:TextBox ID="txthorarios_dev" runat="server" TextMode="MultiLine" 
                                                    Width="95%" CssClass="cajas"></asp:TextBox>
                </td>
        </tr>
        <tr>
    <td width="15%">
                    Observaciones</td>
                <td width="85%">
                                                <asp:TextBox ID="txtobs" runat="server" TextMode="MultiLine" 
                                                    Width="95%" CssClass="cajas"></asp:TextBox>
            </td>
        </tr>
        <tr>
    <td width="15%">
                    &nbsp;</td>
                <td width="85%">
                                                <asp:CheckBox ID="chkValidarDeuda" runat="server" Checked="True" 
                                                    ForeColor="Blue" Text="Validar deuda" />
&nbsp;(Determina que la inscripción permita el registro de personas con deuda como participantes)</td>
        </tr>
        <tr style="background-color:Red; font-size: 12px; font-family:Verdana; color: #FFFFFF;">
    <td width="15%">
                    Gestiona notas</td>
                <td width="85%">
                                        <asp:CheckBox ID="chkgestionanotas_dev" runat="server" Text="Al marcar este check, el coordinador debe registrar el plan de estudios, programación, carga académica y emisión de notas." />
            </td>
        </tr>
        <tr id="trGeneraNotas">
    <td width="15%">
                    &nbsp;</td>
                <td width="85%">
                                        Tipo de Estudio:
                                        <asp:DropDownList ID="dpTipo" runat="server" AutoPostBack="True" 
                                            SkinID="ComboObligatorio">
                                        </asp:DropDownList>
&nbsp; SubTipo:
                                        <asp:DropDownList ID="dpSubTipo" runat="server" AutoPostBack="True" 
                                            SkinID="ComboObligatorio">
                                        </asp:DropDownList>
                                        &nbsp;Nombre:
                                        <asp:DropDownList ID="dpEscuela" runat="server" SkinID="ComboObligatorio">
                                        </asp:DropDownList>
            </td>
        </tr>
        <tr>
    <td width="15%">
                    &nbsp;</td>
                <td width="85%">&nbsp;</td>
        </tr>
        </table>
    <p align="center">
        <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
            SkinID="BotonGuardar" />
        &nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="Cerrar" 
            SkinID="BotonCancelar" ValidationGroup="Salir" />
    </p>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" />
</form>
</body>
</html>

