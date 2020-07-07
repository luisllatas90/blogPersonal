<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmasignarcargaacademica.aspx.vb" Inherits="frmasignarcargaacademica" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
<meta charset="utf-8">
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

<script type="text/javascript" src="../../assets/js/jquery.dataTables.min.js?a=1"></script>	
<link rel='stylesheet' href='../../assets/css/jquery.dataTables.min.css?a=1'/>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css?a=m">
<script type="text/javascript" language="JavaScript" src="../../../private/funciones.js?b=m"></script>
    <title>Asignación de Carga Académica</title>    
        <style type="text/css">
            .ie2{
                float:left; 
                width:50%;
                }
            .ie3{
                float:left; 
                width:33%;
                }  
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
        .derecha
        {
            float:right;
            }
            
    </style>
    
    
     <style type="text/css">
         
         
         
         
      </style>   
        <script type="text/javascript" language="javascript">
         var event = jQuery.Event("DefaultPrevented");
             $(document).trigger(event);
             $(document).ready(function() {

                 var oTable = $('#grwGruposProgramados').DataTable({                     
                     "bPaginate": false,
                     "bFilter": true,
                     "bLengthChange": false,
                     "bSort": false,
                     "bInfo": true,
                     "bAutoWidth": true
                 });

                 var oTable2 = $('#gDataHorarioCurso').DataTable({                     
                     "bPaginate": false,
                     "bFilter": false,
                     "bLengthChange": false,
                     "bSort": false,
                     "bInfo": false,
                     "bAutoWidth": true
                 });

                 var oTable3 = $('#dgvCruceHorarioDocente').DataTable({                   
                     "bPaginate": false,
                     "bFilter": false,
                     "bLengthChange": false,
                     "bSort": false,
                     "bInfo": false,
                     "bAutoWidth": true
                 });

                 var oTable4 = $('#dgvCruceHorarioDisponibleDocente').DataTable({                     
                     "bPaginate": false,
                     "bFilter": false,
                     "bLengthChange": false,
                     "bSort": false,
                     "bInfo": false,
                     "bAutoWidth": true
                 });
                 
                 var oTable5 = $('#gDataHorarioDisponible').DataTable({                    
                     "bPaginate": false,
                     "bFilter": false,
                     "bLengthChange": false,
                     "bSort": false,
                     "bInfo": false,
                     "bAutoWidth": true
                 });


                 fnEstilo();


             });

             function fnEstilo() {
                 var v = msieversion();
                 if (v > 0) {
                     $("#div1").removeClass("ie2").addClass("ie2");
                     $("#div2").removeClass("ie2").addClass("ie2");
                     $("#div3").removeClass("ie2").addClass("ie2");
                     $("#div4").removeClass("ie2").addClass("ie2");

                 } else {
                     $("#div1").removeClass("ie2");
                     $("#div2").removeClass("ie2");
                     $("#div3").removeClass("ie2");
                     $("#div4").removeClass("ie2");

                 }
             }
        
       
        function msieversion() {

            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE");
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
        
        function PintarFilaMarcada(obj,estado)
        {
            if (estado==true){
                obj.style.backgroundColor="#EBEBEB"//#395ACC
            }
            else{
                obj.style.backgroundColor="white"
            }
        }
        
        function MostrarCaja(tbl)
        {
            if (document.getElementById(tbl).style.display=="none"){
                document.getElementById(tbl).style.display=""
            }
            else{
                document.getElementById(tbl).style.display="none"
            }
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
            return true;
        }

        function fnConsultarHorarioDocente(cod,docente,dedicacion) {
           // alert(cod + '  ' + docente + '  ' + dedicacion);
            if(fnDivLoad('report',2000)){
                if (fnHorarioDocente(cod, docente, dedicacion)) {
                    //alert(1);
                    PintarCeldasHorarioDocenteCA();
                } else {
                    //alert(2);
                    PintarCeldasHorarioDocenteCA();
                }
             }
            
        }
        function fnHorarioDocente(cod, docente, dedicacion) {
            var rpta = false;
            var cac =<%=dpCodigo_cac.SelectedValue %>
            
            var titulo = '';
           
            
            try {                
                    $.get("../horarios/vsthorariodocente.asp",
                    { "codigo_per": cod, "codigo_cac": cac, "titulo": titulo, 'vista': "1", "modo": "D", "docente": docente, "dedicacion": dedicacion,"descripcion_cac":'' }
                    , function(data, status) {
                        if (status == 'success') {
                           
                            // $("#divContent").html(data);
                            $("#divHorarioDocente").empty();
                            $("#divHorarioDocente").html(data);

                        } else {
                        $("#divHorarioDocente").empty();
                            
                        }
                   
                    });
                
                
            
//                //var URLactual = window.location;
//        
//                var cac = $('#dpCodigo_cac').val();
//                var titulo = '';
//                $.ajax({
//                    type: "GET",
//                    url: "../horarios/vsthorariodocente.asp",
//                    data: { "codigo_per": cod, "codigo_cac": cac, "titulo": titulo, 'vista': "1", "modo": "D", "docente": docente, "dedicacion": dedicacion },
//                    // async: false,
//                    success: function(data) {
//                        console.log(data);
//                        alert(data);
//                        // $('#mdHorarioDocente').modal('show');
//                        $('div#divHorarioDocente').html(data);
//                        //rpta = true;
//                        return true;
//                    },
//                    error: function(result) {
//                        alert(result);
//                        console.log(result);
//                        //rpta = false;
//                        return false;
//                    }
//                });
//                return true;
            }
            catch (err) {
                alert(err.message);
                console.log(err.message);
                
                return false;
            }
        }
        
    
        function openModal(opc) {
            if (opc == 1) {
                $('#mdCruceHorario').modal('show');
            } else if (opc == 2) {
                $('#mdCruceDisponibilidad').modal('show');
            } else if (opc == 3) {
            $('#mdTotalHorasDia').modal('show');
            } 
        }

        function closeModal(opc) {
            if (opc == 1) {
                $('#mdCruceHorario').modal('hide');
            } else if (opc == 2) {
                $('#mdCruceDisponibilidad').modal('hide');
            } else if (opc == 3) {
            $('#mdTotalHorasDia').modal('hide');
            }
        }


        //Funcion utilizada para pintar las celdas de la tabla de la vista del horario por docente.
        function PintarCeldasHorarioDocenteCA() {
           
            ocultar = "N";
            var i = 0
            var ArrFilas = document.all.tblHorario.getElementsByTagName('tr')
            var total = 0
            for (var f = 0; f < ArrFilas.length; f++) {
                /*Excluir a cabezeras*/
                if (ArrFilas[f].className == "") {
                    var ArrCeldas = ArrFilas[f].cells
                    i = 0

                    for (var c = 0; c < ArrCeldas.length; c++) {
                        var Celda = ArrCeldas[c]
                        if (Celda.className == "" && Celda.innerText != "") {

                            //**********************************************************************************************************************
                            //dguevara 12.09.2013
                            //Este bloque va a sombrear los horarios que tienen (*)
                            //Los cuales indican que estan registrados en su horario personal, el (*) es actualizado en el procedimiento.
                            var hor = Celda.innerText;
                            if (hor.indexOf('*') != -1) {
                                Celda.className = "CU"
                            } else {
                                Celda.className = "DE";
                            }
                            //**********************************************************************************************************************

                            i = i + 1
                            total = total + 1
                        }
                    }
                    /*Oculta filas vacías*/

                    if (i == 0 && ocultar == "S") {
                        ArrFilas[f].style.display = "none"
                    }

                }
            }
            if (document.all.totalhrs != undefined) {
                totalhrs.innerHTML = total + " horas ocupadas"
            }
        }
	

        
    </script>
    
    <style type="text/css">
    /* .... */
    .FondoAplicacion
    {
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
        </style>
    </head>
<body class="">
    <form id="form1" runat="server">
    
    <div id="loading" class="piluku-preloader text-center" runat="server">
         <!--<div class="progress">
            <div class="indeterminate"></div>
            </div>-->
         <div class="loader">Loading...</div>
      </div>
<div class="wrapper">
<div class="content">
<div class="main-content">
<div  id="report">
<div id="PanelLista" runat="server">
                <div class="row">                   
                        <div class="manage_buttons">
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="page_header">
                                        <div class="pull-left" id="TituloForm">
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Asignación de Carga Académica según Programación</span><br />
                                            
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                               <%-- <asp:LinkButton ID="btnConsultar" runat="server" CssClass="btn btn-primary" Text="Consultar"><span class="fa fa-search"></span>&nbsp;Consultar</asp:LinkButton>                                                --%>
                                                <asp:LinkButton  ID="cmdBuscar" runat="server" CssClass="btn btn-primary" Text="Buscar"><span class="fa fa-search" ></span>&nbsp;Buscar</asp:LinkButton>
                                                <asp:LinkButton ID="btnExportar" runat="server" CssClass="btn btn-green" Text="Exportar" Visible="false"><span class="fa fa-file-excel-o"></span>&nbsp;Exportar</asp:LinkButton>
                                            </div>
                                        </div>                                        
                                        <div class="row">
                                        <div class="col-md-12 col-sm-12 col-lg-12">
                                       
                                        </div>
                                        </div>
                                        <div class="col-md-12 col-sm-12 col-lg-12">
                                            <div class="col-md-4 col-sm-4 col-lg-4" style="float:left;">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label ">
                                                    Semestre acad&eacute;mico</label>
                                                    <div class="col-md-8 col-sm-8 col-lg-8">
                                                        <asp:DropDownList ID="dpCodigo_cac" CssClass="form-control" runat="server" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        
                                                        
                                                        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true"  runat="server"></asp:ScriptManager>
                                                </div>
                                            </div>
                                            </div>
                                            <div class="col-md-4" style="float:left;">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label ">
                                                    Carrera Profesional
                                                </label>
                                                <div class="col-md-8">
                                                     <asp:DropDownList ID="dpCodigo_cpf" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                </div>
                                            </div>
                                            </div>
                                             <div class="col-md-4" style="float:left;">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label ">
                                                    Estado
                                                </label>
                                                <div class="col-md-8">
                                                       <asp:DropDownList ID="dpEstado" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">Cursos SIN Docente asignado</asp:ListItem>
                                                    <asp:ListItem Value="1">Cursos CON Docente asignado</asp:ListItem>
                                                </asp:DropDownList>
                                                </div>
                                            </div>
                                            </div>
                                            </div>
                                            
                                           <div class="row">
                                           
                                             <div class="col-md-12 col-sm-12 col-lg-12" style="float:left;">
                                             <hr />
                                                     <ul><li><asp:Label ID="lblnombre_dac" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" 
                                            Text="Ud. no está registrado como Director de Dpto. Académico. Consultar con la Of. de Personal."></asp:Label>
                                            </li></ul>
                                            </div>
                                            </div>
                                       </div>
                                       <div class="row" id="divFila"></div>
                                        <div class="row">                    
                                            <div class="panel-piluku">
                                                <div class="panel-heading" style="background-color: #E33439; color:White;">
                                                    <h3 class="panel-title">
                                                        CARGA ACAD&Eacute;MICA
                                                    </h3>
                                                </div>
                                                <div class="panel-body">
                                                    <asp:HiddenField ID="hdcodigo_dac" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hdcodigo_cac" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hdAgregar" runat="server" Value="false" />
                                                    <asp:HiddenField ID="hdModificar" runat="server" Value="false" />
                                                    <asp:HiddenField ID="hdEliminar" runat="server" Value="false" />
                                                    <asp:HiddenField ID="hdCodigo_Cup" runat="server" Value="0" />
                                                    <div class="table-responsive">
                                                    
                                                    <asp:GridView ID="grwGruposProgramados" runat="server" 
                                                        AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="Solid" 
                                                        CellPadding="2" DataKeyNames="codigo_cup,nombre_Cpf,descripcion_Cac" CssClass="display" Font-Size=12px Width="100%">
                                                        <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="#" />
                                                            <asp:TemplateField HeaderText="Asignatura">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblnombre_cur" runat="server" Text='<%# Bind("nombre_cur") %>'></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblidentificador_cur" runat="server" Font-Italic="True" 
                                                                        Text='<%# Bind("identificador_cur") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo">
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Escuela">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEscuela" runat="server" ForeColor="#0066FF" 
                                                                        Text='<%# Bind("abreviatura_cpf") %>'></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblPlan" runat="server" Font-Italic="True" ForeColor="#006666" 
                                                                        Text='<%# Bind("abreviatura_pes") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20%" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo">
                                                                <ItemStyle Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="creditos_cur" HeaderText="Crd">
                                                                <ItemStyle Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="totalhoras_cur" HeaderText="TH">
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="estado_cup" HeaderText="Estado">
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="vacantes_cup" HeaderText="Vacantes">
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Profesor" ItemStyle-VerticalAlign="Top">
                                                            <ItemTemplate>
                                                                <asp:BulletedList ID="lstProfesores" runat="server" DataTextField="docente" 
                                                                    DataValueField="codigo_per" Font-Size="7pt">
                                                                </asp:BulletedList>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top"></ItemStyle>
                                                        </asp:TemplateField>
                                                            <asp:CommandField  ButtonType="Image" SelectImageUrl="../../../images/menu0.gif" 
                                                                SelectText="" ShowSelectButton="True" HeaderText=""  >
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="codigo_cup" ReadOnly="True" Visible="False">
                                                            <ItemStyle Font-Size="Smaller" ForeColor="White" Width="0px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" 
                                                            CssClass="usatsugerencia" Font-Bold="True" ForeColor="Red" />
                                                        <EmptyDataTemplate>
                                                            &nbsp;&nbsp;&nbsp;&nbsp; No se encontraron grupos horario registrados según los criterios 
                                                            seleccionados
                                                        </EmptyDataTemplate>
                                                        <HeaderStyle BackColor="#E8EEF7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                            BorderWidth="1px" ForeColor="#3366CC" />
                                                        <SelectedRowStyle BackColor="#6699FF" ForeColor="White" />
                                                    </asp:GridView>
                                                    
                                                    </div>
                                                    <div class="row">
                                                       

 <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Red" Text="&lt;img src=&quot;../../../images/bloquear.gif&quot;&gt;&amp;nbsp;El Acceso para Agregar/Modificar la Carga Académica ha finalizado."></asp:Label>
&nbsp;<asp:LinkButton ID="lnkSolicitar" runat="server" Font-Underline="True" 
        ForeColor="Blue" onclientclick="MostrarCaja('trPermiso');return(false)" 
        Visible="False" Font-Size="10pt">Haga clic aquí para [Solicitar Acceso]</asp:LinkButton>
&nbsp;<table cellpadding="3" cellspacing="0" style="border: 1px solid #808080; width:60%; border-collapse: collapse; background-color: #E5E5E5;display:none" ID="trPermiso">
            <tr>
                <td valign="top">
                    Indique el motivo por el cual solicita el Acceso.<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtmotivo" 
                        ErrorMessage="Debe ingresar un motivo para Cambio de Carga Académica" 
                        SetFocusOnError="True" ValidationGroup="MotivoCambio">*</asp:RequiredFieldValidator><br />
                    <asp:TextBox ID="txtmotivo" runat="server" CssClass="cajas" MaxLength="255" 
                        Rows="3" TextMode="MultiLine" Width="98%"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="cmdEnviar" runat="server" CssClass="guardar2" 
                        Text="    Enviar" ValidationGroup="MotivoCambio" />&nbsp;El acceso lo habilitará ViceRectorado Académico por un lapso de 24 horas.</td>
            </tr>
        </table>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        BorderStyle="Solid" BorderWidth="1px" />
    <br />
                                                <asp:Button ID="cmdPopUp" runat="server" Text="Abrir" style="display:none" />

            <br />
    
                                   
    <cc1:ModalPopupExtender ID="mpeFicha" runat="server"
        CancelControlID="cmdCancelar"
        PopupControlID="fraProfesores"
        TargetControlID="cmdPopUp"  BackgroundCssClass="FondoAplicacion" Y="50" />
       

                                                    </div>
                                                </div>
                                             </div>
                                        </div>
                                     
                                                        
                                  
                                    </div>
                            </div>
                          
                        </div>
                    </div>                   
                </div>

<div id="PanelRegistro" runat="server">
 <div class="panel-piluku">
<div class="panel-body">
<asp:Panel ID="fraProfesores" runat="server">

        <div class="panel-piluku">
                        <div class="panel-heading" style="background-color: #E33439; color:White;">
                            <h1 class="panel-title"><br />CARGA ACAD&Eacute;MICA<span class="panel-options">
						    <asp:Button ID="cmdCancelar" runat="server" Text="X" CssClass="btn-sm" BackColor="white" ForeColor="#E33439" Font-Bold="true"  ValidationGroup="Ninguna" />
					        </span>
					        </h1>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                      <div class="col-md-6 col-sm-6 col-lg-6" id="div1">
                                       <div class="info-five" style="padding-top: 0px;padding-bottom: 0px;background-color:gray">
					                        <div class="logo" style="padding-bottom: 10px;"><i class="ion-android-folder"></i></div>
					                         <asp:Label ID="lblcurso" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>-<asp:Label ID="lblgrupo" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label><br />
					                          <asp:Label ID="lblcpf" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label><br />
					                          <asp:Label ID="lblcac" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>
					                          <p><i class="ion-android-person"></i> <asp:Label ID="lblGrupoProfesores" runat="server" Font-Bold="True" Visible="false"></asp:Label>
					                          <asp:Label ID="lblCarga" runat="server" Text="" Font-Bold=true Font-Size=Medium></asp:Label><br />
                                            <asp:Label ID="lblCargaReg" runat="server" Text="" Font-Bold="true" Font-Size=Medium></asp:Label><br />                     
                                            <asp:HiddenField ID="hdNumCarga" runat="server" />
                                            <asp:HiddenField ID="hdSelHorario" runat="server" />
					                        </p>
				                        </div>
                                      </div>
                                      <div class="col-md-6 col-sm-6 col-lg-6" id="div2">
                                       <div class="form-group">
                                        <fieldset>
                                        <legend><asp:Label ID="Label2" runat="server" Font-Bold="True"  Text="Horario del Curso:"></asp:Label></legend>
                                          <asp:GridView ID="gDataHorarioCurso" runat="server" AutoGenerateColumns="False" class="display" Width="98%" DataKeyNames="codigo_Hdo,codigo_Lho" CellPadding="0"  BorderStyle="None" >
                                                  <Columns>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" >                           
                                                        <ItemTemplate >
                                                            <asp:CheckBox ID="chkElegir" runat="server" AutoPostBack="true" OnCheckedChanged="chkview_CheckedChanged" />
                                                            
                                                        </ItemTemplate>
                                                        
                                                    <ItemStyle HorizontalAlign="Center"  />
                                                    
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="HORAS" DataField="Horas" />
                                                    <asp:BoundField HeaderText="TIPO" DataField="tipoHoraCur_Lho" />
                                                  </Columns>
                                                <FooterStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                               <EmptyDataTemplate>
                                                 <div style="color:#3266DB; background-color:#E8EEF7; padding:5px; font-style:italic;">
                                                     No se encontraron registros.
                                                 </div>
                                                </EmptyDataTemplate>
                                <%-- <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#3366CC" />--%>
                                 <%--<AlternatingRowStyle BackColor="#F7F6F4" />--%>
                                            </asp:GridView><br />
                                         <div style="float:right;">
                                         <asp:HiddenField ID="hdaCruceCurso" runat="server" value=""/>
                                         <asp:LinkButton ID="aCruceCurso" runat="server" CssClass="btn btn-warning" Text="Ver Cruce" ForeColor="White"> <span class="ion-alert-circled" style="color:White"></span>&nbsp;Ver Cruce</asp:LinkButton>
                                         </div>
                                        </fieldset>          
                                        
                             
                              
                                       </div>
                                      </div>                                       
                                </div>
                            </div>
                             <div class="row">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                       <div class="col-md-6 col-sm-6 col-lg-6" id="div3">
                                        <div class="form-group">
                                            <fieldset>
                                            <legend>Asignar Docente</legend>
                                            <asp:Button ID="cmdAgregar" runat="server" CssClass="agregar2" 
                                                onclientclick="MostrarCaja('trNuevo');return(false)" 
                                                Text="      Agregar un profesor..." UseSubmitBehavior="False" 
                                                Width="130px" Visible="False" />
                                           <asp:DropDownList ID="dpCodigo_per" runat="server" Visible="false"  
                                                Width="350px" AutoPostBack="True" >
                                            </asp:DropDownList>                
                                            <asp:ListBox ID="dpCodigo_per2" runat="server"  Width="100%"  Height="150px" 
                                                AutoPostBack="True" CssClass="form-control" Font-Size=Small>
                                            </asp:ListBox>
                                            <br />
                                            <asp:LinkButton ID="cmdGuardar" runat="server" CssClass="btn btn-success" Width="100%" Text="Guardar" ForeColor="White"><span class="ion-android-done" style="color:White"></span>&nbsp;Guardar</asp:LinkButton>
                                            
                                            
                                            </fieldset>
                                        </div>
                                       </div>
                            
                                       <div class="col-md-6 col-sm-6 col-lg-6" id="div4">
                                        <div class="form-group">
                                            <fieldset>
                                            <legend><asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Horario disponible del docente:"></asp:Label></legend>
                                             <asp:GridView ID="gDataHorarioDisponible" runat="server" 
                                       AutoGenerateColumns="False" 
                                        BorderStyle="None" CellPadding="0" class="display" 
                                        Width="98%">
                                        <Columns>
                                            <asp:BoundField DataField="nomdia" HeaderText="Dia" />
                                            <asp:BoundField DataField="horas" HeaderText="Hora" />
                                        </Columns>
                                            <FooterStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                           <EmptyDataTemplate>
                                                 <div style="color:White; background-color:Red; padding:5px; font-style:italic;">
                                                     El docente no ha registrado disponibilidad de horarios.
                                                 </div>
                                             </EmptyDataTemplate>           
                                        </asp:GridView> <br />
                                        <label id="lblVerificaDia" runat="server" style="color:#2196f3;font-weight:bold;font-size:medium;"></label>
                                        <br />
                                                <div style="float:right;">
                                                 <asp:HiddenField ID="hdaCruceHorarioDisponible" runat="server" value=""/>
                                                <asp:LinkButton ID="aCruceHorarioDisponible" runat="server" CssClass="btn btn-primary"  Text="Disponibilidad" ForeColor="White"> <span class="ion-android-person" style="color:White"></span>&nbsp;Disponibilidad</asp:LinkButton>                                  
                                                </div>
                                            </fieldset>
                                        </div>
                                       </div>
                                </div>
                            </div>
                             <div class="row">
                                <div class="col-md-12 col-sm-12 col-lg-12" id="div5">
                                 <div class="form-group">
                                    <div class="col-md-12 col-sm-12 col-lg-12">
                                            <fieldset>
                                            <legend>Docentes Asignados</legend>
                                            <asp:DataList ID="dlProfesores" runat="server" DataKeyField="codigo_per" 
                                                RepeatColumns="4" RepeatDirection="Horizontal" Width="98%" 
                                                GridLines="Vertical">
                                                <ItemTemplate>
                                                <div class="row">
                                                <div class="panel-piluku" id="div6">
                                                    <div class="panel-piluku">
                                                    <div class="panel-heading" style="background-color: #E33439; color:White;">
                                                        <h3 class="panel-title" style="text-align:center">
                                                         <asp:Label ID="lblprofesor" runat="server" Font-Bold="" ForeColor="" 
                                                                        Text='<%# eval("docente") %>'></asp:Label>                                                                
                                                                    <asp:HiddenField ID="hdCodigo_per" runat="server" 
                                                                        Value='<%# eval("codigo_per") %>' />
					                                    </h3>
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="col-md-12 col-sm-12 col-lg-12" >
                                                           <div class="form-group"> 
                                                                <div class="col-md-4 col-sm-4 col-lg-4">         
                                                                <asp:Image ID="FotoProfesor" runat="server" Height="114px" 
                                                                        ImageUrl='<%# "../" & eval("foto_per") %>' Width="90px" />
                                                                      
                                                                </div>
                                                                <div class="col-md-8 col-sm-8 col-lg-8" >         
                                                               <table style="width=100%">
                                                                   <tr>
                                                                   <td style="text-align:left">
                                                                   <asp:Label ID="lblDedicacion" runat="server" 
                                                                        Text='<%# eval("descripcion_ded") %>'></asp:Label>
                                                                   </td>
                                                                   </tr>
                                                                   <tr>
                                                                   <td style="text-align:left">
                                                                   <asp:Label ID="lblTipo" runat="server"  Text='<%#  eval("descripcion_fun") %>'></asp:Label><br />
                                                               
                                                                 <asp:Label ID="lblAsesoria" runat="server" 
                                                                        Text='<%# eval("horas") %>'></asp:Label>
                                                                   </td>
                                                                   </tr>
                                                                   <tr>
                                                                   <td style="text-align:left">
                                                                     <asp:Label ID="lblHoras" runat="server"   Text='<%# eval("total") %>'></asp:Label>
                                                                   </td>
                                                                   </tr>
                                                                   <tr>
                                                                   <td style="text-align:left">
                                                                   Horarios del curso seleccionado: <br />
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# eval("horario") %>'></asp:Label><br />
                                                                   </td>
                                                                   </tr>
                                                                   <tr>
                                                                   <td style="text-align:left">
                                                                    Total de Horas asignadas <%#Me.dpCodigo_cac.SelectedItem.Text%>: <%#Eval("sumaTH")%>
                                                                    <br />
                                                                   </td>
                                                                   </tr>                                                                   
                                                                   <tr>
                                                                   <td style="text-align:left; float:left;">                                                                    
                                                                   <a href="#" class="btn btn-sm" data-toggle="modal" data-target="#mdHorarioDocente" onclick="fnConsultarHorarioDocente(<%# eval("codigo_per") %>,'<%# eval("docente") %>','<%# eval("descripcion_ded") %>');" style="float:left;padding-left:0px;color:#2196f3"> <i class="ion-android-person"></i> <i class='ion-android-calendar'></i> Ver Horario Docente</a>
                                                                    
                                                                   <%-- <button type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#mdHorarioDocente" onclick="fnConsultarHorarioDocente(<%# eval("codigo_per") %>,'<%# eval("docente") %>','<%# eval("descripcion_ded") %>');">
                                                                       <i class="ion-android-person"></i> <i class='ion-android-calendar'></i> Ver Horario Docente
                                                                    </button>--%>
                                                                   </td>
                                                                   </tr>
                                                               </table>    
                                                             </div>
                                                        
                                                            </div>
                                                        </div>  
                                                        <div class="col-md-12 col-sm-12 col-lg-12"  >
                                                          <center>
                                                         <asp:Button ID="cmdQuitar" runat="server" 
                                                           CausesValidation="False"  Font-Bold="true"
                                                            CommandName="delete" Text="[X] Quitar"  CssClass="btn btn-danger btn-sm" Width />
                                        
                                                        <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="True" 
                                                            ForeColor="Blue" Visible="False">Más detalles...</asp:HyperLink>
                                                        </center>
                                                        </div>                                                     
                                                    </div>
                                                    </div>
                                                </div>
                                                </div>
                       
                        </ItemTemplate>
                    </asp:DataList>
                                            </fieldset>
                                            </div>
                                            
                                </div>                                 
                            </div>                                 
                             </div>
                              <div class="row">
                                <div class="col-md-12 col-sm-12 col-lg-12" id="div6">
                                 <div class="form-group">
                                    <div class="col-md-12 col-sm-12 col-lg-12">
                                    <fieldset>
                                    <legend>Profesores sugeridos para la asignatura</legend>
                                    <div class="row">
                                      <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="(Clic sobre el nombre del profesor para seleccionar)"></asp:Label>
                                    </div>
                                    <div class="row">
                                     <asp:BulletedList ID="blstProfesoresSugeridos" runat="server"  
                                        BulletStyle="Numbered" DisplayMode="LinkButton" BorderStyle="None">
                                    </asp:BulletedList>
                                    </div>
                                    </fieldset>
                                    </div>
                                  </div>
                              </div>
                        </div>
        </div>
        
<div class="modal fade" id="mdTotalHorasDia" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false" >
        <div class="modal-dialog" style="margin-top:150px;">
			<div class="modal-content">
				<div class="modal-header" style="background-color:#2196f3;" >
					<a href="#" class="btn btn-white close derecha"  data-dismiss="modal" style="background-color:White; font-weight:bold; border: 2px solid"><i class="ti-close"></i></a>
					<h4 class="modal-title"  style="color:White;">TOTAL DE HORAS ASIGNADAS POR DIA HASTA 
                        EL MOMENTO</h4>
				</div>
				<div class="modal-body">
				    <div class="row">
    				    <div class="col-md-12">                      
                            <div id="divTotalHorasDia" runat="server">
                            </div>
    				    </div>
				    </div>
				</div>
		    </div>
		</div>
	    </div>	 
	      <div class="modal fade modal-full-pad" id="mdHorarioDocente" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false" >
        <div class="modal-dialog modal-full" style="margin-top:150px;">
			<div class="modal-content">
				<div class="modal-header" style="background-color:#000066;" >
					<a href="#" class="btn btn-white close derecha"  data-dismiss="modal" style="background-color:White; font-weight:bold; border: 2px solid"><i class="ti-close"></i></a>
					<h4 class="modal-title"  style="color:White;">Horario de Docente</h4>
				</div>
				<div class="modal-body">
				    <div class="row">
    				    <div class="col-md-12">                      
                            <div id="divHorarioDocente">
                            </div>
    				    </div>
				    </div>
				</div>
		    </div>
		</div>
	    </div>	 
	   
    </asp:Panel>
</div>
</div>
</div>                
   
                </div>
</div>
</div>

</div>

<div class="modal fade modal-full-pad" id="mdCruceHorario" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false" >
		<div class="modal-dialog modal-full" style="margin-top:150px;">
			<div class="modal-content">
				<div class="modal-header" style="background-color:#f0ad4e;" >
				
				<%--	<button type="button" id="btnCerrarCruceHorario"   CommandName="Sort"
           CommandArgument="Ascending"
           OnCommand="CerrarCruceHorario_Click"  class="btn btn-white"  runat="server"  data-dismiss="modal" aria-label="Close" style="background-color:red;font-weight:bold;float:right;">
					<span aria-hidden="true" class="ti-close" style="color:White;font-weight:bold;"></span></button>--%>
					
					<asp:LinkButton  ID="btnCerrarCruceHorario" runat="server"   CssClass="btn btn-white derecha" ForeColor="White" Font-Bold="true" Text="Buscar" BackColor="#f0ad4e" ><span aria-hidden="true" class="ti-close" style="color:White;font-weight:bold;"></span></asp:LinkButton>
					<%--<asp:Button ID="btnCerrarCruceHorario" runat="server"  data-dismiss="modal" aria-label="Close"><span aria-hidden="true" class="ti-close" style="color:White;font-weight:bold;"></span></asp:Button>--%>
					
					<h4 class="modal-title"  style="color:White;">Cruce horario con carga ya asignada</h4>
				</div>
				<div class="modal-body">
				    <div class="row">
    				    <div class="col-md-12">
    				   
                    <asp:Label ID="lblMensajeCruce" runat="server" Font-Bold="True" 
                        Font-Size="Small" ForeColor="Red"></asp:Label>
                    <br /> 
                    <table id="dgvCruceHorarioDocente" class="display cell-border" style="width:100%">
                    <thead>
                    <tr>
                    <th colspan="6" style="text-align:center;">CARGA ASIGNADA AL DOCENTE</th>
                    <th colspan="2" style="text-align:center;border-left: solid 1px;border-right: solid 1px;border-top: solid 1px;">CARGA DEL CURSO SELECCIONADO</th>
                    </tr>
                    <tr>
                    <th style="text-align:center;border-left: solid 1px;">CARRERA PROFESIONAL</th>
                    <th style="text-align:center">ASIGNATURA</th>
                    <th style="text-align:center">GRUPO</th>
                    <th style="text-align:center">D&Iacute;A</th>
                    <th style="text-align:center">INICIO</th>
                    <th style="text-align:center">FIN</th>
                    <th style="text-align:center;border-left: solid 1px;">CRUCE-INICIO</th>                    
                    <th style="text-align:center;border-right: solid 1px;">CRUCE-FIN</th>
                    </tr>
                    </thead>
                    <tbody id="tbdCruceHorarioDocente" runat="server">
                    </tbody>
                    <tfoot>
                    <tr>
                    <th colspan="8"></th>
                    </tr>
                    </tfoot>
                    </table>                   
                    
    				    </div>
				    </div>
				    <div class="row">
    				    <div class="col-md-12">
    				        <center>
    				         <asp:LinkButton  ID="btnAceptarCruceHorarioCurso" runat="server" Visible="false" CssClass="btn btn-success" ForeColor="White" Font-Bold="true" Text="Buscar"><span class="ion-checkmarck-round"></span>&nbsp;Aceptar</asp:LinkButton>
    				        </center>
    				    </div>
    				</div>
				</div>
		    </div>
		 </div>
	    </div>
<div class="modal fade modal-full-pad" id="mdCruceDisponibilidad" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false" >
		<div class="modal-dialog modal-full" style="margin-top:150px;">
			<div class="modal-content">
				<div class="modal-header" style="background-color:#2196f3;" >
					<%--<button type="button" class="btn btn-white" data-dismiss="modal" aria-label="Close" style="background-color:red;font-weight:bold;float:right;">
					<span aria-hidden="true" class="ti-close" style="color:White;font-weight:bold;"></span></button>--%>
					<asp:LinkButton  ID="btnCerrarCruceDisponibilidad" runat="server"   CssClass="btn btn-white derecha" ForeColor="White" Font-Bold="true" Text="Buscar" BackColor="#2196f3" ><span aria-hidden="true" class="ti-close" style="color:White;font-weight:bold;"></span></asp:LinkButton>
					<h4 class="modal-title"  style="color:White">Comparativo de horarios</h4>
				</div>
				<div class="modal-body">
				    <div class="row">
    				    <div class="col-md-12">
    				   
                    <asp:Label ID="lblMensajeCruceDisponible" runat="server" Font-Bold="True" 
                        Font-Size="Small" ForeColor="Red"></asp:Label>
                    <br />  
                     <table id="dgvCruceHorarioDisponibleDocente" class="display cell-border" style="width:100%">
                    <thead>
                    <tr >
                    <th colspan="3" style="text-align:center;border-left: solid 1px;border-top: solid 1px;background-color:#D8D8D8">DISPONIBILIDAD DOCENTE</th>
                    <th colspan="3" style="text-align:center;border-left: solid 1px;border-right: solid 1px;border-top: solid 1px;">DISPONIBILIDAD DEL CURSO</th>
                    </tr>
                    <tr>
                    <th style="text-align:center;border-left: solid 1px; width:25%;background-color:#D8D8D8;">D&Iacute;A</th>
                    <th style="text-align:center; width:15%;background-color:#D8D8D8;">INICIO</th>
                    <th style="text-align:center; width:15%;background-color:#D8D8D8;">FIN</th>
                    <th style="text-align:center;border-left: solid 1px; width:15%;">INICIO CURSO</th>                    
                    <th style="text-align:center;border-left: solid 1px;border-right: solid 1px; width:15%">FIN CURSO</th>
                    <th style="text-align:center;border-right: solid 1px; width:15%">OBSERVACI&Oacute;N</th>                    
                    </tr>
                    </thead>
                    <tbody id="tbdCruceHorarioDisponibleDocente" runat="server">
                    </tbody>
                    <tfoot>
                    <tr>
                    <th colspan="6"></th>
                    </tr>
                    </tfoot>
                    </table>                          
                 
    				    </div>
    				    <div class="row">
    				    <div class="col-md-12">
    				    <center>
    				     <asp:LinkButton  ID="btnAceptarCruceHorarioDisponible" runat="server"  Visible="false" CssClass="btn btn-success" ForeColor="White" Font-Bold="true" Text="Buscar"><span class="ion-checkmark-round"></span>&nbsp;Aceptar</asp:LinkButton>
    				     </center>
    				    </div>
    				</div>
				    </div>
				</div>
		    </div>
		 </div>
	    </div>

       
          
    </form>
</body>
</html>
