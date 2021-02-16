Imports System.Net
Imports System.IO
Imports System.Drawing
Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

#Region "ENTIDADES"

Public Class e_Categoria

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_cat As String
    Public nombre_cat As String
    Public grupo_cat As String
    Public codigoSuperior As String
    Public abreviatura_cat As String

    Public cod_user As String
    Public operacion As String    
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_cat = String.Empty
        nombre_cat = String.Empty
        grupo_cat = String.Empty
        codigoSuperior = String.Empty
        abreviatura_cat = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_Oferta

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_ofe As String
    Public idPro As String
    Public codigo_dep As String
    Public titulo_ofe As String
    Public descripcion_ofe As String
    Public requisitos_ofe As String
    Public contacto_ofe As String
    Public correocontacto_ofe As String
    Public telefonocontacto_ofe As String
    Public lugar_ofe As String
    Public tipotrabajo_ofe As String
    Public duracion_ofe As String
    Public fechaInicioAnuncio As String
    Public fechaFinAnuncio As String
    Public fechaReg_ofe As String
    Public estado_ofe As String
    Public fechaActiv_ofe As String
    Public codigo_sec As String
    Public visible_ofe As String
    Public web_ofe As String
    Public modopostular_ofe As String
    Public mostrarcorreocontacto As String
    Public tipo_oferta As String
    Public fechaIniReg As String
    Public fechaFinReg As String
    Public desc_banner As String
    Public codigo_emp As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_ofe = String.Empty
        idPro = String.Empty
        codigo_Dep = String.Empty
        titulo_ofe = String.Empty
        descripcion_ofe = String.Empty
        requisitos_ofe = String.Empty
        contacto_ofe = String.Empty
        correocontacto_ofe = String.Empty
        telefonocontacto_ofe = String.Empty
        lugar_ofe = String.Empty
        tipotrabajo_ofe = String.Empty
        duracion_ofe = String.Empty
        fechaInicioAnuncio = String.Empty
        fechaFinAnuncio = String.Empty
        fechaReg_ofe = String.Empty
        estado_ofe = String.Empty
        fechaActiv_ofe = String.Empty
        codigo_sec = String.Empty
        visible_ofe = String.Empty
        web_ofe = String.Empty
        modopostular_ofe = String.Empty
        mostrarcorreocontacto = String.Empty
        tipo_oferta = String.Empty
        fechaIniReg = String.Empty
        fechaFinReg = String.Empty
        desc_banner = String.Empty
        codigo_emp = String.Empty
    End Sub

#End Region

End Class

Public Class e_TipoEstudio

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_test As String
    Public descripcion_test As String
    Public mostrarenweb_test As String
    Public cod_user As String
    Public operacion As String

#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_test = String.Empty
        descripcion_test = String.Empty
        mostrarenweb_test = String.Empty
        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_Facultad

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_Fac As String
    Public identificador_Fac As String
    Public nombre_Fac As String
    Public abreviatura_fac As String
    Public vigencia_fac As String
    Public codigo_Cco As String
    Public refcodigo_fac As String
    Public cod_user As String
    Public operacion As String

    'Otros
    Public codigo_per As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_Fac = String.Empty
        identificador_Fac = String.Empty
        nombre_Fac = String.Empty
        abreviatura_fac = String.Empty
        vigencia_fac = String.Empty
        codigo_Cco = String.Empty
        refcodigo_fac = String.Empty
        cod_user = String.Empty
        operacion = String.Empty

        'Otros
        codigo_per = String.Empty
    End Sub

#End Region

End Class

Public Class e_CarreraProfesional

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_Cpf As String
    Public codigo_test As String
    Public vigencia_Cpf As String
    Public eliminado_cpf As String
    Public codigo_Fac As String
    Public tiene_facultad As String
    Public codigo_per As String
    Public codigo_tfu As String
    Public cod_user As String
    Public operacion As String

    Public modalidad As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_Cpf = String.Empty
        codigo_test = String.Empty
        vigencia_Cpf = String.Empty
        eliminado_cpf = String.Empty
        codigo_Fac = String.Empty
        tiene_facultad = String.Empty
        codigo_per = String.Empty
        codigo_tfu = String.Empty
        cod_user = String.Empty
        operacion = String.Empty

        modalidad = String.Empty
    End Sub

#End Region

End Class

Public Class e_Anio

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public anio_inicio As String
    Public anio_fin As String    
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        anio_inicio = String.Empty
        anio_fin = String.Empty        
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_EgresadoAlumni

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_ega As String
    Public nivel_ega As String
    Public modalidad_ega As String
    Public codigo_fac As String
    Public codigo_cpf As String
    Public sexo_ega As String
    Public anio_egreso As String
    Public anio_bachiller As String
    Public anio_titulo As String
    Public nombre_ega As String
    Public codigo_pso As String
    Public emailPrincipal_pso As String
    Public emailAlternativo_pso As String
    Public prefijoTelefono_pso As String
    Public telefonoFijo_pso As String
    Public telefonoCelular_pso As String
    Public telefonoCelular2_pso As String
    Public actualmenteLabora_ega As String
    Public codigo_emp As String
    Public empresaLabora_ega As String
    Public cargoActual_ega As String
    Public prefijoTelEmp_ega As String
    Public telefonoEmp_ega As String
    Public correoEmp_ega As String
    Public celularEmp_ega As String
    Public cod_user As String
    Public operacion As String
    Public codigo_alu As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_ega = String.Empty
        nivel_ega = String.Empty
        modalidad_ega = String.Empty
        codigo_fac = String.Empty
        codigo_cpf = String.Empty
        sexo_ega = String.Empty
        anio_egreso = String.Empty
        anio_bachiller = String.Empty
        anio_titulo = String.Empty
        nombre_ega = String.Empty
        codigo_pso = String.Empty
        emailPrincipal_pso = String.Empty
        emailAlternativo_pso = String.Empty
        prefijoTelefono_pso = String.Empty
        telefonoFijo_pso = String.Empty
        telefonoCelular_pso = String.Empty
        telefonoCelular2_pso = String.Empty
        actualmenteLabora_ega = String.Empty
        codigo_emp = String.Empty
        empresaLabora_ega = String.Empty
        cargoActual_ega = String.Empty
        prefijoTelEmp_ega = String.Empty
        telefonoEmp_ega = String.Empty
        correoEmp_ega = String.Empty
        celularEmp_ega = String.Empty
        cod_user = String.Empty
        operacion = String.Empty
        codigo_alu = String.Empty
    End Sub

#End Region

End Class

Public Class e_CentroCostos

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_cco As String
    Public codigo_sco As String
    Public descripcion_cco As String
    Public codigo_test As String
    Public visibilidad_cco As Boolean
    Public cod_user As String
    Public operacion As String

    'Otros
    Public anio As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_cco = String.Empty
        codigo_sco = String.Empty
        descripcion_cco = String.Empty
        codigo_test = String.Empty
        visibilidad_cco = True
        cod_user = String.Empty
        operacion = String.Empty
        anio = String.Empty
    End Sub

#End Region

End Class

Public Class e_ActividadEvento

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_aev As String
    Public codigo_act As String
    Public codigo_cco As String
    Public nombre_aev As String
    Public fechahora_aev As String
    Public lugar_aev As String
    Public grupo_aev As String
    Public estado_aev As String
    Public fechahorafin_aev As String
    Public cupos_aev As String
    Public costo_aev As String
    Public codigo_sco As String
    Public urlEncuesta_aev As String
    Public inscritos As String
    Public envioSMS_aev As String
    Public envioSMS As String
    Public cod_user As String
    Public operacion As String    
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_aev = String.Empty
        codigo_act = String.Empty
        codigo_cco = String.Empty
        nombre_aev = String.Empty
        fechahora_aev = String.Empty
        lugar_aev = String.Empty
        grupo_aev = String.Empty
        estado_aev = String.Empty
        fechahorafin_aev = String.Empty
        cupos_aev = String.Empty
        costo_aev = String.Empty
        codigo_sco = String.Empty
        urlEncuesta_aev = String.Empty
        inscritos = String.Empty
        envioSMS_aev = String.Empty
        envioSMS = String.Empty
        cod_user = String.Empty
        operacion = String.Empty        
    End Sub

#End Region

End Class

Public Class e_Participacion

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_aev As String
    Public codigo_par As String
    Public fechahora_par As DateTime
    Public observacion_par As String
    Public cod_user As String
    Public operacion As String

    'Otros
    Public numero_doc As String
    Public codigo_pso As String
    Public codigo_cco As String
    Public ciclo_aca As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_aev = String.Empty
        codigo_par = String.Empty
        observacion_par = String.Empty
        fechahora_par = #1/1/1901#
        cod_user = String.Empty
        operacion = String.Empty

        'Otros
        numero_doc = String.Empty
        codigo_pso = String.Empty
        codigo_cco = String.Empty
        ciclo_aca = String.Empty
    End Sub

#End Region

End Class

Public Class e_EnvioSMS

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_env As String
    Public tabla_env As String
    Public codigoTabla_env As String
    Public codigo_per As String
    Public tipo_env As String
    Public mensaje_env As String
    Public cod_user As String
    Public operacion As String

    'Otros
    Public detalles As List(Of e_EnvioSMSDetalle)
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_env = String.Empty
        tabla_env = String.Empty
        codigoTabla_env = String.Empty
        codigo_per = String.Empty
        tipo_env = String.Empty
        mensaje_env = String.Empty
        cod_user = String.Empty
        operacion = String.Empty

        'Otros
        detalles = New List(Of e_EnvioSMSDetalle)
    End Sub

#End Region

End Class

Public Class e_EnvioSMSDetalle

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_end As String
    Public codigo_env As String
    Public codigo_pso As String
    Public nombre_end As String
    Public celular_end As String
    Public idRespuesta_end As String
    Public respuesta_end As String
    Public fechaRespuesta_end As String
    Public estado_end As String
    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_end = String.Empty
        codigo_env = String.Empty
        codigo_pso = String.Empty
        nombre_end = String.Empty
        celular_end = String.Empty        
        idRespuesta_end = String.Empty
        respuesta_end = String.Empty
        fechaRespuesta_end = "01/01/1901"
        estado_end = String.Empty        
        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_ActividadProgramacion

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_apr As String
    Public codigo_aev As String
    Public fechahoraini_apr As String
    Public fechahorafin_apr As String
    Public lugar_apr As String
    Public fecha As String
    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_apr = String.Empty
        codigo_aev = String.Empty
        fechahoraini_apr = "01/01/1901 00:00"
        fechahorafin_apr = "01/01/1901 00:00"
        lugar_apr = String.Empty
        fecha = "01/01/1901"
        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_Empresa

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_emp As String
    Public idPro As String
    Public ruc_emp As String
    Public razonSocial_emp As String
    Public nombreComercial_emp As String
    Public abreviatura_emp As String
    Public password_emp As String
    Public codigoTipo_cat As String
    Public codigo_sec As String
    Public codigo_dep As String
    Public codigo_pro As String
    Public codigo_dis As String
    Public direccion_emp As String
    Public correo_emp As String
    Public logo_emp As String    
    Public direccionWeb_emp As String
    Public prefijoTel_emp As String
    Public telefono_emp As String
    Public celular_emp As String
    Public codigoEstado_cat As String
    Public externo_emp As String
    Public codigoExterno_emp As String
    Public accesoCampus_emp As String
    Public actualizoInformacion_emp As String
    Public id_archivos_compartidos As String
    Public cod_user As String
    Public operacion As String

#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_emp = String.Empty
        idPro = String.Empty
        ruc_emp = String.Empty
        razonSocial_emp = String.Empty
        nombreComercial_emp = String.Empty
        abreviatura_emp = String.Empty
        password_emp = String.Empty
        codigoTipo_cat = String.Empty
        codigo_sec = String.Empty
        codigo_dep = String.Empty
        codigo_pro = String.Empty
        codigo_dis = String.Empty
        direccion_emp = String.Empty
        correo_emp = String.Empty
        logo_emp = String.Empty        
        direccionWeb_emp = String.Empty
        prefijoTel_emp = String.Empty
        telefono_emp = String.Empty
        celular_emp = String.Empty
        codigoEstado_cat = String.Empty
        externo_emp = String.Empty
        codigoExterno_emp = String.Empty
        accesoCampus_emp = String.Empty
        actualizoInformacion_emp = "S"
        id_archivos_compartidos = String.Empty
        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_CicloAcademico

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_cac As String
    Public descripcion_cac As String
    Public fechaIni_Cro As String
    Public fechaFin_Cro As String
    Public fvigente_cac As String
    '20200225-ENevado --------------------\
    Public fechaIniAdm_cro As Date
    Public fechafinAdm_cro As Date
    Public tipocac As String
    Public tipooperacion As String
    Public vigente_cac As Boolean
    Public notaMinima_cac As Double
    Public moraDiaria_cac As Double
    Public vigenciaaux_cac As Boolean
    Public admision_cac As Boolean
    Public admisionaux_cac As Boolean
    '-------------------------------------/
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_cac = String.Empty
        descripcion_cac = String.Empty
        fechaIni_Cro = String.Empty
        fechaFin_Cro = String.Empty
        fvigente_cac = String.Empty
        '20200225-ENevado --------------------\
        fechaIniAdm_cro = #1/1/1901#
        fechafinAdm_cro = #1/1/1901#
        tipocac = String.Empty
        tipooperacion = String.Empty
        vigente_cac = False
        notaMinima_cac = 13.5
        moraDiaria_cac = 0.5
        vigenciaaux_cac = False
        admision_cac = False
        admisionaux_cac = False
        '-------------------------------------/
    End Sub

#End Region

End Class

Public Class e_Alumno

#Region "constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_alu As String
    Public codigoUniver_alu As String
    Public alumno As String
    Public descripcion_pes As String
    Public estadoActual_alu As String
    Public CCreditosAprobados_alu As String
    Public totalCredElecObl_Pes As String
    Public totalCreObl_Pes As String
    Public cicloIng_alu As String
    Public EMAIL_alU As String
    Public EMAIL2_alu As String
    Public DebeTesis As String
    Public descripcion_Cac As String
    Public debeIdiomas_alu As String
    Public ultimaMat_alu As String
    Public codigo_cac As String
    Public tempcodigo_cpf As String
    Public nombreOficial_cpf As String
    Public codigo_Fac As String
    Public nombre_Fac As String
    Public creditosEgresar_pes As String
    Public descripcion_test As String
    Public creditosAprobadosActual_alu As String
    Public codigo_pes As String
    Public codigo_per As String
    Public nombres As String
    Public accion As String
    Public codigo_pcur As String
    Public nivel_ega As String
    Public tiene_diploma As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_alu = String.Empty
        codigoUniver_alu = String.Empty
        nombres = String.Empty
        cicloIng_alu = String.Empty
        estadoActual_alu = String.Empty
        ultimaMat_alu = String.Empty
        codigo_cac = String.Empty
        tempcodigo_cpf = String.Empty
        nombreOficial_cpf = String.Empty
        codigo_Fac = String.Empty
        nombre_Fac = String.Empty
        creditosEgresar_pes = String.Empty
        descripcion_test = String.Empty
        creditosAprobadosActual_alu = String.Empty
        codigo_pes = String.Empty
        codigo_per = String.Empty
        accion = String.Empty
        codigo_pcur = String.Empty
        nivel_ega = String.Empty
        tiene_diploma = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_RequisitoEgreso

#Region "constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_req As String
    Public codigo_pqur As String
    Public codigo_tip As String
    Public nombre As String
    Public cantidad As String
    Public codigo_cat As String
    Public indica_pe As String
    
#End Region

#Region "Metodos"
    Private Sub Inicializar()
        codigo_req = String.Empty
        codigo_pqur = String.Empty
        codigo_tip = String.Empty
        nombre = String.Empty
        cantidad = String.Empty
        codigo_cat = String.Empty
        indica_pe = String.Empty
    End Sub
#End Region

End Class

Public Class e_AlumnoRequisito

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_are As String
    Public codigo_alu As String
    Public codigo_req As String
    Public codigo_pcur As String
    Public codigo_tip As String
    Public observacion As String
    Public cumplio As String
    Public usuario_reg As String
    Public fecha_reg As String
    Public usuario_mod As String
    Public fecha_mod As String
    Public estado As String

#End Region

#Region "Metodos"

    Private Sub Inicializar()

        codigo_are = String.Empty
        codigo_alu = String.Empty
        codigo_req = String.Empty
        codigo_pcur = String.Empty
        codigo_tip = String.Empty
        observacion = String.Empty
        cumplio = String.Empty
        usuario_reg = String.Empty
        fecha_reg = String.Empty
        usuario_mod = String.Empty
        fecha_mod = String.Empty
        estado = String.Empty


    End Sub

#End Region

End Class

Public Class e_Personal

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_per As String
    Public apellidoPat_per As String
    Public apellidoMat_per As String
    Public nombres_per As String

    Public codigo_tfu As String
    Public nombres As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_per = String.Empty
        apellidoPat_per = String.Empty
        apellidoMat_per = String.Empty
        nombres_per = String.Empty

        codigo_tfu = String.Empty
        nombres = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_Departamento

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_dep As String
    Public nombre_dep As String
    Public codigo_pai As String
    Public ubigeo_dep As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_dep = String.Empty
        nombre_dep = String.Empty
        codigo_pai = String.Empty
        ubigeo_dep = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_Provincia

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_pro As String
    Public nombre_pro As String
    Public codigo_dep As String
    Public ubigeo_pro As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_pro = String.Empty
        nombre_pro = String.Empty
        codigo_dep = String.Empty
        ubigeo_pro = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_Distrito

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_dis As String
    Public nombre_dis As String
    Public codigo_pro As String
    Public ubigeo_dis As String
    Public ubigeoReniec As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_dis = String.Empty
        nombre_dis = String.Empty
        codigo_pro = String.Empty
        ubigeo_dis = String.Empty
        ubigeoReniec = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_PlanEstudio

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_Pes As String
    Public descripcion_Pes As String
    Public codigo_cpf As String
    Public codigo_ctf As String
    Public codigo_test As String
    Public vigencia_pes As String
    Public operadoraut_acr As String
    Public ind_ppcodigo_cpf As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_Pes = String.Empty
        descripcion_Pes = String.Empty
        codigo_cpf = String.Empty
        codigo_ctf = String.Empty
        codigo_test = String.Empty
        vigencia_pes = String.Empty
        operadoraut_acr = String.Empty
        ind_ppcodigo_cpf = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_Sector

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_sec As String
    Public nombre_sec As String
#End Region

#Region "Metodos"
    Private Sub Inicializar()
        codigo_sec = String.Empty
        nombre_sec = String.Empty
    End Sub
#End Region
End Class

Public Class e_InformacionContacto

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_inc As String
    Public codigo_emp As String
    Public idPro As String
    Public denominacion_inc As String
    Public apellidos_inc As String
    Public nombres_inc As String
    Public cargo_inc As String
    Public prefijoTel_inc As String
    Public telefono_inc As String
    Public celular_inc As String
    Public correo01_inc As String
    Public correo02_inc As String
    Public externo_inc As String
    Public codigoExterno_inc As String
    Public actualizoInformacion_inc As String
    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_inc = String.Empty
        codigo_emp = String.Empty
        idPro = String.Empty
        denominacion_inc = String.Empty
        apellidos_inc = String.Empty
        nombres_inc = String.Empty
        cargo_inc = String.Empty
        prefijoTel_inc = String.Empty
        telefono_inc = String.Empty
        celular_inc = String.Empty
        correo01_inc = String.Empty
        correo02_inc = String.Empty
        externo_inc = String.Empty
        codigoExterno_inc = String.Empty
        actualizoInformacion_inc = String.Empty
        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_RevisionEmpresa

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_rem As String
    Public codigo_emp As String
    Public codigoRevisor_pso As String
    Public codigoEstado_cat As String
    Public vigente_rem As String
    Public comentario_rem As String
    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_rem = String.Empty
        codigo_emp = String.Empty
        codigoRevisor_pso = String.Empty
        codigoEstado_cat = String.Empty
        vigente_rem = String.Empty
        comentario_rem = String.Empty
        cod_user = String.Empty
        operacion = String.Empty
        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_DetalleOfertaCarrera

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_ofc As String
    Public codigo_ofe As String
    Public codigo_cpf As String
    Public accion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()

        codigo_ofc = String.Empty
        codigo_ofe = String.Empty
        codigo_cpf = String.Empty
        accion = String.Empty

    End Sub

