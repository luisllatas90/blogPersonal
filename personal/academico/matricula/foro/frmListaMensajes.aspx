<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaMensajes.aspx.vb" Inherits="academico_matricula_foro_frmListaMensajes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="js/jquery-ui.css" />
    <script src="js/jquery-1.9.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/jquery-ui.js"></script>
    <script src="js/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function() {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#txtHasta").datepicker({
                firstDay: 1
            });
        });
        function muestraform(inc) {
            var link = "frmResponder.aspx?instancia=E&incidente=" + inc;
            window.open(link, "Responder Solicitud", "location=no,width=500,height=300,scrollbars=yes,top=100,left=400,resizable=no,toolbar=no,statusbar=no,menubar=no");
        }      
    </script>    
      <style type="text/css">
     body
        { 
            font-family:Verdana, Geneva, Arial, Helvetica, sans-serif;
          font-size:11px;
          cursor:hand;
          
        }
        
          .style1
          {
              height: 2px;
          }
        
          .style2
          {
              height: 22px;
          }
        .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3 ; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  
            padding:5px; 
            
           
            
       }
       
       .btnRevisar
       {
           background:url(img/revisar.png) no-repeat;
           background-position:left;
       }
        .btnResolver
       {
           background:url(img/resolver.png) no-repeat;
           background-position:left;
       }
       .btnCerrar
       {
           background:url(img/cerrar.png) no-repeat;
           background-position:3px center;
        }
.btnDerivar
       {
           background:url('img/next.png') no-repeat 3px center;
               height: 29px;
          }

     
          .style3
          {
              width: 10px;
          }

     
    </style>
    
    </head>
