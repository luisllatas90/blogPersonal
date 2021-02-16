Imports System.IO
Imports System.Security.Cryptography
Imports System.Drawing

Partial Class administrativo_tramite_consultapublica_frmConsultarTramiteVirtual
    Inherits System.Web.UI.Page

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        cpf.Value = Me.ddlEscuela.SelectedValue

        If cpf.Value = "" Then
            cpf.Value = 0
        End If
        fnMostrarEvaluar(False)
        Me.txtnroTramite.Attributes("placeholder") = "TRL-" & Date.Now.Year.ToString & "-####"

        Dim obj As New ClsConectarDatos
        Dim objFun As New ClsFunciones
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.ddlEscuela.Items.Clear()
        Me.ddlEscuela.DataBind()
        'objFun.CargarListas(Me.ddlEscuela, obj.TraerDataTable("ConsultarEscuelaPorPersonal", "3", CInt(Session("id_per").ToString()), 2), "codigo_Cpf", "nombre_Cpf")
        objFun.CargarListasGrupo(Me.ddlEscuela, obj.TraerDataTable("ConsultarEscuelaPorPersonal", "3", CInt(Session("id_per").ToString()), 2), "codigo_Cpf", "nombre_Cpf", "TipoEstudio")
        Page.RegisterStartupScript("Combo", "<script> GroupDropdownlist();</script>")
        obj.CerrarConexion()
        obj = Nothing
        objFun = Nothing

    End Sub

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub


