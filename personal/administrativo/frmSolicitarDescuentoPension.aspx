<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSolicitarDescuentoPension.aspx.vb" Inherits="SolicitarDescuentoPension" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../../private/estilo.css?z=x" rel="stylesheet" type="text/css" />
        <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
        <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script> 
        <script type="text/javascript" language="JavaScript" src="../../private/PopCalendar.js"></script>
        <style type="text/css">
                     .filarequisito { border-bottom:1px solid; border-left:1px solid;}
        </style>
</head>
 <script type="text/javascript">
     $(document).ready(function() {
        $('#_codigoCac').hide();
     });    
</script>
<body>
    <form id="form1" runat="server">
 
        <asp:Panel ID="pnlPrincipal" runat="server" Visible="true">
            <table style="width: 100%" class="contornotabla"  >
            <tr>
                <td bgcolor="#EFF3FB" height="35px">
                    <b>
                    <asp:Label ID="Label11" runat="server" Text="Solicitar Descuento Pensión - Pago Anticipado"></asp:Label></b>
                </td>
            </tr>
            <tr>
            <td>
                    <asp:Panel ID="panelTipoPersonal" runat="server" Font-Bold="True" 
                        Font-Italic="False" ForeColor="#3366CC" Visible="False">La solicitud de beca solo esta disponible para trabajadores a tiempo completo.</asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlBusqueda" runat="server" Visible="false">
                        <!-- Busqueda del Alumno -->
                        <table class="contornotabla" style="width: 65%"  >
                                    <asp:HiddenField ID="HiddenField" runat="server" Value="0" />                         
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Buscar por: "></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTipo" runat="server">
                                        <asp:ListItem Value="0">Apellidos y Nombres</asp:ListItem>
                                        <asp:ListItem Value="1">Código</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtBuscarAlumno" runat="server" ></asp:TextBox>
                                    <asp:Button ID="btnBuscar" runat="server" CssClass="buscarBeca" Height="30" 
                                        Text="    Buscar" Width="100" />
                                    <asp:Button ID="btnEnviar" runat="server" CssClass="selecionarBeca" Height="30px" 
                                        Text="    Calcular descuento" Visible="false" Width="180px" /><br />
                                        <asp:Label ID="lblMensajeCalculo" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                            <td colspan="2">
                                <asp:Label ID="Label1" runat="server"                                     
                                    Text="Seleccione el estudiante para procesar cálculo de descuento en pensión" 
                                     ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                <asp:Label ID="lblbuscar" runat="server" BackColor="#FFFFCC" BorderColor="#3399FF" Font-Bold="True" Visible=false >El estudiante buscado no tiene matricula o ya aceptó la solicitud"</asp:Label>
                                    <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" 
                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                                        CellPadding="3" DataKeyNames="codigo_Alu" 
                                        EmptyDataText="" Width="100%">
                                        <RowStyle ForeColor="#4A4848" />
                                        <EmptyDataRowStyle BackColor="#FFFFCC" BorderColor="#3399FF" Font-Bold="True" 
                                            ForeColor="#3333CC" />
                                        <Columns>
                                            <asp:BoundField DataField="codigo_Alu" HeaderText="ID" />
                                            <asp:BoundField DataField="codigoUniver_Alu" HeaderText="C&Oacute;DIGO UNIVERSITARIO" />
                                            <asp:BoundField DataField="alumno" HeaderText="ESTUDIANTE" />
                                            <asp:BoundField DataField="nombre_cpf" HeaderText="CARRERA PROFESIONAL" />
                                            <asp:BoundField DataField="pago" Visible ="false" HeaderText="PAG&Oacute;" />
                                            <asp:CommandField SelectText="" ShowSelectButton="True" />
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#FFFFB1" Font-Bold="True" ForeColor="Blue" />
                                        <HeaderStyle BackColor="#E33439" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <!-- Fin busqueda -->
                    </asp:Panel>
                </td>
            
                
                
               
            </tr>
            <tr>
                <td bgcolor="#EFF3FB">
                    <asp:LinkButton ID="lbkBuscar" runat="server" Font-Bold="true" ForeColor="Blue" 
                        Visible="False">Buscar Alumno...</asp:LinkButton>
                </td>
            </tr>
            </table>
            <table class="contornotabla">
            <tr>
                 <asp:Panel ID="pnlDatosAlumno" Visible="false" runat="server">
                     <!-- Datos del Alumno -->
                            
                    <table style="width: 50%" class="contornotabla"  >
                        <tr>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Alumno"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAlumno" runat="server" Font-Bold="False" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Código Universitario"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCodigoUniv" Font-Bold="False" ForeColor="Blue" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Sem. Acad. Ingreso"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblcicloingreso" Font-Bold="False" ForeColor="Blue" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Carrera Profesional"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEscuelaprofesional" Font-Bold="False" ForeColor="Blue" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Plan Estudios"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblPlanEstudios" Font-Bold="False" ForeColor="Blue" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                
                <!--Requisitos -->
                 <table style="width: 100%" class="contornotabla"  >
                            <tr>
                            <td>
                <asp:GridView ID="gvPensiones" runat="server" AutoGenerateColumns="False" 
                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                                        CellPadding="3" DataKeyNames="codigo_Sco" 
                                        EmptyDataText="" Width="100%">
                                        <RowStyle ForeColor="#000066" />
                                        <EmptyDataRowStyle BackColor="#FFFFCC" BorderColor="#3399FF" Font-Bold="True" 
                                            ForeColor="#3333CC" />
                                        <Columns>
                                             <asp:BoundField DataField="codigo_Sco" HeaderText="" ItemStyle-Width="0%" ItemStyle-ForeColor="White"/>
                                            <asp:BoundField DataField="fechaCiclo" HeaderText="Fecha Vcto" />
                                            <asp:BoundField DataField="descripcion_Sco" HeaderText="Servicio Concepto" />
                                            <asp:BoundField DataField="CentroCostos" HeaderText=""  ItemStyle-Width="0%" ItemStyle-ForeColor="White"/>
                                            <asp:BoundField DataField="Pensionanterior" HeaderText="Pensión Sin Descuento" />
                                            <asp:BoundField DataField="Pension" HeaderText="Pensión Con Descuento" /> 
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#FFFFB1" Font-Bold="True" ForeColor="Blue" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                            </td>
                            </tr>
                </table>
                <br />
                <table style="width: 100%;" style="border:1px solid #808080" cellspacing="0" cellpadding="1"  class="contornotabla" >
                                    <tr style="font-weight:bold; border:1px solid #808080">
                                        <td>Costo Total</td>
                                        <td><asp:TextBox ID="txttotalsindcto" runat="server" ReadOnly=true  BackColor="#F2DAA3"  ></asp:TextBox></td>
                                        <td>Costo Con Dcto</td>
                                        <td><asp:TextBox ID="txttotalcondcto" runat="server" ReadOnly=true Font-Bold=true  BackColor="#F2DAA3"></asp:TextBox></td>                    
                                        <td>Beneficio (5%)</td>
                                        <td><asp:TextBox ID="txttotal" runat="server" ReadOnly=true Font-Bold=true  BackColor="#AFF2A3" ></asp:TextBox></td>  
                                    </tr>                                     
                               </table> 
                                    
                            <%--    <table style="width: 50%; height:100%" style="border:1px solid #808080" cellspacing="0" cellpadding="1"  >
                                    <tr style="font-weight:bold; border:1px solid #808080">
                                        <td>Requisito</td>
                                        <td>Requerido</td>
                                        <td>Cumplimiento</td>
                                        <td></td>                    
                                    </tr>                
                                    <div id="tb" runat="server"></div>
                                             
                               </table>  --%> 
                <br />
                 </asp:Panel> 
                <!-- :::::::::::::::::::::::::::::::::::::::::::::::: -->                                               
            </tr>
            <tr>
                <td align="left">
                    <asp:Button ID="btnAgregar" runat="server" CssClass="asignarPension" 
                        Enabled="false" Height="40" Text="           Solicitar" />
                </td>
            </tr>
            
            <tr>
                <td>
                <br />
                <h4>Historial de Solicitudes de Descuento - Pago Anticipado</h4>
                <asp:Button ID="btnExportar" runat=server Text="     Exportar" CssClass="excel" />
                <br />    
                     <asp:GridView ID="gvListaBecas" 
                         Width="100%" runat="server" BackColor="White"  ForeColor="#BEC2BD"
                         BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                         AutoGenerateColumns="False" 
                         DataKeyNames="codigo_mat,codigo_Alu" 
                         EmptyDataText="">
                         <RowStyle ForeColor="#4A4848" />
                         <EmptyDataRowStyle BackColor="#FFFFCC" BorderColor="#3399FF" Font-Bold="True" 
                             ForeColor="#3333CC" />
                         <Columns>
                             <asp:BoundField DataField="codigo_Alu" HeaderText="ID" />
                             <asp:BoundField DataField="codigoUniver_Alu" HeaderText="C&Oacute;DIGO UNIVERSITARIO" />
                             <asp:BoundField DataField="estudiante" HeaderText="ESTUDIANTE" />
                             <asp:BoundField DataField="escuela" HeaderText="CARRERA PROFESIONAL" />  
                             <asp:BoundField DataField="pago" HeaderText="PAG&Oacute;" />
                             <asp:BoundField DataField="total" HeaderText="S/ Total" />
                            <asp:TemplateField HeaderText="ELIMINAR" ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                    CommandName="Delete" ImageUrl="../../Images/menus/noconforme_small.gif" alternateText="Eliminar" OnClientClick="return confirm('¿Desea anular convenio de descuento del 5% de pensión?.')" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                         </Columns>
                         <FooterStyle BackColor="White" ForeColor="#000066" />
                         <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                         <SelectedRowStyle BackColor="#FFFFB1" Font-Bold="True" ForeColor="Blue" />
                         <HeaderStyle BackColor="#E33439" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                     <br />
                </td>
            </tr>
        </table>
        </asp:Panel>   
            
        <!--:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::: -->
          <br />
             <center>
             
            
    <asp:Panel ID="pnlPregunta" runat="server" BorderColor="#5D7B9D" 
        BorderStyle="Solid" BorderWidth="1px" style="text-align: center; padding:5px;" 
        Visible="False" Width="25%" BackColor="#F7F6F4">
         <table style="width: 100%" class="contornotabla"  >
             <tr>
                <td bgcolor="#EFF3FB" height="35px">                   
                    <asp:Label ID="Label2" runat="server" Text="Solicitar decuento en las pensiones por pago anticipado"></asp:Label></b>
             </td>                
             </tr>
             </table>
        <b><span class="style1">
              ¿Desea generar el pago anticipado del semestre acad&eacute;mico 2018-I al estudiante <span class="style2" id="spalumno" runat="server"></span>?</span></b><br />
        Costo Sin Dscto <asp:Label ID="lblCostoSinDscto" runat="server" 
            style="color: #3366CC;  font-weight: 700;" Text="Label"></asp:Label>
        <br />
        Costo Con Dscto <asp:Label ID="lblCostoConDscto" runat="server" 
            style="color: #3366CC;  font-weight: 700;" Text="Label"></asp:Label>
        <br />
        Beneficio (5%) <asp:Label ID="lblBeneficio" runat="server" 
            style="color: #000000; font-weight: 700;" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnSi" runat="server" CssClass="btn" Text="Sí" Width="50px" />
        &nbsp;&nbsp;
        <asp:Button ID="btnNo" runat="server" CssClass="btn" Text="No" Width="50px" />
        <br />
      
    </asp:Panel>
    </center> 

    <p>
                                <asp:Label ID="LblCronograma" runat="server" 
                                    
                                    Text="Seleccione el estudiante para validar el cumplimiento de los requisitos" 
                                    Font-Bold="True" ForeColor="Red" Visible="false" 
            style="color: #006699" ></asp:Label>
                            </p>
    <p>
                                &nbsp;</p>
                                <asp:TextBox ID="_codigoCac"  runat="server" ReadOnly="true"></asp:TextBox>
    </form>
</body>
</html>


