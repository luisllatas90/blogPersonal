<!--#include file="../../funciones.asp"-->
<%
dim codigo_men
dim NumNodoAbierto

call Enviarfin(session("codigo_usu"),"../../")

codigo_men=request.querystring("codigo_men")
NumNodoAbierto=request.querystring("NumNodoAbierto")
tipoImagen=request.querystring("tipoImagen")

Public Function CrearMenu(ByVal codigoRaiz_Men,ByVal prefix,ByVal codigo_apl,ByVal codigo_tfu,ByVal TipoImagen)
		Dim ArrCarpeta, NodoIzquiero, preadd, Resultado, NodoAbierto,ImagenMenu,EventoNodo
		Dim AnchoTexto,TextoMenu
	
		Set Obj= Server.CreateObject("PryUSAT.clsDatAplicacion")
			ArrCarpeta=Obj.ConsultarAplicacionUsuario("AR","11",codigo_apl,codigo_tfu,codigoRaiz_men)
		Set Obj=nothing
		
		If IsEmpty(ArrCarpeta)=false then
			preadd = ""
			Resultado = ""
	
			FOR C=Lbound(ArrCarpeta,2) to Ubound(ArrCarpeta,2)
				NodoAbierto = 0
				NodoIzquierdo=ArrCarpeta(12,C) ''Número de Nodos
				NodoIzquiero = NodoIzquiero - 1
				ImagenMenu=""
				EventoNodo=""
				
				'Verificar si es un sólo Menú para cargar los íconos pequeños en el mnuPrincipal
				if (TipoImagen="7" and trim(codigoRaiz_men)="0") then
					TipoImagen="7" 'Carga el campo de íconos de 16x16
				else
					TipoImagen="5" 'Carga el campo de íconos normales
				end if
				
				'Verificar si hay imágen, caso contrario colocar una por defecto
				if ArrCarpeta(tipoImagen,C)="" or IsNull(ArrCarpeta(tipoImagen,C))=true  then
					ImagenMenu="editar_1_s.gif"
				else
					ImagenMenu=ArrCarpeta(tipoImagen,C)				
				end if
				
				'Verificar si tiene enlace, caso contrario no llamar al evento onClick
				if ArrCarpeta(1,C)="" or IsNull(ArrCarpeta(1,C))=true  then
					EventoNodo=""
				else
					EventoNodo=" onClick=""AbrirMenu(this,'" & ArrCarpeta(4,C) & "')"""
				end if
				
				Resultado=Resultado & "<tr id=""tbl" & ArrCarpeta(1,C) & EventoNodo & ">"
				Resultado=Resultado & "<td>" & prefix
				if NodoIzquiero then
					Resultado = Resultado & "<img src='../../images/menus/beforenode.gif' align=absbottom>"
				else
					Resultado = Resultado & "<img src='../../images/menus/beforelastnode.gif' align=absbottom>"
				end if
		
				if ArrCarpeta(12,C) > 0 then
					NodoAbierto = instr(NumNodoAbierto,"[" & ArrCarpeta(1,C) & "]")
					if NodoAbierto > 0 then
						Resultado = Resultado & "<a href='carpetas.asp?tipoImagen=" & tipoImagen & "&codigo_men=" & codigoRaiz_men & "&NumNodoAbierto=" & _
						replace(NumNodoAbierto, "[" & ArrCarpeta(1,C) & "]", "") & "'>" & _
						"<img src='../../images/menus/NodoAbierto.gif' align=absbottom></a>"
					else
						Resultado=Resultado & "<a href='carpetas.asp?tipoImagen=" & tipoImagen & "&codigo_men=" & codigoRaiz_men & "&NumNodoAbierto=" & _
						NumNodoAbierto & "[" & ArrCarpeta(1,C) & "]'>" & _
						"<img src='../../images/menus/NodoCerrado.gif' align=absbottom></a>"
					end if
				else
					Resultado=Resultado & "<img src='../../images/menus/nodeleaf.gif' align=absbottom>"
				end if
				
				AnchoMnu=len(ArrCarpeta(3,C))
				
				if int(AnchoMnu)<=20 then
					TextoMenu=ArrCarpeta(3,C)
				else
					TextoMenu=left(ArrCarpeta(3,C),20) & "..."				
				end if
							
				Resultado = Resultado & "&nbsp; <img border=""0"" name=""arrImgCarpetas"" id=""imgCarpeta" & ArrCarpeta(1,C) & """ src=""../../images/menus/" & ImagenMenu & """ align=absbottom ALT=""" & ArrCarpeta(3,C) &  """>&nbsp;" & _
									"<span id=""spCarpeta" & ArrCarpeta(1,C) & """ " & EventoNodo & ">" & TextoMenu & "</span></td></tr>" & chr(13) & chr(10)
					if NodoAbierto then
						if NodoIzquiero then
							preadd = "<img src='../../images/menus/beforechild.gif' align=absbottom>"
						else
							preadd = "<img src='../../images/menus/beforelastchild.gif' align=absbottom>"
						end if
						Resultado = Resultado & CrearMenu(ArrCarpeta(1,C), prefix & preadd,codigo_apl,codigo_tfu,TipoImagen)
					end if
			NEXT
			
		end if

		CrearMenu= Resultado
end function
%>
<html>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../../private/estilo.css">
<script language="JavaScript" src="../../private/funciones.js"></script>
<STYLE>
<!--
	img 	{border:0px none;align:absbottom}
	tr      {top: 0;cursor:hand }
-->
</STYLE>
<script language="Javascript">
	function AbrirMenu(id,pagina)
	{
	 	var  menu = id.innerText
		if (pagina!='about:blank' && pagina!="") {		
			if (pagina.indexOf('?')==-1){//Si no encuentra una referencia
				pagina=pagina + '?menu=' + menu
			}
			else{
				pagina=pagina + '&menu=' + menu
			}
			ElegirRecurso(id)
			AbrirMensaje('../../images/')
			top.parent.frames[2].location.href=pagina
		}
	}
</script>

<base target="_self">
</HEAD>
<body topmargin="0" leftmargin="0">
<table cellpadding=0 cellspacing=0 border=0 width="100%" heigth="100%">
<%=CrearMenu(codigo_Men,"",session("codigo_apl"),session("codigo_tfu"),tipoimagen)%>
</table>
</body>
</html>