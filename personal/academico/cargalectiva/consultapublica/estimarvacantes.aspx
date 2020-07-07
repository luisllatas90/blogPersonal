<%@ Page Language="VB" AutoEventWireup="false" CodeFile="estimarvacantes.aspx.vb" Inherits="academico_cargalectiva_consultapublica_estimarvacantes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<script type="text/javascript" src="../private/jquery.js"></script>	
<script type="text/javascript" src="../private/jquery.dataTables.min.js"></script>	
<link rel='stylesheet' href='../private/css/jquery.dataTables.min.css'/>

<script  type="text/javascript" >
    $(document).ready(function() {


        var htmlCpf = $("#cboEscProf").html();
        console.log(htmlCpf);
        $("#cboEscProf").html('<option value="0" selected="selected">-[Selecione]-</option>' + htmlCpf);

        //selectPlanEstudio();
    });
   
   
   
    function selectPlanEstudio() { 
        
            var id = $("#cboEscProf").val();
           // console.log("cbo: " + id);
		   var url_data = "http://"+$(location).attr('hostname')+"/campusvirtual/personal/academico/cargalectiva/consultapublica/post/json.aspx";
		   
		   //console.log(url_data);
            var sOut = '';
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                //url: "post/json.aspx",
				url: url_data,
                data: { "opc": "plnEst", param1: '', param2: id, param3: '' },
                dataType: "json",
                success: function(data) {
                   // console.log("OK: "+ data);                    
                    for (var i = 0; i < data.length - 1; i++) {
                        sOut += '<option value="' + data[i].codigo + '">' + data[i].nombre + '</option>';
                    }
                    //console.log(sOut);
                    $("#cboPlanEst").html(sOut);
                },
                error: function(result) {
                   // console.log("error: "+result);
                }
            });
       
    }

    function calcular() {
    
        var pe = $("#cboPlanEst").val();
        var cp = $("#cboEscProf").val();
        //console.log(pe + '  ' + cp);
        var sOut = '';
        $("#tbDetalle").html('<tr><td colspan="12"><center><img src="../private/images/loading.gif"></center></td></tr>');
        var url_data = "http://" + $(location).attr('hostname') + "/campusvirtual/personal/academico/cargalectiva/consultapublica/post/json.aspx";

       // console.log(url_data);
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            //url: "post/json.aspx",
            url:url_data,
            data: { "opc": "vacCur", param1: pe, param2: '', param3: cp },
            dataType: "json",
            success: function(data) {
                //console.log(data);
                for (var i = 0; i < data.length - 1; i++) {
                    var neca = parseFloat(data[i].necca) * parseFloat(data[i].tdca) +
                    (
                    parseFloat(data[i].neccapr) - (parseFloat(data[i].neccapr) * parseFloat(data[i].tdcapr))
                    )
                    +
                    parseFloat(data[i].nenmap)
                    var _neca = neca.toFixed(4);

                    var necaMax = parseFloat(data[i].necca) * parseFloat(data[i].tdca) +
                    (
                    parseFloat(data[i].neccapr) - (parseFloat(data[i].neccapr) * parseFloat(data[i].tdcapr))
                    )
                    + parseFloat(data[i].nenmap)
                    + parseFloat(data[i].nenmin)
                    var _necaMax = necaMax.toFixed(4);
                    sOut += '<tr style="font-size:11px;"><td>' + data[i].ciclo + '</td>';
                    sOut += '<td style="text-align:left;">' + data[i].nombre + '</td>';
                    sOut += '<td>' + data[i].creditos + '</td>';
                    sOut += '<td>' + data[i].tipo + '</td>';
                    sOut += '<td style="text-align:right;">';
                    sOut += '<input type="hidden" id="txtnecca[' + i + ']" value="' + data[i].necca + '" >' + data[i].necca;
                    sOut += '</td>';
                    sOut += '<td style="text-align:right;">';
                    sOut += '<input type="hidden" id="txttdca[' + i + ']" value="' + data[i].tdca + '" >' + data[i].tdca;
                    sOut += '<td style="text-align:right;">';
                    sOut += '<input type="hidden" id="txtneccapr[' + i + ']" value="' + data[i].neccapr + '" >' + data[i].neccapr;
                    sOut += '<td style="text-align:right;">';
                    sOut += '<input type="hidden" id="txttdcapr[' + i + ']" value="' + data[i].tdcapr + '" >' + data[i].tdcapr;
                    sOut += '<td style="text-align:right;" >' + data[i].nenmap;
                    sOut += '<td style="text-align:right;background-color: #B92828; color: white;">' + _neca;
                    sOut += '<td style="text-align:right;">' + data[i].nenmin;
                    sOut += '<td style="text-align:right;background-color: #B27BB1; color: white;">' + _necaMax;
                    sOut += '</td></tr>';
                }
                //console.log(sOut);
                $("#tbDetalle").html(sOut);
            },
            error: function(result) {
                //console.log(result);
            }
        });
    
    }
    //selectPlanEstudio()
