var Semanas=new Array();
var Meses=new Array();

function CompararFechas(fechaI,fechaF)
{

	var fechainicio=Date.parse(fechaI)
	var fechafin=Date.parse(fechaF)
	fechainicio=parseInt(fechainicio)/10000000000
	fechafin=parseInt(fechafin)/10000000000
	fechainicio=parseInt(fechainicio)
	fechafin=parseInt(fechafin)

	/*	
	if (fechafin<fechainicio){
		alert("La fecha final debe ser mayor o igual a la fecha inicial")
		var diainicio=fechaI.substr(0,2)
		var mesinicio=fechaI.substr(3,2)
		var anioinicio=fechaF.substr(6,4)		

		if (eval(diainicio)==eval("31"))
			{diainicio="31"}
		document.all.fechafin.value=diainicio + "/" + mesinicio + "/" + anioinicio
		return (false)
	}
	*/
}

function anchotexto(valor)
{
	var texto=""
	var auxiliar=new String(valor)
			    
	if (auxiliar.length==1)
		{texto="0"+valor}
	else
		{texto=valor}
	return texto
}

function VerificarTurno(modo)
{
	if (modo=="I"){
		var hora=document.all.horainicio
		var turno=document.all.turnoinicio
	}
	else{
		var hora=document.all.horafin
		var turno=document.all.turnofin	
	}

	//Activar o Desactivar turno
	if (hora.value==12){
		turno.value="pm"
		turno.disabled=true
	}
	else{
		turno.value="am"
		turno.disabled=false
	}
}


