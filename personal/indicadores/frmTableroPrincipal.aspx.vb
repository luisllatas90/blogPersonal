Imports System.Collections.Generic

'Imports System.Data
Imports Microsoft.Reporting.WebForms

Partial Class indicadores_frmTableroPrincipal
    Inherits System.Web.UI.Page
    Dim usuario As Integer

    Public ReadOnly Property Nombre() As Integer
        Get
            Dim x As Integer
            x = Val(ddlIndicadores.SelectedItem)

            'Return 2
            Return x
            'Return ddlIndicadores.SelectedValue
            'Return ddlCeCo.SelectedValue

        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                'Debe tomar del inicio de sesión
                usuario = Request.QueryString("id")

                CargarComboPeriodos()
                CargarComboPlanes()
                CargarComboCentroCostos()

            End If

            
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboObjetivos(ByVal codigo_pers As Integer, ByVal combo As DropDownList)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListarObjetivos(codigo_pers, ddlPlan.SelectedValue)

            If dts.Rows.Count > 0 Then                
                combo.DataSource = dts
                combo.DataValueField = "Codigo"
                combo.DataTextField = "Descripcion"
                combo.DataBind()
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

    Private Sub CargarComboCentroCostos()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListarCentroCostosConPlan()

            If dts.Rows.Count > 0 Then
                ddlCeCo.DataSource = dts
                ddlCeCo.DataValueField = "codigo_cco"
                ddlCeCo.DataTextField = "descripcion_cco"
                ddlCeCo.DataBind()
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
        Dim codigo_pers As Integer
        Dim bgcolor_obj As String

        Dim altomenu As Integer


        bgcolor_obj = "#FFFFFF"        

        dts = obj.ListaPerspectivasSegunPlanYCeCo(ddlPlan.SelectedValue, ddlCeCo.SelectedValue)
        altomenu = 70 * dts.Rows.Count

        For i As Integer = 0 To dts.Rows.Count() - 1
            nombre_pers = dts.Rows(i).Item("Perspectiva").ToString
            codigo_pers = dts.Rows(i).Item("codigo_pers").ToString

            ''border:1px solid #fed8d5;    de la perspectiva
            Me.menuperspectivas.InnerHtml = Me.menuperspectivas.InnerHtml & _
                    "<div id='" & codigo_pers & _
            "' class='degradado'><a href='#' onclick='CargarCuerpoPerspectiva(" & codigo_pers & "," & """" & nombre_pers & """)'>" & nombre_pers & "</a></div>"

            'Crear combo

            Dim nuevocombo As New DropDownList

            nuevocombo.ID = "ddlObjetivos" & codigo_pers.ToString            
            nuevocombo.Attributes.Add("class", "combos")
            nuevocombo.Attributes.Add("onchange", "javascript:GetCountries(this,event);")

            'nuevoCmb.Items.Add("---Seleccione el Plazo---");
            'nuevoCmb.Items.Add("Corto Plazo");
            'nuevoCmb.Items.Add("Mediano Plazo");
            'nuevoCmb.Items.Add("Largo Plazo");
            'nuevoCmb.SelectedIndex = 0;

            'Abrir div dentro del panel
            Dim literal As New Literal

            literal.Text = "<div id='div" & codigo_pers & "' style='display:none'>"
            Me.panelComboObjetivo.Controls.Add(literal)

            'Eliminar combo del panel, si ya existe
            'Me.panelComboObjetivo.Controls.Remove(nuevocombo)

            'Agregar combo al panel, dentro del div creado
            Me.panelComboObjetivo.Controls.Add(nuevocombo)
            ' Add a spacer in the form of an HTML <BR> element
            Me.panelComboObjetivo.Controls.Add((New LiteralControl(" ")))


            'Cerrar el div
            Dim literal2 As New Literal
            literal2.Text = "</div>"
            Me.panelComboObjetivo.Controls.Add(literal2)

            'Cargar combo
            CargarComboObjetivos(codigo_pers, nuevocombo)

        Next


    End Sub


    Protected Sub ddlPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlan.SelectedIndexChanged
        Try            
            If ddlPlan.SelectedValue <> 0 And ddlCeCo.SelectedValue <> 0 And ddlPeriodo.SelectedValue <> 0 Then
                'menuperspectivas.InnerHtml = "<div class='degradado'>TODOS</div>"
                menuperspectivas.InnerHtml = "<div class='degradado'><a href='#' onclick='MostrarContenedorSemaforos()'>TODOS</a></div>"
                CargarTableroControl()
                MostrarContenedorSemaforos()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub ddlCeCo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCeCo.SelectedIndexChanged
        Try
            If ddlCeCo.SelectedValue <> 0 And ddlPlan.SelectedValue <> 0 And ddlPeriodo.SelectedValue <> 0 Then
                'menuperspectivas.InnerHtml = "<div class='degradado'>TODOS</div>"
                menuperspectivas.InnerHtml = "<div class='degradado'><a href='#' onclick='MostrarContenedorSemaforos()'>TODOS</a></div>"
                CargarTableroControl()
                MostrarContenedorSemaforos()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    <System.Web.Services.WebMethod()> _
