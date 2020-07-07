/*
==========================================================================================
  CV-USAT
  Fecha de Creación: 17/08/2005
  Fecha de Modificación: 13/12/2010
  Creado por	: Gerardo Chunga Chinguel, modificado por hreyes
  Observación	: Validar Curso Programado para GRABAR en la tabla detalle matricula
==========================================================================================
*/

var arrMensajes= new Array(10);
arrMensajes[0] = "Debe elegir los cursos que Ud. se matriculará";
arrMensajes[1] = "Ud. sólo puede elegir un Grupo de Horario del Curso a Matricularse";
arrMensajes[2] = "¿Está completamente seguro que desea matricular en los cursos elegidos?"
arrMensajes[3]="¿Está completamente seguro que desea retirar la asignatura seleccionada?"

function AbrirCurso(codigo_cur)
{
	var fila=document.all.item("codigo_cur" + codigo_cur)
	var img="../../../../images/mas.gif"
			
	if (fila.length==undefined){
		if (fila.style.display=="none"){
			fila.style.display=""
			img="../../../../images/menos.gif"
		}
		else{
			fila.style.display="none"
		}
	}
	else{
		for (var i=0;i<fila.length;i++){
			var item=fila[i]
			
			if (item.style.display=="none"){
				item.style.display=""
				img="../../../../images/menos.gif"
			}
			else{
				item.style.display="none"
			}
		}
	}

	document.getElementById("img" + codigo_cur).src=img
}

function redondear(cantidad, decimales) {
   // var cantidad = parseFloat(cantidad);
   // var decimales = parseFloat(decimales);
    //decimales = (!decimales ? 2 : decimales);
    //return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);
    //	alert(cantidad+' -- '+decimales);
    var cantidad = parseFloat(cantidad);
    var decimales = parseFloat(decimales);
    //alert(cantidad+' -- '+decimales);
    //decimales = (!decimales ? 2 : decimales);
    //return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);
    //alert(cantidad.toFixed(decimales));
    return cantidad.toFixed(decimales);
}

function pintafilamarcada(idcheck)
{
	
	var _cc= $('#'+idcheck.id).attr('cc');
	var fila=event.srcElement.parentElement.parentElement;

    var curso=document.all.item("curso_padre" + _cc);
	var claseAnterior=curso.clase;
      
    if(idcheck.checked==true){
        fila.className="SelOn";
        curso.className="SelOn";
	}
    else{
	    fila.className="";
	    curso.className=claseAnterior;
	}
   /* var fila=event.srcElement.parentElement.parentElement;

    var curso=document.all.item("curso_padre" + idcheck.cc);
	var claseAnterior=curso.clase;
      
    if(idcheck.checked==true){
        fila.className="SelOn";
        curso.className="SelOn";
	}
    else{
	    fila.className="";
	    curso.className=claseAnterior;
    }*/
}

function DebeCursosAnteriores(idCheck) {
    var sw = 0;
    var CursoGrupo = "";
    var NroControles = parseInt($("input:checkbox[name=chkcursoshabiles]").size());
    var electivo = $(idCheck).attr("electivo");
    var curso = $(idCheck).attr("cc");
    var ciclo = $(idCheck).attr("ciclo");
        
    if (($(idCheck).attr("electivo") == 0)) {
        $(document).ready(function() {
            var ListaControles = $("input:checkbox[name=chkcursoshabiles]").each(function() { return $(this); });

            sw = 0;
            $("input:checkbox[name=chkcursoshabiles]").each(function() {
                if (parseInt($(this).attr("Ciclo")) < parseInt(ciclo)) { //$(this).attr("electivo") == 0
                    //Mientras no tenga el sw activo que siga verificando
                    if (sw == 0) {
                        for (var i = 0; i < NroControles; i++) {
                            var control = ListaControles[i];
                            if ($(control).attr("cc") == $(this).attr("cc") && $(control).attr("electivo") == 0){
                                if ($(control).val() == 0) {
                                    if ($("#llevoidioma").val() == "N") {
                                        if ($(control).is(':checked') == false) {
                                            sw = 1;
                                        } else {
                                            sw = 0;
                                            i = NroControles;
                                        }
                                    }
                                } else {                                    
                                    if ($(control).is(':checked') == false) {
                                        sw = 1;
                                    } else {
                                        sw = 0;
                                        i = NroControles;
                                    }                                
                                }


                            }
                        }
                    }
                }
            });
        });
    }
    

    if (sw == 0) {
        return false;
    } else {
        return true;
    }
}