#End Region

End Class

Public Class e_ArchivoCompartido

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public id_archivos_compartidos As String
    Public nombre_archivo As String
    Public fecha As String
    Public extencion As String
    Public id_tabla As String
    Public id_transaccion As String
    Public nro_operacion As String
    Public descripcion As String
    Public ruta_archivo As String
    Public fecha_reg As String
    Public fecha_act As String
    Public usuario_reg As String
    Public usuario_act As String
    Public ip_reg As String
    Public ip_act As String
    Public token_tabla As String
    Public content_type As String
    Public id_encriptado As String

    Public cod_user As String
    Public operacion As String

    Public detalles As List(Of e_ArchivoCompartidoDetalle)
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        id_archivos_compartidos = String.Empty
        nombre_archivo = String.Empty
        fecha = String.Empty
        extencion = String.Empty
        id_tabla = String.Empty
        id_transaccion = String.Empty
        nro_operacion = String.Empty
        descripcion = String.Empty
        ruta_archivo = String.Empty
        fecha_reg = "01/01/1901"
        fecha_act = "01/01/1901"
        usuario_reg = String.Empty
        usuario_act = String.Empty
        ip_reg = String.Empty
        ip_act = String.Empty
        token_tabla = String.Empty
        content_type = String.Empty        
        id_encriptado = String.Empty
        cod_user = String.Empty
        operacion = String.Empty

        detalles = New List(Of e_ArchivoCompartidoDetalle)
    End Sub

#End Region

End Class

Public Class e_ArchivoCompartidoDetalle

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_acd As String
    Public id_archivos_compartidos As String
    Public tabla_acd As String
    Public codigoTabla_acd As String
    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_acd = String.Empty
        id_archivos_compartidos = String.Empty
        tabla_acd = String.Empty
        codigoTabla_acd = String.Empty
        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_DiagramaER

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public diagram_id As String
    Public diagram_name As String
    Public server_name As String    
    Public database_name As String
    Public database_name_origen As String
    Public database_name_destino As String
    Public server_name_origen As String
    Public server_name_destino As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        diagram_id = String.Empty
        diagram_name = String.Empty
        server_name = String.Empty        
        database_name = String.Empty
        database_name_origen = String.Empty
        database_name_destino = String.Empty
        server_name_origen = String.Empty
        server_name_destino = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_TipoParticipante

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_tpar As String
    Public descripcion_tpar As String
    Public operacion As String

#End Region

#Region "Metodos"

    Private Sub Inicializar()
        operacion = String.Empty
        codigo_tpar = String.Empty
        descripcion_tpar = String.Empty
    End Sub

#End Region

End Class

Public Class e_PlantillaOnomastico

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_plo As String
    Public imgHeader_plo As String
    Public imgFooter_plo As String
    Public vigente_plo As String

    Public cod_user As String
    Public operacion As String

#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_plo = String.Empty
        imgHeader_plo = String.Empty
        imgFooter_plo = String.Empty
        vigente_plo = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class
'-- olluen 26/03/2020
Public Class e_Horario

#Region "Constructor"
    Public Sub New()
        Inicializar()
    End Sub
#End Region

#Region "Propiedades"
    Public operacion As String
    Public nombre_cur As String
    Public codigo_cac As String
    Public codigo_cpf As String
    Public dia As String
    Public hora_ini As String
    Public hora_fin As String
    Public capacidad_actual As String
    Public capacidad_necesaria As String
    Public nro_mat As String
    Public codigo_cup As String
    Public codigo_lho As String
    Public codigo_amb As String
    Public codigo_tes As String
    Public codigo_act As String
#End Region

#Region "Metodos"
    Private Sub Inicializar()
        operacion = String.Empty
        nombre_cur = String.Empty
        codigo_cac = String.Empty
        codigo_cpf = String.Empty
        dia = String.Empty
        hora_ini = String.Empty
        hora_fin = String.Empty
        capacidad_actual = String.Empty
        capacidad_necesaria = String.Empty
        nro_mat = String.Empty
        codigo_cup = String.Empty
        codigo_lho = String.Empty
        codigo_amb = String.Empty
        codigo_tes = String.Empty
        codigo_act = String.Empty

    End Sub

#End Region
End Class

Public Class e_EventoVirtual

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public operacion As String
    Public codigo_evi As String
    Public nombre_evi As String
    Public nombrePonente_evi As String
    Public fechaHoraInicio_evi As Object
    Public fechaHoraFin_evi As Object
    Public url_evi As String
    Public tipo_evi As String
    Public estado_evi As String
    Public cod_usuario As Integer

#End Region

#Region "Metodos"

    Private Sub Inicializar()
        operacion = String.Empty
        codigo_evi = String.Empty
        nombre_evi = String.Empty
        nombrePonente_evi = String.Empty
        fechaHoraInicio_evi = DBNull.Value
        fechaHoraFin_evi = DBNull.Value
        url_evi = String.Empty
        tipo_evi = String.Empty
        estado_evi = "A"
        cod_usuario = 0
    End Sub

#End Region

End Class

Public Class e_InscripcionEventoVirtual

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public operacion As String
    Public codigo_iev As String
    Public codigo_alu As Integer
    Public codigo_evi As Integer
    Public nombreCompleto_iev As String
    Public numDocIdentidad_iev As String
    Public email_iev As String
    Public celular_iev As String
    Public estaTrabajando_iev As Object
    Public empresa_iev As String
    Public cargo_iev As String
    Public medioIngresoLaboral_iev As String
    Public codigo_tpar As Integer
    Public obtenerConstancia_iev As Object
    Public medioInscripcion_iev As String
    Public estado_iev As String
    Public usuarioReg_iev As Integer
    Public fechaHoraReg_iev As Object
    Public usuarioMod_iev As Integer
    Public fechaHoraMod_iev As Object
    Public fechaDesde As Object
    Public fechaHasta As Object
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        operacion = String.Empty
        codigo_iev = String.Empty
        codigo_alu = 0
        codigo_evi = 0
        nombreCompleto_iev = String.Empty
        numDocIdentidad_iev = String.Empty
        email_iev = String.Empty
        celular_iev = String.Empty
        estaTrabajando_iev = DBNull.Value
        empresa_iev = String.Empty
        cargo_iev = String.Empty
        medioIngresoLaboral_iev = String.Empty
        codigo_tpar = 0
        obtenerConstancia_iev = DBNull.Value
        medioInscripcion_iev = String.Empty
        estado_iev = "A"
        usuarioReg_iev = 0
        fechaHoraReg_iev = DBNull.Value
        usuarioMod_iev = 0
        fechaHoraMod_iev = DBNull.Value
        fechaDesde = DBNull.Value
        fechaHasta = DBNull.Value
    End Sub

#End Region

End Class

Public Class e_Cronograma

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_cro As String
    Public codigo_act As String
    Public codigo_cac As String
    Public codigo_test As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_cro = String.Empty
        codigo_act = String.Empty
        codigo_cac = String.Empty
        codigo_test = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_ComunicadoPersonal

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_cpe As String
    Public codigo_per As String
    Public numeroComunicado_cpe As String
    Public nombreComunicado_cpe As String
    Public tipo_cpe As String
    Public fechaVigenciaIni_cpe As DateTime
    Public fechaVigenciaFin_cpe As DateTime
    Public indDescarga_cpe As String
    Public fechaDescarga_cpe As DateTime
    Public descripcionVar1_cpe As String
    Public var1_cpe As String
    Public descripcionVar2_cpe As String
    Public var2_cpe As String

    Public cod_user As String
    Public operacion As String
    Public verificaVigencia As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_cpe = String.Empty
        codigo_per = String.Empty
        numeroComunicado_cpe = String.Empty
        nombreComunicado_cpe = String.Empty
        tipo_cpe = String.Empty
        fechaVigenciaIni_cpe = #1/1/1901#
        fechaVigenciaFin_cpe = #1/1/1901#
        indDescarga_cpe = String.Empty
        fechaDescarga_cpe = #1/1/1901#
        descripcionVar1_cpe = String.Empty
        var1_cpe = String.Empty
        descripcionVar2_cpe = String.Empty
        var2_cpe = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
        verificaVigencia = String.Empty
    End Sub

#End Region

End Class

Public Class e_TipoFuncion

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_tfu As String
    Public descripcion_tfu As String
    Public abreviatura_tfu As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_tfu = String.Empty
        descripcion_tfu = String.Empty
        abreviatura_tfu = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_Aplicacion

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_apl As String
    Public descripcion_apl As String

    Public codigo_tfu As String
    Public codigo_per As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_apl = String.Empty
        descripcion_apl = String.Empty

        codigo_tfu = String.Empty
        codigo_per = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_DatosPersonal

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_per As String
    Public direccion_per As String
    Public email_per As String
    Public email_alternativo_per As String
    Public celular_per As String
    Public telefono_per As String
    Public operadorCelular_per As String
    Public operadorInternet_per As String
    Public actualizoDatos_per As String
    Public codigo_pro As String
    Public distrito As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_per = String.Empty
        direccion_per = String.Empty
        email_per = String.Empty
        email_alternativo_per = String.Empty
        celular_per = String.Empty
        telefono_per = String.Empty
        operadorCelular_per = String.Empty
        operadorInternet_per = String.Empty
        actualizoDatos_per = String.Empty
        codigo_pro = String.Empty
        distrito = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_MotivoNotaAbono

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_mno As String
    Public descripcion_mno As String
    Public codigo_pco As String
    Public bloqueaAgregadoRetiros As Boolean
    Public conveniobeca As Boolean
    Public solicitudAnulacion As Boolean
    Public codigo_gmn As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_mno = String.Empty
        descripcion_mno = String.Empty
        codigo_pco = String.Empty
        bloqueaAgregadoRetiros = False
        conveniobeca = False
        solicitudAnulacion = False
        codigo_gmn = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_GrupoMotivoAbono

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_gmn As String
    Public nombre_gmn As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_gmn = String.Empty
        nombre_gmn = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_Adeudos

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_ade As String
    Public codigo_alu As String
    Public codigoArea_cco As String
    Public codigo_tfu As String
    Public codigo_sco As String
    Public codigo_cco As String
    Public codigoUniver_alu As String
    Public nombre_alu As String
    Public motivo_ade As String
    Public fechaDeuda_ade As String
    Public monto_ade As String
    Public codigo_deu As String
    Public fechaCancelado_ade As String
    Public comentario_ade As String
    Public estado_ade As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_ade = String.Empty
        codigo_alu = String.Empty
        codigoArea_cco = String.Empty
        codigo_tfu = String.Empty
        codigo_sco = String.Empty
        codigo_cco = String.Empty
        codigoUniver_alu = String.Empty
        nombre_alu = String.Empty
        motivo_ade = String.Empty
        fechaDeuda_ade = #1/1/1901#
        monto_ade = String.Empty
        codigo_deu = String.Empty
        fechaCancelado_ade = #1/1/1901#
        comentario_ade = String.Empty
        estado_ade = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_ConfiguracionInstanciasAdeudos

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_cia As String
    Public codigoArea_cco As String
    Public codigo_tfu As String
    Public codigo_sco As String
    Public codigo_cco As String
    Public estado_cia As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_cia = String.Empty
        codigoArea_cco = String.Empty
        codigo_tfu = String.Empty
        codigo_sco = String.Empty
        codigo_cco = String.Empty
        estado_cia = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_AsistenciaDocente

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_ado As String
    Public codigo_hdo As String
    Public codigo_cup As String    
    Public codigo_lho As String
    Public codigo_hop As String
    Public codigo_per As String    
    Public descripcionHorario_ado As String    
    Public tipo As String
    Public observacion As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_ado = String.Empty
        codigo_hdo = String.Empty
        codigo_cup = String.Empty        
        codigo_lho = String.Empty
        codigo_hop = String.Empty
        codigo_per = String.Empty        
        descripcionHorario_ado = String.Empty        
        tipo = String.Empty
        observacion = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_Consejo

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_con As String
    Public nombre_con As String
    Public codigo_fac As String
    Public vigencia_gyt As String
    Public operacion As String
    Public abreviatura_con As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_con = String.Empty
        nombre_con = String.Empty
        codigo_fac = String.Empty
        vigencia_gyt = String.Empty
        operacion = String.Empty
        abreviatura_con = String.Empty
    End Sub

#End Region

End Class

Public Class e_ConsejoFacultad

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_cjf As String
    Public codigo_fac As String
    Public codigo_pcc As String
    Public estado_cjf As String
    Public cargo_cjf As String
    Public codigo_con As String
    Public codigo_cgo As String
    Public usuario As String

    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_cjf = String.Empty
        codigo_fac = String.Empty
        codigo_pcc = String.Empty
        estado_cjf = String.Empty
        cargo_cjf = String.Empty
        codigo_con = String.Empty
        codigo_cgo = String.Empty
        operacion = String.Empty
        usuario = String.Empty

    End Sub

#End Region

End Class

Public Class e_SesionConsejoUniv_GYT

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_scu As String
    Public descripcion_scu As String
    Public fecha_scu As String
    Public estado_scu As String
    Public usuario_reg As String
    Public fecha_reg As String
    Public vigencia_scu As String
    Public abreviatura_con As String
    Public tipo_sesion As String
    Public codigo_fac As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_scu = String.Empty
        descripcion_scu = String.Empty
        fecha_scu = String.Empty
        estado_scu = String.Empty
        usuario_reg = String.Empty
        fecha_reg = String.Empty
        vigencia_scu = String.Empty
        abreviatura_con = String.Empty
        tipo_sesion = String.Empty
        codigo_fac = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_ServicioConcepto

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_sco As String
    Public tipo_sco As String
    Public descripcion_sco As String
    Public adeudo_sco As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_sco = String.Empty
        tipo_sco = String.Empty
        descripcion_sco = String.Empty
        adeudo_sco = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_TramiteAlumno

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_trl As String
    Public codigo_alu As String
    Public codigo_cac As String
    Public correlativo_trl As String
    Public glosaCorrelativo_trl As String
    Public fechaReg_trl As String
    Public estado_trl As String
    Public observacion_trl As String
    Public fechaMod_AUD As String
    Public usuarioMod_AUD As String
    Public codigo_tfu As String
    Public estado_dft As String
    Public estadoAprobacion As String

    Public operacion As String
    Public fechaIni As String
    Public fechaFin As String


#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_trl = String.Empty
        codigo_alu = String.Empty
        codigo_cac = String.Empty
        correlativo_trl = String.Empty
        glosaCorrelativo_trl = String.Empty
        fechaReg_trl = String.Empty
        estado_trl = String.Empty
        observacion_trl = String.Empty
        fechaMod_AUD = String.Empty
        usuarioMod_AUD = String.Empty
        codigo_tfu = String.Empty
        estado_dft = String.Empty
        estadoAprobacion = String.Empty

        operacion = String.Empty
        fechaIni = String.Empty
        fechaFin = String.Empty

    End Sub

#End Region

End Class

Public Class e_Tesis

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_Tes As String
    Public FechaReg_Tes As String
    Public Codigo_TIn As String
    Public CodigoReg_Tes As String
    Public Titulo_Tes As String
    Public FechaInicio_Tes As String
    Public FechaFin_Tes As String
    Public url_Tes As String
    Public fechaUrl_tes As String
    Public Codigo_per As String

    Public operacion As String
   
#End Region

#Region "Metodos"

    Private Sub Inicializar()

        codigo_Tes = String.Empty
        FechaReg_Tes = String.Empty
        Codigo_TIn = String.Empty
        CodigoReg_Tes = String.Empty
        Titulo_Tes = String.Empty
        FechaInicio_Tes = String.Empty
        FechaFin_Tes = String.Empty
        url_Tes = String.Empty
        fechaUrl_tes = String.Empty
        Codigo_per = String.Empty

        operacion = String.Empty

    End Sub

#End Region

End Class

Public Class e_GrupoEgresado

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_gru As String

    Public operacion As String
    Public cod_user As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_gru = String.Empty

        operacion = String.Empty
        cod_user = String.Empty
    End Sub

#End Region

End Class

Public Class e_TipoDenominacionGradoTitulo

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_tdg As String

    Public operacion As String
    Public cod_user As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_tdg = String.Empty

        operacion = String.Empty
        cod_user = String.Empty
    End Sub

#End Region

End Class

Public Class e_EnvioDiplomasProveedor

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_edp As String
    Public tipo_edp As String
    Public codigo_scu As String
    Public codigo_tdg As String
    Public tipo_emision As String
    Public codigo_dta As String
    Public codigo_tfu As String

    Public detalles As List(Of e_EnvioDiplomasProveedorDetalle)

    Public operacion As String
    Public cod_user As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_edp = String.Empty
        tipo_edp = String.Empty
        codigo_scu = String.Empty
        codigo_tdg = String.Empty
        tipo_emision = String.Empty
        codigo_dta = String.Empty
        codigo_tfu = String.Empty
        detalles = New List(Of e_EnvioDiplomasProveedorDetalle)

        operacion = String.Empty
        cod_user = String.Empty
    End Sub

#End Region

End Class

Public Class e_EnvioDiplomasProveedorDetalle

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_edd As String
    Public codigo_edp As String
    Public codigo_trl As String
    Public codigo_egr As String    
    Public codigoOperacionGrupo As String
    Public estadoOperacionGrupo As String
    Public mensajeOperacionGrupo As String
    Public estadoOperacionFirma As String
    Public mensajeOperacionFirma As String

    Public operacion As String
    Public cod_user As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_edd = String.Empty
        codigo_edp = String.Empty
        codigo_trl = String.Empty
        codigo_egr = String.Empty

        codigoOperacionGrupo = String.Empty
        estadoOperacionGrupo = String.Empty
        mensajeOperacionGrupo = String.Empty
        estadoOperacionFirma = String.Empty
        mensajeOperacionFirma = String.Empty

        operacion = String.Empty
        cod_user = String.Empty
    End Sub

#End Region

End Class

Public Class e_ControlPersonal

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_cpe As String
    Public codigo_per As String
    Public tipo As String

    Public operacion As String
    Public cod_user As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_cpe = String.Empty
        codigo_per = String.Empty
        tipo = String.Empty

        operacion = String.Empty
        cod_user = String.Empty
    End Sub

#End Region

End Class

Public Class e_Marcaciones

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_mar As String
    Public dni_per As String
    Public id_marcador As String
    Public procesado_mar As String
    Public tipo_mar As String
    Public id_marcacion_zk As String
    Public codigo_cpe As String
    Public tipo_operacion As String

    Public operacion As String
    Public cod_user As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_mar = String.Empty
        dni_per = String.Empty
        id_marcador = String.Empty
        procesado_mar = String.Empty
        tipo_mar = String.Empty
        id_marcacion_zk = String.Empty
        codigo_cpe = String.Empty
        tipo_operacion = String.Empty
        operacion = String.Empty
        cod_user = String.Empty
    End Sub

#End Region

End Class

Public Class e_RecursoVirtual

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_rvi As String
    Public tipoRepo_rvi As String
    Public disciplinaRepo_rvi As String
    Public nombre_rvi As String
    Public logo_rvi As String
    Public contarVisita_rvi As String
    Public codigo_biv As String
    Public acceso_rvi As String
    Public orden_rvi As String
    Public IdArchivosCompartidos As String
    Public estado_rvi As String

    Public operacion As String
    Public cod_user As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_rvi = String.Empty
        tipoRepo_rvi = String.Empty
        disciplinaRepo_rvi = String.Empty
        nombre_rvi = String.Empty
        logo_rvi = String.Empty
        contarVisita_rvi = String.Empty
        codigo_biv = String.Empty
        acceso_rvi = String.Empty
        orden_rvi = String.Empty
        IdArchivosCompartidos = String.Empty
        estado_rvi = String.Empty

        operacion = String.Empty
        cod_user = String.Empty
    End Sub

#End Region

End Class

Public Class e_RecursoVirtualDetalle

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_rvd As String
    Public codigo_rvi As String
    Public titulo_rvd As String
    Public cuerpo_rvd As String
    Public acceso_rvd As String
    Public orden_rvd As String

    Public operacion As String
    Public cod_user As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_rvd = String.Empty
        codigo_rvi = String.Empty
        titulo_rvd = String.Empty
        cuerpo_rvd = String.Empty
        acceso_rvd = String.Empty
        orden_rvd = String.Empty

        operacion = String.Empty
        cod_user = String.Empty
    End Sub

#End Region

