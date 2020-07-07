<%@ Page Language="VB" AutoEventWireup="false" CodeFile="listacaracteristicas.aspx.vb" Inherits="administrativo_activofijo_listacaracteristicas" %>
<html id="Html1" lang="en" runat="server">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
    <title>Mantenimiento Caracteristicas</title>

	<!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
    
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/> <!--320-->
	<script type="text/javascript" src="../../assets/js/jquery.js"></script>
	<script type="text/javascript" src="../../assets/js/bootstrap.min.js"></script>	
	<script type="text/javascript" src='../../assets/js/noty/jquery.noty.js'></script>
    <script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>
    <script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>
    <script type="text/javascript" src='../../assets/js/noty/notifications-custom.js'></script>
    <script type="text/javascript" src='../../assets/js/jquery-ui-1.10.3.custom.min.js'></script>
    
    <!-- Manejo de tablas -->
    <script type="text/javascript" src='../../assets/js/jquery.dataTables.min.js'></script>
    <link href="../../assets/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script src="../../assets/js/funcionesDataTable.js?y=1" type="text/javascript"></script>

    <script src="assets/js/funcionesDataTableAF.js" type="text/javascript"></script>
    
    <!-- Piluku -->        
    <link rel="stylesheet" type="text/css" href="../../assets/css/bootstrap.min.css"/>
	<link rel="stylesheet" href="../../assets/css/material.css?x=1"/>		
	<link rel="stylesheet" type="text/css" href="../../assets/css/style.css?y=4"/>
	
	<!-- activo fijo -->
    <link href="assets/css/style_af.css" rel="stylesheet" type="text/css" />

