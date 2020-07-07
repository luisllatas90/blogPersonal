if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PPK_RegistrarParticipacion]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[PPK_RegistrarParticipacion]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- cambiarpermisoobjeto 'p'
CREATE PROCEDURE dbo.PPK_RegistrarParticipacion
	@tipo char(2),
	@codigo_per int,
	@participa bit	
AS
IF @tipo ='CO'
BEGIN
	SELECT * FROM TMP_desayunoPPK WHERE codigo_per = @codigo_per
END

IF @tipo='RE'
begin
	INSERT INTO TMP_desayunoPPK (desayuno, codigo_per) VALUES (@participa,@codigo_per)
end


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