End Class

Public Class e_RecursoVirtualEnlace

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_rve As String
    Public codigo_rvd As String
    Public descripcion_rve As String
    Public enlace_rve As String
    Public contarVisita_rve As String
    Public codigo_biv As String
    Public acceso_rve As String
    Public orden_rve As String
    Public IdArchivosCompartidos As String

    Public enlace As String

    Public tipo_vis As String
    Public codigo_vis As String    

    Public operacion As String
    Public cod_user As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_rve = String.Empty
        codigo_rvd = String.Empty
        descripcion_rve = String.Empty
        enlace_rve = String.Empty
        contarVisita_rve = String.Empty
        codigo_biv = String.Empty
        acceso_rve = String.Empty
        orden_rve = String.Empty
        IdArchivosCompartidos = String.Empty

        tipo_vis = String.Empty
        codigo_vis = String.Empty
        codigo_biv = String.Empty
        enlace = String.Empty

        operacion = String.Empty
        cod_user = String.Empty
    End Sub

#End Region

End Class

Public Class e_BibliotecaVirtual

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_biv As String
    Public nombre_biv As String

    Public operacion As String
    Public cod_user As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_biv = String.Empty
        nombre_biv = String.Empty

        operacion = String.Empty
        cod_user = String.Empty
    End Sub

#End Region

End Class

Public Class e_DerechoHabientes

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_dhab As String
    Public codigo_per As String
    Public nombre As String
    Public edad As String

    Public codigo_niv As String
    Public nivel As String
    Public codigo_gra As String
    Public grado As String
    Public centro_estudios As String
    Public IdArchivosCompartidosRecibo As String
    Public IdArchivosCompartidosDNI As String

    Public operacion As String
    Public cod_user As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_dhab = String.Empty
        codigo_per = String.Empty
        nombre = String.Empty
        edad = String.Empty

        codigo_niv = String.Empty
        nivel = String.Empty
        codigo_gra = String.Empty
        grado = String.Empty
        centro_estudios = String.Empty
        IdArchivosCompartidosRecibo = 0
        IdArchivosCompartidosDNI = 0

        operacion = String.Empty
        cod_user = String.Empty
    End Sub

#End Region

End Class

Public Class e_SolicitudEscolaridad

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_soe As String
    Public codigo_dhab As String
    Public estado_soe As String
    Public tipocentroestudio_soe As String
    Public nombrecentroestudio_soe As String
    Public grado_soe As String
    Public centroaplicacion_soe As Boolean
    Public documentosadjuntos_soe As String
    Public IdArchivosCompartidosRecibo As String
    Public IdArchivosCompartidosDNI As String
    Public codigo_per As String
    Public anio_soe As String

    Public operacion As String
    Public cod_user As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()        
        codigo_soe = String.Empty
        codigo_dhab = String.Empty
        estado_soe = String.Empty
        tipocentroestudio_soe = String.Empty
        nombrecentroestudio_soe = String.Empty
        grado_soe = String.Empty
        centroaplicacion_soe = False
        documentosadjuntos_soe = String.Empty
        IdArchivosCompartidosRecibo = String.Empty
        IdArchivosCompartidosDNI = String.Empty
        codigo_per = String.Empty
        anio_soe = String.Empty

        operacion = String.Empty
        cod_user = String.Empty
    End Sub

#End Region

End Class

#End Region

#Region "DATOS"

Public Class d_Categoria
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function RegistrarCategoria(ByVal le_Categoria As e_Categoria) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GEN_CategoriaIUD", le_Categoria.operacion, _
                                     le_Categoria.cod_user, _
                                     le_Categoria.codigo_cat, _
                                     le_Categoria.nombre_cat, _
                                     le_Categoria.grupo_cat, _
                                     le_Categoria.codigoSuperior, _
                                     le_Categoria.abreviatura_cat)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarCategoria(ByVal le_Categoria As e_Categoria) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GEN_CategoriaListar", le_Categoria.operacion, _
                                    le_Categoria.codigo_cat, _
                                    le_Categoria.grupo_cat, _
                                    le_Categoria.nombre_cat, _
                                    le_Categoria.abreviatura_cat)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarGrupo(ByVal le_Categoria As e_Categoria) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GEN_GrupoListar", le_Categoria.operacion, _
                                    le_Categoria.grupo_cat)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetCategoria(ByVal codigo As Integer) As e_Categoria
        Try
            Dim me_Categoria As New e_Categoria

            If codigo > 0 Then
                me_Categoria.operacion = "GEN"
                me_Categoria.codigo_cat = codigo

                dt = ListarCategoria(me_Categoria)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de categoría no ha sido encontrado.")

                me_Categoria = New e_Categoria

                With me_Categoria
                    .codigo_cat = dt.Rows(0).Item("codigo_cat")
                    .nombre_cat = dt.Rows(0).Item("nombre_cat")
                    .grupo_cat = dt.Rows(0).Item("grupo_cat")
                    .codigoSuperior = dt.Rows(0).Item("codigoSuperior_cat")
                    .abreviatura_cat = dt.Rows(0).Item("abreviatura_cat")
                End With
            Else
                With me_Categoria
                    .codigo_cat = 0
                    .codigoSuperior = 0
                    .cod_user = 0
                End With
            End If

            Return me_Categoria
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_TipoEstudio
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarTipoEstudio(ByVal le_TipoEstudio As e_TipoEstudio) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_TipoEstudioListar", le_TipoEstudio.operacion, _
                                    le_TipoEstudio.codigo_test)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_Facultad
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarFacultad(ByVal le_Facultad As e_Facultad) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_FacultadListar", le_Facultad.operacion, _
                                    le_Facultad.codigo_Fac, _
                                    le_Facultad.codigo_per)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
End Class
Public Class d_Horario

    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    '20200326 - Olluen =====================================================
    'para obtener el ciclo academico Horario
    '=======================================================================
    Public Function ObtenerCicloAcademicoHorario(ByVal obj As e_CicloAcademico) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            dt = cnx.TraerDataTable("ConsultarCicloAcademico", obj.tipooperacion, obj.tipocac)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    '20200326 - Olluen =====================================================
    'para listar Carrera Profesional horario
    '=======================================================================
    Public Function ListarCarreraProfesionalHorario(ByVal le_CarreraProfesional As e_CarreraProfesional) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ConsultarCarreraProfesional", le_CarreraProfesional.operacion, _
                                    le_CarreraProfesional.codigo_test)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    '20200326 - Olluen =====================================================
    'para obtener las carreras post grado -- armar horario
    '=======================================================================
    Public Function ListarCarreraPostGrado(ByVal le_CarreraProfesional As e_CarreraProfesional) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ACAD_CarrerasPostgrado", le_CarreraProfesional.operacion, _
                                    le_CarreraProfesional.codigo_test)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    '20200326 - Olluen =====================================================
    'para Consultar el acceso -- armar horario
    '=======================================================================
    Public Function ConsultarAcceso(ByVal le_CarreraProfesional As e_CarreraProfesional) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("consultaracceso", le_CarreraProfesional.operacion, _
                                    le_CarreraProfesional.codigo_test, le_CarreraProfesional.cod_user)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function Horario_ListarAmbientesCursos(ByVal le_horario As e_Horario) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("Horario_ListarAmbientesCursos", le_horario.operacion, le_horario.codigo_cup)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function Horario_ListarAmbientesParaCambiar(ByVal le_horario As e_Horario) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("Horario_ListarAmbientesParaCambiar", le_horario.operacion, _
                                    le_horario.dia, le_horario.hora_ini, le_horario.hora_fin, le_horario.capacidad_actual, le_horario.capacidad_necesaria, _
                                    le_horario.nro_mat, le_horario.codigo_cac)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function Horario_ListarCursosProgramadosParaCambiar(ByVal le_horario As e_Horario) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("Horario_ListaCursosProgramados", le_horario.operacion, _
                                    le_horario.nombre_cur, le_horario.codigo_cac, le_horario.codigo_cpf)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function Horario_CambiarAmbiente(ByVal le_horario As e_Horario) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            dt = cnx.TraerDataTable("Horario_cambioAmbiente", le_horario.codigo_lho, le_horario.codigo_amb)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function Horario_ValidaCambioAmbiente(ByVal le_horario As e_Horario) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            dt = cnx.TraerDataTable("Horario_ValidaCambioAmbiente", le_horario.operacion, le_horario.codigo_tes, le_horario.codigo_cac, le_horario.codigo_act)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function


End Class

Public Class d_CarreraProfesional
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarCarreraProfesional(ByVal le_CarreraProfesional As e_CarreraProfesional) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_CarreraProfesionalListar", le_CarreraProfesional.operacion, _
                                    le_CarreraProfesional.codigo_Cpf, _
                                    le_CarreraProfesional.codigo_test, _
                                    le_CarreraProfesional.vigencia_Cpf, _
                                    le_CarreraProfesional.eliminado_cpf, _
                                    le_CarreraProfesional.codigo_Fac, _
                                    le_CarreraProfesional.tiene_facultad, _
                                    le_CarreraProfesional.codigo_per, _
                                    le_CarreraProfesional.modalidad)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarCarreraProfesionalByAcceso(ByVal le_CarreraProfesional As e_CarreraProfesional) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("Alumni_ListarCarrerasxAccesoxtest", le_CarreraProfesional.codigo_per, _
                                    le_CarreraProfesional.codigo_tfu, le_CarreraProfesional.codigo_test)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarCarreraByCodOfe(ByVal codigo_ofe As String) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_ListarCarreraByCodOferta", codigo_ofe)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ConsultarCarreraProfesional(ByVal le_CarreraProfesional As e_CarreraProfesional) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("EVE_ConsultarCarreraProfesional", le_CarreraProfesional.codigo_test, _
                                    le_CarreraProfesional.codigo_tfu, _
                                    le_CarreraProfesional.codigo_per)

            Dim filas As DataRowCollection = dt.Rows
            Dim fila_encontrada As DataRow
            Dim ind_encontro As Boolean = False

            For Each fila As DataRow In filas
                If fila("nombre_cpf") = "TODOS" Then fila_encontrada = fila : ind_encontro = True
            Next

            If ind_encontro Then filas.Remove(fila_encontrada)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function


End Class

Public Class d_Anio
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarAnio(ByVal le_Anio As e_Anio) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_AnioListar", le_Anio.operacion, _
                                    le_Anio.anio_inicio, _
                                    le_Anio.anio_fin)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_Oferta
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable
    Public Function LitarOferta(ByVal le_Oferta As e_Oferta) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            dt = cnx.TraerDataTable("ALUMNI_ListaOfertasByFechasAndCarrera", le_Oferta.fechaIniReg, le_Oferta.fechaFinReg)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function ListaOfertaByCodOfe(ByVal le_Ofertas As e_Oferta) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            dt = cnx.TraerDataTable("ALUMNI_RetornaOferta", le_Ofertas.codigo_ofe)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function InsertaUpdateOferta(ByVal le_Ofertas As e_Oferta) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            dt = cnx.TraerDataTable("ALUMNI_RegistraOferta", le_Ofertas.codigo_ofe, _
                                    le_Ofertas.idPro, _
                                    le_Ofertas.codigo_dep, _
                                    le_Ofertas.titulo_ofe, _
                                    le_Ofertas.descripcion_ofe, _
                                    le_Ofertas.requisitos_ofe, _
                                    le_Ofertas.contacto_ofe, _
                                    le_Ofertas.correocontacto_ofe, _
                                    le_Ofertas.telefonocontacto_ofe, _
                                    le_Ofertas.lugar_ofe, _
                                    le_Ofertas.tipotrabajo_ofe, _
                                    le_Ofertas.duracion_ofe, _
                                    le_Ofertas.fechaInicioAnuncio, _
                                    le_Ofertas.fechaFinAnuncio, _
                                    le_Ofertas.codigo_sec, _
                                    le_Ofertas.visible_ofe, _
                                    le_Ofertas.estado_ofe, _
                                    le_Ofertas.web_ofe, _
                                    le_Ofertas.modopostular_ofe, _
                                    le_Ofertas.mostrarcorreocontacto, _
                                    le_Ofertas.tipo_oferta, _
                                    le_Ofertas.desc_banner, _
                                    le_Ofertas.codigo_emp)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function ActualizarEstadoOferta(ByVal le_Ofertas As e_Oferta) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            dt = cnx.TraerDataTable("ALUMNI_ActualizaEstadoOfertaLab", le_Ofertas.codigo_ofe, le_Ofertas.estado_ofe)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_EgresadoAlumni

    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarEgresadoAlumni(ByVal le_EgresadoAlumni As e_EgresadoAlumni) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_EgresadoAlumniListar", le_EgresadoAlumni.operacion, _
                                    le_EgresadoAlumni.codigo_ega, _
                                    le_EgresadoAlumni.nivel_ega, _
                                    le_EgresadoAlumni.modalidad_ega, _
                                    le_EgresadoAlumni.codigo_fac, _
                                    le_EgresadoAlumni.codigo_cpf, _
                                    le_EgresadoAlumni.sexo_ega, _
                                    le_EgresadoAlumni.anio_egreso, _
                                    le_EgresadoAlumni.anio_bachiller, _
                                    le_EgresadoAlumni.anio_titulo, _
                                    le_EgresadoAlumni.nombre_ega)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarEgresadoAlumni(ByVal le_EgresadoAlumni As e_EgresadoAlumni) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_EgresadoAlumniIUD", le_EgresadoAlumni.operacion, _
                        le_EgresadoAlumni.cod_user, _
                        le_EgresadoAlumni.codigo_ega, _
                        le_EgresadoAlumni.codigo_pso, _
                        le_EgresadoAlumni.emailPrincipal_pso, _
                        le_EgresadoAlumni.emailAlternativo_pso, _
                        le_EgresadoAlumni.prefijoTelefono_pso, _
                        le_EgresadoAlumni.telefonoFijo_pso, _
                        le_EgresadoAlumni.telefonoCelular_pso, _
                        le_EgresadoAlumni.telefonoCelular2_pso, _
                        le_EgresadoAlumni.actualmenteLabora_ega, _
                        le_EgresadoAlumni.codigo_emp, _
                        le_EgresadoAlumni.empresaLabora_ega, _
                        le_EgresadoAlumni.cargoActual_ega, _
                        le_EgresadoAlumni.prefijoTelEmp_ega, _
                        le_EgresadoAlumni.telefonoEmp_ega, _
                        le_EgresadoAlumni.correoEmp_ega, _
                        le_EgresadoAlumni.celularEmp_ega)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_CentroCostos

    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarCentroCostosXPermisos(ByVal le_CentroCostos As e_CentroCostos) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("EVE_ConsultarCentroCostosXPermisosXVisibilidad", le_CentroCostos.operacion, _
                                    le_CentroCostos.cod_user, _
                                    le_CentroCostos.descripcion_cco, _
                                    le_CentroCostos.codigo_test, _
                                    le_CentroCostos.visibilidad_cco)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarEventosAlumni(ByVal le_CentroCostos As e_CentroCostos) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_ListarEventosAlumni", _
                                    le_CentroCostos.anio, _
                                    le_CentroCostos.descripcion_cco)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarCentroCostos(ByVal le_CentroCostos As e_CentroCostos) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PEN_CentroCostosListar", le_CentroCostos.operacion, _
                                    le_CentroCostos.codigo_cco, _
                                    le_CentroCostos.codigo_sco)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_ActividadEvento

    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function BuscarActividadEvento(ByVal le_ActividadEvento As e_ActividadEvento) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("EVE_BuscaActividadEvento", le_ActividadEvento.codigo_aev, _
                                    le_ActividadEvento.codigo_cco)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarActividadEvento(ByVal le_ActividadEvento As e_ActividadEvento) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_ActividadEventoIUD", le_ActividadEvento.operacion, _
                         le_ActividadEvento.codigo_aev, _
                         le_ActividadEvento.codigo_act, _
                         le_ActividadEvento.codigo_cco, _
                         le_ActividadEvento.nombre_aev, _
                         le_ActividadEvento.fechahora_aev, _
                         le_ActividadEvento.lugar_aev, _
                         le_ActividadEvento.grupo_aev, _
                         le_ActividadEvento.estado_aev, _
                         le_ActividadEvento.fechahorafin_aev, _
                         le_ActividadEvento.cupos_aev, _
                         le_ActividadEvento.costo_aev, _
                         le_ActividadEvento.codigo_sco, _
                         le_ActividadEvento.urlEncuesta_aev)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ConsultarConfiguracionAsistencia(ByVal le_ActividadEvento As e_ActividadEvento) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("EVE_ConsultaPermisosEvento", _
                                    le_ActividadEvento.codigo_cco)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarActividadEvento(ByVal le_ActividadEvento As e_ActividadEvento) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_ActividadEventoListar", _
                                    le_ActividadEvento.operacion, _
                                    le_ActividadEvento.codigo_aev, _
                                    le_ActividadEvento.codigo_cco)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetActividadEvento(ByVal codigo As Integer) As e_ActividadEvento
        Try
            Dim me_ActividadEvento As New e_ActividadEvento

            If codigo > 0 Then
                me_ActividadEvento.operacion = "GEN"
                me_ActividadEvento.codigo_aev = codigo

                dt = ListarActividadEvento(me_ActividadEvento)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de actividad no ha sido encontrado.")

                me_ActividadEvento = New e_ActividadEvento

                With me_ActividadEvento
                    .codigo_aev = dt.Rows(0).Item("codigo_aev")
                    .codigo_act = dt.Rows(0).Item("codigo_act")
                    .codigo_cco = dt.Rows(0).Item("codigo_cco")
                    .nombre_aev = dt.Rows(0).Item("nombre_aev")
                    .fechahora_aev = dt.Rows(0).Item("fechahora_aev")
                    .lugar_aev = dt.Rows(0).Item("lugar_aev")
                    .grupo_aev = dt.Rows(0).Item("grupo_aev")
                    .estado_aev = dt.Rows(0).Item("estado_aev")
                    .fechahorafin_aev = dt.Rows(0).Item("fechahorafin_aev")
                    .cupos_aev = dt.Rows(0).Item("cupos_aev")
                    .costo_aev = dt.Rows(0).Item("costo_aev")
                    .codigo_sco = dt.Rows(0).Item("codigo_sco")
                    .urlEncuesta_aev = dt.Rows(0).Item("urlEncuesta_aev")
                    .envioSMS = dt.Rows(0).Item("envioSMS")
                    .inscritos = dt.Rows(0).Item("inscritos")
                End With
            Else
                With me_ActividadEvento
                    .codigo_aev = 0
                    .codigo_act = 0
                    .codigo_cco = 0
                    .fechahora_aev = "01/01/1901"
                    .grupo_aev = 0
                    .estado_aev = "A"
                    .fechahorafin_aev = "01/01/1901"
                    .cupos_aev = 0
                    .costo_aev = 0
                    .codigo_sco = 0
                    .inscritos = 0
                End With

            End If

            Return me_ActividadEvento
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function BuscarActividades() As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("EVE_BuscaActividades", 0, String.Empty)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    '--------------- olluen 27/02/2020 -----------------
    Public Function ConsultarTipoParticipante() As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("EVE_ConsultarTipoParticipante")

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function BuscarServicioConcepto(ByVal le_ActividadEvento As e_ActividadEvento) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("EVE_ConsultarServicioPorCeco", le_ActividadEvento.codigo_cco)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_Participacion

    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarParticipacion(ByVal le_Participacion As e_Participacion) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_ParticipacionListar", le_Participacion.operacion, _
                                    le_Participacion.codigo_par, _
                                    le_Participacion.codigo_aev)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Sub RegistrarParticipacion(ByVal le_Participacion As e_Participacion)
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("EVE_RegistrarParticipacion", _
                                    le_Participacion.codigo_cco, _
                                    le_Participacion.codigo_aev, _
                                    le_Participacion.codigo_pso, _
                                    le_Participacion.observacion_par, _
                                    le_Participacion.fechahora_par)

            cnx.TerminarTransaccion()
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Sub

    Public Function BuscarPersonaXDocumento(ByVal le_Participacion As e_Participacion) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("EVE_BuscaPersonaxDocumento", _
                                    le_Participacion.numero_doc)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function BuscarDeuda(ByVal le_Participacion As e_Participacion) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("EVE_BuscaDeudaPersonaPagoWeb", _
                                    le_Participacion.codigo_pso, _
                                    le_Participacion.ciclo_aca, _
                                    le_Participacion.codigo_cco)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function BuscarCruceHorario(ByVal le_Participacion As e_Participacion) As Integer
        Try
            Dim valor As Integer = 0
            Dim ObjCnx1 As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

            valor = ObjCnx1.TraerValor("EVE_BuscaCruceHorario", _
                                        le_Participacion.codigo_cco, _
                                        String.Empty, _
                                        le_Participacion.codigo_pso, _
                                        "01/01/1901", _
                                        le_Participacion.codigo_aev)

            Return valor
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_EnvioSMS
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function EnviarSMSEventoAlumni(ByVal le_EnvioSMS As e_EnvioSMS) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_EnvioSMSIUD", le_EnvioSMS.operacion, _
                         le_EnvioSMS.cod_user, _
                         le_EnvioSMS.codigo_env, _
                         le_EnvioSMS.tabla_env, _
                         le_EnvioSMS.codigoTabla_env, _
                         le_EnvioSMS.codigo_per, _
                         le_EnvioSMS.tipo_env, _
                         le_EnvioSMS.mensaje_env)

            If dt.Rows.Count > 0 Then
                le_EnvioSMS.codigo_env = dt.Rows(0).Item(0).ToString.Trim

                For Each detalle As e_EnvioSMSDetalle In le_EnvioSMS.detalles
                    With detalle
                        .operacion = "I"
                        .cod_user = le_EnvioSMS.cod_user
                        .codigo_end = "0"
                        .codigo_env = le_EnvioSMS.codigo_env
                    End With

                    'Ejecutar Procedimiento
                    dt = cnx.TraerDataTable("ALUMNI_EnvioSMSDetalleIUD", detalle.operacion, _
                                        detalle.cod_user, _
                                        detalle.codigo_end, _
                                        detalle.codigo_env, _
                                        detalle.codigo_pso, _
                                        detalle.nombre_end, _
                                        detalle.celular_end, _
                                        detalle.idRespuesta_end, _
                                        detalle.respuesta_end, _
                                        detalle.fechaRespuesta_end, _
                                        detalle.estado_end)
                Next

                dt = cnx.TraerDataTable("ALUMNI_EnviarSMS", le_EnvioSMS.codigo_env, _
                                        le_EnvioSMS.cod_user)
            Else
                Throw New Exception("Error al realizar el envío.")
            End If

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
End Class

