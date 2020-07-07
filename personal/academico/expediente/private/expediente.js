
    function validagrado(source, arguments)  {
    var Valor = document.form1.TxtOtrosGrados.value;
    var Valor2 = parseInt(document.form1.DDLGrado.value);
    if ((Valor2==26 || Valor2 == 27 || Valor2 == 28) && Valor=="")    {
        arguments.IsValid = false;
        return; }
    arguments.IsValid=true;}
    
   function mostrarcajagrado(){
    if (eval("document.form1.DDLGrado.value==26") || eval("document.form1.DDLGrado.value==27") || eval("document.form1.DDLGrado.value==28"))
        eval("document.form1.TxtOtrosGrados.disabled=false")
     else
        eval("document.form1.TxtOtrosGrados.disabled=true")
    }
    
    function mostrarcaja2grado(){
    if (eval("document.form1.DDLCentroGrado.value==1") || ( eval("document.form1.DDLCentroGrado.value>=190") && eval("document.form1.DDLCentroGrado.value<=204") ))
        eval("document.form1.TxtOtrosCentroGrados.disabled=false")
     else
        eval("document.form1.TxtOtrosCentroGrados.disabled=true")
    }



function titulo(source, arguments)  {
    var Valor = document.form1.TxtOtrosTitulo.value;
    var Valor2 = parseInt(document.form1.DDLTitulo.value);
    if (Valor2==65 && Valor=="")    {
        arguments.IsValid = false;
        return; }
    arguments.IsValid=true;}
    
    
    function estudios(source, arguments){
    var valor2 = parseInt(document.form1.DDLCentro.value);
    var valor = document.form1.TxtOtros.value;
    if ( (valor2==1 && valor=="") || (valor2>=190 && valor2<=204 && valor=="") )
        {
        arguments.IsValid = false;
        return; }
    arguments.IsValid=true;}

    
    function mostrarcaja(){
    if (eval("document.form1.DDLTitulo.value==65"))
        eval("document.form1.TxtOtrosTitulo.disabled=false")
     else
        eval("document.form1.TxtOtrosTitulo.disabled=true")
    }

    
      function mostrarcaja2(){
        if (eval("document.form1.DDLCentro.value==1") || ( eval("document.form1.DDLCentro.value>=190") && eval("document.form1.DDLCentro.value<=204") ))
            eval("document.form1.TxtOtros.disabled=false")
        else
            eval("document.form1.TxtOtros.disabled=true")
        }
        
        
    function tabsobre(objeto,modo)
        {
            if (modo==1)
                {
                    objeto.className='tab_pasar';
                }
            else
                {
                    objeto.className='tab_normal';
                }
              
        }


    function validaidioma(source, arguments)  
    {
    var Valor = document.form1.TxtOtros.value;
    var Valor2 = parseInt(document.form1.DDLCentro.value);
    if ( (Valor2==1 && Valor=="") || (Valor2>=190 && Valor2<=204 && Valor==""))
        {
        arguments.IsValid = false;
        return; }
    arguments.IsValid=true;
    }
	
	
	function validaotros(source, arguments)  
    {
    var Valor = document.form1.TxtOtroCentroArea.value;
    var Valor2 = parseInt(document.form1.DDLCentroArea.value);
    if ( (Valor2==1 && Valor=="") || (Valor2>=190 && Valor2<=204 && Valor==""))
        {
        arguments.IsValid = false;
        return; }
    arguments.IsValid=true;
    }
	
	
    function mostrarcaja2idioma(){
    if (eval("document.form1.DDLCentro.value==1") || ( eval("document.form1.DDLCentro.value>=190") && eval("document.form1.DDLCentro.value<=204") ))
        eval("document.form1.TxtOtros.disabled=false")
     else
        eval("document.form1.TxtOtros.disabled=true")
    }


    /*
        Funciones agregadas Experiencia Academica
        xDguevara 04.10.2013
    */

    //DDLAnioFinUnv

            function validaexperienciaUnv(source, arguments) {
                var Valor = parseInt(document.form1.DDLMesFinUnv.value);
                var Valor2 = parseInt(document.form1.DDLAnioFinUnv.value);
                if ((Valor2 != 0 && Valor == 0) || (Valor2 == 0 && Valor != 0)) {
                    arguments.IsValid = false; return false;
                }
                else {
                    arguments.IsValid = true;
                }
            }

        /** **/


            function valida2experienciaUnv(source, arguments) {
                if (document.form1.DDLCeseUnv.disabled == false && document.form1.DDLCeseUnv.value == 'Laborando') {
                    arguments.IsValid = false;
                    return false;
                }
                else
                    arguments.IsValid = true;

            }

            function activarUnv() {
                var Valor = parseInt(document.form1.DDLMesFinUnv.value);
                var Valor2 = parseInt(document.form1.DDLAnioFinUnv.value);
                if ((Valor2 != 0 && Valor == 0) || (Valor2 == 0 && Valor != 0) || (Valor2 == 0 && Valor == 0)) {
                    document.form1.DDLCeseUnv.options[0].selected = true;
                    document.form1.DDLCeseUnv.disabled = true;
                }
                else
                    document.form1.DDLCeseUnv.disabled = false;
            }
        
    /*----------------------------------------------------*/


    function validaexperiencia(source, arguments)  
    {

        
        var Valor = parseInt(document.form1.DDLMesFin.value);
        var Valor2 = parseInt(document.form1.DDLAnioFin.value);
        if ((Valor2!=0 && Valor==0) || (Valor2==0 && Valor!=0))
            {
                arguments.IsValid = false; return false; 
            }
            else {
                arguments.IsValid = true; 
            }
    }
    
 function valida2experiencia(source,arguments)
    {
        if (document.form1.DDLCese.disabled==false && document.form1.DDLCese.value=='Laborando')
        {
            arguments.IsValid = false;
            return false;
        }else
            arguments.IsValid = true;
       
    }
    
    function activar()
    {
        var Valor = parseInt(document.form1.DDLMesFin.value);
        var Valor2 = parseInt(document.form1.DDLAnioFin.value);
        if ((Valor2!=0 && Valor==0) || (Valor2==0 && Valor!=0) ||(Valor2==0 && Valor==0))
         {
            document.form1.DDLCese.options[0].selected = true;   
            document.form1.DDLCese.disabled = true;
         }
        else
            document.form1.DDLCese.disabled = false;
    }
    
