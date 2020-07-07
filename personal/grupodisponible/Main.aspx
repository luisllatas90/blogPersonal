﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Main.aspx.vb" Inherits="Main" %>
<!DOCTYPE html>
<html lang="en">
<head >
  <meta charset="utf-8"/>
  <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 

  <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/> <!--320-->
    <title>Campus Virtual Estudiante</title>

  <!-- <link rel="shortcut icon" href="images/favicon.ico" type="image/x-icon">
  <link rel="icon" href="images/favicon.ico" type="image/x-icon"> -->
  <!-- Bootstrap CSS -->
<link rel='stylesheet' href='assets/css/bootstrap.min.css'/>
<link rel='stylesheet' href='assets/css/material.css'/>
<link rel='stylesheet' href='assets/css/style.css?a=1'/>
<link rel='stylesheet' href='assets/css/sweet-alerts/sweetalert.css'/>

<script type="text/javascript" src='assets/js/jquery.js'></script>
<script type="text/javascript" src='assets/js/app.js'></script>

<script type="text/javascript" src='assets/js/noty/jquery.noty.js'></script>
<script type="text/javascript" src='assets/js/noty/layouts/top.js'></script>
<script type="text/javascript" src='assets/js/noty/layouts/default.js'></script>
<script type="text/javascript" src='assets/js/noty/notifications-custom.js'></script>
<script type="text/javascript" src='assets/js/noty/notificacioncve.js'></script>

<script type="text/javascript" src='assets/js/jquery-ui-1.10.3.custom.min.js'></script>
<script type="text/javascript" src='assets/js/bootstrap.min.js'></script>
<script type="text/javascript" src='assets/js/jquery.nicescroll.min.js'></script>
<script type="text/javascript" src='assets/js/wow.min.js'></script>
<script type="text/javascript" src='assets/js/jquery.loadmask.min.js'></script>
<script type="text/javascript" src='assets/js/jquery.accordion.js'></script>
<script type="text/javascript" src='assets/js/materialize.js'></script>
<script type="text/javascript" src='assets/js/bic_calendar.js'></script>

<script type="text/javascript" src='assets/js/core.js'></script>
<script type="text/javascript" src="assets/js/jquery.countTo.js"></script>
<link rel='stylesheet' href='assets/css/jquery.dataTables.min.css'/>



<script  type="text/javascript" >
    var _SWUPREQ = true;
    var _SWUPREQGH = true;
    var _CALPEN = false;
    var _GDAL = false;
    var _H = 0;
    var _aDataEv = [];
    jQuery(document).ready(function() {

        //INICIO JR
        $("#divEncuesta").on("hidden.bs.modal", function() {
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "procesar.aspx",
                data: { "param0": "CnfSE" },
                dataType: "json",
                async: false,
                success: function(data) {
                    console.log(data);
                    if (data.swEe) {
                        $('#divEncuesta').modal('show');
                        $('#btnCse').remove();
                    }
                    else {

                        $('#mHEe').html('<button type="button" id="btnCse" name="btnCse" class="close" data-dismiss="modal" aria-label="Close" style="color:White;"><span aria-hidden="true" class="ti-close" style="color:White;"></span></button><h4 class="modal-title"  style="color:White">Encuesta</h4>');
                        fnInfoALuDeuda();
                    }


                },
                error: function(result) {
                }
            });


        });
        //FIN JR



        //$(window).resize(fnResizeimg);
        //fnResize();
        _H = $('#top-bar').height();
        $('#imgLogo').click(function() {
            f_Menu('bienvenida.aspx');
        });
        $('#lnkMail').click(function() {
            f_Menu('infomail.aspx');
        });


        $('#_txtconcred').keypress(function(e) {
            var key = e.which;
            if (key == 13)  // the enter key code
            {
                fnSimuPens();
            }
        });

        $('#spSimuPens').click(function() {
            fnSimuPens();
        });
        $('.piluku-preloader').addClass('hidden');
        $('#lnkHome').click(function() {
            window.location.reload();
        });
        fnCursoInhabil();
        //fnCuenta();
        //fnObtDatos();
        fnEvento();

        if (fnEncuesta() == 0) {

            //#002 INICIO - JR
            fnInfoALuDeuda();

            //#002 INICIO - JR
        }

        //fnInfoALu(); //=> comentado temporalmente, si muestra el aviso hace conflicto con div encuesta: no muestra scroll
        //fnAviso(); //=> comentado temporalmente,si muestra el aviso hace conflicto con div encuesta: no muestra scroll

        fnAvisoAlumno();
        fnAvisoAlumnoSunedu();
        //$('#modalDatos').modal('toggle');  

        $("#modalDatos").css({ 'top': _H });
        $("#modalCuota").css({ 'top': _H });
        $("#mdTerCond").css({ 'top': _H });
        $("#divmdEvent").css({ 'top': _H });
        $("#divEncuesta").css({ 'top': _H });

        $("div#modalDatos").on("hidden.bs.modal", function(event) {
            if (_CALPEN) {
                $("#modalCuota").css({ 'top': _H });
                $('#modalCuota').modal('toggle');
                $('#_txtconcred').focus();
                fnSimuPens();
            }
        });



        $("div#modalDatos").on('shown.bs.modal', function() {
            fnResizeimg();
            $('#txtdaemailp').focus();
        });

        $("div#divmdEvent").on("hidden.bs.modal", function(event) {
            fnLimpiarRegEvent();

        });

        $("#cboscocantidad").change(fnCalcularEvento);

        var i = 0;

        for (i = 0; i < 5; i++) {

            $("#rdb" + i + ">input").show();
        }


    });