Public Class d_ActividadProgramacion
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarActividadProgramacion(ByVal le_ActividadProgramacion As e_ActividadProgramacion) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_ActividadProgramacionListar", le_ActividadProgramacion.operacion, _
                                    le_ActividadProgramacion.codigo_apr, _
                                    le_ActividadProgramacion.codigo_aev, _
                                    le_ActividadProgramacion.fecha)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarActividadProgramacion(ByVal le_ActividadProgramacion As e_ActividadProgramacion) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_ActividadProgramacionIUD", le_ActividadProgramacion.operacion, _
                                    le_ActividadProgramacion.codigo_apr, _
                                    le_ActividadProgramacion.codigo_aev, _
                                    le_ActividadProgramacion.fechahoraini_apr, _
                                    le_ActividadProgramacion.fechahorafin_apr, _
                                    le_ActividadProgramacion.lugar_apr)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetActividadProgramacion(ByVal codigo As Integer) As e_ActividadProgramacion
        Try
            Dim me_ActividadProgramacion As New e_ActividadProgramacion

            If codigo > 0 Then
                With me_ActividadProgramacion
                    .operacion = "GEN"
                    .codigo_apr = codigo
                    .fecha = "01/01/1901"
                End With

                dt = ListarActividadProgramacion(me_ActividadProgramacion)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de programación no ha sido encontrado.")

                me_ActividadProgramacion = New e_ActividadProgramacion

                With me_ActividadProgramacion
                    .codigo_apr = dt.Rows(0).Item("codigo_apr")
                    .codigo_aev = dt.Rows(0).Item("codigo_aev")
                    .fechahoraini_apr = dt.Rows(0).Item("fechahoraini_apr")
                    .fechahorafin_apr = dt.Rows(0).Item("fechahorafin_apr")
                    .lugar_apr = dt.Rows(0).Item("lugar_apr")
                End With
            Else
                With me_ActividadProgramacion
                    .codigo_apr = 0
                    .codigo_aev = 0
                    .fechahoraini_apr = "01/01/1901"
                    .fechahorafin_apr = "01/01/1901"
                End With
            End If

            Return me_ActividadProgramacion
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_CicloAcademico
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ObtenerCicloAcademicoActual() As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("EVE_CicloAcademicoActual")

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ObtenerCicloAcademico() As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            dt = cnx.TraerDataTable("ConsultarCicloAcademico_SUNE", "EGR", "2")
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarCicloAcademico(ByVal le_CicloAcademico As e_CicloAcademico) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ACAD_CicloAcademicoListar", le_CicloAcademico.operacion, _
                                    le_CicloAcademico.codigo_cac, _
                                    le_CicloAcademico.fvigente_cac, _
                                    le_CicloAcademico.tipocac)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    '20200225-ENevado =============================================================================
    ''' <summary>
    ''' Función para obtener un ciclo academico o una lista segun el tipo de operación
    ''' </summary>
    ''' <param name="obj">Objeto de Clase Ciclo Academico</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function fc_ListarCicloAcademico(ByVal obj As e_CicloAcademico) As System.Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            With obj
                dt = cnx.TraerDataTable("ACAD_CicloAcademico_listar", .tipooperacion, .codigo_cac, .tipocac)
            End With
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    '20200226-ENevado =============================================================================
    ''' <summary>
    ''' Función para registar un ciclo académico
    ''' </summary>
    ''' <param name="obj">Objeto de Clase Ciclo Academico</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function fc_RegistrarCicloAcademico(ByVal obj As e_CicloAcademico) As System.Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            With obj
                dt = cnx.TraerDataTable("ACAD_CicloAcademico_insertar", .descripcion_cac, .tipocac, .fechaIni_Cro, .fechaFin_Cro, .vigente_cac, .notaMinima_cac, .moraDiaria_cac, .vigenciaaux_cac, .fechaIniAdm_cro, .fechafinAdm_cro, .admision_cac, .admisionaux_cac)
            End With
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function




End Class

Public Class d_Empresa
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarEmpresa(ByVal le_Empresa As e_Empresa) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_EmpresaListar", _
                                    le_Empresa.operacion, _
                                    le_Empresa.codigo_emp, _
                                    le_Empresa.ruc_emp, _
                                    le_Empresa.razonSocial_emp, _
                                    le_Empresa.nombreComercial_emp, _
                                    le_Empresa.codigoEstado_cat)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarEmpresa(ByVal le_Empresa As e_Empresa) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_EmpresaIUD", le_Empresa.operacion, _
                                        le_Empresa.cod_user, _
                                        le_Empresa.codigo_emp, _
                                        le_Empresa.idPro, _
                                        le_Empresa.ruc_emp, _
                                        le_Empresa.razonSocial_emp, _
                                        le_Empresa.nombreComercial_emp, _
                                        le_Empresa.abreviatura_emp, _
                                        le_Empresa.password_emp, _
                                        le_Empresa.codigoTipo_cat, _
                                        le_Empresa.codigo_sec, _
                                        le_Empresa.codigo_dep, _
                                        le_Empresa.codigo_pro, _
                                        le_Empresa.codigo_dis, _
                                        le_Empresa.direccion_emp, _
                                        le_Empresa.logo_emp, _
                                        le_Empresa.correo_emp, _
                                        le_Empresa.direccionWeb_emp, _
                                        le_Empresa.prefijoTel_emp, _
                                        le_Empresa.telefono_emp, _
                                        le_Empresa.celular_emp, _
                                        le_Empresa.codigoEstado_cat, _
                                        le_Empresa.externo_emp, _
                                        le_Empresa.codigoExterno_emp, _
                                        le_Empresa.accesoCampus_emp, _
                                        le_Empresa.actualizoInformacion_emp, _
                                        le_Empresa.id_archivos_compartidos)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetEmpresa(ByVal codigo As Integer) As e_Empresa
        Try
            Dim me_Empresa As New e_Empresa

            If codigo > 0 Then
                me_Empresa.operacion = "GEN"
                me_Empresa.codigo_emp = codigo

                dt = ListarEmpresa(me_Empresa)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de empresa no ha sido encontrado.")

                me_Empresa = New e_Empresa
                With me_Empresa
                    .codigo_emp = dt.Rows(0).Item("codigo_emp")
                    .idPro = dt.Rows(0).Item("idPro")
                    .ruc_emp = dt.Rows(0).Item("ruc_emp")
                    .razonSocial_emp = dt.Rows(0).Item("razonSocial_emp")
                    .nombreComercial_emp = dt.Rows(0).Item("nombreComercial_emp")
                    .abreviatura_emp = dt.Rows(0).Item("abreviatura_emp")
                    .password_emp = dt.Rows(0).Item("password_emp")
                    .codigoTipo_cat = dt.Rows(0).Item("codigoTipo_cat")
                    .codigo_sec = dt.Rows(0).Item("codigo_sec")
                    .codigo_dep = dt.Rows(0).Item("codigo_dep")
                    .codigo_pro = dt.Rows(0).Item("codigo_pro")
                    .codigo_dis = dt.Rows(0).Item("codigo_dis")
                    .direccion_emp = dt.Rows(0).Item("direccion_emp")
                    .logo_emp = dt.Rows(0).Item("logo_emp")
                    .correo_emp = dt.Rows(0).Item("correo_emp")
                    .direccionWeb_emp = dt.Rows(0).Item("direccionWeb_emp")
                    .prefijoTel_emp = dt.Rows(0).Item("prefijoTel_emp")
                    .telefono_emp = dt.Rows(0).Item("telefono_emp")
                    .celular_emp = dt.Rows(0).Item("celular_emp")
                    .codigoEstado_cat = dt.Rows(0).Item("codigoEstado_cat")
                    .externo_emp = dt.Rows(0).Item("externo_emp")
                    .codigoExterno_emp = dt.Rows(0).Item("codigoExterno_emp")
                    .accesoCampus_emp = dt.Rows(0).Item("accesoCampus_emp")
                    .id_archivos_compartidos = dt.Rows(0).Item("IdArchivosCompartidos")
                End With
            Else
                With me_Empresa
                    .codigo_emp = 0
                    .idPro = 0
                    .codigoTipo_cat = 0
                    .codigo_sec = 0
                    .codigo_dep = 0
                    .codigo_pro = 0
                    .codigo_dis = 0
                    .codigoEstado_cat = 0
                    .externo_emp = "N"
                    .codigoExterno_emp = 0
                    .accesoCampus_emp = "N"
                    .actualizoInformacion_emp = "S"
                    .id_archivos_compartidos = 0
                    .cod_user = 0
                End With
            End If

            Return me_Empresa
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_Alumno
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarAlumReqEgr(ByVal le_Alumno As e_Alumno) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_ListarAlumnosEgresReq", le_Alumno.codigo_cac)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarAlumnoReq(ByVal le_Alumno As e_Alumno) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_ListarAlumnoReq", le_Alumno.codigo_alu)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarReqEgreDePlanCurByCodAlum(ByVal le_Alumno As e_Alumno) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_ListarReqEgreDePlanCurByCodAlum", le_Alumno.codigo_alu)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarAlumnoReqEgreByPEstudioCprofe(ByVal le_Alumno As e_Alumno) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            'Ejecutar Procedimiento
            'dt = cnx.TraerDataTable("ALUMNI_ListarAlumnosEgresReqByPlanCarrera", le_Alumno.codigo_pes, le_Alumno.tempcodigo_cpf)
            dt = cnx.TraerDataTable("ACAD_ConsultarAlumnoEgresado", le_Alumno.tempcodigo_cpf, le_Alumno.nombres, le_Alumno.codigo_pes, le_Alumno.codigo_pcur)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function FinalizarPlanEstudio(ByVal le_alumno As e_Alumno) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento

            dt = cnx.TraerDataTable("FinalizarAlumnoPlanEstudio", le_alumno.codigo_alu, le_alumno.codigo_per)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function FinalizarPlanEstudioAlumni(ByVal le_alumno As e_Alumno) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento

            dt = cnx.TraerDataTable("ALUMNI_InsertaEgresadov2", le_alumno.codigo_alu, le_alumno.codigo_per, "Campus de Personal.", "72")

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarAlumno(ByVal le_Alumno As e_Alumno) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ACAD_AlumnoListar", _
                                    le_Alumno.operacion, _
                                    le_Alumno.codigo_alu, _
                                    le_Alumno.codigo_cac, _
                                    le_Alumno.cicloIng_alu, _
                                    le_Alumno.nivel_ega, _
                                    le_Alumno.codigo_Fac, _
                                    le_Alumno.tempcodigo_cpf, _
                                    le_Alumno.alumno, _
                                    le_Alumno.tiene_diploma)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarHistorialAcademico(ByVal le_Alumno As e_Alumno) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ConsultarNotas", le_Alumno.operacion, _
                                    le_Alumno.codigo_alu, _
                                    "", "")

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ConsultarAlumnoMatricula(ByVal le_Alumno As e_Alumno) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("EVE_ConsultarAlumnoParaMatricula", le_Alumno.codigoUniver_alu, _
                                    "-1", "0")

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_RequisitoEgreso
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable
    Public Function ListarRequisitosByAlumno(ByVal le_Alumno As e_Alumno) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_ListarRequisitosByAlumno", le_Alumno.codigo_alu, le_Alumno.accion)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function ListarRequisitos(ByVal le_RequisitoEgreso As e_RequisitoEgreso) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("[ALUMNI_RequisitoEgresoListar]", "RE", le_RequisitoEgreso.codigo_req, -1, -1)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function UpdateRequisitoEgreso(ByVal le_RequisitoEgreso As e_RequisitoEgreso) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_UpdateRequisitoEgreso", le_RequisitoEgreso.codigo_req, le_RequisitoEgreso.codigo_cat, le_RequisitoEgreso.indica_pe)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
End Class

Public Class d_AlumnoRequisito
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable
    Public Function InsertarAlumnoRequisito(ByVal le_AlumnoRequisito As e_AlumnoRequisito) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento

            dt = cnx.TraerDataTable("ALUMNI_InsertUpdateAlumnoRequisito", le_AlumnoRequisito.codigo_alu, _
                                    le_AlumnoRequisito.codigo_are, _
                                    le_AlumnoRequisito.codigo_req, _
                                    le_AlumnoRequisito.codigo_pcur, _
                                    le_AlumnoRequisito.codigo_tip, _
                                    le_AlumnoRequisito.observacion, _
                                    le_AlumnoRequisito.usuario_reg, _
                                    le_AlumnoRequisito.fecha_reg, _
                                    le_AlumnoRequisito.usuario_mod, _
                                    le_AlumnoRequisito.fecha_mod, _
                                    le_AlumnoRequisito.estado)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function ListarAlumnoRequisito(ByVal le_AlumnoRequisito As e_AlumnoRequisito) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_ListarAlumnoRequisito", le_AlumnoRequisito.codigo_are)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
End Class

Public Class d_Personal
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ObtenerCelular(ByVal le_Personal As e_Personal) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_ListarPersonalCelular", _
                                    le_Personal.codigo_per)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ObtenerFirmaAlumni(ByVal le_Personal As e_Personal) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_FIRMA", _
                                    le_Personal.codigo_per)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarPersonal(ByVal le_Personal As e_Personal) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("CONF_PersonalListar", le_Personal.operacion, _
                                    le_Personal.codigo_per, _
                                    le_Personal.nombres)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function CorreoPersonal(ByVal le_Personal As e_Personal) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PER_CorreoPersonal", le_Personal.codigo_per)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_Departamento
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarDepartamento(ByVal le_Departamento As e_Departamento) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_DepartamentoListar", _
                                    le_Departamento.operacion, _
                                    le_Departamento.codigo_dep, _
                                    le_Departamento.nombre_dep, _
                                    le_Departamento.codigo_pai)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function BuscaDepartamento() As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_BuscaDepartamento", 0)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetDepartamento(ByVal codigo As Integer) As e_Departamento
        Try
            Dim me_Departamento As New e_Departamento

            If codigo > 0 Then
                me_Departamento.operacion = "GEN"
                me_Departamento.codigo_dep = codigo

                dt = ListarDepartamento(me_Departamento)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de departamento no ha sido encontrado.")

                me_Departamento = New e_Departamento

                With me_Departamento
                    .codigo_dep = dt.Rows(0).Item("codigo_Dep")
                    .nombre_dep = dt.Rows(0).Item("nombre_Dep")
                    .codigo_pai = dt.Rows(0).Item("codigo_Pai")
                    .ubigeo_dep = dt.Rows(0).Item("ubigeo_Dep")
                End With
            Else
                With me_Departamento
                    .codigo_dep = 0
                    .codigo_pai = 0
                End With
            End If

            Return me_Departamento
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_Provincia
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarProvincia(ByVal le_Provincia As e_Provincia) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_ProvinciaListar", _
                                    le_Provincia.operacion, _
                                    le_Provincia.codigo_pro, _
                                    le_Provincia.nombre_pro, _
                                    le_Provincia.codigo_dep)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetProvincia(ByVal codigo As Integer) As e_Provincia
        Try
            Dim me_Provincia As New e_Provincia

            If codigo > 0 Then
                me_Provincia.operacion = "GEN"
                me_Provincia.codigo_pro = codigo

                dt = ListarProvincia(me_Provincia)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de provincia no ha sido encontrado.")

                me_Provincia = New e_Provincia

                With me_Provincia
                    .codigo_pro = dt.Rows(0).Item("codigo_Pro")
                    .nombre_pro = dt.Rows(0).Item("nombre_Pro")
                    .codigo_dep = dt.Rows(0).Item("codigo_Dep")
                    .ubigeo_pro = dt.Rows(0).Item("ubigeo_Pro")
                End With
            Else
                With me_Provincia
                    .codigo_pro = 0
                    .codigo_dep = 0
                End With
            End If

            Return me_Provincia
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_Distrito
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarDistrito(ByVal le_Distrito As e_Distrito) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_DistritoListar", _
                                    le_Distrito.operacion, _
                                    le_Distrito.codigo_dis, _
                                    le_Distrito.nombre_dis, _
                                    le_Distrito.codigo_pro)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetDistrito(ByVal codigo As Integer) As e_Distrito
        Try
            Dim me_Distrito As New e_Distrito

            If codigo > 0 Then
                me_Distrito.operacion = "GEN"
                me_Distrito.codigo_dis = codigo

                dt = ListarDistrito(me_Distrito)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de distrito no ha sido encontrado.")

                me_Distrito = New e_Distrito

                With me_Distrito
                    .codigo_dis = dt.Rows(0).Item("codigo_Dis")
                    .nombre_dis = dt.Rows(0).Item("nombre_Dis")
                    .codigo_pro = dt.Rows(0).Item("codigo_Pro")
                    .ubigeo_dis = dt.Rows(0).Item("ubigeo_Dis")
                    .ubigeoReniec = dt.Rows(0).Item("ubigeoReniec")
                End With
            Else
                With me_Distrito
                    .codigo_dis = 0
                    .codigo_pro = 0
                End With
            End If

            Return me_Distrito
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_PlanEstudio
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarPlanEstudioByCarrera(ByVal le_PlanEstudio As e_PlanEstudio) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("Alumni_ListarPlanEstudiosxCarrera", _
                                    le_PlanEstudio.codigo_cpf)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarPlanEstudio(ByVal le_PlanEstudio As e_PlanEstudio) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ACAD_PlanEstudioListar", _
                                    le_PlanEstudio.operacion, _
                                    le_PlanEstudio.codigo_Pes, _
                                    le_PlanEstudio.codigo_cpf, _
                                    le_PlanEstudio.codigo_ctf, _
                                    le_PlanEstudio.codigo_test, _
                                    le_PlanEstudio.vigencia_pes, _
                                    le_PlanEstudio.operadoraut_acr, _
                                    le_PlanEstudio.ind_ppcodigo_cpf)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_Sector
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function BuscaSector() As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_BuscaSector", _
                                    0)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
End Class