function duracion(source,arguments)
{
if (document.form1.LstEventos.value == '0' && otros.style.visibility=='visible')
    if (document.form1.TxtHoras.value=="")
        arguments.IsValid = false;
    else
        arguments.IsValid = true;
}

function abrir()
{
var id;
var tipo;
id = document.form1.LstEventos.value;
if (document.form1.RbAcademico.checked == true )
    tipo = 1;
else
    tipo = 2;
showModalDialog("detalleevento.aspx?id="+id + "&tipo=" + tipo,window,"dialogWidth:545px;dialogHeight:170px;status:no;help:no;center:yes;scroll:no")
}

function validacheck(source,arguments)
{
var i;
var fin;
var bandera;
bandera=0;
fin = parseInt(document.form1.HddLista.value)- 1;
for(i=0;i<=fin;i++) {
    if (eval("document.form1.ChkParticipa_" + i + ".checked")== true)
        bandera=1; }       
if (bandera==0)
    arguments.IsValid = false;
else
    arguments.IsValid = true; 
}


function validalista(source,arguments)
{
    if (document.form1.LstEventos.selectedIndex < 0)
        arguments.IsValid = false;
     else
        arguments.IsValid = true;
}


function validaevento(source,arguments)
{
if( document.form1.TxtOtro.value == "" && otros.style.visibility=='visible')
    arguments.IsValid = false;
 else
    arguments.IsValid = true;
}


function validaorganiza(source,arguments)
{
if (document.form1.TxtOrganizado.value == "" && otros.style.visibility=='visible')
    arguments.IsValid = false;
else
    arguments.IsValid = true;
}

function validafecha1(source, arguments)
{
    if (otros.style.visibility=='visible')
        if (document.form1.DDLIniDia.value=='0' || document.form1.DDLIniMes.value=='0' || document.form1.DDLIniAño.value=='0')
            arguments.IsValid = false;
        else
            arguments.IsValid = true;
 }      
 
 function validafecha2(source, arguments)
 {
    if (otros.style.visibility=='visible')
        if (document.form1.DDLFinDIa.value=='0' || document.form1.DDLFinMes.value=='0' || document.form1.DDLFinAño.value=='0')
            arguments.IsValid = false;
        else
            arguments.IsValid = true;
 }
        

function vernuevo()
    {
    if (document.form1.LstEventos.selectedIndex>0)
       { otros.style.visibility = 'hidden';
            //filaocultar.display = 'none'; 
        }
    else
       { otros.style.visibility = 'visible';
            //filaocultar.display = ''; 
        }
    }
function validarnumero()
{
	if (event.keyCode < 45 || event.keyCode > 57)
		{event.returnValue = false}
}