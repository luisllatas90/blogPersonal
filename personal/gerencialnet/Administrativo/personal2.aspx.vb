'Clase del Formulario datos correspondiente al Modulo de Personalizacion de reportes
'Elaborado por  : Wilfredo Aljobin Cumpa
'Fecha          : 24/10/2006
'Observaciones  : Se encuentra el codigo para realizar todas las consultas al CUBO
'del analysis services en un entorno personalizado, pudiendo elegir entre un y otro dato a mostrar
Imports System.Data
Imports System.Data.SqlClient

Partial Class Academico_personal
    Inherits System.Web.UI.Page
    Public objcubo As New Cubo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Controlo que no sea la primera vez que se accede a la pagina.

        If IsPostBack = False Then
            Call LlenaEscuela()
            objcubo.Cubo = Session("cubo")
            objcubo.LlenaTree(Me.TreeDim1, Session("dim1"))
            objcubo.LlenaTree(Me.TreeDim2, Session("dim2"))
            objcubo.LlenaTree(Me.TreeDim3, Session("dim3"))
        End If

    End Sub

    Protected Sub TreeDim1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeDim1.SelectedNodeChanged
        objcubo.ExpandirArbol(Me.TreeDim1)
    End Sub

    Protected Sub TreeDim2_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeDim2.SelectedNodeChanged
        objcubo.ExpandirArbol(Me.TreeDim2)
    End Sub

    Protected Sub TreeDim3_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeDim3.SelectedNodeChanged
        objcubo.ExpandirArbol(Me.TreeDim3)
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Declaro un objeto cubo y me conecto al servidor con la base de datos respectiva, 
        ' tomo como datos los establecidos mediante la seleccion hecha en loa TreeViews correspondientes
        Session.Contents.Remove("titulo")
        Dim strWhere As String
        Dim pos0, pos1, pos2, pos3 As Integer
        pos0 = 0 : pos1 = 1 : pos2 = 2 : pos3 = 3
        strWhere = " where ("
        Select Case Me.DDLEscuela.SelectedValue
            Case -1
                strWhere = strWhere & "[carreraprofesional]"
                Session("titulo") = Session("titulo") & " Todas las Escuelas"
            Case Else
                strWhere = strWhere & "[carreraprofesional].[" & Me.DDLEscuela.SelectedItem.Text & "]"
                Session("titulo") = Session("titulo") & " " & Me.DDLEscuela.SelectedItem.Text
                Select Case Me.DDLPlan.SelectedValue
                    Case -1
                        strWhere = strWhere & ", " & "[planestudio]"
                        Session("titulo") = Session("titulo") & " " & "Todos los Planes"
                    Case Else
                        strWhere = strWhere & ", " & "[planestudio].[" & Me.DDLPlan.SelectedItem.Text & "]"
                        Session("titulo") = Session("titulo") & " " & Me.DDLPlan.SelectedItem.Text
                        Select Case Me.DDLCursos.SelectedValue
                            Case -1
                                strWhere = strWhere & ", " & "[curso]"
                            Case 0
                                'strWhere = strWhere & ", " & "[curso].[" & Me.DDLCursos.SelectedItem.Text & "]"
                                objcubo.Server = "127.0.0.1"
                                objcubo.DB = "sig_usat"
                                objcubo.Cubo = Session("cubo")

                                Me.TreeDim3.Nodes.Clear()
                                objcubo.LlenaTree(Me.TreeDim3, "curso")
                                Me.TreeDim3.Nodes(0).ChildNodes(1).Select()
                                pos1 = 0 : pos2 = 1 : pos0 = 3 : pos3 = 2
                                For j As Integer = 0 To Me.TreeDim3.Nodes(0).ChildNodes(1).ChildNodes.Count - 1
                                    Me.TreeDim3.Nodes(0).ChildNodes(1).ChildNodes(j).Checked = True
                                Next
                                strWhere = strWhere & ", " & "[estado curso].[matriculado]"
                                Session("titulo") = Session("titulo") & " " & "Todos Los Cursos"
                            Case Else
                                strWhere = strWhere & ", " & "[curso].[" & Me.DDLCursos.SelectedItem.Text & "]"
                                Session("titulo") = Session("titulo") & " " & Me.DDLCursos.SelectedItem.Text
                        End Select
                End Select
                End Select
                strWhere = strWhere & " )"
                Session("where") = strWhere

                Try
                    Dim objCubo As New Cubo
            objCubo.Cubo = Session("cubo")
            objCubo.ObtenerConsulta(Session("param"), Session("members"), Me.TreeDim1, Me.TreeDim2, Me.TreeDim3, pos0, pos1, pos2, pos3, Session("where"))
                    Session.Contents.Remove("param")
                    Session.Contents.Remove("members")
                    Session.Contents.Remove("where")
                    Session("consulta") = objCubo._strConsulta
                    Session("informe") = 9999
                    Response.Write("<script>window.opener.location.reload();window.close();</script>")
                Catch ex As Exception
                    Session.Contents.Remove("param")
                    Session.Contents.Remove("members")
                    Session.Contents.Remove("where")
                    Response.Redirect("../error.asp?Des=Se produjo un error, comuniquese con el administrador del sistema&Rec=" & ex.Source & "&Ruta=main.html")
                End Try
    End Sub

    Protected Sub LlenaEscuela()
        Dim Reader As SqlDataReader
        Dim cnx As SqlConnection
        Dim spComand As SqlCommand
        Dim i As Int16
        i = 0
        Try
            cnx = New SqlConnection
            cnx.ConnectionString = ConfigurationManager.ConnectionStrings(1).ConnectionString
            cnx.Open()
            spComand = New SqlCommand("ConsultarCarreraProfesional", cnx)
            spComand.CommandType = CommandType.StoredProcedure
            spComand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "MA"
            spComand.Parameters.Add("@PARAM", SqlDbType.VarChar, 50).Value = "0"
            Reader = spComand.ExecuteReader
            Me.DDLEscuela.Items.Clear()
            Me.DDLEscuela.Items.Add(" ----- Seleccione Carrera Profesional ----- ")
            Me.DDLEscuela.Items(0).Value = "-1"

            While Reader.Read
                Me.DDLEscuela.Items.Add(Reader.Item(2))
                Me.DDLEscuela.Items(i + 1).Value = Reader.Item(0)
                i = i + 1
            End While
            Me.DDLEscuela.DataBind()
            Reader.Close()
            cnx.Close()
            spComand = Nothing
            Reader = Nothing
            cnx = Nothing
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub LlenaPlan(ByVal CodEscuelas As String)
        Dim reader As SqlDataReader
        Dim Cnx As SqlConnection
        Dim spcomand As SqlCommand
        Dim i As Int16
        i = 0
        Try
            Cnx = New SqlConnection
            Cnx.ConnectionString = ConfigurationManager.ConnectionStrings(1).ConnectionString
            Cnx.Open()
            spcomand = New SqlCommand("consultarplanestudio", Cnx)
            spcomand.CommandType = CommandType.StoredProcedure
            spcomand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "HI"
            spcomand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = CodEscuelas
            spcomand.Parameters.Add("@param2", SqlDbType.VarChar, 25).Value = "0"
            reader = spcomand.ExecuteReader()
            Me.DDLPlan.Items.Clear()
            Me.DDLPlan.Items.Add("----- Seleccione Plan de Estudio -----")
            Me.DDLPlan.Items(0).Value = "-1"
            While reader.Read
                Me.DDLPlan.Items.Add(reader.Item(1))
                Me.DDLPlan.Items(i + 1).Value = reader.Item(0)
                i = i + 1
            End While
            Me.DDLPlan.DataBind()
            reader.Close()
            Cnx.Close()
            spcomand = Nothing
            reader = Nothing
            Cnx = Nothing
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub LlenaCursos(ByVal codPlan As String)
        Dim reader As SqlDataReader
        Dim Cnx As SqlConnection
        Dim spcomand As SqlCommand
        Dim i As Int16
        i = 1
        Try
            Cnx = New SqlConnection
            Cnx.ConnectionString = ConfigurationManager.ConnectionStrings(1).ConnectionString
            Cnx.Open()
            spcomand = New SqlCommand("consultarcursoplan", Cnx)
            spcomand.CommandType = CommandType.StoredProcedure
            spcomand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "CP"
            spcomand.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = codPlan
            spcomand.Parameters.Add("@param2", SqlDbType.VarChar, 50).Value = "1"
            spcomand.Parameters.Add("@param3", SqlDbType.Int).Value = 0
            reader = spcomand.ExecuteReader
            Me.DDLCursos.Items.Clear()
            Me.DDLCursos.Items.Add(" ----- Seleccione Curso ----- ")
            Me.DDLCursos.Items(0).Value = "-1"
            Me.DDLCursos.Items.Add(" ----- Todos los Cursos ----- ")
            Me.DDLCursos.Items(1).Value = "0"
            While reader.Read
                Me.DDLCursos.Items.Add(reader.Item(2) + " " + reader.Item(3))
                Me.DDLCursos.Items(i + 1).Value = reader.Item(0)
                i = i + 1
            End While
            Me.DDLCursos.DataBind()
            reader.Close()
            Cnx.Close()
            spcomand = Nothing
            reader = Nothing
            Cnx = Nothing
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub DDLEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLEscuela.SelectedIndexChanged
        Call LlenaPlan(Me.DDLEscuela.SelectedValue)
        Me.DDLCursos.Items.Clear()
    End Sub

    Protected Sub DDLPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLPlan.SelectedIndexChanged
        Call LlenaCursos(Me.DDLPlan.SelectedValue)
    End Sub
End Class
