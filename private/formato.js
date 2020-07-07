var ModoHTML=false

function ResaltaVista(num,btn){
	if (num==0){
		ResaltaEntrada(frmEditar.cmdnormal)
		ResaltaSalida(frmEditar.cmdhtml)
		tblherramientas.style.display=""
		document.all.Contenido.style.height="87%"
		if (btn=="no")
			{parent.guardar.disabled=false}
	}
	else{
		ResaltaEntrada(frmEditar.cmdhtml)
		ResaltaSalida(frmEditar.cmdnormal)
		tblherramientas.style.display="none"
		document.all.Contenido.style.height="95%"
		if (btn=="no")
			{parent.guardar.disabled=true}

	}
}

function ResaltaEntrada(eButton){
	eButton.style.backgroundColor = "#EEEEEE";
	eButton.style.borderColor = "darkblue darkblue darkblue darkblue";
	eButton.style.borderWidth = '1px';
	eButton.style.borderStyle = 'solid'; 
	eButton.style.cursor='hand';
}

function ResaltaSalida(eButton) {
	eButton.style.backgroundColor = "#EEEEEE";
	eButton.style.borderColor = "#EEEEEE";
}


function ResaltaAbajo(eButton) {
	eButton.style.backgroundColor = "#EEEEEE";
	eButton.style.borderColor = "darkblue darkblue darkblue darkblue";
}


function ResaltaArriba(eButton) {
	eButton.style.backgroundColor = "#EEEEEE";
	eButton.style.borderColor = "darkblue darkblue darkblue darkblue";
	eButton = null; 

}

function ConvertirHTML() {
	if (ModoHTML==true){
		alert("Debe cambiar a la modalidad vista diseño para Grabar la página web")
		return(false)
	}
	else{
		document.frmEditar.web.value = Contenido.document.body.innerHTML
		return(true)
	}
}