/*
    function fnAviso() {

        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "processnoty.aspx",
            data: { "param0": "avisoAlu" },
            dataType: "json",
            async: false,
            success: function(data) {
                console.log(data);
                if (data.r)
                //$('#divInfoALu').modal('toggle');
                $('#divAviso').modal('toggle'); 
                


            },
            error: function(result) {
                console.log(result);
            }
        });

    }
*/

    function fnAvisoAlumno() {

        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "processnoty.aspx",
            data: { "param0": "avisoAlumno","param1": 1 },
            dataType: "json",
            async: false,
            success: function(data) {
                console.log(data);
                if (data.r)
                    $('#divInfoALu').modal('toggle');



            },
            error: function(result) {
                console.log(result);
            }
        });

    }

    function fnAvisoAlumnoSunedu() {

        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "processnoty.aspx",
            data: { "param0": "avisoAlumno", "param1": 2 },
            dataType: "json",
            async: false,
            success: function(data) {
                console.log(data);
                if (data.r)
                    $('#divInfoAlu3').modal('toggle');



            },
            error: function(result) {
                console.log(result);
            }
        });

    }
    
    function fnCursoInhabil() {

        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "procesar.aspx",
            data: { "param0": "lstCurInhabil" },
            dataType: "json",
            async: false,
            success: function(data) {
                
                console.log(data);

                var t = '';
                var i = 0;

                if (data.length > 0) {
                    for (i = 0; i < data.length; i++) {
                        t += '<tr>';
                        t += '<td>';
                        t += data[i].cur;
                        t += '</td>';
                        t += '<td>';
                        t += data[i].doc;
                        t += '</td>';
                        t += '<td>';
                        t += data[i].ciclo;
                        t += '</td>';
                        t += '<td>';
                        t += data[i].cred;
                        t += '</td>';
                        t += '<td>';
                        t += data[i].th;
                        t += '</td>';
                        t += '<td>';
                        t += data[i].vcs;
                        t += '</td>';
                        t += '<td>';
                        t += data[i].est;
                        t += '</td>';
                        t += '</tr>';

                    }
                    $('#tbCursosInhabilitado').html(t);
                    $('#divInfoCursoInhabilitado').modal('toggle');
                }




            },
            error: function(result) {
            
                console.log(result);
            }
        });

    }
    
    function fnInfoALuDeuda() {
        if ($("#hdDeudaAlumno").val() == 1) {
            var deuda = 0;
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "processnoty.aspx",
                data: { "param0": "infoAluDeuda" },
                dataType: "json",
                async: false,
                success: function(data) {
                    //console.log(data);
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            deuda = deuda + (parseFloat(data[i].d_saldos) + parseFloat(data[i].d_moradeu))
                        }
                    }
                    deuda = deuda.toFixed(2);
                    $('#montoDeudaAlumno').html("S/." + deuda);
                    $('#divInfoALuDeuda').modal('toggle');
                },
                error: function(result) {
                    //alert(result);
                    //console.log(result);
                    fnMensaje(result);

                }
            });
        }
    }

    function fnSelEncText(i,val) {
     var len = val.value.length;
     if (len > 0) { fnSelEnc(i); }
     else {
         $("#trfEnc" + i).css("font-weight", "normal");
         $("#trfEnc" + i).css("color", "#9398a0");
         $("#trfEnc" + i).removeClass('selected');
         
     }
    }


    function fnSelEnc(i) {
        /*if ($("#trfEnc"+i).hasClass('selected')) {
            $("#trfEnc" + i).removeClass('selected');
        }
        else {*/
        $("#trfEnc" + i).css("font-weight", "bold");
        $("#trfEnc" + i).css("color", "black");
            $("#trfEnc" + i).removeClass('selected');
            $("#trfEnc" + i).addClass('selected');
        //}
    }

    function fnGuardarEnc() {
        var f = parseInt($("#np").val());

        var i = 0;
        var j = 0;
        var num = 2;
        var arraEnc = new Array();
        var t = "";

        for (i = 0; i < f; i++) {
            t = $("#tord" + i).val();
       
            if (t == "C") {
                if ($("input[name='rdb" + i + "']").is(':checked')) {

                    arraEnc[i] = new Object();
                    arraEnc[i].cod = parseInt($("#idk" + i).val());
                    arraEnc[i].valor = parseInt($('input[name=rdb' + i + ']:checked').val());
                }
            } else {            
                arraEnc[i] = new Object();
                arraEnc[i].cod = parseInt($("#idk" + i).val());
                arraEnc[i].valor = $('#preg' + i ).val();
            }            
        }

        console.log(arraEnc);
       // return false;
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "processnoty.aspx",
            data: { "param0": "RegEnc", "param1": arraEnc },
            dataType: "json",
            async: false,
            success: function(data) {
                console.log(data);
                if (data[0].r) {
                    //$('#divEncuesta').modal('hide');
                    //location.reload();
                }
                fnMensaje(data[0].alert, data[0].msg);
            },
            error: function(result) {
            console.log(result);
            }
        });

    }

    function fnEncuesta() {
        var np = parseInt($('#np').val());
        if (np>0)        
        $('#divEncuesta').modal('toggle');
        return np;
    }

    function fnInfoALu() {

        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "processnoty.aspx",
            data: { "param0": "infoAlu" },
            dataType: "json",
            async: false,
            success: function(data) {
                console.log(data);
                if (data.r)
                    //$('#divInfoALu').modal('toggle');
                    $('#divInfoALu2').modal('toggle');  
                    
                    

            },
            error: function(result) {
                console.log(result);
            }
        });



        
        
        
        
    }
    
    function fnGuardarEvent() {
        var cc = parseInt($("#txtsco").val());
        var c = parseInt($("#cboscocantidad").val());
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "processventa.aspx",
            data: { "param0": "regEvent", "param1": cc, "param2": c },
            dataType: "json",
            async: false,
            success: function(data) {
                console.log(data);
                fnLimpiarRegEvent();
                fnMensaje(data[0].alert, data[0].msje);

            },
            error: function(result) {
            console.log(result);
            }
        });

    }
    
    function fnCalcularEvento() {

        var sc = $("#txtsco").val();
        var cuo = parseInt(fnBuscarData(sc, 'cuota', false));
        var p = parseFloat(fnBuscarData(sc, 'precio', false));
        var c = parseInt($("#cboscocantidad").val());

       // alert(sc + '  ' + p + '  ' + c);
        var st = p * c;
        
        $("#txtscosubtotal").val(st.toFixed(2));
        $("#txtscocuota").val(cuo);
    }
    function fnLimpiarRegEvent() {
        $("#cboscocantidad").val(1);
        $("#txtscosubtotal").val(0);
        $("#txtscocuota").val(0);
        $("#txtsco").val('');
    }

    function fnRegEvent(c) {
        $("#txtsco").val(c);
        fnCalcularEvento();
    }
    function fnBuscarData(valor, resulado, up) {
        var r = '';
        var f = _aDataEv.length;
        for (var i = 0; i < f; i++) {

            if (_aDataEv[i].codscc == valor) {
                switch (resulado) {
                    case 'precio':
                        r = _aDataEv[i].precio;
                        break;
                    case 'cuota':
                        r = _aDataEv[i].cuotas;
                        break;
                        
                    default:
                        r = "";
                        break;
                }
            }
        }
        return r;
    }
    
    function fnResizeimg() {
        var tamhDiv = $("#divColDatos").height();
        var tamwDiv = $("#divColDatos").width();
        $("#imgDatos").height(tamhDiv);
        $("#imgDatos").width(tamwDiv);
    }
    
    function fnDivRefresh(div,time) {
        var $target = $('#' + div);
        $target.mask('<i class="fa fa-refresh fa-spin"></i> Cargando...');
        setTimeout(function() {
            $target.unmask();
            //console.log('ended');
        }, time);
    }

  
    
    function fnSimuPens() {
    var _cuota=$("#_txtconcred").val();
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "procesar.aspx",
        data: { "param0": "SPns", "param2": _cuota },
        dataType: "json",
        async: false,
        success: function(data) {
            $('#_txtconcuomenant').val(data[0].pagoAnterior);
            $('#_txtconcuomennue').val(data[0].pagoActual);
            $('#_txtconcuovariacion').val(data[0].difxmen);
            $('#_txtconcuovariacioncred').val(data[0].difxcred);

        },
        error: function(result) {
        }
    });
}
    
    function fnEvento() {

        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "processventa.aspx",
            data: { "param0": "lstEvent" },
            dataType: "json",
            async: false,
            success: function(data) {
                _aDataEv = data;
                var t = '';
                var i = 0;
                var filas = data.length;
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        t += '<div class="panel-body"><div class="col-md-12"><div class="pricing-header" style="background-color:#E33439">' + data[i].etiqueta + '</div><div class="pricing-price">S/. ' + data[i].precio + '</div><div class="pricing-features"><img src="assets/evento/"'+data[i].img+' style="width:100%;height:100%;"/></div>';
                        t += '<ul class="list-unstyled pricing-features">';
                        t += '<li><span>Fecha Inicio</span> ' + data[i].fecini + '</li>';
                        t += '<li><span>Fecha Vencimiento</span> ' + data[i].fecfin + '</li>';
                        t += '<li>' + data[i].cuotas + ' ' + data[i].lblcuotas + '</li>';
                        t += '<li> <a href="#" class="btn btn-primary btn-lg" data-toggle="modal" data-target="#divmdEvent" onclick="fnRegEvent(' + data[i].codscc + ')"><i class="ion-android-cart"></i> Registrar Comprar</a></li>';
                        t += '</ul></div></div>';
                    } 
                    $('#divEvento').html(t);
                    $('#pnlEvento').show();
                    $('#pnlServicio').hide();
                } else {
                    $('#pnlEvento').hide();
                    $('#pnlServicio').hide();
                }

            },
            error: function(result) {
                console.log(result);
            }
        });
    }
    
    function fnCuenta() {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "procesar.aspx",
            data: { "param0": "CnfS" },
            dataType: "json",
            async: false,
            success: function(data) {
                $('#lblCodigoUniversitario').html(data[0].codigoUniversitario);
                $('#imgAvatar').attr('src', data[0].foto);
                _CALPEN = data[0].pen;
            },
            error: function(result) {
            }
        });
    }
    
    function fnObtDatos() {
       
                        $('#modalCuota').modal('toggle');
                        $('#_txtconcred').focus();
                        fnSimuPens();
    }

    function f_Menu(page) {

        if (page.length > 0) {
            if (page != 'notificaciones.aspx') { $("#divNotificacion").html(''); }
                $("#divleft").removeClass("left-bar menu_appear").addClass("left-bar");
                $("#divleft").css(["overflow", "hidden", "outline", "none"]);
                $("#divOverlay").removeClass("overlay show").addClass("overlay");
                $('.piluku-preloader').removeClass('hidden');
                $.post(page, {
            }, function(data, status) {
                if (status == 'success') {
                    fnCuenta();
                    // $("#divContent").html(data);
                    $("#divContent").empty();
                    $("#divContent").html(data);

                } else {
                    $("#divContent").html("");
                    //location.reload();
                }
                $('.piluku-preloader').addClass('hidden');
            });
        }
    }


