---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- CAJ_ConsultarDatosNotaCredito 1447120  
--Usuario			fecha Actualiza			Sistema				Formulario		Observacion
--01: Jguevara			24-03-2014 11:00 am		Caja y penciones	frmNotaCredito	se agrego el parametro UsuCajaCodigo_per para que guarde el usurio para nota de credito electronica
--002 moises.vilchez	08-05-2018 05:00 pm		Se asigno por defecto la moneda a 'S'

--select * from CajaIngreso  where codigo_Tdo=16 order by codigo_Cin

--CREATE PROCEDURE [dbo].[AgregarCajaIngreso_NCI_v4]      
ALTER PROCEDURE [dbo].[AgregarCajaIngreso_NCI_v4]      
 (      
  @codigo_tdo    tinyint,      
  @nrodocumento_cin  varchar (15),      
  @fecha_cin    datetime,      
  @tipocliente_cin  char (1),      
  @codigo_alu    int,      
  @codigo_per    int,      
  @codigo_ocl    int,      
  @nombrecliente_cin  varchar (80),      
  @estado_cin    char (1),      
  @total_cin    float,      
  @moneda_cin    Varchar (3),      
  @nroliquidacion_cin  varchar (20),      
  @impreso_cin   bit,      
  @codigo_cba    tinyint,      
  @numeronota_cin   varchar (15),    
  @importenota_cin  float,      
  @codigo_emp    tinyint,      
  @usuario_cin   varchar(50),      
  @gStrEquipo    varchar(200),      
  @importeIGV_Cin   decimal(18,2),      
  @afectoIGV_Cin   bit,      
  @notacanje_cin   bit,     
  @codigo_mnc int,  -- Moises 31-05-2016 Se Agrego Motivo de Nota de Crédito    
  @glosa_mnc varchar(200), -- Moises 31-05-2016 Se Agrego Glosa  
  @UsuCajaCodigo_per integer,--01
  @codigo_cin integer,
  @codigogenerado   int OUTPUT      
 )      
AS  

DECLARE @codigo_pso integer
IF @tipoCliente_Cin='M'
	BEGIN		
		SELECT @codigo_pso = codigo_Pso FROM CajaIngreso where codigo_Cin=@codigo_cin		
	END

 IF @codigo_alu=0 SET @codigo_alu=NULL       
 IF @codigo_per=0 SET @codigo_per=NULL       
 IF @codigo_ocl=0 SET @codigo_ocl=NULL      
 IF @codigo_cba=0 SET @codigo_cba=NULL       
       
 --Para Agregar Observacion -- Hector: 26-09-06      
 Declare @Observacion_Cin varchar(100)      
 set @Observacion_Cin ='-'      
       
 if @codigo_cba=1      
  begin      
     set @Observacion_Cin ='Banco de Credito'       
  end      
 else      
  begin      
   if @codigo_cba=2      
   begin      
      set @Observacion_Cin ='Banco Interbank'       
   end      
  end      
        
 --OBTENER EL ULTIMO NUMERO SEGUN EL TIPO DE DOCUMENTO      
       
 declare @NroDoc varchar(10), @NroDoc1 varchar(10), @serie char(3), @NroDocum varchar(15)      
        
  --/ Cambio del procedimiento para determinar el numero de cod para la nota de credito. ==      

       
SELECT 
	@NroDocum=RIGHT('000'+ltrim(rtrim(t2.serietdoc_ter)),3)+RIGHT('000000'+ltrim(rtrim(t2.numeroactual_ter+1)),6)       
FROM 
	terminal t       
	INNER JOIN terminaldocumento t2 ON t2.codigo_Ter = t.codigo_Ter      
