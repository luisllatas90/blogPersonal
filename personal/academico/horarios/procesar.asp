<!--#include file="../../../NoCache.asp"-->
<!--#include file="../../../funciones.asp"-->
<%
accion=request.querystring("accion")
codigo_cac=request.querystring("codigo_cac")
codigo_cup=request.querystring("codigo_cup")
codigo_amb=request.querystring("codigo_amb")

function DevolverFormato(modo,valor)

	if modo="h" then
		if len(valor)=1 then
			DevolverFormato="0" & valor
		else
			DevolverFormato=valor
		end if
	else
		select case valor
			case 0:DevolverFormato="LU"
			case 1:DevolverFormato="MA"
			case 2:DevolverFormato="MI"
			case 3:DevolverFormato="JU"
			case 4:DevolverFormato="VI"
			case 5:DevolverFormato="SA"
			case 6:DevolverFormato="DO"
		end select																
	end if
end function

if accion="registrarhorario" then
	on Error resume next
	codigo_per=request.querystring("codigo_per")
	codigo_cpf=request.querystring("codigo_cpf")
	fechaini=request.querystring("fechaini")
	fechafin=request.querystring("fechafin")

	'Remplazar variables asignadas
	ini1=replace(fechaini,"e"," ")
	ini1=replace(ini1,"d",":")
	ini1=replace(ini1,"x","/")
	ini1=replace(ini1,"q",".")
	
	fin1=replace(fechafin,"e"," ")
	fin1=replace(fin1,"d",":")
	fin1=replace(fin1,"x","/")
	fin1=replace(fin1,"q",".")
'	response.write "fin ___ >" & fechafin & "<br>"
	tipo_Lho=request.querystring("tipo_Lho")
	mensaje = ""
	th=request.querystring("th")
	Marcas=verificacomaAlfinal(request.querystring("Marcas"))
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		
		Marcas=split(Marcas,",")
		total=Ubound(Marcas)+1
		inicio=0
		fin=0
		Dim horariodia()
		Dim dia
		Dim mensaje()
		
		redim  horariodia(ubound(marcas))
		redim  mensaje(ubound(marcas))  
		  
        'response.Write "<script>alert('" & HayCruce & "')</script>"
        
        'response.Write "<script>alert('no hay cruce')</script>"
        FOR d=0 TO 6
		    n=0
		    for j=0 to total-1
			    dia =int (mid ( Marcas(j),1,1) )
			    if dia= d then
				    hora=MID(trim(Marcas(j)),2)
				    horariodia(n)=hora
				    'response.write "<br> dia : "  & dia & "hora : " & hora
				    n=n+1
			    end if				
		    next
					
		    if n>0 then
			    'La misma Hora + 1
			    inicio=int(horariodia(0))
			    fin=inicio+1
			    for i=0 to n-1
				    horaactual=int(horariodia(i))
					
				    if (horaactual-fin)<=0 then
					    'Hay un nuevo fin
					    fin=horaactual+1
				    else
					    'Insertar en BD e inicializar variables
					    'response.write "dia : " & WeekdayName (d +1,FALSE,tuesday) & "INICIO:" & DevolverFormato("h",inicio) & "-->FIN:" & DevolverAncho(fin) & "<br>"	                    
					    mensaje=obj.Ejecutar("RegistrarHorario_v2",true,codigo_cac,codigo_cup,DevolverFormato("d",d),DevolverFormato("h",inicio),DevolverFormato("h",fin),codigo_daa,tipo_Lho,codigo_per,session("codigo_usu"),null)
					    ' imprimir mensajes
					    inicio=horaactual
					    fin=inicio+1

				    end if
			    next
				
			    'Insertar en BD e inicializar variables
			    'response.write "dia : " & WeekdayName (d +1,FALSE,tuesday) & "INICIO:" & DevolverFormato("h",inicio) & "-->FIN:" & DevolverFormato("h",fin) & "<br>"

			    mensaje=obj.Ejecutar("RegistrarHorario_v2",true,codigo_cac,codigo_cup,DevolverFormato("d",d),DevolverFormato("h",inicio),DevolverFormato("h",fin),codigo_amb,tipo_Lho,codigo_per,session("codigo_usu"),null)
		
			    'response.write codigo_cac & "," & codigo_cup & "," & DevolverFormato("d",d) & "," & DevolverFormato("h",inicio) & "," & DevolverFormato("h",fin) & "," & codigo_amb & "," & tipo_Lho & "," & codigo_per  & "," & session("codigo_usu")& "," & ini1 & "," & fin1
			    if mensaje<>"OK" then
				    exit for
			    end if
		    end if
		
	    next            
           		
        if mensaje = "CRUCE" then
            response.Write "<script>alert('No puede guardarse la informaci�n por haber cruce')</script>"
        end if
           		
    set rsCruces = createobject("ADODB.Recordset")
	Set rsCruces=obj.Consultar("ConsultarHorarios","FO",0,codigo_amb,codigo_cac,0)%>
		<html>
			<head>
			<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
			<link rel="stylesheet" type="text/css" href="../../../private/estilo.css" />
			<script language="JavaScript" src="../../../private/funciones.js"></script>
			</head>
			<body>
				<% IF Not(rsCruces.BOF and rsCruces.EOF) then  %>
				<h3><b><u> Se han presentado los siguientes cruces de horarios</u></b></h3>
				<%				    				    
					ArrEncabezados=Array("Escuela Profesional","Curso","Fecha Inicio","Fecha Fin","Dia","Hora Inicio","Hora Fin")
	                ArrCampos=Array("nombre_cpf2","nombre_cur2","fechainicio_cup2","fechafin_cup2","dia_lho","nombre_hor2","horafin_lho2")
	                ArrCeldas=Array("20%","30%","15%","15%","5%","10%","10%")				
					Call CrearRpteTabla(ArrEncabezados,rsCruces,"",ArrCampos,ArrCeldas,"S","N","","N","","")
				else
				    'response.Write "Ambiente:" & mensaje 
					response.write("<h5>Se han guardado los datos correctamente</h5>")
				end if
				%>
			</body>
		</html>
	<%
	obj.CerrarConexion
	Set Obj=nothing
	%>
	<h5 align="center"><a href="tblhorario.asp?codigo_cup=<%=codigo_cup%>&codigo_amb=0&codigo_cac=<%=codigo_cac%>&th=<%=th%>&codigo_cpf=<%=codigo_cpf%>">Haga clic aqu� para regresar</a></h5>
	<%	
	'response.redirect "tblhorario.asp?codigo_cup=" & codigo_cup & "&codigo_amb=" & codigo_amb & "&codigo_cac=" & codigo_cac & "&codigo_cpf=" & codigo_cpf
