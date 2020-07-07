
Partial Class indicadores_frmResumenPerspectivas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                MostrarContenedorSemaforos()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub MostrarContenedorSemaforos()
        Try
            Me.contenedor.InnerHtml = ""
            CargarSemaforos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarSemaforos()

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
            colorfila = dts.Rows(i).Item("Color").ToString


            dts_obj = obj.ListarObjetivosSegunPerspectiva(Request.QueryString("anio"), Request.QueryString("Codigo_pla"), Request.QueryString("codigo_cco"), codigo_pers)


            'El ancho del div que contiene los objetivos, es proporcional al numero de divs de objetivos
            '100px es la anchura que ocupa cada div de objetivos            
            ancho_contenedorobjetivos = (95 * dts_obj.Rows.Count) - 20

            ''Si la Fila es Numero Par, se pinta de color claro
            'If Val((i + 1) / 2) - Int(Val((i + 1) / 2)) <> 0 Then
            '    colorfila = "#6f98ae"
            'Else
            '    colorfila = "#415864"
            'End If


            'Los Colores para las perspectivas se asignan dinamicamnte de acuerdo al color asiganado en la BD.
            ''border:1px solid #fed8d5;    de la perspectiva
            Me.contenedor.InnerHtml = Me.contenedor.InnerHtml & "<div id='fila" & i & _
            "' style='border:1px solid #3063c5; position:relative; padding-left:10px; padding-top:10px; background-color:" & _
            colorfila & "'><div id='" & nombre_pers & _
            "' style='float:left; width:200px; font-size: 14pt; color:#444649'> " & nombre_pers & _
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

                ElseIf status_obj = 2 Then
                    bgcolor_obj = "#F2F5A9"
                    bordercolor_obj = "#FACC2E"

                ElseIf status_obj = 3 Then
                    bgcolor_obj = "#fed8d5"
                    bordercolor_obj = "#F50743"
                Else
                    bgcolor_obj = "#fed8d5"
                    bordercolor_obj = "#F50743"
                End If

                Me.contenedor.InnerHtml = Me.contenedor.InnerHtml & "<div id='" & abreviatura_obj & _
                                        "' style='float:left; font-size: 10pt; width: 70px'> <div class='borde-redondeado-objetivo' style='background-color:" & _
                                        bgcolor_obj & "; border:15px solid " & bordercolor_obj & "'>" & avance_obj & _
                                        "% de " & meta_obj & "%</div> <div style='text-align:center; font-weight:bold; width: 100px; color: #0A0A0B'>" & _
                                        abreviatura_obj & "     <img src='../../images/atencion.gif' alt='" & Trim(nombre_obj) & "'/></div></div>"

                '../Images/transparente.gif

                If x < dts_obj.Rows.Count - 1 Then
                    Me.contenedor.InnerHtml = Me.contenedor.InnerHtml & "<div style='float:left; width:20px'> </div>"
                End If
            Next

            Me.contenedor.InnerHtml = Me.contenedor.InnerHtml & "</div></div>"
            Me.contenedor.InnerHtml = Me.contenedor.InnerHtml & "<div style='clear:both; height:30px'> </div>"
        Next
    End Sub


End Class
