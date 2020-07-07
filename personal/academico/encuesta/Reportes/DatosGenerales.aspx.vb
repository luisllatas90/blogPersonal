
Partial Class Encuesta_Reportes_DatosGenerales
    Inherits System.Web.UI.Page

  
    Protected Sub CmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdExportar.Click
        Axls()
    End Sub

    Private Sub Axls()
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=ReporteInvestigaciones.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        Response.Write(ClsFunciones.HTML(Me.GvDatosGenerales, "Matriz de Acreditación Universitaria: Datos Generales - Estudiantes - al ", "Encuesta de Acreditación Universitaria - Campus virtual USAT"))
        Response.End()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LblTotal.Text = "Se encontraron " & GvDatosGenerales.Rows.Count & " registros"
        End If
    End Sub

    Protected Sub GvDatosGenerales_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvDatosGenerales.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            With fila.Row
                e.Row.Cells(9).Text = Mid(.Item("Tipo Col").ToString, 2, .Item("Tipo Col").ToString.Length)
                e.Row.Cells(16).Text = Mid(.Item("Modalidad Est").ToString, 2, .Item("Modalidad Est").ToString.Length)
                e.Row.Cells(19).Text = IIf(.Item("Preparación Casa") = 1, "SI", "NO")
                e.Row.Cells(20).Text = IIf(.Item("Preparación Acad") = 1, "SI", "NO")
                e.Row.Cells(22).Text = IIf(.Item("Preparación Escuela Pre") = 1, "SI", "NO")
                e.Row.Cells(24).Text = IIf(.Item("Habla Ingles") = "4Elegir", "Nulo", Mid(.Item("Habla Ingles").ToString, 2, .Item("Habla Ingles").ToString.Length))
                e.Row.Cells(25).Text = IIf(.Item("Lee Ingles") = "4Elegir", "Nulo", Mid(.Item("Lee Ingles").ToString, 2, .Item("Lee Ingles").ToString.Length))
                e.Row.Cells(26).Text = IIf(.Item("Escribe Ingles") = "4Elegir", "Nulo", Mid(.Item("Escribe Ingles").ToString, 2, .Item("Escribe Ingles").ToString.Length))
                e.Row.Cells(28).Text = IIf(.Item("Certificacion Ingles") = 1, "SI", "NO")
                e.Row.Cells(30).Text = IIf(.Item("Habla Frances") = "4Elegir", "Nulo", Mid(.Item("Habla Frances").ToString, 2, .Item("Habla Frances").ToString.Length))
                e.Row.Cells(31).Text = IIf(.Item("Lee Frances") = "4Elegir", "Nulo", Mid(.Item("Lee Frances").ToString, 2, .Item("Lee Frances").ToString.Length))
                e.Row.Cells(32).Text = IIf(.Item("Escribe Frances") = "4Elegir", "Nulo", Mid(.Item("Escribe Frances").ToString, 2, .Item("Escribe Frances").ToString.Length))
                e.Row.Cells(34).Text = IIf(.Item("Certificación Frances") = 1, "SI", "NO")
                e.Row.Cells(36).Text = IIf(.Item("Habla Italiano") = "4Elegir", "Nulo", Mid(.Item("Habla Italiano").ToString, 2, .Item("Habla Italiano").ToString.Length))
                e.Row.Cells(37).Text = IIf(.Item("Lee Italiano") = "4Elegir", "Nulo", Mid(.Item("Lee Italiano").ToString, 2, .Item("Lee Italiano").ToString.Length))
                e.Row.Cells(38).Text = IIf(.Item("Escribe Italiano") = "4Elegir", "Nulo", Mid(.Item("Escribe Italiano").ToString, 2, .Item("Escribe Italiano").ToString.Length))
                e.Row.Cells(40).Text = IIf(.Item("Certificación Italiano") = 1, "SI", "NO")
                e.Row.Cells(42).Text = IIf(.Item("Otro Idioma Habla") = "4Elegir", "Nulo", Mid(.Item("Otro Idioma Habla").ToString, 2, .Item("Otro Idioma Habla").ToString.Length))
                e.Row.Cells(43).Text = IIf(.Item("Otro Idioma Lee") = "4Elegir", "Nulo", Mid(.Item("Otro Idioma Lee").ToString, 2, .Item("Otro Idioma Lee").ToString.Length))
                e.Row.Cells(44).Text = IIf(.Item("Otro Idioma Escribe") = "4Elegir", "Nulo", Mid(.Item("Otro Idioma Escribe").ToString, 2, .Item("Otro Idioma Escribe").ToString.Length))
                e.Row.Cells(46).Text = IIf(.Item("Certificación Italiano") = 1, "SI", "NO")
                e.Row.Cells(48).Text = IIf(.Item("Es Discapacitado") = 1, "SI", "NO")
                e.Row.Cells(50).Text = IIf(.Item("Tiene Seguro") = 1, "SI", "NO")
                e.Row.Cells(52).Text = IIf(.Item("Religión que Profesa") = 1, "SI", "NO")
                e.Row.Cells(53).Text = Mid(.Item("Religión").ToString, 2, .Item("Religión").ToString.Length)
                e.Row.Cells(54).Text = IIf(.Item("Religión") = "1Católica", IIf(.Item("Bautizado") = 1, "SI", "NO"), "")
                e.Row.Cells(55).Text = IIf(.Item("Religión") = "1Católica", IIf(.Item("Reconciliación") = 1, "SI", "NO"), "")
                e.Row.Cells(56).Text = IIf(.Item("Religión") = "1Católica", IIf(.Item("Eucaristía") = 1, "SI", "NO"), "")
                e.Row.Cells(57).Text = IIf(.Item("Religión") = "1Católica", IIf(.Item("Confirmación") = 1, "SI", "NO"), "")
                e.Row.Cells(58).Text = IIf(.Item("Religión") = "1Católica", IIf(.Item("Unción de los Enfermos") = 1, "SI", "NO"), "")
                e.Row.Cells(59).Text = IIf(.Item("Religión") = "1Católica", IIf(.Item("Matrimonio") = 1, "SI", "NO"), "")
                e.Row.Cells(60).Text = IIf(.Item("Religión") = "1Católica", IIf(.Item("Orden Sacerdotal") = 1, "SI", "NO"), "")
                e.Row.Cells(61).Text = Mid(.Item("Frec. Reconciliacion").ToString, 2, .Item("Frec. Reconciliacion").ToString.Length)
                e.Row.Cells(62).Text = Mid(.Item("Frec. Eucaristia").ToString, 2, .Item("Frec. Eucaristia").ToString.Length)
                e.Row.Cells(63).Text = IIf(.Item("Participa Grupo Parr.") = 1, "SI", "NO")
                e.Row.Cells(65).Text = Mid(.Item("Estado Civil").ToString, 2, .Item("Estado Civil").ToString.Length)
                e.Row.Cells(66).Text = IIf(.Item("Tiene Hijos") = 1, "SI", "NO")
                e.Row.Cells(68).Text = IIf(.Item("Num. Hijos") >= 1, .Item("Hijo 1").ToString, "")
                e.Row.Cells(69).Text = IIf(.Item("Num. Hijos") >= 1, .Item("Sexo H1").ToString, "")
                e.Row.Cells(70).Text = IIf(.Item("Num. Hijos") >= 1, .Item("Fecha Nac H1").ToString, "")
                e.Row.Cells(71).Text = IIf(.Item("Num. Hijos") >= 2, .Item("Hijo 2").ToString, "")
                e.Row.Cells(72).Text = IIf(.Item("Num. Hijos") >= 2, .Item("Sexo H2").ToString, "")
                e.Row.Cells(73).Text = IIf(.Item("Num. Hijos") >= 2, .Item("Fecha Nac H2").ToString, "")
                e.Row.Cells(74).Text = IIf(.Item("Num. Hijos") >= 3, .Item("Hijo 3").ToString, "")
                e.Row.Cells(75).Text = IIf(.Item("Num. Hijos") >= 3, .Item("Sexo H3").ToString, "")
                e.Row.Cells(76).Text = IIf(.Item("Num. Hijos") >= 3, .Item("Fecha Nac H3").ToString, "")
                e.Row.Cells(77).Text = IIf(.Item("Num. Hijos") >= 4, .Item("Hijo 4").ToString, "")
                e.Row.Cells(78).Text = IIf(.Item("Num. Hijos") >= 4, .Item("Sexo H4").ToString, "")
                e.Row.Cells(79).Text = IIf(.Item("Num. Hijos") >= 4, .Item("Fecha Nac H4").ToString, "")
                e.Row.Cells(80).Text = IIf(.Item("Num. Hijos") >= 5, .Item("Hijo 5").ToString, "")
                e.Row.Cells(81).Text = IIf(.Item("Num. Hijos") >= 5, .Item("Sexo H5").ToString, "")
                e.Row.Cells(82).Text = IIf(.Item("Num. Hijos") >= 5, .Item("Fecha Nac H5").ToString, "")
            End With
        End If
    End Sub
End Class
