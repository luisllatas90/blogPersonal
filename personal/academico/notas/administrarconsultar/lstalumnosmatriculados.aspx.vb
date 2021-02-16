Imports System.Collections.Generic

Partial Class academico_notas_administrarconsultar_lstalumnosmatriculados
    Inherits System.Web.UI.Page

    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles")

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message.ToString.Replace("'", "") & "','" & type & "');</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../ErrorSistema.aspx")
        End If
        If IsPostBack = False Then
            If Session("cup") Is Nothing Then
                ShowMessage("Curso no encontrado", MessageType.Error)
            Else
                Me.HdCodigoCup.Value = Session("cup")
                VerificaSiEsRecuperacion()
                VerificaSiEsFormacionComplementaria() 'andy.diaz 31/07/2020

                'Yperez
                Session("Ncodigo_aut") = Not_ConsultarAutorizacion()
                '20200730-ENevado ------------------------------------------------------------
                Session("rec_cod_test") = fc_Recuperar_Codigo_test(Session("cup"))
                If CInt(Session("rec_cod_test")) = 2 Then
                    Me.btnDescargarActa.visible = True
                Else
                    Me.btnDescargarActa.visible = False
                End If
                ' ------------------------------------------------------------------------------

                If VerificaCursoDocente() = True Then
                    If ValidaAcceso() = True Then
                        Me.chkConfirmarPublicacion.Visible = True : Me.chkConfirmarPublicacion.checked = False '20200730-ENevado
                        Me.btnGuardar.Visible = True
                        Me.btnGuardar.enabled = False
                    Else
                        Me.chkConfirmarPublicacion.Visible = False : Me.chkConfirmarPublicacion.checked = False '20200730-ENevado
                        Me.btnGuardar.Visible = False
                    End If
                    ListaAlumnos()
                Else
                    ShowMessage("Ud. no es el docente del curso", MessageType.Error)
                End If

                'andy.diaz 31/07/2020
                If HdEsFormacionComplementaria.Value = "S" Then
                    Me.btnDescargarActa.Visible = False 'No debe mostrarse el botón de descarga para formación complementaria
                    Me.chkConfirmarPublicacion.Visible = False
                    Me.btnGuardar.Enabled = True
                    Me.hTitulo.InnerText = "REGISTRO DE ACREDITACIÓN DE ACTIVIDADES DE FORMACIÓN COMPLEMENTARIA"
                    Me.btnInhabilitados.Visible = False
                Else
                    Me.hTitulo.InnerText = "REGISTRO DE NOTAS"
                    Me.btnInhabilitados.Visible = True
                End If
                '---
            End If
        Else
            mt_RefreshGrid() '20200729-ENevado
        End If
    End Sub

    Private Sub VerificaSiEsRecuperacion()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_EsCursoRecuperacion", Me.HdCodigoCup.Value)
            obj.CerrarConexion()

            If (dt.Rows.Count > 0) Then
                Me.HdEsRecuperacion.Value = "S"
            Else
                Me.HdEsRecuperacion.Value = "N"
            End If

        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    'andy.diaz 31/07/2020
    Private Sub VerificaSiEsFormacionComplementaria()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_EsCursoFormacionComplementaria", Me.HdCodigoCup.Value)
            obj.CerrarConexion()

            If (dt.Rows.Count > 0) Then
                Me.HdEsFormacionComplementaria.Value = "S"
            Else
                Me.HdEsFormacionComplementaria.Value = "N"
            End If

        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub
    '--

    Private Function VerificaCursoDocente() As Boolean
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_VerificaCursoDocente", Me.HdCodigoCup.Value, Session("id_perNota"))
            'dt = obj.TraerDataTable("ValidarCargaProfesor", Session("id_per"), Me.HdCodigoCup.Value)
            obj.CerrarConexion()

            If dt.Rows.Count = 0 Then
                Return False
            End If

            Return True
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Return False
        End Try
    End Function

    Private Function ValidaAcceso() As Boolean
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("NOT_VerificaAccesoNotas", Me.HdCodigoCup.Value, Session("id_perNota"))
            obj.CerrarConexion()            

            If dt.Rows.Count = 0 Then
                ShowMessage("No se pudo validar el acceso", MessageType.Error)
                Return False
            Else
                Me.lblCurso.Text = "(" & dt.Rows(0).Item("descripcion_cac") & ")  " & dt.Rows(0).Item("nombre_cur") & " - Grupo: " & dt.Rows(0).Item("grupohor_cup")
                Me.lblDocente.Text = dt.Rows(0).Item("nombre_per")
                Me.HdEstadoCurso.Value = dt.Rows(0).Item("estadoNota_Cup")
                spAprobados.InnerText = dt.Rows(0).Item("Aprobados")
                spDesaprobados.InnerText = dt.Rows(0).Item("Desaprobados")
                spRetirados.InnerText = dt.Rows(0).Item("Retirados")
                spInhabilitados.InnerText = dt.Rows(0).Item("Inhabilitados")

                'andy.diaz 31/07/2020
                If HdEsFormacionComplementaria.Value = "S" Then
                    spTextAprobados.InnerHtml = "Acreditan"
                    spTextDesaprobados.InnerHtml = "No Acreditan"
                Else
                    spTextAprobados.InnerHtml = "Aprobados"
                    spTextDesaprobados.InnerHtml = "Desaprobados"
                End If
                '---

                If CInt(Session("Ncodigo_aut")) <= 0 Then
                    If dt.Rows(0).Item("mensajeProfesor").ToString.Trim <> "" Then
                        ShowMessage(dt.Rows(0).Item("mensajeProfesor"), MessageType.Error)
                        Return False
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Return False
        End Try
    End Function

    Private Sub ListaAlumnos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_ListaAlumnosMatriculadosxCup", Me.HdCodigoCup.Value)
            obj.CerrarConexion()

            Me.gvAlumnos.DataSource = dt
            Me.gvAlumnos.DataBind()

            'andy.diaz 31/07/2020
            If HdEsFormacionComplementaria.Value = "S" Then
                gvAlumnos.HeaderRow.Cells(5).Text = "ACREDITA"
                gvAlumnos.Columns(6).Visible = False
            Else
                gvAlumnos.HeaderRow.Cells(5).Text = "NOTA FINAL"
                gvAlumnos.Columns(6).Visible = True
            End If
            '---

        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub    

    Protected Sub gvAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAlumnos.RowDataBound
        Try
            If (e.Row.RowType.ToString = "DataRow") Then
                If gvAlumnos.DataKeys(e.Row.RowIndex).Values(1) = 0 Then
                    If e.Row.Cells(7).Text = "M" Or e.Row.Cells(7).Text = "Matriculado" Then
                        e.Row.Cells(7).Text = "Matriculado"

                        'andy.diaz 31/07/2020
                        If HdEsFormacionComplementaria.Value = "S" Then
                            e.Row.Cells(7).Text = "Inscrito"
                        End If

                    ElseIf e.Row.Cells(7).Text = "R" Or e.Row.Cells(7).Text = "Retirado" Then
                        e.Row.Cells(7).Text = "Retirado"
                        e.Row.Cells(5).Text = "-"
                        e.Row.Cells(5).Enabled = False
                        e.Row.Cells(9).Text = ""
                    End If
                Else
                    e.Row.Cells(7).Text = "Inhabilitado"
                    e.Row.Cells(5).Text = "-"
                    e.Row.Cells(5).Enabled = False
                    e.Row.Cells(9).Text = ""
                End If


                If CInt(Session("Ncodigo_aut")) = 0 Then
                    e.Row.Cells(9).Text = ""
                Else
                    Me.btnGuardar.Visible = False
                End If

                'andy.diaz 31/07/2020
                If HdEsFormacionComplementaria.Value = "S" Then
                    If e.Row.DataItem IsNot Nothing Then
                        If e.Row.Cells(7).Text = "Inscrito" Then
                            Dim lblAcredita As Label = e.Row.Cells(5).FindControl("lblAcredita")
                            If e.Row.DataItem("condicion_dma").ToString = "Acredita" Then
                                lblAcredita.Text = "SI"
                            Else
                                lblAcredita.Text = "NO"
                            End If

                            Dim chkAcredita As HtmlInputCheckBox = e.Row.Cells(5).FindControl("chkAcredita")

                            If HdEstadoCurso.Value = "R" Then
                                chkAcredita.Visible = False
                                lblAcredita.Visible = True

                                If e.Row.DataItem("condicion_dma").ToString = "Acredita" Then
                                    lblAcredita.ForeColor = System.Drawing.Color.Blue
                                Else
                                    lblAcredita.ForeColor = System.Drawing.Color.Red
                                End If
                            Else
                                chkAcredita.Visible = True
                                lblAcredita.Visible = False
                            End If
                        End If
                    End If
                Else '---
                    If Me.btnGuardar.Visible = False Then
                        Dim tx As TextBox = e.Row.Cells(5).FindControl("txtNota")
                        If e.Row.Cells(7).Text = "Matriculado" Then
                            tx.ReadOnly = True
                            tx.BorderStyle = BorderStyle.None
                        End If

                    Else
                        Dim tx As TextBox = e.Row.Cells(5).FindControl("txtNota")
                        If e.Row.Cells(7).Text = "Matriculado" Then
                            If tx.Text.ToString = "0.00" Then
                                tx.Text = "0"
                            End If
                        End If

                    End If
                End If

                e.Row.Cells(1).Text = e.Row.RowIndex + 1

                If e.Row.Cells(6).Text = "Retirado" Then
                    e.Row.Cells(6).ForeColor = System.Drawing.Color.Black
                ElseIf e.Row.Cells(6).Text = "Desaprobado" Then
                    e.Row.Cells(6).ForeColor = System.Drawing.Color.Red
                    e.Row.Cells(5).ForeColor = System.Drawing.Color.Red
                ElseIf e.Row.Cells(6).Text = "Aprobado" Then
                    e.Row.Cells(6).ForeColor = System.Drawing.Color.Blue
                    e.Row.Cells(5).ForeColor = System.Drawing.Color.Blue
                End If

                If e.Row.Cells(7).Text = "Inhabilitado" Then
                    e.Row.Cells(7).ForeColor = System.Drawing.Color.Orange
                ElseIf e.Row.Cells(7).Text = "Retirado" Then
                    e.Row.Cells(7).ForeColor = System.Drawing.Color.Black
                ElseIf e.Row.Cells(7).Text = "Matriculado" Then
                    e.Row.Cells(7).ForeColor = System.Drawing.Color.Green
                ElseIf e.Row.Cells(7).Text = "Inscrito" Then
                    e.Row.Cells(7).ForeColor = System.Drawing.Color.Green
                End If
            End If
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message, MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            If ValidarDatos() = True Then
                Dim nota_dma As Double = 0
                Dim condicion_dma As String = ""
                Dim swRegNotas As Boolean = False
                Dim txtNota As TextBox
                Dim chkAcredita As HtmlInputCheckBox

                For i As Integer = 0 To Me.gvAlumnos.Rows.Count - 1

                    If gvAlumnos.Rows(i).Cells(7).Text = "Matriculado" OrElse gvAlumnos.Rows(i).Cells(7).Text = "Inscrito" Then
                        swRegNotas = True

                        'andy.diaz 31/07/2020
                        If HdEsFormacionComplementaria.Value = "S" Then
                            chkAcredita = gvAlumnos.Rows(i).FindControl("chkAcredita")

                            If chkAcredita.Checked Then
                                condicion_dma = "A"
                            Else
                                condicion_dma = "N"
                            End If

                            obj.AbrirConexion()
                            obj.Ejecutar("ACAD_ActualizarCondicionFormacionComplementaria", Session("id_per") _
                                         , gvAlumnos.DataKeys(i).Values("codigo_dma"), condicion_dma, Session("id_per"), _
                                         "Registro de Notas", "0", "", "0")
                            obj.CerrarConexion()
                        Else '---
                            txtNota = TryCast(gvAlumnos.Rows(i).FindControl("txtNota"), TextBox)
                            nota_dma = txtNota.Text
                            If nota_dma >= 14 Then
                                condicion_dma = "A"
                            Else
                                condicion_dma = "D"
                            End If

                            obj.AbrirConexion()
                            obj.Ejecutar("ActualizarNotaEstudiante", Session("id_per") _
                                         , gvAlumnos.DataKeys(i).Values("codigo_dma"), nota_dma _
                                         , condicion_dma, Session("id_per"), "Registro de Notas", "")
                            obj.CerrarConexion()
                        End If
                    End If
                Next

                If swRegNotas = True Then
                    obj.AbrirConexion()
                    obj.Ejecutar("ACAD_CierraCursoProgramado", Me.HdCodigoCup.Value)
                    obj.CerrarConexion()
                    'ShowMessage("Notas Registradas", MessageType.Success)
                    Me.btnGuardar.Visible = False

                    '20200729-ENevado --------------------------------------------------------------------------------------------------
                    Me.chkConfirmarPublicacion.Visible = False : Me.chkConfirmarPublicacion.Checked = False
                    If CInt(Session("rec_cod_test")) = 2 AndAlso HdEsFormacionComplementaria.Value <> "S" Then 'andy.diaz 31/07/2020: No generar documento para formación complementaria
                        Dim _idRegistroNota As Integer = 0
                        'Dim codigo_dot As Integer      '' es lo que se va obtener a invocar el metododo generarDocumentoPdf y va servir para descargar doucmento luego
                        Dim codigo_cda As Integer = 8  ''-- Configuracion del documento obligatorio en este caso es 8
                        Dim serieCorrelativoDoc As String ''--- Este es el correlativo o la numeración del documento a utilizar
                        ''''**** 1. GENERA CORRELATIVO DEL DOCUMNETO CONFIGURADO *******************************************************
                        serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorCda(codigo_cda, Year(Now), Session("id_per")) '' Se obtiene el correlativo
                        ''''******* GENERA DOCUMENTO PDF *****************************************************************************
                        If serieCorrelativoDoc <> "" Then
                            '--------necesarios
                            Dim arreglo As New Dictionary(Of String, String)
                            arreglo.Add("nombreArchivo", "ActaEvaluacion") '' nombre del documento tal como está
                            arreglo.Add("sesionUsuario", Session("perlogin").ToString) '---- ejemplo: USAT\LLLONTOP
                            '-----------------                
                            arreglo.Add("codigo_cup", Session("cup"))  ''Aqui se debe enviar el codigo del curso programado codigo_cup en ese caso
                            '********2. GENERA DOCUMENTO PDF **************************************************************
                            _idRegistroNota = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo) '' aqui se obtiene el codigo_dot para usarlo despues en la descarga del documento
                            '**********************************************************************************************
                        End If
                        obj.AbrirConexion()
                        obj.Ejecutar("DEA_DetalleMatricula_PromedioFinal", "3", Session("cup"), 0, -1, Session("id_per"), _idRegistroNota)
                        obj.CerrarConexion()
                    End If

                    'andy.diaz 31/07/2020
                    Dim mensaje As String = ""
                    If HdEsFormacionComplementaria.Value = "S" Then
                        mensaje = "Acreditación registrada "
                    Else
                        mensaje = "Notas Registradas "
                    End If

                    ShowMessage(mensaje, MessageType.Success) '---
                    ' --------------------------------------------------------------------------------------------------------------------

                    If HdEsFormacionComplementaria.Value = "S" Then
                        ValidaAcceso()
                        mt_RefreshGrid()
                    Else
                        For i As Integer = 0 To Me.gvAlumnos.Rows.Count - 1
                            txtNota = TryCast(gvAlumnos.Rows(i).FindControl("txtNota"), TextBox)
                            txtNota.Enabled = False
                            txtNota.ReadOnly = True
                            txtNota.BorderStyle = BorderStyle.None
                        Next
                    End If
                Else
                    ShowMessage("No se pudo registrar", MessageType.Error)
                End If

                ListaAlumnos()


                'Response.Write("<script>alert('Notas registradas');</script>")
                'Response.Redirect("../profesor/miscursos.aspx?id=" & Session("id_per") & "&ctf=" & Session("ctf_notas") & "&")

                obj = Nothing
            End If
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Public Function ValidarDatos() As Boolean
        For i As Integer = 0 To Me.gvAlumnos.Rows.Count - 1

            'andy.diaz 31/07/2020
            If gvAlumnos.Rows(i).Cells(7).Text = "Matriculado" OrElse gvAlumnos.Rows(i).Cells(7).Text = "Inscrito" Then
                If HdEsFormacionComplementaria.Value = "S" Then
                    Dim chkAcredita As HtmlInputCheckBox = TryCast(gvAlumnos.Rows(i).FindControl("chkAcredita"), HtmlInputCheckBox)
                Else '---
                    Dim txtNota As TextBox = TryCast(gvAlumnos.Rows(i).FindControl("txtNota"), TextBox)
                    If ParseaNumero(txtNota.Text) = False Then
                        ShowMessage("Nota no válida en " & gvAlumnos.Rows(i).Cells(4).Text, MessageType.Error)
                        Return False
                    End If

                    If txtNota.Text > 14 And HdEsRecuperacion.Value = "S" Then
                        ShowMessage("La nota no puede exceder 14", MessageType.Error)
                        Return False
                    End If
                End If
            End If
        Next

        Return True
    End Function

    Public Function ParseaNumero(ByVal dato As String) As Boolean
        Try
            Integer.Parse(dato)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("../profesor/miscursos.aspx?id=" & Session("id_perNota") & "&ctf=" & Session("ctf_notas"))
    End Sub

    Private Function Not_ConsultarAutorizacion() As Integer
        'Yperez
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("NOT_ConsultarRegistroNotas", 1, Me.HdCodigoCup.Value, Session("id_perNota"))
            obj.CerrarConexion()

            If dt.Rows.Count = 0 Then
                Return 0
            Else
                Return dt.Rows(0).Item("codigo_aut").toString
            End If

            Return True
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Return False
        End Try

    End Function

    Protected Sub gvAlumnos_RowCommand1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAlumnos.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "ModificarNota") Then
            Dim txtNota As TextBox
            txtNota = CType(gvAlumnos.Rows(index).FindControl("txtNota"), TextBox)
            Page.RegisterStartupScript("Pop", "<script>openModal('myModal');</script>")
            Me.lblCodigo.Text = Me.gvAlumnos.Rows(index).Cells(2).Text
            Me.lblNombre.Text = Me.gvAlumnos.Rows(index).Cells(4).Text
            Me.lblNotaAnterior.text = FormatNumber(txtNota.text, 0)
            Me.lblCondicionAnterior.text = Me.gvAlumnos.Rows(index).Cells(6).Text
            Session("Ncodigo_dma") = gvAlumnos.DataKeys(index).Values("codigo_dma")
            'Cargar la Foto
            Dim ruta As String
            Dim obEnc As Object
            obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")

            ruta = obEnc.CodificaWeb("069" & Me.gvAlumnos.Rows(index).Cells(2).Text)
            ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
            Me.FotoAlumno.ImageUrl = ruta
            obEnc = Nothing

            Me.txtNotaNueva.focus()
        End If
    End Sub

    Protected Sub btnGuardarCambio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarCambio.Click
        If Me.txtNotaNueva.Text.Trim = "" Then
            ShowMessage("Falta ingresar la nueva nota.", MessageType.Error)
        ElseIf Me.txtMotivo.Text.Trim = "" Then
            ShowMessage("Debe ingresar el motivo.", MessageType.Error)
        Else
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("ActualizarNotaEstudiante", Session("Ncodigo_aut"), Session("Ncodigo_dma"), Me.txtNotaNueva.Text, IIf(CInt(Me.txtNotaNueva.Text) < 14, "D", "A"), Session("id_per"), Me.txtMotivo.Text, "")
            obj.CerrarConexion()
        End If
    End Sub

    Protected Sub chkConfirmarPublicacion_ChekedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            btnGuardar.Enabled = chkConfirmarPublicacion.checked
        Catch ex As Exception
            ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    '20200729-ENevado
    Protected Sub btnDescargarActa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDescargarActa.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            Dim _idRegistroNota As Integer = -1
            obj.AbrirConexion()
            dt = obj.TraerDataTable("CursoProgramado_Listar", "AN", Session("cup"), -1, -1, -1)
            obj.CerrarConexion()

            If dt.rows.count > 0 Then
                _idRegistroNota = dt.rows(0).item(0)
            End If

            If _idRegistroNota <> -1 Then
                mt_DescargarArchivo(_idRegistroNota, 30, "3N23G777FS")
            Else
                Throw New exception("¡ No se encontro el registro de Acta de Notas !")
            End If
        Catch ex As Exception
            ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    '20200729-ENevado
    Private Sub mt_DescargarArchivo(ByVal IdArchivo As Long, ByVal idTabla As Integer, ByVal token As String)
        Try
            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Dictionary(Of String, String)
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim usuario As String = Session("perlogin")

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, idTabla, IdArchivo, token)
            obj.CerrarConexion()
            If tb.Rows.Count = 0 Then Throw New Exception("¡ Archivo no encontrado !")

            list.Add("IdArchivo", tb.Rows(0).Item("IdArchivo").ToString)
            list.Add("Usuario", usuario)
            list.Add("Token", token)

            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", usuario)
            Dim imagen As String = fc_ResultFile(result)

            If tb.Rows.Count > 0 Then
                Dim extencion As String
                extencion = tb.Rows(0).Item("Extencion")
                Select Case tb.Rows(0).Item("Extencion")
                    Case ".txt"
                        extencion = "text/plain"
                    Case ".doc"
                        extencion = "application/ms-word"
                    Case ".xls"
                        extencion = "application/vnd.ms-excel"
                    Case ".gif"
                        extencion = "image/gif"
                    Case ".jpg"
                    Case ".jpeg"
                    Case "jpeg"
                        extencion = "image/jpeg"
                    Case "png"
                        extencion = "image/png"
                    Case ".bmp"
                        extencion = "image/bmp"
                    Case ".wav"
                        extencion = "audio/wav"
                    Case ".ppt"
                        extencion = "application/mspowerpoint"
                    Case ".dwg"
                        extencion = "image/vnd.dwg"
                    Case ".pdf"
                        extencion = "application/pdf"
                    Case Else
                        extencion = "application/octet-stream"
                End Select

                Dim bytes As Byte() = Convert.FromBase64String(imagen)
                Response.Clear()
                Response.Buffer = False
                Response.Charset = ""
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = extencion
                Response.AddHeader("content-disposition", "attachment;filename=" & tb.Rows(0).Item("NombreArchivo").ToString.Replace(",", ""))
                Response.AppendHeader("Content-Length", bytes.Length.ToString())
                Response.BinaryWrite(bytes)
                Response.End()
            End If
            'Response.Write(envelope)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    '20200729-ENevado
    Function fc_ResultFile(ByVal cadXml As String) As String
        Try
            Dim xError As String()
            Dim nsMgr As System.Xml.XmlNamespaceManager
            Dim xml As System.Xml.XmlDocument = New System.Xml.XmlDocument()
            xml.LoadXml(cadXml)
            nsMgr = New System.Xml.XmlNamespaceManager(xml.NameTable)
            nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim res As System.Xml.XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body", nsMgr)
            xError = res.InnerText.Split(":")

            If xError.Length = 2 Then
                Throw New Exception(res.InnerText)
            End If

            Return res.InnerText
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '20200729-ENevado
    Private Sub mt_RefreshGrid()
        Try
            For Each _Row As GridViewRow In Me.gvAlumnos.Rows
                gvAlumnos_RowDataBound(Me.gvAlumnos, New GridViewRowEventArgs(_Row))
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    '20200730-ENevado
    Function fc_Recuperar_Codigo_test(ByVal cod_cup As Integer) As Integer
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            Dim _codigo_test As Integer = -1
            obj.AbrirConexion()
            dt = obj.TraerDataTable("CursoProgramado_Listar", "AN", cod_cup, -1, -1, -1)
            obj.CerrarConexion()
            If dt.rows.count > 0 Then
                _codigo_test = dt.rows(0).item(1)
            End If
            Return _codigo_test
        Catch ex As Exception
            ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Function


End Class
