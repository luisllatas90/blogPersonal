// Archivo JScript

function pintarcelda(celda)
{
    celda.style.backgroundColor = '#FFFFC1'//'#FFFF99'//'#DFEFFF'
}

function despintarcelda(celda)  
{
    celda.style.backgroundColor = ''
}

function regresar(){ 
    var ventanaPadre = window.opener; 
    var campoHija1 = document.getElementById("field"); 
    var campoHija2 = document.getElementById("id"); 
    var campoHija3 = document.getElementById("asignar"); 
    var campoPadre1 = ventanaPadre.getElementById("field"); 
    var campoPadre2 = ventanaPadre.getElementById("id"); 
    var campoPadre3 = ventanaPadre.getElementById("asignar"); 
    campoPadre1.value = campoHija1.value; 
    campoPadre2.value = campoHija2.value; 
    campoPadre3.value = campoHija3.value; 
    window.close()
} 

function CerrarPopup(){
    window.opener.location.reload();
    window.close();
}

function RemarcarCelda(celda1,celda2,celda3)
{
    celda1.style.background = '#4382b4';
    celda1.style.color = '#FFFFFF';
    celda2.style.background = '#DFEFFF';
    celda2.style.color = '#000000';
    celda3.style.background = '#DFEFFF';
    celda3.style.color = '#000000';
}

function NoRemarcarCelda(celda1,celda2,celda3)
{
    celda1.style.background = '#DFEFFF';
    celda1.style.color = '#000000';
    celda2.style.background = '#DFEFFF';
    celda2.style.color = '#000000';
    celda3.style.background = '#DFEFFF';
    celda3.style.color = '#000000';
}