</script>
 <style>
      body {
        font-family: 'Roboto', sans-serif;
        font-size: 13px;
      }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr style="background-color:#6694e3; color: white;">
    <td>&nbsp;Escuela Profesional:&nbsp;<select runat="server" id="cboEscProf" onchange="selectPlanEstudio();"></select>&nbsp;Plan de Estudios:&nbsp;<select runat="server" id="cboPlanEst" onfocus="this.defaultIndex = this.selectedIndex;" onchange="this.selectedIndex = this.defaultIndex;" onkeyup="this.selectedIndex = this.defaultIndex;" style="background-color:Gray; color:White; font-weight:bold;"></select> <input type="button" id="btnAceptar" style="float:right;" value="Mostrar" onclick="calcular()"  runat="server"/></td>
    </tr>
    </table>
    <table width="100%" align="center" cellpadding="0" cellspacing="0">
	<tr valign="top">
	<td width="50%">
	<ul>
	<li><b>neca: </b>Estimado de estudiantes por curso.</li>
	<li><b>necaMax: </b>Estimado de estudiantes por curso incluyendo nenmin.</li>
	<li><b>necca: </b>Estimado de estudiantes por asignatura actual.</li>
	<li><b>tdca: </b>Tasa de desaprobación de la asignatura actual.</li>
	<li><b>neca: </b>necca*tdca+(neccapr-(neccapr*tdcapr))+ nenmap .</li>
	<li><b>necaMax: </b>necca*tdca+(neccapr-(neccapr*tdcapr))+ nenmap + nenmin .</li>
	</ul>
	</td>
	<td>
	<ul>
	<li><b>neccapr: </b>Estimado de estudiantes por asignatura requisito.</li>
	<li><b>tdcapr: </b>Tasa de desaprobación de la asignatura requisito.</li>
	<li><b>nenmap: </b>Estimado de estudiantes que no se matricularon en el curso pero están aptos para llevarlo.</li>
	<li><b>nenmin: </b>Estimado de estudiantes que cumplen el requisito y que se encuentran inactivos por no presentar matrícula cercana al ciclo actual.</li>
	</ul>
	</Td>
	</tr>
	</table>
    <!--<script type="text/javascript" src='../private/dataTable/estimarvacantes.js'></script>-->
    <table width="100%" cellpadding='0' cellspacing='0' border='1' class="display dataTable" id="table">
    <thead style="background-color: #6694e3; color: white;">
    <th style="width:5%">Ciclo</th>
    <th style="width:40%">Nombre Curso</th>   
    <th style="width:5%">Cr&eacute;d</th>
    <th style="width:10%">Tipo Curso</th>
    <th style="width:5%">necca</th>
    <th style="width:5%">tdca</th>
    <th style="width:5%">neccapr</th>
    <th style="width:5%">tdcapr</th>
    <th style="width:5%">nenmap</th>
    <th style="width:5%">neca</th>
    <th style="width:5%">nenmin</th>
    <th style="width:5%">necaMax</th>
    </thead>
    <tbody id="tbDetalle">
    </tbody>
    </table>
    </div>
    </form>
</body>
</html>
