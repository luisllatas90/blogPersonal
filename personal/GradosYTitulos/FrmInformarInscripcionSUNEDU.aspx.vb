Imports System.Collections.Generic

Partial Class GradosYTitulos_FrmInformarInscripcionSUNEDU
    Inherits System.Web.UI.Page
    Dim contador As Integer = 0

    Private Sub ListarSesionesConsejo()
        Dim dt As New Data.DataTable
        Dim obj As New ClsGradosyTitulos
        dt = obj.ListaSesionConsejoU("L", "%")

        For i As Integer = 0 To dt.Rows.Count - 1
            Me.cboSesion.Items.Add(New ListItem(dt.Rows(i).Item("descripcion_scu").ToString, dt.Rows(i).Item("codigo_scu")))
        Next
        Me.cboSesion.DataBind()
    End Sub

    Private Sub ListarTipoDenominacion()
        Dim dt As New Data.DataTable
        Dim obj As New ClsGradosyTitulos
        dt = obj.ConsultarTipoDenominacion("GYT", "")

        For i As Integer = 0 To dt.Rows.Count - 1
            Me.cboTipoDenominacion.Items.Add(New ListItem(dt.Rows(i).Item("nombre").ToString, dt.Rows(i).Item("codigo")))
        Next
        Me.cboTipoDenominacion.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            ListarSesionesConsejo()
            ListarTipoDenominacion()
        End If
    End Sub

    Function Validar() As Boolean
        If Me.cboSesion.SelectedValue = "" And Me.cboTipoDenominacion.SelectedValue = "" Then
            If Me.txtBusqueda.Text.Length < 3 Then
                Me.divMensaje.InnerHtml = "Debe Ingresar al menos 3 Caracteres para Buscar Sin Filtros."
                Me.divMensaje.Attributes.Add("Class", "alert alert-danger")
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtBusqueda)
                Me.gvAlumnos.DataSource = Nothing
                Me.gvAlumnos.DataBind()
                Return False
            End If
        End If
        Return True
    End Function


    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
   
        contador = 0
        Me.lblcontador.Text = ""
        If Validar() = True Then
            Me.divMensaje.InnerHtml = ""
            Me.divMensaje.Attributes.Remove("Class")

            Dim obj As New ClsGradosyTitulos
            Dim dt As New Data.DataTable
            dt = obj.ConsultarInformarInscripcionSUNEDU(Me.cboSesion.SelectedValue, Me.cboTipoDenominacion.SelectedValue, Me.txtBusqueda.Text)
            Me.gvAlumnos.DataSource = dt
            Me.gvAlumnos.DataBind()

            Me.lblcontador.Text = " Filas seleccionadas de " + Me.gvAlumnos.Rows.Count.ToString + ""

            For Each row As GridViewRow In Me.gvAlumnos.Rows
                Dim chckrw As CheckBox = CType(row.FindControl("chkAutorizar"), CheckBox)
                If chckrw.Checked Then
                    contador += 1
                    row.ControlStyle.BackColor = Drawing.Color.AntiqueWhite
                    row.ControlStyle.Font.Bold = True
                Else
                    row.ControlStyle.BackColor = Drawing.Color.White
                    row.ControlStyle.Font.Bold = False
                End If
            Next
            Me.lblContadorSeleccionado.Text = contador.ToString
        End If

    End Sub

    Protected Sub chckchanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chckheader As CheckBox = CType(gvAlumnos.HeaderRow.FindControl("chkall"), CheckBox)
        contador = 0
        For Each row As GridViewRow In gvAlumnos.Rows
            Dim chckrw As CheckBox = CType(row.FindControl("chkAutorizar"), CheckBox)
            If chckheader.Checked = True Then
                chckrw.Checked = True
            Else
                chckrw.Checked = False
            End If
            If chckrw.Checked = True Then
                contador = contador + 1
                row.ControlStyle.BackColor = Drawing.Color.AntiqueWhite
                row.ControlStyle.Font.Bold = True
            Else
                row.ControlStyle.BackColor = Drawing.Color.White
                row.ControlStyle.Font.Bold = False
            End If
        Next
        Me.lblContadorSeleccionado.Text = contador.ToString
    End Sub

    Public Sub lnkContar_Click(ByVal sender As Object, ByVal e As EventArgs)

        For Each row As GridViewRow In Me.gvAlumnos.Rows
            Dim chckrw As CheckBox = CType(row.FindControl("chkAutorizar"), CheckBox)
            If chckrw.Checked Then
                contador += 1
                row.ControlStyle.BackColor = Drawing.Color.AntiqueWhite
                row.ControlStyle.Font.Bold = True
            Else
                row.ControlStyle.BackColor = Drawing.Color.White
                row.ControlStyle.Font.Bold = False
            End If

        Next
        Me.lblContadorSeleccionado.Text = contador.ToString
    End Sub


    Public Sub btnEnviarCorreo_Click(ByVal sender As Object, ByVal e As EventArgs)
        EnviarEmail()
    End Sub

    Sub EnviarEmail()
        Me.divMensaje.InnerHtml = ""
        Me.divMensaje.Attributes.Remove("Class")

        Dim JSONresult As String = ""

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable

        Dim objemail As New ClsMail
        Try
            Dim receptor, AsuntoCorreo As String
            Dim mensaje As String = ""
            Dim descripcion As String = ""

            receptor = ""

            Dim dt As New Data.DataTable
            dt.Columns.Add("denominacion")
            dt.Columns.Add("correos")
            dt.Columns.Add("codigos")


            For Each row As GridViewRow In Me.gvAlumnos.Rows
                Dim chckrw As CheckBox = CType(row.FindControl("chkAutorizar"), CheckBox)
                If chckrw.Checked Then
                    If dt.Rows.Count = 0 Then
                        dt.Rows.Add(Me.gvAlumnos.DataKeys(row.RowIndex).Values("descripcion_tdg").ToString, "", "")
                    Else
                        Dim contador As Integer = 0
                        For i As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(i).Item("denominacion").ToString = Me.gvAlumnos.DataKeys(row.RowIndex).Values("descripcion_tdg").ToString Then
                                contador = contador + 1
                            End If
                        Next
                        If contador = 0 Then
                            dt.Rows.Add(Me.gvAlumnos.DataKeys(row.RowIndex).Values("descripcion_tdg").ToString, "", "")
                        End If
                    End If
                    'receptor += Me.gvAlumnos.DataKeys(row.RowIndex).Values("email_alu").ToString + ","
                End If
            Next

            For Each row As GridViewRow In Me.gvAlumnos.Rows
                Dim chckrw As CheckBox = CType(row.FindControl("chkAutorizar"), CheckBox)
                If chckrw.Checked Then
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If Me.gvAlumnos.DataKeys(row.RowIndex).Values("descripcion_tdg").ToString = dt.Rows(j).Item("denominacion") Then
                            If dt.Rows(j).Item("correos") = "" Then
                                dt.Rows(j).Item("correos") = Me.gvAlumnos.DataKeys(row.RowIndex).Values("email_alu").ToString
                            Else
                                dt.Rows(j).Item("correos") += "," + Me.gvAlumnos.DataKeys(row.RowIndex).Values("email_alu").ToString
                            End If
                            If dt.Rows(j).Item("codigos") = "" Then
                                dt.Rows(j).Item("codigos") = Me.gvAlumnos.DataKeys(row.RowIndex).Values("codigo_egr").ToString
                            Else
                                dt.Rows(j).Item("codigos") += "," + Me.gvAlumnos.DataKeys(row.RowIndex).Values("codigo_egr").ToString
                            End If
                        End If
                    Next
                End If
            Next


            Dim dtr As New Data.DataTable
            Dim objGyt As New ClsGradosyTitulos

            If dt.Rows.Count > 0 Then

                For k As Integer = 0 To dt.Rows.Count - 1
                    'Response.Write(dt.Rows(k).Item("denominacion") + " - " + dt.Rows(k).Item("correos") + "<br/>")

                    Dim list As New List(Of Dictionary(Of String, Object))()

                    AsuntoCorreo = "Inscripción de diploma de " + dt.Rows(k).Item("denominacion").ToString

                    descripcion = ""

                    descripcion += "Estimado egresado: <br><br> Reciban mi cordial saludo, y a la vez, hago de su conocimiento que su diploma de " + dt.Rows(k).Item("denominacion").ToString
                    descripcion += " ya está inscrito en la Superintendencia Nacional de Educación Superior Universitario SUNEDU.<br><br>"
                    descripcion += "Esta información podrá encontrarla ingresando a: <a href='https://www.sunedu.gob.pe/registro-nacional-de-grados-y-titulos/'>https://www.sunedu.gob.pe/registro-nacional-de-grados-y-titulos/</a>  en el icono <b>Verifica si estás inscrito en el Registro Nacional de Grados Títulos</b><br><br>"
                    descripcion += "Ante cualquier inquietud estamos para atenderlos.<br><br>"

                    descripcion += "Atentamente"


                    receptor = dt.Rows(k).Item("correos")
                    receptor = "hcano@usat.edu.pe"

                    mensaje = ""

                    mensaje = mensaje + "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' />"
                    mensaje = mensaje + "<title>Evaluación de Proyecto</title>"
                    mensaje = mensaje + "<style type='text/css'>.usat { font-family:Humnst777 Lt BT;font-size:14px;} "
                    mensaje = mensaje + ".bolsa{color:#F1132A;font-family:Calibri;font-size: 13px;font-weight: 500;}</style></head>"
                    mensaje = mensaje + "<body>"
                    mensaje = mensaje + "<div style='text-align:left;width:100%'>"
                    mensaje = mensaje + "<table border='0' width='90%' cellpadding='0' cellspacing='0'><tr><td>"
                    mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:left;'><div class='usat'>" + descripcion + "</div></div>"
                    mensaje = mensaje + "</td></tr></tbody></table>"
                    mensaje = mensaje + "<table border='0' width='970%' cellpadding='0' cellspacing='0'><tr><td>"
                    mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:center;color:white'>"
                    mensaje = mensaje + "</div></td></tr></table>"
                    mensaje = mensaje + "<table border='0' width='90%' cellpadding='0' cellspacing='0'>"
                    mensaje = mensaje + "<tr><td style='background:none;border-bottom:1px solid #F1132A;height:1px;width:50%;margin:0px 0px 0px 0px' > &nbsp;</td></tr></table><br />"
                    mensaje = mensaje + "<table border='0' width='90%' cellpadding='0' cellspacing='0'><tr><td>"
                    mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:center;'><img src='//intranet.usat.edu.pe/campusestudiante/assets/images/logousat.png' width='100' height='100' ></div>"
                    mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:center;'></td></tr></table>"
                    mensaje = mensaje + "<table border='0' width='90%' cellpadding='0' cellspacing='0'><tr><td>"
                    mensaje = mensaje + "<div style='margin:0 auto;text-align:center;color:gray;font-family:Calibri '><b>Oficina de Grados y Titulos</b></div><br /></td></tr></table>"
                    mensaje = mensaje + "<table border='0' width='90%' cellpadding='0' cellspacing='0'><tr><td>"
                    mensaje = mensaje + "<div style='text-align:center;font-size:11px;color:gray;font-family:Calibri '><div>Av. San Josemaría Escrivá de Balaguer Nº 855 Chiclayo - Perú | Teléfono: 606200 - anexo: 1013"
                    mensaje = mensaje + "<a href='mailto:pdiaz@usat.edu.pe' style='color:gray;text-decoration:none;' target='_blank'><br/><b>pdiaz@usat.edu.pe</b></a></div> "
                    mensaje = mensaje + "<div style='font-family:Calibri'>© Copyright " + Year(Now()).ToString + ": USAT - Todos los derechos reservados</div>"
                    mensaje = mensaje + "</td></tr></table></div></body></html>"

                    objemail.EnviarMailCCO("campusvirtual@usat.edu.pe", "Inscripción de diploma de Grado Académico de Bachiller", "campusvirtual@usat.edu.pe", AsuntoCorreo, mensaje, True, receptor, "pdiaz@usat.edu.pe")

                    dtr = objGyt.ActualizarEstadoInformarInscripcionSUNEDU(dt.Rows(k).Item("codigos").ToString)
                Next

                Dim dtc As New Data.DataTable
                dtc = objGyt.ConsultarInformarInscripcionSUNEDU(Me.cboSesion.SelectedValue, Me.cboTipoDenominacion.SelectedValue, Me.txtBusqueda.Text)
                Me.gvAlumnos.DataSource = dtc
                Me.gvAlumnos.DataBind()

                Me.divMensaje.InnerHtml = "Envío de correo(s) realizado correctamente."
                Me.divMensaje.Attributes.Add("Class", "alert alert-success")
            Else
                Me.divMensaje.InnerHtml = "Debe Seleccionar al menos una Fila."
                Me.divMensaje.Attributes.Add("Class", "alert alert-danger")
            End If
        Catch ex As Exception
            Me.divMensaje.InnerHtml = ex.Message.ToString
            Me.divMensaje.Attributes.Add("Class", "alert alert-danger")
        End Try
    End Sub

    Protected Sub gvAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAlumnos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim email As String = DataBinder.Eval(e.Row.DataItem, "email_alu").ToString()
            'e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC0CB")
            If email = "" Or validarEmail(email) = False Then
                e.Row.Cells(0).ForeColor = Drawing.Color.Red
                e.Row.Cells(1).ForeColor = Drawing.Color.Red
                e.Row.Cells(2).ForeColor = Drawing.Color.Red
                e.Row.Cells(3).ForeColor = Drawing.Color.Red
                e.Row.Cells(4).ForeColor = Drawing.Color.Red
                e.Row.Cells(5).ForeColor = Drawing.Color.Red
                e.Row.Font.Bold = True
            End If
        End If
    End Sub

    Function validarEmail(ByVal email As String) As Boolean
        Try
            Dim estructura As String = "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$"
            Dim match As Match = Regex.Match(email.Trim(), estructura, RegexOptions.IgnoreCase)

            If match.Success Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function

End Class