Public Class d_InformacionContacto
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarInformacionContacto(ByVal le_InformacionContacto As e_InformacionContacto) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_InformacionContactoListar", _
                                    le_InformacionContacto.operacion, _
                                    le_InformacionContacto.codigo_inc, _
                                    le_InformacionContacto.codigo_emp)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarInformacionContacto(ByVal le_InformacionContacto As e_InformacionContacto) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_InformacionContactoIUD", _
                                    le_InformacionContacto.operacion, _
                                    le_InformacionContacto.cod_user, _
                                    le_InformacionContacto.codigo_inc, _
                                    le_InformacionContacto.codigo_emp, _
                                    le_InformacionContacto.idPro, _
                                    le_InformacionContacto.denominacion_inc, _
                                    le_InformacionContacto.apellidos_inc, _
                                    le_InformacionContacto.nombres_inc, _
                                    le_InformacionContacto.cargo_inc, _
                                    le_InformacionContacto.prefijoTel_inc, _
                                    le_InformacionContacto.telefono_inc, _
                                    le_InformacionContacto.celular_inc, _
                                    le_InformacionContacto.correo01_inc, _
                                    le_InformacionContacto.correo02_inc, _
                                    le_InformacionContacto.externo_inc, _
                                    le_InformacionContacto.codigoExterno_inc, _
                                    le_InformacionContacto.actualizoInformacion_inc)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetInformacionContacto(ByVal codigo As Integer) As e_InformacionContacto
        Try
            Dim me_InformacionContacto As New e_InformacionContacto

            If codigo > 0 Then
                me_InformacionContacto.operacion = "GEN"
                me_InformacionContacto.codigo_inc = codigo

                dt = ListarInformacionContacto(me_InformacionContacto)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de información de contacto no ha sido encontrado.")

                me_InformacionContacto = New e_InformacionContacto

                With me_InformacionContacto
                    .codigo_inc = dt.Rows(0).Item("codigo_inc")
                    .codigo_emp = dt.Rows(0).Item("codigo_emp")
                    .idPro = dt.Rows(0).Item("idPro")
                    .denominacion_inc = dt.Rows(0).Item("denominacion_inc")
                    .apellidos_inc = dt.Rows(0).Item("apellidos_inc")
                    .nombres_inc = dt.Rows(0).Item("nombres_inc")
                    .cargo_inc = dt.Rows(0).Item("cargo_inc")
                    .prefijoTel_inc = dt.Rows(0).Item("prefijoTel_inc")
                    .telefono_inc = dt.Rows(0).Item("telefono_inc")
                    .celular_inc = dt.Rows(0).Item("celular_inc")
                    .correo01_inc = dt.Rows(0).Item("correo01_inc")
                    .correo02_inc = dt.Rows(0).Item("correo02_inc")
                    .externo_inc = dt.Rows(0).Item("externo_inc")
                    .codigoExterno_inc = dt.Rows(0).Item("codigoExterno_inc")
                    .actualizoInformacion_inc = "S"
                End With
            Else
                With me_InformacionContacto
                    .codigo_inc = 0
                    .codigo_emp = 0
                    .idPro = 0
                    .externo_inc = "N"
                    .codigoExterno_inc = 0
                    .actualizoInformacion_inc = "S"
                End With
            End If

            Return me_InformacionContacto
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_RevisionEmpresa
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarRevisionEmpresa(ByVal le_RevisionEmpresa As e_RevisionEmpresa) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_RevisionEmpresaListar", _
                                    le_RevisionEmpresa.operacion, _
                                    le_RevisionEmpresa.codigo_rem, _
                                    le_RevisionEmpresa.codigo_emp)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarRevisionEmpresa(ByVal le_RevisionEmpresa As e_RevisionEmpresa) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_RevisionEmpresaIUD", _
                                    le_RevisionEmpresa.operacion, _
                                    le_RevisionEmpresa.cod_user, _
                                    le_RevisionEmpresa.codigo_rem, _
                                    le_RevisionEmpresa.codigo_emp, _
                                    le_RevisionEmpresa.codigoRevisor_pso, _
                                    le_RevisionEmpresa.codigoEstado_cat, _
                                    le_RevisionEmpresa.vigente_rem, _
                                    le_RevisionEmpresa.comentario_rem)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_DetalleOfertaCarrera
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function InsertarDetalleOferta(ByVal le_DetalleOfertaCarrera As e_DetalleOfertaCarrera) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            dt = cnx.TraerDataTable("ALUMNI_RegistraDetalleOferta", le_DetalleOfertaCarrera.codigo_ofe, _
                                    le_DetalleOfertaCarrera.codigo_cpf)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarDetalleOferta(ByVal le_DetalleOfertaCarrera As e_DetalleOfertaCarrera) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_DetalleOferta", le_DetalleOfertaCarrera.codigo_ofe, le_DetalleOfertaCarrera.codigo_cpf, le_DetalleOfertaCarrera.accion)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function EliminarItemDetOferta(ByVal le_DetalleOfertaCarrera As e_DetalleOfertaCarrera) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_EliminarItemDetalleOferta", le_DetalleOfertaCarrera.codigo_ofe, le_DetalleOfertaCarrera.codigo_cpf)
            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function


End Class

Public Class d_ArchivoCompartido
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarArchivoCompartido(ByVal le_ArchivoCompartido As e_ArchivoCompartido) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_ArchivoCompartidoListar", _
                                    le_ArchivoCompartido.operacion, _
                                    le_ArchivoCompartido.id_archivos_compartidos)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function CargarArchivoCompartido(ByVal le_ArchivoCompartido As e_ArchivoCompartido, ByVal archivo_subir As HttpPostedFile) As Data.DataTable
        Try
            Dim file As HttpPostedFile = archivo_subir
            Dim Input(file.ContentLength) As Byte
            dt = New DataTable

            Dim b As New BinaryReader(file.InputStream)
            Dim bin_data As Byte() = b.ReadBytes(file.InputStream.Length)

            'Dim ws_cloud As New ClsArchivosCompartidos
            Dim ws_cloud As New ClsArchivosCompartidosV2
            Dim parametros As New Dictionary(Of String, String)

            'Modificar el nombre con caracteres validos
            le_ArchivoCompartido.nombre_archivo = QuitarCaracteresEspeciales(le_ArchivoCompartido.nombre_archivo)

            With le_ArchivoCompartido
                parametros.Add("Fecha", .fecha)
                parametros.Add("Extencion", System.IO.Path.GetExtension(file.FileName))
                parametros.Add("Nombre", .nombre_archivo)
                parametros.Add("TransaccionId", .id_transaccion)
                parametros.Add("TablaId", .id_tabla)
                parametros.Add("NroOperacion", .nro_operacion)
                parametros.Add("Archivo", System.Convert.ToBase64String(bin_data, 0, bin_data.Length))
                parametros.Add("Usuario", .usuario_reg)
                parametros.Add("Equipo", String.Empty)
                parametros.Add("Ip", .ip_reg)
                parametros.Add("param8", .usuario_reg)
            End With

            Dim envelope As String = ws_cloud.SoapEnvelope(parametros)
            Dim result As String = ws_cloud.PeticionRequestSoap(le_ArchivoCompartido.ruta_archivo, envelope, "http://usat.edu.pe/UploadFile", le_ArchivoCompartido.usuario_reg)

            If result.Contains("procesado correctamente") Then
                'Buscar el ultimo registro de archivo compartido
                le_ArchivoCompartido.operacion = "ULT"
                le_ArchivoCompartido.id_archivos_compartidos = 0

                dt = ListarArchivoCompartido(le_ArchivoCompartido)
            End If

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RegistrarArchivoCompartido(ByVal le_ArchivoCompartido As e_ArchivoCompartido) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            dt = cnx.TraerDataTable("ALUMNI_ArchivoCompartidoIUD", _
                    le_ArchivoCompartido.operacion, _
                    le_ArchivoCompartido.id_archivos_compartidos, _
                    le_ArchivoCompartido.id_transaccion)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ObtenerArchivoCompartido(ByVal le_ArchivoCompartido As e_ArchivoCompartido) As Byte()
        Try
            Dim ws_cloud As New ClsArchivosCompartidosV2
            Dim parametros As New Dictionary(Of String, String)

            parametros.Add("IdArchivo", le_ArchivoCompartido.id_encriptado)
            parametros.Add("Usuario", le_ArchivoCompartido.usuario_act)
            parametros.Add("Token", le_ArchivoCompartido.token_tabla)

            Dim envelope As String = ws_cloud.SoapEnvelopeDescarga(parametros)
            Dim result As String = ws_cloud.PeticionRequestSoap(le_ArchivoCompartido.ruta_archivo, envelope, "http://usat.edu.pe/DownloadFile", le_ArchivoCompartido.usuario_act)
            Dim archivo As String = ws_cloud.ResultFile(result)
            Dim bytes As Byte() = Convert.FromBase64String(archivo)

            Return bytes
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetArchivoCompartido(ByVal codigo As Integer) As e_ArchivoCompartido
        Try
            Dim me_ArchivoCompartido As New e_ArchivoCompartido

            If codigo > 0 Then
                me_ArchivoCompartido.operacion = "GEN"
                me_ArchivoCompartido.id_archivos_compartidos = codigo

                dt = ListarArchivoCompartido(me_ArchivoCompartido)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de archivo compartido no ha sido encontrado.")

                me_ArchivoCompartido = New e_ArchivoCompartido

                With me_ArchivoCompartido
                    .id_archivos_compartidos = dt.Rows(0).Item("IdArchivosCompartidos")
                    .nombre_archivo = dt.Rows(0).Item("NombreArchivo")
                    .fecha = dt.Rows(0).Item("Fecha")
                    .extencion = dt.Rows(0).Item("Extencion")
                    .id_tabla = dt.Rows(0).Item("IdTabla")
                    .id_transaccion = dt.Rows(0).Item("IdTransaccion")
                    .nro_operacion = dt.Rows(0).Item("NroOperacion")
                    .descripcion = dt.Rows(0).Item("Descripcion")
                    .ruta_archivo = dt.Rows(0).Item("RutaArchivo")
                    .fecha_reg = dt.Rows(0).Item("FechaReg")
                    .fecha_act = dt.Rows(0).Item("FechaAct")
                    .usuario_reg = dt.Rows(0).Item("UsuarioReg")
                    .usuario_act = dt.Rows(0).Item("UsuarioAct")
                    .ip_reg = dt.Rows(0).Item("IpReg")
                    .ip_act = dt.Rows(0).Item("IpAct")
                    .token_tabla = dt.Rows(0).Item("tokenTabla")
                    .id_encriptado = dt.Rows(0).Item("IdEncriptado")
                End With
            Else
                With me_ArchivoCompartido
                    .id_archivos_compartidos = 0
                    .fecha = "01/01/1901"
                    .id_tabla = 0
                    .id_transaccion = 0
                    .fecha_reg = "01/01/1901"
                    .fecha_act = "01/01/1901"
                    .cod_user = 0
                End With
            End If

            Return me_ArchivoCompartido
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerIdTabla(ByVal tokenTabla As String) As Integer
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            Dim IdTabla As Integer = 0

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ObtenerTablaArchivoByToken", tokenTabla)

            If dt.Rows.Count > 0 Then
                IdTabla = CInt(dt.Rows(0).Item("IdTabla").ToString)
            End If

            cnx.TerminarTransaccion()

            Return IdTabla
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function QuitarCaracteresEspeciales(ByVal cadena As String, Optional ByVal chars As String = ":<>{}[]^+,;_-/*?¿!$%&/¨()='¡|@Ã› " + Chr(34)) As String
        cadena = Replace(cadena, "á", "a")
        cadena = Replace(cadena, "é", "e")
        cadena = Replace(cadena, "í", "i")
        cadena = Replace(cadena, "ó", "o")
        cadena = Replace(cadena, "ú", "u")
        cadena = Replace(cadena, "ñ", "n")
        cadena = Replace(cadena, "Á", "A")
        cadena = Replace(cadena, "É", "E")
        cadena = Replace(cadena, "Í", "I")
        cadena = Replace(cadena, "Ó", "O")
        cadena = Replace(cadena, "Ú", "U")
        cadena = Replace(cadena, "Ñ", "N")

        Dim i As Integer
        Dim nCadena As String
        On Error Resume Next
        'Asignamos valor a la cadena de trabajo para
        'no modificar la que envía el cliente.
        nCadena = cadena
        For i = 1 To Len(chars)
            nCadena = Replace(nCadena, Mid(chars, i, 1), "")
        Next i
        'Devolvemos la cadena tratada
        QuitarCaracteresEspeciales = nCadena
    End Function

End Class

