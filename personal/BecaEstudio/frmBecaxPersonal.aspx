<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBecaxPersonal.aspx.vb" Inherits="BecaEstudio_frmBecaxPersonal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../../private/estilo.css?x=x" rel="stylesheet" type="text/css" />
        <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
        <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script> 
        <script type="text/javascript" language="JavaScript" src="../../private/PopCalendar.js"></script>
        <style type="text/css">
                     .filarequisito { border-bottom:1px solid; border-left:1px solid;}
        </style>
</head>

<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlPrincipal" runat="server" Visible="true">
            <table style="width: 100%" class="contornotabla1"  >
            <tr>
                <td bgcolor="#EFF3FB" height="35px">
                    <b>
                    <asp:Label ID="Label11" runat="server" Text="Solicitar Becas Personal USAT"></asp:Label></b>
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
                        <table class="contornotabla1" style="width: 65%"  >
                            
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
                                        Text="    Validar Requisitos" Visible="false" Width="164px" />
                                </td>
                            </tr>
                            <tr>
                            <td colspan="2">
                                <asp:Label ID="Label1" runat="server" 
                                    
                                    Text="Seleccione el estudiante para validar el cumplimiento de los requisitos" 
                                    Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" 
                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                                        CellPadding="3" DataKeyNames="codigo_Alu" 
                                        EmptyDataText="Solo podrán solicitar el beneficio por los hijos que se encuentren matriculados en el presente semestre académico y su edad menor a 25 años. " Width="100%">
                                        <RowStyle ForeColor="#000066" />
                                        <EmptyDataRowStyle BackColor="#FFFFCC" BorderColor="#3399FF" Font-Bold="True" 
                                            ForeColor="#3333CC" />
                                        <Columns>
                                            <asp:BoundField DataField="codigo_Alu" HeaderText="ID" />
                                            <asp:BoundField DataField="codigoUniver_Alu" 
                                                HeaderText="Código Universitario" />
                                            <asp:BoundField DataField="alumno" HeaderText="Apellidos y Nombres" />
                                            <asp:BoundField DataField="nombre_cpf" HeaderText="Carrera Profesional" />
                                            <asp:BoundField DataField="Personal" HeaderText="Personal USAT" />
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
            <table>
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
                
                                <table style="width: 50%; height:100%" style="border:1px solid #808080" cellspacing="0" cellpadding="1"  >
                                    <tr style="font-weight:bold; border:1px solid #808080">                  
                                        <td>Requisito</td>
                                        <td>Requerido</td>
                                        <td>Cumplimiento</td>
                                        <td></td>                    
                                    </tr>                
                                    <div id="tb" runat="server"></div>
                                             
                               </table>   
                <br />
                 </asp:Panel> 
                <!-- :::::::::::::::::::::::::::::::::::::::::::::::: -->                                               
            </tr>
            <tr>
                <td align="left">
                    <asp:CheckBox ID="chkAcepto" runat="server" 
                        Text="He leído y acepto las condiciones que se consignan en el" 
                        AutoPostBack="True" />
                        <a href="../../librerianet/reglamentos/REGLAMENTO-BECAS-PREGRADO-V05.pdf" target="_blank"> <b><u>Nuevo Reglamento de Becas</u></b></a>
                    <br />
                    <br />
                    <asp:Button ID="btnAgregar" runat="server" CssClass="asignarbecas" 
                        Enabled="false" Height="40" Text="      Solicitar" />
                </td>
            </tr>
            <tr>
                <td>
                <br /><br />
                <h4>Historial de Solicitudes</h4>
                     <asp:GridView ID="gvListaBecas" 
                         Width="100%" runat="server" BackColor="White" 
                         BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                         AutoGenerateColumns="False" 
                         DataKeyNames="codigo_bso, estado_bso" 
                         EmptyDataText="">
                         <RowStyle ForeColor="#000066" />
                         <EmptyDataRowStyle BackColor="#FFFFCC" BorderColor="#3399FF" Font-Bold="True" 
                             ForeColor="#3333CC" />
                         <Columns>
                             <asp:BoundField DataField="codigo_bso" HeaderText="ID" />
                             <asp:BoundField DataField="descripcion_cac" HeaderText="Semestre Académico" />
                             <asp:BoundField DataField="descripcion_bec" HeaderText="Tipo Beca" />
                             <asp:BoundField DataField="alumno" HeaderText="Apellidos y Nombres" />
                             <asp:BoundField DataField="porcentaje_bco" HeaderText="Beneficio" />                             
                             <asp:BoundField DataField="estado_bso" HeaderText="Estado" />
                             <asp:CommandField EditText="Eliminar" HeaderText="Eliminar" 
                                ShowDeleteButton="True" ButtonType="Image" 
                                DeleteImageUrl="../../images/eliminar.gif" DeleteText="" >
                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                            </asp:CommandField>
                         </Columns>
                         <FooterStyle BackColor="White" ForeColor="#000066" />
                         <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                         <SelectedRowStyle BackColor="#FFFFB1" Font-Bold="True" ForeColor="Blue" />
                         <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                     <br />
                </td>
            </tr>
        </table>
        </asp:Panel>   
            
        <!--:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::: -->
            
        
        
    </div>
    <p>
                                <asp:Label ID="LblCronograma" runat="server" 
                                    
                                    Text="Seleccione el estudiante para validar el cumplimiento de los requisitos" 
                                    Font-Bold="True" ForeColor="Red" Visible="true" 
            style="color: #006699"></asp:Label>
                            </p>
    <p>
                                &nbsp;</p>
    </form>
</body>
</html>


