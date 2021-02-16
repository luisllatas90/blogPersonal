Imports System.Data
Imports System.Data.DataTable
Imports System.Collections.Generic
Imports System.Data.DataRow
Imports System.Data.DataColumn

Partial Class administrativo_frmEmailUsat
    Inherits System.Web.UI.Page

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub
    Protected Sub ShowMessage2(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje2", "<script>ShowMessage2('" & Message & "','" & type & "');</script>")
    End Sub
    Protected Sub ShowMessage3(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje3", "<script>ShowMessage3('" & Message & "','" & type & "');</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If

        If (IsPostBack = False) Then
            'aviso.Visible = False

            btnImportar.Attributes.CssStyle("display") = "none"
        End If
    End Sub

    Protected Sub btnImportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportar.Click

        Try

            Dim phraseEmail As String = Me.email.Text.ToString
            Dim wordsEmail As String() = phraseEmail.Split(","c)
            Dim phrasePassword As String = Me.clave.Text.ToString
            Dim wordsPassword As String() = phrasePassword.Split(","c)



            Dim dt As New DataTable()
            grwResultado.DataSource = dt
            grwResultado.DataBind()

            dt.Columns.AddRange(New DataColumn() {New DataColumn("nro", GetType(Integer)), _
                                                    New DataColumn("nrodocident", GetType(String)), _
                                                    New DataColumn("emailusat", GetType(String)), _
                                                  New DataColumn("clave", GetType(String)), _
                                                   New DataColumn("procesado", GetType(String))})


            Dim _clave As String
            Dim _email As String
            For i As Integer = 0 To wordsEmail.Length - 2



                If wordsEmail(i) = "undefined" Then
                    _email = String.Empty
                Else
                    _email = wordsEmail(i).ToString.Trim
                End If

                If wordsPassword(i) = "undefined" Then
                    _clave = String.Empty
                Else
                    _clave = wordsPassword(i).ToString.Trim
                End If

                Dim phraseDocIdent As String = wordsEmail(i).ToString
                Dim wordsDocIdent As String() = phraseDocIdent.Split("@"c)
                If _email = "" Then
                    dt.Rows.Add((i + 1), _email, _email.Trim, _clave, "")
                Else
                    dt.Rows.Add((i + 1), wordsDocIdent(0), _email.Trim, _clave, "")
                End If


            Next

            grwResultado.DataSource = dt
            grwResultado.DataBind()

        Catch ex As Exception
            'Response.Write(ex.Message & " -- " & ex.StackTrace)
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Function fnValidar() As Boolean

        Try
            Dim filas As Integer = grwResultado.Rows.Count
            Dim fila As String = ""
            With Me.grwResultado
                For i As Integer = 0 To filas - 1
                    If .DataKeys(i).Values(1).ToString() = "" Then
                        fila = (i + 1).ToString
                        ShowMessage("Advertencia: Existe Correo vacío en la fila " & fila.ToString, MessageType.Warning)
                        Return False
                    End If

                Next
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try

            Dim CLS As New clsEmailUsatAlumno

            Dim filas As Integer = grwResultado.Rows.Count
            Dim grabar As Boolean = False
            Dim rpta As DataTable
            Dim nro As Integer
            Dim nrodocident As String
            Dim email As String
            Dim clave As String
            'Response.Write(1)

            If fnValidar() Then
                'Response.Write(2)
                With CLS
                    .tipooperacion = "A"

                    For i As Integer = 0 To filas - 1

                        Dim CLSD As New clsEmailUsatAlumno
                        nro = Me.grwResultado.DataKeys(i).Values(0).ToString
                        nrodocident = Me.grwResultado.DataKeys(i).Values(1).ToString
                        email = Me.grwResultado.DataKeys(i).Values(2).ToString
                        clave = Me.grwResultado.DataKeys(i).Values(3).ToString
                        CLSD.tipooperacion = "A"
                        CLSD._nro = nro
                        CLSD._nrodocident = nrodocident
                        CLSD._email = email
                        CLSD._clave = clave
                        grabar = True
                        CLS.listaemail.Add(CLSD)
                    Next
                End With

                rpta = CLS.Actualizar

                'rpta = True
                'Response.Write(rpta.Rows.Count)
                If grabar Then

                    If rpta Is Nothing Then
                        Call ShowMessage("Error al actualizar", MessageType.Error)

                    Else
                        Call ShowMessage("Se actualizaron los email y password usat del estudiante", MessageType.Success)
                        fnActualizarProceso(rpta)

                    End If
                End If
            End If

        Catch ex As Exception
            'Response.Write(ex.Message & " -- " & ex.StackTrace)
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub
    Private Sub fnActualizarProceso(ByVal dt As DataTable)
        Dim nroprocesados As Integer = 0
        Dim nronoprocesados As Integer = 0
        Dim filas As Integer = grwResultado.Rows.Count
        Dim filas2 As Integer = dt.Rows.Count

        Dim str As New StringBuilder
        Dim strnp As New StringBuilder

        str.Append("No Procesados:<br><ul>")
        strnp.Append("Nro de Procesados:<br><ul>")

        For i As Integer = 0 To filas - 1
            For j As Integer = 0 To filas2 - 1
                'Response.Write("i: " & grwResultado.Rows(i).Cells(0).Text)
                If grwResultado.Rows(i).Cells(0).Text = dt.Rows(j).Item("nro") Then

                    grwResultado.Rows(i).Cells(4).Text = dt.Rows(j).Item("procesado")

                    If dt.Rows(j).Item("procesado") = "SI" Then
                        nroprocesados = nroprocesados + 1
                    Else
                        nronoprocesados = nronoprocesados + 1

                        str.Append("<li>" & grwResultado.Rows(i).Cells(1).text & " " & grwResultado.Rows(i).Cells(2).text & "</li>")
                    End If

                    ' Else
                    '   grwResultado.Rows(i).Cells(4).Text = "SI" 
                End If
            Next
        Next
        str.Append("</ul>")
        strnp.Append(nroprocesados.ToString)

        strnp.Append("</ul>")
        'Response.Write(str.ToString)

        If nronoprocesados > 0 Then

            Call ShowMessage2(strnp.ToString, MessageType.Info)
        End If
        If nroprocesados > 0 Then
            Call ShowMessage3(str.ToString, MessageType.Error)
        End If


    End Sub



    Protected Sub grwResultado_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwResultado.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                'Dim celda As TableCellCollection = e.Row.Cells
                'Dim clave As String = ""
                'clave = celda(2).Text

               
                'If clave = String.Empty Then
                '    e.Row.ForeColor = Drawing.Color.Red
                'Else

                '    e.Row.ForeColor = Drawing.Color.Blue
                'End If
            End If
        Catch ex As Exception
            Call ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

End Class


Public Class clsEmailUsatAlumno

    Private C As ClsConectarDatos
    Public tipooperacion As String
    Private nro As Integer
    Private nrodocident As String
    Private email As String
    Private clave As String

    Public listaemail As New List(Of clsEmailUsatAlumno)

    Public Property _nro() As Integer
        Get
            Return nro
        End Get
        Set(ByVal value As Integer)
            nro = value
        End Set
    End Property
    Public Property _nrodocident() As String
        Get
            Return nrodocident
        End Get
        Set(ByVal value As String)
            nrodocident = value
        End Set
    End Property
    Public Property _email() As String
        Get
            Return email
        End Get
        Set(ByVal value As String)
            email = value
        End Set
    End Property

    Public Property _clave() As String
        Get
            Return clave
        End Get
        Set(ByVal value As String)
            clave = value
        End Set
    End Property


    Sub New()
        If C Is Nothing Then
            C = New ClsConectarDatos
            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        End If
        _email = String.Empty
        _clave = String.Empty

    End Sub

    Public Function Actualizar() As DataTable
        Try
            Dim dt As New DataTable

            Dim dtrpta As New DataTable()
            dtrpta.Columns.AddRange(New DataColumn() {New DataColumn("nro", GetType(Integer)), _
                                                   New DataColumn("procesado", GetType(String))})

            C.IniciarTransaccion()


            For Each detalle As clsEmailUsatAlumno In listaemail
                With detalle

                    dt = C.TraerDataTable("EMAIL_ActualizarDatos", .tipooperacion, .nrodocident, .email, .clave)

                    If dt.Rows(0).Item("RPTA") = "NO" Then
                        dtrpta.Rows.Add(.nro, "NO")
					Else
						 dtrpta.Rows.Add(.nro, "SI")
                    End If

                End With
            Next


            C.TerminarTransaccion()
            Return dtrpta
        Catch ex As Exception
            C.TerminarTransaccion()
            Return Nothing
        End Try
    End Function


End Class
