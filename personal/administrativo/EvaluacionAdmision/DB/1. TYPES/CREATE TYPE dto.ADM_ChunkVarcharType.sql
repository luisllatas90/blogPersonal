CREATE TYPE dbo.ADM_ChunkVarcharType
AS TABLE (
    chunk VARCHAR(MAX)
)
GO

GRANT EXECUTE ON TYPE::ADM_ChunkVarcharType TO usuariogeneral
GRANT EXECUTE ON TYPE::ADM_ChunkVarcharType TO iusrvirtualsistema
GO