function MostrarCalendario(textbox){
	var cal=document.getElementById("Calendar");
	if(cal!=null)cal.datetype="ES";
	var obj=document.getElementById(textbox);
	if(obj!=null){
		var txt=obj.value;

		// Cargar array de días de semana y meses
		Semanas=Array('dom','lun','mar','mie','jue','vie','sab');
		Meses=Array('Enero','Febrero','Marzo','Abril','Mayo','Junio','Julio','Agosto','Septiembre','Octubre','Noviembre','Diciembre');
		var month=txt.substring(3,5);
		var year=txt.substring(6,10);
		month=month-1
		MostrarCalendarioEx(year,month,textbox);
	}
}
// extended function
function MostrarCalendarioEx(year,month,textbox){
	// init calendar
	var today=new Date();
	if(parseInt(year)!=0&&year!=null&&year!=''&&parseInt(month)!=0&&month!=null&&month!=''){
		today.setFullYear(year,month-1)}
	fillCalendar(today.getMonth(),today.getFullYear());
	var tab=document.getElementById("tabMeses");
	// clear month list
	var m=tab.rows;
	if(m!=null){
		for(var r=m.length;r>-1;r--){
			try{
				tab.deleteRow(m(r).rowIndex);
			}catch(e){}
		}
	}
	// fill month list
	for(var i=0;i<Meses.length;i++){
		var row=tab.insertRow(-1);
		addEvent(row,"mouseover",doItemOver);
		addEvent(row,"mouseout",doItemOut);
		addEvent(row,"click",doMonthClick);
		// month number (first cell)
		var cell=row.insertCell(-1);
		cell.innerText=i+1;
		// month name (last cell)
		var cell=row.insertCell(-1);
		cell.innerText=Meses[i];
	}
	// fill year list (one time)
	var tab=document.getElementById("tabYear");
	var years=tab.rows;
	if(years!=null){
		if(years.length==0){
			for(var i=0;i<150;i++){
				var row=tab.insertRow(-1);
				addEvent(row,"mouseover",doItemOver);
				addEvent(row,"mouseout",doItemOut);
				addEvent(row,"click",doYearClick);
				var cell=row.insertCell(-1);
				cell.innerText=1910+i;
			}
		}
	}
	setCalendar(textbox);
}
// set calendar position from mouse position and show
function setCalendar(textbox){
	var a=document.getElementById("Calendar");
	if(a!=null){
		var sty=a.style;
		if(sty!=null){
			var e=window.event;
			sty.left=e.clientX-10;
			sty.top=e.clientY-10;
			if(parseInt(sty.left)+parseInt(sty.width)>screen.availWidth)
				sty.left=(e.clientX-parseInt(sty.width))+10;
			if(parseInt(sty.top)+parseInt(sty.height)>screen.availHeight)
				sty.top=(e.clientY-parseInt(sty.height))+10;
			a.textbox=textbox;
			sty.display="inline";
		}
	}
}
// fill table month/year/days
function fillCalendar(month,year){
	var tab=document.getElementById("tabCalendar");
	// clear clendar table
	var rows=tab.rows;
	if(rows!=null){
		for(var r=rows.length;r>-1;r--){
			try{
				tab.deleteRow(rows(r).rowIndex);
			}catch(e){}
		}
	}
	// month
	var m=document.getElementById("month");
	m.innerText=Meses[month];
	m.number=month;
	// year
	document.getElementById("year").innerText=year;
	// week names
	var tab=document.getElementById("tabCalendar");
	var row=tab.insertRow(-1);
	for(var i=0;i<Semanas.length;i++){
		var cell=row.insertCell(-1);
		cell.innerText=Semanas[i];
		if(i==0){
			var sty=cell.style;
			if(sty!=null) sty.color="#FF0000";
		}
	}
	// retrieve first week
	var range=new Date();
	range.setFullYear(year,month,1);
	var start=range.getDay();
	// retrieve last day
	range.setMonth(range.getMonth()+1);
	range.setDate(1);
	range.setDate(range.getDate()-1);
	var max=range.getDate();
	// days
	var c=0;
	for(var r=0;r<6;r++){
		var row=tab.insertRow(-1);
		for(var i=0;i<7;i++){
			var cell=row.insertCell(-1);
			if((r==0&&i>=start)||(r>0&&c<max)){
				c++;
				cell.innerText=c;
				cell.align="right";
				addEvent(cell,"mouseover",doDayOver);
				addEvent(cell,"mouseout",doDayOut);
				addEvent(cell,"click",doDayClick);
				if(i==0){
					var sty=cell.style;
					if(sty!=null) sty.color="#FF0000";
				}
			}
		}
	}
}
// make color on mouse over
function doItemOver(){
	var cell=event.srcElement;
	if(cell!=null){
		var row=cell.parentElement;
		if(row!=null){
			var sty=row.style;
			if(sty!=null){
				sty.color="#FFFFFF";
				sty.background="#000080";
				sty.cursor="hand";
			}
		}
	}
}
// restore color on mouse out
function doItemOut(){
	var cell=event.srcElement;
	if(cell!=null){
		var row=cell.parentElement
		if(row!=null){
			var sty=row.style;
			if(sty!=null){
				sty.color="#000000";
				sty.background="#FFFFFF";
				sty.cursor="default";
			}
		}
	}
}
// show month list
function showMeses(){
	var month=document.getElementById("lstMeses");
	if(month!=null){
		var sty=month.style;
		if(sty!=null){
			sty.left=event.clientX-10;
			sty.top=event.clientY-10;
			sty.display="inline";
		}
	}
}
// show year list
function showYears(){
	var year=document.getElementById("lstYear");
	if(year!=null){
		var sty=year.style;
		if(sty!=null){
			sty.left=event.clientX-10;
			sty.top=event.clientY-10;
			sty.display="inline";
		}
	}
}
// hide list
function hideList(){
	var e=event.srcElement;
	if(e!=null){
		var sty=e.style;
		if(sty!=null) sty.display="none";
	}
}
// set new month
function doMonthClick(){
	var cell=event.srcElement;
	if(cell!=null){
		var row=cell.parentElement;
		var year=document.getElementById("year");
		if(row!=null&&year!=null){
			var cells=row.cells;
			if(cells!=null) fillCalendar(parseInt(cells(0).innerText)-1,year.innerText);
		}
	}
	var month=document.getElementById("lstMeses");
	if(month!=null){
		var sty=month.style;
		if(sty!=null) sty.display="none";
	}
}
// set new year
function doYearClick(){
	var e=event.srcElement;
	var month=document.getElementById("month");
	if(e!=null&&month!=null) fillCalendar(month.number,e.innerText);
	var year=document.getElementById("lstYear");
	if(year!=null){
		var sty=year.style;
		if(sty!=null) sty.display="none";
	}
}
// make color on mouse over for single day
function doDayOver(){
	var cell=event.srcElement;
	var sty=cell.style;
	if(sty!=null){
		sty.color="#FFFFFF";
		sty.background="#000010";
		sty.cursor="hand";
	}
}
// restore color on mouse out for single day
function doDayOut(){
	var cell=event.srcElement;
	if(cell!=null){
		var row=cell.parentElement;
		if(row!=null){
			var cells=row.cells;
			var sty=cell.style;
			if(sty!=null&&cells!=null){
				if(cell.innerText==cells(0).innerText){sty.color="#FF0000"
				}else{sty.color="#000000"}
				sty.background="#FFFFFF";
				sty.cursor="default";
			}
		}
	}
}

