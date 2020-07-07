Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class Investigacion
    Private _strCadena As String = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    Private _strNombreDIrInv As String = "Victor Alvitres Castillo"
    Private _strMailDirInv As String = "valvitres@usat.edu.pe"

    Public Function NuevaInvestigacion(ByVal tipo As String, ByVal titulo_inv As String, ByVal codigo_eti As Int16, _
    ByVal codigo_tem As Int16, ByVal estado_inv As Int16, ByVal archivo As FileUpload, ByVal duracion As String, ByVal tipoduracion As String, _
    ByVal ambito As String, ByVal poblacion As String, ByVal detallezona As String, ByVal ruta As String, ByVal codigo_per As String) As String

        Dim obj As New ClsSqlServer(_strCadena)
        'Dim codigo_lip As Integer   'El codigo de la linea de investigacion
        Dim retorno As Integer      'El codigo de la investigacion
        Try
            obj.IniciarTransaccion()
            'En este procedimiento agrego la investigacion
            retorno = obj.Ejecutar("AgregarInvestigaciones", tipo, titulo_inv, codigo_eti, codigo_tem, estado_inv, duracion, tipoduracion, ambito, poblacion, detallezona, 0)

            'En este procedimiento creo la carpeta y el archivo de investigacion
            Dim Carpeta As New System.IO.DirectoryInfo(ruta & "\" & retorno)
            Dim nombreArchivo As String
            nombreArchivo = retorno & "proyecto" & System.IO.Path.GetExtension(archivo.FileName).ToLower()

            If Carpeta.Exists = False Then
                Carpeta.Create()
            End If

            If archivo.HasFile Then
                archivo.PostedFile.SaveAs(ruta & "\" & retorno & "\" & nombreArchivo)
            End If

            'En este procedimiento modifico la ruta de la investigacion
            obj.Ejecutar("ModificarRutaInvestigaciones", "1", retorno, nombreArchivo, Now.Date)

            'En este procedimiento consulto la linea de personal del autor
            'codigo_lip = obj.TraerValor("ConsultarRetornarLineaPersonal", codigo_tem, CInt(codigo_per), 0)
            'En este procedimiento agrego al responsable como autor cuando pertenece a una linea de investigacion
            'obj.Ejecutar("AgregarResponsable", "1", codigo_lip, 0, "", "", "", "", retorno, 1)

            'En este procedimiento agrego al responsable como autor cuando no esta relacionado a una linea de investigacion
            obj.Ejecutar("AgregarResponsable", "2", codigo_per, 0, "", "", "", "", retorno, 1)

            ' ####################################################################
            ' Generando las consultas para saber a que DIrector tengo que enviar el MAIL
            ' ####################################################################
            Dim Datos As New Data.DataTable
            Dim objMail As New ClsMail
            Dim nombreautor As String
            Dim nombredirector As String
            Dim maildirector As String
            Dim objInv As New Investigacion

            Datos = obj.TraerDataTable("ConsultarPersonal", "PE", codigo_per)
            nombreautor = Datos.Rows(0).Item("nombres") & " " & Datos.Rows(0).Item("paterno") & "  " & Datos.Rows(0).Item("materno")
            Datos.Dispose()

            Datos = obj.TraerDataTable("INV_ConsultarDirector", codigo_tem)
            If Datos.Rows.Count > 0 Then
                If CInt(Datos.Rows(0).Item("codigo_per")) = codigo_per Then
                    'Dim mensaje As String
                    'mensaje = Me.CambiarestadoInvestigacion(retorno, 3, codigo_per, 1)
                    obj.Ejecutar("ModificarEstadoInvestigacion", "1", retorno, 3)
                    '-- objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES", _strMailDirInv, "REVISION DE PERFIL", MensajeInvestigacion(5, Me._strNombreDIrInv, nombreautor, titulo_inv), True)
                Else
                    nombredirector = Datos.Rows(0).Item("director")
                    maildirector = Datos.Rows(0).Item("usuario_per").ToString.Trim & "@usat.edu.pe"
                    '-- objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES", maildirector, "NUEVA INVESTIGACION", MensajeInvestigacion(1, nombredirector, nombreautor, titulo_inv), True)
                End If
            End If

            obj.TerminarTransaccion()
            Return retorno.ToString     'Exito en el ingreso de los datos, devuelvo el numero de investigacion
        Catch ex As Exception
            obj.AbortarTransaccion()
            Return ex.Message    'Ocurrio un error al insertar el registro
        End Try

    End Function
    '14-07-10
    Public Function NuevaInvestigacionOtros(ByVal tipo As String, ByVal titulo_inv As String, ByVal fechaini As DateTime, _
        ByVal codigo_eti As Int16, ByVal codigo_tem As Int16, ByVal estado_inv As Int16, ByVal fecha As DateTime, ByVal archivo1 As FileUpload, _
        ByVal archivo2 As FileUpload, ByVal ruta As String, ByVal institucion As String, ByVal codigo_per As String, ByVal duracion As Integer, ByVal tipoduracion As String, _
        ByVal ambito As String, ByVal poblacion As String, ByVal detallezona As String, ByVal citapublicacion As String) As Integer

        'Dim codigo_lip As Integer           'El codigo de la linea de investigacion
        Dim retorno As Integer              'El codigo de la investigacion
        Dim ObjInv As New ClsSqlServer(_strCadena)
        Try
            Dim nombreArchivo1, nombreArchivo2 As String

            ObjInv.IniciarTransaccion()
            ' ####################################################################
            ' Agregando una investigacion y devolviendo su codigo de investigacion
            ' ####################################################################
            retorno = ObjInv.Ejecutar("AgregarInvestigacionesExternas", "1", titulo_inv, fechaini, codigo_eti, -1, _
            estado_inv, fecha, institucion, duracion, tipoduracion, ambito, poblacion, detallezona, citapublicacion, 0)

            ' ####################################################################
            ' Creando una carpeta y agregando los archivos de Informe y Resumen
            ' ####################################################################
            Dim Carpeta As New System.IO.DirectoryInfo(ruta & "\" & retorno)
            nombreArchivo1 = retorno & "informe" & System.IO.Path.GetExtension(archivo1.FileName).ToLower()
            If Carpeta.Exists = False Then : Carpeta.Create() : End If
            If archivo1.HasFile Then : archivo1.PostedFile.SaveAs(ruta & "\" & retorno & "\" & nombreArchivo1) : End If

            nombreArchivo2 = retorno & "resumen" & System.IO.Path.GetExtension(archivo2.FileName).ToLower()
            If archivo2.HasFile Then : archivo2.PostedFile.SaveAs(ruta & "\" & retorno & "\" & nombreArchivo2) : End If

            ' ####################################################################
            ' Modificar las rutas de los archivos que se suben.
            ' ####################################################################
            ObjInv.Ejecutar("ModificarRutaInvestigaciones", 3, retorno, nombreArchivo1, Now.Date)
            ObjInv.Ejecutar("ModificarRutaInvestigaciones", 4, retorno, nombreArchivo2, Now.Date)

            ' ####################################################################
            ' Consulto la linea de investigacion
            ' ####################################################################
            'If codigo_tem <> 0 Then
            ' codigo_lip = ObjInv.TraerValor("ConsultarRetornarLineaPersonal", "1", codigo_tem, codigo_per, 0)
            'End If

            ' ####################################################################
            ' Agregando los Responsables de la investigacion.
            ' ####################################################################
            ObjInv.Ejecutar("AgregarResponsable", "2", codigo_per, 0, "", "", "", "", retorno, 1)

            ObjInv.TerminarTransaccion()

            Return retorno              'Exito en el ingreso de los datos.
        Catch ex As Exception
            ObjInv.AbortarTransaccion()
            Return -1                  'Ocurrio un error al insertar el registro
        End Try

    End Function
    Public Function NuevoProyecto(ByVal tipo As String, ByVal titulo_inv As String, ByVal codigo_eti As Int16, _
    ByVal codigo_tem As Int16, ByVal estado_inv As Int16, ByVal archivo As FileUpload, ByVal duracion As String, ByVal tipoduracion As String, _
    ByVal ambito As String, ByVal poblacion As String, ByVal detallezona As String, ByVal costo As String, ByVal financ As String, _
    ByVal ruta As String, ByVal codigo_per As String) As String

        Dim obj As New ClsSqlServer(_strCadena)
        'Dim codigo_lip As Integer  'El codigo de la linea de investigacion
        Dim retorno As Integer      'El codigo de la investigacion

        Try
            obj.IniciarTransaccion()

            'En este procedimiento agrego el proyecto si perfil
            retorno = obj.Ejecutar("AgregarProyectosp", tipo, titulo_inv, codigo_eti, codigo_tem, estado_inv, duracion, tipoduracion, ambito, poblacion, detallezona, costo, financ, 0)
            'En este procedimiento creo la carpeta y el archivo de investigacion
            Dim Carpeta As New System.IO.DirectoryInfo(ruta & "\" & retorno)
            Dim nombreArchivo As String
            nombreArchivo = retorno & "proyecto" & System.IO.Path.GetExtension(archivo.FileName).ToLower()

            If Carpeta.Exists = False Then
                Carpeta.Create()
            End If

            If archivo.HasFile Then
                archivo.PostedFile.SaveAs(ruta & "\" & retorno & "\" & nombreArchivo)
            End If

            'En este procedimiento modifico la ruta de la investigacion
            obj.Ejecutar("ModificarRutaInvestigaciones", "2", retorno, nombreArchivo, Now.Date)

            'En este procedimiento consulto la linea de personal del autor
            'codigo_lip = obj.TraerValor("ConsultarRetornarLineaPersonal", codigo_tem, CInt(codigo_per), 0)
            'En este procedimiento agrego al responsable como autor cuando pertenece a una linea de investigacion
            'obj.Ejecutar("AgregarResponsable", "1", codigo_lip, 0, "", "", "", "", retorno, 1)

            'En este procedimiento agrego al responsable como autor cuando no esta relacionado a una linea de investigacion
            obj.Ejecutar("AgregarResponsable", "2", codigo_per, 0, "", "", "", "", retorno, 1)

            ' ####################################################################
            ' Generando las consultas para saber a que DIrector tengo que enviar el MAIL
            ' ####################################################################
            Dim Datos As New Data.DataTable
            Dim objMail As New ClsMail
            Dim nombreautor As String
            Dim nombredirector As String
            Dim maildirector As String
            Dim objInv As New Investigacion

            Datos = obj.TraerDataTable("ConsultarPersonal", "PE", codigo_per)
            nombreautor = Datos.Rows(0).Item("nombres") & " " & Datos.Rows(0).Item("paterno") & "  " & Datos.Rows(0).Item("materno")
            Datos.Dispose()

            Datos = obj.TraerDataTable("INV_ConsultarDirector", codigo_tem)
            If Datos.Rows.Count > 0 Then
                If CInt(Datos.Rows(0).Item("codigo_per")) = codigo_per Then
                    'Dim mensaje As String
                    'mensaje = Me.CambiarestadoInvestigacion(retorno, 3, codigo_per, 1)

                    obj.Ejecutar("ModificarEstadoInvestigacion", "3", retorno, 3)
                    '-- objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES", _strMailDirInv, "REVISION DE PERFIL", MensajeInvestigacion(5, Me._strNombreDIrInv, nombreautor, titulo_inv), True)
                Else
                    nombredirector = Datos.Rows(0).Item("director")
                    maildirector = Datos.Rows(0).Item("usuario_per").ToString.Trim & "@usat.edu.pe"
                    '-- objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES", maildirector, "NUEVA INVESTIGACION", MensajeInvestigacion(1, nombredirector, nombreautor, titulo_inv), True)
                End If
            End If

            obj.TerminarTransaccion()
            Return retorno.ToString     'Exito en el ingreso de los datos, devuelvo el numero de investigacion

        Catch ex As Exception
            obj.AbortarTransaccion()
            Return ex.Message    'Ocurrio un error al insertar el registro
        End Try

    End Function

    Public Function ModificarInvestigacion(ByVal codigo_inv As Integer, ByVal titulo_inv As String, _
     ByVal codigo_eti As Int16, ByVal estado_inv As Int16, ByVal archivo As FileUpload, _
     ByVal ruta As String, ByVal tipo_finan As String, ByVal costo_inv As Double, ByVal codigo_per As Integer, _
     ByVal codigo_are As Integer, ByVal duracion_inv As Integer, ByVal tipoduracion_inv As String, ByVal ambito_inv As String, _
     ByVal poblacion_inv As String, ByVal detallezona_inv As String, Optional ByVal DescripcionFinan As String = "") As Integer

        Dim ObjModificar As New ClsSqlServer(_strCadena)
        Dim nombreArchivo As String
        Dim objInv As New Investigacion

        Try
            ObjModificar.IniciarTransaccion()
            If codigo_eti = 1 Then
                ObjModificar.Ejecutar("ModificarInvestigaciones", 1, codigo_inv, titulo_inv, 1, 1, 0.0, "0", Now(), "", codigo_are, duracion_inv, _
                tipoduracion_inv, ambito_inv, poblacion_inv, detallezona_inv)
                nombreArchivo = codigo_inv.ToString & "perfil" & System.IO.Path.GetExtension(archivo.FileName).ToLower()
                If archivo.HasFile Then
                    archivo.PostedFile.SaveAs(ruta & "\" & codigo_inv & "\" & nombreArchivo)
                End If
                ObjModificar.Ejecutar("ModificarRutaInvestigaciones", 1, codigo_inv, nombreArchivo, Now)
                ' ####################################################################
                ' Generando las consultas para saber a que DIrector tengo que enviar el MAIL
                ' ####################################################################
                Dim Datos As New Data.DataTable
                Dim objMail As New ClsMail
                Dim nombreautor As String
                Dim nombredirector As String
                Dim maildirector As String


                Datos = ObjModificar.TraerDataTable("ConsultarPersonal", "PE", codigo_per)
                nombreautor = Datos.Rows(0).Item("nombres") & " " & Datos.Rows(0).Item("paterno") & "  " & Datos.Rows(0).Item("materno")
                Datos.Dispose()

                Datos = ObjModificar.TraerDataTable("INV_ConsultarDirector", codigo_are)
                If Datos.Rows.Count > 0 Then
                    If CInt(Datos.Rows(0).Item("codigo_per")) = codigo_per Then
                        'Dim mensaje As String
                        'mensaje = Me.CambiarestadoInvestigacion(retorno, 3, codigo_per, 1)
                        ObjModificar.Ejecutar("ModificarEstadoInvestigacion", "1", codigo_inv, 3)
                        '-- objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES", _strMailDirInv, "INVESTIGACION MODIFICADA", MensajeInvestigacion(5, Me._strNombreDIrInv, nombreautor, titulo_inv), True)
                    Else
                        nombredirector = Datos.Rows(0).Item("director")
                        maildirector = Datos.Rows(0).Item("usuario_per").ToString.Trim & "@usat.edu.pe"
                        '-- objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES", maildirector, "NUEVA INVESTIGACION", MensajeInvestigacion(2, nombredirector, nombreautor, titulo_inv), True)
                    End If
                End If


                'Datos = ObjModificar.TraerDataTable("INV_ConsultarDirector", codigo_per)
                'nombredirector = Datos.Rows(0).Item("director")
                'maildirector = Datos.Rows(0).Item("usuario_per").ToString.Trim & "@usat.edu.pe"
                'objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES ", maildirector, "INVESTIGACION MODIFICADA", objMail.MensajeInvestigacion(2, nombredirector, nombreautor, titulo_inv), True)


            ElseIf codigo_eti = 2 Then
                ObjModificar.Ejecutar("ModificarInvestigaciones", 2, codigo_inv, "", 0, 0, costo_inv, tipo_finan, Now, DescripcionFinan, 0, 0, "", "", "", "")
                nombreArchivo = codigo_inv.ToString & "proyecto" & System.IO.Path.GetExtension(archivo.FileName).ToLower()
                If archivo.HasFile Then
                    archivo.PostedFile.SaveAs(ruta & "\" & codigo_inv & "\" & nombreArchivo)
                End If

                ObjModificar.Ejecutar("ModificarRutaInvestigaciones", 2, codigo_inv, nombreArchivo, Now)
                ' ####################################################################
                ' Generando las consultas para saber a que DIrector tengo que enviar el MAIL
                ' ####################################################################
                Dim Datos As New Data.DataTable
                Dim objMail As New ClsMail
                Dim nombreautor As String
                'Dim nombredirector As String
                'Dim maildirector As String

                Datos = ObjModificar.TraerDataTable("ConsultarPersonal", "PE", codigo_per)
                nombreautor = Datos.Rows(0).Item("nombres") & " " & Datos.Rows(0).Item("paterno") & "  " & Datos.Rows(0).Item("materno")
                Datos.Dispose()

                Datos = ObjModificar.TraerDataTable("ConsultarInvestigaciones2", 12, codigo_inv)
                titulo_inv = Datos.Rows(0).Item("titulo_inv").ToString
                Datos.Dispose()

                'Datos = ObjModificar.TraerDataTable("INV_ConsultarDirector", codigo_per)
                'nombredirector = Datos.Rows(0).Item("director")
                'maildirector = Datos.Rows(0).Item("usuario_per").ToString.Trim & "@usat.edu.pe"
                '-- objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES ", _strMailDirInv, "REVISION DE INVESTIGACION", MensajeInvestigacion(3, _strNombreDIrInv, nombreautor, titulo_inv), True)
            End If

            ObjModificar.TerminarTransaccion()
            Return codigo_inv
        Catch ex As Exception
            ObjModificar.AbortarTransaccion()
            Return -1
        End Try

    End Function

    Public Function RegistrarAvances(ByVal codigo_inv As Integer, ByVal archivo As FileUpload, ByVal strruta As String, ByVal codigo_per As Integer) As Int16
        Dim ObjInv As New ClsSqlServer(_strCadena)
        Dim numero As Integer
        Dim nomArchivo As String

        Try
            numero = ObjInv.TraerValor("ConsultarAvances2", 1, codigo_inv, 0)
            nomArchivo = codigo_inv.ToString & "Avance" & numero.ToString & System.IO.Path.GetExtension(archivo.FileName).ToLower()

            If archivo.HasFile Then
                archivo.PostedFile.SaveAs(strruta & "\" & codigo_inv & "\" & nomArchivo)
            End If

            ObjInv.IniciarTransaccion()
            ObjInv.Ejecutar("AgregarAvances", 1, codigo_inv, nomArchivo)

            Dim Datos As New Data.DataTable
            Dim objMail As New ClsMail
            Dim nombreautor As String
            'Dim nombredirector As String
            'Dim maildirector As String
            Dim titulo_inv As String

            Datos = ObjInv.TraerDataTable("ConsultarPersonal", "PE", codigo_per)
            nombreautor = Datos.Rows(0).Item("nombres") & " " & Datos.Rows(0).Item("paterno") & "  " & Datos.Rows(0).Item("materno")
            Datos.Dispose()

            Datos = ObjInv.TraerDataTable("ConsultarInvestigaciones2", 12, codigo_inv)
            titulo_inv = Datos.Rows(0).Item("titulo_inv").ToString
            Datos.Dispose()

            'Datos = ObjInv.TraerDataTable("INV_ConsultarDirector", codigo_per)
            'nombredirector = Datos.Rows(0).Item("director")
            'maildirector = Datos.Rows(0).Item("usuario_per").ToString.Trim & "@usat.edu.pe"
            '-- objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES ", _strMailDirInv, "REVISION DE INVESTIGACION", MensajeInvestigacion(7, _strNombreDIrInv, nombreautor, titulo_inv), True)

            ObjInv.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            ObjInv.AbortarTransaccion()
            Return -1
        End Try

    End Function

    Public Function AgregarAreas(ByVal nombre As String, ByVal codigo As Integer, ByVal proposito As String, ByVal codigo_pcc As Integer) As Int16
        Dim obj As New ClsSqlServer(_strCadena)
        Try
            obj.IniciarTransaccion()
            obj.Ejecutar("AgregarAreaInvestigacion", nombre, proposito, codigo, codigo_pcc)
            obj.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            obj.AbortarTransaccion()
            Return -1
        End Try

    End Function

    Public Function ModificarAreas(ByVal nombre As String, ByVal codigo As Integer, ByVal proposito As String, ByVal codigo_pcc As Integer) As Int16
        Dim obj As New ClsSqlServer(_strCadena)
        Try
            obj.IniciarTransaccion()
            obj.Ejecutar("ModificarAreaInvestigacion", codigo, nombre, proposito, codigo_pcc)
            obj.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            obj.AbortarTransaccion()
            Return -1

        End Try
    End Function

    Public Function EliminarArea(ByVal codigo_are As Integer) As Integer
        Dim ObjInv As New ClsSqlServer(_strCadena)
        Try
            Dim valordevuelto As Int16
            ObjInv.IniciarTransaccion()
            valordevuelto = ObjInv.Ejecutar("EliminarAreaInvestigacion", codigo_are, 0)
            ObjInv.TerminarTransaccion()
            Return valordevuelto
        Catch ex As Exception
            ObjInv.AbortarTransaccion()
            Return -1
        End Try
        ObjInv = Nothing
    End Function

    Public Function EliminarLinea(ByVal codigo_lin As Integer) As Integer
        Dim Objinv As New ClsSqlServer(_strCadena)
        Try
            Objinv.IniciarTransaccion()
            Objinv.Ejecutar("EliminarLineaInvestigacion", codigo_lin)
            Objinv.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            Objinv.AbortarTransaccion()
            Return -1
        End Try
        Objinv = Nothing
    End Function

    Public Function EliminarTematica(ByVal codigo_tem As Integer) As Integer
        Dim objinv As New ClsSqlServer(_strCadena)
        Try
            objinv.IniciarTransaccion()
            objinv.Ejecutar("EliminarTematicaInvestigacion", codigo_tem)
            objinv.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            objinv.AbortarTransaccion()
            Return -1
        End Try
        objinv = Nothing
    End Function

    Public Function AgregarLineas(ByVal nombre As String, ByVal proposito As String, _
    ByVal codigo_cco As Integer, ByVal codigo_ref As Integer, ByVal nivel_are As Integer) As Int16
        Dim ObjInv As New ClsSqlServer(_strCadena)
        Try
            ObjInv.IniciarTransaccion()
            'ObjInv.Ejecutar("AgregarLineaInvestigacion", nombre, proposito, codigo)
            ObjInv.Ejecutar("INV_AgregarLinea", nombre, proposito, codigo_cco, codigo_ref, nivel_are)
            ObjInv.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            ObjInv.AbortarTransaccion()
            Return -1
        End Try
    End Function

    Public Function ModificarLineas(ByVal nombre As String, ByVal proposito As String, ByVal codigo_are As Integer) As Int16
        Dim objinv As New ClsSqlServer(_strCadena)
        Try
            objinv.IniciarTransaccion()
            objinv.Ejecutar("ModificarAreaInvestigacion", codigo_are, nombre, proposito)
            objinv.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            objinv.AbortarTransaccion()
            Return -1
        End Try
    End Function

    Public Function AgregarTematicas(ByVal nombre As String, ByVal proposito As String, ByVal codigo As Integer) As Int16
        Dim ObjInv As New ClsSqlServer(_strCadena)
        Try
            ObjInv.IniciarTransaccion()
            ObjInv.Ejecutar("AgregarTematicaInvestigacion", nombre, proposito, codigo)
            ObjInv.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            ObjInv.AbortarTransaccion()
            Return -1
        End Try
    End Function

    Public Function ModificarTematicas(ByVal nombre As String, ByVal proposito As String, ByVal codigo As Integer) As Int16
        Dim objinv As New ClsSqlServer(_strCadena)
        Try
            objinv.IniciarTransaccion()
            objinv.Ejecutar("ModificarTematicaInvestigacion", codigo, nombre, proposito)
            objinv.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            objinv.AbortarTransaccion()
            Return -1
        End Try
    End Function

    Public Function RegistrarInformes(ByVal codigo_inv As Integer, ByVal archivo As FileUpload, ByVal strRuta As String, ByVal codigo_per As Integer) As Integer
        Dim objinv As New ClsSqlServer(_strCadena)
        Dim nomArchivo As String
        Try
            nomArchivo = codigo_inv.ToString & "Informe" & System.IO.Path.GetExtension(archivo.FileName).ToLower()

            If archivo.HasFile Then
                archivo.PostedFile.SaveAs(strRuta & "\" & codigo_inv & "\" & nomArchivo)
            End If
            objinv.IniciarTransaccion()
            objinv.Ejecutar("ModificarRutaInvestigaciones", 3, codigo_inv, nomArchivo, Now)

            Dim Datos As New Data.DataTable
            Dim objMail As New ClsMail
            Dim nombreautor As String
            'Dim nombredirector As String
            'Dim maildirector As String
            Dim titulo_inv As String

            Datos = objinv.TraerDataTable("ConsultarPersonal", "PE", codigo_per)
            nombreautor = Datos.Rows(0).Item("nombres") & " " & Datos.Rows(0).Item("paterno") & "  " & Datos.Rows(0).Item("materno")
            Datos.Dispose()

            Datos = objinv.TraerDataTable("ConsultarInvestigaciones2", 12, codigo_inv)
            titulo_inv = Datos.Rows(0).Item("titulo_inv").ToString
            Datos.Dispose()

            'Datos = objinv.TraerDataTable("INV_ConsultarDirector", codigo_per)
            'nombredirector = Datos.Rows(0).Item("director")
            'maildirector = Datos.Rows(0).Item("usuario_per").ToString.Trim & "@usat.edu.pe"
            '-- objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES ", _strMailDirInv, "REVISION DE INVESTIGACION", MensajeInvestigacion(8, _strNombreDIrInv, nombreautor, titulo_inv), True)

            objinv.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            objinv.AbortarTransaccion()
            Return -1
        End Try
    End Function

    Public Function RegistrarResumen(ByVal codigo_inv As Integer, ByVal archivo As FileUpload, ByVal strRuta As String, ByVal codigo_per As Integer) As Integer
        Dim objinv As New ClsSqlServer(_strCadena)
        Dim nomArchivo As String
        Try
            nomArchivo = codigo_inv.ToString & "Resumen" & System.IO.Path.GetExtension(archivo.FileName).ToLower()

            If archivo.HasFile Then
                archivo.PostedFile.SaveAs(strRuta & "\" & codigo_inv & "\" & nomArchivo)
            End If
            objinv.IniciarTransaccion()
            objinv.Ejecutar("ModificarRutaInvestigaciones", 4, codigo_inv, nomArchivo, Now)

            'Dim Datos As New Data.DataTable
            'Dim objMail As New ClsMail
            'Dim nombreautor As String
            'Dim nombredirector As String
            'Dim maildirector As String
            'Dim titulo_inv As String

            'Datos = objinv.TraerDataTable("ConsultarPersonal", "PE", codigo_per)
            'nombreautor = Datos.Rows(0).Item("nombres") & " " & Datos.Rows(0).Item("paterno") & "  " & Datos.Rows(0).Item("materno")
            'Datos.Dispose()

            'Datos = objinv.TraerDataTable("ConsultarInvestigaciones2", 12, codigo_inv)
            'titulo_inv = Datos.Rows(0).Item("titulo_inv").ToString
            'Datos.Dispose()

            'Datos = objinv.TraerDataTable("INV_ConsultarDirector", codigo_per)
            'nombredirector = Datos.Rows(0).Item("director")
            'maildirector = Datos.Rows(0).Item("usuario_per").ToString.Trim & "@usat.edu.pe"
            'objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES ", maildirector, "REVISION DE INVESTIGACION", objMail.MensajeInvestigacion(10, nombredirector, nombreautor, titulo_inv), True)
            objinv.TerminarTransaccion()

            Return 1
        Catch ex As Exception
            objinv.AbortarTransaccion()
            Return -1
        End Try
    End Function

    Public Function AgregarObservaciones(ByVal tipo As String, ByVal asunto As String, ByVal observacion As String, ByVal codigo_per As Integer, ByVal codigo_inv As Integer) As Integer
        Dim Obj As New ClsSqlServer(_strCadena)
        Try
            'Obj.IniciarTransaccion()
            Obj.Ejecutar("AgregarObservaciones", tipo, asunto, observacion, codigo_per, codigo_inv)
            'Obj.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            'Obj.AbortarTransaccion()
            Return -1
        End Try
        Obj = Nothing
    End Function

    Public Function AgregarResponsable(ByVal tipo As String, ByVal codigo_lip As Integer, ByVal codigo_alu As Integer, ByVal nombre As String, _
    ByVal paterno As String, ByVal materno As String, ByVal centrolab As String, ByVal codigo_inv As Integer, ByVal codigo_tpi As Integer) As Integer
        Dim obj As New ClsSqlServer(_strCadena)
        Try
            obj.IniciarTransaccion()
            obj.Ejecutar("AgregarResponsable", tipo, codigo_lip, codigo_alu, nombre, paterno, materno, centrolab, codigo_inv, codigo_tpi)
            obj.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            obj.AbortarTransaccion()
            Return -1
        End Try
        obj = Nothing
    End Function


    Public Function AgregarIvestigacionRevisor(ByVal Lista As SortedList, ByVal codigo_rev As Integer)
        Dim ObjINv As New ClsSqlServer(_strCadena)
        Try
            ObjINv.IniciarTransaccion()
            For i As Int32 = 0 To Lista.Count - 1
                ObjINv.Ejecutar("INV_AsignarInvestigacionRevisor", codigo_rev, Lista(i))
            Next
            ObjINv.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            ObjINv.TerminarTransaccion()
            Return -1
        End Try
        ObjINv = Nothing
    End Function

    Public Function RetirarInvestigacionRevisor(ByVal Lista As SortedList, ByVal codigo_rev As Integer)
        Dim ObjInv As New ClsSqlServer(_strCadena)
        Try
            ObjInv.IniciarTransaccion()
            For i As Int32 = 0 To Lista.Count - 1
                ObjInv.Ejecutar("INV_EliminarInvestigacionRevisor", codigo_rev, Lista(i))
            Next
            ObjInv.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            ObjInv.AbortarTransaccion()
            Return -1
        End Try
    End Function

    Public Function AgregarPersonalALinea(ByVal Lista As SortedList, ByVal codigo_lin As Integer)
        Dim objinv As New ClsSqlServer(_strCadena)
        Try
            objinv.IniciarTransaccion()
            For i As Int32 = 0 To Lista.Count - 1
                objinv.Ejecutar("AgregarLineaInvestigacionPersonal", Lista(i), codigo_lin)
            Next
            objinv.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            objinv.AbortarTransaccion()
            Return -1
        End Try
        objinv = Nothing
    End Function

    Public Function RetirarPersonaldelinea(ByVal Lista As SortedList, ByVal codigo_lin As Integer)
        Dim objinv As New ClsSqlServer(_strCadena)
        Try
            objinv.IniciarTransaccion()
            For i As Int32 = 0 To Lista.Count - 1
                objinv.Ejecutar("ModificarLineaInvestigacionPersonal", "1", Lista(i), codigo_lin)
            Next
            objinv.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            objinv.AbortarTransaccion()
            Return -1
        End Try
        objinv = Nothing
    End Function

    Public Function ModificarUnidadInvestigacion(ByVal tipo As Integer, ByVal codigo_cco As Integer)
        Dim ObjInv As New ClsSqlServer(_strCadena)
        Try
            ObjInv.IniciarTransaccion()
            ObjInv.Ejecutar("ModificarUnidadInvestigacion", tipo, codigo_cco)
            ObjInv.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            ObjInv.AbortarTransaccion()
            Return -1
        End Try
    End Function

    Public Function CambiarestadoInvestigacion(ByVal Codigo_inv As Integer, ByVal Estado As Integer, ByVal codigo_per As Integer, Optional ByVal tipo As Integer = 0)
        Dim ObjInv As New ClsSqlServer(_strCadena)
        Try
            ObjInv.IniciarTransaccion()
            ObjInv.Ejecutar("ModificarEstadoInvestigacion", "1", Codigo_inv, Estado)

            Select Case Estado
                Case 7
                    Dim Datos As New Data.DataTable
                    Dim objMail As New ClsMail
                    Dim nombreautor As String
                    Dim mailautor As String
                    Datos = ObjInv.TraerDataTable("ConsultarInvestigaciones", 8, Codigo_inv)
                    nombreautor = Datos.Rows(0).Item("datos_per")
                    mailautor = Datos.Rows(0).Item("usuario_per").ToString.Trim & "@usat.edu.pe"
                    Datos.Dispose()
                    If tipo = 1 Then
                        '-- objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES", mailautor, "REVISION DE INVESTIGACION", MensajeInvestigacion(4, nombreautor, "", ""), True)
                    ElseIf tipo = 3 Then
                        '-- objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES", mailautor, "REVISION DE INVESTIGACION", MensajeInvestigacion(9, nombreautor, "", ""), True)
                    End If

                Case 3
                    Dim Datos As New Data.DataTable
                    Dim objMail As New ClsMail
                    Dim nombreautor As String
                    Dim titulo_inv As String
                    Datos = ObjInv.TraerDataTable("ConsultarInvestigaciones", 8, Codigo_inv)
                    nombreautor = Datos.Rows(0).Item("datos_per")
                    Datos.Dispose()
                    Datos = ObjInv.TraerDataTable("ConsultarInvestigaciones2", 12, Codigo_inv)
                    titulo_inv = Datos.Rows(0).Item("titulo_inv").ToString
                    '-- objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES", _strMailDirInv, "REVISION DE PERFIL", MensajeInvestigacion(5, Me._strNombreDIrInv, nombreautor, titulo_inv), True)

                Case 8
                    Dim Datos As New Data.DataTable
                    Dim objMail As New ClsMail
                    Dim nombreautor As String
                    Dim mailautor As String
                    Datos = ObjInv.TraerDataTable("ConsultarInvestigaciones", 8, Codigo_inv)
                    nombreautor = Datos.Rows(0).Item("datos_per")
                    mailautor = Datos.Rows(0).Item("usuario_per").ToString.Trim & "@usat.edu.pe"
                    Datos.Dispose()
                    '-- objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES", mailautor, "REVISION DE INVESTIGACION", MensajeInvestigacion(11, nombreautor, "", ""), True)

                Case 4
                    Dim Datos As New Data.DataTable
                    Dim objMail As New ClsMail
                    Dim nombreautor As String
                    Dim mailautor As String
                    Datos = ObjInv.TraerDataTable("ConsultarInvestigaciones", 8, Codigo_inv)
                    nombreautor = Datos.Rows(0).Item("datos_per")
                    mailautor = Datos.Rows(0).Item("usuario_per").ToString.Trim & "@usat.edu.pe"
                    Datos.Dispose()
                    '-- objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES", mailautor, "REVISION DE INVESTIGACION", MensajeInvestigacion(12, nombreautor, "", ""), True)

                Case 2
                    Dim Datos As New Data.DataTable
                    Dim objMail As New ClsMail
                    Dim nombreautor As String
                    Dim mailautor As String
                    Datos = ObjInv.TraerDataTable("ConsultarInvestigaciones", 8, Codigo_inv)
                    nombreautor = Datos.Rows(0).Item("datos_per")
                    mailautor = Datos.Rows(0).Item("usuario_per").ToString.Trim & "@usat.edu.pe"
                    Datos.Dispose()
                    '-- objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES", mailautor, "REVISION DE INVESTIGACION", MensajeInvestigacion(13, nombreautor, "", ""), True)

            End Select
            ObjInv.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            ObjInv.AbortarTransaccion()
            Return -1
        End Try
        ObjInv = Nothing
    End Function

    Public Sub EliminarResponsableInv(ByVal codigo_res As Integer)
        Dim obj As New ClsSqlServer(_strCadena)
        Try
            obj.IniciarTransaccion()
            obj.Ejecutar("EliminarResponsableInvestigacion", codigo_res)
            obj.TerminarTransaccion()
        Catch ex As Exception

        End Try
    End Sub

    Public Function ConsultarUnidadesInvestigacion(ByVal tipo As String, ByVal param1 As String) As DataTable
        Dim obj As New ClsSqlServer(_strCadena)
        Try
            Return obj.TraerDataTable("ConsultarUnidadesInvestigacion", tipo, param1)
        Catch ex As Exception
            Return Nothing
        End Try
        obj = Nothing
    End Function

    Public Function ConsultarUnidadesInvestigacion_New(ByVal codigo_ref As String, ByVal codigo_cco As String) As DataTable
        Dim obj As New ClsSqlServer(_strCadena)
        Try
            Return obj.TraerDataTable("INV_ConsultarUnidadesInvestigacion", codigo_ref, codigo_cco)
        Catch ex As Exception
            Return Nothing
        End Try
        obj = Nothing
    End Function

    Public Function ConsultarInvestigaciones(ByVal tipo As String, ByVal param1 As String) As DataTable
        Dim obj As New ClsSqlServer(_strCadena)
        Try
            Return obj.TraerDataTable("ConsultarInvestigaciones", tipo, param1)
        Catch ex As Exception
            Return Nothing
        End Try
        obj = Nothing
    End Function

    Public Function ConsultarInvPorEstado(ByVal tipo As String, ByVal codigo_per As Integer, ByVal estado As Integer, _
    ByVal etapa As Integer, ByVal titulo As String) As DataTable
        Dim obj As New ClsSqlServer(_strCadena)
        Try
            If titulo = "NO" Or titulo Is Nothing Then
                Return obj.TraerDataTable("ConsultarInvestigacionesPorEstado", tipo, codigo_per, etapa, estado, "")
            Else
                If tipo = 5 Then tipo = 6
                Return obj.TraerDataTable("ConsultarInvestigacionesPorEstado", tipo, codigo_per, etapa, estado, titulo)
            End If

        Catch ex As Exception
            Return Nothing
        End Try
        obj = Nothing
    End Function

    Public Function ConsultarInvestigacion(ByVal codigo_inv As String, ByVal tipo As String) As Object
        Dim obj As New ClsSqlServer(_strCadena)
        Try
            Return obj.TraerDataTable("ConsultarInvestigaciones2", tipo, codigo_inv)
        Catch ex As Exception
            Return Nothing
        End Try
        obj = Nothing
    End Function

    Public Function ConsultarAvances(ByVal codigo_inv As String, ByVal tipo As String) As DataTable
        Dim obj As New ClsSqlServer(_strCadena)
        Try
            Return obj.TraerDataTable("ConsultarAvances", tipo, codigo_inv)
        Catch ex As Exception
            Return Nothing
        End Try
        obj = Nothing
    End Function

    Public Function ConsultarPersonalCCInvestigacion(ByVal tipo As String, ByVal param1 As String, ByVal param2 As String) As DataTable
        Dim obj As New ClsSqlServer(_strCadena)
        Try
            Return obj.TraerDataTable("ConsultarPersonalCCInvestigacion", tipo, param1, param2)
        Catch ex As Exception
            Return Nothing
        End Try
        obj = Nothing
    End Function

    Public Function ConsultarPersonaldeLineaInvestigacion(ByVal codigo_cco As Integer, ByVal codigo_lin As Integer, ByVal tipo As Integer) As DataTable
        Dim objInv As New ClsSqlServer(_strCadena)

        Try
            If tipo = 1 Then
                Return objInv.TraerDataTable("ConsultarPersonalDeLineasDeInvestigacion", "1", codigo_lin, "")
            ElseIf tipo = 2 Then
                Return objInv.TraerDataTable("ConsultarPersonalDeLineasDeInvestigacion", "2", codigo_cco, codigo_lin)
            End If
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function ActualizarDecreto(ByVal Codigo_inv As Integer, ByVal Decreto As String, ByVal archivo As FileUpload, ByVal strRuta As String) As Integer
        Dim nomarchivo As String
        nomarchivo = Codigo_inv.ToString & "decreto" & System.IO.Path.GetExtension(archivo.FileName).ToLower()

        If archivo.HasFile Then
            archivo.PostedFile.SaveAs(strRuta & "\" & Codigo_inv & "\" & nomarchivo)
        End If

        Dim ObjInv As New ClsSqlServer(_strCadena)
        Try
            ObjInv.IniciarTransaccion()
            ObjInv.Ejecutar("INV_ActualizarDecreto", Codigo_inv, Decreto)
            ObjInv.Ejecutar("ModificarEstadoInvestigacion", "1", Codigo_inv, 7)

            Dim Datos As New Data.DataTable
            Dim objMail As New ClsMail
            Dim nombreautor As String
            Dim mailautor As String

            Datos = ObjInv.TraerDataTable("ConsultarInvestigaciones", 8, Codigo_inv)
            nombreautor = Datos.Rows(0).Item("datos_per")
            mailautor = Datos.Rows(0).Item("usuario_per").ToString.Trim & "@usat.edu.pe"
            Datos.Dispose()
            '-- objMail.EnviarMail("investigaciones@usat.edu.pe", "SISTEMA DE INVESTIGACIONES", mailautor, "REVISION DE INVESTIGACION", MensajeInvestigacion(6, nombreautor, "", ""), True)

            ObjInv.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            Return -1
        End Try

    End Function

    Public Function MensajeInvestigacion(ByVal tipo As String, ByVal nombredestino As String, ByVal nombreautor As String, ByVal nombreinvestigacion As String) As String

        Dim Mensaje As String
        Mensaje = ""
        Select Case tipo
            Case 1 ' Para una nueva investigación
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Investigaciones pendientes de revisi&oacute;n al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>Ha recibido una nueva "
                Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong>, para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 2
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Investigaciones pendientes de revisi&oacute;n al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>El perfil de la "
                Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido MODIFICADA para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 3
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Investigaciones pendientes de revisi&oacute;n al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>El Proyecto de la "
                Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido ENVIADO para su revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 4
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Revisión de Investigación al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>El perfil de la investigación que a registrado en el SISTEMA DE INVESTIGACIONES - USAT ha sido <B>APROBADO</B> por el Director de Investigación "
                Mensaje = Mensaje + ", verifique en el sistema de INVESTIGACIONES para continuar con el proceso.</span></div></td>"
                'Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido MODIFICADA para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 5 ' Para una nueva investigación
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Investigaciones pendientes de revisi&oacute;n al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>Ha recibido una nueva "
                Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong>, para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 6
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Revisión de Investigación al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>El Proyecto de investigación que a registrado en el SISTEMA DE INVESTIGACIONES - USAT ha sido <B>APROBADO Y EMITIDO SU DECRETO</B> por la Dirección de Investigación"
                Mensaje = Mensaje + ", verifique en el sistema de INVESTIGACIONES para continuar con el proceso.</span></div></td>"
                'Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido MODIFICADA para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 7
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Investigaciones pendientes de revisi&oacute;n al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>Se han registrado nuevos <b>AVANCES</b> de la "
                Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> para su revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 8
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Investigaciones pendientes de revisi&oacute;n al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>Se ha registrado un nuevo <b>INFORME</b> de la "
                Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> para su revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 9
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Revisión de Investigación al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>El Informe de la investigación que a registrado en el SISTEMA DE INVESTIGACIONES - USAT ha sido <B>APROBADO</B> por el Director de Investigación "
                Mensaje = Mensaje + ", verifique en el sistema de INVESTIGACIONES para continuar con el proceso.</span></div></td>"
                'Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido MODIFICADA para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 10
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Revisión de Investigación al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>El Informe de la investigación que a registrado en el SISTEMA DE INVESTIGACIONES - USAT ha sido <B>APROBADO</B> por el Director de Investigación "
                Mensaje = Mensaje + ", verifique en el sistema de INVESTIGACIONES para continuar con el proceso.</span></div></td>"
                'Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido MODIFICADA para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 11
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Revisión de Investigación al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>La investigación que a registrado en el SISTEMA DE INVESTIGACIONES - USAT ha sido <B>DESAPROBADA</B> por la Dirección de Investigación"
                Mensaje = Mensaje + ", verifique en el sistema de INVESTIGACIONES para mas detalle.</span></div></td>"
                'Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido MODIFICADA para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 12
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Revisión de Investigación al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>La investigación que a registrado en el SISTEMA DE INVESTIGACIONES - USAT ha sido <B>DESAPROBADA</B> por su Director de Departamento"
                Mensaje = Mensaje + ", verifique en el sistema de INVESTIGACIONES para mas detalle.</span></div></td>"
                'Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido MODIFICADA para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 13
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Revisión de Investigación al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>La investigación que a registrado en el SISTEMA DE INVESTIGACIONES - USAT ha sido <B>OBSERVADA</B>"
                Mensaje = Mensaje + ", verifique en el sistema de INVESTIGACIONES para mas detalle.</span></div></td>"
                'Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido MODIFICADA para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

        End Select
        Return Mensaje

    End Function

End Class