function ValidaElectivo(idCheck){
    //Validar por ciclo la cantidad mínima permitia
}

function SoloUnComplementario(idCheck) {
    var cont = 0;
    $(document).ready(function() {
        $("input:checkbox[name=chkcursoshabiles]").each(function() {
            if ($(this).attr("tc") == "CC") {
                if ($(this).is(':checked') == true) {
                    cont = cont + 1;
                    return;
                }
            }                        
        });
    });

    if (cont > 1) {
        return false;
    } else {
        return true;
    }
}


function FaltanElectivos() {
    var cont = 0;
    var ListaControles = $("input:checkbox[name=chkcursoshabiles]").each(function() { return $(this); });
    var NroControles = parseInt($("input:checkbox[name=chkcursoshabiles]").size());
    var ultCiclo = 1;
    for (var x = 0; x < NroControles; x++) {
        var con = ListaControles[x];
        if ($(con).is(':checked') == true) {
            if (parseInt($(con).attr("ciclo")) > ultCiclo) {
                ultCiclo = parseInt($(con).attr("ciclo"));
            }
        }
    }

    for (var i = 0; i < NroControles; i++) {
        var control = ListaControles[i];
        if ($(control).attr("electivo") == "1" && parseInt($(control).attr("ciclo")) < ultCiclo
                && parseInt($(control).attr("tipocomp")) != "I") {
            dec = $(control).attr("dec");
            for (var j = 0; j < NroControles; j++) {
                var det = ListaControles[j];
                if ($(det).attr("electivo") == "1" && parseInt($(det).attr("tipocomp")) != "I" 
                    && $(det).is(':checked') == true) {
                    cont++;                    
                }
            }
            if (parseInt(cont) < parseInt(dec)) {
                i = NroControles;
                return "SI";
            }
        }
    }

    return "NO";
}
//obligaComp
function FaltanIdiomas() {
    var cont = 0;
    var ListaControles = $("input:checkbox[name=chkcursoshabiles]").each(function() { return $(this); });
    var NroControles = parseInt($("input:checkbox[name=chkcursoshabiles]").size());
    var ultCiclo = 1;
    for (var x = 0; x < NroControles; x++) {
        var con = ListaControles[x];
        if ($(con).is(':checked') == true) {
            if (parseInt($(con).attr("ciclo")) > ultCiclo) {
                ultCiclo = parseInt($(con).attr("ciclo"));
            }
        }
    }
    
    for (var i = 0; i < NroControles; i++) {
        var control = ListaControles[i];
        if ($(control).attr("tipocomp") == "I" && parseInt($(control).attr("ciclo")) < ultCiclo
            && $(control).attr("obligaComp") == '1'){
            var codCurso = $(control).attr("cc");
            //alert(codCurso);
                var activo = "NO";
                for (var j = 0; j < NroControles; j++) {
                    var grupo = ListaControles[j];
                    if ($(grupo).attr("tipocomp") == "I" && parseInt($(grupo).attr("ciclo")) < ultCiclo
                        && $(grupo).attr("cc") == codCurso && $(grupo).attr("obligaComp") == '1' 
                        && $(grupo).is(':checked') == true) {
                        activo = "SI";          
                    }
                }

                if (activo == "NO") {
                    return "SI";
                }        
        }
    }
       

    return "NO";
}
/*
function FaltanIdiomas() {
    var cont = 0;
    var ListaControles = $("input:checkbox[name=chkcursoshabiles]").each(function() { return $(this); });
    var NroControles = parseInt($("input:checkbox[name=chkcursoshabiles]").size());
    var ultCiclo = 1;
    for (var x = 0; x < NroControles; x++) {
        var con = ListaControles[x];
        if ($(con).is(':checked') == true) {
            if (parseInt($(con).attr("ciclo")) > ultCiclo) {
                ultCiclo = parseInt($(con).attr("ciclo"));
            }
        }
    }

    for (var i = 0; i < NroControles; i++) {
        var control = ListaControles[i];
        if ($(control).attr("tipocomp") == "I" && parseInt($(control).attr("ciclo")) == ultCiclo) {
            dec = $(control).attr("dei");
            for (var j = 0; j < NroControles; j++) {
                var det = ListaControles[j];
                if ($(det).attr("tipocomp") == "I" && $(det).is(':checked') == true) {
                    cont++;
                }
            }
            if (parseInt(cont) < parseInt(dec)) {
                i = NroControles;
                return "SI"
            }
        }
    }

    return "NO"  
}
*/