function CargarPagina() {	
	Contenido.document.body.innerHTML=document.frmEditar.web.value
}

	function Negrita() {
		var tr = frames.Contenido.document.selection.createRange()
		tr.execCommand("Bold")
		tr.select()
		frames.Contenido.focus()
	}
	function Subrayado() {
		var tr = frames.Contenido.document.selection.createRange()
		tr.execCommand("Underline")
		tr.select()
		frames.Contenido.focus()
	}
	function Cursiva() {
		var tr = frames.Contenido.document.selection.createRange()
		tr.execCommand("Italic")
		tr.select()
		frames.Contenido.focus()
	}
	function Vinetas() {
		var tr = frames.Contenido.document.selection.createRange()
		tr.execCommand("Insertunorderedlist")
		tr.select()
		frames.Contenido.focus()
	}
	function Numeros() {
		var tr = frames.Contenido.document.selection.createRange()
		tr.execCommand("insertorderedlist")
		tr.select()
		frames.Contenido.focus()
	}
	function AlineaIzquierda() {
		var tr = frames.Contenido.document.selection.createRange()
		tr.execCommand("justifyleft")
		tr.select()
		frames.Contenido.focus()
	}
	function AlineaCentro() {
		var tr = frames.Contenido.document.selection.createRange()
		tr.execCommand("justifycenter")
		tr.select()
		frames.Contenido.focus()
	}
	function AlineaDerecha() {
		var tr = frames.Contenido.document.selection.createRange()
		tr.execCommand("justifyright")
		tr.select()
		frames.Contenido.focus()
	}
	
	function SangriaIzquierda() {
		var tr = frames.Contenido.document.selection.createRange()
		tr.execCommand("indent")
		tr.select()
		frames.Contenido.focus()
	}
	function SangriaDerecha() {
		var tr = frames.Contenido.document.selection.createRange()
		tr.execCommand("outdent")
		tr.select()
		frames.Contenido.focus()
	}
	
	function Superindice() {
		var tr = frames.Contenido.document.selection.createRange()
		tr.execCommand("superscript")
		tr.select()
		frames.Contenido.focus()
	}
	
	function Subindice() {
		var tr = frames.Contenido.document.selection.createRange()
		tr.execCommand("subscript")
		tr.select()
		frames.Contenido.focus()
	}

	function Vinculo() {
		var tr = frames.Contenido.document.selection.createRange()
		var cadReemp='\" title="Haga click para ingresar al Enlace" TARGET="_blank">'
		var nuevohref=""
		var primerhref=""

		tr.execCommand("CreateLink")
		tr.select()		//Asigna el texto que enlazará al link
		primerhref=tr.htmlText 	//URL digitada por el usuario
		nuevohref=primerhref.replace('\">',cadReemp)
		tr.pasteHTML(nuevohref);
		frames.Contenido.focus()
	}


	function VentanaTabla(pagina)
	{
	   var rtNumRows = null;
	   var rtNumCols = null;
	   var rtTblAlign = null;
	   var rtTblWidth = null;
	   showModalDialog(pagina,window,"dialogWidth:350px;dialogHeight:210px;status:no;help:no;center:yes");
	}

	function CrearTabla(){
	   var cursor = Contenido.document.selection.createRange();
	   if (rtNumRows == "" || rtNumRows == "0"){
	      rtNumRows = "1";}
	   if (rtNumCols == "" || rtNumCols == "0"){
	      rtNumCols = "1";}
	   var rttrnum=1
	   var rttdnum=1

	   var rtNewTable = "<table border='" + "1" + "' align='" + rtTblAlign + "' cellpadding='" + "0" + "' style='" + "border-collapse: collapse" + "' bordercolor='" + "#111111" + "' cellspacing='" + "0" + "' width='" + rtTblWidth + "'>"
	   while (rttrnum <= rtNumRows){
		  rttrnum=rttrnum+1
	    	  rtNewTable = rtNewTable + "<tr>"
	      		while (rttdnum <= rtNumCols){
	         		rtNewTable = rtNewTable + "<td>&nbsp;</td>"
			    	  rttdnum=rttdnum+1
 			}
       		rttdnum=1
       		rtNewTable = rtNewTable + "</tr>"
   	    }
   		rtNewTable = rtNewTable + "</table>"
	   	Contenido.focus();
	   	cursor.pasteHTML(rtNewTable);
	}
	
	function EjecutarComando(cmd,opt){
  	if (ModoHTML) {
		alert("No se puede aplicar este formato");
		return;
	}
  	Contenido.document.execCommand(cmd,"",opt);
	Contenido.focus();
	}

	
	function VentanaPaletaColores()	{
	var arr = showModalDialog("Paleta.html",window,"font-family:Verdana; font-size:12; status:no; help:no;dialogWidth:25;dialogHeight:21;center=yes");
	if (arr != null) EjecutarComando("ForeColor",arr);
	}

	
	function AgregarImagen(ide){
		var arrImagen = window.showModalDialog(ide,"","status:no;help:no;dialogWidth:32;dialogHeight:15;center=yes");
		if (arrImagen != null)
		{EjecutarComando("InsertImage",arrImagen)}
	}
		
	
	function VistaPrevia(pagina){
		temp = Contenido.document.body.innerHTML;
		preWindow= open(pagina, 'previewWindow', 'width=500,height=440,status=yes,scrollbars=yes,resizable=yes,toolbar=no,menubar=yes');
		preWindow.document.open();
		preWindow.document.write(temp);
		preWindow.document.close();
	}

	function VistaHTML(modo) {
	var sTmp;
  	ModoHTML = modo;
  	if (ModoHTML){
		sTmp=Contenido.document.body.innerHTML;
		Contenido.document.body.innerText=sTmp;	
		}
	else{
		sTmp=Contenido.document.body.innerText;
		Contenido.document.body.innerHTML=sTmp;
	}
  	Contenido.focus();
	}

	frames.Contenido.document.designMode="On"