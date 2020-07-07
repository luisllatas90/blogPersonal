﻿
Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Imports System.Xml

Partial Class operaciones
    Inherits System.Web.UI.Page

    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles")

    Private token As String = "FHAIWBVE36"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsGestionInvestigacion
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim k As String = Request("k")
            Dim f As String = Request("f")

            Select Case Request("action")
                Case "gRegistrarInvestigador"
                    RegistrarInvestigador()
                Case "lAreaConocimientosOCDE"
                    ListarAreaConocimientosOCDE()
                Case "lInvesitgadores"
                    ListrInvestigadores()
                Case "lInvestigadoresEstado"
                    ListarInvestigadoresEstado()
                Case "lRolInvestigador"
                    Dim tipo As String = Request("tipo")
                    ListarRolInvestigador(tipo)
                Case "gGrupoDetalleInvestigacion"
                    RegistrarGrupoDetalleInvestigacion()
                Case "lGrupoComoCoordinador"
                    ListarGrupoComoCoordinador()
                Case "gBonoPublicacion"
                    RegistrarBonoPublicacion()
                Case "lBonosPublicacion"
                    ListarBonosPublicacion()
                Case "actualizarInvestigador"
                    actualizarEstadoInvestigador()
                Case "lGrupoInvestigadores"
                    ListaGrupoInvestigadores()
                Case "actualizarGrupoInvestigador"
                    actualizarEstadoGrupoInvestigador()
                Case "lLineasUsat"
                    ListarLineasUsat()
                Case "lRegion"
                    ListarRegion()
                Case "gHistorialGI"
                    GuardarHistorialGI()
                Case "gVisualizarHistorialGI"
                    VisualizarHistorialGI()
                Case "actualizarBonoPublicacion"
                    ActualizarBonoPublicacion()
                Case "lBonosPublicacionTO"
                    ListarBonosPublicacionTO()
                Case "lBonosPublicacionEstado"
                    ListarBonosPublicacionEstado()
                Case "SubmitEMail"
                    EnviarEmail()
                Case "lPais"
                    ListaPais()
                Case "gEvaluadorExterno"
                    RegistrarEvaluadorExterno()
                Case "SurbirCV"
                    SurbirCV()
                    'Hcano 18-06-18
                Case "envioNotificacionEvaluadorExterno"
                    enviarEmailNotificacionEvaluadorExterno()
                Case "SurbirCVNew"
                    SurbirCVNew()
                Case "lEvaluadoresExt"
                    ListaEvaluadoresExternos()
                Case "gEstadoInvestigadorExterno"
                    EstadoInvestigadorExterno()
                Case "lPostulacionesPorEvaluar"
                    ListarPostulacionesPorEvaluar()
                Case "lPostulacionesXCodigo"
                    ListaPostulacionesXCodigo()
                Case "lObjetivosPos"
                    ListaObjetivosPos()
                Case "aEvaluarPostulacionExterno"
                    ActualizarEvaluarPostulacionExterno()
                Case "SubirRubrica"
                    SubirRubrica()
                Case "SubirRubricaNew"
                    SubirRubricaNew()
                Case "lConcursosEvaluacionFinal"
                    ListaConcursosEvaluacionFinal()
                Case "lPostulacionesEvaluacionFinal"
                    ListaPostulacionesEvaluacionFinal()
                Case "lDetallePostulacionesFinal"
                    ListaDetallePostulacionesFinal()
                Case "gActualizarEvaluacionFinal"
                    ActualizarEvaluacionFinal()
                Case "ListaEvaluadoresRF"
                    ListarEvaluadoresRF()
                Case "AsignarEvaluadorRF"
                    AsignaEvaluadorRF()
                Case "gGanadorConcursoPostulaciones"
                    GanadorConcursoPostulaciones()
                Case "envioEmailEvaluadorExterno"
                    enviarEmailEvaluadorExterno()
                Case "validaInvestigador"
                    validaInvestigador()
                Case "SubirPropuestaGrupo"
                    SubirPropuestaGrupo()
                Case "SubirPropuestaGrupoNew"
                    SubirPropuestaGrupoNew()
                Case "lInvestigadorTesistaGraduado"
                    ListaInvestigadorTesistaGraduado()
                Case "actualizarEliminaInvGrupoInvestigador"
                    actualizarEliminaInvGrupoInvestigador()
                Case "Download2"
                    DescargarArchivo2()
                Case "Download3"
                    DescargarArchivo3()
                Case "ValidaSession"
                    ValidaSession()
                Case "ope"
                    TiposOperacion()
                Case "ConsultarPersonal"
                    Dim ctf As Integer = Request("ctf")
                    Dim texto As String = Request("texto")
                    ListarPersonal(Session("id_per"), ctf, texto)
                Case "ConsultarLineas"
                    Dim cpf As String = Request("cpf")
                    ListaLineasInvestigacion(cpf)
                Case "ListaTipoAutorProyecto"
                    Dim codigo As String = Request("cod")
                    ListaTipoAutorProyecto(codigo)
                Case "ListarInvestigadorGrupos"
                    Dim tipo As String = Request("tipo")
                    Dim codigo As Integer = 0
                    If Request("codbusq") <> "" Then
                        codigo = Request("codbusq")
                    End If
                    Dim ctf As String = Request("ctf")
                    ListarInvestigadorGrupos(tipo, codigo, Session("id_per"), ctf)
                Case "ConsultarAlumnosTesis"
                    Dim texto As String = Request("texto")
                    ConsultarAlumnosTesis(texto)
                Case "ListaRolInvestigador"
                    Dim tipo As String = Request("tipo")
                    ConsultarRolInvestigador(tipo)
                Case "lProvincia"
                    Dim tipo As String = "ES"
                    Dim codigo As Integer = Request("codRegion")
                    ListarProvincias(tipo, codigo)
                Case "lDistrito"
                    Dim tipo As String = "ES"
                    Dim codigo As Integer = Request("codProvincia")
                    ListarDistritos(tipo, codigo)
                Case "Download"
                    DescargarArchivo()
                Case "ldatosDocInv"
                    ConsultarDatosDocenteInvestigador(Session("id_per"))
                Case "lPersxDep"
                    Dim codigo_dac As String = Request("cod_dac")
                    ConsultarPersonalxDepAcademico(codigo_dac)
                Case "ListarBaseDatosRevista"
                    ListarBaseDatosRevista()
            End Select

        Catch ex As Exception
            Data.Add("idper", Session("id_per"))
            Data.Add("rpta", ex.Message & "0 - LOAD")
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try

    End Sub

    'Hcano 18-06-18
    Private Sub enviarEmailNotificacionEvaluadorExterno()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim codigo_con As Integer = obj1.DecrytedString64(Request("param1"))
        Dim codigo_pos As String = Request("param2")

        Dim tb As New Data.DataTable
        Dim tb1 As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_envioNotificacionCorreoEvaluadorExterno", codigo_con, codigo_pos)
        obj.CerrarConexion()

        Dim objemail As New ClsMail
        Dim mensaje, receptor, AsuntoCorreo, eveCambiarCorreo As String
        Dim contador As Integer
        eveCambiarCorreo = ""
        contador = 0
        AsuntoCorreo = "[USAT]Recordatorio de Postulaciones por revisar - Fondos Concursables"
        If (tb.Rows.Count > 0) Then


            For i As Integer = 0 To tb.Rows.Count - 1
                mensaje = ""
                mensaje = mensaje + "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' />"
                mensaje = mensaje + "<title>Recordatorio de Postulaciones por Revisar</title>"
                mensaje = mensaje + "<style type='text/css'>.usat { font-family:Calibri;color:#F1132A;font-size:25px;font-weight: bold;} "
                mensaje = mensaje + ".bolsa{color:#F1132A;font-family:Calibri;font-size: 13px;font-weight: 500;}</style></head>"
                mensaje = mensaje + "<body>"
                mensaje = mensaje + "<div style='text-align:center;width:100%'>"
                mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
                mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:center;'><img src='https://intranet.usat.edu.pe/campusestudiante/assets/images/logousat.png' width='100' height='100' ></div>"
                mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:center;'><div class='usat'>SISTEMA DE GESTIÓN DE INVESTIGACIÓN</div></div></td></tr></table>"
                mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr>"
                mensaje = mensaje + "<td style = 'background:none;border-bottom:1px solid #F1132A;height:1px;width:50%;margin:0px 0px 0px 0px' > &nbsp;</td></tr></table><br />"
                mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
                mensaje = mensaje + "<div style='text-align:center;color:gray;font-family:Calibri'>Estimado Evaluador <b> " + tb.Rows(i).Item("nombre_eve").ToString() + " </b></div>"
                mensaje = mensaje + "<div style='margin-top:10px;text-align:center;color:gray;font-family:Calibri '>Se le recuerda que según el cronograma de evaluación del Concurso de Proyectos de Investigación de Docentes USAT 2018, el día " + tb.Rows(i).Item("fecha_fineva").ToString() + " se vence el plazo máximo para la entrega de rúbricas de evaluación, por favor ingresar al siguiente link</b></div></br>"

                'serverdev
                'mensaje = mensaje + "<div style='text-align:center'><a href= 'http://serverdev/campusvirtual/librerianet/EvaluadorExternoGI/FrmEvaluacionEvaluadorExterno.aspx?EVE=" + obj1.EncrytedString64(tb.Rows(i).Item("codigo_eve").ToString()) + "&CON=" + obj1.EncrytedString64(tb.Rows(i).Item("codigo_con").ToString()) + "' style='background-color:#F1132A;color:white;border-radius:10px;font-size:15px;padding:10px 10px;width:200px;height:20px;text-decoration:none;font-weight:200px;display:block;margin:0 auto;text-align:center' target='_blank'>Revisión de Postulaciones</a></div>"
                'serverQA
                'mensaje = mensaje + "<div style='text-align:center'><a href= 'http://serverQA/campusvirtual/librerianet/EvaluadorExternoGI/FrmEvaluacionEvaluadorExterno.aspx?EVE=" + obj1.EncrytedString64(tb.Rows(i).Item("codigo_eve").ToString()) + "&CON=" + obj1.EncrytedString64(tb.Rows(i).Item("codigo_con").ToString()) + "' style='background-color:#F1132A;color:white;border-radius:10px;font-size:15px;padding:10px 10px;width:200px;height:20px;text-decoration:none;font-weight:200px;display:block;margin:0 auto;text-align:center' target='_blank'>Revisión de Postulaciones</a></div>"
                'producción
                mensaje = mensaje + "<div style='text-align:center'><a href= '//intranet.usat.edu.pe/campusestudiante/librerianet/EvaluadorExternoGI/FrmEvaluacionEvaluadorExterno.aspx?EVE=" + obj1.EncrytedString64(tb.Rows(i).Item("codigo_eve").ToString()) + "&CON=" + obj1.EncrytedString64(tb.Rows(i).Item("codigo_con").ToString()) + "' style='background-color:#F1132A;color:white;border-radius:10px;font-size:15px;padding:10px 10px;width:200px;height:20px;text-decoration:none;font-weight:200px;display:block;margin:0 auto;text-align:center' target='_blank'>Revisión de Postulaciones</a></div>"

                mensaje = mensaje + "<div style='margin-top:5px;text-align:left;color:gray;font-family:Calibri'></div></td></tr></table>"
                mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
                mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:center;color:white'>"
                mensaje = mensaje + "</div></td></tr></table>"
                mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'>"
                mensaje = mensaje + "<tr><td style='background:none;border-bottom:1px solid #F1132A;height:1px;width:50%;margin:0px 0px 0px 0px' > &nbsp;</td></tr></table><br />"
                mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
                mensaje = mensaje + "<div style='margin:0 auto;text-align:center;color:gray;font-family:Calibri '><b>VICERRECTORADO INVESTIGACIÓN USAT</b></div><br /></td></tr></table>"
                mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
                mensaje = mensaje + "<div style='text-align:center;font-size:11px;color:gray;font-family:Calibri '><div>Av. San Josemaría Escrivá de Balaguer Nº 855 Chiclayo - Perú | Teléfono: 606200 - anexo: 1291"
                mensaje = mensaje + "<a href='mailto:vri@usat.edu.pe' style='color:gray;text-decoration:none;' target='_blank'><br/><b>vri@usat.edu.pe</b></a></div> "
                mensaje = mensaje + "<div style='font-family:Calibri'>© Copyright 2018: USAT - Todos los derechos reservados</div>"
                mensaje = mensaje + "</td></tr></table></div></body></html>"

                receptor = tb.Rows(i).Item("email_eve").ToString() & ",vcastro@usat.edu.pe,cgamarra@usat.edu.pe"
                If ConfigurationManager.AppSettings("CorreoUsatActivo") = 0 Then
                    receptor = "hcano@usat.edu.pe"
                End If

                objemail.EnviarMail("campusvirtual@usat.edu.pe", "GESTIÓN DE INVESTIGACIÓN", receptor, AsuntoCorreo, mensaje, True)

                If contador = 1 Then
                    eveCambiarCorreo = eveCambiarCorreo & ","
                End If
                eveCambiarCorreo = eveCambiarCorreo & tb.Rows(i).Item("codigo_eve").ToString()
                contador = 1

            Next

            'Dim list As New List(Of Dictionary(Of String, Object))()
            'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            obj.AbrirConexion()
            tb1 = obj.TraerDataTable("INV_actualizarEnvioCorreoEvaluadorExterno", codigo_pos, eveCambiarCorreo)
            obj.CerrarConexion()

            'Dim list As New List(Of Dictionary(Of String, Object))()
            'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            For i As Integer = 0 To tb1.Rows.Count - 1
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("Status", tb1.Rows(i).Item("Status"))
                Data.Add("Message", tb1.Rows(i).Item("Message"))
                Data.Add("Code", tb1.Rows(i).Item("Code"))
                list.Add(Data)
            Next

        Else
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", "warning")
            Data.Add("Message", "No se encuentran correos por enviar")
            Data.Add("Code", "0")
            list.Add(Data)
        End If

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub actualizarEliminaInvGrupoInvestigador()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim codigo_inv As Integer = Request("param1")
        Dim codigo_gru As Integer = Request("param2")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_eliminaInvestigadorGrupoInvestigacion", codigo_inv, codigo_gru)
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", tb.Rows(i).Item("Status"))
            Data.Add("Message", tb.Rows(i).Item("Message"))
            Data.Add("Code", tb.Rows(i).Item("Code"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub ListaInvestigadorTesistaGraduado()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_InvestigadorTesistaGraduado")
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("d_tip", tb.Rows(i).Item("tipo"))
            Data.Add("c_inv", tb.Rows(i).Item("codigopso"))
            Data.Add("c_per", tb.Rows(i).Item("codigo"))
            Data.Add("d_per", tb.Rows(i).Item("nombre"))
            Data.Add("c_dni", tb.Rows(i).Item("dni"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub SubirPropuestaGrupoNew()
        Try
            Dim post As HttpPostedFile = HttpContext.Current.Request.Files("ArchivoASubir")
            Dim codigo As String = Request("codigo")
            Dim NroRend As String = Request("codigo")

            Dim Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim Usuario As String = Session("perlogin")
            Dim Input(post.ContentLength) As Byte
            ' Dim b As New BinaryReader(post.InputStream)
            '  Dim by() As Byte = b.ReadByte(post.ContentLength)

            Dim b As New BinaryReader(post.InputStream)
            Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)
            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)
            '  Dim list As New List(Of Dictionary(Of String, String))()
            list.Add("Fecha", Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
            list.Add("Nombre", System.IO.Path.GetFileName(post.FileName))
            list.Add("TransaccionId", codigo)
            list.Add("TablaId", "9")
            list.Add("NroOperacion", "1") '2 Nro de operación para grupo
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", Usuario)
            Dim envelope As String = wsCloud.SoapEnvelope(list)

            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", Usuario)

            Dim obj1 As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj1.AbrirConexion()
            tb = obj1.TraerDataTable("INV_ActualizarIDArchivoCompartido", 9, codigo, 1) '2 Nro de Grupo de Investigacion
            obj1.CerrarConexion()

            'Response.Write(result)
        Catch ex As Exception
            Dim Data1 As New Dictionary(Of String, Object)()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim list As New List(Of Dictionary(Of String, Object))()
            Data1.Add("rpta", "1 - LIST")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try

    End Sub
    Sub SubirPropuestaGrupo()
        Dim obj As New ClsGestionInvestigacion
        Dim obj1 As New ClsConectarDatos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim linea_error As String = ""
        Dim tb As New Data.DataTable
        Try
            Dim ArchivoSubir As HttpPostedFile = HttpContext.Current.Request.Files("ArchivoASubir")
            Dim codigo As Integer = Request("codigo")
            Dim tipo As String = Request("tipo")

            '1.nombre de archivo
            Dim nomArchivo As String = System.IO.Path.GetFileName(ArchivoSubir.FileName).Substring(0, System.IO.Path.GetFileName(ArchivoSubir.FileName).IndexOf(System.IO.Path.GetExtension(ArchivoSubir.FileName).ToString))
            '2.ruta a guardar
            '2.1.verificamos si hay una carpeta para el codigo del concurso,sino se crea
            Dim strRutaArchivo As String
            strRutaArchivo = Server.MapPath("../../GestionInvestigacion/Archivos/Grupos/" & codigo)
            linea_error = "1 - " + strRutaArchivo
            If Directory.Exists(strRutaArchivo) Then
            Else
                Directory.CreateDirectory(strRutaArchivo)
            End If
            '3.Nombre Final
            nomArchivo = nomArchivo & System.IO.Path.GetExtension(ArchivoSubir.FileName)
            linea_error = "4 - " + strRutaArchivo
            '4.Guardamos Archivo 
            linea_error = "5.1 - " + strRutaArchivo & "/" & nomArchivo
            ArchivoSubir.SaveAs(strRutaArchivo & "/" & nomArchivo)

            Dim cn As New clsaccesodatos
            obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj1.AbrirConexion()
            tb = obj1.TraerDataTable("INV_ActualizarArchivosGrupoInvestigador", codigo, nomArchivo, tipo)
            obj1.CerrarConexion()

            'Response.Write(codigo & "-" & nomArchivo & "-" & tipo)

            linea_error = "5 - " + strRutaArchivo

        Catch ex As Exception
            Data.Add("msje", ex.Message)
            Data.Add("alert", "error" + linea_error)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub
    Private Sub validaInvestigador()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim codigo_per As Integer = obj1.DecrytedString64(Request("param1"))
        Dim tipo As String = Request("param2")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_listaInvestigadores", codigo_per, tipo, 0)
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("c_inv", tb.Rows(i).Item("c_inv"))
            Data.Add("c_per", tb.Rows(i).Item("c_per"))
            Data.Add("d_per", tb.Rows(i).Item("d_per"))
            Data.Add("dni_per", tb.Rows(i).Item("dni_per"))
            Data.Add("d_url", tb.Rows(i).Item("d_url"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Private Sub enviarEmailEvaluadorExterno()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim codigo_con As Integer = obj1.DecrytedString64(Request("param1"))
        Dim codigo_pos As String = Request("param2")

        Dim tb As New Data.DataTable
        Dim tb1 As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_envioCorreoEvaluadorExterno", codigo_con, codigo_pos)
        obj.CerrarConexion()

        Dim objemail As New ClsMail
        Dim mensaje, receptor, AsuntoCorreo, eveCambiarCorreo As String
        Dim contador As Integer
        eveCambiarCorreo = ""
        contador = 0
        AsuntoCorreo = "[USAT]Postulaciones por revisar - Fondos Concursables"

        For i As Integer = 0 To tb.Rows.Count - 1
            mensaje = ""
            mensaje = mensaje + "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' />"
            mensaje = mensaje + "<title>Confirmac&íacute;on de Referencia Laboral</title>"
            mensaje = mensaje + "<style type='text/css'>.usat { font-family:Calibri;color:#F1132A;font-size:25px;font-weight: bold;} "
            mensaje = mensaje + ".bolsa{color:#F1132A;font-family:Calibri;font-size: 13px;font-weight: 500;}</style></head>"
            mensaje = mensaje + "<body>"
            mensaje = mensaje + "<div style='text-align:center;width:100%'>"
            mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
            mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:center;'><img src='https://intranet.usat.edu.pe/campusestudiante/assets/images/logousat.png' width='100' height='100' ></div>"
            mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:center;'><div class='usat'>SISTEMA DE GESTIÓN DE INVESTIGACIÓN</div></div></td></tr></table>"
            mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr>"
            mensaje = mensaje + "<td style = 'background:none;border-bottom:1px solid #F1132A;height:1px;width:50%;margin:0px 0px 0px 0px' > &nbsp;</td></tr></table><br />"
            mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
            mensaje = mensaje + "<div style='text-align:center;color:gray;font-family:Calibri'><b> " + tb.Rows(i).Item("nombre_eve").ToString() + " </b></div>"
            mensaje = mensaje + "<div style='margin-top:10px;text-align:center;color:gray;font-family:Calibri '>Se han agregado Postulaciones por revisar, por favor ingresar al siguiente link</b></div></br>"

            'mensaje = mensaje + "<a href= 'http://localhost/campusvirtual/CampusVirtualEstudiante/CampusVirtualEstudiante/confirmacionLaboral.aspx?tipo=" + idLab + "' style='background-color:#F1132A;color:white;border-radius:10px;font-size:15px;padding:10px 10px;width:200px;height:20px;text-decoration:none;font-weight:200px;display:block;margin:0 auto;text-align:center' target='_blank'>Confirma Referencia</a>"
            'mensaje = mensaje + "<a href= 'http://localhost/campusvirtual/CampusVirtualEstudiante/CampusVirtualEstudiante/confirmacionLaboral.aspx?tipo=" + idLab + "' style='background-color:#F1132A;color:white;border-radius:10px;font-size:15px;padding:10px 10px;width:200px;height:20px;text-decoration:none;font-weight:200px;display:block;margin:0 auto;text-align:center' target='_blank'>Confirma Referencia</a>"

            'mensaje = mensaje + "<a href= 'http://localhost/campusvirtual/personal/GestionInvestigacion/FrmEvaluacionEvaluadorExterno.aspx?EVE=" + obj1.EncrytedString64(tb.Rows(i).Item("codigo_eve").ToString()) + "&CON=" + obj1.EncrytedString64(tb.Rows(i).Item("codigo_con").ToString()) + "' style='background-color:#F1132A;color:white;border-radius:10px;font-size:15px;padding:10px 10px;width:200px;height:20px;text-decoration:none;font-weight:200px;display:block;margin:0 auto;text-align:center' target='_blank'>Revisión de Postulaciones</a>"
            'mensaje = mensaje + "<a href= '../../../personal/GestionInvestigacion/FrmEvaluacionEvaluadorExterno.aspx?EVE=" + obj1.EncrytedString64(tb.Rows(i).Item("codigo_eve").ToString()) + "&CON=" + obj1.EncrytedString64(tb.Rows(i).Item("codigo_con").ToString()) + "' style='background-color:#F1132A;color:white;border-radius:10px;font-size:15px;padding:10px 10px;width:200px;height:20px;text-decoration:none;font-weight:200px;display:block;margin:0 auto;text-align:center' target='_blank'>Revisión de Postulaciones</a>"
            mensaje = mensaje + "<a href= '//intranet.usat.edu.pe/campusestudiante/librerianet/EvaluadorExternoGI/FrmEvaluacionEvaluadorExterno.aspx?EVE=" + obj1.EncrytedString64(tb.Rows(i).Item("codigo_eve").ToString()) + "&CON=" + obj1.EncrytedString64(tb.Rows(i).Item("codigo_con").ToString()) + "' style='background-color:#F1132A;color:white;border-radius:10px;font-size:15px;padding:10px 10px;width:200px;height:20px;text-decoration:none;font-weight:200px;display:block;margin:0 auto;text-align:center' target='_blank'>Revisión de Postulaciones</a>"

            mensaje = mensaje + "<div style='margin-top:5px;text-align:left;color:gray;font-family:Calibri'></div></td></tr></table>"
            mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
            mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:center;color:white'>"
            mensaje = mensaje + "</div></td></tr></table>"
            mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'>"
            mensaje = mensaje + "<tr><td style='background:none;border-bottom:1px solid #F1132A;height:1px;width:50%;margin:0px 0px 0px 0px' > &nbsp;</td></tr></table><br />"
            mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
            mensaje = mensaje + "<div style='margin:0 auto;text-align:center;color:gray;font-family:Calibri '><b>VICERRECTORADO INVESTIGACIÓN USAT</b></div><br /></td></tr></table>"
            mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
            mensaje = mensaje + "<div style='text-align:center;font-size:11px;color:gray;font-family:Calibri '><div>Av. San Josemaría Escrivá de Balaguer Nº 855 Chiclayo - Perú | Teléfono: 606200 - anexo: 1291"
            mensaje = mensaje + "<a href='mailto:vri@usat.edu.pe' style='color:gray;text-decoration:none;' target='_blank'><br/><b>vri@usat.edu.pe</b></a></div> "
            mensaje = mensaje + "<div style='font-family:Calibri'>© Copyright 2018: USAT - Todos los derechos reservados</div>"
            mensaje = mensaje + "</td></tr></table></div></body></html>"

            'receptor = "yperez@usat.edu.pe"
            receptor = tb.Rows(i).Item("email_eve").ToString()
            If ConfigurationManager.AppSettings("CorreoUsatActivo") = 0 Then
                receptor = "hcano@usat.edu.pe"
            End If

            objemail.EnviarMail("campusvirtual@usat.edu.pe", "GESTIÓN DE INVESTIGACIÓN", receptor, AsuntoCorreo, mensaje, True)


            If contador = 1 Then
                eveCambiarCorreo = eveCambiarCorreo & ","
            End If
            eveCambiarCorreo = eveCambiarCorreo & tb.Rows(i).Item("codigo_eve").ToString()
            contador = 1

        Next

        obj.AbrirConexion()
        tb1 = obj.TraerDataTable("INV_actualizarEnvioCorreoEvaluadorExterno", codigo_pos, eveCambiarCorreo)
        obj.CerrarConexion()

        'Dim list As New List(Of Dictionary(Of String, Object))()
        'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        For i As Integer = 0 To tb1.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", tb1.Rows(i).Item("Status"))
            Data.Add("Message", tb1.Rows(i).Item("Message"))
            Data.Add("Code", tb1.Rows(i).Item("Code"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Private Sub GanadorConcursoPostulaciones()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim codigo_pos As Integer = Request("param1")
        Dim codigo_con As Integer = Request("param2")
        Dim codigo_use As Integer = obj1.DecrytedString64(Request("param3"))

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_ganadorConcursoPostulaciones", codigo_con, codigo_pos, codigo_use)
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", tb.Rows(i).Item("Status"))
            Data.Add("Message", tb.Rows(i).Item("Message"))
            Data.Add("Code", tb.Rows(i).Item("Code"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Private Sub AsignaEvaluadorRF()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim codigo_pos As Integer = Request("param1")
        Dim codigo_eva As Integer = obj1.DecrytedString64(Request("param2"))
        Dim codigo_use As Integer = Request("param3")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_AsignarEvaluadorExterno", codigo_pos, codigo_eva, codigo_use)
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", tb.Rows(i).Item("Respuesta"))
            Data.Add("Message", tb.Rows(i).Item("Mensaje"))
            Data.Add("Code", tb.Rows(i).Item("cod"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Private Sub ListarEvaluadoresRF()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim codigo_pos As Integer = Request("param1")

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Try
            Dim dt As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("INV_ListarEvaluadoresPostulacion", codigo_pos)
            obj.CerrarConexion()
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj1.EncrytedString64(dt.Rows(i).Item("codigo")))
                    data.Add("nombre", dt.Rows(i).Item("nombre"))
                    data.Add("dina", dt.Rows(i).Item("dina"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "1 - LIST")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub
    Sub ActualizarEvaluacionFinal()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim codigo_con As Integer = Request("param1")
        Dim codigo_pos As Integer = Request("param2")
        Dim calificacion_global As Integer = Request("param3")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_actualizarCalificacionglobalPostulacion", codigo_con, codigo_pos, calificacion_global)
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", tb.Rows(i).Item("Status"))
            Data.Add("Message", tb.Rows(i).Item("Message"))
            Data.Add("Code", tb.Rows(i).Item("Code"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub ListaDetallePostulacionesFinal()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim Codigo_pos As Integer = Request("param1")
        Dim tb As New Data.DataTable
        tb = Session("lstPostFinal")

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            If (tb.Rows(i).Item("codigo_pos") = Codigo_pos) Then
                Data.Add("codigo_eve", tb.Rows(i).Item("codigo_eve"))
                Data.Add("nombre_eve", tb.Rows(i).Item("nombre_eve"))
                Data.Add("calif_eva", tb.Rows(i).Item("calificacion"))
                Data.Add("rubri_eva", tb.Rows(i).Item("rubrica_eva"))
                list.Add(Data)
            End If
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub
    Sub ListaPostulacionesEvaluacionFinal()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim Codigo_con As Integer = Request("param1")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_listaResultadoFinalConcurso", Codigo_con)
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("ganador", tb.Rows(i).Item("ganador"))
            Data.Add("calif", tb.Rows(i).Item("calificacion"))
            Data.Add("cod_pos", tb.Rows(i).Item("codigo_pos"))
            Data.Add("cod_epo", tb.Rows(i).Item("codigo_epo"))
            Data.Add("cod_con", tb.Rows(i).Item("codigo_con"))
            Data.Add("cod_lin", tb.Rows(i).Item("codigo_lin"))
            Data.Add("cod_gru", tb.Rows(i).Item("codigo_gru"))
            Data.Add("cod_alu", tb.Rows(i).Item("codigo_alu"))
            Data.Add("tit_pos", tb.Rows(i).Item("titulo_pos"))
            Data.Add("cod_doc", tb.Rows(i).Item("codigo_doc"))
            Data.Add("res_pos", tb.Rows(i).Item("resumen_pos"))
            Data.Add("pal_pos", tb.Rows(i).Item("palabrasclave_pos"))
            Data.Add("jus_pos", tb.Rows(i).Item("justificacion_pos"))
            Data.Add("fini_pos", tb.Rows(i).Item("fechaini_pos"))
            Data.Add("ffin_pos", tb.Rows(i).Item("fechafin_pos"))
            Data.Add("pres_pos", tb.Rows(i).Item("Presupuesto_pos"))
            Data.Add("prod_pos", tb.Rows(i).Item("Producto_pos"))
            Data.Add("dj_pos", tb.Rows(i).Item("declaracionjurada_pos"))
            Data.Add("califg_pos", tb.Rows(i).Item("calificacionglobal_pos"))
            Data.Add("cod_eta", tb.Rows(i).Item("codigo_eta"))
            Data.Add("est_pos", tb.Rows(i).Item("estado_pos"))
            Data.Add("cod_eve", tb.Rows(i).Item("codigo_eve"))
            Data.Add("calif_eva", tb.Rows(i).Item("calificacion_eva"))
            Data.Add("rub_eva", tb.Rows(i).Item("rubrica_eva"))
            Data.Add("pais_eve", tb.Rows(i).Item("pais_eve"))
            Data.Add("nomb_eve", tb.Rows(i).Item("nombre_eve"))
            Data.Add("email_eve", tb.Rows(i).Item("email_eve"))
            Data.Add("tlf_eve", tb.Rows(i).Item("nrotelefono_eve"))
            Data.Add("dina_eve", tb.Rows(i).Item("dina_eve"))
            Data.Add("regina_eve", tb.Rows(i).Item("regina_eve"))
            Data.Add("urldina_eve", tb.Rows(i).Item("urldina_eve"))
            Data.Add("cv_eve", tb.Rows(i).Item("cv_eve"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub ListaConcursosEvaluacionFinal()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim Codigo_id As Integer = obj1.DecrytedString64(Request("param1"))
        Dim Codigo_ctf As Integer = obj1.DecrytedString64(Request("param2"))
        Dim tipo As String = Request("param3")
        Dim estado As String = Request("param4")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_ListarConcurso", tipo, 0, "%", estado, "%", Codigo_id, Codigo_ctf)
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("cod_con", tb.Rows(i).Item("codigo_con"))
            Data.Add("cod_con1", obj1.EncrytedString64(tb.Rows(i).Item("codigo_con")))
            Data.Add("tit_con", tb.Rows(i).Item("titulo_con"))
            Data.Add("fini_con", tb.Rows(i).Item("fechaini_con"))
            Data.Add("ffin_con", tb.Rows(i).Item("fechafin_con"))
            Data.Add("tip_con", tb.Rows(i).Item("tipo_con"))
            Data.Add("bas_con", tb.Rows(i).Item("bases_con"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub SubirRubricaNew()
        Try
            Dim post As HttpPostedFile = HttpContext.Current.Request.Files("ArchivoASubir")
            Dim codigo As String = Request("codigo")
            Dim NroRend As String = 10 '10 Nro de operación para evaluadores externos

            Dim Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim Usuario As String = Session("perlogin")
            Dim Input(post.ContentLength) As Byte
            ' Dim b As New BinaryReader(post.InputStream)
            '  Dim by() As Byte = b.ReadByte(post.ContentLength)

            Dim b As New BinaryReader(post.InputStream)
            Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)
            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)
            '  Dim list As New List(Of Dictionary(Of String, String))()
            list.Add("Fecha", Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
            list.Add("Nombre", System.IO.Path.GetFileName(post.FileName))
            list.Add("TransaccionId", codigo)
            list.Add("TablaId", "9")
            list.Add("NroOperacion", NroRend)
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", Usuario)
            Dim envelope As String = wsCloud.SoapEnvelope(list)

            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", Usuario)

            Dim obj1 As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj1.AbrirConexion()
            tb = obj1.TraerDataTable("INV_ActualizarIDArchivoCompartido", 9, codigo, NroRend)
            obj1.CerrarConexion()

            'Response.Write(result)
        Catch ex As Exception
            Dim Data1 As New Dictionary(Of String, Object)()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim list As New List(Of Dictionary(Of String, Object))()
            Data1.Add("rpta", "1 - LIST")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try

    End Sub
    Sub SubirRubrica()
        Dim obj As New ClsGestionInvestigacion
        Dim obj1 As New ClsConectarDatos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim linea_error As String = ""
        Dim tb As New Data.DataTable
        Try
            Dim ArchivoSubir As HttpPostedFile = HttpContext.Current.Request.Files("ArchivoASubir")
            Dim codigo As Integer = Request("codigo")
            Dim tipo As String = Request("tipo")

            '1.nombre de archivo
            Dim nomArchivo As String = System.IO.Path.GetFileName(ArchivoSubir.FileName).Substring(0, System.IO.Path.GetFileName(ArchivoSubir.FileName).IndexOf(System.IO.Path.GetExtension(ArchivoSubir.FileName).ToString))
            '2.ruta a guardar
            '2.1.verificamos si hay una carpeta para el codigo del concurso,sino se crea
            Dim strRutaArchivo As String
            strRutaArchivo = Server.MapPath("../../GestionInvestigacion/Archivos/Rubricas/" & codigo)
            linea_error = "1 - " + strRutaArchivo
            If Directory.Exists(strRutaArchivo) Then
            Else
                Directory.CreateDirectory(strRutaArchivo)
            End If
            '3.Nombre Final
            nomArchivo = nomArchivo & System.IO.Path.GetExtension(ArchivoSubir.FileName)
            linea_error = "4 - " + strRutaArchivo
            '4.Guardamos Archivo
            linea_error = "5.1 - " + strRutaArchivo & "/" & nomArchivo
            ArchivoSubir.SaveAs(strRutaArchivo & "/" & nomArchivo)

            linea_error = "5 - " + strRutaArchivo

        Catch ex As Exception
            Data.Add("msje", ex.Message)
            Data.Add("alert", "error" + linea_error)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Sub ActualizarEvaluarPostulacionExterno()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim Calificacion_pos As Integer = Request("param1")
        Dim Codigo_pos As Integer = Request("param2")
        Dim Codigo_eve As Integer = obj1.DecrytedString64(Request("param3"))
        Dim Rubrica_pos As String = Request("param4")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_actualizarCalificacionRubricaPostulacion", Codigo_pos, Codigo_eve, Calificacion_pos, Rubrica_pos)
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", tb.Rows(i).Item("Status"))
            Data.Add("Message", tb.Rows(i).Item("Message"))
            Data.Add("Code", tb.Rows(i).Item("Code"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub ListaObjetivosPos()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim Codigo_pos As Integer = Request("param1")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_ListarObjetivosPostulacion", Codigo_pos)
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("cod", obj1.EncrytedString64(tb.Rows(i).Item("codigo")))
            Data.Add("des", tb.Rows(i).Item("descripcion"))
            Data.Add("codtipo", tb.Rows(i).Item("tipo_opo"))
            Data.Add("tipo", tb.Rows(i).Item("TIPO"))
            Data.Add("estado", tb.Rows(i).Item("estado_opo"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub ListaPostulacionesXCodigo()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim Codigo_pos As Integer = Request("param1")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        tb = obj.TraerDataTable("INV_InformacionPostulacion", Codigo_pos)
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("codigo", tb.Rows(i).Item("codigo"))
            Data.Add("resumen", tb.Rows(i).Item("resumen"))
            Data.Add("palabras", tb.Rows(i).Item("palabras"))
            Data.Add("justificacion", tb.Rows(i).Item("justificacion"))
            Data.Add("presupuesto", tb.Rows(i).Item("presupuesto"))
            Data.Add("cronograma", tb.Rows(i).Item("cronograma"))
            Data.Add("propuesta", tb.Rows(i).Item("propuesta"))
            Data.Add("resultado", tb.Rows(i).Item("resultado"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub ListarPostulacionesPorEvaluar()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim Codigo_eve As Integer = obj1.DecrytedString64(Request("param1"))
        Dim Codigo_con As Integer = obj1.DecrytedString64(Request("param2"))

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        tb = obj.TraerDataTable("INV_ListarPostulacioneEvaluador", Codigo_eve, Codigo_con)
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("cod_pos", tb.Rows(i).Item("codigo_pos"))
            Data.Add("cod_con", tb.Rows(i).Item("codigo_con"))
            Data.Add("tit_pos", tb.Rows(i).Item("titulo_pos"))
            Data.Add("calglobal_pos", tb.Rows(i).Item("calificacionglobal_pos"))
            Data.Add("prd_pos", tb.Rows(i).Item("Producto_pos"))
            Data.Add("cod_epo", tb.Rows(i).Item("codigo_epo"))
            Data.Add("cod_eve", tb.Rows(i).Item("codigo_eve"))
            Data.Add("cal_eva", tb.Rows(i).Item("calificacion_eva"))
            Data.Add("rub_eva", tb.Rows(i).Item("rubrica_eva"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub EstadoInvestigadorExterno()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim Codigo As Integer = obj1.DecrytedString64(Request("param1"))
        Dim Ruta As String = Request("param2")
        Dim Tipo As String = Request("param3")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        tb = obj.TraerDataTable("INV_ActualizarArchivosEvaluador", Codigo, Ruta, Tipo)
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", tb.Rows(i).Item("Status"))
            Data.Add("Message", tb.Rows(i).Item("Message"))
            Data.Add("Code", tb.Rows(i).Item("Code"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub
    Sub ListaEvaluadoresExternos()
        Dim JSONresult As String = ""
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion
        Try


            Dim tipo As String = Request("tipo")
            Dim parametro As String = ""
            If Request("parametro") <> "" Then
                If Request("parametro") <> "%" Then
                    parametro = obj1.DecrytedString64(Request("parametro"))
                Else
                    parametro = "%"
                End If
            End If
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("INV_listarEvaluadoresExternos", tipo, parametro)
            obj.CerrarConexion()

            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim sele As String = "0"
            For i As Integer = 0 To tb.Rows.Count - 1
                Dim dict As New Dictionary(Of String, Object)()
                dict.Add("reset", False)
                dict.Add("cod_eve", obj1.EncrytedString64(tb.Rows(i).Item("codigo_eve").ToString()))
                dict.Add("pai_eve", tb.Rows(i).Item("pais_eve").ToString())
                dict.Add("nom_eve", tb.Rows(i).Item("nombre_eve").ToString())
                dict.Add("ema_eve", tb.Rows(i).Item("email_eve").ToString())
                dict.Add("tlf_eve", tb.Rows(i).Item("nrotelefono_eve").ToString())
                dict.Add("din_eve", tb.Rows(i).Item("dina_eve").ToString())
                dict.Add("reg_eve", tb.Rows(i).Item("regina_eve").ToString())
                dict.Add("url_eve", tb.Rows(i).Item("urldina_eve").ToString())
                dict.Add("cv_eve", tb.Rows(i).Item("cv_eve").ToString())
                dict.Add("est_eve", tb.Rows(i).Item("estado_eve").ToString())
                dict.Add("nom_pai", tb.Rows(i).Item("nombre_pai").ToString())
                dict.Add("linea", obj1.EncrytedString64(tb.Rows(i).Item("codigo_Lin").ToString()))
                dict.Add("cod_dis", tb.Rows(i).Item("codigo_dis_ocde").ToString())
                dict.Add("cod_sub", tb.Rows(i).Item("codigo_sa_ocde").ToString())
                dict.Add("cod_area", tb.Rows(i).Item("codigo_ocde").ToString())
                list.Add(dict)
            Next

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("msje", ex.Message)
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub
    Sub SurbirCVNew()
        Dim obj As New ClsGestionInvestigacion
        Dim Data As New Dictionary(Of String, Object)()
        Try
            Dim post As HttpPostedFile = HttpContext.Current.Request.Files("ArchivoASubir")
            Dim codigo As String = Request("codigo")
            Dim NroRend As String = Request("codigo")

            Dim Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim Usuario As String = Session("perlogin")
            Dim Input(post.ContentLength) As Byte
            ' Dim b As New BinaryReader(post.InputStream)
            '  Dim by() As Byte = b.ReadByte(post.ContentLength)

            Dim b As New BinaryReader(post.InputStream)
            Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)
            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)
            '  Dim list As New List(Of Dictionary(Of String, String))()
            list.Add("Fecha", Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
            list.Add("Nombre", System.IO.Path.GetFileName(post.FileName))
            list.Add("TransaccionId", codigo)
            list.Add("TablaId", "9")
            list.Add("NroOperacion", "9") '2 Nro de operación para grupo
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", Usuario)
            Dim envelope As String = wsCloud.SoapEnvelope(list)

            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", Usuario)

            Dim obj1 As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj1.AbrirConexion()
            tb = obj1.TraerDataTable("INV_ActualizarIDArchivoCompartido", 9, codigo, 9) '2 Nro de Grupo de Investigacion
            obj1.CerrarConexion()

            'Response.Write(result)
        Catch ex As Exception
            Dim Data1 As New Dictionary(Of String, Object)()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim list As New List(Of Dictionary(Of String, Object))()
            Data1.Add("rpta", "1 - LIST")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try

        'linea_error = "5 - " + strRutaArchivo

    End Sub
    Sub SurbirCV()
        Dim obj As New ClsGestionInvestigacion
        Dim obj1 As New ClsConectarDatos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim linea_error As String = ""
        Dim tb As New Data.DataTable
        Try
            Dim ArchivoSubir As HttpPostedFile = HttpContext.Current.Request.Files("ArchivoASubir")
            Dim codigo As Integer = Request("codigo")
            Dim tipo As String = Request("tipo")

            '1.nombre de archivo
            Dim nomArchivo As String = System.IO.Path.GetFileName(ArchivoSubir.FileName).Substring(0, System.IO.Path.GetFileName(ArchivoSubir.FileName).IndexOf(System.IO.Path.GetExtension(ArchivoSubir.FileName).ToString)) + Now.ToString("yyyyMMddHmmss")
            '2.ruta a guardar
            '2.1.verificamos si hay una carpeta para el codigo del concurso,sino se crea
            Dim strRutaArchivo As String
            strRutaArchivo = Server.MapPath("../../GestionInvestigacion/Archivos/CV/" & codigo)
            linea_error = "1 - " + strRutaArchivo
            If Directory.Exists(strRutaArchivo) Then
            Else
                Directory.CreateDirectory(strRutaArchivo)
            End If
            '3.Nombre Final
            nomArchivo = nomArchivo & System.IO.Path.GetExtension(ArchivoSubir.FileName)
            linea_error = "4 - " + strRutaArchivo
            '4.Guardamos Archivo
            linea_error = "5.1 - " + strRutaArchivo & "/" & nomArchivo
            ArchivoSubir.SaveAs(strRutaArchivo & "/" & nomArchivo)

            Dim archivoBD As String = ""
            archivoBD = "/" & tipo & "/" & codigo & "/" & nomArchivo

            obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj1.AbrirConexion()

            tb = obj1.TraerDataTable("INV_ActualizarArchivosEvaluador", codigo, archivoBD, tipo)
            obj1.CerrarConexion()

            linea_error = "5 - " + strRutaArchivo

        Catch ex As Exception
            Data.Add("msje", ex.Message)
            Data.Add("alert", "error" + linea_error)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Sub RegistrarEvaluadorExterno()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim Accion As String = Request("hdAccion")
        Dim Evaluador As String = Request("txtEvaluador").ToString.ToUpper
        Dim Email As String = Request("txtEmail")
        Dim NroTlf As String = Request("txtNroTlf")
        Dim URLDINA As String = Request("txtURLDINA")
        Dim Codigo_user As Integer = obj1.DecrytedString64(Request("hdUser"))
        Dim Codigo As Integer
        If (Request("hdAccion") = "A") Then
            Codigo = obj1.DecrytedString64(Request("hdCod"))
        Else
            Codigo = Request("hdCod")
        End If
        Dim Pais As Integer = Request("cboPais")
        Dim chkRegina As Integer = Request("chkREGINA")
        Dim chkDina As Integer = Request("chkDINA")

        Dim codigo_lin As Integer = obj1.DecrytedString64(Request("cboLinea"))
        Dim codigo_dis As Integer = 0
        If Request("cboDisciplina") <> 0 Then
            codigo_dis = Request("cboDisciplina")
        End If


        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        tb = obj.TraerDataTable("INV_RegistrarEvaluadorExterno", Accion, Codigo, Pais, Evaluador, Email, NroTlf, chkDina, chkRegina, URLDINA, codigo_lin, codigo_dis, Codigo_user)
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", tb.Rows(i).Item("Status"))
            Data.Add("Message", tb.Rows(i).Item("Message"))
            Data.Add("Code", tb.Rows(i).Item("Code"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub ListaPais()
        Dim JSONresult As String = ""
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("ConsultarPais", "T", "")
        obj.CerrarConexion()

        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim sele As String = "0"
        For i As Integer = 0 To tb.Rows.Count
            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("reset", False)
            If (sele = "0") Then
                dict.Add("c_pai", 0)
                dict.Add("d_pai", "-- Seleccione --")
                dict.Add("d_sel", "selected")
                sele = "1"
            Else
                dict.Add("c_pai", tb.Rows(i - 1).Item("codigo_Pai").ToString())
                dict.Add("d_pai", tb.Rows(i - 1).Item("nombre_Pai").ToString())
                dict.Add("d_sel", "")
            End If
            list.Add(dict)
        Next
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub EnviarEmail()
        Dim JSONresult As String = ""
        Dim blnResultado As Boolean = False

        Dim obj1 As New ClsGestionInvestigacion
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable

        Dim objemail As New ClsMail
        Dim receptor, AsuntoCorreo, opcion As String
        Dim mensaje As String = ""

        Dim Codigo As Integer = Request("param1")
        Dim Tipo As String = Request("param2")
        Dim Estado As String = Request("param3")
        Dim Descripcion As String = Request("param4")

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_RecuperarDatosEnvioCorreo", Codigo, Tipo)
        obj.CerrarConexion()

        AsuntoCorreo = ""
        opcion = ""
        If (Tipo = "INV") Then
            opcion = " REGISTRO DE DOCENTES Y ADMINISTRATIVOS CON ACTIVIDAD INVESTIGADORA"
            AsuntoCorreo = Estado & " EN " & opcion
        Else
            If (Tipo = "GRU") Then
                opcion = " REGISTRO DE GRUPO DE INVESTIGACIÓN"
                AsuntoCorreo = Estado & " EN " & opcion
            Else
                If (Tipo = "BON") Then
                    opcion = " POSTULACIÓN AL BONO POR PUBLICACIÓN"
                    AsuntoCorreo = Estado & " EN " & opcion
                Else
                    If (Tipo = "POS") Then
                        opcion = "POSTULACIÓN A CONCURSO"
                        AsuntoCorreo = Estado & " EN " & opcion
                    End If
                End If
            End If
        End If

        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim nombre, denominacion, email, email2 As String
        denominacion = ""
        nombre = ""
        email = ""
        For i As Integer = 0 To tb.Rows.Count - 1
            nombre = tb.Rows(i).Item("nombre_per").ToString()
            denominacion = tb.Rows(i).Item("denominacion").ToString()
            email = tb.Rows(i).Item("email").ToString()
            email2 = tb.Rows(i).Item("email2").ToString()
        Next

        If (Estado = "GANADOR") Then
            Tipo = "GAN"
            AsuntoCorreo = Estado & " DEL CONCURSO "
        End If

        mensaje = mensaje + "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' />"
        mensaje = mensaje + "<title></title>"
        mensaje = mensaje + "<style type='text/css'>.usat { font-family:Calibri;color:#F1132A;font-size:25px;font-weight: bold;} "
        mensaje = mensaje + ".bolsa{color:#F1132A;font-family:Calibri;font-size: 13px;font-weight: 500;}</style></head>"
        mensaje = mensaje + "<body>"
        mensaje = mensaje + "<div style='text-align:center;width:100%'>"
        mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
        mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:center;'><img src='//intranet.usat.edu.pe/campusestudiante/assets/images/logousat.png' width='100' height='100' ></div>"
        mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:center;'><div class='usat'>SISTEMA DE GESTIÓN DE INVESTIGACIÓN</div></div></td></tr></table>"
        mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr>"
        mensaje = mensaje + "<td style = 'background:none;border-bottom:1px solid #F1132A;height:1px;width:50%;margin:0px 0px 0px 0px' > &nbsp;</td></tr></table><br />"
        mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"

        If (Estado = "GANADOR") Then
            mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:center;'><div class='usat'> ¡FELICIDADES GANASTE! </div></div>"
            mensaje = mensaje + "<div style='text-align:center;color:gray;font-family:Calibri'><b> " + denominacion + " </b></div>"
        Else
            mensaje = mensaje + "<div style='text-align:center;color:gray;font-family:Calibri'><b> " + denominacion + " </b></div>"
        End If


        If (Tipo = "INV") Then
            If Estado = "APROBACIÓN" Then
                mensaje = mensaje + "<div style='margin-top:5px;text-align:left;color:gray;font-family:Calibri'><b>Su Registro ha sido Aprobado, ingrese al módulo de Gestión de Investigación para visualizarlo.</b></div>"
            ElseIf Estado = "RECHAZADO" Then
                mensaje = mensaje + "<div style='margin-top:5px;text-align:left;color:gray;font-family:Calibri'><b>Su Registro ha sido Rechazado, ingrese al módulo de Gestión de Investigación para visualizarlo.</b></div>"
            Else
                mensaje = mensaje + "<div style='margin-top:10px;text-align:left;color:gray;font-family:Calibri'><b>Su Registro ha sido Observado, ingrese al módulo de Gestión de Investigación para visualizarlo.</b></div>"
            End If
            'mensaje = mensaje + "<div style='margin-top:10px;text-align:center;color:gray;font-family:Calibri '>Se agregó un estado en <b>" + opcion + "</b></div>"
        Else
            If (Estado = "GANADOR") Then
                mensaje = mensaje + "<div style='margin-top:10px;text-align:left;color:gray;font-family:Calibri '>Nos complace en informar que su postulación al concurso ha sido elegida como ganadora</div>"
            Else
                If Estado = "APROBACIÓN" Then
                    mensaje = mensaje + "<div style='margin-top:5px;text-align:left;color:gray;font-family:Calibri'><b>Su Registro ha sido Aprobado, ingrese al módulo de Gestión de Investigación para visualizarlo.</b></div>"
                ElseIf Estado = "RECHAZADO" Then
                    mensaje = mensaje + "<div style='margin-top:5px;text-align:left;color:gray;font-family:Calibri'><b>Su Registro ha sido Rechazado, ingrese al módulo de Gestión de Investigación para visualizarlo.</b></div>"
                Else
                    mensaje = mensaje + "<div style='margin-top:10px;text-align:left;color:gray;font-family:Calibri'><b>Su Registro ha sido Observado, ingrese al módulo de Gestión de Investigación para visualizarlo.</b></div>"
                End If
                'mensaje = mensaje + "<div style='margin-top:10px;text-align:left;color:gray;font-family:Calibri '><b> " + nombre + " </b> se agrego un estado en <b>" + opcion + "</b></div>"
            End If
        End If

        If (Estado <> "GANADOR") Then
            If (Estado = "APROBACIÓN" Or Estado = "REACTIVADO") Then
                mensaje = mensaje + "<div style='margin-top:5px;text-align:left;color:gray;font-family:Calibri'><b>ESTADO:</b> " + Estado + "</div></td></tr></table>"
            Else
                mensaje = mensaje + "<div style='margin-top:5px;text-align:left;color:gray;font-family:Calibri'><b>ESTADO:</b> " + Estado + "</div></td></tr></tbody></table>"
                'If (Tipo <> "INV") Then
                'mensaje = mensaje + "<div style='margin-top:5px;text-align:left;color:gray;font-family:Calibri'><b>MOTIVO:</b> " + Descripcion + "</div></td></tr></table>"
                'End If
            End If
        Else
            mensaje = mensaje + "<div style='margin-top:5px;text-align:left;color:gray;font-family:Calibri'></div></td></tr></table>"
        End If
        mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
        mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:center;color:white'>"
        mensaje = mensaje + "</div></td></tr></table>"
        mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'>"
        mensaje = mensaje + "<tr><td style='background:none;border-bottom:1px solid #F1132A;height:1px;width:50%;margin:0px 0px 0px 0px' > &nbsp;</td></tr></table><br />"
        mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
        'mensaje = mensaje + "<div style='margin:0 auto;text-align:center;color:gray;font-family:Calibri '>¡Muchas gracias por confiar en nosotros!</div>"
        mensaje = mensaje + "<div style='margin:0 auto;text-align:center;color:gray;font-family:Calibri '><b>VICERRECTORADO INVESTIGACIÓN USAT</b></div><br /></td></tr></table>"
        mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
        mensaje = mensaje + "<div style='text-align:center;font-size:11px;color:gray;font-family:Calibri '><div>Av. San Josemaría Escrivá de Balaguer Nº 855 Chiclayo - Perú | Teléfono: 606200 - anexo: 1291"
        mensaje = mensaje + "<a href='mailto:vri@usat.edu.pe' style='color:gray;text-decoration:none;' target='_blank'><br/><b>vri@usat.edu.pe</b></a></div> "
        mensaje = mensaje + "<div style='font-family:Calibri'>© Copyright 2018: USAT - Todos los derechos reservados</div>"
        mensaje = mensaje + "</td></tr></table></div></body></html>"

        receptor = email

        If ConfigurationManager.AppSettings("CorreoUsatActivo") = 0 Then
            receptor = "hcano@usat.edu.pe"
        End If

        objemail.EnviarMail("campusvirtual@usat.edu.pe", "GESTIÓN DE INVESTIGACIÓN", receptor, AsuntoCorreo, mensaje, True)

        'Dim list As New List(Of Dictionary(Of String, Object))()
        'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        Dim dict As New Dictionary(Of String, Object)()
        dict.Add("Message", "correo Enviado con éxito")
        dict.Add("Status", "error")
        dict.Add("Code", "0")

        list.Add(dict)

        JSONresult = serializer.Serialize(list)
    End Sub
    Sub ListarBonosPublicacionEstado()
        Dim JSONresult As String = ""
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion
        Dim tipo As String = Request("param1")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_ListarBonoPublicacionEstado", tipo)
        obj.CerrarConexion()

        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim sele As String = "0"
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("reset", False)
            dict.Add("c_bon", tb.Rows(i).Item("c_bon").ToString())
            dict.Add("d_tit", tb.Rows(i).Item("d_tit").ToString())
            dict.Add("f_tit", tb.Rows(i).Item("f_tit").ToString())
            dict.Add("c_bdrev", tb.Rows(i).Item("c_bdrev").ToString())
            dict.Add("r_bon", tb.Rows(i).Item("r_bon").ToString())
            dict.Add("u_bon", tb.Rows(i).Item("u_bon").ToString())
            dict.Add("c_inv", tb.Rows(i).Item("c_inv").ToString())
            dict.Add("n_inv", tb.Rows(i).Item("n_inv").ToString())
            dict.Add("c_iea", tb.Rows(i).Item("c_iea").ToString())
            dict.Add("d_iea", tb.Rows(i).Item("d_iea").ToString())
            dict.Add("fin_bon", tb.Rows(i).Item("fin_bon").ToString())
            list.Add(dict)
        Next
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub ListarBonosPublicacionTO()
        Dim JSONresult As String = ""
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim codigo As Integer = Request("param1")
        Dim tipo As String = Request("param2")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_ListarBonoPublicacion", codigo, tipo)
        obj.CerrarConexion()

        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim sele As String = "0"
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("reset", False)
            dict.Add("c_bon", tb.Rows(i).Item("c_bon").ToString())
            dict.Add("d_tit", tb.Rows(i).Item("d_tit").ToString())
            dict.Add("f_tit", tb.Rows(i).Item("f_tit").ToString())
            dict.Add("c_bdrev", tb.Rows(i).Item("c_bdrev").ToString())
            dict.Add("c_tipopart", tb.Rows(i).Item("c_tipopart").ToString())
            dict.Add("r_bon", tb.Rows(i).Item("r_bon").ToString())
            dict.Add("u_bon", tb.Rows(i).Item("u_bon").ToString())
            dict.Add("c_inv", tb.Rows(i).Item("c_inv").ToString())
            dict.Add("n_inv", tb.Rows(i).Item("n_inv").ToString())
            dict.Add("c_iea", tb.Rows(i).Item("c_iea").ToString())
            dict.Add("d_iea", tb.Rows(i).Item("d_iea").ToString())
            dict.Add("fin_bon", tb.Rows(i).Item("fin_bon").ToString())
            list.Add(dict)
        Next
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub ActualizarBonoPublicacion()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim Codigo_bon As Integer = Request("param1")
        Dim Tipo_act As String = Request("param2")
        Dim Codigo_use As Integer = obj1.DecrytedString64(Request("param3"))
        Dim Rechazo As String = Request("param4")
        Dim codRechazo As String = Request("param5")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_actualizarBonoPublicacion", Codigo_bon, Tipo_act, Codigo_use, Rechazo, codRechazo)
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", tb.Rows(i).Item("Status"))
            Data.Add("Message", tb.Rows(i).Item("Message"))
            Data.Add("Code", tb.Rows(i).Item("Code"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub
    Sub VisualizarHistorialGI()
        Dim JSONresult As String = ""
        'Try
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion
        Dim codigo As Integer = Request("param1")
        Dim tipo As String = Request("param2")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_VisualizarHistorialGI", codigo, tipo)
        obj.CerrarConexion()

        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("reset", False)
            dict.Add("c_his", tb.Rows(i).Item("c_his").ToString())
            dict.Add("c_id", tb.Rows(i).Item("c_id").ToString())
            dict.Add("d_his", tb.Rows(i).Item("d_his").ToString())
            dict.Add("c_ins", tb.Rows(i).Item("c_ins").ToString())
            dict.Add("d_ins", tb.Rows(i).Item("d_ins").ToString())
            dict.Add("d_fech", tb.Rows(i).Item("d_fech").ToString())
            dict.Add("c_usr", tb.Rows(i).Item("c_usr").ToString())
            dict.Add("d_obs", tb.Rows(i).Item("d_obs").ToString())
            dict.Add("c_obs", tb.Rows(i).Item("c_obs").ToString())
            dict.Add("f_res", tb.Rows(i).Item("f_res").ToString())
            dict.Add("u_res", tb.Rows(i).Item("u_res").ToString())
            dict.Add("c_eia", tb.Rows(i).Item("c_eia").ToString())
            dict.Add("d_eia", tb.Rows(i).Item("d_eia").ToString())
            list.Add(dict)
        Next
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub
    Sub GuardarHistorialGI()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim Correlativo As Integer = Request("param1")
        Dim Observacion As String = Request("param2")
        Dim Tipo As String = Request("param3")
        Dim Instancia As Integer = Request("param4")
        Dim Estado As String = Request("param5")
        Dim Codigo_his As Integer = Request("param6")
        Dim Codigo_user As Integer = obj1.DecrytedString64(Request("hdUser"))

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString


        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_HistorialInvestigacion", Correlativo, Observacion, Tipo, Instancia, Estado, Codigo_user, Codigo_his)
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", tb.Rows(i).Item("Status"))
            Data.Add("Message", tb.Rows(i).Item("Message"))
            Data.Add("Code", tb.Rows(i).Item("Code"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub

    Sub ListarRegion()
        Dim JSONresult As String = ""
        'Try
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_listarDepartamentos")
        obj.CerrarConexion()

        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim sele As String = "0"
        For i As Integer = 0 To tb.Rows.Count
            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("reset", False)
            If (sele = "0") Then
                dict.Add("c_reg", 0)
                dict.Add("d_reg", "-- Seleccione --")
                dict.Add("d_sel", "selected")
                sele = "1"
            Else
                dict.Add("c_reg", tb.Rows(i - 1).Item("codigo").ToString())
                dict.Add("d_reg", tb.Rows(i - 1).Item("nombre").ToString())
                dict.Add("d_sel", "")
            End If
            list.Add(dict)
        Next
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub ListarLineasUsat()
        Dim JSONresult As String = ""
        'Try
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion
        Dim param As String = Request("param1")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_ListaLineasInvestigacion", param)
        obj.CerrarConexion()

        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim sele As String = "0"
        For i As Integer = 0 To tb.Rows.Count
            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("reset", False)
            If (sele = "0") Then
                dict.Add("c_lin", 0)
                dict.Add("d_lin", "-- Seleccione --")
                sele = "1"
            Else
                dict.Add("c_lin", tb.Rows(i - 1).Item("codigo").ToString())
                dict.Add("d_lin", tb.Rows(i - 1).Item("nombre").ToString())
            End If
            list.Add(dict)
        Next
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub actualizarEstadoGrupoInvestigador()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim Codigo_gru As Integer = Request("param1")
        Dim Tipo_act As String = Request("param2")
        Dim Codigo_use As Integer = obj1.DecrytedString64(Request("param3"))
        Dim Rechazo As String = Request("param4")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_actualizarGrupoInvestigador", Codigo_gru, Tipo_act, Codigo_use, Rechazo)
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", tb.Rows(i).Item("Status"))
            Data.Add("Message", tb.Rows(i).Item("Message"))
            Data.Add("Code", tb.Rows(i).Item("Code"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

        'Catch ex As Exception
        'Response.Write("messaaaaage" & ex.Message.ToString)
        'Dim list As New List(Of Dictionary(Of String, Object))()
        'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'Dim dict As New Dictionary(Of String, Object)()
        'dict.Add("Status", "error")
        'dict.Add("msje", ex.Message.ToString)
        'dict.Add("Code", "0")
        'list.Add(dict)
        'JSONresult = serializer.Serialize(list)
        'Response.Write(JSONresult)
        'End Try
    End Sub

    Sub ListaGrupoInvestigadores()
        Dim JSONresult As String = ""
        'Try
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion
        Dim codigo As Integer = Request("param1")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_listarGrupoInvestigador", codigo)
        obj.CerrarConexion()

        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim sele As String = "0"
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("reset", False)
            dict.Add("c_gru", tb.Rows(i).Item("c_gru").ToString())
            dict.Add("d_gru", tb.Rows(i).Item("d_gru").ToString())
            dict.Add("c_tipo", tb.Rows(i).Item("c_tipo").ToString())
            dict.Add("c_lin", tb.Rows(i).Item("c_lin").ToString())
            dict.Add("d_lin", tb.Rows(i).Item("d_lin").ToString())
            dict.Add("c_are", tb.Rows(i).Item("c_are").ToString())
            dict.Add("d_are", tb.Rows(i).Item("d_are").ToString())
            dict.Add("c_sub", tb.Rows(i).Item("c_sub").ToString())
            dict.Add("d_sar", tb.Rows(i).Item("d_sar").ToString())
            dict.Add("dis", tb.Rows(i).Item("dis").ToString())
            dict.Add("d_dis", tb.Rows(i).Item("d_dis").ToString())
            dict.Add("c_reg", tb.Rows(i).Item("c_reg").ToString())
            dict.Add("d_reg", tb.Rows(i).Item("d_reg").ToString())
            dict.Add("c_iea", tb.Rows(i).Item("c_iea").ToString())
            dict.Add("d_iea", tb.Rows(i).Item("d_iea").ToString())
            dict.Add("c_inv", tb.Rows(i).Item("c_inv").ToString())
            dict.Add("c_per", tb.Rows(i).Item("c_per").ToString())
            dict.Add("d_nom", tb.Rows(i).Item("d_nom").ToString())
            dict.Add("c_dni", tb.Rows(i).Item("c_dni").ToString())
            dict.Add("c_dgi", tb.Rows(i).Item("c_dgi").ToString())
            dict.Add("d_ded", tb.Rows(i).Item("d_ded").ToString())
            dict.Add("c_rin", tb.Rows(i).Item("c_rin").ToString())
            dict.Add("d_rin", tb.Rows(i).Item("d_rin").ToString())
            dict.Add("prov_gru", tb.Rows(i).Item("prov_gru").ToString())
            dict.Add("dist_gru", tb.Rows(i).Item("dist_gru").ToString())
            dict.Add("lug_gru", tb.Rows(i).Item("lug_gru").ToString())
            dict.Add("prop_gru", tb.Rows(i).Item("prop_gru").ToString())
            dict.Add("coord_dgi", tb.Rows(i).Item("coord_dgi").ToString())

            list.Add(dict)
        Next
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

        'Catch ex As Exception
        'Response.Write(ex.Message)
        'Dim list As New List(Of Dictionary(Of String, Object))()
        'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'Dim dict As New Dictionary(Of String, Object)()
        'dict.Add("reset", True)
        'list.Add(dict)
        'JSONresult = serializer.Serialize(list)
        'End Try
    End Sub

    Sub actualizarEstadoInvestigador()
        Dim JSONresult As String = ""
        Try
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim obj As New ClsConectarDatos
            Dim obj1 As New ClsGestionInvestigacion

            Dim Codigo_inv As Integer = Request("param1")
            Dim Tipo_act As String = Request("param2")
            Dim Codigo_use As Integer = obj1.DecrytedString64(Request("param3"))

            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("INV_actualizarInvestigador", Codigo_inv, Tipo_act, Codigo_use)
            obj.CerrarConexion()

            For i As Integer = 0 To tb.Rows.Count - 1
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("Status", tb.Rows(i).Item("Status"))
                Data.Add("Message", tb.Rows(i).Item("Message"))
                Data.Add("Code", tb.Rows(i).Item("Code"))
                list.Add(Data)
            Next

            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception
            'Response.Write("messaaaaage" & ex.Message.ToString)
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("Status", "error")
            dict.Add("msje", ex.Message.ToString)
            dict.Add("Code", "0")
            list.Add(dict)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try

    End Sub

    Sub ListarBonosPublicacion()
        Dim JSONresult As String = ""
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim codigo As Integer = obj1.DecrytedString64(Request("param1"))
        Dim tipo As String = Request("param2")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_ListarBonoPublicacion", codigo, tipo)
        obj.CerrarConexion()

        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim sele As String = "0"
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("reset", False)
            dict.Add("c_bon", tb.Rows(i).Item("c_bon").ToString())
            dict.Add("d_tit", tb.Rows(i).Item("d_tit").ToString())
            dict.Add("f_tit", tb.Rows(i).Item("f_tit").ToString())
            dict.Add("c_bdrev", tb.Rows(i).Item("c_bdrev").ToString())
            dict.Add("c_tipopart", tb.Rows(i).Item("c_tipopart").ToString())
            dict.Add("r_bon", tb.Rows(i).Item("r_bon").ToString())
            dict.Add("u_bon", tb.Rows(i).Item("u_bon").ToString())
            dict.Add("c_inv", tb.Rows(i).Item("c_inv").ToString())
            dict.Add("c_iea", tb.Rows(i).Item("c_iea").ToString())
            dict.Add("d_iea", tb.Rows(i).Item("d_iea").ToString())
            dict.Add("fin_bon", tb.Rows(i).Item("fin_bon").ToString())
            list.Add(dict)
        Next
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub
    Sub RegistrarBonoPublicacion()
        Dim JSONresult As String = ""
        'Try
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim Accion As String = Request("hdAccion")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        If (Accion = "R") Then
            Dim titulo_bon As String = Request("txtTitulo").ToString.ToUpper
            Dim fecha_bon As String = Request("txtFecha")
            Dim BDrevista_bon As String = Request("cboBDRevista")
            Dim tipoparticipacion As String = Request("cboParticipacion")
            Dim revista_bon As String = Request("txtRevista").ToString.ToUpper
            Dim URLPubli_bon As String = Request("txtURLPubli")
            Dim Codigo_user As Integer = obj1.DecrytedString64(Request("hdUser"))
            tb = obj.TraerDataTable("INV_registrarBonoPublicacion", 0, titulo_bon, fecha_bon, tipoparticipacion, BDrevista_bon, revista_bon, URLPubli_bon, Codigo_user, Codigo_user)
        Else
            Dim codigo_bon As Integer = Request("hdBono")
            Dim titulo_bon As String = Request("txtTituloE").ToString.ToUpper
            Dim fecha_bon As String = Request("txtFechaE")
            Dim tipoparticipacion As String = Request("cboParticipacionE")
            Dim BDrevista_bon As String = Request("cboBDRevistaE")
            Dim revista_bon As String = Request("txtRevistaE").ToString.ToUpper
            Dim URLPubli_bon As String = Request("txtURLPubliE")
            Dim Codigo_user As Integer = obj1.DecrytedString64(Request("hdUser"))
            tb = obj.TraerDataTable("INV_registrarBonoPublicacion", codigo_bon, titulo_bon, fecha_bon, tipoparticipacion, BDrevista_bon, revista_bon, URLPubli_bon, Codigo_user, Codigo_user)
        End If
        obj.CerrarConexion()

        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", tb.Rows(i).Item("Status"))
            Data.Add("Message", tb.Rows(i).Item("Message"))
            Data.Add("Code", tb.Rows(i).Item("Code"))
            list.Add(Data)
        Next

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub ListarGrupoComoCoordinador()
        Dim JSONresult As String = ""
        Try
            Dim obj As New ClsConectarDatos
            Dim obj1 As New ClsGestionInvestigacion
            Dim param1 As Integer = obj1.DecrytedString64(Request("param1"))
            Dim param2 As String = Request("param2")

            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("INV_listaGrupoInvestigacion", param1, param2)
            obj.CerrarConexion()

            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim sele As String = "0"
            For i As Integer = 0 To tb.Rows.Count
                Dim dict As New Dictionary(Of String, Object)()
                dict.Add("reset", False)
                If (sele = "0") Then
                    dict.Add("c_gru", 0)
                    dict.Add("n_gru", "-- Seleccione --")
                    dict.Add("e_gru", 0)
                    dict.Add("selected", "selected")
                    sele = "1"
                Else
                    dict.Add("c_gru", tb.Rows(i - 1).Item("codigo_gru").ToString())
                    dict.Add("n_gru", tb.Rows(i - 1).Item("nombre_gru").ToString())
                    dict.Add("e_gru", tb.Rows(i - 1).Item("estado_gru").ToString())
                    dict.Add("selected", "")
                End If
                list.Add(dict)
            Next
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception
            'Response.Write(ex.Message)
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("reset", True)
            list.Add(dict)
            JSONresult = serializer.Serialize(list)
        End Try
    End Sub
    Sub RegistrarGrupoDetalleInvestigacion()
        Dim JSONresult As String = ""
        Try
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim obj As New ClsConectarDatos
            Dim obj1 As New ClsGestionInvestigacion

            Dim Nombre_gru As String = Request("txtGrupo").ToString.ToUpper
            Dim Codigo_lin As Integer = Request("cboLineasUSAT")
            'Dim Codigo_tip As Integer = Request("hdTipo") ' -- POR DEFECTO MULTI
            Dim Codigo_tip As Integer = 2 ' -- POR DEFECTO MULTI
            Dim Codigo_are As Integer = Request("cboArea")
            Dim Codigo_sub As Integer = Request("cboSubArea")
            Dim Codigo_dis As Integer = Request("cboDisciplina")
            Dim Codigo_reg As Integer = Request("cboRegion")
            Dim Provincia_gru As Integer = Request("cboProvincia")
            Dim Distrito_gru As Integer = Request("cboDistrito")
            Dim txtLugar As String = Request("txtLugar")
            Dim file_propuesta As String = Request("file_propuesta")
            Dim hdCoord As Integer = Request("hdCoord")

            Dim Array_inv As String = Request("hdDetalleInv")
            Dim Array_ded As String = Request("hdDetalleDed")
            Dim Array_tip As String = Request("hdDetalleTip")
            Dim Array_tipo As String = Request("hdDetalleTipo")
            Dim Codigo_user As Integer = obj1.DecrytedString64(Request("hdUser"))
            Dim Accion As String = Request("hdAccion")

            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            If (Accion = "R") Then
                'Response.Write(Nombre_gru & "-" & Codigo_lin & "-" & Codigo_tip & "-" & Codigo_are & "-" & Codigo_sub & "-" & Codigo_dis & "-" & Codigo_reg & "-" & Provincia_gru & "-" & Distrito_gru & "-" & txtLugar & "-" & file_propuesta & "-" & hdCoord & "-" & Array_inv & "-" & Array_ded & "-" & Array_tip & "-" & Codigo_user & "-" & Accion)
                file_propuesta = "file"
                tb = obj.TraerDataTable("INV_RegistrarGrupoInvestigador", Nombre_gru, Codigo_tip, Codigo_lin, Codigo_are, Codigo_sub, Codigo_dis, Codigo_reg, Provincia_gru, Distrito_gru, txtLugar, file_propuesta, hdCoord, Array_inv, Array_ded, Array_tip, Array_tipo, Codigo_user)
            Else
                Dim Codigo_gru As String = Request("hdGru")
                'Response.Write(Nombre_gru & "-" & Codigo_lin & "-" & Codigo_tip & "-" & Codigo_are & "-" & Codigo_sub & "-" & Codigo_dis & "-" & Codigo_reg & "-" & Provincia_gru & "-" & Distrito_gru & "-" & txtLugar & "-" & file_propuesta & "-" & hdCoord & "-" & Array_inv & "-" & Array_ded & "-" & Array_tip & "-" & Codigo_user & "-" & Accion)
                'Response.Write("-" & file_propuesta & "-")
                If (file_propuesta = "") Then
                    file_propuesta = "X"
                End If

                tb = obj.TraerDataTable("INV_ActualizarRegistroGrupoInvestigador", Codigo_gru, Nombre_gru, Codigo_tip, Codigo_lin, Codigo_are, Codigo_sub, Codigo_dis, Codigo_reg, Provincia_gru, Distrito_gru, txtLugar, file_propuesta, hdCoord, Array_inv, Array_ded, Array_tip, Array_tipo, Codigo_user)
            End If
            obj.CerrarConexion()

            For i As Integer = 0 To tb.Rows.Count - 1
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("Status", tb.Rows(i).Item("Status"))
                Data.Add("Message", tb.Rows(i).Item("Message"))
                Data.Add("Code", tb.Rows(i).Item("Code"))
                list.Add(Data)
            Next

            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception
            'Response.Write("messaaaaage" & ex.Message.ToString)
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("Status", "error")
            dict.Add("msje", ex.Message.ToString)
            dict.Add("Code", "0")
            list.Add(dict)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Sub ListarRolInvestigador(ByVal tipo As String)
        Dim JSONresult As String = ""
        Try

            Dim obj As New ClsConectarDatos
            Dim obj1 As New ClsGestionInvestigacion

            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("INV_listarRolInvestigador", tipo)
            obj.CerrarConexion()

            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim sele As String = "0"
            For i As Integer = 0 To tb.Rows.Count
                Dim dict As New Dictionary(Of String, Object)()
                dict.Add("reset", False)
                If (sele = "0") Then
                    dict.Add("c_rin", 0)
                    dict.Add("d_rin", "-- Seleccione --")
                    dict.Add("e_rin", 0)
                    dict.Add("selected", "selected")
                    sele = "1"
                Else
                    dict.Add("c_rin", tb.Rows(i - 1).Item("c_rin").ToString())
                    dict.Add("d_rin", tb.Rows(i - 1).Item("d_rin").ToString())
                    dict.Add("e_rin", tb.Rows(i - 1).Item("e_rin").ToString())
                    dict.Add("selected", "")
                End If
                list.Add(dict)
            Next
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception
            'Response.Write(ex.Message)
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("reset", True)
            list.Add(dict)
            JSONresult = serializer.Serialize(list)
        End Try
    End Sub
    Sub ListarInvestigadoresEstado()
        Dim JSONresult As String = ""
        Try

            Dim obj As New ClsConectarDatos
            Dim obj1 As New ClsGestionInvestigacion

            Dim codigo As Integer = Request("param1")
            Dim tipo As String = Request("param2")
            Dim estado As String = Request("param3")

            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("INV_listaInvestigadores", codigo, tipo, estado)
            obj.CerrarConexion()


            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim sele As String = "0"
            For i As Integer = 0 To tb.Rows.Count - 1
                Dim dict As New Dictionary(Of String, Object)()
                dict.Add("reset", False)
                dict.Add("c_inv", tb.Rows(i).Item("c_inv").ToString())
                dict.Add("c_per", tb.Rows(i).Item("c_per").ToString())
                dict.Add("d_rev", tb.Rows(i).Item("d_rev").ToString())
                dict.Add("d_url", tb.Rows(i).Item("d_url").ToString())
                dict.Add("c_rev", tb.Rows(i).Item("c_rev").ToString())
                dict.Add("d_est", tb.Rows(i).Item("d_est").ToString())
                dict.Add("d_per", tb.Rows(i).Item("d_per").ToString())
                dict.Add("dni_per", tb.Rows(i).Item("dni_per").ToString())
                dict.Add("c_tpe", tb.Rows(i).Item("c_tpe").ToString())
                dict.Add("d_tpe", tb.Rows(i).Item("d_tpe").ToString())
                dict.Add("c_cgo", tb.Rows(i).Item("c_cgo").ToString())
                dict.Add("d_cgo", tb.Rows(i).Item("d_cgo").ToString())
                dict.Add("d_iea", tb.Rows(i).Item("d_iea").ToString())
                dict.Add("dina", tb.Rows(i).Item("dina").ToString())
                dict.Add("regina", tb.Rows(i).Item("regina").ToString())
                dict.Add("cod_renacyt", tb.Rows(i).Item("Codigoregina").ToString())
                dict.Add("orcid", tb.Rows(i).Item("orcid").ToString())
                If tb.Rows(i).Item("cod_linea").ToString() = "" Or tb.Rows(i).Item("cod_linea").ToString() = "0" Then
                    dict.Add("cod_linea", "")
                Else
                    dict.Add("cod_linea", obj1.EncrytedString64(tb.Rows(i).Item("cod_linea").ToString()))
                End If

                list.Add(dict)
            Next
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception
            'Response.Write(ex.Message)
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("reset", True)
            list.Add(dict)
            JSONresult = serializer.Serialize(list)
        End Try
    End Sub

    Sub ListrInvestigadores()
        Dim JSONresult As String = ""
        Try

            Dim obj As New ClsConectarDatos
            Dim obj1 As New ClsGestionInvestigacion

            Dim codigo As Integer = Request("param1")
            Dim tipo As String = Request("param2")
            Dim estado As String = Request("param3")

            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("INV_listaInvestigadores", codigo, tipo)
            obj.CerrarConexion()

            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim sele As String = "0"
            For i As Integer = 0 To tb.Rows.Count - 1
                Dim dict As New Dictionary(Of String, Object)()
                dict.Add("reset", False)
                dict.Add("c_inv", tb.Rows(i).Item("c_inv").ToString())
                dict.Add("c_per", tb.Rows(i).Item("c_per").ToString())
                dict.Add("d_url", tb.Rows(i).Item("d_url").ToString())
                dict.Add("c_rev", tb.Rows(i).Item("c_rev").ToString())
                dict.Add("d_est", tb.Rows(i).Item("d_est").ToString())
                dict.Add("d_per", tb.Rows(i).Item("d_per").ToString())
                dict.Add("dni_per", tb.Rows(i).Item("dni_per").ToString())
                list.Add(dict)
            Next
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception
            'Response.Write(ex.Message)
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("reset", True)
            list.Add(dict)
            JSONresult = serializer.Serialize(list)
        End Try
    End Sub

    Sub ListarAreaConocimientosOCDE()
        Dim JSONresult As String = ""
        'Try
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion

        Dim codigo As String = Request("param1")
        Dim tipo As String = Request("param2")

        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("INV_listaAreasConocimientoOCDE", codigo, tipo)
        obj.CerrarConexion()

        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim sele As String
        sele = "0"
        For i As Integer = 0 To tb.Rows.Count
            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("reset", False)
            If (sele = "0") Then
                dict.Add("codigo", 0)
                dict.Add("subcodigo", 0)
                dict.Add("descripcion", "-- Seleccione --")
                dict.Add("selected", "selected")
                dict.Add("estado", 1)
                sele = "1"
            Else
                dict.Add("codigo", tb.Rows(i - 1).Item("codigo").ToString())
                dict.Add("subcodigo", tb.Rows(i - 1).Item("subcodigo").ToString())
                dict.Add("descripcion", tb.Rows(i - 1).Item("descripcion").ToString())
                dict.Add("selected", "")
                dict.Add("estado", tb.Rows(i - 1).Item("estado").ToString())
            End If

            list.Add(dict)
        Next
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

        'Catch ex As Exception
        ''Response.Write(ex.Message)
        'Dim list As New List(Of Dictionary(Of String, Object))()
        'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'Dim dict As New Dictionary(Of String, Object)()
        ' dict.Add("reset", True)
        'list.Add(dict)
        'JSONresult = serializer.Serialize(list)
        'End Try
    End Sub

    Sub RegistrarInvestigador()
        Dim JSONresult As String = ""
        Try
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim obj As New ClsConectarDatos
            Dim obj1 As New ClsGestionInvestigacion

            Dim Codigo_user As Integer = obj1.DecrytedString64(Request("hdUser"))
            Dim Url_dina As String = Request("txtURLDina")
            Dim orcid As String = Request("txtOrcid")
            Dim dina As Integer = Request("chkDINA")
            Dim regina As Integer = Request("chkREGINA")
            Dim Accion As String = Request("hdAccion")
            Dim codigo_linea As Integer = obj1.DecrytedString64(Request("cboLinea"))

            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            If (Accion = "R") Then
                tb = obj.TraerDataTable("INV_registrarInvestigador", Codigo_user, codigo_linea, Url_dina, dina, regina, Codigo_user, orcid)
            Else
                If (Accion = "A") Then
                    tb = obj.TraerDataTable("INV_actualizarInvestigadorHistorial", Codigo_user, codigo_linea, Url_dina, dina, regina, Codigo_user, orcid)
                End If
            End If
            obj.CerrarConexion()

            For i As Integer = 0 To tb.Rows.Count - 1
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("Status", tb.Rows(i).Item("Status"))
                Data.Add("Message", tb.Rows(i).Item("Message"))
                Data.Add("Code", tb.Rows(i).Item("Code"))
                list.Add(Data)
            Next

            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception
            'Response.Write("messaaaaage" & ex.Message.ToString)
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("Status", "error")
            dict.Add("msje", ex.Message.ToString)
            dict.Add("Code", "0")
            list.Add(dict)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try

    End Sub





    Private Sub ValidaSession()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim data As New Dictionary(Of String, Object)()
        If Session("id_per") <> "" Then
            data.Add("msje", True)
            data.Add("link", "")
        Else
            data.Add("msje", False)
            data.Add("link", "../../sinacceso.html")
        End If
        list.Add(data)
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub TiposOperacion()
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim obj As New ClsGradosyTitulos

            Dim data As New Dictionary(Of String, Object)()
            data.Add("ValSes", obj.EncrytedString64("ValidaSession")) ' Valida Sesion
            data.Add("lst", obj.EncrytedString64("Listar")) ' Listar
            data.Add("reg", obj.EncrytedString64("Registrar")) ' Registrar
            data.Add("edi", obj.EncrytedString64("Editar")) ' Modificar
            data.Add("mod", obj.EncrytedString64("Modificar")) ' Modificar
            data.Add("eli", obj.EncrytedString64("Eliminar")) ' Eliminar
            data.Add("rob", obj.EncrytedString64("RegistrarObjetivos")) ' Registrar Objetivos
            data.Add("req", obj.EncrytedString64("RegistrarEquipo")) ' Registrar Equipo 
            data.Add("Up", obj.EncrytedString64("SurbirArchivo")) ' Subir Archivo
            data.Add("lob", obj.EncrytedString64("ListaObjetivos")) ' Lista Objetivos de Proyecto
            data.Add("lap", obj.EncrytedString64("ListarAutorProyecto")) ' Lista Autor(es) de Proyecto
            data.Add("aie", obj.EncrytedString64("ActualizarInstanciaEstado")) ' Lista Autor(es) de Proyecto
            data.Add("cfe", obj.EncrytedString64("CargarFiltroEstado")) ' Listar Estados por tipo funcion para filtros
            data.Add("lop", obj.EncrytedString64("ListarObservaciones")) ' Observar Proyecto
            data.Add("lpo", obj.EncrytedString64("ListarPostulaciones")) ' Listar Todas las Postulaciones 
            data.Add("ace", obj.EncrytedString64("ActualizarEtapa")) ' Actualizar Etapa de Postulacion
            data.Add("lcp", obj.EncrytedString64("ListarConcursosPostulacion")) ' Concursos con postulaciones
            data.Add("lep", obj.EncrytedString64("ListarEquipoPostulacion")) ' Lista Equipo Postulacion
            data.Add("lpe", obj.EncrytedString64("ListarPostulacionEvaluacionExterna")) ' Concursos con postulaciones
            data.Add("lev", obj.EncrytedString64("ListaEvaluadores")) ' asignar evaluador a postulaciones
            data.Add("aev", obj.EncrytedString64("AsignarEvaluador")) ' asignar evaluador a postulaciones
            data.Add("qev", obj.EncrytedString64("QuitarEvaluador")) ' asignar evaluador a postulaciones
            data.Add("email", obj.EncrytedString64("EnviarMail")) ' Subir Archivo
            data.Add("Up2", obj.EncrytedString64("SurbirArchivo2")) ' Subir Archivo
            data.Add("lstex", obj.EncrytedString64("ListarPostulacionExterna")) ' Listar PostulacionExterna
            data.Add("rpe", obj.EncrytedString64("RegistrarPostulacionExterna")) ' Subir Archivo
            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ListarPersonal(ByVal codigo As Integer, ByVal ctf As Integer, ByVal texto As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListaPersonal(codigo, ctf, texto)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                data.Add("nombre", dt.Rows(i).Item("nombre"))
                data.Add("dina", dt.Rows(i).Item("urldina"))
                data.Add("inv", obj.EncrytedString64(dt.Rows(i).Item("codigo_inv")))
                list.Add(data)
            Next
            'Dim data1 As New Dictionary(Of String, Object)()
            'data1.Add("cod", obj.EncrytedString64("T"))
            'data1.Add("nombre", "TODOS")
            'list.Add(data1)
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListaLineasInvestigacion(ByVal codigo As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListaLineasInvestigacion(codigo)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                data.Add("nombre", dt.Rows(i).Item("nombre"))
                list.Add(data)
            Next
            'Dim data1 As New Dictionary(Of String, Object)()
            'data1.Add("cod", obj.EncrytedString64("T"))
            'data1.Add("nombre", "TODOS")
            'list.Add(data1)
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListaTipoAutorProyecto(ByVal codigo As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListaTipoAutorProyecto(codigo)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                data.Add("nombre", dt.Rows(i).Item("nombre"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListarProvincias(ByVal tipo As String, ByVal codigo_Reg As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListaProvincias(tipo, codigo_Reg)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", dt.Rows(i).Item("codigo_pro"))
                data.Add("nombre", dt.Rows(i).Item("nombre_pro"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListarDistritos(ByVal tipo As String, ByVal codigo_prov As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListaDistritos(tipo, codigo_prov)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", dt.Rows(i).Item("codigo_dis"))
                data.Add("nombre", dt.Rows(i).Item("nombre_dis"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub


    Private Sub ConsultarRolInvestigador(ByVal tipo As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListaRolInvestigador(tipo)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("c_rin")))
                data.Add("nombre", dt.Rows(i).Item("d_rin"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ConsultarAlumnosTesis(ByVal texto As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListarAlumnosTesis(texto)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                data.Add("nombre", dt.Rows(i).Item("nombre"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub



    Private Sub ListarInvestigadorGrupos(ByVal tipo As String, ByVal codigo As Integer, ByVal id As Integer, ByVal ctf As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListarInvestigadorGrupos(tipo, codigo, id, ctf)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                If tipo = "IU" Or tipo = "IM" Then
                    data.Add("cod", dt.Rows(i).Item("codigo"))
                    data.Add("nombre", dt.Rows(i).Item("nombre"))
                    data.Add("tipo", dt.Rows(i).Item("tipo"))
                End If
                If tipo = "INV" Then
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                    data.Add("nombre", dt.Rows(i).Item("nombre"))
                    data.Add("dina", dt.Rows(i).Item("urldina_inv"))
                    'data.Add("rol", dt.Rows(i).Item("descripcion_rin"))
                End If
                If tipo = "GRU" Then
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                    data.Add("nombre", dt.Rows(i).Item("nombre"))
                    data.Add("dina", dt.Rows(i).Item("urldina_inv"))
                    'data.Add("rol", dt.Rows(i).Item("descripcion_rin"))
                End If
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub DescargarArchivo()
        Try
            Dim usuario_session As String = Session("perlogin")

            Dim IdArchivo As String = Request("IdArchivo")


            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Dictionary(Of String, String)

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.USP_LISTARARCHIVOSCOMPARTIDOS", 2, 9, IdArchivo, token)
            obj.CerrarConexion()

            Dim resultData As New List(Of Dictionary(Of String, Object))()
            ' Response.Write(IdArchivo)
            list.Add("IdArchivo", IdArchivo)
            list.Add("Usuario", usuario_session)
            list.Add("Token", token)

            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)

            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", Session("perlogin").ToString)

            Dim imagen As String = ResultFile(result)

            For i As Integer = 0 To tb.Rows.Count - 1
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("File", imagen)
                Data.Add("Nombre", tb.Rows(i).Item("NombreArchivo"))
                Data.Add("Extencion", tb.Rows(i).Item("Extencion"))
                resultData.Add(Data)
            Next
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            'serializer.MaxJsonLength = Int32.MaxValue
            JSONresult = serializer.Serialize(resultData)
            Response.Write(JSONresult)

        Catch ex As Exception
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("msje", ex.Message)
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub
    Function ResultFile(ByVal cadXml As String) As String
        Dim nsMgr As XmlNamespaceManager
        Dim xml As XmlDocument = New XmlDocument()
        xml.LoadXml(cadXml)
        nsMgr = New XmlNamespaceManager(xml.NameTable)
        nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
        Dim res As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body", nsMgr)
        '  Dim mNombre = xml.ReadElementString("nombre")
        Return res.InnerText
        '   Response.Write("dd" + res.InnerText)
    End Function

    Private Function ExtensionArchivo(ByVal ext As String) As String
        Dim extencion As String = ""
        Select Case ext.Trim.ToString
            Case ".txt"
                'extencion = "text/plain"
                extencion = "fa fa-file-text-o"
            Case ".doc"
            Case ".docx"

                'extencion = "application/ms-word"
                extencion = "fa fa-file-word-o"
            Case ".xls"
            Case ".xlsx"
                'extencion = "application/vnd.ms-excel"
                extencion = "fa fa-file-excel-o"
            Case ".gif"
                'extencion = "image/gif"
                extencion = "fa fa-file-image-o"
            Case ".jpg"
                'extencion = "image/jpeg"
                extencion = "fa fa-file-image-o"
            Case ".jpeg"
                'extencion = "image/jpeg"
                extencion = "fa fa-file-image-o"
            Case ".bmp"
                'extencion = "image/bmp"
                extencion = "fa fa-file-image-o"
            Case ".wav"
                'extencion = "audio/wav"
                extencion = "fa fa-file-audio-o"
            Case ".ppt"
                'extencion = "application/mspowerpoint"
                extencion = "fa fa-file-powerpoint-o"
            Case ".dwg"
                'extencion = "image/vnd.dwg"
                extencion = "fa fa-file-code-o"
            Case ".zip"
                extencion = "fa fa-file-archive-o"
            Case ".rar"
                extencion = "fa fa-file-archive-o"
            Case ".pdf"
                extencion = "fa fa-file-pdf-o"
            Case Else
                'extencion = "application/octet-stream"
                extencion = ""
        End Select
        Return extencion
    End Function

    Sub DescargarArchivo2()
        Try
            Dim usuario_session As String = Session("perlogin")
            Dim crm As New ClsCRM
            Dim IdArchivo As String = Request("IdArchivo")

            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Dictionary(Of String, String)

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.USP_LISTARARCHIVOSCOMPARTIDOS", 2, 9, IdArchivo, token)
            obj.CerrarConexion()

            Dim resultData As New List(Of Dictionary(Of String, Object))()
            ' Response.Write(IdArchivo)
            list.Add("IdArchivo", IdArchivo)
            list.Add("Usuario", usuario_session)
            list.Add("Token", token)

            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)

            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", Session("perlogin").ToString)

            Dim imagen As String = ResultFile(result)
            For i As Integer = 0 To tb.Rows.Count - 1
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("File", imagen)
                Data.Add("Nombre", tb.Rows(i).Item("NombreArchivo"))
                Data.Add("Extencion", tb.Rows(i).Item("Extencion"))
                resultData.Add(Data)
            Next
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            'serializer.MaxJsonLength = Int32.MaxValue
            JSONresult = serializer.Serialize(resultData)
            Response.Write(JSONresult)

        Catch ex As Exception
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("msje", ex.Message)
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub
    Sub DescargarArchivo3()
        Try
            'Dim usuario_session_ As String() = Session("perlogin").ToString.Split(New Char() {"\"c})
            Dim usuario_session As String = Session("perlogin")
            Dim crm As New ClsCRM
            Dim IdArchivo As String = Request("IdArchivo")

            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Dictionary(Of String, String)

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.USP_LISTARARCHIVOSCOMPARTIDOS", 2, 9, IdArchivo, token)
            obj.CerrarConexion()

            Dim resultData As New List(Of Dictionary(Of String, Object))()
            ' Response.Write(IdArchivo)
            list.Add("IdArchivo", IdArchivo)
            list.Add("Usuario", usuario_session)
            list.Add("Token", token)

            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)


            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", usuario_session)

            Dim imagen As String = ResultFile(result)
            For i As Integer = 0 To tb.Rows.Count - 1
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("File", imagen)
                Data.Add("Nombre", tb.Rows(i).Item("NombreArchivo"))
                Data.Add("Extencion", tb.Rows(i).Item("Extencion"))
                resultData.Add(Data)
            Next
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            'serializer.MaxJsonLength = Int32.MaxValue
            JSONresult = serializer.Serialize(resultData)
            Response.Write(JSONresult)

        Catch ex As Exception
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("msje", ex.Message)
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ConsultarDatosDocenteInvestigador(ByVal codigo_per As Integer)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim list As New List(Of Dictionary(Of String, Object))()

            Dim obj As New ClsGestionInvestigacion
            Dim dt As New Data.DataTable
            dt = obj.ConsultarDatosDocenteInvestigador(codigo_per)

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod_inv", obj.EncrytedString64(dt.Rows(i).Item("codigo_inv")))
                    data.Add("cod_per", obj.EncrytedString64(dt.Rows(i).Item("codigo_per")))
                    data.Add("nombre_per", dt.Rows(i).Item("nombre_per"))
                    data.Add("urldina_inv", dt.Rows(i).Item("urldina_inv"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("msje", ex.Message)
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub
    ' Docentes por Departamento Academico (TODOS : %)
    Private Sub ConsultarPersonalxDepAcademico(ByVal codigo_dac As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ConsultarPersonalxDepartamentoAcademico(codigo_dac)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                data.Add("nombre", dt.Rows(i).Item("descripcion"))
                'data.Add("dina", dt.Rows(i).Item("urldina"))
                'data.Add("inv", obj.EncrytedString64(dt.Rows(i).Item("codigo_inv")))
                list.Add(data)
            Next
            'Dim data1 As New Dictionary(Of String, Object)()
            'data1.Add("cod", obj.EncrytedString64("T"))
            'data1.Add("nombre", "TODOS")
            'list.Add(data1)
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub



    ' Docentes por Departamento Academico (TODOS : %)
    Private Sub ListarBaseDatosRevista()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListarBaseDeDatosRevista()

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", dt.Rows(i).Item("codigo_bdr"))
                data.Add("nombre", dt.Rows(i).Item("descripcion_bdr"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
End Class
