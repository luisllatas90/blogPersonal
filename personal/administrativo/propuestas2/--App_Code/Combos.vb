'Clase para llenar los DropDowList
'Escrita por:   Wilfredo Aljobin CUmpa
'Fecha      :   20/10/2006
'Observaciones  : Se utiliza en casi todos los formularios con que cuentan con un
'DropdownList

Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class Combos
    Private _strConecctionString As String = ConfigurationManager.ConnectionStrings(1).ConnectionString

    Public Sub LLenaTematica(ByVal Combo As DropDownList, ByVal param1 As String)
        Dim Cnx As SqlConnection
        Dim Cmd As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Integer = 0
        Try
            Cnx = New SqlConnection(_strConecctionString)
            Cnx.Open()
            Cmd = New SqlCommand("ConsultarInvestigaciones", Cnx)
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "10"
            Cmd.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = param1
            Reader = Cmd.ExecuteReader
            Combo.Items.Clear()
            Combo.Items.Add("----- Seleccione Temática -----")
            Combo.Items(0).Value = 0
            While Reader.Read
                i = i + 1
                Combo.Items.Add(Reader.Item(1).ToString & " - " & Reader.Item(2).ToString)
                Combo.Items(i).Value = Reader.Item(0)
            End While
        Catch ex As Exception
        End Try
    End Sub

    Public Sub LlenaTipoInvestigacion(ByVal combo As DropDownList)
        Dim Cnx As SqlConnection
        Dim Cmd As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Integer = 0
        Try
            Cnx = New SqlConnection(_strConecctionString)
            Cnx.Open()
            Cmd = New SqlCommand("ConsultarInvestigaciones", Cnx)
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "5"
            Cmd.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = ""
            Reader = Cmd.ExecuteReader
            combo.Items.Clear()
            combo.Items.Add("----- Seleccione Tipo de Investigación -----")
            combo.Items(0).Value = 0
            While Reader.Read
                i = i + 1
                combo.Items.Add(Reader.Item(1).ToString)
                combo.Items(i).Value = Reader.Item(0)
            End While
        Catch ex As Exception
        End Try
    End Sub

    Public Sub LlenaLineaPersonal(ByVal combo As DropDownList, ByVal param1 As String)
        Dim Cnx As SqlConnection
        Dim Cmd As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Integer = 0
        Try
            Cnx = New SqlConnection(_strConecctionString)
            Cnx.Open()
            Cmd = New SqlCommand("ConsultarLineasDePersonal", Cnx)
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "3"
            Cmd.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = param1
            Reader = Cmd.ExecuteReader
            combo.Items.Clear()
            combo.Items.Add("----- Seleccione Linea de Personal -----")
            combo.Items(0).Value = 0
            While Reader.Read
                i = i + 1
                combo.Items.Add(Reader.Item(1).ToString)
                combo.Items(i).Value = Reader.Item(0)
            End While
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ConsultarTipoParticipacionInvestigacion(ByVal combo As DropDownList)
        Dim Cnx As SqlConnection
        Dim Cmd As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Integer = 0
        Try
            Cnx = New SqlConnection(_strConecctionString)
            Cnx.Open()
            Cmd = New SqlCommand("ConsultarTipoParticipacionInvestigacion", Cnx)
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "1"
            Cmd.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = ""
            Reader = Cmd.ExecuteReader
            combo.Items.Clear()
            combo.Items.Add("----- Seleccione Tipo de participacion -----")
            combo.Items(0).Value = 0
            While Reader.Read
                i = i + 1
                combo.Items.Add(Reader.Item(1).ToString)
                combo.Items(i).Value = Reader.Item(0)
            End While
        Catch ex As Exception
        End Try
    End Sub

    Public Sub LlenaPais(ByVal Combo As DropDownList)
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Try
            Adapter = New SqlDataAdapter("consultarlugares", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 1).Value = "1"
            Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = ""
            Adapter.SelectCommand.Parameters.Add("@param2", SqlDbType.VarChar, 50).Value = ""
            Adapter.Fill(Tabla)
            Combo.DataSource = Tabla
            Combo.DataTextField = Tabla.Columns(1).ToString
            Combo.DataValueField = Tabla.Columns(0).ToString
            Combo.DataBind()
            For i As Integer = 0 To Combo.Items.Count
                If Combo.Items(i).Text = "Perú" Then
                    Combo.Items(i).Selected = True
                End If
            Next
        Catch ex As Exception

        End Try
        Tabla = Nothing
        Adapter = Nothing
    End Sub

    Public Sub LlenaDepartamento(ByVal Combo As DropDownList)
        Dim cnx As SqlConnection = Nothing
        Dim Comand As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Int16
        i = 1
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = _strConecctionString
            cnx.Open()

            Comand = New SqlCommand("consultarlugares", cnx)
            Comand.CommandType = CommandType.StoredProcedure
            Comand.Parameters.Add("@tipo", SqlDbType.Char, 1).Value = "2"
            Comand.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = "156"
            Comand.Parameters.Add("@param2", SqlDbType.VarChar, 50).Value = ""
            Reader = Comand.ExecuteReader

            Combo.Items.Clear()
            Combo.Items.Add("-- Departamento --")
            Combo.Items(0).Value = 0
            While Reader.Read
                Combo.Items.Add(Reader.Item(1))
                Combo.Items(i).Value = Reader.Item(0)
                i = i + 1
            End While
            Reader.Close()
        Catch ex As Exception

        End Try
        Reader = Nothing
        Comand = Nothing
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx.Dispose()
            cnx = Nothing
        End If
    End Sub

    Public Sub LlenaProvincia(ByVal combo As DropDownList, ByVal departamento As Integer)
        Dim cnx As SqlConnection = Nothing
        Dim Comand As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Int16
        i = 1
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = _strConecctionString
            cnx.Open()

            Comand = New SqlCommand("consultarlugares", cnx)
            Comand.CommandType = CommandType.StoredProcedure
            Comand.Parameters.Add("@tipo", SqlDbType.Char, 1).Value = "3"
            Comand.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = departamento.ToString
            Comand.Parameters.Add("@param2", SqlDbType.VarChar, 50).Value = ""
            Reader = Comand.ExecuteReader

            combo.Items.Clear()
            combo.Items.Add("-- Provincia --")
            combo.Items(0).Value = 0
            While Reader.Read
                combo.Items.Add(Reader.Item(1))
                combo.Items(i).Value = Reader.Item(0)
                i = i + 1
            End While
            Reader.Close()

        Catch ex As Exception

        End Try
        Comand = Nothing
        Reader = Nothing
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If
    End Sub

    Public Sub LlenaDistrito(ByVal combo As DropDownList, ByVal provincia As String)
        Dim cnx As SqlConnection = Nothing
        Dim Comand As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Int16
        i = 1
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = _strConecctionString
            cnx.Open()

            Comand = New SqlCommand("consultarlugares", cnx)
            Comand.CommandType = CommandType.StoredProcedure
            Comand.Parameters.Add("@tipo", SqlDbType.Char, 1).Value = "4"
            Comand.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = provincia
            Comand.Parameters.Add("@param2", SqlDbType.VarChar, 50).Value = ""
            Reader = Comand.ExecuteReader

            combo.Items.Clear()
            combo.Items.Add("-- Distrito --")
            combo.Items(0).Value = 0
            While Reader.Read
                combo.Items.Add(Reader.Item(1))
                combo.Items(i).Value = Reader.Item(0)
                i = i + 1
            End While
            Reader.Close()
        Catch ex As Exception

        End Try
        Reader = Nothing
        Comand = Nothing
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If
    End Sub

    Public Sub llenaTitulo(ByVal Combo As DropDownList)
        Dim cnx As SqlConnection = Nothing
        Dim Comand As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Int16
        i = 1
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = _strConecctionString
            cnx.Open()

            Comand = New SqlCommand("ConsultarTituloProfesional", cnx)
            Comand.CommandType = CommandType.StoredProcedure
            Comand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "TO"
            Comand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = ""
            Reader = Comand.ExecuteReader

            Combo.Items.Clear()
            Combo.Items.Add("-- Seleccione Titulo Profesional --")
            Combo.Items(0).Value = 0
            While Reader.Read
                Combo.Items.Add(Reader.Item(1))
                Combo.Items(i).Value = Reader.Item(0)
                i = i + 1
            End While
            Reader.Close()
        Catch ex As Exception

        End Try
        Reader = Nothing
        Comand = Nothing
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If
    End Sub

    Public Sub LlenaAreaEstudio(ByVal combo As DropDownList)
        Dim cnx As SqlConnection
        Dim Comand As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Int16
        i = 1
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = _strConecctionString
            cnx.Open()

            Comand = New SqlCommand("ConsultarOtrosEstudios", cnx)
            Comand.CommandType = CommandType.StoredProcedure
            Comand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "TO"
            Comand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = ""
            Reader = Comand.ExecuteReader

            combo.Items.Clear()
            combo.Items.Add("-- Seleccione Area de Estudio --")
            combo.Items(0).Value = 0
            While Reader.Read
                combo.Items.Add(Reader.Item(1))
                combo.Items(i).Value = Reader.Item(0)
                i = i + 1
            End While
            Reader.Close()
            cnx.Close()
        Catch ex As Exception

        End Try
        Reader = Nothing
        cnx = Nothing
        Comand = Nothing
    End Sub

    Public Sub LlenaTipoInstitucion(ByVal combo As DropDownList)
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Try
            Adapter = New SqlDataAdapter("ConsultarTipoInstitucion", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "TO"
            Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = ""
            Adapter.Fill(Tabla)
            combo.Items.Clear()
            combo.DataSource = Tabla
            combo.DataTextField = Tabla.Columns(1).ToString
            combo.DataValueField = Tabla.Columns(0).ToString
            combo.DataBind()
        Catch ex As Exception

        End Try
        Tabla = Nothing
        Adapter = Nothing
    End Sub

    Public Sub LlenaSituacion(ByVal combo As DropDownList)
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Try
            Adapter = New SqlDataAdapter("ConsultarSituacion", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "TO"
            Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = ""
            Adapter.Fill(Tabla)
            combo.Items.Clear()
            combo.DataSource = Tabla
            combo.DataTextField = Tabla.Columns(1).ToString
            combo.DataValueField = Tabla.Columns(0).ToString
            combo.DataBind()
        Catch ex As Exception

        End Try
        Tabla = Nothing
        Adapter = Nothing

    End Sub

    Public Sub LlenaInstitucion(ByVal combo As DropDownList, ByVal tipo As String, ByVal proc As String)
        Dim cnx As SqlConnection
        Dim Comand As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Int16
        i = 1
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = _strConecctionString
            cnx.Open()

            Comand = New SqlCommand("ConsultarInstitucion", cnx)
            Comand.CommandType = CommandType.StoredProcedure
            Comand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "PR"
            Comand.Parameters.Add("@param1", SqlDbType.Char, 2).Value = tipo
            Comand.Parameters.Add("@param2", SqlDbType.Char, 2).Value = proc
            Reader = Comand.ExecuteReader

            combo.Items.Clear()
            combo.Items.Add("-- Seleccione Institucion --")
            combo.Items(0).Value = -2
            While Reader.Read
                combo.Items.Add(Reader.Item(1))
                combo.Items(i).Value = Reader.Item(0)
                i = i + 1
            End While
            Reader.Close()
            cnx.Close()
        Catch ex As Exception

        End Try
        Reader = Nothing
        cnx = Nothing
        Comand = Nothing
    End Sub

    Public Sub LlenaIdiomas(ByVal combo As DropDownList, ByVal param As String)
        Dim cnx As SqlConnection
        Dim Comand As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Int16
        i = 1
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = _strConecctionString
            cnx.Open()

            Comand = New SqlCommand("ConsultarIdiomas", cnx)
            Comand.CommandType = CommandType.StoredProcedure
            Comand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "TO"
            Comand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = param
            Reader = Comand.ExecuteReader

            combo.Items.Clear()
            combo.Items.Add("-- Seleccione Idioma --")
            combo.Items(0).Value = 0
            While Reader.Read
                combo.Items.Add(Reader.Item(1))
                combo.Items(i).Value = Reader.Item(0)
                i = i + 1
            End While
            Reader.Close()
            cnx.Close()
        Catch ex As Exception

        End Try
        Reader = Nothing
        cnx = Nothing
        Comand = Nothing
    End Sub

    Public Sub LlenaGrados(ByVal combo As DropDownList, ByVal param As String)
        Dim cnx As SqlConnection
        Dim Comand As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Int16
        i = 1
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = _strConecctionString
            cnx.Open()

            Comand = New SqlCommand("ConsultarGradoAcademico", cnx)
            Comand.CommandType = CommandType.StoredProcedure
            Comand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "TO"
            Comand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = Trim(param)
            Reader = Comand.ExecuteReader

            combo.Items.Clear()
            combo.Items.Add("-- Seleccione Grado Academico --")
            combo.Items(0).Value = 0
            While Reader.Read
                combo.Items.Add(Reader.Item(1))
                combo.Items(i).Value = Reader.Item(0)
                i = i + 1
            End While
            Reader.Close()
            cnx.Close()
        Catch ex As Exception

        End Try
        Reader = Nothing
        cnx = Nothing
        Comand = Nothing
    End Sub

    Public Sub LlenaCargos(ByVal Combo As DropDownList)
        Dim cnx As SqlConnection
        Dim Comand As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Int16
        i = 1
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = _strConecctionString
            cnx.Open()

            Comand = New SqlCommand("ConsultarExperiencia", cnx)
            Comand.CommandType = CommandType.StoredProcedure
            Comand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "TA"
            Comand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = ""
            Reader = Comand.ExecuteReader

            Combo.Items.Clear()
            Combo.Items.Add("-- Seleccione Cargo --")
            Combo.Items(0).Value = 0
            While Reader.Read
                Combo.Items.Add(Reader.Item(1))
                Combo.Items(i).Value = Reader.Item(0)
                i = i + 1
            End While
            Reader.Close()
            cnx.Close()
        Catch ex As Exception

        End Try
        Reader = Nothing
        cnx = Nothing
        Comand = Nothing

    End Sub

    Public Sub LlenaTipoContrato(ByVal Combo As DropDownList)
        Dim cnx As SqlConnection
        Dim Comand As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Int16
        i = 1
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = _strConecctionString
            cnx.Open()

            Comand = New SqlCommand("ConsultarExperiencia", cnx)
            Comand.CommandType = CommandType.StoredProcedure
            Comand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "TC"
            Comand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = ""
            Reader = Comand.ExecuteReader

            Combo.Items.Clear()
            Combo.Items.Add("-- Seleccione Tipo de Contrato --")
            Combo.Items(0).Value = 0
            While Reader.Read
                Combo.Items.Add(Reader.Item(1))
                Combo.Items(i).Value = Reader.Item(0)
                i = i + 1
            End While
            Reader.Close()
            cnx.Close()
        Catch ex As Exception

        End Try
        Reader = Nothing
        cnx = Nothing
        Comand = Nothing

    End Sub

    Public Function ObtieneDepartamento(ByVal IdProvincia As String) As String
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Try
            Adapter = New SqlDataAdapter("consultarlugares", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 1).Value = "6"
            Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = IdProvincia
            Adapter.SelectCommand.Parameters.Add("@param2", SqlDbType.VarChar, 50).Value = ""
            Adapter.Fill(Tabla)
            If Tabla.Rows.Count = 0 Then
                ObtieneDepartamento = 0
            Else
                ObtieneDepartamento = Tabla.Rows(0).Item(0)
            End If

        Catch ex As Exception
            ObtieneDepartamento = 0
        End Try
        Tabla = Nothing
        Adapter = Nothing
    End Function

    Public Function ObtieneProvincia(ByVal iddistrito As String)
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Try
            Adapter = New SqlDataAdapter("consultarlugares", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 1).Value = "7"
            Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = iddistrito
            Adapter.SelectCommand.Parameters.Add("@param2", SqlDbType.VarChar, 50).Value = ""
            Adapter.Fill(Tabla)
            If Tabla.Rows.Count = 0 Then
                ObtieneProvincia = 0
            Else
                ObtieneProvincia = Tabla.Rows(0).Item(0)
            End If

        Catch ex As Exception
            ObtieneProvincia = 0
        End Try
        Tabla = Nothing
        Adapter = Nothing
    End Function

    Public Sub LlenaClaseEvento(ByVal combo As DropDownList)
        Dim cnx As SqlConnection
        Dim Comand As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Int16
        i = 0
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = _strConecctionString
            cnx.Open()

            Comand = New SqlCommand("ConsultarEvento", cnx)
            Comand.CommandType = CommandType.StoredProcedure
            Comand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "CE"
            Comand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = ""
            Comand.Parameters.Add("@param2", SqlDbType.VarChar, 25).Value = ""
            Comand.Parameters.Add("@param3", SqlDbType.VarChar, 25).Value = ""
            Reader = Comand.ExecuteReader
            While Reader.Read
                combo.Items.Add(Reader.Item(1))
                combo.Items(i).Value = Reader.Item(0)
                i = i + 1
            End While
            Reader.Close()
            cnx.Close()
        Catch ex As Exception

        End Try
        Reader = Nothing
        cnx = Nothing
        Comand = Nothing
    End Sub

    Public Sub LlenaTipoEvento(ByVal combo As DropDownList, ByVal tipo As Integer)

        Dim cnx As SqlConnection
        Dim Comand As SqlCommand
        Dim Reader As SqlDataReader
        Dim valortipo As String
        Dim i As Int16
        i = 1
        If tipo = 1 Then
            valortipo = "TE"
        Else
            valortipo = "TS"
        End If
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = _strConecctionString
            cnx.Open()

            Comand = New SqlCommand("ConsultarEvento", cnx)
            Comand.CommandType = CommandType.StoredProcedure
            Comand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = valortipo
            Comand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = ""
            Comand.Parameters.Add("@param2", SqlDbType.VarChar, 25).Value = ""
            Comand.Parameters.Add("@param3", SqlDbType.VarChar, 25).Value = ""
            Reader = Comand.ExecuteReader

            combo.Items.Clear()
            combo.Items.Add("-- Tipo Evento  --")
            combo.Items(0).Value = 0
            While Reader.Read
                combo.Items.Add(Reader.Item(1))
                combo.Items(i).Value = Reader.Item(0)
                i = i + 1
            End While
            Reader.Close()
            cnx.Close()
        Catch ex As Exception

        End Try
        Reader = Nothing
        cnx = Nothing
        Comand = Nothing
    End Sub

    Public Sub LlenaDepartamentoAcademico(ByVal combo As DropDownList)

        Dim cnx As SqlConnection
        Dim Comand As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Int16
        i = 0
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = _strConecctionString
            cnx.Open()

            Comand = New SqlCommand("ConsultarDepartamentoAcademico", cnx)
            Comand.CommandType = CommandType.StoredProcedure
            Comand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "TO"
            Comand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = ""
            Reader = Comand.ExecuteReader
            combo.Items.Clear()
            While Reader.Read
                combo.Items.Add(Reader.Item(1))
                combo.Items(i).Value = Reader.Item(0)
                i = i + 1
            End While
            Reader.Close()
            cnx.Close()
        Catch ex As Exception

        End Try
        Reader = Nothing
        cnx = Nothing
        Comand = Nothing
    End Sub

    Public Sub LlenaTipoParticipacion(ByVal ChekBox As CheckBoxList)
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Try
            Adapter = New SqlDataAdapter("ConsultarEvento", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "PE"
            Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = ""
            Adapter.SelectCommand.Parameters.Add("@param2", SqlDbType.VarChar, 25).Value = ""
            Adapter.SelectCommand.Parameters.Add("@param3", SqlDbType.VarChar, 25).Value = ""

            Adapter.Fill(Tabla)
            ChekBox.DataSource = Tabla
            ChekBox.DataTextField = Tabla.Columns(1).ToString
            ChekBox.DataValueField = Tabla.Columns(0).ToString
            ChekBox.DataBind()
        Catch ex As Exception

        End Try
        Tabla = Nothing
        Adapter = Nothing
    End Sub

    Public Sub LlenaEventos(ByVal Lista As ListBox, ByVal param1 As String, ByVal param2 As String, ByVal param3 As String, ByVal tipo As Integer)
        Dim cnx As SqlConnection
        Dim Comand As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Int16
        Dim valortipo As String
        If tipo = 1 Then
            valortipo = "EA"  'Evento Academico
        Else
            valortipo = "ES"  'Evento Social
        End If
        i = 1
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = _strConecctionString
            cnx.Open()

            Comand = New SqlCommand("ConsultarEvento", cnx)
            Comand.CommandType = CommandType.StoredProcedure
            Comand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = valortipo
            Comand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = param1
            Comand.Parameters.Add("@param2", SqlDbType.VarChar, 25).Value = param2
            Comand.Parameters.Add("@param3", SqlDbType.VarChar, 25).Value = param3
            Reader = Comand.ExecuteReader
            Lista.Items.Clear()
            Lista.Items.Add("<---- Otros ---->")
            Lista.Items(0).Value = 0
            While Reader.Read
                Lista.Items.Add(Reader.Item(1))
                Lista.Items(i).Value = Reader.Item(0)
                i = i + 1
            End While
            Reader.Close()
            cnx.Close()
        Catch ex As Exception

        End Try
        Reader = Nothing
        cnx = Nothing
        Comand = Nothing
    End Sub

    Public Sub LlenaTipoDistincion(ByVal combo As DropDownList)
        Dim cnx As SqlConnection
        Dim Comand As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Int16
        i = 0
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = _strConecctionString
            cnx.Open()

            Comand = New SqlCommand("ConsultarDistinciones ", cnx)
            Comand.CommandType = CommandType.StoredProcedure
            Comand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "TD"
            Comand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = ""
            Reader = Comand.ExecuteReader

            combo.Items.Clear()
           
            While Reader.Read
                combo.Items.Add(Reader.Item(1))
                combo.Items(i).Value = Reader.Item(0)
                i = i + 1
            End While
            Reader.Close()
            cnx.Close()
        Catch ex As Exception

        End Try
        Reader = Nothing
        cnx = Nothing
        Comand = Nothing
    End Sub

    Public Sub LlenaCentroCostos(ByVal combo As DropDownList, ByVal codigo_per As String, ByVal tipo As String)
        Dim cnx As SqlConnection
        Dim Comand As SqlCommand
        Dim Reader As SqlDataReader
        Dim i As Int16
        i = 1
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = _strConecctionString
            cnx.Open()

            Comand = New SqlCommand("ConsultarCentroCosto", cnx)
            Comand.CommandType = CommandType.StoredProcedure
            Comand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = tipo
            Comand.Parameters.Add("@param", SqlDbType.VarChar, 25).Value = codigo_per
            Reader = Comand.ExecuteReader
            combo.Items.Clear()
            If tipo = "CP" Then
                combo.Items.Add("-- Seleccione Area --")
                combo.Items(0).Value = 0
                While Reader.Read
                    combo.Items.Add(Reader.Item(3))
                    combo.Items(i).Value = Reader.Item(1)
                    i = i + 1
                End While
            Else
                combo.Items.Add("-- Seleccione Area --")
                combo.Items(0).Value = 0
                While Reader.Read
                    combo.Items.Add(Reader.Item(2))
                    combo.Items(i).Value = Reader.Item(0)
                    i = i + 1
                End While
            End If
            Reader.Close()
            cnx.Close()
        Catch ex As Exception

        End Try
        Reader = Nothing
        cnx = Nothing
        Comand = Nothing
    End Sub

    Public Function ObtieneDetalleEvento(ByVal tipo As Integer, ByVal param1 As String) As DataTable
        Dim adapter As SqlDataAdapter
        Dim Tabla As New DataTable
        Dim i As Int16
        Dim valortipo As String
        If tipo = 1 Then
            valortipo = "DA"
        Else
            valortipo = "DS"
        End If
        i = 1
        Try
            adapter = New SqlDataAdapter("consultarevento", _strConecctionString)
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = valortipo
            adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = param1
            adapter.SelectCommand.Parameters.Add("@param2", SqlDbType.VarChar, 25).Value = ""
            adapter.SelectCommand.Parameters.Add("@param3", SqlDbType.VarChar, 25).Value = ""
            adapter.Fill(Tabla)
            Return Tabla
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

End Class