function popup(url, ancho, alto) {
    var posicion_x;
    var posicion_y;
    posicion_x = (screen.width / 2) - (ancho / 2);
    posicion_y = (screen.height / 2) - (alto / 2);
    window.open(url, "leonpurpura.com", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");
}

function popupVisa(url, ancho, alto) {
    var posicion_x;
    var posicion_y;
    posicion_x = (screen.width / 2) - (ancho / 2);
    posicion_y = (screen.height / 2) - (alto / 2);
    window.open(url, "leonpurpura.com", "width=" + ancho + ",height=" + alto + ",status=no,toolbar=no,menubar=no,left=" + posicion_x + ",top=" + posicion_y + "");
}
function visa(pag) 
    {
        window.location.href = pag;
    }
     
</script>
<script type="text/javascript" src='assets/js/funciones.js'></script>
<style>
.MOTRAR
{
    display:block;
    }
    
    .ImgURL 
    { min-height:110px;
      min-width:100px; 
      max-height:110px;
      max-width:100px; 
             
    }
    
    
</style>
</head>
	<!-- <frameset cols="100%">
          <frame src="http://www.usat.edu.pe/anuncios/index.php"/>
         </frameset>-->
<body class="" runat="server" id="bodyPrincipal" >
    <div class="piluku-preloader text-center">
  <!-- <div class="progress">
      <div class="indeterminate"></div>
  </div> -->
  <div class="loader">Loading...</div>
</div>
<div class="wrapper ">
<div class="left-bar " id="divleft">
	<div class="admin-logo">
		<div class="logo-holder pull-left">
			<img class="logo" src="assets/images/logocampusestudiante_small.png" alt="Logo Campus Virtual" id="imgLogo" style="cursor:pointer">	
		</div>
		<!-- logo-holder -->			
		<a href="#" class="menu-bar  pull-right"><i class="ti-menu"></i></a>
	</div>
	<div id="divLeftbar" runat="server"></div>
</div>
<!-- left-bar -->
<div class="content" id="content"  runat="server">

	<div class="overlay" id="divOverlay"></div>			
	<form id="frmContent" runat="server">
	<div class="top-bar" id="top-bar">
	<nav class="navbar navbar-default top-bar"/>
		<div class="menu-bar-mobile" id="open-left"><i class="ti-menu"></i>
		</div>
		<ul class="nav navbar-nav navbar-right top-elements">
		<li class="piluku-dropdown dropdown">
			    <a href="#"  id="lnkCiclo" class="dropdown-toggle" role="button" aria-expanded="false"><span id="lblCICLO" runat="server"></span></a>
            </li>
		    <li class="piluku-dropdown dropdown">
			    <a href="#"  id="lnkMail" class="dropdown-toggle" role="button" aria-expanded="false" runat=server><i class="ion-android-mail"></i> Correo USAT</a>
            </li>
		    <li class="piluku-dropdown dropdown">			
				<a href="#"  id="lnkHome" class="dropdown-toggle" role="button" aria-expanded="false"><i class="ion-ios-home"></i></a>
			</li>
			<li class="piluku-dropdown dropdown">
				<a href="#"  class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="ion-ios-bell-outline icon-notification"></i><span class="badge info-number message" id="spNumberMessage"></span></a>
				<ul class="dropdown-menu dropdown-piluku-menu  animated fadeInUp wow notification-drop neat_drop dropdown-right" data-wow-duration="1500ms" role="menu" id="ulNoty">
					<li>
					<%--	<a href="#" onclick="f_Menu('bienvenida.aspx')" >--%>
							<div class="hexagon info">
								<span><i class="ion-ios-calendar-outline"></i></span>
							</div>
							<span class="text_info"> Bienvenido a tu nuevo campus virtual</span>
							<span class="time_info">2016</span
						</a>
						
					</li>
				</ul>
			</li>
			<li class="piluku-dropdown dropdown">
				<!-- @todo Change design here, its bit of odd or not upto usable -->

				<a href="#" class="dropdown-toggle avatar_width" data-toggle="dropdown" role="button" aria-expanded="false"><span class="avatar-holder">
    <asp:Image ID="imgAvatar" runat="server" /></span><span class="avatar_info"><asp:Label ID="lblCodigoUniversitario" runat="server" Text=""></asp:Label></span><span class="drop-icon"><!-- <i class="ion ion-chevron-down"></i> --></span></a>
				<ul class="dropdown-menu dropdown-piluku-menu  animated fadeInUp wow avatar_drop neat_drop dropdown-right" data-wow-duration="1500ms" role="menu">
				
					<li>
						<a href="#" onclick="f_Menu('misdatos.aspx')"> <i class="ion-android-create" "></i>Mis Datos</a>
					</li>
					<li>	
						<asp:LoginStatus ID="LoginStatus1" runat="server" CssClass="logout_button"  ></asp:LoginStatus>
					</li>   
				</ul>
			</li>

		</ul>

	</nav>

</div>
    <!-- /top-bar -->
    <!-- INICIO ENCUESTA -->
    
    <div class="row" >
<div class="modal fade modal-full-pad" id="divEncuesta" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
		<div class="modal-dialog  modal-full">
			<div class="modal-content">
				<div id="mHEe" class="modal-header" style="background-color:#E33439;" >
					<button type="button" id="btnCse" name="btnCse" class="close" data-dismiss="modal" aria-label="Close" style="color:White;"><span aria-hidden="true" class="ti-close" style="color:White;"></span></button>
					<h4 class="modal-title"  style="color:White">Encuesta</h4>
				</div>
				<div class="modal-body">
                    <div class="row">	
					<div class="col-md-12" >
                   
			        <div class="col-md-12" style="border: 1px solid #ddd;"> 
			        <div class="col-md-12" id="div3">			    
				            <div class="row">
						        <div class="col-md-12">
						        <div class="form-group">
						        	<div class="col-md-1">
								     
								    </div>
								    	<div class="col-md-11">
								        <div class="row">
								           <div class="col-md-12">
								              <!-- <asp:Image ID="Image1" runat="server" 
                                                    ImageUrl="http://www.usat.edu.pe/web/wp-content/uploads/2015/04/logousat.png" Width="70px" Height="50px" />-->
                                                    <center><h2>Evaluación del Desempeño Docente</h2></center>
								                    <!--Inicio Cambio 29.05.17-->
								                    <p style="text-align:justify;">
                                                    Estimado estudiante:
                                                    A continuación te presentamos una serie de afirmaciones sobre el desempeño de los docentes que te acompañan en tu aprendizaje, te pedimos pongas atención y respondas con tranquilidad seleccionando el número que consideres, según el siguiente cuadro:  
                                                    </p>
                                                    <!--Fin Cambio 29.05.17-->	
                                                   							              
								           </div>
								       </div>
								      
								    
								       <div class="row">
								           <div class="col-md-12">		
								           <center>
								           <table border="0">
								           <tr style="background-color:#E33439; color:White;">
								           <td colspan="2"><center>DOCENTE A EVALUAR</center></td>
								           </tr>
								          
								           			           
								           <tr>
								            <td><asp:Image ID="imgDocente" runat="server" CssClass="ImgURL" 
                                                    AlternateText="Imagen del Docente" DescriptionUrl="Imagen del Docente" /></td>
								            <td style="padding:5px" valign=middle>
								                    <table style="text-align:center">
								                        <tr><td><b><h4><asp:Label ID="lblNombreDocente" runat="server" Text="Label"></asp:Label></h4></b></td></tr>
								                      
								                        <tr><td><b> <asp:Label ID="lblCursoDocente" runat="server" Text="Label"></asp:Label></b></td></tr>
								                       
								                    </table>
								                    
                                           </td>
								           </tr>
								           
								           </table>
								           	</center>					              
                                           
                                            <!--Inicio Cambio 29.05.17-->
                                            <center>
                                                <table style="text-align:center">
                                                 <tr>
                                                    <td>
                                                <img src="assets/images/escalaEncuesta.png" style="width:95%;height:95%;margin-top:5px;margin-bottom:5px;" alt="Escala" />
                                                </td>
                                                    </tr>
                                                </table>
                                           </center> 
                                           	<!--Fin Cambio 29.05.17-->					              
								           </div>
								       </div>
								      
								       <div class="row">
								           <div class="col-md-12">
								           <div class="table-responsive">
								           <input type="hidden" id="np" value="0" runat="server" />
								           <table id="gvPregunta" style="width:100%;" class="display dataTable cell-border">
								           <thead>
								           <tr style="background-color:#E33439; color:White; ">
								           <th style="width:5%;padding:5px;"><center>N&deg;</center></th>
								           <th style="width:45%">Pregunta</th>
								           <th style="width:10%"><center><br />1</center></th>
								           <th style="width:10%"><center><br />2</center></th>
								           <th style="width:10%"><center><br />3</center></th>
								           <th style="width:10%"><center><br />4</center></th>
								           <th style="width:10%"><center><br />5</center></th>
								           </tr>
								           </thead>
								           <tbody id="tbdPregunta" runat="server" style="color:black;">
								           								           
								           </tbody>	
								           <tfoot>
								           <tr>
								           <th colspan="7"></th>
								           </tr>
								           </tfoot>							           
								           </table>
								           
								           <br />
								           
								           
                                            </div>
								           </div>
								       </div>
					                    
					                    </div>
							        </div>
					            </div>
						        </div>
					       
					       
				         
				            
					        </div>
					        </div>
					        <div class="row">
					        <center>
                            <%-- <asp:Button ID="btnGuardarEncuesta" runat="server" Text="Registrar Encuesta"  />--%>
					        <button type="button" class="btn btn-primary"  onclick="fnGuardarEnc()" ><i class="ion-done"></i>Enviar Respuestas</button>
				           				
					        </center>
					        </div>
					       
			        </div>
			        </div>
			                
					</div>
				    </div>
			        </div>
			        <div class="modal-footer">
				    
				</div>
				</div>
				
			</div>	
    
    
    
    
    <!-- FIN ENCUESTA -->
    
    
	
	<div class="main-content" style="padding-top:0px; padding-left:2px;padding-right:2px;">
	<div id="divNotificacion">
	</div>
	<div id="divContent">	
	<div class="row grid col-md-12"> 
	 <div class="col-md-8">
    			<div class="panel panel-piluku">
					<div class="panel-heading" style="background-color:White; color:black">
						<h3 class="panel-title">
							<b style="color:red">Anuncios</b>
						</h3>
					</div>
    		         <div class="panel-body" id="div_anuncio_bd" runat="server">
    		         <!--#001 - JR 
    		         <OBJECT id="anuncio" type="text/html" data="http://www.usat.edu.pe/anuncios/index.php" width=100% height=200% style="position: relative; overflow: hidden; height: 800px;"></OBJECT>-->
    		         </div>
    		    </div>
    	     </div>  
	 <div class="col-md-4">
	           <!-- <div class="panel panel-piluku">
					<div class="panel-heading" 
						<h3 class="panel-title">
							<h3 class='panel-title'>Encuesta</h3>
						</h3>
					</div>
					<div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <a href="https://intranet.usat.edu.pe/encuestas/index.php/781851?lang=es" target="_blank" style="text-align:justify">[BIBLIOTECA] - ENCUESTA DE SATISFACCION DEL SERVICIO - 2018</p><i>Ingresar aqu&iacute;</i></a>
                            </div>
                        </div>
    		        </div>
    		    </div>-->
    		    
    		    <div class="panel panel-piluku" id="divEncuestaAlu" runat="server" >
					<div class="panel-heading" 
						<h3 class="panel-title">
							<h3 class='panel-title'>Encuesta</h3>
						</h3>
					</div>
					<div class="panel-body">
                        <div class="row">
                            <div class="col-md-12"  id="divEncuestaAluBody" runat="server" >
                                <a href="https://intranet.usat.edu.pe/encuestas/index.php/367819?newtest=Y" target="_blank" style="text-align:justify">Encuesta de satisfacción del estudiante 2018</p><i>Ingresar aqu&iacute;</i></a>
                            </div>
                        </div>
    		        </div>
    		    </div>
    		     <div class="panel panel-piluku" id="divEvaluacion" runat="server" >
					<div class="panel-heading" 
						<h3 class="panel-title">
							<h3 class='panel-title'>Proceso de Evaluaci&oacute;n</h3>
					    </h3>
					</div>
					<div class="panel-body">
					    <div class="row">
                            <div class="col-md-12"  id="divImgResultado" runat="server" >
                            <asp:Image runat="server" ID="imgResultado" Width="100%" Height="100%" />
                            </div>                            
                        </div>
                        <div class="row">
                            <div class="col-md-12"   >
                                <a href="http://tuproyectodevida.pe/resultados.php" target="_blank" style="text-align:justify">Resultados de Evaluaciones:</p></a>
                            </div>
                        </div>
    		        </div>
    		    </div>
    		    <!--
    		     <div class="panel panel-piluku" id="divEncuestaWifi" runat="server" >
					<div class="panel-heading" 
						<h3 class="panel-title">
							<h3 class='panel-title'>Encuesta</h3>
						</h3>
					</div>
					<div class="panel-body">
                        <div class="row">
                            <div class="col-md-12"  id="div6" runat="server" >
                                <a href="https://intranet.usat.edu.pe/encuestas/index.php/525847" target="_blank" style="text-align:justify">Encuesta de satisfacción con el servicio wifi USAT</p><i>Ingresar aqu&iacute;</i></a>
                            </div>
                        </div>
    		        </div>
    		    </div>
    		    -->
    			 <div class="panel panel-piluku" id="pnlEvento">
					<div class="panel-heading" style="background-color:White; color:black">
						<h3 class="panel-title">
							<b style="color:red">Eventos</b>
						</h3>
					</div>
<div id="divEvento">
</div>
    		        
    		    </div>
    		    <div class="panel panel-piluku" id="pnlServicio">
					<div class="panel-heading" style="background-color:White; color:black">
						<h3 class="panel-title">
							<b style="color:red">Mesa de Servicios de TI</b>
						</h3>
					</div>
                        <div id="divMesaServicio">
                        <center><img src="assets/images/serviciostimesa.png" style="width:95%;height:95%;margin-top:5px;margin-bottom:5px;" /></center>
                        </div>
    		        
    		    </div>
    		   
    		    
    	     </div>   	     
    </div>
  
    
    <div class="modal fade" id="modalCuota" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true" class="ti-close"></span></button>
					<h4 class="modal-title" id="myModalLabel5" style="color:#E33439">Consulta tu nueva cuota mensual</h4>
				</div>
				<div class="modal-body">
					<div class="row">
						<div class="col-md-1">
							<div class="responsive-bottom">
								<div class="hexagon warning">
                                    <span><i class="ion-android-alert"></i></span>
                                    </div>
							</div>
						</div>
						<div class="col-md-11">
						
						<div class="form-group">
							<label class="control-label">Ingresa el n&uacute;mero de cr&eacute;ditos que deseas matricularte en el semestre 2016-II</label>
							<div class="input-group">	
							<span class="input-group-addon bg" id="spSimuPens">
									<i class="ion-calculator"></i>							
							</span>							
								<input type="number" id="_txtconcred" name="_txtconcred" maxlength=2 value=20 style="text-align:center;width:20%;height:30px;" class="form-control"/><label class="control-label">&nbsp;Creditos.	</label>							
							</div>
							<!-- /input-group -->
						</div>

						</div>
			        </div><br />
			        <div class="row">
			        <div class="col-md-12">
						<div class="col-md-3">
						    <div class="form-group">
							<div class="responsive-bottom">
								<h6>Cuota mensual hasta 2016-I</h6>
								<input type="text" id="_txtconcuomenant" name="_txtconcuomenant" value="" style="text-align:center;font-weight:bold;color:Black;" class="form-control" readonly="readonly"  /> 
							</div>
							</div>
						</div>
						<div class="col-md-3">
						<div class="form-group">
							<div class="responsive-bottom">
								<h6>Cuota mensual desde 2016-II</h6>
								<input type="text" id="_txtconcuomennue" name="_txtconcuomennue" value="" style="text-align:center;font-weight:bold;color:Black;"  class="form-control"  readonly="readonly" /> 
							</div>
					    </div>
						</div>
						<div class="col-md-3">
						<div class="form-group">
							<div class="responsive-bottom">
								<h6>Variacion en cuota mensual</h6>
								<input type="text" id="_txtconcuovariacion" name="_txtconcuovariacion" value="" style="text-align:center;font-weight:bold;color:Black;" class="form-control"  readonly="readonly"  /> 
							</div>
						</div>
						</div>
						<div class="col-md-3">
						<div class="form-group">
							<div class="responsive-bottom">
								<h6>Variacion de cuota por credito</h6>
								<input type="text" id="_txtconcuovariacioncred" name="_txtconcuovariacioncred" value="" style="text-align:center;font-weight:bold;color:Black;"  class="form-control"  readonly="readonly" /> 
							</div>
						</div>
						</div>
					</div>
					</div>
				</div>
				<div class="modal-footer">
				<h6>Recuerda que el costo de ciclo se financia en 5 cuotas, la universidad no trabaja con mensualidades</h6>
					<%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
					<button type="button" class="btn btn-primary">Save changes</button>--%>
				</div>
			</div>
		</div>
	</div>
    
    </div> 

	</div>
	
	</form>
</div>  
</div>
<div class="content" >
<div class="row">
<div class="modal fade modal-full-pad" id="modalDatos" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
		<div class="modal-dialog modal-full">
			<div class="modal-content">
				<div class="modal-header" style="background-color:#E33439;" >
					<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color:White;"><span aria-hidden="true" class="ti-close" style="color:White;"></span></button>
					<h4 class="modal-title"  style="color:White">Modifica tus datos</h4>
				</div>
				<div class="modal-body">
                    <div class="row">	
					<div class="col-md-12" >
					   <%-- <div class="col-md-6" style="border: 1px solid #ddd;" >	
					            
										<div class="profile-sidebar-heading">
											<center><h3>Términos y Condiciones</h3></center>
											<!--<a href="#"><i class="ion ion-edit"></i></a>-->
										</div>
										<!--                        *** Profile Personal ***-->
										<br />
										<p style="text-align:justify">
										Estimados Estudiantes la Universidad Cat&oacute;lica Santo Toribio de Mogrovejo desea estar en constante comunicaci&oacute;n con Usted, por lo cual solicita  la actualizaci&oacute;n  de  sus datos. Esto nos permitir&aacute;  comunicarles informaci&oacute;n acad&eacute;mica,  eventos acad&eacute;micos, encuestas,  ofertas, promociones y recomendaciones dadas por la Universidad. 
                                        De conformidad con la Ley N° 29733, Ley de Protecci&oacute;n de Datos Personales y su norma reglamentaria D.S N° 003-2013-JUS, AUTORIZO a la Universidad Cat&oacute;lica Santo Toribio de Mogrovejo – USAT, a utilizar los datos personales proporcionados o que proporcione a futuro, para la gesti&oacute;n acad&eacute;mica, administrativa y comercial que realice la universidad. Acepto el env&iacute;o, por cualquier medio de informaci&oacute;n y publicidad
                                        Declaro como usuario, que tengo el derecho de acceder, rectificar, oponerme y cancelar mis datos personales, enviando un correo a informacion@usat.edu.pe.
                                        </p>
										<br />
										<p>
										[<input type='checkbox' class='mark-complete' id='chkacda' name='chkacda' checked="checked">
                                        <label for='chkacda'><span></span></label>]<font style="font-weight:bold;color:Black;"> Acepto T&eacute;rminos y Condiciones</font>
										</p>								
									
						</div>--%>
			        <div class="col-md-12" style="border: 1px solid #ddd;" > 
			        <div class="row">
			        <div class="col-md-6">
			        <img id="imgDatos" src="assets/images/tablet_datos_0.png"/>
			        </div>
			        <div class="col-md-6" id="divColDatos">
			        <div class="row">
						        <div class="col-md-12">
						            <div class="form-group">
								        <h5>Email Principal</h5>
								        <div class="input-group">	
							                <span class="input-group-addon bg">
									        <i class="ion-edit"></i>							
							            </span>	
								        <input type="text" id="txtdaemailp" name="txtdaemailp" value="" style="text-align:left;font-weight:bold;color:Black;" class="form-control" /> 
							            </div>
							        </div>
						        </div>
				            </div>
				            <div class="row">
						        <div class="col-md-12">
						        <div class="form-group">
								        <h5>Email Alternativo</h5>
								        <div class="input-group">	
							                <span class="input-group-addon bg">
									        <i class="ion-edit"></i>							
							            </span>
								        <input type="text" id="txtdaemaila" name="txtdaemailpa" value="" style="text-align:left;font-weight:bold;color:Black;"  class="form-control" /> 
							        </div>
					            </div>
						        </div>
					        </div>	
					        <div class="row">
						        <div class="col-md-12">
						        <div class="form-group">
								        <h5>Tel&eacute;fono Fijo</h5>
								        <div class="input-group">	
							                <span class="input-group-addon bg">
									        <i class="ion-edit"></i>							
							            </span>
								        <input type="text" id="txtdafonofijo" name="txtdafonofijo" value="" style="text-align:left;font-weight:bold;color:Black;" class="form-control"  maxlength=10  /> 
							        </div>
						        </div>
						        </div>
				            </div>
				            <div class="row">
						        <div class="col-md-12">
						        <div class="form-group">
								        <h5>Tel&eacute;fono M&oacute;vil</h5>
								        <div class="input-group">	
							                <span class="input-group-addon bg">
									        <i class="ion-edit"></i>							
							            </span>
								        <input type="text" id="txtdafonomovil" name="txtdafonomovil" value="" style="text-align:left;font-weight:bold;color:Black;" class="form-control"  maxlength=10  /> 
							        </div>
						        </div>
						        </div>
					        </div>
					        <div class="row">
						        <div class="col-md-12">
						        <div class="form-group">
								        <h5>Direcci&oacute;n</h5>
								        <div class="input-group">	
							                <span class="input-group-addon bg">
									        <i class="ion-edit"></i>							
							            </span>
								        <input type="text" id="txtdadireccion" name="txtdadireccion" value="" style="text-align:left;font-weight:bold;color:Black;" class="form-control"  /> 
							        </div> 
						        </div>
						        </div>
					        </div>
					        <div class="row">
					        <div class="cold-md-12">
					        <center><p>
										        [<input type='checkbox' class='mark-complete' id='chkacda' name='chkacda' checked="checked">
                                                <label for='chkacda'><span></span></label>]<font style="font-weight:bold;color:Black;">Acepto  <a href="#"  data-toggle="modal" data-target="#mdTerCond">T&eacute;rminos y Condiciones</a></font>
										        </p></center>
					        </div>
					        </div>
					        <div class="row">
					        <center>
					        <button type="button" class="btn btn-primary"  onclick="fnModDatos()"><i class="ion-done"></i>Guardar</button>
				            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>					
					        </center>
					        </div>
			        </div>
			        </div>
			                
					</div>
				    </div>
			        </div>
			
				</div>
				<div class="modal-footer">
				    
				</div>
			</div>
		</div>
	</div>
</div>
<div class="row">
<div class="modal fade" id="mdTerCond" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="btn btn-default" data-dismiss="modal" aria-label="Close" style="float:right"><span aria-hidden="true" class="ti-close"></span></button>
					<h4 class="modal-title" id="H1" style="color:#E33439">T&eacute;rminos y Condiciones</h4>
				</div>
				<div class="modal-body">
                    <div class="row">	
					<div class="col-md-12" >
					   <%-- <div class="col-md-6" style="border: 1px solid #ddd;" >	
					            
										<div class="profile-sidebar-heading">
											<center><h3>Términos y Condiciones</h3></center>
											<!--<a href="#"><i class="ion ion-edit"></i></a>-->
										</div>
										<!--                        *** Profile Personal ***-->
										<br />
										<p style="text-align:justify">
										Estimados Estudiantes la Universidad Cat&oacute;lica Santo Toribio de Mogrovejo desea estar en constante comunicaci&oacute;n con Usted, por lo cual solicita  la actualizaci&oacute;n  de  sus datos. Esto nos permitir&aacute;  comunicarles informaci&oacute;n acad&eacute;mica,  eventos acad&eacute;micos, encuestas,  ofertas, promociones y recomendaciones dadas por la Universidad. 
                                        De conformidad con la Ley N° 29733, Ley de Protecci&oacute;n de Datos Personales y su norma reglamentaria D.S N° 003-2013-JUS, AUTORIZO a la Universidad Cat&oacute;lica Santo Toribio de Mogrovejo – USAT, a utilizar los datos personales proporcionados o que proporcione a futuro, para la gesti&oacute;n acad&eacute;mica, administrativa y comercial que realice la universidad. Acepto el env&iacute;o, por cualquier medio de informaci&oacute;n y publicidad
                                        Declaro como usuario, que tengo el derecho de acceder, rectificar, oponerme y cancelar mis datos personales, enviando un correo a informacion@usat.edu.pe.
                                        </p>
										<br />
										<p>
										[<input type='checkbox' class='mark-complete' id='chkacda' name='chkacda' checked="checked">
                                        <label for='chkacda'><span></span></label>]<font style="font-weight:bold;color:Black;"> Acepto T&eacute;rminos y Condiciones</font>
										</p>								
									
						</div>--%>
			        <div class="col-md-12" style="border: 1px solid #ddd;" > 
			        <div class="row">
						<div class="col-md-12">
						    <div class="form-group">
								<p style="text-align:justify" class="form-control">
										Estimados Estudiantes la Universidad Cat&oacute;lica Santo Toribio de Mogrovejo desea estar en constante comunicaci&oacute;n con Usted, por lo cual solicita  la actualizaci&oacute;n  de  sus datos. Esto nos permitir&aacute;  comunicarles informaci&oacute;n acad&eacute;mica,  eventos acad&eacute;micos, encuestas,  ofertas, promociones y recomendaciones dadas por la Universidad. 
                                        De conformidad con la Ley N° 29733, Ley de Protecci&oacute;n de Datos Personales y su norma reglamentaria D.S N° 003-2013-JUS, AUTORIZO a la Universidad Cat&oacute;lica Santo Toribio de Mogrovejo – USAT, a utilizar los datos personales proporcionados o que proporcione a futuro, para la gesti&oacute;n acad&eacute;mica, administrativa y comercial que realice la universidad. Acepto el env&iacute;o, por cualquier medio de informaci&oacute;n y publicidad
                                        Declaro como usuario, que tengo el derecho de acceder, rectificar, oponerme y cancelar mis datos personales, enviando un correo a informacion@usat.edu.pe.
                                   
                                   </p>
							</div>
						</div>
				    </div>

	

					</div>
				    </div>
			        </div>
			
				</div>
				<div class="modal-footer">

				</div>
				
			</div>
		</div>
	</div>
</div>

<div class="row">
<div class="modal fade" id="divmdEvent" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header" style="background-color:#E33439;" >
					<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color:White;"><span aria-hidden="true" class="ti-close" style="color:White;"></span></button>
					<h4 class="modal-title"  style="color:White">Ingresa datos de compra</h4>
				</div>
				<div class="modal-body">
                    <div class="row">	
					<div class="col-md-12" >

			        <div class="col-md-12" style="border: 1px solid #ddd;"> 
			        <div class="col-md-12" id="div2">			    
				            <div class="row">
						        <div class="col-md-12">
						        <div class="form-group">
								        <h5>Cantidad</h5>
								        <input type="hidden" id="txtsco" name="txtsco" value="" />							        
								        <select id="cboscocantidad" name="cboscocantidad" class="form-control">
								        <option value="1" selected="selected">1</option>
								        <option value="2">2</option>
								        <option value="3">3</option>
								        <option value="4">4</option>
								        <option value="5">5</option>
								        </select>
							        </div>
					            </div>
						        </div>
					       
					        <div class="row">
						        <div class="col-md-12">
						        <div class="form-group">
								        <h5>Sub Total</h5>
								        <input type="text" id="txtscosubtotal" name="txtscosubtotal" value="0" style="text-align:right;font-weight:bold;color:Black;" class="form-control" readonly /> 
							        </div>
						        </div>
						        </div>
				         
				            <div class="row">
						        <div class="col-md-12">
						        <div class="form-group">
								        <h5>N&deg; de Cuotas</h5>
								        <input type="text" id="txtscocuota" name="txtscocuota" value="0" style="text-align:right;font-weight:bold;color:Black;" class="form-control" readonly /> 
							        </div>
						        </div>
						        </div>
					        </div>
					        </div>
					        <div class="row">
					        <center>
					        <button type="button" class="btn btn-primary"  onclick="fnGuardarEvent()" data-dismiss="modal"><i class="ion-done"></i>Comprar</button>
				            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>					
					        </center>
					        </div>
					       
			        </div>
			        </div>
			                
					</div>
				    </div>
			        </div>
			        <div class="modal-footer">
				    
				</div>
				</div>
				
</div>

<!--
<div class="row">
    <div class="modal fade" id="divAviso" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true"  data-backdrop="static" data-keyboard="false">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header" style="background-color:#E33439;" >					
					<h4 class="modal-title"  style="color:White">Estimado(a) Estudiante</h4>
				</div>
				<div class="modal-body">
                    <div class="row">	
					
					<div class="col-md-12">
					    <p style="text-align:justify">
					    <font> Te informamos que ya puedes recoger el carné SUNEDU 2018 con vigencia hasta el mes de julio del 2019 en la Oficina de Evaluación y Registro (3er piso Edificio Antiguo) en los siguientes horarios hasta el  </font> <font style="font-weight:bold">miércoles 28 de noviembre:</font>
					    </p>
                    </div>
					<div class="row">
					    <div class="col-md-12">
    					    <img src="assets/images/AVISO_CARNE_SUNEDU2.png?x=1" style="width:100%; height:100%"/>
					    </div>
					</div>	
					
					</div>
				</div>
				 <div class="modal-footer">
                <center>
                    <div class="btn-group">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">
                            <i class="ion-android-cancel"></i>&nbsp;Cerrar</button>
                    </div>
                </center>
            </div>
		    </div>
		 </div>
    </div>
		    
</div>	
-->
<div class="row">
    <div class="modal fade" id="divInfoALu" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true"  data-backdrop="static" data-keyboard="false">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header" style="background-color:#E33439;" >
					<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color:White;"><span aria-hidden="true" class="ti-close" style="color:White;"></span></button>
					<h4 class="modal-title"  style="color:White">Estimado Alumno</h4>
				</div>
				<div class="modal-body">
                    <div class="row">	
					<div class="col-md-12">					    
					<div class="row" id="divCarta" runat="server">
					<center><a class="btn btn-primary" href="ImprimirAvisoAlumno.aspx" target="_blank"><i class="icon ion-ios-paper"></i> Clic para descargar la carta</a></center>
					</div>
					<div class="row">
					    <div class="col-md-12">
					    <p style="text-align:justify">
					    Para mayor información puede acercarse a la oficina de
                        Pensiones en nuestro Campus Universitario o a través del email
                        <a href='mailto:informacion@usat.edu.pe' target='_top' style ='color:blue;'>información@usat.edu.pe.</a>.

					    </p>
					    </div>
					</div>
					</div>
					</div>
				</div>
		    </div>
		 </div>
    </div>
		    
</div>
<!--
<div class="row">
    <div class="modal fade" id="divInfoALu2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true"  data-backdrop="static" data-keyboard="false">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header" style="background-color:#E33439;" >
					
					<h4 class="modal-title"  style="color:White">Estimado(a) Estudiante</h4>
				</div>
				<div class="modal-body">
                    <div class="row">	
					<div class="col-md-12">					    
					<div class="row" id="div5" runat="server">
					
					</div>
					<div class="row">
					    <div class="col-md-12">
					    <p style="text-align:justify">
					    <%--<font> Te informamos que ya puedes recoger el carné SUNEDU 2018 con vigencia hasta el mes de julio del 2019 en la Oficina de Evaluación y Registro (3er piso Edificio Antiguo) en los siguientes horarios hasta el  </font> <font style="font-weight:bold">miércoles 28 de noviembre:</font>--%>
					    </p>
					    </div>
					</div>
					<div class="row">
					    <div class="col-md-12">
    					    <img src="assets/images/AVISO_CARNE_SUNEDU2.png" style="width:100%; height:100%"/>
					    </div>
					</div>				
					</div>
					</div>
				</div>
				  <div class="modal-footer">
                <center>
                    <div class="btn-group">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">
                            <i class="ion-android-cancel"></i>&nbsp;Cerrar</button>
                    </div>
                </center>
            </div>
		    </div>
		 </div>
    </div>
		    
</div>-->
<div class="row">
    <div class="modal fade" id="divInfoAlu3" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true"  data-backdrop="static" data-keyboard="false">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header" style="background-color:#E33439;" >
					<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color:White;"><span aria-hidden="true" class="ti-close" style="color:White;"></span></button>
					<h4 class="modal-title"  style="color:White">Estimado (a) Estudiante</h4>
				</div>
				<div class="modal-body">
                    <div class="row">	
					    <div class="col-md-12">					    
					        <div class="row">
					            <div class="col-md-12">
					            <p style="text-align:justify">
					            Te informamos que ya puedes recoger el carné SUNEDU 2018 con vigencia hasta el mes de julio del 2019 en la Oficina de Evaluación y Registro (3er piso Edificio Antiguo) de Lunes a Viernes de 07:30 – 13:30 hrs. hasta el <font style="font-weight:bold">jueves 31 de enero</font>. El costo del carné SUNEDU es de S/ 17.00 soles.
                                <br />
                                <br />
                                <font style="font-weight:bold">*Nota</font>: Si a la fecha ya te acercaste a recoger tu carné, omite este mensaje. Si eres estudiante de la <font style="font-weight:bold">Modalidad pregrado para Gente que trabaja (GO), Posgrado o 2das Especialidades</font>, tu coordinador te hará entrega del documento. 
                               </p>
					            </div>
					        </div>
					    </div>
					</div>
				</div>
		    </div>
		 </div>
    </div>
		    
</div>
<div class="row">
    <div class="modal fade" id="divInfoALuDeuda" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true"  data-backdrop="static" data-keyboard="false">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header" style="background-color:#E33439;" >
					<button type="button"  class="close" data-dismiss="modal" aria-label="Close"  style="color:White;"><span aria-hidden="true" class="ti-close" style="color:White;"></span></button>					
					<%--<a href="javascript:void(0)"  class="btn"><span aria-hidden="true" class="ti-close" style="color:White;"></span></a>--%>
					<h4 class="modal-title"  style="color:White">Estimado Estudiante</h4>
				</div>
				<div class="modal-body">
                    <div class="row">	
					<div class="col-md-12">
					    
					<div class="row" id="div4" runat="server">
					<!--<center><a class="btn btn-primary" href="ImprimirAvisoAlumno.aspx" target="_blank"><i class="icon ion-ios-paper"></i> Clic para descargar la carta</a></center>-->
					</div>
					<input type="hidden" id="hdDeudaAlumno" name="hdDeudaAlumno" value="" runat="server"/>							        
					
					<div class="row">
					    <div class="col-md-12">
					    <p style="text-align:justify;color:#337ab7;font-style:italic;">
					    A la fecha tiene usted una deuda vencida de: <label id="montoDeudaAlumno" style="color:red"></label>
					    </p>
					    <p style="text-align:justify;color:#337ab7;font-style:italic;">
                        Puede cancelar su pensión en los bancos: BCP (Banco de Crédito) o BBVA (Banco Continental) indicando su número de DNI.
					    </p>
					    <p style="text-align:right;color:#337ab7;font-style:italic;font-weight:bold">
					    Oficina de Pensiones
					    </p>
					    </div>
					</div>
					</div>
					</div>
				</div>
		    </div>
		 </div>
    </div>
		    
</div>

<div class="row">
    <div class="modal fade" id="divInfoCursoInhabilitado" tabindex="2" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true"  data-backdrop="static" data-keyboard="false">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header" style="background-color:#E33439;" >
					<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color:White;"><span aria-hidden="true" class="ti-close" style="color:White;"></span></button>
					<h4 class="modal-title"  style="color:White">Cursos Inhabilitados</h4>
				</div>
				<div class="modal-body">
                    <div class="row">	
					<div class="col-md-12" id="rowCursoInhabilitado">					    
					
					<table class="display dataTable" id="tablacursosmatriculadosInhabilitado">
									<thead>
										<tr role="row">		
											<th style="width:30%">Curso</th>
											<th style="width:20%">Docente</th>
											<th style="width:5%">Ciclo</th>
											<th style="width:5%">Crd.</th>
											<th style="width:5%">Hrs.</th>											
											<th style="width:5%">Veces</th>
											<th style="width:5%">Estado</th>
										</tr>
									</thead>
									<tbody id="tbCursosInhabilitado" runat="server" style="font-size:x-small">
									</tbody>
									<tfoot id="tfCursosInhabilitado" runat="server">
									<tr role="row">
									<th colspan="7"></th>
									</tr>
									
									</tfoot>
								</table>
					</div>
					</div>
				</div>
				 <div class="modal-footer">
                <center>
                    <div class="btn-group">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">
                            <i class="ion-android-cancel"></i>&nbsp;Cerrar</button>
                    </div>
                </center>
            </div>
		    </div>
		 </div>
    </div>
		    
</div>


		</div>

<script>
    var ha = $("#anuncio").outerHeight();
   // var otraClaseElems = $("#anuncio").find(".bodyPrincipal");

    //var bp = $('#bodyPrincipal');
    //alert(ha);

    var an = $("#anuncio").children();
    //console.log(an);

    var d_an = $("#div_anuncio_bd").siblings("body");
    //console.log(d_an);


   // var d_con = d_an[0].children();
   // console.log(d_con);
    
    
   //$("#anuncio").css("height",800);
  (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
  })(window,document,'script','//www.google-analytics.com/analytics.js','ga');
  
  ga('create', 'UA-62217121-4', 'auto');
  ga('send', 'pageview');
  setInterval(fnNotificacion, 90000);
</script>
</body>
</html>
