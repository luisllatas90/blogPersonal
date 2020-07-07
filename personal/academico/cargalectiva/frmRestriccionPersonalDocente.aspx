<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRestriccionPersonalDocente.aspx.vb" Inherits="academico_cargalectiva_frmRestriccionPersonalDocente" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
   <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
  <meta http-equiv='X-UA-Compatible' content='IE=8' />
  <meta http-equiv='X-UA-Compatible' content='IE=10' />
  <meta http-equiv='X-UA-Compatible' content='IE=11' />
 <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/> <!--320-->
<link rel='stylesheet' href='../../assets/css/bootstrap.min.css?x=1'/>
<link rel='stylesheet' href='../../assets/css/material.css?x=1'/>
<link rel='stylesheet' href='../../assets/css/style.css?y=1'/>
<script type="text/javascript" src='../../assets/js/jquery.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/app.js?x=1'></script>

<script type="text/javascript" src='../../assets/js/jquery-ui-1.10.3.custom.min.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/bootstrap.min.js?x=1'></script>

<script type="text/javascript" src='../../assets/js/jquery.nicescroll.min.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/wow.min.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/jquery.loadmask.min.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/jquery.accordion.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/materialize.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/funciones.js?a=1'></script>
<script type="text/javascript" src="../../assets/js/noty/jquery.noty.js"></script>
<script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>
<script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>
<script type="text/javascript" src="../../assets/js/noty/notifications-custom.js"></script>

<script type="text/javascript" src="../../assets/js/jquery.dataTables.min.js?x=1"></script>	
<link rel='stylesheet' href='../../assets/css/jquery.dataTables.min.css?z=1'/>
    <title>Disponibilidad de horario docente</title>
     <script type="text/javascript">
     var event = jQuery.Event("DefaultPrevented");
     $(document).trigger(event);
     $(document).ready(function() {
         var v = msieversion();

         if (v > 0) {
           //  $('div#TituloForm').html('<h2>Restricciones horario de docente</h2>');
         } else {
             $('div#divFila').remove();
         }
         
         $('#hdtv').val(v);
         
         var oTable = $('#gData').DataTable({
             //"sPaginationType": "full_numbers",
             "bPaginate": false,
             "bFilter": true,
             "bLengthChange": false,
             "bSort": false,
             "bInfo": true,
             "bAutoWidth": true
         });

     });
     function msieversion() {

         var ua = window.navigator.userAgent;
         var msie = ua.indexOf("MSIE ");
         var version = 0;
         if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))  // If Internet Explorer, return version number
         {
             //alert(parseInt(ua.substring(msie + 5, ua.indexOf(".", msie))));
             version = parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)));
         }
         else  // If another browser, return 0
         {
             version = 0;
         }

         return version;
     }

     function fnFoco(input) {
         $('#' + input).focus();
     }
     
     function fnDivLoad(div, time) {

         var $target = $('#' + div);
         $target.mask('<i class="fa fa-refresh fa-spin"></i> Cargando...');
         setTimeout(function() {
             $target.unmask();
         }, time);
     }
     </script>
    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 3px;
        }
        .content .main-content
        {
            padding-right: 0px;
        }
        .content
        {
            margin-left: 0px;
        }
        .form-control
        {
            border-radius: 0px;
            box-shadow: none;
            border-color: #d9d9d9;
            font-weight: 300;  line-height: 40px;
            color: black;
        }
        .i-am-new
        {
            z-index: 100;
        }
        .form-group
        {
            margin: 0px;
        }
        .form-horizontal .control-label
        {
            padding-top: 0px;
        }
    </style>
</head>
<body class="">
<div id="loading" class="piluku-preloader text-center" runat="server">
         <!--<div class="progress">
            <div class="indeterminate"></div>
            </div>-->
         <div class="loader">Loading...</div>
      </div>
<div class="wrapper">
<div class="content">


