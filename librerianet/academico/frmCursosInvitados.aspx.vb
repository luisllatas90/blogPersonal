
Partial Class academico_frmCursosInvitados
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'se le envia la sesion con AsignaSesionesNivelacion.aspx, de librerianet por el problema
                'que existe en el compartir sesiones.
                CargarCursosInvitado()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarCursosInvitado()
        Try
            Dim obj As New clsProfesionalizacion
            Dim dts As New Data.DataTable

            'Pruebas: 
            'Response.Write("testing session codigo_alu:  " & Session("codigo_alu"))
            dts = obj.ConsultarCursosInvitadosAlumno(Session("codigo_alu"))        'Antes del 03.07.2013
            'Response.Write("Datos: " & dts.Rows.Count)
            If dts.Rows.Count > 0 Then
                gvLista.DataSource = dts
                gvLista.DataBind()
            Else
                gvLista.DataSource = Nothing
                gvLista.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub ibtnConfirma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim dts As New Data.DataTable
            Dim ibtnConfirma As ImageButton
            Dim row As GridViewRow
            Dim obj As New clsProfesionalizacion
            ibtnConfirma = sender
            row = ibtnConfirma.NamingContainer

            Dim ObjMailNet As New ClsMail
            Dim mensaje As String = ""
            Dim para As String = ""

            'Verificamos la session del alumno, para poder continuar.
            If Session.Item("codigo_alu") Is Nothing Then
                Page.RegisterStartupScript("ok", "<script>alert('Su sesión a expirado, favor de volver a cargar la página nivelación del menú.');</script>")
                Exit Sub
            End If
            '====================;

            '===================================================================================================================================================================================
            'Este bloque se va ha funcionar de manare temporal, para identificar un problema de sesion del alumno con el codigo_cac
            'Si la Session("codigo_alu")<=400 quiere decir que se esta confundiendo con el codigo_cac
            If Session("codigo_alu") <= 400 Then
                para = "</br><font face='Courier'>" & "Estimado(a): <b>" & "Administrador del Módulo - Profesionalización" & "</b>"
                mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, la session de alumno es: <b>" & Session("codigo_alu") & "</b> </P>"
                mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"
                ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Profesionalizacón - Session(Alumno)", "dguevara@usat.edu.pe", "Programas de Profesionalización", para & mensaje, True)
            End If
            '===================================================================================================================================================================================


            '===## Iniio del proceso ### ====
            'Este proceso, genera la deuda y actualiza la el detalle matricula a Matriculado.
            'Envia el correo al coordinardor 
            dts = obj.EjecutaProcesoNivelacion(Session("codigo_alu"), _
                                               gvLista.DataKeys.Item(row.RowIndex).Values("codigo_Cup"), _
                                               gvLista.DataKeys.Item(row.RowIndex).Values("codigo_Dma"), _
                                               CType(row.Cells(9).Text, Decimal))
            If dts.Rows(0).Item("codigo_deu") > 0 Then
                For i As Integer = 0 To dts.Rows.Count - 1
                    '========================
                    If dts.Rows(i).Item("emailCoordinar") <> "" Then
                        para = "</br><font face='Courier'>" & "Estimado(a): <b>" & dts.Rows(i).Item("NombreCoordinador").ToString.ToUpper & "</b>"
                        mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, el alumno <b>" & dts.Rows(i).Item("alumno").ToString.ToUpper & "</b> del Programa <b>" & dts.Rows(i).Item("ProgramaProfesionalizacion").ToString.ToUpper & "</b> ha confirmado su matricula para el curso <b>" & gvLista.DataKeys.Item(row.RowIndex).Values("nombre_Cur").ToString.ToUpper & "</b> </P>"
                        mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"

                        'Descomentar:
                        ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Profesionalizacón", dts.Rows(i).Item("emailCoordinar").ToString, "Programas de Profesionalización", para & mensaje, True)

                        'Comentar
                        'ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Profesionalizacón", "dguevara@usat.edu.pe", "Pruebas - Programas de Profesionalización", para & mensaje, True)


                        'Response.Write("Codigo_alu: " + Session("codigo_alu"))
                        'Response.Write("<br />")
                        'Response.Write("Codigo_cup: " + gvLista.DataKeys.Item(row.RowIndex).Values("codigo_Cup").ToString.ToUpper)
                        'Response.Write("<br />")
                        'Response.Write("NombreCoordinador " + dts.Rows(i).Item("NombreCoordinador").ToString.ToUpper)
                        'Response.Write("<br />")
                        'Response.Write("Alumno " + dts.Rows(i).Item("alumno").ToString.ToUpper)
                        'Response.Write("<br />")
                        'Response.Write("Nombre Curso:  " + gvLista.DataKeys.Item(row.RowIndex).Values("nombre_Cur").ToString.ToUpper)
                        'Response.Write("<br />")
                        'Response.Write("Nombre Curso:  " + dts.Rows(i).Item("ProgramaProfesionalizacion").ToString.ToUpper)
                        'Response.Write("<br />")
                        'Response.Write("Email Coordinador:  " + dts.Rows(i).Item("emailCoordinar").ToString)
                    End If
                Next

                'Response.Redirect("..\AsignaSesionesNivelacion.aspx?x=" & Session("codigo_alu") & "&y=" & dts.Rows(0).Item("codigo_Cac") & "&w=" & dts.Rows(0).Item("codigo_pes") & "&z=cursosmatriculados.asp")
                'CargarCursosInvitado()
            Else
                If dts.Rows(0).Item("codigo_deu") = -1 Then
                    Page.RegisterStartupScript("ok", "<script>alert('Ocurrio un problema al confirmar la matricula, favor de volver intentar.');</script>")
                Else
                    'Aviso Sistemas.
                    para = "</br><font face='Courier'>" & "Estimado: <b>" & "Desarrollo Sistemas" & "</b>"
                    mensaje = "</br></br><P><ALIGN='justify'> CODIGO_ALU<b>" & Session("codigo_alu") & "<b> CODIGO_CUP " & gvLista.DataKeys.Item(row.RowIndex).Values("codigo_Cup") & "</b> CODIGO_DMA " & gvLista.DataKeys.Item(row.RowIndex).Values("codigo_Dma") & "</b> "
                    mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"
                    'Descomentar
                    'ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Profesionalizacón", "dguevara@usat.edu.pe", "Err. Confirmar Matricula.", para & mensaje, True)
                    Page.RegisterStartupScript("ok", "<script>alert('Ocurrio un problema al confirmar la matricula, favor de volver intentar.');</script>")
                End If
            End If
            'Lista los cursos.
            CargarCursosInvitado()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ibtnRechaza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            'Verificamos la session del alumno, para poder continuar.
            If Session.Item("codigo_alu") Is Nothing Then
                Page.RegisterStartupScript("ok", "<script>alert('Su sesión a expirado, favor de volver a cargar la página nivelación del menú.');</script>")
                Exit Sub
            End If
            '====================;

            Dim dts As New Data.DataTable
            Dim obj As New clsProfesionalizacion
            Dim ibtnRechaza As ImageButton
            Dim row As GridViewRow
            'Dim hfCodigo_cup As HiddenField

            ibtnRechaza = sender
            row = ibtnRechaza.NamingContainer

            Dim ObjMailNet As New ClsMail
            Dim mensaje As String = ""
            Dim para As String = ""

            '===========
            'Para pruebas:
            'Response.Write("<br />")
            'Response.Write("codigo_alu: " & Session("codigo_alu"))
            'Response.Write("<br />")
            'Response.Write("codigo_cup: " & gvLista.DataKeys.Item(row.RowIndex).Values("codigo_Cup"))
            'Response.Write("<br />")
            'Response.Write("codigo_dma: " & gvLista.DataKeys.Item(row.RowIndex).Values("codigo_Dma"))



            dts = obj.RechazaProcesoNivelacion(Session("codigo_alu"), _
                                               gvLista.DataKeys.Item(row.RowIndex).Values("codigo_Cup"), _
                                               gvLista.DataKeys.Item(row.RowIndex).Values("codigo_Dma"))

            '========================
            'pruebas:
            'Response.Write("<br />")
            'Response.Write("emailCoordinar " + dts.Rows(0).Item("emailCoordinar"))
            'Response.Write("<br />")
            'Response.Write("NombreCoordinador " + dts.Rows(0).Item("NombreCoordinador").ToString)
            'Response.Write("<br />")
            'Response.Write("alumno: " + dts.Rows(0).Item("alumno").ToString.ToUpper)
            'Response.Write("<br />")
            'Response.Write("ProgramaProfesionalizacion: " + dts.Rows(0).Item("ProgramaProfesionalizacion").ToString.ToUpper)


            ''''->> Email al coordinador  <<---
            If dts.Rows(0).Item("emailCoordinar") <> "" Then
                para = "</br><font face='Courier'>" & "Estimado(a): <b>" & dts.Rows(0).Item("NombreCoordinador").ToString.ToUpper & "</b>"
                mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, el alumno <b>" & dts.Rows(0).Item("alumno").ToString.ToUpper & "</b> del Programa <b>" & dts.Rows(0).Item("ProgramaProfesionalizacion").ToString.ToUpper & "</b> ha rechazado la matricula para el curso de nivelación <b>" & gvLista.DataKeys.Item(row.RowIndex).Values("nombre_Cur").ToString.ToUpper & "</b> que se llevara acabo del " & gvLista.DataKeys.Item(row.RowIndex).Values("fechainicio_Cup").ToString & " al " & gvLista.DataKeys.Item(row.RowIndex).Values("fechafin_Cup").ToString & "</P>"
                mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"

                'Descomentar
                ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Profesionalizacón", dts.Rows(0).Item("emailCoordinar").ToString, "Programas de Profesionalización", para & mensaje, True)
            End If

            'Response.Redirect("..\AsignaSesionesNivelacion.aspx?x=" & Session("codigo_alu") & "&y=" & dts.Rows(0).Item("codigo_Cac") & "&w=" & dts.Rows(0).Item("codigo_pes") & "&z=cursosmatriculados.asp")

            'Lista los cursos.
            CargarCursosInvitado()

            '========================
            'Response.Write("<br/>")
            'Response.Write("<br/>")
            'Response.Write("Codigo_alu" & Session("codigo_alu"))
            'Response.Write("<br/>")
            'Response.Write("codigo_Cac" & dts.Rows(0).Item("codigo_Cac"))
            'Response.Write("<br/>")
            'Response.Write("codigo_pes" & dts.Rows(0).Item("codigo_pes"))
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class