</head>
<script type="text/javascript">
    var lstArt;
    var codigo_art;
    var nombre_art = "";
    
    jQuery(document).ready(function() {

    
        fnResetDataTableTramite('tbCaracteristica', 0, 'desc');
        $("#btnDelReg").click(fnDelRegistro);
        $("#btnConfirmar").click(fnGuardarCaracteristica);
        $("#btnBuscarArt").click(fnBuscarArtCrt);
        $("#btnActualizarArtCrt").click(fnActualizarArtCrt);

        $('#btnGuardarCaract').click(function() {
            limpia();
            $('div#mdCaracteristica').modal('show');
        });


        lstArt = fnCargaLista("lstArticulos");
        var jsonStringA = JSON.parse(lstArt);

        $('#txtArticulo').autocomplete({
            source: $.map(jsonStringA, function(item) {
                return item.d_des;
            }),
            select: function(event, ui) {
                var selectecItem = jsonStringA.filter(function(value) {
                    return value.d_des == ui.item.value;
                });
                codigo_art = selectecItem[0].d_id;
                nombre_art = selectecItem[0].d_des;
                $('#hdArt').val(codigo_art);
                //alert("cod: " + selectecItem[0].d_id + ", nombre: " + selectecItem[0].d_des);
            },
            minLength: 3,
            delay: 100
        });

        $('#txtArticulo').keyup(function() {
            var l = parseInt($(this).val().length);
            if (l == 0) {
                document.getElementById('divOcultoArtCrt').style.display = 'none';
                codigo_art = "";
                nombre_art = "";
            }

        });
        
        

    });

    function fnActualizarArtCrt() {
        var form = document.all("frmListaArtCaracteristicas");
        var contador = 0
        var detalle = ""
        $('#tbArtCrt').DataTable().$('input[type="checkbox"]').each(function() {
            if (this.checked) {
                if (contador > 0) {
                    detalle = detalle + ",";
                }
                detalle = detalle + this.value;
            }
            contador++;
        });
        //alert(detalle);
        $('#hdDetalle').val(detalle)


        $('.piluku-preloader').removeClass('hidden');
        $("input#param0").val("dActualizarCrtArt");
        var form = $('#frmListaArtCaracteristicas').serialize();
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "../../DataJson/activofijo/processactivofijo.aspx",
            data: form,
            dataType: "json",
            success: function(data) {
                //console.log(":" + data);
                fnMensaje("success", data[0].Message);
                $('.piluku-preloader').addClass('hidden');
                //f_Menu("listacaracteristicas.aspx");
                MostrarContenido($('#hdArt').val());
            },
            error: function(result) {
                $('.piluku-preloader').addClass('hidden');
            }
        });
        document.getElementById("param0").value = "";
        $('div#mdDelRegistro').modal('hide');
        
        
    }


    function MostrarContenido(Art) {        
//        var tb = document.getElementById('tbArtCrt');
//        while (tb.rows.length > 1) {
//            tb.deleteRow(1);
        //        }
        if (Art != "") {
            $('.piluku-preloader').removeClass('hidden');
            $("input#param0").val("gLstArtCrt");
            var form = $('#frmListaArtCaracteristicas').serialize();
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "../../DataJson/activofijo/processactivofijo.aspx",
                data: form,
                dataType: "json",
                success: function(data) {
                    aData = data;
                    //console.log(data);
                    var i = 0;
                    var conta = 0;
                    var t = '';
                    var tt = '';
                    $("input#param0").val("gLstArtCrt");

                    $('#hdCount').val(data.length);
                    
                    if (data.length > 0) {
                        //fnResetDataTableTramite('tbArtCrt', 0, 'desc');
                        for (var i = 0; i < data.length; i++) {
                            conta += 1;
                            t += '<tr>';
                            if (data[i].c_chk == "1") {
                                t += '<td><input type="checkbox" name="chkAC['+i+']" value="' + data[i].c_idC + '" checked="vchk" /></td>';
                            } else {
                                t += '<td><input type="checkbox" name="chkAC['+i+']" value="' + data[i].c_idC + '" /></td>';
                            }
                            t += '<td>' + conta + '</td>';
                            //t += '<td>' + data[i].c_idO + '</td>';
                            //t += '<td>' + data[i].c_idC + '</td>';
                            //t += '<td>' + data[i].c_idA + '</td>';

                            t += '<td>' + data[i].d_crt + '</td>';
                            t += '<td>' + data[i].d_art + '</td>';
                            t += '</tr>';
                        }
                    }
                    fnDestroyDataTableDetalle('tbArtCrt');
                    $('#pArtCrt').html(t);
                    fnResetDataTableTramite('tbArtCrt', 0, 'desc');
                },
                error: function(result) {
                    $('.piluku-preloader').addClass('hidden');
                    //f_Menu("listaConfigArticulos.aspx");
                }
            });
            document.getElementById("param0").value = "";
            $('div#mdConfigArticulo').modal('hide');
        }
    }
    
    function fnBuscarArtCrt() {
        var sw = 0;
        var arrayvalida = new Array();
        var i = 0;
        document.getElementById('errorAC[0]').style.visibility = 'hidden';
        if (nombre_art == "") {
            if ($('#txtArticulo').val() == "") {
                sw = 1;
                arrayvalida[0] = "1";
            }
        }
        if (sw == 1) {
            for (i = 0; i < arrayvalida.length; i++) {
                if (arrayvalida[i] == "1") {
                    document.getElementById('error[' + i + ']').style.visibility = 'visible';
                }
            }
            return false;
        }
        else {   
            document.getElementById('divOcultoArtCrt').style.display = 'block';
            MostrarContenido($('#hdArt').val());
        }
    }

    function fnCargaLista(param) {
        try {
            var arr;
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "../../DataJson/activofijo/processactivofijo.aspx",
                data: { "param0": param },
                async: false,
                cache: false,
                success: function(data) {
                    arr = data;
                },
                error: function(result) {
                    arr = null;
                }
            })
            return arr;
        }
        catch (err) {
            //alert(err.message);
            console.log('error');
        }
    }
    
    function fnBorrar(c) {
        document.getElementById('param1').value = c;
        //alert(c);
        $('div#mdDelRegistro').modal('show');
        return true;
    }

    function fnDelRegistro() {
        $('.piluku-preloader').removeClass('hidden');
        $("input#param0").val("dCaracteristica");
        var form = $('#frmListaArtCaracteristicas').serialize();
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "../../DataJson/activofijo/processactivofijo.aspx",
            data: form,
            dataType: "json",
            success: function(data) {
                //console.log(":" + data);
                fnMensaje("success", data[0].Message);
                $('.piluku-preloader').addClass('hidden');
                //                f_Menu("listacaracteristicas.aspx");
                actualizarTabla();
            },
            error: function(result) {
                $('.piluku-preloader').addClass('hidden');
            }
        });
        document.getElementById("param0").value = "";
        $('div#mdDelRegistro').modal('hide');
    }

    function limpia() {
        $('#txtCaracteristica').val("");
        document.getElementById("c5").checked = true;
        document.getElementById('c6').checked = false;
        $('#hdtipoA').val("G");
    }

    function fnEditar(cc, ca, es) {
//        alert(cc + "-" + ca + "-" + es);
        document.getElementById('hdId').value = cc;
        for (i = 0; i <= 1; i++) {
            document.getElementById('error[' + i + ']').style.visibility = 'hidden';
        }
        $("#txtCaracteristica").val(ca);

        if (es == "Activo") {
            document.getElementById("c5").checked = true;
            document.getElementById('c6').checked = false;
        }
        if (es == "Inactivo") {
            document.getElementById("c5").checked = false;
            document.getElementById('c6').checked = true;
        }
        $('#hdtipoA').val("A");

        $('div#mdCaracteristica').modal('show');
        return true;
    }

    function fnGuardarCaracteristica() {
        var sw = 0;
        var arrayvalida = new Array();
        for (i = 0; i <= 1; i++) {
            document.getElementById('error[' + i + ']').style.visibility = 'hidden';
        }
        if ($("#txtCaracteristica").val() == "") {
            arrayvalida[0] = "1";
            sw = 1;
        }
        if (document.getElementById('c5').checked) {
            $("#hdEstado").val($("#c5").val());
        } else if (document.getElementById('c6').checked) {
            $("#hdEstado").val($("#c6").val());
        }
        
        if (sw == 1) {
            for (i = 0; i < arrayvalida.length; i++) {
                if (arrayvalida[i] == "1") {
                    document.getElementById('error[' + i + ']').style.visibility = 'visible';
                }
            }
            return false;
        } else {
            $('.piluku-preloader').removeClass('hidden');
            $("input#param0").val("gAddCaracteristica");
            var form = $('#frmListaArtCaracteristicas').serialize();
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "../../DataJson/activofijo/processactivofijo.aspx",
                data: form,
                dataType: "json",
                success: function(data) {
                    console.log(data);
                    //fnMensaje("success", data[0].Message);
                    fnMensaje(data[0].Status, data[0].Message);
                    $('.piluku-preloader').addClass('hidden');
                    //f_Menu("listacaracteristicas.aspx");
                    actualizarTabla();
                },
                error: function(result) {
                    $('.piluku-preloader').addClass('hidden');
                    //f_Menu("listacaracteristicas.aspx");
                }
            });
            document.getElementById("param0").value = "";
            $('div#mdCaracteristica').modal('hide');
        }
    }

    function actualizarTabla() {
        //alert("entro");
        $('.piluku-preloader').removeClass('hidden');
        $("input#param0").val("lstCrt");
        var form = $('#frmListaArtCaracteristicas').serialize();
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "../../DataJson/activofijo/processactivofijo.aspx",
            data: form,
            dataType: "json",
            success: function(data) {
                aData = data;
                //console.log(data);
                var i = 0;
                var conta = 0;
                var t = '';
                var tt = '';
                if (data.length > 0) {
                    //fnResetDataTableTramite('tbArtCrt', 0, 'desc');
                    for (var i = 0; i < data.length; i++) {
                        conta += 1;
                        t += '<tr>';
                        t += '<td><a href="#" class="btn btn-green btn-xs" onclick="fnEditar(\'' + data[i].c_crt + '\',\'' + data[i].d_crt + '\',\'' + data[i].d_est + '\')" ><i class="ion-edit"></i></a>';
                        t += '<a href="#" class="btn btn-red btn-xs" onclick="fnBorrar(\'' +  data[i].c_crt + '\')" ><i class="ion-android-cancel"></i></a>';
                        t += '</td>';
                        t += '<td>' + conta + '</td>';
                        t += '<td>' + data[i].d_crt + '</td>';
                        t += '<td>' + data[i].d_est + '</td>';
                        t += '</tr>';
                    }
                }
                else {
                    t += '<tr><td colspan=4 style="text-align:center;">';
                    t += "No se encontraron registros";
                    t += '</tr></td>';
                }
                //$('#pArtCrt').html(t);
                if (conta > 0) {
                    //alert("tabla!");
                    fnDestroyDataTableDetalle('tbCaracteristica');
                    $('#pCaracteristica').html(t);
                    //fnResetDataTableTramite('pArtCrt', 0, 'desc');
                    fnResetDataTableTramite('tbCaracteristica', 0, 'desc');
                }
            },
            error: function(result) {
                $('.piluku-preloader').addClass('hidden');
                //f_Menu("listaConfigArticulos.aspx");
            }
        });
        document.getElementById("param0").value = "";
        $('div#mdConfigArticulo').modal('hide');
    
      
    }
    