<div class="main-content">
 <form class="form form-horizontal" id="frmRestriccion"  runat="server">  
  <asp:HiddenField ID="hdtv" runat="server" />
 <div  id="report">
 <div id="PanelLista" runat="server">
 
                <div class="row">                   
                    <div class="manage_buttons">
                        <div class="row">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="page_header">
                                        <div class="pull-left" id="TituloForm">
                                            <span class="main-text">Disponibilidad de horario docente</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <asp:LinkButton ID="btnConsultar" runat="server" CssClass="btn btn-primary" Text="Consultar"><span class="fa fa-search"></span>&nbsp;Consultar</asp:LinkButton>                                                
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                        <div class="col-md-12 col-sm-12 col-lg-12">
                                            <div class="col-md-6" style="float:left;width:50%;">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label ">
                                                    Semestre acad&eacute;mico</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlCiclo" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            </div>
                                            
                                            </div>
                                       </div>
                                     
                                                              
                                  
                                    </div>
                                </div>
                       
                        </div>
                    </div>                   
                </div>
                <div class="row" id="divFila"></div>
                
                <div class="row">                    
                    <div class="panel-piluku">
                        <div class="panel-heading" style="background-color: #E33439; color:White;">
                            <h3 class="panel-title">
                                Disponibilidad
                            </h3>
                        </div>
                        <div class="panel-body">
                           <div class="row" id="MensajeForm" runat="server">
                           
                           </div>
                           
                            <div class="table-responsive">
                                <asp:GridView ID="gData" runat="server" AutoGenerateColumns="False" 
            CellPadding="4"  BorderStyle="None" 
             AlternatingRowStyle-BackColor="#F7F6F4"  DataKeyNames="codigo_Per,Descripcion_Ded,Personal,departamento,codigo_cac,cicloacademico" CssClass="display" Font-Size=12px Width="100%">
            <RowStyle BorderColor="#C2CFF1" />
            <Columns>          
                <asp:BoundField DataField="Descripcion_Ded" HeaderText="Descripcion" />
                <asp:BoundField DataField="departamento" HeaderText="Departamento" >
                </asp:BoundField>
               <asp:BoundField DataField="Personal" HeaderText="Docente" >
                </asp:BoundField>
                  <asp:BoundField DataField="cicloacademico" HeaderText="Semestre" >
                </asp:BoundField>
                  <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>                                                         
                            <asp:LinkButton ID="BtnDetalle" CssClass="btn btn-success" runat="server" Text="Ver" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" OnClientClick=" fnDivLoad('report', 7000);"  ToolTip="Registro de Disponibilidad"  CommandName="Registrar"><span class="ion-android-add-circle"></span></asp:LinkButton>                            
                        </ItemTemplate>
                        <HeaderStyle />                     
                    </asp:TemplateField>
            </Columns>
              <FooterStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
           <EmptyDataTemplate>
                 <div style="color:#3266DB; background-color:#E8EEF7; padding:5px; font-style:italic;">
                     No se encontraron registros.
                 </div>
             </EmptyDataTemplate>
             <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#3366CC" />
             <AlternatingRowStyle BackColor="#F7F6F4" />
        </asp:GridView>
                            </div>
                               
                        </div>
                    </div>                    
                </div>
 
 </div>
 <div id="PanelDisponibildiad" runat="server">
     <div id="PanelDisponibildiadLista" runat="server">
            <!--------------------------------------------->
              <div class="row">                   
                    <div class="manage_buttons">
                        <div class="row">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="page_header">
                                        <div class="pull-left" id="TituloForm2">
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Disponibilidad de horario docente</span>                                                                                        
                                        </div>                                        
                                      
                                    </div>
                                </div>
                       
                        </div>
                        <div class="row">
                                <div class="col-md-6 col-sm-6 col-lg-6" style="float:left">
                                <div id="divDocente">
                                <table style="width:80%">
                                <tr>
                                <td style="width:50%;">
                                <ul>
                                <li id="lidocente" runat="server">Docente: </li>                                
                                <li id="lidepartamento" runat="server">Departamento: </li>
                                </ul>
                                </td>
                                <td style="width:50%;">
                                <ul>
                                <li id="lisemestre" runat="server">Semestre: </li>
                                <li id="lidescripcion" runat="server">Descripcion: </li>
                                </ul>
                                </td>
                                </tr>
                                </table>
                                
                                </div>                                
                                </div>
                                <div class="col-md-6 col-sm-6 col-lg-6"  style="float:right">
                                  <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-red" Text="Regresar" Width="100px"><span class="ion-android-arrow-back"></span>&nbsp;Regresar</asp:LinkButton>
                                                <asp:LinkButton ID="btnConsultar2" runat="server" CssClass="btn btn-primary" Text="Consultar" Width="100px"><span class="fa fa-search"></span>&nbsp;Consultar</asp:LinkButton>
                                                <asp:LinkButton ID="btnAgregar" runat="server" CssClass="btn btn-success" Text="Agregar" Width="100px"><span class="ion-android-add"></span>&nbsp;Agregar</asp:LinkButton>   
                                                
                                            </div>
                                        </div>
                                </div>
                                
                        </div>
                    </div>                   
                </div>
                <div class="row" id="divFila2"></div>
            
           <!---------------------------------------------> 
     </div>
     <div id="PanelDisponibildiadRegistro" runat="server">
      <div class="row">                    
                    <div class="panel-piluku">
                        <div class="panel-heading" style="background-color: #E33439; color:White;">
                            <h3 class="panel-title">
                                Disponibilidad
                            </h3>
                        </div>
                        <div class="panel-body">
                           
                            <div class="table-responsive">
     <table style="width:100%">
         <tr valign="top">
    <td style="width:70%" valign="top">
    
    <div id="Lista" runat="server" >

    <asp:GridView ID="gDetalle" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="codigo_rper,codigo_per,codigo_cac,puede,dia,horaInicio,horaFin"  BorderStyle="None" 
                 AlternatingRowStyle-BackColor="#F7F6F4" Width="100%" 
            Font-Size="Small" >
                    <Columns> 
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="BtnDetalleEditar" CssClass="btn btn-warning btn-xs" runat="server" Text="Editar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" OnClientClick=" fnDivLoad('report', 7000);"  ToolTip="Editar de Disponibilidad"  CommandName="Editar"><span class="ion-android-create"></span></asp:LinkButton>
                            <asp:LinkButton ID="BtnDetalleEliminar" CssClass="btn btn-danger btn-xs" runat="server" Text="Eliminar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" OnClientClick=" fnDivLoad('report', 7000);"  ToolTip="Elimnar de Disponibilidad"  CommandName="Eliminar"><span class="ion-android-cancel"></span></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle  />
                    </asp:TemplateField>
                        <asp:BoundField DataField="nompuede" HeaderText="PUEDE" >
                            <HeaderStyle Font-Bold="True" Font-Size="Small" />
                            <ItemStyle Width="23%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomdia" HeaderText="DIA" >
                            <HeaderStyle Font-Bold="True" Font-Size="Small" />
                            <ItemStyle Width="23%" HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="horaInicio" HeaderText="HORA INICIO" 
                          >
                            <ItemStyle Width="23%" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="horaFin" HeaderText="HORA FIN" 
                            >
                        <ItemStyle Width="23%" HorizontalAlign="Right" />
                        </asp:BoundField>           
                       </Columns>
