<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AdministrarCronograma.aspx.vb" Inherits="academico_matricula_administrar_AdministrarCronograma" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="../../../javascript/DateTime/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../../../private/funciones.js"></script>  
    <script language="JavaScript" src="../../../../private/tooltip.js"></script>
    
  <%--  <script type="text/javascript" src="../../../javascript/js/jquery.js"></script>--%>
  <%--  <script type="text/javascript" src="../../../javascript/js/jquery.dataTables.min.js"></script>    
    <link rel='stylesheet' href='../../../javascript/css/jquery.dataTables.min.css'/>--%>
    
    <script src="../../../javascript/DateTime/Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../../../javascript/DateTime/Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="../../../javascript/DateTime/Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        var nav1 = window.Event ? true : false;
        var nav2 = window.Event ? true : false;
        function solonumerosentero(evt) {
            // Backspace = 8, Enter = 13, '0' = 48, '9' = 57, '.' = 46
            var key = nav1 ? evt.which : evt.keyCode;
            return (key <= 13 || (key >= 48 && key <= 57));
        }
        function solonumerodecimal(evt) {
            // Backspace = 8, Enter = 13, '0' = 48, '9' = 57, '.' = 46
            var key = nav2 ? evt.which : evt.keyCode;
            return (key <= 13 || (key >= 48 && key <= 57) || key == 46);
        }
</script>
     <script language="javascript">

         $(document).ready(function() {
             //alert('hola');
    
             $("#<%=txtfi.ClientID %>").dynDateTime({
                 showsTime: true,
                 ifFormat: "%d/%m/%Y %H:%M",
                 daFormat: "%l;%M %p, %e %m,  %Y",
                 align: "BR",
                 electric: false,
                 singleClick: false,
                 displayArea: ".siblings('.dtcDisplayArea')",
                 button: ".next()"
             });
             $("#<%=txtff.ClientID %>").dynDateTime({
                 showsTime: true,
                 ifFormat: "%d/%m/%Y %H:%M",
                 daFormat: "%l;%M %p, %e %m,  %Y",
                 align: "BR",
                 electric: false,
                 singleClick: false,
                 displayArea: ".siblings('.dtcDisplayArea')",
                 button: ".next()"
             });

         });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdFn" runat="server" />
    <asp:HiddenField ID="hd" runat="server" />
    <div id="divLoad"  runat="server" >
    <center><img src="../../../../images/loading.gif" /></center>
    </div>
    <div id="divCro"  runat="server"> 
        <table style="width:100%;">
            <tr>
                <td class="usatTitulo">
                    Actualizar cronograma por semestre académico</td>
            </tr>
            <tr>
                <td>
                    Semestre Académico:
                    <asp:DropDownList ID="cboCicloAcademico" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Modalidad de Estudio:
                    <asp:DropDownList ID="cboTipoEstudio" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                &nbsp;<asp:Button ID="btnGenerar" runat="server" Text="Generar Cronograma" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvCronograma" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="codigo_Cro,codigo_Act,codigo_Cac,Actividad,Ciclo Acad." 
                        DataSourceID="SqlDataSource1" Width="100%"> 
                        <Columns>
                            <asp:BoundField DataField="codigo_Cro" HeaderText="codigo_Cro" 
                                InsertVisible="False" ReadOnly="True" SortExpression="codigo_Cro" 
                                Visible="False" />
                            <asp:BoundField DataField="Ciclo Acad." HeaderText="SEMESTRE ACAD." 
                                SortExpression="Ciclo Acad." ReadOnly="True" />
                            <asp:BoundField DataField="Actividad" HeaderText="ACTIVIDAD" 
                                SortExpression="Actividad" ReadOnly="True" />
                            <asp:BoundField DataField="Fecha_Inicio" HeaderText="F. INICIO" 
                                SortExpression="Fecha_Inicio" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Fecha_Fin" HeaderText="F. FINAL" 
                                SortExpression="Fecha_Fin" DataFormatString="{0:dd/MM/yyyy}"/>
                            <asp:BoundField DataField="Observación" HeaderText="OBSERVACION" 
                                SortExpression="Observación" />
                            <asp:CommandField ShowEditButton="True" />
                            <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="BtnMostrar" runat="server" Text="Cronograma Matricula" class="btn" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                 CommandName="configurar"  />
                        </ItemTemplate>
                        <ItemStyle Width="15%" />
                    </asp:TemplateField>
                        </Columns>
                        
                        <HeaderStyle BackColor="#e33439" ForeColor="#FFFFFF" Height="22px" />
                        <RowStyle Height="22px" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="MAT_ConsultarCronogramaV2" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="cboCicloAcademico" Name="codigo_cac" 
                                PropertyName="SelectedValue" Type="Int32" />
                            <asp:ControlParameter ControlID="cboTipoEstudio" Name="codigo_test" 
                                PropertyName="SelectedValue" Type="Int32" />
                              <asp:QueryStringParameter Name="codigo_tfu" DbType = "String" Direction = "Input" QueryStringField="ctf" DefaultValue="0" ConvertEmptyStringToNull="True" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table> 
    </div>
    <div id="divCroMat"  runat="server">
        <table id="tableMat" style="width:100%;" runat="server">
    <tr>
    <td colspan='2'>
        <div id="Filtro">
    <table style="width:90%; border: 1px black;" align="center">
    <tbody>
        <tr>
            <td colspan="2" style="text-align:center">
                <h3>Configuración Cronograma de Matriculas</h3></td>
              
        </tr>
        <tr>
            <td style="width:100%;text-align:center;" colspan="2">
              <h5 id="tituloMat" runat="server" ></h5>              
              </td>    
        </tr>
        <tr>
            <td style="width:100%;text-align:center;" colspan="2">
              <h6 id="leyendaMat" runat="server" >
              Rango de Promedio Ponderado Semestral: PPS INI - PPS FIN<br />
              Rango de Fechas Para Matricularse segun PPS: F. INICIO - F. FINAL
              </h6>              
              </td>    
        </tr>
        <tr>
           <!-- <td style="width:50%;">
                &nbsp;</td>-->
            <td style="width:100%;text-align:center;" colspan="2">
                <asp:Button ID="btnRegresar" runat="server" class="regresar2" Text="Regresar" Width="100px" Height="20px" />
                <asp:Button ID="btnConsultar" runat="server" class="buscar" Text="Consultar" Width="100px" Height="20px" />
                <asp:Button ID="btnNuevo" runat="server" class="nuevo" Text="Registrar" Width="100px" Height="20px" />
            </td>
    
        </tr>
     </tbody>
     <tbody>
 <tr>
 <td>&nbsp;</td>
  <td>&nbsp;</td>

 </tr>
 </tbody>
 
 
 </table>
     <hr />
    </div>
    </td>
    </tr>
    <tr valign="top">
    <td style="width:70%" valign="top">
    
    <div id="Lista" runat="server" >

    <asp:GridView ID="lstGrupo" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="codigo_crmat"  BorderStyle="None" 
                 AlternatingRowStyle-BackColor="#F7F6F4" Width="100%" 
            Font-Size="X-Small"  class="display">
                    <Columns> 
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="BtnMostrar" runat="server" Width="30px" Height="20px" class="editar11" ToolTip="Editar"  CommandArgument="<%# CType(Container,GridViewRow).RowIndex %> " 
                                 CommandName="Editar"   />
                                 <asp:Button ID="BtnEliminar" runat="server" Text="" class="eliminar2" Width="15px" Height="20px" ToolTip="Eliminar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                 CommandName="Eliminar"  />
                        </ItemTemplate>
                        <ItemStyle  />
                    </asp:TemplateField>
                        <asp:BoundField DataField="pps_ini" HeaderText="PPS INI" >
                            <HeaderStyle Font-Bold="True" Font-Size="Small" />
                            <ItemStyle Width="23%" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="pps_fin" HeaderText="PPS FIN" >
                            <HeaderStyle Font-Bold="True" Font-Size="Small" />
                            <ItemStyle Width="23%" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_ini" HeaderText="FECHA INI" 
                            DataFormatString="{0:dd/MM/yyyy HH:mm}" >
                            <ItemStyle Width="23%" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_fin" HeaderText="FECHA FIN" 
                            DataFormatString="{0:dd/MM/yyyy HH:mm}" >
                        <ItemStyle Width="23%" HorizontalAlign="Right" />
                        </asp:BoundField>           
                       </Columns>