</script>
<body>
    
<form id="frmListaArtCaracteristicas" name="frmListaArtCaracteristicas" action="#"  > 
    <input type="hidden" id="param0" name="param0" value="" />  
    <input type="hidden" id="param1" name="param1" value="" />
    <input type="hidden" id="hdId" name="hdId" value="" />
    <input type="hidden" id="hdEstado" name="hdEstado" value="" />
    <input type="hidden" id="hdtipoA" name="hdtipoA" value="" />
    <input type="hidden" id="hdArt" name="hdArt" value="" />
    <input type="hidden" id="hdCount" name="hdCount" value="" />
    <input type="hidden" id="hdDetalle" name="hdDetalle" value="" />
    <input type="hidden" id="hdUser" name="hdUser" runat="server" />
    
<div class="col-md-12" >
    <div class="panel panel-piluku">
        <div class="panel-heading">
                <h3 class="panel-title">
                    Mantenimiento de Caracteristicas					
                </h3>
        </div>                            																						
	    <div class="panel-body">									
	        <div class="col-md-12">

                <div role="tabpanel" style=" text-align:center;">
                    <ul class="nav nav-tabs piluku-tabs" role="tablist" >
					    <li role="presentationCrt" class="active" id="lstCrt" runat="server" style="width:49%;"><a href="#lstCrt_tab" aria-controls="home" role="tab" data-toggle="tab" > <div id="labelrps">Listado Caracteristicas</div></a></li>
					    <li role="presentationCrt" id="ConfArtPro"  runat="server" style="width:49%;"><a href="#ConfCrt_tab" aria-controls="profile" role="tab" data-toggle="tab" > <div id="labelrps">Config Articulo-Caracteristica</div></a></li>
				    </ul>
                    <div class="tab-content piluku-tab-content">
                        <div role="tabpanel" class="tab-pane active" id="lstCrt_tab" runat="server" >
                        
	                         <div class='table-responsive'>	        
                                <div class='panel-body' >
                                   <div class='table-responsive'>
                                    <!--Default Form-->
                                                        
                                        <table class='display dataTable cell-border' id='tbCaracteristica' style="width:100%;font-size:smaller;">
                                            <thead>
                                            <tr>
                                                 <th style="width:10%;text-align:center;"></th>
                                                 <th style="width:10%;">Item</th>
                                                 <th style="width:40%;">Caracteristica</th>
                                                 <th style="width:30%;">Estado</th>
                                             </tr>
                                             </thead>     
                                             <tbody id ="pCaracteristica" runat ="server">
                                             </tbody>                             
                                               <tfoot>
                                                <tr>
                                                <th colspan="4"></th>
                                                </tr>
                                                </tfoot>
                                        </table>
                                        
                                    <!--Default Form-->           
                                </div>              
                            </div> 
                                <center>                        
                                    <a href="#" id="btnGuardarCaract" class="btn btn-primary btn-lg" style="width:30%"><i class="ion-android-done"></i>&nbsp;Agregar</a>                
                                </center>    
                                
                            <br>  
                            </div>
                        </div>            
                                        
                        <div role="tabpanel" class="tab-pane " id="ConfCrt_tab" runat="server" >