function Actualizar(idCheck, maxCreditos)
{
    var cursos = 0;
	var creditos=0;
	var totalcur=0;
	var totalcrd = 0;
	var cantCursosMat = 0;
	var credMat = 0;
	var CreditosPension = 0;
	var funcion = $("#tfu").val();;
	var tipoMotivo = $("#hdmotivo").val();
	var chkcur = true;
	var credAnt = 0;
    
	cantCursosMat = $("#cantCursos").val();
	credMat = $("#credMat").val();
	var NroVeces = 0;  
	var size = parseInt($("input:checkbox[name=" + idCheck.name + "]").size());

    if(funcion != 9){
        /* Verificamos si no adelante cursos */
	    if (DebeCursosAnteriores(idCheck) == true) {
	        alert('Debe cursos de ciclos anteriores');
	        $(idCheck).attr('checked', false);
	        chkcur = false;
	    }
    	
        var codigo_cpf = 0;
	    codigo_cpf = $("#txtcodigo_cpf").val();
	    if (parseInt(codigo_cpf) != 2) {
	        if (SoloUnComplementario(idCheck) == false) {
	            alert('Solo puede llevar un curso complementario o de idiomas');
	            $(idCheck).attr('checked', false);
	            chkcur = false;
	        }
	    }	

	    if (validaCursosElectivos(idCheck) == "1") {
	        alert('Debe un curso electivo');
	        $(idCheck).attr('checked', false);
	        chkcur = false;
	    }
    }		
	
	//if (parseInt(codigo_cpf) == 2) {
	    //if (validaCursosElectivosDerecho(idCheck) == "1") {
	    //    alert('Debe un curso electivo de ciclos inferiores');
	    //    $(idCheck).attr('checked', false);
	    //}
    //}
    
    if (size== 0 && $(idCheck.id).is(':checked') == true) {		
        totalcur = 1
        totalcrd = $(idCheck).val()	   		
	}
	else {		
	    creditos = parseInt(credMat)
	    var cursoAdelantar = 0
	    var cursoNivelacion = 0
	    //alert();

	    if (funcion != 9) {
	        if ($(idCheck).is(':checked') == false) {
	            /* No incluye electivos */
	            if (($(idCheck).attr("electivo") == "0") || (parseInt($("#txtcodigo_cpf").val()) == 2)) {
	                /* No incluye complementarios */
	                if (($(idCheck).attr("tc") != "CC" || $(idCheck).attr("tc") != "CO") || parseInt($("#txtcodigo_cpf").val()) == 2) {
	                    if (ValidaCursosSuperiores(idCheck) == "1") {
	                        alert("Tiene seleccionado cursos de ciclos superiores");
	                        $(idCheck).attr('checked', true);
	                        return false;
	                    }
	                }
	            }

	       }
	    }

	    $(document).ready(function() {
	        $("input:checkbox[name=chkcursoshabiles]").each(function() {
	            if ($(this).attr("cc") == $(idCheck).attr("cc") && $(this).attr("id") != $(idCheck).attr("id")) {
	                $(this).attr('disabled', true)
	                chkcur = false;
	            }
	            //agregado por edgard 24/07/2015: no descheckaba y no calculaba al deseleccionar el curso
	            if ($(this).attr("cc") == $(idCheck).attr("cc") && $(idCheck).is(':checked') == false) {
	                $(this).attr('checked', false)
	                $(this).removeAttr("disabled");
	                chkcur = false;
	            }
	            if ($(this).is(':checked')) {
	                creditos = parseInt(creditos) + parseInt($(this).val())
	                cursos = parseInt(cursos) + 1
	                credAnt = parseInt($(this).val());

	                if (parseInt($(this).attr("vd")) >= 2) {
	                    NroVeces = 1;
	                }

	                switch (parseInt($(this).attr("vd"))) {
	                    case 1: CreditosPension = parseFloat(CreditosPension) + parseFloat(redondear(parseFloat($(this).val()) * 1.3, 2)); //parseFloat(Control.vd)
	                        break;
	                    case 0: CreditosPension = parseFloat(CreditosPension) + parseFloat(redondear(parseFloat($(this).val()), 2));
	                        break;
	                    default: CreditosPension = parseFloat(CreditosPension) + parseFloat(redondear(parseFloat($(this).val()) * 1.5, 2)); //parseFloat(Control.vd)
	                        break;
	                }
	            }
	            /*
	            if ($(this).attr("vd") > 0 && $(this).is(':checked') == true) {
	            cursoNivelacion++;
	            } else if ($(this).attr("vd") == 0 && $(this).is(':checked') == true) {
	            cursoAdelantar++;
	            }
	            */
	            var tipo_cac = $("#txttipo_cac").val();
	            if (tipo_cac == "E") {
	                if ($(this).is(':checked') == true) {
	                    var cicloReferencia = $("#lblCicloActual").val();
	                    var MaxAdelantar = $("#maxAdelantar").html();
	                    var MaxNivelar = $("#maxNivelacion").html();
	                    var CursosMatriculados = $("#lblCursosMat").val();

	                    //Si está invicto
	                    if ($("#lblNroCursosDesap").val() == 0) {
	                        if (parseInt($(this).attr("ciclo")) > parseInt(cicloReferencia)) {
	                            cursoAdelantar++;
	                        }

	                        if (parseInt($(this).attr("ciclo")) <= parseInt(cicloReferencia)) {
	                            cursoNivelacion++;
	                        }
	                    } else {    //Si NO está invicto
	                        if (parseInt($(this).attr("ciclo")) <= parseInt(cicloReferencia)) {
	                            cursoNivelacion++;
	                        }
	                    }

	                    if (MaxAdelantar > 0) { // 02-01-2020 EPENA
	                        if (parseInt(cursoAdelantar) + parseInt(CursosMatriculados) > parseInt(MaxAdelantar)) {  // 02/01/2020: se agrego la suma con cursosmatriculados
	                            alert("No puedes adelantar más de " + MaxAdelantar + " cursos");
	                            $(idCheck).attr('checked', false);
	                            cursoAdelantar = cursoAdelantar - 1;
	                            chkcur = false;
	                        }
	                    }

	                    if (MaxNivelar > 0) { // 02-01-2020 EPENA
	                        if (parseInt(cursoNivelacion) + parseInt(CursosMatriculados) > parseInt(MaxNivelar)) {
	                            alert("No puedes nivelar más de " + MaxNivelar + " cursos");
	                            $(idCheck).attr('checked', false);

	                            cursoNivelacion = cursoNivelacion - 1;
	                            chkcur = false;
	                        }
	                    }

	                    if (tipoMotivo == "C" || tipoMotivo == "P") {
	                        if (cursoNivelacion > 5) {
	                            alert("No puedes nivelar más de 1 cursos");
	                            $(idCheck).attr('checked', false);
	                            cursoNivelacion = cursoNivelacion - 1;
	                            chkcur = false;
	                        }

	                        if (cursoAdelantar > 1) {
	                            alert("No puedes Adelantar cursos");
	                            $(idCheck).attr('checked', false);
	                            cursoAdelantar = cursoAdelantar - 1;
	                            chkcur = false;
	                        }
	                    }
	                }


	                //EPENA 02/01/2019: La validacion de nivelacion lo esta haciendo en el for ya no se debe hacer aca
	                /* 	                
	                if (cursoAdelantar > 0) {
	                alert("No puedes adelantar cursos, solo nivelar");
	                $(idCheck).attr('checked', false);
	                cursoAdelantar = cursoAdelantar - 1;
	                }

	                if (cursoNivelacion > 5) {
	                alert("No puedes nivelar más de 5 cursos");
	                $(idCheck).attr('checked', false);
	                cursoNivelacion = cursoNivelacion - 1;
	                }
	                */
	                //EPENA 02/01/2019

	                /*	                
	                //Aprobados
	                if (tipoMotivo == "N" || tipoMotivo == "O") {
	                if (cursoAdelantar > 2) {
	                alert("No puedes adelantar más de 2 cursos");
	                $(idCheck).attr('checked', false);
	                cursoAdelantar = cursoAdelantar - 1;
	                }
	                } else {
	                if (tipoMotivo == "P") {
	                if (cursoAdelantar > 1) {
	                alert("No puedes adelantar más de 1 cursos");
	                $(idCheck).attr('checked', false);
	                cursoAdelantar = cursoAdelantar - 1;
	                }

	                        if (cursoNivelacion > 1) {
	                alert("No puedes nivelar más de 1 cursos");
	                $(idCheck).attr('checked', false);
	                cursoNivelacion = cursoNivelacion - 1;
	                }
	                } else if (tipoMotivo == "C") {
	                if (cursoNivelacion > 1) {
	                alert("No puedes nivelar más de 1 cursos");
	                $(idCheck).attr('checked', false);
	                cursoNivelacion = cursoNivelacion - 1;
	                }

	                        if (cursoAdelantar > 0) {
	                alert("No puedes adelantar más de 1 curso por tener matricula condicional");
	                $(idCheck).attr('checked', false);
	                cursoAdelantar = cursoAdelantar - 1;
	                }
	                }
	                }
	                */
	            }
	        });
	    });
	}

	if (cursos > 0 || totalcur > 0) {
	    $("#cmdAgregar").attr('disabled', false)
	}
	else{
		$("#cmdAgregar").attr('disabled', true)
    }
    
    var credMatVeces = 0;
    if ($("#hdDesaprobado").val() == "S") {
        //credMatVeces = $("#creditosMat").html();
        NroVeces = 1;
    }
    
    if (parseInt(NroVeces) == 1) {
        if (parseInt(creditos) > 15) { 
            //var maxCreditos = $("#txtMaxCred").val();
            alert("No puede exceder el máximo de créditos 15 créditos")
            $(idCheck).attr('checked', false)
        }
    } 

    if (parseInt(creditos) > parseInt(maxCreditos)) {
        var maxCreditos = $("#txtMaxCred").val();
        alert("No puede exceder el máximo de créditos permitidos según reglamento académico. Máximo de créditos a matricular " + maxCreditos)	    
	    $(idCheck).attr('checked', false)
    } else {
        var codigo_cac = 0  
        $("#totalcrd").html(parseInt(creditos) - parseInt(credMat));
        $("#creditosMat").html(creditos)         
        $("#totalcur").html(cursos)
	    codigo_cac = $("#lblCicloAcademico").val()
		pintafilamarcada(idCheck)		
		switch (parseInt($("#txtcodigo_cpf").val())) {		   
             case 24:
		        if ((codigo_cac == "2007-I") || (codigo_cac == "2007-II") ||
		                (codigo_cac == "2008-I") || (codigo_cac == "2008-II")) {
		            if (creditos <= 13) // medicina
		                $("#lblPrecioCiclo").html((parseFloat($("#lblPrecioCurso").val()) * parseInt(CreditosPension) * 5).toFixed(2));
		            else
		                $("#lblPrecioCiclo").html(parseFloat(1300 * 5).toFixed(2));
		        } else{
    		        if (creditos <= 13) // medicina
    		            $("#lblPrecioCiclo").html((parseFloat($("#lblPrecioCurso").val()) * parseInt(CreditosPension) * 5).toFixed(2));
		            else
    		            $("#lblPrecioCiclo").html((1600 * 5).toFixed(2));
		        }		  
		        break    
            case 31:
    		    if (creditos <= 13) // odontologia
    		        $("#lblPrecioCiclo").html((parseFloat($("#lblPrecioCurso").val()) * parseInt(CreditosPension) * 5).toFixed(2));
                else
    		        $("#lblPrecioCiclo").html((750 * 5).toFixed(2));
                break
            default:              	  
               $("#lblPrecioCiclo").html((parseFloat($("#lblPrecioCurso").val())*parseFloat(CreditosPension) *5).toFixed(2));
       }

       if (parseInt(creditos) >= 12) {
           $("#idTipoMatricula").html("Regular");
       } else {
           $("#idTipoMatricula").html("No regular");
       }
       
       //alert(chkcur);
       /*
       if (chkcur = false) {        
           var _t1= $("#totalcrd").html();
           var _crdmat = $("#creditosMat").html();
           var _tcur = $("#totalcur").html(cursos);

           $("#totalcrd").html(parseInt(_t1) - parseInt(credAnt));
           $("#creditosMat").html(_crdmat - credAnt);
           $("#totalcur").html(_tcur - 1);
           
       }
       	*/
	}		
}