end if	
	
If Accion="eliminarhorario" then
	codigo_lho=request.querystring("codigo_lho")
		Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
			obj.AbrirConexion
				call Obj.Ejecutar("EliminarHorario",false,codigo_Lho,session("codigo_usu"))
			obj.CerrarConexion
		set Obj=nothing
		
		response.write "<script>window.opener.location.reload();window.close()</script>"
End if

if Accion="AgregarAsignacionAmbiente" then
	dim arrmensaje()
	
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexionTrans
		
			For i=1 to request.form("nocheck")
				codigo_cpf=trim(Request.form("txtcodigo_cpf" & i))
				if codigo_cpf<>"" then
					'---------------------------------------------------------
					'Si SE ha marcado el check
					'---------------------------------------------------------
					If (Request.Form("chk" & i ) <> "") then
						call Obj.Ejecutar("AgregarAsignacionAmbiente",false,"A",codigo_amb,codigo_cpf,codigo_cac,session("codigo_usu"),null,null,null)
					End If
					
					'---------------------------------------------------------
					'Si NO se ha marcado el check
					'---------------------------------------------------------	
					If(Request.Form("chk" & i ) = "") Then
						mensaje=Obj.Ejecutar("AgregarAsignacionAmbiente",true,"E",codigo_amb,codigo_cpf,codigo_cac,session("codigo_usu"),null,null,null)
					End If
				end if
			Next
		Obj.CerrarConexionTrans
	set obj=nothing
	response.redirect("administrar/lstambientesescuela.asp?codigo_cac=" & codigo_cac & "&codigo_amb=" & codigo_amb)
end if

if Accion="AgregarDetalleAsignacion" then
	codigo_aam=request.querystring("codigo_aam")
	codigo_cpf=request.querystring("codigo_cpf")
	fechainicio=request.form("fechainicio") & " " & request.form("horainicio") & ":" & request.form("mininicio")
	fechafin=request.form("fechafin")& " " & request.form("horafin") & ":" & request.form("minfin")
	
	fechainicio=cdate(fechainicio)
	fechafin=cdate(fechafin)

	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion
		    'response.Write (fechainicio)
		    'response.Write (fechafin)
			Call Obj.Ejecutar("AgregarAsignacionAmbiente",false,"A",codigo_amb,codigo_cpf,codigo_cac,session("codigo_usu"),fechainicio,fechafin,null)
			
		Obj.CerrarConexion
	Set obj=nothing
	
	response.redirect("administrar/frmdetalleasignacionambiente.asp?codigo_aam=" & codigo_aam & "&codigo_cpf=" & codigo_cpf & "&codigo_cac=" & codigo_cac & "&codigo_amb=" & codigo_amb)
		
end if

if Accion="QuitarFechaAsignacion" then
	codigo_aam=request.querystring("codigo_aam")
	codigo_cpf=request.querystring("codigo_cpf")
	fechainicio=request.querystring("fechainicio")
	fechafin=request.querystring("fechafin")
	codigo_daa=request.querystring("codigo_daa")
	
	fechainicio=cdate(fechainicio)
	fechafin=cdate(fechafin)

	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion
			'response.write (" codigo_aam : >>" + codigo_Aam)
			mensaje=Obj.Ejecutar("AgregarAsignacionAmbiente",true,"Q",null,null,null,session("codigo_usu"),null,null,codigo_daa)
			
		Obj.CerrarConexion
	Set obj=nothing
	
	if isnull(mensaje)=false then
		response.write "<h3>" & mensaje & "</h3>"
	else
		response.redirect("administrar/frmdetalleasignacionambiente.asp?codigo_aam=" & codigo_aam & "&codigo_cpf=" & codigo_cpf & "&codigo_cac=" & codigo_cac & "&codigo_amb=" & codigo_amb)
	end if
	
end if


%>