Public Class d_ArchivoCompartidoDetalle
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function RegistrarArchivoCompartidoDetalle(ByVal le_ArchivoCompartido As e_ArchivoCompartido) As Boolean
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Si se trata de un solo registro de detalle actualizamos el id_transaccion
            If le_ArchivoCompartido.detalles.Count = 1 AndAlso _
               Not String.IsNullOrEmpty(le_ArchivoCompartido.id_archivos_compartidos) AndAlso _
               le_ArchivoCompartido.id_archivos_compartidos > 0 Then

                Dim md_ArchivoCompartido As New d_ArchivoCompartido
                le_ArchivoCompartido.operacion = "U"
                le_ArchivoCompartido.id_transaccion = le_ArchivoCompartido.detalles(0).codigoTabla_acd
                md_ArchivoCompartido.RegistrarArchivoCompartido(le_ArchivoCompartido)

            End If

            'Ejecutar Procedimiento
            For Each le_Detalle As e_ArchivoCompartidoDetalle In le_ArchivoCompartido.detalles
                le_Detalle.operacion = "I"
                le_Detalle.cod_user = le_ArchivoCompartido.cod_user
                le_Detalle.id_archivos_compartidos = le_ArchivoCompartido.id_archivos_compartidos

                cnx.TraerDataTable("ALUMNI_ArchivoCompartidoDetalleIUD", _
                    le_Detalle.operacion, _
                    le_Detalle.cod_user, _
                    le_Detalle.codigo_acd, _
                    le_Detalle.id_archivos_compartidos, _
                    le_Detalle.tabla_acd, _
                    le_Detalle.codigoTabla_acd)
            Next

            cnx.TerminarTransaccion()
            Return True
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetArchivoCompartidoDetalle(ByVal codigo As Integer) As e_ArchivoCompartidoDetalle
        Try
            Dim me_ArchivoCompartidoDetalle As New e_ArchivoCompartidoDetalle

            If codigo > 0 Then
            Else
                With me_ArchivoCompartidoDetalle
                    .codigo_acd = 0
                    .id_archivos_compartidos = 0
                    .codigoTabla_acd = 0
                    .cod_user = 0
                End With
            End If

            Return me_ArchivoCompartidoDetalle
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_DiagramaER
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarServidorVinculado() As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("sp_linkedservers")

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarDiagramaER(ByVal le_DiagramaER As e_DiagramaER) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_DiagramaERListar", _
                                    le_DiagramaER.operacion, _
                                    le_DiagramaER.diagram_name, _
                                    le_DiagramaER.server_name, _
                                    le_DiagramaER.database_name, _
                                    le_DiagramaER.server_name_origen, _
                                    le_DiagramaER.server_name_destino, _
                                    le_DiagramaER.database_name_origen, _
                                    le_DiagramaER.database_name_destino)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function SincronizarDiagramaER(ByVal le_DiagramaER As e_DiagramaER) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            'cnx.IniciarTransaccion()
            cnx.AbrirConexion()

            'Ejecutar Procedimiento
            cnx.Ejecutar("ALUMNI_DiagramaERSincronizar", _
                                    le_DiagramaER.operacion, _
                                    le_DiagramaER.server_name_origen, _
                                    le_DiagramaER.server_name_destino, _
                                    le_DiagramaER.database_name_origen, _
                                    le_DiagramaER.database_name_destino, _
                                    le_DiagramaER.diagram_name)

            'cnx.TerminarTransaccion()
            cnx.CerrarConexion()
            Return dt
        Catch ex As Exception
            'cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_TipoParticipante
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarTipoParticipante(ByVal le_TipoParticipante As e_TipoParticipante) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_TipoParticipanteListar", _
                                    le_TipoParticipante.operacion, _
                                    le_TipoParticipante.codigo_tpar, _
                                    le_TipoParticipante.descripcion_tpar)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarInscritosEvento(ByVal codigo_cco As Integer) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_SRInscritos", _
                                    codigo_cco)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_PlantillaOnomastico
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarPlantillaOnomastico(ByVal le_PlantillaOnomastico As e_PlantillaOnomastico) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PER_PlantillaOnomasticoListar", _
                                    le_PlantillaOnomastico.operacion, _
                                    le_PlantillaOnomastico.codigo_plo, _
                                    le_PlantillaOnomastico.vigente_plo)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarPlantillaOnomastico(ByVal le_PlantillaOnomastico As e_PlantillaOnomastico) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PER_PlantillaOnomasticoIUD", _
                                    le_PlantillaOnomastico.operacion, _
                                    le_PlantillaOnomastico.cod_user, _
                                    le_PlantillaOnomastico.codigo_plo, _
                                    le_PlantillaOnomastico.imgHeader_plo, _
                                    le_PlantillaOnomastico.imgFooter_plo, _
                                    le_PlantillaOnomastico.vigente_plo)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetPlantillaOnomastico(ByVal codigo As Integer) As e_PlantillaOnomastico
        Try
            Dim me_PlantillaOnomastico As New e_PlantillaOnomastico

            If codigo > 0 Then
            Else
                With me_PlantillaOnomastico
                    .codigo_plo = 0
                    .vigente_plo = "S"
                    .cod_user = 0
                End With
            End If

            Return me_PlantillaOnomastico
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_EventoVirtual
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarEventoVirtual(ByVal le_EventoVirtual As e_EventoVirtual) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_EventoVirtual_Listar", _
                                    le_EventoVirtual.operacion, _
                                    le_EventoVirtual.codigo_evi, _
                                    le_EventoVirtual.nombre_evi, _
                                    le_EventoVirtual.nombrePonente_evi)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function IUDEventoVirtual(ByVal le_EventoVirtual As e_EventoVirtual) As Dictionary(Of String, String)
        Try
            Dim lo_Resultado As New Dictionary(Of String, String)
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            Dim lo_Salida As Object() = cnx.Ejecutar("ALUMNI_EventoVirtual_IUD", _
                                    le_EventoVirtual.operacion, _
                                    le_EventoVirtual.codigo_evi, _
                                    le_EventoVirtual.nombre_evi, _
                                    le_EventoVirtual.nombrePonente_evi, _
                                    le_EventoVirtual.fechaHoraInicio_evi, _
                                    le_EventoVirtual.fechaHoraFin_evi, _
                                    le_EventoVirtual.url_evi, _
                                    le_EventoVirtual.tipo_evi, _
                                    le_EventoVirtual.estado_evi, _
                                    le_EventoVirtual.cod_usuario, _
                                    "0", "", "0")
            cnx.TerminarTransaccion()

            lo_Resultado.Item("rpta") = lo_Salida(0)
            lo_Resultado.Item("msg") = lo_Salida(1)
            lo_Resultado.Item("cod") = lo_Salida(2)

            Return lo_Resultado
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetEventoVirtual(ByVal codigo As Integer) As e_EventoVirtual
        Try
            Dim me_EventoVirtual As New e_EventoVirtual

            If codigo > 0 Then
            Else
                With me_EventoVirtual
                    .codigo_evi = 0
                End With
            End If

            Return me_EventoVirtual
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_InscripcionEventoVirtual
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function ListarInscripcionEventoVirtual(ByVal le_InscripcionEventoVirtual As e_InscripcionEventoVirtual) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ALUMNI_InscripcionEventoVirtual_Listar", _
                                    le_InscripcionEventoVirtual.operacion, _
                                    le_InscripcionEventoVirtual.codigo_iev, _
                                    le_InscripcionEventoVirtual.codigo_alu, _
                                    le_InscripcionEventoVirtual.codigo_evi, _
                                    le_InscripcionEventoVirtual.nombreCompleto_iev, _
                                    le_InscripcionEventoVirtual.numDocIdentidad_iev, _
                                    le_InscripcionEventoVirtual.email_iev, _
                                    le_InscripcionEventoVirtual.celular_iev, _
                                    le_InscripcionEventoVirtual.estaTrabajando_iev, _
                                    le_InscripcionEventoVirtual.empresa_iev, _
                                    le_InscripcionEventoVirtual.cargo_iev, _
                                    le_InscripcionEventoVirtual.medioIngresoLaboral_iev, _
                                    le_InscripcionEventoVirtual.codigo_tpar, _
                                    le_InscripcionEventoVirtual.obtenerConstancia_iev, _
                                    le_InscripcionEventoVirtual.medioInscripcion_iev, _
                                    le_InscripcionEventoVirtual.estado_iev, _
                                    le_InscripcionEventoVirtual.usuarioReg_iev, _
                                    le_InscripcionEventoVirtual.fechaHoraReg_iev, _
                                    le_InscripcionEventoVirtual.usuarioMod_iev, _
                                    le_InscripcionEventoVirtual.fechaHoraMod_iev, _
                                    le_InscripcionEventoVirtual.fechaDesde, _
                                    le_InscripcionEventoVirtual.fechaHasta)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_Cronograma
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarCronograma(ByVal le_Cronograma As e_Cronograma) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ACAD_CronogramaListar", le_Cronograma.operacion, _
                                    le_Cronograma.codigo_cro, _
                                    le_Cronograma.codigo_act, _
                                    le_Cronograma.codigo_cac, _
                                    le_Cronograma.codigo_test)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_ComunicadoPersonal
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarComunicadoPersonal(ByVal le_ComunicadoPersonal As e_ComunicadoPersonal) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PER_ComunicadoPersonalListar", le_ComunicadoPersonal.operacion, _
                                    le_ComunicadoPersonal.codigo_cpe, _
                                    le_ComunicadoPersonal.codigo_per, _
                                    le_ComunicadoPersonal.numeroComunicado_cpe, _
                                    le_ComunicadoPersonal.verificaVigencia)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarComunicadoPersonal(ByVal le_ComunicadoPersonal As e_ComunicadoPersonal) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PER_ComunicadoPersonalIUD", le_ComunicadoPersonal.operacion, _
                                    le_ComunicadoPersonal.cod_user, _
                                    le_ComunicadoPersonal.codigo_cpe, _
                                    le_ComunicadoPersonal.numeroComunicado_cpe, _
                                    le_ComunicadoPersonal.nombreComunicado_cpe, _
                                    le_ComunicadoPersonal.codigo_per, _
                                    le_ComunicadoPersonal.tipo_cpe, _
                                    le_ComunicadoPersonal.fechaVigenciaIni_cpe, _
                                    le_ComunicadoPersonal.fechaVigenciaFin_cpe, _
                                    le_ComunicadoPersonal.indDescarga_cpe, _
                                    le_ComunicadoPersonal.fechaDescarga_cpe, _
                                    le_ComunicadoPersonal.descripcionVar1_cpe, _
                                    le_ComunicadoPersonal.var1_cpe, _
                                    le_ComunicadoPersonal.descripcionVar2_cpe, _
                                    le_ComunicadoPersonal.var2_cpe)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetComunicadoPersonal(ByVal codigo As Integer) As e_ComunicadoPersonal
        Try
            Dim me_ComunicadoPersonal As New e_ComunicadoPersonal

            If codigo > 0 Then

            Else
                With me_ComunicadoPersonal
                    .cod_user = 0
                    .codigo_cpe = 0
                    .codigo_per = 0
                    .numeroComunicado_cpe = 0
                    .fechaVigenciaIni_cpe = #1/1/1901#
                    .fechaVigenciaFin_cpe = #1/1/1901#
                    .fechaDescarga_cpe = #1/1/1901#
                End With
            End If

            Return me_ComunicadoPersonal
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_TipoFuncion
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarTipoFuncion(ByVal le_TipoFuncion As e_TipoFuncion) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("CONF_TipoFuncionListar", le_TipoFuncion.operacion, _
                                    le_TipoFuncion.codigo_tfu, _
                                    le_TipoFuncion.descripcion_tfu, _
                                    le_TipoFuncion.abreviatura_tfu)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_Aplicacion
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarAplicacion(ByVal le_Aplicacion As e_Aplicacion) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("CONF_AplicacionListar", le_Aplicacion.operacion, _
                                    le_Aplicacion.codigo_apl, _
                                    le_Aplicacion.descripcion_apl, _
                                    le_Aplicacion.codigo_tfu, _
                                    le_Aplicacion.codigo_per)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_DatosPersonal
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarDatosPersonal(ByVal le_DatosPersonal As e_DatosPersonal) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PER_DatosPersonalListar", le_DatosPersonal.operacion, _
                                    le_DatosPersonal.codigo_per, _
                                    le_DatosPersonal.actualizoDatos_per)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarDatosPersonal(ByVal le_DatosPersonal As e_DatosPersonal) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PER_DatosPersonalIUD", le_DatosPersonal.operacion, _
                                    le_DatosPersonal.codigo_per, _
                                    le_DatosPersonal.direccion_per, _
                                    le_DatosPersonal.email_per, _
                                    le_DatosPersonal.email_alternativo_per, _
                                    le_DatosPersonal.celular_per, _
                                    le_DatosPersonal.telefono_per, _
                                    le_DatosPersonal.operadorCelular_per, _
                                    le_DatosPersonal.operadorInternet_per, _
                                    le_DatosPersonal.actualizoDatos_per, _
                                    le_DatosPersonal.codigo_pro, _
                                    le_DatosPersonal.distrito)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetDatosPersonal(ByVal codigo As Integer) As e_DatosPersonal
        Try
            Dim me_DatosPersonal As New e_DatosPersonal

            If codigo > 0 Then
                me_DatosPersonal.operacion = "GEN"
                me_DatosPersonal.codigo_per = codigo

                dt = ListarDatosPersonal(me_DatosPersonal)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de personal no ha sido encontrado.")

                me_DatosPersonal = New e_DatosPersonal

                With me_DatosPersonal
                    .codigo_per = dt.Rows(0).Item("codigo_Per")
                    .direccion_per = dt.Rows(0).Item("direccion_Per")
                    .email_per = dt.Rows(0).Item("email_Per")
                    .email_alternativo_per = dt.Rows(0).Item("email_alternativo_Per")
                    .celular_per = dt.Rows(0).Item("celular_Per")
                    .telefono_per = dt.Rows(0).Item("telefono_Per")
                    .operadorCelular_per = dt.Rows(0).Item("operadorCelular_Per")
                    .operadorInternet_per = dt.Rows(0).Item("operadorInternet_Per")
                    .actualizoDatos_per = dt.Rows(0).Item("actualizoDatos_Per")
                    .codigo_pro = dt.Rows(0).Item("codigo_pro")
                    .distrito = dt.Rows(0).Item("distrito")
                End With
            Else
                With me_DatosPersonal
                    .codigo_per = 0
                    .codigo_pro = 0
                    .distrito = 0
                End With
            End If

            Return me_DatosPersonal
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_MotivoNotaAbono
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarMotivoNotaAbono(ByVal le_MotivoNotaAbono As e_MotivoNotaAbono) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PEN_MotivoNotaAbonoListar", le_MotivoNotaAbono.operacion, _
                                    le_MotivoNotaAbono.codigo_mno, _
                                    le_MotivoNotaAbono.descripcion_mno, _
                                    le_MotivoNotaAbono.codigo_gmn)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarMotivoNotaAbono(ByVal le_MotivoNotaAbono As e_MotivoNotaAbono) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PEN_MotivoNotaAbonoIUD", le_MotivoNotaAbono.operacion, _
                                    le_MotivoNotaAbono.codigo_mno, _
                                    le_MotivoNotaAbono.descripcion_mno, _
                                    le_MotivoNotaAbono.codigo_pco, _
                                    le_MotivoNotaAbono.bloqueaAgregadoRetiros, _
                                    le_MotivoNotaAbono.conveniobeca, _
                                    le_MotivoNotaAbono.solicitudAnulacion, _
                                    le_MotivoNotaAbono.codigo_gmn)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetMotivoNotaAbono(ByVal codigo As Integer) As e_MotivoNotaAbono
        Try
            Dim me_MotivoNotaAbono As New e_MotivoNotaAbono

            If codigo > 0 Then
                me_MotivoNotaAbono.operacion = "GEN"
                me_MotivoNotaAbono.codigo_mno = codigo

                dt = ListarMotivoNotaAbono(me_MotivoNotaAbono)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de motivo de nota de abono no ha sido encontrado.")

                me_MotivoNotaAbono = New e_MotivoNotaAbono

                With me_MotivoNotaAbono
                    .codigo_mno = dt.Rows(0).Item("codigo_mno")
                    .descripcion_mno = dt.Rows(0).Item("descripcion_mno")
                    .codigo_pco = dt.Rows(0).Item("codigo_pco")
                    .bloqueaAgregadoRetiros = CBool(dt.Rows(0).Item("bloqueaAgregadoRetiros"))
                    .conveniobeca = CBool(dt.Rows(0).Item("conveniobeca"))
                    .solicitudAnulacion = CBool(dt.Rows(0).Item("solicitudAnulacion"))
                    .codigo_gmn = dt.Rows(0).Item("codigo_gmn")
                End With
            Else
                With me_MotivoNotaAbono
                    .codigo_mno = 0
                    .codigo_pco = 0
                    .codigo_gmn = 0
                End With
            End If

            Return me_MotivoNotaAbono
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_GrupoMotivoAbono
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarGrupoMotivoAbono(ByVal le_GrupoMotivoAbono As e_GrupoMotivoAbono) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PEN_GrupoMotivoAbonoListar", le_GrupoMotivoAbono.operacion, _
                                    le_GrupoMotivoAbono.codigo_gmn, _
                                    le_GrupoMotivoAbono.nombre_gmn)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_Adeudos
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarAdeudos(ByVal le_Adeudos As e_Adeudos) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GYT_AdeudosListar", le_Adeudos.operacion, _
                                    le_Adeudos.codigo_ade, _
                                    le_Adeudos.estado_ade)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarAdeudos(ByVal le_Adeudos As e_Adeudos) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GYT_AdeudosIUD", le_Adeudos.operacion, _
                                    le_Adeudos.cod_user, _
                                    le_Adeudos.codigo_ade, _
                                    le_Adeudos.codigo_alu, _
                                    le_Adeudos.codigoArea_cco, _
                                    le_Adeudos.codigo_tfu, _
                                    le_Adeudos.codigo_sco, _
                                    le_Adeudos.codigo_cco, _
                                    le_Adeudos.motivo_ade, _
                                    le_Adeudos.fechaDeuda_ade, _
                                    le_Adeudos.monto_ade, _
                                    le_Adeudos.codigo_deu, _
                                    le_Adeudos.fechaCancelado_ade, _
                                    le_Adeudos.comentario_ade, _
                                    le_Adeudos.estado_ade)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetAdeudos(ByVal codigo As Integer) As e_Adeudos
        Try
            Dim me_Adeudos As New e_Adeudos

            If codigo > 0 Then
                me_Adeudos.operacion = "GEN"
                me_Adeudos.codigo_ade = codigo

                dt = ListarAdeudos(me_Adeudos)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de adeudo no ha sido encontrado.")

                me_Adeudos = New e_Adeudos

                With me_Adeudos
                    .codigo_ade = dt.Rows(0).Item("codigo_ade")
                    .codigo_alu = dt.Rows(0).Item("codigo_alu")
                    .codigoArea_cco = dt.Rows(0).Item("codigoArea_cco")
                    .codigo_tfu = dt.Rows(0).Item("codigo_tfu")
                    .codigo_sco = dt.Rows(0).Item("codigo_sco")
                    .codigo_cco = dt.Rows(0).Item("codigo_cco")
                    .codigoUniver_alu = dt.Rows(0).Item("codigoUniver_alu")
                    .nombre_alu = dt.Rows(0).Item("nombre_alu")
                    .motivo_ade = dt.Rows(0).Item("motivo_ade")
                    .fechaDeuda_ade = dt.Rows(0).Item("fechaDeuda_ade")
                    .monto_ade = dt.Rows(0).Item("monto_ade")
                    .codigo_deu = dt.Rows(0).Item("codigo_deu")
                    .fechaCancelado_ade = dt.Rows(0).Item("fechaCancelado_ade")
                    .comentario_ade = dt.Rows(0).Item("comentario_ade")
                    .estado_ade = dt.Rows(0).Item("estado_ade")
                End With
            Else
                With me_Adeudos
                    .cod_user = 0
                    .codigo_ade = 0
                    .codigo_alu = 0
                    .codigoArea_cco = 0
                    .codigo_tfu = 0
                    .codigo_sco = 0
                    .codigo_cco = 0
                    .monto_ade = 0
                    .codigo_deu = 0
                End With
            End If

            Return me_Adeudos
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_ConfiguracionInstanciasAdeudos
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarConfiguracionInstanciasAdeudos(ByVal le_ConfiguracionInstanciasAdeudos As e_ConfiguracionInstanciasAdeudos) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GYT_ConfiguracionInstanciasAdeudosListar", le_ConfiguracionInstanciasAdeudos.operacion, _
                                    le_ConfiguracionInstanciasAdeudos.codigo_cia, _
                                    le_ConfiguracionInstanciasAdeudos.codigoArea_cco, _
                                    le_ConfiguracionInstanciasAdeudos.codigo_tfu, _
                                    le_ConfiguracionInstanciasAdeudos.estado_cia, _
                                    le_ConfiguracionInstanciasAdeudos.cod_user)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarConfiguracionInstanciasAdeudos(ByVal le_ConfiguracionInstanciasAdeudos As e_ConfiguracionInstanciasAdeudos) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GYT_ConfiguracionInstanciasAdeudosIUD", le_ConfiguracionInstanciasAdeudos.operacion, _
                                    le_ConfiguracionInstanciasAdeudos.cod_user, _
                                    le_ConfiguracionInstanciasAdeudos.codigo_cia, _
                                    le_ConfiguracionInstanciasAdeudos.codigoArea_cco, _
                                    le_ConfiguracionInstanciasAdeudos.codigo_tfu, _
                                    le_ConfiguracionInstanciasAdeudos.codigo_sco, _
                                    le_ConfiguracionInstanciasAdeudos.codigo_cco, _
                                    le_ConfiguracionInstanciasAdeudos.estado_cia)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetConfiguracionInstanciasAdeudos(ByVal codigo As Integer) As e_ConfiguracionInstanciasAdeudos
        Try
            Dim me_ConfiguracionInstanciasAdeudos As New e_ConfiguracionInstanciasAdeudos

            If codigo > 0 Then
                me_ConfiguracionInstanciasAdeudos.operacion = "GEN"
                me_ConfiguracionInstanciasAdeudos.codigo_cia = codigo

                dt = ListarConfiguracionInstanciasAdeudos(me_ConfiguracionInstanciasAdeudos)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de configuración de Instancia de Adeudo no ha sido encontrado.")

                me_ConfiguracionInstanciasAdeudos = New e_ConfiguracionInstanciasAdeudos

                With me_ConfiguracionInstanciasAdeudos
                    .codigo_cia = dt.Rows(0).Item("codigo_cia")
                    .codigoArea_cco = dt.Rows(0).Item("codigoArea_cco")
                    .codigo_tfu = dt.Rows(0).Item("codigo_tfu")
                    .codigo_sco = dt.Rows(0).Item("codigo_sco")
                    .codigo_cco = dt.Rows(0).Item("codigo_cco")
                    .estado_cia = dt.Rows(0).Item("estado_cia")
                End With
            Else
                With me_ConfiguracionInstanciasAdeudos
                    .cod_user = 0
                    .codigo_cia = 0
                    .codigoArea_cco = 0
                    .codigo_tfu = 0
                    .codigo_sco = 0
                    .codigo_cco = 0
                End With
            End If

            Return me_ConfiguracionInstanciasAdeudos
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_AsistenciaDocente
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarAsistenciaDocente(ByVal le_AsistenciaDocente As e_AsistenciaDocente) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PER_AsistenciaDocenteListar", le_AsistenciaDocente.operacion, _
                                    le_AsistenciaDocente.codigo_ado, _
                                    le_AsistenciaDocente.codigo_per, _
                                    le_AsistenciaDocente.codigo_hdo, _
                                    le_AsistenciaDocente.codigo_cup, _
                                    le_AsistenciaDocente.codigo_lho, _
                                    le_AsistenciaDocente.codigo_hop, _
                                    le_AsistenciaDocente.tipo)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarAsistenciaDocente(ByVal le_AsistenciaDocente As e_AsistenciaDocente) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PER_AsistenciaDocenteIUD", le_AsistenciaDocente.operacion, _
                                    le_AsistenciaDocente.cod_user, _
                                    le_AsistenciaDocente.codigo_ado, _
                                    le_AsistenciaDocente.codigo_hdo, _
                                    le_AsistenciaDocente.codigo_cup, _
                                    le_AsistenciaDocente.codigo_lho, _
                                    le_AsistenciaDocente.codigo_hop, _
                                    le_AsistenciaDocente.codigo_per, _
                                    le_AsistenciaDocente.descripcionHorario_ado, _
                                    le_AsistenciaDocente.tipo, _
                                    le_AsistenciaDocente.observacion)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetAsistenciaDocente(ByVal codigo As Integer) As e_AsistenciaDocente
        Try
            Dim me_AsistenciaDocente As New e_AsistenciaDocente

            If codigo > 0 Then

            Else
                With me_AsistenciaDocente
                    .cod_user = 0
                    .codigo_ado = 0
                    .codigo_hdo = 0
                    .codigo_cup = 0
                    .codigo_lho = 0
                    .codigo_hop = 0
                    .codigo_per = 0
                End With
            End If

            Return me_AsistenciaDocente
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ConsultarCodigoAccesoMoodle(ByVal le_AsistenciaDocente As e_AsistenciaDocente) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("MOODLE_ConsultarCodigoAcceso", le_AsistenciaDocente.operacion, _
                                    le_AsistenciaDocente.codigo_per)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ConsultarAplicacionUsuario(ByVal le_AsistenciaDocente As e_AsistenciaDocente) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ConsultarAplicacionUsuario", le_AsistenciaDocente.operacion, "", _
                                    le_AsistenciaDocente.codigo_per, "")

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_Consejo
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarConsejos(ByVal le_Consejo As e_Consejo) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GYT_ListaConsejo", le_Consejo.operacion, _
                                    le_Consejo.codigo_con, _
                                    le_Consejo.nombre_con, _
                                    le_Consejo.codigo_fac, _
                                    le_Consejo.vigencia_gyt, _
                                    le_Consejo.abreviatura_con)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_ConsejoFacultad
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarConsejoFacultad(ByVal le_ConsejoFacultad As e_ConsejoFacultad) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GYT_ListaConsejoFacultad", le_ConsejoFacultad.operacion, _
                                    le_ConsejoFacultad.codigo_cjf, _
                                    le_ConsejoFacultad.codigo_fac, _
                                    le_ConsejoFacultad.codigo_pcc, _
                                    le_ConsejoFacultad.estado_cjf, _
                                    le_ConsejoFacultad.cargo_cjf, _
                                    le_ConsejoFacultad.codigo_con, _
                                    le_ConsejoFacultad.codigo_cgo)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    ''lista personal para consejos
    Public Function ListarPersonalConsejo(ByVal le_consejoFacultad As e_ConsejoFacultad) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento ya echo se adecuo
            dt = cnx.TraerDataTable("PRP_ListaPersonalConsejo_POA", le_consejoFacultad.codigo_con, _
                                    le_consejoFacultad.operacion, le_consejoFacultad.codigo_fac)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    ''lista cargos por personal de consejo
    Public Function ListarCargosPersonalConsejo(ByVal le_Personal As e_Personal) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PRP_ConsultarCargosPersonal_POA", le_Personal.codigo_per)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    'inserta o actualiza personal o miembro del consejo
    Public Function ActualizaConsejo(ByVal le_consejo As e_ConsejoFacultad) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PRP_RegistrarMiembroConsejo_v2", le_consejo.codigo_con, _
                                    le_consejo.codigo_pcc, le_consejo.estado_cjf, le_consejo.cargo_cjf, _
                                    le_consejo.codigo_cgo, le_consejo.codigo_cjf, le_consejo.codigo_fac, le_consejo.usuario)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function



End Class

