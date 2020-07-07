Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data

Partial Class select_ajax
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objCRM As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Try
            Dim k As String = Request("k")
            Dim f As String = ""

            Select Case Request("action")

                Case "cicloacad"
                    f = CInt(Request("f"))
                    ListarCicloAcademico("TO", "")
                Case "depacad"
                    f = CInt(Request("f"))
                    ListarDepartamentoAcademico(0, "")
                Case "depdoc"
                    f = CInt(Request("f"))
                    ListarDepartamentoAcademicoDocente("1", k, f)
                Case "ope"
                    TiposOperacion()
                Case "CCOxPxV"
                    Dim tf As String = objCRM.DecrytedString64(Request("f"))
                    ListarCentroCostoPorPermisoPorVisibilidad(tf, objCRM.DecrytedString64(k))
                Case "CCOevent"
                    Dim cco As String = objCRM.DecrytedString64(Request("f"))
                    BuscarEvento(k, cco)
                Case "moding"
                    Dim modu As String = objCRM.DecrytedString64(Request("mod"))
                    ListarModalidadIngreso("7", modu)
                Case "ubi"
                    ListarUbigeo(Request("p1"), Request("p2"), Request("p3"))

            End Select

        Catch ex As Exception
            Data.Add("msje", ex.Message)
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListarUbigeo(ByVal param1 As String, ByVal param2 As String, ByVal param3 As String)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim objCRM As New ClsCRM
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.ConsultarLugares", param1, param2, param3)
            obj.CerrarConexion()

            For i As Integer = 0 To tb.Rows.Count - 1

                Dim data As New Dictionary(Of String, Object)()
                If param1 = "2" Then
                    data.Add("cod", tb.Rows(i).Item("codigo_Dep"))
                    data.Add("nombre", tb.Rows(i).Item("nombre_Dep"))
                ElseIf param1 = "3" Then
                    data.Add("cod", tb.Rows(i).Item("codigo_Pro"))
                    data.Add("nombre", tb.Rows(i).Item("nombre_Pro"))
                ElseIf param1 = "4" Then
                    data.Add("cod", tb.Rows(i).Item("codigo_Dis"))
                    data.Add("nombre", tb.Rows(i).Item("nombre_Dis"))
                End If

                list.Add(data)


            Next
            objCRM = Nothing
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim list As New List(Of Dictionary(Of String, Object))()


            Dim data As New Dictionary(Of String, Object)()
            data.Add("error", ex.Message)
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub


    Private Sub ListarModalidadIngreso(ByVal tipo As String, ByVal modulo As String)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim objCRM As New ClsCRM
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.EVE_ConsultarInformacionParaEvento", tipo, modulo, 0, 0)
            obj.CerrarConexion()

            For i As Integer = 0 To tb.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", objCRM.EncrytedString64(tb.Rows(i).Item("codigo_min")))
                data.Add("nombre", tb.Rows(i).Item("nombre_min"))
                list.Add(data)
            Next
            objCRM = Nothing
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim list As New List(Of Dictionary(Of String, Object))()


            Dim data As New Dictionary(Of String, Object)()
            data.Add("error", ex.Message)
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub BuscarEvento(ByVal tipo As String, ByVal cco As String)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim objCRM As New ClsCRM
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.EVE_ConsultarEventos", tipo, cco, 0)
            obj.CerrarConexion()


            If tipo = "2" Then
                Dim data As New Dictionary(Of String, Object)()
                'data.Add("cod", objCRM.EncrytedString64(tb.Rows(i).Item("codigo_Cco")))
                data.Add("nombre", tb.Rows(0).Item("nombre_dev"))
                data.Add("nroresolucion", tb.Rows(0).Item("nroresolucion_dev"))
                data.Add("coordinador", tb.Rows(0).Item("coordinador"))
                data.Add("apoyo", tb.Rows(0).Item("apoyo"))
                data.Add("participantes", tb.Rows(0).Item("nroparticipantes_dev"))
                data.Add("preunitcont", tb.Rows(0).Item("preciounitcontado_dev"))
                data.Add("preunifin", tb.Rows(0).Item("preciounitfinanciado_dev"))
                data.Add("montoinicial", tb.Rows(0).Item("montocuotainicial_dev"))
                data.Add("cuotas", tb.Rows(0).Item("nrocuotas_dev"))
                data.Add("porcdctoper", tb.Rows(0).Item("porcentajedescpersonalusat_dev"))
                data.Add("porcdctoalu", tb.Rows(0).Item("porcentajedescalumnousat_dev"))
                data.Add("porcdctocorp", tb.Rows(0).Item("porcentajedesccorportativo_dev"))
                data.Add("porcdctoegr", tb.Rows(0).Item("porcentajedescegresado_dev"))
                If tb.Rows(0).Item("gestionanotas_dev") = True Then
                    data.Add("gestion", "Si")
                    data.Add("gestionnota", True)
                Else
                    data.Add("gestion", "No")
                    data.Add("gestionnota", False)
                End If
                data.Add("horario", tb.Rows(0).Item("horarios_dev"))
                data.Add("obs", tb.Rows(0).Item("obs_dev"))
                data.Add("feciniprop", CDate(tb.Rows(0).Item("fechainiciopropuesta_dev")).ToShortDateString)
                data.Add("fecfinprop", CDate(tb.Rows(0).Item("fechafinpropuesta_dev")).ToShortDateString)
                data.Add("escuela", tb.Rows(0).Item("nombre_cpf"))
                data.Add("cpf", tb.Rows(0).Item("codigo_cpf"))
                list.Add(data)

            ElseIf tipo = "0" Then
                Dim sw As Boolean = True

                Dim data As New Dictionary(Of String, Object)()
                data.Add("codcpf", tb.Rows(0).Item("codigo_cpf"))
                If tb.Rows(0).Item("gestionanotas_dev") = True Then
                    data.Add("gestion", "Si")
                    data.Add("gestionnota", True)
                Else
                    data.Add("gestion", "No")
                    data.Add("gestionnota", False)
                End If
                If IsDBNull(tb.Rows(0).Item("codigo_pes")) Then
                    data.Add("codpes", "")
                Else
                    data.Add("codpes", tb.Rows(0).Item("codigo_pes"))
                End If
                data.Add("codmin", tb.Rows(0).Item("codigo_min"))

                If tb.Rows(0).Item("gestionanotas_dev") = True Then
                    If IsDBNull(tb.Rows(0).Item("codigo_pes")) = True Or tb.Rows(0).Item("codigo_pes").ToString = "" Then
                        sw = False
                    End If
                    data.Add("bloqmin", False)
                Else
                    data.Add("bloqmin", True)
                End If
                data.Add("sw", sw)
                list.Add(data)
            End If

            objCRM = Nothing
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim list As New List(Of Dictionary(Of String, Object))()


            Dim data As New Dictionary(Of String, Object)()
            data.Add("error", ex.Message)
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListarCentroCostoPorPermisoPorVisibilidad(ByVal tipo As String, ByVal param As String)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim objCRM As New ClsCRM
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos


            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.EVE_ConsultarCentroCostosXPermisosXVisibilidad", tipo, Session("id_per"), "", param, 1)
            obj.CerrarConexion()

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", objCRM.EncrytedString64(tb.Rows(i).Item("codigo_Cco")))
                    data.Add("nombre", tb.Rows(i).Item("Nombre"))
                    list.Add(data)
                Next
            End If

            objCRM = Nothing
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim list As New List(Of Dictionary(Of String, Object))()


            Dim data As New Dictionary(Of String, Object)()
            data.Add("error", ex.Message)
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub TiposOperacion()
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim obj As New ClsCRM

            Dim data As New Dictionary(Of String, Object)()
            data.Add("reg", obj.EncrytedString64("Reg")) ' Registrar
            data.Add("mod", obj.EncrytedString64("Mod")) ' Modificar
            data.Add("eli", obj.EncrytedString64("Eli")) ' Eliminar
            data.Add("lst", obj.EncrytedString64("Listar")) ' Listar
            data.Add("bsq", obj.EncrytedString64("Buscar")) ' Buscar
            data.Add("lstFl", obj.EncrytedString64("ListarFiles")) ' Listar Archivos
            data.Add("upl", obj.EncrytedString64("Upload")) ' Subir Archivos 
            data.Add("dwl", obj.EncrytedString64("Download")) ' Descargar Archivos
            data.Add("rpsc", obj.EncrytedString64("RegPerSinC")) ' Registrar persona sin cargo


            Dim dt As New DataTable
            dt = obj.ListaTipoEstudio("TO", 0)

            For i As Integer = 0 To dt.Rows.Count - 1
                data.Add(dt.Rows(i).Item("abreviatura").ToString.ToLower, obj.EncrytedString64(dt.Rows(i).Item("codigo_test").ToString)) ' Tipos de Estudio
            Next
            dt = Nothing

            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ListarCicloAcademico(ByVal tipo As String, ByVal param As String)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.ConsultarCicloAcademico", tipo, param)
            obj.CerrarConexion()

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cCiclo", tb.Rows(i).Item("codigo_Cac"))
                    data.Add("nCiclo", tb.Rows(i).Item("descripcion_Cac"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ListarDepartamentoAcademico(ByVal cod As Integer, ByVal param As String)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.ACAD_BuscaDepartamentoAcademico", cod, param)
            obj.CerrarConexion()

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cDac", tb.Rows(i).Item("codigo_Dac"))
                    data.Add("nDac", tb.Rows(i).Item("nombre_Dac"))
                    data.Add("abrDac", tb.Rows(i).Item("nombre_Dac"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ListarDepartamentoAcademicoDocente(ByVal tipo As String, ByVal k As Integer, ByVal f As String)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.ACAD_DocenteporDepAcademico", tipo, k, f)
            obj.CerrarConexion()

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cDac", tb.Rows(i).Item("codigo_Dac"))
                    data.Add("cPer", tb.Rows(i).Item("codigo_Per"))
                    data.Add("nPer", tb.Rows(i).Item("Personal"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception

        End Try
    End Sub

End Class