<%--                            <div class="row">
		                    <div class="form-group">
			                    <label class="col-md-4 control-label">Buscar Por:</label>
			                    <div class="col-md-4">
			                    <select class="form-control" id="cboBuscar" name="cboBuscar" runat="server">							                        
			                        <option value="0">Seleccione</option>
			                        <option value="Crt">Caracteristica</option>
			                        <option value="Prd">Producto</option>
			                    </select>						                    
			                    </div>
		                    </div>
		                    </div>--%>
		                    
<%--		                    <div id="divCrtOculto" style='display:none;'>
                                <div class="row">
                                <div class="form-group">
				                    <label class="col-md-2 control-label">Caracteristica:</label>
				                    <div class="col-md-5">
					                    <input name="txtCaracteristica" type="text" id="txtCaracteristica" value="" class="form-control" />
				                    </div>
                                    <div class="col-md-1">					        
					                    <div class="diverror" id="Div1" style="visibility:hidden"><p>(*)</p></div>
				                    </div>
                                    <div class="col-md-2">					        
					                    <a href="#" id="btnBuscarCrt" class="btn btn-green btn-xs" style="width:100%"><i class="ion-search"></i>&nbsp;Buscar</a>                
				                    </div>
			                    </div>
			                    </div>

		                    </div>--%>
                            
                                <div class="row">
                                <div class="form-group">
				                    <label class="col-md-2 control-label">Articulo:</label>
				                    <div class="col-md-5">
					                    <input name="txtArticulo" type="text" id="txtArticulo" value="" class="form-control" />
				                    </div>
                                    <div class="col-md-1">					        
					                    <div class="diverror" id="errorAC[0]" style="visibility:hidden"><p>(*)</p></div>
				                    </div>
                                    <div class="col-md-2">					        
					                    <a href="#" id="btnBuscarArt" class="btn btn-green btn-xs" style="width:100%"><i class="ion-search"></i>&nbsp;Buscar</a>                
				                    </div>
			                    </div>
			                    </div>
			                 
			                    <br />
                                <div id='divOcultoArtCrt' style='display:none;'>   
                                <div class="row">
                                <div class="form-group">

                                        <table class='display dataTable cell-border' id='tbArtCrt' style="width:100%;font-size:smaller;">
                                            <thead>
                                            <tr>
                                                 <th style="width:5%;text-align:center;"></th>                                                 
                                                 <th style="width:5%;">Item</th>
                                                 <th style="width:20%;">Caracteristica</th>
                                                 <th style="width:50%;">Articulo</th>
                                             </tr>
                                             </thead>     
                                             <tbody id ="pArtCrt" runat ="server">
                                             </tbody>                             
                                               <tfoot>
                                                <tr>
                                                <th colspan="4"></th>
                                                </tr>
                                                </tfoot>
                                        </table>
                                    </div>
			                        </div>
			                        <br />
		                            <div class="modal-footer">
		                              <center>
		                                  <div class="btn-group">			      
		                                        <button type="button" class="btn btn-primary" id="btnActualizarArtCrt" ><i class="ion-android-done"></i>&nbsp;Actualizar</button>			                                    
		                                   </div>
		                              </center>
		                            </div> 
                                </div>
			                    
                        </div>
                    
                    </div>
            
                </div> 
           
            </div>
        </div>	
    </div>	