Public Class d_SesionConsejoUniv_GYT
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarSesionesConsejo(ByVal le_SesionConsejoUniv_GYT As e_SesionConsejoUniv_GYT) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            'cambio 11/11/2020
            'dt = cnx.TraerDataTable("GYT_ListaSesionConsejoUniversitario", le_SesionConsejoUniv_GYT.operacion, _
            '                        le_SesionConsejoUniv_GYT.codigo_scu)

            dt = cnx.TraerDataTable("GYT_ListaSesionesConsejo", le_SesionConsejoUniv_GYT.operacion, _
                                                                le_SesionConsejoUniv_GYT.codigo_scu, _
                                                                le_SesionConsejoUniv_GYT.descripcion_scu, _
                                                                le_SesionConsejoUniv_GYT.fecha_scu, _
                                                                le_SesionConsejoUniv_GYT.estado_scu, _
                                                                le_SesionConsejoUniv_GYT.usuario_reg, _
                                                                le_SesionConsejoUniv_GYT.fecha_reg, _
                                                                le_SesionConsejoUniv_GYT.abreviatura_con, _
                                                                le_SesionConsejoUniv_GYT.vigencia_scu, _
                                                                le_SesionConsejoUniv_GYT.tipo_sesion, _
                                                                le_SesionConsejoUniv_GYT.codigo_fac)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function ActualizaSesionesConsejo(ByVal le_SesionConsejoUniv_GYT As e_SesionConsejoUniv_GYT) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GYT_ActualiaSesionesConsejo", le_SesionConsejoUniv_GYT.operacion, _
                                    le_SesionConsejoUniv_GYT.codigo_scu, _
                                    le_SesionConsejoUniv_GYT.descripcion_scu, _
                                    le_SesionConsejoUniv_GYT.fecha_scu, _
                                    le_SesionConsejoUniv_GYT.estado_scu, _
                                    le_SesionConsejoUniv_GYT.usuario_reg, _
                                    le_SesionConsejoUniv_GYT.fecha_reg, _
                                    le_SesionConsejoUniv_GYT.abreviatura_con, _
                                    le_SesionConsejoUniv_GYT.vigencia_scu, _
                                    le_SesionConsejoUniv_GYT.tipo_sesion, _
                                    le_SesionConsejoUniv_GYT.codigo_fac)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_ServicioConcepto
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarServicioConcepto(ByVal le_ServicioConcepto As e_ServicioConcepto) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PEN_ServicioConceptoListar", le_ServicioConcepto.operacion, _
                                    le_ServicioConcepto.codigo_sco, _
                                    le_ServicioConcepto.tipo_sco, _
                                    le_ServicioConcepto.descripcion_sco, _
                                    le_ServicioConcepto.adeudo_sco)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_TramiteAlumno
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarTramitesTitulosUrl(ByVal le_tramiteAlumno As e_TramiteAlumno) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GYT_ListaTramitesAlumnos", le_tramiteAlumno.operacion, le_tramiteAlumno.codigo_tfu, le_tramiteAlumno.estado_dft, le_tramiteAlumno.estadoAprobacion, le_tramiteAlumno.fechaIni, le_tramiteAlumno.fechaFin _
                                    , le_tramiteAlumno.codigo_trl, le_tramiteAlumno.codigo_alu _
                                    , le_tramiteAlumno.codigo_cac, le_tramiteAlumno.correlativo_trl _
                                    , le_tramiteAlumno.glosaCorrelativo_trl, le_tramiteAlumno.fechaReg_trl _
                                    , le_tramiteAlumno.estado_trl, le_tramiteAlumno.observacion_trl)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class


Public Class d_Tesis
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ActualizarUrlTesis(ByVal le_tesis As e_Tesis) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GYT_ActualizarTesis", le_tesis.codigo_Tes, le_tesis.Titulo_Tes, le_tesis.url_Tes, le_tesis.operacion)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function ConsultarTesis(ByVal le_tesis As e_Tesis) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GYT_ConsultarTesis", le_tesis.operacion, "", le_tesis.codigo_Tes, "")

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_GrupoEgresado
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ConsultarGrupoEgresado(ByVal le_GrupoEgresado As e_GrupoEgresado) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ConsultarGrupoEgresado", le_GrupoEgresado.operacion, _
                                    le_GrupoEgresado.codigo_gru)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_TipoDenominacionGradoTitulo
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ConsultarTipoDenominacion(ByVal le_TipoDenominacionGradoTitulo As e_TipoDenominacionGradoTitulo) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GYT_ConsultarTipoDenominacion", le_TipoDenominacionGradoTitulo.operacion, _
                                    le_TipoDenominacionGradoTitulo.codigo_tdg)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_EnvioDiplomasProveedor
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function FormatoTramaEmisionDiploma(ByVal le_EnvioDiplomasProveedor As e_EnvioDiplomasProveedor) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GYT_FormatoTramaEmisionDiploma", le_EnvioDiplomasProveedor.operacion, _
                                    le_EnvioDiplomasProveedor.codigo_scu, _
                                    le_EnvioDiplomasProveedor.codigo_tdg, _
                                    le_EnvioDiplomasProveedor.tipo_emision)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function EstructuraTramaElectronica(ByVal le_EnvioDiplomasProveedor As e_EnvioDiplomasProveedor) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GYT_EstructuraTramaElectronica", le_EnvioDiplomasProveedor.operacion, _
                                    le_EnvioDiplomasProveedor.codigo_scu, _
                                    le_EnvioDiplomasProveedor.codigo_tdg, _
                                    le_EnvioDiplomasProveedor.tipo_emision)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function EntregarTramite(ByVal le_EnvioDiplomasProveedor As e_EnvioDiplomasProveedor) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("TRL_EntregaTramite", le_EnvioDiplomasProveedor.codigo_dta, _
                                    le_EnvioDiplomasProveedor.codigo_tfu, _
                                    le_EnvioDiplomasProveedor.cod_user)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarEnvioDiplomasProveedor(ByVal le_EnvioDiplomasProveedor As e_EnvioDiplomasProveedor) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GYT_EnvioDiplomasProveedorIUD", le_EnvioDiplomasProveedor.operacion, _
                                    le_EnvioDiplomasProveedor.cod_user, _
                                    le_EnvioDiplomasProveedor.codigo_edp, _
                                    le_EnvioDiplomasProveedor.tipo_edp, _
                                    le_EnvioDiplomasProveedor.codigo_scu, _
                                    le_EnvioDiplomasProveedor.codigo_tdg, _
                                    le_EnvioDiplomasProveedor.tipo_emision)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetEnvioDiplomasProveedor(ByVal codigo As Integer) As e_EnvioDiplomasProveedor
        Try
            Dim me_EnvioDiplomasProveedor As New e_EnvioDiplomasProveedor

            If codigo > 0 Then

            Else
                With me_EnvioDiplomasProveedor
                    .cod_user = 0
                    .codigo_edp = 0
                    .codigo_scu = 0                    
                    .codigo_tdg = 0                    
                End With
            End If

            Return me_EnvioDiplomasProveedor
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_EnvioDiplomasProveedorDetalle
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function RegistrarEnvioDiplomasProveedorDetalle(ByVal le_EnvioDiplomasProveedorDetalle As e_EnvioDiplomasProveedorDetalle) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GYT_EnvioDiplomasProveedorDetalleIUD", le_EnvioDiplomasProveedorDetalle.operacion, _
                                                le_EnvioDiplomasProveedorDetalle.cod_user, _
                                                le_EnvioDiplomasProveedorDetalle.codigo_edd, _
                                                le_EnvioDiplomasProveedorDetalle.codigo_edp, _
                                                le_EnvioDiplomasProveedorDetalle.codigo_trl, _
                                                le_EnvioDiplomasProveedorDetalle.codigo_egr, _
                                                le_EnvioDiplomasProveedorDetalle.codigoOperacionGrupo, _
                                                le_EnvioDiplomasProveedorDetalle.estadoOperacionGrupo, _
                                                le_EnvioDiplomasProveedorDetalle.mensajeOperacionGrupo, _
                                                le_EnvioDiplomasProveedorDetalle.estadoOperacionFirma, _
                                                le_EnvioDiplomasProveedorDetalle.mensajeOperacionFirma)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetEnvioDiplomasProveedorDetalle(ByVal codigo As Integer) As e_EnvioDiplomasProveedorDetalle
        Try
            Dim me_EnvioDiplomasProveedorDetalle As New e_EnvioDiplomasProveedorDetalle

            If codigo > 0 Then

            Else
                With me_EnvioDiplomasProveedorDetalle
                    .cod_user = 0                    
                    .codigo_edd = 0
                    .codigo_edp = 0
                    .codigo_trl = 0
                    .codigo_egr = 0
                    .codigoOperacionGrupo = 0
                    .estadoOperacionGrupo = 0
                    .estadoOperacionFirma = -1
                End With
            End If

            Return me_EnvioDiplomasProveedorDetalle
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''***** OLLUEN 13/11/2020
    Public Function ListarEstadoFirmaDiplomaProveedor(ByVal le_EnvioDiplomasProveedorDetalle As e_EnvioDiplomasProveedorDetalle) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GYT_ConsultaEstadoFirmaProveedor", le_EnvioDiplomasProveedorDetalle.operacion, _
                                    le_EnvioDiplomasProveedorDetalle.estadoOperacionFirma)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function


End Class

Public Class d_ControlPersonal
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarControlPersonal(ByVal le_ControlPersonal As e_ControlPersonal) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PER_ControlPersonalListar", le_ControlPersonal.operacion, _
                                    le_ControlPersonal.codigo_cpe, _
                                    le_ControlPersonal.codigo_per, _
                                    le_ControlPersonal.tipo)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_Marcaciones
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function RegistrarMarcaciones(ByVal le_Marcaciones As e_Marcaciones) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PER_MarcacionesIUD", le_Marcaciones.operacion, _
                                    le_Marcaciones.codigo_mar, _
                                    le_Marcaciones.dni_per, _
                                    le_Marcaciones.id_marcador, _
                                    le_Marcaciones.procesado_mar, _
                                    le_Marcaciones.tipo_mar, _
                                    le_Marcaciones.id_marcacion_zk, _
                                    le_Marcaciones.codigo_cpe, _
                                    le_Marcaciones.tipo_operacion)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetMarcaciones(ByVal codigo As Integer) As e_Marcaciones
        Try
            Dim me_Marcaciones As New e_Marcaciones

            If codigo > 0 Then

            Else
                With me_Marcaciones
                    .cod_user = 0
                    .codigo_mar = 0
                    .procesado_mar = 0
                    .id_marcacion_zk = 0
                    .codigo_cpe = 0
                End With
            End If

            Return me_Marcaciones
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_RecursoVirtual
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarRecursoVirtual(ByVal le_RecursoVirtual As e_RecursoVirtual) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("BIB_RecursoVirtualListar", le_RecursoVirtual.operacion, _
                                    le_RecursoVirtual.codigo_rvi, _
                                    le_RecursoVirtual.tipoRepo_rvi, _
                                    le_RecursoVirtual.disciplinaRepo_rvi, _
                                    le_RecursoVirtual.acceso_rvi, _
                                    le_RecursoVirtual.estado_rvi)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarRecursoVirtual(ByVal le_RecursoVirtual As e_RecursoVirtual) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("BIB_RecursoVirtualIUD", le_RecursoVirtual.operacion, _
                                    le_RecursoVirtual.cod_user, _
                                    le_RecursoVirtual.codigo_rvi, _
                                    le_RecursoVirtual.tipoRepo_rvi, _
                                    le_RecursoVirtual.disciplinaRepo_rvi, _
                                    le_RecursoVirtual.nombre_rvi, _
                                    le_RecursoVirtual.logo_rvi, _
                                    le_RecursoVirtual.contarVisita_rvi, _
                                    le_RecursoVirtual.codigo_biv, _
                                    le_RecursoVirtual.acceso_rvi, _
                                    le_RecursoVirtual.orden_rvi, _
                                    le_RecursoVirtual.IdArchivosCompartidos, _
                                    le_RecursoVirtual.estado_rvi)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetRecursoVirtual(ByVal codigo As Integer) As e_RecursoVirtual
        Try
            Dim me_RecursoVirtual As New e_RecursoVirtual

            If codigo > 0 Then
                me_RecursoVirtual.operacion = "GEN"
                me_RecursoVirtual.codigo_rvi = codigo

                dt = ListarRecursoVirtual(me_RecursoVirtual)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de recurso virtual no ha sido encontrado.")

                me_RecursoVirtual = New e_RecursoVirtual

                With me_RecursoVirtual
                    .codigo_rvi = dt.Rows(0).Item("codigo_rvi")
                    .tipoRepo_rvi = dt.Rows(0).Item("tipoRepo_rvi")
                    .disciplinaRepo_rvi = dt.Rows(0).Item("disciplinaRepo_rvi")
                    .nombre_rvi = dt.Rows(0).Item("nombre_rvi")
                    .logo_rvi = dt.Rows(0).Item("logo_rvi")
                    .contarVisita_rvi = dt.Rows(0).Item("contarVisita_rvi")
                    .codigo_biv = dt.Rows(0).Item("codigo_biv")
                    .acceso_rvi = dt.Rows(0).Item("acceso_rvi")
                    .orden_rvi = dt.Rows(0).Item("orden_rvi")
                    .IdArchivosCompartidos = dt.Rows(0).Item("IdArchivosCompartidos")
                    .estado_rvi = dt.Rows(0).Item("estado_rvi")
                End With
            Else
                With me_RecursoVirtual
                    .cod_user = 0
                    .codigo_rvi = 0
                    .codigo_biv = 0
                    .orden_rvi = 0
                    .IdArchivosCompartidos = 0
                End With
            End If

            Return me_RecursoVirtual
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_RecursoVirtualDetalle
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarRecursoVirtualDetalle(ByVal le_RecursoVirtualDetalle As e_RecursoVirtualDetalle) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("BIB_RecursoVirtualDetalleListar", le_RecursoVirtualDetalle.operacion, _
                                    le_RecursoVirtualDetalle.codigo_rvd, _
                                    le_RecursoVirtualDetalle.codigo_rvi, _
                                    le_RecursoVirtualDetalle.acceso_rvd)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarRecursoVirtualDetalle(ByVal le_RecursoVirtualDetalle As e_RecursoVirtualDetalle) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("BIB_RecursoVirtualDetalleIUD", le_RecursoVirtualDetalle.operacion, _
                                    le_RecursoVirtualDetalle.cod_user, _
                                    le_RecursoVirtualDetalle.codigo_rvd, _
                                    le_RecursoVirtualDetalle.codigo_rvi, _
                                    le_RecursoVirtualDetalle.titulo_rvd, _
                                    le_RecursoVirtualDetalle.cuerpo_rvd, _
                                    le_RecursoVirtualDetalle.acceso_rvd, _
                                    le_RecursoVirtualDetalle.orden_rvd)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetRecursoVirtualDetalle(ByVal codigo As Integer) As e_RecursoVirtualDetalle
        Try
            Dim me_RecursoVirtualDetalle As New e_RecursoVirtualDetalle

            If codigo > 0 Then
                me_RecursoVirtualDetalle.operacion = "GEN"
                me_RecursoVirtualDetalle.codigo_rvd = codigo

                dt = ListarRecursoVirtualDetalle(me_RecursoVirtualDetalle)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de recurso virtual detalle no ha sido encontrado.")

                me_RecursoVirtualDetalle = New e_RecursoVirtualDetalle

                With me_RecursoVirtualDetalle
                    .codigo_rvd = dt.Rows(0).Item("codigo_rvd")
                    .codigo_rvi = dt.Rows(0).Item("codigo_rvi")
                    .titulo_rvd = dt.Rows(0).Item("titulo_rvd")
                    .cuerpo_rvd = dt.Rows(0).Item("cuerpo_rvd")
                    .acceso_rvd = dt.Rows(0).Item("acceso_rvd")
                    .orden_rvd = dt.Rows(0).Item("orden_rvd")
                End With
            Else
                With me_RecursoVirtualDetalle
                    .cod_user = 0
                    .codigo_rvd = 0
                    .codigo_rvi = 0                    
                    .orden_rvd = 0
                End With
            End If

            Return me_RecursoVirtualDetalle
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_RecursoVirtualEnlace
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarRecursoVirtualEnlace(ByVal le_RecursoVirtualEnlace As e_RecursoVirtualEnlace) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("BIB_RecursoVirtualEnlaceListar", le_RecursoVirtualEnlace.operacion, _
                                    le_RecursoVirtualEnlace.codigo_rve, _
                                    le_RecursoVirtualEnlace.codigo_rvd, _
                                    le_RecursoVirtualEnlace.acceso_rve)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarRecursoVirtualEnlace(ByVal le_RecursoVirtualEnlace As e_RecursoVirtualEnlace) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("BIB_RecursoVirtualEnlaceIUD", le_RecursoVirtualEnlace.operacion, _
                                    le_RecursoVirtualEnlace.cod_user, _
                                    le_RecursoVirtualEnlace.codigo_rve, _
                                    le_RecursoVirtualEnlace.codigo_rvd, _
                                    le_RecursoVirtualEnlace.descripcion_rve, _
                                    le_RecursoVirtualEnlace.enlace_rve, _
                                    le_RecursoVirtualEnlace.contarVisita_rve, _
                                    le_RecursoVirtualEnlace.codigo_biv, _
                                    le_RecursoVirtualEnlace.acceso_rve, _
                                    le_RecursoVirtualEnlace.orden_rve, _
                                    le_RecursoVirtualEnlace.IdArchivosCompartidos)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetRecursoVirtualEnlace(ByVal codigo As Integer) As e_RecursoVirtualEnlace
        Try
            Dim me_RecursoVirtualEnlace As New e_RecursoVirtualEnlace

            If codigo > 0 Then
                me_RecursoVirtualEnlace.operacion = "GEN"
                me_RecursoVirtualEnlace.codigo_rve = codigo

                dt = ListarRecursoVirtualEnlace(me_RecursoVirtualEnlace)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de recurso virtual enlace no ha sido encontrado.")

                me_RecursoVirtualEnlace = New e_RecursoVirtualEnlace

                With me_RecursoVirtualEnlace
                    .codigo_rve = dt.Rows(0).Item("codigo_rve")
                    .codigo_rvd = dt.Rows(0).Item("codigo_rvd")
                    .descripcion_rve = dt.Rows(0).Item("descripcion_rve")
                    .enlace_rve = dt.Rows(0).Item("enlace_rve")
                    .contarVisita_rve = dt.Rows(0).Item("contarVisita_rve")
                    .codigo_biv = dt.Rows(0).Item("codigo_biv")
                    .acceso_rve = dt.Rows(0).Item("acceso_rve")
                    .orden_rve = dt.Rows(0).Item("orden_rve")
                    .IdArchivosCompartidos = dt.Rows(0).Item("IdArchivosCompartidos")
                    .enlace = dt.Rows(0).Item("enlace")
                End With
            Else
                With me_RecursoVirtualEnlace
                    .cod_user = 0
                    .codigo_rve = 0
                    .codigo_rvd = 0                    
                    .codigo_biv = 0
                    .orden_rve = 0
                    .IdArchivosCompartidos = 0
                End With
            End If

            Return me_RecursoVirtualEnlace
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RegistrarVisita(ByVal le_RecursoVirtualEnlace As e_RecursoVirtualEnlace) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("BIB_RegistrarVisita", le_RecursoVirtualEnlace.tipo_vis, _
                                    le_RecursoVirtualEnlace.codigo_vis, _
                                    le_RecursoVirtualEnlace.codigo_biv)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_BibliotecaVirtual
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarBibliotecaVirtual(ByVal le_BibliotecaVirtual As e_BibliotecaVirtual) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("BIB_BibliotecaVirtualListar", le_BibliotecaVirtual.operacion, _
                                    le_BibliotecaVirtual.codigo_biv, _
                                    le_BibliotecaVirtual.nombre_biv)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_DerechoHabientes
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarDerechoHabientes(ByVal le_DerechoHabientes As e_DerechoHabientes) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PER_DerechoHabientesListar", le_DerechoHabientes.operacion, _
                                    le_DerechoHabientes.codigo_dhab, _
                                    le_DerechoHabientes.codigo_per)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetDerechoHabientes(ByVal codigo As Integer) As e_DerechoHabientes
        Try
            Dim me_DerechoHabientes As New e_DerechoHabientes

            If codigo > 0 Then

            Else
                With me_DerechoHabientes
                    .cod_user = 0
                    .codigo_dhab = 0
                    .codigo_niv = 0
                    .codigo_gra = 0
                    .IdArchivosCompartidosRecibo = 0
                    .IdArchivosCompartidosDNI = 0
                End With
            End If

            Return me_DerechoHabientes
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_SolicitudEscolaridad
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function RegistrarSolicitudEscolaridad(ByVal le_SolicitudEscolaridad As e_SolicitudEscolaridad) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PER_SolicitudEscolaridadIUD", le_SolicitudEscolaridad.operacion, _
                                    le_SolicitudEscolaridad.codigo_soe, _
                                    le_SolicitudEscolaridad.codigo_dhab, _
                                    le_SolicitudEscolaridad.estado_soe, _
                                    le_SolicitudEscolaridad.tipocentroestudio_soe, _
                                    le_SolicitudEscolaridad.nombrecentroestudio_soe, _
                                    le_SolicitudEscolaridad.grado_soe, _
                                    le_SolicitudEscolaridad.centroaplicacion_soe, _
                                    le_SolicitudEscolaridad.documentosadjuntos_soe, _
                                    le_SolicitudEscolaridad.IdArchivosCompartidosRecibo, _
                                    le_SolicitudEscolaridad.IdArchivosCompartidosDNI)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarSolicitudEscolaridad(ByVal le_SolicitudEscolaridad As e_SolicitudEscolaridad) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("PER_SolicitudEscolaridadListar", le_SolicitudEscolaridad.operacion, _
                                    le_SolicitudEscolaridad.codigo_soe, _
                                    le_SolicitudEscolaridad.codigo_dhab, _
                                    le_SolicitudEscolaridad.codigo_per, _
                                    le_SolicitudEscolaridad.anio_soe)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetSolicitudEscolaridad(ByVal codigo As Integer) As e_SolicitudEscolaridad
        Try
            Dim me_SolicitudEscolaridad As New e_SolicitudEscolaridad

            If codigo > 0 Then

            Else
                With me_SolicitudEscolaridad
                    .cod_user = 0
                    .codigo_soe = 0
                    .codigo_dhab = 0
                    .centroaplicacion_soe = False
                    .codigo_per = 0
                End With
            End If

            Return me_SolicitudEscolaridad
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarNivelEscolaridad(ByVal le_SolicitudEscolaridad As e_SolicitudEscolaridad) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ES_ListaNivelEscolaridad")

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarGrados(ByVal le_SolicitudEscolaridad As e_SolicitudEscolaridad) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ES_ListarGrados", le_SolicitudEscolaridad.tipocentroestudio_soe)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