// ANTERIOR MES --> modificado por gchunga 31/10/2006
function prevMonth(){
	var dt=new Date();
	var year=document.getElementById("year");
	var month=document.getElementById("month");
	var mesAnterior=month.number-1
	var AnioActual=year.innerText

	//Cuando el mes recorrido sea Enero, es un año mes
	if (month.number=="0"){
		month.innerText="Diciembre"
		mesAnterior=11;
		AnioActual=eval(AnioActual)-1
  	}

	fillCalendar(mesAnterior,AnioActual);
}

// PRÓXIMO MES --> modificado por gchunga 31/10/2006
function nextMonth(){
	var dt=new Date();
	var year=document.getElementById("year");
	var month=document.getElementById("month");
	var mesSiguiente=month.number+1
	var AnioActual=year.innerText

	if (mesSiguiente=="12"){
		month.innerText="Enero"
		mesSiguiente=0;
		AnioActual=eval(AnioActual)+1
  	}
	fillCalendar(mesSiguiente,AnioActual);
}

// return date selected and hide calendar
function doDayClick(){
	var day=event.srcElement.innerText;
	var month=document.getElementById("month").number+1;
	var year=document.getElementById("year").innerText;
	var a=document.getElementById("Calendar");
	if(a!=null){
		if(day<10) day="0"+day;
		if(month<10) month="0"+month;
		if(a.textbox!=" "){
			if(a.datetype=="US"||a.datetype=="us"){
				document.getElementById(a.textbox).value=month+"/"+day+"/"+year;
			}else if(a.datetype=="IT"||a.datetype=="it"){
				document.getElementById(a.textbox).value=day+"-"+month+"-"+year;
			}else if(a.datetype=="JP"||a.datetype=="jp"){
				document.getElementById(a.textbox).value=year+"/"+month+"/"+day;
			}else if(a.datetype=="DE"||a.datetype=="de"){
				document.getElementById(a.textbox).value=day+"."+month+"."+year;
			}else if(a.datetype=="FR"||a.datetype=="fr"){
				document.getElementById(a.textbox).value=day+"/"+month+"/"+year;
			}else if(a.datetype=="ES"||a.datetype=="es"){
				document.getElementById(a.textbox).value=day+"/"+month+"/"+year;
			}else if(a.datetype=="PT"||a.datetype=="pt"||a.datetype=="BR"||a.datetype=="br"){
				document.getElementById(a.textbox).value=day+"/"+month+"/"+year;
			}
			closeCalendar();
		}
	}
}
// close calendar
function closeCalendar(){
	var a=document.getElementById("Calendar");
	if(a!=null){
		var sty=a.style;
		if(sty!=null) sty.display="none";
	}
}
// add a event
function addEvent(obj,evname,func){
	try{
		obj.attachEvent("on"+evname,func);
	}catch(e){
		try{
			obj.addEventListener(evname,func,true);
		}catch(e){}
	}
}
//
// document html, calendar popup
var htm= "<div id=\"Calendar\" style=\"BORDER:#000000 2px solid;FONT-SIZE:12px;BACKGROUND:#ffffff;OVERFLOW auto;FONT-FAMILY:Verdana,Arial,Helvetica;POSITION:absolute;DISPLAY:none;Z-INDEX:200;FILTER:progid:DXImageTransform.Microsoft.Shadow(color=\"#000000\",Direction=135,Strength=6)\" textbox=\" \" datetype=\" \">";
htm+= "<table bgcolor=\"#295284\" border=\"0\" style=\"FONT-SIZE:12px;FONT-FAMILY:Verdana,Arial,Helvetica\">";
htm+= "<tr><td width=\"20px\"><input type=\"button\" value=\"<<\"; onclick=\"prevMonth()\" style=\"cursor:hand;width:30px\"></td>";
htm+= "<td align=\"center\" width=\"90px\"><b><span id=\"month\" style=\"color:#FFFFFF;cursor:hand\" onclick=\"showMeses()\" number=\"0\">month</span></b></td>";
htm+= "<td align=\"center\" width=\"40px\"><b><span id=\"year\" style=\"color:#FFFFFF;cursor:hand\" onclick=\"showYears()\">year</span></b></td>";
htm+= "<td width=\"20px\"><input type=\"button\" value=\">>\"; onclick=\"nextMonth()\" style=\"cursor:hand;width:30px\"></td>";
htm+= "<td><input type=\"button\" value=\"X\" onclick=\"closeCalendar()\" style=\"cursor:hand;font-weigh:bold;width:20px\"></td></tr></table>";
htm+= "<table id=\"tabCalendar\" width=\"220px\" style=\"COLOR:#000000;FONT-SIZE:12px;FONT-FAMILY:Verdana,Arial,Helvetica\"><tbody></tbody></table>";
htm+= "</div>";
htm+= "<div id=\"lstMeses\" onmouseleave=\"hideList()\" style=\"WIDTH:120px;HEIGHT:130px;BORDER:#000000 2px solid;BACKGROUND:#ffffff;OVERFLOW:auto;FONT-SIZE:12px;FONT-FAMILY:Verdana,Arial,Helvetica;SCROLLBAR-FACE-COLOR:#295284;SCROLLBAR-SHADOW-COLOR:#003e68;SCROLLBAR-3DLIGHT-COLOR:#295284;SCROLLBAR-ARROW-COLOR:#f7efb5;SCROLLBAR-TRACK-COLOR:#cedef7;SCROLLBAR-DARKSHADOW-COLOR:#000000;SCROLLBAR-HIGTLIGHT-COLOR:#CEDEF7;POSITION:absolute;DISPLAY:none;Z-INDEX:201\"><table id=\"tabMeses\" border=\"0\" width=\"100%\" style=\"COLOR:#000000;FONT-SIZE:12px;FONT-FAMILY:Verdana,Arial,Helvetica\"></table></div>";
htm+= "<div id=\"lstYear\" onmouseleave=\"hideList()\" style=\"WIDTH:70px;HEIGHT:130px;BORDER:#000000 2px solid;BACKGROUND:#ffffff;OVERFLOW:scroll;FONT-SIZE:12px;FONT-FAMILY:Verdana,Arial,Helvetica;SCROLLBAR-FACE-COLOR:#295284;SCROLLBAR-SHADOW-COLOR:#003e68;SCROLLBAR-3DLIGHT-COLOR:#295284;SCROLLBAR-ARROW-COLOR:#f7efb5;SCROLLBAR-TRACK-COLOR:#cedef7;SCROLLBAR-DARKSHADOW-COLOR:#000000;SCROLLBAR-HIGTLIGHT-COLOR:#CEDEF7;POSITION:absolute;DISPLAY:none;Z-INDEX:201\"><table id=\"tabYear\" border=\"0\" width=\"100%\" style=\"COLOR:#000000;FONT-SIZE:12px;FONT-FAMILY:Verdana,Arial,Helvetica\"></table></div>";
document.write(htm);
