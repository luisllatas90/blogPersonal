
Partial Class indicadores_frmTableroControl
    Inherits System.Web.UI.Page

    Dim usuario As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                'Debe tomar del inicio de sesión
                usuario = Request.QueryString("id")
                CargarComboCentroCostos()
                CargarComboPeriodos()
                CargarComboPlanes()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboCentroCostos()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListarCentroCostos()

            If dts.Rows.Count > 0 Then
                ddlCco.DataSource = dts
                ddlCco.DataValueField = "codigo_cco"
                ddlCco.DataTextField = "descripcion_cco"
                ddlCco.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try        
    End Sub

    Private Sub CargarComboPeriodos()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ConsultarPeriodosPosteriores(1, 0)

            If dts.Rows.Count > 0 Then
                ddlPeriodo.DataSource = dts
                ddlPeriodo.DataValueField = "Codigo"
                ddlPeriodo.DataTextField = "Descripcion"
                ddlPeriodo.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboPlanes()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ConsultarPlanes("%")

            If dts.Rows.Count > 0 Then
                ddlPlan.DataSource = dts
                ddlPlan.DataValueField = "Codigo"
                ddlPlan.DataTextField = "Periodo"
                ddlPlan.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarTableroControl()

        Dim obj As New clsIndicadores
        Dim dts As New Data.DataTable
        Dim dts_obj As New Data.DataTable

        Dim nombre_pers As String
        Dim abreviatura_pers As String
        Dim nombre_obj As String
        Dim codigo_obj As Integer
        Dim status_obj As Integer
        Dim avance_obj As Decimal
        Dim abreviatura_obj As String
        Dim codigo_pers As Integer
        Dim bgcolor_obj As String
        Dim bordercolor_obj As String
        Dim meta_obj As Decimal
        Dim ancho_contenedorobjetivos As Integer
        Dim colorfila As String

        bgcolor_obj = "#FFFFFF"
        bordercolor_obj = "#FFFFFF"
        colorfila = "#FFFFFF"

        dts = obj.ConsultarPerspectivas("%")

        For i As Integer = 0 To dts.Rows.Count() - 1
            nombre_pers = dts.Rows(i).Item("Descripcion").ToString
            abreviatura_pers = dts.Rows(i).Item("Abreviatura").ToString
            codigo_pers = dts.Rows(i).Item("Codigo").ToString

            'dts_obj = obj.ListarObjetivosSegunPerspectiva(2011, 3, 547, codigo_pers)
            dts_obj = obj.ListarObjetivosSegunPerspectiva(ddlPeriodo.SelectedValue, _
                                                            ddlPlan.SelectedValue, _
                                                            ddlCco.SelectedValue, codigo_pers)

            'El ancho del div que contiene los objetivos, es proporcional al numero de divs de objetivos
            '100px es la anchura que ocupa cada div de objetivos            
            ancho_contenedorobjetivos = (110 * dts_obj.Rows.Count) - 40


            'Si la Fila es Numero Par, se pinta de color claro
            If Val((i + 1) / 2) - Int(Val((i + 1) / 2)) <> 0 Then
                colorfila = "#6f98ae"
            Else
                colorfila = "#415864"
            End If

            ''border:1px solid #fed8d5;    de la perspectiva
            Me.contenedor.InnerHtml = Me.contenedor.InnerHtml & "<div id='fila" & i & _
                    "' style='border:1px solid #3063c5; position:relative; padding-left:10px; padding-top:10px; background-color:" & _
                    colorfila & "'><div id='" & nombre_pers & _
                    "' style='float:left; width:200px; font-size: 14pt; color:#FFFFFF'> " & nombre_pers & _
                    "</div> <div id='objetivos" & i & "' style='width:" & ancho_contenedorobjetivos _
                    & "px; margin: 0 auto; min-height: 110px'>"

            For x As Integer = 0 To dts_obj.Rows.Count() - 1

                nombre_obj = dts_obj.Rows(x).Item("nombre_obj").ToString
                codigo_obj = dts_obj.Rows(x).Item("codigo_obj").ToString

                If IsDBNull(dts_obj.Rows(x).Item("codigo_st")) Then
                    status_obj = 0
                Else
                    status_obj = dts_obj.Rows(x).Item("codigo_st")
                End If

                If IsDBNull(dts_obj.Rows(x).Item("avance_vo")) Then
                    avance_obj = 0
                Else
                    avance_obj = dts_obj.Rows(x).Item("avance_vo")
                End If

                abreviatura_obj = dts_obj.Rows(x).Item("abreviatura_obj").ToString
                meta_obj = dts_obj.Rows(x).Item("meta_obj").ToString

                'Definir colores
                If status_obj = 1 Then
                    bgcolor_obj = "#c5f4b5"
                    bordercolor_obj = "#488e00"
                End If

                If status_obj = 2 Then
                    bgcolor_obj = "#F2F5A9"
                    bordercolor_obj = "#FACC2E"
                End If

                If status_obj = 3 Then
                    bgcolor_obj = "#fed8d5"
                    bordercolor_obj = "#e99491"
                End If

                Me.contenedor.InnerHtml = Me.contenedor.InnerHtml & "<div id='" & abreviatura_obj & _
                                        "' style='float:left; font-size: 10pt; width: 70px'> <div class='borde-redondeado' style='background-color:" & _
                                        bgcolor_obj & "; border:15px solid " & bordercolor_obj & "'>" & avance_obj & _
                                        "% de " & meta_obj & "%</div> <div style='text-align:center; font-weight:bold; width: 100px; color: #FFFFFF'>" & _
                                        abreviatura_obj & "     <img src='../../images/atencion.gif' alt='" & Trim(nombre_obj) & "'/></div></div>"

                '../Images/transparente.gif

                If x < dts_obj.Rows.Count - 1 Then
                    Me.contenedor.InnerHtml = Me.contenedor.InnerHtml & "<div style='float:left; width:40px'> </div>"

                End If

            Next

            Me.contenedor.InnerHtml = Me.contenedor.InnerHtml & "</div></div>"
            Me.contenedor.InnerHtml = Me.contenedor.InnerHtml & "<div style='clear:both; height:30px'> </div>"
        Next
    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Try
            Me.contenedor.InnerHtml = ""
            CargarTableroControl()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class