<HeaderStyle BackColor="#e33439" ForeColor="#FFFFFF" Height="22px" />
                        <RowStyle Height="22px" />
    <AlternatingRowStyle BackColor="#F7F6F4"></AlternatingRowStyle>
                </asp:GridView>
     </div>
     </td>
    <td style="width:30%">
     <div id="Registro" runat="server" visible="false">
                 <input type="hidden" id="txtid" value="" runat="server" />
                 <table width="100%" >
                 <tr>
                 <td colspan="2" style="text-align:center; background-color:#e33439; font-weight:bold; color:White; height:22px" id="tdRegistro" runat="server"></td>
                 </tr>
                 <tr>
                 <td style="width:50%">Prom. Pond. Semestral Inicial</td>
                 <td>
                 <input type="text" id="txtppsi" value="" runat="server"   style="text-align: right"  autocomplete="off"   onkeypress="javascript:return solonumerodecimal(event);" /></td>
                 </tr>
                 <tr>
                 <td>Prom. Pond. Semestral Final</td>
                 <td>
                 <input type="text" id="txtppsf" value="" runat="server" style="text-align: right"  autocomplete="off"   onkeypress="javascript:return solonumerodecimal(event);"/></td>
                 </tr>
                 <tr>
                 <td>Fecha Inicio</td>
                 <td>
                <asp:TextBox ID="txtfi" runat="server" ReadOnly="false"  style="text-align: right"></asp:TextBox><img src="../../../javascript/DateTime/calender.png" /></td>
                 </tr>
                 <tr>
                 <td>Fecha Fin</td>
                 <td>
                 <asp:TextBox ID="txtff" runat="server" ReadOnly = "false"  style="text-align: right"></asp:TextBox><img src="../../../javascript/DateTime/calender.png" /></td>
                 </tr>
                 <tr>
                 <td colspan="2" style="text-align:center">                
                    <asp:Button ID="btnGrabar" runat="server"  Text="Grabar" CssClass="guardar2" Width="100px" Height="20px" />
                    <asp:Button ID="btnCancelar" runat="server"  Text="Cancelar" CssClass="regresar2" Width="100px" Height="20px"  />
                 </td>
                 </tr>
                 </table>
                 </div>
    </td>
    </tr>    
    </table>
     </div>   
    </form>
</body>
</html>
