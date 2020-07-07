
SELECT o.*, vca.docente
FROM OPENQUERY(servermoodle,'
		select shortname , fullname from mdl_course where cc = 48') o
INNER JOIN vstcargaacademica vca ON vca.codigo_Cup = CONVERT(varchar(10),o.shortname)