#Region "Eventos"
    'Protected Sub gvFlujo_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvFlujo.RowDataBound

    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        'Response.Write(e.Row.Cells(2).Text)
    '        If e.Row.Cells(0).Text.ToString.Trim = "" Then
    '            e.Row.Cells(0).ForeColor = Drawing.Color.Red
    '            e.Row.Cells(1).ForeColor = Drawing.Color.Red
    '            e.Row.Cells(2).ForeColor = Drawing.Color.Red
    '            e.Row.Cells(3).ForeColor = Drawing.Color.Red
    '            e.Row.ForeColor = Drawing.Color.Red
    '        End If

    '    End If
    'End Sub

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(2).Text.ToString.Trim.Contains("Pendiente") Or e.Row.Cells(2).Text.ToString.Trim.Contains("Observado") Then

                e.Row.Cells(0).ForeColor = Drawing.Color.Red
                e.Row.Cells(1).ForeColor = Drawing.Color.Red
                e.Row.Cells(2).ForeColor = Drawing.Color.Red
                e.Row.Cells(3).ForeColor = Drawing.Color.Red
            Else


            End If
        End If
    End Sub

    Protected Sub gvDatos_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDatos.PreRender
        If gvDatos.Rows.Count > 0 Then
            gvDatos.UseAccessibleHeader = True
            gvDatos.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        CargaDatos()
    End Sub


    Protected Sub gvDatos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDatos.RowCommand
        Try

            ' Response.Write(e.CommandArgument)
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Response.Write(e.CommandName)
            If (e.CommandName = "Evaluar") Then
                Dim codigo_dta As Integer = 0
                hddtareq.Value = Encriptar(gvDatos.DataKeys(index).Values("codigo_dta").ToString)
                codigo_dta = gvDatos.DataKeys(index).Values("codigo_dta")
                Dim fechaPago As String = ""
                If Not IsDBNull(gvDatos.DataKeys(index).Values("fecha_cin")) Then
                    fechaPago = gvDatos.DataKeys(index).Values("fecha_cin")
                End If
                Me.lblSemestreMatriculado.Visible = False
                Me.lblTotalSemestre.Visible = False
				Me.lblInfoAdicional.Visible = False


                Dim codigoAlu As Integer = 0
                codigoAlu = gvDatos.DataKeys(index).Values("codigo_Alu")

                Dim codigoUniver As String = ""
                codigoUniver = gvDatos.DataKeys(index).Values("codigoUniver_Alu")

                Dim codigo_trl As Integer = 0
                codigo_trl = gvDatos.DataKeys(index).Values("codigo_trl")

                Dim tramite As String = ""
                tramite = gvDatos.DataKeys(index).Values("descripcion_ctr")

                Dim fechaTramite As String = ""
                fechaTramite = gvDatos.DataKeys(index).Values("fechaReg_trl").ToString

                fnMostrarEvaluar(True)
                ' fnInformacionTramite(CInt(gvDatos.DataKeys(index).Values("codigo_dta").ToString))
                fnInformacionTramite(CInt(gvDatos.DataKeys(index).Values("codigo_dta").ToString))
                accionURL(codigoUniver, codigoAlu, "", codigo_trl, codigo_dta, tramite, fechaPago, fechaTramite)
                MostrarInformacionEvaluadores(codigo_dta)
            End If
        Catch ex As Exception
            Response.Write("Error gvDatos_RowCommand: " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Protected Sub gDatosAdicional_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gDatosAdicional.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Cells(0).Font.Bold = True
            e.Row.Cells(0).BackColor = Drawing.Color.AliceBlue

            e.Row.Cells(1).Font.Size = 10
            e.Row.Cells(1).BackColor = Drawing.Color.Linen
            e.Row.Cells(2).Font.Size = 10
            e.Row.Cells(2).BackColor = Drawing.Color.Linen
            If e.Row.Cells(0).Text.Contains("ARCHIVO") Then
                Dim myLink As HyperLink = New HyperLink()
                myLink.NavigateUrl = "javascript:void(0)"
                myLink.Text = "Descargar"
                myLink.CssClass = "btn btn-primary"
                myLink.Attributes.Add("onclick", "DescargarArchivo('" & e.Row.Cells(1).Text & "')")


                e.Row.Cells(1).Controls.Add(myLink)
            End If

        End If
    End Sub

    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        fnMostrarEvaluar(False)
    End Sub


#End Region


#Region "Procedimiento"
    Private Sub accionURL(ByVal codigoUniversitario As String, ByVal codigoAlu As Integer, ByVal DetTipo As String, ByVal codigo_trl As Integer, ByVal codigo_dta As Integer, ByVal tramite As String, ByVal fechaPago As String, ByVal fechaTramite As String)
        Try
            ddlCiclo.Items.Clear()
            ifrAccion.Visible = True

            SelectCiclosMatriculados(codigoAlu)
            MostrarInformacionAdicional(codigo_trl, codigo_dta)
            Me.lblTramite.Text = tramite
            Me.lblFechaPago.Text = fechaPago
            Me.lblFechaTramite.Text = fechaTramite
            fnVerUltimaAsistencia(codigo_dta)

            If DetTipo = "" Then
                Me.ddlCiclo.Visible = False

            Else
                Me.lblSemestreMatriculado.Visible = True
            End If

        Catch ex As Exception
            Response.Write("accionURL " & ex.Message & "-" & ex.StackTrace)
        End Try

    End Sub
    Private Sub fnVerUltimaAsistencia(ByVal codigo_dta As Integer)
        Try
            'Response.Write("fnVerCursosUltimaAsistencia")
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            Dim codigo_alu As Integer = 0
            'codigo_alu = CInt(codigoAlu)
            Dim codigo_cac As Integer = 0
            If ddlCiclo.SelectedIndex > -1 Then
                codigo_cac = CInt(Me.ddlCiclo.SelectedValue.ToString)
            End If
            'Response.Write("cac: " & codigo_cac)
            obj.AbrirConexion()
            'dt = obj.TraerDataTable("TRL_BuscaTramiteLinea", Me.txtAlumno.Text.Replace(" ", "%"), Me.cboEstado.SelectedValue, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"))
            dt = obj.TraerDataTable("TRL_MatriculaAsistenciaRpte", "1", codigo_alu, codigo_cac, codigo_dta)

            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                Me.ifrRetCiclo.Visible = True
                Me.txtUltimaFechaAsistencia.Text = dt.Rows(0).Item("fecha").ToString()
            Else
                Me.ifrRetCiclo.Visible = False
                Me.txtUltimaFechaAsistencia.Text = ""
            End If


            dt = Nothing

        Catch ex As Exception
            Response.Write("2." & ex.Message)
        End Try

    End Sub
    Private Sub SelectCiclosMatriculados(ByVal codigo_alu As Integer)

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim objFun As New ClsFunciones

        objFun.CargarListas(Me.ddlCiclo, obj.TraerDataTable("ConsultarMatricula", "35", codigo_alu, "0", "0"), "codigo_Cac", "descripcion_Cac")
        obj.CerrarConexion()
        obj = Nothing

    End Sub

    Private Sub MostrarInformacionAdicional(ByVal codigo_trl As Integer, ByVal codigo_dta As Integer)
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_TramiteAdicionalInfo", "1", codigo_trl, codigo_dta)
            obj.CerrarConexion()

            Me.gDatosAdicional.DataSource = dt
            Me.gDatosAdicional.DataBind()
			
			If dt.Rows.Count > 0 Then
                Me.lblInfoAdicional.Visible = True
            Else
                Me.lblInfoAdicional.Visible = False
            End If

            CalcularTotalesInformacionAdicional()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CalcularTotalesInformacionAdicional()
        Dim semestre As Integer = 0
        With gDatosAdicional
            For i As Integer = 0 To .Rows.Count - 1
                If .DataKeys(i).Values("tabla").ToString = "SEMESTRE" Then
                    semestre = semestre + 1
                End If

            Next
        End With
        If semestre > 0 Then
            lblNumSemestre.Text = semestre.ToString & " semestres académicos"
            Me.lblTotalSemestre.Visible = True
            Me.lblNumSemestre.Visible = True
        Else
            Me.lblTotalSemestre.Visible = False
            Me.lblNumSemestre.Visible = False
        End If

    End Sub

    Private Sub CargaDatos()
        If fnValidarFiltroBuscar() Then


            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Try
                obj.AbrirConexion()
                'dt = obj.TraerDataTable("TRL_BuscaTramiteLinea", Me.txtAlumno.Text.Replace(" ", "%"), Me.cboEstado.SelectedValue, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"))
                dt = obj.TraerDataTable("TRL_rpteconsultarTramite", "1", Me.txtAlumno.Text.Replace(" ", "%"), Me.txtnroTramite.Text, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"), cpf.Value)
                obj.CerrarConexion()

                Me.gvDatos.DataSource = dt
                Me.gvDatos.DataBind()

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End If
    End Sub

    Private Function fnValidarFiltroBuscar() As Boolean
        Try
            If Me.txtAlumno.Text.Trim <> "" Or Me.txtnroTramite.Text.Trim <> "" Then
                Return True
            Else
                ShowMessage("Ingrese algun dato en los filtros de búsqueda: ", MessageType.Warning)
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub fnMostrarEvaluar(ByVal sw As Boolean)

        If sw Then
            Me.pnlLista.Visible = False
            Me.pnlRegistro.Visible = True
        Else
            Me.pnlLista.Visible = True
            Me.pnlRegistro.Visible = False
        End If

    End Sub

    Private Sub fnInformacionTramite(ByVal codigo_dta As Integer)
        'Response.Write(codigo_dta)

        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim str As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try





            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_informaciontramite", "2", codigo_dta)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then

                Me.lblInfEstNumero.Text = dt.Rows(0).Item("numero").ToString()
                Me.lblInfEstEscuela.Text = dt.Rows(0).Item("escuela").ToString()
                Me.lblInfEstAlumno.Text = dt.Rows(0).Item("estudiante").ToString()
                Me.lblInfEstEmail.Text = dt.Rows(0).Item("email").ToString()
                Me.lblInfEstTelefono.Text = dt.Rows(0).Item("telefonoMovil_Dal").ToString()
                Me.lblInfEstFechaNac.Text = dt.Rows(0).Item("fechaNacimiento_Alu").ToString()

                Me.lblInfEstDocIdent.Text = dt.Rows(0).Item("nroDocIdent_Alu").ToString()
                If dt.Rows(0).Item("estadoActual_Alu") = 1 Then
                    Me.lblInfEstEstado.Text = "Activo"
                Else
                    Me.lblInfEstEstado.Text = "Inactivo"
                End If

                If dt.Rows(0).Item("estadoDeuda_Alu").ToString() = "1" Then
                    Me.lblInfEstTieneDeuda.Text = "Si"
                Else
                    Me.lblInfEstTieneDeuda.Text = "No"
                End If

                If dt.Rows(0).Item("beneficio_Alu").ToString() = "1" Then
                    Me.lblInfEstBeneficio.Text = "Si"
                Else
                    Me.lblInfEstBeneficio.Text = "No"
                End If


                Me.lblInfEstRespPago.Text = dt.Rows(0).Item("PersonaApod_Dal").ToString()
                Me.lblInfEstDirRespPago.Text = dt.Rows(0).Item("direccionApod_Dal").ToString()
                Me.lblTramiteDescripcion.Text = dt.Rows(0).Item("observacion_trl").ToString()

            End If



        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            obj = Nothing
        End Try

    End Sub

    Private Sub MostrarInformacionEvaluadores(ByVal codigo_dta As Integer)
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_TramiteFlujo_Listar", "3", codigo_dta)
            obj.CerrarConexion()

            Me.gvFlujo.DataSource = dt
            Me.gvFlujo.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

#End Region


#Region "Funciones"
    Public Function Encriptar(ByVal Input As String) As String

        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Encoding.UTF8.GetBytes(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV

        Return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function

    Public Function Desencriptar(ByVal Input As String) As String
        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Convert.FromBase64String(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV
        Return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function
#End Region



End Class