function validaCursosElectivosDerecho(check) {
    var retorna = "0";
    var ListaControles = $("input:checkbox[name=chkcursoshabiles]").each(function() { return $(this); });
    var NroControles = parseInt($("input:checkbox[name=chkcursoshabiles]").size());
    
    for (var i = 0; i < NroControles; i++) {
        var control = ListaControles[i];        
        cc = $(control).attr("cc"); //Curso
                     
        /* Si es electivo y no esta seleccionado */
        if ($(control).attr("electivo") == "1" && $(control).is(':checked') == false) {            
            if (parseInt($(control).attr("ciclo")) < parseInt($(check).attr("ciclo"))) {
                retorna = "1";

                /* Verificamos que otro de su grupo este activo */
                for (var j = 0; j < NroControles; j++) {
                    var controlHijo = ListaControles[j];
                    if ($(controlHijo).attr("cc") == cc && $(controlHijo).is(':checked')) {
                        retorna = "0";
                    }                    
                }
            }
            
        }
    }
    
    return retorna;
}

function validaCursosElectivos(control) {
    var retorna = "0";
    
    $(document).ready(function() {
        $("input:checkbox[name=chkcursoshabiles]").each(function() {
            /* Verificamos si existe algun curso seleccionado de ciclos superiores */
            if ($(this).is(':checked') && parseInt($(this).attr("ciclo")) < parseInt($(control).attr("ciclo"))) {

                /* Verificamos si el curso es electivo y obligatorio */
                if ($(this).attr("electivo") == "1" && $(this).attr("obl") == "1") {
                    retorna = "1"
                }                
            }            
        });
    });

    return retorna;
}

