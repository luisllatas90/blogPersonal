declare @codigoraiz_men int = 0
select @codigoraiz_men = codigo_Men  from MenuAplicacion where codigo_Apl = 32 and descripcion_Men = 'Planificar Evaluaci�n'

update MenuAplicacion set codigoRaiz_Men = @codigoraiz_men
where codigo_Apl = 32 and descripcion_Men in ('Registro de Grupos de Evaluaci�n', 'Asignaci�n de Postulantes a Grupos de Evaluaci�n')