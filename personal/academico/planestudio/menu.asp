<% @ LANGUAGE="VBSCRIPT" TRANSACTION =Required%>
<%
'*************************************************************************
'creado el 11/06/05
'*************************************************************************
%>
<html>
<head>
<title>Plan Estudio</title>
<script LANGUAGE="JavaScript">
function validar()
{
  if (frmcursos.cbocurso.selectedIndex < 0){
    alert("Por favor seleccione el curso que busca..")
    frmcursos.cbocurso.focus()
    return (false)
  }
 // frmcursos.submit();
}
function Buscar(s, t){
	timeoutCtr = new Date().getTime();
	listadescriptores  = s;
	txtFind    = t;
	keycode    = event.keyCode;
	setTimeout("Encontrar()", 10);
}
 
function Encontrar() {
	if (new Date().getTime() - timeoutCtr < 0) return false;
	if (txtFind == '' || keycode == 16) {
		return false;
	}

	Aencontrar = txtFind.value
	if(keycode==8) Aencontrar = Aencontrar.substr(0, Aencontrar.length-1);

	if(Aencontrar.length==0){
		txtFind.value='';
		return false;
	}

	allWords    = listadescriptores.options;
	var posLow  = 0;
	var posHigh = allWords.length;
	var foundIt = false;
	s2          = Aencontrar.toLowerCase();
	while (posLow <= posHigh) { 
		posMid = Math.floor((posLow + posHigh) / 2); 
		s1     = allWords[posMid].text;
		s      = allWords[posMid].text.toLowerCase();
		if (s.indexOf(s2) == 0){
			go = true;
			for (var i=posMid; i>=0; i--){
				if(allWords[i].text.toLowerCase().indexOf(s2) == 0){
					s1     = allWords[i].text;
					s      = allWords[i].text.toLowerCase();
				} else {
					break;
				}
			}
			posMid                  = i+1;
			foundIt                 = true;
			listadescriptores.selectedIndex = posMid;
			var t                   = s.length - (s.length - s2.length);
			end_string              = s.substr(t, s.length);
			txtFind.value           = s1;
			if (end_string != "") {
				var range = txtFind.createTextRange();
				range.moveStart('character', txtFind.value.toLowerCase().lastIndexOf(end_string));
				range.select();
			}
 			return true;
		} else {
			if (s2 < s) {
				posHigh = posMid - 1;
			} else {
				posLow  = posMid + 1;
			}
		}
	}
}
function Encontrado(e, obj)
{
	if(e.selectedIndex>=1) obj.value=e[e.selectedIndex].text;
}
function enviarformulario(codcurso,nombrecurso)
{
	var control=frmcursos.cbocurso
	window.parent.frames[2].location.href="plancurso.asp?Codigocurso=" + control.value + "&nomCurso=" + control.options[control.selectedIndex].text
}
function abrirformulario()
{
	window.parent.frames[2].location="Cursos.asp";

}
</script>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<base target="_self">
</head>
<body oncontextmenu="return false" onLoad="document.all.txtcurso.focus()">
<form name="frmcursos" action="" method="post" onSubmit="return validar()">

  <table width="100%" border="0" style="border-collapse: collapse" bordercolor="#111111" cellpadding="3" cellspacing="0" height="381">
    <tr> 
      <td class="usatTitulo" height="16">Escriba el curso a buscar</td>
    </tr>
    <tr> 
      <td height="22"> 
      <input type="text" name="txtcurso" onKeyPress="Buscar(this.form.cbocurso, this);" onFocus="var range = this.createTextRange();range.moveStart('character',0);range.select();" size="41" style="width: 80%">&nbsp; </td>
      <!--onClick="Javascript:enviarformulario()"><img name="Image2" border="0" src="../images/buscar.jpg" width="50" height="15"></td> -->
    </tr>
    <tr> 
      <td height="25">  
        <input type="button" name="cmdBuscar" value="Buscar" onClick="Javascript:enviarformulario()" title="Buscar curso" class="usatBuscar"> 
      <input type="button" value="Agregar Curso" name="cmdCancelar" onClick="abrirformulario()" class="usatnuevo" title="Nuevo Curso"></td>
    </tr>
    <tr> 
      <%
	 	Dim objcursoPlan
		Dim rscursoPlan
		Set objcursoPlan=Server.CreateObject("PryUSAT.clsDatCurso")
		Set rscursoPlan=Server.CreateObject("ADODB.Recordset")
		Set rscursoPlan= objcursoPlan.ConsultarCurso("RS","TO","","")
   		set objcursoPlan=Nothing
	 %>
      <td height="294"> 
      <select name="cbocurso" multiple size="10" style="width: 100%; height: 100%">
          <%
		'*******Generamos el menu desplegable*************************************
		Do While not rscursoPlan.eof%>
          <option value="<%=rscursoPlan(0)%>"><%=rscursoPlan("nombre_Cur")%></option>
          <%rscursoPlan.movenext
		Loop
		%>
        </select></td>
    </tr>
  </table>
</form>

</body>
</html>