<body>
    <form id="form1" runat="server">
    <div> 
        <asp:Panel ID="PanelBusqueda" runat="server">
        <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%" >
        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td>REVISAR SOLICITUDES DESDE DIRECCIÓN DE ESCUELA</td>
        </tr>
        
        <tr><td>
       
               
        <table class="style2">
           
            <tr>
                <td>
                    Carrera Profesional
                </td>
                <td>
        <asp:DropDownList ID="cboEscuelas" runat="server">
        </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
                <td>
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" />        
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:CheckBox ID="chkFecha" runat="server" Text="Fecha de Registro" 
                        Checked="False" /> 
                </td>
                <td class="style1">
                    <input type="text" runat="server" id="txtHasta"/></td>
                <td class="style1">
                    <asp:CheckBox ID="chkCodUniv" runat="server" Text="Solo por Código Univ." 
                        AutoPostBack="True" /> 
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtcoduniv" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Estado</td>
                <td>
        <asp:DropDownList ID="cboEstado" runat="server">
            <asp:ListItem Value="%">Todos</asp:ListItem>
            <asp:ListItem Selected="True" Value="P">Pendiente</asp:ListItem>
            <asp:ListItem Value="R">Resuelto</asp:ListItem>
        </asp:DropDownList>
                </td>
                <td>
                    Instancia</td>
                <td>
        <asp:DropDownList ID="cboInstancia" runat="server">
            <asp:ListItem Value="%" Selected="True">Todos</asp:ListItem>
            <asp:ListItem Value="A">Por Revisar</asp:ListItem>
            <asp:ListItem Value="E">En Revisión</asp:ListItem>
            <asp:ListItem Value="D">En Dirección Académica</asp:ListItem>
        </asp:DropDownList>
                    </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        
         </td></tr></table>
        </asp:Panel>
        
        <br />
        <br />
         <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%" >
        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td>BANDEJA DE SOLICITUDES</td>
        </tr>
        
        
       <tr>
       <td>
        <asp:Panel ID="PanelLista" runat="server">
         <div>        
    <asp:GridView ID="gvListaIncidente" runat="server" AllowSorting="True"  
            DataKeyNames="codigo_incidencia,codigo_Cpf,nombre_cpf,mensaje,estado,instancia,adjunto,incidencia_raiz,email,telefonos,codigo_alu,descripcion_Pes"       
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                >
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="codigo_incidencia" HeaderText="ID" Visible="False" />
                <asp:BoundField DataField="codigo_Cpf" HeaderText="codigo_Cpf" 
                    Visible="False" />
                <asp:BoundField DataField="nombre_Cpf" HeaderText="nombre_Cpf" 
                    Visible="False" />
                <asp:BoundField DataField="numero" HeaderText="No." />
                <asp:BoundField DataField="fecha" HeaderText="FECHA" />
                <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIV." />
                <asp:BoundField DataField="Estudiante" HeaderText="ESTUDIANTE" />
                <asp:BoundField DataField="asunto" HeaderText="ASUNTO" />
                <asp:BoundField DataField="estado" HeaderText="ESTADO" />
                <asp:BoundField DataField="instancia" HeaderText="INSTANCIA" />
                 <asp:BoundField DataField="cuenta" HeaderText="Cant." />
                <asp:CommandField HeaderText="VER" ShowSelectButton="True" ButtonType="Image" 
                    SelectImageUrl="~/academico/matricula/foro/img/ver.png" >
                    <ItemStyle HorizontalAlign="Center" /> 
                </asp:CommandField>
                
                <asp:BoundField DataField="email" HeaderText="email" Visible="False" />
                <asp:BoundField DataField="telefonos" HeaderText="telefonos" Visible="False" />
                <asp:BoundField DataField="codigo_alu" HeaderText="codigo_alu" Visible="False" />
               
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EmptyDataTemplate>
                No se encontró información relacionada con su búsqueda
            </EmptyDataTemplate>
        </asp:GridView>        
    </div>
        </asp:Panel>
        </td></tr></table>
        <br />
        
        
        <asp:Panel ID="PanelDetalle" runat="server" Visible="false">
        <div style="padding-left:30px;">
            
        
        <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%"   >
        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td colspan="2" >DETALLE</td>            
            
        </tr>
        <tr>
            <td  class="style3" style="padding:3px;"><asp:Image ID="FotoAlumno" runat="server"  Height="104px" Width="95px" /></td>
            <td>
                <table cellpadding="3" cellspacing="0">
                    <tr>
                        <td><b>Fecha Registro</b></td>
                        <td class="style6">
                            <asp:Label ID="lblFecha" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>            
                        <td><b>N° Solicitud</b></td>
                        <td class="style6">
                            <asp:Label ID="lblNro" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                    <td class="style5">
                        <b>Código Universitario</b></td>
                         <td class="style7">
                             <asp:Label ID="lblCodUniv" runat="server" Text="Label"></asp:Label>
                         </td>
                </tr>
                 <tr>
                    <td>
                        <b>Estudiante</b></td>
                    <td class="style6">
                        <asp:Label ID="lblEstudiante" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="LinkHistorial" runat="server" Target="_blank"><b>Ver Historial del Alumno</b></asp:HyperLink>
                    </td>
                </tr>
                 <tr>
                    <td class="style5">
                        <b>Carrera Profesional</b></td>
                     <td class="style7">
                         <asp:Label ID="lblEscuela" runat="server" Text="Label"></asp:Label>
                     </td>
                </tr>
                   <tr>
                    <td class="style5">
                        <b>Plan de estudios</b></td>
                     <td class="style7">
                         <asp:Label ID="lblPlanEstudio" runat="server" Text="Label"></asp:Label>
                     </td>
                </tr>
                    <tr>
                        <td class="style5">
                            <b>E-Mail</b></td>
                        <td class="style7">
                            <asp:Label ID="lblEmail" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            <b>Teléfonos</b></td>
                        <td class="style7">
                            <asp:Label ID="lblTelefono" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            
            
        </tr>
        
               
               
               
                <tr style="background-color: #E8EEF7; font-weight: bold;">
                    <td class="style4">
                        Asunto</td>
                    <td class="style2" >
                        <asp:Label ID="lblAsunto" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td class="style3">
                        Mensaje</td>
                    <td >
                        <asp:Label ID="txtMensaje" runat="server" Text="Label" Font-Names="Courier" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        Adjunto</td>
                    <td>
                        <asp:HyperLink ID="EnlaceAdjunto" runat="server" Target="_blank">Visualizar Adjunto</asp:HyperLink>
                    </td>
                    
                </tr>
                <tr>
                    <td class="style3">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
            </tr>
                <tr style="background-color: #E8EEF7; font-weight: bold;">
                    <td class="style3">
                        <B>Responder</B></td>
                    <td >
                        <asp:TextBox ID="txtResponder" runat="server" TextMode="MultiLine" Width="100%" 
                            BorderStyle="None" Height="60px" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        Adjunto</td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="100%" />
                    </td>
            </tr>
                <tr>
                    <td class="style3">
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="lblmensaje" runat="server" style="color: #CC3300"></asp:Label>
                    </td>
            </tr>
                <tr>
                    <td class="style3">
                        &nbsp;</td>
                    <td >
                        <asp:Button ID="btnResponder" runat="server" CssClass="btn btnResolver" 
                            Text=" Resolver" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnRevisarApropiar" runat="server" CssClass="btn btnRevisar" 
                            Text="   Revisar" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnDerivar" runat="server" CssClass="btn btnDerivar" 
                            Text="   Derivar" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnResponder0" runat="server" CssClass="btn btnCerrar" 
                            Text="   Cerrar"  />
                    </td>
                </tr>
            </table>
            
        
        </div>
        </asp:Panel>
    </div>

    <asp:HiddenField ID="HdCiclo" runat="server" /> 
    <asp:HiddenField ID="CodInc" runat="server" /> 
    <asp:HiddenField ID="CodIncRaiz" runat="server" /> 
    <asp:HiddenField ID="id_per" runat="server" /> 
    
    <br />
    </form>
</body>
</html>
