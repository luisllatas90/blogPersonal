
Partial Class academico_cargalectiva_administrarcargalectiva_frmCargaAcademicaDocente
    Inherits System.Web.UI.Page

    Dim vCodigo_per As Integer
    Dim vCodigo_cac As Integer
    Dim vParametro As Integer
    Dim vDedicacion As String
    Dim vEx As Integer
    Dim vTh As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Call ListaCargaAcademica()
        End If
    End Sub

    Private Sub ListaCargaAcademica()
        Try
            vCodigo_per = Request.QueryString("codigo_per")
            vCodigo_cac = Request.QueryString("codigo_cac")
            vDedicacion = Request.QueryString("dedicacion")
            vParametro = Request.QueryString("parametrohoras")
            vTh = Request.QueryString("th")

            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim tbl As Data.DataTable
            Dim data As Data.DataTable
            tbl = obj.TraerDataTable("DetalleExcesoCargaAcademica", "DO", vCodigo_cac, vCodigo_per)

            If vTh > 0 Then 'No se ha enviado la descripción de la Dedicación
                data = obj.TraerDataTable("ConsultarPersonal", "HO", vCodigo_per)
                vDedicacion = data.rows(0).item("Descripcion_Ded").tostring
            End If

            If tbl.Rows.Count > 0 Then
                Me.lblCicloCac.text = tbl.rows(0).item("descripcion_Cac").tostring
                Me.lblDocente.text = tbl.rows(0).item("apellidoPat_Per") & " " & tbl.rows(0).item("apellidoMat_Per") & " " & tbl.rows(0).item("nombres_Per")
                Me.lblTotalCac.Text = tbl.Rows(0).Item("cargaTotal").ToString

                'If vParametro = 15 Then
                '    Me.lblDedicacion.text = vDedicacion & " - ( 15 Hrs. Semanales Permitidas)"
                'Else
                '    If vParametro = 10 Then
                '        Me.lblDedicacion.text = vDedicacion & " - ( 10 Hrs. Semanales Permitidas)"
                '    End If
                'else
                '    If vParametro = 18 Then
                '        Me.lblDedicacion.text = vDedicacion & " - ( 18 Hrs. Semanales Permitidas)"
                '    End If
                'End If


                If vParametro = 15 Then     'Medio Tiempo
                    Me.lblDedicacion.Text = vDedicacion & " - ( 15 Hrs. Semanales Permitidas)"
                End If
                If vParametro = 10 Then     'Tiempo Parcial <20 
                    Me.lblDedicacion.Text = vDedicacion & " - ( 10 Hrs. Semanales Permitidas)"
                End If
                If vParametro = 18 Then     'Tiempo Completo
                    Me.lblDedicacion.Text = vDedicacion & " - ( 18 Hrs. Semanales Permitidas)"
                End If


                If vTh > 0 Then    'Se ha enviado carga nueva por asignar. Se suma también para calcular el exceso.
                    If vParametro < (CInt(tbl.Rows(0).Item("cargaTotal")) + vTh) Then
                        vEx = (CInt(tbl.Rows(0).Item("cargaTotal")) + vTh) - CInt(vParametro)
                        lblMensaje.text = " (*) Asignando estas " & vTh & " Hrs., el docente estaría excediendo en " & vEx.ToString & " Hrs. su carga académica permitida."
                        btnCancelar.Visible = False 'Oculto botón que cierra el thickbox, ya que esta url ha sido abierta como popup
                    End If
                End If

                If vTh = 0 Then   'No se ha enviado carga nueva por asignar. Se calcula el exceso sobre la carga académica actual.
                    If vParametro < CInt(tbl.Rows(0).Item("cargaTotal")) Then
                        vEx = CInt(tbl.Rows(0).Item("cargaTotal")) - CInt(vParametro)

                        lblMensaje.text = " (*) El docente está excediendo en " & vEx.ToString & " Hrs su carga académica permitida."
                    End If
                End If

                gvCargaAcademica.DataSource = tbl
                gvCargaAcademica.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


End Class