function ValidaCursosSuperiores(control) {
    var retorna = "0";
    $(document).ready(function() {
        $("input:checkbox[name=chkcursoshabiles]").each(function() {
            /* Verificamos que el curso este seleccionado */
            if ($(this).is(':checked')) {
                /* No sean complementarios */
                if (($(this).attr("tc") != "CC" || $(this).attr("tc") != "CO") || parseInt($("#txtcodigo_cpf").val()) == 2) {
                    /* No sean electivos */
                    if (($(this).attr("electivo") == "0") || ($(this).attr("electivo") == "1") && parseInt($("#txtcodigo_cpf").val()) == 2) {
                        /* No debe permitir superiores */
                        if (parseInt($(control).attr("ciclo")) < parseInt($(this).attr("ciclo"))) {
                            retorna = "1";
                        }
                    }
                }
            }            
        });
    });

    return retorna;
}

function modificarmatricula(modo,ID)
{
	//alert('nuvea matricula v1 modo2015: '+modo);
	//return false ;
	var pagina=""
	if (modo=='N'){ //Para nueva matrícula
	    pagina = "../academico/matricula/administrar/frmagregarcurso2015.asp?accion=matriculasegura&codigo_pes=" + ID
		window.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
	}
		
	if (modo=='A'){ //Para agregado de matrícula
		pagina="../academico/matricula/administrar/frmagregarcurso2015.asp?accion=agregarcursomatricula&codigo_pes=" + ID
		parent.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
	}

	if (modo == 'R') { //Para retiro de asinaturas	    
	    if (parseInt($("#cbocodigo_mar").val()) > 0) {
	        if (parseInt($("#cbocodigo_mar").val()) == 36) {
	            var texto = $("#txtMotAyR").val();
	            texto = $.trim(texto);
	            if (texto.length > 0) {
	                if (confirm(arrMensajes[3]) == true) {
	                    location.href = "procesarmatricula2015.asp?accion=retirarcursomatricula&codigo_dma=" + ID + "&estado_dma=" + modo + "&codigo_mar=" + $("#cbocodigo_mar").val()
	                }
	            } else {
	                alert("Debe ingresar un motivo de retiro");
	            }
	        } else {
	            if (confirm(arrMensajes[3]) == true) {
	                location.href = "procesarmatricula2015.asp?accion=retirarcursomatricula&codigo_dma=" + ID + "&estado_dma=" + modo + "&codigo_mar=" + $("#cbocodigo_mar").val()
	            }
	        }
	    } else {
	        alert("Debe seleccionar un motivo para retirar al alumno.");
	    }		
	}
}



