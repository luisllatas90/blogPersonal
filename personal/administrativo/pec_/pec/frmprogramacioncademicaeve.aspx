<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmprogramacioncademicaeve.aspx.vb" Inherits="frmprogramacioncademicaeve" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Programación Académica</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css"/>   
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../../private/PopCalendar.js"></script>
   
  
   <script src="jquery/jquery-1.10.2.js" type="text/javascript"></script>   
  <script src="jquery/jquery-ui.js" type="text/javascript"></script>    
  <script src="jquery/jquery.ui.datepicker-es.js" type="text/javascript"></script>
  <script>
        function MarcarCursos(obj)
        {
           //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input');
            for (var i = 0 ; i < arrChk.length ; i++){
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox"){
                    chk.checked = obj.checked;
                    if (chk.id!=obj.id){
                        PintarFilaMarcada(chk.parentNode.parentNode,obj.checked)
                    }
                }
            }
        }
      
         
        function PintarFilaMarcada(obj,estado)
        {
            if (estado==true){
                obj.style.backgroundColor="#FFE7B3"
            }
            else{
                obj.style.backgroundColor="white"
            }
        }

        function ejecutar() {
            window.open("frmProgramarHorarios.aspx", "_blank", "toolbar=no, scrollbars=no, resizable=no, top=350, left=500, width=500, height=300,menubar=no");
        }
        function solicitar() {
            window.open("frmSolicitarAmbiente.aspx", "_blank", "toolbar=no, scrollbars=yes, resizable=no, top=220, left=350, width=auto, height=500,menubar=no");
        }
        $(function() {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#txtDesde").datepicker({
                firstDay: 0
            });
            $("#txtHasta").datepicker({
                firstDay: 0
            });
        }); 
    </script>
    <style type="text/css">
        .style1
        {
            height: 11px;
        }
    
        .style3
        {
            height: 147px;
        }
        body
        { font-size:10px;
            
            }
     .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px;
            }
        .style4
        {
            height: 46px;
        }
        .style5
        {
            font-size: xx-small;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
     <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock" Text="Buscando ambientes disponibles..." Title="Por favor espere" />
    <%Response.Write(ClsFunciones.CargaCalendario)%> 
    <p class="usatTitulo">Programación Académica:&nbsp;
    <asp:DropDownList ID="dpCodigo_cac" runat="server" AutoPostBack="True">
    </asp:DropDownList>
    <!--<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true"  runat="server"></asp:ScriptManager>-->
    </p>
<table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
        <tr>
            <td style="height: 30px;">
                Centro de Costos:                                         
            </td>
            <td colspan="3" style="width:80%">
                <asp:DropDownList 
                                    ID="cboCecos" 
                                    runat="server" 
                                    AutoPostBack="True" 
                                    SkinID="ComboObligatorio" 
                                    Width="95%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="height: 30px; ">
                Plan:                 
                
                &nbsp;</td>
            <td colspan="3">
                <asp:DropDownList ID="dpCodigo_pes" Width="95%" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="height:30px" >
                Filtrado por:</td>
            <td style="width:20%">
                <asp:DropDownList ID="ddlFiltro" Width="100%" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="0">TODOS</asp:ListItem>
                    <asp:ListItem Value="1">CURSOS</asp:ListItem>
                    <asp:ListItem Value="2">NIVELACIÓN</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width:15%">
                Nombre del curso:</td>
            <td>
                <asp:TextBox ID="txtBusqueda" Width="91%" runat="server" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                
                <asp:Button ID="cmdVer" runat="server" Text="Consultar" 
                    CssClass="buscar2" />
                
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height:17px; font-weight: bold; font-size: xx-small;" bgcolor="#ECECFF">
            
                <asp:Label ID="lblMsjFechas" runat="server" Font-Bold="False" 
                    ForeColor="#000099" Text="Fechas definidas para la programación académica son:"></asp:Label>
&nbsp;<asp:Label ID="lblFechaIni" runat="server" ForeColor="#CC0000"></asp:Label>
&nbsp;<asp:Label ID="lblIntervalo" runat="server" Font-Bold="False" ForeColor="#000099" 
                    Text="al"></asp:Label>
&nbsp;<asp:Label ID="LblFechaFin" runat="server" ForeColor="#CC0000"></asp:Label>
            
            </td>
        </tr>
        </table>
            
            <table id="DetallesCurso" runat="server">
            <tr>  
            <td valign="top">
                 <table runat="server" id="fraDetalleGrupoHorario" cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1;" visible="false" width="100%">
                                        
                                        <tr style="background-color: #E8EEF7; font-weight: bold;">
                                            <td colspan="3">
                                                Datos del Curso Programado</td>
                                            <td align="right">
                                                <asp:Button ID="cmdGuardar" runat="server" Text="  Guardar" 
                                                    CssClass="guardar2" />
                                                &nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="Regresar" 
                                                    CssClass="regresar2" />
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="rojo" colspan="4">
                                                &nbsp;</td>
                                                    
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblNiv" runat="server" Text="Curso faltante/desaprobado:" 
                                                    style="font-weight: 700"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:CheckBox ID="chkFaltanteDesap" 
                                                    ToolTip="Habilitar el Check para indicar que el curso es de NIVELACIÓN" 
                                                    runat="server" Visible="False" AutoPostBack="True" Checked="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Curso</b></td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="dpCurso" runat="server">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblnombre_cur" runat="server" style="font-weight: 700"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Fecha</b></td>
                                            <td>
                                                <asp:Label ID="lblFecha" runat="server" EnableViewState="False"></asp:Label>
                                            </td>
                                            <td>
                                                <b>Vacantes</b></td>
                                            <td valign="bottom">
                                                &nbsp;
                                                <asp:TextBox ID="txtVacantes" runat="server" Columns="4" MaxLength="3">30</asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RqGrupoHorario" runat="server" 
                                                    ControlToValidate="txtGrupoHor_cup" 
                                                    ErrorMessage="Debe especificar la denonimación del grupo horario">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                <asp:Label ID="lblgrupohor_cup" runat="server" EnableViewState="False" 
                                                    Text="Grupo" Visible="False" style="font-weight: 700"></asp:Label>
                                            </td>
                                            <td class="style1" colspan="3">
                                                <asp:TextBox ID="txtGrupoHor_cup" runat="server" CssClass="cajas"  Width="90%"
                                                    MaxLength="120" Visible="False">A</asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RqVacantes" runat="server" 
                                                    ControlToValidate="txtVacantes" 
                                                    ErrorMessage="Debe especificar el número vacantes por cada grupo horario">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Inicio</b></td>
                                            <td>
                                                <asp:TextBox ID="txtInicio" runat="server" BackColor="#CCCCCC" Font-Size="8pt" 
                                                    ForeColor="Navy" MaxLength="12" style="text-align: right" Columns="12"></asp:TextBox>
                                                <asp:Button ID="cmdInicio" runat="server" CausesValidation="False" 
                                                    Font-Size="7pt" 
                                                    onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtInicio,'dd/mm/yyyy');return(false)" 
                                                    Text="..." UseSubmitBehavior="False" />
                                                <asp:RequiredFieldValidator ID="RqFechaInicio" runat="server" 
                                                    ControlToValidate="txtInicio" 
                                                    ErrorMessage="Debe especificar la fecha de inicio">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <b>Fin</b></td>
                                            <td>
                                                <asp:TextBox ID="txtFin" runat="server" BackColor="#CCCCCC" Font-Size="8pt" 
                                                    ForeColor="Navy" MaxLength="12" style="text-align: right" Columns="12"></asp:TextBox>
                                                <asp:Button ID="cmdFin" runat="server" CausesValidation="False" Font-Size="7pt" 
                                                    onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFin,'dd/mm/yyyy');return(false)" 
                                                    Text="..." UseSubmitBehavior="False" />
                                                &nbsp;<asp:RequiredFieldValidator ID="RqFechaFin" runat="server" 
                                                    ControlToValidate="txtFin" 
                                                    ErrorMessage="Debe especificar la fecha de término">*</asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CRqFechaFin" runat="server" 
                                                    ControlToCompare="txtFin" ControlToValidate="txtInicio" 
                                                    ErrorMessage="Fecha de Termino menor o igual a fecha de inicio." 
                                                    Operator="LessThan" Type="Date">*</asp:CompareValidator>
                                                <asp:TextBox ID="txtRetiro" runat="server" BackColor="#CCCCCC" Font-Size="8pt" 
                                                    ForeColor="Navy" MaxLength="12" style="text-align: right" Columns="12" 
                                                    Visible="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Estado</b></td>
                                            <td>
                                                <asp:DropDownList ID="dpestado_cup" runat="server" Enabled="False">
                                                    <asp:ListItem Value="1">Abierto</asp:ListItem>
                                                    <asp:ListItem Value="0">Cerrado</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td align="left">
                                                <asp:CheckBox ID="chkNivelacion" 
                                                    ToolTip="Habilitar el Check para indicar que el curso es de NIVELACIÓN" 
                                                    runat="server" Visible="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Registrado</b>: </td>
                                            <td colspan="3">
                                                <asp:Label ID="lblOperador" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        
            </table>
            </td>
            
               <td valign="top">
               <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1;"  id="fraDetalleAmbiente" runat="server" visible="false">
               <tr style="background-color: #E8EEF7; font-weight: bold; height: 26px">
               <td>Asignación de Profesores al Grupo Horario</td>               
               </tr>
               <tr><td>Profesor:<asp:DropDownList ID="dpCodigo_per" runat="server">
                                                </asp:DropDownList>
                                                <asp:Button ID="cmdAsignarProfesor" runat="server" 
                                                    Text="Agregar" Visible="False" CssClass="agregar2" />
                   </td></tr>
               <tr>
               <td class="style3" valign="top">
                                                <br />
                                                <asp:GridView ID="grwProfesores" runat="server" AutoGenerateColumns="False" 
                                                    CaptionAlign="Top" DataKeyNames="codigo_per" Width="80%" 
                                                    EnableModelValidation="True" BorderStyle="None" CellPadding="3" 
                                                    GridLines="Horizontal">
                                                    <RowStyle BorderColor="#C2CFF1" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        <asp:BoundField DataField="docente" HeaderText="Profesor">
                                                            <ItemStyle Font-Underline="True" ForeColor="#0066CC" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="descripcion_fun" HeaderText="Función">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="totalHorasAula" HeaderText="Hrs. Clase">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="totalHorasAsesoria" HeaderText="Hrs. Asesoría">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="totalHoras_Car" HeaderText="Total Hrs.">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="../../../images/eliminar.gif" 
                                                            ShowDeleteButton="True">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
                                                <br />
                                                <asp:Label ID="lblMsjHorarioAmbiente" runat="server" 
                                                    style="font-style: italic; color: #336699; background-color: #FFFFFF"></asp:Label>
                                                </td>
               
               </tr>
           
              </table>
            </td>
            </tr>
            </table>
          
            
             <br />
             <table runat="server" ID="fraProfesores" cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%" visible="false">
                                        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                                            <td >Horarios y Ambientes&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                           
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCurso" runat="server" Text="Label" style="font-weight: 700" 
                                                    Visible="False"></asp:Label>
                
                                                <asp:Button ID="cmdRegistrarHorario" runat="server" Text="Registrar Horario" 
                                                    CssClass="horario2" Width="120px" Enabled="False" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox ID="CheckBox1" runat="server" Text="Incluir Finalizados" 
            AutoPostBack="True" />
                                                &nbsp;&nbsp;
                                                <asp:Label ID="lblLimite" runat="server" style="color: #FF6600" Text="Label"></asp:Label>
                                                &nbsp;&nbsp;<span class="style5">(*)Excepto Solicitud de Auditorios </span>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                   <div id="divHor" style="width:100%">                                             
                    <div id="divHorLista" runat="server" style="width:70%;float:left;" >
                    <asp:GridView ID="gridHorario" runat="server" AutoGenerateColumns="False"
                                                CaptionAlign="Top" DataKeyNames="codigo_lho"
                                                    BorderStyle="None" CellPadding="2" Width="100%"
             AlternatingRowStyle-BackColor="#F7F6F4" >
             <RowStyle BorderColor="#C2CFF1" />
             <Columns>
                 <asp:BoundField DataField="codigo_lho" HeaderText="codigo_lho" Visible="false">
                 <ItemStyle Font-Underline="false" ForeColor="#0066CC" />
                 </asp:BoundField>
                 <asp:BoundField DataField="dia_Lho" HeaderText="Día">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField>
                 <asp:BoundField DataField="fechaIni_lho" HeaderText="Fecha" >
                 <ItemStyle Font-Underline="false" />
                 </asp:BoundField>
                  <asp:BoundField DataField="descripcion_lho" HeaderText="Descripción" />
                 <asp:BoundField DataField="nombre_hor" HeaderText="Inicio" />
                 <asp:BoundField DataField="horaFin_Lho" HeaderText="Fin" >
                 <ItemStyle Font-Underline="false" />
                 </asp:BoundField>
                 <asp:BoundField DataField="codigo_amb" HeaderText="codigo_amb" Visible="false" >
                 <ItemStyle Font-Underline="false"  />
                 </asp:BoundField>  
                   <asp:BoundField DataField="preferencial_amb" HeaderText="Tipo"  ItemStyle-HorizontalAlign="Center">
                   <ItemStyle Font-Underline="false" />
                  </asp:BoundField>                              
                 <asp:BoundField DataField="Ambiente" HeaderText="Ambiente" ItemStyle-HorizontalAlign="left">
                  <ItemStyle Font-Underline="false" />
                   </asp:BoundField>
                 
                 <asp:BoundField DataField="estadoHorario_lho" HeaderText="Estado">
                   <ItemStyle Font-Underline="false" />
                  </asp:BoundField>
                
                 <asp:TemplateField HeaderText="Solicitar Ambiente" 
                     ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px">
                     <ItemTemplate>
                         <asp:ImageButton ID="btnSolicitarAmbiente" runat="server" 
                             CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="SolicitarAmbiente" ImageUrl="~/administrativo/pec/image/Asol.png" 
                             ToolTip="Solicitar Ambiente" />
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Borrar Ambiente" 
                     ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px">
                     <ItemTemplate>
                         <asp:ImageButton ID="btnQuitarAmbiente" runat="server" 
                             CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="LimpiarAmbiente" ImageUrl="~/administrativo/pec/image/eA.png" 
                             ToolTip="Borrar Ambiente" />
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
                 <asp:CommandField ButtonType="Image" 
                     DeleteImageUrl="~/administrativo/pec/image/eH.png" DeleteText="Borrar Horario" 
                     HeaderText="Borrar Horario" ItemStyle-HorizontalAlign="Center" 
                     ItemStyle-Width="45px" ShowDeleteButton="True">
                 <ItemStyle HorizontalAlign="Center" />
                 </asp:CommandField>
                <%-- <asp:CommandField ButtonType="Image" 
                     DeleteImageUrl="~/administrativo/pec/image/editar.gif" EditText="Mod Descripcion" 
                     HeaderText="Modificar Descripcion" ItemStyle-HorizontalAlign="Center" 
                     ItemStyle-Width="45px" ShowDeleteButton="True">
                 <ItemStyle HorizontalAlign="Center" />
                 </asp:CommandField>--%>
                 <asp:TemplateField HeaderText="Mod Descripcion" 
                     ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px">
                     <ItemTemplate>
                         <asp:ImageButton ID="btnModificarLho" runat="server" 
                             CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="ModificarLho" ImageUrl="~/administrativo/pec/image/editar.gif" 
                             ToolTip="Modificar Horario" />
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
             </Columns>
             <EmptyDataTemplate>
                 <div style="color:#3266DB; background-color:#E8EEF7; padding:5px; font-style:italic;">
                     No se ha registrado horarios.
                 </div>
             </EmptyDataTemplate>
             <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" 
                 ForeColor="#3366CC" />
             <AlternatingRowStyle BackColor="#F7F6F4" />
         </asp:GridView>
                    </div>
                    <div id="divHorReg" runat="server" style="width:30%;float:left;">
                    <table style="width:100%">
                    <thead>
                    <tr>
                    <th colspan="2" style="background-color:Red; font-weight:bold; color:White;text-align:center"><asp:Image  runat="server" ImageUrl="~/administrativo/pec/image/editar.gif" /> Descripci&oacute;n 
                        Horario<asp:HiddenField ID="txtcodhor" runat="server" /></th>
                    </tr>
                    </thead>
                    <tbody>
                    <tr>
                    <td style="width:30%;">Descripci&oacute;n</td>
                    <td style="width:70%;"><input type="text" id="txtdescripcionhor" runat="server" style="width:100%" />
                    </td>
                    </tr>
                    </tbody>
                    <tfoot>
                    <tr>
                    <td colspan="2" style="text-align:center">
                    <asp:Button ID="btnGuardarLho" runat="server" Text="  Guardar" CssClass="guardar2" />
                                                &nbsp;<asp:Button ID="btnCancelarLho" runat="server" Text="Cancelar" CssClass="regresar2" />
                    </td>
                    </tr>
                    </tfoot>
                    </table>
                    <hr />
                    </div>
                   </div>
        <asp:Panel ID="pnlRegistrar" runat="server" Width="55%" Visible="False">
        <table style="width: 95%;" border="0" cellpadding="0">
                
            <tr>
            <td>Fecha Inicio</td>
            <td>
                <input ID="txtDesde" runat="server" type="text" /></td><td colspan="2">
                <asp:Label ID="lbldia" runat="server" Text="Día de Sesión: -"></asp:Label>
                </td>
            </tr>
            <tr>
            <td>Hora Inicio</td>
            <td>
                &nbsp;<br />
                <asp:DropDownList ID="ddlInicioHora" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlHoraInicioProf" runat="server" Enabled="False">
                    <asp:ListItem Value="08">08:00</asp:ListItem>
                    <asp:ListItem Value="15">15:00</asp:ListItem>
                    <asp:ListItem Value="18">18:40</asp:ListItem>
                    <asp:ListItem Value="19">19:30</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlInicioMinuto" runat="server">
                </asp:DropDownList>
                </td>
                <td>
                    Nro. Ambientes</td>
                <td>
                    <asp:DropDownList ID="ddlNro" runat="server">
                        <asp:ListItem Selected="True">1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                    </asp:DropDownList>
            
                </td>
            </tr>
            <tr>
            <td>Hora Fin</td>
            <td>
                
                <br />
                <asp:DropDownList ID="ddlFinHora" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlHoraFinProf" runat="server" Enabled="False">
                <asp:ListItem Value="13">13:00</asp:ListItem>
                <asp:ListItem Value="18">18:20</asp:ListItem>
                <asp:ListItem Value="20">20:00</asp:ListItem>
                <asp:ListItem Value="22">22:00</asp:ListItem>
                <asp:ListItem Value="23">22:30</asp:ListItem>
                <asp:ListItem Value="23:10">23:10</asp:ListItem>
                    
                    
                    <asp:ListItem>23:20</asp:ListItem>
                    
                    
                </asp:DropDownList>
                
                <asp:DropDownList ID="ddlFinMinuto" runat="server">
                </asp:DropDownList>
                
                </td>
                <td>
                    Capacidad</td>
                <td>
                    <asp:DropDownList ID="ddlCap" runat="server">
              
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
            <td>Fecha Fin</td>
            <td>
            <asp:CheckBox ID="chkVarias" runat="server" Text="Crear varias sesiones" 
             CssClass="fuente"  AutoPostBack=true/><br />
                <input ID="txtHasta" runat="server" type="text" /></td>
                <td>
                    Día de Sesión<br />
                        <asp:DropDownList ID="ddlDiaSelPer" runat="server">
                        <asp:ListItem Value="LU">Lunes</asp:ListItem>
                        <asp:ListItem Value="MA">Martes</asp:ListItem>
                        <asp:ListItem Value="MI">Miércoles</asp:ListItem>
                        <asp:ListItem Value="JU">Jueves</asp:ListItem>
                        <asp:ListItem Value="VI">Viernes</asp:ListItem>
                        <asp:ListItem Value="SA">Sábado</asp:ListItem>
                        <asp:ListItem Value="DO">Domingo</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>

             <tr>
                 <td>
                     Motivo Solicitud</td>
                 <td>
                     <asp:DropDownList ID="ddlTipSolicitud" runat="server">
                     </asp:DropDownList>
                 </td>
                 <td>
                     <asp:CheckBox ID="chkAudi" runat="server" style="font-weight: 700" 
                         Text="Auditorios (*)" />
                     </td>
                 <td>
                     &nbsp;</td>
            </tr>

             <tr><td>Descripción del Evento</td><td colspan="3">
                 <asp:Label ID="lblmsj" runat="server" style="color: #CC3300"></asp:Label>
                 <br />
                <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="500" TextMode="MultiLine" 
                    Width="415px" Height="77px"></asp:TextBox></td>
            </tr>
            <tr><td>&nbsp;</td>
            <td colspan="3">
            <asp:Button ID="btnRegistrarPers" runat="server" Text="Registrar" CssClass="btn" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn" Text="Cancelar" />
            </td>
            </tr>
                 </table>
    </asp:Panel>
    
 <center>
   <asp:Panel ID="pnlPregunta" runat="server" BorderColor="#5D7B9D" 
        BorderStyle="Solid" BorderWidth="1px" style="text-align: center; padding:5px;" 
        Visible="False" Width="25%" BackColor="#F7F6F4">
        <b><span class="style1">
              ¿Desea registrar la actividad para el día <span class="style2">Domingo</span>?</span></b><br />
        <br />
        <asp:Label ID="Label1" runat="server" 
            style="color: #3366CC;  font-weight: 700;" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lblActividad" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnSi" runat="server" CssClass="btn" Text="Sí" Width="50px" />
        &nbsp;&nbsp;
        <asp:Button ID="btnNo" runat="server" CssClass="btn" Text="No" Width="50px" />
        <br />
      
    </asp:Panel>
    </center>

    
                                                <asp:Panel ID="pnlSolictar" runat="server" Visible="False">
                                                   <table  cellpadding="3" cellspacing="0" style="border: 0px solid #C2CFF1; width:70%" >
        <tr>
            <td><b>Fecha <span class="style1">*</span></b></td>
            <td>
                <b>Audio</b></td>
            
            <td>
                <b>Video</b></td>
            
            <td>
                <b>Sillas</b></td>
            
            <td>
                <b>Distribución</b></td>
            <td>
                <b>Ventilación</b></td>
            <td>
                <b>Otros</b></td>
            
            <td>
                &nbsp;</td>
            
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlHorarios" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="ddlAudio" runat="server"></asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="ddlVideo" runat="server"></asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="ddlSillas" runat="server"></asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="ddlDis" runat="server"></asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="ddlVenti" runat="server"></asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="ddlOtros" runat="server"></asp:DropDownList>            
            </td>
            <td>&nbsp;</td>
        </tr>
                                                       <tr>
                                                           <td>
                                                               <b>Tipo de Ambiente</b></td>
                                                           <td>
                                                               &nbsp;</td>
                                                           <td>
                                                               &nbsp;</td>
                                                           <td>
                                                               &nbsp;</td>
                                                           <td>
                                                               &nbsp;</td>
                                                           <td>
                                                               &nbsp;</td>
                                                           <td>
                                                               &nbsp;</td>
                                                           <td>
                                                               &nbsp;</td>
                                                       </tr>
                                                       <tr>
                                                           <td>
                                                               <asp:DropDownList ID="ddlTipoAmbiente" runat="server">
                                                               </asp:DropDownList>
                                                           </td>
                                                           <td>
                                                               <asp:Button ID="Button1" runat="server" CssClass="btn" Text="Buscar" 
                                                                   Width="79px" />
                                                           </td>
                                                           <td>
                                                               <asp:Button ID="btnCancelar0" runat="server" CssClass="btn" Text="Regresar" 
                                                                   Width="79px" />
                                                           </td>
                                                           <td>
                                                               &nbsp;</td>
                                                           <td>
                                                               &nbsp;</td>
                                                           <td>
                                                               &nbsp;</td>
                                                           <td>
                                                               &nbsp;</td>
                                                           <td>
                                                               &nbsp;</td>
                                                       </tr>
        <tr>
            <td colspan="7" class="style4">
                <img alt="" longdesc="Aula" src="image/door.png" 
                    style="width: 16px; height: 16px" />Ambiente será asignado inmediatamente. 
                No para días domingo.<br />
                <img alt="" longdesc="Preferencial" src="image/star.png" 
                    style="width: 16px; height: 16px" />Ambiente será asignado según 
                disponibilidad de ambiente. (Máx en 48h)<br />
                (<span class="style1">*</span>) Se listan los horarios registrados que no tienen 
                ambiente asignado.</td>
        </tr>
        </table>
        <br />
        <table id="ListadoAmbientes" runat="server"  cellpadding="1" cellspacing="0" style="border: 1px solid #C2CFF1; width:70%" visible=false>
        <tr  style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td colspan="6">                
            <b>Listado de ambientes según filtros de búsqueda </b>
            </td>            
        </tr>
        <tr>
            <td colspan="6"><br />
                <asp:GridView ID="gridAmbientes" runat="server" BorderStyle="Solid" 
                    CellPadding="2" BackColor="White" BorderColor="#C2CFF1" 
                    BorderWidth="1px" Width="65%" DataKeyNames="Ambiente"  >
                    <RowStyle BorderColor="#C2CFF1" HorizontalAlign="Center" BackColor="White" ForeColor="#333333" Font-Size="10px" />
                    <Columns>
                    <asp:TemplateField HeaderText="Accion" 
                     ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px">
                     <ItemTemplate>                                                                            
                        
                         <asp:LinkButton ID="btnSolicitar" runat="server"
                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                CommandName="Asignar/Solicitar"> <asp:Image ID="imgSol" runat="server" />Solicitar/Asignar</asp:LinkButton>  
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <PagerStyle BackColor="#C2CFF1" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C2CFF1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#E8EEF7" Font-Bold="True" ForeColor="#587ECB" />
                    <EmptyDataTemplate>
                    <div><i>No se encontraron ambientes.</i></div>
                    </EmptyDataTemplate>
               </asp:GridView>
            </td>            
        </tr>
        <tr>
            <td colspan="6"></td>
        </tr>
        <tr>
            <td colspan="6">&nbsp;</td>
        </tr>
         
        <tr>
            <td colspan="6"><br />
            </td>            
        </tr>
    </table>    
                                                </asp:Panel>
   

 

                                            </td>
                                        </tr>
                                    </table>             
              <br />
              
              
              
    <br />
