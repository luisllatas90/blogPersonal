<!--#include file="../NoCache.asp"-->
<!--#include file="../funciones.asp"-->
<!--<SCRIPT>-->
<!--	AbrirPopUp('../estudiante/avisos/todousat/mejoramosprocesos.pdf','550','950','yes','yes','yes','beca')-->
<!--</SCRIPT>-->
<%
    'Response.Write("<script>alert('antes if')</script>")
    'Response.Write("<script>alert('" & request.QueryString("sw") & "')</script>")
    'if(request.QueryString("sw") <> 1) then       
    '    response.Redirect("AsignaSesionNet.aspx?per=" & session("codigo_usu"))    
    'end if

    If(session("codigo_Usu") = "") Then
        'response.Redirect "../sinacceso.html   
    End If

    Dim total_men
    'if session("codigo_usu")="" then response.redirect "../tiempofinalizado.asp"  

	Set ObjUsuario = Server.CreateObject("PryUSAT.clsAccesoDatos")		
	objUsuario.AbrirConexion
	Set rsAccesos=ObjUsuario.consultar("ConsultarAplicacionUsuario","FO","8",session("tipo_usu"),session("codigo_usu"),"")
	session("dias") = rsAccesos("dias")
	objUsuario.CerrarConexion
	Set ObjUsuario = nothing

    Sub ImprimirMenu(ByVal icono, ByVal tfu, ByVal ctfu, Byval dtfu, ByVal capl, ByVal dapl, ByVal estilo, ByVal texto, ByVal enlace)
	    Dim vinculo

		If capl = "0" Then
			vinculo = "top.location.href='" & enlace & "'"
		ElseIf tfu = "1" Then 'Verificar si solo tiene una funciùn
			vinculo = "top.location.href='abriraplicacion.asp?codigo_tfu=" & ctfu & "&codigo_apl=" & capl & "&descripcion_apl=" & dapl & "&estilo_apl=" & estilo & "'"
		Else
			vinculo = "AbrirFuncion('" & capl & "','" & dapl & "','" & estilo & "')"
			dtfu = ""
		End If
		
		response.write "<table class='Menu' width='200px' border='0' align='center' cellpadding='4' cellspacing='0' onMouseOver=""ResaltarMenuElegido(1,this)"" onMouseOut=""ResaltarMenuElegido(0,this)"" onClick=""" & vinculo & """>" & vbcrlf & vbtab & vbtab
		response.write "<tr>" & vbcrlf & vbtab & vbtab
		response.write "<td height='60' width='5%' rowspan='2'><img src='../images/menus/" & icono & "'></td>" & vbcrlf & vbtab & vbtab
		response.write "<td height='30'>" & dapl & "</td>" & vbcrlf & vbtab & vbtab
		response.write "</tr>" & vbcrlf & vbtab & vbtab
		response.write "<tr>" & vbcrlf & vbtab & vbtab
		response.write "<td height='30' class='MenuDescripcion'>" & ConvertirTitulo(dtfu) & "<br>" &  texto & "</td>" & vbcrlf & vbtab & vbtab
		response.write "</tr>" & vbcrlf & vbtab & vbtab
		response.write "</table>" & vbcrlf & vbtab & vbtab
    End Sub        
%>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=8859-1"/>
    <title>Pùgina Principal del Campus Virtual</title>
    
    <link rel="stylesheet" type="text/css" href="assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" type="text/css" href="assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">    
    <link rel="stylesheet" type="text/css" href="assets/fontawesome-5.2/css/all.min.css"> 
    
    <link rel="stylesheet" type="text/css" href="../private/estilo.css"/>  
    <link rel="stylesheet" type="text/css" href="Alumni/css/sweetalert/sweetalert2.min.css"/> 
    <link rel="stylesheet" type="text/css" href="lytebox.css" media="screen" />  

    <script type="text/javascript" src="../private/funciones.js"></script>
    
    <!--<script type="text/javascript" src="../private/jq/jquery.js"></script> -->
    <script type="text/javascript" src="assets/jquery/jquery-3.3.1.js"></script>  
    <script type="text/javascript" src="assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script type="text/javascript" src="assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>
    <script type="text/javascript" src="assets/iframeresizer/iframeResizer.min.js"></script>
    
    <script type="text/javascript" src="Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
    <script type="text/javascript" src="Alumni/js/sweetalert/sweetalert2.js"></script> 
    <script type="text/javascript" src="lytebox.js"></script>     

    <style type="text/css">
        
        .Menu
        {
            border-style: solid;
            border-width: 0px;
            border-color: #96965E;
            font-weight: bold;
            font-size: 10pt;
            font-family: Arial Narrow;
        }
        .menuElegido
        {
            border-style: solid;
            border-width: 2px;
            border-color: #96965E;
            background-color: #EBE1BF;
            cursor: hand;
            font-weight: bold;
        }
        .menuNoElegido
        {
            border-style: solid;
            border-width: 0px;
            border-color: #96965E;
            background-color: #FFFFFF;
            font-weight: bold;
            font-size: 12pt;
        }
        .MenuDescripcion
        {
            font-size: xx-small;
            font-weight: normal;
        }
        a:link
        {
            text-decoration: none;
        }
        a:visited
        {
            text-decoration: none;
        }
        a:hover
        {
            text-decoration: underline;
        }
        a:active
        {
            text-decoration: none;
        }
        .Estilo1
        {
            color: #FF6600;
            font-weight: bold;
        }
        .barratitulo
        {
            background-color: #D1C490;
            color: #FFFFFF;
            font-size: 13px;
        }
        body
        {
            font-family: Arial, Verdana;
        }
        .AC
        {
            color: black;
            font-size: 14px;
            border-style: none none solid none;
            font-weight: bold;
            border-width: 2px;
        }
        .AD
        {
            color: black;
            font-size: 14px;
            border-style: none none solid none;
            font-weight: bold;
            border-width: 2px;
        }
        .style1
        {
            height: 80px;
        }
        .alert
        {
            padding: 15px;
            margin-bottom: 20px;
            border: 1px solid transparent;
            border-radius: 4px;
        }
        .alert .alert-link
        {
            font-weight: 700;
        }
        .alert-danger
        {
            color: #a94442;
            background-color: #f2dede;
            border-color: #ebccd1;
        }
        .alert-danger .alert-link
        {
            color: #843534;
        }
        .alert-warning
        {
            color: #8a6d3b;
            background-color: #fcf8e3;
            border-color: #faebcc;
        }
        .alert-warning .alert-link
        {
            color: #66512c;
        }
        .alert-success
        {
            color: #3c763d;
            background-color: #dff0d8;
            border-color: #d6e9c6;
        }
        .alert-success .alert-link
        {
            color: #2b542c;
        }
        .alert-info
        {
            color: #31708f;
            background-color: #d9edf7;
            border-color: #bce8f1;
        }
        .alert-info .alert-link
        {
            color: #245269;
        }
        
        .modal-lg-70 {
            max-width: 70% !important;
        }
        
        /* Tabla Asistencia */        
        .tbl_asistencia td 
        {        	    
        	font-size: 8pt;      
        	padding: 5px;     
        	font-weight: bold; 
            text-align: center;              
            border-top-style: solid; 
            border-bottom-style: solid;  
            border-color: Black;       
            border-width: 2px;
            vertical-align: middle; 
            font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;              
        }
        
        .tbl_asistencia .cabecera             
        {        	
            color: white;        	
            background-color: #E33439;                
        }
    </style>

    <script type="text/javascript">
        /* Mostrar mensajes al usuario. */
        function showMessageRedirect(message, messagetype){
            setTimeout(function() {
                swal({ 
                    title: message,
                    type: messagetype ,
                    confirmButtonText: "OK" ,
                    confirmButtonColor: "#45c1e6"     
                }).then(function() {
                    window.top.location.href = "listaaplicaciones.asp";                                                     
                }); 
            }, 1000);            
        }

        function showMessagePage(message, messagetype, page) {                    
            swal({
                title: message,
                type: messagetype,
                showCancelButton: true ,
                confirmButtonText: "SI" ,
                confirmButtonColor: "#45c1e6" ,
                cancelButtonText: "NO"
            }).then(function (isConfirm) {
                if (isConfirm) {
                    //window.location.href = page;
                    window.open(page, '_blank');
                    return true;
                } else {
                    return false;
                }
            });
        }
        
        function refreshPage(){
            window.top.location.href = "listaaplicaciones.asp";   
        }
        
        function confirmarAsistencia(titulo, icono, cpe, nro_doc, tipo){
            swal({
                title: titulo,                
                type: icono,
                showCancelButton: true ,
                confirmButtonText: "SI" ,
                confirmButtonColor: "#45c1e6" ,
                cancelButtonText: "NO"
            }).then(function (isConfirm) {
                if (isConfirm) {      
                    registrarAsistencia(cpe, nro_doc, tipo);     
                    return true;
                } else {
                    return false;
                }
            });            
        }
                
        function registrarAsistencia(cpe, nro_doc, tipo){
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "frmOperacionListaAplicaciones.aspx",
                data: { "operacion": "REGISTRAR_ASISTENCIA", "codigo_cpe": cpe, "nro_documento": nro_doc, "tipo": tipo },
                dataType: "json",
                async: false,
                success: function(data) {                
                    if (data.length > 0) {
                        showMessageRedirect(data[0].mensaje, data[0].tipo_mensaje);                    
                    }else{
                        showMessageRedirect("Ha ocurrido un error, por favor volver a intentar.", "error");
                    }                                        
                },
                error: function(result) {     
                    var msj = encodeURI("La sesiùn ha expirado. Debe iniciar sesiùn nuevamente para poder realizar la marcaciùn.");
                    window.location.href = "frmSinAcceso.aspx?msj=" + msj;
                }
            });            
        }
        
        function ResaltarMenuElegido(op, fila) {
            if (op == 1)
            { fila.className = "menuElegido" }
            else
            { fila.className = "menuNoElegido" }
        }
        
        function fnReglamento() {
            //2020-06-04-ENevado
            location.href = "../librerianet/reglamentos/reglamentos.aspx"
        }

        function AbrirFuncion(capl, dapl, eapl) {
            var ctfu = null;
            var dtfu = null;
            var c_apl = null;
            var d_apl = null;
            var e_apl = null;

            if (window.showModalDialog) {
                showModalDialog("abrirfuncion.asp?codigo_apl=" + capl + "&descripcion_apl=" + dapl + "&estilo_apl=" + eapl, window, "dialogWidth:400px;dialogHeight:250px;status:no;help:no;center:yes");
            } else {
                var modal = window.open("abrirfuncion.asp?codigo_apl=" + capl + "&descripcion_apl=" + dapl + "&estilo_apl=" + eapl, window, "dialogWidth:40px;dialogHeight:50px;status:no;help:no;center:yes");
                modal.dialogArguments = window;
            }
        }

        function AbrirAplicacion() {
            top.location.href = "abriraplicacion.asp?codigo_tfu=" + ctfu + "&codigo_apl=" + c_apl + "&descripcion_apl=" + d_apl + "&estilo_apl=" + e_apl + "&descripcion_tfu=" + dtfu
        }

        function SemanaIngenieria() {
            AbrirPopUp('../estudiante/avisos/ingenieria/nochetalentos.html', '550', '950', 'yes', 'yes', 'yes', 'beca')
        }

        function Mayo() {
            AbrirPopUp('../avisos/capellania/13mayo.html', '600', '450', 'yes', 'yes', 'yes', '13mayo')
        }

        function Conferencia() {
            AbrirPopUp('../avisos/progEsp/becas/universia/seg_confPER.asp', '600', '650', 'yes', 'yes', 'yes', 'beca')
        }

        function corpus() {
            AbrirPopUp('../avisos/capellania/corpusChristi.html', '760', '630', 'yes', 'yes', 'yes', 'CORPUS')
        }

        function ReglamentoSST() {
            //AbrirPopUp('../avisos/progEsp/becas/universia/seg_confPER.asp', '600', '650', 'yes', 'yes', 'yes', 'beca')
            window.open('../librerianet/reglamentos/PUBLICACIùN RISST 2019.pdf', '_blank')
        }

        function AceptarReglamento() {
            var isChecked = document.getElementById('chkTerminos').checked;
            if (isChecked) {
                var u = '<%=session("codigo_Usu") %>'
                if (u != '') {
                    var pagina = 'AceptarReglamento.aspx?u=' + u
                    window.open(pagina, '_blank')
                    window.location.reload();
                } else {
                    alert('Su sesiùn ha expirado, debe volver a ingresar para poder procesar la peticiùn.')
                }
            } else {
                alert('Debe Aceptar haber leùdo y conocer las actualizaciones del reglamento')
            }
        }

        function cargarModal(titulo, ancho_modal, pagina) {
            $('#lblTituloModal').html(titulo);
            ancho_modal = 'modal-lg-' + ancho_modal;
            $('#mddMostrarPagina').addClass(ancho_modal);
            var frame = $('#ifrmContenido');
            var url = pagina;
            frame.attr('src', url).show();
            $('#mdlMostrarPagina').modal('show');
            resizeIframe();            
        }

        function resizeIframe() {
            $('#ifrmContenido').iFrameResize({

        });

        function cargarAvisoPersonal() {
            $('#mdlAvisoPersonal').modal('show');
        }
    } 
    </script>        
</head>
<body oncontextmenu="return event.ctrlKey" style="margin: 0px">
    <table width="100%" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse">
        <tr>
            <td width="100%" height="56px" style="background-image: url('Images/sup5.png'); background-repeat: no-repeat;
                color: #FFFFFF; text-align: right;">
                <b>Usuario:
                    <%=session("nombre_usu")%>&nbsp;<%=session("Usuario_bit")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <a href="cerrar.asp" style="font-weight: bold; color: yellow">[Cerrar sesiùn X]</a></b>
            </td>
        </tr>
    </table>
    <br />

    <script type="text/javascript" language="javascript" src="lytebox.js">
        AbrirPopUp('/images/identidadUSAT.jpg', '1000', '1000', 'yes', 'yes', 'yes', 'IdentidadUSAT')
    </script>

    <table width="100%" border="0" cellpadding="4" cellspacing="0">
        
<% 
    'COMUNICADO DE AUTOEVALUACIùN
    
    Set ObjAutoevaluacion = Server.CreateObject("PryUSAT.clsAccesoDatos")
    ObjAutoevaluacion.AbrirConexion
    Set rsAutoevaluacion = ObjAutoevaluacion.consultar("PER_ComunicadoPersonalListar","FO","EVA","",session("codigo_usu"),"0","N")                        
    
    If rsAutoevaluacion.recordcount > 0 Then                
        Response.Write("<script>showMessagePage('Usted tiene pendiente su encuesta de autoevaluaciùn. \n ùDesea realizarla en este momento?', 'warning', 'academico/encuesta/EncuestaEvaluacionDocente/ResponderEncuestaAuto.aspx')</script>")
    End If
    
    ObjAutoevaluacion.CerrarConexion
    Set ObjAutoevaluacion = Nothing                           
%>        
                                                                
        <tr>
            <td width="80%" valign="top" style="padding-left: 3px; padding-top: 3px">
                <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                
<% 
    'VERIFICAR  QUE EL PERSONAL MARQUE ASISTENCIA
    Set ObjMarcarAsistencia = Server.CreateObject("PryUSAT.clsAccesoDatos")
    ObjMarcarAsistencia.AbrirConexion
    Set rsMarcarAsistencia=ObjMarcarAsistencia.consultar("PER_ControlPersonalListar","FO", "NMA", "", session("codigo_usu"), "")  
    
    If (rsMarcarAsistencia.recordcount = 0  And Now() > cdate("05/10/2020")) Then
%>                
                
        <!-- INICIO MARCACIùN DE ASISTENCIA -->   
                    <tr><td class="AC">Registro de Asistencia - <span style="font-size: 11px;">⁄ltima actualizaciÛn: <% response.write Now() %></span></td></tr>
                    <tr id="tbAsistencia">            
                        <td valign="top">
                            <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td valign="top">                                                                                                            
                                        <tr><td>&nbsp;</td></tr>
<% 
        Set ObjAsistencia = Server.CreateObject("PryUSAT.clsAccesoDatos")
        ObjAsistencia.AbrirConexion
        Set rsAsistencia=ObjAsistencia.consultar("PER_ControlPersonalListar","FO", "ASI", "", session("codigo_usu"), "")

        If rsAsistencia.recordcount > 0 Then    
%>                            
                                        <tr>
                                            <td align="center">
                                                <table class="tbl_asistencia" style="width: 80%;">                                                                
                                                    <tr class="cabecera">
                                                        <td>HORARIO</td>
                                                        <td>INGRESO</td>
                                                        <td>SALIDA</td>
                                                    </tr>
<% 
                                    Do While Not rsAsistencia.EOF
%>
                                                    <tr>
                                                        <td style="color: Black;"><% response.write rsAsistencia("descripcion_horario") %></td>                                                        
<% 
                                        If rsAsistencia("tipo_operacion") = "I" Then
%>                                 
                                                         <td>           
                                                            <a class="btn btn-success active" onclick="return confirmarAsistencia('ùRealmente desea registrar su ingreso?', 'warning', '<%response.write rsAsistencia("codigo_cpe")%>', '<%response.write rsAsistencia("nroDocIdentidad_per")%>', 'I');" role="button">
                                                                <span><i class="fa fa-toggle-on"></i></span>
                                                            </a>
                                                         </td>
                                                         <td>
                                                            <a class="btn btn-secondary active" role="button">
                                                                <span><i class="fa fa-toggle-off"></i></span>
                                                            </a>                                                                                                                                                                         
                                                         </td>   
<% 
                                        ElseIf rsAsistencia("tipo_operacion") = "F" Then
%>                                                                    
                                                        <td>        
                                                            <a class="btn btn-secondary active" role="button">
                                                                <span><i class="fa fa-toggle-on"></i></span>
                                                            </a>
                                                        </td>
                                                        <td>                                                                                                                                                                                                           
                                                            <a class="btn btn-danger active" onclick="return confirmarAsistencia('ùRealmente desea registrar su salida?', 'warning', '<%response.write rsAsistencia("codigo_cpe")%>', '<%response.write rsAsistencia("nroDocIdentidad_per")%>', 'F');" role="button">
                                                                <span><i class="fa fa-toggle-off"></i></span>
                                                            </a>                                                                                                            
                                                        </td>   
<% 
                                        ElseIf rsAsistencia("tipo_operacion") = "N" Then
%>                                                                    
                                                        <td>        
                                                            <a class="btn btn-secondary active" role="button">
                                                                <span><i class="fa fa-toggle-on"></i></span>
                                                            </a>
                                                        </td>
                                                        <td>
                                                            <a class="btn btn-secondary active" role="button">
                                                                <span><i class="fa fa-toggle-off"></i></span>
                                                            </a>                                                                                                                                                                         
                                                        </td>                                                                                                                                                                
<%                
                                        End If
%>
                                                    </tr>
<%                                        
                                        rsAsistencia.movenext
                                    Loop                
%>                                     
                                                </table>
                                            </td>
                                        </tr>
<% 
        End If
        
        ObjAsistencia.CerrarConexion
        Set ObjAsistencia = Nothing
%>                            
                                        <tr><td>&nbsp;</td></tr>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>    
        <!-- FIN MARCACIùN DE ASISTENCIA -->                   
<% 
    End If
    
    ObjMarcarAsistencia.CerrarConexion
    Set ObjMarcarAsistencia = Nothing
%>    
                
<%
    'REGLAMENTO INTERNO DE SEGURIDAD Y SALUD EN EL TRABAJO
    Set ObjUsuario = Server.CreateObject("PryUSAT.clsAccesoDatos")		
    objUsuario.AbrirConexion
    Set rsRpta = ObjUsuario.consultar("ConsultarAceptacionReglamento","FO","1",session("codigo_usu"))
	
	If rsRpta("nro") = "0" Then
%>
                    <tr id="tbReglamento">
                        <td width="80%" valign="top" style="padding-left: 3px; padding-top: 0px">
                            <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td valign="top">
                                        <div class="alert alert-danger" style="padding-top: 0px">
                                            <h3>
                                                Reglamento Interno de Seguridad y Salud en el Trabajo(RISST) V.02 &nbsp;
                                                <button onclick="ReglamentoSST()">
                                                    <img src="images/descargar_reglamento.png" height="30" width="30" />
                                                </button>
                                            </h3>
                                            <input type="checkbox" id="chkTerminos" checked="checked" />
                                            <label style="font-weight: bold;">
                                                He leùdo y conozco las actualizaciones del reglamento</label>
                                            <br />
                                            <br />
                                            <input type="button" style="align: right" value="Aceptar" onclick="AceptarReglamento()"
                                                class="btn btn-primary" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
<%
    End If

	objUsuario.CerrarConexion
	Set ObjUsuario=nothing
%>                
                
<% 
    'COMUNICADO DE TRABAJO REMOTO
    
    Set ObjComunicado = Server.CreateObject("PryUSAT.clsAccesoDatos")
    ObjComunicado.AbrirConexion
    Set rsComunicado=ObjComunicado.consultar("PER_ComunicadoPersonalListar","FO","CP1","",session("codigo_usu"),"3","S")                        
    
    If rsComunicado.recordcount > 0 Then                    
%>                        
                    <tr id="tbComunicadoPersonal">
                        <td width="80%" valign="top" style="padding-left: 3px; padding-top: 0px">
                            <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td valign="top">
                                        <div class="alert alert-danger" style="padding-top: 0px">
                                            <h3>
                                                Condiciones de Trabajo Remoto &nbsp;
                                                <button onclick="window.open('ComunicadoPersonal/frmComunicadoPersonalPDF.aspx?codigo_usu=<%response.write session("codigo_usu")%>&num_comunicado=3&tipo_comunicado=TRABAJO_REMOTO');">
                                                    <img src="images/descargar_reglamento.png" height="30" width="30" />
                                                </button>
                                            </h3>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>        
<% 
    End If
    
    ObjComunicado.CerrarConexion
    Set ObjComunicado = Nothing
%>                
                
<% 
    'COMUNICADO DE REGLAMENTO INTERNO DE TRABAJO
    
    Set ObjComunicado = Server.CreateObject("PryUSAT.clsAccesoDatos")
    ObjComunicado.AbrirConexion
    Set rsComunicado=ObjComunicado.consultar("PER_ComunicadoPersonalListar","FO","RIT","",session("codigo_usu"),"4","N")                        
    
    If rsComunicado.recordcount = 0 Then                    
%>  
                    <tr id="tbComunicadoRIT">
                        <td width="80%" valign="top" style="padding-left: 3px; padding-top: 0px">
                            <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td valign="top">
                                        <div class="alert alert-danger" style="padding-top: 0px">
                                            <h2 style="text-align: justify;"><strong>Reglamento Interno de Trabajo</strong></h2>
                                            <p style="text-align: justify; padding: 0px; font-size: small;">
                                                Se les comunica que se encuentra disponible en el Campus la ùltima versiùn del Reglamento Interno de Trabajo aprobada y registrada por la Autoridad Administrativa de Trabajo. 
                                                <br/>Asimismo, este Reglamento ha sido publicado en el portal de transparencia USAT.
                                                <br/>Agradecerù se sirvan revisarlo a fin de que conozcan las nuevas disposiciones contenidas en esta Normativa.
                                            </p>                                                                
                                            <p style="text-align: justify; padding: 0px; font-size: small;">Atentamente,</p>
                                            <p style="text-align: justify; padding: 0px; font-size: small;">
                                                <strong>Maria Cecilia Mendoza Diaz</strong>
                                                <br/>Directora de Personal
                                            </p>                                
                                            <button onclick="window.open('ComunicadoPersonal/frmComunicadoPersonalPDF.aspx?codigo_usu=<%response.write session("codigo_usu")%>&num_comunicado=4&tipo_comunicado=RIT');">
                                                <img src="images/descargar_reglamento.png" height="30" width="30" />
                                            </button>                                
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr> 
<% 
    End If
    
    ObjComunicado.CerrarConexion
    Set ObjComunicado = Nothing
%>                                                                  
                    <tr>
                        <td valign="top">
                            <table style="width: 100%;">                                                                                        
<%
    response.write("<tr><td class='AC' colspan=3>Sistemas Acad&eacute;micos</td></tr>")

    codigo_apl = -1
    i = 0
    total = rsAccesos.recordcount

    Do while not rsAccesos.EOF
        If rsAccesos("tipo_apl") = "AC" Then
        
            ''Verificar que no se repita la aplicaci&oacute;n
            If codigo_apl<>rsAccesos("codigo_apl") Then
                codigo_apl=rsAccesos("codigo_apl")
            ElseIf not(rsAccesos.EOF) Then
                'Pasar al siguiente registro
                rsAccesos.movenext
                codigo_apl=-1
            End If
            
            'Salir del bucle sin terminaron los registros
            If (rsAccesos.EOF) Then
                Exit do
            End If
    		
	        If (i=0) Then
	            response.write ("<tr>")
	        ElseIf (i mod 3 = 0) Then
	            response.write ("</tr><tr>")
	        End If
    	    
	        response.write "<td>"
            
            ImprimirMenu rsAccesos("icono_apl"),rsAccesos("total_tfu"),rsAccesos("codigo_tfu"),rsAccesos("descripcion_tfu"),rsAccesos("codigo_apl"),rsAccesos("descripcion_apl"),rsAccesos("estilo_apl"),rsAccesos("obs_apl"),rsAccesos("enlace_apl")
            
            response.write "</td>"
            
            'if i=1 then
            '    response.write ("</tr>")
            'end if
            
            i = i+1
        End If
        
        rsAccesos.movenext
    Loop
%>
        </tr>
        <tr>
            <td>
            <!--REGLAMENTO -->
<% 
    'condicion por sunedu 19/10/2017
    If session("Usuario_bit") <> "USAT\SUNEDU" Then    
%>
                <table onclick="fnReglamento()" onmouseover="ResaltarMenuElegido(1,this)" onmouseout="ResaltarMenuElegido(0,this)"
                    class="menuNoElegido" cellspacing="0" cellpadding="4" width="200" align="center"
                    border="0">
                    <tbody>
                        <tr>
                            <td height="60" rowspan="2" width="5%">
                                <img src="../librerianet/reglamentos/0.jpg" width="50px" height="50px">
                            </td>
                            <td height="30">
                                REGLAMENTOS USAT
                            </td>
                        </tr>
                    </tbody>
                </table>
<% 
    End If 
%>
            </td>
<%
	response.write ("<tr><td>&nbsp;</td></tr><tr><td class='AD' colspan=3>Sistemas Administrativos</td></tr>")
    '-----------------------------
    rsAccesos.moveFirst
    i = 0
    
    Do While not rsAccesos.EOF
        If rsAccesos("tipo_apl") = "AD" Then
        
            ''Verificar que no se repita la aplicaci&oacute;n
            If codigo_apl<>rsAccesos("codigo_apl") Then
                codigo_apl=rsAccesos("codigo_apl")
            ElseIf not(rsAccesos.EOF) Then
                'Pasar al siguiente registro
                rsAccesos.movenext
                codigo_apl=-1
            End If
            
            'Salir del bucle sin terminaron los registros
            If (rsAccesos.EOF) Then
                Exit Do
            End If
    	
	        If (i=0) Then
	            response.write ("<tr>")
	        ElseIf (i mod 3 = 0) then
	            response.write ("</tr><tr>")
	        End If
	        
	        response.write "<td>"
            ImprimirMenu rsAccesos("icono_apl"),rsAccesos("total_tfu"),rsAccesos("codigo_tfu"),rsAccesos("descripcion_tfu"),rsAccesos("codigo_apl"),rsAccesos("descripcion_apl"),rsAccesos("estilo_apl"),rsAccesos("obs_apl"),rsAccesos("enlace_apl")
            response.write "</td>"
            
            i = i+1
        End If
        
        rsAccesos.movenext
    Loop
    '----------------
    Set accesos = nothing
	        
%>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20%" valign="top" style="border-left: 0px solid #ff6600; border-width: 0 0 0 1px; border-left-color: #808080;">
                <!-- Eliminar form pasado el almuerzo USAT el 15/10/2012-->
                <form id="almuerzo" action="solicitaentrada.asp" method="post">
                    <table width="100%" border="0" cellspacing="1" cellpadding="1">
                    <!--Aquù va el codigo de Avisos Campus que proceden de una BDatos-->
                    <!---INI--->
                    <!--Inicio: Aviso de caducidad de contrase?a-->            
<% 
    If session("dias") > 0 Then
    
        If session("dias")<15 Then
            strCss="danger" 
        ElseIf session("dias")>15 and session("dias")<90  Then
            strCss="info" 
        Else
            strCss="success" 
        End If
%>
                        <tr>
                            <td>
                                <div class="alert alert-<%=strCSS%>">
                                    Te quedan
                                    <%=session("dias")%>
                                    dùas para cambiar tu contraseùa.
                                    <br />
                                    <a href="servicios/GuiaCambioPassword.pdf" target="_blank" class="alert-link" title="ùCùmo lo hago?">
                                        <span style="color: #d21d22; text-decoration: underline; margin-top: 3px; display: inline-block;
                                            cursor: pointer" onmouseover="this.style.color='#900206';" onmouseout="this.style.color='#d21d22';">
                                            ùCùmo cambiar mi contraseùa?</span></a>
                                </div>
                            </td>
                        </tr>
<%
    End If
%>
    <!--Fin: Aviso de caducidad de contrase?a-->
    <!--<tr><td><b><br />Encuesta RED ODUCAL</td></tr>
    <tr><td><a href="https://drive.google.com/open?id=1HBLxyLupeB9R5xbcqv8csW_JHth_q22xM4EUiYE_adY">Para Personal Docente</a></td></tr>
    <tr><td><a href="https://drive.google.com/open?id=1wT4ZzLeB9CRgFilW41gvUkCwk-7f2IZ9LMppPTdUTxA">Para Personal Administrativo / Servicios<br /><br /></a></td></tr>-->
						<!--<tr style="border-top: 1px solid #2c2a2a;">
							<td>
								<a href="https://intranet.usat.edu.pe/encuestas/index.php/175489?newtest=Y&lang=es" target="_blank" title="Clic aqu&iacute; para ingresar a la encuesta">
									<img style="border-color: red;" src="Images/anuncio_identidad_catolica.jpeg"
										alt="Clic aqu&iacute; para ingresar a la encuesta" width="370" height="425" border="1" /></a>
							</td>
						</tr>-->						
						
						<!-- Encuesta por Luis Q.T. | 14AGO2020 
						<tr style="border-top: 1px solid #2c2a2a;background-color: #ccdbdf;"><td><b>Encuesta de satisfacciùn de Servicios</b><br /></td></tr>
						<tr>
							<td>
								<a href="https://docs.google.com/forms/d/1o_rxAFd4M1BgcVt7xDvy0a3hI0GdNh1nKyOe7TUO_b4" target="_blank" title="Clic aqu&iacute; para llenar la encuesta">
									<img style="border-color: red;" src="https://lh4.googleusercontent.com/JxBT_vU_YYZSEbenAp5dYH7BS43KdSOomaJpQ50RQtmYchv2dPDgQRS589yzQY8Y9a80LSrDXa3TdywunjczHcgqf0ODzbfgpNlQETH75m-GKVupxVD70rj2naio"
										alt="Clic aqu&iacute; para llenar la encuesta" width="370" height="92" border="1" /></a>
							</td>
						</tr>
						<tr>
                            <td>
							<br /><br />
                            </td>
                        </tr> 
                        -->
						<!-- Encuesta por Luis Q.T. | 14SEP2020 -->
						<tr style="border-top: 1px solid #2c2a2a;background-color: #ccdbdf;"><td><b>Encuesta a docentes sobre conexiùn a Internet</b><br /></td></tr>
						<tr>
							<td>
								<a href="https://forms.gle/Napmxt5FZKjKBNu66" target="_blank" title="Clic aqu&iacute; para llenar la encuesta">
									<img style="border-color: red;" src="https://lh4.googleusercontent.com/MKRrMXs725u7XWqNCW-MoQuPP_oARVVUjqwW0w2QYpQL_wlREpyugCyCdSkUI-syr0ue1XdFiBPgO1ddQfZpdtsf9XNVlmj7sv8cIonrDDDeYMUAG9wpIB-HIxwH"
										alt="Clic aqu&iacute; para llenar la encuesta" width="370" height="92" border="1" /></a>
							</td>
						</tr>
						<!-- Encuesta por Luis Q.T. | 21SEP2020 -->
						<tr><td><br /></td></tr>
						<tr style="border-top: 1px solid #2c2a2a;">
							<td>
								<a href="GestionEgresado/SustentacionTesis/Explicaciùn_del_proceso_Sustentaciùn_v5.pdf" target="_blank" title="Clic aqu&iacute; para ver el nuevo proceso">
									<img style="border-color: red;" src="Images/pasoSustentacion.jpg"
										alt="Clic aqu&iacute; para ver el nuevo proceso" width="370" height="98" border="1" /></a>
							</td>
						</tr>
						<tr style="border-top: 1px solid #2c2a2a;">
							<td>
								<a href="administrativo/PROTOCOLO_PVPC_COVID_USAT.pdf" target="_blank" title="Clic aqu&iacute; para ver el plan para la vigilancia, prevenciùn y control contra el Covid">
									<img style="border-color: red;" src="Images/IMAGEN_DIFUSION_PLAN.jpg"
										alt="Clic aqu&iacute; para ver el plan para la vigilancia, prevenciùn y control contra el Covid" width="370" height="370" border="1" /></a>
							</td>
						</tr>						
						<tr>
                            <td>
							<br /><br />
                            <!--
                                <div class="alert alert-warning">            
                                    <a href="https://intranet.usat.edu.pe/encuestas/index.php/781851?lang=es"><b>[BIBLIOTECA] - ENCUESTA DE SATISFACCION DEL SERVICIO - 2018</b><br/><i>Ingresar aqu&iacute;</i></a>
                                </div>    
                            -->
                            </td>
                        </tr>
                        <tr>
                            <td width="94%" align="left" valign="middle" class="style3">
                                <div>
                                    <iframe src="https://www.usat.edu.pe/anuncios/index2.php" width="100%" height="100%"
                                        style="position: relative; overflow: hidden; height: 470px;"></iframe>
                                    <!--<OBJECT id="anuncio" type="text/html" data="http://www.usat.edu.pe/anuncios/index.php" width=100% height=200% style="position: relative; overflow: hidden; height: 800px;"></OBJECT>-->
                                </div>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="90%" valign="middle">
                                            <img src="../images/arrow.gif" width="11" height="11" />Servicios TI
                                        </td>
                                        <td width="10%" align="center">
                                            <img src="../images/librohoja.gif" width="12" height="15" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="3" cellpadding="3" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <table cellspacing="3" cellpadding="3" width="100%" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td width="100%" align="center">
                                                                <p>
                                                                    <a onclick="top.location.href='abriraplicacion.asp?codigo_tfu=1&amp;codigo_apl=61&amp;descripcion_apl=SERVICIOS TI&amp;estilo_apl=O'"
                                                                        target="contenido">
                                                                        <img id="imgServiciosTI" border="1" src="../images/menus/serviciostimesa.png?X=1"
                                                                            style="width: auto; height: auto;" />
                                                                    </a>
                                                                </p>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <!-- Encuesta Biblioteca-->
                        <tr>
                            <td width="94%" align="left" valign="middle" class="style3" style ="display:none">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="90%" valign="middle">
                                            <img src="../images/arrow.gif" width="11" height="11" />Encuesta Biblioteca
                                        </td>
                                        <td width="10%" align="center">
                                            <img src="../images/librohoja.gif" width="12" height="15" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%" valign="middle">
                                            <a href="https://intranet.usat.edu.pe/encuestas/index.php/387563?lang=es" target="_blank">
                                                <img id="imgEncuestaBiblioteca" src="../images/biblioteca/ENCUESTA CAMPUS.JPG" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%" align="center">
                                            <a href="https://intranet.usat.edu.pe/encuestas/index.php/387563?lang=es" target="_blank">
                                                <b>[BIBLIOTECA] - ENCUESTA DE SATISFACCION DE LOS SERVICIOS DE BIBLIOTECA - 2019</b><br />
                                                <i>Ingresar aqu&iacute;</i></a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
            <!------>
<%				
    Set cn=server.createobject("pryusat.clsaccesodatos")
    cn.abrirconexion	

    'Para mostrar desayuno de trabajo con Pedro Pablo Ku... (Semana de Ingenierùa)
    Set rs= cn.consultar ("CV_ConsultarAvisoCampus","FO","V","P",-1)		
    cn.cerrarconexion

    i = 0
		
	For i = i To rs.recordcount-1
	    If rs("idAviso_avc")<>27 and rs("idAviso_avc")<> 109  Then
	    
	        If(rs("idAviso_avc")<>145 and session("codigo_tpe") = 3) Then
%>
                        <tr>
                            <td align="center">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="<%=RS("ColorFondo_Avc")%>">
                                    <tr>
                                        <td width="6%" valign="top">
                                            <img src="../images/arrow.gif" width="11" height="11" />
                                        </td>
                                        <td width="94%" align="left" valign="middle" class="Estilo1">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="90%" valign="middle">
                                                        <%=rs("Titulo_Avc")%>
                                                    </td>
                                                    <td width="10%" align="center">
                                                        <img src="../images/librohoja.gif" width="12" height="15" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <table width="100%" border="0" cellspacing="3" cellpadding="3">
                                                <tr>
                                                    <td>
                                                        <table width="100%" border="0" cellspacing="3" cellpadding="3">
                                                            <tr>
                                                                <td width="47%" valign="top">
                                                                    <p>
                                                                        <%=rs("descripcion_avc")%><br />
                                                                        <br />
                                                                        <%if rs("vermas_avc") <> "" then%>
                                                                        <a href="<%=rs("vermas_avc")%>" target="_top">Participa AQUI!!!</a>
                                                                        <br />
                                                                        <%end if%>
                                                                    </p>
                                                                </td>
                                                                <td width="53%" align="center">
                                                                    <p>
                                                                        <%if right(rs("vinculoImagen"),3)="gif" or right(rs("vinculoImagen"),3)="jpg" then%>
                                                                        <a href="<%=rs("vinculoImagen")%>" rel="lytebox">
                                                                            <%else%>
                                                                            <a href="<%response.write(rs("vinculoImagen") & "?id=" & session("codigo_Usu"))%>"
                                                                                target="<%=rs("target")%>">
                                                                                <%end if%>
                                                                                <img src="<%=rs("img_avc")%>" alt="<%=rs("titulo_avc")%>" border="1" />
                                                                            </a>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                            <%=rs("fecha_avc")%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
<% 
            ElseIf (session("codigo_tpe") <> 3) Then 
%>
                        <tr>
                            <td align="center">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="<%=RS("ColorFondo_Avc")%>">
                                    <tr>
                                        <td width="6%" valign="top">
                                            <img src="../images/arrow.gif" width="11" height="11" />
                                        </td>
                                        <td width="94%" align="left" valign="middle" class="Estilo1">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="90%" valign="middle">
                                                        <%=rs("Titulo_Avc")%>
                                                    </td>
                                                    <td width="10%" align="center">
                                                        <img src="../images/librohoja.gif" width="12" height="15" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <table width="100%" border="0" cellspacing="3" cellpadding="3">
                                                <tr>
                                                    <td>
                                                        <table width="100%" border="0" cellspacing="3" cellpadding="3">
                                                            <tr>
                                                                <td width="47%" valign="top">
                                                                    <p>
                                                                        <%=rs("descripcion_avc")%><br />
                                                                        <br />
                                                                        <%if rs("vermas_avc") <> "" then%>
                                                                        <a href="<%=rs("vermas_avc")%>" target="_top">Participa AQUI!!!</a>
                                                                        <br />
                                                                        <%end if%>
                                                                    </p>
                                                                </td>
                                                                <td width="53%" align="center">
                                                                    <p>
                                                                        <%if right(rs("vinculoImagen"),3)="gif" or right(rs("vinculoImagen"),3)="jpg" then%>
                                                                        <a href="<%=rs("vinculoImagen")%>" rel="lytebox">
                                                                            <%else%>
                                                                            <a href="<%response.write(rs("vinculoImagen") & "?id=" & session("codigo_usu"))%>"
                                                                                target="<%=rs("target")%>">
                                                                                <%end if%>
                                                                                <img src="<%=rs("img_avc")%>" alt="<%=rs("titulo_avc")%>" border="1" />
                                                                            </a>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                            <%=rs("fecha_avc")%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
<%  
            End If		     
		    
		    rs.movenext		
		 End If
		 		 
    next
%>
            
<%
    cn.abrirconexion		    
    Set rs= cn.consultar ("ACAD_ConsultarDptoCcss","FO",session("codigo_usu"))
    cn.cerrarconexion
    
    If rs.recordcount > 0 then 
%>
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="3" cellpadding="3">
                                    <tr>
                                        <th style="color: #B61C1E">
                                            <b>ESCUELA DE MEDICINA </b>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="color: #B61C1E">
                                            <b>PROP&Oacute;SITO</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: justify;">
                                            Formar M&eacute;dicos Cirujanos competentes en promoci&oacute;n, prevenci&oacute;n,
                                            recuperaci&oacute;n de la salud; con actitud investigadora y respeto pleno a la
                                            dignidad de la persona humana.
                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>
                                    <tr>
                                    </tr>
                                    <tr>
                                        <th style="color: #B61C1E">
                                            <b>ESCUELA DE ODONTOLOG&Iacute;A </b>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="color: #B61C1E">
                                            <b>PROP&Oacute;SITO</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: justify;">
                                            Formar Cirujanos Dentistas de una manera integral que contribuyan mediante la responsabilidad
                                            social universitaria y la investigaci&oacute;n al proceso de la sociedad, respetando
                                            la libertad de conciencia y los principios de la Iglesia Cat&oacute;lica.
                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>
                                    <tr>
                                    </tr>
                                    <tr>
                                    </tr>
                                    <tr>
                                        <th style="color: #B61C1E">
                                            <b>ESCUELA DE PSICOLOG&Iacute;A </b>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="color: #B61C1E">
                                            <b>PROP&Oacute;SITO</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: justify;">
                                            Formar psic&oacute;logos con competencia en la promoci&oacute;n, prevenci&oacute;n,
                                            diagn&oacute;stico e intervenci&oacute;n en la salud mental de la persona y la sociedad.
                                            Comprometido con la investigaci&oacute;n cient&iacute;fica y la responsabilidad
                                            social, formados integralmente, respetando la libertad de las conciencias y los
                                            principios de la Iglesia Cat&oacute;lica.
                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>
                                </table>
                            </td>
                        </tr>
<%
    End If 
%>
                    </table>
                    <!--EXAMEN MEDICO  OCUPACIONAL -->
<%
    'cn.abrirconexion		    
    'set rsM= cn.consultar ("ConsultarProgExaMed","FO",session("codigo_usu"))
    'cn.cerrarconexion
    'if rsM.recordcount > 0 then 
		'rsM.moveFirst
%>
                    <!--<br/>
		            <div>
			            <table cellspacing="0">-->
                    <!--
				            <tr>	
					            <td colspan="4" style="background-color:#E33439; color:white;padding:2px;font-size:13px; " >
							            <center><b>CITA EXùMENES MEDICOS OCUPACIONALES</b></center>
					            </td>
				            </tr>
				            <tr>
					            <td colspan="4" style="font-size:12px;padding:5px;">
						            <center><b> <%'=rsM("doc")& " "  & rsM("nrodoc")%></b></center>
					            </td>
				            </tr>
				            -->
                    <!--<tr >
				                <td colspan="4" align="CENTER" style="background-color:#E33439; color:white;padding:2px;border:1px solid #E33439; "><b>ESTIMADO COLABORADOR</b></td>				    	
				            </tr>
				            <tr>
				                <td colspan="4" align="center">
				                La presente es con el fin de solicitarle presentarse CON CAR&Aacute;CTER OBLIGATORIO el d&iacute;a <b><u><% 'response.Write rsM("fase1") %></u></b>  a las <b><u>07:30 a.m.</u></b> totalmente en ayunas  en <font color="red"><b>Preventiva, Cl&iacute;nica de Salud Ocupacional Av. Francisco Cuneo 680 Atl. Loreto Urb. Patazca </b></font> para la realizaci&oacute;n de sus Ex&aacute;menes M&eacute;dicos Ocupacionales y Ex&aacute;menes Complementarios.
				                </td>
				            </tr>				
            				
				            <tr><td align="center" colspan="4" height="20px;">Agradecemos su puntual asistencia</td></tr>
				            <tr><td align="center" colspan="4" height="20px;">Atte.</td></tr>
				            <tr><td align="center" colspan="4" height="20px;">&nbsp;</td></tr>
                            <tr><td align="center" colspan="4" height="20px;">Direcci&oacute;n de personal &ndash; Coordinaci&oacute;n Seguridad y Salud Ocupacional</td></tr>					
                            <tr><td align="center" ><img src="images/saludUSAT.png" /></td></tr>-->
                    <!--
				            <tr>
					            <td rowspan="2"  style="background-color:#E33439; color:white;padding:2px;border:1px solid #E33439; "><b><center>INFORMES</center></b></td>
					            <td colspan="3" style="border-top:1px solid #E33439;border-right:1px solid #E33439;padding:5px;">
						            <center>
							            <b>Lic. Gloria Pinglo Neyra</b></br>
							            <a href="mailto:gpinglo@usat.edu.pe" target="_top">gpinglo@usat.edu.pe</a>
						            </center>
					            </td>					
				            </tr>
            				
				            <tr><td colspan="4" style="border-right:1px solid #E33439;background-color:#EFE6E7;border-bottom:1px solid #E33439; padding-top:2px;"><b><center><a target="_blank" href="../librerianet/reglamentos/INSTRUCTIVO_EXAMEN_OCUPACIONAL.pdf">Leer Instructivo Obligatorio</a>&nbsp;&nbsp; <img src="images/desc.png"/></center><b></td></tr>
				            -->
                    <!--
				            <tr><td colspan="4"></td></tr>
				            <tr><td colspan="4"><i>Lic. Gloria Pinglo Neyra</i></td></tr>
				            <tr><td colspan="4"><a href="mailto:gpinglo@usat.edu.pe" target="_top">gpinglo@usat.edu.pe</a></td></tr>
				            -->
                    <!--	</table>
		            </div>-->
                    <!--EXAMEN MEDICO  OCUPACIONAL -->
                    <br />
<%
    'End If 
%>
                </form>
            </td>
        </tr> 
    </table>            

    <div class="modal fade" id="mdlMostrarPagina" tabindex="-1" role="dialog" aria-labelledby="lblTituloModal"
        aria-hidden="true">
        <div id="mddMostrarPagina" class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="lblTituloModal">Titulo del Modal</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <iframe id="ifrmContenido" src="" scrolling="no" frameborder="0" width="100%" style="min-height: 400px;"></iframe>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>                    
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="mdlAvisoPersonal" tabindex="-1" role="dialog" aria-labelledby="lblAvisoPersonal"
            aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header" style="color: white; background-color: #e33439;">
                    <h5 class="modal-title" id="lblAvisoPersonal">Encuesta Clima Laboral</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">                                        
                    <div class="form-group" style="font-size: 14px; text-align: justify;">
                        A continuaciùn usted encontrarù una serie de preguntas o afirmaciones en las cuales no hay respuestas buenas o malas, se busca conocer su opiniùn respecto al ambiente laboral actual que usted identifica como colaborador de la Universidad Catùlica Santo Toribio de Mogrovejo.
                    </div>
                </div>
                <div class="modal-footer">
                    <a class="btn btn-success" style="color: White !important;" href="https://intranet.usat.edu.pe/encuestas/index.php/965274?lang=es" target="_blank">Llenar Encuesta</a>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>                    
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var wSTI = document.getElementById("imgServiciosTI").width;
        var hSTI = document.getElementById("imgServiciosTI").height;
        var wSTI = document.getElementById("imgEncuestaBiblioteca").width = wSTI;
        var hSTI = document.getElementById("imgEncuestaBiblioteca").height = hSTI;

        var perdioFoco = 0;  
    </script>
    
<% 
    'CARGAR MODALES
    
    Set ObjActualizarDatos = Server.CreateObject("PryUSAT.clsAccesoDatos")
    ObjActualizarDatos.AbrirConexion
    Set rsActualizarDatos = ObjActualizarDatos.consultar("PER_DatosPersonalListar","FO","GEN",session("codigo_usu"),"N")                        
    
    If rsActualizarDatos.recordcount > 0 Then                
        Response.Write("<script>cargarModal('Actualizaciùn de Datos del Personal', '70', 'personal/frmActualizarDatosPersonal.aspx?codigo_per=" & session("codigo_usu") & "')</script>")
    End If
    
    ObjActualizarDatos.CerrarConexion
    Set ObjActualizarDatos = Nothing
    
    If (Now() < cdate("15/12/2020")) Then
        Response.Write("<script>$('#mdlAvisoPersonal').modal('show');</script>")
    End If
%>
</body>
</html>