Public Shared Function GetCountries(ByVal num As Integer) As List(Of Indicador)

        Dim indicadores As List(Of Indicador) = New List(Of Indicador)

        Dim obj As New clsIndicadores
        Dim dts As New Data.DataTable
        Dim indicador

        dts = obj.CargarIndicadoresSegunObjetivo(num)

        If dts.Rows.Count > 0 Then
            For i As Integer = 0 To dts.Rows.Count - 1

                'myAL.Add(dts.Rows(i).Item("Descripcion"))
                indicador = New Indicador
                indicador.Name = dts.Rows(i).Item("Descripcion")
                indicador.Id = dts.Rows(i).Item("codigo")
                indicadores.Add(indicador)
            Next

        End If

        Return indicadores
    End Function


    'Protected Sub ddlIndicadores_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlIndicadores.SelectedIndexChanged
    '    Try
    '        Response.Write("moni")
    '        Cargar()
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    'Private Sub Cargar()

    '    'Dim instance As New Microsoft.Reporting.WebForms.ReportParameter("Oficina", "01")
    '    'Dim instance1 As New Microsoft.Reporting.WebForms.ReportParameter("Fecha", "2004.01.01")
    '    'Dim instance2 As New Microsoft.Reporting.WebForms.ReportParameter("Ordenadopor", "01")

    '    Dim instance As New Microsoft.Reporting.WebForms.ReportParameter("codigo_ind", ddlIndicadores.SelectedValue)
    '    Dim instance1 As New Microsoft.Reporting.WebForms.ReportParameter("año", ddlPeriodo.SelectedValue)
    '    Dim instance2 As New Microsoft.Reporting.WebForms.ReportParameter("id", 0)
    '    Dim instance3 As New Microsoft.Reporting.WebForms.ReportParameter("ctf", 0)
    '    Dim instance4 As New Microsoft.Reporting.WebForms.ReportParameter("mod", 0)

    '    Dim prms(4) As Microsoft.Reporting.WebForms.ReportParameter
    '    prms(0) = instance
    '    prms(1) = instance1
    '    prms(2) = instance2
    '    prms(3) = instance3
    '    prms(4) = instance4

    '    rptGrafico.ServerReport.ReportServerUrl = New Uri("https://intranet.usat.edu.pe/rptusat/?/PRIVADOS/ACADEMICO")

    '    rptGrafico.ServerReport.ReportPath = "/IND_GraficoBarrasUnIndicador"
    '    rptGrafico.ServerReport.SetParameters(prms)
    '    rptGrafico.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote

    '    'rptGrafico.RefreshReport()

    '    rptTabla.ServerReport.ReportServerUrl = New Uri("https://intranet.usat.edu.pe/rptusat/?/PRIVADOS/ACADEMICO")
    '    rptTabla.ServerReport.ReportPath = "/IND_GraficoBarrasUnIndicador"
    '    rptTabla.ServerReport.SetParameters(prms)
    '    rptTabla.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote

    '    'rptTabla.RefreshReport()

    'End Sub


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

            'dts_obj = obj.ListarObjetivosSegunPerspectiva(2011, 3, 547, codigo_pers)
            dts_obj = obj.ListarObjetivosSegunPerspectiva(ddlPeriodo.SelectedValue, _
                                                            ddlPlan.SelectedValue, _
                                                            ddlCeCo.SelectedValue, codigo_pers)
            'ddlCco.SelectedValue, codigo_pers)


            'El ancho del div que contiene los objetivos, es proporcional al numero de divs de objetivos
            '100px es la anchura que ocupa cada div de objetivos            
            ancho_contenedorobjetivos = (90 * dts_obj.Rows.Count) - 20


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
                                        "% de " & meta_obj & "%</div> <div style='text-align:center; font-weight:bold; width: 100px; color: #FFFFFF'>" & _
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


    Private Sub MostrarContenedorSemaforos()
        Try
            Me.contenedor.InnerHtml = ""

            Me.contenedorperspectivas.Visible = False

            Me.titulopagina.InnerHtml = "TABLERO DE CONTROL"
            CargarSemaforos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub ddlPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPeriodo.SelectedIndexChanged

        Try
            If ddlPlan.SelectedValue <> 0 And ddlCeCo.SelectedValue <> 0 And ddlPeriodo.SelectedValue <> 0 Then
                menuperspectivas.InnerHtml = "<div class='degradado'><a href='#' onclick='MostrarContenedorSemaforos()'>TODOS</a></div>"

                CargarTableroControl()
                MostrarContenedorSemaforos()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Protected Sub ddlindicadores_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlIndicadores.SelectedIndexChanged

    'End Sub


    Protected Sub boton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles boton.Click
        Dim x As Integer
        x = Val(ddlIndicadores.SelectedItem)

        Response.Write(ddlIndicadores.SelectedItem)
        Response.Write("  **   ")
        Response.Write(ddlIndicadores.SelectedValue.GetType)
        Response.Write("  **   ")
        Response.Write(x.GetType)
        Response.Write("  **   ")
        Response.Write(x)

    End Sub
End Class



