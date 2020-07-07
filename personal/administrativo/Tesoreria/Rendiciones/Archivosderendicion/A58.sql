USE cmusat
GO



select * from evaluacion where tituloevaluacion like '%eval%proce%mat%'

sp_help resultadosevaluacion 


select titulopregunta,idusuario, idpregunta,count (*) from  resultadosevaluacion where idevaluacion=169  AND IDUSUARIO= '031cpr1665'
		and idpregunta=742
group by idusuario, idpregunta,titulopregunta


select * from  resultadosevaluacion where idevaluacion=169  AND IDUSUARIO= '031cpr1665'
		and idpregunta=742



select * from  respuesta

select* from alternativa


select * from  pregunta where idpregunta in (730)

select * from  alternativa where idalternativa in (1521, 1523, 1526)
ordenpregunta not in (3,5,16) 




select * from  where idalternativa in (1521, 1523, 1526)

/*sacar las alterntivas multiples en otra tabla*/

create table #resultadosevaluacion_jmanay
	(
		codigo_usuario		,
		descripcion_pregunta	,
		alternativamarcada	,
		
	)

declare  curres_jmanay cursor for
	select * from resultadosevaluacion where ordenpregunta not in (3,5,16)
	

		


go
select  	resultadosevaluacion.idusuario, ordenpregunta ,resultadosevaluacion.titulopregunta,tituloalternativa  , 
		count (distinct alternativa.idalternativa) from
		resultadosevaluacion  inner join respuesta on  resultadosevaluacion.idrespuesta=respuesta.idrespuesta 
		---inner join alternativa 	on  alternativa.idalternativa=respuesta.descripcionrpta
		where resultadosevaluacion.idevaluacion=169  /*AND resultadosevaluacion.IDUSUARIO= '031cpr1665'*/
		/*and resultadosevaluacion.idpregunta=742*/ and ordenpregunta not in (3,5,16)
	group by  resultadosevaluacion.idusuario,resultadosevaluacion.titulopregunta,tituloalternativa,ordenpregunta
	order by ordenpregunta




	having count (*) >=2 ORDER BY titulopregunta







