/*
================================================================================================
	CVUSAT
	Fecha de Creación: 16/08/2006
	Fecha de Modificación: 02/03/2007
	Creador: Gerardo Chunga Chinguel
	Obs: Permite validar que el alumno no adelante cursos de ciclos inferiores
================================================================================================
*/

//Declarar array de mensajes de alerta en matrícula
var arrMensajeCiclo= new Array(5);
arrMensajeCiclo[0] ="UD. NO PUEDE RETIRARSE de cursos de ciclos inferiores, ya que es Obligatorio llevarlos.\n Para cambios de grupo horario puede realizarlo en la Dirección de su Escuela Profesional"
arrMensajeCiclo[1] ="Ud. NO PUEDE ADELANTAR ASIGNATURAS DE CICLOS SUPERIORES,\nPRIMERO DEBE ELEGIR ASIGNATURAS DE CICLO INFERIORES"

/*Modificado por gchunga 02/03/2007*/
function ValidarRetiroCursoInferior(codigo_dma)
{
    var curso=document.all.hdcursos //Recorre solo las cabeceras de cursos
    var idCheck=document.getElementById("hd" + codigo_dma);
    
    /*Validar sólo cuando hay varios cursos*/
    if (curso.length!=undefined){
        /*Si el botón analizado va a ser ejecutado*/
	    for (i=0;i<curso.length;i++){
			var Control=curso[i]
			/*Cuenta total de cursos únicos por ciclo Y ES OBLIGATORIO*/
			if (eval(Control.electivo)==0 && eval(Control.ciclo)>0 && eval(Control.value)>0)
			{
				//alert('Actual' + eval(idCheck.ciclo) + '-->> Anteriores ' + eval(Control.ciclo))
				/*Desmarcados menores al analizado y que sea curso único*/
				if (eval(idCheck.ciclo)<eval(Control.ciclo)){
					alert(arrMensajeCiclo[0])
					return 0
				}
			}			
	    }
    }
    return 1
}

function ObtenerGruposCurso(idCheck,chkAnalizado)
{
	var curso=frmFicha.chkcursoshabiles	
	for (i=0;i<curso.length;i++)
	{
	   var Control=curso[i]
	   
	   if (eval(Control.ciclo)>eval(chkAnalizado.ciclo)){
	   		return 0
	   }
	   
	   /*Cuenta total de cursos únicos por ciclo Y ES OBLIGATORIO*/
	   if (Control.checked==true && Control.cc==idCheck.cc && Control.cp!=idCheck.cp)
	   {
		//alert(Control.checked==true + ' ' + Control.cc + '== ' + idCheck.cc + '&&' + Control.cp + '!= ' + idCheck.cp)
		return 1
	   }
	}
	return 0
}

/*Modificado por hzelada y gchunga*/
function ValidarCicloCursoInferior_v1(idCheck)
{
    var curso=frmFicha.chkcursoshabiles
    

    /*Validar sólo cuando hay varios cursos*/
    if (curso.length!=undefined){
        /*Si el check analizado va a ser Marcado*/
        if (idCheck.checked==true){
		    for (i=0;i<curso.length;i++)
		    {
			    var Control=curso[i]
			    /*Cuenta total de cursos únicos por ciclo Y ES OBLIGATORIO*/
		        if (Control.electivo=="0" && Control.checked==false && Control.ciclo!="0" && Control.value!="0")
			    {
				/*Desmarcados menores al analizado y que sea curso único*/
			        if (eval(Control.ciclo)<eval(idCheck.ciclo) && ObtenerGruposCurso(Control,idCheck)==0)
				    { 
					    alert(arrMensajeCiclo[1])
					    return 0
				    }
			    }
		    }
	    }
	    /*Si el check analizado va a ser Desmarcado*/
        if (idCheck.checked==false){
            if (idCheck.electivo==0)//Es Obligatorio
            {
		        for (i=0;i<curso.length;i++)
		        {
			        var Control=curso[i]
			        /*Cuenta total de cursos únicos por ciclo Y ES OBLIGATORIO*/
		            if (Control.electivo=="0" && Control.checked==true && Control.ciclo!="0" && Control.value!="0")
			        {
				        /*Marcados mayores al analizado*/
				        if (eval(Control.ciclo)>eval(idCheck.ciclo))
				        {
					        alert(arrMensajeCiclo[1])
					        return 0
				        }
			        }
		        }
	        }
        }
    }

	return 1
}