function BuscarCursosProgramados(codigo_pes)
{
	pagina="../academico/matricula/administrar/frmagregarcurso.asp?accion=agregarcursomatricula&codigo_pes=" + codigo_pes
	window.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
}
function ActualizaPreRequisito(param1, param2, param3, param4) {
	//alert(param1+' '+ param2+' '+ param3+' '+ param4);

	pagina = "../administrar/calculaPreRequisito2015.aspx?param1=" + param1 + "&param2=" + param2 + "&param3=" + param3 + "&param4=" + param4
    //alert(pagina);
    window.location.href = pagina
}
function EnviarFichaMatricula(_codigo_alu,_codigo_pes,_codigo_cac) {
    var cuotas = 0
	//agregado por hreyes
    //Validar que haya marcado la cantidad de cuotas
	/*
	if (document.frmFicha.grupoCuotas[0].checked == true) {
	    cuotas = frmFicha.grupoCuotas[0].value
	}
	else {
	    if (document.frmFicha.grupoCuotas[1].checked == true) {
	        cuotas = frmFicha.grupoCuotas[1].value
	    }
	}*/

   // cuotas = frmFicha.NroCuotas.value
    cuotas = 5
    if ($("#txtAccionMat").val() == "agregarcursomatricula") {
        if (parseInt($("#cbocodigo_mar").val()) < 0) {
            alert("Debe seleccionar un motivo para agregar al alumno.");
            return false;
        } else {
            if (parseInt($("#cbocodigo_mar").val()) == 40) {
                var texto = $("#txtMotAyR").val();
                texto = $.trim(texto);
                if (texto.length == 0) {
                    alert("Debe ingresar un motivo de agregado");
                    return false;
                }
            }            
        }
    }

    if (FaltanElectivos() == "SI") {
        alert("Debe cursos electivos, por favor consultar su plan de estudios");
        return false;
    }

    if (FaltanIdiomas() == "SI") {
        alert("Debe cursos de idiomas, por favor consultar su plan de estudios");
        return false;
    }
    
    //alert(cuotas);
    //return false;
	if (cuotas > 0) { 
	//Mostrar arrMensajes de confirmación
 	  if (confirm("Está seguro que desea agregar los cursos seleccionados a la matrícula")==true){
		//Declarar array de propiedades del check marcado
		var arrCP="" //Array de codigo de curso programado
		var arrVD="" //Array de codigo de curso programado
		//var chk1=frmFicha.chkcursoshabiles
		var totalmarcados=0
		   

		//Recorriendo 1er Iframe Con cursos curriculares/complementarios
 		$(document).ready(function() {
	        $("input:checkbox[name=chkcursoshabiles]").each(function() {	           
	          //$(this).attr("cc") == $(idCheck).attr("cc") && $(this).attr("id") != $(idCheck).attr("id")) {	          	

	        	  if ($(this).is(':checked')) {
		        	  	
		        	  	arrCP+=$(this).attr("cp") + ",";
		        	  	arrVD+=$(this).attr("vd") + ",";
		        	  	totalmarcados=eval(totalmarcados)+1;


	        	  }
	        });
	    });
		

		if (totalmarcados==0){
			alert(arrMensajes[0])
		}
		else{		    
			    //Mostrar arrMensajes de confirmación y enviar datos
			    //procesararrMensajes()
			    //frmFicha.CursosProgramados.value=arrCP
			    //frmFicha.VecesDesprobados.value = arrVD
			    $('#CursosProgramados').val(arrCP);
			    $('#VecesDesprobados').val(arrVD);
			    //alert("ok")
			    var sw=0;

			    $.post("frmcruces.asp", {
						codigo_alu: 		_codigo_alu,
						codigo_pes: 		_codigo_pes,
						codigo_cac: 		_codigo_cac,
						cursosprogramados: 	arrCP
					}, function(data,status) {		            			            
			            if (status=='success'){			            	
			            	$("#divCruces").hide();
			            	$("#divCruces").html(data+'<input type="button" id="btnCerrar" value="Regresar" onclick="cerrarCruce()" class="regresar2">' );		
			            	sw=$('#sw').val();
			            	//alert(sw);

				            if(sw==1){
				            		$("#divCruces").show();				            		
				            		$("#frmFicha").hide();
			            	}
			            	else{
			            		//alert("submit");
			            		$("#divCruces").hide();	
			            		frmFicha.submit();
			            	}
			            }								            

			        });

	
			    //$('#frmFicha').submit();
			    //frmFicha.submit()			
		}
	  }
    }
    else {
        alert("Debe indicar el número de cuotas que desea pagar")
        return (false)
    }
}
function cerrarCruce(){
	$("#divCruces").html("");
	$("#divCruces").hide();
	$("#frmFicha").show();
}
function procesararrMensajes()
{ 
	tblFicha.style.display="none"
	tblmensaje.style.display=""

}

function CalcularCuota(valor, precioCredito) {
	//alert(valor+' '+precioCredito);
	precioCredito=parseFloat($('#lblPrecioCiclo').html());
	//alert(valor+' '+precioCredito);
	//var cuota=0;
	//cuota=parseFloat(precioCredito) / parseFloat(valor);
	//alert(cuota);
    //lblCuota.innerHTML = redondear(parseFloat(parseFloat(precioCredito) / valor), 2);
    $('#lblCuota').html(redondear(parseFloat(parseFloat(precioCredito) / valor), 2));
    //lblCuota.innerHTML =cuota.toFixed(2);
    //alert(valor);
    frmFicha.NroCuotas.value = valor;
}