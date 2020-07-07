<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSolicitudesVacantes.aspx.vb" Inherits="personal_frmSolicitudesVacantes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Solicitudes Vacantes</title>
    
    <link href="../../private/estilo.css?x=47" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script> 
    <script type="text/javascript" language="JavaScript" src="../../private/PopCalendar.js"></script>
   
   <!-- establecer una mascara a los controles fecha --> 
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            jQuery(function($) {
                $("#txtFechaInicio").mask("99/99/9999");
            });
        })

        $(document).ready(function() {
            jQuery(function($) {
                $("#txtFechaFin").mask("99/99/9999");
            });
        })
    
   </script>
         
   <!-- JavaScript para el ingreso de numero enteros y decimales -->
    <script type="text/javascript">
        function ValidNum(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58) || tecla == 46);
        }

        function ValidaSoloNumeros() {
            if ((event.keyCode < 48) || (event.keyCode > 57))
                event.returnValue = false;
         }
    </script>
    
    
    
</head>
<body>
    <form id="form1" runat="server">
    <!--Esta linea es importante para que pueda funcionar el calendario. -->
        <% Response.Write(ClsFunciones.CargaCalendario())%>
    <!-- ** -->
    <div>
        <span class="usatTitulo">
            <asp:Label ID="lblTitulo" runat="server" Text="Registrar Solictud Vacante "></asp:Label>
            <asp:Label ID="lblCodigo_svac" runat="server" Text=""></asp:Label>
        </span>
            <asp:Panel ID="pnlRegistro" Visible="true" runat="server">
                <!-- Tabla principal del registro de solicitudes de vacantes -->
                <table style="width: 100%" class="contornotabla">
                    <tr>
                        <td bgcolor="#D1DDEF" colspan="2" height="30px">
                        <b>
                            <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="Tipo Vacante"></asp:Label>
                        </td>
                        <td>
                    <asp:DropDownList 
                        ID="ddlTipoVacante" 
                         Width="550px"
                        runat="server" AutoPostBack="True" BackColor="#CCFFFF">
                        <asp:ListItem Value="N">NUEVA VACANTE</asp:ListItem>
                        <asp:ListItem Value="E">VACANTE EXISTENTE</asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinkButton 
                        ID="lnkBuscarVacante" 
                        runat="server" 
                        Font-Bold="True" 
                        Font-Underline="True" 
                        ValidationGroup="BuscaVacante" 
                        ForeColor="Blue" 
                        Visible="False">Buscar Vacante</asp:LinkButton>
                </td>
                    </tr>
                        <!--Este panel lo utilizamos para mostrar el nombre del docente al que se le va a generar la vacante. -->
                            <asp:Panel ID="pnlDatoVacante" Visible="false" runat="server">
                            <tr>
                                <td>
                                    <asp:Label ID="Label17" runat="server" Text="Vacante para el Docente:"></asp:Label>
                                </td>
                                <td>
                                    <table style="width: 550px" class="contornotabla">
                                        <tr>
                                            <td style="background:yellow">
                                                <asp:Label ID="lblDocente" runat="server" Text=""></asp:Label>        
                                            </td>
                                        </tr>
                                    </table>
                                </td>        
                            </tr>        
                        </asp:Panel>
                        <!-- Fin Datos Vacante -->
                    <tr>
                <td style="width:15%">
                    <asp:Label ID="Label1" runat="server" Text="Ciclo Académico"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList   
                        ID="ddlCicloAcademico" 
                        SkinID="ComboObligatorio" 
                        Width="550px"
                        runat="server" AutoPostBack="True" TabIndex="1">
                    </asp:DropDownList>
                            
                </td>
            </tr>
                    <tr>
                        <td style="width:15%">
                            <asp:Label ID="Label2" runat="server" Text="Departamento"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList 
                                ID="ddlDepartamento" 
                                SkinID="ComboObligatorio" 
                                Width="550px"
                                runat="server" TabIndex="2" AutoPostBack="True">
                            </asp:DropDownList>
                                    <asp:CompareValidator 
                                        ID="CompareValidator1" 
                                        runat="server" 
                                        ControlToValidate="ddlDepartamento" 
                                        ErrorMessage="Seleccione el departamento académico" 
                                        Operator="NotEqual" 
                                        ValueToCompare="-1">*
                                    </asp:CompareValidator>
                        </td>
            </tr>    
                    <tr>
                         <td style="width:15%">
                             <asp:Label ID="Label3" runat="server" Text="Centro Costo"></asp:Label>  
                        </td>       
                <td>
                     <asp:DropDownList 
                        ID="ddlCeco" 
                        SkinID="ComboObligatorio" 
                        Width="550px"
                        runat="server" TabIndex="3">
                     </asp:DropDownList>
                        <asp:CompareValidator 
                                ID="CompareValidator2" 
                                runat="server" 
                                ControlToValidate="ddlCeco" 
                                ErrorMessage="Seleccione el Centro Costo" 
                                Operator="NotEqual" 
                                ValueToCompare="-1">*
                            </asp:CompareValidator>       
                            
                </td>
            </tr>
                    <tr>
                         <td style="width:15%">
                             <asp:Label ID="Label4" runat="server" Text="Dedicación"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList 
                                            ID="ddlDedicacion" 
                                            SkinID="ComboObligatorio" 
                                            Width="550px"
                                            runat="server" AutoPostBack="True" TabIndex="4">
                                         </asp:DropDownList>
                            <asp:CompareValidator 
                                            ID="CompareValidator3" 
                                            runat="server" 
                                            ControlToValidate="ddlDedicacion" 
                                            ErrorMessage="Seleccione el tipo de dedicación." 
                                            Operator="NotEqual" 
                                            ValueToCompare="-1">*
                                        </asp:CompareValidator> 
                                        
                        </td>
            </tr>
                    <tr>
                         <td style="width:15%">
                        </td>
                        <td>
                            <table class="contornotabla">
                                <!--Este panel se va a mostrar cuando la dedicacion sea <20 horas -->
                                    <asp:Panel ID="pnlmenor20" Visible="false" runat="server">
                                        <tr>
                                            <td>
                                            <asp:Label ID="Label5" runat="server" Text="Remuneración Propuesta"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRemuneración" Width="90px" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                <!-- ******* -->
                                <asp:Panel ID="pnlOtros" Visible="false" runat="server">
                                    <tr>
                                        <td>
                                        <asp:Label ID="Label6" runat="server" Text="Horas"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txthoras"  Width="90px"  runat="server"></asp:TextBox>
                                        </td>
                                         <td>
                                        <asp:Label ID="Label7" runat="server" Text="Precio Hora Propuesta"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtpreciohora" Width="80px"  runat="server"></asp:TextBox>
                                        </td>
                                    </tr> 
                                </asp:Panel> 
                            </table>
                        </td>
            </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Fecha Inicio: "></asp:Label>
                    </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox 
                                    ID="txtFechaInicio" 
                                    runat="server" 
                                    Width="80px" 
                                    ValidationGroup="Subasta" 
                                    TabIndex="5"></asp:TextBox>
                                <input id="btnFechaInicio" runat="server" onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaInicio,'dd/mm/yyyy')" class="cunia" type="button" />
                                 <asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidator2" 
                                        runat="server" 
                                        ControlToValidate="txtFechaInicio" 
                                        ErrorMessage="Debe de ingresar la Fecha de Inicio" >*
                                 </asp:RequiredFieldValidator>
                                    
                            </td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Fecha Fin"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox 
                                    ID="txtFechaFin" 
                                    runat="server" 
                                    Width="80px" 
                                    TabIndex="6"></asp:TextBox>
                                <input id="btnFechaFin" runat="server" onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaFin,'dd/mm/yyyy')" class="cunia" type="button" />
                                    <asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidator1" 
                                        runat="server" 
                                        ControlToValidate="txtFechaFin" 
                                        ErrorMessage="Debe de ingresar la Fecha de Fin" >*
                                    </asp:RequiredFieldValidator>
                                        
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Justificación"></asp:Label>
                </td>
                <td>
                    <asp:TextBox 
                        ID="txtObservacion" 
                        Width="544px" 
                        TextMode="MultiLine" 
                        runat="server" 
                        Height="80px" 
                        MaxLength="120" TabIndex="7"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Formación Académica <br> Grados/Títulos"></asp:Label>
                </td>
                <td>
                    <asp:TextBox 
                        ID="txtformacion" 
                        Width="544px" 
                        TextMode="MultiLine" 
                        runat="server" 
                        Height="80px" 
                        MaxLength="120" TabIndex="7"></asp:TextBox>
                </td>
            </tr>  
                
                <tr>
                <td></td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Button 
                                    ID="btnSoliciar" 
                                    runat="server" 
                                    Height="35px" Width="100px" 
                                    CssClass="solicitarvac" 
                                    ToolTip="Registra una vacante." 
                                    Text="     Solicitar" />
                            </td>
                            <td>
                                <asp:Button 
                                    ID="btnLimpiar" 
                                    Height="35px" Width="100px" 
                                    CssClass="limpiarvac"  
                                    ToolTip="Restable y actualiza los controles."
                                    runat="server" 
                                    ValidationGroup="Limpiar"
                                    Text="       Cancelar" />
                            </td>
                        </tr>
                    </table>
                    
                </td>
            </tr> 
                    <tr>
                <td colspan="2">
                    <asp:ValidationSummary 
                    ID="ValidationSummary1"  
                    runat="server" 
                    ShowMessageBox="True" 
                    ShowSummary="False" />
                </td>
            </tr>  
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>  
                    <tr>
                <td colspan="2">
                    
                </td>
            </tr>
                    <tr>
                 <td colspan="2" style="width:100%">
                     <asp:GridView ID="gvLista" Width="100%" runat="server" BackColor="White" 
                         BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                         AutoGenerateColumns="False" 
                         DataKeyNames="Codigo,codigo_Ded,codigo_Cac,codigo_Dac,codigo_Cco,codigo_Per" 
                         EmptyDataText="No se encontraron registros para el departamento académico....">
                         <RowStyle ForeColor="#000066" />
                         <EmptyDataRowStyle BackColor="#FFFFCC" BorderColor="#3399FF" Font-Bold="True" 
                             ForeColor="#3333CC" />
                         <Columns>
                             <asp:BoundField DataField="Codigo" HeaderText="ID" />
                             <asp:BoundField DataField="codigo_Cac" HeaderText="Codigo_cac" 
                                 Visible="False" />
                             <asp:BoundField DataField="descripcion_Cac" HeaderText="Ciclo" />
                             <asp:BoundField DataField="codigo_Dac" HeaderText="codigo_Dac" 
                                 Visible="False" />
                             <asp:BoundField DataField="nombre_Dac" HeaderText="Departamento Académico" />
                             <asp:BoundField DataField="codigo_Cco" HeaderText="codigo_Cco" 
                                 Visible="False" />
                             <asp:BoundField DataField="descripcion_Cco" HeaderText="Centro Costo" />
                             <asp:BoundField DataField="codigo_Ded" HeaderText="codigo_Ded" 
                                 Visible="False" />
                             <asp:BoundField DataField="Descripcion_Ded" HeaderText="Dedicación" />
                             <asp:BoundField DataField="Salario" HeaderText="Salario/Precio Hora" 
                                 DataFormatString=" {0:C}" >
                                 <ItemStyle HorizontalAlign="Center" />
                             </asp:BoundField>
                             <asp:BoundField DataField="Numhoras_svac" HeaderText="N° Horas" >
                                 <ItemStyle HorizontalAlign="Center" />
                             </asp:BoundField>
                             <asp:BoundField DataField="codigo_Per" HeaderText="codigo_Per" 
                                 Visible="False" />
                             <asp:BoundField DataField="Docente" 
                                 HeaderText=" Nombres - Apellidos / Vacantes" />
                             <asp:BoundField DataField="EstadoRev_svac" HeaderText="Estado" />
                             
                             <asp:BoundField DataField="Tipo_svac" HeaderText="T.V" />
                             <asp:BoundField DataField="FechaIni_svac" HeaderText="F.Inicio" />
                             <asp:BoundField DataField="FechaFin_svac" HeaderText="F.Fin" />
                             <asp:BoundField DataField="Observacion" HeaderText="Justificación" />
                             
                             <asp:BoundField DataField="login_Per" Visible="false" HeaderText="UsuarioReg" />
                             <asp:BoundField DataField="FechaReg" Visible="false" HeaderText="FechaReg" />
                             <asp:CommandField ButtonType="Image" HeaderText="Editar" SelectImageUrl="~/Images/editar.gif" 
                                 ShowSelectButton="True" />
                             <asp:CommandField ButtonType="Image" HeaderText="Eliminar" DeleteImageUrl="~/Images/deleteSol.gif" 
                                 ShowDeleteButton="True" />
                         </Columns>
                         <FooterStyle BackColor="White" ForeColor="#000066" />
                         <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                         <SelectedRowStyle BackColor="#FFFFB1" Font-Bold="True" ForeColor="Blue" />
                         <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                     </asp:GridView>   
                </td>
            </tr>
        </table>
            <!-- Fin tabla principal de registro solicitud de vacantes -->   
            </asp:Panel>
            
            <asp:Panel ID="pnlBusqueda" Visible="false" runat="server">
                <!-- Tabla principal del registro de solicitudes de vacantes -->
                <table style="width: 100%" class="contornotabla">
                    <tr>
                        <td bgcolor="#D1DDEF" colspan="2" height="30px">
                        <b>
                            <asp:Label ID="Label13" runat="server" Text="Búsqueda de Vacantes"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        
                            <asp:HiddenField ID="HiddenField" runat="server" Value="0" />
                        
                        </td>
                        <td align="right">
                            <table style="width: 100%" class="contornotabla">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label15" runat="server" Text="Dedicación"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList 
                                            ID="ddlBusDedicacion" 
                                            runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" Text="T.Persona"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList 
                                            ID="ddlBusTipoPersona" 
                                            runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>            
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:Label ID="Label14" runat="server" Text="Búscar por Apellidos y Nombres"></asp:Label>
                        </td>
                        <td>
                            <table style="width: 100%" class="contornotabla">
                                <tr>
                                    <td style="width:80%">
                                            <asp:TextBox 
                                                ID="txtBuscar" Width="97%" 
                                                runat="server">
                                            </asp:TextBox>
                                    </td>
                                    <td style="width:10%" align="right">
                                        <asp:Button 
                                            ID="btnBuscar" 
                                            runat="server" 
                                            Height="23px" Width="100px" 
                                            CssClass="buscarvac"
                                            ToolTip="Permiste buscar una vacante registrada." 
                                            Text="   Consultar" />
                                    </td>
                                    <td style="width:10%" align="right">
                                        <asp:Button 
                                            ID="btnEnviar" 
                                            runat="server" 
                                            Height="23px" Width="100px" 
                                            CssClass="regresarvac"
                                            ToolTip="Permiste buscar una vacante registrada." 
                                            Text="  Regresar" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            
                            <asp:Label ID="lblnumreg" runat="server" Text=""></asp:Label>
                            
                        </td>
                    </tr>
                    
                    <tr>
                        <td bgcolor="#FFFDCD" colspan="2" height="20px">
                        <b>
                            <asp:Label ID="lblInstrucciones" runat="server" ForeColor="#FF6600"></asp:Label></b>
                        </td>
                    </tr>
                    
                    
                    <tr>
                 <td colspan="2" style="width:100%">
                     <asp:GridView ID="gvVacantes" Width="100%" runat="server" BackColor="White" 
                         BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                         AutoGenerateColumns="False" 
                         DataKeyNames="Codigo" 
                         EmptyDataText="No se encontraron registros...">
                         <RowStyle ForeColor="#000066" />
                         <EmptyDataRowStyle BackColor="#FFFFCC" BorderColor="#3399FF" Font-Bold="True" 
                             ForeColor="#3333CC" />
                         <Columns>
                             <asp:BoundField DataField="codigo" HeaderText="ID" />
                             <asp:BoundField DataField="docente" HeaderText="Apellidos Nombres - Vacante" />
                             <asp:BoundField DataField="codigo_Ded" HeaderText="codigo_Ded" 
                                 Visible="False" />
                             <asp:BoundField DataField="Descripcion_Ded" HeaderText="Dedicación" />
                             <asp:BoundField DataField="codigo_Tpe" HeaderText="codigo_Tpe" 
                                 Visible="False" />
                             <asp:BoundField DataField="descripcion_Tpe" HeaderText="Tipo Persona" />
                             <asp:BoundField DataField="estado_Per" HeaderText="Estado" />
                             <asp:BoundField DataField="codigo_Est" HeaderText="E.Campus" />
                             <asp:CommandField SelectText="" ShowSelectButton="True" />
                         </Columns>
                         <FooterStyle BackColor="White" ForeColor="#000066" />
                         <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                         <SelectedRowStyle BackColor="#FFFFB1" Font-Bold="True" ForeColor="Blue" />
                         <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                     </asp:GridView>   
                </td>
            </tr>
        </table>
            <!-- Fin tabla principal de registro solicitud de vacantes -->   
            </asp:Panel>
            
    </div>
    </form>
</body>
</html>