<HeaderStyle BackColor="#e33439" ForeColor="#FFFFFF" Height="22px" />
                        <RowStyle Height="22px" />
    <AlternatingRowStyle BackColor="#F7F6F4"></AlternatingRowStyle>
    <EmptyDataTemplate>
                 <div style="color:#3266DB; background-color:#E8EEF7; padding:5px; font-style:italic;">
                     No se encontraron registros.
                 </div>
             </EmptyDataTemplate>
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
          <%--       <tr>
                 <td style="width:50%">Puede</td>
                 <td>
                 <asp:DropDownList ID="ddpPuede" runat="server" CssClass="form-control">                 
                 <asp:ListItem Value="S" Selected >SI PUEDE</asp:ListItem>                 
                 </asp:DropDownList>
                                                
                 </td>
                 </tr>--%>
                 <tr>
                 <td>Dia</td>
                 <td>
                 <asp:DropDownList ID="ddpDia" runat="server" CssClass="form-control">
                 <asp:ListItem Value="" Selected>---[SELECIONE]---</asp:ListItem>
                 <asp:ListItem Value="LU" >LUNES</asp:ListItem>
                 <asp:ListItem Value="MA" >MARTES</asp:ListItem>
                 <asp:ListItem Value="MI" >MIERCOLES</asp:ListItem>
                 <asp:ListItem Value="JU" >JUEVES</asp:ListItem>
                 <asp:ListItem Value="VI" >VIERNES</asp:ListItem>
                 <asp:ListItem Value="SA" >SABADO</asp:ListItem>
                 </asp:DropDownList>
                 </tr>
                 <tr>
                 <td>Hora Inicio</td>
                 <td>
                 <asp:DropDownList ID="ddpHoraI" runat="server" CssClass="form-control">
                 <asp:ListItem Value="" Selected>---[SELECIONE]---</asp:ListItem>
                 </asp:DropDownList>                
                 </tr>
                 <tr>
                 <td>Hora Fin</td>
                 <td>
                 <asp:DropDownList ID="ddpHoraF" runat="server" CssClass="form-control">
                 <asp:ListItem Value="" Selected>---[SELECIONE]---</asp:ListItem>
                 </asp:DropDownList>
                 </tr>
                 <tr>
                 <td colspan="2" style="text-align:center">
                 
                    <%--<asp:Button ID="btnGrabar" runat="server"  Text="Grabar" CssClass="guardar2" Width="100px" Height="20px" />--%>
                    <asp:LinkButton ID="btnGrabarDetalle" runat="server" CssClass="btn btn-green" Text="Regresar" Width="100px"><span class="ion-android-done"></span>&nbsp;Guardar</asp:LinkButton>
                    <asp:LinkButton ID="btnCancelarDetalle" runat="server" CssClass="btn btn-red" Text="Regresar" Width="100px"><span class="ion-android-cancel"></span>&nbsp;Cancelar</asp:LinkButton>
                 </td>
                 </tr>
                 </table>
                 </div>
    </td>
    </tr>
    </table> 
    </div>
    </div>
    </div>
    </div>
     </div>
 </div> 
 </div> 
 </form>              
            </div>
        </div>
    </div>
</body>
</html>
