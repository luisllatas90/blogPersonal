<%@ Page Language="VB" AutoEventWireup="false" CodeFile="eventosaulavirtual.aspx.vb" Inherits="academico_eventosaulavirtual" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
<title></title>
<link href='fullcalendar/fullcalendar.css' rel='stylesheet' />
<link href='fullcalendar/fullcalendar.print.css' rel='stylesheet' media='print' />
<script src='lib/jquery.min.js' type="text/javascript"></script>
<script src='lib/jquery-ui.custom.min.js' type="text/javascript"></script>
<script src='fullcalendar/fullcalendar.min.js' type="text/javascript"></script>
<script src="calendarxestudiante.aspx" type="text/javascript"></script>
<style type="text/css">
	body {font-size: 11px;font-family: "Lucida Grande",Helvetica,Arial,Verdana,sans-serif;}
	#calendar{width: 580px;	margin: 0 auto;	float: left;margin-left:15px;font-size:11px;}
	#leyenda {margin-top: 150px; float:left;width:20%; border:1px solid; padding:2px; font-size:10px;}	
</style>
</head>
<body>
<p style=" font-weight:bold; font-size:12px; color:#1F5E70;">CALENDARIO DE ACTIVIDADES EN AULA VIRTUAL</p>
<div id="leyenda"><b>Leyenda:</b><br />
<table cellspacing="2">
<script type="text/javascript">document.write(leyenda);</script>
</table>
</div><br /><br />
<div id='calendar'></div>
</body>
</html>