/*Modificado por hzelada y jmanay*/
function ValidarCicloCursoInferior_v2(idCheck)
{
	
    if (idCheck.electivo==1 || eval(idCheck.ciclo)==0){
		return 1;
	}	
    var curso=frmFicha.chkcursoshabiles
    var PosAnalizada= eval(idCheck.Posicion) - eval(2);
    var CursoAnterior=curso[0].cc
    var CicloInicial=curso[0].ciclo
    var electivoAnterior = curso[0].electivo	
    var DesMarcado=1
   if (curso[0].electivo==1){
		DesMarcado=0;
	}					
	
    //alert (PosAnalizada)	;
    /*Validar sólo cuando hay varios cursos*/
    if (curso.length!=undefined){

        /*Si el check analizado va a ser Marcado*/
        if (idCheck.checked==true) /*Desmarcados menores al analizado y que sea curso único*/
	{
		    for (i=0;i<=PosAnalizada;i++)
		    {
			var Control=curso[i]

			if (eval(Control.electivo==0) && eval(Control.ciclo)!=0 && Control.value!="0" && eval(Control.ciclo)<eval(idCheck.ciclo))
			{
				if (CursoAnterior!=Control.cc){
					if (DesMarcado==1)
					{
						//alert(Control.nc)
						alert(arrMensajeCiclo[1])
						return 0
					}
					else {
						DesMarcado=1
						CursoAnterior=Control.cc
						CicloInicial = Control.ciclo
						electivoAnterior= Control.electivo
					}
					
				}

			    if (Control.checked==true){
					DesMarcado=0
	    			}
		        	
		        }
		    }
		
		    if (DesMarcado==1 && CicloInicial<eval(idCheck.ciclo))
					{
						//alert(Control.nc)
						alert(arrMensajeCiclo[1])
						return 0
					}
			
	    }
	/*Si el check analizado va a ser Desmarcado*/
        if (idCheck.checked==false){
            if (eval(idCheck.electivo)==0)//Es Obligatorio
            {
		        for (i=0;i<curso.length;i++)
		        {
			        var Control=curso[i]
			        /*Cuenta total de cursos únicos por ciclo Y ES OBLIGATORIO*/
		            if (eval(Control.electivo)==0 && Control.checked==true && eval(Control.ciclo)!=0 && Control.value!="0")
			        {
				        /*Marcados mayores al analizado*/
				        if (eval(Control.ciclo)>eval(idCheck.ciclo))
				        {
					        alert(arrMensajeCiclo[1])
					        return 0
				        }
			        }
		        }
	     }
        }
    }

    return 1
}

/*Modificado por hzelada, gchunga y jmanay*/
function ValidarCicloCursoInferior(idCheck)
{
    var curso=frmFicha.chkcursos //Recorre solo las cabeceras de cursos
    
    /*Validar sólo cuando hay varios cursos*/
    if (curso.length!=undefined){
        /*Si el check analizado va a ser Marcado*/
        if (idCheck.checked==true){
		    for (i=0;i<curso.length;i++)
		    {
			    var Control=curso[i]
			    /*Cuenta total de cursos únicos por ciclo Y ES OBLIGATORIO*/
		        if (eval(Control.electivo)==0 && Control.checked==false && eval(Control.ciclo)>0 && eval(Control.value)>0)
			    {
					/*Desmarcados menores al analizado y que sea curso único*/
			        if (eval(Control.ciclo)<eval(idCheck.ciclo))
				    { 
					    alert(arrMensajeCiclo[1])
					    return 0
				    }
			    }
		    }
	    }
	    /*Si el check analizado va a ser Desmarcado*/
        if (idCheck.checked==false){
        	//Es Obligatorio y curricular
            if ((idCheck.electivo==0) && ((idCheck.tc!="CO" && idCheck.tc!="CC") || (idCheck.value>0)))
            {
		        for (i=0;i<curso.length;i++)
		        {
			        var Control=curso[i]
			        /*Cuenta total de cursos únicos por ciclo Y ES OBLIGATORIO*/
		            if (eval(Control.electivo)==0 && Control.checked==true && eval(Control.ciclo)>0 && eval(Control.value)>0)
			        {
				        /*Marcados mayores al analizado*/
				        if (eval(Control.ciclo)>eval(idCheck.ciclo))
				        {
					        alert(arrMensajeCiclo[1])
					        return 0
				        }
			        }
		        }
	        }
        }
    }
	return 1
}
