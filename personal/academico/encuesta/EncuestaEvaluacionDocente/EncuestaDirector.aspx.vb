Imports System.Data
Partial Class academico_encuesta_EncuestaEvaluacionDocente_EncuestaDirector
    Inherits System.Web.UI.Page
    Dim tipo As String = ""

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        tipo = Request.QueryString("tipoEval").ToString
        Session("eva_Tipo") = tipo
        ' Session("eva_codigoTipo") = 0

        If Not IsPostBack Then

            If tipo = "S" Then
                Me.lblTipo.Text = "Director de Escuela"
                Me.lblTituloTipo.Text = "Carrera Profesional"
            ElseIf tipo = "O" Then

                Me.lblTipo.Text = "Director de Departamento Académico"
                Me.lblTituloTipo.Text = "Departamento Académico"

            ElseIf tipo = "X" Then
                Me.lblTipo.Text = "Director de Departamento Académico como Director de Escuela"
                Me.lblTituloTipo.Text = "Evaluar a los docentes de"
            Else
                Me.lblTipo.Text = "Acceso Incorrecto"
                Exit Sub
            End If

            If CInt(Session("id_per")) = 0 Then
                ShowMessage("La sesión ha expirado, porfavor volver a ingresar al campus virtual", MessageType.Info)
            Else
                ConsultarAcceso()
            End If

        End If

    End Sub

    Public Sub ConsultarAcceso()
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("EAD_ConsultarEvaluacionVigenteXTipo", tipo)
            obj.CerrarConexion()

            If dt.Rows.Count Then
                obj.AbrirConexion()
                dt = obj.TraerDataTable("EVAL_ConsultarAcceso", tipo, CInt(Session("id_per")))
                obj.CerrarConexion()

                If dt.Rows.Count Then
                    Me.lblSemestreAcademico.Text = dt.Rows(0).Item("descripcion_cac")
                    'Me.lblCarrera.Text = dt.Rows(0).Item("nombreTipo")
                    Me.ddlCarrera.DataSource = dt
                    Me.ddlCarrera.DataTextField = "nombreTipo"
                    Me.ddlCarrera.DataValueField = "codigoTipo"
                    Me.ddlCarrera.DataBind()

                    'Session("eva_codigoTipo") = dt.Rows(0).Item("codigoTipo") ' codigo_Cpf o codigo_dac segun sea el tipo
                    ' Session("eva_codigoTipo") = Me.ddlCarrera.SelectedValue
                    If CInt(Session("eva_codigoTipo")) = 0 Then
                        Me.ddlCarrera.SelectedValue = dt.Rows(0).Item("codigoTipo")
                        Session("eva_codigoTipo") = Me.ddlCarrera.SelectedValue
                    Else
                        Me.ddlCarrera.SelectedValue = Session("eva_codigoTipo")
                    End If
                    ConsultarDocentes()
                Else
                    ShowMessage("No tiene acceso para esta opción", MessageType.Warning)
                    Exit Sub
                End If
            Else
                ShowMessage("El cronograma de evaluación ha vencido.", MessageType.Warning)
                Me.ddlEstado.Visible = False
                Me.btnConsultar.Visible = False
                Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub

   

    Public Sub ConsultarDocentes()

        Try


            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            'Response.Write(Session("id_per"))
            dt = obj.TraerDataTable("EVAL_ConsultarDocentes", tipo, Session("eva_codigoTipo"), Me.ddlEstado.SelectedValue, Me.txtDocente.Text.Trim, CInt(Session("id_per")))

            obj.CerrarConexion()
            If dt.Rows.Count Then
                Session("eva_codigocev") = dt.Rows(0).Item("codigo_cev")              
                Me.gvData.DataSource = dt
            Else
                Me.gvData.DataSource = Nothing
            End If
            Me.gvData.DataBind()
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub

    Protected Sub gvData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Text = e.Row.RowIndex + 1
            If CInt(e.Row.Cells(3).Text) > 0 Then
                e.Row.Cells(3).Text = "Evaluado"
                e.Row.Cells(4).Text = ""
            Else
                e.Row.Cells(3).Text = "Por Evaluar"
            End If
        End If
    End Sub

    Protected Sub gvData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvData.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "Evaluar") Then
            Session("eva_codigoper") = Me.gvData.DataKeys(index).Values("codigo_per") 'per profesor que será evaluado
            Session("eva_docente") = Me.gvData.DataKeys(index).Values("docente") 'nombre profesor que será evaluado
            Session("eva_semestre") = Me.lblSemestreAcademico.Text
            Session("eva_dedicacion") = Me.gvData.DataKeys(index).Values("descripcion_ded")
            Session("eva_codigoTipo") = Me.ddlCarrera.SelectedValue
            Response.Redirect("ResponderEncuesta.aspx")
        End If
    End Sub

   
    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        'ConsultarDocentes()ç
        Session("eva_codigoTipo") = Me.ddlCarrera.SelectedValue
        ConsultarAcceso()
    End Sub

    Protected Sub gvData_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvData.PreRender
        If gvData.Rows.Count > 0 Then
            gvData.UseAccessibleHeader = True
            gvData.HeaderRow.TableSection = TableRowSection.TableHeader
        End If

    End Sub
End Class