WHERE 
	--rtrim(ltrim(t.descripcion_ter))=@gStrEquipo       
	--AND t2.codigo_Tdo=@codigo_tdo      
	rtrim(ltrim(t.descripcion_ter))='RECAUDA'
	AND t2.codigo_Tdo=@codigo_tdo  
	
       
 --=======================================================================================      
       
       
  --Select @NroDoc=ultimoNumero_Tdo,@serie=serie_Tdo from TipoDocumento where codigo_Tdo=@codigo_tdo      
       
  --SET @NroDoc = @NroDoc + 1      
      
  --Select @NroDoc=ultimoNumero_Tdo,@serie=serie_Tdo from TipoDocumento where codigo_Tdo='1'      
  --set @NroDoc1 = @serie + @NroDoc      
      
 --//Agregado para la impresion del formato. ----------------------------------------      
  --SET @serie=RIGHT('000'+ltrim(rtrim(@serie)),3)      
  --SET @NroDoc=RIGHT('000000'+ltrim(rtrim(@NroDoc)),6)      
 ------------------------------------------------------------------------------------      
 --set @NroDocum = cast((@serie + @NroDoc) AS varchar(15))      
       
 INSERT INTO cajaingreso      
	(codigo_tdo,   
	nrodocumento_cin,   
	fecha_cin,   
	tipocliente_cin,   
	codigo_alu,   
	codigo_per,      
	codigo_ocl,   
	nombrecliente_cin,   
	estado_cin,   
	total_cin,   
	moneda_cin,   
	nroliquidacion_cin,      
	impreso_cin,   
	codigo_cba,   
	numeronota_cin,   
	importenota_cin,   
	codigo_emp,   
	usuario_cin,   
	Observacion_Cin,  
	importeIGV_Cin,  
	afectoIGV_Cin,   
	notaxCanje_cin,  
	EstadonotaxCanje_cin,   
	codigo_mnc,  -- Moises 31-05-2016 Se Agrego Motivo de Nota de Crédito    
	glosa_mnc,
	UsuCajaCodigo_per,
	NombreEquipo_cin, 
	refcodigo_cin, TotalMonNac_Cin, codigo_ctes, tipoCambio_Cin, codigo_pso
	)   -- Moises 31-05-2016 Se Agrego Glosa  
 VALUES      
	(@codigo_tdo,   
	@NroDocum,   
	@fecha_cin,   
	@tipocliente_cin,   
	@codigo_alu,   
	@codigo_per,  
	@codigo_ocl,   
	@nombrecliente_cin,   
	@estado_cin,   
	@total_cin,      
	CASE @moneda_cin WHEN 'S/.' THEN 'S' WHEN 'US$' THEN 'D' WHEN '€' THEN 'E' ELSE 'S' END,   --002   
	@nroliquidacion_cin,   
	@impreso_cin,   
	@codigo_cba,   
	@numeronota_cin,   
	@importenota_cin,      
	@codigo_emp,   
	@usuario_cin,  
	@Observacion_Cin,  
	@importeIGV_Cin,  
	@afectoIGV_Cin,  
	@notacanje_cin,   0,   
	@codigo_mnc,  -- Moises 31-05-2016 Se Agrego Motivo de Nota de Crédito    
	@glosa_mnc,
	@UsuCajaCodigo_per,
	@gStrEquipo,
	@codigo_cin, @total_cin, 2, 1.0, @codigo_pso
	)   -- Moises 31-05-2016 Se Agrego Glosa  
        
  /*      
  Este incremento lo hace en el procedure: -->>  sp_registraingreso_NC   -<<      
   update terminaldocumento set numeroactual_ter=numeroactual_ter+1         
   where codigo_tdo=@codigo_tdo and serietdoc_ter= @seriedoc_ing        
        
  -----------------------------------------------------------------------------------------------------      
   --update TipoDocumento set ultimoNUmero_Tdo=@NroDoc where codigo_Tdo=@codigo_tdo      
   -- UPDATE terminaldocumento SET numeroactual_ter =numeroactual_ter+1       
 -- WHERE codigo_Tdo=@codigo_tdo       
   -- AND codigo_Ter IN(SELECT t.codigo_Ter FROM terminal t WHERE rtrim(ltrim(t.descripcion_ter))=@gStrEquipo)      
  */      
       
  SELECT @codigoGenerado=@@IDENTITY --Almacena codMatricula recien almacenado      
 

