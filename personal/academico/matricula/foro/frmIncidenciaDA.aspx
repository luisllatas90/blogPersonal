<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmIncidenciaDA.aspx.vb" Inherits="academico_matricula_foro_frmIncidenciaDA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Lista de Solicitudes Coord. Académica</title>  
    <link rel="stylesheet" href="js/jquery-ui.css" />
    <script src="js/jquery-1.9.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/jquery-ui.js"></script>        
    <script type="text/javascript" language="javascript">
        function MuestraError(swVisible) {
            if (swVisible) {
                
            } else { 
            
            }
        }
        function CerrarResponder() {
            $("#DivFrmRpta").hide();
            $("#DivIncIni").hide();            
            $("#DivListado").show();            
        }
        
        function muestraform() {
            $("#DivFrmRpta").show();
            $("#DivIncIni").show();
            $("#DivListado").hide();
        }       
    </script>
    <style type="text/css">
     body
        { font-family:Verdana, Geneva, Arial, Helvetica, sans-serif;
          font-size:11px;
          cursor:hand;
          
        }     
     .col1
        {
         width:15%;
         }
     .col2
        {
         width:65%;
         }    
      .col3
        {
         width:20%;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">    
    <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="#E33439"></asp:Label><br />  
    <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%" >
        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td colspan="7"><b>Filtrar por: &nbsp;</b></td>
        </tr>
        <tr>
            <td>
            Carrera Profesional:
            <asp:DropDownList ID="cboEscuelas" runat="server" Width="25%">
            </asp:DropDownList>&nbsp;&nbsp;
            Instancia: 
                <asp:DropDownList ID="cboInstancia" runat="server">
                    <asp:ListItem Value="T" Selected="True">Todos</asp:ListItem>
                    <asp:ListItem Value="A">Por Revisar</asp:ListItem>
                    <asp:ListItem Value="E">En Revisión</asp:ListItem>
                </asp:DropDownList>&nbsp;&nbsp;
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" />   
            </td>
        </tr>
    </table>
    <br />
    <div id="DivListado">                            
    <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%" >
        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td><b>Bandeja de Solicitudes&nbsp;               
                </b></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvListaIncidente" runat="server" AllowSorting="True"  
                    DataKeyNames="codigo_incidencia,codigo_Cpf,codigo_incidencia_ref" Width="100%"       
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="codigo_incidencia" HeaderText="ID" />
                        <asp:BoundField DataField="codigo_Cpf" HeaderText="codigo_Cpf"  
                            Visible="False" />
                        
                        <asp:BoundField DataField="numero" HeaderText="No." />
                        <asp:BoundField DataField="fecha" HeaderText="FECHA" />
                        <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIV." />
                        
                        <asp:BoundField DataField="Estudiante" HeaderText="ESTUDIANTE" />
                        <asp:BoundField DataField="nombre_Cpf" HeaderText="CARRERA PROFESIONAL" />
                        <asp:BoundField DataField="asunto" HeaderText="ASUNTO" />
                        <asp:CommandField HeaderText="VER" ShowSelectButton="True" >                
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:CommandField>
                        <asp:CommandField HeaderText="DERIVAR" ShowDeleteButton="True" 
                            DeleteText="Derivar" Visible="False" >
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:CommandField>
                        <asp:BoundField DataField="codigo_incidencia_ref" 
                            HeaderText="codigo_incidencia_ref" Visible="False" />
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <RowStyle Height="20px" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EmptyDataTemplate>
                        No se encontró información relacionada con su búsqueda
                    </EmptyDataTemplate>
                </asp:GridView>  
            </td>
        </tr>
     </table>                       
    </div> 
    <div id="DivIncIni" style="display:none">
        <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%" >
                <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                    <td colspan="7"><b>Solicitud - Alumno&nbsp;</b></td>            
                </tr>                   
                <tr>
                    <td class="col3" rowspan="10" align="center">
                        <asp:Image ID="FotoAlumno" runat="server"   Height="208px" Width="190px" />
                    </td>
                    <td class="col1">No. Solicitud:</td>
                    <td class="col2">
                        <asp:Label ID="lblIncidenciaPadre" runat="server" Text=""></asp:Label>
                    </td>
                </tr>                
                <tr>                    
                    <td class="col1">Carrera Profesional:</td>
                    <td class="col2">
                        <asp:Label ID="lblEscuela" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>                    
                    <td class="col1">Cod. Univ.:</td>
                    <td class="col2"><asp:Label ID="lblCodUniv" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>                    
                    <td class="col1">Estudiante:</td>
                    <td class="col2"><asp:Label ID="lblAlumno" runat="server" Text=""></asp:Label></td>
                </tr> 
                <tr>                    
                    <td class="col1">Plan de estudios:</td>
                    <td class="col2"><asp:Label ID="lblPlanEstudio" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>                    
                    <td class="col1">Email:</td>
                    <td class="col2"><asp:Label ID="lblCorreo" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>                    
                    <td class="col1">Telefono:</td>
                    <td class="col2"><asp:Label ID="lblTelefono" runat="server" Text=""></asp:Label></td>
                </tr>   
                 <tr>                    
                    <td class="col1">Asunto:</td>
                    <td class="col2"><asp:Label ID="lblAsunto" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>                   
                    <td class="col1">Mensaje:</td>
                    <td class="col2"><asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>                 
                    <td class="col1">Adjunto:</td>
                    <td class="col2"><asp:HyperLink ID="lblAdjunto" runat="server" Target="_blank">Visualizar Adjunto</asp:HyperLink></td>
                </tr>      
        </table>
    </div>
    <br />
    <div id="DivFrmRpta" style="display:none">
        <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%" >
                <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                    <td colspan="7"><b>Respuesta de Escuela Profesional&nbsp;</b></td>            
                </tr>
                <tr>                    
                    <td class="col1">No. Solicitud:</td>
                    <td class="col2">
                        <asp:Label ID="lblIncidencia" runat="server" Text=""></asp:Label>
                    </td>
                </tr> 
                <tr>                    
                    <td class="col1">Atendido por:</td>
                    <td class="col2"><asp:Label ID="lblAtendidoPor" runat="server" Text=""></asp:Label></td>
                </tr>              
                <tr>                    
                    <td class="col1">Respuesta:</td>
                    <td class="col2"><asp:Label ID="lblRespuestaEscuela" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>                    
                    <td class="col1">Adjunto:</td>
                    <td class="col2">
                        <asp:HyperLink ID="lblAdjuntoEscuela" runat="server" Target="_blank">Visualizar Adjunto</asp:HyperLink>                        
                    </td>
                </tr>
                <tr>                    
                    <td class="col1"><b>Resolver:</b></td>
                    <td class="col2">
                        <asp:TextBox ID="txtResponder" runat="server" TextMode="MultiLine" Width="99%" BackColor="#E8EEF7" ></asp:TextBox>
                    </td>
                </tr>
                <tr>                    
                    <td class="col1">Adjunto:</td>
                    <td class="col2"><asp:FileUpload ID="FileUpload1" runat="server" Width="50%" /></td>
                </tr>
                <tr align="center">                    
                    <td colspan="2">
                        <asp:Button ID="btnResponder" runat="server" Text="Resolver" style="height: 26px; width:120px" CssClass="btn btnResolver" /> &nbsp;
                        <asp:Button ID="btnRevisarApropiar" runat="server" style="height: 26px; width:120px" CssClass="btn btnRevisar" Text="Revisar" /> &nbsp;
                        <asp:Button ID="btnDerivar" runat="server" Text="Derivar" style="height: 26px; width:120px" CssClass="btn btnDerivar"  /> &nbsp;
                        <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" style="height: 26px; width:120px" CssClass="btn btnCerrar" /> 
                    </td>
                </tr>
            </table>  
    </div>
    <asp:HiddenField ID="HdInstancia" runat="server" />
    <asp:HiddenField ID="HdIncidente" runat="server" />
    <asp:HiddenField ID="HdIncidentePadre" runat="server" />
    <asp:HiddenField ID="HdCiclo" runat="server" /> 
    </form>
</body>
</html>