&nbsp;<table ID="fraGruposProgramados" runat="server" cellpadding="3" 
                                        cellspacing="0" style="border: 1px solid #C2CFF1; width:100%" visible="false">
                                        <tr style="background-color: #E8EEF7; font-weight: bold;">
                                            <td>
                                                <asp:Label ID="lblGrupos" runat="server" 
                                                    Text="Lista de Grupos Horario Programados"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="cmdAgregar" runat="server" Text="Nuevo grupo" 
                                                    CssClass="agregar2" Width="100px" />
                                            &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="grwGruposProgramados" runat="server" 
                                                    AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Top" 
                                                    DataKeyNames="codigo_cup" Width="100%" BorderColor="Silver" 
                                                    EnableModelValidation="True" CellPadding="2">
                                                    <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nombre_cur" HeaderText="Asignatura">
                                                            <ItemStyle Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="fechainicio_cup" HeaderText="Inicio" 
                                                            DataFormatString="{0:d}">
                                                            <ItemStyle Font-Size="7pt" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="fechafin_cup" DataFormatString="{0:d}" 
                                                            HeaderText="Fin">
                                                            <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="descripcion_pes" HeaderText="Plan Estudio" />
                                                        <asp:BoundField DataField="vacantes_cup" HeaderText="Vacantes">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="total_mat" HeaderText="Inscritos">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="estado_cup" HeaderText="Estado">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:CheckBoxField DataField="faltanteDesap_cup" HeaderText="Falt Desap." />
                                                        <asp:TemplateField HeaderText="Profesor" ItemStyle-VerticalAlign="Top">
                                                            <ItemTemplate>
                                                                <asp:BulletedList ID="lstProfesores" runat="server" DataTextField="docente" 
                                                                    DataValueField="codigo_per" Font-Size="7pt">
                                                                </asp:BulletedList>
                                                            </ItemTemplate>

                                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Exam." Visible="False">
                                                            <ItemTemplate>
                                                                <table style="width:100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:HiddenField ID="hfcodigo_cupRec"  runat="server" value='<%# Bind("codigo_cup") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton 
                                                                                    ID="ibtnRecuperacion" 
                                                                                    runat="server" 
                                                                                    ToolTip="Ex. Recuperación" 
                                                                                    Visible='<%# iif(Eval("nivelacion_cup") = "1", "True", "False") %>'
                                                                                    ImageUrl="../../../images/examen.png" 
                                                                                    onclick="ibtnRecuperacion_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="3%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                          </asp:TemplateField>
                                                         
                                                         <asp:TemplateField HeaderText="Asig. Desp." Visible="False">
                                                           <%-- <ItemTemplate>
                                                                <table style="width:100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:HiddenField ID="hfcodigo_cup"  runat="server" value='<%# Bind("codigo_cup") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton 
                                                                                    ID="ibtnInvitar" 
                                                                                    runat="server" 
                                                                                    ToolTip="Invitar al Curso" 
                                                                                    Visible='<%# iif(Eval("nivelacion_cup") = "1", "True", "False") %>'
                                                                                    ImageUrl="../../../images/invitar.png" 
                                                                                    onclick="ibtnInvitar_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="3%" HorizontalAlign="Center" VerticalAlign="Middle" />--%>
                                                          </asp:TemplateField>                                                         
                                                          
                                                        
                                                        <asp:TemplateField HeaderText="Asig. Falt.">
                                                         <ItemTemplate>
                                                                <table style="width:100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:HiddenField ID="hfcodigo_cup"  runat="server" value='<%# Bind("codigo_cup") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton 
                                                                                    ID="ibtnInvitar2" 
                                                                                    runat="server" 
                                                                                    ToolTip="Invitar al Curso" 
                                                                                    Visible='<%# iif(Eval("faltanteDesap_cup") = "1", "True", "False") %>'
                                                                                    ImageUrl="../../../images/invitar.png" 
                                                                                    onclick="ibtnInvitar2_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="3%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        
                                                        
                                                        </asp:TemplateField>
                                                         
                                                          
                                                        
                                                        <asp:ButtonField ButtonType="Image" CommandName="editar"  HeaderText="Edit." 
                                                            ImageUrl="../../../images/editar.gif" Text="Editar" />
                                                        <asp:CommandField ShowDeleteButton="True" 
                                                            DeleteText="Eliminar" ButtonType="Image" 
                                                            DeleteImageUrl="../../../images/eliminar.gif">
                                                            <ControlStyle Font-Underline="True" />
                                                            <ItemStyle Font-Underline="True" ForeColor="Blue" 
                                                                HorizontalAlign="Center" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
                                                <asp:HiddenField ID="hdcodigo_cup" runat="server" Value="0" />
                                            </td>
                                        </tr>
                                    </table>
                                  
            <!--
            </ContentTemplate>
            </asp:UpdatePanel>
            -->
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