</div>	


<div class="modal fade" id="mdCaracteristica" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false" style="z-index:0;"> 
<div class="modal-dialog">
	<div class="modal-content">
		<div class="modal-header" style="background-color:#E33439;" >
			<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color:White;"><span aria-hidden="true" class="ti-close" style="color:White;"></span></button>
			<h4 class="modal-title"  style="color:White">Registrar/Actualizar Caracteristica</h4>
		</div>
		<div class="modal-body">
	        <div class="row">
	            <div class="col-md-12" id="divConfirmar">
	            
			        <div class="row">
                    <div class="form-group">
				        <label class="col-md-4 control-label">Caracteristica:</label>
				        <div class="col-md-7">
					        <input name="txtCaracteristica" type="text" id="txtCaracteristica" value="" class="form-control" />
				        </div>
                        <div class="col-md-1">					        
					        <div class="diverror" id="error[0]" style="visibility:hidden"><p>(*)</p></div>
				        </div>
			        </div>
			        </div>	
			        
                    <div class="row">
                    <div class="form-group">
				        <label class="col-md-4 control-label">Estado:</label>
                        <div class="col-sm-5">
							<ul class="list-inline checkboxes-radio">
								<li>
									<input type="radio" name="active" id="c5" value="1" checked/>
									<label for="c5"><span></span>ACTIVO</label>
								</li>
								<li>
									<input type="radio" name="active" id="c6"  value="0" />
									<label for="c6"><span></span>INACTIVO</label>
								</li>
							</ul>
						</div>
                        <div class="col-md-1">					        
					        <div class="diverror" id="error[1]" style="visibility:hidden"><p>(*)</p></div>
				        </div>
			        </div>
			        </div>	
			        				        	        			            		                 
	            </div>		
	        </div>
		</div>
		
		<div class="modal-footer">
		  <center>
		      <div class="btn-group">			      
		            <button type="button" class="btn btn-primary" id="btnConfirmar" ><i class="ion-android-done"></i>&nbsp;Guardar</button>	
		            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="ion-android-cancel"></i>&nbsp;Cancelar</button>		
		       </div>
		  </center>
		</div>
	</div>
</div>
</div>


<div class="modal fade" id="mdDelRegistro" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false" style="z-index: 0;"> 
<div class="modal-dialog">
	<div class="modal-content">
		<div class="modal-header" style="background-color:#E33439;" >
			<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color:White;"><span aria-hidden="true" class="ti-close" style="color:White;"></span></button>
			<h4 class="modal-title"  style="color:White">Confirmar Operaci&oacute;n</h4>
		</div>
		<div class="modal-body">
            <div class="row">
	            <div class="col-md-12" id="">
	                <label class="col-md-12 control-label">Desea Confirmar la Eliminaci&oacute;n del Registro</label>
	            </div>
            </div>
	            
		</div>		
		<div class="modal-footer">
		  <center>
		      <div class="btn-group">			      
		            <button type="button" class="btn btn-primary" id="btnDelReg" ><i class="ion-android-done"></i>&nbsp;Eliminar</button>	
		            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="ion-android-cancel"></i>&nbsp;Cancelar</button>		
		       </div>
		  </center>
		</div>
	</div>
</div>
</div>


</form>
</body>
</html>