#End Region

#Region "FUNCIONES"

Public Class d_Funciones

    'VARIABLES
    Dim miCookie As New CookieContainer

    Public Sub CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String, Optional ByVal ind_todos As Boolean = False, Optional ByVal txt_todos As String = "", Optional ByVal val_todos As String = "")
        Try
            cbo.DataSource = dt
            cbo.DataTextField = datatext
            cbo.DataValueField = datavalue

            cbo.DataBind()

            If ind_todos Then
                cbo.Items.Insert(0, New ListItem(txt_todos, val_todos))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub AgregarHearders(ByVal grw As GridView)
        Try
            If (grw.Rows.Count > 0) Then
                'This replaces <td> with <th>
                grw.UseAccessibleHeader = True
                'This will add the <thead> and <tbody> elements
                grw.HeaderRow.TableSection = TableRowSection.TableHeader
                'This adds the <tfoot> element. Remove if you don't have a footer row
                grw.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ObtenerDataTable(ByVal ls_TipoTabla As String) As Data.DataTable
        Try
            Dim dt As New Data.DataTable
            dt.Columns.Add("codigo")
            dt.Columns.Add("nombre")

            Select Case ls_TipoTabla
                Case "SEXO"
                    Dim fila As Data.DataRow

                    fila = dt.NewRow()
                    fila("codigo") = "M" : fila("nombre") = "MASCULINO"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "F" : fila("nombre") = "FEMENINO"
                    dt.Rows.Add(fila)

                Case "CODIGO_TELEFONICO"
                    Dim fila As Data.DataRow

                    fila = dt.NewRow()
                    fila("codigo") = "74" : fila("nombre") = "LAMBAYEQUE"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "41" : fila("nombre") = "AMAZONAS"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "43" : fila("nombre") = "ANCASH"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "83" : fila("nombre") = "APURIMAC"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "54" : fila("nombre") = "AREQUIPA"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "76" : fila("nombre") = "CAJAMARCA"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "84" : fila("nombre") = "CUSCO"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "67" : fila("nombre") = "HUANCAVELICA"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "62" : fila("nombre") = "HUANUCO"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "56" : fila("nombre") = "ICA"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "64" : fila("nombre") = "JUNIN"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "44" : fila("nombre") = "LA LIBERTAD"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "1" : fila("nombre") = "LIMA Y CALLAO"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "65" : fila("nombre") = "LORETO"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "82" : fila("nombre") = "MADRE DE DIOS"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "53" : fila("nombre") = "MOQUEGUA"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "63" : fila("nombre") = "PASCO"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "73" : fila("nombre") = "PIURA"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "51" : fila("nombre") = "PUNO"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "42" : fila("nombre") = "SAN MARTIN"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "52" : fila("nombre") = "TACNA"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "72" : fila("nombre") = "TUMBES"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "61" : fila("nombre") = "UCAYALI"
                    dt.Rows.Add(fila)

                Case "DENOMINACION_PERSONA"
                    Dim fila As Data.DataRow

                    fila = dt.NewRow()
                    fila("codigo") = "SR." : fila("nombre") = "SR."
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "SRA." : fila("nombre") = "SRA."
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "SRTA." : fila("nombre") = "SRTA."
                    dt.Rows.Add(fila)

                Case "OPERADOR_MOVIL"
                    Dim fila As Data.DataRow

                    fila = dt.NewRow()
                    fila("codigo") = "MOVISTAR" : fila("nombre") = "MOVISTAR"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "CLARO" : fila("nombre") = "CLARO"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "BITEL" : fila("nombre") = "BITEL"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "ENTEL" : fila("nombre") = "ENTEL"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "OTRO" : fila("nombre") = "OTRO"
                    dt.Rows.Add(fila)

                Case "OPERADOR_INTERNET"
                    Dim fila As Data.DataRow

                    fila = dt.NewRow()
                    fila("codigo") = "MOVISTAR" : fila("nombre") = "MOVISTAR"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "CLARO" : fila("nombre") = "CLARO"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "BITEL" : fila("nombre") = "BITEL"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "ENTEL" : fila("nombre") = "ENTEL"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "OTRO" : fila("nombre") = "OTRO"
                    dt.Rows.Add(fila)

                Case "ESTADO_CONF_INSTANCIA_ADEUDOS"
                    Dim fila As Data.DataRow

                    fila = dt.NewRow()
                    fila("codigo") = "A" : fila("nombre") = "ACTIVO"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "I" : fila("nombre") = "INACTIVO"
                    dt.Rows.Add(fila)

                Case "ESTADO_ADEUDOS"
                    Dim fila As Data.DataRow

                    fila = dt.NewRow()
                    fila("codigo") = "P" : fila("nombre") = "PENDIENTE"
                    dt.Rows.Add(fila)

                    fila = dt.NewRow()
                    fila("codigo") = "D" : fila("nombre") = "DEVUELTO"
                    dt.Rows.Add(fila)

            End Select

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ValidarEmail(ByVal email As String) As Boolean
        Try
            If String.IsNullOrEmpty(email) Then Return True

            Dim estructura As String = "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$"
            Dim match As Match = Regex.Match(email.Trim(), estructura, RegexOptions.IgnoreCase)

            If match.Success Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function EncrytedString64(ByVal texto As String) As String
        Try
            Dim result As String = ""
            Dim encryted As Byte()
            encryted = System.Text.Encoding.Unicode.GetBytes(texto)
            result = Convert.ToBase64String(encryted)
            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DecrytedString64(ByVal textoEncriptado As String) As String
        Try
            Dim result As String = ""
            Dim decryted As Byte()
            decryted = Convert.FromBase64String(textoEncriptado)
            result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.Length)
            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GenerarPassword(ByVal ln_longitud As Integer) As String
        Try
            Dim caracteres As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
            Dim res As New StringBuilder()
            Dim rnd As New Random()
            While 0 < System.Math.Max(System.Threading.Interlocked.Decrement(ln_longitud), ln_longitud + 1)
                res.Append(caracteres(rnd.[Next](caracteres.Length)))
            End While

            Return res.ToString()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ValidarRucSunat(ByVal str_ruc As String) As Boolean
        Try
            Dim urlSunat As String = String.Format("http://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=random", False)
            Dim enlaceSunat As HttpWebRequest = WebRequest.Create(urlSunat)
            enlaceSunat.CookieContainer = Me.miCookie
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
            enlaceSunat.Credentials = CredentialCache.DefaultCredentials
            Dim respuesta_web As WebResponse = enlaceSunat.GetResponse()

            If CType(respuesta_web, HttpWebResponse).StatusCode = HttpStatusCode.OK Then
                Dim myStream As Stream = respuesta_web.GetResponseStream
                Dim myStreamReader As New StreamReader(myStream)
                Dim xDat As String = myStreamReader.ReadToEnd.ToString.Trim

                If InStr(xDat, "consultado no es v�lido") > 0 Then
                    Return False
                End If

                xDat = Replace(xDat, "          ", " ")
                xDat = Replace(xDat, "         ", " ")
                xDat = Replace(xDat, "        ", " ")
                xDat = Replace(xDat, "       ", " ")
                xDat = Replace(xDat, "      ", " ")
                xDat = Replace(xDat, "     ", " ")
                xDat = Replace(xDat, "    ", " ")
                xDat = Replace(xDat, "   ", " ")
                xDat = Replace(xDat, "  ", " ")
                xDat = Replace(Replace(xDat, Chr(10), " "), Chr(13), "")
                xDat = Replace(xDat, "&Ntilde;", "Ñ")
                xDat = Replace(xDat, "&ntilde;", "ñ")
                xDat = Replace(xDat, "&Oacute;", "Ó")
                xDat = Replace(xDat, "&oacute;", "ó")
                xDat = Replace(xDat, "&Uacute;", "Ú")
                xDat = Replace(xDat, "&uacute;", "ú")
                xDat = Replace(xDat, "&Aacute;", "Á")
                xDat = Replace(xDat, "&aacute;", "á")
                xDat = Replace(xDat, "&Eacute;", "É")
                xDat = Replace(xDat, "&eacute;", "é")
                xDat = Replace(xDat, "&Iacute;", "Í")
                xDat = Replace(xDat, "&Iacute;", "Í")
                xDat = Replace(xDat, "�", "Ñ")

                If Not ObtenerDatosSunat(str_ruc, xDat) Then Return False
            Else
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerDatosSunat(ByVal str_ruc As String, ByVal str_captcha As String) As Boolean
        Try
            Dim urlSunat As String = String.Format("http://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc=" & str_ruc & "&numRnd=" & str_captcha, False)
            Dim enlaceSunat As HttpWebRequest = WebRequest.Create(urlSunat)
            enlaceSunat.CookieContainer = Me.miCookie
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
            enlaceSunat.Credentials = CredentialCache.DefaultCredentials
            Dim respuesta_web As WebResponse = enlaceSunat.GetResponse

            If CType(respuesta_web, HttpWebResponse).StatusCode = HttpStatusCode.OK Then
                Dim myStream As Stream = respuesta_web.GetResponseStream
                Dim myStreamReader As New StreamReader(myStream)
                Dim xDat As String = myStreamReader.ReadToEnd.ToString.Trim

                If InStr(xDat, "N&uacute;mero de RUC:") = 0 Then Return False

                Dim Dict As Object
                Dict = CreateObject("Scripting.Dictionary")
                Dim xTabla() As String

                xDat = Replace(xDat, "          ", " ")
                xDat = Replace(xDat, "         ", " ")
                xDat = Replace(xDat, "        ", " ")
                xDat = Replace(xDat, "       ", " ")
                xDat = Replace(xDat, "      ", " ")
                xDat = Replace(xDat, "     ", " ")
                xDat = Replace(xDat, "    ", " ")
                xDat = Replace(xDat, "   ", " ")
                xDat = Replace(xDat, "  ", " ")
                xDat = Replace(Replace(xDat, Chr(10), " "), Chr(13), "")
                xDat = Replace(xDat, "&Ntilde;", "Ñ")
                xDat = Replace(xDat, "&ntilde;", "ñ")
                xDat = Replace(xDat, "&Oacute;", "Ó")
                xDat = Replace(xDat, "&oacute;", "ó")
                xDat = Replace(xDat, "&Uacute;", "Ú")
                xDat = Replace(xDat, "&uacute;", "ú")
                xDat = Replace(xDat, "&Aacute;", "Á")
                xDat = Replace(xDat, "&aacute;", "á")
                xDat = Replace(xDat, "&Eacute;", "É")
                xDat = Replace(xDat, "&eacute;", "é")
                xDat = Replace(xDat, "&Iacute;", "Í")
                xDat = Replace(xDat, "&Iacute;", "Í")
                xDat = Replace(xDat, "�", "Ñ")
                xTabla = Split(Trim(xDat), "<tr>")

                If UBound(xTabla) = 0 Then Return False
            Else
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerContentType(ByVal extencion As String) As String
        Try
            Dim content_type As String = String.Empty

            Select Case extencion.Trim.ToLower
                Case ".pdf"
                    content_type = "application/pdf"

                Case ".txt"
                    content_type = "text/plain"

                Case ".doc"
                    content_type = "application/msword"

                Case ".docx"
                    content_type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"

                Case ".xls"
                    content_type = "application/vnd.ms-excel"

                Case ".xlsx"
                    content_type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"

                Case ".gif"
                    content_type = "image/gif"

                Case ".jpg", ".jpeg"
                    content_type = "image/jpeg"

                Case ".bmp"
                    content_type = "image/bmp"

                Case ".wav"
                    content_type = "audio/wav"

                Case ".ppt"
                    content_type = "application/vnd.ms-powerpoint"

                Case ".pptx"
                    content_type = "application/vnd.openxmlformats-officedocument.presentationml.presentation"

                Case ".dwg"
                    content_type = "image/vnd.dwg"

                Case Else
                    content_type = "application/octet-stream"

            End Select

            Return content_type
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'FIRMA DE MENSAJES

    Public Function FirmaMensajeAlumni(ByVal le_Personal As e_Personal) As String
        Try
            Dim ld_CarreraProfesional As New d_CarreraProfesional
            Dim ld_Personal As New d_Personal
            Dim le_CarreraProfesional As New e_CarreraProfesional

            Dim dt As Data.DataTable
            Dim ls_firma As String = String.Empty


            'Listar escuelas por coordinador
            dt = New Data.DataTable

            With le_CarreraProfesional
                .operacion = "PER"
                .codigo_per = le_Personal.codigo_per
            End With

            dt = ld_CarreraProfesional.ListarCarreraProfesional(le_CarreraProfesional)

            Dim ls_escuela As String = String.Empty

            For i As Integer = 0 To dt.Rows.Count - 1
                If String.IsNullOrEmpty(ls_escuela) Then
                    ls_escuela = dt.Rows(i).Item("nombre_Cpf").ToString
                Else
                    ls_escuela = ls_escuela + ", " + dt.Rows(i).Item("nombre_Cpf").ToString
                End If
            Next

            'Listar los celulares del personal
            dt = New Data.DataTable
            dt = ld_Personal.ObtenerCelular(le_Personal)

            Dim ls_celular As String = String.Empty
            If dt.Rows.Count > 0 Then ls_celular = dt.Rows(0).Item("celular_Per").ToString

            Dim ls_direccion As String = "Av. San Josemaría Escrivá N°855. Chiclayo - Perú"
            Dim ls_telefono As String = "T: (074) 606200. Anexo: 1239 - C: " & ls_celular
            Dim ls_paginaWeb As String = "www.usat.edu.pe / http://www.facebook.com/usat.peru"

            ls_firma = "<br /><br />---------------------------------------<br />"
            ls_firma &= le_Personal.nombres_per & "<br />"

            Select Case CInt(le_Personal.codigo_tfu)

                Case g_VariablesGlobales.TipoFuncionAdministradorSistema
                    ls_firma &= "Administrador del Sistema  - " & ls_escuela & "<br />"

                Case g_VariablesGlobales.TipoFuncionDirectorAlumni
                    ls_firma &= "Dirección de Alumni - " & ls_escuela & "<br />"

                Case g_VariablesGlobales.TipoFuncionCoordinadorAlumni
                    ls_firma &= "Coordinación de Alumni - " & ls_escuela & "<br />"

            End Select

            ls_firma &= ls_direccion & "<br />"
            ls_firma &= ls_telefono & "<br />"
            ls_firma &= ls_paginaWeb & "<br />"

            ls_firma &= "<div>Síguenos en :</div>"

            'Obtener Logo
            Dim ls_logo As String = String.Empty
            Dim ls_estiloLogo As String = " background-color: #5B74A8;  border-color: #29447E #29447E #1A356E;    border-image: none;    border-style: solid;    border-width: 1px;    box-shadow: 0 1px 0 rgba(0, 0, 0, 0.1), 0 1px 0 #8A9CC2 inset;    color: #FFFFFF;    cursor: pointer;    display: inline-block;    font: bold 20px verdana,arial,sans-serif;    margin: 0;    overflow: visible;    padding: 0.1em 0.5em 0.1em;    position: relative;    text-align: center;    text-decoration: none;white-space: nowrap;z-index: 1;"
            ls_logo = "<a href='https://www.facebook.com/AlumniUSAT/' style='" & ls_estiloLogo & " ' ><b>f</b></a>"

            ls_firma &= ls_logo

            Return ls_firma
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

#End Region

#Region "VARIABLES GLOBALES"

Public Class g_VariablesGlobales

#Region "NIVEL_ESTUDIO"
    Public Shared NivelEstudioPreGrado As String = "1"
    Public Shared NivelEstudioPostGrado As String = "2"
    Public Shared NivelEstudioPostTitulo As String = "3"

    Public Shared CodigoTestPreGrado As String = "2,3,4,7,10" '2,3,4,7
    Public Shared CodigoTestPostGrado As String = "5" '5,9,10
    Public Shared CodigoTestPostTitulo As String = "8" '8
#End Region

#Region "ESTADO_EMPRESA"
    Public Shared EstadoEmpresaGenerado As String = "6"
    Public Shared EstadoEmpresaAprobado As String = "7"
    Public Shared EstadoEmpresaAlta As String = "8"
    Public Shared EstadoEmpresaBaja As String = "9"
    Public Shared EstadoEmpresaRechazado As String = "10"
#End Region

#Region "APLICACION"
    'Código de Aplicación
    Public Shared CodigoAplicacionAlumni As Integer = 47

    'Rutas
    Public Shared RutaLogos As String = "../Alumni/files/logos/"
    Public Shared RutaPlantillaOnomastico As String = "../../../filesOnomastico/img/"
    Public Shared RutaEmpresaLogin As String = ConfigurationManager.AppSettings("RutaCampus").ToString & "CampusEmpresa/frmLogin.aspx"

    'Mensajes
    Public Shared MensajeNoTienePermiso As String = "Usted no cuenta con los permisos necesarios para ejecutar la acción."

#End Region

#Region "CODIGOS_TABLA_ARCHIVO"
    Public Shared TablaArchivoCorreosMasivo As Integer = 26
    Public Shared TablaArchivoEmpresaLogos As Integer = 27
#End Region

#Region "ENVIO_CORREOS"

    'CORREOS
    Public Shared CorreoPrueba As String = "adiazval20@gmail.com"
    Public Shared CorreoAlumni As String = "alumni@usat.edu.pe"
    Public Shared CorreoCoordinadorAlumni As String = "esther.vasquez@usat.edu.pe"

    'FORMATO
    Public Shared AbrirFormatoCorreoAlumni As String = "<font face='Trebuchet MS'>"
    Public Shared CerrarFormatoCorreoAlumni As String = "</font>"

#End Region

#Region "TIPO_FUNCION"

    Public Shared TipoFuncionAdministradorSistema As Integer = 1 'ADMINISTRADOR DEL SISTEMA
    Public Shared TipoFuncionDirectorAlumni As Integer = 90 'DIRECTOR DE ALUMNI
    Public Shared TipoFuncionCoordinadorAlumni As Integer = 145 'COORDINADOR DE ALUMNI

#End Region

#Region "PERSONAL"

    Public Shared PersonalCoordinadorAlumni As Integer = 26982 'ESTHER VASQUEZ

#End Region

#Region "CARRERA_PROFESIONAL"

    Public Shared CarreraProfesionalProgramaIntercambio As String = 35
    Public Shared CarreraProfesionalEscuelaPre As String = 36
    Public Shared CarreraProfesionalCursosComplementarios As String = 19

#End Region

End Class

#End Region